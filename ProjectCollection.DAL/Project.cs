using Adapt.Database;
using System;
using System.Data;
using System.Data.Common;

namespace ProjectCollection.DAL
{
    /// <summary>
    /// 
    /// </summary>
    public class Project : BaseDatabase
    {
        #region sql

        #region SELECT
        //
        private const string SELECT_01 = @"select
ProjectId
,Project.ProjectPlanId
,ProjectTypeId
,ProjectNo
,SendingDate
,emergency
,WorkType
,CourseName
,lecture
,notice
,headline
,TextCategory
,Project.lecturer
,Project.LecturerJob
,Project.progress
,CaptureNeeds
,ContentNeeds
,productCourseType
,PublishNeeds
,CapturePersonInCharge
,CaptureOperator
,CaptureProgress
,CaptureFeedBackNote
,ShorthandPersonInCharge
,ShorthandOperator
,ShorthandProgress
,ShorthandFeedBackNote
,ContentPersonInCharge
,ContentOperator
,ContentProgress
,ContentFeedBackNote
,ProductionPersonInCharge
,ProductionOperator
,ProductionProgress
,ProductionFeedBackNote
,PublishPersonInCharge
,PublishOperator
,PublishProgress
,PublishFeedBackNote
,CheckPersonInCharge
,CheckOperator
,CheckProgress
,CheckFeedBackNote
,CaptureDuration
,CaptureReceiveDate
,CaptureFinishDate
,CaptureVideoNeeds
,CaptureAudioNeeds
,CaptureVideoVideoQuality
,CaptureVideoAudioQuality
,CaptureAudioQuality
,CaptureSoundTrack
,CaptureFilePath
,CaptureNote
,ShorthandReceiveDate
,ShorthandAudioReceiveDate
,ShorthandFinishDate
,ShorthandPurveyor
,ShorthandQuality
,ShorthandNote
,ShorthandFilePath
,ContentReceiveDate
,ContentAssignmentDate
,ContentEstimatedDate
,ContentFinishDate
,ContentDelayNote
,ContentCourseNameConfirm
,ContentChangedCourseName
,ContentCourseRecommend
,ContentPPTAdvice
,ContentExercises
,ContentOperateNote
,ContentCourseIntroductionQuality
,ContentResumeQuality
,ContentPPTQuality
,ContentExercisesQuality
,ContentTextQuality
,ContentIsTimely
,ContentCheckDate
,ContentCheckNote
,ContentPPTNeeds
,ContentCourseIntroNeeds
,ContentLecturerResumeNeeds
,ContentExercisesNeeds
,ContentTextEditNeeds
,ContentPPTFilePath
,ProductionReceiveDate
,ProductionAssignmentDate
,ProductionEstimatedDate
,ProductionFinishDate
,ProductionDelayNote
,ProductionVideoQuality
,ProductionAudioQuality
,ProductionFileBackUp
,ProductionOperateNote
,ProductionVideoEditCheck
,ProductionAudioEditCheck
,ProductionProductCheck
,ProductionCheckDate
,ProductionCheckNote
,PublishReceiveContentDate
,PublishReceiveProductionDate
,PublishPublishDate
,PublishTopNewsNeeds
,PublishNoticeNeeds
,PublishPageState
,PublishPlayState
,PublishCommonCategory
,PublishGovernmentCategory
,PublishFinanceCategory
,PublishBankCategory
,PublishNote
,CheckTaskCheckDate
,CheckTaskCategoryCheck
,CheckTaskCourseCommend
,CheckTaskCancelCommendDate
,InCharge
,CreateNote
,ProductionIsTimely
,CheckTaskCourseCancelCommend
,CheckTaskNote
,CaptureCheckPersonInCharge
,CaptureCheckDate
,CaptureCheckNote
,ContentRecheckDate
,ContentRecheckNote
,ContentRecheckPersonInCharge
,ContentReceiveNote
,ContentLastModifyDate
,ProductionLastModifyDate
,SourceProjectId
,CaptureReceiveDelayDate
,CaptureReceiveDelayNote
,ProductionReceiveDelayDate
,ProductionReceiveDelayNote
,ExecutionDate
,DeadLine
,Project.ExtraNote
,ProductionReceiveNote
,CanBeSold
,dd1.text as WorkTypeText,dd2.text as ProgressText,pl.title as ProjectPlanName,pl.ProjectPlanTypeId as PlanTypeId 
,ROW_NUMBER() OVER (order by ProjectNo) as RowNo
from Project 
left join data_dictionary as dd1 on dd1.dictionary_identity=Project.WorkType left join data_dictionary as dd2 on dd2.dictionary_identity=Project.progress left join ProjectPlan as pl on pl.ProjectPlanId=Project.ProjectPlanId";
        private const string SELECT_01_All = SELECT_01 + " order by ProjectNo";
        private const string SELECT_ALLPage = @"select top 8 * from (" + SELECT_01 + ") as p where RowNo > @PageIndex*8";
        private const string SELECT_01_PlanId = SELECT_01 + " where Project.ProjectPlanId=@ProjectPlanId order by ProjectNo";
        //
        private const string SELECT_02 = SELECT_01 + " where ProjectId=@ProjectId";
        private const string SELECT_03 = SELECT_01 + " where dd2.category=@category order by ProjectNo";
        private const string SELECT_04 = SELECT_01 + " where dd2.dictionary_identity=@dictionary_identity";
        private const string SELECT_04_Order = SELECT_01 + " where dd2.dictionary_identity=@dictionary_identity order by ProjectNo";
        private const string SELECT_05 = SELECT_04 + " or dd2.dictionary_identity=@dictionary_identity2 order by ProjectNo Desc";
        private const string SELECT_05_person = SELECT_09 + " where (dd2.dictionary_identity=@dictionary_identity or dd2.dictionary_identity=@dictionary_identity2) and ContentOperator=@person_id order by ProjectNo";
        private const string SELECT_05_person2 = SELECT_10 + " where (dd2.dictionary_identity=@dictionary_identity or dd2.dictionary_identity=@dictionary_identity2) and ProductionOperator=@person_id order by ProjectNo";
        private const string SELECT_05_noperson = SELECT_09 + " where (dd2.dictionary_identity=@dictionary_identity or dd2.dictionary_identity=@dictionary_identity2) order by ProjectNo";
        private const string SELECT_05_noperson2 = SELECT_10 + " where (dd2.dictionary_identity=@dictionary_identity or dd2.dictionary_identity=@dictionary_identity2) order by ProjectNo";
        private const string SELECT_06 = @"select *,dd1.text as WorkTypeText,dd2.text as ProgressText,pl.title as ProjectPlanName from Project 
left join ProjectPlan as pl on pl.ProjectPlanId=Project.ProjectPlanId left join data_dictionary as dd1 on dd1.dictionary_identity=Project.WorkType left join data_dictionary as dd2";
        private const string SELECT_07 = SELECT_06 + " on dd2.dictionary_identity=ContentProgress where dd2.dictionary_identity=@dictionary_identity order by ProjectNo";
        private const string SELECT_08 = SELECT_06 + " on dd2.dictionary_identity=ProductionProgress where dd2.dictionary_identity=@dictionary_identity order by ProjectNo";
        private const string SELECT_09 = SELECT_06 + " on dd2.dictionary_identity=ContentProgress";
        private const string SELECT_10 = SELECT_06 + " on dd2.dictionary_identity=ProductionProgress";
        //
        private const string SELECT_Where_PlanTypeId = " PlanTypeId=@PlanTypeId";
        private const string SELECT_Order_No = " order by ProjectNo";
        //
        private const string SELECT_Total = "select count(ProjectId) from Project";
        private const string SELECT_Total_Today = "select count(ProjectId) from Project where CONVERT(varchar(10),SendingDate,120) = CONVERT(varchar(10),getDate(),120)";
        private const string SELECT_Total_Finish = "select count(ProjectId) from Project where progress in('00000000-0000-0000-0000-000000000119','00000000-0000-0000-0000-000000000128')";
        private const string SELECT_Total_Delay = "select count(ProjectId) from Project where CaptureReceiveDelayDate is not null or ProductionReceiveDelayDate is not null";
        private const string SELECT_Total_PlanType = @"select count(ProjectId) from Project
left join ProjectPlan on ProjectPlan.ProjectPlanId=Project.ProjectPlanId
where ProjectPlan.ProjectPlanTypeId=@ProjectPlanTypeId and datepart(yy, SendingDate) = datepart(yy, GETDATE())";

