using CMS.DataAsscess;
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
    public class MenuWebTrenBLL
    {

        public static List<MenuWebDto> GetPaging(int? PageSize, int? PageIndex, string Key, bool? ASC, int? DanhMucChaId, out int rowsCount)
        {
            rowsCount = 0;
            StoredProcedure sp = SPs.StoreMenuWebTrenTimKiemPhanTrang(PageSize, PageIndex, Key, ASC, DanhMucChaId, out rowsCount);
            DataSet ds = sp.GetDataSet();
            if (ds != null && ds.Tables.Count > 0)
            {
                if (sp.OutputValues.Count > 0)
                    rowsCount = Convert.ToInt32(sp.OutputValues[0]);

            }
            return sp.ExecuteTypedList<MenuWebDto>();
        }
        public static List<MenuWebTren> GetListParentMenu()
        {
            List<MenuWebTren> listMenu = new Select()
                                          .From(MenuWebTren.Schema)
                                          .Where(MenuWebTren.MenuChaIdColumn).IsNull()
                                          .ExecuteTypedList<MenuWebTren>();

            return listMenu;
        }

        public static MenuWebTren Insert(MenuWebTren menuWebTren)
        {
            return new MenuWebTrenController().Insert(menuWebTren);
        }
        public static MenuWebTren Update(MenuWebTren menuWebTren)
        {
            return new MenuWebTrenController().Update(menuWebTren);
        }
        public static MenuWebTrenCollection GetAll()
        {
            return new MenuWebTrenController().FetchAll();
        }
        public static bool Delete(int id)
        {
            return new MenuWebTrenController().Delete(id);
        }
        public static MenuWebTren GetById(int id)
        {
            return new Select().From(MenuWebTren.Schema)
                .Where(MenuWebTren.IdColumn).IsEqualTo(id).ExecuteSingle<MenuWebTren>();
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
            return new Select(BaiViet.IdColumn,BaiViet.TieuDeColumn).From(BaiViet.Schema).InnerJoin(NhomBaiViet.BaiVietIdColumn, BaiViet.IdColumn)
                .Where(NhomBaiViet.DanhmucIdColumn).IsEqualTo(4).ExecuteTypedList<BaiViet>();
        }
        public static List<DanhMuc> GetListDanhMuc()
        {
            return new Select(DanhMuc.IdColumn, DanhMuc.TenColumn).From(DanhMuc.Schema).Where(DanhMuc.IdColumn).IsNotEqualTo(4).ExecuteTypedList<DanhMuc>();
            //string sql = string.Format(@"select * from DanhMuc where DanhMuc.Id <> 4 ");
            //return new InlineQuery().ExecuteTypedList<DanhMuc>(sql);
        }
        //public static List<BaiViet> GetListBaiViet(int id)
        //{
        //    return new Select(BaiViet.IdColumn,BaiViet.TieuDeColumn).From(BaiViet.Schema).ExecuteTypedList<BaiViet>();
        //}
        

    }
}
