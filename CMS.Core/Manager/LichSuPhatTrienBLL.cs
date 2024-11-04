using SubSonic;
using SweetCMS.DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Core.Manager
{
    public class LichSuPhatTrienBLL
    {
        public static List<LichSuPhatTrien> GetPaging(int? PageSize, int? PageIndex, string Key, bool? ASC, out int rowsCount)
        {
            rowsCount = 0;
            StoredProcedure sp = SPs.StoreLichSuPhatTrienTimKiemPhanTrang(PageSize, PageIndex, Key, ASC, out rowsCount);
            DataSet ds = sp.GetDataSet();
            if (ds != null && ds.Tables.Count > 0)
            {
                if (sp.OutputValues.Count > 0)
                    rowsCount = Convert.ToInt32(sp.OutputValues[0]);

            }
            return sp.ExecuteTypedList<LichSuPhatTrien>();
        }
        public static LichSuPhatTrien Insert(LichSuPhatTrien lichSuPhatTrien)
        {
            return new LichSuPhatTrienController().Insert(lichSuPhatTrien);
        }
        public static LichSuPhatTrien Update(LichSuPhatTrien lichSuPhatTrien)
        {
            return new LichSuPhatTrienController().Update(lichSuPhatTrien);
        }
        public static List<LichSuPhatTrien> GetAll()
        {
            return new LichSuPhatTrienController().FetchAll().ToList();
        }
        public static bool Delete(int id)
        {
            return new LichSuPhatTrienController().Delete(id);
        }
        public static LichSuPhatTrien GetById(int id)
        {
            return new Select().From(LichSuPhatTrien.Schema)
                .Where(LichSuPhatTrien.IdColumn).IsEqualTo(id).ExecuteSingle<LichSuPhatTrien>();
        }
    }
}
