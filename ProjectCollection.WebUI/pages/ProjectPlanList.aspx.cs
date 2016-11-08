using ProjectCollection.WebUI.pages.common;
using System;
using System.Web.UI.WebControls;

namespace ProjectCollection.WebUI.pages
{
    /// <summary>
    /// 
    /// </summary>
    public partial class ProjectPlanList : BasePage
    {

        #region 事件
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.SearchProjectPlanList();
            }
            if (this.Request["mode"] == "recond")
            {
                
            }
            else if (this.Request["mode"] == "browse")
            {
                
            }
            else
            {
                btnCreate.Visible = true;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnCreate_Click(object sender, EventArgs e)
        {
            this.Redirect("/pages/ProjectPlanCreateEdit.aspx");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            this.SearchProjectPlanList();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvProjectPlan_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
            Guid identity = Guid.Parse(this.gvProjectPlan.DataKeys[e.NewSelectedIndex].Values["ProjectPlanId"].ToString());
            if (this.Request["mode"] == "recond")
            {
                this.Redirect("~/pages/ProjectPlanCreateEdit.aspx?mode=recond&ProjectPlanId=" + identity);
            }
            else if (this.Request["mode"] == "browse")
            {
                this.Redirect("~/pages/ProjectPlanCreateEdit.aspx?mode=browse&ProjectPlanId=" + identity);
            }
            else
            {
                this.Redirect("~/pages/ProjectPlanCreateEdit.aspx?ProjectPlanId=" + identity);
            }
        }

        protected void gvProjectPlan_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvProjectPlan.PageIndex = e.NewPageIndex;
            this.SearchProjectPlanList();
        }

        #endregion

        #region 方法

        private void SearchProjectPlanList()
        {
            if (this.Request["mode"] == "recond")
            {
                this.gvProjectPlan.DataSource = BLL.ProjectPlan.GetRecondProjectPlan();
                this.gvProjectPlan.DataBind();
            }
            else if (this.Request["mode"] == "browse")
            {
                this.gvProjectPlan.DataSource = BLL.ProjectPlan.GetAllProjectPlan();
                this.gvProjectPlan.DataBind();
            }
            else
            {
                this.gvProjectPlan.DataSource = BLL.ProjectPlan.GetProjectPlan();
                this.gvProjectPlan.DataBind();
            }
        }

        #endregion

    }
}