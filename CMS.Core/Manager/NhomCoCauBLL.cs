using SubSonic;
using SweetCMS.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CMS.Core.Manager.FriendlyUrlBLL;

namespace CMS.Core.Manager
{
    public class NhomCoCauBLL
    {
        #region Danh Muc cơ cấu
        public static DanhMuc InsertDanhMuc(DanhMuc danhMuc)
        {

            danhMuc = new DanhMucController().Insert(danhMuc);
            if (danhMuc.Type == CategoryType.NhomCoCau && danhMuc.DanhMucChaId != 0)
            {
                return danhMuc;
            }
            FriendlyUrlBLL.Insert(new FriendlyUrl()
            {
                PostId = danhMuc.Id,
                PostType = FriendlyUrlBLL.FriendlyURLTypeHelper.NhomCoCau,
                SlugUrl = danhMuc.Slug,
                Status = BasicStatusHelper.Active

            });
            return danhMuc;
        }
        public static List<DanhMuc> GetAllNoPaging(int langId, int CatType, string keySearch = null)
        {
            if (keySearch != null)
            {
                string sql = string.Format("select * from DanhMuc as  where PostType = {1} and langID = {2} and (Ten like N'%{0}%' or Slug like '%{0}%') and Status != {3}", keySearch, CatType, langId, BasicStatusHelper.Deleted)
; return new InlineQuery().ExecuteTypedList<DanhMuc>(sql);
            }
            return new Select().From(DanhMuc.Schema)
                .Where(DanhMuc.LangIDColumn).IsEqualTo(langId).And(DanhMuc.TypeColumn).IsEqualTo(CatType)
                .And(DanhMuc.StatusColumn).IsNotEqualTo(BasicStatusHelper.Deleted)
                .ExecuteTypedList<DanhMuc>();
        }
        public static DanhMuc UpdateDanhMuc(DanhMuc danhMuc)
        {
            danhMuc = new DanhMucController().Update(danhMuc);
            if (danhMuc.DanhMucChaId == 0)
            {
                var friendlyUrl = FriendlyUrlBLL.GetByPostIdAndTypeId(danhMuc.Id, FriendlyURLTypeHelper.NhomCoCau);
                if (friendlyUrl != null && danhMuc.Slug != friendlyUrl.SlugUrl)
                {
                    friendlyUrl.SlugUrl = danhMuc.Slug;
                    FriendlyUrlBLL.Update(friendlyUrl);
                }
            }

            return danhMuc;
        }
        public static List<DanhMuc> GetAllByParentId(int id)
        {
            var select = new Select().From(DanhMuc.Schema).Where(DanhMuc.DanhMucChaIdColumn).IsEqualTo(id);
            return select.ExecuteTypedList<DanhMuc>();
        }
        public static bool DeleteDanhMuc(int id)
        {
            new Delete().From(NhomBaiViet.Schema)
                .Where(NhomBaiViet.DanhmucIdColumn)
                .IsEqualTo(id).Execute();

            FriendlyUrlBLL.DeleteByPostId(id, FriendlyURLTypeHelper.NhomCoCau);
            new Update(DanhMuc.Schema)
                .Set(DanhMuc.DanhMucChaIdColumn).EqualTo(0)
                .Where(DanhMuc.DanhMucChaIdColumn).IsEqualTo(id).Execute();
            new Update(DanhMuc.Schema)
                .Set(DanhMuc.SlugColumn).EqualTo(string.Empty)
                .Where(DanhMuc.DanhMucChaIdColumn).IsEqualTo(id).Execute();

            return new Update(DanhMuc.Schema)
                .Set(DanhMuc.StatusColumn).EqualTo(BasicStatusHelper.Deleted)
                .Where(DanhMuc.IdColumn).IsEqualTo(id).Execute() > 0;
        }





        #endregion

        #region Nhóm bài viết

        public static NhomBaiViet GetById(int id)
        {
            return new NhomBaiVietController().FetchByID(id).SingleOrDefault();

        }
        public static bool DeleteNhomBaiViet(int id)
        {
            return new NhomBaiVietController().Delete(id);

        }
        public static NhomBaiViet InsertNhomBaiViet(int danhMucId, int baiVietId)
        {
            return new NhomBaiVietController().Insert( danhMucId,  baiVietId);
        }

        public static NhomBaiViet UpdateNhomBaiViet(NhomBaiViet baiViet)
        {

            return new NhomBaiVietController().Update(baiViet);
        }
        public static NhomBaiViet GetByIdBaiViet(int idBaiViet)
        {

            return new Select().From(NhomBaiViet.Schema)
                .Where(NhomBaiViet.BaiVietIdColumn).IsEqualTo(idBaiViet).ExecuteSingle<NhomBaiViet>();
        }

        public static NhomBaiViet GetNhomBaiVietByIdBaiVietAndCatId(int danhMucId, int baiVietId)
        {

            return new Select().From(NhomBaiViet.Schema).Where(NhomBaiViet.DanhmucIdColumn).IsEqualTo(danhMucId)
                .And(NhomBaiViet.BaiVietIdColumn).IsEqualTo(baiVietId).ExecuteSingle<NhomBaiViet>();
        }




        #endregion

        #region Thành viên
        public static bool DeleteThanhVien(int baiVietId)
        {
            new Delete().From(NhomBaiViet.Schema).
                Where(NhomBaiViet.BaiVietIdColumn).IsEqualTo(baiVietId).Execute();

            new Delete().From(NhomHinhAnh.Schema)
                .Where(NhomHinhAnh.BaiVietIdColumn).IsEqualTo(baiVietId).Execute();
            FriendlyUrlBLL.DeleteByPostId(baiVietId, FriendlyURLTypeHelper.ThanhVien);
            return new Update(BaiViet.Schema)
                .Set(BaiViet.StatusColumn).EqualTo(BasicStatusHelper.Deleted)
                .Where(BaiViet.IdColumn).IsEqualTo(baiVietId).Execute() > 0;
        }


        public static BaiViet InsertThanhVien(BaiViet baiViet)
        {
            baiViet = new BaiVietController().Insert(baiViet);
            FriendlyUrlBLL.Insert(new FriendlyUrl()
            {
                PostId = baiViet.Id,
                PostType = FriendlyUrlBLL.FriendlyURLTypeHelper.ThanhVien,
                Status = BasicStatusHelper.Active,
                SlugUrl = baiViet.Slug
            });
            return baiViet;
        }

        public static BaiViet UpdateThanhVien(BaiViet baiViet)
        {
            var friendUrl = FriendlyUrlBLL.GetByPostIdAndTypeId(baiViet.Id, FriendlyUrlBLL.FriendlyURLTypeHelper.ThanhVien);
            if (friendUrl.SlugUrl != baiViet.Slug)
            {
                friendUrl.SlugUrl = baiViet.Slug;
                //friendUrl.GetDBType
                FriendlyUrlBLL.Update(friendUrl);
            }
            return new BaiVietController().Update(baiViet);
        }




        #endregion
    }
}
