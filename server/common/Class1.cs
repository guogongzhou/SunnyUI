using caiwu.common2;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using winformUI.common;

namespace caiwu.common
{
    class Class1
    {
        public static Dictionary<string, string> zdDictionary = new Dictionary<string, string>();
        public static void initData() {
            zdDictionary.Add("小郭便利店", "广源商贸");
            zdDictionary.Add("世纪华联超市", "广源商贸");
            zdDictionary.Add("烟酒超市(巴黎春天一期西门)", "广源商贸");
            zdDictionary.Add("广源批发部", "广源商贸");
            zdDictionary.Add("喜洋洋超市", "广源商贸");
            zdDictionary.Add("信达丰泰便利店", "广源商贸");
            zdDictionary.Add("芙蓉兴盛（信达格兰云天东门）", "广源商贸");
            zdDictionary.Add("惠佳超市", "广源商贸");
            zdDictionary.Add("一诺烟酒", "广源商贸");
            zdDictionary.Add("景群百货（菜鸟驿站）", "广源商贸");
            zdDictionary.Add("升阳商贸", "广源商贸");
            zdDictionary.Add("聚信烟酒茶商行", "广源商贸");
            zdDictionary.Add("东湖烟酒超市", "广源商贸");
            zdDictionary.Add("如山烟酒商行", "广源商贸");
            zdDictionary.Add("六姐便利店", "广源商贸");
            zdDictionary.Add("七天批发部", "广源商贸");
            zdDictionary.Add("永兴便利店", "广源商贸");
            zdDictionary.Add("茅台醇酒专卖", "广源商贸");
            zdDictionary.Add("百货店（菜鸟驿站）", "广源商贸");
            zdDictionary.Add("惠子便利店", "广源商贸");
            zdDictionary.Add("惠您烟酒商店", "广源商贸");
            zdDictionary.Add("上海中联超市（光盛豪庭）", "广源商贸");
            zdDictionary.Add("诚价超市", "广源商贸");
            zdDictionary.Add("华成商店", "广源商贸");
            zdDictionary.Add("芙蓉兴盛（方桥新镇）", "广源商贸");
            zdDictionary.Add("海星超市", "广源商贸");
            zdDictionary.Add("汇鑫超市（星河港湾小区）", "广源商贸");
            zdDictionary.Add("臻奇迹美容会所", "广源商贸");
            zdDictionary.Add("原创烟酒商贸", "广源商贸");
            zdDictionary.Add("树熊诚品驿站", "广源商贸");
            zdDictionary.Add("子木烟酒", "广源商贸");
            zdDictionary.Add("岱山湖超市", "合裕路站");
            zdDictionary.Add("鑫优品超市", "合裕路站");
            zdDictionary.Add("左哥烟酒", "合裕路站");
            zdDictionary.Add("正隆烟酒", "合裕路站");
            zdDictionary.Add("烟酒茶行（文一名门学府）", "合裕路站");
            zdDictionary.Add("悦阳烟酒", "合裕路站");
            zdDictionary.Add("久昌烟酒", "合裕路站");
            zdDictionary.Add("优佳生活超市", "合裕路站");
            zdDictionary.Add("一烟酒鼎超市", "合裕路站");
            zdDictionary.Add("桐徽超市", "倩勇商贸");
            zdDictionary.Add("国权烟酒", "倩勇商贸");
            zdDictionary.Add("老陈烟酒店", "倩勇商贸");
            zdDictionary.Add("可多超市", "倩勇商贸");
            zdDictionary.Add("三姐烟酒超市", "倩勇商贸");
            zdDictionary.Add("友嘉便利店", "可可商贸");
            zdDictionary.Add("鑫金鼎烟酒茶经营部", "可可商贸");
            zdDictionary.Add("顺祥宾馆超市", "可可商贸");
        }

