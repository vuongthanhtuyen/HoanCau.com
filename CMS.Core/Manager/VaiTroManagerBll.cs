using CMS.DataAsscess;
using SubSonic;
using SweetCMS.DataAccess;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Core.Manager
{
    public class VaiTroManagerBll
    {

        public static List<VaiTro> GetPaging(int? PageSize, int? PageIndex, string Key, bool? ASC, out int rowsCount)
        {
            rowsCount = 0;
            StoredProcedure sp = SPs.StoreVaiTroTimKiemPhanTrang(PageSize, PageIndex, Key, ASC, out rowsCount);
            DataSet ds = sp.GetDataSet();
            if (ds != null && ds.Tables.Count > 0)
            {
                if (sp.OutputValues.Count > 0)
                    rowsCount = Convert.ToInt32(sp.OutputValues[0]);

            }
            return sp.ExecuteTypedList<VaiTro>();
        }
        public static VaiTroCollection GetAll()
        {
            return new VaiTroController().FetchAll();
        }
        public static VaiTro GetById(int Id)
        {
            var select = new Select().From(VaiTro.Schema).Where(VaiTro.IdColumn).IsEqualTo(Id).ExecuteSingle<VaiTro>();
            return select;
        }
        
        public static VaiTro Insert(VaiTro vaiTro)
        {
            //if (vaiTro.Ma == "Admin")
            //{
            //    return null;
            //}
            return new VaiTroController().Insert(vaiTro);
        }
        public static VaiTro Update(VaiTro vaiTro)
        {
            //if (vaiTro.Ma == "Admin")
            //{
            //    return null;
            //}

            return new VaiTroController().Update(vaiTro);
        }
        public static bool Delete(int id)
        {
            if (id == 1)
            {
                return false;
            }
            // Xóa liên kết khóa ngoại với bảng NguoiDungVaiTroChitiet
            new Delete().From(NguoiDungVaiTroChitiet.Schema)
                .Where(NguoiDungVaiTroChitiet.VaiTroIdColumn).IsEqualTo(id)
                .Execute();

            // Xóa liên kết khóa ngaoij với bảng VaiTroDanhMucLoaiQuyenChiTiet
            new Delete().From(VaiTroDanhMucLoaiQuyenChiTiet.Schema)
                .Where(VaiTroDanhMucLoaiQuyenChiTiet.VaiTroIdColumn).IsEqualTo(id)
                .Execute();

            // Xóa role
            return new VaiTroController().Delete(id);
        }

        public static VaiTro IsExistsVaiTroMa(string roleMa)
        {
            Select select = new Select();
            select.From(VaiTro.Schema).Where(VaiTro.MaColumn).IsEqualTo(roleMa);
            return select.ExecuteSingle<VaiTro>();

        }
        public static VaiTro IsExistsVaiTroMaUpdate(string roleMa, int roleId)
        {
            // Lấy ra role có mã giống nhưng khác Id --> nếu nó trả về role
            Select select = new Select();
            select.From(VaiTro.Schema).Where(VaiTro.MaColumn).IsEqualTo(roleMa)
                .And(VaiTro.IdColumn).IsNotEqualTo(roleId);
            return select.ExecuteSingle<VaiTro>();

        }


        public static List<MenuQuyenCheck> GetAllMenuQuyenCheck(int roleId)
        {

            string sql = string.Format(@"
               SELECT 
                    m.Id AS 'MenuId', 
                    m.Ten AS 'MenuName', 
                    p.Id AS 'LoaiQuyenId', 
                    p.Ten AS 'LoaiQuyen',
                    CASE 
                        WHEN EXISTS (
                            SELECT 1 
                            FROM VaiTro r
                            INNER JOIN VaiTro_DanhMuc_LoaiQuyen_ChiTiet rmp 
                                ON r.Id = rmp.VaiTroId
                            WHERE r.Id = {0}
                            AND rmp.LoaiQuyenId = p.Id  
                            AND rmp.MenuId = m.Id
                        ) THEN 1
                        ELSE 0
                    END AS CoQuyen
                FROM MenuAdmin m
                CROSS JOIN LoaiQuyen p
                WHERE m.Id NOT IN (
                    SELECT DISTINCT MenuChaId 
                    FROM MenuAdmin 
                    WHERE MenuChaId IS NOT NULL
                )
                ORDER BY m.Id, p.Id;
                ", roleId);

            return new InlineQuery().ExecuteTypedList<MenuQuyenCheck>(sql);

        }

        public static int UpdateAllMenuLoaiQyen(List<MenuQuyenCheck> menuPermissionDetails, int vaiTroId)
        {
            //if (GetById(vaiTroId).Ten == "Admin")
            //{
            //    return -1;
            //}

            try
            {
                foreach (var menuPermissionDetail in menuPermissionDetails)
                {
                    if (menuPermissionDetail.CoQuyen == 1)
                    {
                        var sectlect = new Select().From(VaiTroDanhMucLoaiQuyenChiTiet.Schema)
                             .Where(VaiTroDanhMucLoaiQuyenChiTiet.MenuIdColumn).IsEqualTo(menuPermissionDetail.MenuId)
                             .And(VaiTroDanhMucLoaiQuyenChiTiet.LoaiQuyenIdColumn).IsEqualTo(menuPermissionDetail.LoaiQuyenId)
                             .And(VaiTroDanhMucLoaiQuyenChiTiet.VaiTroIdColumn).IsEqualTo(vaiTroId)
                             .ExecuteSingle<VaiTroDanhMucLoaiQuyenChiTiet>();

                        if (sectlect == null)
                        {
                            VaiTroDanhMucLoaiQuyenChiTiet roleMenuPermissionDetail = new VaiTroDanhMucLoaiQuyenChiTiet()
                            {
                                MenuId = menuPermissionDetail.MenuId,
                                LoaiQuyenId = menuPermissionDetail.LoaiQuyenId,
                                VaiTroId = vaiTroId
                            };
                            new VaiTroDanhMucLoaiQuyenChiTietController().Insert(roleMenuPermissionDetail);
                        }
                    }// CMS
                    else
                    {
                        var select = new Select().From(VaiTroDanhMucLoaiQuyenChiTiet.Schema)
                             .Where(VaiTroDanhMucLoaiQuyenChiTiet.MenuIdColumn).IsEqualTo(menuPermissionDetail.MenuId)
                             .And(VaiTroDanhMucLoaiQuyenChiTiet.LoaiQuyenIdColumn).IsEqualTo(menuPermissionDetail.LoaiQuyenId)
                             .And(VaiTroDanhMucLoaiQuyenChiTiet.VaiTroIdColumn).IsEqualTo(vaiTroId)
                             .ExecuteSingle<VaiTroDanhMucLoaiQuyenChiTiet>();
                        if (select != null)
                        {
                            new VaiTroDanhMucLoaiQuyenChiTietController().Delete(select.Id);
                        }
                    }
                }
                return 1;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return 0;
            }
        }


        public static bool AllowAdd(int userId , string menuMa)
        {
            return new Select().From(NguoiDung.Schema)
                .InnerJoin(NguoiDungVaiTroChitiet.NguoiDungIdColumn, NguoiDung.IdColumn)
                .InnerJoin(VaiTroDanhMucLoaiQuyenChiTiet.VaiTroIdColumn, NguoiDungVaiTroChitiet.VaiTroIdColumn)
                .InnerJoin(MenuAdmin.IdColumn, VaiTroDanhMucLoaiQuyenChiTiet.MenuIdColumn)
                .Where(NguoiDung.IdColumn).IsEqualTo(userId)
                .And(MenuAdmin.MaColumn).IsEqualTo(menuMa)
                .And(VaiTroDanhMucLoaiQuyenChiTiet.LoaiQuyenIdColumn).IsEqualTo(1)
                .GetRecordCount() > 0;
        }
        public static bool AllowEdit(int userId , string menuMa)
        {
            return new Select().From(NguoiDung.Schema)
                .InnerJoin(NguoiDungVaiTroChitiet.NguoiDungIdColumn, NguoiDung.IdColumn)
                .InnerJoin(VaiTroDanhMucLoaiQuyenChiTiet.VaiTroIdColumn, NguoiDungVaiTroChitiet.VaiTroIdColumn)
                .InnerJoin(MenuAdmin.IdColumn, VaiTroDanhMucLoaiQuyenChiTiet.MenuIdColumn)
                .Where(NguoiDung.IdColumn).IsEqualTo(userId)
                .And(MenuAdmin.MaColumn).IsEqualTo(menuMa)
                .And(VaiTroDanhMucLoaiQuyenChiTiet.LoaiQuyenIdColumn).IsEqualTo(2)
                .GetRecordCount() > 0;
        }
        public static bool AllowDelete (int userId , string menuMa)
        {
            bool a = new Select().From(NguoiDung.Schema)
                .InnerJoin(NguoiDungVaiTroChitiet.NguoiDungIdColumn, NguoiDung.IdColumn)
                .InnerJoin(VaiTroDanhMucLoaiQuyenChiTiet.VaiTroIdColumn, NguoiDungVaiTroChitiet.VaiTroIdColumn)
                .InnerJoin(MenuAdmin.IdColumn, VaiTroDanhMucLoaiQuyenChiTiet.MenuIdColumn)
                .Where(NguoiDung.IdColumn).IsEqualTo(userId)
                .And(MenuAdmin.MaColumn).IsEqualTo(menuMa)
                .And(VaiTroDanhMucLoaiQuyenChiTiet.LoaiQuyenIdColumn).IsEqualTo(3)
                .GetRecordCount() > 0;
            return a;
        }
    }
}
