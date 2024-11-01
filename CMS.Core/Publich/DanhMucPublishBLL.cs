using CMS.DataAsscess;
using SubSonic;
using SweetCMS.DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Core.Publich
{
    public class DanhMucPublishBLL
    {
        public static List<BaiVietDto> GetPaging(int? PageSize, int? PageIndex, string Key, bool? ASC, int? DanhMucChaId, int langID, out int rowsCount)
        {
            rowsCount = 0;
            StoredProcedure sp = SPs.StoreBaiVietTimKiemPhanTrang(PageSize, PageIndex, Key, ASC, DanhMucChaId,langID, out rowsCount);
            DataSet ds = sp.GetDataSet();
            if (ds != null && ds.Tables.Count > 0)
            {
                if (sp.OutputValues.Count > 0)
                    rowsCount = Convert.ToInt32(sp.OutputValues[0]);
            }
            return sp.ExecuteTypedList<BaiVietDto>();
        }
        public static DanhMuc GetById(int id)
        {
            return new DanhMucController().FetchByID(id).SingleOrDefault();
        }
        public static DanhMuc GetByMa(string slug)
        {
            return new Select().From(DanhMuc.Schema).Where(DanhMuc.SlugColumn)
                .IsEqualTo(slug).ExecuteSingle<DanhMuc>();
        }
    }
}
