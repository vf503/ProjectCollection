using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ProjectCollection.WebUI.pages.common;
using System;
using System.Collections.Generic;
using System.Data;
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
                    if (CourseList is null) { continue; }
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
                            if (ThisCourse.type == "b")
                            { ThisCourse.type = "外出拍摄"; }
                            if (ThisCourse.type == "s")
                            { ThisCourse.type = "单改三"; }
                            if (ThisCourse.type == "NorthOnly")
                            { ThisCourse.type = "客户项目"; }
                            if (ThisCourse.InternalCategoryTop == "文化素养")
                            {
                                ThisCourse.InternalCategoryTop = "文化修养";
                            }
                            if (jc["SourceCourseId"] is null)
                            { }
                            else
                            {
                                ThisCourse.SourceCourseId = jc["SourceCourseId"].ToString();
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
                            if (ThisCourse.type == "b")
                            { ThisCourse.type = "外出拍摄"; }
                            if (ThisCourse.type == "s")
                            { ThisCourse.type = "单改三"; }
                            if (ThisCourse.type == "NorthOnly")
                            { ThisCourse.type = "客户项目"; }
                            if (ThisCourse.InternalCategoryTop == "文化素养")
                            {
                                ThisCourse.InternalCategoryTop = "文化修养";
                            }
                            if (jc["SourceCourseId"] is null)
                            { }
                            else
                            {
                                ThisCourse.SourceCourseId = jc["SourceCourseId"].ToString();
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
            DateTime StartTime;
            DateTime EndTime;
            if (hidStartDate.Value == "" || hidEndDate.Value == "")
            {
                EndTime = DateTime.Now;
                StartTime = DateTime.Now.AddMonths(-6);
            }
            else
            {
                StartTime = Convert.ToDateTime(hidStartDate.Value);
                EndTime = Convert.ToDateTime(hidEndDate.Value);
            }
            var ProjectModel = new ProjectCollection.WebUI.Models.ProjectCollectionEntities();
            List<Models.TempCustomer> NCustomerList = (from o in ProjectModel.TempOrder
                                                       where (o.date >= StartTime) && (o.date <= EndTime) && (o.TempCustomer.area == "n")
                                                       orderby o.TempCustomer.sort
                                                       select o.TempCustomer).Distinct().ToList();
            NCustomerTable.DataSource = NCustomerList;
            NCustomerTable.DataBind();
            NCustomerCount.Text = "北方合计：" + NCustomerList.Count();
            var NTypeCount = from c in NCustomerList
                             group c by new { c.sort } into tc
                             select new
                             {
                                 type = tc.Key.sort,
                                 count = tc.Count()
                             };
            NCustomerTypeTable.DataSource = NTypeCount.ToList();
            NCustomerTypeTable.DataBind();
            //
            List<Models.TempCustomer> SCustomerList = (from o in ProjectModel.TempOrder
                                                       where (o.date >= StartTime) && (o.date <= EndTime) && (o.TempCustomer.area == "s")
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
            #region
            string CurrentYear = EndTime.Year.ToString();
            CurrentYear = CurrentYear + "-01-01 00:00:00";
            DateTime CurrentYearDate = Convert.ToDateTime(CurrentYear);
            List<Models.TempOrder> OrderList = (from o in ProjectModel.TempOrder
                                                where (o.date >= StartTime) && (o.date <= EndTime) && (o.TempCourse.InternalCategory != "")
                                                select o).ToList();
            var CustomerCategoryTable = (from o in OrderList
                                         group o by new { o.TempCourse.InternalCategory, o.TempCustomer.sort } into oc
                                         select new {
                                             category = (from c in ProjectModel.TempCourse
                                                         where c.InternalCategory == oc.Key.InternalCategory
                                                         select c.InternalCategoryTop).First()+"-"+oc.Key.InternalCategory,
                                             CustomerSort = oc.Key.sort,
                                             count = oc.Count()
                                        }).OrderBy(i => i.category).ToList();
            List<string> CustomerSortList = (from c in ProjectModel.TempCustomer
                                             group c by new { c.sort } into oc
                                             select oc.Key.sort).ToList();
            List<string> CategoryList = (from c in CustomerCategoryTable
                                         group c by new { c.category } into oc
                                             select oc.Key.category).ToList();
            DataTable CountData = new DataTable("");
            DataColumn dc = null;
            dc = CountData.Columns.Add("name", Type.GetType("System.String"));
            //GridView
            BoundField nameColumn = new BoundField();
            nameColumn.HeaderText = "表头";
            nameColumn.DataField = "name";
            CustomerCategoryGV.Columns.Add(nameColumn);
            foreach (string s in CustomerSortList)
            {
                dc = CountData.Columns.Add(s, Type.GetType("System.String"));
                //GridView
                BoundField ThisColumn = new BoundField();
                ThisColumn.HeaderText = s;
                ThisColumn.DataField = s;
                CustomerCategoryGV.Columns.Add(ThisColumn);
            }
            DataRow newRow;
            foreach(string c in CategoryList)
            {
                newRow = CountData.NewRow();
                newRow["name"] = c;
                foreach (string s in CustomerSortList)
                {
                    var count = (from d in CustomerCategoryTable
                                 where (d.CustomerSort == s) && (d.category == c)
                                 select d.count);
                    if (count.Count() > 0)
                    {
                        newRow[s] = (from d in CustomerCategoryTable
                                     where (d.CustomerSort == s) && (d.category == c)
                                     select d.count).First();
                    }
                    else { newRow[s] = 0; }
                }
                CountData.Rows.Add(newRow);
            }
            CustomerCategoryGV.DataSource = CountData;
            CustomerCategoryGV.DataBind();
            #endregion
        }

        protected void CourseSourceBtn_Click(object sender, EventArgs e)
        {
            CourseSourcePanel.Visible = true;
            DateTime StartTime;
            DateTime EndTime;
            if (hidStartDate.Value == "" || hidEndDate.Value == "")
            {
                EndTime = DateTime.Now;
                StartTime = DateTime.Now.AddMonths(-6);
            }
            else
            {
                StartTime = Convert.ToDateTime(hidStartDate.Value);
                EndTime = Convert.ToDateTime(hidEndDate.Value);
            }
            string CurrentYear = EndTime.Year.ToString();
            CurrentYear = CurrentYear + "-01-01 00:00:00";
            DateTime CurrentYearDate = Convert.ToDateTime(CurrentYear);
            var ProjectModel = new ProjectCollection.WebUI.Models.ProjectCollectionEntities();
            #region Source All
            List<Models.TempOrder> OrderList = (from o in ProjectModel.TempOrder
                                                where (o.date >= StartTime) && (o.date <= EndTime) && (o.TempCourse.type != "")
                                                select o).ToList();
            int NumberOfTimes = OrderList.Count();
            NumberOfTimesLB.Text = NumberOfTimesLB.Text + NumberOfTimes.ToString();
            var NumberOfCoursesList = (from o in ProjectModel.TempOrder
                                       where (o.date >= StartTime) && (o.date <= EndTime) && (o.TempCourse.type != "")
                                       group o by new { o.TempCourse.CourseId } into oc
                                       select oc.Key.CourseId);
            NumberOfCoursesLB.Text = NumberOfCoursesLB.Text + NumberOfCoursesList.Count();
            RateLB.Text = RateLB.Text + (NumberOfTimes / Convert.ToDouble(NumberOfCoursesList.Count())).ToString("f2");
            var SourceTimesList = from o in OrderList
                                  where o.TempCourse.type != ""
                                  group o by new { o.TempCourse.type } into oc
                                  orderby oc.Count() descending
                                  select new { SourceName = oc.Key.type,
                                      TimesCount = oc.Count(),
                                      TimesRatio = ((oc.Count() / Convert.ToDouble(NumberOfTimes)) * 100).ToString("f2") + "%" };
            var SourceCoursesList = from c in ProjectModel.TempCourse.ToList()
                                    where NumberOfCoursesList.ToList().Contains(c.CourseId) && (c.type != "")
                                    group c by new { c.type } into sc
                                    orderby sc.Count() descending
                                    select new { SourceName = sc.Key.type,
                                        CoursesCount = sc.Count(),
                                        CoursesRatio = (sc.Count() / (Convert.ToDouble(NumberOfCoursesList.Count())) * 100).ToString("f2") + "%" };
            var NumberOfCourseGroupList = (from o in OrderList
                                           where o.TempCourse.SourceCourseId != null
                                           select o).DistinctBy(o => o.TempCourse.SourceCourseId);
            var SourceCourseGroupList = from g in NumberOfCourseGroupList
                                        group g by new { g.TempCourse.type } into sg
                                        select new { SourceName = sg.Key.type,
                                            GroupCount = sg.Count().ToString(),
                                            GroupRatio = (sg.Count() / (Convert.ToDouble(NumberOfCourseGroupList.Count())) * 100).ToString("f2") + "%" };
            var SourceCount = (from t in SourceTimesList
                               join c in SourceCoursesList on t.SourceName equals c.SourceName into TC
                               from tc in TC.DefaultIfEmpty()
                               join g in SourceCourseGroupList on t.SourceName equals g.SourceName into TCG
                               from tcg in TCG.DefaultIfEmpty()
                               orderby t.TimesCount descending
                               select new
                               {
                                   SourceName = t.SourceName,
                                   times = t.TimesCount,
                                   TimesRatio = t.TimesRatio,
                                   number = tc.CoursesCount,
                                   NumberRatio = tc.CoursesRatio,
                                   GroupTimes = tcg != null ? tcg.GroupCount : "无",
                                   GroupTimesRatio = tcg != null ? tcg.GroupRatio : "无",
                                   TimesAVG = (t.TimesCount / Convert.ToDouble(tc.CoursesCount)).ToString("f2")
                               }).ToList();
            SourceTable.DataSource = SourceCount;
            SourceTable.DataBind();
            #endregion Source All
            #region Source Current Year
            List<Models.TempOrder> OrderCurrentYearList = (from o in ProjectModel.TempOrder
                                                           where (o.date >= StartTime) && (o.date <= EndTime) && (o.TempCourse.type != "") && (o.TempCourse.CreateDate >= CurrentYearDate)
                                                           select o).ToList();
            int NumberOfTimesCurrentYear = OrderCurrentYearList.Count();
            NumberOfTimesCurrentYearLB.Text = NumberOfTimesCurrentYearLB.Text + NumberOfTimesCurrentYear.ToString();
            var NumberOfCoursesCurrentYearList = (from o in ProjectModel.TempOrder
                                                  where (o.date >= StartTime) && (o.date <= EndTime) && (o.TempCourse.type != "") && (o.TempCourse.CreateDate >= CurrentYearDate)
                                                  group o by new { o.TempCourse.CourseId } into oc
                                                  select oc.Key.CourseId);
            NumberOfCoursesCurrentYearLB.Text = NumberOfCoursesCurrentYearLB.Text + NumberOfCoursesCurrentYearList.Count();
            RateCurrentYearLB.Text = RateCurrentYearLB.Text + (NumberOfTimesCurrentYear / Convert.ToDouble(NumberOfCoursesCurrentYearList.Count())).ToString("f2");
            var SourceTimesCurrentYearList = from o in OrderCurrentYearList
                                             where o.TempCourse.type != ""
                                             group o by new { o.TempCourse.type } into oc
                                             orderby oc.Count() descending
                                             select new { SourceName = oc.Key.type, TimesCount = oc.Count(), TimesRatio = ((oc.Count() / Convert.ToDouble(NumberOfTimesCurrentYear)) * 100).ToString("f2") + "%" };
            var SourceCoursesCurrentYearList = from c in ProjectModel.TempCourse.ToList()
                                               where NumberOfCoursesCurrentYearList.ToList().Contains(c.CourseId) && (c.type != "")
                                               group c by new { c.type } into sc
                                               orderby sc.Count() descending
                                               select new { SourceName = sc.Key.type, CoursesCount = sc.Count(), CoursesRatio = (sc.Count() / (Convert.ToDouble(NumberOfCoursesCurrentYearList.Count())) * 100).ToString("f2") + "%" };
            var NumberOfCourseGroupCurrentYearList = (from o in OrderList
                                                      where o.TempCourse.SourceCourseId != null
                                                      select o).DistinctBy(o => o.TempCourse.SourceCourseId);
            var SourceCourseGroupCurrentYearList = from g in NumberOfCourseGroupCurrentYearList
                                                   group g by new { g.TempCourse.type } into sg
                                                   select new { SourceName = sg.Key.type, GroupCount = sg.Count().ToString(), GroupRatio = (sg.Count() / (Convert.ToDouble(NumberOfCourseGroupCurrentYearList.Count())) * 100).ToString("f2") + "%" };
            var SourceCountCurrentYear = from t in SourceTimesCurrentYearList
                                         join c in SourceCoursesCurrentYearList on t.SourceName equals c.SourceName into TC
                                         from tc in TC.DefaultIfEmpty()
                                         join g in SourceCourseGroupCurrentYearList on t.SourceName equals g.SourceName into TCG
                                         from tcg in TCG.DefaultIfEmpty()
                                         orderby t.TimesCount descending
                                         select new
                                         {
                                             SourceName = t.SourceName,
                                             times = t.TimesCount,
                                             TimesRatio = t.TimesRatio,
                                             number = tc.CoursesCount,
                                             NumberRatio = tc.CoursesRatio,
                                             GroupTimes = tcg != null ? tcg.GroupCount : "无",
                                             GroupTimesRatio = tcg != null ? tcg.GroupRatio : "无",
                                             TimesAVG = (t.TimesCount / Convert.ToDouble(tc.CoursesCount)).ToString("f2")
                                         };
            SourceTableCurrentYear.DataSource = SourceCountCurrentYear.ToList();
            SourceTableCurrentYear.DataBind();
            #endregion Source Current Year
            #region Rate
            DataTable CountData = new DataTable("");
            DataColumn dc = null;
            dc = CountData.Columns.Add("type", Type.GetType("System.String"));
            dc = CountData.Columns.Add("selled", Type.GetType("System.String"));
            dc = CountData.Columns.Add("produce", Type.GetType("System.String"));
            dc = CountData.Columns.Add("rate", Type.GetType("System.String"));
            DataRow newRow;
            //
            int NewA = (from p in ProjectModel.Project
                        where (p.SendingDate >= StartTime) && (p.SendingDate <= EndTime) && (p.PublishPublishDate >= CurrentYearDate) && p.ProjectNo.StartsWith("A")
                        select p).Count();
            int NewB = (from p in ProjectModel.Project
                        where (p.SendingDate >= StartTime) && (p.SendingDate <= EndTime) && (p.PublishPublishDate >= CurrentYearDate) && p.ProjectNo.StartsWith("B")
                        select p).Count();
            int NewC = (from p in ProjectModel.Project
                        where (p.SendingDate >= StartTime) && (p.SendingDate <= EndTime) && (p.PublishPublishDate >= CurrentYearDate) && p.ProjectNo.StartsWith("C")
                        select p).Count();
            int SelledA = Convert.ToInt32((from n in SourceCountCurrentYear
                                           where n.SourceName == "自筹"
                                           select n.GroupTimes).First().ToString());
            int SelledB = Convert.ToInt32((from n in SourceCountCurrentYear
                                           where n.SourceName == "外出拍摄"
                                           select n.number).First().ToString());
            var C = from n in SourceCountCurrentYear
                    where n.SourceName == "其他外购"
                    select n.number;
            int SelledC = 0;
            if (C.Count() > 0)
            {
                SelledC = Convert.ToInt32((from n in SourceCountCurrentYear
                                           where n.SourceName == "其他外购"
                                           select n.number).First().ToString());
            }
            //
            newRow = CountData.NewRow();
            newRow["type"] = "自筹";
            newRow["selled"] = SelledA;
            newRow["produce"] = NewA;
            newRow["rate"] = (SelledA / Convert.ToDouble(NewA) * 100).ToString("f2") + "%";
            CountData.Rows.Add(newRow);
            newRow = CountData.NewRow();
            newRow["type"] = "外出拍摄";
            newRow["selled"] = SelledB;
            newRow["produce"] = NewB;
            newRow["rate"] = (SelledB / Convert.ToDouble(NewB) * 100).ToString("f2") + "%";
            CountData.Rows.Add(newRow);
            newRow = CountData.NewRow();
            newRow["type"] = "其他外购";
            newRow["selled"] = SelledC;
            newRow["produce"] = NewC;
            newRow["rate"] = (SelledC / Convert.ToDouble(NewC) * 100).ToString("f2") + "%";
            CountData.Rows.Add(newRow);
            SourceCreateTable.DataSource = CountData;
            SourceCreateTable.DataBind();
            #endregion Rate
        }

        protected void CourseCategoryBtn_Click(object sender, EventArgs e)
        {
            CourseCategoryPanel.Visible = true;
            DateTime StartTime;
            DateTime EndTime;
            if (hidStartDate.Value == "" || hidEndDate.Value == "")
            {
                EndTime = DateTime.Now;
                StartTime = DateTime.Now.AddMonths(-6);
            }
            else
            {
                StartTime = Convert.ToDateTime(hidStartDate.Value);
                EndTime = Convert.ToDateTime(hidEndDate.Value);
            }
            string CurrentYear = EndTime.Year.ToString();
            CurrentYear = CurrentYear + "-01-01 00:00:00";
            DateTime CurrentYearDate = Convert.ToDateTime(CurrentYear);
            var ProjectModel = new ProjectCollection.WebUI.Models.ProjectCollectionEntities();
            #region All
            List<Models.TempOrder> OrderList = (from o in ProjectModel.TempOrder
                                                where (o.date >= StartTime) && (o.date <= EndTime) && (o.TempCourse.InternalCategory != "")
                                                select o).ToList();
            int NumberOfTimes = OrderList.Count();
            NumberOfTimesCategoryLB.Text = NumberOfTimesCategoryLB.Text + NumberOfTimes.ToString();
            var NumberOfCoursesList = (from o in ProjectModel.TempOrder
                                       where (o.date >= StartTime) && (o.date <= EndTime) && (o.TempCourse.InternalCategory != "")
                                       group o by new { o.TempCourse.CourseId } into oc
                                       select oc.Key.CourseId);
            NumberOfCoursesCategoryLB.Text = NumberOfCoursesCategoryLB.Text + NumberOfCoursesList.Count();
            RateCategoryLB.Text += (NumberOfTimes / Convert.ToDouble(NumberOfCoursesList.Count())).ToString("f2");
            var SourceTimesList = from o in OrderList
                                  where o.TempCourse.InternalCategory != ""
                                  group o by new { o.TempCourse.InternalCategory } into oc
                                  orderby oc.Count() descending
                                  select new
                                  {
                                      type = oc.Key.InternalCategory,
                                      TypeTop = (from c in ProjectModel.TempCourse
                                                 where c.InternalCategory == oc.Key.InternalCategory
                                                 select c.InternalCategoryTop).First(),
                                      count = oc.Count(),
                                      ratio = ((oc.Count() / Convert.ToDouble(NumberOfTimes)) * 100).ToString("f2") + "%"
                                  };
            var SourceCoursesList = from c in ProjectModel.TempCourse.ToList()
                                    where NumberOfCoursesList.ToList().Contains(c.CourseId) && (c.InternalCategory != "")
                                    group c by new { c.InternalCategory } into sc
                                    orderby sc.Count() descending
                                    select new
                                    {
                                        type = sc.Key.InternalCategory,
                                        TypeTop = (from c in ProjectModel.TempCourse
                                                   where c.InternalCategory == sc.Key.InternalCategory
                                                   select c.InternalCategoryTop).First(),
                                        count = sc.Count(),
                                        ratio = (sc.Count() / (Convert.ToDouble(NumberOfCoursesList.Count())) * 100).ToString("f2") + "%"
                                    };
            var NumberOfCourseGroupList = (from o in OrderList
                                           where o.TempCourse.InternalCategory != null
                                           select o).DistinctBy(o => o.TempCourse.SourceCourseId);
            var SourceCourseGroupList = from g in NumberOfCourseGroupList
                                        group g by new { g.TempCourse.InternalCategory } into sg
                                        select new
                                        {
                                            type = sg.Key.InternalCategory,
                                            TypeTop = (from c in ProjectModel.TempCourse
                                                       where c.InternalCategory == sg.Key.InternalCategory
                                                       select c.InternalCategoryTop).First(),
                                            GroupCount = sg.Count().ToString(),
                                            GroupRatio = (sg.Count() / (Convert.ToDouble(NumberOfCourseGroupList.Count())) * 100).ToString("f2") + "%"
                                        };
            var SourceCount = (from t in SourceTimesList
                               join c in SourceCoursesList on t.type equals c.type into TC
                               from tc in TC.DefaultIfEmpty()
                               join g in SourceCourseGroupList on t.type equals g.type into TCG
                               from tcg in TCG.DefaultIfEmpty()
                               orderby t.TypeTop, t.type descending
                               select new
                               {
                                   type = t.type,
                                   TypeTop = t.TypeTop,
                                   times = t.count,
                                   TimesRatio = t.ratio,
                                   number = tc.count,
                                   NumberRatio = tc.ratio,
                                   GroupTimes = tcg != null ? tcg.GroupCount : "无",
                                   GroupTimesRatio = tcg != null ? tcg.GroupRatio : "无",
                                   TimesAVG = (t.count / Convert.ToDouble(tc.count)).ToString("f2")
                               }).ToList();
            NumberOfTimesCategoryTable.DataSource = SourceCount.ToList();
            NumberOfTimesCategoryTable.DataBind();
            #endregion All
            #region Current Year
            List<Models.TempOrder> OrderCurrentYearList = (from o in ProjectModel.TempOrder
                                                           where (o.date >= StartTime) && (o.date <= EndTime) && (o.TempCourse.InternalCategory != "") && (o.TempCourse.CreateDate >= CurrentYearDate)
                                                           select o).ToList();
            int NumberOfTimesCurrentYear = OrderCurrentYearList.Count();
            NumberOfTimesCategoryCurrentYearLB.Text = NumberOfTimesCategoryCurrentYearLB.Text + NumberOfTimesCurrentYear.ToString();
            var NumberOfCoursesCurrentYearList = (from o in ProjectModel.TempOrder
                                                  where (o.date >= StartTime) && (o.date <= EndTime) && (o.TempCourse.InternalCategory != "") && (o.TempCourse.CreateDate >= CurrentYearDate)
                                                  group o by new { o.TempCourse.CourseId } into oc
                                                  select oc.Key.CourseId);
            NumberOfCoursesCategoryCurrentYearLB.Text = NumberOfCoursesCategoryCurrentYearLB.Text + NumberOfCoursesCurrentYearList.Count();
            RateCategoryCurrentYearLB.Text += (NumberOfTimesCurrentYear / Convert.ToDouble(NumberOfCoursesCurrentYearList.Count())).ToString("f2");
            var SourceTimesCurrentYearList = from o in OrderCurrentYearList
                                             where o.TempCourse.InternalCategoryTop != ""
                                             group o by new { o.TempCourse.InternalCategory } into oc
                                             orderby oc.Count() descending
                                             select new
                                             {
                                                 type = oc.Key.InternalCategory,
                                                 count = oc.Count(),
                                                 ratio = ((oc.Count() / Convert.ToDouble(NumberOfTimesCurrentYear)) * 100).ToString("f2") + "%"
                                             };
            var SourceCoursesCurrentYearList = from c in ProjectModel.TempCourse.ToList()
                                               where NumberOfCoursesCurrentYearList.ToList().Contains(c.CourseId) && (c.InternalCategory != "")
                                               group c by new { c.InternalCategory } into sc
                                               orderby sc.Count() descending
                                               select new
                                               {
                                                   type = sc.Key.InternalCategory,
                                                   count = sc.Count(),
                                                   ratio = (sc.Count() / (Convert.ToDouble(NumberOfCoursesCurrentYearList.Count())) * 100).ToString("f2") + "%"
                                               };
            var NumberOfCourseGroupCurrentYearList = (from o in OrderList
                                                      where o.TempCourse.InternalCategory != null
                                                      select o).DistinctBy(o => o.TempCourse.SourceCourseId);
            var SourceCourseGroupCurrentYearList = from g in NumberOfCourseGroupList
                                                   group g by new { g.TempCourse.InternalCategory } into sg
                                                   select new
                                                   {
                                                       type = sg.Key.InternalCategory,
                                                       TypeTop = (from c in ProjectModel.TempCourse
                                                                  where c.InternalCategory == sg.Key.InternalCategory
                                                                  select c.InternalCategoryTop).First(),
                                                       GroupCount = sg.Count().ToString(),
                                                       GroupRatio = (sg.Count() / (Convert.ToDouble(NumberOfCourseGroupList.Count())) * 100).ToString("f2") + "%"
                                                   };
            var SourceCountCurrentYear = (from t in SourceTimesList
                                          join c in SourceCoursesList on t.type equals c.type into TC
                                          from tc in TC.DefaultIfEmpty()
                                          join g in SourceCourseGroupList on t.type equals g.type into TCG
                                          from tcg in TCG.DefaultIfEmpty()
                                          orderby t.TypeTop, t.type descending
                                          select new
                                          {
                                              type = t.type,
                                              TypeTop = t.TypeTop,
                                              times = t.count,
                                              TimesRatio = t.ratio,
                                              number = tc.count,
                                              NumberRatio = tc.ratio,
                                              GroupTimes = tcg != null ? tcg.GroupCount : "无",
                                              GroupTimesRatio = tcg != null ? tcg.GroupRatio : "无",
                                              TimesAVG = (t.count / Convert.ToDouble(tc.count)).ToString("f2")
                                          }).ToList();
            NumberOfTimesCategoryCurrentYearTable.DataSource = SourceCountCurrentYear;
            NumberOfTimesCategoryCurrentYearTable.DataBind();
            #endregion Current Year
            #region cross
            List<Models.TempOrder> OrderListCross = (from o in OrderList
                                                     where o.TempCourse.type != ""
                                                select o).ToList();
            var SourceCategoryTable = (from o in OrderListCross
                                         group o by new { o.TempCourse.InternalCategory, o.TempCourse.type } into oc
                                         select new
                                         {
                                             category = (from c in ProjectModel.TempCourse
                                                         where c.InternalCategory == oc.Key.InternalCategory
                                                         select c.InternalCategoryTop).First() + "-" + oc.Key.InternalCategory,
                                             source = oc.Key.type,
                                             count = oc.Count()
                                         }).OrderBy(i => i.category).ToList();
            List<string> SourceList = (from c in ProjectModel.TempCourse
                                       where c.type !=""
                                             group c by new { c.type } into oc
                                             select oc.Key.type).ToList();
            List<string> CategoryList = (from c in SourceCategoryTable
                                         where c.category != ""
                                         group c by new { c.category } into oc
                                         select oc.Key.category).ToList();
            DataTable CountData = new DataTable("");
            DataColumn dc = null;
            dc = CountData.Columns.Add("name", Type.GetType("System.String"));
            //GridView
            BoundField nameColumn = new BoundField();
            nameColumn.HeaderText = "表头";
            nameColumn.DataField = "name";
            CategorySourceGV.Columns.Add(nameColumn);
            foreach (string s in SourceList)
            {
                dc = CountData.Columns.Add(s, Type.GetType("System.String"));
                //GridView
                BoundField ThisColumn = new BoundField();
                ThisColumn.HeaderText = s;
                ThisColumn.DataField = s;
                CategorySourceGV.Columns.Add(ThisColumn);
            }
            DataRow newRow;
            foreach (string c in CategoryList)
            {
                newRow = CountData.NewRow();
                newRow["name"] = c;
                foreach (string s in SourceList)
                {
                    var count = (from d in SourceCategoryTable
                                 where (d.source == s) && (d.category == c)
                                 select d.count);
                    if (count.Count() > 0)
                    {
                        newRow[s] = (from d in SourceCategoryTable
                                     where (d.source == s) && (d.category == c)
                                     select d.count).First();
                    }
                    else { newRow[s] = 0; }
                }
                CountData.Rows.Add(newRow);
            }
            CategorySourceGV.DataSource = CountData;
            CategorySourceGV.DataBind();
            #endregion cross
        }

        protected void HistoryBtn_Click(object sender, EventArgs e)
        {
            HistoryPanel.Visible = true;
            DateTime StartTime;
            DateTime EndTime;
            if (hidStartDate.Value == "" || hidEndDate.Value == "")
            {
                EndTime = DateTime.Now;
                StartTime = DateTime.Now.AddMonths(-6);
            }
            else
            {
                StartTime = Convert.ToDateTime(hidStartDate.Value);
                EndTime = Convert.ToDateTime(hidEndDate.Value);
            }
            string CurrentYear = EndTime.Year.ToString();
            CurrentYear = CurrentYear + "-01-01 00:00:00";
            DateTime CurrentYearDate = Convert.ToDateTime(CurrentYear);
            DateTime EndYearDate = CurrentYearDate.AddYears(+1);
            DateTime StartYearDate = CurrentYearDate.AddYears(-2);
            var ProjectModel = new ProjectCollection.WebUI.Models.ProjectCollectionEntities();
            #region All
            List<Models.TempOrder> OrderList = (from o in ProjectModel.TempOrder
                                                where (o.date >= StartYearDate) && (o.date <= EndYearDate)
                                                select o).ToList();
            int TotalTimes = OrderList.Count();
            var NumberOfCoursesList = (from o in ProjectModel.TempOrder
                                       where (o.date >= StartYearDate) && (o.date <= EndYearDate)
                                       group o by new { o.TempCourse.CourseId } into oc
                                       select oc.Key.CourseId);
            int TotalCourses= NumberOfCoursesList.Count();
            var NumberOfCourseGroupList = (from o in OrderList
                                           select o).DistinctBy(o => o.TempCourse.SourceCourseId);
            int TotalGroups = NumberOfCourseGroupList.Count();
            DataTable CountData = new DataTable("");
            DataColumn dc = null;
            dc = CountData.Columns.Add("year", Type.GetType("System.String"));
            dc = CountData.Columns.Add("times", Type.GetType("System.String"));
            dc = CountData.Columns.Add("TimesRate", Type.GetType("System.String"));
            dc = CountData.Columns.Add("count", Type.GetType("System.String"));
            dc = CountData.Columns.Add("CountRate", Type.GetType("System.String"));
            dc = CountData.Columns.Add("group", Type.GetType("System.String"));
            dc = CountData.Columns.Add("GroupRate", Type.GetType("System.String"));
            DataRow newRow;
            for (int i = 0; i < 3; i++)
            {
                DateTime StartDate = CurrentYearDate.AddYears(-i);
                DateTime EndDate = CurrentYearDate.AddYears(-(i-1));
                List<Models.TempOrder> ThisOrderList = (from o in ProjectModel.TempOrder
                                                    where (o.date >= StartDate) && (o.date <= EndDate)
                                                    select o).ToList();
                int ThisTimes = ThisOrderList.Count();
                var ThisNumberOfCoursesList = (from o in ProjectModel.TempOrder
                                           where (o.date >= StartDate) && (o.date <= EndDate)
                                           group o by new { o.TempCourse.CourseId } into oc
                                           select oc.Key.CourseId);
                int ThisCourses = ThisNumberOfCoursesList.Count();
                int ThisGroups = 0;
                if (ThisTimes > 0)
                {
                    var ThisNumberOfCourseGroupList = (from o in ThisOrderList
                                                       select o).DistinctBy(o => o.TempCourse.SourceCourseId);
                    ThisGroups = ThisNumberOfCourseGroupList.Count();
                }
                newRow = CountData.NewRow();
                newRow["year"] = StartDate.Year;
                newRow["times"] = ThisTimes;
                newRow["TimesRate"] = (ThisTimes / Convert.ToDouble(TotalTimes) * 100).ToString("f2") + "%";
                newRow["count"] = ThisCourses;
                newRow["CountRate"] = (ThisCourses / Convert.ToDouble(TotalCourses) * 100).ToString("f2") + "%";
                newRow["group"] = ThisGroups;
                newRow["GroupRate"] = (ThisGroups / Convert.ToDouble(TotalGroups) * 100).ToString("f2") + "%";
                CountData.Rows.Add(newRow);
            }
            newRow = CountData.NewRow();
            newRow["year"] = "总计";
            newRow["times"] = TotalTimes;
            newRow["TimesRate"] = "";
            newRow["count"] = TotalCourses;
            newRow["CountRate"] = "";
            newRow["group"] = TotalGroups;
            newRow["GroupRate"] = "";
            CountData.Rows.Add(newRow);
            HistoryAllTable.DataSource = CountData;
            HistoryAllTable.DataBind();
            #endregion All
            #region Source
            DataTable HistorySourceCountData = new DataTable("");
            DataColumn HistorySourcedc = null;
            HistorySourcedc = HistorySourceCountData.Columns.Add("name", Type.GetType("System.String"));
            //GridView
            BoundField nameColumn = new BoundField();
            nameColumn.HeaderText = "表头";
            nameColumn.DataField = "name";
            HistorySourceGV.Columns.Add(nameColumn);
            for (int i = 0; i < 3; i++)
            {
                DateTime StartDate = CurrentYearDate.AddYears(-i);
                DateTime EndDate = CurrentYearDate.AddYears(-(i - 1));
                HistorySourcedc = HistorySourceCountData.Columns.Add(StartDate.Year.ToString(), Type.GetType("System.String"));
                //GridView
                BoundField ThisColumn = new BoundField();
                ThisColumn.HeaderText = StartDate.Year.ToString();
                ThisColumn.DataField = StartDate.Year.ToString();
                HistorySourceGV.Columns.Add(ThisColumn);
            }
            List<string> SourceList = (from c in ProjectModel.TempCourse
                                       where c.type != ""
                                       group c by new { c.type } into oc
                                       select oc.Key.type).ToList();
            DataRow HistorySourcenewRow;
            foreach (string c in SourceList)
            {
                HistorySourcenewRow = HistorySourceCountData.NewRow();
                HistorySourcenewRow["name"] = c;
                for (int i = 0; i < 3; i++)
                {
                    DateTime StartDate = CurrentYearDate.AddYears(-i);
                    DateTime EndDate = CurrentYearDate.AddYears(-(i - 1));
                    var count = (from o in ProjectModel.TempOrder
                                 where (o.date >= StartTime) && (o.date <= EndTime) && (o.TempCourse.CreateDate >= StartDate) && (o.TempCourse.CreateDate <= EndDate) && o.TempCourse.type == c
                                 select c);
                    if (count.Count() > 0)
                    {
                        HistorySourcenewRow[StartDate.Year.ToString()] = count.Count();
                    }
                    else { HistorySourcenewRow[StartDate.Year.ToString()] = 0; }
                }
                HistorySourceCountData.Rows.Add(HistorySourcenewRow);
            }
            HistorySourceGV.DataSource = HistorySourceCountData;
            HistorySourceGV.DataBind();
            #endregion  Source
            #region Category
            DataTable HistoryCategoryCountData = new DataTable("");
            DataColumn HistoryCategorydc = null;
            HistoryCategorydc = HistoryCategoryCountData.Columns.Add("name", Type.GetType("System.String"));
            //GridView
            BoundField CategoryNameColumn = new BoundField();
            CategoryNameColumn.HeaderText = "表头";
            CategoryNameColumn.DataField = "name";
            HistoryCategoryGV.Columns.Add(CategoryNameColumn);
            for (int i = 0; i < 3; i++)
            {
                DateTime StartDate = CurrentYearDate.AddYears(-i);
                DateTime EndDate = CurrentYearDate.AddYears(-(i - 1));
                HistoryCategorydc = HistoryCategoryCountData.Columns.Add(StartDate.Year.ToString(), Type.GetType("System.String"));
                //GridView
                BoundField ThisColumn = new BoundField();
                ThisColumn.HeaderText = StartDate.Year.ToString();
                ThisColumn.DataField = StartDate.Year.ToString();
                HistoryCategoryGV.Columns.Add(ThisColumn);
            }
            var CategoryList = (from c in ProjectModel.TempCourse
                                       where c.InternalCategory != ""
                                       group c by new { c.InternalCategoryTop,c.InternalCategory } into oc
                                       select new { category=oc.Key.InternalCategory,top = oc.Key.InternalCategoryTop }).ToList();
            DataRow HistoryCategorynewRow;
            foreach (var c in CategoryList)
            {
                HistoryCategorynewRow = HistoryCategoryCountData.NewRow();
                HistoryCategorynewRow["name"] = c.top+"-"+c.category;
                for (int i = 0; i < 3; i++)
                {
                    DateTime StartDate = CurrentYearDate.AddYears(-i);
                    DateTime EndDate = CurrentYearDate.AddYears(-(i - 1));
                    var count = (from o in ProjectModel.TempOrder
                                 where (o.date >= StartTime) && (o.date <= EndTime) && (o.TempCourse.CreateDate >= StartDate) && (o.TempCourse.CreateDate <= EndDate) && o.TempCourse.InternalCategory == c.category
                                 select c.category);
                    if (count.Count() > 0)
                    {
                        HistoryCategorynewRow[StartDate.Year.ToString()] = count.Count();
                    }
                    else { HistoryCategorynewRow[StartDate.Year.ToString()] = 0; }
                }
                HistoryCategoryCountData.Rows.Add(HistoryCategorynewRow);
            }
            HistoryCategoryGV.DataSource = HistoryCategoryCountData;
            HistoryCategoryGV.DataBind();
            #endregion  Category
        }

        protected void CourseBtn_Click(object sender, EventArgs e)
        {
            CoursePanel.Visible = true;
            DateTime StartTime;
            DateTime EndTime;
            if (hidStartDate.Value == "" || hidEndDate.Value == "")
            {
                EndTime = DateTime.Now;
                StartTime = DateTime.Now.AddMonths(-6);
            }
            else
            {
                StartTime = Convert.ToDateTime(hidStartDate.Value);
                EndTime = Convert.ToDateTime(hidEndDate.Value);
            }
            string CurrentYear = EndTime.Year.ToString();
            CurrentYear = CurrentYear + "-01-01 00:00:00";
            DateTime CurrentYearDate = Convert.ToDateTime(CurrentYear);
            DateTime EndYearDate = CurrentYearDate.AddYears(+1);
            DateTime StartYearDate = CurrentYearDate.AddYears(-2);
            var ProjectModel = new ProjectCollection.WebUI.Models.ProjectCollectionEntities();
            #region All
            DataTable HistoryCountData = new DataTable("");
            DataColumn Historydc = null;
            Historydc = HistoryCountData.Columns.Add("name", Type.GetType("System.String"));
            //GridView
            BoundField HistoryNameColumn = new BoundField();
            HistoryNameColumn.HeaderText = "表头";
            HistoryNameColumn.DataField = "name";
            HistoryCourseGV.Columns.Add(HistoryNameColumn);
            for (int i = 0; i < 3; i++)
            {
                DateTime StartDate = CurrentYearDate.AddYears(-i);
                DateTime EndDate = CurrentYearDate.AddYears(-(i - 1));
                Historydc = HistoryCountData.Columns.Add(StartDate.Year.ToString(), Type.GetType("System.String"));
                //GridView
                BoundField ThisColumn = new BoundField();
                ThisColumn.HeaderText = StartDate.Year.ToString();
                ThisColumn.DataField = StartDate.Year.ToString();
                HistoryCourseGV.Columns.Add(ThisColumn);
            }
            List<string> CourseList = (from o in ProjectModel.TempOrder
                                where (o.date >= StartTime) && (o.date <= EndTime) && o.TempCourse.type=="自筹"
                              group o by new { o.TempCourse.title } into oc
                              orderby oc.Key.title
                                       select oc.Key.title).Take(50).ToList();
            DataRow HistorynewRow;
            foreach (var c in CourseList)
            {
                HistorynewRow = HistoryCountData.NewRow();
                HistorynewRow["name"] = c;
                for (int i = 0; i < 3; i++)
                {
                    DateTime StartDate = CurrentYearDate.AddYears(-i);
                    DateTime EndDate = CurrentYearDate.AddYears(-(i - 1));
                    var count = (from o in ProjectModel.TempOrder
                                 where (o.date >= StartDate) && (o.date <= EndDate) && (o.TempCourse.title == c)
                                 group o by new { o.TempCourse.title,o.TempCustomer.id } into oc
                                 select oc);
                    if (count.Count() > 0)
                    {
                        HistorynewRow[StartDate.Year.ToString()] = count.Count();
                    }
                    else { HistorynewRow[StartDate.Year.ToString()] = 0; }
                }
                HistoryCountData.Rows.Add(HistorynewRow);
            }
            HistoryCourseGV.DataSource = HistoryCountData;
            HistoryCourseGV.DataBind();
            #endregion  All
            #region New
            DataTable NewCountData = new DataTable("");
            DataColumn Newdc = null;
            Newdc = NewCountData.Columns.Add("name", Type.GetType("System.String"));
            //GridView
            BoundField NewNameColumn = new BoundField();
            NewNameColumn.HeaderText = "表头";
            NewNameColumn.DataField = "name";
            NewCourseGV.Columns.Add(NewNameColumn);
            for (int i = 0; i <  1; i++)
            {
                DateTime StartDate = CurrentYearDate.AddYears(-i);
                DateTime EndDate = CurrentYearDate.AddYears(-(i - 1));
                Newdc = NewCountData.Columns.Add(StartDate.Year.ToString(), Type.GetType("System.String"));
                //GridView
                BoundField ThisColumn = new BoundField();
                ThisColumn.HeaderText = StartDate.Year.ToString();
                ThisColumn.DataField = StartDate.Year.ToString();
                NewCourseGV.Columns.Add(ThisColumn);
            }
            List<string> NewCourseList = (from o in ProjectModel.TempOrder
                                       where (o.TempCourse.CreateDate>= CurrentYearDate) && o.TempCourse.type == "自筹"
                                       group o by new { o.TempCourse.title } into oc
                                       orderby oc.Key.title
                                       select oc.Key.title).Take(50).ToList();
            DataRow NewnewRow;
            foreach (var c in NewCourseList)
            {
                NewnewRow = NewCountData.NewRow();
                NewnewRow["name"] = c;
                for (int i = 0; i < 1; i++)
                {
                    DateTime StartDate = CurrentYearDate.AddYears(-i);
                    DateTime EndDate = CurrentYearDate.AddYears(-(i - 1));
                    var count = (from o in ProjectModel.TempOrder
                                 where (o.TempCourse.title == c)
                                 group o by new { o.TempCourse.title, o.TempCustomer.id } into oc
                                 select oc);
                    if (count.Count() > 0)
                    {
                        NewnewRow[StartDate.Year.ToString()] = count.Count();
                    }
                    else { NewnewRow[StartDate.Year.ToString()] = 0; }
                }
                NewCountData.Rows.Add(NewnewRow);
            }
            NewCourseGV.DataSource = NewCountData;
            NewCourseGV.DataBind();
            #endregion New
            #region Customer
            DataTable CustomerCountData = new DataTable("");
            DataColumn Customerdc = null;
            Historydc = CustomerCountData.Columns.Add("name", Type.GetType("System.String"));
            //GridView
            BoundField CustomerNameColumn = new BoundField();
            CustomerNameColumn.HeaderText = "表头";
            CustomerNameColumn.DataField = "name";
            CustomerCourseGV.Columns.Add(HistoryNameColumn);
            List<string> CustomerSortList = (from c in ProjectModel.TempCustomer
                                             group c by new { c.sort } into oc
                                             select oc.Key.sort).ToList();
            foreach (string c in CustomerSortList)
            {
                Customerdc = CustomerCountData.Columns.Add(c, Type.GetType("System.String"));
                //GridView
                BoundField ThisColumn = new BoundField();
                ThisColumn.HeaderText = c;
                ThisColumn.DataField = c;
                CustomerCourseGV.Columns.Add(ThisColumn);
            }
            List<string> CustomerCourseList = (from o in ProjectModel.TempOrder
                                       where (o.date >= StartTime) && (o.date <= EndTime) && o.TempCourse.type == "自筹"
                                       group o by new { o.TempCourse.title } into oc
                                       orderby oc.Key.title
                                       select oc.Key.title).Take(50).ToList();
            DataRow CustomernewRow;
            foreach (var c in CustomerCourseList)
            {
                CustomernewRow = CustomerCountData.NewRow();
                CustomernewRow["name"] = c;
                foreach (string cs in CustomerSortList)
                {
                    var count = (from o in ProjectModel.TempOrder
                                 where (o.date >= StartTime) && (o.date <= EndTime) && (o.TempCourse.title == c) && (o.TempCustomer.sort == cs)
                                 group o by new { o.TempCourse.title, o.TempCustomer.id } into oc
                                 select oc);
                    if (count.Count() > 0)
                    {
                        CustomernewRow[cs] = count.Count();
                    }
                    else { CustomernewRow[cs] = 0; }
                }
                CustomerCountData.Rows.Add(CustomernewRow);
            }
            CustomerCourseGV.DataSource = CustomerCountData;
            CustomerCourseGV.DataBind();
            #endregion  Customer
            #region Group
            var GroupList = (from o in ProjectModel.TempOrder
                                where (o.TempCourse.CreateDate >= StartTime) && (o.TempCourse.CreateDate <= EndTime) && o.TempCourse.type=="自筹"
                                       select o).ToList();
            var GroupTimes = from o in GroupList
                             group o by new { o.TempCourse.GroupName, o.TempCourse.SourceCourseId,o.TempCustomer.id } into oc
                             select oc;
            var GroupData = from o in GroupList
                            group o by new { o.TempCourse.GroupName } into od
                            select new {
                                title = od.Key.GroupName,
                                times = (from o in GroupTimes
                                         where o.Key.GroupName == od.Key.GroupName
                                         select o).Count(),
                                groups= (from o in GroupTimes
                                         where o.Key.GroupName == od.Key.GroupName
                                         group o by new { o.Key.SourceCourseId } into og
                                         select og).Count(),
                                rate = ((from o in GroupTimes
                                          where o.Key.GroupName == od.Key.GroupName
                                          select o).Count() / Convert.ToDouble((from o in GroupTimes
                                                                                where o.Key.GroupName == od.Key.GroupName
                                                                                group o by new { o.Key.SourceCourseId } into og
                                                                                select og).Count())).ToString("f2")
                            };
            GroupGV.DataSource = GroupData;
            GroupGV.DataBind();
            #endregion Group
        }
    }

}