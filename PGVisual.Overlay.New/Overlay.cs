// Decompiled with JetBrains decompiler
// Type: PGVisual.Overlay.Overlay
// Assembly: PGVisual.Overlay, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 36E310DD-04E8-450E-8620-AD5F611898A4
// Assembly location: C:\Users\TylerW10\Desktop\xpg_back\16.01.18\14_14 (fake)\PGVisual.Overlay.exe

using SharpDX;
using SharpDX.Direct2D1;
using SharpDX.DirectWrite;
using SharpDX.Mathematics.Interop;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using SharpDX.DXGI;
using AlphaMode = SharpDX.Direct2D1.AlphaMode;
using FontStyle = SharpDX.DirectWrite.FontStyle;
using System.Runtime.InteropServices;
using System.Threading;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using l00l;

namespace PGVisual.Overlay
{
    public class Overlay : Form
    {
        private IntPtr _parentWindow = IntPtr.Zero;
        private WindowRenderTarget _device;
        private HwndRenderTargetProperties _renderProperties;
        private SolidColorBrush _solidColorBrush;
        private SharpDX.Direct2D1.Factory _factory;
        private SolidColorBrush _brush;
        private TextFormat _font;
        private TextFormat _fontSmall;
        private SharpDX.DirectWrite.Factory _fontFactory;
        private const string _fontFamily = "Verdana";
        private const float _fontSize = 12f;
        private const float _fontSizeSmall = 10f;
        private Thread _threadDx;
        private PGVisual.Overlay.Overlay.RECT _oldParentRect;
        private PGVisual.Overlay.Overlay.RECT _parentRect;
        private bool _running;
        private const int WS_EX_NOACTIVATE = 134217728;
        private const int WS_EX_TOPMOST = 8;
        private const short SWP_NOMOVE = 2;
        private const short SWP_NOSIZE = 1;
        private const short SWP_NOZORDER = 4;
        private const int SWP_SHOWWINDOW = 64;
        private const int WM_ACTIVATE = 6;
        private const int WA_INACTIVE = 0;
        private const int WM_MOUSEACTIVATE = 33;
        private const int MA_NOACTIVATEANDEAT = 4;
        private IContainer components;

        [DllImport("user32.dll")]
        public static extern IntPtr SetParent(IntPtr hWndChild, IntPtr hWndNewParent);

        public Overlay(IntPtr parentWindow)
        {
            this._parentWindow = parentWindow;
            this.InitializeComponent();
        }

        private void LoadOverlay(object sender, EventArgs e)
        {
            this.DoubleBuffered = true;
            base.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.SupportsTransparentBackColor | ControlStyles.ResizeRedraw | ControlStyles.Opaque | ControlStyles.UserPaint, true);
            this._factory = new SharpDX.Direct2D1.Factory();
            this._fontFactory = new SharpDX.DirectWrite.Factory();
            HwndRenderTargetProperties properties = new HwndRenderTargetProperties
            {
                Hwnd = base.Handle,
                PixelSize = new Size2(base.Size.Width, base.Size.Height),
                PresentOptions = PresentOptions.None
            };
            this._renderProperties = properties;
            this._device = new WindowRenderTarget(this._factory, new RenderTargetProperties(new PixelFormat(Format.B8G8R8A8_UNorm, AlphaMode.Premultiplied)), this._renderProperties);
            this._brush = new SolidColorBrush(this._device, new RawColor4((float)Color.Red.R, (float)Color.Red.G, (float)Color.Red.B, (float)Color.Red.A));
            this._font = new TextFormat(this._fontFactory, "Verdana", FontWeight.Bold, FontStyle.Normal, 12f);
            this._fontSmall = new TextFormat(this._fontFactory, "Verdana", FontWeight.Bold, FontStyle.Normal, 10f);
            Console.WriteLine("Starting Maler Thread");
            Thread thread1 = new Thread(new ParameterizedThreadStart(this.DirectXThread))
            {
                Priority = ThreadPriority.Highest,
                IsBackground = true
            };
            this._threadDx = thread1;
            this._running = true;
            this._threadDx.Start();
        }