        #region Amount
        private const string Select_AmountCapture = @"select count(ProjectId) from Project
where CaptureFinishDate between @DateBegin and @DateEnd
and CapturePersonInCharge=@CapturePersonInCharge";
        private const string Select_AmountExecution = @"select count(ProjectId) from Project
where ExecutionDate between @DateBegin and @DateEnd
and CaptureCheckPersonInCharge=@CaptureCheckPersonInCharge";
        private const string Select_AmountShorthand = @"select count(ProjectId) from Project
where ShorthandFinishDate between @DateBegin and @DateEnd
and ShorthandPersonInCharge=@ShorthandPersonInCharge";
        private const string Select_AmountContentReceive = @"select count(ProjectId) from Project
where ContentAssignmentDate between @DateBegin and @DateEnd
and ContentPersonInCharge=@ContentPersonInCharge";
        private const string Select_AmountContentCheck = @"select count(ProjectId) from Project
where ContentCheckDate between @DateBegin and @DateEnd
and ContentPersonInCharge=@ContentPersonInCharge";
        private const string Select_AmountContentRecheck = @"select count(ProjectId) from Project
where ContentRecheckDate between @DateBegin and @DateEnd
and ContentRecheckPersonInCharge=@ContentRecheckPersonInCharge";
        private const string Select_AmountContentFinish = @"select count(ProjectId) from Project
where (ContentFinishDate between @DateBegin and @DateEnd)
and (ContentOperator=@ContentOperator)";
        private const string Select_AmountContentFinishDelay = @"select count(ProjectId) from Project
where (ContentFinishDate between @DateBegin and @DateEnd)
and (ContentOperator=@ContentOperator) and (ContentFinishDate>ContentEstimatedDate)";
        private const string Select_AmountProductionReceive = @"select count(ProjectId) from Project
where ProductionReceiveDate between @DateBegin and @DateEnd
and ProductionPersonInCharge=@ProductionPersonInCharge";
        private const string Select_AmountProductionCheck = @"select count(ProjectId) from Project
where ProductionCheckDate between @DateBegin and @DateEnd
and ProductionPersonInCharge=@ProductionPersonInCharge";
        private const string Select_AmountProductionFinish = @"select count(ProjectId) from Project
where (ProductionFinishDate between @DateBegin and @DateEnd)
and (ProductionOperator=@ProductionOperator)";
        private const string Select_AmountProductionFinishDelay = @"select count(ProjectId) from Project
where (ProductionFinishDate between @DateBegin and @DateEnd)
and (ProductionOperator=@ProductionOperator)
and (ProductionFinishDate>ProductionEstimatedDate)";
        private const string Select_AmountPublish = @"select count(ProjectId) from Project
where PublishPublishDate between @DateBegin and @DateEnd
and PublishOperator=@PublishOperator";
        private const string Select_AmountCheck = @"select count(ProjectId) from Project
where CheckTaskCheckDate between @DateBegin and @DateEnd
and CheckPersonInCharge=@ShorthandPersonInCharge";
        #endregion Amount

        #endregion SELECT

        #region INSERT
        private const string INSERT_01 = @"insert into Project(
ProjectId 
,ProjectPlanId              
,ProjectTypeId               
,ProjectNo                     
,SendingDate                 
,emergency                     
,WorkType                       
,CourseName            
,notice                     
,headline                    
,TextCategory            
,lecturer                        
,LecturerJob                  
,progress
,InCharge
,CreateNote
,ExtraNote
,ContentNeeds
,PublishNeeds
,SourceProjectId
,CanBeSold
) values(
@ProjectId       
,@ProjectPlanId                 
,@ProjectTypeId               
,@ProjectNo                     
,@SendingDate                 
,@emergency                     
,@WorkType                       
,@CourseName            
,@notice                     
,@headline                    
,@TextCategory            
,@lecturer                        
,@LecturerJob                  
,@progress
,@InCharge
,@CreateNote
,@ExtraNote
,@ContentNeeds
,@PublishNeeds
,@SourceProjectId
,@CanBeSold
)";
        # endregion

        #region UPDATE

        private const string UPDATE_Execution = @"update Project set 
ProjectTypeId=@ProjectTypeId          
,ProjectNo=@ProjectNo                 
,emergency=@emergency                     
,WorkType=@WorkType                       
,CourseName=@CourseName            
,notice=@notice                     
,headline=@headline                    
,TextCategory=@TextCategory            
,lecturer=@lecturer                        
,LecturerJob=@LecturerJob                  
,progress=@progress
,ContentProgress=@ContentProgress
,ProductionProgress=@ProductionProgress
,CreateNote=@CreateNote
,ExtraNote=@ExtraNote
,ExecutionDate=@ExecutionDate
where ProjectId=@ProjectId";
        private const string UPDATE_CaptureReceive = @"update Project set 
progress=@progress
,CapturePersonInCharge=@CapturePersonInCharge
,CaptureReceiveDate=@CaptureReceiveDate
where ProjectId=@ProjectId";
        private const string UPDATE_CaptureDelayReceive = @"update Project set 
progress=@progress
,CapturePersonInCharge=@CapturePersonInCharge
,CaptureReceiveDelayDate=@CaptureReceiveDelayDate
,CaptureReceiveDelayNote=@CaptureReceiveDelayNote
where ProjectId=@ProjectId";
        private const string UPDATE_CaptureFinish = @"update Project set 
progress=@progress
,CaptureDuration=@CaptureDuration
,CaptureFinishDate=@CaptureFinishDate
,CaptureVideoNeeds=@CaptureVideoNeeds
,CaptureAudioNeeds=@CaptureAudioNeeds
,CaptureVideoVideoQuality=@CaptureVideoVideoQuality
,CaptureVideoAudioQuality=@CaptureVideoAudioQuality
,CaptureAudioQuality=@CaptureAudioQuality
,CaptureSoundTrack=@CaptureSoundTrack
,CaptureFilePath=@CaptureFilePath
,CaptureNote=@CaptureNote
where ProjectId=@ProjectId";
        private const string UPDATE_CaptureCheckFinish = @"update Project set 
progress=@progress
,CaptureCheckPersonInCharge=@CaptureCheckPersonInCharge
,CaptureCheckDate=@CaptureCheckDate
,CaptureCheckNote=@CaptureCheckNote
,ProjectTypeId=@ProjectTypeId 
,ProjectNo=@ProjectNo                     
,emergency=@emergency                     
,WorkType=@WorkType                       
,CourseName=@CourseName            
,notice=@notice                     
,headline=@headline                    
,TextCategory=@TextCategory            
,lecturer=@lecturer                        
,LecturerJob=@LecturerJob
,CreateNote=@CreateNote
,ExtraNote=@ExtraNote
where ProjectId=@ProjectId";
        private const string UPDATE_ShorthandFinish = @"update Project set 
progress=@progress
,ShorthandPersonInCharge=@ShorthandPersonInCharge
,ShorthandAudioReceiveDate=@ShorthandAudioReceiveDate
,ShorthandFinishDate=@ShorthandFinishDate
,ShorthandPurveyor=@ShorthandPurveyor
,ShorthandQuality=@ShorthandQuality
,ShorthandNote=@ShorthandNote
,ContentProgress=@ContentProgress
where ProjectId=@ProjectId";
        private const string UPDATE_ContentReceive = @"update Project set 
progress=@progress
,ContentProgress=@ContentProgress
,ContentPersonInCharge=@ContentPersonInCharge
,ContentOperator=@ContentOperator
,ContentAssignmentDate=@ContentAssignmentDate
,ContentEstimatedDate=@ContentEstimatedDate
,ContentReceiveNote=@ContentReceiveNote
where ProjectId=@ProjectId";
        private const string UPDATE_ContentFinish = @"update Project set 
progress=@progress
,ContentProgress=@ContentProgress
,ContentFinishDate=@ContentFinishDate
,ContentDelayNote=@ContentDelayNote
,ContentCourseNameConfirm=@ContentCourseNameConfirm
,ContentChangedCourseName=@ContentChangedCourseName
,ContentCourseRecommend=@ContentCourseRecommend
,ContentPPTAdvice=@ContentPPTAdvice
,ContentExercises=@ContentExercises
,ContentPPTNeeds=@ContentPPTNeeds
,ContentCourseIntroNeeds=@ContentCourseIntroNeeds
,ContentLecturerResumeNeeds=@ContentLecturerResumeNeeds
,ContentExercisesNeeds=@ContentExercisesNeeds
,ContentTextEditNeeds=@ContentTextEditNeeds
,ContentOperateNote=@ContentOperateNote
where ProjectId=@ProjectId";
        private const string UPDATE_ContentCheck = @"update Project set 
progress=@progress
,ContentProgress=@ContentProgress
,ContentCourseIntroductionQuality=@ContentCourseIntroductionQuality
,ContentResumeQuality=@ContentResumeQuality
,ContentPPTQuality=@ContentPPTQuality
,ContentExercisesQuality=@ContentExercisesQuality
,ContentTextQuality=@ContentTextQuality
,ContentIsTimely=@ContentIsTimely
,ContentCheckDate=@ContentCheckDate
,ContentCheckNote=@ContentCheckNote
where ProjectId=@ProjectId";
        private const string UPDATE_ContentRecheckFinish = @"update Project set 
progress=@progress
,ContentProgress=@ContentProgress
,ProductionProgress=@ProductionProgress
,ContentRecheckPersonInCharge=@ContentRecheckPersonInCharge
,ContentRecheckDate=@ContentRecheckDate
,ContentRecheckNote=@ContentRecheckNote
where ProjectId=@ProjectId";
        private const string UPDATE_ProductionReceive = @"update Project set 
progress=@progress
,ProductionProgress=@ProductionProgress
,ProductionPersonInCharge=@ProductionPersonInCharge
,ProductionReceiveDate=@ProductionReceiveDate
,ProductionReceiveNote=@ProductionReceiveNote
,ProductionAssignmentDate=@ProductionAssignmentDate
,ProductionEstimatedDate=@ProductionEstimatedDate
,ProductionOperator=@ProductionOperator
where ProjectId=@ProjectId";
        private const string UPDATE_ProductionDelayReceive = @"update Project set 
progress=@progress
,ProductionProgress=@ProductionProgress
,ProductionPersonInCharge=@ProductionPersonInCharge
,ProductionReceiveDelayDate=@ProductionReceiveDelayDate
,ProductionReceiveDelayNote=@ProductionReceiveDelayNote
where ProjectId=@ProjectId";
        private const string UPDATE_ProductionFinish = @"update Project set 
progress=@progress
,ProductionProgress=@ProductionProgress
,ProductionLastModifyDate=@ProductionLastModifyDate
,ProductionFinishDate=@ProductionFinishDate
,ProductionDelayNote=@ProductionDelayNote
,ProductionOperateNote=@ProductionOperateNote
,ProductionVideoQuality=@ProductionVideoQuality
,ProductionAudioQuality=@ProductionAudioQuality
,ProductionFileBackUp=@ProductionFileBackUp
where ProjectId=@ProjectId";
        private const string UPDATE_ProductionCheck = @"update Project set 
progress=@progress
,ProductionProgress=@ProductionProgress
,ProductionVideoEditCheck=@ProductionVideoEditCheck
,ProductionAudioEditCheck=@ProductionAudioEditCheck
,ProductionProductCheck=@ProductionProductCheck
,ProductionIsTimely=@ProductionIsTimely
,ProductionCheckDate=@ProductionCheckDate
,ProductionCheckNote=@ProductionCheckNote
where ProjectId=@ProjectId";
        private const string UPDATE_Publish = @"update Project set 
progress=@progress
,PublishOperator=@PublishOperator
,PublishPublishDate=@PublishPublishDate
,PublishTopNewsNeeds=@PublishTopNewsNeeds
,PublishNoticeNeeds=@PublishNoticeNeeds
,PublishCommonCategory=@PublishCommonCategory
,PublishGovernmentCategory=@PublishGovernmentCategory
,PublishFinanceCategory=@PublishFinanceCategory
,PublishBankCategory=@PublishBankCategory
,PublishPageState=@PublishPageState
,PublishPlayState=@PublishPlayState
,PublishNote=@PublishNote
where ProjectId=@ProjectId";
        private const string UPDATE_Check = @"update Project set 
progress=@progress
,CheckPersonInCharge=@CheckPersonInCharge
,CheckTaskCheckDate=@CheckTaskCheckDate
,CheckTaskCourseCommend=@CheckTaskCourseCommend
,CheckTaskCategoryCheck=@CheckTaskCategoryCheck
,CheckTaskNote=@CheckTaskNote
where ProjectId=@ProjectId";
        #endregion

