using Adapt.Attribute;
using ProjectCollection.DAL;
using ProjectCollection.Common;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;

namespace ProjectCollection.BLL
{
    /// <summary>
    /// 
    /// </summary>
    public class Project : BaseLogic
    {
        #region 属性

        #region DataBase

        [TableAttribute.Column("ProjectId")]
        public Guid ProjectId { get; set; }

        [TableAttribute.Column("ProjectPlanId")]
        public Guid ProjectPlanId { get; set; }

        [TableAttribute.Column("ProjectTypeId")]
        public Guid ProjectTypeId { get; set; }

        [TableAttribute.Column("InCharge")]
        public Guid InCharge { get; set; }

        [TableAttribute.Column("ProjectNo")]
        public string ProjectNo { get; set; }

        [TableAttribute.Column("SendingDate")]
        public DateTime SendingDate { get; set; }

        [TableAttribute.Column("emergency")]
        public Guid emergency { get; set; }

        [TableAttribute.Column("WorkType")]
        public Guid WorkType { get; set; }

        [TableAttribute.Column("CourseName")]
        public string CourseName { get; set; }

        [TableAttribute.Column("lecture")]
        public Guid lecture { get; set; }

        [TableAttribute.Column("notice")]
        public Guid notice { get; set; }

        [TableAttribute.Column("headline")]
        public Guid headline { get; set; }

        [TableAttribute.Column("TextCategory")]
        public string TextCategory { get; set; }

        [TableAttribute.Column("lecturer")]
        public string lecturer { get; set; }

        [TableAttribute.Column("LecturerJob")]
        public string LecturerJob { get; set; }

        [TableAttribute.Column("progress")]
        public Guid progress { get; set; }

        [TableAttribute.Column("CreateNote")]
        public string CreateNote { get; set; }

        [TableAttribute.Column("CaptureNeeds")]
        public Guid CaptureNeeds { get; set; }

        [TableAttribute.Column("ContentNeeds")]
        public Guid ContentNeeds { get; set; }

        [TableAttribute.Column("productCourseType")]
        public string ProductCourseType { get; set; }

        [TableAttribute.Column("PublishNeeds")]
        public Guid PublishNeeds { get; set; }

        [TableAttribute.Column("CapturePersonInCharge")]
        public Guid CapturePersonInCharge { get; set; }

        [TableAttribute.Column("CaptureOperator")]
        public Guid CaptureOperator { get; set; }

        [TableAttribute.Column("CaptureProgress")]
        public Guid CaptureProgress { get; set; }

        [TableAttribute.Column("CaptureFeedBackNote")]
        public string CaptureFeedBackNote { get; set; }

        [TableAttribute.Column("ShorthandPersonInCharge")]
        public Guid ShorthandPersonInCharge { get; set; }

        [TableAttribute.Column("ShorthandOperator")]
        public Guid ShorthandOperator { get; set; }

        [TableAttribute.Column("ShorthandProgress")]
        public Guid ShorthandProgress { get; set; }

        [TableAttribute.Column("ShorthandFeedBackNote")]
        public string ShorthandFeedBackNote { get; set; }

        [TableAttribute.Column("ContentPersonInCharge")]
        public Guid ContentPersonInCharge { get; set; }

        [TableAttribute.Column("ContentOperator")]
        public Guid ContentOperator { get; set; }

        [TableAttribute.Column("ContentProgress")]
        public Guid ContentProgress { get; set; }

        [TableAttribute.Column("ContentFeedBackNote")]
        public string ContentFeedBackNote { get; set; }

        [TableAttribute.Column("ProductionPersonInCharge")]
        public Guid ProductionPersonInCharge { get; set; }

        [TableAttribute.Column("ProductionOperator")]
        public Guid ProductionOperator { get; set; }

        [TableAttribute.Column("ProductionProgress")]
        public Guid ProductionProgress { get; set; }

        [TableAttribute.Column("ProductionFeedBackNote")]
        public string ProductionFeedBackNote { get; set; }

        [TableAttribute.Column("PublishPersonInCharge")]
        public Guid PublishPersonInCharge { get; set; }

        [TableAttribute.Column("PublishOperator")]
        public Guid PublishOperator { get; set; }

        [TableAttribute.Column("PublishProgress")]
        public Guid PublishProgress { get; set; }

        [TableAttribute.Column("PublishFeedBackNote")]
        public string PublishFeedBackNote { get; set; }

        [TableAttribute.Column("CheckPersonInCharge")]
        public Guid CheckPersonInCharge { get; set; }

        [TableAttribute.Column("CheckOperator")]
        public Guid CheckOperator { get; set; }

        [TableAttribute.Column("CheckProgress")]
        public Guid CheckProgress { get; set; }

        [TableAttribute.Column("CheckFeedBackNote")]
        public string CheckFeedBackNote { get; set; }

        [TableAttribute.Column("CaptureDuration")]
        public int CaptureDuration { get; set; }

        [TableAttribute.Column("CaptureReceiveDate")]
        public DateTime CaptureReceiveDate { get; set; }

        [TableAttribute.Column("CaptureFinishDate")]
        public DateTime CaptureFinishDate { get; set; }

        [TableAttribute.Column("CaptureVideoNeeds")]
        public Guid CaptureVideoNeeds { get; set; }

        [TableAttribute.Column("CaptureAudioNeeds")]
        public Guid CaptureAudioNeeds { get; set; }

        [TableAttribute.Column("CaptureVideoVideoQuality")]
        public Guid CaptureVideoVideoQuality { get; set; }

        [TableAttribute.Column("CaptureVideoAudioQuality")]
        public Guid CaptureVideoAudioQuality { get; set; }

