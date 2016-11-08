using ProjectCollection.BLL;
using ProjectCollection.WebUI.pages.common;
using System;

namespace ProjectCollection.WebUI.pages
{
    /// <summary>
    /// 项目计划创建/编辑页面
    /// </summary>
    public partial class ProjectPlanCreateEdit : BasePage
    {
        #region 事件

        /// <summary>
        /// 页面载入
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // 初始化下拉框
                this.InitDropDownList();

                //
                if (string.IsNullOrEmpty(this.Request["ProjectPlanId"]))
                {
                    this.hidProjectPlanId.Value = string.Empty;
                    //新建模式
                    btnProject.Visible = false;
                    btnRecond.Visible = false;
                    labMakingDate.Visible = false;
                    txtMakingDate.Visible = false;
                    labProgress.Visible = false;
                    txtProgress.Visible = false;
                    btnOk.Text = "新建任务计划";
                }
                else
                {
                    Guid projectPlanId = new Guid(this.Request["ProjectPlanId"]);
                    this.hidProjectPlanId.Value = projectPlanId.ToString();
                    this.InitEditData();
                    BLL.ProjectPlan projectPlan = BLL.ProjectPlan.GetProjectPlan(new Guid(this.hidProjectPlanId.Value.ToString()));
                    if (projectPlan.progress != new Guid("00000000-0000-0000-0000-000000000126"))
                    {
                        btnFinish.Visible = true;
                    }
                    else { }
                    //
                    if (this.Request["mode"] == "recond")
                    {
                        panelRecording.Visible = true;
                        btnOk.Visible = false;
                        btnRecond.Visible = true;
                        btnFinish.Visible = false;
                    }
                    else if (this.Request["mode"] == "browse")
                    {
                        btnFinish.Visible = false;
                        btnOk.Visible = false;
                        btnRecond.Visible = false;
                        btnFinish.Visible = false;
                        panelRecording.Visible = true;
                        try
                        {
                            InitRecondData();
                        }
                        catch
                        {

                        }
                        finally
                        {

                        }
                    }
                    else
                    {
                        panelRecording.Visible = true;
                        try
                        {
                            InitRecondData();
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
        }

        /// <summary>
        /// 取消
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            if (this.Request["mode"] == "recond")
            {
                this.Redirect("~/pages/ProjectPlanList.aspx?mode=recond");
            }
            else
            {
                this.Redirect("~/pages/ProjectPlanList.aspx");
            }
        }

        /// <summary>
        /// 新建
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnOk_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.hidProjectPlanId.Value))
            {
                //新增
                BLL.ProjectPlan projectPlan = new BLL.ProjectPlan();
                projectPlan.ProjectPlanId = Guid.NewGuid();
                projectPlan.ProjectPlanNo = this.txtProjectPlanNo.Text;
                projectPlan.PlanDate = Convert.ToDateTime(this.hidPlanDate.Value);
                projectPlan.MakingDate = DateTime.Now;
                projectPlan.ProjectPlanTypeId = new Guid(this.ddlProjectPlanType.SelectedValue);
                projectPlan.Title = this.txtTitle.Text;
                projectPlan.Lecturer = this.txtLecturer.Text;
                projectPlan.CreatorId = this.LoginUserInfo.Identity;
                projectPlan.Category = this.txtCategory.Text;
                projectPlan.CourseCount = Convert.ToDecimal(this.txtCourseCount.Text.ToString());
                projectPlan.Price = Convert.ToInt16(this.txtPrice.Text.ToString());
                projectPlan.Source = this.txtSource.Text;
                projectPlan.Note = this.txtNote.Text;
                projectPlan.ExtraNote = this.txtExtraNote.Text;
                projectPlan.RecordingPersonInCharge = new Guid(this.ddlRecordingPersonInCharge.SelectedValue);
                if (projectPlan.ProjectPlanTypeId.ToString() == "00000000-0000-0000-0000-000000000014" || projectPlan.ProjectPlanTypeId.ToString() == "00000000-0000-0000-0000-000000000200")
                {
                    projectPlan.progress = new Guid("00000000-0000-0000-0000-000000000102");
                }
                else if (projectPlan.ProjectPlanTypeId.ToString() == "00000000-0000-0000-0000-000000000202") 
                {
                    projectPlan.progress = new Guid("00000000-0000-0000-0000-000000000102");
                }
                else
                {
                    projectPlan.progress = new Guid("00000000-0000-0000-0000-000000000101");
                }
                BLL.ProjectPlan.Insert(projectPlan);
            }
            else
            {
                //编辑
                BLL.ProjectPlan projectPlan = BLL.ProjectPlan.GetProjectPlan(new Guid(this.hidProjectPlanId.Value.ToString()));
                projectPlan.ProjectPlanNo = this.txtProjectPlanNo.Text;
                projectPlan.PlanDate = Convert.ToDateTime(this.hidPlanDate.Value);
                //projectPlan.ProjectPlanTypeId = new Guid(this.ddlProjectPlanType.SelectedValue);//修改类型需要配合修改进度(类型决定有无拍摄)
                projectPlan.ProjectPlanTypeId = projectPlan.ProjectPlanTypeId;
                projectPlan.Title = this.txtTitle.Text;
                projectPlan.Lecturer = this.txtLecturer.Text;
                projectPlan.Category = this.txtCategory.Text;
                projectPlan.CourseCount = Convert.ToDecimal(this.txtCourseCount.Text.ToString());
                projectPlan.Price = Convert.ToInt16(this.txtPrice.Text.ToString());
                projectPlan.Source = this.txtSource.Text;
                projectPlan.Note = this.txtNote.Text;
                projectPlan.ExtraNote = this.txtExtraNote.Text;
                projectPlan.RecordingPersonInCharge = new Guid(this.ddlRecordingPersonInCharge.SelectedValue);
                BLL.ProjectPlan.Update(projectPlan);
            }

