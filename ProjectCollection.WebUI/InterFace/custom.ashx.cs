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
                string user = (string)o["user"];
                string note = (string)o["note"];
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
                    ThisProject.CreatorId = ThisUser.user_identity;
                    ThisProject.CreateNote = note;
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
                        new JProperty("user", ThisUser.real_name),
                        new JProperty("require", JsonConvert.DeserializeObject(ThisProject.TaskRequire)),
                        new JProperty("CreateDate", ThisProject.CreateDate.ToString(("yyyy-MM-dd"))),
                        new JProperty("note", ThisProject.CreateNote),
                        new JProperty("CheckDate", ThisProject.CheckDate),
                        new JProperty("CheckNote", ThisProject.CheckNote),
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
                string TaskRequire = (string)o["require"].ToString();
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
                    ThisProject.TaskRequire = TaskRequire;
                    ThisProject.CourseData = CourseData;
                    ThisProject.progress = "等待系统处理";
                    ThisProject.FinishDate = DateTime.Now;
                    ThisProject.FinishNote = note;
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