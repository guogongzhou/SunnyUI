using caiwu.common2;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using winformUI.common;

namespace caiwu.common
{
    class Class1
    {

        public static DataTable JsonToDataTable2(string json)
        {
            DataTable table = new DataTable();
            //JsonStr为Json字符串
            JArray array = JsonConvert.DeserializeObject(json) as JArray;//反序列化为数组
            if (array.Count > 0)
            {
                table.Columns.Add("订单号", System.Type.GetType("System.String"));
                table.Columns.Add("收货人名称", System.Type.GetType("System.String"));
                table.Columns.Add("省份", System.Type.GetType("System.String"));
                table.Columns.Add("市级", System.Type.GetType("System.String"));
                table.Columns.Add("区县", System.Type.GetType("System.String"));
                table.Columns.Add("街道乡镇", System.Type.GetType("System.String"));
                table.Columns.Add("收货详细地址", System.Type.GetType("System.String"));
                table.Columns.Add("联系手机号码", System.Type.GetType("System.String"));

                table.Columns.Add("订单附言", System.Type.GetType("System.String"));
                table.Columns.Add("已付款金额", System.Type.GetType("System.String"));
                table.Columns.Add("商品名称", System.Type.GetType("System.String"));
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
                                table.Rows.Add(row1);
                            }
                            

                        }
                        if ("consignee".Equals(name))
                        {
                            row["收货人名称"] = value;
                        }
                        if ("province".Equals(name))
                        {
                            row["省份"] = value;
                        }
                        if ("city".Equals(name))
                        {
                            row["市级"] = value;
                        }
                        if ("district".Equals(name))
                        {
                            row["区县"] = value;
                        }
                        if ("street".Equals(name))
                        {
                            row["街道乡镇"] = value;
                        }
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
