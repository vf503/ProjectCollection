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
                #region helpexecute
                else if (this.Request["mode"] == "helpexecute")
                {
                    PanelCheck.Visible = true;
                    PanelHelpFinish.Visible = true;
                    this.InitCheckData(ThisProject);
                    this.txtHelpSendingDate.Text = ThisProject.HelpSendingDate.ToString();
                    this.btnPass.Visible = true;
                    this.btnPass.Text = "完成";
                }
                #endregion helpexecute
                #region pic
                else if (this.Request["mode"] == "pic")
                {
                    PanelCheck.Visible = true;
                    PanelPicFinish.Visible = true;
                    this.InitCheckData(ThisProject);
                    this.txtPicSendingDate.Text = ThisProject.PicSendingDate.ToString();
                    this.btnPass.Visible = true;
                    this.btnPass.Text = "完成";
                }
                #endregion pic
                #region template
                else if (this.Request["mode"] == "template")
                {
                    PanelCheck.Visible = true;
                    PanelTemplateFinish.Visible = true;
                    this.InitCheckData(ThisProject);
                    this.txtTemplateSendingDate.Text = ThisProject.TemplateSendingDate.ToString();
                    this.btnPass.Visible = true;
                    this.btnPass.Text = "完成";
                }
                #endregion template
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
                if (this.Request["mode"] == "check")
                {
                    ThisProject.signer = this.LoginUserInfo.Identity;
                    ThisProject.CheckDate = DateTime.Now;
                    ThisProject.CheckNote = this.txtCheckNote.Text;
                    ThisProject.progress = "等待执行";
                }
                else if (this.Request["mode"] == "helpexecute")
                {
                    ThisProject.helper = this.LoginUserInfo.Identity;
                    ThisProject.HelperFinishDate = DateTime.Now;
                    ThisProject.HelperFinishNote = this.txtHelperFinishNote.Text;
                }
                else if (this.Request["mode"] == "pic")
                {
                    ThisProject.PicProducer = this.LoginUserInfo.Identity;
                    ThisProject.PicFinishDate = DateTime.Now;
                    ThisProject.PicFinishNote = this.txtPicFinishNote.Text;
                }
                else if (this.Request["mode"] == "template")
                {
                    ThisProject.TemplateProducer = this.LoginUserInfo.Identity;
                    ThisProject.TemplateFinishDate = DateTime.Now;
                    ThisProject.TemplateFinishNote = this.txtTemplateFinishNote.Text;
                }
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
            this.txtDeadLine.Text = ThisProject.DeadLine.HasValue ? ThisProject.DeadLine.Value.ToString("yyyy-MM-dd") : "n/a";
            this.txtCustom.Text = ThisProject.custom;
            JObject TaskRequireJson = (JObject)JsonConvert.DeserializeObject(ThisProject.TaskRequire);
            string TaskRequireStr = "";
            if (TaskRequireJson["template"].ToString() != "")
            {
                TaskRequireStr += "下载套模板：" + TaskRequireJson["template"]+ " ; ";
            }
            if (TaskRequireJson["DisplaySize"].ToString() != "")
            {
                TaskRequireStr += "输出分辨率：" + TaskRequireJson["DisplaySize"] + "\r\n";
            }
            if (TaskRequireJson["BitRate"].ToString() != "")
            {
                TaskRequireStr += "码率：" + TaskRequireJson["BitRate"] + "K ; ";
            }
            if (TaskRequireJson["IsWaterMark"].ToString() != "")
            {
                TaskRequireStr += "水印：";
                TaskRequireStr += TaskRequireJson["IsWaterMark"].ToString() == "1" ? "要\r\n" : "不要\r\n";
            }
            if (TaskRequireJson["IsPic"].ToString() != "")
            {
                TaskRequireStr += "做图片：";
                TaskRequireStr += TaskRequireJson["IsPic"].ToString() == "1" ? "是" : "否";
                TaskRequireStr += " ; ";
                TaskRequireStr += "图片要求：" + TaskRequireJson["PicNote"] + "\r\n";
            }
            if (TaskRequireJson["IsTemplate"].ToString() != "")
            {
                TaskRequireStr += "做新模板：";
                TaskRequireStr += TaskRequireJson["IsTemplate"].ToString() == "1" ? "是 ; " : "否 ; ";
                TaskRequireStr += "模板要求：" + TaskRequireJson["TemplateNote"] + "\r\n";
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
            string encode = string.Empty;
            byte[] bytes = Encoding.UTF8.GetBytes(this.LoginUserInfo.LoginName + "_" + this.LoginUserInfo.Password);
            encode = HttpUtility.UrlEncode(Convert.ToBase64String(bytes), Encoding.UTF8);
            this.aCourseList.NavigateUrl = "http://newpms.cei.cn/webpages/V2/index.html#/HomePage?mode=browse&project=" + ThisProject.id + "&login=" + encode; ;
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