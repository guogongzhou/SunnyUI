using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace caiwu.common
{
    class DataTableGroupBy
    {
        public static DataTable groupBy(DataTable dt)
        {
            DataTable vtblCount = new DataTable();
             var query =
               from q in dt.AsEnumerable() 
               group q by q.Field<string>("商品名称") into r
               select new
               {
                   _qMachType = r.Key,
                   _qCount = r.Count()
               };

            using (vtblCount)
            {
                DataRow vNewRow = null;
                vtblCount.Columns.Add("商品名称", typeof(string));
                vtblCount.Columns.Add("备货量", typeof(int));
                foreach (var vq in query)
                {
                        vNewRow = vtblCount.NewRow();
                        vNewRow["商品名称"] = vq._qMachType;
                        vNewRow["备货量"] = vq._qCount;
                        vtblCount.Rows.Add(vNewRow); 
                }
                
            }
            return vtblCount;
        }

 

    }
}
