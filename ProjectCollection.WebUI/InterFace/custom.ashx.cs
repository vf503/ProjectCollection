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
                string midcustomerid = (string)o["MidCustomerId"];
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
                    ThisProject.MidCustomer = midcustomerid;
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
                    object OldData;
                    if (ThisProject.HelpCourseData is null)
                    {
                        OldData = "";
                    }
                    else
                    {
                        OldData = JsonConvert.DeserializeObject(ThisProject.HelpCourseData);
                    }
                    JObject rss = new JObject();
                    rss = new JObject(
                        new JProperty("id", ThisProject.id),
                        new JProperty("custom", ThisProject.custom),
                        new JProperty("CustomerId", ThisProject.customer),
                        new JProperty("MidCustomerId", ThisProject.MidCustomer),
                        new JProperty("user", ThisUser.real_name),
                        new JProperty("require", JsonConvert.DeserializeObject(ThisProject.TaskRequire)),
                        new JProperty("CreateDate", ThisProject.CreateDate.ToString("yyyy-MM-dd")),
                        new JProperty("DeadLine", ThisProject.DeadLine.HasValue ? ThisProject.DeadLine.Value.ToString("yyyy-MM-dd") : "n/a"), //nullabale type
                        new JProperty("note", ThisProject.CreateNote),
                        new JProperty("CheckDate", ThisProject.CheckDate.HasValue ? ThisProject.CheckDate.Value.ToString("yyyy-MM-dd") : "n/a"),
                        new JProperty("CheckNote", ThisProject.CheckNote),
                        new JProperty("HelpSendingDate", ThisProject.HelpSendingDate.ToString()),
                        new JProperty("HelperFinishDate", ThisProject.HelperFinishDate.ToString()),
                         new JProperty("McHelpSendingDate", ThisProject.McHelpSendingDate.ToString()),
                        new JProperty("McHelperFinishDate", ThisProject.McHelperFinishDate.ToString()),
                        new JProperty("PicSendingDate", ThisProject.PicSendingDate.ToString()),
                        new JProperty("PicFinishDate", ThisProject.PicFinishDate.ToString()),
                        new JProperty("TemplateSendingDate", ThisProject.TemplateSendingDate.ToString()),
                        new JProperty("TemplateFinishDate", ThisProject.TemplateFinishDate.ToString()),
                        new JProperty("AttachmentSendingDate", ThisProject.AttachmentSendingDate.ToString()),
                        new JProperty("AttachmentFinishDate", ThisProject.AttachmentFinishDate.ToString()),
                        new JProperty("FinishDate", ThisProject.FinishDate.ToString()),
                        new JProperty("CourseData", JsonConvert.DeserializeObject(ThisProject.CourseData)),
                        new JProperty("HelpCourseData", OldData)
                        );

                    context.Response.ContentType = "text/plain";
                    context.Response.StatusCode = 200;
                    context.Response.Write(rss.ToString());
                }
            }
            else if (HttpContext.Current.Request["method"] == "OldHelpQuery")
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
                    object OldData;
                    if (ThisProject.HelpCourseData is null)
                    {
                        OldData = "";
                    }
                    else
                    {
                        OldData = JsonConvert.DeserializeObject(ThisProject.HelpCourseData);
                    }
                    JObject rss = new JObject();
                    rss = new JObject(
                        new JProperty("id", ThisProject.id),
                        new JProperty("HelpCourseData", OldData)
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
                    //ThisProject.CourseData = CourseData;
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
                        string HelpCourseData = (string)o["HelpCourseData"].ToString();
                        ThisProject.HelpCourseData = HelpCourseData;
                    }
                    else if (type == "mchelp")
                    {
                        ThisProject.McHelpSendingDate = DateTime.Now;
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
            #region Count
            else if (HttpContext.Current.Request["method"] == "YearProjectCount")
            {
                string year = HttpContext.Current.Request["year"];
                using (var ProjectModel = new ProjectCollection.WebUI.Models.ProjectCollectionEntities())
                {
                    int AllProjectCount = (from p in ProjectModel.Project
                                           where p.ProjectNo.Contains("A-" + year) && p.progress.ToString() != "00000000-0000-0000-0000-000000000128"
                                           select p).Count();
                    int FinProjectCount = (from p in ProjectModel.Project
                                           where p.ProjectNo.Contains("A-" + year) && p.progress.ToString() == "00000000-0000-0000-0000-000000000119"
                                           select p).Count();
                    JObject rss = new JObject();
                    rss = new JObject(
                        new JProperty("all", AllProjectCount),
                        new JProperty("fin", FinProjectCount)
                        );
                    context.Response.ContentType = "text/plain";
                    context.Response.StatusCode = 200;
                    context.Response.Write(rss.ToString());
                }
            }
            else if (HttpContext.Current.Request["method"] == "CourseDistributionRate")
            {
                string start = HttpContext.Current.Request["start"];
                string end = HttpContext.Current.Request["end"];
                string area= HttpContext.Current.Request["area"];
                DateTime StartTime;
                DateTime EndTime;
                StartTime = Convert.ToDateTime(start);
                EndTime = Convert.ToDateTime(end);
                var ProjectModel = new ProjectCollection.WebUI.Models.ProjectCollectionEntities();
                var AllOrder = (from o in ProjectModel.TempOrder
                                join c in ProjectModel.TempCourse on o.CourseId equals c.CourseId
                                where (c.type == "自筹") && (!string.IsNullOrEmpty(c.SourceCourseId))
                                && (c.SourceCourseId != "") && (c.CreateDate >= StartTime) && (c.CreateDate <= EndTime)
                                && o.TempCustomer.area == area
                                select new
                                {
                                    name = c.title,
                                    sourceid = c.SourceCourseId,
                                    customid = o.CustomerId
                                });
                var DistinctOrder = AllOrder.GroupBy(o => new { o.customid, o.sourceid })
                    .Select(g => g.FirstOrDefault());
                var OrderCount = from o in DistinctOrder
                                 group o by new { o.sourceid } into oc
                                 select new
                                 {
                                     id = oc.Key.sourceid,
                                     count = oc.Count()
                                 };
                //int AllCount = (from c in  DistinctOrder
                //               group c by new { c.sourceid } into oc
                //               select oc).Count();
                int AllCount = (from p in ProjectModel.Project
                                where p.ProjectNo.Contains("A-" + StartTime.Year.ToString()) && p.progress.ToString() == "00000000-0000-0000-0000-000000000119"
                                && p.CheckTaskCheckDate >= StartTime && p.CheckTaskCheckDate <= EndTime
                                select p).Count();
                int Count5 = (from o in OrderCount
                              where o.count < 5
                              select o).Count();
                string count5rate = (Count5 / (Convert.ToDouble(AllCount)) * 100).ToString("f2");
                int Count6_10 = (from o in OrderCount
                                 where o.count > 5 && o.count <= 10
                                 select o).Count();
                string count6_10rate = (Count6_10 / (Convert.ToDouble(AllCount)) * 100).ToString("f2");
                int Count11_15 = (from o in OrderCount
                                  where o.count > 10 && o.count <= 15
                                  select o).Count();
                string count11_15rate = (Count11_15 / (Convert.ToDouble(AllCount)) * 100).ToString("f2");
                int Count16 = (from o in OrderCount
                               where o.count > 15
                               select o).Count();
                string count16rate = (Count16 / (Convert.ToDouble(AllCount)) * 100).ToString("f2");
                JObject rss = new JObject();
                rss = new JObject(
                    new JProperty("none", AllCount - Count5 - Count6_10 - Count11_15 - Count16),
                    new JProperty("count5", Count5),
                    new JProperty("count5rate", count5rate),
                    new JProperty("count6_10", Count6_10),
                    new JProperty("count6_10rate", count6_10rate),
                    new JProperty("count11_15", Count11_15),
                    new JProperty("count11_15rate", count11_15rate),
                    new JProperty("count16", Count16),
                    new JProperty("count16rate", count16rate)
                    );
                context.Response.ContentType = "text/plain";
                context.Response.StatusCode = 200;
                context.Response.Write(rss.ToString());
            }
            else if (HttpContext.Current.Request["method"] == "CourseTimesList")
            {
                string start = HttpContext.Current.Request["start"];
                string end = HttpContext.Current.Request["end"];
                DateTime StartTime;
                DateTime EndTime;
                StartTime = Convert.ToDateTime(start);
                EndTime = Convert.ToDateTime(end);
                var ProjectModel = new ProjectCollection.WebUI.Models.ProjectCollectionEntities();
                var AllOrder = (from o in ProjectModel.TempOrder
                                join c in ProjectModel.TempCourse on o.CourseId equals c.CourseId
                                where (c.type == "自筹") && (!string.IsNullOrEmpty(c.SourceCourseId))
                                && (c.SourceCourseId != "") && (c.CreateDate >= StartTime) && (c.CreateDate <= EndTime)
                                select new
                                {
                                    name = c.title,
                                    sourceid = c.SourceCourseId,
                                    customid = o.CustomerId
                                });
                var DistinctOrder = AllOrder.GroupBy(o => new { o.customid, o.sourceid })
                    .Select(g => g.FirstOrDefault());
                var OrderCount = from o in DistinctOrder
                                 group o by new { o.sourceid } into oc
                                 orderby oc.Count() descending
                                 select new
                                 {
                                     id = oc.Key.sourceid,
                                     //title =(from c in ProjectModel.TempCourse where c.SourceCourseId == oc.Key.sourceid select c.title).Take(1),
                                     count = oc.Count()
                                 };
                var OrderGroup = from o in OrderCount
                                 group o by new { o.count } into oc
                                 orderby oc.Count() descending
                                 select new
                                 {
                                     count = oc.Key.count,
                                     CourseCount = oc.Count()
                                 };
                string rss= JsonConvert.SerializeObject(OrderGroup);
                context.Response.ContentType = "text/plain";
                context.Response.StatusCode = 200;
                context.Response.Write(rss);

            }
            #endregion Count
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