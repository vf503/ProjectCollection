using Adapt.Database;
using System;
using System.Data;
using System.Data.Common;

namespace ProjectCollection.DAL
{
    public class ProjectPlan : BaseDatabase
    {
        #region sql

        #region SELECT

        private const string SELECT_01 = @"select ProjectPlanId, ProjectPlanNo, title, ProjectPlanTypeId, MakingDate, PlanDate, ExecuteDate, progress, CreatorId, note, LastModifierId, LastModifyDate, 
                      Lecturer, LeadDate, CourseCount, Price, Source, ProjectPlan.Category, RecordingPersonInCharge, RecordingDate, RecordingPlace, RecordingScriptHolder, RecordingLecture, 
                      RecordingProgress, RecordingNote, FileDeliverDate, RecordingFile, LecturerJob, ExtraNote
,dd1.text as ProjectPlanTypeText,dd2.text as ProgressText 
,ui.real_name as CreatorName
,(select count(ProjectId) from Project where Project.ProjectPlanId=ProjectPlan.ProjectPlanId) 
as ProjectCount
,(select count(ProjectId) from Project 
where Project.ProjectPlanId=ProjectPlan.ProjectPlanId and Project.progress in ('00000000-0000-0000-0000-000000000119','00000000-0000-0000-0000-000000000128') ) 
as ProjectFinishCount
,(select count(ProjectId) from Project 
where Project.ProjectPlanId=ProjectPlan.ProjectPlanId and (Project.CaptureReceiveDelayDate is not null or Project.ProductionReceiveDelayDate is not null) ) 
as ProjectDelayCount
,ROW_NUMBER() OVER (order by progress Asc, PlanDate Asc) as RowNo
from ProjectPlan
left join data_dictionary as dd1 on dd1.dictionary_identity=ProjectPlan.ProjectPlanTypeId 
left join data_dictionary as dd2 on dd2.dictionary_identity=ProjectPlan.progress
left join user_info as ui on ui.user_identity=ProjectPlan.CreatorId";
        private const string SELECT_02 = SELECT_01 + " where ProjectPlanId=@ProjectPlanId";
        //private const string SELECT_03 = SELECT_01 + " where progress<> '00000000-0000-0000-0000-000000000126' order by PlanDate Asc";
        private const string SELECT_03 = SELECT_01 + @" where ((select count(ProjectId) from Project 
where Project.ProjectPlanId=ProjectPlan.ProjectPlanId and Project.progress in ('00000000-0000-0000-0000-000000000119','00000000-0000-0000-0000-000000000128') )  
<> (select count(ProjectId) from Project where Project.ProjectPlanId=ProjectPlan.ProjectPlanId))
or (progress<> '00000000-0000-0000-0000-000000000126') 
order by PlanDate Asc";
        private const string SELECT_04 = SELECT_01 + " order by progress Asc, PlanDate Asc";
        private const string SELECT_Finished = SELECT_01 + @" where ((select count(ProjectId) from Project 
where Project.ProjectPlanId=ProjectPlan.ProjectPlanId and Project.progress in ('00000000-0000-0000-0000-000000000119','00000000-0000-0000-0000-000000000128') )  
= (select count(ProjectId) from Project where Project.ProjectPlanId=ProjectPlan.ProjectPlanId))
and (progress= '00000000-0000-0000-0000-000000000126')";
        private const string SELECT_Unfinish = SELECT_01 + @" where ((select count(ProjectId) from Project 
where Project.ProjectPlanId=ProjectPlan.ProjectPlanId and Project.progress in ('00000000-0000-0000-0000-000000000119','00000000-0000-0000-0000-000000000128') )  
<> (select count(ProjectId) from Project where Project.ProjectPlanId=ProjectPlan.ProjectPlanId))
or (progress<> '00000000-0000-0000-0000-000000000126')";
        private const string SELECT_Recond = SELECT_01 + " where progress='00000000-0000-0000-0000-000000000101' order by progress Asc, PlanDate Asc";
        
        private const string SELECT_ProgressText01 = @"select *,dd1.text as ProgressText from ProjectPlan 
left join data_dictionary as dd1 on dd1.dictionary_identity=ProjectPlan.progress ";
        private const string SELECT_ProgressText02 = SELECT_ProgressText01 + " where ProjectPlanId=@ProjectPlanId";

        #region Page
        private const string SELECT_Page_All = @"select top 8 * from (" + SELECT_01 + ") as p where RowNo > @PageIndex*8";
        private const string SELECT_Page_Unfinish = @"select top 8 * from (" + SELECT_Unfinish + ") as p where RowNo > @PageIndex*8";
        private const string SELECT_Page_Finished = @"select top 8 * from (" + SELECT_Finished + ") as p where RowNo > @PageIndex*8";
        #endregion Page

        #region Total

        private const string SELECT_Total_Today = @"select count(ProjectPlanId)
