using AudioSwitcher.AudioApi;
using AudioSwitcher.AudioApi.CoreAudio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VolumAPO.Helpers
{
    public class RightClickMenuHelper
    {
        public static RightClickMenuHelper rightClickMenuHelperInstance;
        ContextMenuStrip contextMenuTray;
        ToolStripMenuItem toolStripMenuItemShowSettings;
        ToolStripMenuItem toolStripMenuExit;

        SettingsForm settingsForm;
        NotifyIcon  notifyIcon;
        const string STR_PlaybackDeviceNameTooltip = "playbackDeviceNameTooltip";
        const string STR_CaptureDeviceNameTooltip = "captureDeviceNameTooltip";

        public RightClickMenuHelper(SettingsForm settingsFormParam, NotifyIcon notifyIconParam)
        {
            GlobalHelpers.synchronizationContextForm = SynchronizationContext.Current;
            settingsForm = settingsFormParam;
            notifyIcon = notifyIconParam;
            contextMenuTray = new ContextMenuStrip();

            toolStripMenuItemShowSettings = new ToolStripMenuItem();
            toolStripMenuExit = new ToolStripMenuItem();

            toolStripMenuItemShowSettings.Name = "toolStripMenuItemShowSettings";
            toolStripMenuItemShowSettings.Size = new Size(210, 24);
            toolStripMenuItemShowSettings.Text = "Show settings";

            toolStripMenuExit.Name = "toolStripMenuExit";
            toolStripMenuExit.Size = new Size(210, 24);
            toolStripMenuExit.Text = "Exit";

            // device changed observer
            DeviceObserver observer = new DeviceObserver(this);
            GlobalHelpers.CoreAudioControllerGlobal.AudioDeviceChanged.Subscribe(observer);

            // get playback devices
            List<ToolStripMenuItem> toolStripMenuItemsPlaybackDevices = PopulateDevicesToolstrip(
                GlobalHelpers.CoreAudioControllerGlobal.GetPlaybackDevices(DeviceState.Active),
                STR_PlaybackDeviceNameTooltip);

            // get capture devices
            List<ToolStripMenuItem> toolStripMenuItemsCaptureDevices = PopulateDevicesToolstrip(
                GlobalHelpers.CoreAudioControllerGlobal.GetCaptureDevices(DeviceState.Active),
                STR_CaptureDeviceNameTooltip);

            contextMenuTray.ImageScalingSize = new Size(20, 20);
            contextMenuTray.Items.AddRange(new ToolStripItem[] { toolStripMenuItemShowSettings, new ToolStripSeparator() });
            contextMenuTray.Items.Add(new ToolStripMenuItem() { Enabled = false, Text = "Capture devices" } );
            contextMenuTray.Items.AddRange(toolStripMenuItemsCaptureDevices.ToArray());

            contextMenuTray.Items.AddRange(new ToolStripItem[] { new ToolStripSeparator() });
            contextMenuTray.Items.Add(new ToolStripMenuItem() { Enabled = false, Text = "Playback devices" });
            contextMenuTray.Items.AddRange(toolStripMenuItemsPlaybackDevices.ToArray());

            contextMenuTray.Items.AddRange(new ToolStripItem[] { new ToolStripSeparator(), toolStripMenuExit });

            contextMenuTray.Name = "contextMenuTray";
            contextMenuTray.Size = new Size(211, 80);

            contextMenuTray.ItemClicked += contextMenuTray_ItemClicked;
        }

        private static List<ToolStripMenuItem> PopulateDevicesToolstrip(IEnumerable<IRealDevice> devices, string deviceName)
        {
            int i = 0;
            List<ToolStripMenuItem> toolStripMenuItemsPlaybackDevices = new List<ToolStripMenuItem>();
            foreach (CoreAudioDevice coreAudioDevice in devices)
            {
                toolStripMenuItemsPlaybackDevices.Add(new ToolStripMenuItem()
                {
                    Name = deviceName + i,
                    Text = coreAudioDevice.Name,
                    Checked = coreAudioDevice.IsDefaultDevice,
                    Size = new Size(210, 24),
                    Tag = coreAudioDevice.Id
                });
                i++;
            }

            return toolStripMenuItemsPlaybackDevices;
        }

        public static ContextMenuStrip GetContextMenu()
        {
            if (rightClickMenuHelperInstance == null)
            {
                throw new InvalidOperationException($"{nameof(RightClickMenuHelper)} was not initialized.");
            }
            return rightClickMenuHelperInstance.contextMenuTray;
        }

        public static void ShowContextMenu()
        {
            var count = rightClickMenuHelperInstance.contextMenuTray.Items.Count;
            var point = new Point(Cursor.Position.X - 50, Cursor.Position.Y - (22 * count));
            rightClickMenuHelperInstance.contextMenuTray.Show(point);
        }

        private void contextMenuTray_ItemClicked(object? sender, ToolStripItemClickedEventArgs e)
        {
            switch (e.ClickedItem.Name)
            {
                case nameof(toolStripMenuExit):
                    Application.Exit();
                    break;
                case nameof(toolStripMenuItemShowSettings):
                    settingsForm.AllowShowDisplay = true;
                    settingsForm.Show();
                    break;
            }

            if ((e.ClickedItem.Name.Contains(STR_PlaybackDeviceNameTooltip)) || (e.ClickedItem.Name.Contains(STR_CaptureDeviceNameTooltip)))
            {
                GlobalHelpers.CoreAudioControllerGlobal.GetDevice((Guid)e.ClickedItem.Tag).SetAsDefault();
                if (e.ClickedItem.Name.Contains(STR_PlaybackDeviceNameTooltip))
                {
                    UncheckAllDevicesByType(DeviceType.Playback);
                }
                if (e.ClickedItem.Name.Contains(STR_CaptureDeviceNameTooltip))
                {
                    UncheckAllDevicesByType(DeviceType.Capture);
                }
                ((ToolStripMenuItem)e.ClickedItem).Checked = true;
            }
        }

        void UncheckAllDevicesByType(DeviceType deviceType)
        {
            // uncheck others
            foreach (var item in contextMenuTray.Items)
            {
                if (item is ToolStripMenuItem toolStripMenuItem)
                {
                    switch (deviceType)
                    {
                        case DeviceType.Playback:
                            if (toolStripMenuItem.Name.Contains(STR_PlaybackDeviceNameTooltip))
                            {
                                toolStripMenuItem.Checked = false;
                            }
                            break;
                        case DeviceType.Capture:
                            if (toolStripMenuItem.Name.Contains(STR_CaptureDeviceNameTooltip))
                            {
                                toolStripMenuItem.Checked = false;
                            }
                            break;
                    }
                }
            }
        }

        public async Task UpdateDefaultDeviceByGuid(Guid deviceGuid, DeviceType deviceType)
        {
            GlobalHelpers.synchronizationContextForm.Post(state =>
            {
                UncheckAllDevicesByType(deviceType);

                foreach (ToolStripItem toolStripItem in contextMenuTray.Items)
                {
                    if (toolStripItem.Tag is Guid itemGuid)
                    {
                        if (itemGuid.Equals(deviceGuid))
                        {
                            ((ToolStripMenuItem)toolStripItem).Checked = true;
                        }
                    }
                }
            }, null);
        }
    }
}
