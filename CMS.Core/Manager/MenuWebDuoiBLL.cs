﻿using CMS.DataAsscess;
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
    public class MenuWebDuoiBLL
    {
        public static List<MenuWebDto> GetPaging(int? PageSize, int? PageIndex, string Key, bool? ASC, int? DanhMucChaId, out int rowsCount)
        {
            rowsCount = 0;
            StoredProcedure sp = SPs.StoreMenuWebDuoiTimKiemPhanTrang(PageSize, PageIndex, Key, ASC, DanhMucChaId, out rowsCount);
            DataSet ds = sp.GetDataSet();
            if (ds != null && ds.Tables.Count > 0)
            {
                if (sp.OutputValues.Count > 0)
                    rowsCount = Convert.ToInt32(sp.OutputValues[0]);

            }
            return sp.ExecuteTypedList<MenuWebDto>();
        }
        public static List<MenuWebDuoi> GetListParentMenu()
        {
            List<MenuWebDuoi> listMenu = new Select()
                                          .From(MenuWebDuoi.Schema)
                                          .Where(MenuWebDuoi.MenuChaIdColumn).IsNull()
                                          .ExecuteTypedList<MenuWebDuoi>();

            return listMenu;
        }

        public static MenuWebDuoi Insert(MenuWebDuoi MenuWebDuoi)
        {
            return new MenuWebDuoiController().Insert(MenuWebDuoi);
        }
        public static MenuWebDuoi Update(MenuWebDuoi MenuWebDuoi)
        {
            return new MenuWebDuoiController().Update(MenuWebDuoi);
        }
        public static MenuWebDuoiCollection GetAll()
        {
            return new MenuWebDuoiController().FetchAll();
        }
        public static bool Delete(int id)
        {
            return new MenuWebDuoiController().Delete(id);
        }
        public static MenuWebDuoi GetById(int id)
        {
            return new Select().From(MenuWebDuoi.Schema)
                .Where(MenuWebDuoi.IdColumn).IsEqualTo(id).ExecuteSingle<MenuWebDuoi>();
        }
        public static List<BaiViet> GetListBaiViet()
        {

            string sql = string.Format(@" select b.Id, b.TieuDe from BaiViet as b 
                left join NhomBaiViet as n on b.Id = n.BaiVietId
	                and n.DanhmucId <> 4 ");
            return new InlineQuery().ExecuteTypedList<BaiViet>(sql);

        }
        public static List<BaiViet> GetListDuAnTieuBieu()
        {
            return new Select(BaiViet.IdColumn, BaiViet.TieuDeColumn).From(BaiViet.Schema).InnerJoin(NhomBaiViet.BaiVietIdColumn, BaiViet.IdColumn)
                .Where(NhomBaiViet.DanhmucIdColumn).IsEqualTo(4).ExecuteTypedList<BaiViet>();
        }
        public static List<DanhMuc> GetListDanhMuc()
        {
            return new Select(DanhMuc.IdColumn, DanhMuc.TenColumn).From(DanhMuc.Schema).Where(DanhMuc.IdColumn).IsNotEqualTo(4).ExecuteTypedList<DanhMuc>();
            //string sql = string.Format(@"select * from DanhMuc where DanhMuc.Id <> 4 ");
            //return new InlineQuery().ExecuteTypedList<DanhMuc>(sql);
        }
    }
}
