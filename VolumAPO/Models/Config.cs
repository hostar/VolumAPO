using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VolumAPO.Helpers;
using static Vanara.PInvoke.User32;

namespace VolumAPO.Models
{
    public class Config
    {
        public static Config ConfigAccessor = new Config();
        public static string ConfigFileName = "config.json";
        public int VolumeChangeStep { get; set; } = 2;
        public int BalanceChangeStep { get; set; } = 2;
        public int VolumeMax { get; set; } = 100;

        private Hotkey _volumeUpHotkey;
        public Hotkey VolumeUpHotkey { 
            get {
                return _volumeUpHotkey;
            } set {
                Vanara.PInvoke.User32.UnregisterHotKey(GlobalHelpers.VolumeControlFormHandle, (int)Helpers.GlobalHelpers.HotkeyEnum.VolumeUp);
                Vanara.PInvoke.User32.RegisterHotKey(GlobalHelpers.VolumeControlFormHandle, (int)Helpers.GlobalHelpers.HotkeyEnum.VolumeUp, value.GetVanaraHotkey(), (uint)value.Key);
                _volumeUpHotkey = value;
            } }

        Hotkey volumeDownHotkey;
        public Hotkey VolumeDownHotkey
        {
            get => volumeDownHotkey; 
            set
            {
                Vanara.PInvoke.User32.UnregisterHotKey(GlobalHelpers.VolumeControlFormHandle, (int)Helpers.GlobalHelpers.HotkeyEnum.VolumeDown);
                Vanara.PInvoke.User32.RegisterHotKey(GlobalHelpers.VolumeControlFormHandle, (int)Helpers.GlobalHelpers.HotkeyEnum.VolumeDown, value.GetVanaraHotkey(), (uint)value.Key);
                volumeDownHotkey = value;
            }
        }

        Hotkey balanceRightHotkey;
        public Hotkey BalanceRightHotkey
        {
            get => balanceRightHotkey; 
            set
            {
                Vanara.PInvoke.User32.UnregisterHotKey(GlobalHelpers.VolumeControlFormHandle, (int)Helpers.GlobalHelpers.HotkeyEnum.BalanceRight);
                Vanara.PInvoke.User32.RegisterHotKey(GlobalHelpers.VolumeControlFormHandle, (int)Helpers.GlobalHelpers.HotkeyEnum.BalanceRight, value.GetVanaraHotkey(), (uint)value.Key);
                balanceRightHotkey = value;
            }
        }

        Hotkey balanceLeftHotkey;
        public Hotkey BalanceLeftHotkey
        {
            get => balanceLeftHotkey; 
            set
            {
                Vanara.PInvoke.User32.UnregisterHotKey(GlobalHelpers.VolumeControlFormHandle, (int)Helpers.GlobalHelpers.HotkeyEnum.BalanceLeft);
                Vanara.PInvoke.User32.RegisterHotKey(GlobalHelpers.VolumeControlFormHandle, (int)Helpers.GlobalHelpers.HotkeyEnum.BalanceLeft, value.GetVanaraHotkey(), (uint)value.Key);
                balanceLeftHotkey = value;
            }
        }
        public bool TaskBarScrollVolumeChange { get; set; }
        public bool OSDVisible { get; set; }
        public int OSDHorizontal { get; set; }
        public int OSDVertical { get; set; }
        public bool UseApoForBalance { get; set; }
        public bool UseApoForVolume { get; set; }
        public bool ApoInstallationDialogShown { get; set; }

    }
}