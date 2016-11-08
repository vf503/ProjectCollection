using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using ProjectCollection.BLL;

namespace ProjectCollection.WebUI.WebService
{
    /// <summary>
    /// Login 的摘要说明
    /// </summary>
    public class MainMethod : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            //Stream reqStream = HttpContext.Current.Request.InputStream; 
            //byte[] buffer = new byte[(int)reqStream.Length];
            //reqStream.Read(buffer, 0, (int)reqStream.Length);

            //Request
            StreamReader sr = new StreamReader(HttpContext.Current.Request.InputStream);
            string strReq = sr.ReadToEnd();

            //Test
            //JObject Req = new JObject(
            //          new JProperty("loginname", "zxn"),
            //          new JProperty("password", "zxn")
           //new JProperty("userid", "00000000-0000-0000-0000-000000000003")
            //         );
            //string strReq = Req.ToString();


            #region ShowYouSend
            if (HttpContext.Current.Request["method"] == "showyousend")
            {
                context.Response.ContentType = "text/plain";
                context.Response.Write(strReq);
            }
            #endregion ShowYouSend

            #region Login
            if (HttpContext.Current.Request["method"] == "login")
            {
                JObject o = JObject.Parse(strReq);
                string loginname = (string)o["loginname"];
                string password = (string)o["password"];
                BLL.UserInfo userInfo;
                userInfo = UserInfo.LoginInfo(loginname, password);
                JObject rss = new JObject();
                if (userInfo.Identity == Guid.Empty)//失败
                {
                    rss = new JObject(
                    new JProperty("Method", "login"),
                    new JProperty("ReturnStatus", "0")
                    );
                }
                else
                {
                    List<string> CurrentUserAuthority = userInfo.Authority;
                    List<string> AllAuthority = UserAuthority.GetAllAuthority();
                    //Dictionary<string, string> CurrentUserAuthorityDic = new Dictionary<string, string>();
                    //foreach (string s in AllAuthority)
                    //{
                    //    if (CurrentUserAuthority.Contains(s))
                    //    {
                    //        CurrentUserAuthorityDic.Add(s, "1");
                    //    }
                    //    else 
                    //    { 
                    //        CurrentUserAuthorityDic.Add(s, "0"); 
                    //    }
                    //}
                    JObject CurrentUserAuthorityDic = new JObject();
                    foreach (string s in AllAuthority)
                    {
                        if (CurrentUserAuthority.Contains(s))
                        {
                            CurrentUserAuthorityDic.Add(s, "1");
                        }
                        else
                        {
                            CurrentUserAuthorityDic.Add(s, "0");
                        }
                    }
                    rss = new JObject(
                    new JProperty("Method", "login"),
                    new JProperty("ReturnStatus", "1"),
                    new JProperty("UserId", userInfo.Identity),
                    new JProperty("UserName", userInfo.RealName),
                    new JProperty("Authority",
                        new JArray(CurrentUserAuthorityDic)
                            )
                    );
                }
                context.Response.ContentType = "text/plain";
                context.Response.Write(rss.ToString());
            }
            #endregion Login
            #region MyTask
            else if (HttpContext.Current.Request["method"] == "mytask")
            {
                JObject o = JObject.Parse(strReq);
                string UserId = (string)o["userid"];
                BLL.UserInfo userInfo;
                userInfo = UserInfo.GetUserById(UserId);
                List<string> CurrentUserAuthoritys = userInfo.Authority;
                JObject rss = new JObject();
                foreach (string CurrentAuthority in CurrentUserAuthoritys)
                {
                    if (CurrentAuthority != "copy" && CurrentAuthority != "PlanManage")//无需处理特定工单
                    {
                        if (CurrentAuthority == "recond")
                        {
                            List<ProjectPlan> Recond = BLL.ProjectPlan.GetRecondProjectPlan();
                            JObject JRecond = 
                                new JObject(
                                    new JProperty("Recond",
                                        new JArray(
                                            from r in Recond
                                            orderby r.PlanDate
                                            select new JObject(
                                            new JProperty("ProjectPlanId", r.ProjectPlanId),
                                            new JProperty("ProjectPlanNo", r.ProjectPlanNo),
                                            new JProperty("Title", r.Title),
                                            new JProperty("ProjectPlanTypeText", r.ProjectPlanTypeText),
                                            new JProperty("PlanDate", r.PlanDate.ToString("D")),
                                            new JProperty("ProgressText", r.ProgressText)
                                                               )
                                                    )
                                                  )
                                            );
                            rss.Merge(JRecond, new JsonMergeSettings
                            {
                                MergeArrayHandling = MergeArrayHandling.Concat
                            }
                            );
                        }
                        else if (CurrentAuthority == "OpenClassReceive")
                        {
                            List<CustomProject> OpenClass = BLL.CustomProject.GetCustomProjectByType(new Guid("00000000-0000-0000-0000-000000000202"), new Guid("00000000-0000-0000-0000-000000000203"));
                            JObject JOpenClass =
                                new JObject(
                                    new JProperty("OpenClassReceive",
                                        new JArray(
                                            from oc in OpenClass
                                            orderby oc.SendingDate
                                            select new JObject(
                                            new JProperty("CustomProjectId", oc.CustomProjectId),
                                            new JProperty("CustomProjectNo", oc.No),
                                            new JProperty("Title", oc.Title),
                                            new JProperty("SendingDate", oc.SendingDate.ToString("D")),
                                            new JProperty("Lecturer", oc.Lecturer),
                                            new JProperty("ProgressText", oc.ProgressText)
                                                               )
                                                    )
                                                  )
                                            );
                            rss.Merge(OpenClass, new JsonMergeSettings
                            {
                                MergeArrayHandling = MergeArrayHandling.Concat
                            }
                            );
                        }
                        else if (CurrentAuthority == "OpenClassOperation")
                        {
                            List<CustomProject> OpenClass = BLL.CustomProject.GetCustomProjectByType(new Guid("00000000-0000-0000-0000-000000000202"), new Guid("00000000-0000-0000-0000-000000000204"));
                            JObject JOpenClass =
                                new JObject(
                                    new JProperty("OpenClassOperation",
                                        new JArray(
                                            from oc in OpenClass
                                            orderby oc.SendingDate
                                            select new JObject(
                                            new JProperty("CustomProjectId", oc.CustomProjectId),
                                            new JProperty("CustomProjectNo", oc.No),
                                            new JProperty("Title", oc.Title),
                                            new JProperty("SendingDate", oc.SendingDate.ToString("D")),
                                            new JProperty("Lecturer", oc.Lecturer),
                                            new JProperty("ProgressText", oc.ProgressText)
                                                               )
                                                    )
                                                  )
                                            );
                            rss.Merge(OpenClass, new JsonMergeSettings
                            {
                                MergeArrayHandling = MergeArrayHandling.Concat
                            }
                            );
                        }
                        else if (CurrentAuthority == "OpenClassPublish")
                        {
                            List<CustomProject> OpenClass = BLL.CustomProject.GetCustomProjectByType(new Guid("00000000-0000-0000-0000-000000000202"), new Guid("00000000-0000-0000-0000-000000000205"));
                            JObject JOpenClass =
                                new JObject(
                                    new JProperty("OpenClassPublish",
                                        new JArray(
                                            from oc in OpenClass
                                            orderby oc.SendingDate
                                            select new JObject(
                                            new JProperty("CustomProjectId", oc.CustomProjectId),
                                            new JProperty("CustomProjectNo", oc.No),
                                            new JProperty("Title", oc.Title),
                                            new JProperty("SendingDate", oc.SendingDate.ToString("D")),
                                            new JProperty("Lecturer", oc.Lecturer),
                                            new JProperty("ProgressText", oc.ProgressText)
                                                               )
                                                    )
                                                  )
                                            );
                            rss.Merge(OpenClass, new JsonMergeSettings
                            {
                                MergeArrayHandling = MergeArrayHandling.Concat
                            }
                            );
                        }
                        else if (CurrentAuthority == "OpenClassCheck")
                        {
                            List<CustomProject> OpenClass = BLL.CustomProject.GetCustomProjectByType(new Guid("00000000-0000-0000-0000-000000000202"), new Guid("00000000-0000-0000-0000-000000000207"));
                            JObject JOpenClass =
                                new JObject(
                                    new JProperty("OpenClassCheck",
                                        new JArray(
                                            from oc in OpenClass
                                            orderby oc.SendingDate
                                            select new JObject(
                                            new JProperty("CustomProjectId", oc.CustomProjectId),
                                            new JProperty("CustomProjectNo", oc.No),
                                            new JProperty("Title", oc.Title),
                                            new JProperty("SendingDate", oc.SendingDate.ToString("D")),
                                            new JProperty("Lecturer", oc.Lecturer),
                                            new JProperty("ProgressText", oc.ProgressText)
                                                               )
                                                    )
                                                  )
                                            );
                            rss.Merge(OpenClass, new JsonMergeSettings
                            {
                                MergeArrayHandling = MergeArrayHandling.Concat
                            }
                            );
                        }
                        else
                        {
                            List<Project> ProjectList = BLL.Project.GetProjectList(CurrentAuthority, UserId);
                            JObject JProject =
                                new JObject(
                                    new JProperty(CurrentAuthority,
                                        new JArray(
                                            from p in ProjectList
                                            orderby p.SendingDate
                                            select new JObject(
                                            //new JProperty("ProjectId", p.ProjectId),
                                            //new JProperty("ProjectNo", p.ProjectNo),
                                            //new JProperty("Title", p.CourseName),
                                            //new JProperty("SendingDate", p.SendingDate.ToString("D")),
                                            //new JProperty("Lecturer", p.lecturer),
                                            //new JProperty("ProgressText", p.ProgressText)
                                            from System.Reflection.PropertyInfo ProjectProperty in p.GetType().GetProperties()
                                            select new JProperty(
                                                ProjectProperty.Name, ProjectProperty.GetValue(p)
                                                )
                                                               )
                                                    )
                                                  )
                                            );
                            rss.Merge(JProject, new JsonMergeSettings
                            {
                                MergeArrayHandling = MergeArrayHandling.Concat
                            }
                            );
                        }
                    }  
                }
                JObject ResRss = new JObject(
                    new JProperty("method", "mytask"),
                    new JProperty("tasks",
                    new JObject(rss)
                    )
                    );
                context.Response.ContentType = "text/plain";
                context.Response.Write(ResRss.ToString());
            }
            #endregion MyTask
            #region PlanList
            else if (HttpContext.Current.Request["method"] == "allplan")
            {
                JObject o = JObject.Parse(strReq);
                int PageIndex = Convert.ToInt16((string)o["pageindex"]);
                JObject rss = new JObject();
                rss = new JObject(
                    new JProperty("method", "allplan")
                    );
                List<ProjectPlan> PlanList = BLL.ProjectPlan.GetAllProjectPlanPage(PageIndex);
                JObject JPlan =
                    new JObject(
                    new JProperty("Plan",
                    new JArray(
                                from p in PlanList
                                orderby p.PlanDate
                                select new JObject(
                                //new JProperty("ProjectPlanId", p.ProjectPlanId),
                                //new JProperty("ProjectNo", p.ProjectPlanNo),
                                //new JProperty("Title", p.Title),
                                //new JProperty("PlanDate", p.PlanDate.ToString("D")),
                                //new JProperty("Lecturer", p.Lecturer),
                                //new JProperty("ProgressText", p.ProgressText),
                                //new JProperty("ProjectCount", p.ProjectCount),
                                //new JProperty("ProjectFinishCount", p.ProjectFinishCount),
                                //new JProperty("ProjectDelayCount", p.ProjectDelayCount),
                                //new JProperty("ProjectPlanTypeId", p.ProjectPlanTypeId)
                                from System.Reflection.PropertyInfo PlanProperty in p.GetType().GetProperties()
                                select new JProperty(
                                    PlanProperty.Name,PlanProperty.GetValue(p)
                                    )
                                )
                                )
                                )
                                );
                rss.Merge(JPlan, new JsonMergeSettings
                            {
                                MergeArrayHandling = MergeArrayHandling.Concat
                            }
                            );
                context.Response.ContentType = "text/plain";
                context.Response.Write(rss.ToString());
            }
            else if (HttpContext.Current.Request["method"] == "finishedplan")
            {
                JObject o = JObject.Parse(strReq);
                int PageIndex = Convert.ToInt16((string)o["pageindex"]);
                JObject rss = new JObject();
                rss = new JObject(
                    new JProperty("method", "finishedplan")
                    );
                List<ProjectPlan> PlanList = BLL.ProjectPlan.GetFinishedProjectPlanPage(PageIndex);
                JObject JPlan =
                    new JObject(
                    new JProperty("Plan",
                    new JArray(
                                from p in PlanList
                                orderby p.PlanDate
                                select new JObject(
                                from System.Reflection.PropertyInfo PlanProperty in p.GetType().GetProperties()
                                select new JProperty(
                                    PlanProperty.Name, PlanProperty.GetValue(p)
                                    )
                                )
                                )
                                )
                                );
                rss.Merge(JPlan, new JsonMergeSettings
                {
                    MergeArrayHandling = MergeArrayHandling.Concat
                }
                            );
                context.Response.ContentType = "text/plain";
                context.Response.Write(rss.ToString());
            }
            else if (HttpContext.Current.Request["method"] == "unfinishplan")
            {
                JObject o = JObject.Parse(strReq);
                int PageIndex = Convert.ToInt16((string)o["pageindex"]);
                JObject rss = new JObject();
                rss = new JObject(
                    new JProperty("method", "unfinishplan")
                    );
                List<ProjectPlan> PlanList = BLL.ProjectPlan.GetUnfinishProjectPlanPage(PageIndex);
                JObject JPlan =
                    new JObject(
                    new JProperty("Plan",
                    new JArray(
                                from p in PlanList
                                orderby p.PlanDate
                                select new JObject(
                                from System.Reflection.PropertyInfo PlanProperty in p.GetType().GetProperties()
                                select new JProperty(
                                    PlanProperty.Name, PlanProperty.GetValue(p)
                                    )
                                )
                                )
                                )
                                );
                rss.Merge(JPlan, new JsonMergeSettings
                {
                    MergeArrayHandling = MergeArrayHandling.Concat
                }
                            );
                context.Response.ContentType = "text/plain";
                context.Response.Write(rss.ToString());
            }
            #endregion PlanList
            #region ProjectInPlan
            else if (HttpContext.Current.Request["method"] == "projectinplan")
            {
                JObject o = JObject.Parse(strReq);
                Guid PlanId = new Guid((string)o["planid"]);
                JObject rss = new JObject();
                rss = new JObject(
                    new JProperty("method", "projectinplan")
                    );
                List<Project> ProjectList = BLL.Project.GetProjectByPlanId(PlanId);
                JObject JProject =
                    new JObject(
                    new JProperty("Project",
                    new JArray(
                                from p in ProjectList
                                orderby p.SendingDate
                                select new JObject(
                                from System.Reflection.PropertyInfo ProjectProperty in p.GetType().GetProperties()
                                select new JProperty(
                                    ProjectProperty.Name, ProjectProperty.GetValue(p)
                                    )
                                )
                                )
                                )
                                );
                rss.Merge(JProject, new JsonMergeSettings
                {
                    MergeArrayHandling = MergeArrayHandling.Concat
                }
                            );
                context.Response.ContentType = "text/plain";
                context.Response.Write(rss.ToString());
            }
            #endregion ProjectInPlan
            #region Project
            else if (HttpContext.Current.Request["method"] == "allproject")
            {
                JObject o = JObject.Parse(strReq);
                int PageIndex = Convert.ToInt16((string)o["pageindex"]);
                JObject rss = new JObject();
                rss = new JObject(
                    new JProperty("method", "allproject")
                    );
                List<Project> ProjectList = BLL.Project.GetAllProjectPage(PageIndex);
                JObject JProject =
                    new JObject(
                    new JProperty("Project",
                    new JArray(
                                from p in ProjectList
                                orderby p.SendingDate
                                select new JObject(
                                from System.Reflection.PropertyInfo ProjectProperty in p.GetType().GetProperties()
                                select new JProperty(
                                    ProjectProperty.Name, ProjectProperty.GetValue(p)
                                    )
                                )
                                )
                                )
                                );
                rss.Merge(JProject, new JsonMergeSettings
                {
                    MergeArrayHandling = MergeArrayHandling.Concat
                }
                            );
                context.Response.ContentType = "text/plain";
                context.Response.Write(rss.ToString());
            }
            #endregion Project
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