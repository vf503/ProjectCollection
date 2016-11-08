using ProjectCollection.BLL;
using ProjectCollection.WebUI.pages.common;
using System;
using System.Collections.Generic;

namespace ProjectCollection.WebUI
{
    public partial class CustomProjectCreateEdit : BasePage
    {
        #region 事件

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                InitDropDownList();
            }
            #region create
            if (this.Request["mode"] == "create")
            {
                Guid ProjectPlanId = new Guid(this.Request["ProjectPlanId"].ToString());
                ProjectPlan ProjectPlan = BLL.ProjectPlan.GetProjectPlan(ProjectPlanId);
                this.btnOk.Visible = true;
                if (!IsPostBack)
                {
                    this.txtProjectNo.Text = ProjectPlan.ProjectPlanNo.ToString() + "-";
                    ddlPublishNeeds.SelectedValue = "00000000-0000-0000-0000-000000000042";
                }
            }
            #endregion
            #region browse
            else if (this.Request["mode"] == "browse")
            {
                Guid projectId = new Guid(this.Request["ProjectId"]);
                this.hidCustomProjectId.Value = projectId.ToString();
                this.InitBrowseData();
                //
                try
                {
                    this.PanelReceive.Visible = true;
                    this.InitReceiveDate();
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
                    this.PanelOperation.Visible = true;
                    this.InitOperationDate();
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
                    this.InitPublishDate();
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
                    this.InitCheckDate();
                }
                catch
                {

                }
                finally
                {

                }
            }
            #endregion
            #region receive
            else if (this.Request["mode"] == "receive")
            {
                Guid projectId = new Guid(this.Request["ProjectId"]);
                this.hidCustomProjectId.Value = projectId.ToString();
                this.PanelReceive.Visible = true;
                this.btnOk.Visible = true;
                this.btnOk.Text = "接收";
                this.InitBrowseData();
            }
            #endregion
            #region operation
            else if (this.Request["mode"] == "operation")
            {
                Guid projectId = new Guid(this.Request["ProjectId"]);
                this.hidCustomProjectId.Value = projectId.ToString();
                this.PanelOperation.Visible = true;
                this.btnOk.Visible = true;
                this.btnOk.Text = "完成";
                this.InitBrowseData();
                if (!IsPostBack)
                {
                    try
                    {
                        this.InitReceiveDate();
                    }
                    catch
                    {

                    }
                    finally
                    {

                    }
                }
            }
            #endregion operation
            #region publish
            else if (this.Request["mode"] == "publish")
            {
                Guid projectId = new Guid(this.Request["ProjectId"]);
                this.hidCustomProjectId.Value = projectId.ToString();
                this.PanelPublish.Visible = true;
                this.btnOk.Visible = true;
                this.btnOk.Text = "发布完成";
                this.InitBrowseData();
                if (!IsPostBack)
                {
                    try
                    {
                        this.InitReceiveDate();
                        this.InitOperationDate();
                    }
                    catch
                    {

                    }
                    finally
                    {

                    }
                }
            }
            #endregion publish
            #region check
            else if (this.Request["mode"] == "check")
            {
                Guid projectId = new Guid(this.Request["ProjectId"]);
                this.hidCustomProjectId.Value = projectId.ToString();
                this.PanelCheck.Visible = true;
                this.btnOk.Visible = true;
                this.btnOk.Text = "通过";
                this.InitBrowseData();
                if (!IsPostBack)
                {
                    try
                    {
                        this.InitReceiveDate();
                        this.InitOperationDate();
                        this.InitPublishDate();
                    }
                    catch
                    {

                    }
                    finally
                    {

                    }
                }
            }
            #endregion check
            else { }
        }
        protected void btnOk_Click(object sender, EventArgs e)
        {
            string type = this.Request["type"].ToString();
            #region create
            if (this.Request["mode"] == "create")
            {
                //新增
                Guid ProjectPlanId;
                BLL.CustomProject project = new BLL.CustomProject();
                project.CustomProjectId = Guid.NewGuid();
                ProjectPlanId = new Guid(this.Request["ProjectPlanId"].ToString());
                project.ProjectPlanId = ProjectPlanId;
                project.No = this.txtProjectNo.Text;
                project.SendingDate = DateTime.Now;
                project.PublishNeeds = new Guid(this.ddlPublishNeeds.SelectedValue);
                project.Title = this.txtCourseName.Text;
                project.Lecturer = this.txtlecturer.Text;
                project.LecturerJob = this.txtLecturerJob.Text;
                project.CourseAmount = this.txtCourseAmount.Text;
                project.CourseSource = this.txtCourseSource.Text;
                project.Category = this.txtTextCategory.Text;
                project.Note = this.txtCreateNote.Text;
                project.ExtraNote = this.txtExtraNote.Text;
                project.Progress = new Guid("00000000-0000-0000-0000-000000000203");
                BLL.CustomProject.Insert(project);
                this.Redirect("~/pages/CustomProjectList.aspx?mode=browse&type=" + type);
            }
            #endregion
            #region receive
            else if (this.Request["mode"] == "receive")
            {
                BLL.CustomProject project = BLL.CustomProject.GetCustomProject(new Guid(this.hidCustomProjectId.Value.ToString()));
                project.Signer = this.LoginUserInfo.Identity;
                project.ReceiveDate = DateTime.Now;
                project.ReceiveNote = this.txtReceiveNote.Text;
                project.Operator=new Guid(this.ddlOperator.SelectedValue);
                project.Progress = new Guid("00000000-0000-0000-0000-000000000204");
                BLL.CustomProject.UpdateReceive(project);
                //this.Redirect("~/pages/CustomProjectList.aspx?mode=receive&type=" + type);
                this.Redirect("~/pages/MyTask.aspx");
            }
            #endregion receive
            #region operation
            else if (this.Request["mode"] == "operation")
            {
                BLL.CustomProject project = BLL.CustomProject.GetCustomProject(new Guid(this.hidCustomProjectId.Value.ToString()));
                project.FinishDate = DateTime.Now;
                project.OperationNote = this.txtOperationNote.Text;
                if (project.PublishNeeds.ToString() == "00000000-0000-0000-0000-000000000042")
                {
                    project.Progress = new Guid("00000000-0000-0000-0000-000000000205");
                }
                else 
                {
                    project.Progress = new Guid("00000000-0000-0000-0000-000000000206");
                }
                BLL.CustomProject.UpdateOperation(project);
                //this.Redirect("~/pages/CustomProjectList.aspx?mode=operation&type=" + type);
                this.Redirect("~/pages/MyTask.aspx");
            }
            #endregion operation
            #region publish
            else if (this.Request["mode"] == "publish")
            {
                BLL.CustomProject project = BLL.CustomProject.GetCustomProject(new Guid(this.hidCustomProjectId.Value.ToString()));
                project.Publisher = this.LoginUserInfo.Identity;
                project.PublishDate = DateTime.Now;
                project.PublishNote = this.txtPublishNote.Text;
                project.PublishCheck =new Guid (this.ddlPublishCheck.SelectedValue);
                project.Progress = new Guid("00000000-0000-0000-0000-000000000207");
                BLL.CustomProject.UpdatePublish(project);
                //this.Redirect("~/pages/CustomProjectList.aspx?mode=publish&type=" + type);
                this.Redirect("~/pages/MyTask.aspx");
            }
            #endregion publish
            #region check
            else if (this.Request["mode"] == "check")
            {
                BLL.CustomProject project = BLL.CustomProject.GetCustomProject(new Guid(this.hidCustomProjectId.Value.ToString()));
                project.Checker = this.LoginUserInfo.Identity;
                project.CheckDate = DateTime.Now;
                project.CheckNote = this.txtCheckNote.Text;
                project.CategoryCheck = new Guid(this.ddlCategoryCheck.SelectedValue);
                project.Progress = new Guid("00000000-0000-0000-0000-000000000206");
                BLL.CustomProject.UpdateCheck(project);
                //this.Redirect("~/pages/CustomProjectList.aspx?mode=check&type=" + type);
                this.Redirect("~/pages/MyTask.aspx");
            }
            #endregion check
            else
            {

            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {

        }

        #endregion 事件

        #region 方法
        //
        private void InitDropDownList()
        {
            //
            this.ddlPublishNeeds.DataSource = BLL.DataDictionary.GetDataByCategory("IsNot");
            this.ddlPublishNeeds.DataTextField = "text";
            this.ddlPublishNeeds.DataValueField = "dictionary_identity";
            this.ddlPublishNeeds.DataBind();
            //
            this.ddlOperator.DataSource = BLL.UserInfo.GetDataByID();
            this.ddlOperator.DataTextField = "real_name";
            this.ddlOperator.DataValueField = "user_Identity";
            this.ddlOperator.DataBind();
            //
            this.ddlPublishCheck.DataSource = BLL.DataDictionary.GetDataByCategory("IsNot");
            this.ddlPublishCheck.DataTextField = "text";
            this.ddlPublishCheck.DataValueField = "dictionary_identity";
            this.ddlPublishCheck.DataBind();
            //
            this.ddlCategoryCheck.DataSource = BLL.DataDictionary.GetDataByCategory("IsNot");
            this.ddlCategoryCheck.DataTextField = "text";
            this.ddlCategoryCheck.DataValueField = "dictionary_identity";
            this.ddlCategoryCheck.DataBind();

        }
        //
        private void InitBrowseData()
        {
            BLL.CustomProject project = BLL.CustomProject.GetCustomProject(new Guid(this.hidCustomProjectId.Value.ToString()));
            BLL.ProjectPlan projectPlan = BLL.ProjectPlan.GetProjectPlan(project.ProjectPlanId);
            this.txtProjectNo.Text = project.No;
            this.txtSendingDate.Text = project.SendingDate.ToString("yyyy-MM-dd");
            this.ddlPublishNeeds.SelectedValue = project.PublishNeeds.ToString();
            this.txtInCharge.Text = projectPlan.CreatorName.ToString();
            this.txtProjectPlanName.Text = projectPlan.Title.ToString();
            this.txtCourseName.Text = project.Title.ToString();
            this.txtlecturer.Text = project.Lecturer.ToString();
            this.txtLecturerJob.Text = project.LecturerJob.ToString();
            this.txtCourseAmount.Text = project.CourseAmount.ToString();
            this.txtCourseSource.Text = project.CourseSource.ToString();
            this.txtTextCategory.Text = project.Category.ToString();
            this.txtCreateNote.Text = project.Note.ToString();
            this.txtExtraNote.Text = project.ExtraNote;
            this.txtPlanNote.Text = projectPlan.Note.ToString();
        }
        private void InitReceiveDate()
        {
            BLL.CustomProject project = BLL.CustomProject.GetCustomProject(new Guid(this.hidCustomProjectId.Value.ToString()));
            this.txtSigner.Text = BLL.UserInfo.GetRealNameByID(project.Signer);
            this.txtReceiveDate.Text = project.ReceiveDate.ToString("yyyy-MM-dd");
            this.txtReceiveNote.Text = project.ReceiveNote;
            this.ddlOperator.SelectedValue = project.Operator.ToString();
        }
        private void InitOperationDate()
        {
            BLL.CustomProject project = BLL.CustomProject.GetCustomProject(new Guid(this.hidCustomProjectId.Value.ToString()));
            this.txtFinishDate.Text = project.FinishDate.ToString("yyyy-MM-dd");
            this.txtOperationNote.Text = project.OperationNote;
        }
        private void InitPublishDate()
        {
            BLL.CustomProject project = BLL.CustomProject.GetCustomProject(new Guid(this.hidCustomProjectId.Value.ToString()));
            this.txtPublisher.Text = BLL.UserInfo.GetRealNameByID(project.Publisher);
            this.txtPublishDate.Text = project.PublishDate.ToString("yyyy-MM-dd");
            this.ddlPublishCheck.SelectedValue = project.PublishCheck.ToString();
            this.txtPublishNote.Text = project.PublishNote;
        }
        private void InitCheckDate()
        {
            BLL.CustomProject project = BLL.CustomProject.GetCustomProject(new Guid(this.hidCustomProjectId.Value.ToString()));
            this.txtChecker.Text = BLL.UserInfo.GetRealNameByID(project.Checker);
            this.txtCheckDate.Text = project.CheckDate.ToString("yyyy-MM-dd");
            this.ddlCategoryCheck.SelectedValue = project.CategoryCheck.ToString();
            this.txtCheckNote.Text = project.CheckNote;
        }

        //

        #endregion 方法
    }
}