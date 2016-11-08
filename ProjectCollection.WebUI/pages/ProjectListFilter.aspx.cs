using ProjectCollection.BLL;
using ProjectCollection.Common;
using ProjectCollection.WebUI.pages.common;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Web;
using System.Web.UI.WebControls;

namespace ProjectCollection.WebUI.pages
{
    public partial class ProjectListFilter : System.Web.UI.Page
    {
        public Guid type;
        public string mode;

        #region 事件

        protected void Page_Load(object sender, EventArgs e)
        {
            //type = new Guid(this.Request["type"]);
            mode = "browse"; //mode = this.Request["mode"];
            DataBindProjectList(type, mode);
            if (mode == "browse")
            {
                ShowBrowsePanel();
            }
            else { }
            //ViewState
            if (hidViewState.Value == "True")
            {
                DevExpress.Web.GridViewColumn ColOperate = (DevExpress.Web.GridViewColumn)axgvProject.Columns[7];
                DevExpress.Web.ASPxCheckBox cbDelay = (DevExpress.Web.ASPxCheckBox)axgvProject.FindHeaderTemplateControl(ColOperate, "cbDelay");
                cbDelay.Checked = true;
            }
            else { }

            //
            ShowCopyPanel();
        }

        protected void axgvProject_PageIndexChanged(object sender, EventArgs e)
        {
            DataBindProjectList(type, mode);
        }

        protected void axgvProject_CustomButtonCallback(object sender, DevExpress.Web.ASPxGridViewCustomButtonCallbackEventArgs e)
        {
            if (e.ButtonID == "Select")
            {

            }
            else { }
        }

        protected void axgvProject_BeforePerformDataSelect(object sender, EventArgs e)
        {

        }

        protected void axgvProject_DataHandle(object sender, EventArgs e)
        {
            //if (hidViewState.Value == "True")
            //{
            //    DevExpress.Web.GridViewColumn ColOperate = (DevExpress.Web.GridViewColumn)axgvProject.Columns[7];
            //    DevExpress.Web.ASPxCheckBox cbDelay = (DevExpress.Web.ASPxCheckBox)axgvProject.FindHeaderTemplateControl(ColOperate, "cbDelay");
            //    cbDelay.Checked = true;
            //}
            //else { }

            //mode
            if (mode == "browse")
            {
                ShowBrowsePanel();
            }
            else { }
            ShowCopyPanel();
        }

        protected void btnShowPopup_Command(object sender, CommandEventArgs e)
        {
            string id = e.CommandArgument.ToString();
            popupEdit.ContentUrl = "~/pages/ProjectCreateEditEmbed.aspx?ProjectId=" + id + "&mode=" + e.CommandName;
            //popupEdit.ContentUrl = "~/pages/ProjectCreateEditEmbed.aspx?ProjectId=" + id + "&mode=" + mode;
            popupEdit.ShowOnPageLoad = true;
        }

        protected void cbDelay_CheckedChanged(object sender, EventArgs e)
        {
              
        }

        protected void SelectSetChanged(object sender, EventArgs e)
        {
            DataBindProjectList(type, mode);
        }

        protected void btnProjectProgress_Click(object sender, CommandEventArgs e)
        {
            ShowProgressPanel(new Guid(e.CommandArgument.ToString()));
        }

        protected void btnProgressClose_Click(object sender, EventArgs e)
        {
            this.PanelProgress.Visible = false;
        }

        #endregion 事件

        #region 方法

