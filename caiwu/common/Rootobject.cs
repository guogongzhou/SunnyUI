using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace caiwu.common
{
     
public class Rootobject
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
            public int order_id { get; set; }
            public int main_order_id { get; set; }
            public string order_sn { get; set; }
            public int user_id { get; set; }
            public int order_status { get; set; }
            public int shipping_status { get; set; }
            public int pay_status { get; set; }
            public string consignee { get; set; }
            public int country { get; set; }
            public int province { get; set; }
            public int city { get; set; }
            public int district { get; set; }
            public int street { get; set; }
            public string address { get; set; }
            public string zipcode { get; set; }
            public string tel { get; set; }
            public string mobile { get; set; }
            public string email { get; set; }
            public string best_time { get; set; }
            public string sign_building { get; set; }
            public string postscript { get; set; }
            public string shipping_id { get; set; }
            public string shipping_name { get; set; }
            public string shipping_code { get; set; }
            public string shipping_type { get; set; }
            public int pay_id { get; set; }
            public string pay_name { get; set; }
            public string how_oos { get; set; }
            public string how_surplus { get; set; }
            public string pack_name { get; set; }
            public string card_name { get; set; }
            public string card_message { get; set; }
            public string inv_payee { get; set; }
            public string inv_content { get; set; }
            public string goods_amount { get; set; }
            public string cost_amount { get; set; }
            public string shipping_fee { get; set; }
            public string insure_fee { get; set; }
            public string pay_fee { get; set; }
            public string pack_fee { get; set; }
            public string card_fee { get; set; }
            public string money_paid { get; set; }
            public string surplus { get; set; }
            public int integral { get; set; }
            public string integral_money { get; set; }
            public string bonus { get; set; }
            public string order_amount { get; set; }
            public string return_amount { get; set; }
            public int from_ad { get; set; }
            public string referer { get; set; }
            public int add_time { get; set; }
            public int confirm_time { get; set; }
            public int pay_time { get; set; }
            public int shipping_time { get; set; }
            public int confirm_take_time { get; set; }
            public int auto_delivery_time { get; set; }
            public int pack_id { get; set; }
            public int card_id { get; set; }
            public int bonus_id { get; set; }
            public string invoice_no { get; set; }
            public string extension_code { get; set; }
            public int extension_id { get; set; }
            public string to_buyer { get; set; }
            public string pay_note { get; set; }
            public int agency_id { get; set; }
            public string inv_type { get; set; }
            public string tax { get; set; }
            public int is_separate { get; set; }
            public int parent_id { get; set; }
            public string discount { get; set; }
            public string discount_all { get; set; }
            public int is_delete { get; set; }
            public int is_settlement { get; set; }
            public object sign_time { get; set; }
            public int is_single { get; set; }
            public int point_id { get; set; }
            public string shipping_dateStr { get; set; }
            public int supplier_id { get; set; }
            public string froms { get; set; }
            public string coupons { get; set; }
            public int uc_id { get; set; }
            public int is_zc_order { get; set; }
            public int zc_goods_id { get; set; }
            public int is_frozen { get; set; }
            public int drp_is_separate { get; set; }
            public int team_id { get; set; }
            public int team_parent_id { get; set; }
            public int team_user_id { get; set; }
            public string team_price { get; set; }
            public int chargeoff_status { get; set; }
            public int invoice_type { get; set; }
            public int vat_id { get; set; }
            public string tax_id { get; set; }
            public int is_update_sale { get; set; }
            public int ru_id { get; set; }
            public int main_count { get; set; }
            public string rel_name { get; set; }
            public string id_num { get; set; }
            public string rate_fee { get; set; }
            public int main_pay { get; set; }
            public int child_show { get; set; }
            public string store_start_time { get; set; }
            public string store_end_time { get; set; }
            public string selectTime { get; set; }
            public string country_name { get; set; }
            public string province_name { get; set; }
            public string city_name { get; set; }
            public string district_name { get; set; }
            public string street_name { get; set; }
            public string goods_name { get; set; }
            public string goods_number { get; set; }
            public string goods_price { get; set; }
    }
    
 
}
