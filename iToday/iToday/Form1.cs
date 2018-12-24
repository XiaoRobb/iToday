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
        private Point mPoint;
        private List<TodayThingPanel> hotThingPanels;
        private List<TodayThingPanel> historyThingPanels;
        private TodayDatePanel todayDatePanel;
        private TodayWetherPanel todayWetherPanel;
        private List<TodayClassPanel> todayClassPanels;
        private bool showInfor = true;
        private bool showChiLun = true;
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

            pictureBox1.Hide();
            pictureBox2.Hide();
            pictureBox4.Hide();
            dateLabel.Hide();


            this.dateLabel.Text = today.Day.ToString();

            TodayWetherCrawler todayWetherCrawler = new TodayWetherCrawler();
            todayWetherCrawler.Crawl();
            todayWetherPanel = new TodayWetherPanel(todayWetherCrawler.wether);
            if(!string.IsNullOrEmpty(todayWetherCrawler.wether.ImageUrl))
                pictureBox2.Image = Image.FromFile(todayWetherCrawler.wether.ImageUrl);
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
                hotThingPanels = new List<TodayThingPanel>();
                TodayHotCrawler todayCrawler = new TodayHotCrawler();
                todayCrawler.Crawl();
                foreach (TodayHotPoint todayHotPoint in todayCrawler.list)
                {
                    hotThingPanels.Add(new TodayThingPanel(todayHotPoint));
                }
            }
            foreach (TodayThingPanel thingPanel in hotThingPanels)
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
                historyThingPanels = new List<TodayThingPanel>();
                TodayHistoryCrawler todayHistoryCrawler = new TodayHistoryCrawler();
                todayHistoryCrawler.Crawl();
                foreach (TodayHistory todayHistory in todayHistoryCrawler.list)
                {
                    historyThingPanels.Add(new TodayThingPanel(todayHistory));
                }
            }
            foreach (TodayThingPanel thingPanel in historyThingPanels)
            {
                this.flowLayoutPanel1.Controls.Add(thingPanel);
            }
            flowLayoutPanel1.Show();
        }

        private void pictureBox8_MouseClick(object sender, MouseEventArgs e)
        {
            flowLayoutPanel1.Controls.Clear();
            if (todayDatePanel == null)
            {
                
                TodayDateCrawler todayDateCrawler = new TodayDateCrawler();
                todayDateCrawler.Crawl();
                todayDatePanel = new TodayDatePanel(todayDateCrawler.todayDate);
                
            }
            flowLayoutPanel1.Controls.Add(todayDatePanel);
            flowLayoutPanel1.Show();
        }

        private void pictureBox3_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Location.Y > 218 && showInfor)
            {
                pictureBox5.Show();
                pictureBox6.Show();
                pictureBox7.Show();
                pictureBox8.Show();
                showInfor = false;
            }
            else if (e.Location.Y > 218 && !showInfor)
            {
                pictureBox5.Hide();
                pictureBox6.Hide();
                pictureBox7.Hide();
                pictureBox8.Hide();
                flowLayoutPanel1.Hide();
                showInfor = true;
            }
        }

        private void pictureBox3_DoubleClick(object sender, EventArgs e)
        {
            if (showChiLun)
            {
                pictureBox1.Show();
                pictureBox2.Show();
                pictureBox2.BringToFront();
                pictureBox4.Show();
                pictureBox4.BringToFront();
                dateLabel.Show();
                dateLabel.BringToFront();
                showChiLun = false;
            }
            else
            {
                pictureBox5.Hide();
                pictureBox6.Hide();
                pictureBox7.Hide();
                pictureBox8.Hide();
                flowLayoutPanel1.Hide();
                pictureBox1.Hide();
                pictureBox2.Hide();
                pictureBox4.Hide();
                dateLabel.Hide();
                showChiLun = true;
            }
        }

        private void pictureBox3_MouseDown(object sender, MouseEventArgs e)
        {
            mPoint = new Point(e.X, e.Y);
        }

        private void pictureBox3_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Location = new Point(this.Location.X + e.X - mPoint.X, this.Location.Y + e.Y - mPoint.Y);
            }
        }

        private void pictureBox2_MouseClick(object sender, MouseEventArgs e)
        {
            flowLayoutPanel1.Controls.Clear();           
            flowLayoutPanel1.Controls.Add(todayWetherPanel);
            flowLayoutPanel1.Show();
        }

        private void pictureBox5_MouseClick(object sender, MouseEventArgs e)
        {
            flowLayoutPanel1.Controls.Clear();
            if(todayClassPanels == null)
            {
                todayClassPanels = new List<TodayClassPanel>();
                TodayClassCrawler todayClassCrawler = new TodayClassCrawler();
                Login login = new Login(todayClassCrawler);
                login.ShowDialog();
                foreach (TodayClass todayClass in todayClassCrawler.list)
                {
                    todayClassPanels.Add(new TodayClassPanel(todayClass));
                }
            }
            foreach (TodayClassPanel todayClassPanel in todayClassPanels)
            {
                flowLayoutPanel1.Controls.Add(todayClassPanel);
            }
            flowLayoutPanel1.Show();

        }
    }
}
