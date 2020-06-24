using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace caiwu.common
{
    public class Fahuodan
    {
        String goods_name;
        String goods_sn;
        int goods_number;
        decimal goods_price;
        String stores_name;

        public string Goods_name { get => goods_name; set => goods_name = value; }
        
        public string Stores_name { get => stores_name; set => stores_name = value; }
 
        public string Goods_sn { get => goods_sn; set => goods_sn = value; }
        public int Goods_number { get => goods_number; set => goods_number = value; }
        public decimal Goods_price { get => goods_price; set => goods_price = value; }
    }
}
