
namespace PlugIR
{
    partial class FormMain
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.tabPage_3 = new YLib.NewControls.TabPageNew();
            this.richTextBoxConsole = new System.Windows.Forms.RichTextBox();
            this.buttonSend = new YLib.NewControls.ButtonNew();
            this.textBoxSend = new PlugIR.TextBoxEnter();
            this.tabPage_2 = new YLib.NewControls.TabPageNew();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.labelBase2 = new YLib.NewControls.LabelBase();
            this.lbl_CodePowerOn = new System.Windows.Forms.Label();
            this.btnSendPowerOn = new YLib.NewControls.ButtonNew();
            this.btnTestSwitchPower = new YLib.NewControls.ButtonNew();
            this.btnReset = new YLib.NewControls.ButtonNew();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnClose = new YLib.NewControls.ButtonNew();
            this.btnOpen = new YLib.NewControls.ButtonNew();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblState = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cbStopBits = new System.Windows.Forms.ComboBox();
            this.cbDatabits = new System.Windows.Forms.ComboBox();
            this.cbBaudRate = new System.Windows.Forms.ComboBox();
            this.cbParity = new System.Windows.Forms.ComboBox();
            this.cbPortName = new System.Windows.Forms.ComboBox();
            this.chbAutoRun = new YLib.NewControls.CheckBoxNew();
            this.chb_DisableCmd = new YLib.NewControls.CheckBoxNew();
            this.chbAutoHide = new YLib.NewControls.CheckBoxNew();
            this.tabPage_1 = new YLib.NewControls.TabPageNew();
            this.dgv = new PlugIR.DataGridViewExt();
            this.ColumnEnable = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.ColumnName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnIRCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnTypeCommand = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.ColumnCommand = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnTimeReplay = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnTestClick = new System.Windows.Forms.DataGridViewButtonColumn();
            this.labelLastCode = new YLib.NewControls.LabelBase();
            this.tabControlNew1 = new YLib.NewControls.TabControlNew();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextMenuStripTray = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.resetDeviceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.chb_ShowOsdIsReady = new YLib.NewControls.CheckBoxNew();
            this.tabPage_3.SuspendLayout();
            this.tabPage_2.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tabPage_1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).BeginInit();
            this.tabControlNew1.SuspendLayout();
            this.contextMenuStripTray.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabPage_3
            // 
            this.tabPage_3.BackColor = System.Drawing.Color.Silver;
            this.tabPage_3.Controls.Add(this.richTextBoxConsole);
            this.tabPage_3.Controls.Add(this.buttonSend);
            this.tabPage_3.Controls.Add(this.textBoxSend);
            this.tabPage_3.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tabPage_3.ForeColor = System.Drawing.SystemColors.ControlText;
            this.tabPage_3.Location = new System.Drawing.Point(1, 30);
            this.tabPage_3.Name = "tabPage_3";
            this.tabPage_3.Size = new System.Drawing.Size(762, 356);
            this.tabPage_3.TabIndex = 4;
            this.tabPage_3.Text = "tabPage_3";
            this.tabPage_3.TextTitle = "Console";
            // 
            // richTextBoxConsole
            // 
            this.richTextBoxConsole.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.richTextBoxConsole.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBoxConsole.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBoxConsole.ForeColor = System.Drawing.Color.LimeGreen;
            this.richTextBoxConsole.Location = new System.Drawing.Point(0, 0);
            this.richTextBoxConsole.Name = "richTextBoxConsole";
            this.richTextBoxConsole.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.richTextBoxConsole.Size = new System.Drawing.Size(762, 334);
            this.richTextBoxConsole.TabIndex = 2;
            this.richTextBoxConsole.Text = "";
            // 
            // buttonSend
            // 
            this.buttonSend.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonSend.AutoHeight = false;
            this.buttonSend.AutoSize = false;
            this.buttonSend.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.buttonSend.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.buttonSend.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonSend.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonSend.GradientType = YLib.Expansion.GradientTypes.LightDark0;
            this.buttonSend.Location = new System.Drawing.Point(699, 334);
            this.buttonSend.Name = "buttonSend";
            this.buttonSend.Size = new System.Drawing.Size(64, 22);
            this.buttonSend.TabIndex = 1;
            this.buttonSend.Text = "send >>";
            this.buttonSend.Click += new System.EventHandler(this.buttonSend_Click);
            // 
            // textBoxSend
            // 
            this.textBoxSend.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.textBoxSend.BorderColor = System.Drawing.Color.DarkGray;
            this.textBoxSend.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxSend.BorderThickness = 1;
            this.textBoxSend.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.textBoxSend.Location = new System.Drawing.Point(0, 334);
            this.textBoxSend.Name = "textBoxSend";
            this.textBoxSend.Size = new System.Drawing.Size(762, 22);
            this.textBoxSend.TabIndex = 0;
            // 
            // tabPage_2
            // 
            this.tabPage_2.BackColor = System.Drawing.Color.Silver;
            this.tabPage_2.Controls.Add(this.chb_ShowOsdIsReady);
            this.tabPage_2.Controls.Add(this.groupBox2);
            this.tabPage_2.Controls.Add(this.btnReset);
            this.tabPage_2.Controls.Add(this.groupBox1);
            this.tabPage_2.Controls.Add(this.chbAutoRun);
            this.tabPage_2.Controls.Add(this.chb_DisableCmd);
            this.tabPage_2.Controls.Add(this.chbAutoHide);
            this.tabPage_2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tabPage_2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.tabPage_2.Location = new System.Drawing.Point(1, 30);
            this.tabPage_2.Name = "tabPage_2";
            this.tabPage_2.Size = new System.Drawing.Size(762, 356);
            this.tabPage_2.TabIndex = 2;
            this.tabPage_2.Text = "tabPage_2";
            this.tabPage_2.TextTitle = "Settings";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.labelBase2);
            this.groupBox2.Controls.Add(this.lbl_CodePowerOn);
            this.groupBox2.Controls.Add(this.btnSendPowerOn);
            this.groupBox2.Controls.Add(this.btnTestSwitchPower);
            this.groupBox2.Location = new System.Drawing.Point(11, 137);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(696, 87);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Power On";
            // 
            // labelBase2
            // 
            this.labelBase2.Cursor = System.Windows.Forms.Cursors.Default;
            this.labelBase2.ForeColor = System.Drawing.Color.Gray;
            this.labelBase2.Location = new System.Drawing.Point(11, 32);
            this.labelBase2.Name = "labelBase2";
            this.labelBase2.Size = new System.Drawing.Size(71, 14);
            this.labelBase2.TabIndex = 3;
            this.labelBase2.TabStop = false;
            this.labelBase2.Text = "Saved code";
            // 
            // lbl_CodePowerOn
            // 
            this.lbl_CodePowerOn.BackColor = System.Drawing.Color.WhiteSmoke;
            this.lbl_CodePowerOn.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.lbl_CodePowerOn.Location = new System.Drawing.Point(11, 49);
            this.lbl_CodePowerOn.Name = "lbl_CodePowerOn";
            this.lbl_CodePowerOn.Size = new System.Drawing.Size(179, 14);
            this.lbl_CodePowerOn.TabIndex = 2;
            this.lbl_CodePowerOn.Text = "...";
            // 
            // btnSendPowerOn
            // 
            this.btnSendPowerOn.AutoHeight = false;
            this.btnSendPowerOn.AutoSize = false;
            this.btnSendPowerOn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(195)))), ((int)(((byte)(195)))), ((int)(((byte)(195)))));
            this.btnSendPowerOn.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(228)))), ((int)(((byte)(250)))), ((int)(((byte)(249)))));
            this.btnSendPowerOn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSendPowerOn.Location = new System.Drawing.Point(210, 37);
            this.btnSendPowerOn.Name = "btnSendPowerOn";
            this.btnSendPowerOn.Size = new System.Drawing.Size(181, 26);
            this.btnSendPowerOn.TabIndex = 1;
            this.btnSendPowerOn.Text = "Send code for power on";
            this.btnSendPowerOn.TextDy = 4;
            // 
            // btnTestSwitchPower
            // 
            this.btnTestSwitchPower.AutoHeight = false;
            this.btnTestSwitchPower.AutoSize = false;
            this.btnTestSwitchPower.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(195)))), ((int)(((byte)(195)))), ((int)(((byte)(195)))));
            this.btnTestSwitchPower.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(228)))), ((int)(((byte)(250)))), ((int)(((byte)(249)))));
            this.btnTestSwitchPower.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnTestSwitchPower.Location = new System.Drawing.Point(407, 37);
            this.btnTestSwitchPower.Name = "btnTestSwitchPower";
            this.btnTestSwitchPower.Size = new System.Drawing.Size(181, 26);
            this.btnTestSwitchPower.TabIndex = 1;
            this.btnTestSwitchPower.Text = "Test switch power";
            this.btnTestSwitchPower.TextDy = 4;
            // 
            // btnReset
            // 
            this.btnReset.AutoHeight = false;
            this.btnReset.AutoSize = false;
            this.btnReset.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(195)))), ((int)(((byte)(195)))), ((int)(((byte)(195)))));
            this.btnReset.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(228)))), ((int)(((byte)(250)))), ((int)(((byte)(249)))));
            this.btnReset.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnReset.Location = new System.Drawing.Point(358, 255);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(349, 31);
            this.btnReset.TabIndex = 1;
            this.btnReset.Text = "Reset device";
            this.btnReset.TextDy = 4;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnClose);
            this.groupBox1.Controls.Add(this.btnOpen);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.lblState);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.cbStopBits);
            this.groupBox1.Controls.Add(this.cbDatabits);
            this.groupBox1.Controls.Add(this.cbBaudRate);
            this.groupBox1.Controls.Add(this.cbParity);
            this.groupBox1.Controls.Add(this.cbPortName);
            this.groupBox1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.groupBox1.Location = new System.Drawing.Point(11, 11);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(696, 115);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Serial port";
            // 
            // btnClose
            // 
            this.btnClose.AutoHeight = false;
            this.btnClose.AutoSize = false;
            this.btnClose.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(195)))), ((int)(((byte)(195)))), ((int)(((byte)(195)))));
            this.btnClose.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(228)))), ((int)(((byte)(250)))), ((int)(((byte)(249)))));
            this.btnClose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnClose.Location = new System.Drawing.Point(260, 77);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(108, 26);
            this.btnClose.TabIndex = 1;
            this.btnClose.Text = "Close";
            this.btnClose.TextDy = 4;
            // 
            // btnOpen
            // 
            this.btnOpen.AutoHeight = false;
            this.btnOpen.AutoSize = false;
            this.btnOpen.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(195)))), ((int)(((byte)(195)))), ((int)(((byte)(195)))));
            this.btnOpen.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(228)))), ((int)(((byte)(250)))), ((int)(((byte)(249)))));
            this.btnOpen.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnOpen.Location = new System.Drawing.Point(146, 77);
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(108, 26);
            this.btnOpen.TabIndex = 1;
            this.btnOpen.Text = "Open";
            this.btnOpen.TextDy = 4;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.label4.Location = new System.Drawing.Point(438, 23);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(56, 14);
            this.label4.TabIndex = 2;
            this.label4.Text = "Stop bits";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.label5.Location = new System.Drawing.Point(261, 23);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(51, 14);
            this.label5.TabIndex = 2;
            this.label5.Text = "Databits";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.label3.Location = new System.Drawing.Point(147, 23);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 14);
            this.label3.TabIndex = 2;
            this.label3.Text = "Baudrate";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.label2.Location = new System.Drawing.Point(340, 23);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(37, 14);
            this.label2.TabIndex = 2;
            this.label2.Text = "Parity";
            // 
            // lblState
            // 
            this.lblState.AutoSize = true;
            this.lblState.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.lblState.Location = new System.Drawing.Point(9, 82);
            this.lblState.Name = "lblState";
            this.lblState.Size = new System.Drawing.Size(124, 14);
            this.lblState.TabIndex = 2;
            this.lblState.Text = "State: COM1 - Closed";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.label1.Location = new System.Drawing.Point(11, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 14);
            this.label1.TabIndex = 2;
            this.label1.Text = "Name port";
            // 
            // cbStopBits
            // 
            this.cbStopBits.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbStopBits.FormattingEnabled = true;
            this.cbStopBits.Location = new System.Drawing.Point(438, 40);
            this.cbStopBits.Name = "cbStopBits";
            this.cbStopBits.Size = new System.Drawing.Size(62, 22);
            this.cbStopBits.TabIndex = 1;
            // 
            // cbDatabits
            // 
            this.cbDatabits.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbDatabits.FormattingEnabled = true;
            this.cbDatabits.Location = new System.Drawing.Point(260, 40);
            this.cbDatabits.Name = "cbDatabits";
            this.cbDatabits.Size = new System.Drawing.Size(71, 22);
            this.cbDatabits.TabIndex = 1;
            // 
            // cbBaudRate
            // 
            this.cbBaudRate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbBaudRate.FormattingEnabled = true;
            this.cbBaudRate.Location = new System.Drawing.Point(146, 40);
            this.cbBaudRate.Name = "cbBaudRate";
            this.cbBaudRate.Size = new System.Drawing.Size(108, 22);
            this.cbBaudRate.TabIndex = 1;
            // 
            // cbParity
            // 
            this.cbParity.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbParity.FormattingEnabled = true;
            this.cbParity.Location = new System.Drawing.Point(337, 40);
            this.cbParity.Name = "cbParity";
            this.cbParity.Size = new System.Drawing.Size(95, 22);
            this.cbParity.TabIndex = 1;
            // 
            // cbPortName
            // 
            this.cbPortName.FormattingEnabled = true;
            this.cbPortName.Location = new System.Drawing.Point(11, 40);
            this.cbPortName.Name = "cbPortName";
            this.cbPortName.Size = new System.Drawing.Size(121, 22);
            this.cbPortName.TabIndex = 1;
            this.cbPortName.DropDown += new System.EventHandler(this.cbPort_DropDown);
            // 
            // chbAutoRun
            // 
            this.chbAutoRun.AutoHeight = false;
            this.chbAutoRun.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.chbAutoRun.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.chbAutoRun.BorderColor = System.Drawing.Color.Gray;
            this.chbAutoRun.Checked = true;
            this.chbAutoRun.CheckSize = 2;
            this.chbAutoRun.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.chbAutoRun.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.chbAutoRun.GradientType = YLib.Expansion.GradientTypes.DarkLight90;
            this.chbAutoRun.Location = new System.Drawing.Point(11, 231);
            this.chbAutoRun.Margin = new System.Windows.Forms.Padding(6);
            this.chbAutoRun.Name = "chbAutoRun";
            this.chbAutoRun.Size = new System.Drawing.Size(183, 16);
            this.chbAutoRun.TabIndex = 0;
            this.chbAutoRun.Text = "Autorun after start windows";
            this.chbAutoRun.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chbAutoRun.WidthCheckBox = 16;
            // 
            // chb_DisableCmd
            // 
            this.chb_DisableCmd.AutoHeight = false;
            this.chb_DisableCmd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.chb_DisableCmd.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.chb_DisableCmd.BorderColor = System.Drawing.Color.Gray;
            this.chb_DisableCmd.Checked = true;
            this.chb_DisableCmd.CheckSize = 2;
            this.chb_DisableCmd.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.chb_DisableCmd.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.chb_DisableCmd.GradientType = YLib.Expansion.GradientTypes.DarkLight90;
            this.chb_DisableCmd.Location = new System.Drawing.Point(11, 289);
            this.chb_DisableCmd.Margin = new System.Windows.Forms.Padding(6);
            this.chb_DisableCmd.Name = "chb_DisableCmd";
            this.chb_DisableCmd.Size = new System.Drawing.Size(249, 16);
            this.chb_DisableCmd.TabIndex = 0;
            this.chb_DisableCmd.Text = "Disable commands while active window";
            this.chb_DisableCmd.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chb_DisableCmd.WidthCheckBox = 16;
            // 
            // chbAutoHide
            // 
            this.chbAutoHide.AutoHeight = false;
            this.chbAutoHide.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.chbAutoHide.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.chbAutoHide.BorderColor = System.Drawing.Color.Gray;
            this.chbAutoHide.Checked = true;
            this.chbAutoHide.CheckSize = 2;
            this.chbAutoHide.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.chbAutoHide.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.chbAutoHide.GradientType = YLib.Expansion.GradientTypes.DarkLight90;
            this.chbAutoHide.Location = new System.Drawing.Point(11, 261);
            this.chbAutoHide.Margin = new System.Windows.Forms.Padding(6);
            this.chbAutoHide.Name = "chbAutoHide";
            this.chbAutoHide.Size = new System.Drawing.Size(187, 16);
            this.chbAutoHide.TabIndex = 0;
            this.chbAutoHide.Text = "Autohide after start program";
            this.chbAutoHide.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chbAutoHide.WidthCheckBox = 16;
            // 
            // tabPage_1
            // 
            this.tabPage_1.Controls.Add(this.dgv);
            this.tabPage_1.Location = new System.Drawing.Point(1, 30);
            this.tabPage_1.Name = "tabPage_1";
            this.tabPage_1.Size = new System.Drawing.Size(762, 356);
            this.tabPage_1.TabIndex = 0;
            this.tabPage_1.Text = "tabPage_1";
            this.tabPage_1.TextTitle = "List IR commands";
            // 
            // dgv
            // 
            this.dgv.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnEnable,
            this.ColumnName,
            this.ColumnIRCode,
            this.ColumnTypeCommand,
            this.ColumnCommand,
            this.ColumnTimeReplay,
            this.ColumnTestClick});
            this.dgv.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv.GridColor = System.Drawing.Color.LightGray;
            this.dgv.Location = new System.Drawing.Point(0, 0);
            this.dgv.Name = "dgv";
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.White;
            this.dgv.RowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgv.Size = new System.Drawing.Size(762, 356);
            this.dgv.TabIndex = 4;
            this.dgv.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_CellContentClick);
            // 
            // ColumnEnable
            // 
            this.ColumnEnable.HeaderText = "";
            this.ColumnEnable.MinimumWidth = 15;
            this.ColumnEnable.Name = "ColumnEnable";
            this.ColumnEnable.ReadOnly = true;
            this.ColumnEnable.Width = 18;
            // 
            // ColumnName
            // 
            this.ColumnName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ColumnName.HeaderText = "Name";
            this.ColumnName.MinimumWidth = 100;
            this.ColumnName.Name = "ColumnName";
            // 
            // ColumnIRCode
            // 
            this.ColumnIRCode.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.ColumnIRCode.HeaderText = "IRCode";
            this.ColumnIRCode.MinimumWidth = 100;
            this.ColumnIRCode.Name = "ColumnIRCode";
            this.ColumnIRCode.ReadOnly = true;
            // 
            // ColumnTypeCommand
            // 
            this.ColumnTypeCommand.HeaderText = "TypeCommand";
            this.ColumnTypeCommand.MinimumWidth = 120;
            this.ColumnTypeCommand.Name = "ColumnTypeCommand";
            this.ColumnTypeCommand.Width = 120;
            // 
            // ColumnCommand
            // 
            this.ColumnCommand.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ColumnCommand.HeaderText = "Command";
            this.ColumnCommand.MinimumWidth = 150;
            this.ColumnCommand.Name = "ColumnCommand";
            // 
            // ColumnTimeReplay
            // 
            this.ColumnTimeReplay.HeaderText = "TimeReplay";
            this.ColumnTimeReplay.MinimumWidth = 80;
            this.ColumnTimeReplay.Name = "ColumnTimeReplay";
            this.ColumnTimeReplay.Width = 80;
            // 
            // ColumnTestClick
            // 
            this.ColumnTestClick.HeaderText = "Test";
            this.ColumnTestClick.MinimumWidth = 30;
            this.ColumnTestClick.Name = "ColumnTestClick";
            this.ColumnTestClick.Width = 50;
            // 
            // labelLastCode
            // 
            this.labelLastCode.AutoHeight = false;
            this.labelLastCode.AutoSize = false;
            this.labelLastCode.BackColor = System.Drawing.Color.DimGray;
            this.labelLastCode.Cursor = System.Windows.Forms.Cursors.Default;
            this.labelLastCode.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.labelLastCode.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(95)))), ((int)(((byte)(202)))), ((int)(((byte)(99)))));
            this.labelLastCode.Location = new System.Drawing.Point(0, 387);
            this.labelLastCode.Name = "labelLastCode";
            this.labelLastCode.Size = new System.Drawing.Size(764, 24);
            this.labelLastCode.TabIndex = 3;
            this.labelLastCode.TabStop = false;
            this.labelLastCode.Text = "ir code";
            this.labelLastCode.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.labelLastCode.TextDy = 4;
            // 
            // tabControlNew1
            // 
            this.tabControlNew1.BorderColor = System.Drawing.Color.Gray;
            this.tabControlNew1.BtnGeneralBorderColor = System.Drawing.Color.Gray;
            this.tabControlNew1.BtnGeneralFont = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tabControlNew1.BtnGeneralWidth = 131;
            this.tabControlNew1.BtnSelectedFC = System.Drawing.Color.White;
            this.tabControlNew1.BtnUnSelectedFC = System.Drawing.Color.White;
            this.tabControlNew1.Controls.Add(this.tabPage_1);
            this.tabControlNew1.Controls.Add(this.tabPage_2);
            this.tabControlNew1.Controls.Add(this.tabPage_3);
            this.tabControlNew1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlNew1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tabControlNew1.Location = new System.Drawing.Point(0, 0);
            this.tabControlNew1.Name = "tabControlNew1";
            this.tabControlNew1.SelectedIndex = 1;
            this.tabControlNew1.Size = new System.Drawing.Size(764, 387);
            this.tabControlNew1.TabIndex = 0;
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.BalloonTipText = "PlugIR";
            this.notifyIcon1.ContextMenuStrip = this.contextMenuStripTray;
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Text = "PlugIR";
            this.notifyIcon1.Visible = true;
            this.notifyIcon1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon1_MouseClick);
            // 
            // contextMenuStripTray
            // 
            this.contextMenuStripTray.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.resetDeviceToolStripMenuItem,
            this.toolStripSeparator1,
            this.exitToolStripMenuItem});
            this.contextMenuStripTray.Name = "contextMenuStripTray";
            this.contextMenuStripTray.Size = new System.Drawing.Size(138, 54);
            // 
            // resetDeviceToolStripMenuItem
            // 
            this.resetDeviceToolStripMenuItem.Image = global::PlugIR.Properties.Resources.reload_3;
            this.resetDeviceToolStripMenuItem.Name = "resetDeviceToolStripMenuItem";
            this.resetDeviceToolStripMenuItem.Size = new System.Drawing.Size(137, 22);
            this.resetDeviceToolStripMenuItem.Text = "ResetDevice";
            this.resetDeviceToolStripMenuItem.Click += new System.EventHandler(this.resetDeviceToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(134, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Image = global::PlugIR.Properties.Resources.Close;
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(137, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // chb_ShowOsdIsReady
            // 
            this.chb_ShowOsdIsReady.AutoHeight = false;
            this.chb_ShowOsdIsReady.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.chb_ShowOsdIsReady.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.chb_ShowOsdIsReady.BorderColor = System.Drawing.Color.Gray;
            this.chb_ShowOsdIsReady.Checked = true;
            this.chb_ShowOsdIsReady.CheckSize = 2;
            this.chb_ShowOsdIsReady.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.chb_ShowOsdIsReady.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.chb_ShowOsdIsReady.GradientType = YLib.Expansion.GradientTypes.DarkLight90;
            this.chb_ShowOsdIsReady.Location = new System.Drawing.Point(11, 317);
            this.chb_ShowOsdIsReady.Margin = new System.Windows.Forms.Padding(6);
            this.chb_ShowOsdIsReady.Name = "chb_ShowOsdIsReady";
            this.chb_ShowOsdIsReady.Size = new System.Drawing.Size(210, 16);
            this.chb_ShowOsdIsReady.TabIndex = 5;
            this.chb_ShowOsdIsReady.Text = "Show OSD \'Is Ready\' by opening";
            this.chb_ShowOsdIsReady.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chb_ShowOsdIsReady.WidthCheckBox = 16;
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Silver;
            this.ClientSize = new System.Drawing.Size(764, 411);
            this.Controls.Add(this.tabControlNew1);
            this.Controls.Add(this.labelLastCode);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ForeColor = System.Drawing.Color.Black;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "FormMain";
            this.Text = "PlugIR";
            this.tabPage_3.ResumeLayout(false);
            this.tabPage_3.PerformLayout();
            this.tabPage_2.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tabPage_1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).EndInit();
            this.tabControlNew1.ResumeLayout(false);
            this.contextMenuStripTray.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private YLib.NewControls.TabPageNew tabPage_3;
        private YLib.NewControls.TabPageNew tabPage_2;
        private YLib.NewControls.TabPageNew tabPage_1;
        private YLib.NewControls.TabControlNew tabControlNew1;
        private YLib.NewControls.CheckBoxNew chbAutoRun;
        private YLib.NewControls.CheckBoxNew chbAutoHide;
        private System.Windows.Forms.RichTextBox richTextBoxConsole;
        private YLib.NewControls.ButtonNew buttonSend;
        private TextBoxEnter textBoxSend;
        private YLib.NewControls.LabelBase labelLastCode;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private DataGridViewExt dgv;
        private System.Windows.Forms.DataGridViewCheckBoxColumn ColumnEnable;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnName;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnIRCode;
        private System.Windows.Forms.DataGridViewComboBoxColumn ColumnTypeCommand;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnCommand;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnTimeReplay;
        private System.Windows.Forms.DataGridViewButtonColumn ColumnTestClick;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripTray;
        private System.Windows.Forms.ToolStripMenuItem resetDeviceToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbStopBits;
        private System.Windows.Forms.ComboBox cbBaudRate;
        private System.Windows.Forms.ComboBox cbParity;
        private System.Windows.Forms.ComboBox cbPortName;
        private YLib.NewControls.CheckBoxNew chb_DisableCmd;
        private YLib.NewControls.ButtonNew btnReset;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cbDatabits;
        private YLib.NewControls.ButtonNew btnTestSwitchPower;
        private System.Windows.Forms.Label lblState;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label lbl_CodePowerOn;
        private YLib.NewControls.ButtonNew btnSendPowerOn;
        private YLib.NewControls.ButtonNew btnClose;
        private YLib.NewControls.ButtonNew btnOpen;
        private YLib.NewControls.LabelBase labelBase2;
        private YLib.NewControls.CheckBoxNew chb_ShowOsdIsReady;
    }
}

