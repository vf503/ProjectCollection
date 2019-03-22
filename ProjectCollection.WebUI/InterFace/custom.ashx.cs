using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using ProjectCollection.BLL;

namespace ProjectCollection.WebUI.InterFace
{
    /// <summary>
    /// custom 的摘要说明
    /// </summary>
    public class custom : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            //Request
            StreamReader sr = new StreamReader(HttpContext.Current.Request.InputStream);
            string strReq = sr.ReadToEnd();
            //context.Response.AppendHeader("Access-Control-Allow-Headers", "content-type");
            //context.Response.AppendHeader("Access-Control-Allow-Origin", "http://localhost:8080");
            //context.Response.AppendHeader("Access-Control-Allow-Credentials", "true");
            //string origin = HttpContext.Current.Request.Headers.Get("Origin");
            //context.Response.AppendHeader("Access-Control-Allow-Origin", "http://localhost:8080");
            //context.Response.Headers.Set("Access-Control-Allow-Origin", origin);

            #region Insert
            if (HttpContext.Current.Request["method"] == "insert")
            {
                JObject o = JObject.Parse(strReq);
                string id = (string)o["id"];
                string custom = (string)o["custom"];
                string customerid = (string)o["CustomerId"];
                string user = (string)o["user"];
                string note = (string)o["note"];
                string DeadLine = (string)o["DeadLine"];
                string TaskRequire = (string)o["require"].ToString();
                string CourseData = (string)o["CourseData"].ToString();

                using (var ProjectModel = new ProjectCollection.WebUI.Models.ProjectCollectionEntities())
                {
                    ProjectCollection.WebUI.Models.BatchProject ThisProject = new Models.BatchProject();
                    int ProjectCount = (from p in ProjectModel.BatchProject
                                        where p.id == id
                                        select p).Count();
                    if (ProjectCount > 0)
                    {
                        context.Response.ContentType = "text/plain";
                        context.Response.StatusCode = 204;
                        context.Response.Write("ID Repetition");
                        return;
                    }
                    ProjectCollection.WebUI.Models.user_info ThisUser = (from u in ProjectModel.user_info
                                                                         where u.login_name == user
                                                                         select u).First();
                    ThisProject.id = id;
                    ThisProject.custom = custom;
                    ThisProject.customer = customerid;
                    ThisProject.CreatorId = ThisUser.user_identity;
                    ThisProject.CreateNote = note;
                    ThisProject.DeadLine = Convert.ToDateTime(DeadLine);
                    ThisProject.TaskRequire = TaskRequire;
                    ThisProject.progress = "等待审核";
                    ThisProject.CreateDate = DateTime.Now;
                    ThisProject.CourseData = CourseData;
                    ProjectModel.BatchProject.Add(ThisProject);
                    ProjectModel.SaveChanges();
                }
                context.Response.ContentType = "text/plain";
                context.Response.StatusCode = 201;
                context.Response.Write("success");
            }
            #endregion Insert
            #region Get
            else if (HttpContext.Current.Request["method"] == "get")
            {
                string id = HttpContext.Current.Request["id"];
                using (var ProjectModel = new ProjectCollection.WebUI.Models.ProjectCollectionEntities())
                {
                    int ProjectCount = (from p in ProjectModel.BatchProject
                                        where p.id == id
                                        select p).Count();
                    if (ProjectCount == 0)
                    {
                        context.Response.ContentType = "text/plain";
                        context.Response.StatusCode = 404;
                        context.Response.Write("Not Found Project");
                        return;
                    }
                    ProjectCollection.WebUI.Models.BatchProject ThisProject = (from p in ProjectModel.BatchProject
                                                                               where p.id == id
                                                                               select p).First();
                    ProjectCollection.WebUI.Models.user_info ThisUser = (from u in ProjectModel.user_info
                                                                         where u.user_identity == ThisProject.CreatorId
                                                                         select u).First();
                    JObject rss = new JObject();
                    rss = new JObject(
                        new JProperty("id", ThisProject.id),
                        new JProperty("custom", ThisProject.custom),
                        new JProperty("CustomerId", ThisProject.customer),
                        new JProperty("user", ThisUser.real_name),
                        new JProperty("require", JsonConvert.DeserializeObject(ThisProject.TaskRequire)),
                        new JProperty("CreateDate", ThisProject.CreateDate.ToString("yyyy-MM-dd")),
                        new JProperty("DeadLine", ThisProject.DeadLine.HasValue ? ThisProject.DeadLine.Value.ToString("yyyy-MM-dd") : "n/a"), //nullabale type
                        new JProperty("note", ThisProject.CreateNote),
                        new JProperty("CheckDate", ThisProject.CheckDate.HasValue ? ThisProject.CheckDate.Value.ToString("yyyy-MM-dd") : "n/a"),
                        new JProperty("CheckNote", ThisProject.CheckNote),
                        new JProperty("HelpSendingDate", ThisProject.HelpSendingDate.ToString()),
                        new JProperty("HelperFinishDate", ThisProject.HelperFinishDate.ToString()),
                        new JProperty("PicSendingDate", ThisProject.PicSendingDate.ToString()),
                        new JProperty("PicFinishDate", ThisProject.PicFinishDate.ToString()),
                        new JProperty("TemplateSendingDate", ThisProject.TemplateSendingDate.ToString()),
                        new JProperty("TemplateFinishDate", ThisProject.TemplateFinishDate.ToString()),
                        new JProperty("AttachmentSendingDate", ThisProject.AttachmentSendingDate.ToString()),
                        new JProperty("AttachmentFinishDate", ThisProject.AttachmentFinishDate.ToString()),
                        new JProperty("FinishDate", ThisProject.FinishDate.ToString()),
                        new JProperty("CourseData", JsonConvert.DeserializeObject(ThisProject.CourseData))
                        );
                    context.Response.ContentType = "text/plain";
                    context.Response.StatusCode = 200;
                    context.Response.Write(rss.ToString());
                }
            }
            #endregion Get
            #region Update
            else if (HttpContext.Current.Request["method"] == "UpdateProgress")
            {
                JObject o = JObject.Parse(strReq);
                string id = (string)o["id"];
                //string TaskRequire = (string)o["require"].ToString();
                string CourseData = (string)o["CourseData"].ToString();
                string transactor = (string)o["transactor"].ToString();
                string note = (string)o["note"].ToString();
                using (var ProjectModel = new ProjectCollection.WebUI.Models.ProjectCollectionEntities())
                {
                    ProjectCollection.WebUI.Models.BatchProject ThisProject = (from p in ProjectModel.BatchProject
                                                                               where p.id == id
                                                                               select p).First();
                    ProjectCollection.WebUI.Models.user_info ThisUser = (from u in ProjectModel.user_info
                                                                         where u.login_name == transactor
                                                                         select u).First();
                    ThisProject.transactor = ThisUser.user_identity;
                    //ThisProject.TaskRequire = TaskRequire;
                    ThisProject.CourseData = CourseData;
                    ThisProject.progress = "已完成";
                    ThisProject.FinishDate = DateTime.Now;
                    ThisProject.FinishNote = note;
                    ProjectModel.SaveChanges();
                }
                context.Response.ContentType = "text/plain";
                context.Response.StatusCode = 200;
                context.Response.Write("success");
            }
            else if (HttpContext.Current.Request["method"] == "UpdateSending")
            {
                JObject o = JObject.Parse(strReq);
                string id = (string)o["id"];
                string type = (string)o["type"].ToString();
                using (var ProjectModel = new ProjectCollection.WebUI.Models.ProjectCollectionEntities())
                {
                    ProjectCollection.WebUI.Models.BatchProject ThisProject = (from p in ProjectModel.BatchProject
                                                                               where p.id == id
                                                                               select p).First();
                    if (type == "help")
                    {
                        ThisProject.HelpSendingDate = DateTime.Now;
                    }
                    else if (type == "pic")
                    {
                        ThisProject.PicSendingDate = DateTime.Now;
                    }
                    else if (type == "template")
                    {
                        ThisProject.TemplateSendingDate = DateTime.Now;
                    }
                    else if (type == "attachment")
                    {
                        ThisProject.AttachmentSendingDate = DateTime.Now;
                    }
                    else { }
                    ProjectModel.SaveChanges();
                }
                context.Response.ContentType = "text/plain";
                context.Response.StatusCode = 200;
                context.Response.Write("success");
            }
            #endregion Update
            else { }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}