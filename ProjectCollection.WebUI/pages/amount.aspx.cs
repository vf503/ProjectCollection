using ProjectCollection.BLL;
using ProjectCollection.Common;
using ProjectCollection.WebUI.pages.common;
using System;
using System.Linq;
using System.Data;
using System.Configuration;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using DevExpress.Web;
using System.Web.UI.DataVisualization.Charting;
using System.Drawing;

namespace ProjectCollection.WebUI.pages
{
    public partial class amount : System.Web.UI.Page
    {
        public int value = 1;
        public string ChartLabels = "";
        public string ChartCount = "";
        public string ChartRate = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (deStart.Date.ToString() == "0001/1/1 0:00:00")
            {
                deStart.Date = DateTime.Now.AddYears(-1);
            }
            else { }
            if (deEnd.Date.ToString() == "0001/1/1 0:00:00")
            {
                deEnd.Date = DateTime.Now;
            }
            else { }
            DataTable dtManHours = new DataTable();
            if (cbDay.SelectedItem.Value.ToString() == "CalendarDay")
            {
                dtManHours = BLL.Project.GetManHoursCalendarDayAmount(cbType.SelectedItem.Value.ToString(), deStart.Date.Date.ToString(), deEnd.Date.Date.AddHours(24).ToString());
                dtManHours.Columns.Add("Text", typeof(string));
                int Count = 0;
                foreach (DataRow dr in dtManHours.Rows)
                {
                    if (dr["Title"].ToString() == "0")
                    {
                        //dr["Text"] = "其他";
                    }
                    if (dr["Title"].ToString() == "1")
                    {
                        dr["Text"] = "15天内";
                    }
                    else if (dr["Title"].ToString() == "2")
                    {
                        dr["Text"] = "15至20天";
                    }
                    else if (dr["Title"].ToString() == "3")
                    {
                        dr["Text"] = "20至25天";
                    }
                    else if (dr["Title"].ToString() == "4")
                    {
                        dr["Text"] = "超过25天";
                    }
                    else { }
                    Count += Convert.ToInt16(dr["Count"].ToString());
                }
                dtManHours.Columns.Add("Rate", typeof(string));
                foreach (DataRow dr in dtManHours.Rows)
                {
                    double RateResult = Percent(dr["Count"].ToString(), Count.ToString());
                    dr["Rate"] = RateResult + "%";
                    //dr["Text"] = dr["Text"].ToString() + "(" + dr["Rate"] + ")";
                }
                ChartLabels = String.Join("\",\"", dtManHours.AsEnumerable().Select(d => d.Field<string>("Text")).ToArray());
                ChartCount = String.Join(",", dtManHours.AsEnumerable().Select(d => d.Field<int>("Count")).ToArray());
                ChartRate = String.Join("\",\"", dtManHours.AsEnumerable().Select(d => d.Field<string>("Rate")).ToArray());
            }
            else
            {
                dtManHours = BLL.Project.GetManHoursAmount(cbType.SelectedItem.Value.ToString(), deStart.Date.Date.ToString(), deEnd.Date.Date.AddHours(24).ToString());
                dtManHours.Columns.Add("Text", typeof(string));
                int Count = 0;
                foreach (DataRow dr in dtManHours.Rows)
                {
                    if (dr["Title"].ToString() == "1")
                    {
                        dr["Text"] = "5天内";
                    }
                    else if (dr["Title"].ToString() == "2")
                    {
                        dr["Text"] = "5至10天";
                    }
                    else if (dr["Title"].ToString() == "3")
                    {
                        dr["Text"] = "10至15天";
                    }
                    else if (dr["Title"].ToString() == "4")
                    {
                        dr["Text"] = "15至20天";
                    }
                    else if (dr["Title"].ToString() == "5")
                    {
                        dr["Text"] = "超过20天";
                    }
                    else { }
                    Count += Convert.ToInt16(dr["Count"].ToString());
                }
                dtManHours.Columns.Add("Rate", typeof(string));
                foreach (DataRow dr in dtManHours.Rows)
                {
                    double RateResult = Percent(dr["Count"].ToString(), Count.ToString());
                    dr["Rate"] = RateResult + "%";
                    //dr["Text"] = dr["Text"].ToString() + "(" + dr["Rate"] + ")";
                }
                ChartLabels = String.Join("\",\"", dtManHours.AsEnumerable().Select(d => d.Field<string>("Text")).ToArray());
                ChartCount = String.Join(",", dtManHours.AsEnumerable().Select(d => d.Field<int>("Count")).ToArray());
                ChartRate = String.Join("\",\"", dtManHours.AsEnumerable().Select(d => d.Field<string>("Rate")).ToArray());
            }
            //
            //Chart1.DataSource= dtManHours;
            //
        }

        protected double Percent(string numerator, string denominator)
        {
            double CurrentCount = Convert.ToDouble(numerator);
            double CountAmount = Convert.ToDouble(denominator);
            double RateResult = Math.Floor(((CurrentCount / CountAmount) * 100) * 10) / 10;
            return RateResult;
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {

        }

    }
}