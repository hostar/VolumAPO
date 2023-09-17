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

namespace VolumAPO
{
    public partial class OSDForm : Form
    {
        private int penWidth = 6;
        private Color lineColor = Color.FromArgb(0, 125, 224);
        private Font font;
        private Font font2;
        private Timer hideTimer;
        private int interval = 3000;
        private LastChange lastChange;

        public OSDForm()
        {
            hideTimer = new Timer(HideTimer_Tick, null, Timeout.Infinite, Timeout.Infinite);
            InitializeComponent();
            BackColor = Color.FromArgb(0, 100, 179);
            font = new Font(Font.FontFamily, 15, FontStyle.Regular);
            font2 = new Font(Font.FontFamily, 12, FontStyle.Regular);

            Top = Config.ConfigAccessor.OSDVertical;
            Left = Config.ConfigAccessor.OSDHorizontal;
        }

        private void OSDForm_Paint(object sender, PaintEventArgs e)
        {
            Pen pen = new Pen(lineColor, penWidth);
            e.Graphics.DrawLine(pen, x1: 20, y1: 50, x2: 280, y2: 50);

            int xCoords = 0;
            switch (lastChange)
            {
                case LastChange.Volume:
                    xCoords = (GlobalHelpers.CurrentVolume * penWidth / 2) + 20;
                    e.Graphics.DrawString(GlobalHelpers.CurrentVolume.ToString(), font, new SolidBrush(Color.White), 290, 30);

                    e.Graphics.DrawString("Volume", font, new SolidBrush(Color.White), 120, 60);
                    break;
                case LastChange.Balance:
                    xCoords = (GlobalHelpers.CurrentBalance * penWidth / 2) + 20;
                    e.Graphics.DrawString(GlobalHelpers.CurrentBalance.ToString(), font, new SolidBrush(Color.White), 290, 30);

                    e.Graphics.DrawString("Balance", font, new SolidBrush(Color.White), 120, 60);
                    break;
            }
            
            e.Graphics.DrawLine(pen, x1: xCoords, y1: 35, x2: xCoords, y2: 65);
        }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams baseParams = base.CreateParams;

                const int WS_EX_NOACTIVATE = 0x08000000;
                const int WS_EX_TOOLWINDOW = 0x00000080;
                baseParams.ExStyle |= (int)(WS_EX_NOACTIVATE | WS_EX_TOOLWINDOW);

                return baseParams;
            }
        }

        private void HideTimer_Tick(object? sender)
        {
            Invoke(() => { Hide(); });
        }

        public void StartTimer(LastChange lastChangeParam)
        {
            lastChange = lastChangeParam;
            hideTimer.Change(interval, Timeout.Infinite);
        }
    }
}
