using Adapt.Database;
using System;
using System.Data;
using System.Data.Common;

namespace ProjectCollection.DAL
{
    public class CustomProject : BaseDatabase
    {
        #region sql

        #region SELECT

        private const string SELECT = @"select *,PL.ProjectPlanTypeId as PlanTypeId,DD1.text as ProgressText from CustomProject as CP
left join ProjectPlan as PL on PL.ProjectPlanId=CP.ProjectPlanId
left join data_dictionary as DD1 on DD1.dictionary_identity=CP.progress";
        private const string OrderBy = " order by SendingDate Desc";
        private const string SELECT_ALL = SELECT + OrderBy;
        private const string SELECT_Id = SELECT + " where CustomProjectId=@CustomProjectId" + OrderBy;
        private const string SELECT_Type = SELECT + " where PL.ProjectPlanTypeId=@ProjectPlanTypeId" + OrderBy;
        private const string SELECT_Type_Progress = SELECT + " where PL.ProjectPlanTypeId=@ProjectPlanTypeId and CP.Progress=@Progress" + OrderBy;
        private const string SELECT_PlanId = SELECT + " where PL.ProjectPlanId=@ProjectPlanId" + OrderBy;

        #region Amount

        private const string SELECT_ReceiveAmount = @"select count(CustomProjectId) from CustomProject
where ReceiveDate between @DateBegin and @DateEnd
and Signer=@Signer";

        private const string SELECT_FinishAmount = @"select count(CustomProjectId) from CustomProject
where FinishDate between @DateBegin and @DateEnd
and Operator=@Operator";

        private const string SELECT_PublishAmount = @"select count(CustomProjectId) from CustomProject
where PublishDate between @DateBegin and @DateEnd
and Publisher=@Publisher";

        private const string SELECT_CheckAmount = @"select count(CustomProjectId) from CustomProject
where CheckDate between @DateBegin and @DateEnd
and Checker=@Checker";

        #endregion Amount

        #endregion SELECT

        #region INSERT

        private const string INSERT_New = @"insert into CustomProject(
CustomProjectId
,No
,ProjectPlanId
,SendingDate
,Title
,CourseAmount
,Lecturer
,LecturerJob
,CourseSource
,Category
,PublishNeeds
,Note
,ExtraNote
,Progress
) values(
@CustomProjectId
,@No
,@ProjectPlanId
,@SendingDate
,@Title
,@CourseAmount
,@Lecturer
,@LecturerJob
,@CourseSource
,@Category
,@PublishNeeds
,@Note
,@ExtraNote
,@Progress
)";

        #endregion INSERT

        #region UPDATE

        private const string UPDATE_Receive = @"update CustomProject set 
Progress=@Progress
,Signer=@Signer 
,ReceiveDate=@ReceiveDate
,ReceiveNote=@ReceiveNote
,Operator=@Operator
where CustomProjectId=@CustomProjectId";
        private const string UPDATE_Operation = @"update CustomProject set 
Progress=@Progress
,FinishDate=@FinishDate
,OperationNote=@OperationNote
where CustomProjectId=@CustomProjectId";
        private const string UPDATE_Publish = @"update CustomProject set 
Progress=@Progress
,Publisher=@Publisher
,PublishDate=@PublishDate
,PublishNote=@PublishNote
,PublishCheck=@PublishCheck
where CustomProjectId=@CustomProjectId";
        private const string UPDATE_Check = @"update CustomProject set 
Progress=@Progress
,Checker=@Checker
,CheckDate=@CheckDate
,CheckNote=@CheckNote
,CategoryCheck=@CategoryCheck
where CustomProjectId=@CustomProjectId";

        #endregion UPDATE

        #endregion sql

        #region Select

        public DataTable SelectAll()
        {
            DataTable table = this.SelectOperate.Select(CustomProject.SELECT_ALL);
            return table;
        }

        public DataTable SelectId(Guid CustomProjectId)
        {
            DbParameter[] parameters = new DbParameter[1];
            parameters[0] = Manager.CreateParameter("CustomProjectId", CustomProjectId);
            DataTable table = this.SelectOperate.Select(CustomProject.SELECT_Id, parameters);
            return table;
        }

        public DataTable SelectType(Guid ProjectPlanTypeId)
        {
            DbParameter[] parameters = new DbParameter[1];
            parameters[0] = Manager.CreateParameter("ProjectPlanTypeId", ProjectPlanTypeId);
            DataTable table = this.SelectOperate.Select(CustomProject.SELECT_Type, parameters);
            return table;
        }

        public DataTable SelectTypeProgress(Guid ProjectPlanTypeId, Guid Progress)
        {
            DbParameter[] parameters = new DbParameter[2];
            parameters[0] = Manager.CreateParameter("ProjectPlanTypeId", ProjectPlanTypeId);
            parameters[1] = Manager.CreateParameter("Progress", Progress);
            DataTable table = this.SelectOperate.Select(CustomProject.SELECT_Type_Progress, parameters);
            return table;
        }

