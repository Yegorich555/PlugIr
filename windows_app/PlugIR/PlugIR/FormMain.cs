using System;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;
using System.Reflection;
using YLib.Extensions;
using System.Drawing;
using System.Collections.Generic;
using System.IO;
using YLib.NewControls;
using YLib;
using System.Text;
using Microsoft.Win32;
using System.Security.Principal;
using System.Security.AccessControl;


namespace PlugIR
{
    public partial class FormMain : Form
    {
        AsyncOperation asyncOperation;
        List<IrCommand> listCommands = new List<IrCommand>();
        bool loaded;

        public FormMain()
        {
            try
            {
                InitializeComponent();

                var pricipal = new WindowsPrincipal(WindowsIdentity.GetCurrent());
                var adminRights = pricipal.IsInRole(WindowsBuiltInRole.Administrator);
                if (adminRights)
                    Text += " - Admin";
                var writeAllow = HasWritePermissionOnDir(PathConfig);
                Text += " - Write config: " + (writeAllow ? "allow" : "deni");

                asyncOperation = AsyncOperationManager.CreateOperation(null);
                InitSerialConfigurator();
                LoadConfig();

                if (chbAutoHide.Checked)
                {
                    ShowInTaskbar = false;
                    WindowState = FormWindowState.Minimized;
                }
                InitTable();
                InitSerialPort();
                textBoxSend.KeyEnterClick += TextBoxSend_KeyEnterClick;

                btnOpen.Click += BtnOpen_Click;
                btnClose.Click += BtnClose_Click;
                btnSendPowerOn.Click += BtnSendPowerOn_Click;
                btnTestSwitchPower.Click += BtnTestSwitchPower_Click;
                chbAutoRun.Click += ChbAutoRun_Click;
                btnReset.Click += BtnReset_Click;
                loaded = true;

                ChangeStartup();
            }
            catch (Exception ex) { MessageBox.Show("Error " + GetType().FullName + ": " + MethodBase.GetCurrentMethod().Name + "()\n" + ex.ToString(), "Error " + Name); }
        }

        bool HasWritePermissionOnDir(string path)
        {
            try
            {
                var wrAllow = false;
                var wrDeny = false;
                var accessControlList = Directory.GetAccessControl(path);
                if (accessControlList == null)
                    return false;
                var accessRules = accessControlList.GetAccessRules(true, true, typeof(SecurityIdentifier));
                if (accessRules == null)
                    return false;

                foreach (FileSystemAccessRule rule in accessRules)
                {
                    if ((FileSystemRights.Write & rule.FileSystemRights) != FileSystemRights.Write)
                        continue;

                    if (rule.AccessControlType == AccessControlType.Allow)
                        wrAllow = true;
                    else if (rule.AccessControlType == AccessControlType.Deny)
                        wrDeny = true;
                }

                return wrAllow && !wrDeny;
            }
            catch (Exception ex) { MessageBox.Show("Error " + GetType().FullName + ": " + MethodBase.GetCurrentMethod().Name + "()\n" + ex.ToString(), "Error " + Name); }
            return false;
        }

        private void BtnReset_Click(object sender, EventArgs e)
        {
            ResetDevice();
        }

        private void ChbAutoRun_Click(object sender, EventArgs e)
        {
            ChangeStartup();
        }

        private void BtnTestSwitchPower_Click(object sender, EventArgs e)
        {
            SerialPortSend("test");
        }

        private void BtnSendPowerOn_Click(object sender, EventArgs e)
        {
            if (!lastCode.Empty)
                SerialPortSend("set:", lastCode);
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            try
            {
                if (serialPort != null)
                    serialPort.Close();
            }
            catch (Exception ex) { MessageBox.Show("Error " + GetType().FullName + ": " + MethodBase.GetCurrentMethod().Name + "()\n" + ex.ToString(), "Error " + Name); }
        }

        private void BtnOpen_Click(object sender, EventArgs e)
        {
            try
            {
                if (serialPort != null)
                    serialPort.Close();
                InitSerialPort();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error " + GetType().FullName + ": " + MethodBase.GetCurrentMethod().Name + "()\n" + ex.ToString(), "Error " + Name);
            }
        }

