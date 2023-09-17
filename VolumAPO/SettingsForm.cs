using AudioSwitcher.AudioApi.CoreAudio;
using Microsoft.Win32;
using System.Configuration.Internal;
using System.Windows.Forms;
using VolumAPO.Components;
using VolumAPO.Helpers;
using VolumAPO.Models;
using static VolumAPO.Helpers.GlobalHelpers;

namespace VolumAPO
{
    public partial class SettingsForm : Form
    {
        public bool AllowShowDisplay = false;
        private VolumeControlForm volumeControlForm;
        private OSDForm osdForm;

        public SettingsForm(VolumeControlForm volumeControlFormParam, OSDForm osdFormParam)
        {
            InitializeComponent();

            TopMost = true;
            volumeControlForm = volumeControlFormParam;
            osdForm = osdFormParam;
        }

        protected override void SetVisibleCore(bool value)
        {
            base.SetVisibleCore(AllowShowDisplay ? value : AllowShowDisplay);
            AllowShowDisplay = true;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            FormBorderStyle = FormBorderStyle.SizableToolWindow;
        }

        private void notifyIconMain_DoubleClick(object sender, EventArgs e)
        {
            AllowShowDisplay = true;
            Show();
        }

        private void SettingsForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                osdForm.Hide();

                e.Cancel = true; // Cancel the form closing
                Hide();     // Hide the form
            }
        }

        private void hotkeyVolumeUp_OnHotkeyChanged(object sender, EventArgs e)
        {
            SetHotkey(sender, HotkeyEnum.VolumeUp);
        }

        private void hotkeyVolumeDown_OnHotkeyChanged(object sender, EventArgs e)
        {
            SetHotkey(sender, HotkeyEnum.VolumeDown);
        }

        private void hotkeyBalanceLeft_OnHotkeyChanged(object sender, EventArgs e)
        {
            SetHotkey(sender, HotkeyEnum.BalanceLeft);
        }

        private void hotkeyBalanceRight_OnHotkeyChanged(object sender, EventArgs e)
        {
            SetHotkey(sender, HotkeyEnum.BalanceRight);
        }

        private void SetHotkey(object sender, HotkeyEnum hotkeyId)
        {
            var hotkey = ((HotkeyInputBox)(sender)).Hotkey;

            GlobalHelpers.SetHotkey(volumeControlForm.Handle, hotkey, hotkeyId);
        }

        private void btnSaveConfig_Click(object sender, EventArgs e)
        {
            File.WriteAllText(Config.ConfigFileName, System.Text.Json.JsonSerializer.Serialize(Config.ConfigAccessor,
                new System.Text.Json.JsonSerializerOptions()
                {
                    DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull,
                    WriteIndented = true
                }));
        }

        private void trackBarVolumeSpeed_ValueChanged(object sender, EventArgs e)
        {
            toolTipVolSteps.SetToolTip(trackBarVolumeSpeed, trackBarVolumeSpeed.Value.ToString());
            Config.ConfigAccessor.VolumeChangeStep = trackBarVolumeSpeed.Value;
        }

        private void textBoxMaxVolume_TextChanged(object sender, EventArgs e)
        {
            Config.ConfigAccessor.VolumeMax = Convert.ToInt32(textBoxMaxVolume.Text);
        }

        private void checkBoxTaskBarScroll_CheckedChanged(object sender, EventArgs e)
        {
            Config.ConfigAccessor.TaskBarScrollVolumeChange = checkBoxTaskBarScroll.Checked;
            GlobalHelpers.SetWheelTaskbar();
        }

        private void treeViewSettings_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node == null)
            {
                return;
            }
            switch (e.Node.Name)
            {
                case "NodeGeneral":

                    break;
            }
        }

        private void tabPageOsd_Enter(object sender, EventArgs e)
        {
            osdForm.Show();
        }

        private void tabPageOsd_Leave(object sender, EventArgs e)
        {
            osdForm.Hide();
        }

        private void trackBarOsdHorPos_ValueChanged(object sender, EventArgs e)
        {
            osdForm.Left = (int)((Screen.PrimaryScreen.WorkingArea.Width / 100.0) * trackBarOsdHorPos.Value);
            Config.ConfigAccessor.OSDHorizontal = trackBarOsdHorPos.Value;
        }

        private void trackBarOsdVerPos_ValueChanged(object sender, EventArgs e)
        {
            osdForm.Top = (int)((Screen.PrimaryScreen.WorkingArea.Height / 100.0) * trackBarOsdVerPos.Value);
            Config.ConfigAccessor.OSDVertical = trackBarOsdVerPos.Value;
        }

        private void checkBoxOsdEnabled_CheckedChanged(object sender, EventArgs e)
        {
            Config.ConfigAccessor.OSDVisible = checkBoxOsdEnabled.Checked;
        }

        private void tabPageApo_Enter(object sender, EventArgs e)
        {
            string statusInstallation = "Not installed";
            string statusActive = "Not active on current device";

            var apoKey = Registry.LocalMachine.OpenSubKey($"SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\MMDevices\\Audio\\Render\\{{{CoreAudioControllerGlobal.DefaultPlaybackDevice.Id}}}\\FxProperties", false);
            var guid = apoKey.GetValue("{d04e05a6-594b-4fb6-a80d-01af5eed7d1d},5");
            apoKey.Close();
            apoKey.Dispose();

            apoKey = Registry.LocalMachine.OpenSubKey($"SOFTWARE\\Classes\\AudioEngine\\AudioProcessingObjects\\{{{ApoGuid}}}", false);
            if (apoKey != null)
            {
                statusInstallation = "Installed";
                apoKey.Dispose();
            }

            if (guid != null)
            {
                if (guid is string guidStr)
                {
                    if (guidStr == $"{{{ApoGuid}}}")
                    {
                        statusActive = "Active on current device";
                    }
                }
            }

            labelApoStatusValue.Text = statusInstallation + ", " + statusActive;

            checkBoxUseApoBalance.Checked = Config.ConfigAccessor.UseApoForBalance;
            checkBoxUseApoVolume.Checked = Config.ConfigAccessor.UseApoForVolume;
        }

        private void btnApoInstall_Click(object sender, EventArgs e)
        {
            InstallAPO();
        }

        private void btnApoUninstall_Click(object sender, EventArgs e)
        {
            // TODO: is removing of {d3993a3f-99c2-4402-b5ec-a92a0367664b},5 value enough?
        }

        private void checkBoxUseApoBalance_CheckedChanged(object sender, EventArgs e)
        {
            Config.ConfigAccessor.UseApoForBalance = checkBoxUseApoBalance.Checked;
        }

        private void checkBoxUseApoVolume_CheckedChanged(object sender, EventArgs e)
        {
            Config.ConfigAccessor.UseApoForVolume = checkBoxUseApoVolume.Checked;
        }
    }
}