        #region Amount
        public int SelectReceiveAmount(Guid Signer, string DateBegin, string DateEnd)
        {
            DbParameter[] parameters = new DbParameter[3];
            parameters[0] = Manager.CreateParameter("Signer", Signer);
            parameters[1] = Manager.CreateParameter("DateBegin", DateBegin);
            parameters[2] = Manager.CreateParameter("DateEnd", DateEnd);
            DataTable table = this.SelectOperate.Select(CustomProject.SELECT_ReceiveAmount, parameters);
            int count = Convert.ToInt32(table.Rows[0][0]);
            return count;
        }

        public int SelectFinishAmount(Guid Operator, string DateBegin, string DateEnd)
        {
            DbParameter[] parameters = new DbParameter[3];
            parameters[0] = Manager.CreateParameter("Operator", Operator);
            parameters[1] = Manager.CreateParameter("DateBegin", DateBegin);
            parameters[2] = Manager.CreateParameter("DateEnd", DateEnd);
            DataTable table = this.SelectOperate.Select(CustomProject.SELECT_FinishAmount, parameters);
            int count = Convert.ToInt32(table.Rows[0][0]);
            return count;
        }

        public int SelectPublishAmount(Guid Publisher, string DateBegin, string DateEnd)
        {
            DbParameter[] parameters = new DbParameter[3];
            parameters[0] = Manager.CreateParameter("Publisher", Publisher);
            parameters[1] = Manager.CreateParameter("DateBegin", DateBegin);
            parameters[2] = Manager.CreateParameter("DateEnd", DateEnd);
            DataTable table = this.SelectOperate.Select(CustomProject.SELECT_PublishAmount, parameters);
            int count = Convert.ToInt32(table.Rows[0][0]);
            return count;
        }

        public int SelectCheckAmount(Guid Checker, string DateBegin, string DateEnd)
        {
            DbParameter[] parameters = new DbParameter[3];
            parameters[0] = Manager.CreateParameter("Checker", Checker);
            parameters[1] = Manager.CreateParameter("DateBegin", DateBegin);
            parameters[2] = Manager.CreateParameter("DateEnd", DateEnd);
            DataTable table = this.SelectOperate.Select(CustomProject.SELECT_CheckAmount, parameters);
            int count = Convert.ToInt32(table.Rows[0][0]);
            return count;
        }
        #endregion Amount

        #endregion Select

        #region Insert