        void InitSerialConfigurator()
        {
            foreach (int item in Enum.GetValues(typeof(BaudRates)))
                cbBaudRate.Items.Add(item);
            foreach (int item in Enum.GetValues(typeof(DataBits)))
                cbDatabits.Items.Add(item);
            foreach (Parity item in Enum.GetValues(typeof(Parity)))
                cbParity.Items.Add(item);
            foreach (StopBits item in Enum.GetValues(typeof(StopBits)))
                cbStopBits.Items.Add(item);

            cbBaudRate.SelectedIndex = 0;
            cbDatabits.SelectedIndex = 0;
            cbParity.SelectedIndex = 0;
            cbStopBits.SelectedIndex = 0;

            SetPortNames();
        }

        #region Load & Save

        const string configName = "config.ini";
        const char cfgSeparator = '=';
        const string cfgKeyCmd = "cmdIR";
        const string cfgProperty = ">";

        string _pathConfig;
        string PathConfig
        {
            get
            {
                if (_pathConfig == null)
                    _pathConfig = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\" + configName;
                return _pathConfig;
            }
        }

        void LoadConfig()
        {
            try
            {
                if (!File.Exists(PathConfig))
                    return;

                var readLines = File.ReadAllLines(PathConfig);
                for (int i = 0; i < readLines.Length; ++i)
                {
                    var keyVal = readLines[i].Split(cfgSeparator);
                    if (keyVal.Length < 2)
                        continue;
                    var key = keyVal[0];
                    var val = keyVal[1];

                    if (key == cfgKeyCmd)
                    {
                        var cmdStroke = val.Split('>');
                        listCommands.Add(IrCommand.Parse(cmdStroke));
                    }
                    if (key[0] == '>')
                    {
                        key = key.Substring(1);
                        if (key.IsText("Width"))
                            Width = Convert.ToInt16(val);
                        else if (key.IsText("Height"))
                            Height = Convert.ToInt16(val);
                    }
                    else //find name control
                    {
                        var controls = Controls.Find(keyVal[0], true);
                        if (controls.Length < 1)
                            continue;

                        var ctrl = controls[0];

                        if (ctrl is CheckBox)
                            ((CheckBox)ctrl).Checked = bool.Parse(val);
                        else if (ctrl is CheckBoxNew)
                            ((CheckBoxNew)ctrl).Checked = bool.Parse(val);
                        else if (ctrl is ComboBox)
                        {
                            if (ctrl == cbPortName)
                            {
                                if (!string.IsNullOrWhiteSpace(val))
                                    ((ComboBox)ctrl).SelectedItem = val;
                            }
                            else
                            {
                                int value;
                                if (int.TryParse(val, out value))
                                    ((ComboBox)ctrl).SelectedIndex = value < 0 ? 0 : value;
                            }
                              
                        }
                        else
                            ctrl.Text = val;
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show("Error " + GetType().FullName + ": " + MethodBase.GetCurrentMethod().Name + "()\n" + ex.ToString(), "Error " + Name); }
        }

        void Save_Config()
        {
            try
            {
                var listSaved = new List<string>();
                foreach (var cmd in listCommands)
                    listSaved.Add(cfgKeyCmd + cfgSeparator + cmd);

                GetSaveAllControls(this, listSaved);

                listSaved.Add(cfgProperty + "Width" + cfgSeparator + RestoreBounds.Width);
                listSaved.Add(cfgProperty + "Height" + cfgSeparator + RestoreBounds.Height);

                using (var file = new StreamWriter(configName, false))
                {
                    foreach (string line in listSaved)
                        file.WriteLine(line);
                }//using
            }
            catch (Exception ex) { MessageBox.Show("Error " + GetType().FullName + ": " + MethodBase.GetCurrentMethod().Name + "()\n" + ex.ToString(), "Error " + Name); }
        }

        void GetSaveAllControls(Control ctrl, List<string> listSaved)

        {
            try
            {
                object value = null;

                if (ctrl is CheckBox)
                    value = ((CheckBox)ctrl).Checked;
                else if (ctrl is CheckBoxNew)
                    value = ((CheckBoxNew)ctrl).Checked;
                else if (ctrl is ComboBox)
                {
                    if (ctrl == cbPortName)
                        value = ((ComboBox)ctrl).SelectedItem;
                    else
                        value = ((ComboBox)ctrl).SelectedIndex;
                }
                else if (ctrl is TextBox)
                    value = ((TextBox)ctrl).Text;

                if (value != null)
                    listSaved.Add(ctrl.Name + cfgSeparator + value.ToString().Replace(Environment.NewLine, "//n").Replace("\n", "//n"));

                foreach (Control CtrlChild in ctrl.Controls)
                    GetSaveAllControls(CtrlChild, listSaved);
            }
            catch (Exception ex) { MessageBox.Show("Error " + GetType().FullName + ": " + MethodBase.GetCurrentMethod().Name + "()\n" + ex.ToString(), "Error " + Name); }
        }

        #endregion

        DataGridViewTextBoxColumn ColumnId;
        void InitTable()
        {
            ColumnTypeCommand.ValueType = typeof(TypeCommands);
            ColumnTypeCommand.DataSource = Enum.GetValues(typeof(TypeCommands));

            ColumnId = new DataGridViewTextBoxColumn();
            ColumnId.Visible = false;
            dgv.Columns.Add(ColumnId);

            foreach (var cmd in listCommands)
                dgv.Rows.Add(cmd.Enable, cmd.Name, cmd.IrCode, cmd.TypeCommand, cmd.Command, cmd.Timeout, "", cmd.Id);

            dgv.CellValueChanged += Dgv_CellValueChanged;
            dgv.RowsAdded += Dgv_RowsAdded;
            dgv.RowsRemoved += Dgv_RowsRemoved;
        }

        void InitSerialPort()
        {
            SetSerialConfig();
            Task.Factory.StartNew(() =>
            {
                SerialPortWorker();
            });
        }

        void SetSerialConfig()
        {
            try
            {
                NamePort = cbPortName.SelectedItem.ToStringNull();
                BaudRate = (BaudRates)cbBaudRate.SelectedItem;
                DataBits = (DataBits)cbDatabits.SelectedItem;
                Parity = (Parity)cbParity.SelectedItem;
                StopBits = (StopBits)cbStopBits.SelectedItem;
                SerialConfigValidated = true;
            }
            catch (Exception ex)
            {
                SerialConfigValidated = false;
                MessageBox.Show("Error " + GetType().FullName + ": " + MethodBase.GetCurrentMethod().Name + "()\n" + ex.ToString(), "Error " + Name);
            }
        }

        bool SerialConfigValidated { get; set; }

        static string NamePort;
        static BaudRates BaudRate;
        static DataBits DataBits;
        static Parity Parity;
        static StopBits StopBits;

        SafeSerialPort serialPort;
        void SerialPortWorker()
        {
            try
            {
                if (string.IsNullOrWhiteSpace(NamePort))
                    return;
                using (serialPort = new SafeSerialPort(NamePort, BaudRate, DataBits, Parity, StopBits))
                {
                    serialPort.NewLine = "\r\n";
                    var portNames = SerialPort.GetPortNames();
                    if (SerialConfigValidated && portNames.Contains(serialPort.PortName, StringComparer.OrdinalIgnoreCase))
                    {
                        serialPort.Open();
                        asyncOperation.Post(() => { lblState.Text = "State: " + serialPort.PortName + " opened"; });
                    }
                    else
                        asyncOperation.Post(() => SetIconByStateIr(StateIr.Error));

                    while (true)
                    {
                        System.Threading.Thread.Sleep(1);
                        if (!serialPort.IsOpen)
                            break;

                        if (serialPort.BytesToRead > 0)
                        {
                            var readStr = serialPort.TryReadLine();
                            if (readStr == null)
                                continue;
                            if (readStr[0] != '>' || readStr.Length < 2 || readStr[readStr.Length - 2] != 0x0D || readStr.Last() != 0x0A)
                                continue;
                            object readPost;
                            if (readStr[1] == '>')
                                readPost = readStr.Substring(2, readStr.Length - 3);
                            else
                            {
                                var resultRead = readStr.Substring(1, readStr.Length - 3);
                                var code = new RC5code(resultRead);
                                if (WindowState == FormWindowState.Minimized || !chb_DisableCmd.Checked)
                                {
                                    foreach (var cmdIR in listCommands)
                                    {
                                        if (cmdIR.Enable && cmdIR.IrCode == code)
                                            cmdIR.RunCommand();
                                    }
                                }
                                readPost = code;
                            }
                            asyncOperation.Post(CodeReaded, readPost);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                asyncOperation.Post(() => SetIconByStateIr(StateIr.Error));
                MessageBox.Show("Error " + GetType().FullName + ": " + MethodBase.GetCurrentMethod().Name + "()\n" + ex.ToString());
            }
            try
            {
                asyncOperation.Post(() => { lblState.Text = "State: ..."; });
            }
            catch (Exception ex) { MessageBox.Show("Error " + GetType().FullName + ": " + MethodBase.GetCurrentMethod().Name + "()\n" + ex.ToString(), "Error " + Name); }

        }

        DataGridViewCell lastCell;
        static RC5code lastCode;
        void CodeReaded(object state)
        {
            richTextBoxConsole.AppendLine(state);
            richTextBoxConsole.ScrollToCaret();
            labelLastCode.Text = state.ToStringNull();

            SetIconByStateIr(StateIr.Receive);

            if (!(state is RC5code))
                return;
            var code = (RC5code)state;
            lastCode = code;

            if (dgv.CurrentCell.ColumnIndex == ColumnIRCode.Index)
                dgv.CurrentCell.Value = code;
            else
                dgv[ColumnIRCode.Index, dgv.LastRow.Index].Value = code;

            for (int i = 0; i < dgv.Rows.Count; ++i)
            {
                var cell = dgv[ColumnIRCode.Index, i];

                if (cell.Value != null && (RC5code)cell.Value == code)
                {
                    cell.Style.BackColor = Color.LightBlue;
                    cell.Style.ForeColor = Color.White;
                    if (lastCell != null && lastCell != cell)
                    {
                        lastCell.Style.BackColor = ColumnIRCode.DefaultCellStyle.BackColor;
                        lastCell.Style.ForeColor = ColumnIRCode.DefaultCellStyle.ForeColor;
                    }
                    lastCell = cell;
                    break;
                }
            }
        }

        private void Dgv_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            try
            {
                for (int i = 0; i < listCommands.Count; ++i)
                {
                    var cmd = listCommands[i];
                    bool find = false;
                    foreach (DataGridViewRow row in dgv.Rows)
                    {
                        var value = row.Cells[ColumnId.Index].Value;
                        if (value != null && (int)value == cmd.Id)
                        {
                            find = true;
                            break;
                        }
                    }
                    if (!find)
                        listCommands.RemoveAt(i);
                }
            }
            catch (Exception ex) { MessageBox.Show("Error " + GetType().FullName + ": " + MethodBase.GetCurrentMethod().Name + "()\n" + ex.ToString()); }

        }

        void Dgv_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            try
            {
                var cells = dgv.Rows[dgv.Rows.Count - 2].Cells;
                var value = cells[ColumnIRCode.Index].Value;

                RC5code code;
                if (value != null)
                    code = (RC5code)value;
                else
                    code = new RC5code(new byte[] { 0 });

                var cmd = new IrCommand(false, "Name " + (dgv.RowCount - 2), code, TypeCommands.SendKeys, "", 1000);
                listCommands.Add(cmd);

                lockCellChanged = true;
                cells[ColumnEnable.Index].Value = cmd.Enable;
                cells[ColumnName.Index].Value = cmd.Name;
                cells[ColumnIRCode.Index].Value = cmd.IrCode;
                cells[ColumnTypeCommand.Index].Value = cmd.TypeCommand;
                cells[ColumnCommand.Index].Value = cmd.Command;
                cells[ColumnTimeReplay.Index].Value = cmd.Timeout;
                cells[ColumnId.Index].Value = cmd.Id;
                lockCellChanged = false;
            }
            catch (Exception ex) { MessageBox.Show("Error " + GetType().FullName + ": " + MethodBase.GetCurrentMethod().Name + "()\n" + ex.ToString()); }
        }

        bool lockCellChanged;
        void Dgv_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (lockCellChanged)
                    return;
                if (e.ColumnIndex < 0 || e.RowIndex > dgv.RowCount - 2)
                    return;

                var curCmd = GetCommand(Convert.ToInt32(dgv[ColumnId, e.RowIndex].Value));

                var value = dgv[e.ColumnIndex, e.RowIndex].Value;
                if (e.ColumnIndex == ColumnEnable.Index)
                    curCmd.Enable = (bool)value;
                else if (e.ColumnIndex == ColumnName.Index)
                    curCmd.Name = (string)value;
                else if (e.ColumnIndex == ColumnIRCode.Index)
                    curCmd.IrCode = (RC5code)value;
                else if (e.ColumnIndex == ColumnTypeCommand.Index)
                    curCmd.TypeCommand = (TypeCommands)value;
                else if (e.ColumnIndex == ColumnCommand.Index)
                    curCmd.Command = (string)value;
                else if (e.ColumnIndex == ColumnTimeReplay.Index)
                    curCmd.Timeout = Convert.ToInt32(value);

                lockCellChanged = true;
                dgv[ColumnId, e.RowIndex].Value = curCmd.Id;
                lockCellChanged = false;
            }
            catch (Exception ex) { MessageBox.Show("Error " + GetType().FullName + ": " + MethodBase.GetCurrentMethod().Name + "()\n" + ex.ToString(), "Error " + Name); }

        }

        IrCommand GetCommand(int id)
        {
            foreach (var cmd in listCommands)
                if (cmd.Id == id)
                    return cmd;
            return null;
        }


        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (dgv != null)
            {
                var cell = dgv.CurrentCell;
                if (cell.ColumnIndex == ColumnCommand.Index &&
                    !cell.IsInEditMode &&
                    dgv[ColumnTypeCommand.Index, cell.RowIndex].Value != null &&
                    (TypeCommands)dgv[ColumnTypeCommand.Index, cell.RowIndex].Value == TypeCommands.SendKeys)
                {
                    cell.Value = keyData.ToStringExt();
                    return true;
                }
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void dgv_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex < 0 || dgv.Columns[e.ColumnIndex] != ColumnTestClick || e.RowIndex < 0)
                return;
            if (e.RowIndex >= listCommands.Count)
                return;
            listCommands[e.RowIndex].RunCommand();
        }

