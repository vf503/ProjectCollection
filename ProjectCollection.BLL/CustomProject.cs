using Adapt.Attribute;
using ProjectCollection.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;

namespace ProjectCollection.BLL
{
    public class CustomProject : BaseLogic
    {
        #region 属性

        #region Data

        [TableAttribute.Column("CustomProjectId")]
        public Guid CustomProjectId { get; set; }

        [TableAttribute.Column("No")]
        public string No { get; set; }

        [TableAttribute.Column("ProjectPlanId")]
        public Guid ProjectPlanId { get; set; }

        [TableAttribute.Column("SendingDate")]
        public DateTime SendingDate { get; set; }

        [TableAttribute.Column("ReceiveDate")]
        public DateTime ReceiveDate { get; set; }

        [TableAttribute.Column("Title")]
        public string Title { get; set; }

        [TableAttribute.Column("CourseAmount")]
        public string CourseAmount { get; set; }

        [TableAttribute.Column("Lecturer")]
        public string Lecturer { get; set; }

        [TableAttribute.Column("LecturerJob")]
        public string LecturerJob { get; set; }

        [TableAttribute.Column("CourseSource")]
        public string CourseSource { get; set; }

        [TableAttribute.Column("Category")]
        public string Category { get; set; }

        [TableAttribute.Column("Note")]
        public string Note { get; set; }

        [TableAttribute.Column("Signer")]
        public Guid Signer { get; set; }

        [TableAttribute.Column("Operator")]
        public Guid Operator { get; set; }

        [TableAttribute.Column("FinishDate")]
        public DateTime FinishDate { get; set; }

        [TableAttribute.Column("ReceiveNote")]
        public string ReceiveNote { get; set; }

        [TableAttribute.Column("OperationNote")]
        public string OperationNote { get; set; }

        [TableAttribute.Column("Publisher")]
        public Guid Publisher { get; set; }

        [TableAttribute.Column("PublishDate")]
        public DateTime PublishDate { get; set; }

        [TableAttribute.Column("PublishCheck")]
        public Guid PublishCheck { get; set; }

        [TableAttribute.Column("PublishNote")]
        public string PublishNote { get; set; }

        [TableAttribute.Column("Checker")]
        public Guid Checker { get; set; }

        [TableAttribute.Column("CheckDate")]
        public DateTime CheckDate { get; set; }

        [TableAttribute.Column("CategoryCheck")]
        public Guid CategoryCheck { get; set; }

        [TableAttribute.Column("CheckNote")]
        public string CheckNote { get; set; }

        [TableAttribute.Column("Progress")]
        public Guid Progress { get; set; }

        [TableAttribute.Column("PublishNeeds")]
        public Guid PublishNeeds { get; set; }

        [TableAttribute.Column("ExtraNote")]
        public string ExtraNote { get; set; }

        #endregion Data

        #region Query

        [TableAttribute.Column("ProgressText")]
        public string ProgressText { get; set; }

        #endregion Query


        #endregion 属性

        #region 方法

        #region Select

        public static List<CustomProject> GetAllCustomProject()
        {
            DataTable dt = new DAL.CustomProject().SelectAll();
            List<CustomProject> value = new List<CustomProject>();
            Adapt.Convert.ConvertDataTableToObjectList<CustomProject>(dt, value);
            return value;
        }

        public static CustomProject GetCustomProject(Guid CustomProjectId)
        {
            DataTable table = new DAL.CustomProject().SelectId(CustomProjectId);
            CustomProject value = new CustomProject();
            foreach (DataRow row in table.Rows)
                Adapt.Convert.ConvertDataRowToObject(row, value);
            return value;
        }

        public static List<CustomProject> GetCustomProjectByType(Guid ProjectPlanTypeId)
        {
            DataTable dt = new DAL.CustomProject().SelectType(ProjectPlanTypeId);
            List<CustomProject> value = new List<CustomProject>();
            Adapt.Convert.ConvertDataTableToObjectList<CustomProject>(dt, value);
            return value;
        }

        public static List<CustomProject> GetCustomProjectByType(Guid ProjectPlanTypeId,Guid Progress)
        {
            DataTable dt = new DAL.CustomProject().SelectTypeProgress(ProjectPlanTypeId, Progress);
            List<CustomProject> value = new List<CustomProject>();
            Adapt.Convert.ConvertDataTableToObjectList<CustomProject>(dt, value);
            return value;
        }

        #region Amount

        public static int GetReceiveAmount(Guid UserId, string DateBegin, string DateEnd)
        {
            int count = new DAL.CustomProject().SelectReceiveAmount(UserId, DateBegin, DateEnd);
            return count;
        }

        public static int GetFinishAmount(Guid UserId, string DateBegin, string DateEnd)
        {
            int count = new DAL.CustomProject().SelectFinishAmount(UserId, DateBegin, DateEnd);
            return count;
        }

        public static int GetPublishAmount(Guid UserId, string DateBegin, string DateEnd)
        {
            int count = new DAL.CustomProject().SelectPublishAmount(UserId, DateBegin, DateEnd);
            return count;
        }

        public static int GetCheckAmount(Guid UserId, string DateBegin, string DateEnd)
        {
            int count = new DAL.CustomProject().SelectCheckAmount(UserId, DateBegin, DateEnd);
            return count;
        }

        #endregion Amount

        #endregion Select

        #region Insert

        public static int Insert(CustomProject customProject)
        {
            int count = 0;
            count = new DAL.CustomProject().Insert(customProject.CustomProjectId, customProject.No, customProject.ProjectPlanId, customProject.SendingDate
                ,customProject.Title, customProject.CourseAmount, customProject.Lecturer, customProject.LecturerJob,customProject.CourseSource
                ,customProject.Category, customProject.PublishNeeds, customProject.Note, customProject.ExtraNote, customProject.Progress);
            return count;
        }

        #endregion Insert

        #region Update

        public static int UpdateReceive(CustomProject project)
        {
            int count = 0;
            count = new DAL.CustomProject().UpdateReceive(project.CustomProjectId, project.Progress, project.Signer, project.ReceiveDate, project.ReceiveNote, project.Operator);
            return count;
        }

        public static int UpdateOperation(CustomProject project)
        {
            int count = 0;
            count = new DAL.CustomProject().UpdateOperation(project.CustomProjectId, project.Progress, project.FinishDate, project.OperationNote);
            return count;
        }

        public static int UpdatePublish(CustomProject project)
        {
            int count = 0;
            count = new DAL.CustomProject().UpdatePublish(project.CustomProjectId, project.Progress, project.Publisher,project.PublishDate, project.PublishNote, project.PublishCheck);
            return count;
        }

        public static int UpdateCheck(CustomProject project)
        {
            int count = 0;
            count = new DAL.CustomProject().UpdateCheck(project.CustomProjectId, project.Progress, project.Checker, project.CheckDate, project.CheckNote, project.CategoryCheck);
            return count;
        }

        #endregion Update

        #endregion 方法
    }
}
