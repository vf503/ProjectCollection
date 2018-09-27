using ProjectCollection.BLL;
using ProjectCollection.Common;
using ProjectCollection.WebUI.pages.common;
using System;
using System.Linq;
using System.Configuration;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using System.Text;
using System.Web;

namespace ProjectCollection.WebUI.pages
{
    public partial class CustomTaskList : BasePage

    {
        //public List<Guid> BatchProjectId = new List<Guid>();
        //
        //public string SerializeBatchProjectId;

        public Guid CurrentUserId;
        #region 事件

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                SearchProjectList();
            }
            else
            {

            }
            if (this.Request["mode"] == "browse")
            {
            }
            else if (this.Request["mode"] == "check")
            {
            }
            else if (this.Request["mode"] == "execute")
            {
            }
            else
            {

            }
            if (this.gvProject.Rows.Count < 1)
            {
                //aBatchSave.Visible = false;
                //aBatchHandle.Visible = false;
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
            SearchProjectList();
            if (this.Request["mode"] == "browse")
            {

            }
            else
            {
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
            UserInfo CurrentUserInfo = (UserInfo)Session["key_userInfo"];
            if (this.Request["mode"] == "browse")
            {
                using (var ProjectModel = new ProjectCollection.WebUI.Models.ProjectCollectionEntities())
                {
                    var projects = (from p in ProjectModel.BatchProject
                                    select p);
                    this.gvProject.DataSource = projects.ToList();
                    this.gvProject.DataBind();
                }
            }
            else if (this.Request["mode"] == "check")
            {
                using (var ProjectModel = new ProjectCollection.WebUI.Models.ProjectCollectionEntities())
                {
                    var projects = (from p in ProjectModel.BatchProject
                                        where p.progress == "等待审核" 
                                        && p.user_info.SupervisorRole== CurrentUserInfo.role_identity
                                    select p);
                    this.gvProject.DataSource = projects.ToList();
                    this.gvProject.DataBind();
                }
            }
            else if (this.Request["mode"] == "execute")
            {
                using (var ProjectModel = new ProjectCollection.WebUI.Models.ProjectCollectionEntities())
                {
                    var projects = (from p in ProjectModel.BatchProject
                                    where p.progress == "等待执行"
                                    select p);
                    this.gvProject.DataSource = projects.ToList();
                    this.gvProject.DataBind();
                }
            }
            else if (this.Request["mode"] == "helpexecute")
            {
                using (var ProjectModel = new ProjectCollection.WebUI.Models.ProjectCollectionEntities())
                {
                    var projects = (from p in ProjectModel.BatchProject
                                    where p.HelpSendingDate.HasValue && !p.HelperFinishDate.HasValue
                                    select p);
                    this.gvProject.DataSource = projects.ToList();
                    this.gvProject.DataBind();
                }
            }
            else
            {
            }
        }
        //
        protected void gvProject_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            string ID = "";
            string PageIndex = gvProject.PageIndex.ToString();
            int count = gvProject.Rows.Count;
            for (int i = 0; i < count; i++)
            {
                ID = gvProject.DataKeys[i].Value.ToString();
                HyperLink CurrentLink = (HyperLink)gvProject.Rows[i].FindControl("aSelect");
                //CurrentLink.NavigateUrl = "~/pages/CustomTaskDetails.aspx?mode=" + this.Request["mode"] + "&id=" + ID;
                string encode = string.Empty;
                byte[] bytes = Encoding.UTF8.GetBytes(this.LoginUserInfo.LoginName + "_" + this.LoginUserInfo.Password);
                encode = HttpUtility.UrlEncode(Convert.ToBase64String(bytes), Encoding.UTF8);
                using (var ProjectModel = new ProjectCollection.WebUI.Models.ProjectCollectionEntities())
                {
                    ProjectCollection.WebUI.Models.BatchProject ThisProject = (from p in ProjectModel.BatchProject
                                                                               where p.id == ID
                                                                               select p).First();
                    if (this.Request["mode"] == "check")
                    {
                        CurrentLink.NavigateUrl = "~/pages/CustomTaskDetails.aspx?mode=" + this.Request["mode"] + "&id=" + ID;
                    }
                    else if (this.Request["mode"] == "execute")
                    {
                        CurrentLink.NavigateUrl = "http://newpms.cei.cn/webpages/V2/index.html#/HomePage?mode=disposal&project=" + ThisProject.id + "&login=" + encode;
                    }
                    else if (this.Request["mode"] == "helpexecute")
                    {
                        CurrentLink.NavigateUrl = "~/pages/CustomTaskDetails.aspx?mode=" + this.Request["mode"] + "&id=" + ID;
                    }
                    else
                    {
                    }
                }
            }
        }

        //
        protected void BatchSelect()
        {
        }
        #endregion 方法
    }
}