        [TableAttribute.Column("CaptureAudioQuality")]
        public Guid CaptureAudioQuality { get; set; }

        [TableAttribute.Column("CaptureSoundTrack")]
        public Guid CaptureSoundTrack { get; set; }

        [TableAttribute.Column("CaptureFilePath")]
        public string CaptureFilePath { get; set; }

        [TableAttribute.Column("CaptureNote")]
        public string CaptureNote { get; set; }

        [TableAttribute.Column("ShorthandReceiveDate")]
        public DateTime ShorthandReceiveDate { get; set; }

        [TableAttribute.Column("ShorthandAudioReceiveDate")]
        public DateTime ShorthandAudioReceiveDate { get; set; }

        [TableAttribute.Column("ShorthandFinishDate")]
        public DateTime ShorthandFinishDate { get; set; }

        [TableAttribute.Column("ShorthandPurveyor")]
        public string ShorthandPurveyor { get; set; }

        [TableAttribute.Column("ShorthandQuality")]
        public Guid ShorthandQuality { get; set; }

        [TableAttribute.Column("ShorthandNote")]
        public string ShorthandNote { get; set; }

        [TableAttribute.Column("ShorthandFilePath")]
        public string ShorthandFilePath { get; set; }

        [TableAttribute.Column("ContentReceiveDate")]
        public DateTime ContentReceiveDate { get; set; }

        [TableAttribute.Column("ContentAssignmentDate")]
        public DateTime ContentAssignmentDate { get; set; }

        [TableAttribute.Column("ContentEstimatedDate")]
        public DateTime ContentEstimatedDate { get; set; }

        [TableAttribute.Column("ContentFinishDate")]
        public DateTime ContentFinishDate { get; set; }

        [TableAttribute.Column("ContentDelayNote")]
        public string ContentDelayNote { get; set; }

        [TableAttribute.Column("ContentCourseNameConfirm")]
        public Guid ContentCourseNameConfirm { get; set; }

        [TableAttribute.Column("ContentChangedCourseName")]
        public string ContentChangedCourseName { get; set; }

        [TableAttribute.Column("ContentCourseRecommend")]
        public Guid ContentCourseRecommend { get; set; }

        [TableAttribute.Column("ContentPPTAdvice")]
        public Guid ContentPPTAdvice { get; set; }

        [TableAttribute.Column("ContentExercises")]
        public int ContentExercises { get; set; }

        [TableAttribute.Column("ContentOperateNote")]
        public string ContentOperateNote { get; set; }

        [TableAttribute.Column("ContentCourseIntroductionQuality")]
        public Guid ContentCourseIntroductionQuality { get; set; }

        [TableAttribute.Column("ContentResumeQuality")]
        public Guid ContentResumeQuality { get; set; }

        [TableAttribute.Column("ContentPPTQuality")]
        public Guid ContentPPTQuality { get; set; }

        [TableAttribute.Column("ContentExercisesQuality")]
        public Guid ContentExercisesQuality { get; set; }

        [TableAttribute.Column("ContentTextQuality")]
        public Guid ContentTextQuality { get; set; }

        [TableAttribute.Column("ContentIsTimely")]
        public Guid ContentIsTimely { get; set; }

        [TableAttribute.Column("ContentCheckDate")]
        public DateTime ContentCheckDate { get; set; }

        [TableAttribute.Column("ContentCheckNote")]
        public string ContentCheckNote { get; set; }

        [TableAttribute.Column("ContentPPTNeeds")]
        public Guid ContentPPTNeeds { get; set; }

        [TableAttribute.Column("ContentCourseIntroNeeds")]
        public Guid ContentCourseIntroNeeds { get; set; }

        [TableAttribute.Column("ContentLecturerResumeNeeds")]
        public Guid ContentLecturerResumeNeeds { get; set; }

        [TableAttribute.Column("ContentExercisesNeeds")]
        public Guid ContentExercisesNeeds { get; set; }

        [TableAttribute.Column("ContentTextEditNeeds")]
        public Guid ContentTextEditNeeds { get; set; }

        [TableAttribute.Column("ContentPPTFilePath")]
        public string ContentPPTFilePath { get; set; }

        [TableAttribute.Column("ProductionReceiveDate")]
        public DateTime ProductionReceiveDate { get; set; }

        [TableAttribute.Column("ProductionReceiveNote")]
        public string ProductionReceiveNote { get; set; }

        [TableAttribute.Column("ProductionAssignmentDate")]
        public DateTime ProductionAssignmentDate { get; set; }

        [TableAttribute.Column("ProductionEstimatedDate")]
        public DateTime ProductionEstimatedDate { get; set; }

        [TableAttribute.Column("ProductionFinishDate")]
        public DateTime ProductionFinishDate { get; set; }

        [TableAttribute.Column("ProductionDelayNote")]
        public string ProductionDelayNote { get; set; }

        [TableAttribute.Column("ProductionVideoQuality")]
        public Guid ProductionVideoQuality { get; set; }

        [TableAttribute.Column("ProductionAudioQuality")]
        public Guid ProductionAudioQuality { get; set; }

        [TableAttribute.Column("ProductionFileBackUp")]
        public Guid ProductionFileBackUp { get; set; }

        [TableAttribute.Column("ProductionOperateNote")]
        public string ProductionOperateNote { get; set; }

        [TableAttribute.Column("ProductionVideoEditCheck")]
        public Guid ProductionVideoEditCheck { get; set; }

        [TableAttribute.Column("ProductionAudioEditCheck")]
        public Guid ProductionAudioEditCheck { get; set; }

        [TableAttribute.Column("ProductionProductCheck")]
        public Guid ProductionProductCheck { get; set; }

