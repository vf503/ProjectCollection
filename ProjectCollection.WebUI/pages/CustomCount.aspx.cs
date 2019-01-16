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
    public partial class CustomCount : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void UpdataBtn_Click(object sender, EventArgs e)
        {
            UpdataBtn.Enabled = false;
            StateMessage.Text = "正在准备数据";
            #region Customer
            string url = @"http://newpms.cei.cn/customer/";
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
            List<JObject> CustomerList = JsonConvert.DeserializeObject<List<JObject>>(retString);
            using (var ProjectModel = new ProjectCollection.WebUI.Models.ProjectCollectionEntities())
            {
                foreach (JObject jc in CustomerList)
                {
                    string cid = jc["id"].ToString();
                    int count = (from c in ProjectModel.TempCustomer
                                 where c.id == cid
                                 select c).Count();
                    if (count == 0)
                    {
                        Models.TempCustomer ThisCustomer = new Models.TempCustomer();
                        ThisCustomer.id = jc["id"].ToString();
                        ThisCustomer.name= jc["name"].ToString();
                        ThisCustomer.sort= jc["sort"].ToString();
                        ThisCustomer.area = jc["area"].ToString();
                        ProjectModel.TempCustomer.Add(ThisCustomer);
                    }
                    else
                    {
                        Models.TempCustomer ThisCustomer = (from c in ProjectModel.TempCustomer
                                                                                    where c.id == cid
                                                                                    select c).First();
                        ThisCustomer.name = jc["name"].ToString();
                        ThisCustomer.sort = jc["sort"].ToString();
                        ThisCustomer.area = jc["area"].ToString();
                    }
                    ProjectModel.SaveChanges();
                }
            }
            #endregion Customer
            #region course order   
            DateTime QueryStartTime = DateTime.Now.AddMonths(-18);
            using (var ProjectModel = new ProjectCollection.WebUI.Models.ProjectCollectionEntities())
            {
                List<Models.BatchProject> OrderList = (from o in ProjectModel.BatchProject
                                                          where o.CreateDate > QueryStartTime
                                                          select o).ToList();
                foreach (Models.BatchProject o in OrderList)
                {
                    List<JObject> CourseList = JsonConvert.DeserializeObject<List<JObject>>(o.CourseData.ToString());
                    String CustomerId = o.customer;
                    foreach (JObject jc in CourseList)
                    {
                        //course
                        string cid = jc["CourseId"].ToString();
                        int count = (from c in ProjectModel.TempCourse
                                     where c.CourseId == cid
                                     select c).Count();
                        if (count == 0)
                        {
                            Models.TempCourse ThisCourse = new Models.TempCourse();
                            ThisCourse.CourseId = jc["CourseId"].ToString();
                            ThisCourse.title = jc["title"].ToString();
                            ThisCourse.CreateDate = Convert.ToDateTime(jc["CreateDate"].ToString());
                            ThisCourse.GroupName = jc["GroupName"].ToString();
                            ThisCourse.type = jc["type"].ToString();
                            ThisCourse.TempletType = jc["TempletType"].ToString();
                            ThisCourse.lecturer_name = jc["lecturer_name"].ToString();
                            ThisCourse.lecturer_post = jc["lecturer_post"].ToString();
                            ThisCourse.InternalCategoryTop = jc["InternalCategoryTop"].ToString();
                            ThisCourse.InternalCategory = jc["InternalCategory"].ToString();
                            if (ThisCourse.type == "自筹组织")
                            { ThisCourse.type = "自筹"; }
                            if (ThisCourse.InternalCategoryTop == "文化素养")
                            {
                                ThisCourse.InternalCategoryTop = "文化修养";
                            }
                            ProjectModel.TempCourse.Add(ThisCourse);
                        }
                        else
                        {
                            Models.TempCourse ThisCourse = (from c in ProjectModel.TempCourse
                                                            where c.CourseId == cid
                                                            select c).First();
                            ThisCourse.title = jc["title"].ToString();
                            ThisCourse.CreateDate = Convert.ToDateTime(jc["CreateDate"].ToString());
                            ThisCourse.GroupName = jc["GroupName"].ToString();
                            ThisCourse.type = jc["type"].ToString();
                            ThisCourse.TempletType = jc["TempletType"].ToString();
                            ThisCourse.lecturer_name = jc["lecturer_name"].ToString();
                            ThisCourse.lecturer_post = jc["lecturer_post"].ToString();
                            ThisCourse.InternalCategoryTop = jc["InternalCategoryTop"].ToString();
                            ThisCourse.InternalCategory = jc["InternalCategory"].ToString();
                            if (ThisCourse.type == "自筹组织")
                            { ThisCourse.type = "自筹"; }
                            if (ThisCourse.InternalCategoryTop == "文化素养")
                            {
                                ThisCourse.InternalCategoryTop = "文化修养";
                            }
                        }
                        ProjectModel.SaveChanges();
                        //order
                        int OrderCount = (from to in ProjectModel.TempOrder
                                          where (to.CourseId == cid) && (to.CustomerId == CustomerId)
                                          select to).Count();
                        if (OrderCount == 0)
                        {
                            Models.TempOrder ThisOrder = new Models.TempOrder();
                            ThisOrder.CourseId = cid;
                            ThisOrder.CustomerId = CustomerId;
                            ThisOrder.date = Convert.ToDateTime(o.CreateDate);
                            ThisOrder.SellerId = o.user_info.login_name;
                            ProjectModel.TempOrder.Add(ThisOrder);
                            ProjectModel.SaveChanges();
                        }
                        else
                        {
                        }
                    }
                }
            }
            #endregion course order   
            StateMessage.Text = "数据更新完成";
        }

        protected void CustomerQueryBtn_Click(object sender, EventArgs e)
        {
            CustomerPanel.Visible = true;
            DateTime StratTime;
            DateTime EndTime;
            if (hidStartDate.Value == "" || hidEndDate.Value == "")
            {
                EndTime = DateTime.Now;
                StratTime = DateTime.Now.AddMonths(-6);
            }
            else
            {
                StratTime = Convert.ToDateTime(hidStartDate.Value);
                EndTime = Convert.ToDateTime(hidEndDate.Value);
            }
            using (var ProjectModel = new ProjectCollection.WebUI.Models.ProjectCollectionEntities())
            {
                List<Models.TempCustomer> NCustomerList = (from o in ProjectModel.TempOrder
                                                           where (o.date >= StratTime) &&(o.date <= EndTime) &&(o.TempCustomer.area=="n")
                                                           orderby o.TempCustomer.sort
                                                           select o.TempCustomer).Distinct().ToList();
                NCustomerTable.DataSource = NCustomerList;
                NCustomerTable.DataBind();
                NCustomerCount.Text = "北方合计："+ NCustomerList.Count();
                var NTypeCount = from c in NCustomerList
                                 group c by new { c.sort } into tc
                                 select new {
                                     type = tc.Key.sort,count = tc.Count()
                                 };
                NCustomerTypeTable.DataSource = NTypeCount.ToList();
                NCustomerTypeTable.DataBind();
                //
                List<Models.TempCustomer> SCustomerList = (from o in ProjectModel.TempOrder
                                                           where (o.date >= StratTime) && (o.date <= EndTime) && (o.TempCustomer.area == "s")
                                                           orderby o.TempCustomer.sort
                                                           select o.TempCustomer).Distinct().ToList();
                SCustomerTable.DataSource = SCustomerList;
                SCustomerTable.DataBind();
                SCustomerCount.Text = "南方合计：" + SCustomerList.Count();
                var STypeCount = from c in SCustomerList
                                 group c by new { c.sort } into tc
                                 select new
                                 {
                                     type = tc.Key.sort,
                                     count = tc.Count()
                                 };
                SCustomerTypeTable.DataSource = STypeCount.ToList();
                SCustomerTypeTable.DataBind();
            }
        }

        protected void CourseSourceBtn_Click(object sender, EventArgs e)
        {
            CourseSourcePanel.Visible = true;
            DateTime StratTime;
            DateTime EndTime;
            if (hidStartDate.Value == "" || hidEndDate.Value == "")
            {
                EndTime = DateTime.Now;
                StratTime = DateTime.Now.AddMonths(-6);
            }
            else
            {
                StratTime = Convert.ToDateTime(hidStartDate.Value);
                EndTime = Convert.ToDateTime(hidEndDate.Value);
            }
            string CurrentYear = EndTime.Year.ToString();
            CurrentYear = CurrentYear + "-01-01 00:00:00";
            DateTime CurrentYearDate = Convert.ToDateTime(CurrentYear);
            using (var ProjectModel = new ProjectCollection.WebUI.Models.ProjectCollectionEntities())
            {
                //All
                List<Models.TempOrder> OrderList = (from o in ProjectModel.TempOrder
                                                    where (o.date >= StratTime) && (o.date <= EndTime) &&(o.TempCourse.type != "")
                                                    select o).ToList();
                int NumberOfTimes = OrderList.Count();
                NumberOfTimesLB.Text = NumberOfTimesLB.Text + NumberOfTimes.ToString();
                var NumberOfCoursesList = (from o in ProjectModel.TempOrder
                                          where (o.date >= StratTime) && (o.date <= EndTime) && (o.TempCourse.type != "")
                                           group o by new { o.TempCourse.CourseId } into oc
                                          select oc.Key.CourseId);
                NumberOfCoursesLB.Text = NumberOfCoursesLB.Text + NumberOfCoursesList.Count();
                var SourceTimesList = from o in OrderList
                                      where o.TempCourse.type !=""
                                      group o by new { o.TempCourse.type } into oc
                                      orderby oc.Count() descending
                                      select new { type = oc.Key.type, count = oc.Count(), ratio= ((oc.Count() / Convert.ToDouble(NumberOfTimes))*100).ToString("f2") + "%" };
                NumberOfTimesTable.DataSource = SourceTimesList.ToList();
                NumberOfTimesTable.DataBind();
                var SourceCoursesList = from c in ProjectModel.TempCourse.ToList()
                                        where NumberOfCoursesList.ToList().Contains(c.CourseId) && (c.type != "")
                                        group c by new { c.type } into sc
                                        orderby sc.Count() descending
                                        select new { type = sc.Key.type, count = sc.Count(), ratio = (sc.Count() / (Convert.ToDouble(NumberOfCoursesList.Count()))*100).ToString("f2") + "%" };
                NumberOfCoursesTable.DataSource = SourceCoursesList.ToList();
                NumberOfCoursesTable.DataBind();
                //Current Year
                List<Models.TempOrder> OrderCurrentYearList = (from o in ProjectModel.TempOrder
                                                    where (o.date >= StratTime) && (o.date <= EndTime) && (o.TempCourse.type != "") && (o.TempCourse.CreateDate >= CurrentYearDate)
                                                    select o).ToList();
                int NumberOfTimesCurrentYear = OrderCurrentYearList.Count();
                NumberOfTimesCurrentYearLB.Text = NumberOfTimesCurrentYearLB.Text + NumberOfTimesCurrentYear.ToString();
                var NumberOfCoursesCurrentYearList = (from o in ProjectModel.TempOrder
                                                      where (o.date >= StratTime) && (o.date <= EndTime) && (o.TempCourse.type != "") && (o.TempCourse.CreateDate >= CurrentYearDate)
                                                      group o by new { o.TempCourse.CourseId } into oc
                                                      select oc.Key.CourseId);
                NumberOfCoursesCurrentYearLB.Text = NumberOfCoursesCurrentYearLB.Text + NumberOfCoursesCurrentYearList.Count();
                var SourceTimesCurrentYearList = from o in OrderCurrentYearList
                                                 where o.TempCourse.type != ""
                                                 group o by new { o.TempCourse.type } into oc
                                                 orderby oc.Count() descending
                                                 select new { type = oc.Key.type, count = oc.Count(), ratio = ((oc.Count() / Convert.ToDouble(NumberOfTimesCurrentYear)) * 100).ToString("f2") + "%" };
                NumberOfTimesCurrentYearTable.DataSource = SourceTimesCurrentYearList.ToList();
                NumberOfTimesCurrentYearTable.DataBind();
                var SourceCoursesCurrentYearList = from c in ProjectModel.TempCourse.ToList()
                                        where NumberOfCoursesCurrentYearList.ToList().Contains(c.CourseId) && (c.type != "")
                                        group c by new { c.type } into sc
                                        orderby sc.Count() descending
                                        select new { type = sc.Key.type, count = sc.Count(), ratio = (sc.Count() / (Convert.ToDouble(NumberOfCoursesCurrentYearList.Count())) * 100).ToString("f2") + "%" };
                NumberOfCoursesCurrentYearTable.DataSource = SourceCoursesCurrentYearList.ToList();
                NumberOfCoursesCurrentYearTable.DataBind();
            }
        }

        protected void CourseCategoryBtn_Click(object sender, EventArgs e)
        {
            CourseCategoryPanel.Visible = true;
            DateTime StratTime;
            DateTime EndTime;
            if (hidStartDate.Value == "" || hidEndDate.Value == "")
            {
                EndTime = DateTime.Now;
                StratTime = DateTime.Now.AddMonths(-6);
            }
            else
            {
                StratTime = Convert.ToDateTime(hidStartDate.Value);
                EndTime = Convert.ToDateTime(hidEndDate.Value);
            }
            string CurrentYear = EndTime.Year.ToString();
            CurrentYear = CurrentYear + "-01-01 00:00:00";
            DateTime CurrentYearDate = Convert.ToDateTime(CurrentYear);
            using (var ProjectModel = new ProjectCollection.WebUI.Models.ProjectCollectionEntities())
            {
                //All
                List<Models.TempOrder> OrderList = (from o in ProjectModel.TempOrder
                                                    where (o.date >= StratTime) && (o.date <= EndTime) && (o.TempCourse.InternalCategoryTop != "")
                                                    select o).ToList();
                int NumberOfTimes = OrderList.Count();
                NumberOfTimesCategoryLB.Text = NumberOfTimesCategoryLB.Text + NumberOfTimes.ToString();
                var NumberOfCoursesList = (from o in ProjectModel.TempOrder
                                           where (o.date >= StratTime) && (o.date <= EndTime) && (o.TempCourse.InternalCategoryTop != "")
                                           group o by new { o.TempCourse.CourseId } into oc
                                           select oc.Key.CourseId);
                NumberOfCoursesCategoryLB.Text = NumberOfCoursesCategoryLB.Text + NumberOfCoursesList.Count();
                var SourceTimesList = from o in OrderList
                                      where o.TempCourse.InternalCategoryTop != ""
                                      group o by new { o.TempCourse.InternalCategoryTop } into oc
                                      orderby oc.Count() descending
                                      select new { type = oc.Key.InternalCategoryTop, count = oc.Count(), ratio = ((oc.Count() / Convert.ToDouble(NumberOfTimes)) * 100).ToString("f2") + "%" };
                NumberOfTimesCategoryTable.DataSource = SourceTimesList.ToList();
                NumberOfTimesCategoryTable.DataBind();
                var SourceCoursesList = from c in ProjectModel.TempCourse.ToList()
                                        where NumberOfCoursesList.ToList().Contains(c.CourseId) && (c.InternalCategoryTop != "")
                                        group c by new { c.InternalCategoryTop } into sc
                                        orderby sc.Count() descending
                                        select new { type = sc.Key.InternalCategoryTop, count = sc.Count(), ratio = (sc.Count() / (Convert.ToDouble(NumberOfCoursesList.Count())) * 100).ToString("f2") + "%" };
                NumberOfCoursesCategoryTable.DataSource = SourceCoursesList.ToList();
                NumberOfCoursesCategoryTable.DataBind();
                //Current Year
                List<Models.TempOrder> OrderCurrentYearList = (from o in ProjectModel.TempOrder
                                                               where (o.date >= StratTime) && (o.date <= EndTime) && (o.TempCourse.InternalCategoryTop != "") && (o.TempCourse.CreateDate >= CurrentYearDate)
                                                               select o).ToList();
                int NumberOfTimesCurrentYear = OrderCurrentYearList.Count();
                NumberOfTimesCategoryCurrentYearLB.Text = NumberOfTimesCategoryCurrentYearLB.Text + NumberOfTimesCurrentYear.ToString();
                var NumberOfCoursesCurrentYearList = (from o in ProjectModel.TempOrder
                                                      where (o.date >= StratTime) && (o.date <= EndTime) && (o.TempCourse.InternalCategoryTop != "") && (o.TempCourse.CreateDate >= CurrentYearDate)
                                                      group o by new { o.TempCourse.CourseId } into oc
                                                      select oc.Key.CourseId);
                NumberOfCoursesCategoryCurrentYearLB.Text = NumberOfCoursesCategoryCurrentYearLB.Text + NumberOfCoursesCurrentYearList.Count();
                var SourceTimesCurrentYearList = from o in OrderCurrentYearList
                                                 where o.TempCourse.InternalCategoryTop != ""
                                                 group o by new { o.TempCourse.InternalCategoryTop } into oc
                                                 orderby oc.Count() descending
                                                 select new { type = oc.Key.InternalCategoryTop, count = oc.Count(), ratio = ((oc.Count() / Convert.ToDouble(NumberOfTimesCurrentYear)) * 100).ToString("f2") + "%" };
                NumberOfTimesCategoryCurrentYearTable.DataSource = SourceTimesCurrentYearList.ToList();
                NumberOfTimesCategoryCurrentYearTable.DataBind();
                var SourceCoursesCurrentYearList = from c in ProjectModel.TempCourse.ToList()
                                                   where NumberOfCoursesCurrentYearList.ToList().Contains(c.CourseId) && (c.InternalCategoryTop != "")
                                                   group c by new { c.InternalCategoryTop } into sc
                                                   orderby sc.Count() descending
                                                   select new { type = sc.Key.InternalCategoryTop, count = sc.Count(), ratio = (sc.Count() / (Convert.ToDouble(NumberOfCoursesCurrentYearList.Count())) * 100).ToString("f2") + "%" };
                NumberOfCoursesCategoryCurrentYearTable.DataSource = SourceCoursesCurrentYearList.ToList();
                NumberOfCoursesCategoryCurrentYearTable.DataBind();
            }
        }
    }
}