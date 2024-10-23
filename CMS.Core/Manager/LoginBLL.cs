using CMS.DataAsscess;
using SubSonic;
using SweetCMS.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Core.Manager
{
    public class LoginBLL
    {
        public static LoginReturnId Login(string username, string password)
        {
            Select select = new Select();
            select.From(NguoiDung.Schema).Where(NguoiDung.TenTruyCapColumn).IsEqualTo(username);
            select.And(NguoiDung.MatKhauColumn).IsEqualTo(password);
            return select.ExecuteSingle<LoginReturnId>();
        }

        public static List<MenuAdmin> MenuCheckByUser(int userId)
        {
            StoredProcedure sp = SPs.GetAllQuyenOfUser(userId);
            return sp.ExecuteTypedList<MenuAdmin>();
        }

        public static List<MenuPermisstion> GetListMenuPermisstionByUser(int userId)
        {
            string sql = string.Format(@"select DISTINCT   m.Ma as 'MenuMa', p.Ma as 'PermissionMa' from MenuAdmin as m
                inner join VaiTro_DanhMuc_LoaiQuyen_ChiTiet as rmp on m.Id = rmp.MenuId
                inner join LoaiQuyen as p on p.Id = rmp.LoaiQuyenId
                inner join NguoiDung_VaiTro_Chitiet as  rd on rd.VaiTroId = rmp.VaiTroId
                where rd.NguoiDungId  = {0}
                group by m.Ma , p.Ma 
                ", userId);
            return new InlineQuery().ExecuteTypedList<MenuPermisstion>(sql);

        }




    }
}
