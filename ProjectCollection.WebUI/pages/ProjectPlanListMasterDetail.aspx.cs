using ProjectCollection.BLL;
using ProjectCollection.Common;
using ProjectCollection.WebUI.pages.common;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI.WebControls;
using System.Linq;

namespace ProjectCollection.WebUI.pages
{
    public partial class ProjectPlanListMasterDetail : BasePage
    {
        #region 事件

        protected void Page_Load(object sender, EventArgs e)
        {
            UserInfo LoginUserInfo = (UserInfo)Session["key_userInfo"];
            if (LoginUserInfo.Authority.Contains("PlanManage"))
            {
                //this.Redirect("/pages/ProjectPlanListMasterDetail.aspx");//页面无法显示
                //ASPxTabControlMain.ActiveTabIndex = 0;URL自动绑定
                btnCreate.Visible = true;
            }
            else
            {
                //ASPxTabControlMain.Tabs[0].Enabled = false;
                btnCreate.Visible = false;
            }
            DataBindProjectPlanList();
            if (!IsPostBack)
            {
                //DataBindProjectPlanList();//过滤器无数据
            }
            else { }
            //if (this.Request["mode"] == "recond")
            //{

            //}
            //else if (this.Request["mode"] == "browse")
            //{

            //}
            //else
            //{
            //    btnCreate.Visible = true;
            //}
        }

        protected void btnCreate_Click(object sender, EventArgs e)
        {
            this.Redirect("/pages/ProjectPlanCreateEdit.aspx");
        }

        protected void btnProjectProgress_Click(object sender, CommandEventArgs e)
        {
            ShowProgressPanel(new Guid(e.CommandArgument.ToString()));
        }

        protected void axgvProjectPlan_PageIndexChanged(object sender, EventArgs e)
        {
            DataBindProjectPlanList();
        }

        protected void axgvProjectPlan_CustomButtonCallback(object sender, DevExpress.Web.ASPxGridViewCustomButtonCallbackEventArgs e)
        {
            if (e.ButtonID == "Select")
            {

            }
            else { }
        }

        protected void axgvProject_BeforePerformDataSelect(object sender, EventArgs e)
        {
            DevExpress.Web.ASPxGridView CurrentGridView = (DevExpress.Web.ASPxGridView)sender;
            Guid ProjectPlanId = new Guid((sender as DevExpress.Web.ASPxGridView).GetMasterRowKeyValue().ToString());
            BLL.ProjectPlan projectPlan = BLL.ProjectPlan.GetProjectPlan(ProjectPlanId);
            //if (projectPlan.ProjectPlanTypeId.ToString() == "") { CurrentGridView.DataSource = BLL.CustomProject.(); }
            //else {  }
            CurrentGridView.DataSource = BLL.Project.GetProjectByPlanId(ProjectPlanId);

            //CurrentGridView.DataBind();//DataBind()后无限循环
        }

        protected void axgvProjectPlan_DataHandle(object sender, EventArgs e)
        {
            if (this.Request["mode"] == "recond")
            {
                List<object> keyValues = axgvProjectPlan.GetCurrentPageRowValues(axgvProjectPlan.KeyFieldName);
                foreach (object key in keyValues)
                {
                    HyperLink CurrentASelect = (HyperLink)axgvProjectPlan.FindRowCellTemplateControlByKey(key, (DevExpress.Web.GridViewDataColumn)axgvProjectPlan.Columns["Operate"], "aSelect");
                    CurrentASelect.NavigateUrl = "~/pages/ProjectPlanCreateEdit.aspx?mode=recond&ProjectPlanId=" + key.ToString();
                }
            }
            else if (this.Request["mode"] == "browse")
            {
                List<object> keyValues = axgvProjectPlan.GetCurrentPageRowValues(axgvProjectPlan.KeyFieldName);
                foreach (object key in keyValues)
                {
                    HyperLink CurrentASelect = (HyperLink)axgvProjectPlan.FindRowCellTemplateControlByKey(key, (DevExpress.Web.GridViewDataColumn)axgvProjectPlan.Columns["Operate"], "aSelect");
                    CurrentASelect.NavigateUrl = "~/pages/ProjectPlanCreateEdit.aspx?mode=browse&ProjectPlanId=" + key.ToString();
                }
            }
            else
            {
                List<object> keyValues = axgvProjectPlan.GetCurrentPageRowValues(axgvProjectPlan.KeyFieldName);
                foreach (object key in keyValues)
                {
                    //string CurrentId = axgvProjectPlan.GetRowValuesByKeyValue(key, this.axgvProjectPlan.KeyFieldName).ToString();
                    HyperLink CurrentASelect = (HyperLink)axgvProjectPlan.FindRowCellTemplateControlByKey(key, (DevExpress.Web.GridViewDataColumn)axgvProjectPlan.Columns["Operate"], "aSelect");
                    CurrentASelect.NavigateUrl = "~/pages/ProjectPlanCreateEdit.aspx?ProjectPlanId=" + key.ToString();
                }
            }
        }

        protected void btnProgressClose_Click(object sender, EventArgs e)
        {
            this.PanelProgress.Visible = false;
        }

        #endregion

        #region 方法

