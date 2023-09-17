namespace VolumAPO
{
    partial class VolumeControlForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            trackBarBalance = new TrackBar();
            trackBarVolume = new TrackBar();
            checkBoxMute = new CheckBox();
            toolTipVolume = new ToolTip(components);
            toolTipBalance = new ToolTip(components);
            ((System.ComponentModel.ISupportInitialize)trackBarBalance).BeginInit();
            ((System.ComponentModel.ISupportInitialize)trackBarVolume).BeginInit();
            SuspendLayout();
            // 
            // trackBarBalance
            // 
            trackBarBalance.LargeChange = 25;
            trackBarBalance.Location = new Point(28, 260);
            trackBarBalance.Maximum = 100;
            trackBarBalance.Name = "trackBarBalance";
            trackBarBalance.Size = new Size(102, 56);
            trackBarBalance.SmallChange = 5;
            trackBarBalance.TabIndex = 0;
            trackBarBalance.TabStop = false;
            trackBarBalance.TickFrequency = 10;
            trackBarBalance.TickStyle = TickStyle.TopLeft;
            trackBarBalance.ValueChanged += trackBarBalance_ValueChanged;
            trackBarBalance.MouseLeave += VolumeControlForm_MouseLeave;
            trackBarBalance.MouseHover += VolumeControlForm_MouseHover;
            trackBarBalance.MouseMove += VolumeControlForm_MouseMove;
            // 
            // trackBarVolume
            // 
            trackBarVolume.Location = new Point(52, 37);
            trackBarVolume.Maximum = 100;
            trackBarVolume.Name = "trackBarVolume";
            trackBarVolume.Orientation = Orientation.Vertical;
            trackBarVolume.Size = new Size(56, 217);
            trackBarVolume.TabIndex = 1;
            trackBarVolume.TabStop = false;
            trackBarVolume.TickFrequency = 5;
            trackBarVolume.ValueChanged += trackBarVolume_ValueChanged;
            trackBarVolume.MouseLeave += VolumeControlForm_MouseLeave;
            trackBarVolume.MouseHover += VolumeControlForm_MouseHover;
            trackBarVolume.MouseMove += VolumeControlForm_MouseMove;
            // 
            // checkBoxMute
            // 
            checkBoxMute.AutoSize = true;
            checkBoxMute.Location = new Point(38, 306);
            checkBoxMute.Name = "checkBoxMute";
            checkBoxMute.Size = new Size(65, 24);
            checkBoxMute.TabIndex = 2;
            checkBoxMute.TabStop = false;
            checkBoxMute.Text = "Mute";
            checkBoxMute.UseVisualStyleBackColor = true;
            checkBoxMute.MouseLeave += VolumeControlForm_MouseLeave;
            checkBoxMute.MouseHover += VolumeControlForm_MouseHover;
            checkBoxMute.MouseMove += VolumeControlForm_MouseMove;
            // 
            // VolumeControlForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(159, 341);
            ControlBox = false;
            Controls.Add(checkBoxMute);
            Controls.Add(trackBarVolume);
            Controls.Add(trackBarBalance);
            FormBorderStyle = FormBorderStyle.None;
            Name = "VolumeControlForm";
            ShowIcon = false;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.Manual;
            Text = "VolumeControlForm";
            Deactivate += VolumeControlForm_Deactivate;
            Shown += VolumeControlForm_Shown;
            VisibleChanged += VolumeControlForm_Shown;
            MouseLeave += VolumeControlForm_MouseLeave;
            MouseHover += VolumeControlForm_MouseHover;
            MouseMove += VolumeControlForm_MouseMove;
            ((System.ComponentModel.ISupportInitialize)trackBarBalance).EndInit();
            ((System.ComponentModel.ISupportInitialize)trackBarVolume).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private TrackBar trackBarVolume;
        private CheckBox checkBoxMute;
        public TrackBar trackBarBalance;
        private ToolTip toolTipVolume;
        private ToolTip toolTipBalance;
    }
}