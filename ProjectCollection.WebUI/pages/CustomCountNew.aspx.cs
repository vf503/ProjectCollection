using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProjectCollection.WebUI.pages
{
    public partial class CustomCountNew : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string host = HttpContext.Current.Request.Url.Host+":"+ HttpContext.Current.Request.Url.Port;
            string YearCount = HttpGet("http://"+host+"/InterFace/custom.ashx?method=YearProjectCount&year=2019", "application/json");
            JObject YearCountJ = JsonConvert.DeserializeObject<JObject>(YearCount);
            StateMessage.Text = "2019年自筹类共制作 "+ YearCountJ["all"].ToString() + " 门课程； "+ YearCountJ["fin"].ToString() + " 门已完成";
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            //string host = HttpContext.Current.Request.Url.Host + ":" + HttpContext.Current.Request.Url.Port;
            //string CourseTimes = HttpGet("http://" + host + "/InterFace/custom.ashx?method=CourseTimesList&start="+ hidStartDate.Value+ "&end="+hidEndDate.Value, "application/json");
            //JArray CourseTimesJ = JsonConvert.DeserializeObject<JArray>(CourseTimes);
            DateTime StartTime = Convert.ToDateTime(hidStartDate.Value);
            DateTime EndTime = Convert.ToDateTime(hidEndDate.Value);
            //string area = DDarea.SelectedValue;
            var ProjectModel = new ProjectCollection.WebUI.Models.ProjectCollectionEntities();
            var AllOrder = (from o in ProjectModel.TempOrder
                            join c in ProjectModel.TempCourse on o.CourseId equals c.CourseId
                            where (c.type == "自筹") && (!string.IsNullOrEmpty(c.SourceCourseId))
                            && (c.SourceCourseId != "") && (c.CreateDate >= StartTime) && (c.CreateDate <= EndTime) 
                            //&& o.TempCustomer.area == area
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
                             orderby oc.Key.count descending
                             select new
                             {
                                 订购次数 = oc.Key.count,
                                 课件数量 = oc.Count()
                                 
                             };
            GridViewTop.DataSource = OrderGroup.ToList();
            GridViewTop.DataBind();

        }
        protected void GridViewTop_SelectedIndexChanged(object sender, EventArgs e)
        {
            int count =  Convert.ToInt32(GridViewTop.SelectedRow.Cells[0].Text);
            DateTime StartTime = Convert.ToDateTime(hidStartDate.Value);
            DateTime EndTime = Convert.ToDateTime(hidEndDate.Value);
            var ProjectModel = new ProjectCollection.WebUI.Models.ProjectCollectionEntities();
            ProjectModel.Database.CommandTimeout = 600000;
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
                             where oc.Count() == count
                             select new
                             {
                                 //id = oc.Key.sourceid,
                                 title =(from c in ProjectModel.TempCourse where c.SourceCourseId == oc.Key.sourceid select new {title = c.title }).FirstOrDefault().title,
                                 count = oc.Count()
                             };
            GridViewDetails.DataSource = OrderCount.ToList();
            GridViewDetails.DataBind();
        }
        //
        //contentType application/json or application/xml
        public string HttpGet(string Url, string contentType)
        {
            try
            {
                string retString = string.Empty;

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url);
                request.Method = "GET";
                request.ContentType = contentType;

                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                Stream myResponseStream = response.GetResponseStream();
                StreamReader streamReader = new StreamReader(myResponseStream);
                retString = streamReader.ReadToEnd();
                streamReader.Close();
                myResponseStream.Close();
                return retString;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static string HttpPost(string Url, string postDataStr, string contentType, out bool isOK)
        {
            string retString = string.Empty;

            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url);
                request.Method = "POST";
                request.ContentType = contentType;
                request.Timeout = 600000;//设置超时时间
                request.ContentLength = Encoding.UTF8.GetByteCount(postDataStr);
                Stream requestStream = request.GetRequestStream();
                StreamWriter streamWriter = new StreamWriter(requestStream);
                streamWriter.Write(postDataStr);
                streamWriter.Close();

                HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                Stream responseStream = response.GetResponseStream();
                StreamReader streamReader = new StreamReader(responseStream);
                retString = streamReader.ReadToEnd();
                streamReader.Close();
                responseStream.Close();

                isOK = true;
            }
            catch (Exception ex)
            {
                if (ex.GetType() == typeof(WebException))//捕获400错误
                {
                    var response = ((WebException)ex).Response;
                    Stream responseStream = response.GetResponseStream();
                    StreamReader streamReader = new StreamReader(responseStream);
                    retString = streamReader.ReadToEnd();
                    streamReader.Close();
                    responseStream.Close();
                }
                else
                {
                    retString = ex.ToString();
                }
                isOK = false;
            }

            return retString;
        }
    }
}