from ProjectPlan
where CONVERT(varchar(10),MakingDate,120) = CONVERT(varchar(10),getDate(),120) ";
        private const string SELECT_Total = "select count(ProjectPlanId) from ProjectPlan";
        private const string SELECT_Total_Finish = @"select count(distinct PL.ProjectPlanId)
from ProjectPlan as PL
left join Project as P on PL.ProjectPlanId=P.ProjectPlanId
where  P.progress in('00000000-0000-0000-0000-000000000119','00000000-0000-0000-0000-000000000128')";
        private const string SELECT_Total_Delay = @"select  COUNT(distinct PL.ProjectPlanId)
from ProjectPlan as PL
left join Project as P on PL.ProjectPlanId=P.ProjectPlanId
where P.CaptureReceiveDelayDate is not null or P.ProductionReceiveDelayDate is not null";
        private const string SELECT_Total_Type = @"select count(ProjectPlanId)
from ProjectPlan
where ProjectPlanTypeId=@ProjectPlanTypeId and datepart(yy, MakingDate) = datepart(yy, GETDATE())";

        #endregion Total

        #region Amount
        private const string SELECT_RecondAmount = @"select count(ProjectPlanId) from ProjectPlan
where RecordingDate between @DateBegin and @DateEnd
and RecordingPersonInCharge=@RecordingPersonInCharge";
        #endregion Amount

        #endregion SELECT

        #region INSERT

        private const string INSERT_01 = @"insert into ProjectPlan(
ProjectPlanId
,ProjectPlanNo
,title
,ProjectPlanTypeId
,MakingDate
,PlanDate
,progress
,CreatorId
,Category
,CourseCount
,Price
,Source
,note
,Lecturer
,ExtraNote
,RecordingPersonInCharge
) values(
@ProjectPlanId
,@ProjectPlanNo
,@title
,@ProjectPlanTypeId
,@MakingDate
,@PlanDate
,@progress
,@CreatorId
,@Category
,@CourseCount
,@Price
,@Source
,@note
,@Lecturer
,@ExtraNote
,@RecordingPersonInCharge
)";
        #endregion INSERT

        #region UPDATE

        private const string UPDATE_01 = @"update ProjectPlan set 
ProjectPlanNo=@ProjectPlanNo
,title=@title
,ProjectPlanTypeId=@ProjectPlanTypeId
,PlanDate=@PlanDate
,Category=@Category
,CourseCount=@CourseCount
,Price=@Price
,Source=@Source
,note=@note
,Lecturer=@Lecturer
,ExtraNote=@ExtraNote
,RecordingPersonInCharge=@RecordingPersonInCharge
where ProjectPlanId=@ProjectPlanId";
        private const string UPDATE_02 = @"update ProjectPlan set 
progress=@progress
where ProjectPlanId=@ProjectPlanId";
        private const string UPDATE_Recond = @"update ProjectPlan set 
