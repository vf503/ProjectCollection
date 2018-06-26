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
    public partial class AmountServer : System.Web.UI.Page
    {
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
                dtManHours = BLL.Project.GetManHoursCalendarDayAmount(cbType.SelectedItem.Value.ToString(), deStart.Date.ToString(), deEnd.Date.ToString());
                dtManHours.Columns.Add("Text", typeof(string));
                int Count = 0;
                foreach (DataRow dr in dtManHours.Rows)
                {
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
                    double CurrentCount = Convert.ToDouble(dr["Count"].ToString());
                    double CountAmount = Convert.ToDouble(Count);
                    double RateResult = Math.Floor(((CurrentCount / CountAmount) * 100) * 10) / 10;
                    dr["Rate"] = RateResult + "%";
                    dr["Text"] = dr["Text"].ToString() + "(" + dr["Rate"] + ")";
                }
            }
            else
            {
                dtManHours = BLL.Project.GetManHoursAmount(cbType.SelectedItem.Value.ToString(), deStart.Date.ToString(), deEnd.Date.ToString());
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
                    dr["Text"] = dr["Text"].ToString() + "(" + dr["Rate"] + ")";
                }
            }
            //
            Chart1.BackColor = Color.Gray;
            Chart1.BackSecondaryColor = Color.WhiteSmoke;
            Chart1.BackGradientStyle = GradientStyle.DiagonalRight;

            Chart1.BorderlineDashStyle = ChartDashStyle.Solid;
            Chart1.BorderlineColor = Color.Gray;
            Chart1.BorderSkin.SkinStyle = BorderSkinStyle.Emboss;

            Chart1.ChartAreas[0].BackColor = Color.Wheat;
            Chart1.ChartAreas[0].Area3DStyle.Enable3D = false;
            //
            Chart1.Series.Clear();
            Chart1.Titles.Add("工单耗时统计");
            //
            Chart1.Series.Add(new Series("Series1")
            {
                ChartType = SeriesChartType.Doughnut
            });

            Chart1.DataSource = dtManHours;
            Chart1.Series[0].XValueMember = "Text";
            Chart1.Series[0].YValueMembers = "Count";
            Chart1.Series[0].IsValueShownAsLabel = true;
            Chart1.Series[0].IsVisibleInLegend = true;
            Chart1.Series[0].ChartType = SeriesChartType.Column;
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