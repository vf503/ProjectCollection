using ProjectCollection.BLL;
using ProjectCollection.WebUI.pages.common;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Net;
using System.Text;
using System.Web;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Drawing;
using System.Net.Mail;

namespace ProjectCollection.WebUI.pages
{
    public partial class ProjectCreateEdit : BasePage
    {
        public string UserName = "";
        public string PassWord = "";
        public string RedirectUrl = "";
        #region 事件
        //
        protected void Page_Load(object sender, EventArgs e)
        {
            #region Route
            if (this.Request["mode"] != "create" && string.IsNullOrEmpty(this.Request["ProjectId"]) == false)
            {
                Guid SourceId = new Guid(this.Request["ProjectId"].ToString());
                BLL.Project project = BLL.Project.GetProject(SourceId);
                BLL.ProjectPlan plan = BLL.ProjectPlan.GetProjectPlan(project.ProjectPlanId);
                if (plan.ProjectPlanTypeId.ToString() == "00000000-0000-0000-0000-000000000202")
                { this.Redirect("~/pages/CustomProjectCreateEdit.aspx?mode=" + this.Request["mode"] + "&ProjectId=" + this.Request["ProjectId"] + "&type=" + plan.ProjectPlanTypeId.ToString()); }
                else if (plan.ProjectPlanTypeId.ToString() == "")
                { }
                else { }
            }
            else if (this.Request["mode"] == "create" && string.IsNullOrEmpty(this.Request["ProjectPlanId"]) == false)
            {
                BLL.ProjectPlan plan = BLL.ProjectPlan.GetProjectPlan(new Guid(this.Request["ProjectPlanId"]));
                if (plan.ProjectPlanTypeId.ToString() == "00000000-0000-0000-0000-000000000202")
                { this.Redirect("~/pages/CustomProjectCreateEdit.aspx?mode=" + this.Request["mode"] + "&ProjectPlanId=" + this.Request["ProjectPlanId"] + "&type=" + plan.ProjectPlanTypeId.ToString()); }
                else if (plan.ProjectPlanTypeId.ToString() == "")
                { }
                else { }
            }
            else { }
            #endregion Route
            if (!IsPostBack)
            {
                InitDropDownList();
            }
            //
            #region create
            if (this.Request["mode"] == "create")
            {
                this.txtPlanNote.Visible=false;
                this.labelPlanNote.Visible = false;
                Guid ProjectPlanId = new Guid(this.Request["ProjectPlanId"].ToString());
                ProjectPlan ProjectPlan = BLL.ProjectPlan.GetProjectPlan(ProjectPlanId);
                this.btnOk.Visible = true;
                if (!IsPostBack)
                {
                    this.txtProjectNo.Text = ProjectPlan.ProjectPlanNo.ToString() + "-";
                    ddlContentNeeds.SelectedValue = "00000000-0000-0000-0000-000000000042";
                    ddlPublishNeeds.SelectedValue = "00000000-0000-0000-0000-000000000042";
                }
            }
            #endregion
            #region copy
            else if (this.Request["mode"] == "copy")
            {
                this.txtPlanNote.Visible = false;
                this.labelPlanNote.Visible = false;
                Guid SourceId = new Guid(this.Request["ProjectId"].ToString());
                this.hidProjectId.Value = SourceId.ToString();
                this.btnOk.Visible = true;
                if (!IsPostBack)
                {
                    this.InitBrowseData();
                }
            }
            #endregion
            #region browse
            else if (this.Request["mode"] == "browse")
            {
                Guid projectId = new Guid(this.Request["ProjectId"]);
                BLL.Project project = BLL.Project.GetProject(projectId);
                this.hidProjectId.Value = projectId.ToString();
                this.InitBrowseData();
                //
                try
                {
                    this.PanelCapture.Visible = true;
                    InitDropDownListCapture();
                    this.InitCaptureData();
                }
                catch
                {

                }
                finally
                {

                }

                //
                try
                {
                    this.PanelCaptureCheck.Visible = true;
                    this.InitCaptureCheckData();
                }
                catch
                {

                }
                finally
                {

                }

                //
                try
                {
                    this.PanelShorthand.Visible = true;
                    InitDropDownListShorthand();
                    this.InitShorthandData();
                }
                catch
                {

                }
                finally
                {

                }

                //
                try
                {
                    this.PanelContentReceive.Visible = true;
                    InitDropDownListContentReceive();
                    this.InitContentReceiveData();
                }
                catch
                {

                }
                finally
                {

                }
                //
                try
                {
                    this.PanelContentOperator.Visible = true;
                    InitDropDownListContentFinish();
                    this.InitContentFinishData();
                }
                catch
                {

                }
                finally
                {

                }

                //
                try
                {
                    this.PanelContentCheck.Visible = true;
                    InitDropDownListContentCheck();
                    this.InitContentCheckData();
                }
                catch
                {

                }
                finally
                {

                }

                //
                try
                {
                    this.PanelContentRecheck.Visible = true;
                    this.InitContentRecheckData();
                }
                catch
                {

                }
                finally
                {

                }
                //
                try
                {
                    this.PanelProductionReceive.Visible = true;
                    InitDropDownListProductionReceive(project.CourseType);
                    this.InitProductionReceiveData();
                }
                catch
                {

                }
                finally
                {

                }

                //
                try
                {
                    this.PanelProductionOperator.Visible = true;
                    InitDropDownListProductionFinish();
                    this.InitProductionFinishData();
                }
                catch
                {

                }
                finally
                {

                }

                //
                try
                {
                    this.PanelProductionCheck.Visible = true;
                    InitDropDownListProductionCheck();
                    this.InitProductionCheckData();
                }
                catch
                {

                }
                finally
                {

                }

                //
                try
                {
                    this.PanelPublish.Visible = true;
                    InitDropDownListPublish();
                    this.InitPublishData();
                }
                catch
                {

                }
                finally
                {

                }

                //
                try
                {
                    this.PanelCheck.Visible = true;
                    InitDropDownListCheck();
                    this.InitCheckData();
                }
                catch
                {

                }
                finally
                {

                }
            }
            #endregion
            #region capture
            else if (this.Request["mode"] == "capture")
            {
                Guid projectId = new Guid(this.Request["ProjectId"]);
                this.hidProjectId.Value = projectId.ToString();
                this.PanelCapture.Visible = true;
                this.btnOk.ValidationGroup = "capture";
                this.btnReceive.ValidationGroup = "capture";
                this.InitBrowseData();
                BLL.Project Project = BLL.Project.GetProject(new Guid(this.hidProjectId.Value.ToString()));
                if (Project.progress.ToString() == "00000000-0000-0000-0000-000000000105")//初次接收 
                {
                    this.btnReceive.Visible = true;
                    this.btnSentBack.Visible = true;
                    this.btnSentBack.Text = "延迟接收";
                }
                else if (Project.progress.ToString() == "00000000-0000-0000-0000-000000000131")//延迟接收 
                {
                    this.btnReceive.Visible = true;
                    if (!IsPostBack)
                    {
                        try
                        {
                            InitDropDownListCapture();
                            this.InitCaptureData();
                        }
                        catch
                        {

                        }
                        finally
                        {

                        }
                    }
                }
                else
                {
                    this.btnOk.Text = "采集完成";
                    this.btnOk.Visible = true;
                    if (!IsPostBack)
                    {
                        InitDropDownListCapture();
                    }
                }
            }
            else if (this.Request["mode"] == "capturebatchhandle")
            {
                if (!this.IsPostBack)
                {
                    List<Guid> BatchProjectId = new List<Guid>();
                    //BatchProjectId = this.PreviousPage.BatchProjectId;
                    //hidBatchProjectId.Value = ProjectCollection.Common.SerializeObj.Serialize(BatchProjectId);

                    //hidBatchProjectId.Value = this.Request["BatchId"].Replace(' ', '+'); 
                    hidBatchProjectId.Value = Session["BatchId"].ToString();
                    BatchProjectId = ProjectCollection.Common.SerializeObj.Desrialize(BatchProjectId, hidBatchProjectId.Value);
                    this.hidProjectId.Value = BatchProjectId[0].ToString();
                }
                this.PanelCapture.Visible = true;
                this.btnOk.ValidationGroup = "capture";
                this.btnReceive.ValidationGroup = "capture";
                this.InitBrowseData();
                BLL.Project Project = BLL.Project.GetProject(new Guid(this.hidProjectId.Value.ToString()));
                if (Project.progress.ToString() == "00000000-0000-0000-0000-000000000105")//初次接收 
                {
                    this.btnReceive.Visible = true;
                    this.btnSentBack.Visible = true;
                    this.btnSentBack.Text = "延迟接收";
                }
                else if (Project.progress.ToString() == "00000000-0000-0000-0000-000000000131")//延迟接收 
                {
                    this.btnReceive.Visible = true;
                    if (!IsPostBack)
                    {
                        try
                        {
                            InitDropDownListCapture();
                            this.InitCaptureData();
                        }
                        catch
                        {

                        }
                        finally
                        {

                        }
                    }
                }
                else
                {
                    this.btnOk.Text = "采集完成";
                    this.btnOk.Visible = true;
                    //this.btnNewOkNew.Visible = true;
                    if (!IsPostBack)
                    {
                        InitDropDownListCapture();
                    }
                }
            }
            #endregion
            #region capturecheck
            else if (this.Request["mode"] == "capturecheck")
            {
                Guid projectId = new Guid(this.Request["ProjectId"]);
                this.hidProjectId.Value = projectId.ToString();
                this.btnOk.Text = "修改信息并反馈";
                this.btnOk.Visible = true;
                this.btnDiscard.Visible = true;
                this.PanelCaptureCheck.Visible = true;
                this.PanelCaptureCheckBtn.Visible = true;
                if (!IsPostBack)
                {
                    this.InitBrowseData();
                }
            }
            #endregion capturecheck
            #region execution
            else if (this.Request["mode"] == "execution")
            {
                Guid projectId = new Guid(this.Request["ProjectId"]);
                this.hidProjectId.Value = projectId.ToString();
                this.btnOk.Text = "派发制作";
                this.btnOk.Visible = true;
                this.PanelCaptureCheck.Visible = true;
                this.InitCaptureCheckData();
                if (!IsPostBack)
                {
                    this.InitBrowseData();
                }
            }
            #endregion execution
            #region shorthand
            else if (this.Request["mode"] == "shorthand")
            {
                Guid projectId = new Guid(this.Request["ProjectId"]);
                this.hidProjectId.Value = projectId.ToString();
                this.btnOk.Text = "速记完成";
                this.btnOk.Visible = true;
                this.PanelShorthand.Visible = true;
                this.InitBrowseData();
                if (!IsPostBack)
                {
                    InitDropDownListShorthand();
                }
            }
            else if (this.Request["mode"] == "shorthandbatchhandle")
            {
                if (!this.IsPostBack)
                {
                    List<Guid> BatchProjectId = new List<Guid>();
                    hidBatchProjectId.Value = Session["BatchId"].ToString();
                    BatchProjectId = ProjectCollection.Common.SerializeObj.Desrialize(BatchProjectId, hidBatchProjectId.Value);
                    this.hidProjectId.Value = BatchProjectId[0].ToString();
                }
                this.btnOk.Text = "批量完成";
                this.btnOk.Visible = true;
                this.PanelShorthand.Visible = true;
                this.InitBrowseData();
                if (!IsPostBack)
                {
                    InitDropDownListShorthand();
                }
            }
            #endregion shorthand
            #region contentreceive
            else if (this.Request["mode"] == "contentreceive")
            {
                Guid projectId = new Guid(this.Request["ProjectId"]);
                this.hidProjectId.Value = projectId.ToString();
                this.PanelContentReceive.Visible = true;
                this.btnReceive.Visible = true;
                this.InitBrowseData();
                if (!IsPostBack)
                {
                    InitDropDownListContentReceive();
                }
                BLL.Project project = BLL.Project.GetProject(projectId);
                this.ContentReceiveLink.NavigateUrl = @"http://newpms.cei.cn/SrtUpload?ProjectNo="
                + HttpUtility.UrlEncode(project.ProjectNo)
                + "&mode=slide";
                UserName = LoginUserInfo.LoginName;
                PassWord = LoginUserInfo.Password;
                byte[] bytes = Encoding.Default.GetBytes(UserName + "_" + PassWord);
                string str = Convert.ToBase64String(bytes);
                if (string.IsNullOrEmpty(project.CourseType) == false && project.CourseType == "elite")
                {
                    this.ContentReceiveEditLink.NavigateUrl = @"http://newpms.cei.cn/CourseUpload?ProjectNo="
               + HttpUtility.UrlEncode(project.ProjectNo)
               + "&link="
               + HttpUtility.UrlEncode(str)
               + "&type=elite";
                }
                else
                {
                    this.ContentReceiveEditLink.NavigateUrl = @"http://newpms.cei.cn/SlideEdit?ProjectNo="
               + HttpUtility.UrlEncode(project.ProjectNo)
               + "&link="
               + HttpUtility.UrlEncode(str)
               + "&type=a";
                }
             }
            #endregion
            #region contentreceivebatchhandle
            else if (this.Request["mode"] == "contentreceivebatchhandle")
            {
                if (!this.IsPostBack)
                {
                    List<Guid> BatchProjectId = new List<Guid>();
                    //BatchProjectId = this.PreviousPage.BatchProjectId;
                    //hidBatchProjectId.Value = ProjectCollection.Common.SerializeObj.Serialize(BatchProjectId);
                    //hidBatchProjectId.Value = this.Request["BatchId"].Replace(' ', '+');
                    hidBatchProjectId.Value = Session["BatchId"].ToString();
                    BatchProjectId = ProjectCollection.Common.SerializeObj.Desrialize(BatchProjectId, hidBatchProjectId.Value);
                    this.hidProjectId.Value = BatchProjectId[0].ToString();
                }
                this.PanelContentReceive.Visible = true;
                this.btnReceive.Visible = true;
                this.btnReceive.Text = "批量接收";
                this.InitBrowseData();
                if (!IsPostBack)
                {
                    InitDropDownListContentReceive();
                }
            }
            #endregion contentreceivebatchhandle
            #region contentfinish
            else if (this.Request["mode"] == "contentfinish")
            {
                Guid projectId = new Guid(this.Request["ProjectId"]);
                this.hidProjectId.Value = projectId.ToString();
                this.btnOk.Visible = true;
                this.btnOk.Text = "制作完成";
                this.PanelContentOperator.Visible = true;
                this.PanelContentCheck.Visible = true;
                this.PanelContentRecheck.Visible = true;
                this.InitBrowseData();
                if (!IsPostBack)
                {
                    InitDropDownListContentFinish();
                    //
                    this.ddlContentCourseRecommend.SelectedValue = "00000000-0000-0000-0000-000000000043";
                    this.ddlContentPPTAdvice.SelectedValue = "00000000-0000-0000-0000-000000000043";
                    this.ddlContentCourseIntroNeeds.SelectedValue = "00000000-0000-0000-0000-000000000043";
                    this.ddlContentLecturerResumeNeeds.SelectedValue = "00000000-0000-0000-0000-000000000043";
                    this.ddlContentExercisesNeeds.SelectedValue = "00000000-0000-0000-0000-000000000043";
                    this.ddlContentTextEditNeeds.SelectedValue = "00000000-0000-0000-0000-000000000043";
                    //
                    UserName = LoginUserInfo.LoginName;
                    PassWord = LoginUserInfo.Password;
                    byte[] bytes = Encoding.Default.GetBytes(UserName + "_" + PassWord);
                    string str = Convert.ToBase64String(bytes);
                    BLL.Project project = BLL.Project.GetProject(projectId);
                    if (string.IsNullOrEmpty(project.CourseType) == false && project.CourseType == "elite")
                    {
                        this.ContentOperatLink.NavigateUrl = @"http://newpms.cei.cn/CourseUpload?ProjectNo="
                   + HttpUtility.UrlEncode(project.ProjectNo)
                   + "&link="
                   + HttpUtility.UrlEncode(str)
                   + "&type=elite";
                    }
                    else {
                        this.ContentOperatLink.NavigateUrl = @"http://newpms.cei.cn/SlideEdit?ProjectNo="
                   + HttpUtility.UrlEncode(project.ProjectNo)
                   + "&link="
                   + HttpUtility.UrlEncode(str)
                   + "&type=a";
                    }
                    //
                    try
                    {
                        this.InitContentCheckData();
                    }
                    catch
                    {

                    }
                    finally
                    {

                    }
                    try
                    {
                        this.InitContentRecheckData();
                    }
                    catch
                    {

                    }
                    finally
                    {

                    }
                    try
                    {
                        this.PanelContentReceive.Visible = true;
                        InitDropDownListContentReceive();
                        this.InitContentReceiveData();
                    }
                    catch
                    {

                    }
                    finally
                    {

                    }
                }
            }
            #endregion
            #region contentfinishbatch
            else if (this.Request["mode"] == "contentfinishbatchhandle")
            {
                this.btnOk.Visible = true;
                this.btnOk.Text = "制作完成";
                this.PanelContentOperator.Visible = true;
                this.PanelContentCheck.Visible = true;
                this.PanelContentRecheck.Visible = true;
                if (!IsPostBack)
                {
                    List<Guid> BatchProjectId = new List<Guid>();
                    hidBatchProjectId.Value = Session["BatchId"].ToString();
                    BatchProjectId = ProjectCollection.Common.SerializeObj.Desrialize(BatchProjectId, hidBatchProjectId.Value);
                    this.hidProjectId.Value = BatchProjectId[0].ToString();
                    //
                    InitDropDownListContentFinish();
                    //
                    this.ddlContentCourseRecommend.SelectedValue = "00000000-0000-0000-0000-000000000043";
                    this.ddlContentPPTAdvice.SelectedValue = "00000000-0000-0000-0000-000000000043";
                    this.ddlContentCourseIntroNeeds.SelectedValue = "00000000-0000-0000-0000-000000000043";
                    this.ddlContentLecturerResumeNeeds.SelectedValue = "00000000-0000-0000-0000-000000000043";
                    this.ddlContentExercisesNeeds.SelectedValue = "00000000-0000-0000-0000-000000000043";
                    this.ddlContentTextEditNeeds.SelectedValue = "00000000-0000-0000-0000-000000000043";
                    this.InitBrowseData();
                    try
                    {
                        this.InitContentCheckData();
                    }
                    catch
                    {

                    }
                    finally
                    {

                    }
                    try
                    {
                        this.InitContentRecheckData();
                    }
                    catch
                    {

                    }
                    finally
                    {

                    }
                }
            }
            #endregion
            #region contentcheck
            else if (this.Request["mode"] == "contentcheck")
            {
                Guid projectId = new Guid(this.Request["ProjectId"]);
                this.hidProjectId.Value = projectId.ToString();
                this.btnOk.Visible = true;
                this.btnOk.Text = "审核通过";
                this.btnSentBack.Visible = true;
                this.btnSentBack.Text = "退回";
                this.PanelContentReceive.Visible = true;
                this.PanelContentOperator.Visible = true;
                this.PanelContentCheck.Visible = true;
                this.PanelContentRecheck.Visible = true;
                this.InitBrowseData();
                //
                UserName = LoginUserInfo.LoginName;
                PassWord = LoginUserInfo.Password;
                byte[] bytes = Encoding.Default.GetBytes(UserName + "_" + PassWord);
                string str = Convert.ToBase64String(bytes);
                BLL.Project project = BLL.Project.GetProject(projectId);
                if (string.IsNullOrEmpty(project.CourseType) == false && project.CourseType == "elite")
                {
                    this.ContentCheckLink.NavigateUrl = @"http://newpms.cei.cn/CourseUpload?ProjectNo="
               + HttpUtility.UrlEncode(project.ProjectNo)
               + "&link="
               + HttpUtility.UrlEncode(str)
               + "&type=elite";
                }
                else
                {
                    this.ContentCheckLink.NavigateUrl = @"http://newpms.cei.cn/SlideEdit?ProjectNo="
               + HttpUtility.UrlEncode(project.ProjectNo)
               + "&link="
               + HttpUtility.UrlEncode(str)
               + "&type=a";
                }
                //
                if (!IsPostBack)
                {
                    InitDropDownListContentCheck();
                    try
                    {
                        this.InitDropDownListContentFinish();
                        this.InitContentFinishData();
                        this.InitContentCheckData();
                        this.InitContentRecheckData();
                    }
                    catch
                    {

                    }
                    finally
                    {

                    }
                    //
                    try
                    {  
                        InitDropDownListContentReceive();
                        this.InitContentReceiveData();
                    }
                    catch
                    {

                    }
                    finally
                    {

                    }
                }
                //

            }
            #endregion contentcheck
            #region contentcheckbatchsave
            else if (this.Request["mode"] == "contentcheckbatchsave")
            {
                if (!this.IsPostBack)
                {
                    List<Guid> BatchProjectId = new List<Guid>();
                    //BatchProjectId = this.PreviousPage.BatchProjectId;
                    //hidBatchProjectId.Value = ProjectCollection.Common.SerializeObj.Serialize(BatchProjectId);
                    hidBatchProjectId.Value = Session["BatchId"].ToString();
                    BatchProjectId = ProjectCollection.Common.SerializeObj.Desrialize(BatchProjectId, hidBatchProjectId.Value);
                    this.hidProjectId.Value = BatchProjectId[0].ToString();
                }
                this.btnOk.Visible = true;
                this.btnOk.Text = "保存审核信息";
                this.btnSentBack.Visible = true;
                this.btnSentBack.Text = "退回";
                this.PanelContentCheck.Visible = true;
                this.PanelContentRecheck.Visible = true;
                this.InitBrowseData();
                if (!IsPostBack)
                {
                    InitDropDownListContentCheck();
                    try
                    {
                        this.InitContentCheckData();
                        this.InitContentRecheckData();
                    }
                    catch
                    {

                    }
                    finally
                    {

                    }
                }
            }
            #endregion contentcheckbatchsave
            #region contentcheckbatchhandle
            else if (this.Request["mode"] == "contentcheckbatchhandle")
            {
                if (!this.IsPostBack)
                {
                    List<Guid> BatchProjectId = new List<Guid>();
                    //BatchProjectId = this.PreviousPage.BatchProjectId;
                    //hidBatchProjectId.Value = ProjectCollection.Common.SerializeObj.Serialize(BatchProjectId);
                    hidBatchProjectId.Value = Session["BatchId"].ToString();
                    BatchProjectId = ProjectCollection.Common.SerializeObj.Desrialize(BatchProjectId, hidBatchProjectId.Value);
                    this.hidProjectId.Value = BatchProjectId[0].ToString();
                }
                this.btnOk.Visible = true;
                this.btnOk.Text = "审核通过";
                this.btnSentBack.Visible = true;
                this.btnSentBack.Text = "退回";
                this.PanelContentCheck.Visible = true;
                this.PanelContentRecheck.Visible = true;
                this.InitBrowseData();
                if (!IsPostBack)
                {
                    InitDropDownListContentCheck();
                    try
                    {
                        this.InitContentCheckData();
                        this.InitContentRecheckData();
                    }
                    catch
                    {

                    }
                    finally
                    {

                    }
                }
            }
            #endregion contentcheckbatchhandle
            #region contentrecheck
            else if (this.Request["mode"] == "contentrecheck")
            {
                Guid projectId = new Guid(this.Request["ProjectId"]);
                this.hidProjectId.Value = projectId.ToString();
                this.btnOk.Text = "复审通过";
                this.btnSentBack.Visible = true;
                this.btnSentBack.Text = "退回";
                this.btnOk.Visible = true;
                this.PanelContentCheck.Visible = true;
                this.PanelContentRecheck.Visible = true;
                this.InitBrowseData();
                this.InitDropDownListContentCheck();
                //
                UserName = LoginUserInfo.LoginName;
                PassWord = LoginUserInfo.Password;
                byte[] bytes = Encoding.Default.GetBytes(UserName + "_" + PassWord);
                string str = Convert.ToBase64String(bytes);
                BLL.Project project = BLL.Project.GetProject(projectId);
                if (string.IsNullOrEmpty(project.CourseType) == false && project.CourseType == "elite")
                {
                    this.ContentReCheckLink.NavigateUrl = @"http://newpms.cei.cn/CourseUpload?ProjectNo="
               + HttpUtility.UrlEncode(project.ProjectNo)
               + "&link="
               + HttpUtility.UrlEncode(str)
               + "&type=elite";
                }
                else
                {
                    this.ContentReCheckLink.NavigateUrl = @"http://newpms.cei.cn/SlideEdit?ProjectNo="
               + HttpUtility.UrlEncode(project.ProjectNo)
               + "&link="
               + HttpUtility.UrlEncode(str)
               + "&type=a";
                }
                //
                if (!IsPostBack)
                {
                    //
                    try
                    {
                        this.PanelContentReceive.Visible = true;
                        InitDropDownListContentReceive();
                        this.InitContentReceiveData();
                        this.PanelContentOperator.Visible = true;
                        this.InitDropDownListContentFinish();
                        this.InitContentFinishData();
                        this.InitContentCheckData();
                        //
                    }
                    catch
                    {

                    }
                    finally
                    {

                    }
                    //
                    //
                    try
                    {
                        this.PanelPublish.Visible = true;
                        InitDropDownListPublish();
                        this.InitPublishData();
                    }
                    catch
                    {

                    }
                    finally
                    {

                    }
                }
            }
            #endregion contentrecheck
            #region productionreceive
            else if (this.Request["mode"] == "productionreceive")
            {
                Guid projectId = new Guid(this.Request["ProjectId"]);
                this.hidProjectId.Value = projectId.ToString();
                BLL.Project Project = BLL.Project.GetProject(new Guid(this.hidProjectId.Value.ToString()));
                UserName = LoginUserInfo.LoginName;
                PassWord = LoginUserInfo.Password;
                byte[] bytes = Encoding.Default.GetBytes(UserName + "_" + PassWord);
                string str = Convert.ToBase64String(bytes);
                LoadProductionReceivePageDate(Project);
                try
                {
                    this.PanelCaptureCheck.Visible = true;
                    this.InitCaptureCheckData();
                    if (string.IsNullOrEmpty(Project.CourseType) == false && Project.CourseType == "elite")
                    {
                        this.ContentOperatLink.NavigateUrl = @"http://newpms.cei.cn/CourseUpload?ProjectNo="
                   + HttpUtility.UrlEncode(Project.ProjectNo)
                   + "&link="
                   + HttpUtility.UrlEncode(str)
                   + "&type=elite";
                    }
                    else
                    {
                        this.ContentOperatLink.NavigateUrl = @"http://newpms.cei.cn/SlideEdit?ProjectNo="
                   + HttpUtility.UrlEncode(Project.ProjectNo)
                   + "&link="
                   + HttpUtility.UrlEncode(str)
                   + "&type=a";
                    }
                }
                catch
                {

                }
                finally
                {

                }
                if (Project.ProductionProgress.ToString() == "00000000-0000-0000-0000-000000000106")//初次接收 
                {
                    this.btnSentBack.Visible = true;
                    this.btnSentBack.Text = "延迟接收";
                }
                else
                {
                    if (!IsPostBack)
                    {
                        try
                        {
                            this.InitProductionReceiveData();
                        }
                        catch
                        {

                        }
                        finally
                        {

                        }
                    }
                }
                
            }
            #endregion productionreceive
            #region productionreceivebatchhandle
            else if (this.Request["mode"] == "productionreceivebatchhandle")
            {
                if (!this.IsPostBack)
                {
                    List<Guid> BatchProjectId = new List<Guid>();
                    //BatchProjectId = this.PreviousPage.BatchProjectId;
                    //hidBatchProjectId.Value = ProjectCollection.Common.SerializeObj.Serialize(BatchProjectId);

                    //hidBatchProjectId.Value = this.Request["BatchId"].Replace(' ', '+'); 
                    hidBatchProjectId.Value = Session["BatchId"].ToString();
                    BatchProjectId = ProjectCollection.Common.SerializeObj.Desrialize(BatchProjectId, hidBatchProjectId.Value);
                    this.hidProjectId.Value = BatchProjectId[0].ToString();
                }
                this.btnReceive.Text = "批量接收";
                this.btnSentBack.Visible = true;
                this.btnSentBack.Text = "批量延迟";
                BLL.Project Project = BLL.Project.GetProject(new Guid(this.hidProjectId.Value.ToString()));
                LoadProductionReceivePageDate(Project);
            }
            #endregion productionreceivebatchhandle
            #region productionfinish
            else if (this.Request["mode"] == "productionfinish")
            {
                Guid projectId = new Guid(this.Request["ProjectId"]);
                BLL.Project project = BLL.Project.GetProject(projectId);
                this.hidProjectId.Value = projectId.ToString();
                this.btnSentBack.Visible = true;
                this.btnSentBack.Text = "退回";
                this.btnOk.Visible = true;
                this.btnOk.Text = "制作完成";
                UserName = LoginUserInfo.LoginName;
                PassWord = LoginUserInfo.Password;
                byte[] bytes = Encoding.Default.GetBytes(UserName + "_" + PassWord);
                string str = Convert.ToBase64String(bytes);
                LoadProductionFinishPageDate(project);
                try
                {
                    this.PanelCaptureCheck.Visible = true;
                    this.InitCaptureCheckData();
                    if (string.IsNullOrEmpty(project.CourseType) == false && project.CourseType == "elite")
                    {
                        this.PlayVideoLink.Text = "查看资料";
                        this.PlayVideoLink.NavigateUrl = @"http://newpms.cei.cn/CourseUpload?ProjectNo="
                   + HttpUtility.UrlEncode(project.ProjectNo)
                   + "&link="
                   + HttpUtility.UrlEncode(str)
                   + "&type=elite";
                    }
                    else
                    {
                    }
                }
                catch
                {

                }
                finally
                {

                }
                try
                {
                    this.PanelProductionCheck.Visible = true;
                    this.InitProductionCheckData();
                    this.txtProductionCheckNote.ForeColor= Color.FromArgb(255, 255, 0, 0);
                }
                catch
                {

                }
                finally
                {

                }
                //NewPms
                //string FileNo = project.ProjectNo.Replace("-","").Replace("—", "").Replace("_","");
                //UserName = LoginUserInfo.LoginName;
                //PassWord = LoginUserInfo.Password;
                //byte[] bytes = Encoding.Default.GetBytes(UserName + "_" + PassWord);
                //string str = Convert.ToBase64String(bytes);
                //UploadVideoLink.NavigateUrl = @"http://newpms.cei.cn/FTPVideoUpload?ProjectNo=" 
                //    + HttpUtility.UrlEncode(project.ProjectNo)
                //    + "&title="
                //    + HttpUtility.UrlEncode(project.CourseName)
                //    + "&lecturer="
                //    + HttpUtility.UrlEncode(project.lecturer)
                //    + "&type="
                //    + project.ProjectNo.Substring(0,1).ToLower()
                //    + "&link="
                //    + str;
            }
            #endregion productionfinish
            #region productionfinishbatchhandle
            else if (this.Request["mode"] == "productionfinishbatchhandle")
            {
                if (!this.IsPostBack)
                {
                    List<Guid> BatchProjectId = new List<Guid>();
                    //BatchProjectId = this.PreviousPage.BatchProjectId;
                    //hidBatchProjectId.Value = ProjectCollection.Common.SerializeObj.Serialize(BatchProjectId);

                    //hidBatchProjectId.Value = this.Request["BatchId"].Replace(' ', '+');
                    hidBatchProjectId.Value = Session["BatchId"].ToString();
                    BatchProjectId = ProjectCollection.Common.SerializeObj.Desrialize(BatchProjectId, hidBatchProjectId.Value);
                    this.hidProjectId.Value = BatchProjectId[0].ToString();
                }
                this.btnOk.Visible = true;
                this.btnOk.Text = "批量制作完成";
                //this.btnNewOkNew.Visible = true;
                BLL.Project Project = BLL.Project.GetProject(new Guid(this.hidProjectId.Value.ToString()));
                LoadProductionFinishPageDate(Project);
            }
            #endregion productionfinishbatchhandle

            #region productioncheck
            else if (this.Request["mode"] == "productioncheck")
            {
                Guid projectId = new Guid(this.Request["ProjectId"]);
                this.hidProjectId.Value = projectId.ToString();
                this.btnOk.Visible = true;
                this.btnOk.Text = "审核通过";
                this.btnSentBack.Visible = true;
                this.btnSentBack.Text = "退回";
                this.PanelProductionOperator.Visible = true;
                this.PanelProductionCheck.Visible = true;
                this.InitBrowseData();
                this.InitDropDownListProductionFinish();
                this.InitProductionFinishData();
                if (!IsPostBack)
                {
                    InitDropDownListProductionCheck();
                    try
                    {
                        this.InitProductionCheckData();
                    }
                    catch
                    {

                    }
                    finally
                    {

                    }
                    try
                    {
                        this.PanelCaptureCheck.Visible = true;
                        this.InitCaptureCheckData();
                    }
                    catch
                    {

                    }
                    finally
                    {

                    }
                }
                //NewPms
                UserName = LoginUserInfo.LoginName;
                PassWord = LoginUserInfo.Password;
                byte[] bytes = Encoding.Default.GetBytes(UserName + "_" + PassWord);
                string str = Convert.ToBase64String(bytes);
                BLL.Project project = BLL.Project.GetProject(projectId);
                if (project.IsSingleScreen())//单视频
                {
                    this.PlayVideoLink.NavigateUrl = "http://newpms.cei.cn/PlayVideo?ProjectNo="
                    + HttpUtility.UrlEncode(project.ProjectNo)
                    + "&type=noslide"
                    + "&link="
                    + str;
                }
                else
                {
                    this.PlayVideoLink.NavigateUrl = "http://newpms.cei.cn/PlayVideo?ProjectNo="
                    + HttpUtility.UrlEncode(project.ProjectNo)
                    + "&type=a"
                    + "&link="
                    + str;
                }
            }
            #endregion productioncheck

            #region productioncheckbatchsave
            else if (this.Request["mode"] == "productioncheckbatchsave")
            {
                if (!this.IsPostBack)
                {
                    List<Guid> BatchProjectId = new List<Guid>();
                    //BatchProjectId = this.PreviousPage.BatchProjectId;
                    //hidBatchProjectId.Value = ProjectCollection.Common.SerializeObj.Serialize(BatchProjectId);
                    //hidBatchProjectId.Value = this.Request["BatchId"].Replace(' ', '+');
                    hidBatchProjectId.Value = Session["BatchId"].ToString();
                    BatchProjectId = ProjectCollection.Common.SerializeObj.Desrialize(BatchProjectId, hidBatchProjectId.Value);
                    this.hidProjectId.Value = BatchProjectId[0].ToString();
                }
                this.btnOk.Visible = true;
                this.btnOk.Text = "保存审核信息";
                this.btnSentBack.Visible = true;
                this.btnSentBack.Text = "退回";
                this.PanelProductionOperator.Visible = true;
                this.PanelProductionCheck.Visible = true;
                this.InitBrowseData();
                this.InitDropDownListProductionFinish();
                this.InitProductionFinishData();
                if (!IsPostBack)
                {
                    InitDropDownListProductionCheck();
                    try
                    {
                        this.InitProductionCheckData();
                    }
                    catch
                    {

                    }
                    finally
                    {

                    }
                }
            }
            #endregion productioncheckbatchsave
            #region productioncheckbatchhandle
            else if (this.Request["mode"] == "productioncheckbatchhandle")
            {
                if (!this.IsPostBack)
                {
                    List<Guid> BatchProjectId = new List<Guid>();
                    //BatchProjectId = this.PreviousPage.BatchProjectId;
                    //hidBatchProjectId.Value = ProjectCollection.Common.SerializeObj.Serialize(BatchProjectId);
                    //hidBatchProjectId.Value = this.Request["BatchId"].Replace(' ', '+');
                    hidBatchProjectId.Value = Session["BatchId"].ToString();
                    BatchProjectId = ProjectCollection.Common.SerializeObj.Desrialize(BatchProjectId, hidBatchProjectId.Value);
                    this.hidProjectId.Value = BatchProjectId[0].ToString();
                }
                this.btnOk.Visible = true;
                this.btnOk.Text = "审核通过";
                this.btnSentBack.Visible = true;
                this.btnSentBack.Text = "退回";
                this.PanelProductionOperator.Visible = true;
                this.PanelProductionCheck.Visible = true;
                this.InitBrowseData();
                this.InitDropDownListProductionFinish();
                this.InitProductionFinishData();
                if (!IsPostBack)
                {
                    InitDropDownListProductionCheck();
                    try
                    {
                        this.InitProductionCheckData();
                    }
                    catch
                    {

                    }
                    finally
                    {

                    }
                }
            }
            #endregion productioncheckbatchhandle
            #region publish check
            else if (this.Request["mode"] == "publish")
            {
                Guid projectId = new Guid(this.Request["ProjectId"]);
                this.hidProjectId.Value = projectId.ToString();
                this.btnOk.Visible = true;
                this.btnOk.Text = "通过";
                this.btnSentBack.Visible = true;
                this.btnSentBack.Text = "退回";
                this.PanelPublish.Visible = true;
                this.InitBrowseData();
                try
                {
                    this.PanelProductionCheck.Visible = true;
                    InitDropDownListProductionCheck();
                    this.InitProductionCheckData();
                }
                catch
                {

                }
                finally
                {

                }
                //
                try
                {
                    this.PanelContentCheck.Visible = true;
                    InitDropDownListContentCheck();
                    this.InitContentCheckData();
                }
                catch
                {

                }
                finally
                {

                }

                //
                try
                {
                    this.PanelContentRecheck.Visible = true;
                    this.InitContentRecheckData();
                }
                catch
                {

                }
                finally
                {

                }
                //
                UserName = LoginUserInfo.LoginName;
                PassWord = LoginUserInfo.Password;
                byte[] bytes = Encoding.Default.GetBytes(UserName + "_" + PassWord);
                string str = Convert.ToBase64String(bytes);
                BLL.Project project = BLL.Project.GetProject(projectId);
                string CourseType = "a";
                if (project.IsSingleScreen())//单视频
                {
                    CourseType = "noslide";
                }
                this.PublishCheckLink.NavigateUrl = @"http://newpms.cei.cn/CourseUpload?ProjectNo="
                + HttpUtility.UrlEncode(project.ProjectNo)
                + "&type=" + CourseType
                + "&link="
                + str;
                //
                if (!IsPostBack)
                {
                    InitDropDownListPublish();
                }
            }
            else if (this.Request["mode"] == "check")
            {
                Guid projectId = new Guid(this.Request["ProjectId"]);
                this.hidProjectId.Value = projectId.ToString();
                this.btnOk.Visible = true;
                this.btnOk.Text = "审核";
                this.PanelCheck.Visible = true;
                this.InitBrowseData();
                try
                {
                    this.PanelProductionCheck.Visible = true;
                    InitDropDownListProductionCheck();
                    this.InitProductionCheckData();
                }
                catch
                {

                }
                finally
                {

                }
                //
                try
                {
                    this.PanelContentCheck.Visible = true;
                    InitDropDownListContentCheck();
                    this.InitContentCheckData();
                }
                catch
                {

                }
                finally
                {

                }

                //
                try
                {
                    this.PanelContentRecheck.Visible = true;
                    this.InitContentRecheckData();
                }
                catch
                {

                }
                finally
                {

                }
                //
                UserName = LoginUserInfo.LoginName;
                PassWord = LoginUserInfo.Password;
                byte[] bytes = Encoding.Default.GetBytes(UserName + "_" + PassWord);
                string str = Convert.ToBase64String(bytes);
                BLL.Project project = BLL.Project.GetProject(projectId);
                string CourseType = "a";
                if (project.IsSingleScreen())//单视频
                {
                    CourseType = "noslide";
                }
                this.PublishCheckLink2.NavigateUrl = @"http://newpms.cei.cn/CourseUpload?ProjectNo="
                + HttpUtility.UrlEncode(project.ProjectNo)
                + "&type=" + CourseType
                + "&link="
                + str;
                //
                if (!IsPostBack)
                {
                    InitDropDownListCheck();
                }
            }
            else
            {

            }
            #endregion publish check
        }
        //
        protected void btnCancel_Click(object sender, EventArgs e)
        {

        }

