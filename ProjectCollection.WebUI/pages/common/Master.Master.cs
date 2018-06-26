using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ProjectCollection.BLL;
using ProjectCollection.Common;
using ProjectCollection.WebUI.pages.common;

namespace ProjectCollection.WebUI.pages.common
{
    public partial class Master : System.Web.UI.MasterPage
    {
        public const string key_userInfo = "key_userInfo";

        /// <summary>
        /// 获取session中的登录用户信息
        /// </summary>
        public UserInfo LoginUserInfo
        {
            get
            {
                UserInfo value = (UserInfo)Session[key_userInfo];
                if (value == null)
                {
                    //throw new Exception("没有登录用户信息");
                    Response.Redirect("~/pages/login.aspx");
                }
                return value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            //Response.Write(Session.SessionID.ToString());
            string LogName = LoginUserInfo.RealName.ToString();
            if (LogName != "")
            {
                Anonymous.Visible = false;
                LoggedIn.Visible = true;
                txtLogName.Text = LogName;
            }
            else { }
            //if (LoginUserInfo.Authority.Contains("copy"))
            //{
            //    NavLi18.Visible = true;
            //}
            //else { }
            string encode = string.Empty;
            byte[] bytes = Encoding.UTF8.GetBytes(this.LoginUserInfo.LoginName + "_" + this.LoginUserInfo.Password);
            encode = HttpUtility.UrlEncode(Convert.ToBase64String(bytes), Encoding.UTF8);
            HyperLink1.NavigateUrl = "http://newpms.cei.cn/webpages/V2/index.html#/HomePage?mode=dispatch&login=" + encode;
            if (LoginUserInfo.Authority.Contains("PlanManage"))
            {
                HyperLink2.NavigateUrl = "~/pages/ProjectPlanListMasterDetail.aspx";
            }
            else { }
            if (LoginUserInfo.Authority.Contains("manage"))
            {
                HyperLink4.Text = "管理";
                HyperLink4.NavigateUrl = "~/pages/MyTask.aspx?mode=manage&range=now";
            }
            else if (LoginUserInfo.Authority.Contains("cross"))
            {
                NavLi6.Visible = true;
                HyperLink4.NavigateUrl = "~/pages/MyTask.aspx?mode=manufacture&range=now";
            }
            else 
            {
                HyperLink4.NavigateUrl = "~/pages/MyTask.aspx?mode=manufacture&range=now";
            }
            //if (LoginUserInfo.role_identity == new Guid("00000000-0000-0000-0000-000000000003"))
            //{ 
            //    NavLi1.Visible=true;
            //    NavLi17.Visible = true;
            //    NavLi18.Visible = true;
            //}
            //else if (LoginUserInfo.role_identity == new Guid("00000000-0000-0000-0000-000000000008"))
            //{
            //    NavLi1.Visible = true;
            //    NavLi17.Visible = true;
            //}
            //else if (LoginUserInfo.role_identity == new Guid("00000000-0000-0000-0000-000000000010"))
            //{
            //    NavLi2.Visible = true;
            //}
            //else if (LoginUserInfo.role_identity == new Guid("00000000-0000-0000-0000-000000000002"))
            //{
            //    NavLi2.Visible = true;
            //    NavLi4.Visible = true;
            //    //
            //    MainNav.Items[0].Items[1].Visible = true;
            //    MainNav.Items[0].Items[2].Visible = true;
            //}
            //else if (LoginUserInfo.role_identity == new Guid("00000000-0000-0000-0000-000000000012"))
            //{
            //    NavLi2.Visible = true;
            //    NavLi5.Visible = true;
            //}
            //else if (LoginUserInfo.role_identity == new Guid("00000000-0000-0000-0000-000000000006"))
            //{
            //    NavLi6.Visible = true;
            //    NavLi7.Visible = true;
            //    NavLi8.Visible = true;
            //}
            //else if (LoginUserInfo.role_identity == new Guid("00000000-0000-0000-0000-000000000001"))
            //{
            //    NavLi8.Visible = true;
            //}
            //else if (LoginUserInfo.role_identity == new Guid("00000000-0000-0000-0000-000000000011"))
            //{
            //    NavLi9.Visible = true;
            //    NavLi10.Visible = true;
            //}
            //else if (LoginUserInfo.role_identity == new Guid("00000000-0000-0000-0000-000000000009"))
            //{
            //    NavLi8.Visible = true;
            //    NavLi11.Visible = true;
            //}
            //else if (LoginUserInfo.role_identity == new Guid("00000000-0000-0000-0000-000000000004"))
            //{
            //    NavLi8.Visible = true;
            //    NavLi12.Visible = true;
            //    NavLi16.Visible = true;
            //    //
            //    MainNav.Items[0].Items[3].Visible = true;
            //}
            //else if (LoginUserInfo.role_identity == new Guid("00000000-0000-0000-0000-000000000013"))
            //{
            //    NavLi13.Visible = true;
            //    NavLi14.Visible = true;
            //    NavLi15.Visible = true;
            //    //
            //    MainNav.Items[0].Items[4].Visible = true;
            //}
            //else if (LoginUserInfo.role_identity == new Guid("00000000-0000-0000-0000-000000000014"))
            //{
            //    NavLi1.Visible = true;
            //    NavLi14.Visible = true;
            //    NavLi15.Visible = true;
            //    NavLi17.Visible = true;
            //    NavLi18.Visible = true;
            //}
            //else if (LoginUserInfo.role_identity == new Guid("00000000-0000-0000-0000-000000000015"))
            //{
            //    NavLi1.Visible = true;
            //    NavLi5.Visible = true;
            //    NavLi6.Visible = true;
            //    NavLi7.Visible = true;
            //    //NavLi8.Visible = true;
            //    NavLi16.Visible = true;
            //    NavLi17.Visible = true;
            //    NavLi18.Visible = true;
            //    //NavLi9.Visible = true;
            //    //NavLi10.Visible = true;
            //}
            //else if (LoginUserInfo.role_identity == new Guid("00000000-0000-0000-0000-000000000016"))
            //{
            //    NavLi8.Visible = true;
            //    NavLi7.Visible = true;
            //    NavLi16.Visible = true;
            //}
            //else if (LoginUserInfo.role_identity == new Guid("00000000-0000-0000-0000-000000000007"))
            //{
            //    NavLi1.Visible = true;
            //    NavLi2.Visible = true;
            //    NavLi3.Visible = true;
            //    NavLi4.Visible = true;
            //    NavLi5.Visible = true;
            //    NavLi6.Visible = true;
            //    NavLi7.Visible = true;
            //    NavLi8.Visible = true;
            //    NavLi9.Visible = true;
            //    NavLi10.Visible = true;
            //    NavLi11.Visible = true;
            //    NavLi12.Visible = true;
            //    NavLi13.Visible = true;
            //    NavLi14.Visible = true;
            //    NavLi15.Visible = true;
            //    NavLi16.Visible = true;
            //    NavLi17.Visible = true;
            //    NavLi18.Visible = true;
            //    //
            //    MainNav.Items[0].Items[1].Visible = true;
            //    MainNav.Items[0].Items[2].Visible = true;
            //    MainNav.Items[0].Items[3].Visible = true;
            //    MainNav.Items[0].Items[4].Visible = true;
            //}
            //else
            //{
            //}
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Session.Remove(key_userInfo);
            Response.Redirect("~/pages/login.aspx");
        }
    }
}