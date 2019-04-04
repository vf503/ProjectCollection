using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProjectCollection.WebUI.pages
{
    public partial class CustomTaskListFliter : System.Web.UI.Page
    {
        public string range="";
        public string process="";
        public List<Models.BatchProject> data=new List<Models.BatchProject>();
        #region 事件

        protected void Page_Load(object sender, EventArgs e)
        {
            //range = this.Request["range"];
            //range = this.Request["process"];
            DataBindProjectPlanList(range, process);
        }

        protected void axgvProject_PageIndexChanged(object sender, EventArgs e)
        {
            DataBindProjectPlanList(range, process);
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
            List<object> keyValues = axgvProject.GetCurrentPageRowValues(axgvProject.KeyFieldName);
            foreach (object key in keyValues)
            {
               HyperLink CurrentASelect = (HyperLink)axgvProject.FindRowCellTemplateControlByKey(key, (DevExpress.Web.GridViewDataColumn)axgvProject.Columns["Operate"], "aSelect");
                CurrentASelect.NavigateUrl = "~/pages/CustomTaskDetails.aspx?mode=browse" + "&id=" + key.ToString();
            }
        }

        protected void rblMain_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rblMain.Value.ToString() == "normal")
            { Response.Redirect("~/pages/ProjectListFilter.aspx?mode=browse"); }
            else if (rblMain.Value.ToString() == "OpenClass")
            {
                Response.Redirect("~/pages/CustomProjectList.aspx?type=00000000-0000-0000-0000-000000000202&mode=browse");
            }
            else if (rblMain.Value.ToString() == "batch")
            { Response.Redirect("~/pages/CustomTaskListFliter.aspx?mode=browse"); }
            else
            { }
        }
        #endregion 事件

        #region 方法

        private void DataBindProjectPlanList(string range,string process)
        {
            //using (var ProjectModel = new ProjectCollection.WebUI.Models.ProjectCollectionEntities())
            //{
                var ProjectModel = new ProjectCollection.WebUI.Models.ProjectCollectionEntities();
                var projects = (from p in ProjectModel.BatchProject
                                orderby p.CreateDate descending
                                select p);
                data = projects.ToList();
                this.axgvProject.DataSource = data;
                this.axgvProject.DataBind();
           //}
            List<object> keyValues = axgvProject.GetCurrentPageRowValues(axgvProject.KeyFieldName);
            foreach (object key in keyValues)
            {
                HyperLink CurrentASelect = (HyperLink)axgvProject.FindRowCellTemplateControlByKey(key, (DevExpress.Web.GridViewDataColumn)axgvProject.Columns["Operate"], "aSelect");
                CurrentASelect.NavigateUrl = "~/pages/CustomTaskDetails.aspx?mode=browse" + "&id=" + key.ToString();
            }
        }
        #endregion 方法
    }
}