using TBDCMS.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Core.Publich
{
    public class LichSuPhatTrienPublishBLL
    {
        public static List<LichSuPhatTrien> GetAll()
        {
            return new LichSuPhatTrienController().FetchAll().ToList<LichSuPhatTrien>();
        }
    }
}
