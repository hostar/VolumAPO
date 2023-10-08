using AudioSwitcher.AudioApi.CoreAudio;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Vanara.InteropServices;
using VolumAPO.Internals;
using VolumAPO.Models;
using static Vanara.PInvoke.User32;

namespace VolumAPO.Helpers
{
    public static class GlobalHelpers
    {
        [DllImport("VolumAPONative.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
        static extern IntPtr DllRegTakeOwnership(string key, string nameToWrite, string valueToWrite, bool writeAsMultiSz);


        private static int _currValue = 0;
        private static int _currValueBal = 0;
        private static Bitmap baseIconBitmap;

        public static NotifyIcon notifyIcon;
        public static VolumeControlForm volumeControlForm;
        public static OSDForm osdForm;
        public static SynchronizationContext synchronizationContextForm;

        public static int CurrentDeviceChannels = -1;

        public static string ApoGuid = "656BCCDE-BAD1-4BEF-960A-1FD82B332258";

        public static IntPtr VolumeControlFormHandle = IntPtr.Zero;

        public static int CurrentVolume
        {
            get
            {
                return _currValue;
            }
            set
            {
                _currValue = value;
                SetVolumeByValue();
            }
        }

        public static int CurrentBalance
        {
            get
            {
                return _currValueBal;
            }
            set
            {
                _currValueBal = value;
                //SetVolumeByValue();
            }
        }

        public static CoreAudioController CoreAudioControllerGlobal = new CoreAudioController();
        public static Dictionary<HotkeyEnum, Action> HotkeyDict { get; } = new Dictionary<HotkeyEnum, Action>();

        public enum HotkeyEnum
        {
            VolumeUp = 0,
            VolumeDown = 1,
            BalanceLeft = 2,
            BalanceRight = 3,
        }

        public static void SetHotkey(IntPtr handle, Hotkey hotkey, HotkeyEnum hotkeyId)
        {
            if (hotkey == null)
            {
                return;
            }
            // TODO: unregister previous hotkey
            var success = Vanara.PInvoke.User32.RegisterHotKey(handle, (int)hotkeyId, hotkey.GetVanaraHotkey(), (uint)hotkey.Key);
            if (!success)
            {
                //MessageBox.Show("Setting of hotkey failed.");
            }

            SetHotkeyConfiguration(hotkey, hotkeyId);
        }

        public static void SetHotkeyConfiguration(Hotkey hotkey, HotkeyEnum hotkeyId)
        {
            switch (hotkeyId)
            {
                case HotkeyEnum.VolumeUp:
                    Config.ConfigAccessor.VolumeUpHotkey = hotkey;
                    break;
                case HotkeyEnum.VolumeDown:
                    Config.ConfigAccessor.VolumeDownHotkey = hotkey;
                    break;
                case HotkeyEnum.BalanceLeft:
                    Config.ConfigAccessor.BalanceLeftHotkey = hotkey;
                    break;
                case HotkeyEnum.BalanceRight:
                    Config.ConfigAccessor.BalanceRightHotkey = hotkey;
                    break;
            }
        }

        public static void SetInitialConfiguration(IntPtr handle)
        {
            VolumeControlFormHandle = handle;
            SetHotkey(VolumeControlFormHandle, Config.ConfigAccessor.VolumeUpHotkey, HotkeyEnum.VolumeUp);
            SetHotkey(VolumeControlFormHandle, Config.ConfigAccessor.VolumeDownHotkey, HotkeyEnum.VolumeDown);
            SetHotkey(VolumeControlFormHandle, Config.ConfigAccessor.BalanceRightHotkey, HotkeyEnum.BalanceRight);
            SetHotkey(VolumeControlFormHandle, Config.ConfigAccessor.BalanceLeftHotkey, HotkeyEnum.BalanceLeft);
            SetWheelTaskbar();
        }

        public static void SetWheelTaskbar()
        {
            if (Config.ConfigAccessor.TaskBarScrollVolumeChange)
            {
                MouseHook.StartMouseHook();
            }
            else
            {
                MouseHook.StopMouseHook();
            }
        }

        public static void PopulateControls(SettingsForm settingsForm)
        {
            settingsForm.hotkeyBalanceLeft.Hotkey = Config.ConfigAccessor.BalanceLeftHotkey;
            settingsForm.hotkeyBalanceRight.Hotkey = Config.ConfigAccessor.BalanceRightHotkey;
            settingsForm.hotkeyVolumeDown.Hotkey = Config.ConfigAccessor.VolumeDownHotkey;
            settingsForm.hotkeyVolumeUp.Hotkey = Config.ConfigAccessor.VolumeUpHotkey;
            settingsForm.trackBarVolumeSpeed.Value = Config.ConfigAccessor.VolumeChangeStep;
            settingsForm.textBoxMaxVolume.Text = Config.ConfigAccessor.VolumeMax.ToString();
            settingsForm.checkBoxTaskBarScroll.Checked = Config.ConfigAccessor.TaskBarScrollVolumeChange;

            settingsForm.checkBoxOsdEnabled.Checked = Config.ConfigAccessor.OSDVisible;
            settingsForm.trackBarOsdHorPos.Value = Config.ConfigAccessor.OSDHorizontal;
            settingsForm.trackBarOsdVerPos.Value = Config.ConfigAccessor.OSDVertical;

            settingsForm.checkBoxUseApoBalance.Checked = Config.ConfigAccessor.UseApoForBalance;
            settingsForm.checkBoxUseApoVolume.Checked = Config.ConfigAccessor.UseApoForVolume;
        }

        public static bool SetVolumeUp()
        {
            if ((CurrentVolume + Config.ConfigAccessor.VolumeChangeStep) > Config.ConfigAccessor.VolumeMax)
            {
                return false;
            }

            CurrentVolume += Config.ConfigAccessor.VolumeChangeStep;
            ShowOSD(LastChange.Volume);
            volumeControlForm.SetVolumeTrackBar();

            notifyIcon.Icon.Dispose();
            notifyIcon.Icon = CreateIcon();
            return true;
        }

        public static bool SetVolumeDown()
        {
            if ((CurrentVolume - Config.ConfigAccessor.VolumeChangeStep) < 0)
            {
                return false;
            }

            CurrentVolume -= Config.ConfigAccessor.VolumeChangeStep;
            ShowOSD(LastChange.Volume);
            volumeControlForm.SetVolumeTrackBar();

            notifyIcon.Icon.Dispose();
            notifyIcon.Icon = CreateIcon();
            return true;
        }

        private static void SetVolumeByValue()
        {
            if (Config.ConfigAccessor.UseApoForVolume)
            {
                SetVolumeAPO(CurrentVolume);
            }
            else
            {
                CoreAudioControllerGlobal.DefaultPlaybackDevice.SetVolumeAsync(CurrentVolume);
            }

            if (notifyIcon.Icon != null)
            {
                notifyIcon.Icon.Dispose();
            }
            notifyIcon.Icon = CreateIcon();
        }

        public static bool RewriteNotifyIcon()
        {
            if ((CurrentVolume - Config.ConfigAccessor.VolumeChangeStep) < 0)
            {
                return false;
            }
            CurrentVolume -= Config.ConfigAccessor.VolumeChangeStep;
            return true;
        }

        public static Icon CreateIcon()
        {
            // Tahoma "Open Sans" "Calibri"
            Bitmap bitmap = new Bitmap(baseIconBitmap);
            var graphics = Graphics.FromImage(bitmap);
            var font = new Font("Verdana", 40 /*, FontStyle.Bold */);
            graphics.DrawString(GlobalHelpers.CurrentVolume.ToString(), font, new SolidBrush(Color.White), -10, -15);

            return IconHelper.ConvertToIcon(bitmap, false);
        }

        public static void CreateBaseBitmap()
        {
            baseIconBitmap = new Bitmap(100, 100);
            var brush = new SolidBrush(Color.FromArgb(0, 105, 210));

            var graphics = Graphics.FromImage(baseIconBitmap);
            graphics.FillPolygon(brush, new Point[]
            {
                new Point(0,  100),
                new Point(100, 100),
                new Point(100, 50)
            });
        }

        public static void ShowOSD(LastChange lastChange)
        {
            if (Config.ConfigAccessor.OSDVisible)
            {   // show OSD
                osdForm.Show();
                osdForm.Invalidate();
                osdForm.StartTimer(lastChange);
            }
        }

        static bool BackupRegistry()
        {
            int counter = 1;
            var backupFileName = "reg_backup";
            if (File.Exists($"{Directory.GetCurrentDirectory()}\\{backupFileName}_{counter}.reg"))
            {
                while (File.Exists($"{Directory.GetCurrentDirectory()}\\{backupFileName}_{counter}.reg"))
                {
                    counter++;
                    if (counter == 500)
                    {
                        System.Windows.Forms.MessageBox.Show("Too many registry backups, first remove some of them.");
                        break;
                    }
                }
            }

            var processRegBackup = new System.Diagnostics.Process()
            {
                StartInfo = new System.Diagnostics.ProcessStartInfo
                {
                    WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden,
                    FileName = "reg",
                    Arguments = $"export HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\MMDevices\\Audio\\Render\\{{{CoreAudioControllerGlobal.DefaultPlaybackDevice.Id}}}\\FxProperties \"{Directory.GetCurrentDirectory()}\\{backupFileName}_{counter}.reg\" /y",
                    Verb = "runas"
                }
            };
            processRegBackup.Start();
            processRegBackup.WaitForExit(5000);
            if (processRegBackup.ExitCode != 0)
            {
                return false;
            }
            return true;
        }
        public static void InstallAPO()
        {
            // test if APO is installed
            using var registryKey = Registry.LocalMachine.OpenSubKey($"SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\MMDevices\\Audio\\Render\\{{{CoreAudioControllerGlobal.DefaultPlaybackDevice.Id}}}\\FxProperties", false);
            var val = registryKey.GetValue("{d04e05a6-594b-4fb6-a80d-01af5eed7d1d},5");
            registryKey.Close();

            if (val?.ToString() == ApoGuid)
            { // APO is installed, exit
                var res = System.Windows.Forms.MessageBox.Show("APO already installed, do you want to force reinstall?", "Reinstall?", MessageBoxButtons.YesNo);
                if (res == DialogResult.No)
                {
                    return;
                }
            }

            // run registry backup
            if (!BackupRegistry())
            {
                System.Windows.Forms.MessageBox.Show("Registry backup failed. APO installation cancelled.");
                return;
            }

            // enable unsigned APOs
            using var audioKey = Registry.LocalMachine.OpenSubKey($"SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Audio", true);
            audioKey.SetValue("DisableProtectedAudioDG", 1);
            audioKey.Close();

            // run regsvr
            var processRegsvr = new System.Diagnostics.Process()
            {
                StartInfo = new System.Diagnostics.ProcessStartInfo
                {
                    WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden,
                    FileName = "regsvr32.exe",
                    Arguments = "/s VolumAPONative.dll",
                    Verb = "runas"
                }
            };
            processRegsvr.Start();
            processRegsvr.WaitForExit(5000);

            // SOFTWARE\Microsoft\Windows\CurrentVersion\Audio DisableProtectedAudioDG = 1
            /*
            var command = CliWrap.Cli.Wrap("regsvr32")
                .WithArguments("/s VolumAPONative.dll")
                .WithValidation(CliWrap.CommandResultValidation.None)
                .ExecuteAsync().GetAwaiter().GetResult();
            */
            if (processRegsvr.ExitCode != 0)
            {
                if (processRegsvr.ExitCode == 22)
                {
                    System.Windows.Forms.MessageBox.Show($"APO installation failed on RegisterAPO function.");
                }
                System.Windows.Forms.MessageBox.Show($"APO installation failed with error code {processRegsvr.ExitCode}.");
            }

            var apoKey = Registry.LocalMachine.OpenSubKey($"SOFTWARE\\Classes\\AudioEngine\\AudioProcessingObjects\\{{{ApoGuid}}}", false);
            if (apoKey == null)
            {
                System.Windows.Forms.MessageBox.Show($"APO installation failed. Try restarting as administrator.");
                apoKey.Close();
                apoKey.Dispose();
            }
            else
            {
                apoKey.Close();
                apoKey.Dispose();
                bool exOccured = false;
                try
                {
                    var registryPath = $"HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\MMDevices\\Audio\\Render\\{{{CoreAudioControllerGlobal.DefaultPlaybackDevice.Id}}}\\FxProperties";

                    List<string> regNames = new List<string>() { "{d04e05a6-594b-4fb6-a80d-01af5eed7d1d},5", "{d04e05a6-594b-4fb6-a80d-01af5eed7d1d},7" };
                    TakeOwnerShipAndWrite(registryPath, regNames, $"{{{ApoGuid}}}");

                    regNames = new List<string>() { "{d3993a3f-99c2-4402-b5ec-a92a0367664b},5", "{d3993a3f-99c2-4402-b5ec-a92a0367664b},7" };
                    TakeOwnerShipAndWrite(registryPath, regNames, $"{{C18E2F7E-933D-4965-B7D1-1EEF228D2AF3}}", true);
                }
                catch (Exception ex)
                {
                    System.Windows.Forms.MessageBox.Show($"APO installation failed partially with error: {ex.Message}");
                    exOccured = true;
                }

                try
                {
                    RegistryKey key = Registry.LocalMachine.CreateSubKey("SOFTWARE\\VolumAPO", RegistryKeyPermissionCheck.ReadWriteSubTree);
                    if (key == null)
                    {
                        throw new InvalidOperationException($"Cannot create registry key SOFTWARE\\VolumAPO");
                    }
                }
                catch(Exception ex)
                {
                    System.Windows.Forms.MessageBox.Show($"APO installation failed partially with error: {ex.Message}");
                    exOccured = true;
                }

                if (!exOccured)
                {
                    if (System.Windows.Forms.MessageBox.Show($"Do you want to use APO to control volume as well?", "Volume control?", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        Config.ConfigAccessor.UseApoForVolume = true;
                    }

                    System.Windows.Forms.MessageBox.Show($"APO installation successful, please restart computer to apply the effect.");
                    Config.ConfigAccessor.UseApoForBalance = true;
                }
            }
        }

        private static void TakeOwnerShipAndWrite(string registryPath, List<string> regNames, string regValue, bool writeAsMultiSz = false)
        {
            foreach (string item in regNames)
            {
                var res = DllRegTakeOwnership(registryPath, item, regValue, writeAsMultiSz);
                var res2 = Marshal.PtrToStringUni(res);

                Marshal.FreeCoTaskMem(res);

                if (res2 != string.Empty)
                {
                    throw new InvalidOperationException($"Error when writing to the FxProperties registry: {res2}");
                }
            }
        }

        public static void SetBalanceAPO(int value)
        {
            RegistryKey key = Registry.LocalMachine.OpenSubKey("SOFTWARE\\VolumAPO", RegistryKeyPermissionCheck.ReadWriteSubTree);
            if (key != null)
            {
                string lChannel = ((100 - value) / 100.0).ToString(CultureInfo.InvariantCulture);
                string rChannel = (value / 100.0).ToString(CultureInfo.InvariantCulture);
                key.SetValue("LChannel", lChannel);
                key.SetValue("RChannel", rChannel);
                key.Close();
                key.Dispose();
            }
        }

        public static void SetVolumeAPO(int value)
        {
            RegistryKey key = Registry.LocalMachine.OpenSubKey("SOFTWARE\\VolumAPO", RegistryKeyPermissionCheck.ReadWriteSubTree);
            if (key != null)
            {
                //key.SetValue("Volume", (value / 100.0).ToString(CultureInfo.InvariantCulture));
                key.SetValue("Volume", (value / 10.0).ToString(CultureInfo.InvariantCulture));
                key.Close();
                key.Dispose();
            }
        }
    }
}
