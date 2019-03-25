using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsInput;
using YLib.Extensions;

namespace PlugIR
{
    public enum TypeCommands { SendKeys, AppRun, AppClose, ExitWindows };

    public class IrCommand
    {
        Stopwatch stopWatch = new Stopwatch();

        public static IrCommand Parse(string[] cmdStroke)
        {
            var cmd = new IrCommand(
                 Convert.ToBoolean(cmdStroke[0]),
                 cmdStroke[1],
                 RC5code.FromStringHex(cmdStroke[2]),
                 (TypeCommands)Enum.Parse(typeof(TypeCommands), cmdStroke[3], true),
                 cmdStroke[4],
                 Convert.ToInt32(cmdStroke[5])
                 );
            return cmd;
        }

        public IrCommand(bool enable, string name, RC5code code, TypeCommands typeCommand, string command, int timeout)
        {
            Enable = enable;
            _name = name;
            _irCode = code;
            Timeout = timeout;
            TypeCommand = typeCommand;
            Command = command;

            SetId();
        }

        public bool Enable { get; set; }

        string _name;
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                SetId();
            }
        }

        public int Id { get; private set; }

        RC5code _irCode;
        public RC5code IrCode
        {
            get { return _irCode; }
            set
            {
                _irCode = value;
                SetId();
            }
        }
        public TypeCommands TypeCommand { get; set; }
        string command;
        public string Command
        {
            get
            {
                return command;
            }
            set
            {
                command = value;
                switch (TypeCommand)
                {
                    case TypeCommands.SendKeys:
                        if (!command.IsNull())
                            Key = Extension.ParseKey(command);
                        break;
                    case TypeCommands.AppRun:
                        break;
                    case TypeCommands.AppClose:
                        break;
                    case TypeCommands.ExitWindows:
                        break;
                }
            }
        }
        public int Timeout { get; set; }
        public Keys Key { get; set; }

        public void RunCommand()
        {
            try
            {
                if (!Enable)
                    return;
                if (stopWatch.IsRunning && stopWatch.ElapsedMilliseconds < Timeout)
                    return;
                stopWatch.Restart();
                switch (TypeCommand)
                {
                    case TypeCommands.SendKeys:
                        Extension.SendKey(Key);
                        break;
                    case TypeCommands.AppRun:
                        Process.Start(Command);
                        break;
                    case TypeCommands.AppClose:
                        CloseProccess(Command);
                        break;
                    case TypeCommands.ExitWindows:
                        var psi = new ProcessStartInfo("shutdown", "/s /t 0");
                        psi.CreateNoWindow = true;
                        psi.UseShellExecute = false;
                        Process.Start(psi);
                        break;
                }
            }
            catch (Exception ex) { MessageBox.Show("Error " + GetType().FullName + ": " + MethodBase.GetCurrentMethod().Name + "()\n" + ex.ToString(), "Error " + Name); }
        }

        public override string ToString()
        {
            return Enable + ">" + Name + ">" + IrCode + ">" + TypeCommand + ">" + Command + ">" + Timeout;
        }

        void SetId()
        {
            Id = Name.GetHashCode() * 1000 + IrCode.GetHashCode();
        }

        [DllImport("user32.dll")]
        static extern IntPtr FindWindow(string sClassName, string sAppName);
        [DllImport("user32.dll")]
        static extern int SendMessage(IntPtr hWnd, uint Msg, int wParam, int lParam);
        [DllImport("user32.dll")]
        static extern bool SetForegroundWindow(IntPtr hWnd);

        /// <summary>
        /// Корректное завершение любого приложения
        /// </summary>
        /// <param name="_NameProccess">Имя процесса</param>
        /// <param name="_NameEnd_MsgBox">Титульное название MessageBox'a с подтверждением завершения</param>
        public static bool CloseProccess(string _NameProccess, string _NameEnd_MsgBox = null, int TimeWait = 50)
        {
            bool FindPoccess = false;

            try
            {
                //Закрываем приложение
                foreach (Process process in Process.GetProcessesByName(_NameProccess))
                {
                    FindPoccess = true;
                    //Закрываем все окна
                    process.CloseMainWindow();
                    process.Close();

                    if (!_NameEnd_MsgBox.IsNull())
                    {
                        var w = FindWindow(null, _NameEnd_MsgBox);
                        if (w != IntPtr.Zero)
                        {
                            SetForegroundWindow(w);
                            SendKeys.Send("{ENTER}");
                        }
                    }
                }

                if (TimeWait > 0)
                    Thread.Sleep(TimeWait);

                //Убиваем процесс, если приложение не закрылось
                foreach (Process process in Process.GetProcessesByName(_NameProccess))
                {
                    try { process.Kill(); }
                    catch { }
                }
            }
            catch { }

            return FindPoccess;
        }
    }

}
