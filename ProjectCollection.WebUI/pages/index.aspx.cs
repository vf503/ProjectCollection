using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ProjectCollection.BLL;
using ProjectCollection.WebUI.pages.common;

namespace ProjectCollection.WebUI.pages
{
    public partial class index : System.Web.UI.Page
    {
        public Guid CustomWithCamera
        {
            get
            {
                return new Guid("00000000-0000-0000-0000-000000000201"); 
            }
            set
            {
                 CustomWithCamera = value;
            }
        }

        public Guid CustomWithNoCamera
        {
            get
            {
                return new Guid("00000000-0000-0000-0000-000000000200");
            }
            set
            {
                CustomWithNoCamera = value;
            }
        }

        public Guid Diy
        {
            get
            {
                return new Guid("00000000-0000-0000-0000-000000000020");
            }
            set
            {
                Diy = value;
            }
        }

        public Guid Cooperate 
        {
            get
            {
                return new Guid("00000000-0000-0000-0000-000000000016");
            }
            set
            {
                Cooperate = value;
            }
        }

        public Guid InPut
        {
            get
            {
                return new Guid("00000000-0000-0000-0000-000000000014");
            }
            set
            {
                InPut = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            lbTodayPlan.Text = ProjectPlan.GetProjectTotalToday().ToString();
            lbTodayProject.Text = Project.GetProjectTotalToday().ToString();
            lbTotalPlan.Text = ProjectPlan.GetProjectPlanTotal().ToString();
            lbFinishedPlan.Text = ProjectPlan.GetProjectTotalFinish().ToString();
            double numerator=ProjectPlan.GetProjectTotalFinish();
            double denominator=ProjectPlan.GetProjectPlanTotal();
            lbPlanRate.Text = Convert.ToDouble(numerator/denominator).ToString("P");
            lbTotalProject.Text=Project.GetProjectTotal().ToString();
            lbFinishedProject.Text = Project.GetProjectTotalFinish().ToString();
            numerator = Project.GetProjectTotalFinish();
            denominator = Project.GetProjectTotal();
            lbProjectRate.Text = Convert.ToDouble(numerator / denominator).ToString("P");
            lbDelayPlan.Text = ProjectPlan.GetProjectTotalDelay().ToString();
            numerator = ProjectPlan.GetProjectTotalDelay();
            denominator = ProjectPlan.GetProjectPlanTotal();
            lbDelayPlanRate.Text = Convert.ToDouble(numerator / denominator).ToString("0.00");
            lbDelayProject.Text = Project.GetProjectTotalDelay().ToString();
            numerator =  Project.GetProjectTotalDelay();
            denominator = Project.GetProjectTotal();;
            lbDelayProjectRate.Text = Convert.ToDouble(numerator / denominator).ToString("0.00");
            lbDPlan.Text = (ProjectPlan.GetProjectTotalType(CustomWithCamera) + ProjectPlan.GetProjectTotalType(CustomWithNoCamera)).ToString();
            lbDProject.Text = (Project.GetProjectTotalPlanType(CustomWithCamera) + Project.GetProjectTotalPlanType(CustomWithNoCamera)).ToString();
            lbAPlan.Text = ProjectPlan.GetProjectTotalType(Diy).ToString();
            lbAProject.Text = Project.GetProjectTotalPlanType(Diy).ToString();
            lbBPlan.Text = ProjectPlan.GetProjectTotalType(Cooperate).ToString();
            lbBProject.Text = Project.GetProjectTotalPlanType(Cooperate).ToString();
            lbIPlan.Text = ProjectPlan.GetProjectTotalType(InPut).ToString();
            lbIProject.Text = Project.GetProjectTotalPlanType(InPut).ToString();
        }
    }
}