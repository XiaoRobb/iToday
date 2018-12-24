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

        public Login(TodayClassCrawler todayClassCrawler)
        {
            InitializeComponent();
            this.todayClassCrawler = todayClassCrawler;
            pictureBox1.Image = todayClassCrawler.getCodeStream();
        }

        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            pictureBox1.Image = todayClassCrawler.getCodeStream();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = todayClassCrawler.getCodeStream();
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
                MessageBox.Show("登录失败！");
                pictureBox1.Image = todayClassCrawler.getCodeStream();
            }
        }
    }
}
