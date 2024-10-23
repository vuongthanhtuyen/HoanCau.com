using SubSonic;
using SweetCMS.DataAccess;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Core.Publich
{
    public class MenuPulishBLL
    {

        public static List<MenuWebTren> GetAllMenuWebTren()
        {
            string sql = string.Format(@" select * from menuWebTren  order by MenuChaId , Stt ");
            return new InlineQuery().ExecuteTypedList<MenuWebTren>(sql);
         }
    
        public static List<MenuWebDuoi> GetAllMenuWebDuoi()
        {
            string sql = string.Format(@" select * from menuWebDuoi  order by MenuChaId , Stt ");
            return new InlineQuery().ExecuteTypedList<MenuWebDuoi>(sql);
        }
    }
}
