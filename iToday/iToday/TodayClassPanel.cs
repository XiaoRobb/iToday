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
    class TodayClassPanel : FlowLayoutPanel
    {
        private Label label_name = new Label();
        private Label label_teacher = new Label();
        private Label label_infor = new Label();
        public TodayClassPanel(TodayClass todayClass) : base()
        {
            Init();
            label_name.Text = todayClass.lessonName;
            label_teacher.Text = todayClass.teacherName;
            label_infor.Text = todayClass.detail;
        }
       

        private void Init()
        {
            this.Width = 180;
            this.Height = 90;
                     
            label_name.Width = 180;
            label_name.Height = 30;
            label_name.Font = new Font("隶书", 10, FontStyle.Bold);
            label_name.TextAlign = ContentAlignment.MiddleCenter;

            label_teacher.Width = 180;
            label_teacher.Height = 30;
            label_teacher.Font = new Font("隶书", 10, FontStyle.Bold);
            label_teacher.TextAlign = ContentAlignment.MiddleCenter;

            label_infor.Width = 180;
            label_infor.Height = 30;
            label_infor.Font = new Font("隶书", 10, FontStyle.Bold);
            label_infor.TextAlign = ContentAlignment.MiddleCenter;
            this.Controls.Add(label_name);
            this.Controls.Add(label_teacher);
            this.Controls.Add(label_infor);
        } 
     
    }
}
