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
    public partial class ProjectListEmbed : BasePage

    {
        public List<Guid> BatchProjectId = new List<Guid>();
        //
        public string SerializeBatchProjectId;

        public Guid CurrentUserId;
        #region 事件

        protected void Page_Load(object sender, EventArgs e)
        {
            CurrentUserId = new Guid(this.Request["userid"]);
            if (!IsPostBack)
            {
                if (this.Request["mode"] == "browse")
                {
                    
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
                this.gvProject.Columns[0].Visible = false;
            }
            else if (this.Request["mode"] == "copy")
            {
                this.gvProject.Columns[7].Visible = false;
                this.gvProject.Columns[8].Visible = true;
            }
            else if (this.Request["mode"] == "capture")
            {
                aBatchHandle.Visible = true;
                aBatchHandle.Text = "批量处理";
            }
            else if (this.Request["mode"] == "shorthand")
            {
                aBatchHandle.Visible = true;
                aBatchHandle.Text = "批量完成";
            }
            else if (this.Request["mode"] == "contentreceive")
            {
                //    btnBatchHandle.Visible = true;
                //    btnBatchHandle.Text = "批量接收";
                //    btnBatchHandle.PostBackUrl = "~/pages/ProjectCreateEdit.aspx?mode=contentreceivebatchhandle";
                aBatchHandle.Visible = true;
                aBatchHandle.Text = "批量接收";
            }
            else if (this.Request["mode"] == "contentcheck")
            {
                //    btnBatchSave.Visible = true;
                //    btnBatchHandle.Visible = true;
                //    btnBatchHandle.Text = "批量通过";
                //    btnBatchSave.PostBackUrl = "~/pages/ProjectCreateEdit.aspx?mode=contentcheckbatchsave";
                //    btnBatchHandle.PostBackUrl = "~/pages/ProjectCreateEdit.aspx?mode=contentcheckbatchhandle";
                aBatchSave.Visible = true;
                aBatchHandle.Visible = true;
                aBatchHandle.Text = "批量通过";
            }
            else if (this.Request["mode"] == "productionreceive")
            {
                //    btnBatchHandle.Visible = true;
                //    btnBatchHandle.Text = "批量接收";
                //    btnBatchHandle.PostBackUrl = "~/pages/ProjectCreateEdit.aspx?mode=productionreceivebatchhandle";
                aBatchHandle.Visible = true;
                aBatchHandle.Text = "批量处理";
            }
            else if (this.Request["mode"] == "productionfinish")
            {
                //    btnBatchHandle.Visible = true;
                //    btnBatchHandle.Text = "批量完成";
                //    btnBatchHandle.PostBackUrl = "~/pages/ProjectCreateEdit.aspx?mode=productionfinishbatchhandle";
                aBatchHandle.Visible = true;
                aBatchHandle.Text = "批量完成";
            }
            else if (this.Request["mode"] == "productioncheck")
            {
                //    btnBatchSave.Visible = true;
                //    btnBatchHandle.Visible = true;
                //    btnBatchHandle.Text = "批量通过";
                //    btnBatchSave.PostBackUrl = "~/pages/ProjectCreateEdit.aspx?mode=productioncheckbatchsave";
                //    btnBatchHandle.PostBackUrl = "~/pages/ProjectCreateEdit.aspx?mode=productioncheckbatchhandle";
                aBatchSave.Visible = true;
                aBatchHandle.Visible = true;
                aBatchHandle.Text = "批量通过";
            }
            else
            {

            }
            if (this.gvProject.Rows.Count < 1)
            {
                //btnBatchSave.Visible = false;
                //btnBatchHandle.Visible = false;
                aBatchSave.Visible = false;
                aBatchHandle.Visible = false;
            }
            else
            {

            }
        }
        //
        protected void gvProject_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
            
        }
        protected void CustomersGridView_RowCommand(Object sender, GridViewCommandEventArgs e)
        {

        }

        protected void gvProject_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvProject.PageIndex = e.NewPageIndex;
            if (this.Request["mode"] == "browse")
            {
                
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
            BatchSelect();
        }

        protected void btnBatch_Click(object sender, EventArgs e)
        {
            BatchSelect();
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
                this.gvProject.DataSource = BLL.Project.GetDictionaryContentIdProject(new Guid("00000000-0000-0000-0000-000000000111"), new Guid("00000000-0000-0000-0000-000000000124"), CurrentUserId);
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
                List<Project> Projects = BLL.Project.GetDictionaryProductionIdProject(new Guid("00000000-0000-0000-0000-000000000106"), new Guid("00000000-0000-0000-0000-000000000132"));
                this.gvProject.DataSource = Projects;
                this.gvProject.DataBind();
                int count = gvProject.Rows.Count;
                for (int i = 0; i < count; i++)
                {
                    if (Projects[i+(gvProject.PageSize*gvProject.PageIndex)].emergency.ToString() == "00000000-0000-0000-0000-000000000030")
                    { }
                    else
                    {
                        gvProject.Rows[i].ForeColor = System.Drawing.Color.OrangeRed;
                    }
                }
            }
            else if (this.Request["mode"] == "productionfinish")
            {
                this.gvProject.DataSource = BLL.Project.GetDictionaryProductionIdProject(new Guid("00000000-0000-0000-0000-000000000114"), new Guid("00000000-0000-0000-0000-000000000125"), CurrentUserId);
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
        //
        protected void gvProject_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (this.Request["mode"] == "browse")
            {

            }
            else
            {
                string ID = "";
                string PageIndex = gvProject.PageIndex.ToString();
                int count = gvProject.Rows.Count;
                for (int i = 0; i < count; i++)
                {
                    ID = gvProject.DataKeys[i].Value.ToString();
                    HyperLink CurrentLink = (HyperLink)gvProject.Rows[i].FindControl("aSelect");
                    CurrentLink.NavigateUrl = "~/pages/ProjectCreateEdit.aspx?mode=" + this.Request["mode"]+ "&ProjectId=" + ID;
                }
            }
        }

        //
        protected void BatchSelect()
        {
            for (int i = 0; i < gvProject.Rows.Count; i++)
            {
                GridViewRow row = gvProject.Rows[i];
                bool isChecked = ((CheckBox)row.FindControl("SelectCheckBox")).Checked;
                Guid CurrentId = Guid.Parse(gvProject.DataKeys[i].Values["ProjectId"].ToString());
                if (isChecked)
                {
                    //Guid CurrentId = Guid.Parse(gvProject.DataKeys[gvProject.Rows[i].DataItemIndex].Values["ProjectId"].ToString());
                    if (BatchProjectId.Contains(CurrentId))
                    {

                    }
                    else { BatchProjectId.Add(CurrentId); }
                }
                else
                {
                    if (BatchProjectId.Contains(CurrentId))
                    {
                        BatchProjectId.Remove(CurrentId);
                    }
                    else { }
                }
            }
            hidBatchId.Value = SerializeObj.Serialize(BatchProjectId);
            Session["BatchId"] = hidBatchId.Value;

            if (BatchProjectId.Count > 0)
            {
                if (this.Request["mode"] == "capture")
                {
                    tips.Text = "";
                    string CurrentProgress="";
                    bool check = true;
                    for (int i = 0; i < gvProject.Rows.Count; i++)
                    {
                        GridViewRow row = gvProject.Rows[i];
                        bool isChecked = ((CheckBox)row.FindControl("SelectCheckBox")).Checked;
                        if (isChecked)
                        {
                            CurrentProgress = gvProject.Rows[i].Cells[5].Text;
                            break;
                        }
                     }
                     for (int i = 0; i < gvProject.Rows.Count; i++)
                     {
                            GridViewRow row = gvProject.Rows[i];
                            bool isChecked = ((CheckBox)row.FindControl("SelectCheckBox")).Checked;
                            if (isChecked)
                            {
                                if (gvProject.Rows[i].Cells[5].Text == CurrentProgress)
                                { }
                                else
                                {
                                    check = false;
                                    break;
                                }

                            }
                     }
                    if (check)
                    {
                        aBatchHandle.NavigateUrl = "~/pages/ProjectCreateEdit.aspx?mode=capturebatchhandle&BatchId=" + hidBatchId.Value;
                    }
                    else
                    {
                        tips.ForeColor = System.Drawing.Color.Red;
                        tips.Text = "只能多选状态相同的工单！";
                    }
                }
                if (this.Request["mode"] == "contentreceive")
                {
                    aBatchHandle.NavigateUrl = "~/pages/ProjectCreateEdit.aspx?mode=contentreceivebatchhandle&BatchId=" + hidBatchId.Value;
                }
                else if (this.Request["mode"] == "contentcheck")
                {
                    aBatchSave.NavigateUrl = "~/pages/ProjectCreateEdit.aspx?mode=contentcheckbatchsave&BatchId=" + hidBatchId.Value;
                    aBatchHandle.NavigateUrl = "~/pages/ProjectCreateEdit.aspx?mode=contentcheckbatchhandle&BatchId=" + hidBatchId.Value;
                }
                if (this.Request["mode"] == "productionreceive")
                {
                    aBatchHandle.NavigateUrl = "~/pages/ProjectCreateEdit.aspx?mode=productionreceivebatchhandle";
                }
                else if (this.Request["mode"] == "productionfinish")
                {
                    aBatchHandle.NavigateUrl = "~/pages/ProjectCreateEdit.aspx?mode=productionfinishbatchhandle";
                }
                else if (this.Request["mode"] == "productioncheck")
                {
                    aBatchSave.NavigateUrl = "~/pages/ProjectCreateEdit.aspx?mode=productioncheckbatchsave";
                    aBatchHandle.NavigateUrl = "~/pages/ProjectCreateEdit.aspx?mode=productioncheckbatchhandle";
                }
                else if (this.Request["mode"] == "shorthand")
                {
                    aBatchHandle.NavigateUrl = "~/pages/ProjectCreateEdit.aspx?mode=shorthandbatchhandle";
                }
            }
            else
            {
                aBatchSave.NavigateUrl = "";
                aBatchHandle.NavigateUrl = "";
            }
        }
        #endregion 方法
    }
}