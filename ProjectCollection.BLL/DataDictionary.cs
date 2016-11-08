using System;
using System.Data;

namespace ProjectCollection.BLL
{
    public class DataDictionary : BaseLogic
    {
        public static DataTable GetDataByCategory(string category, bool hasBlankItem = false)
        {
            DataTable dt = new DAL.DataDictionary().Select(category);
            if (hasBlankItem)
            {
                DataRow dr = dt.NewRow();
                dr["text"] = string.Empty;
                dr["value"] = Guid.Empty;
                dt.Rows.InsertAt(dr, 0);
            }
            return dt;
        }
    }
}
