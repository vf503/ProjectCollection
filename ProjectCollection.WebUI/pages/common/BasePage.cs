using ProjectCollection.BLL;
using ProjectCollection.Common;
using System;

namespace ProjectCollection.WebUI.pages.common
{
    public class BasePage : Adapt.WebUI.Page
    {
        /// <summary>
        /// 获取登录用户信息
        /// </summary>
        //public UserInfo LoginUserInfo { get { return (UserInfo)SessionManager.LoginUserInfo; } }

        public const string key_userInfo = "key_userInfo";

        public UserInfo LoginUserInfo
        {
            get
            {
                UserInfo value = (UserInfo)Session[key_userInfo];
                if (value == null)
                {
                    //throw new Exception("没有登录用户信息");
                    this.Redirect("~/pages/login.aspx");
                }
                return value;
            }
        }

        protected override void OnLoad(System.EventArgs e)
        {
            base.OnLoad(e);

            // 验证是否有权限访问
            //if (!UserAuthority.CheckAuthority(this.Request.Path))
            //    throw new Exception("没有浏览权限");
        }
    }
}