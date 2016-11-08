using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectCollection.Common
{
    public class DateTimeHandle
    {
        public static void Fun1()
        {
            //格式：2012-8-16 11:21:29
            Console.WriteLine("当前时间：{0}", DateTime.Now.ToString());

            //格式：2012-8-16 0:00:00
            Console.WriteLine("日期部分：{0}", DateTime.Now.Date.ToString());

            //格式：11:21:29
            Console.WriteLine("时间部分：{0}", DateTime.Now.ToLongTimeString());

            //获取此实例的当天的时间。相当的精确【到毫秒】
            Console.WriteLine("TimeOfDay:{0}", DateTime.Now.TimeOfDay.ToString());

            Console.WriteLine("取中文日期显示_年月日时分:{0}", DateTime.Now.ToString("f"));

            Console.WriteLine("取中文日期显示_年月:{0}", DateTime.Now.ToString("y"));

            Console.WriteLine("取中文日期显示_月日:{0}", DateTime.Now.ToString("m"));

            Console.WriteLine("取中文年月日:{0}", DateTime.Now.ToString("D"));

            //取当前时分，格式为：14：24
            Console.WriteLine("取当前时分:{0}", DateTime.Now.ToString("t"));

            //取当前时间，格式为：2003-09-23T14:46:48
            Console.WriteLine("取当前时分:{0}", DateTime.Now.ToString("s"));

            //取当前时间，格式为：2003-09-23 14:48:30Z
            Console.WriteLine("取当前时分:{0}", DateTime.Now.ToString("u"));

            //取当前时间，格式为：2003-09-23 14:48
            Console.WriteLine("取当前时分:{0}", DateTime.Now.ToString("g"));

            //取当前时间，格式为：Tue, 23 Sep 2003 14:52:40 GMT
            Console.WriteLine("取当前时分:{0}", DateTime.Now.ToString("r"));

            //获得当前时间 n 天后的日期时间
            DateTime newDay = DateTime.Now.AddDays(100);
            Console.WriteLine(newDay.ToString());

            Console.WriteLine("年：{0}", DateTime.Now.Year.ToString());
            Console.WriteLine("月：{0}", DateTime.Now.Month.ToString());
            Console.WriteLine("日：{0}", DateTime.Now.Day.ToString());
            Console.WriteLine("时：{0}", DateTime.Now.Hour.ToString());
            Console.WriteLine("分：{0}", DateTime.Now.Minute.ToString());
            Console.WriteLine("秒：{0}", DateTime.Now.Second.ToString());
            Console.WriteLine("毫秒：{0}", DateTime.Now.Millisecond.ToString());

            Console.WriteLine("计时周期数：{0}", DateTime.Now.Ticks.ToString());
            Console.WriteLine("星期：{0}", DateTime.Now.DayOfWeek.ToString());
            Console.WriteLine("一年中的第几天：{0}", DateTime.Now.DayOfYear.ToString());

        }

        //客户端代码
        public static void MyFun()
        {
            //struct本身是一个结构体
            //DateTime dt0 = new DateTime();

            DateTime dt1 = new DateTime(2012, 8, 14, 10, 54, 55);
            DateTime dt2 = new DateTime(2012, 12, 21);//2012-12-21 00:00:00
            Console.WriteLine(DateDiff(dt1, dt2));

            //我活了多少天了
            DateTime dt3 = new DateTime(2012, 8, 14, 12, 00, 00);
            DateTime dt4 = new DateTime(1990, 11, 17, 02, 48, 00);//2012-12-21 00:00:00
            Console.WriteLine("我活了多少天" + DateDiff(dt4, dt3));
        }

        //计算时间的差值
        public static string DateDiff(DateTime DateTime1, DateTime DateTime2)
        {
            string dateDiff = null;
            TimeSpan ts1 = new TimeSpan(DateTime1.Ticks);
            TimeSpan ts2 = new TimeSpan(DateTime2.Ticks);

            TimeSpan ts = ts1.Subtract(ts2).Duration();

            dateDiff = ts.Days.ToString() + "天" + ts.Hours.ToString() + "小时" + ts.Minutes.ToString() + "分钟" + ts.Seconds.ToString() + "秒";
            return dateDiff;

            #region note
            //C#中使用TimeSpan计算两个时间的差值
            //可以反加两个日期之间任何一个时间单位。
            //TimeSpan ts = Date1 - Date2;
            //double dDays = ts.TotalDays;//带小数的天数，比如1天12小时结果就是1.5 
            //int nDays = ts.Days;//整数天数，1天12小时或者1天20小时结果都是1  
            #endregion
        }
        public static string DateDiffDay(DateTime DateTime1, DateTime DateTime2)
        {
            string dateDiff = null;
            TimeSpan ts1 = new TimeSpan(DateTime1.Ticks);
            TimeSpan ts2 = new TimeSpan(DateTime2.Ticks);
            TimeSpan ts = ts1.Subtract(ts2).Duration();
            if (ts.Days.ToString() == "0")
            { dateDiff = "24小时内"; }
            else if (ts.Days > 600000) { dateDiff = "--"; }//无起始时间
            else { dateDiff = ts.Days.ToString() + "天"; }
            return dateDiff;
        }
        public static string DateDiffHour(DateTime DateTime1, DateTime DateTime2)
        {
            string dateDiff = null;
            TimeSpan ts1 = new TimeSpan(DateTime1.Ticks);
            TimeSpan ts2 = new TimeSpan(DateTime2.Ticks);
            TimeSpan ts = ts1.Subtract(ts2).Duration();
            if (ts.Days.ToString() == "0")
            {
                if (ts.Hours.ToString() == "0") { dateDiff = ts.Minutes.ToString() + "分钟"; }
                else
                {
                    dateDiff = ts.Hours.ToString() + "小时";
                }
            }
            else { dateDiff = ts.Days.ToString() + "天" + ts.Hours.ToString() + "小时"; }
            return dateDiff;
        }
        //超时
        public static string TimeOut(DateTime ContrastDate,DateTime StartingTime ,int DeadlineDays)
        {
            DateTime DeadlineDate = StartingTime.AddDays(DeadlineDays);
            //TimeSpan Deadline = new TimeSpan(DeadlineDays, 0, 0, 0);
            if (DateTime.Compare(ContrastDate, DeadlineDate) > 0)
            {
                return DateDiffHour(ContrastDate, DeadlineDate);
            }
            else { return "0"; }
        }

        //工作时
        public static TimeSpan GetWorkTimeSpan(DateTime dtStart, DateTime dtEnd, TimeSpan time_start, TimeSpan time_end, TimeSpan time_start2, TimeSpan time_end2)
        {
            if (dtStart.Date == dtEnd.Date) //如果是同一天
            {
                if (IsWorkDay(dtStart))
                    return GetTimeSpan(dtStart.TimeOfDay, dtEnd.TimeOfDay, time_start, time_end, time_start2, time_end2);
                else
                    return new TimeSpan(0);
            }
            //如果不是同一天 计算天数减去1 乘以标准时长 加上分别计算开始开数和结束天数
            double days = dtEnd.Date.Subtract(dtStart.Date).TotalDays - 1;

            TimeSpan startTimeSpan;
            if (IsWorkDay(dtStart))
                startTimeSpan = GetTimeSpan(dtStart.TimeOfDay, new TimeSpan(23, 59, 60), time_start, time_end, time_start2, time_end2);//开始天
            else
                startTimeSpan = new TimeSpan(0);

            TimeSpan endTimeSpan;
            if (IsWorkDay(dtEnd))
                endTimeSpan = GetTimeSpan(new TimeSpan(0, 0, 0), dtEnd.TimeOfDay, time_start, time_end, time_start2, time_end2);//结束天
            else
                endTimeSpan = new TimeSpan(0);

            TimeSpan totalTimeSpan = startTimeSpan + endTimeSpan; //总值

            TimeSpan preTimeSpan = GetTimeSpan(new TimeSpan(0, 0, 0), new TimeSpan(23, 59, 60), time_start, time_end, time_start2, time_end2);//开始天
            for (int i = 1; i <= days; i++)
            {
                if (IsWorkDay(dtStart.AddDays(i)))
                    totalTimeSpan += preTimeSpan; //添加间隔天
            }

            return totalTimeSpan;
        }

        /// <summary>
        /// 判断是否为工作日
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static bool IsWorkDay(DateTime dt)
        {
            //先从日期表中，查找不是上班时间，如果不是直接返回 false ，如果是，直接返回 true。
            //如果在日期表中，找不到，则查找定义的日历，依据日历定义的周末时间来定义是否为工作日。
            //获取日历中不上班的标准周末时间,判断是不是上班时间
            if (dt.DayOfWeek == DayOfWeek.Sunday || dt.DayOfWeek == DayOfWeek.Saturday)
                return false;
            else
                return true;
        }

        //同一天获取
        public static TimeSpan GetTimeSpan(TimeSpan tsStart, TimeSpan tsEnd, TimeSpan time_start, TimeSpan time_end, TimeSpan time_start2, TimeSpan time_end2)
        {

            //判断 开始时间
            if (tsStart < time_start)
            {
                //标准开始时间不变
                //start1 不变
                //start2 不变
            }
            else if (tsStart >= time_start && tsStart <= time_end)
            {
                //标准开始= dtStart
                time_start = tsStart;
                //start1 变
                //start2 不变
            }
            else if (tsStart > time_end && tsStart < time_start2)
            {
                time_start = time_end;
                //start1 变
                //start2 不变
            }
            else if (tsStart >= time_start2 && tsStart <= time_end2)
            {
                time_start = time_end;
                time_start2 = tsStart;
                //start1 变
                //start2 变
            }
            else if (tsStart > time_end2)
            {
                time_start = time_end;
                time_start2 = time_end2;
                //start1 变
                //start2 变
            }

            //判断 结束时间
            if (tsEnd < time_start)
            {
                //标准开始时间不变
                time_end = time_start;
                time_end2 = time_start2;
                //time_end 变
                //time_end2变
            }
            else if (tsEnd >= time_start && tsEnd <= time_end)
            {
                time_end = tsEnd;
                time_end2 = time_start2;
                //time_end 变
                //time_end2变
            }
            else if (tsEnd > time_end && tsEnd < time_start2)
            {
                time_end2 = time_start2;
                //time_end2 不变
                //time_end1变
            }
            else if (tsEnd >= time_start2 && tsEnd <= time_end2)
            {
                time_end2 = tsEnd;
                //time_end 不变
                //time_end2变
            }
            else if (tsEnd > time_end2)
            {
                //time_end 不变
                //time_end2不变
            }

            return (time_end - time_start) + (time_end2 - time_start2);
        }
    }
}
