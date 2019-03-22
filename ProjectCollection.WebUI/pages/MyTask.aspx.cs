using ProjectCollection.BLL;
using ProjectCollection.Common;
using ProjectCollection.WebUI.pages.common;
using System;
using System.Linq;
using System.Configuration;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using DevExpress.Web;

namespace ProjectCollection.WebUI.pages
{
    public partial class MyTask : BasePage
    {
        public ContentPlaceHolder CurrentPage;
        public UserInfo CurrentUserInfo;
        public string ModeRange="now";
        public string ModeMain = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            CurrentPage = (ContentPlaceHolder)Page.Master.FindControl("ContentPlaceHolder1");
            if(this.Request.QueryString.ToString().Contains("mode"))
            {
                switch (this.Request["mode"])
                {
                    case "manage":
                        {
                            ModeMain = this.Request["mode"];
                            break;
                        }
                    case "manufacture":
                        {
                            ModeMain = this.Request["mode"];
                            break;
                        }
                    default:
                        {
                            SetModeByAuthority();
                            break;
                        }
                }
               
            }
            else 
            {
                SetModeByAuthority();
            }           
            if (ModeMain == "manage")
            {
                ModeRange = this.rblMode.SelectedItem.Value.ToString();
                if (!IsPostBack)
                {
                    InitControl();
                    CurrentUserInfo = (UserInfo)Session["key_userInfo"];
                }
                else
                {
                    if (this.ListBoxUser.Value == null)
                    {
                        CurrentUserInfo = (UserInfo)Session["key_userInfo"];
                    }
                    else
                    {
                        CurrentUserInfo = BLL.UserInfo.GetUserById(this.ListBoxUser.Value.ToString());
                    }
                }
                SwitchPanel();
            }
            else if (ModeMain == "manufacture")
            {
                CurrentPage.FindControl("manage").Visible = false;
                CurrentUserInfo = (UserInfo)Session["key_userInfo"];
            }
            else { CurrentUserInfo = BLL.UserInfo.GetUserById("00000000-0000-0000-0000-000000000007"); }
            //
            HidAllPanel();
            HidAmountLabel();
            //
            LoadData();
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (IsPostBack && ASPxEdit.ValidateEditorsInContainer(this))
            {

            }
        }

