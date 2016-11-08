using Adapt.Database;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;

namespace ProjectCollection.DAL
{
    /// <summary>
    /// 权限表操作
    /// </summary>
    public class UserAuthority : BaseDatabase
    {
        #region 查询语句

        private const string SELECT_01 = "select * from user_authority ";
        private const string SELECT_02 = SELECT_01 + " where url=@url and authority=@authority";

        #endregion

        #region 方法

        /// <summary>
        ///
        /// </summary>
        /// <param name="url"></param>
        /// <param name="authority"></param>
        /// <returns></returns>
        public DataTable Select(string url, string authority)
        {
            DbParameter[] parameters = new DbParameter[2];
            parameters[0] = Manager.CreateParameter("url", url);
            parameters[1] = Manager.CreateParameter("authority", authority);
            DataTable table = this.SelectOperate.Select(UserAuthority.SELECT_02, parameters);
            return table;
        }
        //
        public DataTable SelectAll()
        {
          
            DataTable table = this.SelectOperate.Select(UserAuthority.SELECT_01);
            return table;
        }
        #endregion
    }
}
