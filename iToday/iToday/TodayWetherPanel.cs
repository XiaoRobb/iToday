using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace iToday
{
    class TodayWetherPanel : FlowLayoutPanel
    {
        private Label label_wether = new Label();

        private Label label_temper = new Label();
        private Label label_wind = new Label();
        public TodayWetherPanel(TodayWether wether) : base()
        {
            if(wether!=null)
            {
                label_wether.Text = wether.Wether;
                label_temper.Text = wether.Temperature;
                label_wind.Text = wether.Wind;
            }       
            Init();
        }
        public void Init()
        {
            this.Width = 210;
            this.Height = 200;
            label_wether.Width = 200;
            label_wether.Height = 40;
            label_wether.Font = new Font("隶书", 20, FontStyle.Bold);
            label_wether.TextAlign = ContentAlignment.MiddleCenter;


            label_temper.Width = 200;
            label_temper.Height = 40;
            label_temper.Font = new Font("隶书", 18, FontStyle.Bold);
            label_temper.TextAlign = ContentAlignment.MiddleCenter;

            label_wind.Width = 200;
            label_wind.Height = 40;
            label_wind.Font = new Font("隶书", 18, FontStyle.Bold);
            label_wind.TextAlign = ContentAlignment.MiddleCenter;


            this.Controls.Add(label_wether);
            this.Controls.Add(label_temper);
            this.Controls.Add(label_wind);
        }
    }
}
