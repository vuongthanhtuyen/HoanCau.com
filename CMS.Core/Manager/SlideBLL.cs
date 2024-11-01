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
    public class SlideBLL
    {

        public static List<TrinhChieuAnh> GetPaging(int? PageSize, int? PageIndex, string Key, bool? ASC, out int rowsCount)
        {
            rowsCount = 0;
            StoredProcedure sp = SPs.StoreSlideTimKiemPhanTrang(PageSize, PageIndex, Key, ASC, out rowsCount);
            DataSet ds = sp.GetDataSet();
            if (ds != null && ds.Tables.Count > 0)
            {
                if (sp.OutputValues.Count > 0)
                    rowsCount = Convert.ToInt32(sp.OutputValues[0]);

            }
            return sp.ExecuteTypedList<TrinhChieuAnh>();
        }
        public static TrinhChieuAnh Insert(TrinhChieuAnh trinhChieuAnh)
        {
            return new TrinhChieuAnhController().Insert(trinhChieuAnh);
        }
        public static TrinhChieuAnh Update(TrinhChieuAnh trinhChieuAnh)
        {
            return new TrinhChieuAnhController().Update(trinhChieuAnh);
        }
        public static TrinhChieuAnhCollection GetAll()
        {
            return new TrinhChieuAnhController().FetchAll();
        }
        public static bool Delete(int id)
        {
            return new TrinhChieuAnhController().Delete(id);
        }
        public static TrinhChieuAnh GetById(int id)
        {
            return new Select().From(TrinhChieuAnh.Schema)
                .Where(TrinhChieuAnh.IdColumn).IsEqualTo(id).ExecuteSingle<TrinhChieuAnh>();
        }


        //public static List<string> GetAllFileNameUrlImage
    }
}