            this.Redirect("~/pages/ProjectPlanListMasterDetail.aspx");
        }

        /// <summary>
        /// 新建
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnRecond_Click(object sender, EventArgs e)
        {
            BLL.ProjectPlan projectPlan = BLL.ProjectPlan.GetProjectPlan(new Guid(this.hidProjectPlanId.Value.ToString()));
            if (projectPlan.progress == new Guid("00000000-0000-0000-0000-000000000101") && projectPlan.ProjectPlanTypeId.ToString() != "00000000-0000-0000-0000-000000000014")
            {
                projectPlan.progress = new Guid("00000000-0000-0000-0000-000000000102");
                projectPlan.RecordingDate = Convert.ToDateTime(this.hidRecordingDate.Value);
                projectPlan.RecordingPlace = this.txtRecordingPlace.Text;
                projectPlan.RecordingScriptHolder = Convert.ToBoolean(this.ddlRecordingScriptHolder.SelectedValue);
                projectPlan.RecordingLecture = Convert.ToInt16(this.txtRecordingLecture.Text);
                projectPlan.RecordingFile = this.txtRecordingFile.Text;
                projectPlan.FileDeliverDate = Convert.ToDateTime(this.hidFileDeliverDate.Value);
                projectPlan.RecordingNote = this.txtRecordingNote.Text;
                BLL.ProjectPlan.UpdateRecond(projectPlan);
                ClientScript.RegisterStartupScript(this.GetType(), "Reconding1", "alert('完成')", true);
                this.Redirect("~/pages/MyTask.aspx");
                //Response.Redirect(Request.Url.ToString());
            }
            //else
            //{
            //    ClientScript.RegisterStartupScript(this.GetType(), "Reconding2", "alert('已下达或无需拍摄任务')", true);
            //}
        }


        /// <summary>
        /// 新建
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnProject_Click(object sender, EventArgs e)
        {
            string ProjectPlanId = this.Request["ProjectPlanId"].ToString();
            this.Redirect("~/pages/ProjectCreateEdit.aspx?mode=create&ProjectPlanId=" + ProjectPlanId);
        }

        protected void btnFinish_Click(object sender, EventArgs e)
        {
            BLL.ProjectPlan projectPlan = BLL.ProjectPlan.GetProjectPlan(new Guid(this.hidProjectPlanId.Value.ToString()));
            projectPlan.progress = new Guid("00000000-0000-0000-0000-000000000126");
            BLL.ProjectPlan.UpdateFinish(projectPlan);
            this.Redirect("~/pages/ProjectPlanListMasterDetail.aspx");
        }

        #endregion 事件

        #region 方法

        /// <summary>
        /// 初始化下来框
        /// </summary>
        private void InitDropDownList()
        {
            this.ddlProjectPlanType.DataSource = BLL.DataDictionary.GetDataByCategory("ProjectPlanType");
            this.ddlProjectPlanType.DataTextField = "text";
            this.ddlProjectPlanType.DataValueField = "dictionary_identity";
            this.ddlProjectPlanType.DataBind();
            //
            this.ddlRecordingPersonInCharge.DataSource = BLL.UserInfo.GetDataByID();
            this.ddlRecordingPersonInCharge.DataTextField = "real_name";
            this.ddlRecordingPersonInCharge.DataValueField = "user_Identity";
            this.ddlRecordingPersonInCharge.DataBind();
        }

        private void InitEditData()
        {
            BLL.ProjectPlan projectPlan = BLL.ProjectPlan.GetProjectPlan(new Guid(this.hidProjectPlanId.Value.ToString()));
            this.txtProjectPlanNo.Text = projectPlan.ProjectPlanNo;
            this.txtMakingDate.Text = projectPlan.MakingDate.ToString("yyyy-MM-dd");
            this.hidPlanDate.Value = projectPlan.PlanDate.ToString("yyyy-MM-dd");
            this.ddlProjectPlanType.SelectedValue = projectPlan.ProjectPlanTypeId.ToString();
            this.txtTitle.Text = projectPlan.Title;
            this.txtLecturer.Text = projectPlan.Lecturer;
            this.txtCategory.Text = projectPlan.Category;
            this.txtCourseCount.Text = projectPlan.CourseCount.ToString();
            this.txtPrice.Text = projectPlan.Price.ToString();
            this.txtSource.Text = projectPlan.Source;
            this.txtNote.Text = projectPlan.Note;
            this.txtExtraNote.Text = projectPlan.ExtraNote;
            this.txtProgress.Text = projectPlan.ProgressText;
            this.ddlRecordingPersonInCharge.SelectedValue = projectPlan.RecordingPersonInCharge.ToString(); 
            if (projectPlan.ProjectPlanTypeId.ToString() == "00000000-0000-0000-0000-000000000014")
            {
                btnRecond.Visible = false;
            }
            if (projectPlan.progress.ToString() == "00000000-0000-0000-0000-000000000102" && (LoginUserInfo.role_identity == new Guid("00000000-0000-0000-0000-000000000003") || LoginUserInfo.role_identity == new Guid("00000000-0000-0000-0000-000000000015")))
            {
                btnProject.Visible = true;
            }
        }

        private void InitRecondData()
        {
            BLL.ProjectPlan projectPlan = BLL.ProjectPlan.GetProjectPlan(new Guid(this.hidProjectPlanId.Value.ToString()));
            this.RecordingDate.Text = projectPlan.RecordingDate.ToString("yyyy-MM-dd");
            this.txtRecordingPlace.Text = projectPlan.RecordingPlace;
            this.ddlRecordingScriptHolder.SelectedValue = projectPlan.RecordingScriptHolder.ToString();
            this.txtRecordingLecture.Text = projectPlan.RecordingLecture.ToString();
            this.FileDeliverDate.Text = projectPlan.FileDeliverDate.ToString("yyyy-MM-dd HH:mm");
            this.txtRecordingFile.Text = projectPlan.RecordingFile.ToString();
            this.txtRecordingNote.Text = projectPlan.RecordingNote.ToString();
        }

        #endregion
    }
}