        void ResetDevice()
        {
            SerialPortSend("reset");
        }

        bool userClose;
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            userClose = true;
            Close();
        }

        private void resetDeviceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ResetDevice();
        }

        private void notifyIcon1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left)
                return;
            if (WindowState == FormWindowState.Minimized)
                ShowWindow();
            else
                Set_Hide();
        }

        void Set_Hide()
        {
            WindowState = FormWindowState.Minimized;
            Hide();
            if (loaded)
                Save_Config();
            // Set_AutoRun();
        }

        void ShowWindow()
        {
            ShowInTaskbar = true;
            Show();
            Activate();
            WindowState = FormWindowState.Normal;
            // EndEdit_TextBoxes(this);
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            if (!userClose)
            {
                Set_Hide();
                e.Cancel = true;
            }
            else
                Save_Config();

            base.OnClosing(e);
        }


        protected override void OnDeactivate(EventArgs e)
        {
            base.OnDeactivate(e);
            if (WindowState == FormWindowState.Minimized)
                Set_Hide();
        }

        private void TextBoxSend_KeyEnterClick(object sender, EventArgs e)
        {
            SendConsole();
            ((TextBox)sender).Text = "";
        }

        private void buttonSend_Click(object sender, EventArgs e)
        {
            SendConsole();
        }

        void SendConsole()
        {
            SerialPortSend(textBoxSend.Text);
        }

        void SerialPortSend(string text, RC5code rc5code)
        {
            try
            {
                if (serialPort == null && !serialPort.IsOpen)
                    return;
                text = text.ToLower();

                var bytesText = Encoding.ASCII.GetBytes(text);
                var bytesResult = new byte[bytesText.Length + rc5code.Bytes.Length + 1];

                for (int i = 0; i < bytesText.Length; ++i)
                    bytesResult[i] = bytesText[i];

                for (int i = bytesText.Length; i < bytesResult.Length - 1; ++i)
                    bytesResult[i] = rc5code.Bytes[i - bytesText.Length];

                bytesResult[bytesResult.Length - 1] = Convert.ToByte('\n');

                serialPort.Write(bytesResult, 0, bytesResult.Length);
                richTextBoxConsole.AppendLine(text + rc5code, Color.Orange);
            }
            catch (Exception ex) { MessageBox.Show("Error " + GetType().FullName + ": " + MethodBase.GetCurrentMethod().Name + "()\n" + ex.ToString(), "Error " + Name); }
        }

        void SerialPortSend(string text)
        {
            try
            {
                text = text.ToLower();
                if (serialPort != null && serialPort.IsOpen)
                {
                    serialPort.WriteLine(text);
                    richTextBoxConsole.AppendLine(text, Color.Orange);
                }
            }
            catch (Exception ex) { MessageBox.Show("Error " + GetType().FullName + ": " + MethodBase.GetCurrentMethod().Name + "()\n" + ex.ToString(), "Error " + Name); }
        }

        private void cbPort_DropDown(object sender, EventArgs e)
        {
            SetPortNames();
        }

        void SetPortNames()
        {
            try
            {
                var comboBox = cbPortName;
                comboBox.Items.Clear();
                var portNames = SerialPort.GetPortNames();

                foreach (var portName in portNames)
                    comboBox.Items.Add(portName);
            }
            catch (Exception ex) { MessageBox.Show("Error " + GetType().FullName + ": " + MethodBase.GetCurrentMethod().Name + "()\n" + ex.ToString(), "Error " + Name); }

        }

        enum StateIr { Normal, Error, Receive }

        void SetIconByStateIr(StateIr stateIr)
        {
            try
            {
                switch (stateIr)
                {
                    case StateIr.Normal:
                        notifyIcon1.Icon = ResourceIcons.RemoteIR_tray;
                        break;
                    case StateIr.Error:
                        notifyIcon1.Icon = ResourceIcons.RemoteIR_trayError;
                        break;
                    case StateIr.Receive:
                        notifyIcon1.Icon = ResourceIcons.RemoteIR_trayActive;
                        var timer = new Timer();
                        timer.Tick += Timer_Tick;
                        timer.Interval = 300;
                        timer.Start();
                        break;
                }
            }
            catch (Exception ex) { MessageBox.Show("Error " + GetType().FullName + ": " + MethodBase.GetCurrentMethod().Name + "()\n" + ex.ToString(), "Error " + Name); }
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            notifyIcon1.Icon = ResourceIcons.RemoteIR_tray;
            var timer = ((Timer)sender);
            timer.Stop();
            timer.Tick -= Timer_Tick;
            timer.Dispose();
        }

        private void ChangeStartup()
        {
            try
            {
                var rk = Registry.CurrentUser.OpenSubKey
                       ("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);

                var appName = Application.ProductName;

                if (chbAutoRun.Checked)
                    rk.SetValue(appName, Assembly.GetExecutingAssembly().Location);
                else
                    rk.DeleteValue(appName, false);
            }
            catch (Exception ex) { MessageBox.Show("Error " + GetType().FullName + ": " + MethodBase.GetCurrentMethod().Name + "()\n" + ex.ToString(), "Error " + Name); }
        }
    }

}
