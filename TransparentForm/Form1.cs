using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TransparentForm
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        #region 设置不规则窗体
        private GraphicsPath GetWindowRegion(Bitmap bitmap)
        {
            Color TempColor;
            GraphicsPath gp = new GraphicsPath();
            if (bitmap == null) return null;

            for (int nX = 0; nX < bitmap.Width; nX++)
            {
                for (int nY = 0; nY < bitmap.Height; nY++)
                {
                    TempColor = bitmap.GetPixel(nX, nY);
                    //if (TempColor.A != 0)//如果颜色不是全透明
                    if (TempColor.A == 255)//如果颜色带有透明
                    {
                        gp.AddRectangle(new Rectangle(nX, nY, 1, 1));
                    }
                }
            }
            return gp;
        }
        #endregion

        private void Form1_Load(object sender, EventArgs e)
        {
            TopMost = true;//设置为最顶层
            FormBorderStyle = FormBorderStyle.None;//取消窗口边框
            this.Region = new Region(GetWindowRegion(new Bitmap(BackgroundImage)));//设置不规则窗体
            
        }
    }
}
