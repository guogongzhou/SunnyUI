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
using Newtonsoft.Json;
using Sunisoft.IrisSkin;
using winformUI.common;

namespace caiwu
{
    public partial class Form1 : Form
    {
        private SkinEngine skinEngine1;
        public static Rootobject rootobject = null;
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

            String rep_str = HtmlParser.HttpGet("https://www.feifeigo.cn/api.php?app_key=A20DE5F4-BEA5-4E43-A5DC-AC173661F372&method=dsc.order.list.get&");
            rootobject = JsonConvert.DeserializeObject<Rootobject>(rep_str);
            MessageBox.Show(rootobject.msg);
            MessageBox.Show(rootobject.info.list[1].address);
            this.dataGridView1.DataSource = rootobject.info.list;
        }
    }
}
