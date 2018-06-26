using System;
using System.Data;
using ProjectCollection.BLL;
using ProjectCollection.WebUI.pages.common;

namespace ProjectCollection.WebUI.pages
{
    public partial class LoginMain : BasePage
    {
        #region 事件

        /// <summary>
        /// 页面载入
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnLogin_Click(object sender, EventArgs e)
        {
            //判断用户身份进行跳转
            BLL.UserInfo userInfo;
            if (Login(this.txtLoginUserName.Text, this.txtLoginPassword.Text, out userInfo))
            {
                // 登录成功
                //if (LoginUserInfo.Authority.Contains("manage"))
                //{
                    this.Redirect("~/pages/index.aspx");
                //}
                //else
                //{
                    this.Redirect("~/pages/MyTask.aspx?mode=manufacture&range=now");
                //}
            }
            else
            {
                // 登录失败
            }
        }

        public bool Login(string loginName, string password, out UserInfo userInfo)
        {
            userInfo = UserInfo.LoginInfo(loginName, password);
            if (userInfo.Identity == Guid.Empty)
                return false;
            else
            {
                Session.Add(key_userInfo, userInfo);
                return true;
            }
        }

        #endregion
    }
}