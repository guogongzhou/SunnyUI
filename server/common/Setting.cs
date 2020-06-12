using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace server.common
{
    internal class Setting
    {
        private static Setting uniqueInstance;

        private Setting()
        {
            ConnStr = SQLHelper.GetSqlConn();
        }

        internal static Setting Instance()
        {
            // 如果类的实例不存在则创建，否则直接返回
            if (uniqueInstance == null)
            {
                uniqueInstance = new Setting();
            }
            return uniqueInstance;
        }

        public static System.Windows.Forms.Form FormLogin;
        public static System.Windows.Forms.Form FormMain;


        public const string PROJECTNAEM = "工作台";



 
        public static string SqlIp { get; internal set; }
        public static string ConnStr { get; internal set; }



         
    }
}
