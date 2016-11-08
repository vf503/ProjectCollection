using Adapt.Attribute;
using ProjectCollection.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;

namespace ProjectCollection.BLL
{
    /// <summary>
    /// 
    /// </summary>
    public class ProjectPlan : BaseLogic
    {
        #region 属性

        #region Data

        [TableAttribute.Column("ProjectPlanId")]
        public Guid ProjectPlanId { get; set; }

        [TableAttribute.Column("ProjectPlanNo")]
        public string ProjectPlanNo { get; set; }

        [TableAttribute.Column("title")]
        public string Title { get; set; }

        [TableAttribute.Column("ProjectPlanTypeId")]
        public Guid ProjectPlanTypeId { get; set; }

        [TableAttribute.Column("ProjectPlanTypeText")]
        public string ProjectPlanTypeText { get; set; }

        [TableAttribute.Column("MakingDate")]
        public DateTime MakingDate { get; set; }

        [TableAttribute.Column("PlanDate")]
        public DateTime PlanDate { get; set; }

        [TableAttribute.Column("progress")]
        public Guid progress { get; set; }

        [TableAttribute.Column("ProgressText")]
        public string ProgressText { get; set; }

        [TableAttribute.Column("CreatorId")]
        public Guid CreatorId { get; set; }

        [TableAttribute.Column("note")]
        public string Note { get; set; }

        [TableAttribute.Column("LastModifierId")]
        public Guid LastModifierId { get; set; }

        [TableAttribute.Column("LastModifyDate")]
        public DateTime LastModifyDate { get; set; }

        [TableAttribute.Column("Lecturer")]
        public string Lecturer { get; set; }

        [TableAttribute.Column("LeadDate")]
        public DateTime LeadDate { get; set; }

        [TableAttribute.Column("CourseCount")]
        public decimal CourseCount { get; set; }

        [TableAttribute.Column("Price")]
        public int Price { get; set; }

        [TableAttribute.Column("Source")]
        public string Source { get; set; }

        [TableAttribute.Column("Category")]
        public string Category { get; set; }

        [TableAttribute.Column("ExtraNote")]
        public string ExtraNote { get; set; }

        [TableAttribute.Column("RecordingPersonInCharge")]
        public Guid RecordingPersonInCharge { get; set; }

        [TableAttribute.Column("RecordingDate")]
        public DateTime RecordingDate { get; set; }

        [TableAttribute.Column("RecordingPlace")]
        public string RecordingPlace { get; set; }

        [TableAttribute.Column("RecordingScriptHolder")]
        public bool RecordingScriptHolder { get; set; }

        [TableAttribute.Column("RecordingLecture")]
        public int RecordingLecture { get; set; }

        [TableAttribute.Column("RecordingProgress")]
        public Guid RecordingProgress { get; set; }

        [TableAttribute.Column("RecordingNote")]
        public string RecordingNote { get; set; }

        [TableAttribute.Column("FileDeliverDate")]
        public DateTime FileDeliverDate { get; set; }

        [TableAttribute.Column("RecordingFile")]
        public string RecordingFile { get; set; }

        #endregion Data

        #region Query

        [TableAttribute.Column("ProjectCount")]
        public int ProjectCount { get; set; }

        [TableAttribute.Column("ProjectFinishCount")]
        public int ProjectFinishCount { get; set; }

        [TableAttribute.Column("ProjectDelayCount")]
        public int ProjectDelayCount { get; set; }

        [TableAttribute.Column("CreatorName")]
        public string CreatorName { get; set; }

        #endregion Query

        #endregion 属性

        #region 方法
        
        #region Select

        /// <summary>
        /// 
        /// </summary>
        /// <param name="projectPlanId"></param>
        /// <returns></returns>
        public static ProjectPlan GetProjectPlan(Guid projectPlanId)
        {
            DataTable table = new DAL.ProjectPlan().Select(projectPlanId);
            ProjectPlan value = new ProjectPlan();
            foreach (DataRow row in table.Rows)
                Adapt.Convert.ConvertDataRowToObject(row, value);
            return value;
        }

