using CMS.DataAsscess;
using SubSonic;
using TBDCMS.DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Core.Manager
{
    public class DoiTacBLL
    {

        public static List<DoiTac> GetPaging(int? PageSize, int? PageIndex, string Key, bool? ASC, out int rowsCount)
        {
            rowsCount = 0;
            StoredProcedure sp = SPs.StoreDoiTacTimKiemPhanTrang(PageSize, PageIndex, Key, ASC, out rowsCount);
            DataSet ds = sp.GetDataSet();
            if (ds != null && ds.Tables.Count > 0)
            {
                if (sp.OutputValues.Count > 0)
                    rowsCount = Convert.ToInt32(sp.OutputValues[0]);

            }
            return sp.ExecuteTypedList<DoiTac>();
        }
        public static DoiTac Insert(DoiTac doiTac)
        {
            return new DoiTacController().Insert(doiTac);
        }
        public static DoiTac Update(DoiTac doiTac) 
        {
            return new DoiTacController().Update(doiTac);
        }
        public static DoiTacCollection GetAll()
        {
            return new DoiTacController().FetchAll();
        }
        public static bool Delete(int id)
        {
            return new DoiTacController().Delete(id);
        }
        public static DoiTac GetById(int id) 
        {
            return new Select().From(DoiTac.Schema)
                .Where(DoiTac.IdColumn).IsEqualTo(id).ExecuteSingle<DoiTac>();
        }
        public static List<DoiTac> GetTopListDoiTac(string top)
        {
            return new Select().Top(top).From(DoiTac.Schema).ExecuteTypedList<DoiTac>();
        }

    }
}
