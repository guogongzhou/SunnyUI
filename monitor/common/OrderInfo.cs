using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace caiwu.common2
{
     
    public class OrderInfo
    {
        public string result { get; set; }
        public int error { get; set; }
        public string msg { get; set; }
        public Info info { get; set; }
    }

    public class Info
    {
        public List[] list { get; set; }
        public Filter filter { get; set; }
        public int page_count { get; set; }
        public int record_count { get; set; }
    }

    public class Filter
    {
        public int page { get; set; }
        public int page_size { get; set; }
        public int record_count { get; set; }
        public int page_count { get; set; }
    }

    public class List
    {
        public int rec_id { get; set; }
        public int order_id { get; set; }
        public int user_id { get; set; }
        public string cart_recid { get; set; }
        public int goods_id { get; set; }
        public string goods_name { get; set; }
        public string goods_sn { get; set; }
        public int product_id { get; set; }
        public int goods_number { get; set; }
        public string market_price { get; set; }
        public string goods_price { get; set; }
        public string goods_attr { get; set; }
        public int send_number { get; set; }
        public int is_real { get; set; }
        public string extension_code { get; set; }
        public int parent_id { get; set; }
        public int is_gift { get; set; }
        public int model_attr { get; set; }
        public string goods_attr_id { get; set; }
        public int ru_id { get; set; }
        public string shopping_fee { get; set; }
        public int warehouse_id { get; set; }
        public int area_id { get; set; }
        public int area_city { get; set; }
        public int is_single { get; set; }
        public int freight { get; set; }
        public int tid { get; set; }
        public string shipping_fee { get; set; }
        public string drp_money { get; set; }
        public int is_distribution { get; set; }
        public string commission_rate { get; set; }
        public int stages_qishu { get; set; }
        public string product_sn { get; set; }
        public int is_reality { get; set; }
        public int is_return { get; set; }
        public int is_fast { get; set; }
        public string rate_price { get; set; }
        public string goods_bonus { get; set; }
        public string cost_price { get; set; }
        public string goods_coupons { get; set; }
        public int buy_drp_show { get; set; }
        public int membership_card_id { get; set; }
        public string membership_card_discount_price { get; set; }
        public int is_received { get; set; }
        public int main_count { get; set; }
        public int is_comment { get; set; }
        public string country_name { get; set; }
        public string province_name { get; set; }
        public string city_name { get; set; }
        public string district_name { get; set; }
        public string street_name { get; set; }
    }



}
