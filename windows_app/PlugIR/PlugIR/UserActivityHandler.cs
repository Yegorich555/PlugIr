using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using YLib.Expansion;

namespace PlugIR
{
    class UserActivityHandler : IDisposable
    {
        private const int WH_MOUSE_LL = 14;
        public event EventHandler GotActivity;
        public UserActivityHandler()
        {
            KeyBoardHooker.KeyDown += KeyBoardHooker_KeyDown;
            KeyBoardHooker.Start();

            _proc = HookCallback;
            _hookID = SetHook(_proc);
        }

        private void KeyBoardHooker_KeyDown(object sender, KeyEventArgs e)
        {
            GotActivity?.Invoke(this, e);
        }

        private LowLevelMouseProc _proc;
        private static IntPtr _hookID;

        static IntPtr SetHook(LowLevelMouseProc proc)
        {
            using (Process curProcess = Process.GetCurrentProcess())
            using (ProcessModule curModule = curProcess.MainModule)
                return SetWindowsHookEx(WH_MOUSE_LL, proc, GetModuleHandle(curModule.ModuleName), 0);
        }

        delegate IntPtr LowLevelMouseProc(int nCode, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        static extern IntPtr SetWindowsHookEx(int idHook, LowLevelMouseProc lpfn, IntPtr hMod, uint dwThreadId);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool UnhookWindowsHookEx(IntPtr hhk);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, IntPtr lParam);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        static extern IntPtr GetModuleHandle(string lpModuleName);

        IntPtr HookCallback(int nCode, IntPtr wParam, IntPtr lParam)
        {
            try
            {
                if (nCode >= 0)
                {
                    int wm = wParam.ToInt32();
                    if (wm == WM_Const.LBUTTONDOWN || wm == WM_Const.RBUTTONDOWN)
                        GotActivity?.Invoke(this, null);
                }
            }
            catch { }
            return CallNextHookEx(_hookID, nCode, wParam, lParam);
        }
        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    KeyBoardHooker.KeyDown -= KeyBoardHooker_KeyDown;
                    KeyBoardHooker.Stop();
                    UnhookWindowsHookEx(_hookID);
                }

                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }
        #endregion
    }

}
