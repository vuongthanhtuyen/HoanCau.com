using CMS.DataAsscess;
using SubSonic;
using TBDCMS.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Core.Publich
{
    public class DefaultService
    {
        public static List<TrinhChieuAnh> GetAllSlide()
        {
            return new TrinhChieuAnhController().FetchAll().Where(x => x.TrangThai == true).ToList();
        }
        public static List<BaiVietDto> GetAllBaiViet()
        {
            string sql = string.Format(@"SELECT n.DanhmucId as DanhMucId, d.DanhMucChaId as DanhMucChaId , b.*  FROM BaiViet AS b
                inner JOIN NhomBaiViet as n ON b.Id = n.BaiVietId
				left join DanhMuc as d on d.Id = n.DanhmucId
                ");
            return new InlineQuery().ExecuteTypedList<BaiVietDto>(sql);
        }
    }
}