        private void DataBindProjectPlanList()
        {
            if (this.Request["mode"] == "recond")
            {
                this.axgvProjectPlan.DataSource = BLL.ProjectPlan.GetRecondProjectPlan();
                this.axgvProjectPlan.DataBind();
                List<object> keyValues = axgvProjectPlan.GetCurrentPageRowValues(axgvProjectPlan.KeyFieldName);
                foreach (object key in keyValues)
                {
                    HyperLink CurrentASelect = (HyperLink)axgvProjectPlan.FindRowCellTemplateControlByKey(key, (DevExpress.Web.GridViewDataColumn)axgvProjectPlan.Columns["Operate"], "aSelect");
                    CurrentASelect.NavigateUrl = "~/pages/ProjectPlanCreateEdit.aspx?mode=recond&ProjectPlanId=" + key.ToString();
                }
            }
            else if (this.Request["mode"] == "browse")
            {
                this.axgvProjectPlan.DataSource = BLL.ProjectPlan.GetAllProjectPlan();
                this.axgvProjectPlan.DataBind();
                List<object> keyValues = axgvProjectPlan.GetCurrentPageRowValues(axgvProjectPlan.KeyFieldName);
                foreach (object key in keyValues)
                {
                    HyperLink CurrentASelect = (HyperLink)axgvProjectPlan.FindRowCellTemplateControlByKey(key, (DevExpress.Web.GridViewDataColumn)axgvProjectPlan.Columns["Operate"], "aSelect");
                    CurrentASelect.NavigateUrl = "~/pages/ProjectPlanCreateEdit.aspx?mode=browse&ProjectPlanId=" + key.ToString();
                }
            }
            else
            {
                this.axgvProjectPlan.DataSource = BLL.ProjectPlan.GetProjectPlan();
                this.axgvProjectPlan.DataBind();
                List<object> keyValues = axgvProjectPlan.GetCurrentPageRowValues(axgvProjectPlan.KeyFieldName);
                foreach (object key in keyValues)
                {
                    //string CurrentId = axgvProjectPlan.GetRowValuesByKeyValue(key, this.axgvProjectPlan.KeyFieldName).ToString();
                    HyperLink CurrentASelect = (HyperLink)axgvProjectPlan.FindRowCellTemplateControlByKey(key, (DevExpress.Web.GridViewDataColumn)axgvProjectPlan.Columns["Operate"], "aSelect");
                    if (LoginUserInfo.Authority.Contains("PlanManage"))
                    {
                        CurrentASelect.NavigateUrl = "~/pages/ProjectPlanCreateEdit.aspx?ProjectPlanId=" + key.ToString();
                    }
                    else { CurrentASelect.NavigateUrl = "~/pages/ProjectPlanCreateEdit.aspx?mode=browse&ProjectPlanId=" + key.ToString(); }
                }
            }
        }

