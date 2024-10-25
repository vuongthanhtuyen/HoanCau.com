using CMS.DataAsscess;
using CMS.WebUI.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace CMS.WebUI.Administration.Common
{
    public class AdminPermistion : BaseAdminPage
    {
        public const string Them = "Them";
        public const string Sua = "Sua";
        public const string Xoa = "Xoa";
        public const string Xem = "Xem";
        public virtual string MenuMa { get; set; }
        public static List<MenuPermisstion> menuPermisstions = new List<MenuPermisstion>();
        public static List<MenuPermisstion> menuChilds = new List<MenuPermisstion>();
        public static int CurrentUserId { get; set; }

        public bool IsAlive()
        {
            
            if (menuPermisstions == null || menuPermisstions.Count() <= 0)
            {
                menuPermisstions = (List<MenuPermisstion>)Session["MenuPermission"];
                if (menuPermisstions == null)
                    return false;
            }
            if (CurrentUserId == 0)
            {
                CurrentUserId = int.Parse(Session["UserId"].ToString());

                if (CurrentUserId == 0) return false;
            }

            return true;
        }
        public bool CheckPermission(string menuParent, string Quyen)
        {

            if (menuPermisstions == null || menuPermisstions.Count() <= 0)
            {
                menuPermisstions = (List<MenuPermisstion>)Session["MenuPermission"];
                if (menuPermisstions == null)
                    return false;
            }
            return menuPermisstions.Any(x => x.MenuMa == menuParent && x.PermissionMa == Quyen);

        }




       
    }
}