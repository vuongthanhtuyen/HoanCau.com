using CMS.Core.Manager;
using CMS.DataAsscess;
using SubSonic;
using SweetCMS.Core.Helper;
using SweetCMS.DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.DynamicData;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace CMS.WebUI.Administration.MasterPage
{
    public partial class AdminPage : System.Web.UI.MasterPage
    {
        private static string _currentMenu = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (!IsPostBack)
            {
                _currentMenu = Request.Url.AbsolutePath.ToLower();
                ShowMenu();
                //ddlContentLanguage.SelectedValue = ApplicationContext.Current.ContentCurrentLanguageId.ToString();
            }
            if ((List<MenuPermisstion>)Session["MenuPermission"] == null)
            {
                string requestURL = Request.Url.AbsoluteUri;
                if (requestURL.Contains("Logout"))
                {
                    Response.Redirect("~/Administration/Login.aspx", false);
                    return;
                }
                Response.Redirect("~/Administration/Login.aspx?url="+ requestURL, false);
            }
        }

        public void ShowMessage(string _icon, string title)
        {
            string script = string.Format("ShowAlert('{0}','{1}');", _icon, title);
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "RunScript", script, true);
        }
        protected void ddlContentLanguage_SelectedIndexChanged(object sender, EventArgs e)
        {
            //ApplicationContext.Current.ContentCurrentLanguageId = int.Parse(ddlContentLanguage.SelectedValue);

            //HttpCookie testCookie = new HttpCookie("VuongThanhTuyen", "2");
            //testCookie.Expires = DateTime.UtcNow.AddDays(1);
            //HttpContext.Current.Response.Cookies.Set(testCookie);
            

            Response.Redirect(ApplicationContext.Current.CurrentUri.AbsoluteUri, false);
        }
       
        private void ShowMenu()
        {
            if (ApplicationContext.Current.CurrentUserID <1)
            {
                Response.Redirect("~/Administration/Login.aspx", false);
            }

            else
            {
                try
                {
                    int userId = ApplicationContext.Current.CurrentUserID;
                    var list = LoginBLL.MenuCheckByUser(userId);
                    lblUserName.InnerText = NguoiDungManagerBLL.GetById(userId).HoVaTen ?? string.Empty;

                    string strMenu = string.Empty;
                    // truyền menu cha, và list menu con của menu đó
                    foreach (var menu in list.Where(m => m.MenuChaId == 0).OrderBy(m => m.Ten))
                    {
                        List<MenuAdmin> menuChild = list.Where(m => m.MenuChaId == menu.Id).ToList();
                        strMenu += RenderMenu(menu, menuChild);
                    }
        
                    MenuLeftNavbars.Text =strMenu; 

                }
                catch
                {

                }
            }


        }

       

        private string RenderMenu(MenuAdmin menu, List<MenuAdmin> menuChilds)
        {
            StringBuilder strMenu = new StringBuilder();
            StringBuilder strMenuChild = new StringBuilder();
            bool isOn = false;
            if (menuChilds != null && menuChilds.Count > 0)
            {
               
                if (menuChilds.Any(x => x.Url.ToLower() == _currentMenu))
                {
                    isOn = true;
                }

                foreach (MenuAdmin child in menuChilds)
                {
                    strMenuChild.Append($@"
                 <li class=""nav-item"">
                     <a href=""{child.Url}"" class=""nav-link {GetActive(child.Url.ToLower() ==_currentMenu)} "">
                       <i class=""far fa-circle nav-icon""></i>
                       <p>{child.Ten}</p>
                     </a>
                 </li>
               ");
                }
                strMenu.Append($@"
               <li class=""nav-item {GetOpenMenu(isOn)}"">
                 <a href=""#"" class=""nav-link {GetActive(isOn)}"">
                   <i class=""nav-icon {menu.Icon}""></i>
                   <p>
                     {menu.Ten}
                     <i class=""fas fa-angle-left right""></i>
                   </p>
                 </a>
                 <ul class=""nav nav-treeview"">
                   {strMenuChild}
                 </ul>
               </li>
         ");
            }
            else
            {
                strMenu.Append($@" 
                   <li class=""nav-item  "">
                     <a href=""{menu.Url}"" class=""nav-link {GetActive(menu.Url.ToLower() == _currentMenu)}"">
                       <i class="" nav-icon {menu.Icon}""></i>
                       <p>{menu.Ten}</p>
                     </a>
                   </li>
                 ");
            }
            return strMenu.ToString();
        }
        private string GetActive(bool active)
        {
            if (active)
            {
                return " active ";
            }
            return string.Empty;
        }
        private string GetOpenMenu(bool active)
        {
            if (active)
            {
                return " menu-is-opening menu-open ";
            }
            return string.Empty;
        }
    }
}