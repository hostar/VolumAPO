using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Text.Json;
using System.Windows.Forms;
using AudioSwitcher.AudioApi.CoreAudio;
using AudioSwitcher.AudioApi.Observables;
using System;
using VolumAPO.Helpers;
using VolumAPO.Models;
using System.Drawing;
using System.Drawing.Design;

namespace VolumAPO
{
    public class Program
    {
        public bool ExitNow { get; set; } = false;

        NotifyIcon notifyIcon;

        OSDForm osdForm;
        SettingsForm settingsForm;
        VolumeControlForm volumeControlForm;

        public Program()
        {
            if (GlobalHelpers.CoreAudioControllerGlobal.DefaultPlaybackDevice == null)
            {
                MessageBox.Show("Windows sound service seems to be down. Please enable it first using: net start audiosrv . Exiting...");
                ExitNow = true;
                return;
            }

            GlobalHelpers.CurrentDeviceChannels = GlobalHelpers.CoreAudioControllerGlobal.DefaultPlaybackDevice.ChannelCount;
            GlobalHelpers.CreateBaseBitmap();
            Icon icon = GlobalHelpers.CreateIcon();

            notifyIcon = new NotifyIcon() { Icon = icon, Visible = true };
            GlobalHelpers.notifyIcon = notifyIcon;

            osdForm = new OSDForm();
            volumeControlForm = new VolumeControlForm(osdForm);
            settingsForm = new SettingsForm(volumeControlForm, osdForm);

            GlobalHelpers.volumeControlForm = volumeControlForm;
            GlobalHelpers.osdForm = osdForm;

            GlobalHelpers.SetInitialConfiguration(volumeControlForm.Handle);
            if (File.Exists(Config.ConfigFileName))
            {
                Config.ConfigAccessor = JsonSerializer.Deserialize<Config>(File.ReadAllText(Config.ConfigFileName));
            }

            GlobalHelpers.PopulateControls(settingsForm);

            CreateRightClickMenu();

            notifyIcon.ContextMenuStrip = RightClickMenuHelper.GetContextMenu();
            notifyIcon.Click += (object? sender, EventArgs e) =>
            {
                if (e is MouseEventArgs mouseEventArgs)
                {
                    if (mouseEventArgs.Button == MouseButtons.Left)
                    {
                        var height = Screen.PrimaryScreen.WorkingArea.Height - volumeControlForm.Height;
                        var width = Screen.PrimaryScreen.WorkingArea.Width - volumeControlForm.Width;

                        volumeControlForm.Location = new Point(Cursor.Position.X - volumeControlForm.Width / 2, height);
                        volumeControlForm.Show();
                    }

                    if (mouseEventArgs.Button == MouseButtons.Right)
                    {
                        // Show the context menu strip at the cursor position
                        RightClickMenuHelper.ShowContextMenu();
                    }
                }
            };

            notifyIcon.DoubleClick += (sender, e) =>
            {
                settingsForm.AllowShowDisplay = true;
                settingsForm.Show();
            };
        }

        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();

            var program = new Program();

            if (program.ExitNow)
            {
                return;
            }

            Application.Run();
        }

        void CreateRightClickMenu()
        {
            RightClickMenuHelper.rightClickMenuHelperInstance = new RightClickMenuHelper(settingsForm, notifyIcon);
        }
    }
}