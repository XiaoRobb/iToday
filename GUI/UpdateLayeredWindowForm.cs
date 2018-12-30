using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TransparentForm
{
    public partial class UpdateLayeredWindowForm : Form
    {
        //Form1 form1 = new Form1();
        Form2 f = new Form2();
        bool haveHandle = false;
        public UpdateLayeredWindowForm()
        {
            InitializeComponent();
        }
        private void UpdateLayeredWindowForm_Load(object sender, EventArgs e)
        {
            FormBorderStyle = FormBorderStyle.None;//取消窗口边框
            SetBits(new Bitmap(BackgroundImage));//设置不规则窗体
            FormMovableEvent();//设置拖动窗体移动
        }
        #region 防止窗体闪屏
        private void InitializeStyles()
        {
            SetStyle(
                ControlStyles.UserPaint |
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.ResizeRedraw |
                ControlStyles.SupportsTransparentBackColor, true);
            SetStyle(ControlStyles.Selectable, false);
            UpdateStyles();
        } 
        #endregion
        #region 句柄创建事件
        protected override void OnHandleCreated(EventArgs e)
        {
            InitializeStyles();//设置窗口样式、双缓冲等
            base.OnHandleCreated(e);
            haveHandle = true;
        } 
        #endregion
        #region 设置窗体样式
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cParms = base.CreateParams;
                cParms.ExStyle |= 0x00080000; // WS_EX_LAYERED
                return cParms;
            }
        } 
        #endregion
        #region 设置不规则窗体
        public void SetBits(Bitmap bitmap)
        {
            if (!haveHandle) return;

            if (!Bitmap.IsCanonicalPixelFormat(bitmap.PixelFormat) || !Bitmap.IsAlphaPixelFormat(bitmap.PixelFormat))
                throw new ApplicationException("The picture must be 32bit picture with alpha channel.");

            IntPtr oldBits = IntPtr.Zero;
            IntPtr screenDC = Win32.GetDC(IntPtr.Zero);
            IntPtr hBitmap = IntPtr.Zero;
            IntPtr memDc = Win32.CreateCompatibleDC(screenDC);

            try
            {
                Win32.Point topLoc = new Win32.Point(Left, Top);
                Win32.Size bitMapSize = new Win32.Size(bitmap.Width, bitmap.Height);
                Win32.BLENDFUNCTION blendFunc = new Win32.BLENDFUNCTION();
                Win32.Point srcLoc = new Win32.Point(0, 0);

                hBitmap = bitmap.GetHbitmap(Color.FromArgb(0));
                oldBits = Win32.SelectObject(memDc, hBitmap);

                blendFunc.BlendOp = Win32.AC_SRC_OVER;
                blendFunc.SourceConstantAlpha = 255;//这里设置窗体绘制的透明度
                blendFunc.AlphaFormat = Win32.AC_SRC_ALPHA;
                blendFunc.BlendFlags = 0;

                Win32.UpdateLayeredWindow(Handle, screenDC, ref topLoc, ref bitMapSize, memDc, ref srcLoc, 0, ref blendFunc, Win32.ULW_ALPHA);
            }
            finally
            {
                if (hBitmap != IntPtr.Zero)
                {
                    Win32.SelectObject(memDc, oldBits);
                    Win32.DeleteObject(hBitmap);
                }
                Win32.ReleaseDC(IntPtr.Zero, screenDC);
                Win32.DeleteDC(memDc);
            }
        } 
        #endregion
        #region 无标题栏的窗口移动
        private Point mouseOffset; //记录鼠标指针的坐标
        private bool isMouseDown = false; //记录鼠标按键是否按下


        private void FormMovableEvent()
        {
            //窗体移动
            this.MouseDown += new MouseEventHandler(Frm_MouseDown);
            this.MouseMove += new MouseEventHandler(Frm_MouseMove);
            this.MouseUp += new MouseEventHandler(Frm_MouseUp);
            this.DoubleClick += new EventHandler(Frm_DoubleClick);
        }

       
        void Frm_DoubleClick(object sender, EventArgs e)
        {
            //双击开关
            if (f.Visible == false)
            {
                f.Show();
                f.SetDesktopLocation(this.Location.X + 150, this.Location.Y - 50);
            }
            else
            {
                f.Hide();
            }
        }

        //按下
        private void Frm_MouseDown(object sender, MouseEventArgs e)
        {
            int xOffset;
            int yOffset;
            //点击窗体时，记录鼠标位置，启动移动
            if (e.Button == MouseButtons.Left)
            {
                xOffset = -e.X;
                yOffset = -e.Y;
                mouseOffset = new Point(xOffset, yOffset);
                isMouseDown = true;
            }
            else if(e.Button == MouseButtons.Right)
            {
                this.Dispose();
            }
        }

      
        
        //移动
        private void Frm_MouseMove(object sender, MouseEventArgs e)
        {
            if (isMouseDown)
            {
                //移动的位置计算
                Point mousePos = Control.MousePosition;
                mousePos.Offset(mouseOffset.X, mouseOffset.Y);
                Location = mousePos;
                f.SetDesktopLocation(this.Location.X + 150, this.Location.Y - 50);
            }
           
        }


        //释放
        private void Frm_MouseUp(object sender, MouseEventArgs e)
        {

            if (e.Button == MouseButtons.Left)
            {
                //松开鼠标时，停止移动
                isMouseDown = false;
                //Top高度小于0的时候，等于0
                if (this.Top < 0)
                {
                    this.Top = 0;
                }
            }
        }
        #endregion
    }
}
