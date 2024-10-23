using SubSonic;
using SweetCMS.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Core.Publich
{
    public class DuAnTieuBieuPublishBLL
    {
        public static BaiViet GetById(int id)
        {
            return new BaiVietController().FetchByID(id).SingleOrDefault();
        }

        public static List<NhomHinhAnh> GetNhomHinhAnh(int id)
        {
            return new Select().From(NhomHinhAnh.Schema).Where(NhomHinhAnh.BaiVietIdColumn).IsEqualTo(id).ExecuteTypedList<NhomHinhAnh>();
        }
    }
}
