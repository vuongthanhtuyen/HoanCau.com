using SubSonic;
using SweetCMS.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
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
            //public const int ProjectList = 2;
            public const int Project = 2;
            //public const int PartnersList = 4;
            public const int Info = 3;

            //public const int Project = 2;
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
        public static bool DeleteByMa(string slugString)
        {
            return new Delete().From(FriendlyUrl.Schema).Where(FriendlyUrl.SlugUrlColumn)
                .IsEqualTo(slugString).Execute() > 0;
        }
        public static bool DeleteByPostId(int postId)
        {
            return new Delete().From(FriendlyUrl.Schema).Where(FriendlyUrl.SlugUrlColumn)
                .IsEqualTo(postId).Execute() > 0;
        }
        public static FriendlyUrl GetByPostId(int postID)
        {
            return new Select().From(FriendlyUrl.Schema).Where(FriendlyUrl.PostIdColumn)
                .IsEqualTo(postID).ExecuteSingle<FriendlyUrl>();
        }
    }
}