progress=@progress
,RecordingDate=@RecordingDate
,RecordingPlace=@RecordingPlace
,RecordingScriptHolder=@RecordingScriptHolder
,RecordingLecture=@RecordingLecture
,RecordingFile=@RecordingFile
,FileDeliverDate=@FileDeliverDate
,RecordingNote=@RecordingNote
where ProjectPlanId=@ProjectPlanId";

        #endregion UPDATE

        #endregion sql

        #region select

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public DataTable SelectAll()
        {
            DataTable table = this.SelectOperate.Select(ProjectPlan.SELECT_04);
            return table;
        }

        public DataTable Select()
        {
            DataTable table = this.SelectOperate.Select(ProjectPlan.SELECT_03);
            return table;
        }

        public DataTable SelectPageAll(int PageIndex)
        {
            DbParameter[] parameters = new DbParameter[1];
            parameters[0] = Manager.CreateParameter("PageIndex", PageIndex);
            DataTable table = this.SelectOperate.Select(ProjectPlan.SELECT_Page_All, parameters);
            return table;
        }

        public DataTable SelectPageUnfinish(int PageIndex)
        {
            DbParameter[] parameters = new DbParameter[1];
            parameters[0] = Manager.CreateParameter("PageIndex", PageIndex);
            DataTable table = this.SelectOperate.Select(ProjectPlan.SELECT_Page_Unfinish, parameters);
            return table;
        }

        public DataTable SelectPageFinished(int PageIndex)
        {
            DbParameter[] parameters = new DbParameter[1];
            parameters[0] = Manager.CreateParameter("PageIndex", PageIndex);
            DataTable table = this.SelectOperate.Select(ProjectPlan.SELECT_Finished, parameters);
            return table;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public DataTable Select(Guid projectPlanId)
        {
            DbParameter[] parameters = new DbParameter[1];
            parameters[0] = Manager.CreateParameter("ProjectPlanId", projectPlanId);
            DataTable table = this.SelectOperate.Select(ProjectPlan.SELECT_02, parameters);
            return table;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public DataTable SelectRecond()
        {
            DataTable table = this.SelectOperate.Select(ProjectPlan.SELECT_Recond);
            return table;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public DataTable SelectProgressText(Guid projectPlanId)
        {
            DataTable table = this.SelectOperate.Select(ProjectPlan.SELECT_ProgressText02);
            return table;
        }

        #region Total 

        public int SelectTotal()
        {
            DataTable table = this.SelectOperate.Select(ProjectPlan.SELECT_Total);
            return Convert.ToInt32(table.Rows[0][0]);
        }

        public int SelectTotalToday()
        {
            DataTable table = this.SelectOperate.Select(ProjectPlan.SELECT_Total_Today);
            return Convert.ToInt16(table.Rows[0][0]);
        }

        public int SelectTotalFinish()
        {
            DataTable table = this.SelectOperate.Select(ProjectPlan.SELECT_Total_Finish);
            return Convert.ToInt16(table.Rows[0][0]);
        }

        public int SelectTotalDelay()
        {
            DataTable table = this.SelectOperate.Select(ProjectPlan.SELECT_Total_Delay);
            return Convert.ToInt16(table.Rows[0][0]);
        }

        public int SelectTotalType(Guid TypeId)
        {
            DbParameter[] parameters = new DbParameter[1];
            parameters[0] = Manager.CreateParameter("ProjectPlanTypeId", TypeId);
            DataTable table = this.SelectOperate.Select(ProjectPlan.SELECT_Total_Type,parameters);
            return Convert.ToInt16(table.Rows[0][0]);
        }

        #endregion Total 

        #region Amount
        public int SelectRecondAmount(Guid RecordingPersonInCharge,string DateBegin,string DateEnd)
        {
            DbParameter[] parameters = new DbParameter[3];
            parameters[0] = Manager.CreateParameter("RecordingPersonInCharge", RecordingPersonInCharge);
            parameters[1] = Manager.CreateParameter("DateBegin", DateBegin);
            parameters[2] = Manager.CreateParameter("DateEnd", DateEnd);
            DataTable table = this.SelectOperate.Select(ProjectPlan.SELECT_RecondAmount, parameters);
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
        public int Insert(Guid projectPlanRecordId, string projectPlanNo, string title, Guid projectPlanTypeId, DateTime PlanDate, Guid progress, Guid creatorId, string Category, decimal CourseCount, int Price, string Source, string Note, string Lecturer, string ExtraNote,Guid RecordingPersonInCharge)
        {
            int count = 0;
            DbTransaction transaction = this.BeginTransaction();
            try
            {
                count = this.Insert(transaction, projectPlanRecordId, projectPlanNo, title, projectPlanTypeId, PlanDate, progress, creatorId, Category, CourseCount, Price, Source, Note, Lecturer, ExtraNote, RecordingPersonInCharge);
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
            Guid projectPlanRecordId, string projectPlanNo, string title, Guid projectPlanTypeId, DateTime planDate, Guid progress, Guid creatorId, string category, decimal courseCount, int price, string source, string note, string lecturer, string ExtraNote, Guid RecordingPersonInCharge)
        {
            DbParameter[] parameters = new DbParameter[16];
            parameters[0] = Manager.CreateParameter("ProjectPlanId", projectPlanRecordId);
            parameters[1] = Manager.CreateParameter("ProjectPlanNo", projectPlanNo);
            parameters[2] = Manager.CreateParameter("title", title);
            parameters[3] = Manager.CreateParameter("ProjectPlanTypeId", projectPlanTypeId);
            parameters[4] = Manager.CreateParameter("MakingDate", DateTime.Now);
            parameters[5] = Manager.CreateParameter("PlanDate", planDate);
            parameters[6] = Manager.CreateParameter("progress", progress);
            parameters[7] = Manager.CreateParameter("CreatorId", creatorId);
            parameters[8] = Manager.CreateParameter("Category", category);
            parameters[9] = Manager.CreateParameter("CourseCount", courseCount);
            parameters[10] = Manager.CreateParameter("Price", price);
            parameters[11] = Manager.CreateParameter("Source", source);
            parameters[12] = Manager.CreateParameter("Note", note);
            parameters[13] = Manager.CreateParameter("Lecturer", lecturer);
            parameters[14] = Manager.CreateParameter("ExtraNote", ExtraNote);
            parameters[15] = Manager.CreateParameter("RecordingPersonInCharge", RecordingPersonInCharge);
            int count = this.ModifyOperate.Modify(transaction, ProjectPlan.INSERT_01, parameters);
            return count;
        }

        #endregion

        #region update

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
        public int Update(Guid projectPlanRecordId, string projectPlanNo, string title, Guid projectPlanTypeId, DateTime planDate, string category, decimal courseCount, int price, string source, string note, string lecturer, string ExtraNote,Guid RecordingPersonInCharge)
        {
            int count = 0;
            DbTransaction transaction = this.BeginTransaction();
            try
            {
                count = this.Update(transaction, projectPlanRecordId, projectPlanNo, title, projectPlanTypeId, planDate, category, courseCount, price, source, note, lecturer, ExtraNote, RecordingPersonInCharge);
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
        public int Update(DbTransaction transaction,
            Guid projectPlanRecordId, string projectPlanNo, string title, Guid projectPlanTypeId, DateTime planDate, string category, decimal courseCount, int price, string source, string note, string lecturer, string ExtraNote, Guid RecordingPersonInCharge)
        {
            DbParameter[] parameters = new DbParameter[13];
            parameters[0] = Manager.CreateParameter("ProjectPlanId", projectPlanRecordId);
            parameters[1] = Manager.CreateParameter("ProjectPlanNo", projectPlanNo);
            parameters[2] = Manager.CreateParameter("title", title);
            parameters[3] = Manager.CreateParameter("ProjectPlanTypeId", projectPlanTypeId);
            parameters[4] = Manager.CreateParameter("PlanDate", planDate);
            parameters[5] = Manager.CreateParameter("Category", category);
            parameters[6] = Manager.CreateParameter("CourseCount", courseCount);
            parameters[7] = Manager.CreateParameter("Price", price);
            parameters[8] = Manager.CreateParameter("Source", source);
            parameters[9] = Manager.CreateParameter("Note", note);
            parameters[10] = Manager.CreateParameter("Lecturer", lecturer);
            parameters[11] = Manager.CreateParameter("ExtraNote", ExtraNote);
            parameters[12] = Manager.CreateParameter("RecordingPersonInCharge", RecordingPersonInCharge);
            int count = this.ModifyOperate.Modify(transaction, ProjectPlan.UPDATE_01, parameters);
            return count;
        }

        public int Update(Guid projectPlanId,Guid progress)
        {
            int count = 0;
            DbTransaction transaction = this.BeginTransaction();
            try
            {
                count = this.Update(transaction, projectPlanId, progress);
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
        public int Update(DbTransaction transaction,
            Guid projectPlanId,Guid progress)
        {
            DbParameter[] parameters = new DbParameter[2];
            parameters[0] = Manager.CreateParameter("ProjectPlanId", projectPlanId);
            parameters[1] = Manager.CreateParameter("progress", progress);
            int count = this.ModifyOperate.Modify(transaction, ProjectPlan.UPDATE_02, parameters);
            return count;
        }

        #endregion

        #region UpdateRecond

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
        public int UpdateRecond(Guid projectPlanRecordId, Guid progress, DateTime RecordingDate, string RecordingPlace, bool RecordingScriptHolder, int RecordingLecture, string RecordingFile, DateTime FileDeliverDate, string RecordingNote)
        {
            int count = 0;
            DbTransaction transaction = this.BeginTransaction();
            try
            {
                count = this.UpdateRecond(transaction, projectPlanRecordId, progress,RecordingDate,RecordingPlace,RecordingScriptHolder,RecordingLecture,RecordingFile,FileDeliverDate,RecordingNote);
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
        public int UpdateRecond(DbTransaction transaction,
            Guid projectPlanRecordId, Guid progress,DateTime RecordingDate, string RecordingPlace, bool RecordingScriptHolder, int RecordingLecture, string RecordingFile, DateTime FileDeliverDate, string RecordingNote)
        {
            DbParameter[] parameters = new DbParameter[9];
            parameters[0] = Manager.CreateParameter("ProjectPlanId", projectPlanRecordId);
            parameters[1] = Manager.CreateParameter("progress", progress);
            parameters[2] = Manager.CreateParameter("RecordingDate", RecordingDate);
            parameters[3] = Manager.CreateParameter("RecordingPlace", RecordingPlace);
            parameters[4] = Manager.CreateParameter("RecordingScriptHolder", RecordingScriptHolder);
            parameters[5] = Manager.CreateParameter("RecordingLecture", RecordingLecture);
            parameters[6] = Manager.CreateParameter("RecordingFile", RecordingFile);
            parameters[7] = Manager.CreateParameter("FileDeliverDate", FileDeliverDate);
            parameters[8] = Manager.CreateParameter("RecordingNote", RecordingNote);
            int count = this.ModifyOperate.Modify(transaction, ProjectPlan.UPDATE_Recond, parameters);
            return count;
        }

        #endregion
    }
}
