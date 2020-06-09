using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using caiwu.common;
using caiwu.common2;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Sunisoft.IrisSkin;
using winformUI.common;

namespace caiwu
{
    public partial class Form1 : Form
    {
        private SkinEngine skinEngine1;
        public static Rootobject rootobject = null;
        public static OrderInfo orderInfo = null;
        public Form1()
        {
            InitializeComponent();
            this.skinEngine1 = new Sunisoft.IrisSkin.SkinEngine(((System.ComponentModel.Component)(this)));
            this.skinEngine1.SkinFile = "Skins\\Page.ssk";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            String startdate = this.dateTimePicker1.Text;
            String enddate = this.dateTimePicker2.Text;

            
            long start_pay_time = DateTimeTool.ConvertDateTimeToInt(Convert.ToDateTime(startdate));
            long end_pay_time = DateTimeTool.ConvertDateTimeToInt(Convert.ToDateTime(enddate));
            String pay_status = this.comboBox1.SelectedValue.ToString();
            String shipping_status = this.comboBox2.SelectedValue.ToString();

            String url = "https://www.feifeigo.cn/api.php?app_key=A20DE5F4-BEA5-4E43-A5DC-AC173661F372&method=dsc.order.list.get&pay_status=" + pay_status +
                "&shipping_status=" + shipping_status +
                //"&start_pay_time=" + start_pay_time +
                "&end_pay_time=" + end_pay_time;
            String rep_str = HtmlParser.HttpGet(url);
            JObject jo1 = (JObject)JsonConvert.DeserializeObject(rep_str);
            string zone_data1 = jo1["info"]["list"].ToString();
            DataTable dt01 = Class1.JsonToDataTable2(zone_data1);
            rootobject = JsonConvert.DeserializeObject<Rootobject>(rep_str);
            MessageBox.Show(rootobject.msg);
            //for (int i = 0; i < rootobject.info.list.Length; i++)
            //{
            //    String rep_str2 = HtmlParser.HttpGet("https://www.feifeigo.cn/api.php?app_key=A20DE5F4-BEA5-4E43-A5DC-AC173661F372&method=dsc.order.goods.list.get&order_id="+ rootobject.info.list[i].order_id);
            //    orderInfo = JsonConvert.DeserializeObject<OrderInfo>(rep_str2);

            //    rootobject.info.list[i].goods_name = orderInfo.info.list[0].goods_name;
            //    rootobject.info.list[i].goods_number = ""+orderInfo.info.list[0].goods_number;
            //    rootobject.info.list[i].goods_price = "" + orderInfo.info.list[0].goods_price;
            //}

            //this.dataGridView1.DataSource = rootobject.info.list;
            this.dataGridView1.DataSource = dt01;

        }


        private void Form1_Load(object sender, EventArgs e)
        {
            CenterScreen();
            bindPayStat();
            bingdshipping_status();
        }

        public void bindPayStat() {
            IList<Item1> list2 = new List<Item1>();
            Item1 i0 = new Item1();
            i0.Id = "0";
            i0.Name = "未付款";
            list2.Add(i0);

            Item1 i1 = new Item1();
            i1.Id = "2";
            i1.Name = "已付款";
            list2.Add(i1);

            this.comboBox1.DataSource = list2;
            this.comboBox1.DisplayMember = "name";
            this.comboBox1.ValueMember = "id";
        }

        public void bingdshipping_status()
        {
            IList<Item1> list2 = new List<Item1>();
            Item1 i0 = new Item1();
            i0.Id = "0";
            i0.Name = "未发货";
            list2.Add(i0);

            Item1 i1 = new Item1();
            i1.Id = "1";
            i1.Name = "已发货";
            list2.Add(i1);

            Item1 i2 = new Item1();
            i2.Id = "2";
            i2.Name = "已收货";
            list2.Add(i2);

            Item1 i3 = new Item1();
            i3.Id = "3";
            i3.Name = "备货中";
            list2.Add(i3);

            Item1 i4 = new Item1();
            i4.Id = "4";
            i4.Name = "已发货(部分商品)";
            list2.Add(i4);


            Item1 i5 = new Item1();
            i5.Id = "5";
            i5.Name = "发货中(处理分单)";
            list2.Add(i5);

            Item1 i6 = new Item1();
            i6.Id = "4";
            i6.Name = "已发货(部分商品)";
            list2.Add(i6);


            this.comboBox2.DataSource = list2;
            this.comboBox2.DisplayMember = "name";
            this.comboBox2.ValueMember = "id";
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
    }
}
