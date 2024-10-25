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
        public static List<FriendlyUrl> GetAll()
        {
            return new FriendlyUrlController().FetchAll().GetList();
        }
    }
}
