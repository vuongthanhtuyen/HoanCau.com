using CMS.DataAsscess;
using SubSonic;
using TBDCMS.DataAccess;
using System;
using System.Collections.Generic;
using System.Data;

namespace CMS.Core.Manager
{
    public class NguoiDungManagerBLL
    {
        public static NguoiDung Insert(NguoiDung nguoiDung)
        {
            try
            {
                //if(nguoiDung.TenTruyCap == "admin")
                //{
                //    return null;
                //}
                return new NguoiDungController().Insert(nguoiDung);
            }
            catch
            {
                return null;
            }
        }

        public static List<NguoiDungDto> GetAll()
        {
            string sql = string.Format(@"select u.*, ISNULL(STRING_AGG(r.Ten, ', ' ),N'Không có quyền') as N'VaiTro' 
	                from NguoiDung u
                    left join NguoiDung_VaiTro_Chitiet rl on u.Id = rl.NguoiDungId
                    left join VaiTro r on r.Id = rl.VaiTroId
                    GROUP BY 
                    u.Id, u.HoVaTen,  u.Email,  u.SoDienThoai, u.NgaySinh,   u.MatKhau,   u.TrangThai, 
	                u.TenTruyCap, u.AvataUrl, u.NgayTao, u.ChinhSuaGanNhat,u.DiaChi
                    ORDER BY  u.NgayTao; ");
            return new InlineQuery().ExecuteTypedList<NguoiDungDto>(sql);
        }
        public static NguoiDung GetNguoiDungByTenTruyCap(string TenTuyCap)
        {
            Select select = new Select();
            select.From(NguoiDung.Schema);
            select.Where(NguoiDung.TenTruyCapColumn).IsEqualTo(TenTuyCap);
            return select.ExecuteSingle<NguoiDung>();
        }

        public static NguoiDung GetById(int idNguoiDung)
        {
            Select select = new Select();
            select.From(NguoiDung.Schema);
            select.Where(NguoiDung.IdColumn).IsEqualTo(idNguoiDung);
            return select.ExecuteSingle<NguoiDung>();
        }
        public static NguoiDung NguoiDungCheckAdmin(int idNguoiDung)
        {
            Select select = new Select();
            select.From(NguoiDung.Schema);
            select.InnerJoin(NguoiDungVaiTroChitiet.NguoiDungIdColumn, NguoiDung.IdColumn);
            select.InnerJoin(VaiTro.IdColumn, NguoiDungVaiTroChitiet.VaiTroIdColumn);
            select.Where(VaiTro.IdColumn).IsEqualTo(1);
            select.And(NguoiDung.IdColumn).IsEqualTo(idNguoiDung);
            return select.ExecuteSingle<NguoiDung>();
        }
        public static NguoiDung NguoiDungCheckReferencePost(int idNguoiDung)
        {

            Select select = new Select();
            select.From(NguoiDung.Schema);
            select.InnerJoin(BaiViet.TacGiaIdColumn, NguoiDung.IdColumn);
            select.Where(NguoiDung.IdColumn).IsEqualTo(idNguoiDung);
            return select.ExecuteSingle<NguoiDung>();

        }

        public static bool Delete(int idNguoiDung)
        {
            //if (GetById(idNguoiDung).TenTruyCap == "admin")
            //{
            //    return false;
            //}

            //new Delete().From(NguoiDungVaiTroChitiet.Schema)
            //    .Where(NguoiDungVaiTroChitiet.NguoiDungIdColumn).IsEqualTo(idNguoiDung).Execute();

            new Delete().From(NguoiDungVaiTroChitiet.Schema)
                .Where(NguoiDungVaiTroChitiet.NguoiDungIdColumn).IsEqualTo(idNguoiDung).Execute();


            return new NguoiDungController().Delete(idNguoiDung);
        }
        public static NguoiDung Update(NguoiDung nguoiDung)
        {
            //if (GetById(nguoiDung.Id).TenTruyCap=="admin")
            //{
            //    return null;
            //}
            return new NguoiDungController().Update(nguoiDung);
        }

        public static List<VaiTroUserDto> GetAllVaiTroByNguoiDungId(object idNguoiDung)
        {
            string sql = string.Format(@" select r.Id, r.Ten, r.Ma,case when rd.NguoiDungId is not null then 1 else 0
                end as CoVaiTro from VaiTro as r
                left join NguoiDung_VaiTro_Chitiet as rd on r.Id = rd.VaiTroId AND rd.NguoiDungId = {0}", idNguoiDung);
            return new InlineQuery().ExecuteTypedList<VaiTroUserDto>(sql);
        }

        public static int UpdateVaiTroChoNguoiDung(int idNguoiDung, string listRoleName)
        {
            //if (GetById(idNguoiDung).TenTruyCap == "admin")
            //{
            //    return -1;
            //}
            StoredProcedure sp = SPs.ThemVaiTroChoNguoiDung(idNguoiDung, listRoleName);
            return sp.Execute();

        }
        public static List<NguoiDungDto> GetNguoiDungPaging(int? PageSize, int? PageIndex, string Key, bool? ASC, out int rowsCount)
        {
            rowsCount = 0;
            StoredProcedure sp = SPs.StoreNguoiDungTimKiemPhanTrang(PageSize, PageIndex, Key, ASC, out rowsCount);
            DataSet ds = sp.GetDataSet();
            if (ds != null && ds.Tables.Count > 0)
            {
                if (sp.OutputValues.Count > 0)
                    rowsCount = Convert.ToInt32(sp.OutputValues[0]);

            }
            return sp.ExecuteTypedList<NguoiDungDto>();
        }
    }
}
