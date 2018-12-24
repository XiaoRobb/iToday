using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using Microsoft.Win32;

namespace iToday
{
    class TodayThingPanel : FlowLayoutPanel
    {
        private PictureBox pictureBox;
        private Label label = new Label();
        private string url;
        public TodayThingPanel(TodayHotPoint todayHotPoint) : base()
        {
            Init();
            pictureBox.ImageLocation = todayHotPoint.ImageUrl;
            label.Text = todayHotPoint.HotThing;
            url = todayHotPoint.Url;
        }

        public TodayThingPanel(TodayHistory todayHistory) : base()
        {
            Init();
            pictureBox.ImageLocation = todayHistory.ImageUrl;
            label.Text = todayHistory.HistoryThing;
            url = todayHistory.Url;
        }

        private void Init()
        {
            this.Width = 180;
            this.Height = 40;
            pictureBox = new PictureBox();
            pictureBox.Size = new Size(40, 40);
            pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
            label.Width = 104;
            label.Height = 40;
            label.Font = new Font("隶书", 10, FontStyle.Bold);
            label.TextAlign = ContentAlignment.MiddleCenter;
            this.Controls.Add(pictureBox);
            this.Controls.Add(label);
            label.MouseClick += new MouseEventHandler(url_Click);
            pictureBox.MouseClick += new MouseEventHandler(url_Click);
        }

        private void url_Click(object sender, MouseEventArgs e)
        {
            System.Diagnostics.Process.Start(url);
        }
    }
}
