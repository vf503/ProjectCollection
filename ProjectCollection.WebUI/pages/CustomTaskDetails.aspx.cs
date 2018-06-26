using ProjectCollection.WebUI.pages.common;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Net;
using System.Text;
using System.Web;
using System.Linq;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;


namespace ProjectCollection.WebUI.pages
{
    public partial class CustomTaskDetails : BasePage
    {
        #region 事件
        //
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

            }
            //
            string id = this.Request["id"];
            this.hidProjectId.Value = id;
            using (var ProjectModel = new ProjectCollection.WebUI.Models.ProjectCollectionEntities())
            {
                ProjectCollection.WebUI.Models.BatchProject ThisProject = (from p in ProjectModel.BatchProject
                                                                           where p.id == id
                                                                           select p).First();
                this.InitBrowseData(ThisProject);
                //
                #region browse
                if (this.Request["mode"] == "browse")
                {
                    try
                    {
                        this.InitCheckData(ThisProject);
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
                        this.InitExecuteData(ThisProject);
                    }
                    catch
                    {

                    }
                    finally
                    {

                    }
                }
                #endregion browse
                #region check
                else if (this.Request["mode"] == "check")
                {
                    PanelCheck.Visible = true;
                    this.btnPass.Visible = true;
                    btnDel.Visible = true;
                }
                #endregion check
                #region execute
                else if (this.Request["mode"] == "execute")
                {
                    PanelCheck.Visible = true;
                    //this.btnExecute.Visible = true;
                    this.btnBack.Visible = true;
                    this.aExecute.Visible = true;
                    this.InitCheckData(ThisProject);
                    if (ThisProject.progress == "等待执行")
                    {
                        string encode = string.Empty;
                        byte[] bytes = Encoding.UTF8.GetBytes(this.LoginUserInfo.LoginName + "_" + this.LoginUserInfo.Password);
                        encode = HttpUtility.UrlEncode(Convert.ToBase64String(bytes), Encoding.UTF8);
                        aExecute.NavigateUrl = "http://newpms.cei.cn/webpages/V2/index.html#/HomePage?mode=disposal&project=" + ThisProject.id + "&login=" + encode;
                    }
                    else
                    {
                        aExecute.Text = "已处理";
                    }
                }
                #endregion execute
                else
                {
                    
                }
            }
        }
        //
        protected void btnExecuteOnclick(object sender, EventArgs e)
        {
            using (var ProjectModel = new ProjectCollection.WebUI.Models.ProjectCollectionEntities())
            {
                ProjectCollection.WebUI.Models.BatchProject ThisProject = (from p in ProjectModel.BatchProject
                                                                           where p.id == this.hidProjectId.Value.ToString()
                                                                           select p).First();
                if (ThisProject.progress == "等待执行")
                {
                    string encode = string.Empty;
                    byte[] bytes = Encoding.UTF8.GetBytes(this.LoginUserInfo.LoginName + "_" + this.LoginUserInfo.Password);
                    encode = Convert.ToBase64String(bytes);
                    Response.Write("<script>window.open('http://newpms.cei.cn/webpages/V2/index.html#/HomePage?mode=disposal&project=" + ThisProject.id + "&login=" + encode + "','_blank');</script>");
                }
                else
                {
                    //btnExecute.Text = "已处理";
                }
            }
        }
        //
        protected void btnBackOnclick(object sender, EventArgs e)
        {
            this.Redirect("~/pages/MyTask.aspx?mode=manufacture&range=now");
        }
        //
        protected void btnPassOnclick(object sender, EventArgs e)
        {
            using (var ProjectModel = new ProjectCollection.WebUI.Models.ProjectCollectionEntities())
            {
                ProjectCollection.WebUI.Models.BatchProject ThisProject = (from p in ProjectModel.BatchProject
                                                                           where p.id == this.hidProjectId.Value.ToString()
                                                                           select p).First();
                ThisProject.signer = this.LoginUserInfo.Identity;
                ThisProject.CheckDate = DateTime.Now;
                ThisProject.CheckNote = this.txtCheckNote.Text;
                ThisProject.progress = "等待执行";
                ProjectModel.SaveChanges();
            }
            this.Redirect("~/pages/MyTask.aspx?mode=manufacture&range=now");
        }
        //
        protected void btnDelOnclick(object sender, EventArgs e)
        {
            using (var ProjectModel = new ProjectCollection.WebUI.Models.ProjectCollectionEntities())
            {
                ProjectCollection.WebUI.Models.BatchProject ThisProject = (from p in ProjectModel.BatchProject
                                                                           where p.id == this.hidProjectId.Value.ToString()
                                                                           select p).First();
                if (ThisProject != null)
                {
                    ProjectModel.BatchProject.Remove(ThisProject);
                }
                var i = ProjectModel.SaveChanges();
            }
            this.Redirect("~/pages/MyTask.aspx?mode=manufacture&range=now");
        }
        #endregion 事件

        #region 方法
        private void InitBrowseData(Models.BatchProject ThisProject)
        {
            this.txtId.Text = ThisProject.id;
            this.txtCreateDate.Text = ThisProject.CreateDate.ToString("yyyy-MM-dd");
            this.txtCustom.Text = ThisProject.custom;
            JObject TaskRequireJson = (JObject)JsonConvert.DeserializeObject(ThisProject.TaskRequire);
            string TaskRequireStr = "";
            if (TaskRequireJson["template"].ToString() != "")
            {
                TaskRequireStr += "下载套模板：" + TaskRequireJson["template"]+" ; ";
            }
            if (TaskRequireJson["DisplaySize"].ToString() != "")
            {
                TaskRequireStr += "输出视频：" + TaskRequireJson["DisplaySize"] + " ; ";
            }
            this.txtTaskRequire.Text = TaskRequireStr;
            string CreatorName = "";
            using (var ProjectModel = new ProjectCollection.WebUI.Models.ProjectCollectionEntities())
            {

                ProjectCollection.WebUI.Models.user_info ThisUser = (from u in ProjectModel.user_info
                                                                     where u.user_identity == ThisProject.CreatorId
                                                                     select u).First();
                CreatorName = ThisUser.real_name;
            }
            this.txtCreator.Text = CreatorName;
            this.txtCreateNote.Text = ThisProject.CreateNote;
            this.txtProgress.Text = ThisProject.progress;
            if (ThisProject.progress != null)
            {
            }
            else { }
        }

        private void InitCheckData(Models.BatchProject ThisProject)
        {
            string SignerName = "";
            using (var ProjectModel = new ProjectCollection.WebUI.Models.ProjectCollectionEntities())
            {

                ProjectCollection.WebUI.Models.user_info ThisUser = (from u in ProjectModel.user_info
                                                                     where u.user_identity == ThisProject.signer
                                                                     select u).First();
                SignerName = ThisUser.real_name;
            }
            this.txtSigner.Text = SignerName;
            this.txtCheckDate.Text = Convert.ToDateTime(ThisProject.CheckDate).ToString("yyyy-MM-dd");
            this.txtCheckNote.Text = ThisProject.CheckNote;
        }

        private void InitExecuteData(Models.BatchProject ThisProject)
        {
            string TransactorName = "";
            using (var ProjectModel = new ProjectCollection.WebUI.Models.ProjectCollectionEntities())
            {

                ProjectCollection.WebUI.Models.user_info ThisUser = (from u in ProjectModel.user_info
                                                                     where u.user_identity == ThisProject.transactor
                                                                     select u).First();
                TransactorName = ThisUser.real_name;
            }
            this.txtTransactor.Text = TransactorName;
            this.txtFinishDate.Text = Convert.ToDateTime(ThisProject.FinishDate).ToString("yyyy-MM-dd");
            this.txtFinishNote.Text = ThisProject.FinishNote;
        }
        #endregion 方法
    }
}