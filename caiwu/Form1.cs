using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using caiwu.common;
using caiwu.common2;
using FastReport;
using FastReport.Table;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Sunisoft.IrisSkin;
using winformUI.common;

namespace caiwu
{
    public partial class Form1 : Form
    {
        DataTable dt01;
        DataTable dt02;
        Dictionary<String, Zitidian> dic_zitidian  ;
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
            //while (this.dataGridView1.Rows.Count != 0)
            //{

            //    this.dataGridView1.Rows.RemoveAt(0);
            //}
            
            long startdate = DateTimeTool.ConvertDateTimeToLong(this.dateTimePicker1.Value);
            long enddate = DateTimeTool.ConvertDateTimeToLong(this.dateTimePicker2.Value);
            String zhandianmingcheng = this.comboBox1.SelectedValue.ToString();
            String sql = "SELECT t2.goods_name   , sum(t2.goods_number) ,sum(t2.goods_price), t1.stores_name   FROM t_orderlist t1, t_ordergoods t2 WHERE t1.order_id = t2.order_id AND t1.order_success_time > " + startdate+" AND t1.order_success_time < "+enddate+" AND t1.stores_name != 'false'AND t1.stores_name != ''AND t1.stores_name IS NOT NULL AND t1.zhandianmingcheng = '"+zhandianmingcheng+ "' group by stores_name,goods_sn,goods_name order by t1.stores_name ";
            
                dt01=SQLHelper.ExecuteDt(sql);
                ddd(dt01);
            //this.dataGridView1.DataSource = dt01;
        }