        [TableAttribute.Column("ProductionIsTimely")]
        public Guid ProductionIsTimely { get; set; }

        [TableAttribute.Column("ProductionCheckDate")]
        public DateTime ProductionCheckDate { get; set; }

        [TableAttribute.Column("ProductionCheckNote")]
        public string ProductionCheckNote { get; set; }

        [TableAttribute.Column("PublishReceiveContentDate")]
        public DateTime PublishReceiveContentDate { get; set; }

        [TableAttribute.Column("PublishReceiveProductionDate")]
        public DateTime PublishReceiveProductionDate { get; set; }

        [TableAttribute.Column("PublishPublishDate")]
        public DateTime PublishPublishDate { get; set; }

        [TableAttribute.Column("PublishTopNewsNeeds")]
        public Guid PublishTopNewsNeeds { get; set; }

        [TableAttribute.Column("PublishNoticeNeeds")]
        public Guid PublishNoticeNeeds { get; set; }

        [TableAttribute.Column("PublishPageState")]
        public Guid PublishPageState { get; set; }

        [TableAttribute.Column("PublishPlayState")]
        public Guid PublishPlayState { get; set; }

        [TableAttribute.Column("PublishCommonCategory")]
        public string PublishCommonCategory { get; set; }

        [TableAttribute.Column("PublishGovernmentCategory")]
        public string PublishGovernmentCategory { get; set; }

        [TableAttribute.Column("PublishFinanceCategory")]
        public string PublishFinanceCategory { get; set; }

        [TableAttribute.Column("PublishBankCategory")]
        public string PublishBankCategory { get; set; }

        [TableAttribute.Column("PublishNote")]
        public string PublishNote { get; set; }

        [TableAttribute.Column("CheckTaskCheckDate")]
        public DateTime CheckTaskCheckDate { get; set; }

        [TableAttribute.Column("CheckTaskCategoryCheck")]
        public Guid CheckTaskCategoryCheck { get; set; }

        [TableAttribute.Column("CheckTaskCourseCommend")]
        public Guid CheckTaskCourseCommend { get; set; }

        [TableAttribute.Column("CheckTaskCourseCancelCommend")]
        public Guid CheckTaskCourseCancelCommend { get; set; }

        [TableAttribute.Column("CheckTaskCancelCommendDate")]
        public DateTime CheckTaskCancelCommendDate { get; set; }

        [TableAttribute.Column("CheckTaskNote")]
        public string CheckTaskNote { get; set; }

        [TableAttribute.Column("CaptureCheckPersonInCharge")]
        public Guid CaptureCheckPersonInCharge { get; set; }

        [TableAttribute.Column("CaptureCheckDate")]
        public DateTime CaptureCheckDate { get; set; }

        [TableAttribute.Column("CaptureCheckNote")]
        public string CaptureCheckNote { get; set; }

        [TableAttribute.Column("ContentRecheckPersonInCharge")]
        public Guid ContentRecheckPersonInCharge { get; set; }

        [TableAttribute.Column("ContentRecheckDate")]
        public DateTime ContentRecheckDate { get; set; }

        [TableAttribute.Column("ContentRecheckNote")]
        public string ContentRecheckNote { get; set; }

        [TableAttribute.Column("ContentReceiveNote")]
        public string ContentReceiveNote { get; set; }

        [TableAttribute.Column("ContentLastModifyDate")]
        public DateTime ContentLastModifyDate { get; set; }

        [TableAttribute.Column("ProductionLastModifyDate")]
        public DateTime ProductionLastModifyDate { get; set; }

        [TableAttribute.Column("SourceProjectId")]
        public Guid SourceProjectId { get; set; }

        [TableAttribute.Column("CaptureReceiveDelayDate")]
        public DateTime CaptureReceiveDelayDate { get; set; }

        [TableAttribute.Column("CaptureReceiveDelayNote")]
        public string CaptureReceiveDelayNote { get; set; }

        [TableAttribute.Column("ProductionReceiveDelayDate")]
        public DateTime ProductionReceiveDelayDate { get; set; }

        [TableAttribute.Column("ProductionReceiveDelayNote")]
        public string ProductionReceiveDelayNote { get; set; }

        [TableAttribute.Column("ExecutionDate")]
        public DateTime ExecutionDate { get; set; }

        [TableAttribute.Column("DeadLine")]
        public DateTime DeadLine { get; set; }

        [TableAttribute.Column("ExtraNote")]
        public string ExtraNote { get; set; }

        [TableAttribute.Column("CanBeSold")]
        public Guid CanBeSold { get; set; }

        [TableAttribute.Column("ContentCheckPersonInCharge")]
        public Guid ContentCheckPersonInCharge { get; set; }

        [TableAttribute.Column("EpisodeCount")]
        public int EpisodeCount { get; set; }

        [TableAttribute.Column("CourseType")]
        public string CourseType { get; set; }
        
        //Select
        [TableAttribute.Column("WorkTypeText")]
        public string WorkTypeText { get; set; }

        [TableAttribute.Column("ProgressText")]
        public string ProgressText { get; set; }

        [TableAttribute.Column("ProjectPlanName")]
        public string ProjectPlanName { get; set; }

        #endregion DataBase

        #region 扩展

        public DateTime ExecuteStartingDate
        {
            get
            {
                if (this.IsCaptureNeeds()) { return this.CaptureFinishDate; }
                else { return this.SendingDate; }
            }
            set
            {
                ExecuteStartingDate = value;
            }
        }

        public DateTime ShorthandStartingDate
        {
            get
            {
                return this.ExecutionDate;
            }
            set
            {
                ShorthandStartingDate = value;
            }
        }

