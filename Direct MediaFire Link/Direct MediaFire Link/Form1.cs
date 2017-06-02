using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Direct_MediaFire_Link
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        public string Mediafire(string download)
        {
            string direct = "";
            try
            {
                HttpWebRequest req;
                HttpWebResponse res;
                string str = "";
                req = (HttpWebRequest)WebRequest.Create(download);
                res = (HttpWebResponse)req.GetResponse();
                str = new StreamReader(res.GetResponseStream()).ReadToEnd();
                int indexurl = str.IndexOf("http://download");
                int indexend = GetNextIndexOf('"', str, indexurl);
                 direct = str.Substring(indexurl, indexend - indexurl);
            }
            catch {  }
            return direct;
        }
        int GetNextIndexOf(char c, string source, int start)
        {
            try
            {
                if (start < 0 || start > source.Length - 1)
                {
                    MessageBox.Show("Can't Get The Direct Link");

                }
                for (int i = start; i < source.Length; i++)
                {
                    if (source[i] == c)
                    {
                        return i;
                    }
                    Application.DoEvents();
                }
                
            }
            catch { }
            return -1;
        }
        
        
        class MediaFireDirectLink
        {
            string directLink;
            string source;
            public MediaFireDirectLink(string link)
            {
                source = new WebClient().DownloadString(link);
                directLink = Regex.Match(source, @"http://download.*").Value.Replace("\"", "").Replace(";", "");
            }

            public string FileName
            {
                get
                {
                    return  directLink.Substring(directLink.LastIndexOf('/') + 1);
                }
            }
            public string DirectLink
            {
                get
                {
                    return directLink;
                }
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            textBox2.Text = Mediafire(textBox1.Text);
            
            //or 
              var data = new MediaFireDirectLink(textBox1.Text);
            textBox2.Text = data.DirectLink;
            label1.Text = data.FileName;
        }
    }
}