        public void ddd(DataTable dt01) {
            if (dataGridView1.Columns["name"]!=null) {
                dataGridView1.Columns.Remove("name");
                dataGridView1.Columns.Remove("number");
                dataGridView1.Columns.Remove("price");
                dataGridView1.Columns.Remove("strore_name");
                this.dataGridView1.Rows.Clear();
            }

             
            //每一列必须设置CellTemplate
            //第一列
            dataGridView1.Columns.Add(new DataGridViewColumn() { Name = "name", HeaderText = "商品名称", Width = 100, CellTemplate = new DataGridViewTextBoxCell(), MinimumWidth = 100 });


            //第二列
            DataGridViewColumn ageColumn = new DataGridViewColumn()
            {
                Name = "number",
                HeaderText = "商品数量",
                Width = 100,
                CellTemplate = new DataGridViewTextBoxCell()
            };

            DataGridViewColumn ColumnPrice = new DataGridViewColumn()
            {
                Name = "price",
                HeaderText = "商品总价",
                Width = 100,
                CellTemplate = new DataGridViewTextBoxCell()
            };


            DataGridViewColumn ageColumn3 = new DataGridViewColumn()
            {
                Name = "strore_name",
                HeaderText = "自提点名称",
                Width = 100,
                CellTemplate = new DataGridViewTextBoxCell()
            };
            //设置文本对齐方式
            ageColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.Columns.Add(ageColumn);
            dataGridView1.Columns.Add(ColumnPrice);
            dataGridView1.Columns.Add(ageColumn3);



            foreach (DataRow line in dt01.Rows)
            {
                dataGridView1.Rows.Add(line.ItemArray);
            }

            for (int i = 0; i < dt01.Rows.Count-1; i++)
            {
                //String name= dt01.Rows[i][0].ToString();
                //String number = dt01.Rows[i][1].ToString();
                //String strore_name = dt01.Rows[i][2].ToString();
                //dataGridView1.Rows.Add(Cells["name"].Value = name);// .Rows[i].Cells["name"].Value = name;
                //dataGridView1.Rows[i].Cells["number"].Value = number;
                //dataGridView1.Rows[i].Cells["strore_name"].Value = strore_name;


                //DataGridViewRow dr = new DataGridViewRow();
                //DataGridViewColumn textcell1 = new DataGridViewColumn();
                //textcell1.HeaderText = "宝宝" + i;
                //textcell1
                //dr.Cells.Add(textcell1);
                //DataGridViewTextBoxCell textcell2 = new DataGridViewTextBoxCell();
                //textcell2.Value = "宝宝" + i;
                //dr.Cells.Add(textcell2);
                //dataGridView1.Rows.Add(dr);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            CenterScreen();
            bindZitidian();
            bingdshipping_status();
           // Class1.initData();
        }


        public void bindzitidian() {


        }
        public void bindZitidian() {

            String sql = "select t2.zhandianmingcheng as '站点名称',t2.zitidianmingcheng as '自提点名称',t2.zid as '自提点编号',diqu as '地区',t2.dizhi as '地址' ,t2.xingming as '姓名',t2.mobile as '手机号' from t_zhandian t2";
            this.dataGridView2.DataSource= SQLHelper.ExecuteDt(sql);
            //IList<Item1> list2 = new List<Item1>();
            //Item1 i0 = new Item1();
            //i0.Id = "0";
            //i0.Name = "未付款";
            //list2.Add(i0);

            //Item1 i1 = new Item1();
            //i1.Id = "2";
            //i1.Name = "已付款";
            //list2.Add(i1);

            //this.comboBox1.DataSource = list2;
            //this.comboBox1.DisplayMember = "name";
            //this.comboBox1.ValueMember = "id";
        }

        public void bingdshipping_status()
        {

            String sql = "select distinct t2.zhandianmingcheng  from t_zhandian t2";
            DataTable dtztd = SQLHelper.ExecuteDt(sql);

            String sql2 = "select  t2.zhandianmingcheng, t2.zitidianmingcheng,t2.dizhi,t2.xingming,t2.mobile from t_zhandian t2";
            DataTable ztdtd = SQLHelper.ExecuteDt(sql2);
            dic_zitidian = new Dictionary<String, Zitidian>();
            for (int i = 0; i < ztdtd.Rows.Count; i++)
            {
                Zitidian ztd = new Zitidian();
                ztd.Zhandianmc= ztdtd.Rows[i]["zhandianmingcheng"].ToString();
                ztd.Zitidianmc = ztdtd.Rows[i]["zitidianmingcheng"].ToString();
                ztd.Dizhi = ztdtd.Rows[i]["dizhi"].ToString();
                ztd.Xm = ztdtd.Rows[i]["xingming"].ToString();
                ztd.Mobile = ztdtd.Rows[i]["mobile"].ToString();
                dic_zitidian.Add(ztd.Zitidianmc,ztd);
            }
            this.comboBox1.DataSource = dtztd;
            this.comboBox1.DisplayMember = "zhandianmingcheng";//name为DataTable的字段名
 
            this.comboBox1.ValueMember = "zhandianmingcheng";//id为DataTable的字段名(对于隐藏对个数据，把数据放到一个字段用逗号隔开)

            //绑定数据源
            
  
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
            List<Fahuodan> ListOfT = new List<Fahuodan>();
               
            var result = from s in dt01.AsEnumerable()
                         group s by new
                         {
                             stores_name = s.Field<String>("stores_name")
                         } into goods
                         select new { StoreName = goods.Key.stores_name, Goods = goods };
            foreach (var userInfo in result)
            {
                var goods = from g in userInfo.Goods.ToList()
                           select new { GoodName = g.Field<String>("goods_name") , Goodnumber = g.Field<Double>("sum(t2.goods_number)"), GoodPrice = g.Field<Double>("sum(t2.goods_price)"), stores_name = g.Field<String>("stores_name") };
                List<Fahuodan> l1 = new List<Fahuodan>();
                Zitidian fhd_print = null;
                foreach (var good in goods)
                {
                    Fahuodan fhd = new Fahuodan();
                    String p1 = good.GoodName;
                    String p2 = Convert.ToString(good.Goodnumber) ;
                    String p4 = Convert.ToString(good.GoodPrice);
                    String p3 = good.stores_name;
                    fhd.Goods_name = p1;
                    fhd.Goods_number = p2;
                    fhd.Stores_name = p3;
                    fhd.Goods_price = p4;
                    fhd_print =dic_zitidian[p3];
                    fhd_print.Datetime = DateTime.Now;
                    l1.Add(fhd);
                }

                do_print_chinapost(fhd_print, l1);
            }
            //DataTableToExcel.DataToExcel(dt01);
            //dic_zitidian.g
            //do_print_chinapost();
        }


        public void do_print_chinapost(Zitidian ztd, List<Fahuodan> goods)
        {
            Report report = new Report();
            //获得模板的路径
            string reportLabel = Application.StartupPath + "\\fahuodan.frx";
            //加载报表模板
            report.Load(reportLabel);
              
            var textObject = report.FindObject("Text7") as TextObject;
            textObject.Text = ztd.Xm;

            TextObject textObject8 = report.FindObject("Text8") as TextObject;
            textObject8.Text = ztd.Mobile;

            TextObject textObject9 = report.FindObject("Text9") as TextObject;
            textObject9.Text = ztd.Datetime.ToString();


            TextObject textObject10 = report.FindObject("Text10") as TextObject;
            textObject10.Text = ztd.Zhandianmc+"-"+ ztd.Zitidianmc;

            TextObject textObject11 = report.FindObject("Text11") as TextObject;
            textObject11.Text = ztd.Dizhi;

            TableObject table1 = report.FindObject("Table1") as TableObject;
            if (table1 != null)
            {
                 TableRow r1 = new TableRow();
                TableCell c1 = new TableCell();
                c1.Text = "品名";


                TableCell c3 = new TableCell();
                c3.Text = "总价";

                TableCell c2 = new TableCell();
                c2.Text = "数量";
 

                r1.AddChild(c1);
                r1.AddChild(c3);
                r1.AddChild(c2);
                table1.AddChild(r1);
                
                for (int i = 0; i < goods.Count; i++)
                {
                    Fahuodan good = goods[i];
                    TableRow row1 = new TableRow();
                    TableCell cell1 = new TableCell();
                    TableCell cell2 = new TableCell();
                    TableCell cell3 = new TableCell();
                    cell1.Border.Color = Color.Black;
                    cell1.Border.Lines = BorderLines.All;
                    cell2.Border.Color = Color.Black;
                    cell2.Border.Lines = BorderLines.All;
                    cell3.Border.Color = Color.Black;
                    cell3.Border.Lines = BorderLines.All;
                    cell1.Text = good.Goods_name;
                    cell1.AutoWidth = true;
                    cell2.Text = good.Goods_number;
                    cell3.Text = good.Goods_price;
                    row1.AddChild(cell1);
                    row1.AddChild(cell3);
                    row1.AddChild(cell2);
                    table1.AddChild(row1);
                }
                TableRow r2 = new TableRow();
                //TableCell cz1 = new TableCell();
                //cz1.Text = "总计";
                //TableCell cz2 = new TableCell();
                //TableCell cz3 = new TableCell();
                //TableCell cz4 = new TableCell();
                //r2.AddChild(cz1);
                //r2.AddChild(cz2);
                //r2.AddChild(cz3);
                //r2.AddChild(cz4);
                table1.AddChild(r2);
                
            }

            //默认不显示打印机选择页面
            report.PrintSettings.ShowDialog = false;
             

            report.Print();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (dt01 !=null) {
                DataTableToExcel.DataToExcel(dt01);
                // ExportToExcel();
                //DataGridViewToExcel(this.dataGridView1);
                //DataToExcel(this.dataGridView1);
            }
           
        }

        public void DataToExcel(DataGridView m_DataView)

        {

            SaveFileDialog kk = new SaveFileDialog();

            kk.Title = "保存EXECL文件";

            kk.Filter = "EXECL文件(*.xls) |*.xls |所有文件(*.*) |*.*";

            kk.FilterIndex = 1;

            if (kk.ShowDialog() == DialogResult.OK)

            {

                string FileName = kk.FileName + ".xls";

                if (File.Exists(FileName))

                    File.Delete(FileName);

                FileStream objFileStream;

                StreamWriter objStreamWriter;

                string strLine = "";

                objFileStream = new FileStream(FileName, FileMode.OpenOrCreate, FileAccess.Write);

                objStreamWriter = new StreamWriter(objFileStream, System.Text.Encoding.Unicode);

                for (int i = 0; i < m_DataView.Columns.Count; i++)

                {

                    if (m_DataView.Columns[i].Visible == true)

                    {

                        strLine = strLine + m_DataView.Columns[i].HeaderText.ToString() + Convert.ToChar(9);

                    }

                }

                objStreamWriter.WriteLine(strLine);

                strLine = "";



                for (int i = 0; i < m_DataView.Rows.Count; i++)

                {

                    if (m_DataView.Columns[0].Visible == true)

                    {

                        if (m_DataView.Rows[i].Cells[0].Value == null)

                            strLine = strLine + " " + Convert.ToChar(9);

                        else

                            strLine = strLine + m_DataView.Rows[i].Cells[0].Value.ToString() + Convert.ToChar(9);

                    }

                    for (int j = 1; j < m_DataView.Columns.Count; j++)

                    {

                        if (m_DataView.Columns[j].Visible == true)

                        {

                            if (m_DataView.Rows[i].Cells[j].Value == null)

                                strLine = strLine + " " + Convert.ToChar(9);

                            else

                            {

                                string rowstr = "";

                                rowstr = m_DataView.Rows[i].Cells[j].Value.ToString();

                                if (rowstr.IndexOf("\r\n") > 0)

                                    rowstr = rowstr.Replace("\r\n", " ");

                                if (rowstr.IndexOf("\t") > 0)

                                    rowstr = rowstr.Replace("\t", " ");

                                strLine = strLine + rowstr + Convert.ToChar(9);

                            }

                        }

                    }

                    objStreamWriter.WriteLine(strLine);

                    strLine = "";

                }

                objStreamWriter.Close();

                objFileStream.Close();

                MessageBox.Show(this, "保存EXCEL成功", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }

        }

        private void DataGridViewToExcel(DataGridView dgv)
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Filter = "Execl files (*.xls)|*.xls";
            dlg.FilterIndex = 0;
            dlg.RestoreDirectory = true;
            dlg.CreatePrompt = true;
            dlg.Title = "保存为Excel文件";

            if (dlg.ShowDialog() == DialogResult.OK)
            {
                Stream myStream;
                myStream = dlg.OpenFile();
                StreamWriter sw = new StreamWriter(myStream, System.Text.Encoding.GetEncoding(-0));
                string columnTitle = "";
                try
                {
                    //写入列标题     
                    for (int i = 0; i < dgv.ColumnCount; i++)
                    {
                        if (i > 0)
                        {
                            columnTitle += "/t";
                        }
                        columnTitle += dgv.Columns[i].HeaderText;
                    }
                    sw.WriteLine(columnTitle);

                    //写入列内容     
                    for (int j = 0; j < dgv.Rows.Count; j++)
                    {
                        string columnValue = "";
                        for (int k = 0; k < dgv.Columns.Count; k++)
                        {
                            if (k > 0)
                            {
                                columnValue += "/t";
                            }
                            if (dgv.Rows[j].Cells[k].Value == null)
                                columnValue += "";
                            else
                                columnValue += dgv.Rows[j].Cells[k].Value.ToString().Trim();
                        }
                        sw.WriteLine(columnValue);
                    }
                    sw.Close();
                    myStream.Close();
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.ToString());
                }
                finally
                {
                    sw.Close();
                    myStream.Close();
                }
            }
        }

