using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProjectCollection.WebUI
{
    public partial class CustomProjectListEmbed : System.Web.UI.Page
    {
        public Guid type;
        public string mode;
        #region 事件

        protected void Page_Load(object sender, EventArgs e)
        {
            type = new Guid(this.Request["type"]);
            mode = this.Request["mode"];
            DataBindProjectPlanList(type, mode);
        }

        protected void axgvProject_PageIndexChanged(object sender, EventArgs e)
        {
            DataBindProjectPlanList(type, mode);
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
                CurrentASelect.NavigateUrl = "~/pages/CustomProjectCreateEdit.aspx?mode=" + this.Request["mode"] + "&type=" + this.Request["type"] + "&ProjectId=" + key.ToString();
            }
        }

        #endregion 事件

        #region 方法

        private void DataBindProjectPlanList(Guid type, string mode)
        {
            if (mode == "browse")
            {
                this.axgvProject.DataSource = BLL.CustomProject.GetCustomProjectByType(type);
                this.axgvProject.DataBind();
            }
            else if (mode == "receive")
            {
                this.axgvProject.DataSource = BLL.CustomProject.GetCustomProjectByType(type, new Guid("00000000-0000-0000-0000-000000000203"));
                this.axgvProject.DataBind();
            }
            else if (mode == "operation")
            {
                this.axgvProject.DataSource = BLL.CustomProject.GetCustomProjectByType(type, new Guid("00000000-0000-0000-0000-000000000204"));
                this.axgvProject.DataBind();
            }
            else if (mode == "publish")
            {
                this.axgvProject.DataSource = BLL.CustomProject.GetCustomProjectByType(type, new Guid("00000000-0000-0000-0000-000000000205"));
                this.axgvProject.DataBind();
            }
            else if (mode == "check")
            {
                this.axgvProject.DataSource = BLL.CustomProject.GetCustomProjectByType(type, new Guid("00000000-0000-0000-0000-000000000207"));
                this.axgvProject.DataBind();
            }
            else { }
            List<object> keyValues = axgvProject.GetCurrentPageRowValues(axgvProject.KeyFieldName);
            foreach (object key in keyValues)
            {
                HyperLink CurrentASelect = (HyperLink)axgvProject.FindRowCellTemplateControlByKey(key, (DevExpress.Web.GridViewDataColumn)axgvProject.Columns["Operate"], "aSelect");
                CurrentASelect.NavigateUrl = "~/pages/CustomProjectCreateEdit.aspx?mode=" + this.Request["mode"] + "&type=" + this.Request["type"] + "&ProjectId=" + key.ToString();
            }
        }

        #endregion 方法
    }
}