using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace My_Computer_Tools_Ⅱ
{
    public partial class ShowBox : Form
    {
        #region 窗体穿透函数（API声明）

        private const uint WS_EX_LAYERED = 0x80000;
        private const int WS_EX_TRANSPARENT = 0x20;

        //private const int GWL_STYLE = (-16);
        private const int GWL_EXSTYLE = (-20);

        private const int LWA_ALPHA = 0;

        [DllImport("user32", EntryPoint = "SetWindowLong")]
        private static extern uint SetWindowLong(
                IntPtr hwnd,
                int nIndex,
                uint dwNewLong
         );

        [DllImport("user32", EntryPoint = "GetWindowLong")]
        private static extern uint GetWindowLong(
                 IntPtr hwnd,
                 int nIndex
        );

        [DllImport("user32", EntryPoint = "SetLayeredWindowAttributes")]
        private static extern int SetLayeredWindowAttributes(
                 IntPtr hwnd,
                 int crKey,
                 int bAlpha,
                 int dwFlags
         );

        /// <summary>
        /// 设置窗体具有鼠标穿透效果
        /// </summary>
        public void SetPenetrate()
        {
            this.TopMost = true;
            GetWindowLong(this.Handle, GWL_EXSTYLE);
            SetWindowLong(this.Handle, GWL_EXSTYLE, WS_EX_TRANSPARENT | WS_EX_LAYERED);
            SetLayeredWindowAttributes(this.Handle, 0, 100, LWA_ALPHA);
        }

        #endregion 窗体穿透函数（API声明）

        #region 窗体动画函数（API声明）

        [DllImport("user32")]
        private static extern bool AnimateWindow(IntPtr hwnd, int dwTime, int dwFlags);

        //private const int AW_HOR_POSITIVE = 0x0001;//自左向右显示窗口，该标志可以在滚动动画和滑动动画中使用。使用AW_CENTER标志时忽略该标志
        //private const int AW_HOR_NEGATIVE = 0x0002;//自右向左显示窗口，该标志可以在滚动动画和滑动动画中使用。使用AW_CENTER标志时忽略该标志
        //private const int AW_VER_POSITIVE = 0x0004;//自顶向下显示窗口，该标志可以在滚动动画和滑动动画中使用。使用AW_CENTER标志时忽略该标志
        //private const int AW_VER_NEGATIVE = 0x0008;//自下向上显示窗口，该标志可以在滚动动画和滑动动画中使用。使用AW_CENTER标志时忽略该标志该标志
        //private const int AW_CENTER = 0x0010;若使用了AW_HIDE标志，则使窗口向内重叠；否则向外扩展
        private const int AW_HIDE = 0x10000;//隐藏窗口

        private const int AW_ACTIVE = 0x20000;//激活窗口，在使用了AW_HIDE标志后不要使用这个标志

        //private const int AW_SLIDE = 0x40000;使用滑动类型动画效果，默认为滚动动画类型，当使用AW_CENTER标志时，这个标志就被忽略
        private const int AW_BLEND = 0x80000;//使用淡入淡出效果

        #endregion 窗体动画函数（API声明）

        public ShowBox(string Tip, string Txt, Form_Main form_Main, int S = 3)
        {
            InitializeComponent();
            AnimateWindow(this.Handle, 300, AW_HIDE | AW_BLEND);
            SetPenetrate();

            lab_Tip.Text = Tip;
            lab_Txt.Text = Txt;
            if (S != 3)
            {
                this.S = S;
            }
            _Main = form_Main;
        }

        private Form_Main _Main;
        private int S = 3;//显示秒
        public bool ShowNow = false;
        private Fun_delegate_void fun_ = null;

        private void ShowBox_Load(object sender, EventArgs e)
        {
            ChangThisSize();
            ToolTip.SetToolTip(this.lab_About, "点击跳过·");
        }

        private Thread thread;

        public void Show(string Tip, string Txt, Form_Main form_Main, int S, bool iserror, object fun = null)
        {
            //是否为错误提示
            if (iserror)
                this.BackColor = Color.Crimson;
            else
                this.BackColor = Color.DodgerBlue;

            //是否执行函数委托
            if (fun != null)
            {
                fun_ = (Fun_delegate_void)fun;
                but_Run.Visible = true;
            }
            else
            {
                but_Run.Visible = false;
                fun_ = null;
            }

            if (ShowNow)
            {
                lab_Tip.Text = Tip;
                lab_Txt.Text = Txt;
                this.S = S;
                ChangThisSize();
                return;
            }

            lab_Tip.Text = Tip;
            lab_Txt.Text = Txt;
            this.S = S;
            _Main = form_Main;
            thread = new Thread(ShowTimeHide)
            {
                IsBackground = true
            };
            thread.Start();
            ChangThisSize();
            AnimateWindow(this.Handle, 300, AW_ACTIVE | AW_BLEND);
            this.Show();
            ShowNow = true;
        }

        /// <summary>
        /// 动态更改显示大小
        /// </summary>
        private void ChangThisSize()
        {
            int h = 0;
            if (this.lab_Tip.Size.Height > this.lab_Txt.Size.Height)
                h = this.lab_Tip.Size.Height;
            else
                h = this.lab_Txt.Size.Height;

            if (Program.backWindows_State)//主窗口在后台
            {
                this.Size = new Size(this.Width, h + 30);
                this.Location = new Point(Screen.PrimaryScreen.WorkingArea.Size.Width - this.Width - 5, Screen.PrimaryScreen.WorkingArea.Size.Height - this.Height - 10);
            }
            else
            {
                this.Size = new Size(this.Width, h + 30);
                this.Location = new Point(_Main.Location.X + (this.Width / 2), _Main.Location.Y + 1);
            }
        }

        private void ShowTimeHide()
        {
            try//这里突然结束有几率报错，直接抛出
            {
                while (this.S != 0)
                {
                    if (this.IsHandleCreated)
                        this.Invoke(new MethodInvoker(delegate ()
                        {
                            this.lab_About.Text = "Computer Tools " + S;
                        }));

                    Thread.Sleep(1000);
                    this.S--;
                }
                if (this.IsHandleCreated)
                    this.Invoke(new MethodInvoker(delegate ()
                    {
                        AnimateWindow(this.Handle, 300, AW_HIDE | AW_BLEND);
                        this.Hide();
                        ShowNow = false;
                    }));
            }
            catch (Exception)
            {
            }
        }

        private void Lab_About_Click(object sender, EventArgs e)
        {
            AnimateWindow(this.Handle, 300, AW_HIDE | AW_BLEND);
            this.Hide();
            ShowNow = false;
            thread = null;
        }

        private void but_Run_Click(object sender, EventArgs e)
        {
            if (fun_ != null)
            {
                fun_();
            }
        }
    }
}