        private void ExportToExcel()
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Execl files (*.xls)|*.xls";
            saveFileDialog.FilterIndex = 0;
            saveFileDialog.RestoreDirectory = true;
            saveFileDialog.CreatePrompt = true;
            saveFileDialog.Title = "导出到Excel";
            saveFileDialog.ShowDialog();
            if (saveFileDialog.FileName == "")
            {
                return;
            }
            Stream myStream = saveFileDialog.OpenFile();
            StreamWriter sw = new StreamWriter(myStream, System.Text.Encoding.GetEncoding(-0));
            string str = "";
            try
            {
                for (int i = 0; i < dataGridView1.ColumnCount; i++)
                {
                    //if (dataGridView1.Columns[i].Visible == false || dataGridView1.Columns[i].DataPropertyName == "")
                    //{
                    //    continue;
                    //}
                    str += dataGridView1.Columns[i].HeaderText;
                    str += "\t";
                }
                sw.WriteLine(str);
                for (int j = 0; j < dataGridView1.Rows.Count - 1; j++)
                {
                    string strTemp = "";
                    for (int k = 0; k < dataGridView1.Columns.Count; k++)
                    {
                        if (dataGridView1.Columns[k].Visible == false || dataGridView1.Columns[k].DataPropertyName == "")
                        {
                            continue;
                        }
                        object obj = dataGridView1.Rows[j].Cells[k].Value;
                        if (obj != null)
                        {
                            strTemp += dataGridView1.Rows[j].Cells[k].Value.ToString();
                        }
                        else
                        {
                            strTemp = "";
                        }
                        strTemp += "\t";
                    }
                    sw.WriteLine(strTemp);
                }
                sw.Close();
                myStream.Close();
                MessageBox.Show("成功导出到Excel文件：\n" + saveFileDialog.FileName);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                sw.Close();
                myStream.Close();
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            String zdmc = this.textBox1.Text;
            String ztdmc = this.textBox2.Text;
            String ztdbh = this.textBox3.Text;
            String diqu = this.textBox4.Text;
            String dizhi = this.textBox5.Text;
            String xm = this.textBox6.Text;
            String shoujihao = this.textBox7.Text;

            if ("".Equals(zdmc) || "".Equals(ztdmc) || "".Equals(ztdbh) || "".Equals(diqu) || "".Equals(dizhi) || "".Equals(xm) || "".Equals(shoujihao))
            {
                MessageBox.Show("站点信息不完整");
                return;
            }
            else {
                String sql = "insert into t_zhandian(zhandianmingcheng,zitidianmingcheng,zid,diqu,dizhi,xingming,mobile)values('" + zdmc + "','" + ztdmc + "','" + ztdbh + "','" + diqu + "','" + dizhi + "','" + xm + "','" + shoujihao + "')";
                SQLHelper.updateCgd(sql);
                this.textBox1.Text = null;
                this.textBox2.Text = null;
                this.textBox3.Text = null;
                this.textBox4.Text = null;
                this.textBox5.Text = null;
                this.textBox6.Text = null;
                this.textBox7.Text = null;
                bindZitidian();
            }
        }
    }
}