        public static DataTable JsonToDataTable2(JArray array)
        {
            DataTable table = new DataTable();
             
            if (array.Count > 0)
            {
                table.Columns.Add("订单号", System.Type.GetType("System.String"));
                table.Columns.Add("收货人名称", System.Type.GetType("System.String"));
                table.Columns.Add("站点名称", System.Type.GetType("System.String"));
                table.Columns.Add("自提点名称", System.Type.GetType("System.String"));
                //table.Columns.Add("省份", System.Type.GetType("System.String"));
                //table.Columns.Add("市级", System.Type.GetType("System.String"));
                //table.Columns.Add("区县", System.Type.GetType("System.String"));
                //table.Columns.Add("街道乡镇", System.Type.GetType("System.String"));
                table.Columns.Add("订单添加时间", System.Type.GetType("System.String"));
                table.Columns.Add("收货详细地址", System.Type.GetType("System.String"));
                table.Columns.Add("联系手机号码", System.Type.GetType("System.String"));

                table.Columns.Add("订单附言", System.Type.GetType("System.String"));
                table.Columns.Add("已付款金额", System.Type.GetType("System.String"));
                table.Columns.Add("商品名称", System.Type.GetType("System.String"));
                table.Columns.Add("商品ID", System.Type.GetType("System.String"));
                table.Columns.Add("商品数量", System.Type.GetType("System.String"));
                table.Columns.Add("商品价格", System.Type.GetType("System.String"));



                for (int i = 0; i < array.Count; i++)
                {
                    OrderInfo orderInfo = null;
                    DataRow row = table.NewRow();
                    JObject obj = array[i] as JObject;
                    foreach (JToken jkon in obj.AsEnumerable<JToken>())
                    {
                        string name = ((JProperty)(jkon)).Name;
                        string value = ((JProperty)(jkon)).Value.ToString();
                        if ("add_time".Equals(name))
                        {
                            row["订单添加时间"] = DateTimeTool.ConvertStringToDateTime(value);
                        }
                        if ("order_id".Equals(name))
                        {
                            row["订单号"] = value;
                            String rep_str2 = HtmlParser.HttpGet("https://www.feifeigo.cn/api.php?app_key=A20DE5F4-BEA5-4E43-A5DC-AC173661F372&method=dsc.order.goods.list.get&order_id="+ value);
                            orderInfo = JsonConvert.DeserializeObject<OrderInfo>(rep_str2);
                            for (int j = 0; j < orderInfo.info.list.Length; j++)
                            {
                                DataRow row1 = table.NewRow();
                                row1["商品名称"] = orderInfo.info.list[j].goods_name;
                                row1["商品数量"] = "" + orderInfo.info.list[j].goods_number;
                                row1["商品价格"] = "" + orderInfo.info.list[j].goods_price;
                                row1["商品ID"] = "" + orderInfo.info.list[j].goods_id;
                                
                                table.Rows.Add(row1);
                            }
                            

                        }
                        if ("consignee".Equals(name))
                        {
                            row["收货人名称"] = value;
                        }
                        if ("stores_name".Equals(name))
                        {
                            row["自提点名称"] = value;
                            if ( !"".Equals(value.Trim()) || null != value) { 
                                 
                                row["站点名称"] = Class1.zdDictionary.ContainsKey(value)? Class1.zdDictionary[value] :"";
                            }
                        }


                        //if ("province".Equals(name))
                        //{
                        //    row["省份"] = value;
                        //}
                        //if ("city".Equals(name))
                        //{
                        //    row["市级"] = value;
                        //}
                        //if ("district".Equals(name))
                        //{
                        //    row["区县"] = value;
                        //}
                        //if ("street".Equals(name))
                        //{
                        //    row["街道乡镇"] = value;
                        //}
                        if ("address".Equals(name))
                        {
                            row["收货详细地址"] = value;
                        }
                        if ("mobile".Equals(name))
                        {
                            row["联系手机号码"] = value;
                        }
                        if ("postscript".Equals(name))
                        {
                            row["订单附言"] = value;
                        }
                        if ("money_paid".Equals(name))
                        {
                            row["已付款金额"] = value;
                        }
                        if ("goods_name".Equals(name))
                        {
                            row["商品名称"] = value;
                        }
                        if ("goods_number".Equals(name))
                        {
                            row["商品数量"] = value;
                        }
                        if ("goods_price".Equals(name))
                        {
                            row["商品价格"] = value;
                        }
                    }
                    table.Rows.Add(row);
                    
                }
            }
            return table;
        }
    }
}
