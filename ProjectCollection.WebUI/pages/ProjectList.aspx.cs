using ProjectCollection.BLL;
using ProjectCollection.Common;
using ProjectCollection.WebUI.pages.common;
using System;
using System.Linq;
using System.Configuration;
using System.Collections.Generic;
using System.Web.UI.WebControls;

namespace ProjectCollection.WebUI.pages
{
    public partial class ProjectList : BasePage
    {
        public List<Guid> BatchProjectId = new List<Guid>();
        //每次点击按钮都会执行
        //public Guid FilterListPlanType = new Guid();
        //public Guid FilterProgress = new Guid();

        #region 事件

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //点按钮,点翻页都不执行（Page_Load未执行）
                //FilterListPlanType = new Guid(this.RadioButtonListPlanType.SelectedValue.ToString());
                //FilterProgress = new Guid(this.RadioButtonListProgress.SelectedValue.ToString());
                if (this.Request["mode"] == "browse")
                {
                    SearchProjectListFilter();
                }
                else
                {
                    SearchProjectList();
                }
            }
            else
            {

            }
            if (this.Request["mode"] == "browse")
            {
                //
                this.PanelFilter.Visible = true;
                this.gvProject.Columns[0].Visible = false;
                #region Page状态
                //非Ajax
                //if (this.Request["page"] != null)
                //{
                //    this.gvProject.PageIndex = Convert.ToInt16(this.Request["page"]);
                //    this.SearchProjectList();
                //}
                //else { }
                //if (this.Request["projectid"] != null)
                //{
                //    Guid ProjectId = new Guid(this.Request["projectid"].ToString());
                //    ShowProgressPanel(ProjectId);
                //}
                //else{ }
                #endregion
            }
            else if (this.Request["mode"] == "copy")
            {
                this.gvProject.Columns[7].Visible = false;
                this.gvProject.Columns[8].Visible = true;
            }
            else if (this.Request["mode"] == "shorthand")
            {
                btnBatchHandle.Visible = true;
                btnBatchHandle.Text = "批量完成";
                btnBatchHandle.PostBackUrl = "~/pages/ProjectCreateEdit.aspx?mode=shorthandbatchhandle";
            }
            else if (this.Request["mode"] == "contentreceive")
            {
                //btnBatchSave.Visible = true;
                btnBatchHandle.Visible = true;
                btnBatchHandle.Text = "批量接收";
                btnBatchHandle.PostBackUrl = "~/pages/ProjectCreateEdit.aspx?mode=contentreceivebatchhandle";
            }
            else if (this.Request["mode"] == "contentcheck")
            {
                btnBatchSave.Visible = true;
                btnBatchHandle.Visible = true;
                btnBatchHandle.Text = "批量通过";
                btnBatchSave.PostBackUrl = "~/pages/ProjectCreateEdit.aspx?mode=contentcheckbatchsave";
                btnBatchHandle.PostBackUrl = "~/pages/ProjectCreateEdit.aspx?mode=contentcheckbatchhandle";
            }
            else if (this.Request["mode"] == "productionreceive")
            {
                btnBatchHandle.Visible = true;
                btnBatchHandle.Text = "批量接收";
                btnBatchHandle.PostBackUrl = "~/pages/ProjectCreateEdit.aspx?mode=productionreceivebatchhandle";
            }
            else if (this.Request["mode"] == "productionfinish")
            {
                btnBatchHandle.Visible = true;
                btnBatchHandle.Text = "批量完成";
                btnBatchHandle.PostBackUrl = "~/pages/ProjectCreateEdit.aspx?mode=productionfinishbatchhandle";
            }
            else if (this.Request["mode"] == "productioncheck")
            {
                btnBatchSave.Visible = true;
                btnBatchHandle.Visible = true;
                btnBatchHandle.Text = "批量通过";
                btnBatchSave.PostBackUrl = "~/pages/ProjectCreateEdit.aspx?mode=productioncheckbatchsave";
                btnBatchHandle.PostBackUrl = "~/pages/ProjectCreateEdit.aspx?mode=productioncheckbatchhandle";
            }
            else
            {

            }
            if (this.gvProject.Rows.Count < 1)
            {
                btnBatchSave.Visible = false;
                btnBatchHandle.Visible = false;
            }
            else
            {

            }
        }
        //
        protected void gvProject_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
            Guid identity = Guid.Parse(this.gvProject.DataKeys[e.NewSelectedIndex].Values["ProjectId"].ToString());
            //NewPms
            BLL.Project project = BLL.Project.GetProject(identity);
            if (this.Request["mode"] == "browse")
            {
                this.Response.Redirect("~/pages/ProjectCreateEdit.aspx?mode=browse&ProjectId=" + identity);
            }
            else if (this.Request["mode"] == "capture")
            {
                this.Redirect("~/pages/ProjectCreateEdit.aspx?mode=capture&ProjectId=" + identity);
            }
            else if (this.Request["mode"] == "capturecheck")
            {
                this.Redirect("~/pages/ProjectCreateEdit.aspx?mode=capturecheck&ProjectId=" + identity);
            }
            else if (this.Request["mode"] == "capturecheck")
            {
                this.Redirect("~/pages/ProjectCreateEdit.aspx?mode=capturecheck&ProjectId=" + identity);
            }
            else if (this.Request["mode"] == "execution")
            {
                this.Redirect("~/pages/ProjectCreateEdit.aspx?mode=execution&ProjectId=" + identity);
            }
            else if (this.Request["mode"] == "shorthand")
            {
                this.Redirect("~/pages/ProjectCreateEdit.aspx?mode=shorthand&ProjectId=" + identity);
            }
            else if (this.Request["mode"] == "contentreceive")
            {
                this.Redirect("~/pages/ProjectCreateEdit.aspx?mode=contentreceive&ProjectId=" + identity);
            }
            else if (this.Request["mode"] == "contentfinish")
            {
                this.Redirect("~/pages/ProjectCreateEdit.aspx?mode=contentfinish&ProjectId=" + identity);
            }
            else if (this.Request["mode"] == "contentcheck")
            {
                this.Redirect("~/pages/ProjectCreateEdit.aspx?mode=contentcheck&ProjectId=" + identity);
            }
            else if (this.Request["mode"] == "contentrecheck")
            {
                this.Redirect("~/pages/ProjectCreateEdit.aspx?mode=contentrecheck&ProjectId=" + identity);
            }
            else if (this.Request["mode"] == "productionreceive")
            {
                //NewPms
                this.Redirect("~/pages/ProjectCreateEdit.aspx?mode=productionreceive&ProjectId=" + identity);
                //this.Redirect("http://192.168.194.88:666/AjaxVideoUploadPage/?CourseTitle=" + project.CourseName);
            }
            else if (this.Request["mode"] == "productionfinish")
            {
                this.Redirect("~/pages/ProjectCreateEdit.aspx?mode=productionfinish&ProjectId=" + identity);
            }
            else if (this.Request["mode"] == "productioncheck")
            {
                this.Redirect("~/pages/ProjectCreateEdit.aspx?mode=productioncheck&ProjectId=" + identity);
            }
            else if (this.Request["mode"] == "publish")
            {
                this.Redirect("~/pages/ProjectCreateEdit.aspx?mode=publish&ProjectId=" + identity);
            }
            else if (this.Request["mode"] == "check")
            {
                this.Redirect("~/pages/ProjectCreateEdit.aspx?mode=check&ProjectId=" + identity);
            }
            else
            {
            }
        }
        protected void CustomersGridView_RowCommand(Object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Copy" && this.Request["mode"] == "copy")
            {
                Guid identity = Guid.Parse(this.gvProject.DataKeys[Convert.ToInt32(e.CommandArgument)].Values["ProjectId"].ToString());
                this.Redirect("~/pages/ProjectCreateEdit.aspx?mode=copy&ProjectId=" + identity);
            }
            else
            {

            }

        }
        protected void gvProject_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvProject.PageIndex = e.NewPageIndex;
            if (this.Request["mode"] == "browse")
            {
                SearchProjectListFilter();
            }
            else
            {
                SearchProjectList();
            }
        }
        //
        protected void CheckBoxAll_Changed(object sender, EventArgs e)
        {
            bool isChecked = ((CheckBox)(gvProject.HeaderRow.Cells[0].FindControl("CheckBoxAll"))).Checked;
            foreach (GridViewRow gvRow in gvProject.Rows)
            {
                ((CheckBox)(gvRow.Cells[0].FindControl("SelectCheckBox"))).Checked = isChecked;
            }
        }
        protected void btnShowProgress_Click(object sender, EventArgs e)
        {
            Button BtnSelf = (Button)sender;
            DataControlFieldCell ThisTd = (DataControlFieldCell)BtnSelf.Parent;
            GridViewRow ThisTr = (GridViewRow)ThisTd.Parent;
            Guid ProjectId = new Guid(gvProject.DataKeys[ThisTr.RowIndex].Value.ToString());
            ShowProgressPanel(ProjectId);
        }
        protected void btnBatch_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < gvProject.Rows.Count; i++)
            {
                GridViewRow row = gvProject.Rows[i];
                bool isChecked = ((CheckBox)row.FindControl("SelectCheckBox")).Checked;
                if (isChecked)
                {
                    //Guid CurrentId = Guid.Parse(gvProject.DataKeys[gvProject.Rows[i].DataItemIndex].Values["ProjectId"].ToString());
                    Guid CurrentId = Guid.Parse(gvProject.DataKeys[i].Values["ProjectId"].ToString());
                    BatchProjectId.Add(CurrentId);
                }
                else
                {

                }
            }
        }
        protected void btnFilter_Click(object sender, EventArgs e)
        {
            //this.gvProject.PageIndex = 0;//非Ajax重置
            this.PanelProgress.Visible = false;
            SearchProjectListFilter();
        }
        protected void SelectSetChanged(object sender, EventArgs e)
        {
            SearchProjectListFilter();
        }

        #endregion 事件

        #region 方法
        //Bind
        private void SearchProjectList()
        {
            if (this.Request["mode"] == "browse")
            {
                this.gvProject.DataSource = BLL.Project.GetAllProject();
                this.gvProject.DataBind();
            }
            else if (this.Request["mode"] == "capture")
            {
                this.gvProject.DataSource = BLL.Project.GetCategoryProject("ProgressCapture");
                this.gvProject.DataBind();
            }
            else if (this.Request["mode"] == "capturecheck")
            {
                this.gvProject.DataSource = BLL.Project.GetCategoryProject("ProgressCaptureCheck");
                this.gvProject.DataBind();
            }
            else if (this.Request["mode"] == "execution")
            {
                this.gvProject.DataSource = BLL.Project.GetDictionaryIdProject(new Guid("00000000-0000-0000-0000-000000000121"));
                this.gvProject.DataBind();
            }
            else if (this.Request["mode"] == "shorthand")
            {
                this.gvProject.DataSource = BLL.Project.GetDictionaryIdProject(new Guid("00000000-0000-0000-0000-000000000109"));
                this.gvProject.DataBind();
            }
            else if (this.Request["mode"] == "contentreceive")
            {
                this.gvProject.DataSource = BLL.Project.GetDictionaryContentIdProject(new Guid("00000000-0000-0000-0000-000000000107"));
                this.gvProject.DataBind();
            }
            else if (this.Request["mode"] == "contentfinish")
            {
                this.gvProject.DataSource = BLL.Project.GetDictionaryContentIdProject(new Guid("00000000-0000-0000-0000-000000000111"), new Guid("00000000-0000-0000-0000-000000000124"), LoginUserInfo.Identity);
                this.gvProject.DataBind();
            }
            else if (this.Request["mode"] == "contentcheck")
            {
                this.gvProject.DataSource = BLL.Project.GetDictionaryContentIdProject(new Guid("00000000-0000-0000-0000-000000000112"), new Guid("00000000-0000-0000-0000-000000000123"));
                this.gvProject.DataBind();
            }
            else if (this.Request["mode"] == "contentrecheck")
            {
                this.gvProject.DataSource = BLL.Project.GetDictionaryContentIdProject(new Guid("00000000-0000-0000-0000-000000000122"));
                this.gvProject.DataBind();
            }
            else if (this.Request["mode"] == "productionreceive")
            {
                this.gvProject.DataSource = BLL.Project.GetDictionaryProductionIdProject(new Guid("00000000-0000-0000-0000-000000000106"), new Guid("00000000-0000-0000-0000-000000000132"));
                this.gvProject.DataBind();
            }
            else if (this.Request["mode"] == "productionfinish")
            {
                this.gvProject.DataSource = BLL.Project.GetDictionaryProductionIdProject(new Guid("00000000-0000-0000-0000-000000000114"), new Guid("00000000-0000-0000-0000-000000000125"), LoginUserInfo.Identity);
                this.gvProject.DataBind();
            }
            else if (this.Request["mode"] == "productioncheck")
            {
                this.gvProject.DataSource = BLL.Project.GetDictionaryProductionIdProject(new Guid("00000000-0000-0000-0000-000000000115"));
                this.gvProject.DataBind();
            }
            else if (this.Request["mode"] == "publish")
            {
                this.gvProject.DataSource = BLL.Project.GetDictionaryIdProject(new Guid("00000000-0000-0000-0000-000000000117"));
                this.gvProject.DataBind();
            }
            else if (this.Request["mode"] == "check")
            {
                this.gvProject.DataSource = BLL.Project.GetDictionaryIdProject(new Guid("00000000-0000-0000-0000-000000000118"));
                this.gvProject.DataBind();
            }
            else
            {
                this.gvProject.DataSource = BLL.Project.GetAllProject();
                this.gvProject.DataBind();
            }
        }
        private void SearchProjectListFilter()
        {
            Guid FilterListPlanType = new Guid(this.RadioButtonListPlanType.SelectedValue.ToString());
            Guid FilterProgress = new Guid(this.RadioButtonListProgress.SelectedValue.ToString());
            if (FilterProgress.ToString() == "00000000-0000-0000-0000-999999999999")
            {
                List<Project> Projects = BLL.Project.GetAllProject();
                var DelayProject = from CurrentProject in Projects
                                   where CurrentProject.IsDelay == true
                                   select CurrentProject;
                this.gvProject.DataSource = DelayProject.ToList();
                this.gvProject.DataBind();
            }
            else
            {
                this.gvProject.DataSource = BLL.Project.GetAllProject(FilterListPlanType, FilterProgress,"all");
                this.gvProject.DataBind();
            }
        }
        //
        protected void gvProject_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //e.Row.Attributes.Add("onmouseover", "c=this.style.backgroundColor;this.style.backgroundColor='#95B8FF'");
            //e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=c");
            //e.Row.Attributes["style"] = "Cursor:hand";
            if (this.Request["mode"] == "browse")
            {
                //e.Row.Attributes.Add("onmouseover", "c=this.style.backgroundColor;this.style.backgroundColor='#95B8FF'");
                //e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=c");
                //e.Row.Attributes["style"] = "Cursor:pointer";
                //
                //string ID = "";
                //string PageIndex = gvProject.PageIndex.ToString();
                int count = gvProject.Rows.Count;
                for (int i = 0; i < count; i++)
                {
                    ID = gvProject.DataKeys[i].Value.ToString();
                    #region 非Ajax
                    //gvProject.Rows[i].Cells[1].Attributes.Add("onclick", "window.location.href='projectlist.aspx?mode=browse&projectid=" + ID + "&page=" + PageIndex + "&scrolltop='+$(document).scrollTop();");
                    //gvProject.Rows[i].Cells[2].Attributes.Add("onclick", "window.location.href='projectlist.aspx?mode=browse&projectid=" + ID + "&page=" + PageIndex + "&scrolltop='+$(document).scrollTop();");
                    //gvProject.Rows[i].Cells[3].Attributes.Add("onclick", "window.location.href='projectlist.aspx?mode=browse&projectid=" + ID + "&page=" + PageIndex + "&scrolltop='+$(document).scrollTop();");
                    //gvProject.Rows[i].Cells[4].Attributes.Add("onclick", "window.location.href='projectlist.aspx?mode=browse&projectid=" + ID + "&page=" + PageIndex + "&scrolltop='+$(document).scrollTop();");
                    //gvProject.Rows[i].Cells[5].Attributes.Add("onclick", "window.location.href='projectlist.aspx?mode=browse&projectid=" + ID + "&page=" + PageIndex + "&scrolltop='+$(document).scrollTop();");
                    //gvProject.Rows[i].Cells[6].Attributes.Add("onclick", "window.location.href='projectlist.aspx?mode=browse&projectid=" + ID + "&page=" + PageIndex + "&scrolltop='+$(document).scrollTop();");
                    #endregion
                    gvProject.Rows[i].Cells[1].Attributes.Add("onclick", "$(this).parent().find('input').last().click();");
                    gvProject.Rows[i].Cells[2].Attributes.Add("onclick", "$(this).parent().find('input').last().click();");
                    gvProject.Rows[i].Cells[3].Attributes.Add("onclick", "$(this).parent().find('input').last().click();");
                    gvProject.Rows[i].Cells[4].Attributes.Add("onclick", "$(this).parent().find('input').last().click();");
                    gvProject.Rows[i].Cells[5].Attributes.Add("onclick", "$(this).parent().find('input').last().click();");
                    gvProject.Rows[i].Cells[6].Attributes.Add("onclick", "$(this).parent().find('input').last().click();");
                    gvProject.Rows[i].Attributes.Add("onmouseover", "c=this.style.backgroundColor;this.style.backgroundColor='#95B8FF'");
                    gvProject.Rows[i].Attributes.Add("onmouseout", "this.style.backgroundColor=c");
                    gvProject.Rows[i].Attributes["style"] = "Cursor:pointer";
                }
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
                    { this.ProductionReceiveDuration.Text =  DateTimeHandle.DateDiffDay(project.ProductionReceiveDate, project.ExecutionDate); }
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
            else { this.ProductionReceiveDuration.Text = "(" + this.ProductionReceiveDuration.Text+ ")"; }
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
                        this.CaptureState.Text = "推迟"+DateTimeHandle.DateDiffHour(project.CaptureFinishDate, project.CaptureReceiveDelayDate)+"完成";
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

        #endregion 方法
    }
}