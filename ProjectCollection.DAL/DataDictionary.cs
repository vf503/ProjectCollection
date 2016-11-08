using Adapt.Database;
using System.Data;
using System.Data.Common;

namespace ProjectCollection.DAL
{
    public class DataDictionary : BaseDatabase
    {
        private const string SELECT_01 = "select * from data_dictionary";
        private const string SELECT_02 = SELECT_01 + " where category=@Category";

        public DataTable Select(string category)
        {
            DbParameter[] parameters = new DbParameter[1];
            parameters[0] = Manager.CreateParameter("Category", category);
            DataTable table = this.SelectOperate.Select(DataDictionary.SELECT_02, parameters);
            return table;
        }
    }
}
