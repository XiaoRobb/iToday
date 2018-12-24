using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace iToday
{
    class TodayDatePanel : FlowLayoutPanel
    {
        private Label label_date = new Label();
        //private Label label_num = new Label();
        private Label label_week = new Label();
        private Label label_dateNular = new Label();
        public TodayDatePanel(TodayDate day) : base()
        {
            label_date.Text = day.today.ToString();
            //label_num.Text = day.today.Day.ToString();
            label_week.Text = day.week.ToString();
            label_dateNular.Text = day.todayNular;
            Init();
        }
        public void Init()
        {
            this.Width = 210;
            this.Height = 200;
            label_date.Width = 200;
            label_date.Height = 40;
            label_date.Font = new Font("隶书", 20, FontStyle.Bold);
            label_date.TextAlign = ContentAlignment.MiddleCenter;

            //label_num.Width = 72;
            //label_num.Height = 72;
            //label_num.Font = new Font("隶书", 30, FontStyle.Bold);
            //label_num.TextAlign = ContentAlignment.MiddleCenter;

            label_week.Width = 200;
            label_week.Height = 40;
            label_week.Font = new Font("隶书", 18, FontStyle.Bold);
            label_week.TextAlign = ContentAlignment.MiddleCenter;

            label_dateNular.Width = 200;
            label_dateNular.Height = 40;
            label_dateNular.Font = new Font("隶书", 18, FontStyle.Bold);
            label_dateNular.TextAlign = ContentAlignment.MiddleCenter;

            //this.BackColor = Color.FromArgb(100, Color.Gray);

            this.Controls.Add(label_date);
            //this.Controls.Add(label_num);
            this.Controls.Add(label_week);
            this.Controls.Add(label_dateNular);
        }
    }
}
