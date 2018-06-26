using Adapt.Database;
using System;
using System.Data;
using System.Data.Common;

namespace ProjectCollection.DAL
{
    /// <summary>
    /// 用户信息表操作
    /// </summary>
    public class UserInfo : BaseDatabase
    {
        #region 查询语句

        private const string SELECT_01 = "select * from user_info ";
        private const string SELECT_02 = SELECT_01 + " where login_name=@LoginName and password=@Password";
        private const string SELECT_id = SELECT_01 + " where user_identity=@UserId";
        private const string SELECT_All = SELECT_01 + " order by real_name";
        private const string SELECT_03 = @"
select * from user_info 
inner join role_authority on user_info.role_identity=role_authority.role_identity
inner join user_authority on role_authority.authority_identity=user_authority.authority_identity
where user_info.user_identity=@userIdentity";
        private const string SELECT_04 = @"
select * from user_info 
where role_identity=@role_identity";
        //Id
        private const string SELECT_RealName = "select real_name from user_info where user_identity=@userIdentity";

        #endregion

        #region 方法

        /// <summary>
        /// 查找用户信息
        /// </summary>
        /// <param name="loginName">登录用户名</param>
        /// <param name="password">登录用户密码</param>
        /// <returns></returns>
        public DataTable Select(string loginName, string password)
        {
            DbParameter[] parameters = new DbParameter[2];
            parameters[0] = Manager.CreateParameter("LoginName", loginName);
            parameters[1] = Manager.CreateParameter("Password", password);
            DataTable table = this.SelectOperate.Select(UserInfo.SELECT_02, parameters);
            return table;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public DataTable Select(string UserId)
        {
            DbParameter[] parameters = new DbParameter[1];
            parameters[0] = Manager.CreateParameter("UserId", UserId);
            DataTable table = this.SelectOperate.Select(UserInfo.SELECT_id, parameters);
            return table;
        }



        /// <summary>
        /// 查找用户权限
        /// </summary>
        /// <returns></returns>
        public DataTable SelectAuthority(Guid userIdentity)
        {
            DbParameter[] parameters = new DbParameter[1];
            parameters[0] = Manager.CreateParameter("userIdentity", userIdentity);
            DataTable table = this.SelectOperate.Select(UserInfo.SELECT_03, parameters);
            return table;
        }

        //List
        public DataTable SelectList()
        {
            //DbParameter[] parameters = new DbParameter[1];
            //parameters[0] = Manager.CreateParameter("userIdentity", userIdentity);
            DataTable table = this.SelectOperate.Select(UserInfo.SELECT_All);
            return table;
        }

        public DataTable SelectListByRole(Guid role_identity)
        {
            DbParameter[] parameters = new DbParameter[1];
            parameters[0] = Manager.CreateParameter("role_identity", role_identity);
            DataTable table = this.SelectOperate.Select(UserInfo.SELECT_04, parameters);
            return table;
        }
        //
        public string Select(Guid userIdentity)
        {
            DbParameter[] parameters = new DbParameter[1];
            parameters[0] = Manager.CreateParameter("userIdentity", userIdentity);
            DataTable table = this.SelectOperate.Select(UserInfo.SELECT_RealName, parameters);
            if (table != null && table.Rows.Count > 0)
            {
                string RealName = table.Rows[0][0].ToString();
                return RealName;
            }
            else
            {
                return "";
            }
        }

        #endregion
    }
}