        protected void ListBoxUser_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        protected void rblMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            ModeRange = this.rblMode.SelectedItem.Value.ToString();
            LoadData();
        }

        #region Method

        protected void HidPanel(string Authority)
        {
            CurrentPage = (ContentPlaceHolder)Page.Master.FindControl("ContentPlaceHolder1");
            System.Web.UI.HtmlControls.HtmlIframe CurrentIframe = (System.Web.UI.HtmlControls.HtmlIframe)CurrentPage.FindControl("Iframe" + Authority);
            if (CurrentIframe.InnerHtml.Contains("gvProject") || CurrentIframe.InnerHtml.Contains("gvProjectPlan"))
            {

            }
            else
            {
                CurrentPage.FindControl("Panel" + Authority).Visible = false;
            }
        }

        protected void HidPanelFilterGrid(string Authority)
        {
            CurrentPage = (ContentPlaceHolder)Page.Master.FindControl("ContentPlaceHolder1");
            System.Web.UI.HtmlControls.HtmlIframe CurrentIframe = (System.Web.UI.HtmlControls.HtmlIframe)CurrentPage.FindControl("Iframe" + Authority);
            if (CurrentIframe.InnerHtml.Contains("aSelect"))
            {

            }
            else
            {
                //CurrentPage.FindControl("Panel" + Authority).Visible = false;
            }
        }

        protected void HidAllPanel()
        {
            List<string> PanelList = new List<string>() { "recond", "OpenClassReceive", "OpenClassOperation", "OpenClassPublish", "OpenClassCheck","capture","capturecheck","execution","shorthand","contentreceive","contentcheck","contentrecheck","contentfinish","productionreceive","productioncheck","productionfinish","publish","check", "CustomCheck", "CustomExecute" };
            foreach (string CurrentPanelId in PanelList)
            {
                Panel CurrentPanel = (Panel)CurrentPage.FindControl("Panel" + CurrentPanelId);
                CurrentPanel.Visible = false;
            }
        }

        protected void HidAmountLabel()
        {
            List<string> PanelList = new List<string>() { "recond", "OpenClassReceive", "OpenClassOperation", "OpenClassPublish", "OpenClassCheck", "capture", "capturecheck", "execution", "shorthand", "contentreceive", "contentcheck", "contentrecheck", "contentfinish", "productionreceive", "productioncheck", "productionfinish", "publish", "check", "CustomCheck", "CustomExecute" };
            foreach (string CurrentPanelId in PanelList)
            {
                Label CurrentPanel = (Label)CurrentPage.FindControl("LabelAmount" + CurrentPanelId);
                CurrentPanel.Visible = false;
            }
        }

        private void InitControl()
        {
            this.ListBoxUser.DataSource = BLL.UserInfo.GetDataByID();
            this.ListBoxUser.TextField = "real_name";
            this.ListBoxUser.ValueField = "user_Identity";
            this.ListBoxUser.DataBind();
            //
            deStart.Date = DateTime.Now.AddDays(-(DateTime.Now.DayOfYear + 1));
            deEnd.Date = DateTime.Now;
        }

        private void LoadData()
        {
            foreach (string CurrentAuthority in CurrentUserInfo.Authority)
            {
                if (CurrentAuthority != "copy" && CurrentAuthority != "PlanManage" && CurrentAuthority != "manage")
                {
                    if (CurrentAuthority == "recond")
                    {
                        Panelrecond.Visible = true;
                        if (ModeRange == "now")
                        {
                            Iframerecond.Visible = true;
                            Iframerecond.Attributes["src"] = "~/pages/ProjectPlanListEmbed.aspx?mode=recond";
                            //HidPanel(CurrentAuthority);
                        }
                        else if (ModeRange == "all")
                        {
                            ShowAmountData(CurrentAuthority);
                        }
                        else { }
                    }
                    else if (CurrentAuthority == "OpenClassReceive")
                    {
                        CurrentPage.FindControl("Panel" + CurrentAuthority).Visible = true;
                        if (ModeRange == "now")
                        {
                            IframeOpenClassReceive.Visible = true;
                            IframeOpenClassReceive.Attributes["src"] = "~/pages/CustomProjectListEmbed.aspx?type=00000000-0000-0000-0000-000000000202&mode=receive";
                        }
                        else if (ModeRange == "all")
                        {
                            ShowAmountData(CurrentAuthority);
                        }
                        else { }
                        //HidPanelFilterGrid(CurrentAuthority);
                    }
                    else if (CurrentAuthority == "OpenClassOperation")
                    {
                        CurrentPage.FindControl("Panel" + CurrentAuthority).Visible = true;
                        if (ModeRange == "now")
                        {
                            IframeOpenClassOperation.Visible = true;
                            IframeOpenClassOperation.Attributes["src"] = "~/pages/CustomProjectListEmbed.aspx?type=00000000-0000-0000-0000-000000000202&mode=operation";
                        }
                        else if (ModeRange == "all")
                        {
                            ShowAmountData(CurrentAuthority);
                        }
                        else { }
                        //HidPanelFilterGrid(CurrentAuthority);
                    }
                    else if (CurrentAuthority == "OpenClassPublish")
                    {
                        CurrentPage.FindControl("Panel" + CurrentAuthority).Visible = true;
                        if (ModeRange == "now")
                        {
                            IframeOpenClassPublish.Visible = true;
                            IframeOpenClassPublish.Attributes["src"] = "~/pages/CustomProjectListEmbed.aspx?type=00000000-0000-0000-0000-000000000202&mode=publish";
                        }
                        else if (ModeRange == "all")
                        {
                            ShowAmountData(CurrentAuthority);
                        }
                        else { }
                        //HidPanelFilterGrid(CurrentAuthority);
                    }
                    else if (CurrentAuthority == "OpenClassCheck")
                    {
                        CurrentPage.FindControl("Panel" + CurrentAuthority).Visible = true;
                        if (ModeRange == "now")
                        {
                            IframeOpenClassCheck.Visible = true;
                            IframeOpenClassCheck.Attributes["src"] = "~/pages/CustomProjectListEmbed.aspx?type=00000000-0000-0000-0000-000000000202&mode=check";
                        }
                        else if (ModeRange == "all")
                        {
                            ShowAmountData(CurrentAuthority);
                        }
                        else { }
                        //HidPanelFilterGrid(CurrentAuthority);
                    }
                    else if (CurrentAuthority == "CustomCheck")
                    {
                        CurrentPage.FindControl("Panel" + CurrentAuthority).Visible = true;
                        if (ModeRange == "now")
                        {
                            IframeCustomCheck.Visible = true;
                            IframeCustomCheck.Attributes["src"] = "~/pages/CustomTaskList.aspx?mode=check";
                        }
                        else if (ModeRange == "all")
                        {
                            //ShowAmountData(CurrentAuthority);
                        }
                        else { }
                        //HidPanelFilterGrid(CurrentAuthority);
                    }
                    else if (CurrentAuthority == "CustomExecute")
                    {
                        CurrentPage.FindControl("Panel" + CurrentAuthority).Visible = true;
                        if (ModeRange == "now")
                        {
                            IframeCustomExecute.Visible = true;
                            IframeCustomExecute.Attributes["src"] = "~/pages/CustomTaskList.aspx?mode=execute";
                        }
                        else if (ModeRange == "all")
                        {
                            //ShowAmountData(CurrentAuthority);
                        }
                        else { }
                        //HidPanelFilterGrid(CurrentAuthority);
                    }
                    else if (CurrentAuthority == "CustomHelpExecute")
                    {
                        CurrentPage.FindControl("Panel" + CurrentAuthority).Visible = true;
                        if (ModeRange == "now")
                        {
                            IframeCustomHelpExecute.Visible = true;
                            IframeCustomHelpExecute.Attributes["src"] = "~/pages/CustomTaskList.aspx?mode=helpexecute";
                        }
                        else if (ModeRange == "all")
                        {
                            //ShowAmountData(CurrentAuthority);
                        }
                        else { }
                        //HidPanelFilterGrid(CurrentAuthority);
                    }
                    else if (CurrentAuthority == "CustomPic")
                    {
                        CurrentPage.FindControl("Panel" + CurrentAuthority).Visible = true;
                        if (ModeRange == "now")
                        {
                            IframeCustomPic.Visible = true;
                            IframeCustomPic.Attributes["src"] = "~/pages/CustomTaskList.aspx?mode=pic";
                        }
                        else if (ModeRange == "all")
                        {
                            //ShowAmountData(CurrentAuthority);
                        }
                        else { }
                        //HidPanelFilterGrid(CurrentAuthority);
                    }
                    else if (CurrentAuthority == "CustomTemplate")
                    {
                        CurrentPage.FindControl("Panel" + CurrentAuthority).Visible = true;
                        if (ModeRange == "now")
                        {
                            IframeCustomTemplate.Visible = true;
                            IframeCustomTemplate.Attributes["src"] = "~/pages/CustomTaskList.aspx?mode=template";
                        }
                        else if (ModeRange == "all")
                        {
                            //ShowAmountData(CurrentAuthority);
                        }
                        else { }
                        //HidPanelFilterGrid(CurrentAuthority);
                    }
                    else if (CurrentAuthority == "CustomAttachment")
                    {
                        CurrentPage.FindControl("Panel" + CurrentAuthority).Visible = true;
                        if (ModeRange == "now")
                        {
                            IframeCustomAttachment.Visible = true;
                            IframeCustomAttachment.Attributes["src"] = "~/pages/CustomTaskList.aspx?mode=attachment";
                        }
                        else if (ModeRange == "all")
                        {
                            //ShowAmountData(CurrentAuthority);
                        }
                        else { }
                        //HidPanelFilterGrid(CurrentAuthority);
                    }
                    else if (CurrentAuthority == "cross"|| CurrentAuthority == "CreateProject")
                    {

                    }
                    else
                    {
                        Panel CurrentPanel = (Panel)CurrentPage.FindControl("Panel" + CurrentAuthority);
                        CurrentPanel.Visible = true;
                        System.Web.UI.HtmlControls.HtmlIframe CurrentIframe = (System.Web.UI.HtmlControls.HtmlIframe)CurrentPage.FindControl("Iframe" + CurrentAuthority);
                        if (ModeRange == "now")
                        {
                            CurrentIframe.Visible = true;
                            CurrentIframe.Attributes["src"] = "~/pages/ProjectListEmbed.aspx?mode=" + CurrentAuthority + "&userid=" + CurrentUserInfo.Identity;
                        }
                        else if (ModeRange == "all")
                        {
                            CurrentIframe.Visible = false;
                            ShowAmountData(CurrentAuthority);
                        }
                        else { }

                        //HidPanel(CurrentAuthority);
                    }
                }
                else { }
            }
        }

        private void ShowAmountData(string stages)
        {
            switch (stages)
            {
                case "recond":
                    {
                        Iframerecond.Visible = false;
                        LabelAmountrecond.Visible = true;
                        LabelAmountrecond.Text = BLL.ProjectPlan.GetRecondAmount(CurrentUserInfo.Identity, deStart.Date.ToString(), deEnd.Date.ToString()).ToString();
                        break;
                    }
                case "OpenClassReceive":
                    {
                        IframeOpenClassReceive.Visible = false;
                        LabelAmountOpenClassReceive.Visible = true;
                        LabelAmountOpenClassReceive.Text = BLL.CustomProject.GetReceiveAmount(CurrentUserInfo.Identity, deStart.Date.ToString(), deEnd.Date.ToString()).ToString();
                        break;
                    }
                case "OpenClassOperation":
                    {
                        IframeOpenClassOperation.Visible = false;
                        LabelAmountOpenClassOperation.Visible = true;
                        LabelAmountOpenClassOperation.Text = BLL.CustomProject.GetFinishAmount(CurrentUserInfo.Identity, deStart.Date.ToString(), deEnd.Date.ToString()).ToString();
                        break;
                    }
                case "OpenClassPublish":
                    {
                        IframeOpenClassPublish.Visible = false;
                        LabelAmountOpenClassPublish.Visible = true;
                        LabelAmountOpenClassPublish.Text = BLL.CustomProject.GetPublishAmount(CurrentUserInfo.Identity, deStart.Date.ToString(), deEnd.Date.ToString()).ToString();
                        break;
                    }
                case "OpenClassCheck":
                    {
                        IframeOpenClassCheck.Visible = false;
                        LabelAmountOpenClassCheck.Visible = true;
                        LabelAmountOpenClassCheck.Text = BLL.CustomProject.GetCheckAmount(CurrentUserInfo.Identity, deStart.Date.ToString(), deEnd.Date.ToString()).ToString();
                        break;
                    }
                case "capture":
                    {
                        LabelAmountcapture.Visible = true;
                        LabelAmountcapture.Text = BLL.Project.GetCaptureAmount(CurrentUserInfo.Identity, deStart.Date.ToString(), deEnd.Date.ToString()).ToString();
                        break;
                    }
                case "capturecheck":
                    {
                        Panelcapturecheck.Visible = false;
                        break;
                    }
                case "execution":
                    {
                        LabelAmountexecution.Visible = true;
                        LabelAmountexecution.Text = BLL.Project.GetExecutionAmount(CurrentUserInfo.Identity, deStart.Date.ToString(), deEnd.Date.ToString()).ToString();
                        break;
                    }
                case "shorthand":
                    {
                        LabelAmountshorthand.Visible = true;
                        LabelAmountshorthand.Text = BLL.Project.GetShorthandAmount(CurrentUserInfo.Identity, deStart.Date.ToString(), deEnd.Date.ToString()).ToString();
                        break;
                    }
                case "contentreceive":
                    {
                        LabelAmountcontentreceive.Visible = true;
                        LabelAmountcontentreceive.Text = BLL.Project.GetContentReceiveAmount(CurrentUserInfo.Identity, deStart.Date.ToString(), deEnd.Date.ToString()).ToString();
                        break;
                    }
                case "contentcheck":
                    {
                        LabelAmountcontentcheck.Visible = true;
                        int TypeA = BLL.Project.GetContentCheckAmount(CurrentUserInfo.Identity, "A", deStart.Date.ToString(), deEnd.Date.ToString());
                        int TypeB = BLL.Project.GetContentCheckAmount(CurrentUserInfo.Identity, "B", deStart.Date.ToString(), deEnd.Date.ToString());
                        int TypeS = BLL.Project.GetContentCheckAmount(CurrentUserInfo.Identity, "C", deStart.Date.ToString(), deEnd.Date.ToString());
                        LabelAmountcontentcheck.Text = "自筹：" + TypeA.ToString()+ " ; 会议：" + TypeB.ToString() + " ; 单改三：" + TypeS.ToString();
                        break;
                    }
                case "contentrecheck":
                    {
                        LabelAmountcontentrecheck.Visible = true;
                        int TypeA2 = BLL.Project.GetContentRecheckAmount(CurrentUserInfo.Identity, deStart.Date.ToString(), deEnd.Date.ToString());
                        LabelAmountcontentrecheck.Text = "自筹：" + TypeA2.ToString();
                        LabelAmountcontentcheckTotal.Visible = true;
                        int TypeA1 = BLL.Project.GetContentCheckAmount(CurrentUserInfo.Identity, "A", deStart.Date.ToString(), deEnd.Date.ToString());
                        int TypeB1 = BLL.Project.GetContentCheckAmount(CurrentUserInfo.Identity, "B", deStart.Date.ToString(), deEnd.Date.ToString());
                        int TypeS1 = BLL.Project.GetContentCheckAmount(CurrentUserInfo.Identity, "C", deStart.Date.ToString(), deEnd.Date.ToString());
                        LabelAmountcontentcheckTotal.Text = "审核合计：" + (TypeA2 + TypeA1 + TypeB1 + TypeS1).ToString();
                        break;
                    }
                case "contentfinish":
                    {
                        LabelAmountcontentfinish.Visible = true;
                        string count = BLL.Project.GetContentFinishAmount(CurrentUserInfo.Identity, deStart.Date.ToString(), deEnd.Date.ToString()).ToString();
                        string delayCont = BLL.Project.GetContentFinishDelayAmount(CurrentUserInfo.Identity, deStart.Date.ToString(), deEnd.Date.ToString()).ToString();
                        LabelAmountcontentfinish.Text = "完成数：" + count + "(其中延时完成数：" + delayCont + ")";
                        break;
                    }
                case "productionreceive":
                    {
                        LabelAmountproductionreceive.Visible = true;
                        LabelAmountproductionreceive.Text = BLL.Project.GetProductionReceiveAmount(CurrentUserInfo.Identity, deStart.Date.ToString(), deEnd.Date.ToString()).ToString();
                        break;
                    }
                case "productioncheck":
                    {
                        LabelAmountproductioncheck.Visible = true;
                        LabelAmountproductioncheck.Text = BLL.Project.GetProductionCheckAmount(CurrentUserInfo.Identity, deStart.Date.ToString(), deEnd.Date.ToString()).ToString();
                        break;
                    }
                case "productionfinish":
                    {
                        LabelAmountproductionfinish.Visible = true;
                        string count = BLL.Project.GetProductionFinishAmount(CurrentUserInfo.Identity, deStart.Date.ToString(), deEnd.Date.ToString()).ToString();
                        string delayCont = BLL.Project.GetProductionFinishDelayAmount(CurrentUserInfo.Identity, deStart.Date.ToString(), deEnd.Date.ToString()).ToString();
                        LabelAmountproductionfinish.Text = "完成数：" + count + "(其中延时完成数：" + delayCont + ")";
                        break;
                    }
                case "publish":
                    {
                        LabelAmountpublish.Visible = true;
                        LabelAmountpublish.Text = BLL.Project.GetPublishAmount(CurrentUserInfo.Identity, deStart.Date.ToString(), deEnd.Date.ToString()).ToString();
                        break;
                    }
                case "check":
                    {
                        LabelAmountcheck.Visible = true;
                        LabelAmountcheck.Text = BLL.Project.GetCheckAmount(CurrentUserInfo.Identity, deStart.Date.ToString(), deEnd.Date.ToString()).ToString();
                        break;
                    }
                default:
                    break;
            }
        }

        private void SwitchPanel()
        {
            if (ModeRange == "now")
            {
                CurrentPage.FindControl("PanelDate").Visible = false;
            }
            else if (ModeRange == "all")
            {
                CurrentPage.FindControl("PanelDate").Visible = true;
            }
            else { }
        }

        private void SetModeByAuthority()
        {
            UserInfo LoginUserInfo = (UserInfo)Session["key_userInfo"];
            if (LoginUserInfo.Authority.Contains("manage"))
            {
                ModeMain = "manage";
            }
            else
            {
                ModeMain = "manufacture";
            }
        }

        #endregion  Method
    }
}