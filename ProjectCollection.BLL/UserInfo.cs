using Adapt.Attribute;
using ProjectCollection.Common;
using System;
using System.Collections.Generic;
using System.Data;

namespace ProjectCollection.BLL
{
    public class UserInfo : BaseLogic
    {

        /// <summary>
        /// 用户可访问页面
        /// </summary>
        private List<string> browsablePages;
        private List<string> authority;
        #region 属性

        [TableAttribute.Column("user_identity")]
        public Guid Identity { get; set; }

        [TableAttribute.Column("login_name")]
        public string LoginName { get; set; }

        [TableAttribute.Column("password")]
        public string Password { get; set; }

        [TableAttribute.Column("real_name")]
        public string RealName { get; set; }

        [TableAttribute.Column("Email_address")]
        public string EMailAddress { get; set; }

        [TableAttribute.Column("role_identity")]
        public Guid role_identity { get; set; }

        /// <summary>
        /// 用户可以浏览的网页权限
        /// </summary>
        public List<string> BrowsablePages
        {
            get
            {
                if (this.browsablePages == null)
                    this.browsablePages = UserInfo.GetBrowsablePages(this.Identity);
                return this.browsablePages;
            }
        }

        //public static UserInfo LoginUserInfo { get { return (UserInfo)SessionManager.LoginUserInfo; } }

        //Add
        public List<string> Authority
        {
            get
            {
                if (this.authority == null)
                    this.authority = UserInfo.GetAuthority(this.Identity);
                return this.authority;
            }
        }


        #endregion

        #region 静态方法

        /// <summary>
        /// 用户登录验证
        /// </summary>
        /// <param name="loginName"></param>
        /// <param name="password"></param>
        /// <param name="userInfo"></param>
        /// <returns></returns>
        //public static bool Login(string loginName, string password, out UserInfo userInfo)
        //{
        //    DataTable table = new DAL.UserInfo().Select(loginName, password);
        //    userInfo = new UserInfo();
        //    foreach (DataRow row in table.Rows)
        //        Adapt.Convert.ConvertDataRowToObject(row, userInfo);
        //    if (userInfo.Identity == Guid.Empty)
        //        return false;
        //    else
        //    {
        //        SessionManager.Session.Add(SessionManager.key_userInfo, userInfo);
        //        return true;
        //    }
        //}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userIdentity"></param>
        /// <returns></returns>
        public static List<string> GetBrowsablePages(Guid userIdentity)
        {
            List<string> value = new List<string>();
            DataTable table = new DAL.UserInfo().SelectAuthority(userIdentity);
            foreach (DataRow row in table.Rows)
                value.Add(row["url"].ToString());
            return value;
        }

        //
        public static List<string> GetAuthority(Guid userIdentity)
        {
            List<string> value = new List<string>();
            DataTable table = new DAL.UserInfo().SelectAuthority(userIdentity);
            foreach (DataRow row in table.Rows)
                value.Add(row["caption"].ToString());
            return value;
        }


        //Login
        public static UserInfo LoginInfo(string loginName, string password)
        {
            DataTable table = new DAL.UserInfo().Select(loginName, password);
            UserInfo userInfo = new UserInfo();
            foreach (DataRow row in table.Rows)
                Adapt.Convert.ConvertDataRowToObject(row, userInfo);
            return userInfo;
        }
        ///用户列表
        public static DataTable GetDataByID(bool hasBlankItem = false)
        {
            DataTable dt = new DAL.UserInfo().SelectList();
            if (hasBlankItem)
            {
                DataRow dr = dt.NewRow();
                dr["real_name"] = string.Empty;
                dr["userIdentity"] = Guid.Empty;
                dt.Rows.InsertAt(dr, 0);
            }
            return dt;
        }

        public static DataTable GetDataByRole(Guid RoleID)
        {
            DataTable dt = new DAL.UserInfo().SelectListByRole(RoleID);
            return dt;
        }
        //ID
        public static string GetRealNameByID(Guid Id)
        {
            string RealName = new DAL.UserInfo().Select(Id);
            return RealName;
        }

        //
        public static UserInfo GetUserById(string UserId)
        {
            DataTable table = new DAL.UserInfo().Select(UserId);
            UserInfo userInfo = new UserInfo();
            foreach (DataRow row in table.Rows)
                Adapt.Convert.ConvertDataRowToObject(row, userInfo);
            return userInfo;
        }

        #endregion
    }
}
