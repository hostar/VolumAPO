namespace VolumAPO
{
    partial class SettingsForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            Models.Hotkey hotkey1 = new Models.Hotkey();
            Models.Hotkey hotkey2 = new Models.Hotkey();
            Models.Hotkey hotkey3 = new Models.Hotkey();
            Models.Hotkey hotkey4 = new Models.Hotkey();
            btnSaveConfig = new Button();
            toolTipVolSteps = new ToolTip(components);
            tabControlSettings = new TabControl();
            tabPageGeneral = new TabPage();
            checkBoxTaskBarScroll = new CheckBox();
            label9 = new Label();
            textBoxMaxVolume = new TextBox();
            label8 = new Label();
            label7 = new Label();
            label6 = new Label();
            trackBarVolumeSpeed = new TrackBar();
            label5 = new Label();
            tabPageHotkeys = new TabPage();
            label4 = new Label();
            hotkeyBalanceRight = new Components.HotkeyInputBox();
            label3 = new Label();
            hotkeyBalanceLeft = new Components.HotkeyInputBox();
            label2 = new Label();
            hotkeyVolumeDown = new Components.HotkeyInputBox();
            label1 = new Label();
            hotkeyVolumeUp = new Components.HotkeyInputBox();
            tabPageOsd = new TabPage();
            checkBoxOsdEnabled = new CheckBox();
            label12 = new Label();
            label11 = new Label();
            trackBarOsdVerPos = new TrackBar();
            label10 = new Label();
            trackBarOsdHorPos = new TrackBar();
            tabPageApo = new TabPage();
            checkBoxUseApoVolume = new CheckBox();
            checkBoxUseApoBalance = new CheckBox();
            btnApoUninstall = new Button();
            labelApoStatusValue = new Label();
            labelApoStatus = new Label();
            btnApoInstall = new Button();
            tabControlSettings.SuspendLayout();
            tabPageGeneral.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)trackBarVolumeSpeed).BeginInit();
            tabPageHotkeys.SuspendLayout();
            tabPageOsd.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)trackBarOsdVerPos).BeginInit();
            ((System.ComponentModel.ISupportInitialize)trackBarOsdHorPos).BeginInit();
            tabPageApo.SuspendLayout();
            SuspendLayout();
            // 
            // btnSaveConfig
            // 
            btnSaveConfig.Location = new Point(16, 407);
            btnSaveConfig.Name = "btnSaveConfig";
            btnSaveConfig.Size = new Size(169, 40);
            btnSaveConfig.TabIndex = 8;
            btnSaveConfig.Text = "Save configuration";
            btnSaveConfig.UseVisualStyleBackColor = true;
            btnSaveConfig.Click += btnSaveConfig_Click;
            // 
            // tabControlSettings
            // 
            tabControlSettings.Controls.Add(tabPageGeneral);
            tabControlSettings.Controls.Add(tabPageHotkeys);
            tabControlSettings.Controls.Add(tabPageOsd);
            tabControlSettings.Controls.Add(tabPageApo);
            tabControlSettings.Location = new Point(12, 12);
            tabControlSettings.Name = "tabControlSettings";
            tabControlSettings.SelectedIndex = 0;
            tabControlSettings.Size = new Size(559, 389);
            tabControlSettings.TabIndex = 19;
            // 
            // tabPageGeneral
            // 
            tabPageGeneral.BackColor = SystemColors.Control;
            tabPageGeneral.Controls.Add(checkBoxTaskBarScroll);
            tabPageGeneral.Controls.Add(label9);
            tabPageGeneral.Controls.Add(textBoxMaxVolume);
            tabPageGeneral.Controls.Add(label8);
            tabPageGeneral.Controls.Add(label7);
            tabPageGeneral.Controls.Add(label6);
            tabPageGeneral.Controls.Add(trackBarVolumeSpeed);
            tabPageGeneral.Controls.Add(label5);
            tabPageGeneral.Location = new Point(4, 29);
            tabPageGeneral.Name = "tabPageGeneral";
            tabPageGeneral.Padding = new Padding(3);
            tabPageGeneral.Size = new Size(551, 356);
            tabPageGeneral.TabIndex = 0;
            tabPageGeneral.Text = "General";
            // 
            // checkBoxTaskBarScroll
            // 
            checkBoxTaskBarScroll.AutoSize = true;
            checkBoxTaskBarScroll.Location = new Point(323, 95);
            checkBoxTaskBarScroll.Name = "checkBoxTaskBarScroll";
            checkBoxTaskBarScroll.Size = new Size(135, 24);
            checkBoxTaskBarScroll.TabIndex = 21;
            checkBoxTaskBarScroll.Text = "check to enable";
            checkBoxTaskBarScroll.UseVisualStyleBackColor = true;
            checkBoxTaskBarScroll.CheckedChanged += checkBoxTaskBarScroll_CheckedChanged;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(7, 96);
            label9.Name = "label9";
            label9.Size = new Size(295, 20);
            label9.TabIndex = 20;
            label9.Text = "Control volume by mouse wheel on taskbar";
            // 
            // textBoxMaxVolume
            // 
            textBoxMaxVolume.Location = new Point(170, 56);
            textBoxMaxVolume.Name = "textBoxMaxVolume";
            textBoxMaxVolume.Size = new Size(188, 27);
            textBoxMaxVolume.TabIndex = 19;
            textBoxMaxVolume.TextChanged += textBoxMaxVolume_TextChanged;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(7, 56);
            label8.Name = "label8";
            label8.Size = new Size(128, 20);
            label8.TabIndex = 18;
            label8.Text = "Maximum volume";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(398, 23);
            label7.Name = "label7";
            label7.Size = new Size(34, 20);
            label7.TabIndex = 17;
            label7.Text = "Fast";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(170, 23);
            label6.Name = "label6";
            label6.Size = new Size(41, 20);
            label6.TabIndex = 16;
            label6.Text = "Slow";
            // 
            // trackBarVolumeSpeed
            // 
            trackBarVolumeSpeed.Location = new Point(204, 13);
            trackBarVolumeSpeed.Maximum = 30;
            trackBarVolumeSpeed.Minimum = 1;
            trackBarVolumeSpeed.Name = "trackBarVolumeSpeed";
            trackBarVolumeSpeed.Size = new Size(197, 56);
            trackBarVolumeSpeed.TabIndex = 15;
            trackBarVolumeSpeed.TickStyle = TickStyle.TopLeft;
            trackBarVolumeSpeed.Value = 1;
            trackBarVolumeSpeed.ValueChanged += trackBarVolumeSpeed_ValueChanged;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(7, 23);
            label5.Name = "label5";
            label5.Size = new Size(157, 20);
            label5.TabIndex = 14;
            label5.Text = "Move volume by steps";
            // 
            // tabPageHotkeys
            // 
            tabPageHotkeys.BackColor = SystemColors.Control;
            tabPageHotkeys.Controls.Add(label4);
            tabPageHotkeys.Controls.Add(hotkeyBalanceRight);
            tabPageHotkeys.Controls.Add(label3);
            tabPageHotkeys.Controls.Add(hotkeyBalanceLeft);
            tabPageHotkeys.Controls.Add(label2);
            tabPageHotkeys.Controls.Add(hotkeyVolumeDown);
            tabPageHotkeys.Controls.Add(label1);
            tabPageHotkeys.Controls.Add(hotkeyVolumeUp);
            tabPageHotkeys.Location = new Point(4, 29);
            tabPageHotkeys.Name = "tabPageHotkeys";
            tabPageHotkeys.Padding = new Padding(3);
            tabPageHotkeys.Size = new Size(551, 356);
            tabPageHotkeys.TabIndex = 1;
            tabPageHotkeys.Text = "Hotkeys";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(20, 124);
            label4.Name = "label4";
            label4.Size = new Size(96, 20);
            label4.TabIndex = 15;
            label4.Text = "Balance right";
            // 
            // hotkeyBalanceRight
            // 
            hotkeyBalanceRight.BackColor = Color.White;
            hotkeyBalanceRight.ExternalConflictFlag = false;
            hotkey1.Alt = false;
            hotkey1.Control = false;
            hotkey1.Key = 0;
            hotkey1.Shift = false;
            hotkey1.Win = false;
            hotkeyBalanceRight.Hotkey = hotkey1;
            hotkeyBalanceRight.Location = new Point(126, 121);
            hotkeyBalanceRight.Name = "hotkeyBalanceRight";
            hotkeyBalanceRight.RequireModifier = true;
            hotkeyBalanceRight.Size = new Size(188, 27);
            hotkeyBalanceRight.TabIndex = 14;
            hotkeyBalanceRight.Text = "None";
            hotkeyBalanceRight.TextChanged += hotkeyBalanceRight_OnHotkeyChanged;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(33, 91);
            label3.Name = "label3";
            label3.Size = new Size(87, 20);
            label3.TabIndex = 13;
            label3.Text = "Balance left";
            // 
            // hotkeyBalanceLeft
            // 
            hotkeyBalanceLeft.BackColor = Color.White;
            hotkeyBalanceLeft.ExternalConflictFlag = false;
            hotkey2.Alt = false;
            hotkey2.Control = false;
            hotkey2.Key = 0;
            hotkey2.Shift = false;
            hotkey2.Win = false;
            hotkeyBalanceLeft.Hotkey = hotkey2;
            hotkeyBalanceLeft.Location = new Point(126, 88);
            hotkeyBalanceLeft.Name = "hotkeyBalanceLeft";
            hotkeyBalanceLeft.RequireModifier = true;
            hotkeyBalanceLeft.Size = new Size(188, 27);
            hotkeyBalanceLeft.TabIndex = 12;
            hotkeyBalanceLeft.Text = "None";
            hotkeyBalanceLeft.TextChanged += hotkeyBalanceLeft_OnHotkeyChanged;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(20, 55);
            label2.Name = "label2";
            label2.Size = new Size(100, 20);
            label2.TabIndex = 11;
            label2.Text = "Volume down";
            // 
            // hotkeyVolumeDown
            // 
            hotkeyVolumeDown.BackColor = Color.White;
            hotkeyVolumeDown.ExternalConflictFlag = false;
            hotkey3.Alt = false;
            hotkey3.Control = false;
            hotkey3.Key = 0;
            hotkey3.Shift = false;
            hotkey3.Win = false;
            hotkeyVolumeDown.Hotkey = hotkey3;
            hotkeyVolumeDown.Location = new Point(126, 55);
            hotkeyVolumeDown.Name = "hotkeyVolumeDown";
            hotkeyVolumeDown.RequireModifier = true;
            hotkeyVolumeDown.Size = new Size(188, 27);
            hotkeyVolumeDown.TabIndex = 10;
            hotkeyVolumeDown.Text = "None";
            hotkeyVolumeDown.TextChanged += hotkeyVolumeDown_OnHotkeyChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(40, 22);
            label1.Name = "label1";
            label1.Size = new Size(80, 20);
            label1.TabIndex = 9;
            label1.Text = "Volume up";
            // 
            // hotkeyVolumeUp
            // 
            hotkeyVolumeUp.BackColor = Color.White;
            hotkeyVolumeUp.ExternalConflictFlag = false;
            hotkey4.Alt = false;
            hotkey4.Control = false;
            hotkey4.Key = 0;
            hotkey4.Shift = false;
            hotkey4.Win = false;
            hotkeyVolumeUp.Hotkey = hotkey4;
            hotkeyVolumeUp.Location = new Point(126, 22);
            hotkeyVolumeUp.Name = "hotkeyVolumeUp";
            hotkeyVolumeUp.RequireModifier = true;
            hotkeyVolumeUp.Size = new Size(188, 27);
            hotkeyVolumeUp.TabIndex = 8;
            hotkeyVolumeUp.Text = "None";
            hotkeyVolumeUp.TextChanged += hotkeyVolumeUp_OnHotkeyChanged;
            // 
            // tabPageOsd
            // 
            tabPageOsd.BackColor = SystemColors.Control;
            tabPageOsd.Controls.Add(checkBoxOsdEnabled);
            tabPageOsd.Controls.Add(label12);
            tabPageOsd.Controls.Add(label11);
            tabPageOsd.Controls.Add(trackBarOsdVerPos);
            tabPageOsd.Controls.Add(label10);
            tabPageOsd.Controls.Add(trackBarOsdHorPos);
            tabPageOsd.Location = new Point(4, 29);
            tabPageOsd.Name = "tabPageOsd";
            tabPageOsd.Padding = new Padding(3);
            tabPageOsd.Size = new Size(551, 356);
            tabPageOsd.TabIndex = 2;
            tabPageOsd.Text = "OSD";
            tabPageOsd.Enter += tabPageOsd_Enter;
            tabPageOsd.Leave += tabPageOsd_Leave;
            // 
            // checkBoxOsdEnabled
            // 
            checkBoxOsdEnabled.AutoSize = true;
            checkBoxOsdEnabled.Location = new Point(175, 25);
            checkBoxOsdEnabled.Name = "checkBoxOsdEnabled";
            checkBoxOsdEnabled.Size = new Size(135, 24);
            checkBoxOsdEnabled.TabIndex = 5;
            checkBoxOsdEnabled.Text = "check to enable";
            checkBoxOsdEnabled.UseVisualStyleBackColor = true;
            checkBoxOsdEnabled.CheckedChanged += checkBoxOsdEnabled_CheckedChanged;
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Location = new Point(34, 26);
            label12.Name = "label12";
            label12.Size = new Size(97, 20);
            label12.TabIndex = 4;
            label12.Text = "OSD enabled";
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Location = new Point(34, 96);
            label11.Name = "label11";
            label11.Size = new Size(116, 20);
            label11.TabIndex = 3;
            label11.Text = "Vertical position";
            // 
            // trackBarOsdVerPos
            // 
            trackBarOsdVerPos.Location = new Point(175, 84);
            trackBarOsdVerPos.Maximum = 100;
            trackBarOsdVerPos.Name = "trackBarOsdVerPos";
            trackBarOsdVerPos.Size = new Size(172, 56);
            trackBarOsdVerPos.TabIndex = 2;
            trackBarOsdVerPos.TickFrequency = 10;
            trackBarOsdVerPos.TickStyle = TickStyle.TopLeft;
            trackBarOsdVerPos.ValueChanged += trackBarOsdVerPos_ValueChanged;
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new Point(32, 60);
            label10.Name = "label10";
            label10.Size = new Size(137, 20);
            label10.TabIndex = 1;
            label10.Text = "Horizontal position";
            // 
            // trackBarOsdHorPos
            // 
            trackBarOsdHorPos.LargeChange = 15;
            trackBarOsdHorPos.Location = new Point(175, 48);
            trackBarOsdHorPos.Maximum = 100;
            trackBarOsdHorPos.Name = "trackBarOsdHorPos";
            trackBarOsdHorPos.Size = new Size(172, 56);
            trackBarOsdHorPos.TabIndex = 0;
            trackBarOsdHorPos.TickFrequency = 10;
            trackBarOsdHorPos.TickStyle = TickStyle.TopLeft;
            trackBarOsdHorPos.ValueChanged += trackBarOsdHorPos_ValueChanged;
            // 
            // tabPageApo
            // 
            tabPageApo.BackColor = SystemColors.Control;
            tabPageApo.Controls.Add(checkBoxUseApoVolume);
            tabPageApo.Controls.Add(checkBoxUseApoBalance);
            tabPageApo.Controls.Add(btnApoUninstall);
            tabPageApo.Controls.Add(labelApoStatusValue);
            tabPageApo.Controls.Add(labelApoStatus);
            tabPageApo.Controls.Add(btnApoInstall);
            tabPageApo.Location = new Point(4, 29);
            tabPageApo.Name = "tabPageApo";
            tabPageApo.Padding = new Padding(3);
            tabPageApo.Size = new Size(551, 356);
            tabPageApo.TabIndex = 3;
            tabPageApo.Text = "APO";
            tabPageApo.Enter += tabPageApo_Enter;
            // 
            // checkBoxUseApoVolume
            // 
            checkBoxUseApoVolume.AutoSize = true;
            checkBoxUseApoVolume.Location = new Point(21, 81);
            checkBoxUseApoVolume.Name = "checkBoxUseApoVolume";
            checkBoxUseApoVolume.Size = new Size(164, 24);
            checkBoxUseApoVolume.TabIndex = 5;
            checkBoxUseApoVolume.Text = "Use APO for volume";
            checkBoxUseApoVolume.UseVisualStyleBackColor = true;
            checkBoxUseApoVolume.CheckedChanged += checkBoxUseApoVolume_CheckedChanged;
            // 
            // checkBoxUseApoBalance
            // 
            checkBoxUseApoBalance.AutoSize = true;
            checkBoxUseApoBalance.Location = new Point(21, 51);
            checkBoxUseApoBalance.Name = "checkBoxUseApoBalance";
            checkBoxUseApoBalance.Size = new Size(167, 24);
            checkBoxUseApoBalance.TabIndex = 4;
            checkBoxUseApoBalance.Text = "Use APO for balance";
            checkBoxUseApoBalance.UseVisualStyleBackColor = true;
            checkBoxUseApoBalance.CheckedChanged += checkBoxUseApoBalance_CheckedChanged;
            // 
            // btnApoUninstall
            // 
            btnApoUninstall.Location = new Point(21, 188);
            btnApoUninstall.Name = "btnApoUninstall";
            btnApoUninstall.Size = new Size(132, 37);
            btnApoUninstall.TabIndex = 3;
            btnApoUninstall.Text = "Uninstall APO";
            btnApoUninstall.UseVisualStyleBackColor = true;
            btnApoUninstall.Click += btnApoUninstall_Click;
            // 
            // labelApoStatusValue
            // 
            labelApoStatusValue.AutoSize = true;
            labelApoStatusValue.Location = new Point(161, 17);
            labelApoStatusValue.Name = "labelApoStatusValue";
            labelApoStatusValue.Size = new Size(70, 20);
            labelApoStatusValue.TabIndex = 2;
            labelApoStatusValue.Text = "Unknown";
            // 
            // labelApoStatus
            // 
            labelApoStatus.AutoSize = true;
            labelApoStatus.Location = new Point(27, 16);
            labelApoStatus.Name = "labelApoStatus";
            labelApoStatus.Size = new Size(126, 20);
            labelApoStatus.TabIndex = 1;
            labelApoStatus.Text = "APO install status:";
            // 
            // btnApoInstall
            // 
            btnApoInstall.Location = new Point(21, 126);
            btnApoInstall.Name = "btnApoInstall";
            btnApoInstall.Size = new Size(132, 38);
            btnApoInstall.TabIndex = 0;
            btnApoInstall.Text = "Install APO";
            btnApoInstall.UseVisualStyleBackColor = true;
            btnApoInstall.Click += btnApoInstall_Click;
            // 
            // SettingsForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(612, 496);
            Controls.Add(tabControlSettings);
            Controls.Add(btnSaveConfig);
            Name = "SettingsForm";
            Text = "VolumAPO - Settings";
            FormClosing += SettingsForm_FormClosing;
            Load += Form1_Load;
            tabControlSettings.ResumeLayout(false);
            tabPageGeneral.ResumeLayout(false);
            tabPageGeneral.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)trackBarVolumeSpeed).EndInit();
            tabPageHotkeys.ResumeLayout(false);
            tabPageHotkeys.PerformLayout();
            tabPageOsd.ResumeLayout(false);
            tabPageOsd.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)trackBarOsdVerPos).EndInit();
            ((System.ComponentModel.ISupportInitialize)trackBarOsdHorPos).EndInit();
            tabPageApo.ResumeLayout(false);
            tabPageApo.PerformLayout();
            ResumeLayout(false);
        }

        #endregion
        private Button btnSaveConfig;
        private ToolTip toolTipVolSteps;
        private TabControl tabControlSettings;
        private TabPage tabPageGeneral;
        private TabPage tabPageHotkeys;
        public CheckBox checkBoxTaskBarScroll;
        private Label label9;
        public TextBox textBoxMaxVolume;
        private Label label8;
        private Label label7;
        private Label label6;
        public TrackBar trackBarVolumeSpeed;
        private Label label5;
        private Label label4;
        public Components.HotkeyInputBox hotkeyBalanceRight;
        private Label label3;
        public Components.HotkeyInputBox hotkeyBalanceLeft;
        private Label label2;
        public Components.HotkeyInputBox hotkeyVolumeDown;
        private Label label1;
        public Components.HotkeyInputBox hotkeyVolumeUp;
        private TabPage tabPageOsd;
        private Label label11;
        private Label label10;
        private Label label12;
        public CheckBox checkBoxOsdEnabled;
        public TrackBar trackBarOsdHorPos;
        public TrackBar trackBarOsdVerPos;
        private TabPage tabPageApo;
        private Button btnApoInstall;
        private Label labelApoStatusValue;
        private Label labelApoStatus;
        private Button btnApoUninstall;
        public CheckBox checkBoxUseApoBalance;
        public CheckBox checkBoxUseApoVolume;
    }
}