using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using ProjectCollection.DAL;

namespace ProjectCollection.BLL
{
    class DateHelper
    {
        public static bool IsWorkDay(DateTime dt)
        {
            DAL.EntityDataModel.ProjectCollectionEntities DBEntities = new DAL.EntityDataModel.ProjectCollectionEntities();
            var query = from date in DBEntities.WorkScheduals
                        select date;
            //先从日期表中，查找不是上班时间，如果不是直接返回 false ，如果是，直接返回 true。
            //如果在日期表中，找不到，则查找定义的日历，依据日历定义的周末时间来定义是否为工作日。
            //获取日历中不上班的标准周末时间,判断是不是上班时间
            if (dt.DayOfWeek == DayOfWeek.Sunday || dt.DayOfWeek == DayOfWeek.Saturday)
                return false;
            else
                return true;
        }
    }
}
