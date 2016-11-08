using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace ProjectCollection.WebUI.WebService
{
    /// <summary>
    /// Main 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://pms.cei.com.cn/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
    // [System.Web.Script.Services.ScriptService]
    public class Main : System.Web.Services.WebService
    {

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }

        //[WebMethod]
        //public Dictionary<string, string> Login()
        //{
        //    Dictionary<string, string> LoginInfo = new Dictionary<string, string>();
        //    LoginInfo.Add("UserId", "1");
        //    LoginInfo.Add("LoginName", "2");
        //    LoginInfo.Add("RealName", "3");
        //    LoginInfo.Add("RoleId", "4");
        //    return LoginInfo;
        //}

        [WebMethod]
        public string Login(string InPut)
        {
            Dictionary<string, string> LoginInfo = new Dictionary<string, string>();
            LoginInfo.Add("UserId", "1");
            LoginInfo.Add("LoginName", "2");
            LoginInfo.Add("RealName", "3");
            LoginInfo.Add("RoleId", "4");
            LoginInfo.Add("InPut", InPut);
            string json = (new System.Web.Script.Serialization.JavaScriptSerializer()).Serialize(LoginInfo);
            return json;
        }
    }
}
