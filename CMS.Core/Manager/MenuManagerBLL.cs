using TBDCMS.DataAccess;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Core.Manager
{
    public class MenuManagerBLL
    {
        public static MenuAdminCollection GetAllMenu()
        {
            return new MenuAdminController().FetchAll();
        }

       
    }
}
