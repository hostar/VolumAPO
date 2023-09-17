﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Vanara.PInvoke.User32;

namespace VolumAPO.Models
{
    public class Hotkey
    {
        public int Key { get; set; }
        public bool Control { get; set; }
        public bool Alt { get; set; }
        public bool Shift { get; set; }
        public bool Win { get; set; }

        public static bool IsValidKey(Keys key)
        {
            if (key != Keys.Escape && key != Keys.Delete && key != Keys.Back && key != Keys.Control 
                && key != Keys.Alt && key != Keys.Shift && key != Keys.LWin && key != Keys.RWin
                && key != Keys.ControlKey && key != Keys.LControlKey && key != Keys.RControlKey && key != Keys.Menu)
                return true;
            if (key >= Keys.A && key <= Keys.Z)
                return true;
            if (key >= Keys.D0 && key <= Keys.D9)
                return true;
            if (key >= Keys.NumPad0 && key <= Keys.NumPad9)
                return true;
            if (key >= Keys.F1 && key <= Keys.F12)
                return true;

            /*
             
            Keys.Escape || pressedKey == Keys.Delete || pressedKey == Keys.Back;
            Text = "";
                if ((modifierKeys & Keys.Control) > 0)
                    Text += "Ctrl + ";
                if ((modifierKeys & Keys.Alt) > 0)
                    Text += "Alt + ";
                if ((modifierKeys & Keys.Shift) > 0)
                    Text += "Shift + ";
                if ((modifierKeys & Keys.LWin) > 0 || (modifierKeys & Keys.RWin) > 0)

             */

            return false;
        }

        public static string KeyToString(Keys key)
        {
            if (key >= Keys.F1 && key <= Keys.F12)
                return "F" + (key - Keys.F1 + 1).ToString();
            else
                return ((char)key).ToString();
        }

        public HotKeyModifiers GetVanaraHotkey()
        {
            HotKeyModifiers result = HotKeyModifiers.MOD_NONE;
            if (Control)
            {
                result |= HotKeyModifiers.MOD_CONTROL;
            }
            if (Alt)
            {
                result |= HotKeyModifiers.MOD_ALT;
            }
            if (Shift)
            {
                result |= HotKeyModifiers.MOD_SHIFT;
            }
            return result;
        }

        public static string KeyToString(int key)
        {
            if (key >= (int)Keys.F1 && key <= (int)Keys.F12)
                return "F" + (key - (int)Keys.F1 + 1).ToString();
            else
                return ((char)key).ToString();
        }

        public override string ToString()
        {
            if (Key > 0)
            {
                string str = "";
                if (Control) str += "Ctrl + ";
                if (Alt) str += "Alt + ";
                if (Shift) str += "Shift + ";
                if (Win) str += "Win + ";
                str += KeyToString(Key);
                return str;
            }
            else
            {
                return "None";
            }
        }

        public bool Parse(string para)
        {
            if (para == "None")
            {
                Key = 0;
                Control = false;
                Alt = false;
                Shift = false;
                Win = false;
                return true;
            }
            else if (para.Length >= 1)
            {
                Control = false;
                Alt = false;
                Shift = false;
                Win = false;
                Keys key;
                if (para.Length == 1 || para[para.Length - 2] == ' ' || para[para.Length - 2] == '+')
                    key = (Keys)para[para.Length - 1];
                else if (para[para.Length - 2] == 'F')
                    key = int.Parse(para[para.Length - 1].ToString()) - 1 + Keys.F1;
                else
                    key = Keys.None;
                if (IsValidKey(key))
                {
                    if (para.Contains("Control")) Control = true;
                    if (para.Contains("Ctrl")) Control = true;
                    if (para.Contains("Alt")) Alt = true;
                    if (para.Contains("Shift")) Shift = true;
                    if (para.Contains("Win")) Win = true;
                    Key = (char)key;
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public bool ModifierMatch(bool control, bool alt, bool shift, bool win)
        {
            return Control == control && Alt == alt && Shift == shift && Win == win;
        }

        public bool ConflictWith(Hotkey hotkey)
        {
            if (Key == 0 || hotkey.Key == 0)
                return false;
            else if (Control == hotkey.Control && Alt == hotkey.Alt && Shift == hotkey.Shift && Win == hotkey.Win && Key == hotkey.Key)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