        public int Insert(Guid CustomProjectId, string No, Guid ProjectPlanId, DateTime SendingDate, string Title, string CourseAmount,
            string Lecturer, string LecturerJob, string CourseSource, string Category, Guid PublishNeeds, string Note, string ExtraNote, Guid Progress)
        {
            int count = 0;
            DbTransaction transaction = this.BeginTransaction();
            try
            {
                count = this.Insert(transaction, CustomProjectId, No, ProjectPlanId, SendingDate, Title, CourseAmount, Lecturer, LecturerJob
                    , CourseSource, Category, PublishNeeds, Note, ExtraNote, Progress);
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

        public int Insert(DbTransaction transaction,
           Guid CustomProjectId, string No, Guid ProjectPlanId, DateTime SendingDate, string Title, string CourseAmount,
            string Lecturer, string LecturerJob, string CourseSource, string Category, Guid PublishNeeds, string Note, string ExtraNote, Guid Progress)
        {
            DbParameter[] parameters = new DbParameter[14];
            parameters[0] = Manager.CreateParameter("CustomProjectId", CustomProjectId);
            parameters[1] = Manager.CreateParameter("No", No);
            parameters[2] = Manager.CreateParameter("ProjectPlanId", ProjectPlanId);
            parameters[3] = Manager.CreateParameter("SendingDate", DateTime.Now);
            parameters[4] = Manager.CreateParameter("Title", Title);
            parameters[5] = Manager.CreateParameter("CourseAmount", CourseAmount);
            parameters[6] = Manager.CreateParameter("Lecturer", Lecturer);
            parameters[7] = Manager.CreateParameter("LecturerJob", LecturerJob);
            parameters[8] = Manager.CreateParameter("CourseSource", CourseSource);
            parameters[9] = Manager.CreateParameter("Category", Category);
            parameters[10] = Manager.CreateParameter("PublishNeeds", PublishNeeds);
            parameters[11] = Manager.CreateParameter("Note", Note);
            parameters[12] = Manager.CreateParameter("ExtraNote", ExtraNote);
            parameters[13] = Manager.CreateParameter("Progress", Progress);

            int count = this.ModifyOperate.Modify(transaction, CustomProject.INSERT_New, parameters);
            return count;
        }

        #endregion Insert

        #region Update

        public int UpdateReceive(Guid CustomProjectId, Guid Progress, Guid Signer, DateTime ReceiveDate, string ReceiveNote, Guid Operator)
        {
            int count = 0;
            DbTransaction transaction = this.BeginTransaction();
            try
            {
                count = this.UpdateReceive(transaction, CustomProjectId, Progress, Signer, ReceiveDate, ReceiveNote, Operator);
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

        public int UpdateReceive(DbTransaction transaction,
            Guid CustomProjectId, Guid Progress, Guid Signer, DateTime ReceiveDate, string ReceiveNote, Guid Operator)
        {
            DbParameter[] parameters = new DbParameter[6];
            parameters[0] = Manager.CreateParameter("CustomProjectId", CustomProjectId);
            parameters[1] = Manager.CreateParameter("Progress", Progress);
            parameters[2] = Manager.CreateParameter("Signer", Signer);
            parameters[3] = Manager.CreateParameter("ReceiveDate", ReceiveDate);
            parameters[4] = Manager.CreateParameter("ReceiveNote", ReceiveNote);
            parameters[5] = Manager.CreateParameter("Operator", Operator);
            int count = this.ModifyOperate.Modify(transaction, CustomProject.UPDATE_Receive, parameters);
            return count;
        }

        public int UpdateOperation(Guid CustomProjectId, Guid Progress, DateTime FinishDate, string OperationNote)
        {
            int count = 0;
            DbTransaction transaction = this.BeginTransaction();
            try
            {
                count = this.UpdateOperation(transaction, CustomProjectId, Progress, FinishDate, OperationNote);
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

        public int UpdateOperation(DbTransaction transaction,
            Guid CustomProjectId, Guid Progress, DateTime FinishDate, string OperationNote)
        {
            DbParameter[] parameters = new DbParameter[4];
            parameters[0] = Manager.CreateParameter("CustomProjectId", CustomProjectId);
            parameters[1] = Manager.CreateParameter("Progress", Progress);
            parameters[2] = Manager.CreateParameter("FinishDate", FinishDate);
            parameters[3] = Manager.CreateParameter("OperationNote", OperationNote);
            int count = this.ModifyOperate.Modify(transaction, CustomProject.UPDATE_Operation, parameters);
            return count;
        }

        public int UpdatePublish(Guid CustomProjectId, Guid Progress,Guid Publisher,DateTime PublishDate, string PublishNote, Guid PublishCheck)
        {
            int count = 0;
            DbTransaction transaction = this.BeginTransaction();
            try
            {
                count = this.UpdatePublish(transaction, CustomProjectId, Progress, Publisher, PublishDate, PublishNote, PublishCheck);
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
            Guid CustomProjectId, Guid Progress, Guid Publisher, DateTime PublishDate, string PublishNote, Guid PublishCheck)
        {
            DbParameter[] parameters = new DbParameter[6];
            parameters[0] = Manager.CreateParameter("CustomProjectId", CustomProjectId);
            parameters[1] = Manager.CreateParameter("Progress", Progress);
            parameters[2] = Manager.CreateParameter("Publisher", Publisher);
            parameters[3] = Manager.CreateParameter("PublishDate", PublishDate);
            parameters[4] = Manager.CreateParameter("PublishNote", PublishNote);
            parameters[5] = Manager.CreateParameter("PublishCheck", PublishCheck);
            int count = this.ModifyOperate.Modify(transaction, CustomProject.UPDATE_Publish, parameters);
            return count;
        }

        public int UpdateCheck(Guid CustomProjectId, Guid Progress, Guid Checker, DateTime CheckDate, string CheckNote, Guid CategoryCheck)
        {
            int count = 0;
            DbTransaction transaction = this.BeginTransaction();
            try
            {
                count = this.UpdateCheck(transaction, CustomProjectId, Progress, Checker, CheckDate, CheckNote, CategoryCheck);
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
            Guid CustomProjectId, Guid Progress, Guid Checker, DateTime CheckDate, string CheckNote, Guid CategoryCheck)
        {
            DbParameter[] parameters = new DbParameter[6];
            parameters[0] = Manager.CreateParameter("CustomProjectId", CustomProjectId);
            parameters[1] = Manager.CreateParameter("Progress", Progress);
            parameters[2] = Manager.CreateParameter("Checker", Checker);
            parameters[3] = Manager.CreateParameter("CheckDate", CheckDate);
            parameters[4] = Manager.CreateParameter("CheckNote", CheckNote);
            parameters[5] = Manager.CreateParameter("CategoryCheck", CategoryCheck);
            int count = this.ModifyOperate.Modify(transaction, CustomProject.UPDATE_Check, parameters);
            return count;
        }
        #endregion Update
    }
}
