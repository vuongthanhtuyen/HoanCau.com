using CMS.DataAsscess;
using SubSonic;
using SweetCMS.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Core.Publich
{
    public class BaiVietPublishBLL
    {
        public static BaiViet GetById(int id)
        {
            return new BaiVietController().FetchByID(id).SingleOrDefault();
        }
        public static BaiViet GetByMa(string slugUrl) {
            return new Select().From(BaiViet.Schema).Where(BaiViet.SlugColumn)
                .IsEqualTo(slugUrl).ExecuteSingle<BaiViet>();
        }
        public static DanhMuc GetDanhMucByIdBaiViet(int id)
        {
            string sql = string.Format($@"select top(1) * from DanhMuc as dm
                inner join NhomBaiViet as n on dm.Id = n.DanhmucId and n.BaiVietId = {id}");
            return new InlineQuery().ExecuteTypedList<DanhMuc>(sql).SingleOrDefault();
        }
        public static List<BaiVietDto> GetBaiVietLienQuan(int id)
        {
            string sql = string.Format($@"SELECT top(3) * 
                FROM BaiViet AS b
                LEFT JOIN NhomBaiViet AS n ON b.Id = n.BaiVietId AND n.DanhmucId = {id}
                ORDER BY 
                    CASE 
                        WHEN n.BaiVietId IS NULL THEN 1 
                        ELSE 0 
                    END, 
                    n.BaiVietId ASC;");
            return new InlineQuery().ExecuteTypedList<BaiVietDto>(sql);
        }
        public static List<BaiVietDto> GetBaiVietBanCoTheThich(int id)
        {
            string sql = string.Format($@"SELECT top(3) * FROM BaiViet AS b
                inner JOIN NhomBaiViet as n ON b.Id = n.BaiVietId AND n.DanhmucId <> {id}");
            return new InlineQuery().ExecuteTypedList<BaiVietDto>(sql);
        }

        public static BaiViet Insert(BaiViet baiViet)
        {
            return new BaiVietController().Insert(baiViet);
        }
        public static BaiViet Update(BaiViet baiViet)
        {
            return new BaiVietController().Update(baiViet);
        }
    }
}
