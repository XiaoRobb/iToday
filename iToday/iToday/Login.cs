using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace iToday
{
    public partial class Login : Form
    {
        private TodayClassCrawler todayClassCrawler;
        private Point mPoint;
        public Login(TodayClassCrawler todayClassCrawler)
        {
            InitializeComponent();
            this.todayClassCrawler = todayClassCrawler;
            GetPic();
            this.BackColor = Color.Gray;
            TransparencyKey = Color.Gray;
            if(DateTime.Now.Hour >=19|| DateTime.Now.Hour<6)
            {
                this.BackgroundImage = Image.FromFile("../../Resource/login_night.png");
            }
            
        }
       
        private void GetPic()
        {
            pictureBox1.Image = todayClassCrawler.getCodeStream();
            if (pictureBox1.Image == null)
            {
                pictureBox1.Image = Image.FromFile("../../Resource/net.png");
            }
        }
        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            GetPic();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Dispose();
        }
        public static string MD5(string encryptString)
        {

            byte[] result = Encoding.Default.GetBytes(encryptString);

            MD5 md5 = new MD5CryptoServiceProvider();

            byte[] output = md5.ComputeHash(result);

            string encryptResult = BitConverter.ToString(output).Replace("-", "");

            return encryptResult;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string pwd = MD5(textBox2.Text);
            todayClassCrawler.GetHtml(textBox1.Text, pwd, textBox3.Text);
            int flag = todayClassCrawler.Sucesse();
            if (flag==3)
            {
                todayClassCrawler.GetHtml2();
                todayClassCrawler.GetTodayClass();
                todayClassCrawler.CheckClass();
                Dispose();
            }
            else if(flag == 0)
            {
                MessageBox.Show("验证码错误！");
                pictureBox1.Image = todayClassCrawler.getCodeStream();
            }
            else if (flag == 1)
            {
                MessageBox.Show("用户名/密码错误！");
                pictureBox1.Image = todayClassCrawler.getCodeStream();
            }
            else 
            {
                MessageBox.Show("网络开小差了=_=！");
                GetPic();
            }
        }

        private void Login_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Location = new Point(this.Location.X + e.X - mPoint.X, this.Location.Y + e.Y - mPoint.Y);
            }
        }

        private void Login_MouseDown(object sender, MouseEventArgs e)
        {
            mPoint = new Point(e.X, e.Y);
        }
    }
}
