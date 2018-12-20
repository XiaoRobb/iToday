using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace iToday
{
    public partial class Form1 : Form
    {
        private DateTime today = DateTime.Now;
        private Wether wether = new Wether("雨", "xxxx");
        private Point mPoint;
        private List<ThingPanel> hotThingPanels;
        private List<ThingPanel> historyThingPanels;
        private bool showInfor = true;
        public Form1()
        {
            InitializeComponent();
            BackColor = Color.YellowGreen;
            TransparencyKey = Color.YellowGreen;
            pictureBox5.Hide();
            pictureBox6.Hide();
            pictureBox7.Hide();
            pictureBox8.Hide();
            flowLayoutPanel1.Hide();
            this.dateLabel.Text = today.Day.ToString();
        }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x00080000;  //  WS_EX_LAYERED 扩展样式
                return cp;
            }
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            mPoint = new Point(e.X, e.Y);
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {

                this.Location = new Point(this.Location.X + e.X - mPoint.X, this.Location.Y + e.Y - mPoint.Y);
            }
        }


        private void pictureBox1_DoubleClick(object sender, EventArgs e)
        {
            this.Dispose();
        }


        private void pictureBox4_MouseClick(object sender, MouseEventArgs e)
        {
            if (showInfor)
            {
                pictureBox5.Show();
                pictureBox6.Show();
                pictureBox7.Show();
                pictureBox8.Show();
                showInfor = false;
                pictureBox4.ImageLocation = "../../Resource//remove.png";
            }
            else
            {
                pictureBox5.Hide();
                pictureBox6.Hide();
                pictureBox7.Hide();
                pictureBox8.Hide();
                flowLayoutPanel1.Hide();
                showInfor = true;
                pictureBox4.ImageLocation = "../../Resource//add.png";
            }

        }

        private void pictureBox6_MouseClick(object sender, MouseEventArgs e)
        {
            flowLayoutPanel1.Controls.Clear();
            if (hotThingPanels == null)
            {
                hotThingPanels = new List<ThingPanel>();
                TodayHotCrawler todayCrawler = new TodayHotCrawler();
                todayCrawler.Crawl();
                foreach (TodayHotPoint todayHotPoint in todayCrawler.list)
                {
                    hotThingPanels.Add(new ThingPanel(todayHotPoint));
                }
            }
            foreach (ThingPanel thingPanel in hotThingPanels)
            {
                flowLayoutPanel1.Controls.Add(thingPanel);
            }
            flowLayoutPanel1.Show();
        }

        private void pictureBox7_MouseClick(object sender, MouseEventArgs e)
        {
            flowLayoutPanel1.Controls.Clear();
            if (historyThingPanels == null)
            {
                historyThingPanels = new List<ThingPanel>();
                TodayHistoryCrawler todayHistoryCrawler = new TodayHistoryCrawler();
                todayHistoryCrawler.Crawl();
                foreach (TodayHistory todayHistory in todayHistoryCrawler.list)
                {
                    historyThingPanels.Add(new ThingPanel(todayHistory));
                }
            }
            foreach (ThingPanel thingPanel in historyThingPanels)
            {
                this.flowLayoutPanel1.Controls.Add(thingPanel);
            }
            flowLayoutPanel1.Show();
        }
    }
}
