using SubSonic;
using TBDCMS.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CMS.Core.Manager.FriendlyUrlBLL;

namespace CMS.Core.Manager
{
    public class VideoBLL
    {
        #region Danh Muc cơ cấu
//        public static DanhMuc InsertDanhMuc(DanhMuc danhMuc)
//        {

//            danhMuc = new DanhMucController().Insert(danhMuc);
//            if (danhMuc.Type == CategoryType.NhomCoCau && danhMuc.DanhMucChaId != 0)
//            {
//                return danhMuc;
//            }
//            FriendlyUrlBLL.Insert(new FriendlyUrl()
//            {
//                PostId = danhMuc.Id,
//                PostType = FriendlyUrlBLL.FriendlyURLTypeHelper.NhomCoCau,
//                SlugUrl = danhMuc.Slug,
//                Status = BasicStatusHelper.Active

//            });
//            return danhMuc;
//        }
//        public static List<DanhMuc> GetAllNoPaging(int langId, int CatType, string keySearch = null)
//        {
//            if (keySearch != null)
//            {
//                string sql = string.Format("select * from DanhMuc as  where PostType = {1} and langID = {2} and (Ten like N'%{0}%' or Slug like '%{0}%') and Status != {3}", keySearch, CatType, langId, BasicStatusHelper.Deleted)
//; return new InlineQuery().ExecuteTypedList<DanhMuc>(sql);
//            }
//            return new Select().From(DanhMuc.Schema)
//                .Where(DanhMuc.LangIDColumn).IsEqualTo(langId).And(DanhMuc.TypeColumn).IsEqualTo(CatType)
//                .And(DanhMuc.StatusColumn).IsNotEqualTo(BasicStatusHelper.Deleted)
//                .ExecuteTypedList<DanhMuc>();
//        }
//        public static DanhMuc UpdateDanhMuc(DanhMuc danhMuc)
//        {
//            danhMuc = new DanhMucController().Update(danhMuc);
//            if (danhMuc.DanhMucChaId == 0)
//            {
//                var friendlyUrl = FriendlyUrlBLL.GetByPostIdAndTypeId(danhMuc.Id, FriendlyURLTypeHelper.NhomCoCau);
//                if (friendlyUrl != null && danhMuc.Slug != friendlyUrl.SlugUrl)
//                {
//                    friendlyUrl.SlugUrl = danhMuc.Slug;
//                    FriendlyUrlBLL.Update(friendlyUrl);
//                }
//            }

//            return danhMuc;
//        }
//        public static List<DanhMuc> GetAllByParentId(int id)
//        {
//            var select = new Select().From(DanhMuc.Schema).Where(DanhMuc.DanhMucChaIdColumn).IsEqualTo(id);
//            return select.ExecuteTypedList<DanhMuc>();
//        }
//        public static bool DeleteDanhMuc(int id)
//        {
//            //new Delete().From(Video.Schema)
//            //    .Where(Video.DanhmucIdColumn)
//            //    .IsEqualTo(id).Execute();

//            FriendlyUrlBLL.DeleteByPostId(id, FriendlyURLTypeHelper.NhomCoCau);
//            new Update(DanhMuc.Schema)
//                .Set(DanhMuc.DanhMucChaIdColumn).EqualTo(0)
//                .Where(DanhMuc.DanhMucChaIdColumn).IsEqualTo(id).Execute();
//            new Update(DanhMuc.Schema)
//                .Set(DanhMuc.SlugColumn).EqualTo(string.Empty)
//                .Where(DanhMuc.DanhMucChaIdColumn).IsEqualTo(id).Execute();

//            return new Update(DanhMuc.Schema)
//                .Set(DanhMuc.StatusColumn).EqualTo(BasicStatusHelper.Deleted)
//                .Where(DanhMuc.IdColumn).IsEqualTo(id).Execute() > 0;
//        }





        #endregion

        #region Nhóm bài viết

        public static Video GetById(int id)
        {
            return new VideoController().FetchByID(id).SingleOrDefault();

        }
        public static bool Delete(int id)
        {
            return new Update(Video.Schema).Set(Video.StatusColumn)
                .EqualTo(BasicStatusHelper.Deleted).Where(Video.IdColumn).IsEqualTo(id).Execute() > 0;

        }
        public static Video Insert(Video objVideo)
        {
            return new VideoController().Insert(objVideo);
        }

        public static Video Update(Video video)
        {

            return new VideoController().Update(video);
        }
        public static Video GetByIdVideo(int idVideo)
        {

            return new Select().From(Video.Schema)
                .Where(Video.IdColumn).IsEqualTo(idVideo)
                .And(Video.StatusColumn).IsNotEqualTo(BasicStatusHelper.Deleted)
                .ExecuteSingle<Video>();
        }

        public static List<Video> GetListVideoByCatId(int idCatId)
        {
            try
            {
                return new Select().From(Video.Schema)
                    .Where(Video.CategoryIdColumn).IsEqualTo(idCatId)
                    .And(Video.StatusColumn).IsNotEqualTo(BasicStatusHelper.Deleted)
                    .ExecuteTypedList<Video>();
            }
            catch (Exception ex) { return null; }
        }

        //public static Video GetVideoByIdVideoAndCatId(int danhMucId, int videoId)
        //{

        //    return new Select().From(Video.Schema).Where(Video.DanhmucIdColumn).IsEqualTo(danhMucId)
        //        .And(Video.IdColumn).IsEqualTo(videoId).ExecuteSingle<Video>();
        //}




        #endregion

        #region Thành viên
        public static bool DeleteThanhVien(int videoId)
        {
            new Delete().From(Video.Schema).
                Where(Video.IdColumn).IsEqualTo(videoId).Execute();

            new Delete().From(NhomHinhAnh.Schema)
                .Where(NhomHinhAnh.IdColumn).IsEqualTo(videoId).Execute();
            FriendlyUrlBLL.DeleteByPostId(videoId, FriendlyURLTypeHelper.ThanhVien);
            return new Update(Video.Schema)
                .Set(Video.StatusColumn).EqualTo(BasicStatusHelper.Deleted)
                .Where(Video.IdColumn).IsEqualTo(videoId).Execute() > 0;
        }


        public static Video InsertThanhVien(Video video)
        {
            video = new VideoController().Insert(video);
            FriendlyUrlBLL.Insert(new FriendlyUrl()
            {
                PostId = video.Id,
                PostType = FriendlyUrlBLL.FriendlyURLTypeHelper.ThanhVien,
                Status = BasicStatusHelper.Active,
                SlugUrl = video.SlugUrl
            });
            return video;
        }

        public static Video UpdateThanhVien(Video video)
        {
            var friendUrl = FriendlyUrlBLL.GetByPostIdAndTypeId(video.Id, FriendlyUrlBLL.FriendlyURLTypeHelper.ThanhVien);
            if (friendUrl.SlugUrl != video.SlugUrl)
            {
                friendUrl.SlugUrl = video.SlugUrl;
                //friendUrl.GetDBType
                FriendlyUrlBLL.Update(friendUrl);
            }
            return new VideoController().Update(video);
        }




        #endregion
    }
}