        private void ClosedOverlay(object sender, FormClosedEventArgs e)
        {
            this._running = false;
        }

        private void UpdateDimension()
        {
            this._parentRect = new RECT();
            GetWindowRect(this._parentWindow, out this._parentRect);
            if (((this._parentRect.Left != this._oldParentRect.Left) || (this._parentRect.Top != this._oldParentRect.Top)) || ((this._parentRect.Right != this._oldParentRect.Right) || (this._parentRect.Bottom != this._oldParentRect.Bottom)))
            {
                base.Invoke(new EventHandler(delegate {
                    SetWindowPos(base.Handle, 0, this._parentRect.Left, this._parentRect.Top, this._parentRect.Right - this._parentRect.Left, this._parentRect.Bottom - this._parentRect.Top, 0);
                    Program.Pubg.SetDimension(this._parentRect.Right - this._parentRect.Left, this._parentRect.Bottom - this._parentRect.Top);
                }));
            }
            this._oldParentRect = this._parentRect;
        }

        public void DrawString(string text, float x, float y, Color color)
        {
            this._brush.Color = new RawColor4((float)color.R, (float)color.G, (float)color.B, (float)color.A);
            this._device.DrawText(text, this._fontSmall, new RawRectangleF(x, y, float.MaxValue, float.MaxValue), (SharpDX.Direct2D1.Brush)this._brush, DrawTextOptions.NoSnap, MeasuringMode.Natural);
        }

        public void FillRect(float x, float y, float w, float h, Color color)
        {
            this._brush.Color = new RawColor4((float)color.R, (float)color.G, (float)color.B, (float)color.A);
            this._device.FillRectangle(new RawRectangleF(x, y, x + w, y + h), (SharpDX.Direct2D1.Brush)this._brush);
        }

        public void DrawRect(float x, float y, float w, float h, Color color)
        {
            this._brush.Color = new RawColor4((float)color.R, (float)color.G, (float)color.B, (float)color.A);
            this._device.DrawRectangle(new RawRectangleF(x, y, x + w, y + h), (SharpDX.Direct2D1.Brush)this._brush);
        }

        public Color GetHPColor(float hp)
        {
            if ((double)hp > 60.0)
                return Color.Lime;
            if ((double)hp > 40.0)
                return Color.Orange;
            return Color.Red;
        }