        public bool IsDelay
        {
            get
            {
                if (this.CaptureReceiveDelayDate!= new DateTime(0001, 1, 1, 00, 00, 00) 
                || this.ProductionReceiveDelayDate!= new DateTime(0001, 1, 1, 00, 00, 00)) 
                { 
                    return true; 
                }
                else { return false; }
            }
            set
            {
                IsDelay = value;
            }
        }

        public string ProgressContentText
        {
            get
            {
                string ThisValue = "";
                switch (this.ContentProgress.ToString())
                {
                    case "00000000-0000-0000-0000-000000000107":
                        {
                            ThisValue = "等待制作部接收";
                            break;
                        }
                    case "00000000-0000-0000-0000-000000000111":
                        {
                            ThisValue ="制作部已接收，正在制作";
                            break;
                        }
                    case "00000000-0000-0000-0000-000000000112":
                        {
                            ThisValue ="制作部已制作，等待初审";
                            break;
                        }
                    case "00000000-0000-0000-0000-000000000113":
                        {
                            ThisValue ="制作部复审通过";
                            break;
                        }
                    case "00000000-0000-0000-0000-000000000122":
                        {
                            ThisValue ="制作部初审通过，等待复审";
                            break;
                        }
                    case "00000000-0000-0000-0000-000000000123":
                        {
                            ThisValue ="制作部复审退回，正在申请修改";
                            break;
                        }
                    case "00000000-0000-0000-0000-000000000124":
                        {
                            ThisValue ="审核未通过，等待修改";
                            break;
                        }
                    case "00000000-0000-0000-0000-000000000130":
                        {
                            ThisValue ="制作部未接收";
                            break;
                        }
                    default:
                        {
                            ThisValue = "制作部未接收";
                            break;
                        }         
                };
                return ThisValue;
            }
            set
            {
                ProgressContentText = value;
            }
        }

        public string ProgressProductionText
        {
            get
            {
                string ThisValue = "";
                switch (this.ProductionProgress.ToString())
                {
                    case "00000000-0000-0000-0000-000000000106":
                        {
                            ThisValue ="等待技术部接收";
                            break;
                        }
                    case "00000000-0000-0000-0000-000000000114":
                        {
                            ThisValue ="技术部已接收，正在制作";
                            break;
                        }
                    case "00000000-0000-0000-0000-000000000115":
                        {
                            ThisValue ="技术部已制作，等待审核";
                            break;
                        }
                    case "00000000-0000-0000-0000-000000000116":
                        {
                            ThisValue ="技术部已审核";
                            break;
                        }
                    case "00000000-0000-0000-0000-000000000125":
                        {
                            ThisValue ="审核未通过，等待修改";
                            break;
                        }
                    case "00000000-0000-0000-0000-000000000129":
                        {
                            ThisValue ="技术部未接收";
                            break;
                        }
                    case "00000000-0000-0000-0000-000000000132":
                        {
                            ThisValue ="技术部延迟接收";
                            break;
                        }
                    default:
                        {
                            ThisValue = "技术部未接收";
                            break;
                        }
                };
                return ThisValue;
            }
            set
            {
                ProgressProductionText = value;
            }
        }

        public string ProgressTotalText
        {
            get
            {
                if (this.IsSingleScreen())
                {
                    if(this.ContentNeeds.ToString() == "00000000-0000-0000-0000-000000000042")
                    {
                        if (this.ContentProgress.ToString() == "00000000-0000-0000-0000-000000000000" && this.ProductionProgress.ToString() == "00000000-0000-0000-0000-000000000000")
                        {
                            return ProgressText;
                        }
                        else if (this.ContentProgress.ToString() == "00000000-0000-0000-0000-000000000113" && this.ProductionProgress.ToString() == "00000000-0000-0000-0000-000000000116")
                        {
                            return ProgressText;
                        }
                        else
                        {
                            return ProgressContentText + "；" + ProgressProductionText;
                        }
                    }
                    else
                    {
                        return ProgressText;
                    }
                }
                else
                {
                    return ProgressText;
                }
            }
            set
            {
                ProgressTotalText = value;
            }
        }

        #endregion 扩展

        #endregion 属性

        public Project()
        {

        }
        #region 静态方法

        #region Select

        public static List<Project> GetAllProject()
        {
            DataTable dt = new DAL.Project().SelectAll();
            List<Project> value = new List<Project>();
            Adapt.Convert.ConvertDataTableToObjectList<Project>(dt, value);
            return value;
        }

        public static List<Project> GetAllProjectPage(int PageIndex)
        {
            DataTable dt = new DAL.Project().SelectAllPage(PageIndex);
            List<Project> value = new List<Project>();
            Adapt.Convert.ConvertDataTableToObjectList<Project>(dt, value);
            return value;
        }

        public static List<Project> GetAllProject(Guid PlanTypeId, Guid progress,string date)
        {
            DataTable dt = new DAL.Project().SelectAll(PlanTypeId, progress, date);
            List<Project> value = new List<Project>();
            Adapt.Convert.ConvertDataTableToObjectList<Project>(dt, value);
            return value;
        }

        public static List<Project> GetProjectByPlanId(Guid ProjectPlanId)
        {
            DataTable dt = new DAL.Project().SelectByPlanId(ProjectPlanId);
            List<Project> value = new List<Project>();
            Adapt.Convert.ConvertDataTableToObjectList<Project>(dt, value);
            return value;
        }
     
        public static List<Project> GetCategoryProject(string category)
        {
            DataTable dt = new DAL.Project().SelectCategory(category);
            List<Project> value = new List<Project>();
            Adapt.Convert.ConvertDataTableToObjectList<Project>(dt, value);
            return value;
        }

