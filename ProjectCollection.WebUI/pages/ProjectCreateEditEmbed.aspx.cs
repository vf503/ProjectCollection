using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ProjectCollection.BLL;
using ProjectCollection.WebUI.pages.common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;

namespace ProjectCollection.WebUI.pages
{
    public partial class ProjectCreateEditEmbed : BasePage
    {
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
            #region create
            if (this.Request["mode"] == "create")
            {
                this.txtPlanNote.Visible = false;
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
                this.hidProjectId.Value = projectId.ToString();
                this.InitBrowseData();
                BLL.Project ThisProject = BLL.Project.GetProject(projectId);
                this.ContentReceiveLink.NavigateUrl = @"http://newpms.cei.cn/SrtUpload?ProjectNo="
                + HttpUtility.UrlEncode(ThisProject.ProjectNo)
                + "&mode=slide";
                this.ContentReceiveLink.Visible = true;
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
                    this.PanelSTT.Visible = true;
                    BLL.Project project = BLL.Project.GetProject(new Guid(this.hidProjectId.Value.ToString()));
                    this.txtSTTFinDate.Text = project.ShorthandFinishDate.ToString();
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
                    InitDropDownListProductionReceive();
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
            }
            #endregion
            #region
            else if (this.Request["mode"] == "contentreceivebatchhandle")
            {
                if (!this.IsPostBack)
                {
                    List<Guid> BatchProjectId = new List<Guid>();
                    BatchProjectId = this.PreviousPage.BatchProjectId;
                    hidBatchProjectId.Value = ProjectCollection.Common.SerializeObj.Serialize(BatchProjectId);
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
            #endregion
            #region
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
            #region
            else if (this.Request["mode"] == "contentcheck")
            {
                Guid projectId = new Guid(this.Request["ProjectId"]);
                this.hidProjectId.Value = projectId.ToString();
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
            #endregion
            #region
            else if (this.Request["mode"] == "contentcheckbatchsave")
            {
                if (!this.IsPostBack)
                {
                    List<Guid> BatchProjectId = new List<Guid>();
                    BatchProjectId = this.PreviousPage.BatchProjectId;
                    hidBatchProjectId.Value = ProjectCollection.Common.SerializeObj.Serialize(BatchProjectId);
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
            #endregion
            #region
            else if (this.Request["mode"] == "contentcheckbatchhandle")
            {
                if (!this.IsPostBack)
                {
                    List<Guid> BatchProjectId = new List<Guid>();
                    BatchProjectId = this.PreviousPage.BatchProjectId;
                    hidBatchProjectId.Value = ProjectCollection.Common.SerializeObj.Serialize(BatchProjectId);
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
            #endregion
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
                this.InitContentCheckData();
            }
            #endregion contentrecheck
            #region productionreceive
            else if (this.Request["mode"] == "productionreceive")
            {
                Guid projectId = new Guid(this.Request["ProjectId"]);
                this.hidProjectId.Value = projectId.ToString();
                BLL.Project Project = BLL.Project.GetProject(new Guid(this.hidProjectId.Value.ToString()));
                LoadProductionReceivePageDate();
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
                    BatchProjectId = this.PreviousPage.BatchProjectId;
                    hidBatchProjectId.Value = ProjectCollection.Common.SerializeObj.Serialize(BatchProjectId);
                    this.hidProjectId.Value = BatchProjectId[0].ToString();
                }
                this.btnReceive.Text = "批量接收";
                this.btnSentBack.Visible = true;
                this.btnSentBack.Text = "批量延迟";
                LoadProductionReceivePageDate();
            }
            #endregion productionreceivebatchhandle
            #region productionfinish
            else if (this.Request["mode"] == "productionfinish")
            {
                Guid projectId = new Guid(this.Request["ProjectId"]);
                this.hidProjectId.Value = projectId.ToString();
                this.btnOk.Visible = true;
                this.btnOk.Text = "制作完成";
                LoadProductionFinishPageDate();
            }
            #endregion productionfinish
            #region productionfinishbatchhandle
            else if (this.Request["mode"] == "productionfinishbatchhandle")
            {
                if (!this.IsPostBack)
                {
                    List<Guid> BatchProjectId = new List<Guid>();
                    BatchProjectId = this.PreviousPage.BatchProjectId;
                    hidBatchProjectId.Value = ProjectCollection.Common.SerializeObj.Serialize(BatchProjectId);
                    this.hidProjectId.Value = BatchProjectId[0].ToString();
                }
                this.btnOk.Visible = true;
                this.btnOk.Text = "批量制作完成";
                LoadProductionFinishPageDate();
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
                }
            }
            #endregion productioncheck

            #region
            else if (this.Request["mode"] == "productioncheckbatchsave")
            {
                if (!this.IsPostBack)
                {
                    List<Guid> BatchProjectId = new List<Guid>();
                    BatchProjectId = this.PreviousPage.BatchProjectId;
                    hidBatchProjectId.Value = ProjectCollection.Common.SerializeObj.Serialize(BatchProjectId);
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
            #endregion
            #region
            else if (this.Request["mode"] == "productioncheckbatchhandle")
            {
                if (!this.IsPostBack)
                {
                    List<Guid> BatchProjectId = new List<Guid>();
                    BatchProjectId = this.PreviousPage.BatchProjectId;
                    hidBatchProjectId.Value = ProjectCollection.Common.SerializeObj.Serialize(BatchProjectId);
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
            #endregion
            #region
            else if (this.Request["mode"] == "publish")
            {
                Guid projectId = new Guid(this.Request["ProjectId"]);
                this.hidProjectId.Value = projectId.ToString();
                this.btnOk.Visible = true;
                this.btnOk.Text = "发布";
                this.PanelPublish.Visible = true;
                this.InitBrowseData();
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
                if (!IsPostBack)
                {
                    InitDropDownListCheck();
                }
            }
            else
            {

            }
            #endregion
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
                //新增
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
                //新单改新三（Copy）
                if(this.Request["mode"] == "copy" && this.ddlProjectType.SelectedValue== "00000000-0000-0000-0000-000000000199" && this.ddlWorkType.SelectedValue== "00000000-0000-0000-0000-000000000029")
                {
                    var ProjectModel = new ProjectCollection.WebUI.Models.ProjectCollectionEntities();
                    ProjectCollection.WebUI.Models.Project SourceProject = (from p in ProjectModel.Project
                                                                          where p.ProjectId.ToString() == this.hidProjectId.Value.ToString()
                                                                          select p).First();
                    if (SourceProject.ProjectTypeId.ToString() == "00000000-0000-0000-0000-000000000017" && SourceProject.MakeType== "new")
                    {
                        project.progress = new Guid("00000000-0000-0000-0000-000000000120");
                    }
                }
                BLL.Project.Insert(project);
                //新单视频 
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
                //
                if (this.ddlProjectType.SelectedValue == "00000000-0000-0000-0000-000000000298")
                {
                    using (var ProjectModel = new ProjectCollection.WebUI.Models.ProjectCollectionEntities())
                    {
                        ProjectCollection.WebUI.Models.Project ThisProject = (from p in ProjectModel.Project
                                                                              where p.ProjectId == project.ProjectId
                                                                              select p).First();
                        ThisProject.ProjectTypeId = new Guid("00000000-0000-0000-0000-000000000018");
                        ThisProject.MakeType = "new";
                        ThisProject.ContentNeeds = new Guid("00000000-0000-0000-0000-000000000042");
                        ThisProject.CourseType = "micro";
                        ThisProject.progress = new Guid("00000000-0000-0000-0000-000000000120");
                        ProjectModel.SaveChanges();
                    }
                }
                if (this.ddlProjectType.SelectedValue == "00000000-0000-0000-0000-000000000297")
                {
                    using (var ProjectModel = new ProjectCollection.WebUI.Models.ProjectCollectionEntities())
                    {
                        ProjectCollection.WebUI.Models.Project ThisProject = (from p in ProjectModel.Project
                                                                              where p.ProjectId == project.ProjectId
                                                                              select p).First();
                        ThisProject.ProjectTypeId = new Guid("00000000-0000-0000-0000-000000000018");
                        ThisProject.MakeType = "new";
                        ThisProject.ContentNeeds = new Guid("00000000-0000-0000-0000-000000000042");
                        //ThisProject.PublishNeeds = new Guid("00000000-0000-0000-0000-000000000043");
                        ThisProject.CourseType = "elite";
                        ProjectModel.SaveChanges();
                    }
                }
                if (this.ddlWorkType.SelectedValue == "00000000-0000-0000-0000-000000000300")
                {
                    var ProjectModel = new ProjectCollection.WebUI.Models.ProjectCollectionEntities();
                    ProjectCollection.WebUI.Models.Project ThisProject = (from p in ProjectModel.Project
                                                                          where p.ProjectId == project.ProjectId
                                                                          select p).First();
                    ProjectCollection.WebUI.Models.Project SourceProject = (from p in ProjectModel.Project
                                                                            where p.ProjectId.ToString() == this.hidProjectId.Value.ToString()
                                                                            select p).First();
                    //
                    string UserName = LoginUserInfo.LoginName;
                    string PassWord = LoginUserInfo.Password;
                    byte[] bytes = Encoding.Default.GetBytes(UserName + "_" + PassWord);
                    string str = Convert.ToBase64String(bytes);
                    string url = @"http://newpms.cei.cn/FTPVideoUpload/?link="
                    + str
                    + "&type="
                    + "EliteCourseCopy"
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
                    ThisProject.ProjectTypeId = new Guid("00000000-0000-0000-0000-000000000018");
                    ThisProject.ContentNeeds = new Guid("00000000-0000-0000-0000-000000000042");
                    ThisProject.PublishNeeds = new Guid("00000000-0000-0000-0000-000000000043");
                    ThisProject.progress = new Guid("00000000-0000-0000-0000-000000000107");
                    ThisProject.ProductionProgress = new Guid("00000000-0000-0000-0000-000000000116");
                    ThisProject.ContentProgress = new Guid("00000000-0000-0000-0000-000000000107");
                    ThisProject.MakeType = "new";
                    ThisProject.CourseType = "elite";
                    ProjectModel.SaveChanges();
                }
                //
                using (var ProjectModel = new ProjectCollection.WebUI.Models.ProjectCollectionEntities())
                {
                    ProjectCollection.WebUI.Models.Project ThisProject = (from p in ProjectModel.Project
                                                                          where p.ProjectId == project.ProjectId
                                                                          select p).First();
                    ThisProject.STTType = this.ddlSTTType.SelectedValue;
                    ProjectModel.SaveChanges();
                }
                this.Redirect("~/pages/ProjectList.aspx?mode=browse");
            }
            #endregion
            #region capture
            else if (this.Request["mode"] == "capture")
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
                this.Redirect("~/pages/ProjectList.aspx?mode=capture");
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
                this.Redirect("~/pages/ProjectList.aspx?mode=capturecheck");
            }
            #endregion
            #region execution
            else if (this.Request["mode"] == "execution")
            {
                BLL.Project project = BLL.Project.GetProject(new Guid(this.hidProjectId.Value.ToString()));
                Guid WorkTypeId = new Guid(this.ddlWorkType.SelectedValue);
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
                BLL.Project.UpdateExecution(project);
                this.Redirect("~/pages/ProjectList.aspx?mode=execution");
            }
            #endregion execution
            #region
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
                this.Redirect("~/pages/ProjectList.aspx?mode=shorthand");
            }
            #endregion
            #region
            else if (this.Request["mode"] == "contentfinish")
            {
                BLL.Project project = BLL.Project.GetProject(new Guid(this.hidProjectId.Value.ToString()));
                project.ContentFinishDate = DateTime.Now;
                project.ContentDelayNote = this.txtContentDelayNote.Text.ToString();
                project.ContentCourseNameConfirm = new Guid(this.ddlContentCourseNameConfirm.SelectedValue);
                project.ContentChangedCourseName = this.txtContentChangedCourseName.Text.ToString();
                project.ContentCourseRecommend = new Guid(this.ddlContentCourseRecommend.SelectedValue);
                project.ContentPPTAdvice = new Guid(this.ddlContentCourseRecommend.SelectedValue);
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
                this.Redirect("~/pages/ProjectList.aspx?mode=contentfinish");
            }
            #endregion
            #region
            else if (this.Request["mode"] == "contentcheck")
            {
                BLL.Project project = BLL.Project.GetProject(new Guid(this.hidProjectId.Value.ToString()));
                UpdateContentCheck(project);
                this.Redirect("~/pages/ProjectList.aspx?mode=contentcheck");
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
                    BLL.Project.UpdateContentCheck(project);
                }
                this.Redirect("~/pages/ProjectList.aspx?mode=contentcheck");
            }
            #endregion
            #region
            else if (this.Request["mode"] == "contentcheckbatchhandle")
            {
                List<Guid> BatchProjectId = new List<Guid>();
                BatchProjectId = ProjectCollection.Common.SerializeObj.Desrialize(BatchProjectId, hidBatchProjectId.Value);
                for (int i = 0; i < BatchProjectId.Count; i++)
                {
                    BLL.Project project = BLL.Project.GetProject(BatchProjectId[i]);
                    UpdateContentCheck(project);
                }
                this.Redirect("~/pages/ProjectList.aspx?mode=contentcheck");
            }
            #endregion
            #region
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
                else
                {
                    //三分屏 制作部完成 技术部准备接收
                    project.progress = new Guid("00000000-0000-0000-0000-000000000106");
                    project.ProductionProgress = new Guid("00000000-0000-0000-0000-000000000106");
                }
                BLL.Project.UpdateContentRecheckFinish(project);
                this.Redirect("~/pages/ProjectList.aspx?mode=contentrecheck");
            }
            #endregion
            #region
            else if (this.Request["mode"] == "productionfinish")
            {
                BLL.Project project = BLL.Project.GetProject(new Guid(this.hidProjectId.Value.ToString()));
                UpdateProductionFinish(project);
                this.Redirect("~/pages/ProjectList.aspx?mode=productionfinish");
            }
            #endregion
            #region productionfinishbatchhandle
            else if (this.Request["mode"] == "productionfinishbatchhandle")
            {
                List<Guid> BatchProjectId = new List<Guid>();
                BatchProjectId = ProjectCollection.Common.SerializeObj.Desrialize(BatchProjectId, hidBatchProjectId.Value);
                for (int i = 0; i < BatchProjectId.Count; i++)
                {
                    BLL.Project project = BLL.Project.GetProject(BatchProjectId[i]);
                    UpdateProductionFinish(project);
                }
                this.Redirect("~/pages/ProjectList.aspx?mode=productionfinish");
            }
            #endregion productionfinishbatchhandle
            #region
            else if (this.Request["mode"] == "productioncheck")
            {
                BLL.Project project = BLL.Project.GetProject(new Guid(this.hidProjectId.Value.ToString()));
                UpdateProductionCheck(project);
                this.Redirect("~/pages/ProjectList.aspx?mode=productioncheck");
            }
            #endregion
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
                this.Redirect("~/pages/ProjectList.aspx?mode=productioncheck");
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
                    UpdateProductionCheck(project);
                }
                this.Redirect("~/pages/ProjectList.aspx?mode=productioncheck");
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
                this.Redirect("~/pages/ProjectList.aspx?mode=publish");
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
                this.Redirect("~/pages/ProjectList.aspx?mode=check");
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
                this.Redirect("~/pages/ProjectList.aspx?mode=capture");
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
                this.Redirect("~/pages/ProjectList.aspx?mode=contentreceive");
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
                this.Redirect("~/pages/ProjectList.aspx?mode=contentreceive");
            }
            #endregion
            #region
            else if (this.Request["mode"] == "productionreceive")
            {
                BLL.Project project = BLL.Project.GetProject(new Guid(this.hidProjectId.Value.ToString()));
                UpdateProductionReceive(project);
                this.Redirect("~/pages/ProjectList.aspx?mode=productionreceive");
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
                this.Redirect("~/pages/ProjectList.aspx?mode=productionreceive");
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
                this.Redirect("~/pages/ProjectList.aspx?mode=contentcheck");
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
                this.Redirect("~/pages/ProjectList.aspx?mode=contentrecheck");
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
                this.Redirect("~/pages/ProjectList.aspx?mode=productioncheck");
            }
            else if (this.Request["mode"] == "capture")//采集延时接收
            {
                BLL.Project project = BLL.Project.GetProject(new Guid(this.hidProjectId.Value.ToString()));
                project.progress = new Guid("00000000-0000-0000-0000-000000000131");
                project.CapturePersonInCharge = this.LoginUserInfo.Identity;
                project.CaptureReceiveDelayDate = DateTime.Now;
                project.CaptureReceiveDelayNote = this.txtCaptureReceiveDelayNote.Text.ToString();
                BLL.Project.UpdateCaptureDelayReceive(project);
                this.Redirect("~/pages/ProjectList.aspx?mode=capturereceive");
            }
            else if (this.Request["mode"] == "productionreceive")//技术部延时接收
            {
                BLL.Project project = BLL.Project.GetProject(new Guid(this.hidProjectId.Value.ToString()));
                UpdateProductionDelayReceive(project);
                this.Redirect("~/pages/ProjectList.aspx?mode=productionreceive");
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
                this.Redirect("~/pages/ProjectList.aspx?mode=productionreceive");
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
            this.Redirect("~/pages/ProjectList.aspx?mode=capturecheck");
        }
        protected void rblDeadLineSelectSetChanged(object sender, EventArgs e)
        { }

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
        private void InitDropDownListProductionReceive()
        {
            this.ddlProductionOperator.DataSource = BLL.UserInfo.GetDataByID();
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
            this.ddlProjectType.SelectedValue = project.ProjectTypeId.ToString();
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
            this.rblCanBeSold.SelectedValue = project.CanBeSold.ToString();
            this.ddlEpisodeCount.SelectedValue = project.EpisodeCount.ToString();
            this.txtRecordingDate.Text = ProjectPlan.RecordingDate.ToString("yyyy-MM-dd");
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
                this.ddlSTTType.SelectedValue = ThisProject.STTType;
            }
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
        }
        private void InitContentCheckData()
        {
            BLL.Project project = BLL.Project.GetProject(new Guid(this.hidProjectId.Value.ToString()));
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
        private void LoadProductionReceivePageDate()
        {
            this.btnReceive.ValidationGroup = "ProductionReceive";
            this.btnReceive.Visible = true;
            this.PanelProductionReceive.Visible = true;
            this.PanelCapture.Visible = true;
            this.PanelContentCheck.Visible = true;
            this.PanelContentRecheck.Visible = true;
            this.InitBrowseData();
            if (!IsPostBack)
            {
                this.InitDropDownListProductionReceive();
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
        private void LoadProductionFinishPageDate()
        {
            this.PanelProductionOperator.Visible = true;
            this.PanelCapture.Visible = true;
            this.PanelProductionReceive.Visible = true;
            this.PanelProductionCheck.Visible = true;
            this.PanelContentCheck.Visible = true;
            this.PanelContentRecheck.Visible = true;
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
        //
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
            if (//单视频
                       project.ProjectTypeId == new Guid("00000000-0000-0000-0000-000000000017")
                       || project.ProjectTypeId == new Guid("00000000-0000-0000-0000-000000000019")
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
        }
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
            else
            {
                if (project.PublishNeeds == new Guid("00000000-0000-0000-0000-000000000043"))
                { project.progress = new Guid("00000000-0000-0000-0000-000000000119"); }
                else { project.progress = new Guid("00000000-0000-0000-0000-000000000117"); }
            }
            BLL.Project.UpdateProductionCheck(project);
        }
        private void UpdateProductionFinish(Project project)
        {
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
            BLL.Project.UpdateProductionFinish(project);
        }
        #endregion 方法
    }
}