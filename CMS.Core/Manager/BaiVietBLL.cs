using CMS.DataAsscess;
using SubSonic;
using SweetCMS.DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CMS.Core.Manager.FriendlyUrlBLL;

namespace CMS.Core.Manager
{
    public class TypeBaiViet
    {
        public const int BaiViet = 1;
        public const int DuAnTieuBieu = 2;

    }
    public class BaiVietBLL
    {
        public static List<BaiVietDto> GetPaging(int? PageSize, int? PageIndex, string Key, bool? ASC, int? DanhMucChaId, int langID, out int rowsCount)
        {
            rowsCount = 0;
            StoredProcedure sp = SPs.StoreBaiVietTimKiemPhanTrang(PageSize, PageIndex, Key, ASC, DanhMucChaId, langID, out rowsCount);
            DataSet ds = sp.GetDataSet();
            if (ds != null && ds.Tables.Count > 0)
            {
                if (sp.OutputValues.Count > 0)
                    rowsCount = Convert.ToInt32(sp.OutputValues[0]);
            }
            return sp.ExecuteTypedList<BaiVietDto>();
        }
        public static bool Delete(int baiVietId)
        {
            new Delete().From(NhomBaiViet.Schema).
                Where(NhomBaiViet.BaiVietIdColumn).IsEqualTo(baiVietId).Execute();

            new Delete().From(NhomHinhAnh.Schema)
                .Where(NhomHinhAnh.BaiVietIdColumn).IsEqualTo(baiVietId).Execute();
            FriendlyUrlBLL.DeleteByPostId(baiVietId, FriendlyURLTypeHelper.Article);
            return new Update(BaiViet.Schema)
                .Set(BaiViet.StatusColumn).EqualTo(BasicStatusHelper.Deleted)
                .Where(BaiViet.IdColumn).IsEqualTo(baiVietId).Execute() > 0;
        }
        public static BaiViet GetById(int baiVietId)
        {
            return new BaiVietController().FetchByID(baiVietId).SingleOrDefault();
        }

        public static BaiViet Insert(BaiViet baiViet)
        {
            baiViet = new BaiVietController().Insert(baiViet);
            FriendlyUrlBLL.Insert(new FriendlyUrl()
            {
                PostId = baiViet.Id,
                PostType = FriendlyUrlBLL.FriendlyURLTypeHelper.Article,
                SlugUrl = baiViet.Slug
            });
            return baiViet;
        }
      
        public static BaiViet Update(BaiViet baiViet)
        {
            var friendUrl = FriendlyUrlBLL.GetByPostIdAndTypeId(baiViet.Id, FriendlyUrlBLL.FriendlyURLTypeHelper.Article);
            if (friendUrl.SlugUrl != baiViet.Slug)
            {
                friendUrl.SlugUrl = baiViet.Slug;
                FriendlyUrlBLL.Update(friendUrl);
            }
            return new BaiVietController().Update(baiViet);
        }



        public static List<DanhMucBaiVietDto> GetAllDanhMucBaiVietById(int postId, int langId, int type)
        {
            string sql = string.Format(@"Select c.Id, c.Ten, c.Slug,
                case
	                when cd.BaiVietId is not null then 1
	                else 0
                end as IsHaveDanhMuc
                from DanhMuc as c
                left join NhomBaiViet as cd on cd.DanhMucId = c.Id AND cd.BaiVietId = {0} where c.Status !={3} and c.LangID = {1} and c.Type = {2} ", postId,langId, type, BasicStatusHelper.Deleted);
            return new InlineQuery().ExecuteTypedList<DanhMucBaiVietDto>(sql);
        }

        public static int UpdateCategoryByPostId(List<DanhMucBaiVietDto> categories, int postId)
        {
            try
            {
                if (categories != null && categories.Count > 0)
                {
                    List<NhomBaiViet> select = new Select().From(NhomBaiViet.Schema).ExecuteTypedList<NhomBaiViet>();
                    foreach (var category in categories)
                    {
                        var itemToDelete = select.FirstOrDefault(x => x.DanhmucId == category.Id && x.BaiVietId == postId);

                        if (category.IsHaveDanhMuc == 1)
                        {

                            if (itemToDelete == null)
                            {

                                new NhomBaiVietController().Insert(category.Id, postId);
                            }

                        }
                        else
                        {
                            if (itemToDelete != null)
                            {
                                // Gọi hàm xóa từ controller và truyền phần tử cần xóa
                                new NhomBaiVietController().Delete(itemToDelete.Id);
                            }

                        }
                    }


                }
                return 1;
            }
            catch
            {
                return -1;
            }
        }

        public static List<DanhMuc> GetAllDanhMuc()
        {
            return new DanhMucController().FetchAll().GetList();
        }


        #region Dự án tiêu biểu
        public static BaiViet InsertDuAnTieuBieu(BaiViet baiViet)
        {
            baiViet = new BaiVietController().Insert(baiViet);

            new NhomBaiVietController().Insert(4, baiViet.Id);
            FriendlyUrlBLL.Insert(new FriendlyUrl()
            {
                PostId = baiViet.Id,
                PostType = FriendlyUrlBLL.FriendlyURLTypeHelper.Project,
                SlugUrl = baiViet.Slug,
                Status = BasicStatusHelper.Active
            });
            return baiViet;
        }
        public static List<NhomHinhAnh> GetHinhAnhByIdDuAnTieuBieu(int id)
        {
            var select = new Select().From(NhomHinhAnh.Schema).Where(NhomHinhAnh.BaiVietIdColumn)
                .IsEqualTo(id);
            return select.ExecuteTypedList<NhomHinhAnh>();
        }
        public static NhomHinhAnh InsertHinhAnhDuAnTieuBieu(string stringUrl, int baiVietId)
        {




            return new NhomHinhAnhController().Insert(stringUrl, baiVietId);
        }
        public static bool DeleteHinhAnhDuAnTieuBieu(int nhomBaiVietId)
        {
            return new NhomHinhAnhController().Delete(nhomBaiVietId);
        }

        #endregion
    }
}