        public static List<Project> GetDictionaryIdProject(Guid Id)
        {
            DataTable dt = new DAL.Project().SelectDictionaryId(Id);
            List<Project> value = new List<Project>();
            Adapt.Convert.ConvertDataTableToObjectList<Project>(dt, value);
            return value;
        }

        public static List<Project> GetDictionaryIdProject(Guid Id, Guid Id2)
        {
            DataTable dt = new DAL.Project().SelectDictionaryId(Id, Id2);
            List<Project> value = new List<Project>();
            Adapt.Convert.ConvertDataTableToObjectList<Project>(dt, value);
            return value;
        }

        public static List<Project> GetDictionaryContentIdProject(Guid Id, Guid Id2, Guid PersonId)
        {
            DataTable dt = new DAL.Project().SelectDictionaryContentId(Id, Id2, PersonId);
            List<Project> value = new List<Project>();
            Adapt.Convert.ConvertDataTableToObjectList<Project>(dt, value);
            return value;
        }

        public static List<Project> GetDictionaryContentIdProject(Guid Id, Guid Id2)
        {
            DataTable dt = new DAL.Project().SelectDictionaryContentId(Id, Id2);
            List<Project> value = new List<Project>();
            Adapt.Convert.ConvertDataTableToObjectList<Project>(dt, value);
            return value;
        }

        public static List<Project> GetDictionaryProductionIdProject(Guid Id, Guid Id2)
        {
            DataTable dt = new DAL.Project().SelectDictionaryProductionId(Id, Id2);
            List<Project> value = new List<Project>();
            Adapt.Convert.ConvertDataTableToObjectList<Project>(dt, value);
            return value;
        }

        public static List<Project> GetDictionaryProductionIdProject(Guid Id, Guid Id2, Guid PersonId)
        {
            DataTable dt = new DAL.Project().SelectDictionaryProductionId(Id, Id2, PersonId);
            List<Project> value = new List<Project>();
            Adapt.Convert.ConvertDataTableToObjectList<Project>(dt, value);
            return value;
        }

        public static List<Project> GetDictionaryContentIdProject(Guid Id)
        {
            DataTable dt = new DAL.Project().SelectDictionaryContentId(Id);
            List<Project> value = new List<Project>();
            Adapt.Convert.ConvertDataTableToObjectList<Project>(dt, value);
            return value;
        }

        public static List<Project> GetDictionaryProductionIdProject(Guid Id)
        {
            DataTable dt = new DAL.Project().SelectDictionaryProductionId(Id);
            List<Project> value = new List<Project>();
            Adapt.Convert.ConvertDataTableToObjectList<Project>(dt, value);
            return value;
        }

        public static Project GetProject(Guid projectId)
        {
            DataTable table = new DAL.Project().Select(projectId);
            Project value = new Project();
            foreach (DataRow row in table.Rows)
                Adapt.Convert.ConvertDataRowToObject(row, value);
            return value;
        }

        #region Total

        public static int GetProjectTotal()
        {
            int count = new DAL.Project().SelectTotal();
            return count;
        }

        public static int GetProjectTotalToday()
        {
            int count = new DAL.Project().SelectTotalToday();
            return count;
        }

        public static int GetProjectTotalFinish()
        {
            int count = new DAL.Project().SelectTotalFinish();
            return count;
        }

        public static int GetProjectTotalDelay()
        {
            int count = new DAL.Project().SelectTotalDelay();
            return count;
        }

        public static int GetProjectTotalPlanType(Guid TypeId)
        {
            int count = new DAL.Project().SelectTotalPlanType(TypeId);
            return count;
        }

        #endregion Total

        #region Amount
        public static int GetCaptureAmount(Guid UserId, string DateBegin, string DateEnd)
        {
            int count = new DAL.Project().SelectCaptureAmount(UserId, DateBegin, DateEnd);
            return count;
        }
        public static int GetExecutionAmount(Guid UserId, string DateBegin, string DateEnd)
        {
            int count = new DAL.Project().SelectExecutionAmount(UserId, DateBegin, DateEnd);
            return count;
        }
        public static int GetShorthandAmount(Guid UserId, string DateBegin, string DateEnd)
        {
            int count = new DAL.Project().SelectShorthandAmount(UserId, DateBegin, DateEnd);
            return count;
        }
        public static int GetContentReceiveAmount(Guid UserId, string DateBegin, string DateEnd)
        {
            int count = new DAL.Project().SelectContentReceiveAmount(UserId, DateBegin, DateEnd);
            return count;
        }
        public static int GetContentCheckAmount(Guid UserId,string ProjectType, string DateBegin, string DateEnd)
        {
            int count = new DAL.Project().SelectContentCheckAmount(UserId, ProjectType, DateBegin, DateEnd);
            return count;
        }
        public static int GetContentRecheckAmount(Guid UserId, string DateBegin, string DateEnd)
        {
            int count = new DAL.Project().SelectContentRecheckAmount(UserId, DateBegin, DateEnd);
            return count;
        }
        public static int GetContentFinishAmount(Guid UserId, string DateBegin, string DateEnd)
        {
            int count = new DAL.Project().SelectContentFinishAmount(UserId, DateBegin, DateEnd);
            return count;
        }
        public static int GetContentFinishDelayAmount(Guid UserId, string DateBegin, string DateEnd)
        {
            int count = new DAL.Project().SelectContentFinishDelayAmount(UserId, DateBegin, DateEnd);
            return count;
        }
        public static int GetProductionReceiveAmount(Guid UserId, string DateBegin, string DateEnd)
        {
            int count = new DAL.Project().SelectProductionReceiveAmount(UserId, DateBegin, DateEnd);
            return count;
        }
        public static int GetProductionCheckAmount(Guid UserId, string DateBegin, string DateEnd)
        {
            int count = new DAL.Project().SelectProductionCheckAmount(UserId, DateBegin, DateEnd);
            return count;
        }
        public static int GetProductionFinishAmount(Guid UserId, string DateBegin, string DateEnd)
        {
            int count = new DAL.Project().SelectProductionFinishAmount(UserId, DateBegin, DateEnd);
            return count;
        }
        public static int GetProductionFinishDelayAmount(Guid UserId, string DateBegin, string DateEnd)
        {
            int count = new DAL.Project().SelectProductionFinishDelayAmount(UserId, DateBegin, DateEnd);
            return count;
        }
        public static int GetPublishAmount(Guid UserId, string DateBegin, string DateEnd)
        {
            int count = new DAL.Project().SelectPublishAmount(UserId, DateBegin, DateEnd);
            return count;
        }
        public static int GetCheckAmount(Guid UserId, string DateBegin, string DateEnd)
        {
            int count = new DAL.Project().SelectCheckAmount(UserId, DateBegin, DateEnd);
            return count;
        }