        protected void btnOk_Click(object sender, EventArgs e)
        {
            #region create/copy
            if (this.Request["mode"] == "create" || this.Request["mode"] == "copy")
            {
                //新增 *复制工单在嵌入页执行
                Guid ProjectPlanId;
                Guid WorkTypeId = new Guid(this.ddlWorkType.SelectedValue);
                BLL.Project project = new BLL.Project();
                project.ProjectId = Guid.NewGuid();
                if (this.Request["mode"] == "create")
                {
                    ProjectPlanId = new Guid(this.Request["ProjectPlanId"].ToString());
                    project.SourceProjectId = project.ProjectId;
                }
                else
                {
                    BLL.Project SourceProject = BLL.Project.GetProject(new Guid(this.hidProjectId.Value.ToString()));
                    ProjectPlanId = SourceProject.ProjectPlanId;
                    project.SourceProjectId = SourceProject.ProjectId;
                }
                project.ProjectPlanId = ProjectPlanId;
                project.ProjectTypeId = new Guid(this.ddlProjectType.SelectedValue);
                project.ProjectNo = this.txtProjectNo.Text;
                project.emergency = new Guid(this.ddlemergency.SelectedValue);
                project.WorkType = WorkTypeId;
                project.CourseName = this.txtCourseName.Text;
                project.notice = new Guid(this.ddlnotice.SelectedValue);
                project.headline = new Guid(this.ddlHeadLine.SelectedValue);
                project.TextCategory = this.txtTextCategory.Text;
                project.lecturer = this.txtlecturer.Text;
                project.LecturerJob = this.txtLecturerJob.Text;
                project.InCharge = LoginUserInfo.Identity;
                project.CreateNote = this.txtCreateNote.Text;
                project.ExtraNote = this.txtExtraNote.Text;
                project.ContentNeeds = new Guid(this.ddlContentNeeds.SelectedValue);
                project.PublishNeeds = new Guid(this.ddlPublishNeeds.SelectedValue);
                project.CanBeSold = new Guid(this.rblCanBeSold.SelectedValue);
                project.EpisodeCount = Convert.ToInt32(this.ddlEpisodeCount.SelectedValue);
                if (project.IsCaptureNeeds() == false)
                {
                    project.progress = new Guid("00000000-0000-0000-0000-000000000120");
                }
                else
                {
                    project.progress = new Guid("00000000-0000-0000-0000-000000000105");
                }
                BLL.Project.Insert(project);
                if (this.ddlProjectType.SelectedValue == "00000000-0000-0000-0000-000000000299")
                {
                    using (var ProjectModel = new ProjectCollection.WebUI.Models.ProjectCollectionEntities())
                    {
                        ProjectCollection.WebUI.Models.Project ThisProject = (from p in ProjectModel.Project
                                                                              where p.ProjectId == project.ProjectId
                                                                              select p).First();
                        ThisProject.ProjectTypeId = new Guid("00000000-0000-0000-0000-000000000017");
                        ThisProject.MakeType = "new";
                        ProjectModel.SaveChanges();
                    }
                }
                using (var ProjectModel = new ProjectCollection.WebUI.Models.ProjectCollectionEntities())
                {
                    ProjectCollection.WebUI.Models.Project ThisProject = (from p in ProjectModel.Project
                                                                          where p.ProjectId == project.ProjectId
                                                                          select p).First();
                    ThisProject.STTType = this.ddlSTTType.SelectedValue;
                    //mc
                    if (this.ddlProjectType.SelectedValue== "00000000-0000-0000-0000-000000000298")
                    {
                        ThisProject.ProjectTypeId = new Guid("00000000-0000-0000-0000-000000000019");
                        ThisProject.ContentNeeds = new Guid("00000000-0000-0000-0000-000000000043");
                        ThisProject.MakeType = "new";
                        ThisProject.CourseType = "micro";
                    }
                    //
                    if (this.ddlProjectType.SelectedValue == "00000000-0000-0000-0000-000000000297")
                    {
                        ThisProject.ProjectTypeId = new Guid("00000000-0000-0000-0000-000000000018");
                        ThisProject.ContentNeeds = new Guid("00000000-0000-0000-0000-000000000042");
                        //ThisProject.PublishNeeds = new Guid("00000000-0000-0000-0000-000000000043");
                        ThisProject.MakeType = "new";
                        ThisProject.CourseType = "elite";
                    }
                    ProjectModel.SaveChanges();
                }
                if (this.Request["mode"] == "create")
                {
                    this.Redirect("~/pages/ProjectPlanListMasterDetail.aspx");
                }
                else
                {
                    this.Redirect("~/pages/MyTask.aspx?mode=browse");
                }
            }
            #endregion
            #region capture
            else if (this.Request["mode"] == "capture")
            {
                Guid ThisId = new Guid(this.hidProjectId.Value.ToString());
                var ProjectModel = new ProjectCollection.WebUI.Models.ProjectCollectionEntities();
                Models.Project ThisProject = (from p in ProjectModel.Project
                                                                      where p.ProjectId == ThisId
                                                                      select p).First();
                //新单视频
                if ((ThisProject.ProjectTypeId == new Guid("00000000-0000-0000-0000-000000000017") && ThisProject.MakeType == "new")|| ThisProject.CourseType== "elite")
                {
                    //List OP
                    DataTable tblDatas = new DataTable("Datas");
                    DataColumn dc = null;
                    dc = tblDatas.Columns.Add("id", Type.GetType("System.String"));
                    dc = tblDatas.Columns.Add("title", Type.GetType("System.String"));
                    dc = tblDatas.Columns.Add("result", Type.GetType("System.String"));
                    //List ED
                    //New OP
                    UserName = LoginUserInfo.LoginName;
                    PassWord = LoginUserInfo.Password;
                    byte[] bytes = Encoding.Default.GetBytes(UserName + "_" + PassWord);
                    string str = Convert.ToBase64String(bytes);
                    string url = @"http://newpms.cei.cn/FTPVideoUpload?ProjectNo="
                        + HttpUtility.UrlEncode(ThisProject.ProjectNo)
                        + "&title="
                        + HttpUtility.UrlEncode(ThisProject.CourseName)
                        + "&lecturer="
                        + HttpUtility.UrlEncode(ThisProject.lecturer)
                        + "&post="
                        + HttpUtility.UrlEncode(ThisProject.LecturerJob)
                        + "&type=NoSlideAudio"
                        + "&link="
                        + str;
                    if (ThisProject.CourseType == "elite")
                    {
                        url = @"http://newpms.cei.cn/FTPVideoUpload?ProjectNo="
                                                + HttpUtility.UrlEncode(ThisProject.ProjectNo)
                                                + "&title="
                                                + HttpUtility.UrlEncode(ThisProject.CourseName)
                                                + "&lecturer="
                                                + HttpUtility.UrlEncode(ThisProject.lecturer)
                                                + "&post="
                                                + HttpUtility.UrlEncode(ThisProject.LecturerJob)
                                                + "&type=EliteAudio"
                                                + "&link="
                                                + str;
                    }
                    //
                    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                    request.Method = "GET";
                    request.ContentType = "text/html;charset=UTF-8";
                    HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                    Stream myResponseStream = response.GetResponseStream();
                    StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding("utf-8"));
                    string retString = myStreamReader.ReadToEnd();
                    myStreamReader.Close();
                    myResponseStream.Close();
                    //
                    JObject jo = (JObject)JsonConvert.DeserializeObject(retString);
                    if (jo["status"].ToString() == "文件完备" && jo["data"].ToString() == "数据添加成功")
                    {
                        //
                       
                        //
                        DataRow newRow;
                        newRow = tblDatas.NewRow();
                        newRow["id"] = ThisProject.ProjectNo;
                        newRow["title"] = ThisProject.CourseName;
                        newRow["result"] = "成功";
                        tblDatas.Rows.Add(newRow);
                        //公共流程
                        BLL.Project project = BLL.Project.GetProject(new Guid(this.hidProjectId.Value.ToString()));
                        project.CaptureDuration = Convert.ToInt16(this.txtCaptureDuration.Text.ToString());
                        project.CaptureFinishDate = DateTime.Now;
                        project.CaptureVideoNeeds = new Guid(this.ddlCaptureVideoNeeds.SelectedValue);
                        project.CaptureAudioNeeds = new Guid(this.ddlCaptureAudioNeeds.SelectedValue);
                        project.CaptureVideoVideoQuality = new Guid(this.ddlCaptureVideoVideoQuality.SelectedValue);
                        project.CaptureVideoAudioQuality = new Guid(this.ddlCaptureVideoAudioQuality.SelectedValue);
                        project.CaptureAudioQuality = new Guid(this.ddlCaptureAudioQuality.SelectedValue);
                        project.CaptureSoundTrack = new Guid(this.ddlCaptureSoundTrack.SelectedValue);
                        project.CaptureFilePath = this.txtCaptureFilePath.Text.ToString();
                        project.CaptureNote = this.txtCaptureNote.Text.ToString();
                        project.progress = new Guid("00000000-0000-0000-0000-000000000120");
                        BLL.Project.UpdateCaptureFinish(project);
                        this.Redirect("~/pages/MyTask.aspx?mode=capture");
                    }
                    else
                    {
                        DataRow newRow;
                        newRow = tblDatas.NewRow();
                        newRow["id"] = ThisProject.ProjectNo;
                        newRow["title"] = ThisProject.CourseName;
                        newRow["result"] = "失败";
                        tblDatas.Rows.Add(newRow);
                        //显示失败信息
                        btnOk.Visible = false;
                        PanelResult.Visible = true;
                        TableResult.DataSource = tblDatas;
                        TableResult.DataBind();
                    }
                }
                //非新单视频
                else
                {
                    BLL.Project project = BLL.Project.GetProject(new Guid(this.hidProjectId.Value.ToString()));
                    project.CaptureDuration = Convert.ToInt16(this.txtCaptureDuration.Text.ToString());
                    project.CaptureFinishDate = DateTime.Now;
                    project.CaptureVideoNeeds = new Guid(this.ddlCaptureVideoNeeds.SelectedValue);
                    project.CaptureAudioNeeds = new Guid(this.ddlCaptureAudioNeeds.SelectedValue);
                    project.CaptureVideoVideoQuality = new Guid(this.ddlCaptureVideoVideoQuality.SelectedValue);
                    project.CaptureVideoAudioQuality = new Guid(this.ddlCaptureVideoAudioQuality.SelectedValue);
                    project.CaptureAudioQuality = new Guid(this.ddlCaptureAudioQuality.SelectedValue);
                    project.CaptureSoundTrack = new Guid(this.ddlCaptureSoundTrack.SelectedValue);
                    project.CaptureFilePath = this.txtCaptureFilePath.Text.ToString();
                    project.CaptureNote = this.txtCaptureNote.Text.ToString();
                    //单视频三分屏工作开始分支
                    //if (//单视频
                    //       project.WorkType == new Guid("00000000-0000-0000-0000-000000000017")
                    //       || project.WorkType == new Guid("00000000-0000-0000-0000-000000000019")
                    //       )
                    //{
                    //    project.progress = new Guid("00000000-0000-0000-0000-000000000106");
                    //}
                    //else
                    //{
                    //    project.progress = new Guid("00000000-0000-0000-0000-000000000109");
                    //}
                    project.progress = new Guid("00000000-0000-0000-0000-000000000120");
                    BLL.Project.UpdateCaptureFinish(project);
                    this.Redirect("~/pages/MyTask.aspx?mode=capture");
                } 
            }
            else if (this.Request["mode"] == "capturebatchhandle")
            {
                //
                bool hasFailed = false;
                //List OP
                DataTable tblDatas = new DataTable("Datas");
                DataColumn dc = null;
                dc = tblDatas.Columns.Add("id", Type.GetType("System.String"));
                dc = tblDatas.Columns.Add("title", Type.GetType("System.String"));
                dc = tblDatas.Columns.Add("result", Type.GetType("System.String"));
                //List ED
                //
                List<Guid> BatchProjectId = new List<Guid>();
                BatchProjectId = ProjectCollection.Common.SerializeObj.Desrialize(BatchProjectId, hidBatchProjectId.Value);
                for (int i = 0; i < BatchProjectId.Count; i++)
                {
                    //
                    Guid ThisId = BatchProjectId[i];
                    var ProjectModel = new ProjectCollection.WebUI.Models.ProjectCollectionEntities();
                    Models.Project ThisProject = (from p in ProjectModel.Project
                                                  where p.ProjectId == ThisId
                                                  select p).First();
                    //新单视频
                    if ((ThisProject.ProjectTypeId == new Guid("00000000-0000-0000-0000-000000000017") && ThisProject.MakeType == "new") || ThisProject.CourseType == "elite")
                    {
                        BLL.Project project = BLL.Project.GetProject(BatchProjectId[i]);
                        project.CaptureDuration = Convert.ToInt16(this.txtCaptureDuration.Text.ToString());
                        project.CaptureFinishDate = DateTime.Now;
                        project.CaptureVideoNeeds = new Guid(this.ddlCaptureVideoNeeds.SelectedValue);
                        project.CaptureAudioNeeds = new Guid(this.ddlCaptureAudioNeeds.SelectedValue);
                        project.CaptureVideoVideoQuality = new Guid(this.ddlCaptureVideoVideoQuality.SelectedValue);
                        project.CaptureVideoAudioQuality = new Guid(this.ddlCaptureVideoAudioQuality.SelectedValue);
                        project.CaptureAudioQuality = new Guid(this.ddlCaptureAudioQuality.SelectedValue);
                        project.CaptureSoundTrack = new Guid(this.ddlCaptureSoundTrack.SelectedValue);
                        project.CaptureFilePath = this.txtCaptureFilePath.Text.ToString();
                        project.CaptureNote = this.txtCaptureNote.Text.ToString();
                        //New OP
                        UserName = LoginUserInfo.LoginName;
                        PassWord = LoginUserInfo.Password;
                        byte[] bytes = Encoding.Default.GetBytes(UserName + "_" + PassWord);
                        string str = Convert.ToBase64String(bytes);
                        string url = @"http://newpms.cei.cn/FTPVideoUpload?ProjectNo="
                            + HttpUtility.UrlEncode(project.ProjectNo)
                            + "&title="
                            + HttpUtility.UrlEncode(project.CourseName)
                            + "&lecturer="
                            + HttpUtility.UrlEncode(project.lecturer)
                            + "&post="
                            + HttpUtility.UrlEncode(project.LecturerJob)
                            + "&type=NoSlideAudio"
                            + "&link="
                            + str;
                        if (ThisProject.CourseType == "elite")
                        {
                            url = @"http://newpms.cei.cn/FTPVideoUpload?ProjectNo="
                                                    + HttpUtility.UrlEncode(ThisProject.ProjectNo)
                                                    + "&title="
                                                    + HttpUtility.UrlEncode(ThisProject.CourseName)
                                                    + "&lecturer="
                                                    + HttpUtility.UrlEncode(ThisProject.lecturer)
                                                    + "&post="
                                                    + HttpUtility.UrlEncode(ThisProject.LecturerJob)
                                                    + "&type=EliteAudio"
                                                    + "&link="
                                                    + str;
                        }
                        //
                        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                        request.Method = "GET";
                        request.ContentType = "text/html;charset=UTF-8";
                        HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                        Stream myResponseStream = response.GetResponseStream();
                        StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding("utf-8"));
                        string retString = myStreamReader.ReadToEnd();
                        myStreamReader.Close();
                        myResponseStream.Close();
                        //
                        JObject jo = (JObject)JsonConvert.DeserializeObject(retString);
                        if (jo["status"].ToString() == "文件完备" && jo["data"].ToString() == "数据添加成功")
                        {
                            //
                            
                            //
                            project.progress = new Guid("00000000-0000-0000-0000-000000000120");
                            BLL.Project.UpdateCaptureFinish(project);
                            DataRow newRow;
                            newRow = tblDatas.NewRow();
                            newRow["id"] = project.ProjectNo;
                            newRow["title"] = project.CourseName;
                            newRow["result"] = "成功";
                            tblDatas.Rows.Add(newRow);
                        }
                        else
                        {
                            hasFailed = true;
                            DataRow newRow;
                            newRow = tblDatas.NewRow();
                            newRow["id"] = project.ProjectNo;
                            newRow["title"] = project.CourseName;
                            newRow["result"] = "失败";
                            tblDatas.Rows.Add(newRow);
                        }
                        //New ED
                    }
                    //非新单视频
                    else
                    {
                        BLL.Project project = BLL.Project.GetProject(BatchProjectId[i]);
                        project.CaptureDuration = Convert.ToInt16(this.txtCaptureDuration.Text.ToString());
                        project.CaptureFinishDate = DateTime.Now;
                        project.CaptureVideoNeeds = new Guid(this.ddlCaptureVideoNeeds.SelectedValue);
                        project.CaptureAudioNeeds = new Guid(this.ddlCaptureAudioNeeds.SelectedValue);
                        project.CaptureVideoVideoQuality = new Guid(this.ddlCaptureVideoVideoQuality.SelectedValue);
                        project.CaptureVideoAudioQuality = new Guid(this.ddlCaptureVideoAudioQuality.SelectedValue);
                        project.CaptureAudioQuality = new Guid(this.ddlCaptureAudioQuality.SelectedValue);
                        project.CaptureSoundTrack = new Guid(this.ddlCaptureSoundTrack.SelectedValue);
                        project.CaptureFilePath = this.txtCaptureFilePath.Text.ToString();
                        project.CaptureNote = this.txtCaptureNote.Text.ToString();
                        //单视频三分屏工作开始分支
                        //if (//单视频
                        //       project.WorkType == new Guid("00000000-0000-0000-0000-000000000017")
                        //       || project.WorkType == new Guid("00000000-0000-0000-0000-000000000019")
                        //       )
                        //{
                        //    project.progress = new Guid("00000000-0000-0000-0000-000000000106");
                        //}
                        //else
                        //{
                        //    project.progress = new Guid("00000000-0000-0000-0000-000000000109");
                        //}
                        project.progress = new Guid("00000000-0000-0000-0000-000000000120");
                        BLL.Project.UpdateCaptureFinish(project);
                    }
                }
                btnOk.Visible = false;
                PanelResult.Visible = true;
                TableResult.DataSource = tblDatas;
                TableResult.DataBind();
                if (!hasFailed)
                {
                    this.Redirect("~/pages/MyTask.aspx?mode=capture");
                }
            }
            #endregion
            #region capturecheck
            else if (this.Request["mode"] == "capturecheck")
            {
                BLL.Project project = BLL.Project.GetProject(new Guid(this.hidProjectId.Value.ToString()));
                project.CaptureCheckPersonInCharge = this.LoginUserInfo.Identity;
                project.CaptureCheckDate = DateTime.Now;
                project.CaptureCheckNote = this.txtCaptureCheckNote.Text.ToString();
                project.progress = new Guid("00000000-0000-0000-0000-000000000121");
                project.ProjectTypeId = new Guid(this.ddlProjectType.SelectedValue);
                project.ProjectNo = this.txtProjectNo.Text;
                project.emergency = new Guid(this.ddlemergency.SelectedValue);
                project.WorkType = new Guid(this.ddlWorkType.SelectedValue);
                project.CourseName = this.txtCourseName.Text;
                project.notice = new Guid(this.ddlnotice.SelectedValue);
                project.headline = new Guid(this.ddlHeadLine.SelectedValue);
                project.TextCategory = this.txtTextCategory.Text;
                project.lecturer = this.txtlecturer.Text;
                project.LecturerJob = this.txtLecturerJob.Text;
                project.CreateNote = this.txtCreateNote.Text;
                project.ExtraNote = this.txtExtraNote.Text;
                BLL.Project.UpdateCaptureCheckFinish(project);
                //
                var ProjectModel = new ProjectCollection.WebUI.Models.ProjectCollectionEntities();
                ProjectCollection.WebUI.Models.Project ThisProject = (from p in ProjectModel.Project
                                                                      where p.ProjectId == project.ProjectId
                                                                      select p).First();
                if (this.ddlProjectType.SelectedValue == "00000000-0000-0000-0000-000000000299")
                {
                        ThisProject.ProjectTypeId = new Guid("00000000-0000-0000-0000-000000000017");
                        ThisProject.MakeType = "new";
                }
                else { ThisProject.MakeType = ""; }
                ProjectModel.SaveChanges();
                //
                this.Redirect("~/pages/MyTask.aspx?mode=capturecheck");
            }
            #endregion
            #region execution
            else if (this.Request["mode"] == "execution")
            {
                BLL.Project project = BLL.Project.GetProject(new Guid(this.hidProjectId.Value.ToString()));
                Guid WorkTypeId = new Guid(this.ddlWorkType.SelectedValue);
                if (this.ddlProjectType.SelectedValue == "00000000-0000-0000-0000-000000000299")
                { project.ProjectTypeId = new Guid("00000000-0000-0000-0000-000000000017"); }
                else { project.ProjectTypeId = new Guid(this.ddlProjectType.SelectedValue); }
                project.ProjectNo = this.txtProjectNo.Text;
                project.emergency = new Guid(this.ddlemergency.SelectedValue);
                project.WorkType = WorkTypeId;
                project.CourseName = this.txtCourseName.Text;
                project.notice = new Guid(this.ddlnotice.SelectedValue);
                project.headline = new Guid(this.ddlHeadLine.SelectedValue);
                project.TextCategory = this.txtTextCategory.Text;
                project.lecturer = this.txtlecturer.Text;
                project.LecturerJob = this.txtLecturerJob.Text;
                project.CreateNote = this.txtCreateNote.Text;
                project.ExtraNote = this.txtExtraNote.Text;
                project.ExecutionDate = DateTime.Now;
                if (project.IsSingleScreen())//单视频，同时接收
                {
                    project.progress = new Guid("00000000-0000-0000-0000-000000000127");
                    project.ProductionProgress = new Guid("00000000-0000-0000-0000-000000000106");
                    if (project.ContentNeeds == new Guid("00000000-0000-0000-0000-000000000043"))//无制作部
                    {
                        project.ContentProgress = new Guid("00000000-0000-0000-0000-000000000113");
                    }
                    else
                    {
                        project.ContentProgress = new Guid("00000000-0000-0000-0000-000000000107");
                    }
                }
                else if (project.IsSingleScreen() == false && project.IsShorthandNeeds() == true)//三分屏有速记(有速记必有制作部，牛雷确认)
                {
                    project.progress = new Guid("00000000-0000-0000-0000-000000000109");
                    project.ProductionProgress = new Guid("00000000-0000-0000-0000-000000000129");
                    project.ContentProgress = new Guid("00000000-0000-0000-0000-000000000130");
                }
                else if (project.IsSingleScreen() == false && project.IsShorthandNeeds() == false)//三分屏无速记
                {
                    if (project.ContentNeeds == new Guid("00000000-0000-0000-0000-000000000043"))//无制作部
                    {
                        project.progress = new Guid("00000000-0000-0000-0000-000000000106");
                        project.ContentProgress = new Guid("00000000-0000-0000-0000-000000000113");
                        project.ProductionProgress = new Guid("00000000-0000-0000-0000-000000000106");
                    }
                    else
                    {
                        project.progress = new Guid("00000000-0000-0000-0000-000000000107");
                        project.ContentProgress = new Guid("00000000-0000-0000-0000-000000000107");
                        project.ProductionProgress = new Guid("00000000-0000-0000-0000-000000000129");
                    }
                }
                else { }
                //STT
                if (project.ProjectTypeId == new Guid("00000000-0000-0000-0000-000000000199"))
                {
                    project.progress = new Guid("00000000-0000-0000-0000-000000000106");
                    project.ContentProgress = new Guid("00000000-0000-0000-0000-000000000130");
                    project.ProductionProgress = new Guid("00000000-0000-0000-0000-000000000106");
                }
                //新单转新三
                if (project.ProjectTypeId.ToString() == "00000000-0000-0000-0000-000000000199" && project.WorkType.ToString() == "00000000-0000-0000-0000-000000000029")
                {
                    var Model = new ProjectCollection.WebUI.Models.ProjectCollectionEntities();
                    ProjectCollection.WebUI.Models.Project SourceProject = (from p in Model.Project
                                                                            where p.ProjectId.ToString() == project.SourceProjectId.ToString()
                                                                            select p).First();
                    if (SourceProject.ProjectTypeId.ToString() == "00000000-0000-0000-0000-000000000017" && SourceProject.MakeType == "new")
                    {
                        project.progress = new Guid("00000000-0000-0000-0000-000000000210");
                        project.ContentProgress = new Guid("00000000-0000-0000-0000-000000000130");
                        project.ProductionProgress = new Guid("00000000-0000-0000-0000-000000000129");
                        string UserName = LoginUserInfo.LoginName;
                        string PassWord = LoginUserInfo.Password;
                        byte[] bytes = Encoding.Default.GetBytes(UserName + "_" + PassWord);
                        string type = "CourseCopy";
                        ProjectCollection.WebUI.Models.Project Project = (from p in Model.Project
                                                                            where p.ProjectId.ToString() == this.hidProjectId.Value.ToString()
                                                                          select p).First();
                        if (Project.STTType == "low")
                        {
                            type = "CourseCopyNoSTT";
                        }
                        string str = Convert.ToBase64String(bytes);
                        string url = @"http://newpms.cei.cn/FTPVideoUpload/?link="
                        + str
                        + "&type="
                        + type
                        + "&title="
                        + HttpUtility.UrlEncode(project.CourseName)
                        + "&lecturer="
                        + HttpUtility.UrlEncode(project.lecturer)
                        + "&post="
                        + HttpUtility.UrlEncode(project.LecturerJob)
                        + "&src="
                        + HttpUtility.UrlEncode(SourceProject.ProjectNo)
                        + "&ProjectNo="
                        + HttpUtility.UrlEncode(project.ProjectNo);
                        //
                        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                        request.Method = "GET";
                        request.ContentType = "text/html;charset=UTF-8";
                        HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                        Stream myResponseStream = response.GetResponseStream();
                        StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding("utf-8"));
                        string retString = myStreamReader.ReadToEnd();
                        myStreamReader.Close();
                        myResponseStream.Close();
                        //
                        JObject jo = (JObject)JsonConvert.DeserializeObject(retString);
                        if (jo["status"].ToString() == "文件完备" && jo["data"].ToString() == "数据添加成功")
                        {
                        }
                        else
                        {
                            throw new MyException(jo["status"].ToString());
                        }
                    }
                }
                    BLL.Project.UpdateExecution(project);
                //新单视频
                var ProjectModel = new ProjectCollection.WebUI.Models.ProjectCollectionEntities();
                ProjectCollection.WebUI.Models.Project ThisProject = (from p in ProjectModel.Project
                                                                      where p.ProjectId == project.ProjectId
                                                                      select p).First();
                if (this.ddlProjectType.SelectedValue == "00000000-0000-0000-0000-000000000299")
                {
                    ThisProject.ProjectTypeId = new Guid("00000000-0000-0000-0000-000000000017");
                    ThisProject.MakeType = "new";
                }
                else { ThisProject.MakeType = ""; }
                if (ThisProject.CourseType == "elite")
                {
                    MailMessage message = new MailMessage();
                    //设置发件人,发件人需要与设置的邮件发送服务器的邮箱一致
                    MailAddress fromAddr = new MailAddress("chench@cei.cn");
                    message.From = fromAddr;
                    //设置收件人,可添加多个,添加方法与下面的一样
                    message.To.Add("gaowq@cei.cn");
                    //设置抄送人
                    message.CC.Add("15510466762@163.com");
                    //设置邮件标题
                    message.Subject = "STT_中经网速记任务_" + ThisProject.ProjectNo;
                    //设置邮件内容
                    message.IsBodyHtml = true;
                    message.Body = "标题:" + ThisProject.CourseName + "<br />";
                    message.Body += "<a href=\"http://newpms.cei.cn/AudioFile/" + ThisProject.ProjectNo + ".mp3\">下载音频</a><br />";
                    //资料
                    message.Body += "<a href=\"http://203.207.118.112/CourseFile/TempUpload/" + ThisProject.ProjectNo + ".zip\">相关资料(无法下载则表示无资料)</a><br />";
                    message.Body += "<a href=\"http://newpms.cei.cn/SrtUpload?mode=text&ProjectNo=" + ThisProject.ProjectNo + "\">上传字幕</a>";
                    //设置邮件发送服务器,服务器根据你使用的邮箱而不同,可以到相应的邮箱管理后台查看
                    SmtpClient client = new SmtpClient("smtp.cei.cn", 25);
                    //设置发送人的邮箱账号和密码
                    client.Credentials = new NetworkCredential("chench@cei.cn", "A1b2c3d4");
                    client.Send(message);
                }
                ProjectModel.SaveChanges();
                //
                this.Redirect("~/pages/MyTask.aspx?mode=execution");
            }
            #endregion execution
            #region shorthand
            else if (this.Request["mode"] == "shorthand")//有速记必有制作部，牛雷确认
            {
                BLL.Project project = BLL.Project.GetProject(new Guid(this.hidProjectId.Value.ToString()));
                project.ShorthandPersonInCharge = this.LoginUserInfo.Identity;
                project.ShorthandFinishDate = DateTime.Now;
                project.ShorthandAudioReceiveDate = Convert.ToDateTime(this.hidShorthandAudioReceiveDate.Value);
                project.ShorthandPurveyor = this.txtShorthandPurveyor.Text.ToString();
                project.ShorthandQuality = new Guid(this.ddlShorthandQuality.SelectedValue);
                project.ShorthandNote = this.txtShorthandNote.Text.ToString();
                project.progress = new Guid("00000000-0000-0000-0000-000000000107");
                project.ContentProgress = new Guid("00000000-0000-0000-0000-000000000107");
                BLL.Project.UpdateShorthandFinish(project);
                this.Redirect("~/pages/MyTask.aspx?mode=shorthand");
            }
            else if (this.Request["mode"] == "shorthandbatchhandle")
            {
                List<Guid> BatchProjectId = new List<Guid>();
                BatchProjectId = ProjectCollection.Common.SerializeObj.Desrialize(BatchProjectId, hidBatchProjectId.Value);
                for (int i = 0; i < BatchProjectId.Count; i++)
                {
                    BLL.Project project = BLL.Project.GetProject(BatchProjectId[i]);
                    project.ShorthandPersonInCharge = this.LoginUserInfo.Identity;
                    project.ShorthandFinishDate = DateTime.Now;
                    project.ShorthandAudioReceiveDate = Convert.ToDateTime(this.hidShorthandAudioReceiveDate.Value);
                    project.ShorthandPurveyor = this.txtShorthandPurveyor.Text.ToString();
                    project.ShorthandQuality = new Guid(this.ddlShorthandQuality.SelectedValue);
                    project.ShorthandNote = this.txtShorthandNote.Text.ToString();
                    project.progress = new Guid("00000000-0000-0000-0000-000000000107");
                    project.ContentProgress = new Guid("00000000-0000-0000-0000-000000000107");
                    BLL.Project.UpdateShorthandFinish(project);
                }
                this.Redirect("~/pages/MyTask.aspx?mode=shorthand");
            }
            #endregion shorthand
            #region contentfinish
            else if (this.Request["mode"] == "contentfinish")
            {
                BLL.Project project = BLL.Project.GetProject(new Guid(this.hidProjectId.Value.ToString()));
                UpdateContentFinish(project);
                this.Redirect("~/pages/MyTask.aspx?mode=contentfinish");
            }
            #endregion
            #region contentfinishbatchhandle
            else if (this.Request["mode"] == "contentfinishbatchhandle")
            {
                List<Guid> BatchProjectId = new List<Guid>();
                BatchProjectId = ProjectCollection.Common.SerializeObj.Desrialize(BatchProjectId, hidBatchProjectId.Value);
                for (int i = 0; i < BatchProjectId.Count; i++)
                {
                    BLL.Project project = BLL.Project.GetProject(BatchProjectId[i]);
                    UpdateContentFinish(project);
                }
                this.Redirect("~/pages/MyTask.aspx?mode=contentfinish");
            }
            #endregion
            #region
            else if (this.Request["mode"] == "contentcheck")
            {
                BLL.Project project = BLL.Project.GetProject(new Guid(this.hidProjectId.Value.ToString()));
                UpdateContentCheck(project);
                this.Redirect("~/pages/MyTask.aspx?mode=contentcheck");
            }
            #endregion
            #region
            else if (this.Request["mode"] == "contentcheckbatchsave")
            {
                List<Guid> BatchProjectId = new List<Guid>();
                BatchProjectId = ProjectCollection.Common.SerializeObj.Desrialize(BatchProjectId, hidBatchProjectId.Value);
                for (int i = 0; i < BatchProjectId.Count; i++)
                {
                    BLL.Project project = BLL.Project.GetProject(BatchProjectId[i]);
                    project.ContentCourseIntroductionQuality = new Guid(this.ddlContentCourseIntroductionQuality.SelectedValue);
                    project.ContentResumeQuality = new Guid(this.ddlContentResumeQuality.SelectedValue);
                    project.ContentPPTQuality = new Guid(this.ddlContentPPTQuality.SelectedValue);
                    project.ContentExercisesQuality = new Guid(this.ddlContentExercisesQuality.SelectedValue);
                    project.ContentTextQuality = new Guid(this.ddlContentTextQuality.SelectedValue);
                    project.ContentIsTimely = new Guid(this.ddlContentIsTimely.SelectedValue);
                    project.ContentCheckDate = DateTime.Now;
                    project.ContentCheckNote = this.txtContentCheckNote.Text.ToString();
                    project.progress = project.progress;
                    project.ContentProgress = project.ContentProgress;
                    project.ContentCheckPersonInCharge = this.LoginUserInfo.Identity;
                    BLL.Project.UpdateContentCheck(project);
                }
                this.Redirect("~/pages/MyTask.aspx?mode=contentcheck");
            }
            #endregion
            #region contentcheckbatchhandle
            else if (this.Request["mode"] == "contentcheckbatchhandle")
            {
                List<Guid> BatchProjectId = new List<Guid>();
                BatchProjectId = ProjectCollection.Common.SerializeObj.Desrialize(BatchProjectId, hidBatchProjectId.Value);
                for (int i = 0; i < BatchProjectId.Count; i++)
                {
                    BLL.Project project = BLL.Project.GetProject(BatchProjectId[i]);
                    UpdateContentCheck(project);
                }
                this.Redirect("~/pages/MyTask.aspx?mode=contentcheck");
            }
            #endregion contentcheckbatchhandle
            #region contentrecheck
            else if (this.Request["mode"] == "contentrecheck")
            {
                BLL.Project project = BLL.Project.GetProject(new Guid(this.hidProjectId.Value.ToString()));
                project.ContentRecheckPersonInCharge = this.LoginUserInfo.Identity;
                project.ContentRecheckDate = DateTime.Now;
                project.ContentRecheckNote = this.txtContentRecheckNote.Text.ToString();

                project.ContentProgress = new Guid("00000000-0000-0000-0000-000000000113");
                if (//单视频
                       project.ProjectTypeId == new Guid("00000000-0000-0000-0000-000000000017")
                       || project.ProjectTypeId == new Guid("00000000-0000-0000-0000-000000000019")
                       || project.ProjectTypeId == new Guid("00000000-0000-0000-0000-000000000299")
                       )
                {
                    if (project.ProductionProgress == new Guid("00000000-0000-0000-0000-000000000116"))
                    {
                        project.progress = new Guid("00000000-0000-0000-0000-000000000117");
                    }
                    else
                    {
                        project.progress = new Guid("00000000-0000-0000-0000-000000000113");
                    }
                }
                else if (project.ProjectTypeId == new Guid("00000000-0000-0000-0000-000000000199"))//STT
                {
                    project.progress = new Guid("00000000-0000-0000-0000-000000000117");
                }
                else
                {
                    //三分屏 制作部完成 技术部准备接收
                    project.progress = new Guid("00000000-0000-0000-0000-000000000106");
                    project.ProductionProgress = new Guid("00000000-0000-0000-0000-000000000106");
                }
                BLL.Project.UpdateContentRecheckFinish(project);
                using (var ProjectModel = new ProjectCollection.WebUI.Models.ProjectCollectionEntities())
                {
                    ProjectCollection.WebUI.Models.Project ThisProject = (from p in ProjectModel.Project
                                                                          where p.ProjectId.ToString() == project.ProjectId.ToString()
                                                                          select p).First();
                    ThisProject.ContentCheckScore = this.ddlContentCheckScore.SelectedValue;
                    ThisProject.ContentCheckSlideScore = this.ddlContentCheckSlideScore.SelectedValue;
                    ThisProject.LecturerNote = this.txtLecturerNote.Text;
                    ProjectModel.SaveChanges();
                }
                if (project.ProjectTypeId == new Guid("00000000-0000-0000-0000-000000000199"))
                {
                    string url = @"http://newpms.cei.cn/JsonDataDataBackup?ProjectNo="
                        + HttpUtility.UrlEncode(project.ProjectNo);
                    //
                    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                    request.Method = "GET";
                    request.ContentType = "text/html;charset=UTF-8";
                    HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                    Stream myResponseStream = response.GetResponseStream();
                    StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding("utf-8"));
                    string retString = myStreamReader.ReadToEnd();
                    myStreamReader.Close();
                    myResponseStream.Close();
                }
                this.Redirect("~/pages/MyTask.aspx?mode=contentrecheck");
            }
            #endregion
            #region productionfinish
            else if (this.Request["mode"] == "productionfinish")
            {
                Guid ThisId = new Guid(this.hidProjectId.Value.ToString());
                var ProjectModel = new ProjectCollection.WebUI.Models.ProjectCollectionEntities();
                Models.Project ThisProject = (from p in ProjectModel.Project
                                              where p.ProjectId == ThisId
                                              select p).First();
                //新单视频
                if ((ThisProject.ProjectTypeId == new Guid("00000000-0000-0000-0000-000000000017") || ThisProject.ProjectTypeId == new Guid("00000000-0000-0000-0000-000000000019")) && ThisProject.MakeType == "new")
                {
                    BLL.Project project = BLL.Project.GetProject(new Guid(this.hidProjectId.Value.ToString()));
                    //
                    DataTable tblDatas = new DataTable("Datas");
                    DataColumn dc = null;
                    dc = tblDatas.Columns.Add("id", Type.GetType("System.String"));
                    dc = tblDatas.Columns.Add("title", Type.GetType("System.String"));
                    dc = tblDatas.Columns.Add("result", Type.GetType("System.String"));
                    //
                    UserName = LoginUserInfo.LoginName;
                    PassWord = LoginUserInfo.Password;
                    byte[] bytes = Encoding.Default.GetBytes(UserName + "_" + PassWord);
                    string str = Convert.ToBase64String(bytes);
                    string CourseType = "";
                    if (ThisProject.ProjectTypeId == new Guid("00000000-0000-0000-0000-000000000017"))
                    { CourseType = "NoSlideVideo"; }
                    else if (ThisProject.ProjectTypeId == new Guid("00000000-0000-0000-0000-000000000019"))
                    { CourseType = "NoSlideVideoMicro"; }
                    string url = @"http://newpms.cei.cn/FTPVideoUpload?ProjectNo="
                            + HttpUtility.UrlEncode(ThisProject.ProjectNo)
                            + "&title="
                            + HttpUtility.UrlEncode(ThisProject.CourseName)
                            + "&lecturer="
                            + HttpUtility.UrlEncode(ThisProject.lecturer)
                            + "&post="
                            + HttpUtility.UrlEncode(ThisProject.LecturerJob)
                            + "&type=" + CourseType
                            + "&link="
                            + str;
                    //
                    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                    request.Method = "GET";
                    request.ContentType = "text/html;charset=UTF-8";
                    HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                    Stream myResponseStream = response.GetResponseStream();
                    StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding("utf-8"));
                    string retString = myStreamReader.ReadToEnd();
                    myStreamReader.Close();
                    myResponseStream.Close();
                    //
                    JObject jo = (JObject)JsonConvert.DeserializeObject(retString);
                    DataRow newRow;
                    newRow = tblDatas.NewRow();
                    newRow["id"] = ThisProject.ProjectNo;
                    newRow["title"] = ThisProject.CourseName;
                    if (jo["status"].ToString() == "文件完备")
                    {
                        UpdateProductionFinish(project);
                        newRow["result"] = "成功";
                        tblDatas.Rows.Add(newRow);
                        this.Redirect("~/pages/MyTask.aspx?mode=productionfinish");
                    }
                    else
                    {
                        newRow["result"] = "失败";
                        tblDatas.Rows.Add(newRow);
                        btnOk.Visible = false;
                        PanelResult.Visible = true;
                        TableResult.DataSource = tblDatas;
                        TableResult.DataBind();
                    }
                }
                else if (ThisProject.CourseType== "elite")
                {
                    BLL.Project project = BLL.Project.GetProject(new Guid(this.hidProjectId.Value.ToString()));
                    //
                    DataTable tblDatas = new DataTable("Datas");
                    DataColumn dc = null;
                    dc = tblDatas.Columns.Add("id", Type.GetType("System.String"));
                    dc = tblDatas.Columns.Add("title", Type.GetType("System.String"));
                    dc = tblDatas.Columns.Add("result", Type.GetType("System.String"));
                    //
                    UserName = LoginUserInfo.LoginName;
                    PassWord = LoginUserInfo.Password;
                    byte[] bytes = Encoding.Default.GetBytes(UserName + "_" + PassWord);
                    string str = Convert.ToBase64String(bytes);
                    string CourseType = "NoSlideVideoElite";
                    string url = @"http://newpms.cei.cn/FTPVideoUpload?ProjectNo="
                            + HttpUtility.UrlEncode(ThisProject.ProjectNo)
                            + "&title="
                            + HttpUtility.UrlEncode(ThisProject.CourseName)
                            + "&lecturer="
                            + HttpUtility.UrlEncode(ThisProject.lecturer)
                            + "&post="
                            + HttpUtility.UrlEncode(ThisProject.LecturerJob)
                            + "&type=" + CourseType
                            + "&link="
                            + str;
                    //
                    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                    request.Method = "GET";
                    request.ContentType = "text/html;charset=UTF-8";
                    HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                    Stream myResponseStream = response.GetResponseStream();
                    StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding("utf-8"));
                    string retString = myStreamReader.ReadToEnd();
                    myStreamReader.Close();
                    myResponseStream.Close();
                    //
                    JObject jo = (JObject)JsonConvert.DeserializeObject(retString);
                    DataRow newRow;
                    newRow = tblDatas.NewRow();
                    newRow["id"] = ThisProject.ProjectNo;
                    newRow["title"] = ThisProject.CourseName;
                    if (jo["status"].ToString() == "文件完备")
                    {
                        UpdateProductionFinish(project);
                        //

                        //
                        newRow["result"] = "成功";
                        tblDatas.Rows.Add(newRow);
                        this.Redirect("~/pages/MyTask.aspx?mode=productionfinish");
                    }
                    else
                    {
                        newRow["result"] = "失败";
                        tblDatas.Rows.Add(newRow);
                        btnOk.Visible = false;
                        PanelResult.Visible = true;
                        TableResult.DataSource = tblDatas;
                        TableResult.DataBind();
                    }
                }
                else
                {
                    BLL.Project project = BLL.Project.GetProject(new Guid(this.hidProjectId.Value.ToString()));
                    if (ChromakeyR.Text != "0" && ChromakeyG.Text != "0" && ChromakeyB.Text != "0")
                    {
                        int R = Convert.ToInt16(ChromakeyR.Text);
                        int G = Convert.ToInt16(ChromakeyG.Text);
                        int B = Convert.ToInt16(ChromakeyB.Text);
                        string ChromaKey = System.Drawing.ColorTranslator.ToHtml(System.Drawing.Color.FromArgb(R, G, B));
                        UpdateProductionFinish(project, ChromaKey);
                    }
                    else
                    {
                        UpdateProductionFinish(project);
                    }

                    this.Redirect("~/pages/MyTask.aspx?mode=productionfinish");
                }
            }
            #endregion productionfinish
            #region productionfinishbatch
            else if (this.Request["mode"] == "productionfinishbatchhandle")
            {
                //
                bool hasFailed = false;
                DataTable tblDatas = new DataTable("Datas");
                DataColumn dc = null;
                dc = tblDatas.Columns.Add("id", Type.GetType("System.String"));
                dc = tblDatas.Columns.Add("title", Type.GetType("System.String"));
                dc = tblDatas.Columns.Add("result", Type.GetType("System.String"));
                //
                List<Guid> BatchProjectId = new List<Guid>();
                BatchProjectId = ProjectCollection.Common.SerializeObj.Desrialize(BatchProjectId, hidBatchProjectId.Value);
                for (int i = 0; i < BatchProjectId.Count; i++)
                {
                    //
                    Guid ThisId = BatchProjectId[i];
                    var ProjectModel = new ProjectCollection.WebUI.Models.ProjectCollectionEntities();
                    Models.Project ThisProject = (from p in ProjectModel.Project
                                                  where p.ProjectId == ThisId
                                                  select p).First();
                    //新单视频
                    if (ThisProject.ProjectTypeId == new Guid("00000000-0000-0000-0000-000000000017") && ThisProject.MakeType == "new")
                    {
                        BLL.Project project = BLL.Project.GetProject(BatchProjectId[i]);
                        UserName = LoginUserInfo.LoginName;
                        PassWord = LoginUserInfo.Password;
                        byte[] bytes = Encoding.Default.GetBytes(UserName + "_" + PassWord);
                        string str = Convert.ToBase64String(bytes);
                        string url = @"http://newpms.cei.cn/FTPVideoUpload?ProjectNo="
                            + HttpUtility.UrlEncode(project.ProjectNo)
                            + "&title="
                            + HttpUtility.UrlEncode(project.CourseName)
                            + "&lecturer="
                            + HttpUtility.UrlEncode(project.lecturer)
                            + "&post="
                            + HttpUtility.UrlEncode(project.LecturerJob)
                            + "&type=NoSlideVideo"
                            + "&link="
                            + str;
                        //
                        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                        request.Method = "GET";
                        request.ContentType = "text/html;charset=UTF-8";
                        HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                        Stream myResponseStream = response.GetResponseStream();
                        StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding("utf-8"));
                        string retString = myStreamReader.ReadToEnd();
                        myStreamReader.Close();
                        myResponseStream.Close();
                        //
                        JObject jo = (JObject)JsonConvert.DeserializeObject(retString);
                        DataRow newRow;
                        newRow = tblDatas.NewRow();
                        newRow["id"] = project.ProjectNo;
                        newRow["title"] = project.CourseName;
                        if (jo["status"].ToString() == "文件完备")
                        {
                            UpdateProductionFinish(project);
                            newRow["result"] = "成功";
                        }
                        else
                        {
                            newRow["result"] = "失败";
                            hasFailed = true;
                        }
                        tblDatas.Rows.Add(newRow);
                    }
                    //非单视频
                    else
                    {
                        BLL.Project project = BLL.Project.GetProject(BatchProjectId[i]);
                        if (ChromakeyR.Text != "0" && ChromakeyG.Text != "0" && ChromakeyB.Text != "0")
                        {
                            int R = Convert.ToInt16(ChromakeyR.Text);
                            int G = Convert.ToInt16(ChromakeyG.Text);
                            int B = Convert.ToInt16(ChromakeyB.Text);
                            string ChromaKey = System.Drawing.ColorTranslator.ToHtml(System.Drawing.Color.FromArgb(R, G, B));
                            UpdateProductionFinish(project, ChromaKey);
                        }
                        else
                        {
                            UpdateProductionFinish(project);
                        }
                    }
                }
                if (!hasFailed)
                {
                    this.Redirect("~/pages/MyTask.aspx?mode=productionfinish");
                }
                else {
                    btnOk.Visible = false;
                    PanelResult.Visible = true;
                    TableResult.DataSource = tblDatas;
                    TableResult.DataBind();
                }
            }
            #endregion productionfinishbatch
            #region productioncheck
            else if (this.Request["mode"] == "productioncheck")
            {
                BLL.Project project = BLL.Project.GetProject(new Guid(this.hidProjectId.Value.ToString()));
                if (project.ProjectTypeId == new Guid("00000000-0000-0000-0000-000000000199"))
                {
                    UserName = LoginUserInfo.LoginName;
                    PassWord = LoginUserInfo.Password;
                    byte[] bytes = Encoding.Default.GetBytes(UserName + "_" + PassWord);
                    string str = Convert.ToBase64String(bytes);
                    string url = "http://newpms.cei.cn/PlayVideo" + "?link=" + str;
                    Encoding encoding = Encoding.GetEncoding("utf-8");
                    IDictionary<string, string> parameters = new Dictionary<string, string>();
                    parameters.Add("projectId", project.ProjectNo);
                    //parameters.Add("X-CSRFToken", HttpContext.Current.Request.Cookies["csrftoken"].Value);
                    HttpWebResponse response = ProjectCollection.Common.PostRequest.CreatePostHttpResponse(url, parameters, encoding);
                    //打印返回值  
                    Stream stream = response.GetResponseStream();   //获取响应的字符串流  
                    StreamReader sr = new StreamReader(stream); //创建一个stream读取流  
                    string request = sr.ReadToEnd();   //从头读到尾，放到字符串
                    if (request == "审核完毕")
                    {
                        message.Text = request;
                        message.Visible = true;
                        UpdateProductionCheck(project);
                        this.Redirect("~/pages/MyTask.aspx?mode=productioncheck");
                    }
                    else {
                        message.Text = "提交错误";
                        message.Visible = true;
                    }
                }
                else
                {
                    UpdateProductionCheck(project);
                    this.Redirect("~/pages/MyTask.aspx?mode=productioncheck");
                }
            }
            #endregion productioncheck
            #region
            else if (this.Request["mode"] == "productioncheckbatchsave")
            {
                List<Guid> BatchProjectId = new List<Guid>();
                BatchProjectId = ProjectCollection.Common.SerializeObj.Desrialize(BatchProjectId, hidBatchProjectId.Value);
                for (int i = 0; i < BatchProjectId.Count; i++)
                {
                    BLL.Project project = BLL.Project.GetProject(BatchProjectId[i]);
                    project.ProductionVideoEditCheck = new Guid(this.ddlProductionVideoEditCheck.SelectedValue);
                    project.ProductionAudioEditCheck = new Guid(this.ddlProductionAudioEditCheck.SelectedValue);
                    project.ProductionProductCheck = new Guid(this.ddlProductionProductCheck.SelectedValue);
                    project.ProductionIsTimely = new Guid(this.ddlProductionIsTimely.SelectedValue);
                    project.ProductionCheckDate = DateTime.Now;
                    project.ProductionCheckNote = this.txtProductionCheckNote.Text.ToString();
                    project.ProductionProgress = project.ProductionProgress;
                    project.progress = project.progress;
                    BLL.Project.UpdateProductionCheck(project);
                }
                this.Redirect("~/pages/MyTask.aspx?mode=productioncheck");
            }
            #endregion
            #region
            else if (this.Request["mode"] == "productioncheckbatchhandle")
            {
                List<Guid> BatchProjectId = new List<Guid>();
                BatchProjectId = ProjectCollection.Common.SerializeObj.Desrialize(BatchProjectId, hidBatchProjectId.Value);
                for (int i = 0; i < BatchProjectId.Count; i++)
                {
                    BLL.Project project = BLL.Project.GetProject(BatchProjectId[i]);
                    if (project.ProjectTypeId == new Guid("00000000-0000-0000-0000-000000000199"))
                    {
                        UserName = LoginUserInfo.LoginName;
                        PassWord = LoginUserInfo.Password;
                        byte[] bytes = Encoding.Default.GetBytes(UserName + "_" + PassWord);
                        string str = Convert.ToBase64String(bytes);
                        string url = "http://newpms.cei.cn/PlayVideo" + "?link=" + str;
                        Encoding encoding = Encoding.GetEncoding("utf-8");
                        IDictionary<string, string> parameters = new Dictionary<string, string>();
                        parameters.Add("projectId", project.ProjectNo);
                        //parameters.Add("X-CSRFToken", HttpContext.Current.Request.Cookies["csrftoken"].Value);
                        HttpWebResponse response = ProjectCollection.Common.PostRequest.CreatePostHttpResponse(url, parameters, encoding);
                        //打印返回值  
                        Stream stream = response.GetResponseStream();   //获取响应的字符串流  
                        StreamReader sr = new StreamReader(stream); //创建一个stream读取流  
                        string request = sr.ReadToEnd();   //从头读到尾，放到字符串
                        if (request == "审核完毕")
                        {
                            message.Text = request;
                            message.Visible = true;
                            UpdateProductionCheck(project);
                        }
                        else
                        {
                            message.Text = "提交错误";
                            message.Visible = true;
                        }
                    }
                    else
                    {
                        UpdateProductionCheck(project);
                    }
                }
                this.Redirect("~/pages/MyTask.aspx?mode=productioncheck");
            }
            #endregion
            #region
            else if (this.Request["mode"] == "publish")
            {
                BLL.Project project = BLL.Project.GetProject(new Guid(this.hidProjectId.Value.ToString()));
                project.PublishOperator = this.LoginUserInfo.Identity;
                project.PublishPublishDate = DateTime.Now;
                project.PublishTopNewsNeeds = new Guid(this.ddlPublishTopNewsNeeds.SelectedValue);
                project.PublishNoticeNeeds = new Guid(this.ddlPublishNoticeNeeds.SelectedValue);
                project.PublishCommonCategory = this.txtPublishCommonCategory.Text.ToString();
                project.PublishGovernmentCategory = this.txtPublishGovernmentCategory.Text.ToString();
                project.PublishFinanceCategory = this.txtPublishFinanceCategory.Text.ToString();
                project.PublishBankCategory = this.txtPublishBankCategory.Text.ToString();
                project.PublishPageState = new Guid(this.ddlPublishPageState.SelectedValue);
                project.PublishPlayState = new Guid(this.ddlPublishPlayState.SelectedValue);
                project.PublishNote = this.txtPublishNote.Text.ToString();
                project.progress = new Guid("00000000-0000-0000-0000-000000000118");
                BLL.Project.UpdatePublish(project);
                this.Redirect("~/pages/MyTask.aspx?mode=publish");
            }
            #endregion
            #region
            else if (this.Request["mode"] == "check")
            {
                BLL.Project project = BLL.Project.GetProject(new Guid(this.hidProjectId.Value.ToString()));
                project.CheckPersonInCharge = this.LoginUserInfo.Identity;
                project.CheckTaskCheckDate = DateTime.Now;
                project.CheckTaskCourseCommend = new Guid(this.ddlCheckTaskCourseCommend.SelectedValue);
                project.CheckTaskCategoryCheck = new Guid(this.ddlCheckTaskCategoryCheck.SelectedValue);
                project.CheckTaskNote = this.txtCheckTaskNote.Text.ToString();
                project.progress = new Guid("00000000-0000-0000-0000-000000000119");
                BLL.Project.UpdateCheck(project);
                this.Redirect("~/pages/MyTask.aspx?mode=check");
            }
            #endregion
            else
            {
            }
        }
        protected void btnReceive_Click(object sender, EventArgs e)
        {
            #region
            if (this.Request["mode"] == "capture")
            {
                BLL.Project Project = BLL.Project.GetProject(new Guid(this.hidProjectId.Value.ToString()));
                Project.CapturePersonInCharge = this.LoginUserInfo.Identity;
                Project.CaptureReceiveDate = DateTime.Now;
                Project.progress = new Guid("00000000-0000-0000-0000-000000000108");
                BLL.Project.UpdateCaptureReceive(Project);
                this.Redirect("~/pages/MyTask.aspx?mode=capture");
            }
            else if (this.Request["mode"] == "capturebatchhandle")
            {
                List<Guid> BatchProjectId = new List<Guid>();
                BatchProjectId = ProjectCollection.Common.SerializeObj.Desrialize(BatchProjectId, hidBatchProjectId.Value);
                for (int i = 0; i < BatchProjectId.Count; i++)
                {
                    BLL.Project Project = BLL.Project.GetProject(BatchProjectId[i]);
                    Project.CapturePersonInCharge = this.LoginUserInfo.Identity;
                    Project.CaptureReceiveDate = DateTime.Now;
                    Project.progress = new Guid("00000000-0000-0000-0000-000000000108");
                    BLL.Project.UpdateCaptureReceive(Project);
                }
                this.Redirect("~/pages/MyTask.aspx?mode=capture");
            }
            #endregion
            #region
            else if (this.Request["mode"] == "contentreceive")
            {
                BLL.Project Project = BLL.Project.GetProject(new Guid(this.hidProjectId.Value.ToString()));
                Project.ContentPersonInCharge = this.LoginUserInfo.Identity;
                Project.ContentOperator = new Guid(this.ddlContentOperator.SelectedValue);
                Project.ContentAssignmentDate = DateTime.Now;
                Project.ContentEstimatedDate = Convert.ToDateTime(this.hidContentEstimatedDate.Value);
                Project.ContentReceiveNote = this.txtContentReceiveNote.Text.ToString();
                Project.progress = new Guid("00000000-0000-0000-0000-000000000111");
                Project.ContentProgress = new Guid("00000000-0000-0000-0000-000000000111");
                BLL.Project.UpdateContentReceive(Project);
                this.Redirect("~/pages/MyTask.aspx?mode=contentreceive");
            }
            #endregion
            #region
            else if (this.Request["mode"] == "contentreceivebatchhandle")
            {
                List<Guid> BatchProjectId = new List<Guid>();
                BatchProjectId = ProjectCollection.Common.SerializeObj.Desrialize(BatchProjectId, hidBatchProjectId.Value);
                for (int i = 0; i < BatchProjectId.Count; i++)
                {
                    BLL.Project Project = BLL.Project.GetProject(BatchProjectId[i]);
                    Project.ContentPersonInCharge = this.LoginUserInfo.Identity;
                    Project.ContentOperator = new Guid(this.ddlContentOperator.SelectedValue);
                    Project.ContentAssignmentDate = DateTime.Now;
                    Project.ContentEstimatedDate = Convert.ToDateTime(this.hidContentEstimatedDate.Value);
                    Project.ContentReceiveNote = this.txtContentReceiveNote.Text.ToString();
                    Project.progress = new Guid("00000000-0000-0000-0000-000000000111");
                    Project.ContentProgress = new Guid("00000000-0000-0000-0000-000000000111");
                    BLL.Project.UpdateContentReceive(Project);
                }
                this.Redirect("~/pages/MyTask.aspx?mode=contentreceive");
            }
            #endregion
            #region
            else if (this.Request["mode"] == "productionreceive")
            {
                BLL.Project project = BLL.Project.GetProject(new Guid(this.hidProjectId.Value.ToString()));
                UpdateProductionReceive(project);
                this.Redirect("~/pages/MyTask.aspx?mode=productionreceive");
            }
            #endregion
            #region
            else if (this.Request["mode"] == "productionreceivebatchhandle")
            {
                List<Guid> BatchProjectId = new List<Guid>();
                BatchProjectId = ProjectCollection.Common.SerializeObj.Desrialize(BatchProjectId, hidBatchProjectId.Value);
                for (int i = 0; i < BatchProjectId.Count; i++)
                {
                    BLL.Project project = BLL.Project.GetProject(BatchProjectId[i]);
                    UpdateProductionReceive(project);
                }
                this.Redirect("~/pages/MyTask.aspx?mode=productionreceive");
            }
            #endregion
            else
            {

            }
        }
        protected void btnSentBack_Click(object sender, EventArgs e)
        {
            if (this.Request["mode"] == "contentcheck")
            {
                BLL.Project project = BLL.Project.GetProject(new Guid(this.hidProjectId.Value.ToString()));
                project.ContentCourseIntroductionQuality = new Guid(this.ddlContentCourseIntroductionQuality.SelectedValue);
                project.ContentResumeQuality = new Guid(this.ddlContentResumeQuality.SelectedValue);
                project.ContentPPTQuality = new Guid(this.ddlContentPPTQuality.SelectedValue);
                project.ContentExercisesQuality = new Guid(this.ddlContentExercisesQuality.SelectedValue);
                project.ContentTextQuality = new Guid(this.ddlContentTextQuality.SelectedValue);
                project.ContentIsTimely = new Guid(this.ddlContentIsTimely.SelectedValue);
                project.ContentCheckDate = DateTime.Now;
                project.ContentCheckNote = this.txtContentCheckNote.Text.ToString();
                project.progress = new Guid("00000000-0000-0000-0000-000000000124");
                project.ContentProgress = new Guid("00000000-0000-0000-0000-000000000124");
                BLL.Project.UpdateContentCheck(project);
                this.Redirect("~/pages/MyTask.aspx?mode=contentcheck");
            }
            else if (this.Request["mode"] == "contentrecheck")
            {
                BLL.Project project = BLL.Project.GetProject(new Guid(this.hidProjectId.Value.ToString()));
                project.ContentRecheckPersonInCharge = this.LoginUserInfo.Identity;
                project.ContentRecheckDate = DateTime.Now;
                project.ContentRecheckNote = this.txtContentRecheckNote.Text.ToString();
                project.progress = new Guid("00000000-0000-0000-0000-000000000123");
                project.ContentProgress = new Guid("00000000-0000-0000-0000-000000000123");
                BLL.Project.UpdateContentRecheckFinish(project);
                this.Redirect("~/pages/MyTask.aspx?mode=contentrecheck");
            }
            else if (this.Request["mode"] == "productioncheck")
            {
                BLL.Project project = BLL.Project.GetProject(new Guid(this.hidProjectId.Value.ToString()));
                project.ProductionVideoEditCheck = new Guid(this.ddlProductionVideoEditCheck.SelectedValue);
                project.ProductionAudioEditCheck = new Guid(this.ddlProductionAudioEditCheck.SelectedValue);
                project.ProductionProductCheck = new Guid(this.ddlProductionProductCheck.SelectedValue);
                project.ProductionIsTimely = new Guid(this.ddlProductionIsTimely.SelectedValue);
                project.ProductionCheckDate = DateTime.Now;
                project.ProductionCheckNote = this.txtProductionCheckNote.Text.ToString();
                project.ProductionProgress = new Guid("00000000-0000-0000-0000-000000000125");
                project.progress = new Guid("00000000-0000-0000-0000-000000000125");
                BLL.Project.UpdateProductionCheck(project);
                this.Redirect("~/pages/MyTask.aspx?mode=productioncheck");
            }
            else if (this.Request["mode"] == "capture")//采集延时接收
            {
                BLL.Project project = BLL.Project.GetProject(new Guid(this.hidProjectId.Value.ToString()));
                project.progress = new Guid("00000000-0000-0000-0000-000000000131");
                project.CapturePersonInCharge = this.LoginUserInfo.Identity;
                project.CaptureReceiveDelayDate = DateTime.Now;
                project.CaptureReceiveDelayNote = this.txtCaptureReceiveDelayNote.Text.ToString();
                BLL.Project.UpdateCaptureDelayReceive(project);
                this.Redirect("~/pages/MyTask.aspx?mode=capturereceive");
            }
            else if (this.Request["mode"] == "capturebatchhandle")//批量采集延时接收
            {
                List<Guid> BatchProjectId = new List<Guid>();
                BatchProjectId = ProjectCollection.Common.SerializeObj.Desrialize(BatchProjectId, hidBatchProjectId.Value);
                for (int i = 0; i < BatchProjectId.Count; i++)
                {
                    BLL.Project project = BLL.Project.GetProject(BatchProjectId[i]);
                    project.progress = new Guid("00000000-0000-0000-0000-000000000131");
                    project.CapturePersonInCharge = this.LoginUserInfo.Identity;
                    project.CaptureReceiveDelayDate = DateTime.Now;
                    project.CaptureReceiveDelayNote = this.txtCaptureReceiveDelayNote.Text.ToString();
                    BLL.Project.UpdateCaptureDelayReceive(project);
                }
                this.Redirect("~/pages/MyTask.aspx?mode=capturereceive");
            }
            else if (this.Request["mode"] == "productionreceive")//技术部延时接收
            {
                BLL.Project project = BLL.Project.GetProject(new Guid(this.hidProjectId.Value.ToString()));
                UpdateProductionDelayReceive(project);
                this.Redirect("~/pages/MyTask.aspx?mode=productionreceive");
            }
            else if (this.Request["mode"] == "productionreceivebatchhandle")//批量技术部延时接收
            {
                List<Guid> BatchProjectId = new List<Guid>();
                BatchProjectId = ProjectCollection.Common.SerializeObj.Desrialize(BatchProjectId, hidBatchProjectId.Value);
                for (int i = 0; i < BatchProjectId.Count; i++)
                {
                    BLL.Project project = BLL.Project.GetProject(BatchProjectId[i]);
                    UpdateProductionDelayReceive(project);
                }
                this.Redirect("~/pages/MyTask.aspx?mode=productionreceive");
            }
            else if (this.Request["mode"] == "productionfinish")//技术部接收退回 
            {
                using (var ProjectModel = new ProjectCollection.WebUI.Models.ProjectCollectionEntities())
                {
                    ProjectCollection.WebUI.Models.Project ThisProject = (from p in ProjectModel.Project
                                                                          where p.ProjectId.ToString() == this.hidProjectId.Value.ToString()
                                                                          select p).First();
                    ThisProject.progress = new Guid("00000000-0000-0000-0000-000000000106");
                    ThisProject.ProductionProgress = new Guid("00000000-0000-0000-0000-000000000106");
                    ProjectModel.SaveChanges();
                }
                this.Redirect("~/pages/MyTask.aspx?mode=manufacture&range=now");
            }
            else if (this.Request["mode"] == "publish")//新课件发布环节退回制作部复审
            {
                using (var ProjectModel = new ProjectCollection.WebUI.Models.ProjectCollectionEntities())
                {
                    ProjectCollection.WebUI.Models.Project ThisProject = (from p in ProjectModel.Project
                                                                          where p.ProjectId.ToString() == this.hidProjectId.Value.ToString()
                                                                          select p).First();
                    if (ThisProject.ProjectTypeId == new Guid("00000000-0000-0000-0000-000000000199"))
                    {
                        ThisProject.progress = new Guid("00000000-0000-0000-0000-000000000122");
                        ThisProject.ContentProgress = new Guid("00000000-0000-0000-0000-000000000122");
                        ThisProject.PublishNote = this.txtPublishNote.Text.ToString();
                        ProjectModel.SaveChanges();
                    }
                }
                this.Redirect("~/pages/MyTask.aspx?mode=manufacture&range=now");
            }
            else { }
        }
        protected void btnDelete_Click(object sender, EventArgs e)
        { }
        protected void btnDiscard_Click(object sender, EventArgs e)
        {
            BLL.Project project = BLL.Project.GetProject(new Guid(this.hidProjectId.Value.ToString()));
            project.CaptureCheckPersonInCharge = this.LoginUserInfo.Identity;
            project.CaptureCheckDate = DateTime.Now;
            project.CaptureCheckNote = this.txtCaptureCheckNote.Text.ToString();
            project.progress = new Guid("00000000-0000-0000-0000-000000000128");
            BLL.Project.UpdateCaptureCheckFinish(project);
            this.Redirect("~/pages/MyTask.aspx?mode=capturecheck");
        }
        protected void btnNewOkNew_Click(object sender, EventArgs e)
        {
            if (this.Request["mode"] == "capturebatchhandle")
            {
                List<Guid> BatchProjectId = new List<Guid>();
                BatchProjectId = ProjectCollection.Common.SerializeObj.Desrialize(BatchProjectId, hidBatchProjectId.Value);
                //List OP
                DataTable tblDatas = new DataTable("Datas");
                DataColumn dc = null;
                dc = tblDatas.Columns.Add("id", Type.GetType("System.String"));
                dc = tblDatas.Columns.Add("title", Type.GetType("System.String"));
                dc = tblDatas.Columns.Add("result", Type.GetType("System.String"));
                //List ED
                for (int i = 0; i < BatchProjectId.Count; i++)
                {
                    BLL.Project project = BLL.Project.GetProject(BatchProjectId[i]);
                    project.CaptureDuration = Convert.ToInt16(this.txtCaptureDuration.Text.ToString());
                    project.CaptureFinishDate = DateTime.Now;
                    project.CaptureVideoNeeds = new Guid(this.ddlCaptureVideoNeeds.SelectedValue);
                    project.CaptureAudioNeeds = new Guid(this.ddlCaptureAudioNeeds.SelectedValue);
                    project.CaptureVideoVideoQuality = new Guid(this.ddlCaptureVideoVideoQuality.SelectedValue);
                    project.CaptureVideoAudioQuality = new Guid(this.ddlCaptureVideoAudioQuality.SelectedValue);
                    project.CaptureAudioQuality = new Guid(this.ddlCaptureAudioQuality.SelectedValue);
                    project.CaptureSoundTrack = new Guid(this.ddlCaptureSoundTrack.SelectedValue);
                    project.CaptureFilePath = this.txtCaptureFilePath.Text.ToString();
                    project.CaptureNote = this.txtCaptureNote.Text.ToString();
                    //New OP
                    UserName = LoginUserInfo.LoginName;
                    PassWord = LoginUserInfo.Password;
                    byte[] bytes = Encoding.Default.GetBytes(UserName + "_" + PassWord);
                    string str = Convert.ToBase64String(bytes);
                    string url = @"http://newpms.cei.cn/FTPVideoUpload?ProjectNo="
                        + HttpUtility.UrlEncode(project.ProjectNo)
                        + "&title="
                        + HttpUtility.UrlEncode(project.CourseName)
                        + "&lecturer="
                        + HttpUtility.UrlEncode(project.lecturer)
                        + "&post="
                        + HttpUtility.UrlEncode(project.LecturerJob)
                        + "&type=NoSlideAudio"
                        + "&link="
                        + str;
                    //
                    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                    request.Method = "GET";
                    request.ContentType = "text/html;charset=UTF-8";
                    HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                    Stream myResponseStream = response.GetResponseStream();
                    StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding("utf-8"));
                    string retString = myStreamReader.ReadToEnd();
                    myStreamReader.Close();
                    myResponseStream.Close();
                    //
                    JObject jo = (JObject)JsonConvert.DeserializeObject(retString);
                    if (jo["status"].ToString() == "文件完备" && jo["data"].ToString() == "数据添加成功")
                    {
                        project.progress = new Guid("00000000-0000-0000-0000-000000000120");
                        BLL.Project.UpdateCaptureFinish(project);
                        using (var ProjectModel = new ProjectCollection.WebUI.Models.ProjectCollectionEntities())
                        {
                            ProjectCollection.WebUI.Models.Project ThisProject = (from p in ProjectModel.Project
                                                                                  where p.ProjectId == project.ProjectId
                                                                                  select p).First();
                            ThisProject.MakeType = "new";
                            ProjectModel.SaveChanges();
                        }
                        DataRow newRow;
                        newRow = tblDatas.NewRow();
                        newRow["id"] = project.ProjectNo;
                        newRow["title"] = project.CourseName;
                        newRow["result"] = "成功";
                        tblDatas.Rows.Add(newRow);
                    }
                    else
                    {
                        DataRow newRow;
                        newRow = tblDatas.NewRow();
                        newRow["id"] = project.ProjectNo;
                        newRow["title"] = project.CourseName;
                        newRow["result"] = "失败";
                        tblDatas.Rows.Add(newRow);
                    }
                    //New ED
                    btnNewOkNew.Visible = false;
                    PanelResult.Visible = true;
                    TableResult.DataSource = tblDatas;
                    TableResult.DataBind();
                }
            }
            else if (this.Request["mode"] == "productionfinishbatchhandle")
            {
                List<Guid> BatchProjectId = new List<Guid>();
                BatchProjectId = ProjectCollection.Common.SerializeObj.Desrialize(BatchProjectId, hidBatchProjectId.Value);
                DataTable tblDatas = new DataTable("Datas");
                DataColumn dc = null;
                dc = tblDatas.Columns.Add("id", Type.GetType("System.String"));
                dc = tblDatas.Columns.Add("title", Type.GetType("System.String"));
                dc = tblDatas.Columns.Add("result", Type.GetType("System.String"));
                for (int i = 0; i < BatchProjectId.Count; i++)
                {
                    BLL.Project project = BLL.Project.GetProject(BatchProjectId[i]);
                    UserName = LoginUserInfo.LoginName;
                    PassWord = LoginUserInfo.Password;
                    byte[] bytes = Encoding.Default.GetBytes(UserName + "_" + PassWord);
                    string str = Convert.ToBase64String(bytes);
                    string url = @"http://newpms.cei.cn/FTPVideoUpload?ProjectNo="
                        + HttpUtility.UrlEncode(project.ProjectNo)
                        + "&title="
                        + HttpUtility.UrlEncode(project.CourseName)
                        + "&lecturer="
                        + HttpUtility.UrlEncode(project.lecturer)
                        + "&post="
                        + HttpUtility.UrlEncode(project.LecturerJob)
                        + "&type=NoSlideVideo"
                        + "&link="
                        + str;
                    //
                    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                    request.Method = "GET";
                    request.ContentType = "text/html;charset=UTF-8";
                    HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                    Stream myResponseStream = response.GetResponseStream();
                    StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding("utf-8"));
                    string retString = myStreamReader.ReadToEnd();
                    myStreamReader.Close();
                    myResponseStream.Close();
                    //
                    JObject jo = (JObject)JsonConvert.DeserializeObject(retString);
                    DataRow newRow;
                    newRow = tblDatas.NewRow();
                    newRow["id"] = project.ProjectNo;
                    newRow["title"] = project.CourseName;
                    if (jo["status"].ToString() == "文件完备")
                    {
                        UpdateProductionFinish(project);                    
                        newRow["result"] = "成功";
                    }
                    else
                    {
                        newRow["result"] = "失败";    
                    }
                    tblDatas.Rows.Add(newRow);
                }
                btnNewOkNew.Visible = false;
                PanelResult.Visible = true;
                TableResult.DataSource = tblDatas;
                TableResult.DataBind();
            }
        }
        protected void btnBack_Click(object sender, EventArgs e)
        {
            this.Redirect("~/pages/MyTask.aspx");
        }
        protected void TableResult_RowDataBound(object sender, EventArgs e)
        {

        }
        protected void rblDeadLineSelectSetChanged(object sender, EventArgs e)
        { }

        //protected void BtnUploadVideo_Click(object sender, EventArgs e)
        //{
        //    string url = "http://newpms.cei.cn/JsonLogin";
        //    Encoding encoding = Encoding.GetEncoding("utf-8");
        //    IDictionary<string, string> parameters = new Dictionary<string, string>();
        //    parameters.Add("UserName", "cc");
        //    parameters.Add("PassWord", "abc123");
        //    parameters.Add("X-CSRFToken", HttpContext.Current.Request.Cookies["csrftoken"].Value);
        //    HttpWebResponse response = ProjectCollection.Common.PostRequest.CreatePostHttpResponse(url, parameters, encoding);
        //    //打印返回值  
        //    Stream stream = response.GetResponseStream();   //获取响应的字符串流  
        //    StreamReader sr = new StreamReader(stream); //创建一个stream读取流  
        //    string request = sr.ReadToEnd();   //从头读到尾，放到字符串html
        //    LabelUploadVideo.Text = request;
        //}

        #endregion 事件

        #region 方法
        //
        private void InitDropDownList()
        {
            this.ddlProjectType.DataSource = BLL.DataDictionary.GetDataByCategory("ProjectType");
            this.ddlProjectType.DataTextField = "text";
            this.ddlProjectType.DataValueField = "dictionary_identity";
            this.ddlProjectType.DataBind();
            //
            this.ddlemergency.DataSource = BLL.DataDictionary.GetDataByCategory("emergency");
            this.ddlemergency.DataTextField = "text";
            this.ddlemergency.DataValueField = "dictionary_identity";
            this.ddlemergency.DataBind();
            //
            this.ddlWorkType.DataSource = BLL.DataDictionary.GetDataByCategory("WorkType");
            this.ddlWorkType.DataTextField = "text";
            this.ddlWorkType.DataValueField = "dictionary_identity";
            this.ddlWorkType.DataBind();
            //
            this.ddlnotice.DataSource = BLL.DataDictionary.GetDataByCategory("IsNotice");
            this.ddlnotice.DataTextField = "text";
            this.ddlnotice.DataValueField = "dictionary_identity";
            this.ddlnotice.DataBind();
            //
            this.ddlHeadLine.DataSource = BLL.DataDictionary.GetDataByCategory("IsHeadline");
            this.ddlHeadLine.DataTextField = "text";
            this.ddlHeadLine.DataValueField = "dictionary_identity";
            this.ddlHeadLine.DataBind();
            //
            this.ddlContentNeeds.DataSource = BLL.DataDictionary.GetDataByCategory("IsNot");
            this.ddlContentNeeds.DataTextField = "text";
            this.ddlContentNeeds.DataValueField = "dictionary_identity";
            this.ddlContentNeeds.DataBind();
            //
            this.ddlPublishNeeds.DataSource = BLL.DataDictionary.GetDataByCategory("IsNot");
            this.ddlPublishNeeds.DataTextField = "text";
            this.ddlPublishNeeds.DataValueField = "dictionary_identity";
            this.ddlPublishNeeds.DataBind();
        }
        private void InitDropDownListCapture()
        {
            this.ddlCaptureSoundTrack.DataSource = BLL.DataDictionary.GetDataByCategory("CaptureSoundTrack");
            this.ddlCaptureSoundTrack.DataTextField = "text";
            this.ddlCaptureSoundTrack.DataValueField = "dictionary_identity";
            this.ddlCaptureSoundTrack.DataBind();
            //
            this.ddlCaptureVideoNeeds.DataSource = BLL.DataDictionary.GetDataByCategory("IsNot");
            this.ddlCaptureVideoNeeds.DataTextField = "text";
            this.ddlCaptureVideoNeeds.DataValueField = "dictionary_identity";
            this.ddlCaptureVideoNeeds.DataBind();
            //
            this.ddlCaptureAudioNeeds.DataSource = BLL.DataDictionary.GetDataByCategory("IsNot");
            this.ddlCaptureAudioNeeds.DataTextField = "text";
            this.ddlCaptureAudioNeeds.DataValueField = "dictionary_identity";
            this.ddlCaptureAudioNeeds.DataBind();
            //
            this.ddlCaptureVideoAudioQuality.DataSource = BLL.DataDictionary.GetDataByCategory("Quality");
            this.ddlCaptureVideoAudioQuality.DataTextField = "text";
            this.ddlCaptureVideoAudioQuality.DataValueField = "dictionary_identity";
            this.ddlCaptureVideoAudioQuality.DataBind();
            //
            this.ddlCaptureVideoVideoQuality.DataSource = BLL.DataDictionary.GetDataByCategory("Quality");
            this.ddlCaptureVideoVideoQuality.DataTextField = "text";
            this.ddlCaptureVideoVideoQuality.DataValueField = "dictionary_identity";
            this.ddlCaptureVideoVideoQuality.DataBind();
            //
            this.ddlCaptureAudioQuality.DataSource = BLL.DataDictionary.GetDataByCategory("Quality");
            this.ddlCaptureAudioQuality.DataTextField = "text";
            this.ddlCaptureAudioQuality.DataValueField = "dictionary_identity";
            this.ddlCaptureAudioQuality.DataBind();
        }
        private void InitDropDownListShorthand()
        {
            this.ddlShorthandQuality.DataSource = BLL.DataDictionary.GetDataByCategory("Quality");
            this.ddlShorthandQuality.DataTextField = "text";
            this.ddlShorthandQuality.DataValueField = "dictionary_identity";
            this.ddlShorthandQuality.DataBind();
        }
        private void InitDropDownListContentReceive()
        {
            this.ddlContentOperator.DataSource = BLL.UserInfo.GetDataByID();
            this.ddlContentOperator.DataTextField = "real_name";
            this.ddlContentOperator.DataValueField = "user_Identity";
            this.ddlContentOperator.DataBind();
        }
        private void InitDropDownListContentFinish()
        {
            this.ddlContentCourseNameConfirm.DataSource = BLL.DataDictionary.GetDataByCategory("IsNot");
            this.ddlContentCourseNameConfirm.DataTextField = "text";
            this.ddlContentCourseNameConfirm.DataValueField = "dictionary_identity";
            this.ddlContentCourseNameConfirm.DataBind();
            //
            this.ddlContentCourseRecommend.DataSource = BLL.DataDictionary.GetDataByCategory("IsNot");
            this.ddlContentCourseRecommend.DataTextField = "text";
            this.ddlContentCourseRecommend.DataValueField = "dictionary_identity";
            this.ddlContentCourseRecommend.DataBind();
            //
            this.ddlContentPPTAdvice.DataSource = BLL.DataDictionary.GetDataByCategory("IsNot");
            this.ddlContentPPTAdvice.DataTextField = "text";
            this.ddlContentPPTAdvice.DataValueField = "dictionary_identity";
            this.ddlContentPPTAdvice.DataBind();
            //
            this.ddlContentPPTNeeds.DataSource = BLL.DataDictionary.GetDataByCategory("ContentPPTNeeds");
            this.ddlContentPPTNeeds.DataTextField = "text";
            this.ddlContentPPTNeeds.DataValueField = "dictionary_identity";
            this.ddlContentPPTNeeds.DataBind();
            //
            this.ddlContentCourseIntroNeeds.DataSource = BLL.DataDictionary.GetDataByCategory("IsNot");
            this.ddlContentCourseIntroNeeds.DataTextField = "text";
            this.ddlContentCourseIntroNeeds.DataValueField = "dictionary_identity";
            this.ddlContentCourseIntroNeeds.DataBind();
            //
            this.ddlContentLecturerResumeNeeds.DataSource = BLL.DataDictionary.GetDataByCategory("IsNot");
            this.ddlContentLecturerResumeNeeds.DataTextField = "text";
            this.ddlContentLecturerResumeNeeds.DataValueField = "dictionary_identity";
            this.ddlContentLecturerResumeNeeds.DataBind();
            //
            this.ddlContentExercisesNeeds.DataSource = BLL.DataDictionary.GetDataByCategory("IsNot");
            this.ddlContentExercisesNeeds.DataTextField = "text";
            this.ddlContentExercisesNeeds.DataValueField = "dictionary_identity";
            this.ddlContentExercisesNeeds.DataBind();
            //
            this.ddlContentTextEditNeeds.DataSource = BLL.DataDictionary.GetDataByCategory("IsNot");
            this.ddlContentTextEditNeeds.DataTextField = "text";
            this.ddlContentTextEditNeeds.DataValueField = "dictionary_identity";
            this.ddlContentTextEditNeeds.DataBind();
            //
        }
        private void InitDropDownListContentCheck()
        {
            this.ddlContentCourseIntroductionQuality.DataSource = BLL.DataDictionary.GetDataByCategory("Quality");
            this.ddlContentCourseIntroductionQuality.DataTextField = "text";
            this.ddlContentCourseIntroductionQuality.DataValueField = "dictionary_identity";
            this.ddlContentCourseIntroductionQuality.DataBind();
            //
            this.ddlContentResumeQuality.DataSource = BLL.DataDictionary.GetDataByCategory("Quality");
            this.ddlContentResumeQuality.DataTextField = "text";
            this.ddlContentResumeQuality.DataValueField = "dictionary_identity";
            this.ddlContentResumeQuality.DataBind();
            //
            this.ddlContentPPTQuality.DataSource = BLL.DataDictionary.GetDataByCategory("Quality");
            this.ddlContentPPTQuality.DataTextField = "text";
            this.ddlContentPPTQuality.DataValueField = "dictionary_identity";
            this.ddlContentPPTQuality.DataBind();
            //
            this.ddlContentExercisesQuality.DataSource = BLL.DataDictionary.GetDataByCategory("Quality");
            this.ddlContentExercisesQuality.DataTextField = "text";
            this.ddlContentExercisesQuality.DataValueField = "dictionary_identity";
            this.ddlContentExercisesQuality.DataBind();
            //
            this.ddlContentTextQuality.DataSource = BLL.DataDictionary.GetDataByCategory("Quality");
            this.ddlContentTextQuality.DataTextField = "text";
            this.ddlContentTextQuality.DataValueField = "dictionary_identity";
            this.ddlContentTextQuality.DataBind();
            //
            this.ddlContentIsTimely.DataSource = BLL.DataDictionary.GetDataByCategory("IsNot");
            this.ddlContentIsTimely.DataTextField = "text";
            this.ddlContentIsTimely.DataValueField = "dictionary_identity";
            this.ddlContentIsTimely.DataBind();
        }
        private void InitDropDownListProductionReceive(string type)
        {
            //this.ddlProductionOperator.DataSource = BLL.UserInfo.GetDataByRole(new Guid("00000000-0000-0000-0000-000000000009"));
            //
            DataTable newDataTable = new DataTable();
            if (type == "elite" || type == "micro")
            {
                DataTable dt1 = BLL.UserInfo.GetDataByRole(new Guid("00000000-0000-0000-0000-000000000027"));
                DataTable dt2 = BLL.UserInfo.GetDataByRole(new Guid("00000000-0000-0000-0000-000000000031"));
                newDataTable = dt1.Copy();
                foreach (DataRow dr in dt2.Rows)
                {
                    newDataTable.ImportRow(dr);
                }
            }
            else {
                DataTable dt1 = BLL.UserInfo.GetDataByRole(new Guid("00000000-0000-0000-0000-000000000009"));
                DataTable dt2 = BLL.UserInfo.GetDataByRole(new Guid("00000000-0000-0000-0000-000000000002"));
                newDataTable = dt1.Copy();
                foreach (DataRow dr in dt2.Rows)
                {
                    newDataTable.ImportRow(dr);
                }
            }
            this.ddlProductionOperator.DataSource = newDataTable;
            //
            this.ddlProductionOperator.DataTextField = "real_name";
            this.ddlProductionOperator.DataValueField = "user_Identity";
            this.ddlProductionOperator.DataBind();
        }
        private void InitDropDownListProductionFinish()
        {
            this.ddlProductionVideoQuality.DataSource = BLL.DataDictionary.GetDataByCategory("Quality");
            this.ddlProductionVideoQuality.DataTextField = "text";
            this.ddlProductionVideoQuality.DataValueField = "dictionary_identity";
            this.ddlProductionVideoQuality.DataBind();
            //
            this.ddlProductionAudioQuality.DataSource = BLL.DataDictionary.GetDataByCategory("Quality");
            this.ddlProductionAudioQuality.DataTextField = "text";
            this.ddlProductionAudioQuality.DataValueField = "dictionary_identity";
            this.ddlProductionAudioQuality.DataBind();
            //
            this.ddlProductionFileBackUp.DataSource = BLL.DataDictionary.GetDataByCategory("IsNot");
            this.ddlProductionFileBackUp.DataTextField = "text";
            this.ddlProductionFileBackUp.DataValueField = "dictionary_identity";
            this.ddlProductionFileBackUp.DataBind();
            //
        }
        private void InitDropDownListProductionCheck()
        {

            this.ddlProductionVideoEditCheck.DataSource = BLL.DataDictionary.GetDataByCategory("Quality");
            this.ddlProductionVideoEditCheck.DataTextField = "text";
            this.ddlProductionVideoEditCheck.DataValueField = "dictionary_identity";
            this.ddlProductionVideoEditCheck.DataBind();
            //
            this.ddlProductionAudioEditCheck.DataSource = BLL.DataDictionary.GetDataByCategory("Quality");
            this.ddlProductionAudioEditCheck.DataTextField = "text";
            this.ddlProductionAudioEditCheck.DataValueField = "dictionary_identity";
            this.ddlProductionAudioEditCheck.DataBind();
            //
            this.ddlProductionProductCheck.DataSource = BLL.DataDictionary.GetDataByCategory("Quality");
            this.ddlProductionProductCheck.DataTextField = "text";
            this.ddlProductionProductCheck.DataValueField = "dictionary_identity";
            this.ddlProductionProductCheck.DataBind();
            //
            this.ddlProductionIsTimely.DataSource = BLL.DataDictionary.GetDataByCategory("IsNot");
            this.ddlProductionIsTimely.DataTextField = "text";
            this.ddlProductionIsTimely.DataValueField = "dictionary_identity";
            this.ddlProductionIsTimely.DataBind();
        }
        private void InitDropDownListPublish()
        {
            this.ddlPublishTopNewsNeeds.DataSource = BLL.DataDictionary.GetDataByCategory("IsNot");
            this.ddlPublishTopNewsNeeds.DataTextField = "text";
            this.ddlPublishTopNewsNeeds.DataValueField = "dictionary_identity";
            this.ddlPublishTopNewsNeeds.DataBind();
            //
            this.ddlPublishNoticeNeeds.DataSource = BLL.DataDictionary.GetDataByCategory("IsNot");
            this.ddlPublishNoticeNeeds.DataTextField = "text";
            this.ddlPublishNoticeNeeds.DataValueField = "dictionary_identity";
            this.ddlPublishNoticeNeeds.DataBind();
            //
            this.ddlPublishPageState.DataSource = BLL.DataDictionary.GetDataByCategory("IsNot");
            this.ddlPublishPageState.DataTextField = "text";
            this.ddlPublishPageState.DataValueField = "dictionary_identity";
            this.ddlPublishPageState.DataBind();
            //
            this.ddlPublishPlayState.DataSource = BLL.DataDictionary.GetDataByCategory("IsNot");
            this.ddlPublishPlayState.DataTextField = "text";
            this.ddlPublishPlayState.DataValueField = "dictionary_identity";
            this.ddlPublishPlayState.DataBind();
        }
        private void InitDropDownListCheck()
        {
            this.ddlCheckTaskCourseCommend.DataSource = BLL.DataDictionary.GetDataByCategory("IsNot");
            this.ddlCheckTaskCourseCommend.DataTextField = "text";
            this.ddlCheckTaskCourseCommend.DataValueField = "dictionary_identity";
            this.ddlCheckTaskCourseCommend.DataBind();
            //
            this.ddlCheckTaskCategoryCheck.DataSource = BLL.DataDictionary.GetDataByCategory("IsNot");
            this.ddlCheckTaskCategoryCheck.DataTextField = "text";
            this.ddlCheckTaskCategoryCheck.DataValueField = "dictionary_identity";
            this.ddlCheckTaskCategoryCheck.DataBind();
        }
        //
        private void InitBrowseData()
        {
            BLL.Project project = BLL.Project.GetProject(new Guid(this.hidProjectId.Value.ToString()));
            BLL.ProjectPlan ProjectPlan = BLL.ProjectPlan.GetProjectPlan(project.ProjectPlanId);
            this.txtProjectNo.Text = project.ProjectNo;
            this.txtSendingDate.Text = project.SendingDate.ToString("yyyy-MM-dd");
            //
            this.ddlProjectType.SelectedIndex = ddlProjectType.Items.IndexOf(ddlProjectType.Items.FindByValue(project.ProjectTypeId.ToString()));
            //
            this.ddlWorkType.SelectedValue = project.WorkType.ToString();
            this.txtInCharge.Text = BLL.UserInfo.GetRealNameByID(project.InCharge);
            this.ddlemergency.SelectedValue = project.emergency.ToString();
            this.txtProjectPlanName.Text = project.ProjectPlanName;
            this.txtCourseName.Text = project.CourseName.ToString();
            this.txtlecturer.Text = project.lecturer.ToString();
            this.txtLecturerJob.Text = project.LecturerJob.ToString();
            this.ddlnotice.SelectedValue = project.notice.ToString();
            this.ddlHeadLine.SelectedValue = project.headline.ToString();
            this.txtTextCategory.Text = project.TextCategory.ToString();
            this.txtCreateNote.Text = project.CreateNote.ToString();
            this.txtExtraNote.Text = project.ExtraNote;
            this.ddlContentNeeds.SelectedValue = project.ContentNeeds.ToString();
            this.ddlPublishNeeds.SelectedValue = project.PublishNeeds.ToString();
            this.txtPlanNote.Text = ProjectPlan.Note.ToString();
            this.ddlEpisodeCount.SelectedValue = project.EpisodeCount.ToString();
            this.txtRecordingDate.Text= ProjectPlan.RecordingDate.ToString("yyyy-MM-dd");
            if (ProjectPlan.RecordingNote != null)
            {
                this.txtRecordingNote.Text = ProjectPlan.RecordingNote.ToString();
            }
            else { }
            using (var ProjectModel = new ProjectCollection.WebUI.Models.ProjectCollectionEntities())
            {
                ProjectCollection.WebUI.Models.Project ThisProject = (from p in ProjectModel.Project
                                                                           where p.ProjectId == project.ProjectId
                                                                      select p).First();
                if (ThisProject.ProjectTypeId == new Guid("00000000-0000-0000-0000-000000000017") && ThisProject.MakeType == "new")
                {
                    this.ddlProjectType.SelectedValue = "00000000-0000-0000-0000-000000000299";
                }
                if (ThisProject.CourseType == "micro")
                {
                    this.ddlProjectType.SelectedValue = "00000000-0000-0000-0000-000000000298";
                }
                if (ThisProject.CourseType == "elite")
                {
                    this.ddlProjectType.SelectedValue = "00000000-0000-0000-0000-000000000297";
                }
            }
            this.rblCanBeSold.SelectedValue = project.CanBeSold.ToString();
        }
        private void InitCaptureData()
        {
            BLL.Project project = BLL.Project.GetProject(new Guid(this.hidProjectId.Value.ToString()));
            try//未用Update的SQL语句更新过的列,查询时会出现错误
            {
                this.txtCaptureReceiveDelayDate.Text = project.CaptureReceiveDelayDate.ToString("yyyy-MM-dd HH:mm");
                this.txtCaptureReceiveDelayNote.Text = project.CaptureReceiveDelayNote.ToString();
            }
            catch
            {

            }
            finally
            {

            }
            this.txtCapturePersonInCharge.Text = BLL.UserInfo.GetRealNameByID(project.CapturePersonInCharge);
            this.txtCaptureReceiveDate.Text = project.CaptureReceiveDate.ToString("yyyy-MM-dd HH:mm");
            this.txtCaptureFinishDate.Text = project.CaptureFinishDate.ToString("yyyy-MM-dd HH:mm");
            this.txtCaptureDuration.Text = project.CaptureDuration.ToString();
            this.ddlCaptureSoundTrack.SelectedValue = project.CaptureSoundTrack.ToString();
            this.ddlCaptureAudioNeeds.SelectedValue = project.CaptureAudioNeeds.ToString();
            this.ddlCaptureVideoNeeds.SelectedValue = project.CaptureVideoNeeds.ToString();
            this.ddlCaptureVideoVideoQuality.SelectedValue = project.CaptureVideoVideoQuality.ToString();
            this.ddlCaptureVideoAudioQuality.SelectedValue = project.CaptureVideoAudioQuality.ToString();
            this.ddlCaptureAudioQuality.SelectedValue = project.CaptureAudioQuality.ToString();
            this.txtCaptureFilePath.Text = project.CaptureFilePath.ToString();
            this.txtCaptureNote.Text = project.CaptureNote.ToString();
        }
        private void InitCaptureCheckData()
        {
            BLL.Project project = BLL.Project.GetProject(new Guid(this.hidProjectId.Value.ToString()));
            this.txtCaptureCheckPersonInCharge.Text = BLL.UserInfo.GetRealNameByID(project.CaptureCheckPersonInCharge);
            this.txtCaptureCheckDate.Text = project.CaptureCheckDate.ToString("yyyy-MM-dd HH:mm");
            this.txtCaptureCheckNote.Text = project.CaptureCheckNote.ToString();
        }
        private void InitShorthandData()
        {
            BLL.Project project = BLL.Project.GetProject(new Guid(this.hidProjectId.Value.ToString()));
            this.txtShorthandPersonInCharge.Text = BLL.UserInfo.GetRealNameByID(project.ShorthandPersonInCharge);
            this.txtShorthandReceiveDate.Text = project.ShorthandReceiveDate.ToString("yyyy-MM-dd HH:mm");
            this.hidShorthandAudioReceiveDate.Value = project.ShorthandAudioReceiveDate.ToString("yyyy-MM-dd HH:mm");
            this.txtShorthandFinishDate.Text = project.ShorthandFinishDate.ToString("yyyy-MM-dd HH:mm");
            this.txtShorthandPurveyor.Text = project.ShorthandPurveyor;
            this.ddlShorthandQuality.SelectedValue = project.ShorthandQuality.ToString();
            this.txtShorthandNote.Text = project.ShorthandNote.ToString();
        }
        private void InitContentReceiveData()
        {
            BLL.Project project = BLL.Project.GetProject(new Guid(this.hidProjectId.Value.ToString()));
            this.txtContentPersonInCharge.Text = BLL.UserInfo.GetRealNameByID(project.ContentPersonInCharge);
            this.ddlContentOperator.SelectedValue = project.ContentOperator.ToString();
            this.txtContentReceiveNote.Text = project.ContentReceiveNote;
            this.txtContentReceiveDate.Text = project.ContentReceiveDate.ToString("yyyy-MM-dd HH:mm");
            this.txtContentAssignmentDate.Text = project.ContentAssignmentDate.ToString("yyyy-MM-dd HH:mm");
            this.hidContentEstimatedDate.Value = project.ContentEstimatedDate.ToString("yyyy-MM-dd HH:mm");
            //
            
        }
        private void InitContentFinishData()
        {
            BLL.Project project = BLL.Project.GetProject(new Guid(this.hidProjectId.Value.ToString()));
            this.txtContentFinishDate.Text = project.ContentFinishDate.ToString("yyyy-MM-dd HH:mm");
            this.txtContentDelayNote.Text = project.ContentDelayNote;
            this.ddlContentCourseNameConfirm.SelectedValue = project.ContentCourseNameConfirm.ToString();
            this.txtContentChangedCourseName.Text = project.ContentChangedCourseName;
            this.ddlContentCourseRecommend.SelectedValue = project.ContentCourseRecommend.ToString();
            this.ddlContentPPTAdvice.SelectedValue = project.ContentPPTAdvice.ToString();
            this.txtContentExercises.Text = project.ContentExercises.ToString();
            this.ddlContentPPTNeeds.SelectedValue = project.ContentPPTNeeds.ToString();
            this.ddlContentCourseIntroNeeds.SelectedValue = project.ContentCourseIntroNeeds.ToString();
            this.ddlContentLecturerResumeNeeds.SelectedValue = project.ContentLecturerResumeNeeds.ToString();
            this.ddlContentExercisesNeeds.SelectedValue = project.ContentExercisesNeeds.ToString();
            this.ddlContentTextEditNeeds.SelectedValue = project.ContentTextEditNeeds.ToString();
            this.txtContentOperateNote.Text = project.ContentOperateNote;
            //
        }
        private void InitContentCheckData()
        {
            BLL.Project project = BLL.Project.GetProject(new Guid(this.hidProjectId.Value.ToString()));
            string name = "";
            using (var ProjectModel = new ProjectCollection.WebUI.Models.ProjectCollectionEntities())
            {
                ProjectCollection.WebUI.Models.Project ThisProject = (from p in ProjectModel.Project
                                                                      where p.ProjectId.ToString() == project.ProjectId.ToString()
                                                                      select p).First();
                name = BLL.UserInfo.GetRealNameByID(new Guid(ThisProject.ContentCheckPersonInCharge.ToString()));   
            }
            this.txtContentCheckPersonInCharge.Text = name;
            this.ddlContentCourseIntroductionQuality.SelectedValue = project.ContentCourseIntroductionQuality.ToString();
            this.ddlContentResumeQuality.SelectedValue = project.ContentResumeQuality.ToString();
            this.ddlContentPPTQuality.SelectedValue = project.ContentPPTQuality.ToString();
            this.ddlContentExercisesQuality.SelectedValue = project.ContentExercisesQuality.ToString();
            this.ddlContentTextQuality.SelectedValue = project.ContentTextQuality.ToString();
            this.ddlContentIsTimely.SelectedValue = project.ContentIsTimely.ToString();
            this.txtContentCheckDate.Text = project.ContentCheckDate.ToString("yyyy-MM-dd HH:mm");
            this.txtContentCheckNote.Text = project.ContentCheckNote;
            using (var ProjectModel = new ProjectCollection.WebUI.Models.ProjectCollectionEntities())
            {
                ProjectCollection.WebUI.Models.Project ThisProject = (from p in ProjectModel.Project
                                                                      where p.ProjectId.ToString() == this.hidProjectId.Value.ToString()
                                                                      select p).First();
                this.ddlContentCheckScore.SelectedValue = ThisProject.ContentCheckScore;
                this.ddlContentCheckSlideScore.SelectedValue = ThisProject.ContentCheckSlideScore;
                this.txtLecturerNote.Text = ThisProject.LecturerNote;
            }
        }
        private void InitContentRecheckData()
        {
            BLL.Project project = BLL.Project.GetProject(new Guid(this.hidProjectId.Value.ToString()));
            this.txtContentRecheckPersonInCharge.Text = BLL.UserInfo.GetRealNameByID(project.ContentRecheckPersonInCharge);
            this.txtContentRecheckDate.Text = project.ContentRecheckDate.ToString("yyyy-MM-dd HH:mm");
            this.txtContentRecheckNote.Text = project.ContentRecheckNote.ToString();
        }
        private void InitProductionReceiveData()
        {
            BLL.Project project = BLL.Project.GetProject(new Guid(this.hidProjectId.Value.ToString()));
            try//未用Update的SQL语句更新过的列,查询时会出现错误
            {
                this.txtProductionReceiveDelayDate.Text = project.ProductionReceiveDelayDate.ToString("yyyy-MM-dd HH:mm");
                this.txtProductionReceiveDelayNote.Text = project.ProductionReceiveDelayNote.ToString();
            }
            catch
            {

            }
            finally
            {

            }
            this.txtProductionPersonInCharge.Text = BLL.UserInfo.GetRealNameByID(project.ProductionPersonInCharge);
            this.txtProductionReceiveDate.Text = project.ProductionReceiveDate.ToString("yyyy-MM-dd HH:mm");
            this.txtProductionReceiveNote.Text = project.ProductionReceiveNote.ToString();
            this.hidProductionAssignmentDate.Value = project.ProductionAssignmentDate.ToString("yyyy-MM-dd HH:mm");
            this.hidProductionEstimatedDate.Value = project.ProductionEstimatedDate.ToString("yyyy-MM-dd HH:mm");
            this.ddlProductionOperator.SelectedValue = project.ProductionOperator.ToString();
        }
        private void InitProductionFinishData()
        {
            BLL.Project project = BLL.Project.GetProject(new Guid(this.hidProjectId.Value.ToString()));
            this.txtProductionLastModifyDate.Text = project.ProductionLastModifyDate.ToString("yyyy-MM-dd HH:mm");
            this.txtProductionFinishDate.Text = project.ProductionFinishDate.ToString("yyyy-MM-dd HH:mm");
            this.txtProductionDelayNote.Text = project.ProductionDelayNote;
            this.ddlProductionVideoQuality.SelectedValue = project.ProductionVideoQuality.ToString();
            this.ddlProductionAudioQuality.SelectedValue = project.ProductionAudioQuality.ToString();
            this.ddlProductionFileBackUp.SelectedValue = project.ProductionFileBackUp.ToString();
            this.txtProductionOperateNote.Text = project.ProductionOperateNote;

        }
        private void InitProductionCheckData()
        {
            BLL.Project project = BLL.Project.GetProject(new Guid(this.hidProjectId.Value.ToString()));
            this.ddlProductionVideoEditCheck.SelectedValue = project.ProductionVideoEditCheck.ToString();
            this.ddlProductionAudioEditCheck.SelectedValue = project.ProductionAudioEditCheck.ToString();
            this.ddlProductionProductCheck.SelectedValue = project.ProductionProductCheck.ToString();
            this.ddlProductionIsTimely.SelectedValue = project.ProductionIsTimely.ToString();
            this.txtProductionCheckDate.Text = project.ProductionCheckDate.ToString("yyyy-MM-dd HH:mm");
            this.txtProductionCheckNote.Text = project.ProductionCheckNote;
        }
        private void InitPublishData()
        {
            BLL.Project project = BLL.Project.GetProject(new Guid(this.hidProjectId.Value.ToString()));
            this.txtPublishOperator.Text = BLL.UserInfo.GetRealNameByID(project.PublishOperator);
            this.txtPublishReceiveContentDate.Text = project.PublishReceiveContentDate.ToString("yyyy-MM-dd HH:mm");
            this.txtPublishReceiveProductionDate.Text = project.PublishReceiveProductionDate.ToString("yyyy-MM-dd HH:mm");
            this.txtPublishPublishDate.Text = project.PublishPublishDate.ToString("yyyy-MM-dd HH:mm");
            this.ddlPublishTopNewsNeeds.SelectedValue = project.PublishTopNewsNeeds.ToString();
            this.ddlPublishNoticeNeeds.SelectedValue = project.PublishNoticeNeeds.ToString();
            this.txtPublishCommonCategory.Text = project.PublishCommonCategory;
            this.txtPublishGovernmentCategory.Text = project.PublishGovernmentCategory;
            this.txtPublishFinanceCategory.Text = project.PublishFinanceCategory;
            this.txtPublishBankCategory.Text = project.PublishBankCategory;
            this.ddlPublishPageState.SelectedValue = project.PublishPageState.ToString();
            this.ddlPublishPlayState.SelectedValue = project.PublishPlayState.ToString();
            this.txtPublishNote.Text = project.PublishNote;
        }
        private void InitCheckData()
        {
            BLL.Project project = BLL.Project.GetProject(new Guid(this.hidProjectId.Value.ToString()));
            this.txtCheckPersonInCharge.Text = BLL.UserInfo.GetRealNameByID(project.CheckPersonInCharge);
            this.txtCheckTaskCheckDate.Text = project.CheckTaskCheckDate.ToString("yyyy-MM-dd HH:mm");
            this.ddlCheckTaskCourseCommend.SelectedValue = project.CheckTaskCourseCommend.ToString();
            this.ddlCheckTaskCategoryCheck.SelectedValue = project.CheckTaskCategoryCheck.ToString();
            this.txtCheckTaskNote.Text = project.CheckTaskNote;
        }
        //
        private void LoadProductionReceivePageDate(BLL.Project project)
        {
            this.btnReceive.ValidationGroup = "ProductionReceive";
            this.btnReceive.Visible = true;
            this.PanelProductionReceive.Visible = true;
            this.PanelCapture.Visible = true;
            this.InitBrowseData();
            if (!IsPostBack)
            {
                this.InitDropDownListProductionReceive(project.CourseType);
            }
            else { }
            try
            {
                InitDropDownListCapture();
                this.InitCaptureData();
            }
            catch
            {

            }
            finally
            {

            }
            //新三分屏
            if (project.ProjectTypeId == new Guid("00000000-0000-0000-0000-000000000199"))
            {

            }
            else
            {
                this.PanelContentReceive.Visible = true;
                this.PanelContentCheck.Visible = true;
                this.PanelContentRecheck.Visible = true;
                try
                {
                    InitDropDownListContentReceive();
                    this.InitContentReceiveData();
                }
                catch
                {

                }
                finally
                {

                }
                try
                {
                    this.InitContentCheckData();
                }
                catch
                {

                }
                finally
                {

                }
                try
                {
                    this.InitContentRecheckData();
                }
                catch
                {

                }
                finally
                {

                }
            }
        }
        private void LoadProductionFinishPageDate(BLL.Project project)
        {
            this.PanelProductionOperator.Visible = true;
            this.PanelCapture.Visible = true;
            this.PanelProductionReceive.Visible = true;
            //this.PanelProductionCheck.Visible = true;
            if (!IsPostBack)
            {
                InitDropDownListProductionFinish();
                try
                {
                    this.InitProductionCheckData();
                }
                catch
                {

                }
                finally
                {

                }
            }
            this.InitBrowseData();
            this.InitProductionReceiveData();
            try
            {
                InitDropDownListCapture();
                this.InitCaptureData();
            }
            catch
            {

            }
            finally
            {

            }          
            //新三分屏
            if (project.ProjectTypeId == new Guid("00000000-0000-0000-0000-000000000199"))
            {

            }
            else
            {
                this.PanelContentCheck.Visible = true;
                this.PanelContentRecheck.Visible = true;
                try
                {
                    this.InitContentCheckData();
                }
                catch
                {

                }
                finally
                {

                }
                try
                {
                    this.InitContentRecheckData();
                }
                catch
                {

                }
                finally
                {

                }
            }
        }
        //
        private void UpdateContentFinish(Project project)
        {
            project.ContentFinishDate = DateTime.Now;
            project.ContentDelayNote = this.txtContentDelayNote.Text.ToString();
            project.ContentCourseNameConfirm = new Guid(this.ddlContentCourseNameConfirm.SelectedValue);
            project.ContentChangedCourseName = this.txtContentChangedCourseName.Text.ToString();
            project.ContentCourseRecommend = new Guid(this.ddlContentCourseRecommend.SelectedValue);
            project.ContentPPTAdvice = new Guid(this.ddlContentPPTAdvice.SelectedValue);
            project.ContentExercises = Convert.ToInt16(this.txtContentExercises.Text.ToString());
            project.ContentPPTNeeds = new Guid(this.ddlContentPPTNeeds.SelectedValue);
            project.ContentCourseIntroNeeds = new Guid(this.ddlContentCourseIntroNeeds.SelectedValue);
            project.ContentLecturerResumeNeeds = new Guid(this.ddlContentLecturerResumeNeeds.SelectedValue);
            project.ContentExercisesNeeds = new Guid(this.ddlContentExercisesNeeds.SelectedValue);
            project.ContentTextEditNeeds = new Guid(this.ddlContentTextEditNeeds.SelectedValue);
            project.ContentOperateNote = this.txtContentOperateNote.Text.ToString();
            project.progress = new Guid("00000000-0000-0000-0000-000000000112");
            project.ContentProgress = new Guid("00000000-0000-0000-0000-000000000112");
            BLL.Project.UpdateContentFinish(project);
            if (project.ProjectTypeId == new Guid("00000000-0000-0000-0000-000000000199"))
            {
                string url = @"http://newpms.cei.cn/JsonDataDataBackup?ProjectNo="
                    + HttpUtility.UrlEncode(project.ProjectNo);
                //
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = "GET";
                request.ContentType = "text/html;charset=UTF-8";
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                Stream myResponseStream = response.GetResponseStream();
                StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding("utf-8"));
                string retString = myStreamReader.ReadToEnd();
                myStreamReader.Close();
                myResponseStream.Close();
            }
        }
        private void UpdateContentCheck(Project project)
        {
            project.ContentCourseIntroductionQuality = new Guid(this.ddlContentCourseIntroductionQuality.SelectedValue);
            project.ContentResumeQuality = new Guid(this.ddlContentResumeQuality.SelectedValue);
            project.ContentPPTQuality = new Guid(this.ddlContentPPTQuality.SelectedValue);
            project.ContentExercisesQuality = new Guid(this.ddlContentExercisesQuality.SelectedValue);
            project.ContentTextQuality = new Guid(this.ddlContentTextQuality.SelectedValue);
            project.ContentIsTimely = new Guid(this.ddlContentIsTimely.SelectedValue);
            project.ContentCheckDate = DateTime.Now;
            project.ContentCheckNote = this.txtContentCheckNote.Text.ToString();
            project.ContentCheckPersonInCharge= this.LoginUserInfo.Identity;
            if (//单视频
                       project.ProjectTypeId == new Guid("00000000-0000-0000-0000-000000000017")
                       || project.ProjectTypeId == new Guid("00000000-0000-0000-0000-000000000019")
                       || project.ProjectTypeId == new Guid("00000000-0000-0000-0000-000000000299")
                       )
            {
                if (project.ProductionProgress == new Guid("00000000-0000-0000-0000-000000000116"))
                {
                    project.progress = new Guid("00000000-0000-0000-0000-000000000117");
                }
                else
                {
                    project.progress = new Guid("00000000-0000-0000-0000-000000000113");
                }
                project.ContentProgress = new Guid("00000000-0000-0000-0000-000000000113");
            }
            else
            {
                project.progress = new Guid("00000000-0000-0000-0000-000000000122");
                project.ContentProgress = new Guid("00000000-0000-0000-0000-000000000122");
            }
            BLL.Project.UpdateContentCheck(project);
            using (var ProjectModel = new ProjectCollection.WebUI.Models.ProjectCollectionEntities())
            {
                ProjectCollection.WebUI.Models.Project ThisProject = (from p in ProjectModel.Project
                                                                      where p.ProjectId.ToString() == project.ProjectId.ToString()
                                                                      select p).First();
                ThisProject.ContentCheckScore = this.ddlContentCheckScore.SelectedValue;
                ThisProject.ContentCheckSlideScore = this.ddlContentCheckSlideScore.SelectedValue;
                ThisProject.LecturerNote = this.txtLecturerNote.Text;
                ProjectModel.SaveChanges();
            }
            if (project.ProjectTypeId == new Guid("00000000-0000-0000-0000-000000000199"))
            {
                string url = @"http://newpms.cei.cn/JsonDataDataBackup?ProjectNo="
                    + HttpUtility.UrlEncode(project.ProjectNo);
                //
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = "GET";
                request.ContentType = "text/html;charset=UTF-8";
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                Stream myResponseStream = response.GetResponseStream();
                StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding("utf-8"));
                string retString = myStreamReader.ReadToEnd();
                myStreamReader.Close();
                myResponseStream.Close();
            }

        }
        //
        private void UpdateProductionReceive(Project project)
        {
            project.ProductionPersonInCharge = this.LoginUserInfo.Identity;
            project.ProductionOperator = new Guid(this.ddlProductionOperator.SelectedValue);
            project.ProductionReceiveDate = DateTime.Now;
            project.ProductionReceiveNote = this.txtProductionReceiveNote.Text.ToString();
            project.ProductionAssignmentDate = Convert.ToDateTime(this.hidProductionAssignmentDate.Value);
            project.ProductionEstimatedDate = Convert.ToDateTime(this.hidProductionEstimatedDate.Value);
            project.progress = new Guid("00000000-0000-0000-0000-000000000114");
            project.ProductionProgress = new Guid("00000000-0000-0000-0000-000000000114");
            BLL.Project.UpdateProductionReceive(project);
        }
        private void UpdateProductionDelayReceive(Project project)
        {
            project.progress = new Guid("00000000-0000-0000-0000-000000000132");
            project.ProductionProgress = new Guid("00000000-0000-0000-0000-000000000132");
            project.ProductionPersonInCharge = this.LoginUserInfo.Identity;
            project.ProductionReceiveDelayDate = DateTime.Now;
            project.ProductionReceiveDelayNote = this.txtProductionReceiveDelayNote.Text.ToString();
            BLL.Project.UpdateProductionDelayReceive(project);
        }
        private void UpdateProductionCheck(Project project)
        {
            project.ProductionVideoEditCheck = new Guid(this.ddlProductionVideoEditCheck.SelectedValue);
            project.ProductionAudioEditCheck = new Guid(this.ddlProductionAudioEditCheck.SelectedValue);
            project.ProductionProductCheck = new Guid(this.ddlProductionProductCheck.SelectedValue);
            project.ProductionIsTimely = new Guid(this.ddlProductionIsTimely.SelectedValue);
            project.ProductionCheckDate = DateTime.Now;
            project.ProductionCheckNote = this.txtProductionCheckNote.Text.ToString();
            project.ProductionProgress = new Guid("00000000-0000-0000-0000-000000000116");
            if (project.IsSingleScreen()==true)
            {
                if (project.ContentProgress == new Guid("00000000-0000-0000-0000-000000000113"))
                {
                    if (project.PublishNeeds == new Guid("00000000-0000-0000-0000-000000000043"))
                    { project.progress = new Guid("00000000-0000-0000-0000-000000000119"); }
                    else { project.progress = new Guid("00000000-0000-0000-0000-000000000117"); }
                }
                else
                {
                    project.progress = new Guid("00000000-0000-0000-0000-000000000116");
                }
            }
            else if (project.ProjectTypeId == new Guid("00000000-0000-0000-0000-000000000199"))//STT
            {
                project.progress = new Guid("00000000-0000-0000-0000-000000000197");
                //project.ContentProgress= new Guid("00000000-0000-0000-0000-000000000107"); 
            }
            else
            {
                if (project.PublishNeeds == new Guid("00000000-0000-0000-0000-000000000043"))
                { project.progress = new Guid("00000000-0000-0000-0000-000000000119"); }
                else { project.progress = new Guid("00000000-0000-0000-0000-000000000117"); }
            }
            BLL.Project.UpdateProductionCheck(project);
        }
        private void UpdateProductionCheckAuto(Project project)
        {
            project.ProductionVideoEditCheck = new Guid("00000000-0000-0000-0000-000000000044");
            project.ProductionAudioEditCheck = new Guid("00000000-0000-0000-0000-000000000044");
            project.ProductionProductCheck = new Guid("00000000-0000-0000-0000-000000000044");
            project.ProductionIsTimely = new Guid("00000000-0000-0000-0000-000000000042");
            project.ProductionCheckDate = DateTime.Now;
            project.ProductionCheckNote = this.txtProductionCheckNote.Text.ToString();
            project.ProductionProgress = new Guid("00000000-0000-0000-0000-000000000116");
            if (project.IsSingleScreen() == true)
            {
                if (project.ContentProgress == new Guid("00000000-0000-0000-0000-000000000113"))
                {
                    if (project.PublishNeeds == new Guid("00000000-0000-0000-0000-000000000043"))
                    { project.progress = new Guid("00000000-0000-0000-0000-000000000119"); }
                    else { project.progress = new Guid("00000000-0000-0000-0000-000000000117"); }
                }
                else
                {
                    project.progress = new Guid("00000000-0000-0000-0000-000000000116");
                }
            }
            else if (project.ProjectTypeId == new Guid("00000000-0000-0000-0000-000000000199"))//STT
            {
                project.progress = new Guid("00000000-0000-0000-0000-000000000197");
                //project.ContentProgress= new Guid("00000000-0000-0000-0000-000000000107"); 
            }
            else
            {
                if (project.PublishNeeds == new Guid("00000000-0000-0000-0000-000000000043"))
                { project.progress = new Guid("00000000-0000-0000-0000-000000000119"); }
                else { project.progress = new Guid("00000000-0000-0000-0000-000000000117"); }
            }
            BLL.Project.UpdateProductionCheck(project);
        }
        private void UpdateProductionFinish(Project project,string ChromaKey="none")
        {
            //新
            if (project.ProjectTypeId.ToString() == "00000000-0000-0000-0000-000000000199")
            {
                UserName = LoginUserInfo.LoginName;
                PassWord = LoginUserInfo.Password;
                byte[] bytes = Encoding.Default.GetBytes(UserName + "_" + PassWord);
                string str = Convert.ToBase64String(bytes);
                string type = "";
                var Model = new ProjectCollection.WebUI.Models.ProjectCollectionEntities();
                ProjectCollection.WebUI.Models.Project ThisProject = (from p in Model.Project
                                                                  where p.ProjectId.ToString() == project.ProjectId.ToString()
                                                                  select p).First();
                if (project.WorkType.ToString() == "00000000-0000-0000-0000-000000000209" || ThisProject.STTType=="low")
                {
                    type = "NoSTT";
                }
                else {
                    type = project.ProjectNo.Substring(0, 1).ToLower();
                }
                string url = @"http://newpms.cei.cn/FTPVideoUpload?ProjectNo="
                    + HttpUtility.UrlEncode(project.ProjectNo)
                    + "&title="
                    + HttpUtility.UrlEncode(project.CourseName)
                    + "&lecturer="
                    + HttpUtility.UrlEncode(project.lecturer)
                    + "&post="
                    + HttpUtility.UrlEncode(project.LecturerJob)
                    + "&type="
                    + type
                    + "&chromakey="
                    + HttpUtility.UrlEncode(ChromaKey)
                    + "&link="
                    + str;
                //
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = "GET";
                request.ContentType = "text/html;charset=UTF-8";
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                Stream myResponseStream = response.GetResponseStream();
                StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding("utf-8"));
                string retString = myStreamReader.ReadToEnd();
                myStreamReader.Close();
                myResponseStream.Close();
                //
                JObject jo = (JObject)JsonConvert.DeserializeObject(retString);
                if (jo["status"].ToString() == "文件完备" && jo["data"].ToString() == "数据添加成功")
                { }
                else
                {
                    throw new MyException(jo["status"].ToString());
                }
            }
            if (project.ProductionProgress == new Guid("00000000-0000-0000-0000-000000000125"))
            {
                project.ProductionLastModifyDate = DateTime.Now;
            }
            else
            {
                project.ProductionFinishDate = DateTime.Now;
                project.ProductionLastModifyDate = Convert.ToDateTime("1900-01-01");
            }
            project.ProductionDelayNote = this.txtProductionDelayNote.Text.ToString();
            project.ProductionOperateNote = this.txtProductionOperateNote.Text.ToString();
            project.progress = new Guid("00000000-0000-0000-0000-000000000115");
            project.ProductionVideoQuality = new Guid(this.ddlProductionVideoQuality.SelectedValue);
            project.ProductionAudioQuality = new Guid(this.ddlProductionAudioQuality.SelectedValue);
            project.ProductionFileBackUp = new Guid(this.ddlProductionFileBackUp.SelectedValue);
            project.ProductionProgress = new Guid("00000000-0000-0000-0000-000000000115");
            if (project.ProjectTypeId == new Guid("00000000-0000-0000-0000-000000000199"))//sTT
            {
                project.progress = new Guid("00000000-0000-0000-0000-000000000198");
                project.ProductionProgress = new Guid("00000000-0000-0000-0000-000000000198");
            }
            BLL.Project.UpdateProductionFinish(project);
            if (project.CourseType == "elite") {
                UpdateProductionCheckAuto(project);
            }
        }

        #endregion 方法
    }
}