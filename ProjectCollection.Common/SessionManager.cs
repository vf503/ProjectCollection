using System;
using System.Web;
using System.Web.SessionState;

namespace ProjectCollection.Common
{
    /// <summary>
    /// 会话管理
    /// </summary>
    public class SessionManager
    {
        private static HttpSessionState session = HttpContext.Current.Session;
        public const string key_userInfo = "key_userInfo";

        public static HttpSessionState Session { get { return session; } }

        /// <summary>
        /// 获取session中的登录用户信息
        /// </summary>
        public static object LoginUserInfo
        {
            get
            {
                object value = SessionManager.session[SessionManager.key_userInfo];
                if (value == null)
                    throw new Exception("没有登录用户信息");
                return value;
            }
        }
    }
}