        #endregion sql

        #region select
        //
        public DataTable SelectAll()
        {
            DataTable table = this.SelectOperate.Select(Project.SELECT_01_All);
            return table;
        }
        public DataTable SelectAllPage(int PageIndex)
        {
            DbParameter[] parameters = new DbParameter[1];
            parameters[0] = Manager.CreateParameter("PageIndex", PageIndex);
            DataTable table = this.SelectOperate.Select(Project.SELECT_ALLPage,parameters);
            return table;
        }
        public DataTable SelectAll(Guid PlanTypeId, Guid progress)
        {
            string sql = Project.SELECT_01;
            if (PlanTypeId == new Guid("00000000-0000-0000-0000-000000000000")) { sql = sql + " where 1=1"; }
            else if (PlanTypeId == new Guid("00000000-0000-0000-0000-000000000020")) { sql = sql + " where (ProjectPlanTypeId='00000000-0000-0000-0000-000000000020' or ProjectPlanTypeId='00000000-0000-0000-0000-000000000015')"; }
            else if (PlanTypeId == new Guid("00000000-0000-0000-0000-000000000016")) { sql = sql + " where ProjectPlanTypeId='00000000-0000-0000-0000-000000000016'"; }
            else if (PlanTypeId == new Guid("00000000-0000-0000-0000-000000000014")) { sql = sql + " where ProjectPlanTypeId='00000000-0000-0000-0000-000000000014'"; }
            else if (PlanTypeId == new Guid("00000000-0000-0000-0000-000000000201")) { sql = sql + " where (ProjectPlanTypeId='00000000-0000-0000-0000-000000000200' or ProjectPlanTypeId='00000000-0000-0000-0000-000000000201')"; }
            else { }
            if (progress == new Guid("00000000-0000-0000-0000-000000000000")) { }
            else if (progress == new Guid("99999999-9999-9999-9999-999999999999")) { sql = sql + " and (Project.progress<>'00000000-0000-0000-0000-000000000119' and Project.progress<>'00000000-0000-0000-0000-000000000128')"; }
            else if (progress == new Guid("00000000-0000-0000-0000-000000000119")) { sql = sql + " and Project.progress='" + progress.ToString() + "'"; }
            else if (progress == new Guid("00000000-0000-0000-0000-000000000128")) { sql = sql + " and Project.progress='" + progress.ToString() + "'"; }    
            else { }
            DataTable table = this.SelectOperate.Select(sql + SELECT_Order_No);
            return table;
        }
        public DataTable SelectByPlanId(Guid ProjectPlanId)
        {
            DbParameter[] parameters = new DbParameter[1];
            parameters[0] = Manager.CreateParameter("ProjectPlanId", ProjectPlanId);
            DataTable table = this.SelectOperate.Select(Project.SELECT_01_PlanId, parameters);
            return table;
        }
        //
        public DataTable SelectCategory(String category)
        {
            DbParameter[] parameters = new DbParameter[1];
            parameters[0] = Manager.CreateParameter("category", category);
            DataTable table = this.SelectOperate.Select(Project.SELECT_03, parameters);
            return table;
        }

        public DataTable SelectDictionaryId(Guid Id)
        {
            DbParameter[] parameters = new DbParameter[1];
            parameters[0] = Manager.CreateParameter("dictionary_identity", Id);
            DataTable table = this.SelectOperate.Select(Project.SELECT_04, parameters);
            return table;
        }

        public DataTable SelectDictionaryId(Guid Id, Guid Id2)
        {
            DbParameter[] parameters = new DbParameter[2];
            parameters[0] = Manager.CreateParameter("dictionary_identity", Id);
            parameters[1] = Manager.CreateParameter("dictionary_identity2", Id2);
            DataTable table = this.SelectOperate.Select(Project.SELECT_05, parameters);
            return table;
        }
        public DataTable SelectDictionaryContentId(Guid Id, Guid Id2, Guid PersonId)
        {
            DbParameter[] parameters = new DbParameter[3];
            parameters[0] = Manager.CreateParameter("dictionary_identity", Id);
            parameters[1] = Manager.CreateParameter("dictionary_identity2", Id2);
            parameters[2] = Manager.CreateParameter("person_id", PersonId);
            DataTable table = this.SelectOperate.Select(Project.SELECT_05_person, parameters);
            return table;
        }
        public DataTable SelectDictionaryContentId(Guid Id, Guid Id2)
        {
            DbParameter[] parameters = new DbParameter[2];
            parameters[0] = Manager.CreateParameter("dictionary_identity", Id);
            parameters[1] = Manager.CreateParameter("dictionary_identity2", Id2);
            DataTable table = this.SelectOperate.Select(Project.SELECT_05_noperson, parameters);
            return table;
        }
        public DataTable SelectDictionaryProductionId(Guid Id, Guid Id2)
        {
            DbParameter[] parameters = new DbParameter[2];
            parameters[0] = Manager.CreateParameter("dictionary_identity", Id);
            parameters[1] = Manager.CreateParameter("dictionary_identity2", Id2);
            DataTable table = this.SelectOperate.Select(Project.SELECT_05_noperson2, parameters);
            return table;
        }
        public DataTable SelectDictionaryProductionId(Guid Id, Guid Id2, Guid PersonId)
        {
            DbParameter[] parameters = new DbParameter[3];
            parameters[0] = Manager.CreateParameter("dictionary_identity", Id);
            parameters[1] = Manager.CreateParameter("dictionary_identity2", Id2);
            parameters[2] = Manager.CreateParameter("person_id", PersonId);
            DataTable table = this.SelectOperate.Select(Project.SELECT_05_person2, parameters);
            return table;
        }
        public DataTable SelectDictionaryContentId(Guid Id)
        {
            DbParameter[] parameters = new DbParameter[1];
            parameters[0] = Manager.CreateParameter("dictionary_identity", Id);
            DataTable table = this.SelectOperate.Select(Project.SELECT_07, parameters);
            return table;
        }
        public DataTable SelectDictionaryProductionId(Guid Id)
        {
            DbParameter[] parameters = new DbParameter[1];
            parameters[0] = Manager.CreateParameter("dictionary_identity", Id);
            DataTable table = this.SelectOperate.Select(Project.SELECT_08, parameters);
            return table;
        }

        public DataTable Select(Guid projectId)
        {
            DbParameter[] parameters = new DbParameter[1];
            parameters[0] = Manager.CreateParameter("ProjectId", projectId);
            DataTable table = this.SelectOperate.Select(Project.SELECT_02, parameters);
            return table;
        }

        public int SelectTotal()
        {
            DataTable table = this.SelectOperate.Select(Project.SELECT_Total);
            int count = Convert.ToInt32(table.Rows[0][0]);
            return count;
        }

        public int SelectTotalToday()
        {
            DataTable table = this.SelectOperate.Select(Project.SELECT_Total_Today);
            int count = Convert.ToInt32(table.Rows[0][0]);
            return count;
        }

        public int SelectTotalFinish()
        {
            DataTable table = this.SelectOperate.Select(Project.SELECT_Total_Finish);
            int count = Convert.ToInt32(table.Rows[0][0]);
            return count;
        }

        public int SelectTotalDelay()
        {
            DataTable table = this.SelectOperate.Select(Project.SELECT_Total_Delay);
            int count = Convert.ToInt32(table.Rows[0][0]);
            return count;
        }

        public int SelectTotalPlanType(Guid TypeId)
        {
            DbParameter[] parameters = new DbParameter[1];
            parameters[0] = Manager.CreateParameter("ProjectPlanTypeId", TypeId);
            DataTable table = this.SelectOperate.Select(Project.SELECT_Total_PlanType,parameters);
            int count = Convert.ToInt32(table.Rows[0][0]);
            return count;
        }