        public static DataTable GetManHoursAmount(string ProjectType, string DateBegin, string DateEnd)
        {
            DataTable count = new DAL.Project().SelectManHoursAmount(ProjectType, DateBegin, DateEnd);
            return count;
        }

        public static DataTable GetManHoursCalendarDayAmount(string ProjectType, string DateBegin, string DateEnd)
        {
            DataTable count = new DAL.Project().SelectManHoursCalendarDayAmount(ProjectType, DateBegin, DateEnd);
            return count;
        }
        #endregion Amount

        public static List<Project> GetProjectList(string mode,string UserId)
        {
            List<Project> ProjectList;
            if (mode == "browse")
            {
                ProjectList = BLL.Project.GetAllProject();
            }
            else if (mode == "capture")
            {
                ProjectList = BLL.Project.GetCategoryProject("ProgressCapture");
            }
            else if (mode == "capturecheck")
            {
                ProjectList = BLL.Project.GetCategoryProject("ProgressCaptureCheck");
            }
            else if (mode == "execution")
            {
                ProjectList = BLL.Project.GetDictionaryIdProject(new Guid("00000000-0000-0000-0000-000000000121"));
            }
            else if (mode == "shorthand")
            {
                ProjectList = BLL.Project.GetDictionaryIdProject(new Guid("00000000-0000-0000-0000-000000000109"));
            }
            else if (mode == "contentreceive")
            {
                ProjectList = BLL.Project.GetDictionaryContentIdProject(new Guid("00000000-0000-0000-0000-000000000107"));
            }
            else if (mode == "contentfinish")
            {
                ProjectList = BLL.Project.GetDictionaryContentIdProject(new Guid("00000000-0000-0000-0000-000000000111"), new Guid("00000000-0000-0000-0000-000000000124"), new Guid(UserId));
            }
            else if (mode == "contentcheck")
            {
                ProjectList = BLL.Project.GetDictionaryContentIdProject(new Guid("00000000-0000-0000-0000-000000000112"), new Guid("00000000-0000-0000-0000-000000000123"));
            }
            else if (mode == "contentrecheck")
            {
                ProjectList = BLL.Project.GetDictionaryContentIdProject(new Guid("00000000-0000-0000-0000-000000000122"));
            }
            else if (mode == "productionreceive")
            {
                ProjectList = BLL.Project.GetDictionaryProductionIdProject(new Guid("00000000-0000-0000-0000-000000000106"), new Guid("00000000-0000-0000-0000-000000000132"));
            }
            else if (mode == "productionfinish")
            {
                ProjectList = BLL.Project.GetDictionaryProductionIdProject(new Guid("00000000-0000-0000-0000-000000000114"), new Guid("00000000-0000-0000-0000-000000000125"), new Guid(UserId));
            }
            else if (mode == "productioncheck")
            {
                ProjectList = BLL.Project.GetDictionaryProductionIdProject(new Guid("00000000-0000-0000-0000-000000000115"));
            }
            else if (mode == "publish")
            {
                ProjectList = BLL.Project.GetDictionaryIdProject(new Guid("00000000-0000-0000-0000-000000000117"));
            }
            else if (mode == "check")
            {
                ProjectList = BLL.Project.GetDictionaryIdProject(new Guid("00000000-0000-0000-0000-000000000118"));
            }
            else
            {
                ProjectList = null;
            }
            return ProjectList;
        }
        #endregion Select

        #region Insert
        public static int Insert(Project project)
        {
            int count = 0;
            count = new DAL.Project().Insert(project.ProjectId, project.ProjectPlanId, project.ProjectTypeId, project.ProjectNo, project.emergency, project.WorkType, project.CourseName, project.notice, project.headline, project.TextCategory, project.lecturer, project.LecturerJob, project.progress, project.InCharge, project.CreateNote, project.ExtraNote, project.ContentNeeds, project.PublishNeeds, project.SourceProjectId, project.CanBeSold,project.EpisodeCount);
            return count;
        }
        #endregion Insert

        #region Update
        public static int UpdateExecution(Project project)
        {
            int count = 0;
            count = new DAL.Project().UpdateExecution(project.ProjectId, project.ProjectTypeId, project.ProjectNo, project.emergency, project.WorkType, project.CourseName, project.notice, project.headline, project.TextCategory, project.lecturer, project.LecturerJob, project.progress, project.ContentProgress, project.ProductionProgress, project.CreateNote, project.ExtraNote, project.ExecutionDate,project.EpisodeCount);
            return count;
        }

        public static int UpdateCaptureReceive(Project project)
        {
            int count = 0;
            count = new DAL.Project().UpdateCaptureReceive(project.ProjectId, project.CaptureReceiveDate, project.progress, project.CapturePersonInCharge);
            return count;
        }

