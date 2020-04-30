using ProjectCollection.BLL;
using ProjectCollection.Common;
using ProjectCollection.WebUI.pages.common;
using System;
using System.Linq;
using System.Configuration;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using Newtonsoft.Json;
using System.IO;
using System.Text;
using System.Net;
using Newtonsoft.Json.Linq;

namespace ProjectCollection.WebUI.pages
{
    public partial class ProjectListEmbed : BasePage

    {
        public List<Guid> BatchProjectId = new List<Guid>();
        public List<String> BatchProjectNo = new List<String>();
        //
        public string SerializeBatchProjectId;

        public Guid CurrentUserId;
        #region 事件

        protected void Page_Load(object sender, EventArgs e)
        {
            CurrentUserId = new Guid(this.Request["userid"]);
            var ProjectModel = new ProjectCollection.WebUI.Models.ProjectCollectionEntities();
            ProjectCollection.WebUI.Models.user_info ThisUser = (from p in ProjectModel.user_info
                                                                 where p.user_identity == CurrentUserId
                                                                 select p).First();

            if (!IsPostBack)
            {
                if (this.Request["mode"] == "browse")
                {
                    
                }
                else
                {
                    SearchProjectList(ThisUser.SupervisorRole.ToString());
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
            else if (this.Request["mode"] == "contentfinish")
            {
                aBatchHandle.Visible = true;
                btnBatchDownload.Visible = true;
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
                CurrentUserId = new Guid(this.Request["userid"]);
                var ProjectModel = new ProjectCollection.WebUI.Models.ProjectCollectionEntities();
                ProjectCollection.WebUI.Models.user_info ThisUser = (from p in ProjectModel.user_info
                                                                     where p.user_identity == CurrentUserId
                                                                     select p).First();
                SearchProjectList(ThisUser.SupervisorRole.ToString());
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
        private void SearchProjectList(string group="")
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
                //
                if (group== "00000000-0000-0000-0000-000000000008") {
                    Projects = Projects.Where(a => a.CourseType != "elite" && a.CourseType != "micro").ToList();
                }
                else if (group == "00000000-0000-0000-0000-000000000031")
                {
                    Projects = Projects.Where(a => a.CourseType == "elite" || a.CourseType == "micro").ToList();
                }
                //
                this.gvProject.DataSource = Projects;
                this.gvProject.DataBind();
                //int count = gvProject.Rows.Count;
                //for (int i = 0; i < count; i++)
                //{
                //    if (Projects[i+(gvProject.PageSize*gvProject.PageIndex)].emergency.ToString() == "00000000-0000-0000-0000-000000000030")
                //    { }
                //    else
                //    {
                //        gvProject.Rows[i].ForeColor = System.Drawing.Color.OrangeRed;
                //    }
                //}
            }
            else if (this.Request["mode"] == "productionfinish")
            {
                this.gvProject.DataSource = BLL.Project.GetDictionaryProductionIdProject(new Guid("00000000-0000-0000-0000-000000000114"), new Guid("00000000-0000-0000-0000-000000000125"), CurrentUserId);
                this.gvProject.DataBind();
            }
            else if (this.Request["mode"] == "productioncheck")
            {
                List<Project> Projects = BLL.Project.GetDictionaryProductionIdProject(new Guid("00000000-0000-0000-0000-000000000115"));
                //this.gvProject.DataSource = BLL.Project.GetDictionaryProductionIdProject(new Guid("00000000-0000-0000-0000-000000000115"));

                //
                if (group == "00000000-0000-0000-0000-000000000008")
                {
                    Projects = Projects.Where(a => a.CourseType != "elite" && a.CourseType != "micro").ToList();
                }
                else if (group == "00000000-0000-0000-0000-000000000031")
                {
                    Projects = Projects.Where(a => a.CourseType == "elite" || a.CourseType == "micro").ToList();
                }
                //
                this.gvProject.DataSource = Projects;
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
            //
            int count = gvProject.Rows.Count;
            for (int i = 0; i < count; i++)
            {
                var ProjectModel = new ProjectCollection.WebUI.Models.ProjectCollectionEntities();
                string ThisId = gvProject.Rows[i].Cells[1].Text;
                ProjectCollection.WebUI.Models.Project ThisProject = (from p in ProjectModel.Project
                                                                          where p.ProjectNo.ToString() == ThisId
                                                                      select p).First();
                if (ThisProject.emergency.ToString() == "00000000-0000-0000-0000-000000000031")
                {
                    gvProject.Rows[i].ForeColor = System.Drawing.Color.Orange;
                }
                else if (ThisProject.emergency.ToString() == "00000000-0000-0000-0000-000000000032")
                {
                    gvProject.Rows[i].ForeColor = System.Drawing.Color.Red;
                }
                else
                {
                    
                }
            }
        }
        //
        protected void gvProject_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (this.Request["mode"] == "browse")
            {

            }
            //else if (this.Request["mode"] == "contentfinish" || this.Request["mode"] == "contentcheck" || this.Request["mode"] == "contentrecheck")
            else if (this.Request["mode"] == "contentfinish")
            {
                string LecturerName = "";
                string PageIndex = gvProject.PageIndex.ToString();
                int count = gvProject.Rows.Count;
                for (int i = 0; i < count; i++)
                {
                    GridViewRow row = gvProject.Rows[i];
                    LecturerName = row.Cells[4].Text;
                    Label CurrentLabel = (Label)gvProject.Rows[i].FindControl("LabelInfo");
                    string url = @"http://newpms.cei.cn/UpdateLecturer/?name="
                        + LecturerName;
                    //
                    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                    request.Method = "GET";
                    request.ContentType = "text/html;charset=UTF-8";
                    HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                    Stream myResponseStream = response.GetResponseStream();
                    StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding("utf-8"));
                    string retString = myStreamReader.ReadToEnd();
                    myStreamReader.Close();
                    myResponseStream.Close();
                    //
                    JArray ja = JArray.Parse(retString);
                    string LecturerInfo = "";
                    int InfoCount = 0;
                    foreach(JObject jo in ja)
                    {
                        InfoCount += 1;
                        LecturerInfo += InfoCount.ToString()+")" + jo["job"].ToString() + "； ";
                    }
                    if (InfoCount > 0)
                    {
                        CurrentLabel.Text = LecturerInfo;
                    }
                    CurrentLabel.ToolTip= LecturerInfo;
                    CurrentLabel.Visible = true;
                    //
                    ID = gvProject.DataKeys[i].Value.ToString();
                    HyperLink CurrentLink = (HyperLink)gvProject.Rows[i].FindControl("aSelect");
                    CurrentLink.NavigateUrl = "~/pages/ProjectCreateEdit.aspx?mode=" + this.Request["mode"] + "&ProjectId=" + ID;
                }
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
                        //aBatchHandle.NavigateUrl = "~/pages/ProjectCreateEdit.aspx?mode=capturebatchhandle&BatchId=" + hidBatchId.Value;
                        aBatchHandle.NavigateUrl = "~/pages/ProjectCreateEdit.aspx?mode=capturebatchhandle";
                    }
                    else
                    {
                        tips.ForeColor = System.Drawing.Color.Red;
                        tips.Text = "只能多选状态相同的工单！";
                    }
                }
                if (this.Request["mode"] == "contentreceive")
                {
                    //aBatchHandle.NavigateUrl = "~/pages/ProjectCreateEdit.aspx?mode=contentreceivebatchhandle&BatchId=" + hidBatchId.Value;
                    aBatchHandle.NavigateUrl = "~/pages/ProjectCreateEdit.aspx?mode=contentreceivebatchhandle";
                }
                else if (this.Request["mode"] == "contentfinish")
                {
                    aBatchHandle.NavigateUrl = "~/pages/ProjectCreateEdit.aspx?mode=contentfinishbatchhandle";
                }
                else if (this.Request["mode"] == "contentcheck")
                {
                    aBatchSave.NavigateUrl = "~/pages/ProjectCreateEdit.aspx?mode=contentcheckbatchsave";
                    aBatchHandle.NavigateUrl = "~/pages/ProjectCreateEdit.aspx?mode=contentcheckbatchhandle";
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
        protected void btnBatchDownload_Click(object sender, EventArgs e)
        {
            btnBatchDownload.Text = "正在打包音频";
            for (int i = 0; i < gvProject.Rows.Count; i++)
            {
                GridViewRow row = gvProject.Rows[i];
                bool isChecked = ((CheckBox)row.FindControl("SelectCheckBox")).Checked;
                string CurrentId = row.Cells[1].Text;
                if (CurrentId.Substring(0, 1) == "X") { }
                else
                {
                    if (isChecked)
                    {
                        //Guid CurrentId = Guid.Parse(gvProject.DataKeys[gvProject.Rows[i].DataItemIndex].Values["ProjectId"].ToString());
                        if (BatchProjectNo.Contains(CurrentId))
                        {

                        }
                        else { BatchProjectNo.Add(CurrentId); }
                    }
                    else
                    {
                        if (BatchProjectNo.Contains(CurrentId))
                        {
                            BatchProjectNo.Remove(CurrentId);
                        }
                        else { }
                    }
                }
            }
            JsonSerializer serializer = new JsonSerializer();
            StringWriter sw = new StringWriter();
            serializer.Serialize(new JsonTextWriter(sw), BatchProjectNo);
            string PostData = sw.GetStringBuilder().ToString();
            string result = sw.GetStringBuilder().ToString();
            string UserName = LoginUserInfo.LoginName;
            string PassWord = LoginUserInfo.Password;
            byte[] bytes = Encoding.Default.GetBytes(UserName + "_" + PassWord);
            string str = Convert.ToBase64String(bytes);
            string url = @"http://newpms.cei.cn/DownloadAudioPack/"
                + "?link="
                + str;
            //
            String ret = "";
            HttpWebRequest webrequest = (HttpWebRequest)HttpWebRequest.Create(url);
            webrequest.Method = "post";
            //webrequest.ContentType = "text/html;charset=UTF-8";
            byte[] postdatabyte = Encoding.UTF8.GetBytes(PostData);
            webrequest.ContentLength = postdatabyte.Length;
            Stream stream;
            stream = webrequest.GetRequestStream();
            stream.Write(postdatabyte, 0, postdatabyte.Length);
            stream.Close();
            try
            {
                HttpWebResponse response = (HttpWebResponse)webrequest.GetResponse();
                StreamReader sr = new StreamReader(response.GetResponseStream(), Encoding.Default);
                ret = sr.ReadToEnd();
                sr.Close();
                response.Close();
                stream.Close();
            }
            catch (WebException ex)
            {
                StreamReader reex = new StreamReader(ex.Response.GetResponseStream());
                string strex = reex.ReadToEnd();
            }
            //
            JObject jo = (JObject)JsonConvert.DeserializeObject(ret);
            if (jo["status"].ToString() == "success")
            {
                aDownload.NavigateUrl = jo["data"].ToString();
                aDownload.Visible = true;
                btnBatchDownload.Enabled = false;
                btnBatchDownload.Text = "已打包音频";
                System.Web.UI.ScriptManager.RegisterStartupScript(this.UpdatePanel1, this.GetType(), "download", "window.open('" + jo["data"].ToString() + "');", true);
            }
            else
            {

            }
            //New ED
        }
        #endregion 方法
    }
}