        #region Amount
        public int SelectCaptureAmount(Guid CapturePersonInCharge, string DateBegin, string DateEnd)
        {
            DbParameter[] parameters = new DbParameter[3];
            parameters[0] = Manager.CreateParameter("CapturePersonInCharge", CapturePersonInCharge);
            parameters[1] = Manager.CreateParameter("DateBegin", DateBegin);
            parameters[2] = Manager.CreateParameter("DateEnd", DateEnd);
            DataTable table = this.SelectOperate.Select(Project.Select_AmountCapture, parameters);
            int count = Convert.ToInt32(table.Rows[0][0]);
            return count;
        }
        public int SelectExecutionAmount(Guid CaptureCheckPersonInCharge, string DateBegin, string DateEnd)
        {
            DbParameter[] parameters = new DbParameter[3];
            parameters[0] = Manager.CreateParameter("CaptureCheckPersonInCharge", CaptureCheckPersonInCharge);
            parameters[1] = Manager.CreateParameter("DateBegin", DateBegin);
            parameters[2] = Manager.CreateParameter("DateEnd", DateEnd);
            DataTable table = this.SelectOperate.Select(Project.Select_AmountExecution, parameters);
            int count = Convert.ToInt32(table.Rows[0][0]);
            return count;
        }
        public int SelectShorthandAmount(Guid ShorthandPersonInCharge, string DateBegin, string DateEnd)
        {
            DbParameter[] parameters = new DbParameter[3];
            parameters[0] = Manager.CreateParameter("ShorthandPersonInCharge", ShorthandPersonInCharge);
            parameters[1] = Manager.CreateParameter("DateBegin", DateBegin);
            parameters[2] = Manager.CreateParameter("DateEnd", DateEnd);
            DataTable table = this.SelectOperate.Select(Project.Select_AmountShorthand, parameters);
            int count = Convert.ToInt32(table.Rows[0][0]);
            return count;
        }
        public int SelectContentReceiveAmount(Guid ContentPersonInCharge, string DateBegin, string DateEnd)
        {
            DbParameter[] parameters = new DbParameter[3];
            parameters[0] = Manager.CreateParameter("ContentPersonInCharge", ContentPersonInCharge);
            parameters[1] = Manager.CreateParameter("DateBegin", DateBegin);
            parameters[2] = Manager.CreateParameter("DateEnd", DateEnd);
            DataTable table = this.SelectOperate.Select(Project.Select_AmountContentReceive, parameters);
            int count = Convert.ToInt32(table.Rows[0][0]);
            return count;
        }
        public int SelectContentCheckAmount(Guid ContentPersonInCharge, string DateBegin, string DateEnd)
        {
            DbParameter[] parameters = new DbParameter[3];
            parameters[0] = Manager.CreateParameter("ContentPersonInCharge", ContentPersonInCharge);
            parameters[1] = Manager.CreateParameter("DateBegin", DateBegin);
            parameters[2] = Manager.CreateParameter("DateEnd", DateEnd);
            DataTable table = this.SelectOperate.Select(Project.Select_AmountContentCheck, parameters);
            int count = Convert.ToInt32(table.Rows[0][0]);
            return count;
        }
        public int SelectContentRecheckAmount(Guid ContentRecheckPersonInCharge, string DateBegin, string DateEnd)
        {
            DbParameter[] parameters = new DbParameter[3];
            parameters[0] = Manager.CreateParameter("ContentRecheckPersonInCharge", ContentRecheckPersonInCharge);
            parameters[1] = Manager.CreateParameter("DateBegin", DateBegin);
            parameters[2] = Manager.CreateParameter("DateEnd", DateEnd);
            DataTable table = this.SelectOperate.Select(Project.Select_AmountContentRecheck, parameters);
            int count = Convert.ToInt32(table.Rows[0][0]);
            return count;
        }
        public int SelectContentFinishAmount(Guid ContentOperator, string DateBegin, string DateEnd)
        {
            DbParameter[] parameters = new DbParameter[3];
            parameters[0] = Manager.CreateParameter("ContentOperator", ContentOperator);
            parameters[1] = Manager.CreateParameter("DateBegin", DateBegin);
            parameters[2] = Manager.CreateParameter("DateEnd", DateEnd);
            DataTable table = this.SelectOperate.Select(Project.Select_AmountContentFinish, parameters);
            int count = Convert.ToInt32(table.Rows[0][0]);
            return count;
        }
        public int SelectContentFinishDelayAmount(Guid ContentOperator, string DateBegin, string DateEnd)
        {
            DbParameter[] parameters = new DbParameter[3];
            parameters[0] = Manager.CreateParameter("ContentOperator", ContentOperator);
            parameters[1] = Manager.CreateParameter("DateBegin", DateBegin);
            parameters[2] = Manager.CreateParameter("DateEnd", DateEnd);
            DataTable table = this.SelectOperate.Select(Project.Select_AmountContentFinishDelay, parameters);
            int count = Convert.ToInt32(table.Rows[0][0]);
            return count;
        }
        public int SelectProductionReceiveAmount(Guid ProductionPersonInCharge, string DateBegin, string DateEnd)
        {
            DbParameter[] parameters = new DbParameter[3];
            parameters[0] = Manager.CreateParameter("ProductionPersonInCharge", ProductionPersonInCharge);
            parameters[1] = Manager.CreateParameter("DateBegin", DateBegin);
            parameters[2] = Manager.CreateParameter("DateEnd", DateEnd);
            DataTable table = this.SelectOperate.Select(Project.Select_AmountProductionReceive, parameters);
            int count = Convert.ToInt32(table.Rows[0][0]);
            return count;
        }
        public int SelectProductionCheckAmount(Guid ProductionPersonInCharge, string DateBegin, string DateEnd)
        {
            DbParameter[] parameters = new DbParameter[3];
            parameters[0] = Manager.CreateParameter("ProductionPersonInCharge", ProductionPersonInCharge);
            parameters[1] = Manager.CreateParameter("DateBegin", DateBegin);
            parameters[2] = Manager.CreateParameter("DateEnd", DateEnd);
            DataTable table = this.SelectOperate.Select(Project.Select_AmountProductionCheck, parameters);
            int count = Convert.ToInt32(table.Rows[0][0]);
            return count;
        }
        public int SelectProductionFinishAmount(Guid ProductionOperator, string DateBegin, string DateEnd)
        {
            DbParameter[] parameters = new DbParameter[3];
            parameters[0] = Manager.CreateParameter("ProductionOperator", ProductionOperator);
            parameters[1] = Manager.CreateParameter("DateBegin", DateBegin);
            parameters[2] = Manager.CreateParameter("DateEnd", DateEnd);
            DataTable table = this.SelectOperate.Select(Project.Select_AmountProductionFinish, parameters);
            int count = Convert.ToInt32(table.Rows[0][0]);
            return count;
        }
        public int SelectProductionFinishDelayAmount(Guid ProductionOperator, string DateBegin, string DateEnd)
        {
            DbParameter[] parameters = new DbParameter[3];
            parameters[0] = Manager.CreateParameter("ProductionOperator", ProductionOperator);
            parameters[1] = Manager.CreateParameter("DateBegin", DateBegin);
            parameters[2] = Manager.CreateParameter("DateEnd", DateEnd);
            DataTable table = this.SelectOperate.Select(Project.Select_AmountProductionFinishDelay, parameters);
            int count = Convert.ToInt32(table.Rows[0][0]);
            return count;
        }
        public int SelectPublishAmount(Guid PublishOperator, string DateBegin, string DateEnd)
        {
            DbParameter[] parameters = new DbParameter[3];
            parameters[0] = Manager.CreateParameter("PublishOperator", PublishOperator);
            parameters[1] = Manager.CreateParameter("DateBegin", DateBegin);
            parameters[2] = Manager.CreateParameter("DateEnd", DateEnd);
            DataTable table = this.SelectOperate.Select(Project.Select_AmountPublish, parameters);
            int count = Convert.ToInt32(table.Rows[0][0]);
            return count;
        }
        public int SelectCheckAmount(Guid CheckPersonInCharge, string DateBegin, string DateEnd)
        {
            DbParameter[] parameters = new DbParameter[3];
            parameters[0] = Manager.CreateParameter("CheckPersonInCharge", CheckPersonInCharge);
            parameters[1] = Manager.CreateParameter("DateBegin", DateBegin);
            parameters[2] = Manager.CreateParameter("DateEnd", DateEnd);
            DataTable table = this.SelectOperate.Select(Project.Select_AmountCheck, parameters);
            int count = Convert.ToInt32(table.Rows[0][0]);
            return count;
        }

        #endregion Amount

        #endregion select

        #region insert

