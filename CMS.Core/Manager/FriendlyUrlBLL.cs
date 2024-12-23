using SubSonic;
using SweetCMS.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Core.Manager
{
    public class FriendlyUrlBLL
    {

        public class FriendlyURLTypeHelper
        {
            public const int Category = 0;
            public const int Article = 1;
            public const int Project = 2;
            public const int Info = 3;
            public const byte FileAttactment = 12;
            public const byte ChuongTrinhDaoTao = 20;

        }
        public static List<FriendlyUrl> GetAll()
        {
            return new FriendlyUrlController().FetchAll().GetList();
        }
        public static FriendlyUrl GetByMa(string slugString)
        {
            return new Select().From(FriendlyUrl.Schema).Where(FriendlyUrl.SlugUrlColumn)
                .IsEqualTo(slugString).ExecuteSingle<FriendlyUrl>();
        }
        public static bool CheckExists(string slugString, int postID)
        {
            string sql = string.Empty;
            if (postID > 0)
            {
                sql = $"select COUNT(*) from FriendlyUrl where SlugUrl = '{slugString}' and PostId != {postID} ";
            }
            else
                sql = $"select COUNT(*) from FriendlyUrl where SlugUrl = '{slugString}'";

            return new InlineQuery().ExecuteScalar<int>(sql) > 0;
        }

        public static FriendlyUrl Insert(FriendlyUrl friendlyUrl)
        {
            return new FriendlyUrlController().Insert(friendlyUrl);
        }
      
        public static FriendlyUrl Update(FriendlyUrl friendlyUrl)
        {
            return new FriendlyUrlController().Update(friendlyUrl);
        }
        public static FriendlyUrl GetById(int friendlyUrlID)
        {
            return new FriendlyUrlController().FetchByID(friendlyUrlID).SingleOrDefault();
        }
        public static FriendlyUrl GetByPostIdAndTypeId(int postId, int catId)
        {
            return new Select().From(FriendlyUrl.Schema)
                .Where(FriendlyUrl.PostIdColumn).IsEqualTo(postId)
                .And(FriendlyUrl.PostTypeColumn).IsEqualTo(catId).ExecuteSingle<FriendlyUrl>();
        }
        public static bool DeleteByMa(string slugString)
        {
            return new Delete().From(FriendlyUrl.Schema).Where(FriendlyUrl.SlugUrlColumn)
                .IsEqualTo(slugString).Execute() > 0;
        }
        public static bool DeleteByPostId(int postId, int posttype)
        {
            return new Delete().From(FriendlyUrl.Schema).Where(FriendlyUrl.PostIdColumn)
                .IsEqualTo(postId).And(FriendlyUrl.PostTypeColumn).IsEqualTo(posttype).Execute() > 0;
        }
        public static FriendlyUrl GetByPostId(int postID)
        {
            return new Select().From(FriendlyUrl.Schema).Where(FriendlyUrl.PostIdColumn)
                .IsEqualTo(postID).ExecuteSingle<FriendlyUrl>();
        }



        //public static DanhMuc IsExistsSlugOtherID(string danhMucSlug, int danhMucId)
        //{
        //    Select select = new Select();
        //    select.From(DanhMuc.Schema).Where(DanhMuc.SlugColumn).IsEqualTo(danhMucSlug)
        //    .And(DanhMuc.IdColumn).IsNotEqualTo(danhMucId);
        //    return select.ExecuteSingle<DanhMuc>();

        //}


        //public static FriendlyUrl IsSlugExists(string baiVietSlug)
        //{
        //    var select = new Select().From(FriendlyUrl.Schema)
        //        .Where(FriendlyUrl.SlugUrlColumn).IsEqualTo(baiVietSlug)
        //        .ExecuteSingle<FriendlyUrl>();
        //    return select;
        //}
    }
}
