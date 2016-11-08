using System.Collections.Generic;
using System.Data;

namespace ProjectCollection.BLL
{
    /// <summary>
    /// 权限操作
    /// </summary>
    public class UserAuthority : BaseLogic
    {
        #region 静态方法

        /// <summary>
        /// 检查权限
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        //public static bool CheckAuthority(string url)
        //{
        //    DAL.UserAuthority dalUserAuthority = new DAL.UserAuthority();
        //    // 检查访问路径是否为free权限
        //    DataTable dt = dalUserAuthority.Select(url, "free");
        //    if (dt.Rows.Count > 0)
        //        return true;

        //    // 检查登录用户信息
        //    if (UserInfo.LoginUserInfo.BrowsablePages.Contains(url))
        //        return true;

        //    return false;
        //}
        //
        public static List<string> GetAllAuthority()
        {
            List<string> value = new List<string>();
            DataTable table = new DAL.UserAuthority().SelectAll();
            foreach (DataRow row in table.Rows)
                value.Add(row["caption"].ToString());
            return value;
        }
        #endregion
    }
}