        #region 时间
        protected void ShowCaptureCheckDurationUnfinish(Project project)
        {
            if (//无采集
                project.ProjectTypeId == new Guid("00000000-0000-0000-0000-000000000019")
                || project.ProjectTypeId == new Guid("00000000-0000-0000-0000-000000000038")
                || project.ProjectTypeId == new Guid("00000000-0000-0000-0000-000000000039")
                )
            {
                this.CaptureCheckDuration.Text = "(" + DateTimeHandle.DateDiffHour(DateTime.Now, project.SendingDate) + ")";
            }
            else
            {
                this.CaptureCheckDuration.Text = "(" + DateTimeHandle.DateDiffHour(DateTime.Now, project.CaptureFinishDate) + ")";
            }
        }
        protected void ShowCaptureCheckDurationFinish(Project project)
        {
            if (//无采集
                project.ProjectTypeId == new Guid("00000000-0000-0000-0000-000000000019")
                || project.ProjectTypeId == new Guid("00000000-0000-0000-0000-000000000038")
                || project.ProjectTypeId == new Guid("00000000-0000-0000-0000-000000000039")
                )
            {
                this.CaptureCheckDuration.Text = "(" + DateTimeHandle.DateDiffDay(project.ExecutionDate, project.SendingDate) + ")";
            }
            else
            {
                this.CaptureCheckDuration.Text = "(" + DateTimeHandle.DateDiffDay(project.ExecutionDate, project.CaptureFinishDate) + ")";
            }
        }
        protected void ShowContentReceiveDurationUnfinish(Project project)
        {
            if (//无速记
                    project.ProjectTypeId == new Guid("00000000-0000-0000-0000-000000000017")
                    || project.ProjectTypeId == new Guid("00000000-0000-0000-0000-000000000019")
                    || project.ProjectTypeId == new Guid("00000000-0000-0000-0000-000000000021")
                    || project.ProjectTypeId == new Guid("00000000-0000-0000-0000-000000000037")
                    || project.ProjectTypeId == new Guid("00000000-0000-0000-0000-000000000038")
                        )
            {
                this.ContentReceiveDuration.Text = "(" + DateTimeHandle.DateDiffHour(DateTime.Now, project.ExecutionDate) + ")";
            }
            //新三分屏
            else if (project.ProjectTypeId == new Guid("00000000-0000-0000-0000-000000000199"))
            {
                using (var ProjectModel = new ProjectCollection.WebUI.Models.ProjectCollectionEntities())
                {
                    ProjectCollection.WebUI.Models.Project ThisProject = (from p in ProjectModel.Project
                                                                          where p.ProjectId.ToString() == project.ProjectId.ToString()
                                                                          select p).First();
                    this.ContentReceiveDuration.Text = "(" + DateTimeHandle.DateDiffDay(DateTime.Now, Convert.ToDateTime(ThisProject.ShorthandFinishDate)) + ")";
                }
            }
            else
            { this.ContentReceiveDuration.Text = "(" + DateTimeHandle.DateDiffHour(DateTime.Now, project.ShorthandFinishDate) + ")"; }
        }
        protected void ShowContentReceiveDurationFinish(Project project)
        {
            if (//无速记
                    project.ProjectTypeId == new Guid("00000000-0000-0000-0000-000000000017")
                    || project.ProjectTypeId == new Guid("00000000-0000-0000-0000-000000000019")
                    || project.ProjectTypeId == new Guid("00000000-0000-0000-0000-000000000021")
                    || project.ProjectTypeId == new Guid("00000000-0000-0000-0000-000000000037")
                    || project.ProjectTypeId == new Guid("00000000-0000-0000-0000-000000000038")
                        )
            {
                this.ContentReceiveDuration.Text = "(" + DateTimeHandle.DateDiffDay(project.ContentAssignmentDate, project.ExecutionDate) + ")";
            }
            //新三分屏
            else if (project.ProjectTypeId == new Guid("00000000-0000-0000-0000-000000000199"))
            {
                using (var ProjectModel = new ProjectCollection.WebUI.Models.ProjectCollectionEntities())
                {
                    ProjectCollection.WebUI.Models.Project ThisProject = (from p in ProjectModel.Project
                                                                          where p.ProjectId.ToString() == project.ProjectId.ToString()
                                                                          select p).First();
                    this.ContentReceiveDuration.Text = "(" + DateTimeHandle.DateDiffDay(Convert.ToDateTime(ThisProject.ContentAssignmentDate), Convert.ToDateTime(ThisProject.ShorthandFinishDate)) + ")";
                }
            }
            else
            { this.ContentReceiveDuration.Text = "(" + DateTimeHandle.DateDiffDay(project.ContentAssignmentDate, project.ShorthandFinishDate) + ")"; }
        }
        protected void ShowProductionReceiveDurationUnfinish(Project project, string Message, string color)
        {
            if (//单视频(必无速记)
                project.ProjectTypeId == new Guid("00000000-0000-0000-0000-000000000017")
                || project.ProjectTypeId == new Guid("00000000-0000-0000-0000-000000000019")
                )
            {
                this.ProductionReceiveState.Text = Message;
                ProgressProductionReceive.BackColor = System.Drawing.ColorTranslator.FromHtml(color);
                this.ProductionReceiveDuration.Text = "(" + DateTimeHandle.DateDiffHour(DateTime.Now, project.ExecutionDate) + ")";
            }
            //新三分屏(无速记)
            else if (project.ProjectTypeId == new Guid("00000000-0000-0000-0000-000000000199"))
            {
                this.ProgressProductionReceive.Visible = false;
                this.NewProgressProductionReceive.Visible = true;
                this.NewProductionReceiveState.Text = Message;
                NewProgressProductionReceive.BackColor = System.Drawing.ColorTranslator.FromHtml(color);
                this.NewProductionReceiveDuration.Text = "(" + DateTimeHandle.DateDiffHour(DateTime.Now, project.ExecutionDate) + ")";
            }
            else//三分屏
            {
                this.ProductionReceiveState.Text = Message;
                ProgressProductionReceive.BackColor = System.Drawing.ColorTranslator.FromHtml(color);
                if (//无速记
                    project.ProjectTypeId == new Guid("00000000-0000-0000-0000-000000000017")
                    || project.ProjectTypeId == new Guid("00000000-0000-0000-0000-000000000019")
                    || project.ProjectTypeId == new Guid("00000000-0000-0000-0000-000000000021")
                    || project.ProjectTypeId == new Guid("00000000-0000-0000-0000-000000000037")
                    || project.ProjectTypeId == new Guid("00000000-0000-0000-0000-000000000038")
                    )
                {
                    if (project.ContentNeeds == new Guid("00000000-0000-0000-0000-000000000043"))//无制作部
                    { this.ProductionReceiveDuration.Text = "(" + DateTimeHandle.DateDiffHour(DateTime.Now, project.ExecutionDate) + ")"; }
                    else //有制作部(三分屏必有复审)
                    {
                        this.ProductionReceiveDuration.Text = "(" + DateTimeHandle.DateDiffHour(DateTime.Now, project.ContentRecheckDate) + ")";
                    }
                }
                else//有速记(必有制作部)
                { this.ProductionReceiveDuration.Text = "(" + DateTimeHandle.DateDiffHour(DateTime.Now, project.ContentRecheckDate) + ")"; }
            }

        }
        protected void ShowProductionReceiveDurationFinish(Project project, string Message, string color)
        {
            if (//单视频(必无速记)
                project.ProjectTypeId == new Guid("00000000-0000-0000-0000-000000000017")
                || project.ProjectTypeId == new Guid("00000000-0000-0000-0000-000000000019")
                )
            {
                this.ProductionReceiveState.Text = Message;
                ProgressProductionReceive.BackColor = System.Drawing.ColorTranslator.FromHtml(color);
                this.ProductionReceiveDuration.Text = DateTimeHandle.DateDiffDay(project.ProductionReceiveDate, project.ExecutionDate);
            }
            //新三分屏
            else if (project.ProjectTypeId == new Guid("00000000-0000-0000-0000-000000000199"))
            {
                this.ProgressProductionReceive.Visible = false;
                this.NewProgressProductionReceive.Visible = true;
                this.NewProductionReceiveState.Text = Message;
                NewProgressProductionReceive.BackColor = System.Drawing.ColorTranslator.FromHtml(color);
                this.NewProductionReceiveDuration.Text = DateTimeHandle.DateDiffDay(project.ProductionReceiveDate, project.ExecutionDate);
            }
            else//三分屏
            {
                this.ProductionReceiveState.Text = Message;
                ProgressProductionReceive.BackColor = System.Drawing.ColorTranslator.FromHtml(color);
                if (//无速记
                    project.ProjectTypeId == new Guid("00000000-0000-0000-0000-000000000017")
                    || project.ProjectTypeId == new Guid("00000000-0000-0000-0000-000000000019")
                    || project.ProjectTypeId == new Guid("00000000-0000-0000-0000-000000000021")
                    || project.ProjectTypeId == new Guid("00000000-0000-0000-0000-000000000037")
                    || project.ProjectTypeId == new Guid("00000000-0000-0000-0000-000000000038")
                    )
                {
                    if (project.ContentNeeds == new Guid("00000000-0000-0000-0000-000000000043"))//无制作部
                    { this.ProductionReceiveDuration.Text = DateTimeHandle.DateDiffDay(project.ProductionReceiveDate, project.ExecutionDate); }
                    else //有制作部(三分屏必有复审)
                    {
                        this.ProductionReceiveDuration.Text = DateTimeHandle.DateDiffDay(project.ProductionReceiveDate, project.ContentRecheckDate);
                    }
                }
                else//有速记(必有制作部)
                { this.ProductionReceiveDuration.Text = DateTimeHandle.DateDiffDay(project.ProductionReceiveDate, project.ContentRecheckDate); }
            }
            if (project.ProductionReceiveDelayDate != new DateTime(0001, 1, 1, 00, 00, 00))
            {
                ProgressProductionReceive.BackColor = System.Drawing.ColorTranslator.FromHtml("#fecf45");
                this.ProductionReceiveDuration.Text = "(" + this.ProductionReceiveDuration.Text + ")";
                this.ProductionReceiveState.Text = "推迟" + DateTimeHandle.DateDiffHour(project.ProductionReceiveDate, project.ProductionReceiveDelayDate) + "完成";

            }
            else { this.ProductionReceiveDuration.Text = "(" + this.ProductionReceiveDuration.Text + ")"; }
        }
        protected void ShowPublishDurationUnfinish(Project project)
        {
            if (//单视频(无复审)
              project.ProjectTypeId == new Guid("00000000-0000-0000-0000-000000000017")
              || project.ProjectTypeId == new Guid("00000000-0000-0000-0000-000000000019")
              )
            {
                if (DateTime.Compare(project.ContentCheckDate, project.ProductionCheckDate) > 0)
                {
                    this.PublishDuration.Text = "(" + DateTimeHandle.DateDiffHour(DateTime.Now, project.ContentCheckDate) + ")";
                }
                else { this.PublishDuration.Text = "(" + DateTimeHandle.DateDiffHour(DateTime.Now, project.ProductionCheckDate) + ")"; }
            }
            else//三分屏(有复审)
            {
                if (DateTime.Compare(project.ContentRecheckDate, project.ProductionCheckDate) > 0)
                {
                    this.PublishDuration.Text = "(" + DateTimeHandle.DateDiffHour(DateTime.Now, project.ContentRecheckDate) + ")";
                }
                else { this.PublishDuration.Text = "(" + DateTimeHandle.DateDiffHour(DateTime.Now, project.ProductionCheckDate) + ")"; }
            }

        }
        protected void ShowPublishDurationFinish(Project project)
        {
            if (//单视频(无复审)
              project.ProjectTypeId == new Guid("00000000-0000-0000-0000-000000000017")
              || project.ProjectTypeId == new Guid("00000000-0000-0000-0000-000000000019")
              )
            {
                if (DateTime.Compare(project.ContentCheckDate, project.ProductionCheckDate) > 0)
                {
                    this.PublishDuration.Text = "(" + DateTimeHandle.DateDiffDay(project.PublishPublishDate, project.ContentCheckDate) + ")";
                }
                else { this.PublishDuration.Text = "(" + DateTimeHandle.DateDiffDay(project.PublishPublishDate, project.ProductionCheckDate) + ")"; }
            }
            else//三分屏(有复审)
            {
                if (DateTime.Compare(project.ContentRecheckDate, project.ProductionCheckDate) > 0)
                {
                    this.PublishDuration.Text = "(" + DateTimeHandle.DateDiffDay(project.PublishPublishDate, project.ContentRecheckDate) + ")";
                }
                else { this.PublishDuration.Text = "(" + DateTimeHandle.DateDiffDay(project.PublishPublishDate, project.ProductionCheckDate) + ")"; }
            }

        }
        #endregion 时间

