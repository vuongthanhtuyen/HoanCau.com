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
    public class LienHeBLL
    {
        public static List<LienHe> GetPaging(int? PageSize, int? PageIndex, string Key, bool? ASC, out int rowsCount)
        {
            rowsCount = 0;
            StoredProcedure sp = SPs.StoreLienHeTimKiemPhanTrang(PageSize, PageIndex, Key, ASC, out rowsCount);
            DataSet ds = sp.GetDataSet();
            if (ds != null && ds.Tables.Count > 0)
            {
                if (sp.OutputValues.Count > 0)
                    rowsCount = Convert.ToInt32(sp.OutputValues[0]);

            }
            return sp.ExecuteTypedList<LienHe>();
        }
        public static LienHe Insert(LienHe lienHe)
        {
            return new LienHeController().Insert(lienHe);
        }
        public static LienHe Update(LienHe lienHe)
        {
            return new LienHeController().Update(lienHe);
        }
        public static LienHeCollection GetAll()
        {
            return new LienHeController().FetchAll();
        }
        public static bool Delete(int id)
        {
            return new LienHeController().Delete(id);
        }
        public static LienHe GetById(int id)
        {
            return new Select().From(LienHe.Schema)
                .Where(LienHe.IdColumn).IsEqualTo(id).ExecuteSingle<LienHe>();
        }
    }
}