        public static int UpdateCaptureFinish(Project project)
        {
            int count = 0;
            count = new DAL.Project().UpdateCaptureFinish(project.ProjectId, project.CaptureDuration, project.progress, project.CaptureFinishDate, project.CaptureVideoNeeds, project.CaptureAudioNeeds, project.CaptureVideoVideoQuality, project.CaptureVideoAudioQuality, project.CaptureAudioQuality, project.CaptureSoundTrack, project.CaptureFilePath, project.CaptureNote);
            return count;
        }

        public static int UpdateCaptureCheckFinish(Project project)
        {
            int count = 0;
            count = new DAL.Project().UpdateCaptureCheckFinish(project.ProjectId, project.progress, project.CaptureCheckPersonInCharge, project.CaptureCheckDate, project.CaptureCheckNote, project.ProjectTypeId, project.ProjectNo, project.emergency, project.WorkType, project.CourseName, project.notice, project.headline, project.TextCategory, project.lecturer, project.LecturerJob,project.CreateNote, project.ExtraNote);
            return count;
        }

        public static int UpdateShorthandFinish(Project project)
        {
            int count = 0;
            count = new DAL.Project().UpdateShorthandFinish(project.ProjectId, project.progress, project.ShorthandPersonInCharge, project.ShorthandFinishDate, project.ShorthandAudioReceiveDate, project.ShorthandPurveyor, project.ShorthandQuality, project.ShorthandNote, project.ContentProgress);
            return count;
        }

        public static int UpdateContentReceive(Project project)
        {
            int count = 0;
            count = new DAL.Project().UpdateContentReceive(project.ProjectId, project.progress, project.ContentPersonInCharge, project.ContentOperator, project.ContentAssignmentDate, project.ContentEstimatedDate, project.ContentReceiveNote, project.ContentProgress);
            return count;
        }

        public static int UpdateContentFinish(Project project)
        {
            int count = 0;
            count = new DAL.Project().UpdateContentFinish(project.ProjectId, project.progress, project.ContentFinishDate, project.ContentDelayNote, project.ContentCourseNameConfirm, project.ContentChangedCourseName, project.ContentCourseRecommend, project.ContentPPTAdvice, project.ContentExercises, project.ContentPPTNeeds, project.ContentCourseIntroNeeds, project.ContentLecturerResumeNeeds, project.ContentExercisesNeeds, project.ContentTextEditNeeds, project.ContentOperateNote, project.ContentProgress);
            return count;
        }

        public static int UpdateContentCheck(Project project)
        {
            int count = 0;
            count = new DAL.Project().UpdateContentCheck(project.ProjectId, project.progress, project.ContentProgress, project.ContentCourseIntroductionQuality, project.ContentResumeQuality, project.ContentPPTQuality, project.ContentExercisesQuality, project.ContentTextQuality, project.ContentIsTimely, project.ContentCheckDate, project.ContentCheckNote,project.ContentCheckPersonInCharge);
            return count;
        }

        public static int UpdateContentRecheckFinish(Project project)
        {
            int count = 0;
            count = new DAL.Project().UpdateContentRecheckFinish(project.ProjectId, project.progress, project.ContentProgress, project.ProductionProgress, project.ContentRecheckPersonInCharge, project.ContentRecheckDate, project.ContentRecheckNote);
            return count;
        }
        public static int UpdateProductionReceive(Project project)
        {
            int count = 0;
            count = new DAL.Project().UpdateProductionReceive(project.ProjectId, project.progress, project.ProductionPersonInCharge, project.ProductionReceiveDate, project.ProductionReceiveNote,project.ProductionAssignmentDate, project.ProductionEstimatedDate, project.ProductionOperator, project.ProductionProgress);
            return count;
        }
        public static int UpdateProductionFinish(Project project)
        {
            int count = 0;
            count = new DAL.Project().UpdateProductionFinish(project.ProjectId, project.progress, project.ProductionLastModifyDate, project.ProductionFinishDate, project.ProductionDelayNote, project.ProductionOperateNote, project.ProductionVideoQuality, project.ProductionAudioQuality, project.ProductionFileBackUp, project.ProductionProgress);
            return count;
        }
        public static int UpdateProductionCheck(Project project)
        {
            int count = 0;
            count = new DAL.Project().UpdateProductionCheck(project.ProjectId, project.progress, project.ProductionProgress,project.ContentProgress, project.ProductionVideoEditCheck, project.ProductionAudioEditCheck, project.ProductionProductCheck, project.ProductionIsTimely, project.ProductionCheckDate, project.ProductionCheckNote);
            return count;
        }
        public static int UpdatePublish(Project project)
        {
            int count = 0;
            count = new DAL.Project().UpdatePublish(project.ProjectId, project.progress, project.PublishOperator, project.PublishPublishDate, project.PublishTopNewsNeeds, project.PublishNoticeNeeds, project.PublishCommonCategory, project.PublishGovernmentCategory, project.PublishFinanceCategory, project.PublishBankCategory, project.PublishPageState, project.PublishPlayState, project.PublishNote);
            return count;
        }
        public static int UpdateCheck(Project project)
        {
            int count = 0;
            count = new DAL.Project().UpdateCheck(project.ProjectId, project.progress, project.CheckPersonInCharge, project.CheckTaskCheckDate, project.CheckTaskCourseCommend, project.CheckTaskCategoryCheck, project.CheckTaskNote);
            return count;
        }
        public static int UpdateCaptureDelayReceive(Project project)
        {
            int count = 0;
            count = new DAL.Project().UpdateCaptureDelayReceive(project.ProjectId, project.progress, project.CapturePersonInCharge, project.CaptureReceiveDelayDate, project.CaptureReceiveDelayNote);
            return count;
        }
        public static int UpdateProductionDelayReceive(Project project)
        {
            int count = 0;
            count = new DAL.Project().UpdateProductionDelayReceive(project.ProjectId, project.progress, project.ProductionProgress, project.ProductionPersonInCharge, project.ProductionReceiveDelayDate, project.ProductionReceiveDelayNote);
            return count;
        }
        #endregion Update