        private void DataBindProjectList(Guid type, string mode)
        {
            if (mode == "browse")
            {
                //DevExpress.Web.GridViewColumn ColOperate = (DevExpress.Web.GridViewColumn)axgvProject.Columns[7];
                //DevExpress.Web.ASPxCheckBox cbDelay = (DevExpress.Web.ASPxCheckBox)axgvProject.FindHeaderTemplateControl(ColOperate, "cbDelay");
                //if (cbDelay.Checked == true)
                //{
                //    List<Project> Projects = BLL.Project.GetAllProject();
                //    var DelayProject = from CurrentProject in Projects
                //                       where CurrentProject.IsDelay == true
                //                       select CurrentProject;
                //    this.axgvProject.DataSource = DelayProject.ToList();
                //    this.axgvProject.DataBind();
                //    hidViewState.Value = cbDelay.Checked.ToString();
                //}
                //else
                //{
                //    this.axgvProject.DataSource = BLL.Project.GetAllProject();
                //    this.axgvProject.DataBind();
                //    hidViewState.Value = cbDelay.Checked.ToString();
                //}
                Guid FilterListPlanType = new Guid(this.RadioButtonListPlanType.SelectedValue.ToString());
                Guid FilterProgress = new Guid(this.RadioButtonListProgress.SelectedValue.ToString());
                //
                if (FilterListPlanType.ToString() == "99999999-9999-9999-9999-999999999999")
                {
                    Response.Redirect("~/pages/CustomProjectList.aspx?type=00000000-0000-0000-0000-000000000202&mode=browse");
                }
                else { }
                //
                if (FilterProgress.ToString() == "00000000-0000-0000-0000-999999999999")
                {
                    List<Project> Projects = BLL.Project.GetAllProject();
                    var DelayProject = from CurrentProject in Projects
                                       where CurrentProject.IsDelay == true
                                       select CurrentProject;
                    this.axgvProject.DataSource = DelayProject.ToList();
                    this.axgvProject.DataBind();
                }
                else
                {
                    this.axgvProject.DataSource = BLL.Project.GetAllProject(FilterListPlanType, FilterProgress);
                    this.axgvProject.DataBind();
                }
                ShowBrowsePanel();
                ShowCopyPanel();
            }
            else if (mode == "copy")
            {
                this.axgvProject.DataSource = BLL.Project.GetAllProject();
                this.axgvProject.DataBind();
            }
            else { }
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
            else
            { this.ContentReceiveDuration.Text = "(" + DateTimeHandle.DateDiffDay(project.ContentAssignmentDate, project.ShorthandFinishDate) + ")"; }
        }
        protected void ShowProductionReceiveDurationUnfinish(Project project)
        {
            if (//单视频(必无速记)
                project.ProjectTypeId == new Guid("00000000-0000-0000-0000-000000000017")
                || project.ProjectTypeId == new Guid("00000000-0000-0000-0000-000000000019")
                )
            { this.ProductionReceiveDuration.Text = "(" + DateTimeHandle.DateDiffHour(DateTime.Now, project.ExecutionDate) + ")"; }
            else//三分屏
            {
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
        protected void ShowProductionReceiveDurationFinish(Project project)
        {
            if (//单视频(必无速记)
                project.ProjectTypeId == new Guid("00000000-0000-0000-0000-000000000017")
                || project.ProjectTypeId == new Guid("00000000-0000-0000-0000-000000000019")
                )
            { this.ProductionReceiveDuration.Text = DateTimeHandle.DateDiffDay(project.ProductionReceiveDate, project.ExecutionDate); }
            else//三分屏
            {
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
            #endregion
            BLL.Project project = BLL.Project.GetProject(ProjectId);
            this.ProgressNo.Text = "编号：" + project.ProjectNo.ToString();
            this.ProgressDate.Text = "派单时间：" + project.SendingDate.ToString("yy-MM-dd HH:mm");
            this.ProgressTitle.Text = project.CourseName.ToString();
            this.ProgressLecturer.Text = project.lecturer.ToString();
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
                this.ProductionReceiveState.Text = "等待接收";
                ProgressProductionReceive.BackColor = System.Drawing.ColorTranslator.FromHtml("#e29e4b");
                ShowProductionReceiveDurationUnfinish(project);
            }
            else if (project.progress == new Guid("00000000-0000-0000-0000-000000000132"))
            {
                this.ProductionReceiveState.Text = "延迟接收";
                ProgressProductionReceive.BackColor = System.Drawing.ColorTranslator.FromHtml("#f55066");
                ShowProductionReceiveDurationUnfinish(project);
            }
            else if (project.ProductionReceiveDate != new DateTime(0001, 1, 1, 00, 00, 00))
            {
                this.ProductionReceiveState.Text = "√";
                ProgressProductionReceive.BackColor = System.Drawing.ColorTranslator.FromHtml("#02f1e4");
                ShowProductionReceiveDurationFinish(project);
            }
            else { }
            //制作
            if (project.ProductionProgress == new Guid("00000000-0000-0000-0000-000000000114"))
            {
                this.ProductionOperatorState.Text = "正在制作";
                ProgressProductionOperator.BackColor = System.Drawing.ColorTranslator.FromHtml("#b7d28d");
                this.ProductionOperatorDuration.Text = "(" + DateTimeHandle.DateDiffHour(DateTime.Now, project.ProductionReceiveDate) + ")";
            }
            else if (project.ProductionFinishDate != new DateTime(0001, 1, 1, 00, 00, 00))
            {
                this.ProductionOperatorState.Text = "√";
                ProgressProductionOperator.BackColor = System.Drawing.ColorTranslator.FromHtml("#02f1e4");
                this.ProductionOperatorDuration.Text = "(" + DateTimeHandle.DateDiffDay(project.ProductionFinishDate, project.ProductionReceiveDate) + ")";
            }
            else { }
            //审核
            if (project.ProductionProgress == new Guid("00000000-0000-0000-0000-000000000115"))
            {
                this.ProductionCheckState.Text = "等待审核";
                ProgressProductionCheck.BackColor = System.Drawing.ColorTranslator.FromHtml("#e29e4b");
                this.ProductionCheckDuration.Text = "(" + DateTimeHandle.DateDiffHour(DateTime.Now, project.ProductionFinishDate) + ")";
            }
            else if (project.ProductionCheckDate != new DateTime(0001, 1, 1, 00, 00, 00))
            {
                this.ProductionCheckState.Text = "√";
                ProgressProductionCheck.BackColor = System.Drawing.ColorTranslator.FromHtml("#02f1e4");
                this.ProductionCheckDuration.Text = "(" + DateTimeHandle.DateDiffDay(project.ProductionCheckDate, project.ProductionFinishDate) + ")";
            }
            else { }
            #endregion
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

        protected void ShowBrowsePanel() 
        {
            PanelFilter.Visible = true;
            //DevExpress.Web.GridViewColumn ColOperate = (DevExpress.Web.GridViewColumn)axgvProject.Columns[7];
            //Panel panelBrowseHead = (Panel)axgvProject.FindHeaderTemplateControl(ColOperate, "panelBrowseHead");
            //panelBrowseHead.Visible = true;
            List<object> keyValues = axgvProject.GetCurrentPageRowValues(axgvProject.KeyFieldName);
            foreach (object key in keyValues)
            {
                Panel CurrentPanel = (Panel)axgvProject.FindRowCellTemplateControlByKey(key, (DevExpress.Web.GridViewDataColumn)axgvProject.Columns["Operate"], "panelBrowseCol");
                CurrentPanel.Visible = true;
            }
        }

        protected void ShowCopyPanel()
        {
            UserInfo LoginUserInfo = (UserInfo)Session["key_userInfo"];
            if (LoginUserInfo.Authority.Contains("copy"))
            {
                List<object> keyValues = axgvProject.GetCurrentPageRowValues(axgvProject.KeyFieldName);
                foreach (object key in keyValues)
                {
                    Panel CurrentPanel = (Panel)axgvProject.FindRowCellTemplateControlByKey(key, (DevExpress.Web.GridViewDataColumn)axgvProject.Columns["Operate"], "panelCopyCol");
                    CurrentPanel.Visible = true;
                }
            }
            else { }

        }

        #endregion 方法

    }
}