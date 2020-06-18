using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace caiwu.common
{
    class DateTimeTool
    {
        public static DateTime TodayDate()
        {
            DateTime today = DateTime.Now;
            DateTime result = new DateTime(today.Year, today.Month, today.Day);
            return result;
        }
        public static DateTime TodayDate(DateTime today)
        {
            DateTime result = new DateTime(today.Year, today.Month, today.Day);
            return result;
        }
        public static TimeSpan TimeSpan(DateTime dt1, DateTime dt2)
        {
            if (dt1 > dt2)
                return dt1 - dt2;
            else
                return dt2 - dt1;
        }
        public static Tuple<int, int> ToMS(double second)
        {
            int Minute = 0, Second = 0;
            Minute = (int)second / 60;
            Second = (int)second % 60;
            return new Tuple<int, int>(Minute, Second);
        }
        public static Tuple<long, long, long> ToHMS(double second)
        {
            double Hour = second / 60 / 60;
            double Minute = second / 60 % 60;
            double Second = second % 60;
            return new Tuple<long, long, long>((long)Hour, (long)Minute, (long)Second);
        }
        public static Tuple<long, long, long> ToHMS(long ms)
        {
            long Hour = ms / 1000 / 60 / 60;
            long Minute = ms / 1000 / 60 % 60;
            long Second = ms / 1000 % 60;
            return new Tuple<long, long, long>(Hour, Minute, Second);
        }
        public static Tuple<int, int, int> ToHMS(int ms)
        {
            int Hour = ms / 1000 / 60 / 60;
            int Minute = ms / 1000 / 60 % 60;
            int Second = ms / 1000 % 60;
            return new Tuple<int, int, int>(Hour, Minute, Second);
        }

        /// <summary>  
        /// 将c# DateTime时间格式转换为Unix时间戳格式  
        /// </summary>  
        /// <param name="time">时间</param>  
        /// <returns>long</returns>  
        public static long ConvertDateTimeToInt(DateTime dateTime)
        {
            var ts = dateTime.ToUniversalTime() - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return Convert.ToInt64(ts.TotalSeconds);
        }
        /// <summary>        
        /// 时间戳转为C#格式时间        
        /// </summary>        
        /// <param name=”timeStamp”></param>        
        /// <returns></returns>        
        public static DateTime ConvertStringToDateTime(string timestamp)
        {
            var tz = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1, 0, 0, 0, 0));
            return timestamp.Length == 13
                ? tz.AddMilliseconds(Convert.ToInt64(timestamp))
                : tz.AddSeconds(Convert.ToInt64(timestamp));
        }

        public static long ConvertDateTimeToLong(DateTime time)
        {
            DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1)); // 当地时区
            long timeStamp = (long)(time - startTime).TotalSeconds; // 相差毫秒数     
            return timeStamp;
        }
    }
}