        #endregion 静态方法

        #region 方法
        public bool IsSingleScreen()
        {
            if (//单视频
                this.ProjectTypeId == new Guid("00000000-0000-0000-0000-000000000017")
                || this.ProjectTypeId == new Guid("00000000-0000-0000-0000-000000000019")
                )
            { return true; }
            else { return false; }
        }
        public bool IsCaptureNeeds()
        {
            if (//无采集
            this.ProjectTypeId == new Guid("00000000-0000-0000-0000-000000000019")
            || this.ProjectTypeId == new Guid("00000000-0000-0000-0000-000000000038")
            || this.ProjectTypeId == new Guid("00000000-0000-0000-0000-000000000039")
                )
            { return false; }
            else { return true; }
        }
        public bool IsShorthandNeeds()
        {
            if (//无速记
            this.ProjectTypeId == new Guid("00000000-0000-0000-0000-000000000017")
            || this.ProjectTypeId == new Guid("00000000-0000-0000-0000-000000000019")
            || this.ProjectTypeId == new Guid("00000000-0000-0000-0000-000000000021")
            || this.ProjectTypeId == new Guid("00000000-0000-0000-0000-000000000037")
            || this.ProjectTypeId == new Guid("00000000-0000-0000-0000-000000000038")
                )
            { return false; }
            else { return true; }
        }
        //
        public bool IsCaptureFinished()
        {
            if (this.CaptureFinishDate != new DateTime(0001, 1, 1, 00, 00, 00)) { return true; }
            else { return false; }
        }
        public bool IsCaptureCheckFinished()
        {
            return true;
        }
        public bool IsExecutFinished()
        {
            if (this.ExecutionDate != new DateTime(0001, 1, 1, 00, 00, 00)) { return true; }
            else { return false; }
        }
        public bool IsShorthandFinished()
        {
            if (this.ShorthandFinishDate != new DateTime(0001, 1, 1, 00, 00, 00)) { return true; }
            else { return false; }
        }
        //
        protected string IsProgressCaptureTimeout(out bool IsFinish)
        {
            IsFinish = false;
            string TimeSpan;
            if (this.IsCaptureNeeds() == true)
            {
                if (!this.IsCaptureFinished())
                {
                    TimeSpan = DateTimeHandle.TimeOut(DateTime.Now, this.SendingDate, Convert.ToInt32(ConfigurationManager.AppSettings["TimeSpanCapture"]));
                    if (TimeSpan != "0")
                    { return TimeSpan; }
                    else { return "0"; }
                }
                else
                {
                    IsFinish = true;
                    TimeSpan = DateTimeHandle.TimeOut(this.CaptureFinishDate, this.SendingDate, Convert.ToInt32(ConfigurationManager.AppSettings["TimeSpanCapture"]));
                    if (TimeSpan != "0")
                    { return TimeSpan; }
                    else { return "0"; }
                }
            }
            else { return "NoNeed"; }
        }
        protected string IsProgressExecutedTimeout(out bool IsFinish)
        {
            IsFinish = false;
            string TimeSpan;
            if (!this.IsExecutFinished())
            {
                TimeSpan = DateTimeHandle.TimeOut(DateTime.Now, this.ExecuteStartingDate, Convert.ToInt32(ConfigurationManager.AppSettings["TimeSpanExecution"]));
                if (TimeSpan != "0") { return TimeSpan; }
                else { return "0"; }
            }
            else
            {
                IsFinish = true;
                TimeSpan = DateTimeHandle.TimeOut(this.ExecutionDate, this.ExecuteStartingDate, Convert.ToInt32(ConfigurationManager.AppSettings["TimeSpanExecution"]));
                if (TimeSpan != "0") { return TimeSpan; }
                else { return "0"; }
            }
        }
        protected string IsProgressShorthandTimeout(out bool IsFinish)
        {
            IsFinish = false;
            string TimeSpan;
            if (this.IsShorthandNeeds() == true)
            {
                if (!this.IsExecutFinished())
                {
                    TimeSpan = DateTimeHandle.TimeOut(DateTime.Now, this.ShorthandStartingDate, Convert.ToInt32(ConfigurationManager.AppSettings["TimeSpanExecution"]));
                    if (TimeSpan != "0") { return TimeSpan; }
                    else { return "0"; }
                }
                else
                {
                    IsFinish = true;
                    TimeSpan = DateTimeHandle.TimeOut(this.ExecutionDate, this.ShorthandStartingDate, Convert.ToInt32(ConfigurationManager.AppSettings["TimeSpanExecution"]));
                    if (TimeSpan != "0") { return TimeSpan; }
                    else { return "0"; }
                }
            }
            else { return "NoNeed"; }
        }
        //
        protected bool IsProgressTimeout()
        {
            bool IsFinish = false;
            //
            if (this.IsProgressCaptureTimeout(out IsFinish) == "0" || this.IsProgressCaptureTimeout(out IsFinish) == "NoNeed") { }
            else { return true; }
            //
            if (this.IsProgressExecutedTimeout(out IsFinish) == "0") { }
             else { return true; }
            //
            return false;
        }
        protected bool IsFinalTimeout(Guid ProjectId)
        {
            return false;
        }
        #endregion 方法
    }
}
