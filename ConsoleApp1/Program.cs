using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            DateTime now = DateTime.UtcNow;
            Console.WriteLine("标准UTC时间"+ now);
            DateTime today = new DateTime(now.Year, now.Month, now.Day);
            Console.WriteLine("UTC当天零点时间" + today);
            Console.WriteLine("UTC当天零点时间戳" + getUtcTrick(today));
            DateTime shanggexiaoshi = now.AddHours(-1).AddMinutes(-now.Minute).AddSeconds(-now.Second);
            Console.WriteLine("UTC上个小时时间" + shanggexiaoshi);
            Console.WriteLine("UTC上个小时时间戳" + (getUtcTrick(shanggexiaoshi) - 28800));
           
           
            Console.Read();
        }

        public static long getUtcTrick(DateTime time)
        {
            DateTimeOffset fs = new DateTimeOffset(time);
            long trick = (fs.UtcTicks - 621355968000000000) / 10000000;//秒值
            return trick;
        }
    }
}
