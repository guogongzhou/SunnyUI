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
        Boolean flag=false;
        private SkinEngine skinEngine1;
        public static Rootobject rootobject = null;
        public static OrderInfo orderInfo = null;
        public Form1()
        {
            InitializeComponent();
            this.skinEngine1 = new Sunisoft.IrisSkin.SkinEngine(((System.ComponentModel.Component)(this)));
            this.skinEngine1.SkinFile = "Skins\\Page.ssk";
        }


        public void todo(object sender, EventArgs e) {
            

            System.DateTime time1 = System.DateTime.Now;

            //每天的10:50干活
            //if (time.Hour == 10 && time.Minute == 50 && time.Second == 0).

            //每小时的第一分钟干活
            //if (time1.Minute == 17)
            int runMinute = 30;
            LogHelper.WriteLog("定时执行，检查"+ runMinute + "分是否运行");
            if (time1.Minute == runMinute)
            {
                LogHelper.WriteLog("开始执行");
 
                long start_add_time = DateTimeTool.ConvertDateTimeToInt(DateTimeTool.TodayDate1(1));
                long end_add_time = DateTimeTool.ConvertDateTimeToInt(DateTimeTool.TodayDate1(0));

                String pay_status = "2";
                //String shipping_status = this.comboBox2.SelectedValue.ToString();
                String url = "https://www.feifeigo.cn/api.php?app_key=A20DE5F4-BEA5-4E43-A5DC-AC173661F372&page_size=1000&method=dsc.order.list.get&pay_status=" + pay_status +
                
                 "&start_add_time=" + start_add_time +
                 "&end_add_time=" + end_add_time;
                String rep_str = HtmlParser.HttpGet(url);
                LogHelper.WriteLog("获取订单列表请求地址" + url);
                if ("".Equals(rep_str)) {
                    LogHelper.WriteLog("获取订单列表无响应");
                    return;
                }
                LogHelper.WriteLog("获取订单列表返回数据" + rep_str);
                JObject jo1 = (JObject)JsonConvert.DeserializeObject(rep_str);
                string zone_data1 = jo1["info"]["list"].ToString();

                rootobject = JsonConvert.DeserializeObject<Rootobject>(rep_str);
                StringBuilder stringBuilder = new StringBuilder();
                StringBuilder stringBuilder_orderGoods = new StringBuilder();
                for (int i = 0; i < rootobject.info.list.Length; i++)
                {
                    String getOrderinfo_req = "https://www.feifeigo.cn/api.php?app_key=A20DE5F4-BEA5-4E43-A5DC-AC173661F372&method=dsc.order.goods.list.get&order_id=" + rootobject.info.list[i].order_id;
                    LogHelper.WriteLog("获取订单详情请求地址" + getOrderinfo_req);
                    String rep_str2 = HtmlParser.HttpGet(getOrderinfo_req);
                    if ("".Equals(rep_str2))
                    {
                        LogHelper.WriteLog("获取订单详情无响应");
                        return;
                    }
                    LogHelper.WriteLog("获取订单详情返回数据" + rep_str2);
                    orderInfo = JsonConvert.DeserializeObject<OrderInfo>(rep_str2);
                    stringBuilder.Append("('" + rootobject.info.list[i].order_id + "', '" + rootobject.info.list[i].main_order_id + "', '" + rootobject.info.list[i].order_sn + "', '" + rootobject.info.list[i].user_id + "', '" + rootobject.info.list[i].order_status + "', '" + rootobject.info.list[i].shipping_status + "', '" + rootobject.info.list[i].pay_status + "', '" + rootobject.info.list[i].consignee + "', '" + rootobject.info.list[i].country + "', '" + rootobject.info.list[i].province + "', '" + rootobject.info.list[i].city + "', '" + rootobject.info.list[i].district + "', '" + rootobject.info.list[i].street + "', '" + rootobject.info.list[i].address + "', '" + rootobject.info.list[i].zipcode + "', '" + rootobject.info.list[i].tel + "', '" + rootobject.info.list[i].mobile + "', '" + rootobject.info.list[i].email + "', '" + rootobject.info.list[i].best_time + "', '" + rootobject.info.list[i].sign_building + "', '" + rootobject.info.list[i].postscript + "', '" + rootobject.info.list[i].shipping_id + "', '" + rootobject.info.list[i].shipping_name + "', '" + rootobject.info.list[i].shipping_code + "', '" + rootobject.info.list[i].shipping_type + "', '" + rootobject.info.list[i].pay_id + "', '" + rootobject.info.list[i].pay_name + "', '" + rootobject.info.list[i].how_oos + "', '" + rootobject.info.list[i].how_surplus + "', '" + rootobject.info.list[i].pack_name + "', '" + rootobject.info.list[i].card_name + "', '" + rootobject.info.list[i].card_message + "', '" + rootobject.info.list[i].inv_payee + "', '" + rootobject.info.list[i].inv_content + "', '" + rootobject.info.list[i].goods_amount + "', '" + rootobject.info.list[i].cost_amount + "', '" + rootobject.info.list[i].shipping_fee + "', '" + rootobject.info.list[i].insure_fee + "', '" + rootobject.info.list[i].pay_fee + "', '" + rootobject.info.list[i].pack_fee + "', '" + rootobject.info.list[i].card_fee + "', '" + rootobject.info.list[i].money_paid + "', '" + rootobject.info.list[i].surplus + "', '" + rootobject.info.list[i].integral + "', '" + rootobject.info.list[i].integral_money + "', '" + rootobject.info.list[i].bonus + "', '" + rootobject.info.list[i].order_amount + "', '" + rootobject.info.list[i].return_amount + "', '" + rootobject.info.list[i].from_ad + "', '" + rootobject.info.list[i].referer + "', '" + rootobject.info.list[i].add_time + "', '" + rootobject.info.list[i].confirm_time + "', '" + rootobject.info.list[i].pay_time + "', '" + rootobject.info.list[i].shipping_time + "', '" + rootobject.info.list[i].confirm_take_time + "', '" + rootobject.info.list[i].auto_delivery_time + "', '" + rootobject.info.list[i].pack_id + "', '" + rootobject.info.list[i].card_id + "', '" + rootobject.info.list[i].bonus_id + "', '" + rootobject.info.list[i].invoice_no + "', '" + rootobject.info.list[i].extension_code + "', '" + rootobject.info.list[i].extension_id + "', '" + rootobject.info.list[i].to_buyer + "', '" + rootobject.info.list[i].pay_note + "', '" + rootobject.info.list[i].agency_id + "', '" + rootobject.info.list[i].inv_type + "', '" + rootobject.info.list[i].tax + "', '" + rootobject.info.list[i].is_separate + "', '" + rootobject.info.list[i].parent_id + "', '" + rootobject.info.list[i].discount + "', '" + rootobject.info.list[i].discount_all + "', '" + rootobject.info.list[i].is_delete + "', '" + rootobject.info.list[i].is_settlement + "', '" + rootobject.info.list[i].sign_time + "', '" + rootobject.info.list[i].is_single + "', '" + rootobject.info.list[i].point_id + "', '" + rootobject.info.list[i].shipping_dateStr + "', '" + rootobject.info.list[i].supplier_id + "', '" + rootobject.info.list[i].froms + "', '" + rootobject.info.list[i].coupons + "', '" + rootobject.info.list[i].uc_id + "', '" + rootobject.info.list[i].is_zc_order + "', '" + rootobject.info.list[i].zc_goods_id + "', '" + rootobject.info.list[i].is_frozen + "', '" + rootobject.info.list[i].drp_is_separate + "', '" + rootobject.info.list[i].team_id + "', '" + rootobject.info.list[i].team_parent_id + "', '" + rootobject.info.list[i].team_user_id + "', '" + rootobject.info.list[i].team_price + "', '" + rootobject.info.list[i].chargeoff_status + "', '" + rootobject.info.list[i].invoice_type + "', '" + rootobject.info.list[i].vat_id + "', '" + rootobject.info.list[i].tax_id + "', '" + rootobject.info.list[i].is_update_sale + "', '" + rootobject.info.list[i].ru_id + "', '" + rootobject.info.list[i].main_count + "', '" + rootobject.info.list[i].rel_name + "', '" + rootobject.info.list[i].id_num + "', '" + rootobject.info.list[i].stores_name + "', '" + rootobject.info.list[i].rate_fee + "', '" + rootobject.info.list[i].main_pay + "', '" + rootobject.info.list[i].child_show + "', '" + rootobject.info.list[i].store_start_time + "', '" + rootobject.info.list[i].store_end_time + "', '" + rootobject.info.list[i].selectTime + "', '" + rootobject.info.list[i].country_name + "', '" + rootobject.info.list[i].province_name + "', '" + rootobject.info.list[i].city_name + "', '" + rootobject.info.list[i].district_name + "', '" + rootobject.info.list[i].street_name + "')" + (i < rootobject.info.list.Length - 1 ? "," : "") + "");

                    for (int j = 0; j < orderInfo.info.list.Length; j++)
                    {
                        stringBuilder_orderGoods.Append("('" + orderInfo.info.list[j].order_id + "', '" + orderInfo.info.list[j].rec_id + "', '" + orderInfo.info.list[j].user_id + "', '" + orderInfo.info.list[j].cart_recid + "', '" + orderInfo.info.list[j].goods_id + "', '" + orderInfo.info.list[j].goods_name + "', '" + orderInfo.info.list[j].goods_sn + "', '" + orderInfo.info.list[j].product_id + "', '" + orderInfo.info.list[j].goods_number + "', '" + orderInfo.info.list[j].market_price + "', '" + orderInfo.info.list[j].goods_price + "', '" + orderInfo.info.list[j].goods_attr + "', '" + orderInfo.info.list[j].send_number + "', '" + orderInfo.info.list[j].is_real + "', '" + orderInfo.info.list[j].extension_code + "', '" + orderInfo.info.list[j].parent_id + "', '" + orderInfo.info.list[j].is_gift + "', '" + orderInfo.info.list[j].model_attr + "', '" + orderInfo.info.list[j].goods_attr_id + "', '" + orderInfo.info.list[j].ru_id + "', '" + orderInfo.info.list[j].shopping_fee + "', '" + orderInfo.info.list[j].warehouse_id + "', '" + orderInfo.info.list[j].area_id + "', '" + orderInfo.info.list[j].area_city + "', '" + orderInfo.info.list[j].is_single + "', '" + orderInfo.info.list[j].freight + "', '" + orderInfo.info.list[j].tid + "', '" + orderInfo.info.list[j].shipping_fee + "', '" + orderInfo.info.list[j].drp_money + "', '" + orderInfo.info.list[j].is_distribution + "', '" + orderInfo.info.list[j].commission_rate + "', '" + orderInfo.info.list[j].stages_qishu + "', '" + orderInfo.info.list[j].product_sn + "', '" + orderInfo.info.list[j].is_reality + "', '" + orderInfo.info.list[j].is_return + "', '" + orderInfo.info.list[j].is_fast + "', '" + orderInfo.info.list[j].rate_price + "', '" + orderInfo.info.list[j].goods_bonus + "', '" + orderInfo.info.list[j].cost_price + "', '" + orderInfo.info.list[j].goods_coupons + "', '" + orderInfo.info.list[j].buy_drp_show + "', '" + orderInfo.info.list[j].membership_card_id + "', '" + orderInfo.info.list[j].membership_card_discount_price + "', '" + orderInfo.info.list[j].is_received + "', '" + orderInfo.info.list[j].main_count + "', '" + orderInfo.info.list[j].is_comment + "', '" + orderInfo.info.list[j].country_name + "', '" + orderInfo.info.list[j].province_name + "', '" + orderInfo.info.list[j].city_name + "', '" + orderInfo.info.list[j].district_name + "', '" + orderInfo.info.list[j].street_name + "')" + (i < rootobject.info.list.Length - 1 ? "," : "") + "");

                    }
                }

                //this.dataGridView1.DataSource = rootobject.info.list;
                if (rootobject.info.list.Length > 0)
                {
                    String sql = "insert into t_orderlist(order_id,main_order_id,order_sn,user_id,order_status,shipping_status,pay_status,consignee,country,province,city,district,street,address,zipcode,tel,mobile,email,best_time,sign_building,postscript,shipping_id,shipping_name,shipping_code,shipping_type,pay_id,pay_name,how_oos,how_surplus,pack_name,card_name,card_message,inv_payee,inv_content,goods_amount,cost_amount,shipping_fee,insure_fee,pay_fee,pack_fee,card_fee,money_paid,surplus,integral,integral_money,bonus,order_amount,return_amount,from_ad,referer,add_time,confirm_time,pay_time,shipping_time,confirm_take_time,auto_delivery_time,pack_id,card_id,bonus_id,invoice_no,extension_code,extension_id,to_buyer,pay_note,agency_id,inv_type,tax,is_separate,parent_id,discount,discount_all,is_delete,is_settlement,sign_time,is_single,point_id,shipping_dateStr,supplier_id,froms,coupons,uc_id,is_zc_order,zc_goods_id,is_frozen,drp_is_separate,team_id,team_parent_id,team_user_id,team_price,chargeoff_status,invoice_type,vat_id,tax_id,is_update_sale,ru_id,main_count,rel_name,id_num,stores_name,rate_fee,main_pay,child_show,store_start_time,store_end_time,selectTime,country_name,province_name,city_name,district_name,street_name) values " + stringBuilder + "";
                    String sq2 = "insert into t_ordergoods(order_id,rec_id,user_id,cart_recid,goods_id,goods_name,goods_sn,product_id,goods_number,market_price,goods_price,goods_attr,send_number,is_real,extension_code,parent_id,is_gift,model_attr,goods_attr_id,ru_id,shopping_fee,warehouse_id,area_id,area_city,is_single,freight,tid,shipping_fee,drp_money,is_distribution,commission_rate,stages_qishu,product_sn,is_reality,is_return,is_fast,rate_price,goods_bonus,cost_price,goods_coupons,buy_drp_show,membership_card_id,membership_card_discount_price,is_received,main_count,is_comment,country_name,province_name,city_name,district_name,street_name) values " + stringBuilder_orderGoods + "";
                    SQLHelper.ExecuteNonQuery(sql);

                    SQLHelper.ExecuteNonQuery(sq2);
                }
                else {
                    LogHelper.WriteLog("获取订单结束没有订单列表" + url);
                }
                
             
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            todo(sender,e);
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            CenterScreen();
            bindPayStat();
            bingdshipping_status();
            Class1.initData();
            timer1.Interval = 1000*60;
            timer1.Tick += new EventHandler(todo);
            
            timer1.Start();
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

        private void button2_Click(object sender, EventArgs e)
        {
            //DataTableToExcel.DataToExcel(dt01);
            DataTableToExcel.ExportDataToExcel(this.dataGridView1);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DataTableToExcel.DataToExcel(dt02);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            String rep_str2 = HtmlParser.HttpGet("https://www.feifeigo.cn/api.php?app_key=A20DE5F4-BEA5-4E43-A5DC-AC173661F372&method=dsc.order.goods.list.get&order_id=356" );
            orderInfo = JsonConvert.DeserializeObject<OrderInfo>(rep_str2);
         
            this.dataGridView1.DataSource = orderInfo.info.list;
        }
    }
}
