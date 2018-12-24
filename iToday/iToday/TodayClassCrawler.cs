using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace iToday
{

    public class TodayClassCrawler
    {
        public string html;
        public string html2;
        public List<TodayClass> list = new List<TodayClass>();
        private readonly string url = "http://210.42.121.241/servlet/Login";                //爬取的网站

        public CookieContainer cookies = new CookieContainer(); //存储验证码cookie
        public Image getCodeStream()
        {
            //验证码请求
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://210.42.121.241/servlet/GenImg");
            request.Method = "GET";
            request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/70.0.3538.110 Safari/537.36";
            request.Accept = "image/webp,image/apng,image/*,*/*;q=0.8";
            request.CookieContainer = cookies;//暂存到新实列
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream stream = response.GetResponseStream();
            Image img = Image.FromStream(stream);
            response.Close();
            return img;
        }
        public bool GetHtml(string id, string pwd, string xdvfb)                     //开始爬取
        {


            try
            {
                string postdata = string.Format("id={0}&pwd={1}&xdvfb={2}", id, pwd, xdvfb);  //这里按照前面FireBug中查到的POST字符串做相应修改。
                HttpWebRequest MyRequest = (HttpWebRequest)WebRequest.Create(url);
                MyRequest.Method = "POST";
                MyRequest.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8";
                MyRequest.ContentType = "application/x-www-form-urlencoded";
                MyRequest.CookieContainer = cookies;
                MyRequest.UserAgent = "Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/70.0.3538.110 Safari/537.36";
                Encoding myEncoding = Encoding.GetEncoding("gb2312");
                if (postdata != null)
                {

                    byte[] MyByte = Encoding.UTF8.GetBytes(postdata);
                    Stream MyStream = MyRequest.GetRequestStream();
                    MyStream.Write(MyByte, 0, postdata.Length);
                    MyStream.Close();

                }
                HttpWebResponse MyResponse = (HttpWebResponse)MyRequest.GetResponse();
                using (StreamReader MyStreamReader = new StreamReader(MyResponse.GetResponseStream(), myEncoding))
                {
                    html = MyStreamReader.ReadToEnd();
                }
                return true;
            }
            catch (Exception e)
            {
                html = e.ToString();
                return false;
            }
        }
        public string GetClassHtml()
        {
            string reg = @"/servlet/Svlt_QueryStuLsn[\s\S]*?'";
            Regex r = new Regex(reg, RegexOptions.IgnoreCase);
            Match match = r.Match(html);
            string temp = match.ToString();
            temp = "http://210.42.121.241" + temp.Substring(0, temp.Length - 1);
            return temp;
        }
        public bool GetHtml2()                     //开始爬取
        {
            try
            {
                String url2 = GetClassHtml();
                //string postdata = string.Format("id={0}&pwd={1}&xdvfb={2}", id, pwd, xdvfb);  //这里按照前面FireBug中查到的POST字符串做相应修改。
                HttpWebRequest MyRequest = (HttpWebRequest)WebRequest.Create(url2);
                MyRequest.Method = "GET";
                MyRequest.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8";
                MyRequest.ContentType = "application/x-www-form-urlencoded";
                MyRequest.CookieContainer = cookies;
                MyRequest.UserAgent = "Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/70.0.3538.110 Safari/537.36";
                Encoding myEncoding = Encoding.GetEncoding("gb2312");
                HttpWebResponse MyResponse = (HttpWebResponse)MyRequest.GetResponse();
                using (StreamReader MyStreamReader = new StreamReader(MyResponse.GetResponseStream(), myEncoding))
                {
                    html2 = MyStreamReader.ReadToEnd();
                }
                return true;
            }
            catch (Exception e)
            {
                html2 = e.ToString();
                return false;
            }
        }

        public void GetTodayClass()                     //开始爬取
        {
            int w = 0;
            DayOfWeek week = DateTime.Now.DayOfWeek;
            switch (week)
            {
                case DayOfWeek.Monday:
                    w = 1;
                    break;
                case DayOfWeek.Tuesday:
                    w = 2;
                    break;
                case DayOfWeek.Wednesday:
                    w = 3;
                    break;
                case DayOfWeek.Thursday:
                    w = 4;
                    break;
                case DayOfWeek.Friday:
                    w = 5;
                    break;
                case DayOfWeek.Saturday:
                    w = 6;
                    break;
                case DayOfWeek.Sunday:
                    w = 7;
                    break;
            }
            if (!string.IsNullOrEmpty(html2))
            {
                //string reg = @"var lessonName =[\s\S]*?day = """ + w + @"[\s\S]*?任课老师";
                string reg = @"var lessonName =[\s\S]*?任课老师";
                // 定义正则表达式用来匹配 img 标签   
                Regex regImg = new Regex(reg, RegexOptions.IgnoreCase);
                // 搜索匹配的字符串   
                MatchCollection matches = regImg.Matches(html2);
                List<string> sUrlList = new List<string>();

                // 取得匹配项列表   
                foreach (Match match in matches)
                    sUrlList.Add(match.ToString());
                foreach (string str in sUrlList)
                {
                    reg = @"day = """ + w;
                    regImg = new Regex(reg, RegexOptions.IgnoreCase);
                    Match match = regImg.Match(str);
                    if (match.Success)
                    {
                        list.Add(CreatClass(str));
                    }
                }
            }

        }

        public TodayClass CreatClass(string str)
        {
            string lessonName;                       //课程名
            string teacherName;                      //老师姓名
            string beginTime;                        //开始时间
            string endTime;                          //结束时间
            string detail;                           //信息
            string beginWeek;                        //开始周
            string endWeek;                          //结束周
            string temp;

            string reg = @"lessonName[\s\S]*?"";";
            Regex r = new Regex(reg, RegexOptions.IgnoreCase);
            Match match = r.Match(str);
            temp = match.ToString();
            lessonName = temp.Substring(14, temp.Length - 14 - 2);

            reg = @"beginWeek[\s\S]*?"";";
            r = new Regex(reg, RegexOptions.IgnoreCase);
            match = r.Match(str);
            temp = match.ToString();
            beginTime = temp.Substring(13, temp.Length - 13 - 2);

            reg = @"endWeek[\s\S]*?"";";
            r = new Regex(reg, RegexOptions.IgnoreCase);
            match = r.Match(str);
            temp = match.ToString();
            endTime = temp.Substring(11, temp.Length - 11 - 2);

            reg = @"teacherName[\s\S]*?"";";
            r = new Regex(reg, RegexOptions.IgnoreCase);
            match = r.Match(str);
            temp = match.ToString();
            teacherName = temp.Substring(15, temp.Length - 15 - 2);

            reg = @"detail[\s\S]*?"";";
            r = new Regex(reg, RegexOptions.IgnoreCase);
            match = r.Match(str);
            temp = match.ToString();
            detail = temp.Substring(8, temp.Length - 8 - 2);

            reg = @"beginWeek[\s\S]*?"";";
            r = new Regex(reg, RegexOptions.IgnoreCase);
            match = r.Match(str);
            temp = match.ToString();
            beginWeek = temp.Substring(13, temp.Length - 13 - 2);


            reg = @"endWeek[\s\S]*?"";";
            r = new Regex(reg, RegexOptions.IgnoreCase);
            match = r.Match(str);
            temp = match.ToString();
            endWeek = temp.Substring(11, temp.Length - 11 - 2);

            return new TodayClass(lessonName, teacherName, beginTime, endTime, detail, beginTime, endWeek);
        }

        public void getTodayWeek()
        {
            string reg = @"第[1-9]*教学周";
            Regex r = new Regex(reg, RegexOptions.IgnoreCase);
            Match match = r.Match(html);
            string temp = match.ToString();
            reg = @"[1-9]+";
            r = new Regex(reg, RegexOptions.IgnoreCase);
            match = r.Match(temp);
            temp = match.ToString();
            TodayClass.nowWeek = temp;
        }
        public int Sucesse()
        {
            string reg = @"验证码错误";
            Regex r = new Regex(reg, RegexOptions.IgnoreCase);
            Match match = r.Match(html);
            if (match.Success)
            {
                return 0;
            }
            reg = @"密码错误";
            r = new Regex(reg, RegexOptions.IgnoreCase);
            match = r.Match(html);
            if (match.Success)
            {
                return 1;
            }
            reg = @"修改密码";
            r = new Regex(reg, RegexOptions.IgnoreCase);
            match = r.Match(html);
            if (match.Success)
            {
                return 3;
            }
            return 2;
        }
        public void CheckClass()
        {
            getTodayWeek();
            for (int i = 0; i < list.Count; i++)
            {
                if (string.Compare(TodayClass.nowWeek, list.ElementAt(i).endTime) == 1 || string.Compare(TodayClass.nowWeek, list.ElementAt(i).beginTime) == -1)
                {
                    list.RemoveAt(i);
                    i--;
                }
            }
        }
    }
}