        /// <summary>
        /// 
        /// </summary>
        /// <param name="projectPlanRecordId"></param>
        /// <param name="projectPlanNo"></param>
        /// <param name="title"></param>
        /// <param name="projectPlanTypeId"></param>
        /// <param name="PlanDate"></param>
        /// <param name="progress"></param>
        /// <param name="creatorId"></param>
        /// <returns></returns>
        public int Insert(Guid ProjectId, Guid ProjectPlanId, Guid ProjectTypeId, string ProjectNo, Guid emergency, Guid WorkType, string CourseName, Guid notice, Guid headline, string TextCategory, string lecturer, string LecturerJob, Guid progress, Guid InCharge, string CreateNote, string ExtraNote, Guid ContentNeeds, Guid PublishNeeds, Guid SourceProjectId, Guid CanBeSold)
        {
            int count = 0;
            DbTransaction transaction = this.BeginTransaction();
            try
            {
                count = this.Insert(transaction, ProjectId, ProjectPlanId, ProjectTypeId, ProjectNo, emergency, WorkType, CourseName, notice, headline, TextCategory, lecturer, LecturerJob, progress, InCharge, CreateNote, ExtraNote, ContentNeeds, PublishNeeds, SourceProjectId, CanBeSold);
                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
            }
            finally
            {
                this.EndTransaction();
            }
            return count;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="transaction"></param>
        /// <param name="projectPlanRecordId"></param>
        /// <param name="projectPlanNo"></param>
        /// <param name="title"></param>
        /// <param name="projectPlanTypeId"></param>
        /// <param name="planDate"></param>
        /// <param name="progress"></param>
        /// <param name="creatorId"></param>
        /// <returns></returns>
        public int Insert(DbTransaction transaction,
            Guid ProjectId, Guid ProjectPlanId, Guid ProjectTypeId, string ProjectNo, Guid emergency, Guid WorkType, string CourseName, Guid notice, Guid headline, string TextCategory, string lecturer, string LecturerJob, Guid progress, Guid InCharge, string CreateNote, string ExtraNote, Guid ContentNeeds, Guid PublishNeeds, Guid SourceProjectId, Guid CanBeSold)
        {
            DbParameter[] parameters = new DbParameter[21];
            parameters[0] = Manager.CreateParameter("ProjectId", ProjectId);
            parameters[1] = Manager.CreateParameter("ProjectPlanId", ProjectPlanId);
            parameters[2] = Manager.CreateParameter("ProjectTypeId", ProjectTypeId);
            parameters[3] = Manager.CreateParameter("ProjectNo", ProjectNo);
            parameters[4] = Manager.CreateParameter("SendingDate", DateTime.Now);
            parameters[5] = Manager.CreateParameter("emergency", emergency);
            parameters[6] = Manager.CreateParameter("WorkType", WorkType);
            parameters[7] = Manager.CreateParameter("CourseName", CourseName);
            parameters[8] = Manager.CreateParameter("notice", notice);
            parameters[9] = Manager.CreateParameter("headline", headline);
            parameters[10] = Manager.CreateParameter("TextCategory", TextCategory);
            parameters[11] = Manager.CreateParameter("lecturer", lecturer);
            parameters[12] = Manager.CreateParameter("LecturerJob", LecturerJob);
            parameters[13] = Manager.CreateParameter("progress", progress);
            parameters[14] = Manager.CreateParameter("InCharge", InCharge);
            parameters[15] = Manager.CreateParameter("CreateNote", CreateNote);
            parameters[16] = Manager.CreateParameter("ExtraNote", ExtraNote);
            parameters[17] = Manager.CreateParameter("ContentNeeds", ContentNeeds);
            parameters[18] = Manager.CreateParameter("PublishNeeds", PublishNeeds);
            parameters[19] = Manager.CreateParameter("SourceProjectId", SourceProjectId);
            parameters[20] = Manager.CreateParameter("CanBeSold", CanBeSold);
            int count = this.ModifyOperate.Modify(transaction, Project.INSERT_01, parameters);
            return count;
        }

        #endregion insert

        #region update

        public int UpdateExecution(Guid ProjectId, Guid ProjectTypeId, string ProjectNo, Guid emergency, Guid WorkType, string CourseName, Guid notice, Guid headline, string TextCategory, string lecturer, string LecturerJob, Guid progress, Guid ContentProgress, Guid ProductionProgress, string CreateNote, string ExtraNote,DateTime ExecutionDate)
        {
            int count = 0;
            DbTransaction transaction = this.BeginTransaction();
            try
            {
                count = this.UpdateExecution(transaction, ProjectId, ProjectTypeId, ProjectNo, emergency, WorkType, CourseName, notice, headline, TextCategory, lecturer, LecturerJob, progress, ContentProgress, ProductionProgress, CreateNote, ExtraNote, ExecutionDate);
                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
            }
            finally
            {
                this.EndTransaction();
            }
            return count;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="transaction"></param>
        /// <param name="projectPlanRecordId"></param>
        /// <param name="projectPlanNo"></param>
        /// <param name="title"></param>
        /// <param name="projectPlanTypeId"></param>
        /// <param name="planDate"></param>
        /// <param name="progress"></param>
        /// <param name="creatorId"></param>
        /// <returns></returns>
        public int UpdateExecution(DbTransaction transaction,
            Guid ProjectId, Guid ProjectTypeId, string ProjectNo, Guid emergency, Guid WorkType, string CourseName, Guid notice, Guid headline, string TextCategory, string lecturer, string LecturerJob, Guid progress, Guid ContentProgress, Guid ProductionProgress, string CreateNote, string ExtraNote,DateTime ExecutionDate)
        {
            DbParameter[] parameters = new DbParameter[17];
            parameters[0] = Manager.CreateParameter("ProjectId", ProjectId);
            parameters[1] = Manager.CreateParameter("ProjectTypeId", ProjectTypeId);
            parameters[2] = Manager.CreateParameter("ProjectNo", ProjectNo);
            parameters[3] = Manager.CreateParameter("emergency", emergency);
            parameters[4] = Manager.CreateParameter("WorkType", WorkType);
            parameters[5] = Manager.CreateParameter("CourseName", CourseName);
            parameters[6] = Manager.CreateParameter("notice", notice);
            parameters[7] = Manager.CreateParameter("headline", headline);
            parameters[8] = Manager.CreateParameter("TextCategory", TextCategory);
            parameters[9] = Manager.CreateParameter("lecturer", lecturer);
            parameters[10] = Manager.CreateParameter("LecturerJob", LecturerJob);
            parameters[11] = Manager.CreateParameter("progress", progress);
            parameters[12] = Manager.CreateParameter("ContentProgress", ContentProgress);
            parameters[13] = Manager.CreateParameter("ProductionProgress", ProductionProgress);
            parameters[14] = Manager.CreateParameter("CreateNote", CreateNote);
            parameters[15] = Manager.CreateParameter("ExtraNote", ExtraNote);
            parameters[16] = Manager.CreateParameter("ExecutionDate", ExecutionDate);
            int count = this.ModifyOperate.Modify(transaction, Project.UPDATE_Execution, parameters);
            return count;
        }

        public int UpdateCaptureReceive(Guid ProjectId, DateTime CaptureReceiveDate, Guid progress, Guid CapturePersonInCharge)
        {
            int count = 0;
            DbTransaction transaction = this.BeginTransaction();
            try
            {
                count = this.UpdateCaptureReceive(transaction, ProjectId, CaptureReceiveDate, progress, CapturePersonInCharge);
                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
            }
            finally
            {
                this.EndTransaction();
            }
            return count;
        }

        public int UpdateCaptureReceive(DbTransaction transaction,
            Guid ProjectId, DateTime CaptureReceiveDate, Guid progress, Guid CapturePersonInCharge)
        {
            DbParameter[] parameters = new DbParameter[4];
            parameters[0] = Manager.CreateParameter("ProjectId", ProjectId);
            parameters[1] = Manager.CreateParameter("CaptureReceiveDate", CaptureReceiveDate);
            parameters[2] = Manager.CreateParameter("progress", progress);
            parameters[3] = Manager.CreateParameter("CapturePersonInCharge", CapturePersonInCharge);
            int count = this.ModifyOperate.Modify(transaction, Project.UPDATE_CaptureReceive, parameters);
            return count;
        }

        public int UpdateCaptureFinish(DbTransaction transaction,
            Guid ProjectId, int CaptureDuration, Guid progress, DateTime CaptureFinishDate, Guid CaptureVideoNeeds,
            Guid CaptureAudioNeeds, Guid CaptureVideoVideoQuality, Guid CaptureVideoAudioQuality, Guid CaptureAudioQuality,
            Guid CaptureSoundTrack, String CaptureFilePath, String CaptureNote)
        {
            DbParameter[] parameters = new DbParameter[12];
            parameters[0] = Manager.CreateParameter("ProjectId", ProjectId);
            parameters[1] = Manager.CreateParameter("CaptureDuration", CaptureDuration);
            parameters[2] = Manager.CreateParameter("progress", progress);
            parameters[3] = Manager.CreateParameter("CaptureFinishDate", CaptureFinishDate);
            parameters[4] = Manager.CreateParameter("CaptureVideoNeeds", CaptureVideoNeeds);
            parameters[5] = Manager.CreateParameter("CaptureAudioNeeds", CaptureAudioNeeds);
            parameters[6] = Manager.CreateParameter("CaptureVideoVideoQuality", CaptureVideoVideoQuality);
            parameters[7] = Manager.CreateParameter("CaptureVideoAudioQuality", CaptureVideoAudioQuality);
            parameters[8] = Manager.CreateParameter("CaptureAudioQuality", CaptureAudioQuality);
            parameters[9] = Manager.CreateParameter("CaptureSoundTrack", CaptureSoundTrack);
            parameters[10] = Manager.CreateParameter("CaptureFilePath", CaptureFilePath);
            parameters[11] = Manager.CreateParameter("CaptureNote", CaptureNote);
            int count = this.ModifyOperate.Modify(transaction, Project.UPDATE_CaptureFinish, parameters);
            return count;
        }

        public int UpdateCaptureFinish(Guid ProjectId, int CaptureDuration, Guid progress, DateTime CaptureFinishDate,
            Guid CaptureVideoNeeds, Guid CaptureAudioNeeds, Guid CaptureVideoVideoQuality, Guid CaptureVideoAudioQuality,
            Guid CaptureAudioQuality, Guid CaptureSoundTrack, String CaptureFilePath, String CaptureNote)
        {
            int count = 0;
            DbTransaction transaction = this.BeginTransaction();
            try
            {
                count = this.UpdateCaptureFinish(transaction, ProjectId, CaptureDuration, progress, CaptureFinishDate, CaptureVideoNeeds, CaptureAudioNeeds, CaptureVideoVideoQuality, CaptureVideoAudioQuality, CaptureAudioQuality, CaptureSoundTrack, CaptureFilePath, CaptureNote);
                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
            }
            finally
            {
                this.EndTransaction();
            }
            return count;
        }

        public int UpdateCaptureCheckFinish(DbTransaction transaction,
            Guid ProjectId, Guid progress, Guid CaptureCheckPersonInCharge, DateTime CaptureCheckDate, String CaptureCheckNote, Guid ProjectTypeId, string ProjectNo, Guid emergency, Guid WorkType, string CourseName, Guid notice, Guid headline, string TextCategory, string lecturer, string LecturerJob,string CreateNote, string ExtraNote)
        {
            DbParameter[] parameters = new DbParameter[17];
            parameters[0] = Manager.CreateParameter("ProjectId", ProjectId);
            parameters[1] = Manager.CreateParameter("progress", progress);
            parameters[2] = Manager.CreateParameter("CaptureCheckPersonInCharge", CaptureCheckPersonInCharge);
            parameters[3] = Manager.CreateParameter("CaptureCheckDate", CaptureCheckDate);
            parameters[4] = Manager.CreateParameter("CaptureCheckNote", CaptureCheckNote);
            parameters[5] = Manager.CreateParameter("ProjectTypeId", ProjectTypeId);
            parameters[6] = Manager.CreateParameter("ProjectNo", ProjectNo);
            parameters[7] = Manager.CreateParameter("emergency", emergency);
            parameters[8] = Manager.CreateParameter("WorkType", WorkType);
            parameters[9] = Manager.CreateParameter("CourseName", CourseName);
            parameters[10] = Manager.CreateParameter("notice", notice);
            parameters[11] = Manager.CreateParameter("headline", headline);
            parameters[12] = Manager.CreateParameter("TextCategory", TextCategory);
            parameters[13] = Manager.CreateParameter("lecturer", lecturer);
            parameters[14] = Manager.CreateParameter("LecturerJob", LecturerJob);
            parameters[15] = Manager.CreateParameter("CreateNote", CreateNote);
            parameters[16] = Manager.CreateParameter("ExtraNote",ExtraNote);
            int count = this.ModifyOperate.Modify(transaction, Project.UPDATE_CaptureCheckFinish, parameters);
            return count;
        }
        public int UpdateCaptureCheckFinish(Guid ProjectId, Guid progress, Guid CaptureCheckPersonInCharge, DateTime CaptureCheckDate,
            String CaptureCheckNote, Guid ProjectTypeId, string ProjectNo, Guid emergency, Guid WorkType, string CourseName, Guid notice, Guid headline, string TextCategory, string lecturer, string LecturerJob,string CreateNote,string ExtraNote)
        {
            int count = 0;
            DbTransaction transaction = this.BeginTransaction();
            try
            {
                count = this.UpdateCaptureCheckFinish(transaction, ProjectId, progress, CaptureCheckPersonInCharge, CaptureCheckDate, CaptureCheckNote, ProjectTypeId, ProjectNo, emergency, WorkType, CourseName, notice, headline, TextCategory, lecturer, LecturerJob,CreateNote,ExtraNote);
                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
            }
            finally
            {
                this.EndTransaction();
            }
            return count;
        }

        public int UpdateShorthandFinish(Guid ProjectId, Guid progress, Guid ShorthandPersonInCharge, DateTime ShorthandFinishDate, DateTime ShorthandAudioReceiveDate, String ShorthandPurveyor, Guid ShorthandQuality, String ShorthandNote, Guid ContentProgress)
        {
            int count = 0;
            DbTransaction transaction = this.BeginTransaction();
            try
            {
                count = this.UpdateShorthandFinish(transaction, ProjectId, progress, ShorthandPersonInCharge, ShorthandFinishDate, ShorthandAudioReceiveDate, ShorthandPurveyor, ShorthandQuality, ShorthandNote, ContentProgress);
                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
            }
            finally
            {
                this.EndTransaction();
            }
            return count;
        }

        public int UpdateShorthandFinish(DbTransaction transaction,
           Guid ProjectId, Guid progress, Guid ShorthandPersonInCharge, DateTime ShorthandFinishDate, DateTime ShorthandAudioReceiveDate, String ShorthandPurveyor, Guid ShorthandQuality, String ShorthandNote, Guid ContentProgress)
        {
            DbParameter[] parameters = new DbParameter[9];
            parameters[0] = Manager.CreateParameter("ProjectId", ProjectId);
            parameters[1] = Manager.CreateParameter("progress", progress);
            parameters[2] = Manager.CreateParameter("ShorthandPersonInCharge", ShorthandPersonInCharge);
            parameters[3] = Manager.CreateParameter("ShorthandFinishDate", ShorthandFinishDate);
            parameters[4] = Manager.CreateParameter("ShorthandAudioReceiveDate", ShorthandAudioReceiveDate);
            parameters[5] = Manager.CreateParameter("ShorthandPurveyor", ShorthandPurveyor);
            parameters[6] = Manager.CreateParameter("ShorthandQuality", ShorthandQuality);
            parameters[7] = Manager.CreateParameter("ShorthandNote", ShorthandNote);
            parameters[8] = Manager.CreateParameter("ContentProgress", ContentProgress);
            int count = this.ModifyOperate.Modify(transaction, Project.UPDATE_ShorthandFinish, parameters);
            return count;
        }

        public int UpdateContentReceive(Guid ProjectId, Guid progress, Guid ContentPersonInCharge, Guid ContentOperator, DateTime ContentAssignmentDate, DateTime ContentEstimatedDate, string ContentReceiveNote, Guid ContentProgress)
        {
            int count = 0;
            DbTransaction transaction = this.BeginTransaction();
            try
            {
                count = this.UpdateContentReceive(transaction, ProjectId, progress, ContentPersonInCharge, ContentOperator, ContentAssignmentDate, ContentEstimatedDate, ContentReceiveNote, ContentProgress);
                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
            }
            finally
            {
                this.EndTransaction();
            }
            return count;
        }

        public int UpdateContentReceive(DbTransaction transaction,
           Guid ProjectId, Guid progress, Guid ContentPersonInCharge, Guid ContentOperator, DateTime ContentAssignmentDate, DateTime ContentEstimatedDate, string ContentReceiveNote, Guid ContentProgress)
        {
            DbParameter[] parameters = new DbParameter[8];
            parameters[0] = Manager.CreateParameter("ProjectId", ProjectId);
            parameters[1] = Manager.CreateParameter("progress", progress);
            parameters[2] = Manager.CreateParameter("ContentPersonInCharge", ContentPersonInCharge);
            parameters[3] = Manager.CreateParameter("ContentOperator", ContentOperator);
            parameters[4] = Manager.CreateParameter("ContentAssignmentDate", ContentAssignmentDate);
            parameters[5] = Manager.CreateParameter("ContentEstimatedDate", ContentEstimatedDate);
            parameters[6] = Manager.CreateParameter("ContentReceiveNote", ContentReceiveNote);
            parameters[7] = Manager.CreateParameter("ContentProgress", ContentProgress);
            int count = this.ModifyOperate.Modify(transaction, Project.UPDATE_ContentReceive, parameters);
            return count;
        }

        public int UpdateContentFinish(Guid ProjectId, Guid progress, DateTime ContentFinishDate, string ContentDelayNote, Guid ContentCourseNameConfirm,
                    string ContentChangedCourseName, Guid ContentCourseRecommend, Guid ContentPPTAdvice, int ContentExercises, Guid ContentPPTNeeds,
                    Guid ContentCourseIntroNeeds, Guid ContentLecturerResumeNeeds, Guid ContentExercisesNeeds, Guid ContentTextEditNeeds, string ContentOperateNote, Guid ContentProgress)
        {
            int count = 0;
            DbTransaction transaction = this.BeginTransaction();
            try
            {
                count = this.UpdateContentFinish(transaction, ProjectId, progress, ContentFinishDate, ContentDelayNote, ContentCourseNameConfirm, ContentChangedCourseName, ContentCourseRecommend, ContentPPTAdvice, ContentExercises, ContentPPTNeeds, ContentCourseIntroNeeds, ContentLecturerResumeNeeds, ContentExercisesNeeds, ContentTextEditNeeds, ContentOperateNote, ContentProgress);
                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
            }
            finally
            {
                this.EndTransaction();
            }
            return count;
        }

        public int UpdateContentFinish(DbTransaction transaction,
                   Guid ProjectId, Guid progress, DateTime ContentFinishDate, string ContentDelayNote, Guid ContentCourseNameConfirm,
                    string ContentChangedCourseName, Guid ContentCourseRecommend, Guid ContentPPTAdvice, int ContentExercises, Guid ContentPPTNeeds,
                    Guid ContentCourseIntroNeeds, Guid ContentLecturerResumeNeeds, Guid ContentExercisesNeeds, Guid ContentTextEditNeeds, string ContentOperateNote, Guid ContentProgress)
        {
            DbParameter[] parameters = new DbParameter[16];
            parameters[0] = Manager.CreateParameter("ProjectId", ProjectId);
            parameters[1] = Manager.CreateParameter("progress", progress);
            parameters[2] = Manager.CreateParameter("ContentFinishDate", ContentFinishDate);
            parameters[3] = Manager.CreateParameter("ContentDelayNote", ContentDelayNote);
            parameters[4] = Manager.CreateParameter("ContentCourseNameConfirm", ContentCourseNameConfirm);
            parameters[5] = Manager.CreateParameter("ContentChangedCourseName", ContentChangedCourseName);
            parameters[6] = Manager.CreateParameter("ContentCourseRecommend", ContentCourseRecommend);
            parameters[7] = Manager.CreateParameter("ContentPPTAdvice", ContentPPTAdvice);
            parameters[8] = Manager.CreateParameter("ContentExercises", ContentExercises);
            parameters[9] = Manager.CreateParameter("ContentPPTNeeds", ContentPPTNeeds);
            parameters[10] = Manager.CreateParameter("ContentCourseIntroNeeds", ContentCourseIntroNeeds);
            parameters[11] = Manager.CreateParameter("ContentLecturerResumeNeeds", ContentLecturerResumeNeeds);
            parameters[12] = Manager.CreateParameter("ContentExercisesNeeds", ContentExercisesNeeds);
            parameters[13] = Manager.CreateParameter("ContentTextEditNeeds", ContentTextEditNeeds);
            parameters[14] = Manager.CreateParameter("ContentOperateNote", ContentOperateNote);
            parameters[15] = Manager.CreateParameter("ContentProgress", ContentProgress);
            int count = this.ModifyOperate.Modify(transaction, Project.UPDATE_ContentFinish, parameters);
            return count;
        }

        public int UpdateContentCheck(Guid ProjectId, Guid progress, Guid ContentProgress, Guid ContentCourseIntroductionQuality, Guid ContentResumeQuality,
            Guid ContentPPTQuality, Guid ContentExercisesQuality, Guid ContentTextQuality, Guid ContentIsTimely, DateTime ContentCheckDate,
            string ContentCheckNote)
        {
            int count = 0;
            DbTransaction transaction = this.BeginTransaction();
            try
            {
                count = this.UpdateContentCheck(transaction, ProjectId, progress, ContentProgress, ContentCourseIntroductionQuality, ContentResumeQuality, ContentPPTQuality, ContentExercisesQuality, ContentTextQuality, ContentIsTimely, ContentCheckDate, ContentCheckNote);
                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
            }
            finally
            {
                this.EndTransaction();
            }
            return count;
        }

        public int UpdateContentCheck(DbTransaction transaction,
            Guid ProjectId, Guid progress, Guid ContentProgress, Guid ContentCourseIntroductionQuality, Guid ContentResumeQuality,
            Guid ContentPPTQuality, Guid ContentExercisesQuality, Guid ContentTextQuality, Guid ContentIsTimely, DateTime ContentCheckDate,
            string ContentCheckNote)
        {
            DbParameter[] parameters = new DbParameter[11];
            parameters[0] = Manager.CreateParameter("ProjectId", ProjectId);
            parameters[1] = Manager.CreateParameter("progress", progress);
            parameters[2] = Manager.CreateParameter("ContentProgress", ContentProgress);
            parameters[3] = Manager.CreateParameter("ContentCourseIntroductionQuality", ContentCourseIntroductionQuality);
            parameters[4] = Manager.CreateParameter("ContentResumeQuality", ContentResumeQuality);
            parameters[5] = Manager.CreateParameter("ContentPPTQuality", ContentPPTQuality);
            parameters[6] = Manager.CreateParameter("ContentExercisesQuality", ContentExercisesQuality);
            parameters[7] = Manager.CreateParameter("ContentTextQuality", ContentTextQuality);
            parameters[8] = Manager.CreateParameter("ContentIsTimely", ContentIsTimely);
            parameters[9] = Manager.CreateParameter("ContentCheckDate", ContentCheckDate);
            parameters[10] = Manager.CreateParameter("ContentCheckNote", ContentCheckNote);
            int count = this.ModifyOperate.Modify(transaction, Project.UPDATE_ContentCheck, parameters);
            return count;
        }

        public int UpdateContentRecheckFinish(DbTransaction transaction,
          Guid ProjectId, Guid progress, Guid ContentProgress, Guid ProductionProgress, Guid ContentRecheckPersonInCharge, DateTime ContentRecheckDate, String ContentRecheckNote)
        {
            DbParameter[] parameters = new DbParameter[7];
            parameters[0] = Manager.CreateParameter("ProjectId", ProjectId);
            parameters[1] = Manager.CreateParameter("progress", progress);
            parameters[2] = Manager.CreateParameter("ContentProgress", ContentProgress);
            parameters[3] = Manager.CreateParameter("ProductionProgress", ProductionProgress);
            parameters[4] = Manager.CreateParameter("ContentRecheckPersonInCharge", ContentRecheckPersonInCharge);
            parameters[5] = Manager.CreateParameter("ContentRecheckDate", ContentRecheckDate);
            parameters[6] = Manager.CreateParameter("ContentRecheckNote", ContentRecheckNote);
            int count = this.ModifyOperate.Modify(transaction, Project.UPDATE_ContentRecheckFinish, parameters);
            return count;
        }
        public int UpdateContentRecheckFinish(Guid ProjectId, Guid progress, Guid ContentProgress, Guid ProductionProgress, Guid ContentRecheckPersonInCharge, DateTime ContentRecheckDate,
            String ContentRecheckNote)
        {
            int count = 0;
            DbTransaction transaction = this.BeginTransaction();
            try
            {
                count = this.UpdateContentRecheckFinish(transaction, ProjectId, progress, ContentProgress, ProductionProgress, ContentRecheckPersonInCharge, ContentRecheckDate, ContentRecheckNote);
                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
            }
            finally
            {
                this.EndTransaction();
            }
            return count;
        }

        public int UpdateProductionReceive(Guid ProjectId, Guid progress, Guid ProductionPersonInCharge, DateTime ProductionReceiveDate, string ProductionReceiveNote, DateTime ProductionAssignmentDate, DateTime ProductionEstimatedDate, Guid ProductionOperator, Guid ProductionProgress)
        {
            int count = 0;
            DbTransaction transaction = this.BeginTransaction();
            try
            {
                count = this.UpdateProductionReceive(transaction, ProjectId, progress, ProductionPersonInCharge, ProductionReceiveDate, ProductionReceiveNote, ProductionAssignmentDate, ProductionEstimatedDate, ProductionOperator, ProductionProgress);
                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
            }
            finally
            {
                this.EndTransaction();
            }
            return count;
        }

        public int UpdateProductionReceive(DbTransaction transaction,
            Guid ProjectId, Guid progress, Guid ProductionPersonInCharge, DateTime ProductionReceiveDate, string ProductionReceiveNote, DateTime ProductionAssignmentDate, DateTime ProductionEstimatedDate, Guid ProductionOperator, Guid ProductionProgress)
        {
            DbParameter[] parameters = new DbParameter[9];
            parameters[0] = Manager.CreateParameter("ProjectId", ProjectId);
            parameters[1] = Manager.CreateParameter("progress", progress);
            parameters[2] = Manager.CreateParameter("ProductionPersonInCharge", ProductionPersonInCharge);
            parameters[3] = Manager.CreateParameter("ProductionReceiveDate", ProductionReceiveDate);
            parameters[4] = Manager.CreateParameter("ProductionAssignmentDate", ProductionAssignmentDate);
            parameters[5] = Manager.CreateParameter("ProductionEstimatedDate", ProductionEstimatedDate);
            parameters[6] = Manager.CreateParameter("ProductionOperator", ProductionOperator);
            parameters[7] = Manager.CreateParameter("ProductionProgress", ProductionProgress);
            parameters[8] = Manager.CreateParameter("ProductionReceiveNote", ProductionReceiveNote);
            int count = this.ModifyOperate.Modify(transaction, Project.UPDATE_ProductionReceive, parameters);
            return count;
        }
        public int UpdateProductionFinish(Guid ProjectId, Guid progress, DateTime ProductionLastModifyDate, DateTime ProductionFinishDate,
            string ProductionDelayNote, string ProductionOperateNote, Guid ProductionVideoQuality, Guid ProductionAudioQuality, Guid ProductionFileBackUp, Guid ProductionProgress)
        {
            int count = 0;
            DbTransaction transaction = this.BeginTransaction();
            try
            {
                count = this.UpdateProductionFinish(transaction, ProjectId, progress, ProductionLastModifyDate, ProductionFinishDate, ProductionDelayNote, ProductionOperateNote, ProductionVideoQuality, ProductionAudioQuality, ProductionFileBackUp, ProductionProgress);
                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
            }
            finally
            {
                this.EndTransaction();
            }
            return count;
        }

        public int UpdateProductionFinish(DbTransaction transaction,
            Guid ProjectId, Guid progress, DateTime ProductionLastModifyDate, DateTime ProductionFinishDate, string ProductionDelayNote, string ProductionOperateNote, Guid ProductionVideoQuality,
            Guid ProductionAudioQuality, Guid ProductionFileBackUp, Guid ProductionProgress)
        {
            DbParameter[] parameters = new DbParameter[10];
            parameters[0] = Manager.CreateParameter("ProjectId", ProjectId);
            parameters[1] = Manager.CreateParameter("progress", progress);
            parameters[2] = Manager.CreateParameter("ProductionLastModifyDate", ProductionLastModifyDate);
            parameters[3] = Manager.CreateParameter("ProductionFinishDate", ProductionFinishDate);
            parameters[4] = Manager.CreateParameter("ProductionDelayNote", ProductionDelayNote);
            parameters[5] = Manager.CreateParameter("ProductionOperateNote", ProductionOperateNote);
            parameters[6] = Manager.CreateParameter("ProductionVideoQuality", ProductionVideoQuality);
            parameters[7] = Manager.CreateParameter("ProductionAudioQuality", ProductionAudioQuality);
            parameters[8] = Manager.CreateParameter("ProductionFileBackUp", ProductionFileBackUp);
            parameters[9] = Manager.CreateParameter("ProductionProgress", ProductionProgress);
            int count = this.ModifyOperate.Modify(transaction, Project.UPDATE_ProductionFinish, parameters);
            return count;
        }
        public int UpdateProductionCheck(Guid ProjectId, Guid progress, Guid ProductionProgress,
            Guid ProductionVideoEditCheck, Guid ProductionAudioEditCheck, Guid ProductionProductCheck, Guid ProductionIsTimely
            , DateTime ProductionCheckDate, string ProductionCheckNote)
        {
            int count = 0;
            DbTransaction transaction = this.BeginTransaction();
            try
            {
                count = this.UpdateProductionCheck(transaction, ProjectId, progress, ProductionProgress,
                    ProductionVideoEditCheck, ProductionAudioEditCheck, ProductionProductCheck, ProductionIsTimely, ProductionCheckDate, ProductionCheckNote);
                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
            }
            finally
            {
                this.EndTransaction();
            }
            return count;
        }

        public int UpdateProductionCheck(DbTransaction transaction,
            Guid ProjectId, Guid progress, Guid ProductionProgress,
            Guid ProductionVideoEditCheck, Guid ProductionAudioEditCheck, Guid ProductionProductCheck, Guid ProductionIsTimely
            , DateTime ProductionCheckDate, string ProductionCheckNote)
        {
            DbParameter[] parameters = new DbParameter[9];
            parameters[0] = Manager.CreateParameter("ProjectId", ProjectId);
            parameters[1] = Manager.CreateParameter("progress", progress);
            parameters[2] = Manager.CreateParameter("ProductionProgress", ProductionProgress);
            parameters[3] = Manager.CreateParameter("ProductionVideoEditCheck", ProductionVideoEditCheck);
            parameters[4] = Manager.CreateParameter("ProductionAudioEditCheck", ProductionAudioEditCheck);
            parameters[5] = Manager.CreateParameter("ProductionProductCheck", ProductionProductCheck);
            parameters[6] = Manager.CreateParameter("ProductionIsTimely", ProductionIsTimely);
            parameters[7] = Manager.CreateParameter("ProductionCheckDate", ProductionCheckDate);
            parameters[8] = Manager.CreateParameter("ProductionCheckNote", ProductionCheckNote);
            int count = this.ModifyOperate.Modify(transaction, Project.UPDATE_ProductionCheck, parameters);
            return count;
        }
        public int UpdatePublish(Guid ProjectId, Guid progress, Guid PublishOperator, DateTime PublishPublishDate, Guid PublishTopNewsNeeds
            , Guid PublishNoticeNeeds, string PublishCommonCategory, string PublishGovernmentCategory, string PublishFinanceCategory
            , string PublishBankCategory, Guid PublishPageState, Guid PublishPlayState, string PublishNote)
        {
            int count = 0;
            DbTransaction transaction = this.BeginTransaction();
            try
            {
                count = this.UpdatePublish(transaction, ProjectId, progress, PublishOperator, PublishPublishDate, PublishTopNewsNeeds, PublishNoticeNeeds
                    , PublishCommonCategory, PublishGovernmentCategory, PublishFinanceCategory, PublishBankCategory, PublishPageState, PublishPlayState, PublishNote);
                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
            }
            finally
            {
                this.EndTransaction();
            }
            return count;
        }

        public int UpdatePublish(DbTransaction transaction,
            Guid ProjectId, Guid progress, Guid PublishOperator, DateTime PublishPublishDate, Guid PublishTopNewsNeeds
            , Guid PublishNoticeNeeds, string PublishCommonCategory, string PublishGovernmentCategory, string PublishFinanceCategory
            , string PublishBankCategory, Guid PublishPageState, Guid PublishPlayState, string PublishNote)
        {
            DbParameter[] parameters = new DbParameter[13];
            parameters[0] = Manager.CreateParameter("ProjectId", ProjectId);
            parameters[1] = Manager.CreateParameter("progress", progress);
            parameters[2] = Manager.CreateParameter("PublishOperator", PublishOperator);
            parameters[3] = Manager.CreateParameter("PublishPublishDate", PublishPublishDate);
            parameters[4] = Manager.CreateParameter("PublishTopNewsNeeds", PublishTopNewsNeeds);
            parameters[5] = Manager.CreateParameter("PublishNoticeNeeds", PublishNoticeNeeds);
            parameters[6] = Manager.CreateParameter("PublishCommonCategory", PublishCommonCategory);
            parameters[7] = Manager.CreateParameter("PublishGovernmentCategory", PublishGovernmentCategory);
            parameters[8] = Manager.CreateParameter("PublishFinanceCategory", PublishFinanceCategory);
            parameters[9] = Manager.CreateParameter("PublishBankCategory", PublishBankCategory);
            parameters[10] = Manager.CreateParameter("PublishPageState", PublishPageState);
            parameters[11] = Manager.CreateParameter("PublishPlayState", PublishPlayState);
            parameters[12] = Manager.CreateParameter("PublishNote", PublishNote);
            int count = this.ModifyOperate.Modify(transaction, Project.UPDATE_Publish, parameters);
            return count;
        }
        public int UpdateCheck(Guid ProjectId, Guid progress, Guid CheckPersonInCharge, DateTime CheckTaskCheckDate, Guid CheckTaskCourseCommend
            , Guid CheckTaskCategoryCheck, string CheckTaskNote)
        {
            int count = 0;
            DbTransaction transaction = this.BeginTransaction();
            try
            {
                count = this.UpdateCheck(transaction, ProjectId, progress, CheckPersonInCharge, CheckTaskCheckDate, CheckTaskCourseCommend, CheckTaskCategoryCheck, CheckTaskNote);
                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
            }
            finally
            {
                this.EndTransaction();
            }
            return count;
        }
        public int UpdateCheck(DbTransaction transaction,
            Guid ProjectId, Guid progress, Guid CheckPersonInCharge, DateTime CheckTaskCheckDate, Guid CheckTaskCourseCommend
            , Guid CheckTaskCategoryCheck, string CheckTaskNote)
        {
            DbParameter[] parameters = new DbParameter[7];
            parameters[0] = Manager.CreateParameter("ProjectId", ProjectId);
            parameters[1] = Manager.CreateParameter("progress", progress);
            parameters[2] = Manager.CreateParameter("CheckPersonInCharge", CheckPersonInCharge);
            parameters[3] = Manager.CreateParameter("CheckTaskCheckDate", CheckTaskCheckDate);
            parameters[4] = Manager.CreateParameter("CheckTaskCourseCommend", CheckTaskCourseCommend);
            parameters[5] = Manager.CreateParameter("CheckTaskCategoryCheck", CheckTaskCategoryCheck);
            parameters[6] = Manager.CreateParameter("CheckTaskNote", CheckTaskNote);
            int count = this.ModifyOperate.Modify(transaction, Project.UPDATE_Check, parameters);
            return count;
        }
        //
        public int UpdateCaptureDelayReceive(Guid ProjectId, Guid progress, Guid CapturePersonInCharge, DateTime CaptureReceiveDelayDate, string CaptureReceiveDelayNote)
        {
            int count = 0;
            DbTransaction transaction = this.BeginTransaction();
            try
            {
                count = this.UpdateCaptureDelayReceive(transaction, ProjectId, progress, CapturePersonInCharge, CaptureReceiveDelayDate, CaptureReceiveDelayNote);
                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
            }
            finally
            {
                this.EndTransaction();
            }
            return count;
        }
        public int UpdateCaptureDelayReceive(DbTransaction transaction,
           Guid ProjectId, Guid progress, Guid CapturePersonInCharge, DateTime CaptureReceiveDelayDate, string CaptureReceiveDelayNote)
        {
            DbParameter[] parameters = new DbParameter[5];
            parameters[0] = Manager.CreateParameter("ProjectId", ProjectId);
            parameters[1] = Manager.CreateParameter("progress", progress);
            parameters[2] = Manager.CreateParameter("CapturePersonInCharge", CapturePersonInCharge);
            parameters[3] = Manager.CreateParameter("CaptureReceiveDelayDate", CaptureReceiveDelayDate);
            parameters[4] = Manager.CreateParameter("CaptureReceiveDelayNote", CaptureReceiveDelayNote);
            int count = this.ModifyOperate.Modify(transaction, Project.UPDATE_CaptureDelayReceive, parameters);
            return count;
        }
        //
        public int UpdateProductionDelayReceive(Guid ProjectId, Guid progress, Guid ProductionProgress, Guid ProductionPersonInCharge, DateTime ProductionReceiveDelayDate, string ProductionReceiveDelayNote)
        {
            int count = 0;
            DbTransaction transaction = this.BeginTransaction();
            try
            {
                count = this.UpdateProductionDelayReceive(transaction, ProjectId, progress, ProductionProgress, ProductionPersonInCharge, ProductionReceiveDelayDate, ProductionReceiveDelayNote);
                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
            }
            finally
            {
                this.EndTransaction();
            }
            return count;
        }
        public int UpdateProductionDelayReceive(DbTransaction transaction,
           Guid ProjectId, Guid progress, Guid ProductionProgress, Guid ProductionPersonInCharge, DateTime ProductionReceiveDelayDate, string ProductionReceiveDelayNote)
        {
            DbParameter[] parameters = new DbParameter[6];
            parameters[0] = Manager.CreateParameter("ProjectId", ProjectId);
            parameters[1] = Manager.CreateParameter("progress", progress);
            parameters[2] = Manager.CreateParameter("ProductionProgress", ProductionProgress);
            parameters[3] = Manager.CreateParameter("ProductionPersonInCharge", ProductionPersonInCharge);
            parameters[4] = Manager.CreateParameter("ProductionReceiveDelayDate", ProductionReceiveDelayDate);
            parameters[5] = Manager.CreateParameter("ProductionReceiveDelayNote", ProductionReceiveDelayNote);
            int count = this.ModifyOperate.Modify(transaction, Project.UPDATE_ProductionDelayReceive, parameters);
            return count;
        }
        #endregion update
    }
}
