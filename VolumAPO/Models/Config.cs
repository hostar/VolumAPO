using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VolumAPO.Models
{
    public class Config
    {
        public static Config ConfigAccessor = new Config();
        public static string ConfigFileName = "config.json";
        public int VolumeChangeStep { get; set; } = 2;
        public int BalanceChangeStep { get; set; } = 2;
        public int VolumeMax { get; set; } = 100;
        public Hotkey VolumeUpHotkey { get; set; }
        public Hotkey VolumeDownHotkey { get; set; }
        public Hotkey BalanceRightHotkey { get; set; }
        public Hotkey BalanceLeftHotkey { get; set; }
        public bool TaskBarScrollVolumeChange { get; set; }
        public bool OSDVisible { get; set; }
        public int OSDHorizontal { get; set; }
        public int OSDVertical { get; set; }
        public bool UseApoForBalance { get; set; }
        public bool UseApoForVolume { get; set; }
        public bool ApoInstallationDialogShown { get; set; }

    }
}