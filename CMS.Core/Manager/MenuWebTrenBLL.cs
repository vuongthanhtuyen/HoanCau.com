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
        public static List<MenuWebTren> GetListParentMenu(int langID)
        {
            List<MenuWebTren> listMenu = new Select()
                                          .From(MenuWebTren.Schema)
                                          .Where(MenuWebTren.MenuChaIdColumn).IsEqualTo(0)
                                          .And(MenuWebTren.LangIDColumn).IsEqualTo(langID)
                                          .ExecuteTypedList<MenuWebTren>();

            return listMenu;
        }

        public static List<MenuWebTren> GetAllByLangId(int langID)
        {
            return new Select().From(MenuWebTren.Schema)
                .Where(MenuWebTren.LangIDColumn).IsEqualTo(langID)
                .ExecuteTypedList<MenuWebTren>();
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
        public static List<BaiViet> GetListBaiViet(int langID)
        {

            string sql = string.Format(@" select b.TieuDe, b.Slug from BaiViet as b 
                left join NhomBaiViet as n on b.Id = n.BaiVietId
	                and n.DanhmucId <> 4 where b.LangID =  " + langID);
            return new InlineQuery().ExecuteTypedList<BaiViet>(sql);

        }
        public static List<BaiViet> GetListDuAnTieuBieu(int langID)
        {
            return new Select(BaiViet.TieuDeColumn, BaiViet.SlugColumn).From(BaiViet.Schema).InnerJoin(NhomBaiViet.BaiVietIdColumn, BaiViet.IdColumn)
                .Where(NhomBaiViet.DanhmucIdColumn).IsEqualTo(4)
                .And(BaiViet.LangIDColumn).IsEqualTo(langID)
                .ExecuteTypedList<BaiViet>();
        }
        public static List<DanhMuc> GetListDanhMuc(int langID)
        {
            return new Select(DanhMuc.TenColumn,DanhMuc.SlugColumn).From(DanhMuc.Schema)
                .Where(DanhMuc.IdColumn).IsNotEqualTo(4)
                .And(DanhMuc.LangIDColumn).IsEqualTo(langID)
                .ExecuteTypedList<DanhMuc>();
            //string sql = string.Format(@"select * from DanhMuc where DanhMuc.Id <> 4 ");
            //return new InlineQuery().ExecuteTypedList<DanhMuc>(sql);
        }
        //public static List<BaiViet> GetListBaiViet(int id)
        //{
        //    return new Select(BaiViet.IdColumn,BaiViet.TieuDeColumn).From(BaiViet.Schema).ExecuteTypedList<BaiViet>();
        //}
        

    }
}
