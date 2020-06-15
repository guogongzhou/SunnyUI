using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;
using caiwu.common;
using caiwu.common2;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using server.common;
using Sunisoft.IrisSkin;
using winformUI.common;

namespace caiwu
{
    public partial class Form1 : Form
    {

        DataTable dt01;
        DataTable dt02;
        Boolean flag = false;
        private SkinEngine skinEngine1;

        public Form1()
        {
            InitializeComponent();
            this.skinEngine1 = new Sunisoft.IrisSkin.SkinEngine(((System.ComponentModel.Component)(this)));
            this.skinEngine1.SkinFile = "Skins\\Page.ssk";
        }


        public void todo(object sender, EventArgs e) {

            monitorHaoliu();
        }


        public void monitorHaoliu(){
            LogHelper.WriteLog("开始执行监控haoliu");
            String rep_str2 = HtmlParser.HttpGet("http://api.coyuat.cn:1024/haoliu/tongji/tongji?begin_date=2020-03-01&end_date=2020-03-27&pwd=mMuOQA0UytgP7rb3ZJxYSVSVyGNGw5bh&type=1");
            if (!"".Equals(rep_str2))
            {
                LogHelper.WriteLog("haoliu状态正常");
            }
            else {
                LogHelper.WriteLog("haoliu状态");
            }
            LogHelper.WriteLog("结束执行监控haoliu");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            todo(sender,e);
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            CenterScreen();
            
          
            Class1.initData();
            timer1.Interval = 1000*60;
            timer1.Tick += new EventHandler(todo);
            
            timer1.Start();
        }
         
       
        

        public void CenterScreen()
        {
            int height = System.Windows.Forms.SystemInformation.WorkingArea.Height;
            int width = System.Windows.Forms.SystemInformation.WorkingArea.Width;
            int formheight = this.Size.Height;
            int formwidth = this.Size.Width;
            int newformx = width / 2 - formwidth / 2;
            int newformy = height / 2 - formheight / 2;
            this.SetDesktopLocation(newformx, newformy);
            this.MaximizeBox = false;
        }

        private void dataGridView1_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            var grid = sender as DataGridView;
            var rowIdx = (e.RowIndex + 1).ToString();

            var centerFormat = new StringFormat()
            {
                // right alignment might actually make more sense for numbers
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center
            };

            var headerBounds = new Rectangle(e.RowBounds.Left, e.RowBounds.Top, grid.RowHeadersWidth, e.RowBounds.Height);
            e.Graphics.DrawString(rowIdx, this.Font, SystemBrushes.ControlText, headerBounds, centerFormat);
        }

         
        private void button4_Click(object sender, EventArgs e)
        {
            monitorHaoliu();

        }
    }
}