        private void DirectXThread(object sender)
        {
            while (this._running)
            {
                this._device.BeginDraw();
                WindowRenderTarget device = this._device;
                Color transparent = Color.Transparent;
                double r = (double)transparent.R;
                transparent = Color.Transparent;
                double g = (double)transparent.G;
                transparent = Color.Transparent;
                double b = (double)transparent.B;
                transparent = Color.Transparent;
                double a = (double)transparent.A;
                RawColor4? clearColor = new RawColor4?(new RawColor4((float)r, (float)g, (float)b, (float)a));
                device.Clear(clearColor);
                if (PGVisual.Overlay.Overlay.GetForegroundWindow() == this._parentWindow)
                {
                    this.UpdateDimension();
                    this._device.TextAntialiasMode = SharpDX.Direct2D1.TextAntialiasMode.Aliased;
                    this.DrawString("WillZweiEis", 60f, 60f, Color.Yellow);

                    double range = 500.0;


                    if (Program.Pubg.farDistance == true)
                    {
                        range = 1000.0;
                        this.DrawString("FarDistance ON [F7]", 60f, 75f, Color.Green);
                    }

                    if (Program.Pubg.farDistance == false)
                    {
                        range = 500.0;
                        this.DrawString("FarDistance OFF [F7]", 60f, 75f, Color.Red);
                    }
                    if (Program.Pubg.LocalActor != null)
                    {
                        foreach (Actor player in Program.Pubg.Players)
                        {
                            if ((long)player.ptr != (long)Program.Pubg.LocalActor.ptr)
                            {
                                float num1 = Program.Pubg.LocalActor.position3D.Distance(player.position3D) / 100f;
                                float health = player.health;


                                if ((double)num1 < range && (double)health > 0.0)
                                {
                                    float num2 = player.positionHead2D.Y - player.position2D.Y;
                                    if ((double)num2 < 0.0)
                                        num2 *= -1f;
                                    float w = num2;
                                    this.DrawString(string.Format("{0}m ({1}%)", (object)num1.ToString("0.00"), (object)health.ToString("0")), player.position2D.X - w, player.position2D.Y + num2 * 1.2f, Color.Red);
                                    this.DrawRect(player.positionHead2D.X - w / 2f, player.positionHead2D.Y - num2 * 2f, w, num2 * 2f, Color.Red);
                                    this.DrawRect(player.position2D.X - w, (float)((double)player.position2D.Y + (double)num2 * 1.20000004768372 + 13.0), 100f, 7f, Color.Black);
                                    this.FillRect(player.position2D.X - w, (float)((double)player.position2D.Y + (double)num2 * 1.20000004768372 + 13.0), 100f, 7f, Color.DarkGray);
                                    this.FillRect(player.position2D.X - w, (float)((double)player.position2D.Y + (double)num2 * 1.20000004768372 + 13.0), health, 7f, this.GetHPColor(health));
                                }
                            }
                        }
                        if (Convert.ToBoolean(Program.Pubg.getKeyF8() & 0x8000))
                        {
                            this.DrawString("ICH LIEBE DICH BABE<3", 100f, 100f, Color.Yellow);
                        }

                    }
                }
                this._device.EndDraw();
            }
        }

        protected override bool ShowWithoutActivation
        {
            get
            {
                return true;
            }
        }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams createParams = base.CreateParams;
                int num1 = createParams.ExStyle | 8;
                createParams.ExStyle = num1;
                int num2 = createParams.ExStyle | 134217728;
                createParams.ExStyle = num2;
                return createParams;
            }
        }

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool GetWindowRect(IntPtr hWnd, out PGVisual.Overlay.Overlay.RECT lpRect);

        [DllImport("user32.dll")]
        private static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll")]
        private static extern IntPtr SetActiveWindow(IntPtr handle);

        [DllImport("user32.dll")]
        public static extern IntPtr SetWindowPos(IntPtr hWnd, int hWndInsertAfter, int x, int Y, int cx, int cy, int wFlags);




        protected override void WndProc(ref Message m)
        {
            if (m.Msg == 33)
                m.Result = (IntPtr)4;
            else if (m.Msg == 6)
            {
                if (((int)m.WParam & (int)ushort.MaxValue) == 0)
                    return;
                if (m.LParam != IntPtr.Zero)
                    PGVisual.Overlay.Overlay.SetActiveWindow(m.LParam);
                else
                    PGVisual.Overlay.Overlay.SetActiveWindow(IntPtr.Zero);
            }
            else
                base.WndProc(ref m);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && this.components != null)
                this.components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            this.AutoScaleMode = AutoScaleMode.None;
            this.BackColor = Color.Black;
            this.ClientSize = new Size(284, 261);
            this.ControlBox = false;
            this.DoubleBuffered = true;
            this.FormBorderStyle = FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = Guid.NewGuid().ToString().Replace("-", "");
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = Guid.NewGuid().ToString().Replace("-", "");
            this.TopMost = true;
            this.TransparencyKey = Color.Black;
            this.WindowState = FormWindowState.Maximized;
            this.FormClosed += new FormClosedEventHandler(this.ClosedOverlay);
            this.Load += new EventHandler(this.LoadOverlay);
            this.ResumeLayout(false);
        }

        public struct RECT
        {
            public int Left;
            public int Top;
            public int Right;
            public int Bottom;
        }
    }
}