        protected void ShowProgressPanel(Guid ProjectId)
        {
            #region Ajax需要初始化状态
            this.PanelProgress.Visible = true;
            this.ProgressCapture.Visible = true;
            ProgressCapture.BackColor = System.Drawing.ColorTranslator.FromHtml("#e3f0f6");
            CaptureDuration.Text = "";
            CaptureState.Text = "";
            this.ProgressCaptureCheck.Visible = true;
            ProgressCaptureCheck.BackColor = System.Drawing.ColorTranslator.FromHtml("#e3f0f6");
            CaptureCheckDuration.Text = "";
            CaptureCheckState.Text = "";
            this.ProgressShorthand.Visible = true;
            ProgressShorthand.BackColor = System.Drawing.ColorTranslator.FromHtml("#e3f0f6");
            ShorthandDuration.Text = "";
            ShorthandState.Text = "";
            this.ProgressContentReceive.Visible = true;
            ProgressContentReceive.BackColor = System.Drawing.ColorTranslator.FromHtml("#e3f0f6");
            ContentReceiveDuration.Text = "";
            ContentReceiveState.Text = "";
            this.ProgressContentOperator.Visible = true;
            ProgressContentOperator.BackColor = System.Drawing.ColorTranslator.FromHtml("#e3f0f6");
            ContentOperatorDuration.Text = "";
            ContentOperatorState.Text = "";
            this.ProgressContentCheck.Visible = true;
            ProgressContentCheck.BackColor = System.Drawing.ColorTranslator.FromHtml("#e3f0f6");
            ContentCheckDuration.Text = "";
            ContentCheckState.Text = "";
            this.ProgressContentRecheck.Visible = true;
            ProgressContentRecheck.BackColor = System.Drawing.ColorTranslator.FromHtml("#e3f0f6");
            ContentRecheckDuration.Text = "";
            ContentRecheckState.Text = "";
            this.ProgressProductionReceive.Visible = true;
            ProgressProductionReceive.BackColor = System.Drawing.ColorTranslator.FromHtml("#e3f0f6");
            ProductionReceiveDuration.Text = "";
            ProductionReceiveState.Text = "";
            this.ProgressProductionOperator.Visible = true;
            ProgressProductionOperator.BackColor = System.Drawing.ColorTranslator.FromHtml("#e3f0f6");
            ProductionOperatorDuration.Text = "";
            ProductionOperatorState.Text = "";
            this.ProgressProductionCheck.Visible = true;
            ProgressProductionCheck.BackColor = System.Drawing.ColorTranslator.FromHtml("#e3f0f6");
            ProductionCheckDuration.Text = "";
            ProductionCheckState.Text = "";
            this.ProgressPublish.Visible = true;
            ProgressPublish.BackColor = System.Drawing.ColorTranslator.FromHtml("#e3f0f6");
            PublishDuration.Text = "";
            PublishState.Text = "";
            this.ProgressCheck.Visible = true;
            ProgressCheck.BackColor = System.Drawing.ColorTranslator.FromHtml("#e3f0f6");
            CheckDuration.Text = "";
            CheckState.Text = "";
            //新流程
            this.NewProgressProductionReceive.Visible = false;
            NewProgressProductionReceive.BackColor = System.Drawing.ColorTranslator.FromHtml("#e3f0f6");
            ProductionReceiveDuration.Text = "";
            ProductionReceiveState.Text = "";
            this.NewProgressProductionOperator.Visible = false;
            NewProgressProductionOperator.BackColor = System.Drawing.ColorTranslator.FromHtml("#e3f0f6");
            ProductionOperatorDuration.Text = "";
            ProductionOperatorState.Text = "";
            this.NewProgressProductionCheck.Visible = false;
            NewProgressProductionCheck.BackColor = System.Drawing.ColorTranslator.FromHtml("#e3f0f6");
            ProductionCheckDuration.Text = "";
            ProductionCheckState.Text = "";
            this.NewProgressSTT.Visible = false;
            NewProgressSTT.BackColor = System.Drawing.ColorTranslator.FromHtml("#e3f0f6");
            NewProgressSTTDuration.Text = "";
            NewProgressSTTState.Text = "";
            #endregion
            BLL.Project project = BLL.Project.GetProject(ProjectId);
            this.ProgressNo.Text = "编号：" + project.ProjectNo.ToString();
            this.ProgressDate.Text = "派单时间：" + project.SendingDate.ToString("yy-MM-dd HH:mm");
            this.ProgressTitle.Text = project.CourseName.ToString();
            this.ProgressLecturer.Text = project.lecturer.ToString();
            #region 新流程
            if (project.ProjectTypeId == new Guid("00000000-0000-0000-0000-000000000199"))
            {
                ProgressProductionReceive.Visible = false;
                NewProgressProductionReceive.Visible = true;
                ProgressProductionOperator.Visible = false;
                NewProgressProductionOperator.Visible = true;
                ProgressProductionCheck.Visible = false;
                NewProgressProductionCheck.Visible = true;
                NewProgressSTT.Visible = true;
            }
            #endregion 新流程
            #region 采集
            if (//无采集
            project.ProjectTypeId == new Guid("00000000-0000-0000-0000-000000000019")
            || project.ProjectTypeId == new Guid("00000000-0000-0000-0000-000000000038")
            || project.ProjectTypeId == new Guid("00000000-0000-0000-0000-000000000039")
                )
            {
                this.ProgressCapture.Visible = false;
            }
            else
            {
                this.CaptureDuration.Text = "(" + DateTimeHandle.DateDiffHour(DateTime.Now, project.SendingDate) + ")";
                if (project.progress == new Guid("00000000-0000-0000-0000-000000000105"))
                {
                    this.CaptureState.Text = "等待接收";
                    ProgressCapture.BackColor = System.Drawing.ColorTranslator.FromHtml("#e29e4b");
                }
                else if (project.progress == new Guid("00000000-0000-0000-0000-000000000108"))
                {
                    this.CaptureState.Text = "正在采集";
                    ProgressCapture.BackColor = System.Drawing.ColorTranslator.FromHtml("#b7d28d");
                }
                else if (project.progress == new Guid("00000000-0000-0000-0000-000000000131"))
                {
                    this.CaptureState.Text = "延迟接收";
                    ProgressCapture.BackColor = System.Drawing.ColorTranslator.FromHtml("#f55066");
                }
                //else if (project.CaptureFinishDate != Convert.ToDateTime("0001/1/1 0:00:00"))
                else if (project.CaptureFinishDate != new DateTime(0001, 1, 1, 00, 00, 00))
                {
                    this.CaptureState.Text = "√";
                    ProgressCapture.BackColor = System.Drawing.ColorTranslator.FromHtml("#02f1e4");
                    if (project.CaptureReceiveDelayDate != new DateTime(0001, 1, 1, 00, 00, 00))
                    {
                        ProgressCapture.BackColor = System.Drawing.ColorTranslator.FromHtml("#fecf45");
                        this.CaptureDuration.Text = "(" + DateTimeHandle.DateDiffDay(project.CaptureFinishDate, project.SendingDate) + ")";
                        this.CaptureState.Text = "推迟" + DateTimeHandle.DateDiffHour(project.CaptureFinishDate, project.CaptureReceiveDelayDate) + "完成";
                    }
                    else
                    {
                        this.CaptureDuration.Text = "(" + DateTimeHandle.DateDiffDay(project.CaptureFinishDate, project.SendingDate) + ")";
                    }
                }
                else { }
            }
            #endregion
            #region 预审
            if (project.progress == new Guid("00000000-0000-0000-0000-000000000120"))
            {
                this.CaptureCheckState.Text = "等待预审";
                ProgressCaptureCheck.BackColor = System.Drawing.ColorTranslator.FromHtml("#e29e4b");
                ShowCaptureCheckDurationUnfinish(project);
            }
            else if (project.progress == new Guid("00000000-0000-0000-0000-000000000121"))
            {
                this.CaptureCheckState.Text = "准备派发制作";
                ProgressCaptureCheck.BackColor = System.Drawing.ColorTranslator.FromHtml("#02f1e4");
                ShowCaptureCheckDurationUnfinish(project);
            }
            else if (project.ExecutionDate != new DateTime(0001, 1, 1, 00, 00, 00))
            {
                this.CaptureCheckState.Text = "√";
                ProgressCaptureCheck.BackColor = System.Drawing.ColorTranslator.FromHtml("#02f1e4");
                ShowCaptureCheckDurationFinish(project);
            }
            else { }
            #endregion
            #region 速记
            if (//无速记
            project.ProjectTypeId == new Guid("00000000-0000-0000-0000-000000000017")
            || project.ProjectTypeId == new Guid("00000000-0000-0000-0000-000000000019")
            || project.ProjectTypeId == new Guid("00000000-0000-0000-0000-000000000021")
            || project.ProjectTypeId == new Guid("00000000-0000-0000-0000-000000000037")
            || project.ProjectTypeId == new Guid("00000000-0000-0000-0000-000000000038")
            //新三分屏无速记 
            || project.ProjectTypeId == new Guid("00000000-0000-0000-0000-000000000199")
                )
            {
                this.ProgressShorthand.Visible = false;
            }
            else
            {
                if (project.progress == new Guid("00000000-0000-0000-0000-000000000109"))
                {
                    this.ShorthandState.Text = "等待接收";
                    ProgressShorthand.BackColor = System.Drawing.ColorTranslator.FromHtml("#e29e4b");
                    this.ShorthandDuration.Text = "(" + DateTimeHandle.DateDiffHour(DateTime.Now, project.ExecutionDate) + ")";
                }
                else if (project.progress == new Guid("00000000-0000-0000-0000-000000000110"))
                {
                    this.ShorthandState.Text = "正在制作";
                    ProgressShorthand.BackColor = System.Drawing.ColorTranslator.FromHtml("#b7d28d");
                    //暂时没有速记接收
                    //this.ShorthandDuration.Text = "(" + DateTimeHandle.DateDiff(DateTime.Now, project.ShorthandReceiveDate) + ")";
                    this.ShorthandDuration.Text = "(" + DateTimeHandle.DateDiffHour(DateTime.Now, project.ExecutionDate) + ")";
                }
                else if (project.ShorthandFinishDate != new DateTime(0001, 1, 1, 00, 00, 00))
                {
                    this.ShorthandState.Text = "√";
                    ProgressShorthand.BackColor = System.Drawing.ColorTranslator.FromHtml("#02f1e4");
                    this.ShorthandDuration.Text = "(" + DateTimeHandle.DateDiffDay(project.ShorthandFinishDate, project.ExecutionDate) + ")";
                }
                else { }
            }
            #endregion
            #region 制作部
            if (project.ContentNeeds == new Guid("00000000-0000-0000-0000-000000000043"))//无制作部
            {
                this.ProgressContentReceive.Visible = false;
                this.ProgressContentOperator.Visible = false;
                this.ProgressContentCheck.Visible = false;
                this.ProgressContentRecheck.Visible = false;
            }
            else
            {
                //接收
                if (project.ContentProgress == new Guid("00000000-0000-0000-0000-000000000107"))
                {
                    this.ContentReceiveState.Text = "等待接收";
                    ProgressContentReceive.BackColor = System.Drawing.ColorTranslator.FromHtml("#e29e4b");
                    ShowContentReceiveDurationUnfinish(project);
                }
                else if (project.ContentAssignmentDate != new DateTime(0001, 1, 1, 00, 00, 00))
                {
                    this.ContentReceiveState.Text = "√";
                    ProgressContentReceive.BackColor = System.Drawing.ColorTranslator.FromHtml("#02f1e4");
                    ShowContentReceiveDurationFinish(project);
                }
                else { }
                //制作
                if (project.ContentProgress == new Guid("00000000-0000-0000-0000-000000000111"))
                {
                    this.ContentOperatorState.Text = "正在制作";
                    ProgressContentOperator.BackColor = System.Drawing.ColorTranslator.FromHtml("#b7d28d");
                    this.ContentOperatorDuration.Text = "(" + DateTimeHandle.DateDiffHour(DateTime.Now, project.ContentAssignmentDate) + ")";
                }
                else if (project.ContentFinishDate != new DateTime(0001, 1, 1, 00, 00, 00))
                {
                    this.ContentOperatorState.Text = "√";
                    ProgressContentOperator.BackColor = System.Drawing.ColorTranslator.FromHtml("#02f1e4");
                    this.ContentOperatorDuration.Text = "(" + DateTimeHandle.DateDiffDay(project.ContentFinishDate, project.ContentAssignmentDate) + ")";
                }
                else { }
                //初审
                if (project.ContentProgress == new Guid("00000000-0000-0000-0000-000000000112"))
                {
                    this.ContentCheckState.Text = "等待初审";
                    ProgressContentCheck.BackColor = System.Drawing.ColorTranslator.FromHtml("#e29e4b");
                    this.ContentCheckDuration.Text = "(" + DateTimeHandle.DateDiffHour(DateTime.Now, project.ContentFinishDate) + ")";
                }
                else if (project.ContentCheckDate != new DateTime(0001, 1, 1, 00, 00, 00))
                {
                    this.ContentCheckState.Text = "√";
                    ProgressContentCheck.BackColor = System.Drawing.ColorTranslator.FromHtml("#02f1e4");
                    this.ContentCheckDuration.Text = "(" + DateTimeHandle.DateDiffDay(project.ContentCheckDate, project.ContentFinishDate) + ")";
                }
                else { }
                //复审
                if (//单视频不复审
                    project.ProjectTypeId == new Guid("00000000-0000-0000-0000-000000000017")
                    || project.ProjectTypeId == new Guid("00000000-0000-0000-0000-000000000019")
                    )
                { this.ProgressContentRecheck.Visible = false; }
                else
                {
                    if (project.ContentProgress == new Guid("00000000-0000-0000-0000-000000000122"))
                    {
                        this.ContentRecheckState.Text = "等待复审";
                        ProgressContentRecheck.BackColor = System.Drawing.ColorTranslator.FromHtml("#e29e4b");
                        this.ContentRecheckDuration.Text = "(" + DateTimeHandle.DateDiffHour(DateTime.Now, project.ContentCheckDate) + ")";
                    }
                    else if (project.ContentRecheckDate != new DateTime(0001, 1, 1, 00, 00, 00))
                    {
                        this.ContentRecheckState.Text = "√";
                        ProgressContentRecheck.BackColor = System.Drawing.ColorTranslator.FromHtml("#02f1e4");
                        this.ContentRecheckDuration.Text = "(" + DateTimeHandle.DateDiffDay(project.ContentRecheckDate, project.ContentCheckDate) + ")";
                    }
                    else { }
                }
            }
            #endregion
            #region 技术部
            //接收
            if (project.ProductionProgress == new Guid("00000000-0000-0000-0000-000000000106"))
            {
                ShowProductionReceiveDurationUnfinish(project, "等待接收", "#e29e4b");
            }
            else if (project.progress == new Guid("00000000-0000-0000-0000-000000000132"))
            {
                ShowProductionReceiveDurationUnfinish(project, "延迟接收", "#f55066");
            }
            else if (project.ProductionReceiveDate != new DateTime(0001, 1, 1, 00, 00, 00))
            {
                ShowProductionReceiveDurationFinish(project, "√", "#02f1e4");
            }
            else { }
            //制作
            if (project.ProductionProgress == new Guid("00000000-0000-0000-0000-000000000114"))
            {
                if (project.ProjectTypeId == new Guid("00000000-0000-0000-0000-000000000199"))
                {
                    this.NewProductionOperatorState.Text = "正在制作";
                    NewProgressProductionOperator.BackColor = System.Drawing.ColorTranslator.FromHtml("#b7d28d");
                    this.NewProductionOperatorDuration.Text = "(" + DateTimeHandle.DateDiffHour(DateTime.Now, project.ProductionReceiveDate) + ")";
                }
                else
                {
                    this.ProductionOperatorState.Text = "正在制作";
                    ProgressProductionOperator.BackColor = System.Drawing.ColorTranslator.FromHtml("#b7d28d");
                    this.ProductionOperatorDuration.Text = "(" + DateTimeHandle.DateDiffHour(DateTime.Now, project.ProductionReceiveDate) + ")";
                }
            }
            else if (project.ProductionFinishDate != new DateTime(0001, 1, 1, 00, 00, 00))
            {
                if (project.ProjectTypeId == new Guid("00000000-0000-0000-0000-000000000199"))
                {
                    this.NewProductionOperatorState.Text = "√";
                    NewProgressProductionOperator.BackColor = System.Drawing.ColorTranslator.FromHtml("#02f1e4");
                    this.NewProductionOperatorDuration.Text = "(" + DateTimeHandle.DateDiffDay(project.ProductionFinishDate, project.ProductionReceiveDate) + ")";
                }
                else
                {
                    this.ProductionOperatorState.Text = "√";
                    ProgressProductionOperator.BackColor = System.Drawing.ColorTranslator.FromHtml("#02f1e4");
                    this.ProductionOperatorDuration.Text = "(" + DateTimeHandle.DateDiffDay(project.ProductionFinishDate, project.ProductionReceiveDate) + ")";
                }
            }
            else { }
            //审核
            if (project.ProductionProgress == new Guid("00000000-0000-0000-0000-000000000115"))
            {
                if (project.ProjectTypeId == new Guid("00000000-0000-0000-0000-000000000199"))
                {
                    this.NewProductionCheckState.Text = "等待审核";
                    NewProgressProductionCheck.BackColor = System.Drawing.ColorTranslator.FromHtml("#e29e4b");
                    this.NewProductionCheckDuration.Text = "(" + DateTimeHandle.DateDiffHour(DateTime.Now, project.ProductionFinishDate) + ")";
                }
                else
                {
                    this.ProductionCheckState.Text = "等待审核";
                    ProgressProductionCheck.BackColor = System.Drawing.ColorTranslator.FromHtml("#e29e4b");
                    this.ProductionCheckDuration.Text = "(" + DateTimeHandle.DateDiffHour(DateTime.Now, project.ProductionFinishDate) + ")";
                }
            }
            else if (project.ProductionCheckDate != new DateTime(0001, 1, 1, 00, 00, 00))
            {
                if (project.ProjectTypeId == new Guid("00000000-0000-0000-0000-000000000199"))
                {
                    this.NewProductionCheckState.Text = "√";
                    NewProgressProductionCheck.BackColor = System.Drawing.ColorTranslator.FromHtml("#02f1e4");
                    using (var ProjectModel = new ProjectCollection.WebUI.Models.ProjectCollectionEntities())
                    {
                        ProjectCollection.WebUI.Models.Project ThisProject = (from p in ProjectModel.Project
                                                                              where p.ProjectId.ToString() == project.ProjectId.ToString()
                                                                              select p).First();
                        this.NewProductionCheckDuration.Text = "(" + DateTimeHandle.DateDiffDay(Convert.ToDateTime(ThisProject.ProductionCheckDate), Convert.ToDateTime(ThisProject.VideoEncodeFinishDate)) + ")";
                    }
                }
                else
                {
                    this.ProductionCheckState.Text = "√";
                    ProgressProductionCheck.BackColor = System.Drawing.ColorTranslator.FromHtml("#02f1e4");
                    this.ProductionCheckDuration.Text = "(" + DateTimeHandle.DateDiffDay(project.ProductionCheckDate, project.ProductionFinishDate) + ")";
                }
            }
            else { }
            #endregion
            #region STT
            if (project.progress == new Guid("00000000-0000-0000-0000-000000000197"))
            {
                this.NewProgressSTTState.Text = "等待制作";
                NewProgressSTT.BackColor = System.Drawing.ColorTranslator.FromHtml("#e29e4b");
                this.NewProgressSTTDuration.Text = "(" + DateTimeHandle.DateDiffHour(DateTime.Now, project.ProductionCheckDate) + ")";
            }
            else if (project.ShorthandFinishDate != new DateTime(0001, 1, 1, 00, 00, 00))
            {
                this.NewProgressSTTState.Text = "√";
                NewProgressSTT.BackColor = System.Drawing.ColorTranslator.FromHtml("#02f1e4");
                this.NewProgressSTTDuration.Text = "(" + DateTimeHandle.DateDiffDay(project.ShorthandFinishDate, project.ProductionCheckDate) + ")";
            }
            #endregion STT
            #region 发布审核
            if (project.PublishNeeds == new Guid("00000000-0000-0000-0000-000000000043")) //无发布
            {
                this.ProgressPublish.Visible = false;
                this.ProgressCheck.Visible = false;
            }
            else
            {
                //发布
                if (project.progress == new Guid("00000000-0000-0000-0000-000000000117"))
                {
                    this.PublishState.Text = "等待发布";
                    ProgressPublish.BackColor = System.Drawing.ColorTranslator.FromHtml("#e29e4b");
                    ShowPublishDurationUnfinish(project);
                }
                else if (project.PublishPublishDate != new DateTime(0001, 1, 1, 00, 00, 00))
                {
                    this.PublishState.Text = "√";
                    ProgressPublish.BackColor = System.Drawing.ColorTranslator.FromHtml("#02f1e4");
                    ShowPublishDurationFinish(project);
                }
                else { }
                //审核
                if (project.progress == new Guid("00000000-0000-0000-0000-000000000118"))
                {
                    this.CheckState.Text = "等待审核";
                    ProgressCheck.BackColor = System.Drawing.ColorTranslator.FromHtml("#e29e4b");
                    this.CheckDuration.Text = "(" + DateTimeHandle.DateDiffDay(DateTime.Now, project.PublishPublishDate) + ")";
                }
                else if (project.CheckTaskCheckDate != new DateTime(0001, 1, 1, 00, 00, 00))
                {
                    this.CheckState.Text = "√";
                    ProgressCheck.BackColor = System.Drawing.ColorTranslator.FromHtml("#02f1e4");
                    this.CheckDuration.Text = "(" + DateTimeHandle.DateDiffDay(project.CheckTaskCheckDate, project.PublishPublishDate) + ")";
                }
                else { }
            }
            #endregion
        }

        #endregion
    }
}