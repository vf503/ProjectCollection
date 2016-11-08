using Adapt.Database;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectCollection.DAL
{
    public class ProjectPlanRecord : BaseDatabase
    {

        #region sql
        private const string INSERT_01 = @"insert into ProjectPlanRecord(
ProjectPlanRecordId
,ProjectPlanId
,item
,note
,date)
values(
@ProjectPlanRecordId
,@ProjectPlanId
,@item,
,@note,
,@date)";
        private const string UPDATE_01 = @"update ProjectPlanRecord set
ProjectPlanId=@ProjectPlanId
,item=@item
,note=@note
,date=@date
where ProjectPlanRecordId=@ProjectPlanRecordId"; // TODO
        #endregion

        #region select

        #endregion

        #region insert

        public int Insert(Guid projectPlanRecordId, Guid projectPlanId, string item, string note, DateTime date)
        {
            int count = 0;
            DbTransaction transaction = this.BeginTransaction();
            try
            {
                count = this.Insert(transaction, projectPlanRecordId, projectPlanId, item, note, date);
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

        public int Insert(DbTransaction transaction, Guid projectPlanRecordId, Guid projectPlanId, string item, string note, DateTime date)
        {
            DbParameter[] parameters = new DbParameter[5];
            parameters[0] = Manager.CreateParameter("ProjectPlanRecordId", projectPlanRecordId);
            parameters[1] = Manager.CreateParameter("ProjectPlanId", projectPlanId);
            parameters[2] = Manager.CreateParameter("item", item);
            parameters[3] = Manager.CreateParameter("note", note);
            parameters[4] = Manager.CreateParameter("date", date);
            int count = this.ModifyOperate.Modify(transaction, ProjectPlanRecord.INSERT_01, parameters);
            return count;
        }

        #endregion

        #region update

        public int Update(Guid projectPlanRecordId, Guid projectPlanId, string item, string note, DateTime date)
        {
            int count = 0;
            DbTransaction transaction = this.BeginTransaction();
            try
            {
                count = this.Update(transaction, projectPlanRecordId, projectPlanId, item, note, date);
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

        public int Update(DbTransaction transaction, Guid projectPlanRecordId, Guid projectPlanId, string item, string note, DateTime date)
        {
            DbParameter[] parameters = new DbParameter[5];
            parameters[0] = Manager.CreateParameter("ProjectPlanRecordId", projectPlanRecordId);
            parameters[1] = Manager.CreateParameter("ProjectPlanId", projectPlanId);
            parameters[2] = Manager.CreateParameter("item", item);
            parameters[3] = Manager.CreateParameter("note", note);
            parameters[4] = Manager.CreateParameter("date", date);
            int count = this.ModifyOperate.Modify(transaction, ProjectPlanRecord.UPDATE_01, parameters);
            return count;
        }

        #endregion
    }
}