        public static List<ProjectPlan> GetProjectPlan()
        {
            DataTable dt = new DAL.ProjectPlan().Select();
            List<ProjectPlan> value = new List<ProjectPlan>();
            Adapt.Convert.ConvertDataTableToObjectList<ProjectPlan>(dt, value);
            return value;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static List<ProjectPlan> GetAllProjectPlan()
        {
            DataTable dt = new DAL.ProjectPlan().SelectAll();
            List<ProjectPlan> value = new List<ProjectPlan>();
            Adapt.Convert.ConvertDataTableToObjectList<ProjectPlan>(dt, value);
            return value;
        }

        public static List<ProjectPlan> GetAllProjectPlanPage(int PageIndex)
        {
            DataTable dt = new DAL.ProjectPlan().SelectPageAll(PageIndex);
            List<ProjectPlan> value = new List<ProjectPlan>();
            Adapt.Convert.ConvertDataTableToObjectList<ProjectPlan>(dt, value);
            return value;
        }

        public static List<ProjectPlan> GetFinishedProjectPlanPage(int PageIndex)
        {
            DataTable dt = new DAL.ProjectPlan().SelectPageFinished(PageIndex);
            List<ProjectPlan> value = new List<ProjectPlan>();
            Adapt.Convert.ConvertDataTableToObjectList<ProjectPlan>(dt, value);
            return value;
        }

        public static List<ProjectPlan> GetUnfinishProjectPlanPage(int PageIndex)
        {
            DataTable dt = new DAL.ProjectPlan().SelectPageUnfinish(PageIndex);
            List<ProjectPlan> value = new List<ProjectPlan>();
            Adapt.Convert.ConvertDataTableToObjectList<ProjectPlan>(dt, value);
            return value;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static List<ProjectPlan> GetRecondProjectPlan()
        {
            DataTable dt = new DAL.ProjectPlan().SelectRecond();
            List<ProjectPlan> value = new List<ProjectPlan>();
            Adapt.Convert.ConvertDataTableToObjectList<ProjectPlan>(dt, value);
            return value;
        }

        public static int GetProjectPlanTotal()
        {
            int count = new DAL.ProjectPlan().SelectTotal();
            return count;
        }

        public static int GetProjectTotalToday()
        {
            int count = new DAL.ProjectPlan().SelectTotalToday();
            return count;
        }

        public static int GetProjectTotalFinish()
        {
            int count = new DAL.ProjectPlan().SelectTotalFinish();
            return count;
        }

        public static int GetProjectTotalDelay()
        {
            int count = new DAL.ProjectPlan().SelectTotalDelay();
            return count;
        }

        public static int GetProjectTotalType(Guid TypeId)
        {
            int count = new DAL.ProjectPlan().SelectTotalType(TypeId);
            return count;
        }

        #region Amount

        public static int GetRecondAmount(Guid RecordingPersonInCharge, string DateBegin, string DateEnd)
        {
            int count = new DAL.ProjectPlan().SelectRecondAmount(RecordingPersonInCharge, DateBegin, DateEnd);
            return count;
        }

        #endregion Amount

        #endregion Select

        #region Insert

        /// <summary>
        /// 
        /// </summary>
        /// <param name="projectPlan"></param>
        /// <returns></returns>
        public static int Insert(ProjectPlan projectPlan)
        {
            int count = 0;
            count = new DAL.ProjectPlan().Insert(projectPlan.ProjectPlanId, projectPlan.ProjectPlanNo, projectPlan.Title,
                projectPlan.ProjectPlanTypeId, projectPlan.PlanDate, projectPlan.progress, projectPlan.CreatorId,projectPlan.Category, projectPlan.CourseCount, projectPlan.Price, projectPlan.Source,projectPlan.Note,projectPlan.Lecturer,projectPlan.ExtraNote,projectPlan.RecordingPersonInCharge);
            return count;
        }

        #endregion Insert

        #region Update

        /// <summary>
        /// 
        /// </summary>
        /// <param name="projectPlan"></param>
        /// <returns></returns>
        public static int Update(ProjectPlan projectPlan)
        {
            int count = 0;
            count = new DAL.ProjectPlan().Update(projectPlan.ProjectPlanId, projectPlan.ProjectPlanNo, projectPlan.Title, projectPlan.ProjectPlanTypeId, projectPlan.PlanDate, projectPlan.Category, projectPlan.CourseCount, projectPlan.Price, projectPlan.Source, projectPlan.Note, projectPlan.Lecturer, projectPlan.ExtraNote,projectPlan.RecordingPersonInCharge);
            return count;
        }

        public static int UpdateFinish(ProjectPlan projectPlan)
        {
            int count = 0;
            count = new DAL.ProjectPlan().Update(projectPlan.ProjectPlanId,projectPlan.progress);
            return count;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="projectPlan"></param>
        /// <returns></returns>
        public static int UpdateRecond(ProjectPlan projectPlan)
        {
            int count = 0;
            count = new DAL.ProjectPlan().UpdateRecond(projectPlan.ProjectPlanId, projectPlan.progress, projectPlan.RecordingDate, projectPlan.RecordingPlace, projectPlan.RecordingScriptHolder, projectPlan.RecordingLecture, projectPlan.RecordingFile, projectPlan.FileDeliverDate, projectPlan.RecordingNote);
            return count;
        }

        #endregion Update

        #endregion
    }
}
