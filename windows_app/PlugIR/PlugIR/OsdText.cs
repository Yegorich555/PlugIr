using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace PlugIR
{
    /// <summary>
    /// Base class which represents a floating native window with transparency and custom painting
    /// </summary>
    public class OsdText : NativeWindow, IDisposable
    {
        #region #  Fields  #
        private bool _disposed = false;
        private byte _alpha = 255;
        private Size _size = new Size(250, 50);
        private Point _location = new Point(50, 50);

        private SolidBrush _brush;
        private StringFormat _stringFormat;
        private Timer _viewClock;
        public Font Font { get; set; }
        public string Text { get; set; }
        public Screen Screen { get; private set; }

        private GraphicsPath _gp;
        #endregion

        #region #  Methods  #

        #region == Painting ==
        /// <summary>
        /// Performs the painting of the window. Overide this method to provide custom painting
        /// </summary>
        /// <param name="e">A <see cref="PaintEventArgs"/> containing the event data.</param>
        protected virtual void PerformPaint(PaintEventArgs e)
        {
            if (Handle == IntPtr.Zero)
                return;
            Graphics g = e.Graphics;
            if (_gp != null)
                _gp.Dispose();
            if (BackColor != null)
                g.FillRectangle(new SolidBrush((Color)BackColor), Bound);

            _gp = new GraphicsPath();
            _gp.AddString(Text, Font.FontFamily, (int)Font.Style, g.DpiY * Font.SizeInPoints / 72, Bound, _stringFormat);
            g.SmoothingMode = SmoothingMode.HighQuality;
            g.FillPath(_brush, _gp);
        }
        #endregion

        #region == Updating ==
        protected internal void Invalidate()
        {
            UpdateLayeredWindow();
        }
        private void UpdateLayeredWindow()
        {
            Bitmap bitmap1 = new Bitmap(Size.Width, Size.Height, PixelFormat.Format32bppArgb);
            using (Graphics graphics1 = Graphics.FromImage(bitmap1))
            {
                Rectangle rectangle1;
                SIZE size1;
                POINT point1;
                POINT point2;
                BLENDFUNCTION blendfunction1;
                rectangle1 = new Rectangle(0, 0, Size.Width, Size.Height);
                PerformPaint(new PaintEventArgs(graphics1, rectangle1));
                IntPtr ptr1 = User32.GetDC(IntPtr.Zero);
                IntPtr ptr2 = Gdi32.CreateCompatibleDC(ptr1);
                IntPtr ptr3 = bitmap1.GetHbitmap(Color.FromArgb(0));
                IntPtr ptr4 = Gdi32.SelectObject(ptr2, ptr3);
                size1.cx = Size.Width;
                size1.cy = Size.Height;
                point1.x = Location.X;
                point1.y = Location.Y;
                point2.x = 0;
                point2.y = 0;
                blendfunction1 = new BLENDFUNCTION
                {
                    BlendOp = 0,
                    BlendFlags = 0,
                    SourceConstantAlpha = _alpha,
                    AlphaFormat = 1
                };
                User32.UpdateLayeredWindow(Handle, ptr1, ref point1, ref size1, ptr2, ref point2, 0, ref blendfunction1, 2); //2=ULW_ALPHA
                Gdi32.SelectObject(ptr2, ptr4);
                User32.ReleaseDC(IntPtr.Zero, ptr1);
                Gdi32.DeleteObject(ptr3);
                Gdi32.DeleteDC(ptr2);
            }
        }
        #endregion

        #region == Show / Hide ==
        /// <summary>
        /// Shows the window. Position determined by Location property in screen coordinates
        /// </summary>
        public void Show()
        {
            if (Text == null)
                return;
            if (Handle == IntPtr.Zero) //if handle don't equal to zero - window was created and just hided
                CreateWindowOnly();
            User32.ShowWindow(Handle, User32.SW_SHOWNOACTIVATE);
        }

        public static OsdText CreateAndShow(string text, Color? textColor = null, Color? backColor = null, int fontSize = 36, Screen screen = null, int showTime = -1)
        {
            var osd = new OsdText();
            osd.Show(text, textColor, backColor, fontSize, screen, showTime);
            return osd;
        }

        public void Show(string text, Color? textColor = null, Color? backColor = null, int fontSize = 36, Screen screen = null, int showTime = -1)
        {
            if (_viewClock != null)
            {
                _viewClock.Stop();
                _viewClock.Dispose();
            }
            _brush = new SolidBrush(textColor ?? Color.ForestGreen);
            BackColor = backColor;
            Font = new Font("Arial", fontSize, FontStyle.Regular, GraphicsUnit.Point, 204);

            Text = text;
            Screen = screen ?? Screen.FromHandle(Handle);

            var rScreen = Screen.Bounds;
            if (_stringFormat == null)
            {
                _stringFormat = new StringFormat
                {
                    Alignment = StringAlignment.Near,
                    LineAlignment = StringAlignment.Near,
                    Trimming = StringTrimming.EllipsisWord
                };
            }

            SizeF textAreaF;
            using (Bitmap bm = new Bitmap(Width, Height))
            using (Graphics fx = Graphics.FromImage(bm))
                textAreaF = fx.MeasureString(text, Font, rScreen.Width, _stringFormat);
            var textArea = Size.Round(textAreaF);
            Location = new Point(rScreen.Location.X + (rScreen.Width - textArea.Width) / 2, rScreen.Location.Y + (rScreen.Height - textArea.Height) / 2);
            Size = new Size((int)Math.Ceiling(textAreaF.Width), (int)Math.Ceiling(textAreaF.Height));

            Show();
            if (showTime > 0)
            {
                _viewClock = new Timer();
                _viewClock.Tick += new EventHandler(ViewTimer);
                _viewClock.Interval = showTime;
                _viewClock.Start();
            }
        }

        protected void ViewTimer(object sender, EventArgs e)
        {
            _viewClock.Stop();
            _viewClock.Dispose();
            Close();
        }

        public void Show(int x, int y)
        {
            _location.X = x;
            _location.Y = y;
            Show();
        }

        /// <summary>
        /// Hides the window and release it's handle.
        /// </summary>
        public virtual void Hide()
        {
            if (Handle == IntPtr.Zero)
                return;
            User32.ShowWindow(Handle, User32.SW_HIDE);
            DestroyHandle();
        }

        /// <summary>
        /// Close the window and destroy it's handle.
        /// </summary>
        public virtual void Close()
        {
            Hide();
            Dispose();
        }

        private void CreateWindowOnly()
        {
            uint ui = User32.WS_POPUP;
            CreateParams params1 = new CreateParams
            {
                Caption = "FloatingNativeWindow",
                X = Location.X,
                Y = Location.Y,
                Height = _size.Height,
                Width = _size.Width,
                Parent = IntPtr.Zero,
                Style = (int)ui,
                ExStyle = User32.WS_EX_TOPMOST | User32.WS_EX_TOOLWINDOW | User32.WS_EX_LAYERED | User32.WS_EX_NOACTIVATE | User32.WS_EX_TRANSPARENT
            };
            CreateHandle(params1);
            UpdateLayeredWindow();
        }
        #endregion

        #region == Other messages ==
        private void PerformWmPaint_WmPrintClient(ref Message m, bool isPaintMessage)
        {
            PAINTSTRUCT paintstruct1;
            RECT rect1;
            Rectangle rectangle1;
            paintstruct1 = new PAINTSTRUCT();
            IntPtr ptr1 = (isPaintMessage ? User32.BeginPaint(m.HWnd, ref paintstruct1) : m.WParam);
            rect1 = new RECT();
            User32.GetWindowRect(Handle, ref rect1);
            rectangle1 = new Rectangle(0, 0, rect1.right - rect1.left, rect1.bottom - rect1.top);
            using (Graphics graphics1 = Graphics.FromHdc(ptr1))
            {
                Bitmap bitmap1 = new Bitmap(rectangle1.Width, rectangle1.Height);
                using (Graphics graphics2 = Graphics.FromImage(bitmap1))
                    PerformPaint(new PaintEventArgs(graphics2, rectangle1));
                graphics1.DrawImageUnscaled(bitmap1, 0, 0);
            }
            if (isPaintMessage)
                User32.EndPaint(m.HWnd, ref paintstruct1);
        }

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == 15) // WM_PAINT
            {
                PerformWmPaint_WmPrintClient(ref m, true);
                return;
            }
            else if (m.Msg == 0x318) // WM_PRINTCLIENT
            {
                PerformWmPaint_WmPrintClient(ref m, false);
                return;
            }
            base.WndProc(ref m);
        }
        #endregion

        #region == Size and Location ==
        protected virtual void SetBoundsCore(int x, int y, int width, int height)
        {
            if (((X != x) || (Y != y)) || ((Width != width) || (Height != height)))
            {
                if (Handle != IntPtr.Zero)
                {
                    int num1 = 20;
                    if ((X == x) && (Y == y))
                    {
                        num1 |= 2;
                    }
                    if ((Width == width) && (Height == height))
                    {
                        num1 |= 1;
                    }
                    User32.SetWindowPos(Handle, IntPtr.Zero, x, y, width, height, (uint)num1);
                }
                else
                {
                    Location = new Point(x, y);
                    Size = new Size(width, height);
                }
            }
        }
        #endregion

        #endregion

        #region #  Properties  #
        /// <summary>
        /// Get or set position of top-left corner of floating native window in screen coordinates
        /// </summary>
        public virtual Point Location
        {
            get { return _location; }
            set
            {
                if (Handle != IntPtr.Zero)
                {
                    SetBoundsCore(value.X, value.Y, _size.Width, _size.Height);
                    RECT rect = new RECT();
                    User32.GetWindowRect(Handle, ref rect);
                    _location = new Point(rect.left, rect.top);
                    UpdateLayeredWindow();
                }
                else
                {
                    _location = value;
                }
            }
        }
        /// <summary>
        /// Get or set size of client area of floating native window
        /// </summary>
        public virtual Size Size
        {
            get { return _size; }
            set
            {
                if (Handle != IntPtr.Zero)
                {
                    SetBoundsCore(_location.X, _location.Y, value.Width, value.Height);
                    RECT rect = new RECT();
                    User32.GetWindowRect(Handle, ref rect);
                    _size = new Size(rect.right - rect.left, rect.bottom - rect.top);
                    UpdateLayeredWindow();
                }
                else
                {
                    _size = value;
                }
            }
        }
        /// <summary>
        /// Gets or sets the height of the floating native window
        /// </summary>
		public int Height
        {
            get { return _size.Height; }
            set
            {
                _size = new Size(_size.Width, value);
            }
        }
        /// <summary>
        /// Gets or sets the width of the floating native window
        /// </summary>
        public int Width
        {
            get { return _size.Width; }
            set
            {
                _size = new Size(value, _size.Height);
            }
        }
        /// <summary>
        /// Get or set x-coordinate of top-left corner of floating native window in screen coordinates
        /// </summary>
		public int X
        {
            get { return _location.X; }
            set
            {
                Location = new Point(value, Location.Y);
            }
        }
        /// <summary>
        /// Get or set y-coordinate of top-left corner of floating native window in screen coordinates
        /// </summary>
        public int Y
        {
            get { return _location.Y; }
            set
            {
                Location = new Point(Location.X, value);
            }
        }
        /// <summary>
        /// Get rectangle represented client area of floating native window in client coordinates(top-left corner always has coord. 0,0)
        /// </summary>
		public Rectangle Bound
        {
            get
            {
                return new Rectangle(new Point(0, 0), _size);
            }
        }
        /// <summary>
        /// Get or set full opacity(255) or full transparency(0) or any intermediate state for floating native window transparency
        /// </summary>
		public byte Alpha
        {
            get { return _alpha; }
            set
            {
                if (_alpha == value) return;
                _alpha = value;
                UpdateLayeredWindow();
            }
        }

        public Color? BackColor { get; set; }
        #endregion

        #region IDisposable Members
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        private void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                DestroyHandle();
                _disposed = true;
            }
        }
        #endregion
    }

    #region #  Win32  #
    internal struct PAINTSTRUCT
    {
        public IntPtr hdc;
        public int fErase;
        public Rectangle rcPaint;
        public int fRestore;
        public int fIncUpdate;
        public int Reserved1;
        public int Reserved2;
        public int Reserved3;
        public int Reserved4;
        public int Reserved5;
        public int Reserved6;
        public int Reserved7;
        public int Reserved8;
    }
    [StructLayout(LayoutKind.Sequential)]
    internal struct POINT
    {
        public int x;
        public int y;
    }
    [StructLayout(LayoutKind.Sequential)]
    internal struct RECT
    {
        public int left;
        public int top;
        public int right;
        public int bottom;
    }
    [StructLayout(LayoutKind.Sequential)]
    internal struct SIZE
    {
        public int cx;
        public int cy;
    }
    [StructLayout(LayoutKind.Sequential)]
    internal struct TRACKMOUSEEVENTS
    {
        public uint cbSize;
        public uint dwFlags;
        public IntPtr hWnd;
        public uint dwHoverTime;
    }
    [StructLayout(LayoutKind.Sequential)]
    internal struct MSG
    {
        public IntPtr hwnd;
        public int message;
        public IntPtr wParam;
        public IntPtr lParam;
        public int time;
        public int pt_x;
        public int pt_y;
    }
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    internal struct BLENDFUNCTION
    {
        public byte BlendOp;
        public byte BlendFlags;
        public byte SourceConstantAlpha;
        public byte AlphaFormat;
    }
    internal class User32
    {
        public const uint WS_POPUP = 0x80000000;
        public const int WS_EX_TOPMOST = 0x8;
        public const int WS_EX_TOOLWINDOW = 0x80;
        public const int WS_EX_LAYERED = 0x80000;
        public const int WS_EX_TRANSPARENT = 0x20;
        public const int WS_EX_NOACTIVATE = 0x08000000;
        public const int SW_SHOWNOACTIVATE = 4;
        public const int SW_HIDE = 0;
        public const uint AW_HOR_POSITIVE = 0x1;
        public const uint AW_HOR_NEGATIVE = 0x2;
        public const uint AW_VER_POSITIVE = 0x4;
        public const uint AW_VER_NEGATIVE = 0x8;
        public const uint AW_CENTER = 0x10;
        public const uint AW_HIDE = 0x10000;
        public const uint AW_ACTIVATE = 0x20000;
        public const uint AW_SLIDE = 0x40000;
        public const uint AW_BLEND = 0x80000;
        // Methods
        private User32()
        {
        }
        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        internal static extern bool AnimateWindow(IntPtr hWnd, uint dwTime, uint dwFlags);
        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        internal static extern IntPtr BeginPaint(IntPtr hWnd, ref PAINTSTRUCT ps);
        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        internal static extern bool ClientToScreen(IntPtr hWnd, ref POINT pt);
        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        internal static extern bool DispatchMessage(ref MSG msg);
        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        internal static extern bool DrawFocusRect(IntPtr hWnd, ref RECT rect);
        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        internal static extern bool EndPaint(IntPtr hWnd, ref PAINTSTRUCT ps);
        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        internal static extern IntPtr GetDC(IntPtr hWnd);
        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        internal static extern IntPtr GetFocus();
        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        internal static extern ushort GetKeyState(int virtKey);
        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        internal static extern bool GetMessage(ref MSG msg, int hWnd, uint wFilterMin, uint wFilterMax);
        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        internal static extern IntPtr GetParent(IntPtr hWnd);
        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern bool GetClientRect(IntPtr hWnd, [In, Out] ref RECT rect);
        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        internal static extern int GetWindowLong(IntPtr hWnd, int nIndex);
        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        internal static extern IntPtr GetWindow(IntPtr hWnd, int cmd);
        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        internal static extern bool GetWindowRect(IntPtr hWnd, ref RECT rect);
        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        internal static extern bool HideCaret(IntPtr hWnd);
        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        internal static extern bool InvalidateRect(IntPtr hWnd, ref RECT rect, bool erase);
        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        internal static extern IntPtr LoadCursor(IntPtr hInstance, uint cursor);
        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern int MapWindowPoints(IntPtr hWndFrom, IntPtr hWndTo, [In, Out] ref RECT rect, int cPoints);
        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        internal static extern bool MoveWindow(IntPtr hWnd, int x, int y, int width, int height, bool repaint);
        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        internal static extern bool PeekMessage(ref MSG msg, int hWnd, uint wFilterMin, uint wFilterMax, uint wFlag);
        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        internal static extern bool PostMessage(IntPtr hWnd, int Msg, uint wParam, uint lParam);
        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        internal static extern bool ReleaseCapture();
        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        internal static extern int ReleaseDC(IntPtr hWnd, IntPtr hDC);
        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        internal static extern bool ScreenToClient(IntPtr hWnd, ref POINT pt);
        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        internal static extern uint SendMessage(IntPtr hWnd, int Msg, uint wParam, uint lParam);
        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        internal static extern IntPtr SetCursor(IntPtr hCursor);
        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        internal static extern IntPtr SetFocus(IntPtr hWnd);
        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        internal static extern int SetWindowLong(IntPtr hWnd, int nIndex, int newLong);
        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        internal static extern int SetWindowPos(IntPtr hWnd, IntPtr hWndAfter, int X, int Y, int Width, int Height, uint flags);
        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        internal static extern bool SetWindowRgn(IntPtr hWnd, IntPtr hRgn, bool redraw);
        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        internal static extern bool ShowCaret(IntPtr hWnd);
        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        internal static extern bool SetCapture(IntPtr hWnd);
        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        internal static extern int ShowWindow(IntPtr hWnd, short cmdShow);
        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        internal static extern bool SystemParametersInfo(uint uiAction, uint uiParam, ref int bRetValue, uint fWinINI);
        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        internal static extern bool TrackMouseEvent(ref TRACKMOUSEEVENTS tme);
        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        internal static extern bool TranslateMessage(ref MSG msg);
        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        internal static extern bool UpdateLayeredWindow(IntPtr hwnd, IntPtr hdcDst, ref POINT pptDst, ref SIZE psize, IntPtr hdcSrc, ref POINT pprSrc, int crKey, ref BLENDFUNCTION pblend, int dwFlags);
        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        internal static extern bool UpdateWindow(IntPtr hwnd);
        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        internal static extern bool WaitMessage();
        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern bool AdjustWindowRectEx(ref RECT lpRect, int dwStyle, bool bMenu, int dwExStyle);
    }

    internal class Gdi32
    {
        // Methods
        private Gdi32()
        {
        }
        [DllImport("gdi32.dll", CharSet = CharSet.Auto)]
        internal static extern int CombineRgn(IntPtr dest, IntPtr src1, IntPtr src2, int flags);
        [DllImport("gdi32.dll", CharSet = CharSet.Auto)]
        internal static extern IntPtr CreateBrushIndirect(ref LOGBRUSH brush);
        [DllImport("gdi32.dll", CharSet = CharSet.Auto)]
        internal static extern IntPtr CreateCompatibleDC(IntPtr hDC);
        [DllImport("gdi32.dll", CharSet = CharSet.Auto)]
        internal static extern IntPtr CreateRectRgnIndirect(ref RECT rect);
        [DllImport("gdi32.dll", CharSet = CharSet.Auto)]
        internal static extern bool DeleteDC(IntPtr hDC);
        [DllImport("gdi32.dll", CharSet = CharSet.Auto)]
        internal static extern IntPtr DeleteObject(IntPtr hObject);
        [DllImport("gdi32.dll", CharSet = CharSet.Auto)]
        internal static extern int GetClipBox(IntPtr hDC, ref RECT rectBox);
        [DllImport("gdi32.dll", CharSet = CharSet.Auto)]
        internal static extern bool PatBlt(IntPtr hDC, int x, int y, int width, int height, uint flags);
        [DllImport("gdi32.dll", CharSet = CharSet.Auto)]
        internal static extern int SelectClipRgn(IntPtr hDC, IntPtr hRgn);
        [DllImport("gdi32.dll", CharSet = CharSet.Auto)]
        internal static extern IntPtr SelectObject(IntPtr hDC, IntPtr hObject);
    }
    [StructLayout(LayoutKind.Sequential)]
    public struct LOGBRUSH
    {
        public uint lbStyle;
        public uint lbColor;
        public uint lbHatch;
    }

    #endregion
}
