using AudioSwitcher.AudioApi.CoreAudio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VolumAPO.Helpers;
using VolumAPO.Models;
using Timer = System.Threading.Timer;
using Microsoft.Win32;
using System.Configuration;
using System.Globalization;
using Microsoft.VisualBasic.Devices;
//using Timer = System.Windows.Forms.Timer;

namespace VolumAPO
{
    public partial class VolumeControlForm : Form
    {
        int interval = 5000;
        Timer hideTimer;

        OSDForm osdForm;

        public VolumeControlForm(OSDForm osdFormParam)
        {
            osdForm = osdFormParam;
            hideTimer = new Timer(HideTimer_Tick2, null, Timeout.Infinite, Timeout.Infinite);

            InitializeComponent();

            TopMost = true;

            var balance = GlobalHelpers.CoreAudioControllerGlobal.DefaultPlaybackDevice.GetBalanceRatio();

            trackBarBalance.Value = balance.Item2;

            trackBarVolume.Value = (int)GlobalHelpers.CoreAudioControllerGlobal.DefaultPlaybackDevice.GetVolumeAsync().Result;

            GlobalHelpers.HotkeyDict.Add(GlobalHelpers.HotkeyEnum.VolumeUp, SetVolumeUp);
            GlobalHelpers.HotkeyDict.Add(GlobalHelpers.HotkeyEnum.VolumeDown, SetVolumeDown); ;
            GlobalHelpers.HotkeyDict.Add(GlobalHelpers.HotkeyEnum.BalanceLeft, SetBalanceLeft);
            GlobalHelpers.HotkeyDict.Add(GlobalHelpers.HotkeyEnum.BalanceRight, SetBalanceRight);
        }

        private void HideTimer_Tick2(object? sender)
        {
            Invoke(() => { Hide(); });
        }

        public void StartHideTimer()
        {
            hideTimer.Change(interval, Timeout.Infinite);
        }

        public void StopHideTimer()
        {
            hideTimer.Change(Timeout.Infinite, Timeout.Infinite);
        }

        private void VolumeControlForm_Shown(object sender, EventArgs e)
        {
            StartHideTimer();
            Activate();
            Console.WriteLine(nameof(VolumeControlForm_Shown));
        }

        private void VolumeControlForm_MouseMove(object sender, MouseEventArgs e)
        {
            StopHideTimer();
            //Console.WriteLine(nameof(VolumeControlForm_MouseMove));
        }

        private void VolumeControlForm_MouseHover(object sender, EventArgs e)
        {
            StopHideTimer();
            Console.WriteLine(nameof(VolumeControlForm_MouseHover));
        }

        private void VolumeControlForm_MouseLeave(object sender, EventArgs e)
        {
            StartHideTimer();
            Console.WriteLine(nameof(VolumeControlForm_MouseLeave));
        }

        private void trackBarVolume_ValueChanged(object sender, EventArgs e)
        {
            if ((trackBarVolume.Value + Config.ConfigAccessor.VolumeChangeStep) > Config.ConfigAccessor.VolumeMax)
            {
                return;
            }
            SetVolume();
        }

        public void SetVolumeUp()
        {
            if (GlobalHelpers.SetVolumeUp())
            {
                trackBarVolume.Value = GlobalHelpers.CurrentVolume;
            }
        }

        public void SetVolumeDown()
        {
            if (GlobalHelpers.SetVolumeDown())
            {
                trackBarVolume.Value = GlobalHelpers.CurrentVolume;
            }
        }

        private void SetVolume()
        {
            GlobalHelpers.CurrentVolume = trackBarVolume.Value;
            toolTipVolume.SetToolTip(trackBarVolume, trackBarVolume.Value.ToString());
        }

        public void SetVolumeTrackBar()
        {
            trackBarVolume.Value = GlobalHelpers.CurrentVolume;
            toolTipVolume.SetToolTip(trackBarVolume, trackBarVolume.Value.ToString());
        }

        private void VolumeControlForm_Deactivate(object sender, EventArgs e)
        {
            Hide();
        }

        private void trackBarBalance_ValueChanged(object sender, EventArgs e)
        {
            SetBalance();
        }

        private void SetBalanceLeft()
        {
            if (trackBarBalance.Value == 0)
            {
                return;
            }
            trackBarBalance.Value--; // TODO: make the step settable
            GlobalHelpers.CurrentBalance = trackBarBalance.Value;
            GlobalHelpers.ShowOSD(LastChange.Balance);
        }

        private void SetBalanceRight()
        {
            if (trackBarBalance.Value == 100)
            {
                return;
            }
            trackBarBalance.Value++; // TODO: make the step settable
            GlobalHelpers.CurrentBalance = trackBarBalance.Value;
            GlobalHelpers.ShowOSD(LastChange.Balance);
        }

        private void SetBalance()
        {
            toolTipBalance.SetToolTip(trackBarBalance, $"{100 - trackBarBalance.Value}/{trackBarBalance.Value}");

            if (Config.ConfigAccessor.UseApoForBalance)
            {
                GlobalHelpers.SetBalanceAPO(trackBarBalance.Value);
            }
            else
            {
                GlobalHelpers.CoreAudioControllerGlobal.DefaultPlaybackDevice.SetBalance(100 - trackBarBalance.Value, trackBarBalance.Value);
            }
            
            if (GlobalHelpers.CurrentDeviceChannels == 1)
            {
                if (!Config.ConfigAccessor.ApoInstallationDialogShown)
                {
                    // APO must be used
                    var resp = MessageBox.Show("Looks like the device you are using does not publish audio channels natively. This can be solved by APO. Do you want to install it now? If not, you can install it later in Settings, this dialog will not appear in future.", "Install APO?", MessageBoxButtons.YesNo);
                    
                    if (resp == DialogResult.Yes)
                    {
                        GlobalHelpers.InstallAPO();
                    }
                    Config.ConfigAccessor.ApoInstallationDialogShown = true;
                }
            }
        }

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == (int)Vanara.PInvoke.User32.WindowMessage.WM_HOTKEY)
            {
                //Console.WriteLine($"msg received: {m.Msg}, hotkeyID: {m.WParam}");
                Invoke(GlobalHelpers.HotkeyDict[(GlobalHelpers.HotkeyEnum)m.WParam]);
            }

            base.WndProc(ref m);
        }
    }
}