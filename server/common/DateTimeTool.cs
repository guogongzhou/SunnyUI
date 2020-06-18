//************************************************************************
//      author:     yuzhengyang
//      date:       2017.3.29 - 2017.8.17
//      desc:       日期时间工具
//      Copyright (c) yuzhengyang. All rights reserved.
//************************************************************************
using System;
using System.Collections.Generic;
using System.Text;

namespace caiwu.common
{
    public static class DateTimeTool
    {

        public static long getUtcTrick(DateTime time)
        {
            DateTimeOffset fs = new DateTimeOffset(time);
            long trick = (fs.UtcTicks - 621355968000000000) / 10000000;//秒值
            return trick;
        }

        public static long GetTimeStamp1(System.DateTime time)
        {
            DateTime DateStart = new DateTime(1970, 1, 1, 0, 0, 0);
            return  (Convert.ToInt32((time - DateStart).TotalSeconds));
        }

        public static DateTime TodayDate1(int n)
        {
            
          

            DateTime today = DateTime.Now.AddHours(-8); 
             DateTime result = new DateTime(today.Year, today.Month, today.Day, today.Hour- n, today.Minute- today.Minute, today.Second- today.Second);
            return result;

        }

        public static DateTime  ToDateTime(this long timestamp, bool millisecond = true, bool localTime = true)
        {
             
                int ms = millisecond ? 10000 : 10000000;
                var dt = new DateTime(Const.TiksUtc1970 + timestamp * ms, DateTimeKind.Utc);
                if (localTime)
                    dt.ToLocalTime();
                return dt;
            
        }

        public static long ConvertToTimestamp(DateTime value)
        {
            long epoch = (value.ToUniversalTime().Ticks - 621355968000000000) / 10000000;
            return epoch;
        }
        public static DateTime TodayDate2()
        {
            DateTime today = DateTime.Now.AddHours(-8);
            DateTime result = new DateTime(today.Year, today.Month, today.Day, today.Hour - today.Hour, today.Minute - today.Minute, today.Second - today.Second);
            return result;
        }
        public static DateTime TodayDate_UTC(int n)
        {
            DateTime today = DateTime.UtcNow;
            DateTime result = new DateTime(today.Year, today.Month, today.Day,today.Hour - n, today.Minute, today.Second);
            return result;
        }

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
        public static long ConvertDateTimeToInt(DateTime dateTime )
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
    }
    public class Const
    {
        /// <summary>
        /// 日期时间格式化
        /// </summary>
        public const string DateTimeFormatString = "yyyy-MM-dd HH:mm:ss";

        /// <summary>
        /// 日期时间格式化
        /// </summary>
        public const string DateHmFormatString = "yyyy-MM-dd HH:mm";

        /// <summary>
        /// 日期格式化
        /// </summary>
        public const string DateFormatString = "yyyy-MM-dd";

        /// <summary>
        /// utc 1601-1-1 到 utc 1970-1-1 的 Ticks
        /// </summary>
        public const long TiksUtc1970 = 621355968000000000;
    }
}
