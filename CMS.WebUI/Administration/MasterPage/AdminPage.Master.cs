using CMS.Core.Manager;
using CMS.DataAsscess;
using SubSonic;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace CMS.WebUI.Administration.MasterPage
{
    public partial class AdminPage : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ShowMenu();
            }
            if ((List<MenuPermisstion>)Session["MenuPermission"] == null)
            {
                Response.Redirect("~/Administration/Login.aspx", false);
            }
        }

        private void ShowMenu()
        {
            if (Session["UserId"] == null)
            {
                Response.Redirect("~/Administration/Login.aspx", false);
            }
            //else if ((List<MenuPermisstion>)Session["MenuPermission"] == null)
            //{
            //    Response.Redirect("~/Administration/Login.aspx", false);
            //}
            else
            {
                try
                {
                    int userId = int.Parse(Session["UserId"].ToString());
                    
                    string currentUrl = Request.Url.AbsolutePath + ".aspx";
                    var list = LoginBLL.MenuCheckByUser(userId);
                    nameUser.Text = NguoiDungManagerBLL.GetById(userId).HoVaTen ?? "";

                    //var list = MenuManagerBLL.GetAllMenu();

                    string a = "";
                    foreach (var menu in list)
                    {

                        if (menu.Url == null)
                        {
                            if (list.Any(x => x.Url == currentUrl && x.MenuChaId == menu.Id))
                            {
                                a = a + string.Format($@" 
                            <li class=""nav-item active"">
                                <a class=""nav-link collapsed"" href=""#"" data-toggle=""collapse"" data-target=""#{menu.Ma}""
                                    aria-expanded=""true"" aria-controls=""{menu.Ma}"">
                                    <i class=""fas fa-fw fa-cog""></i>
                                    <span>{menu.Ten}</span>
                                </a>
                                <div id=""{menu.Ma}"" class=""collapse show"" aria-labelledby=""headingTwo"" data-parent=""#accordionSidebar"">
                                    <div class=""bg-white py-2 collapse-inner rounded"">
                                        <h6 class=""collapse-header"">{menu.Ten}</h6>");
                                foreach (var submenu in list.Where(x => x.MenuChaId == menu.Id))
                                {
                                    if (submenu.Url == currentUrl)
                                    {
                                        a += string.Format(@"<a class=""collapse-item active"" href=""{0}"">{1}</a>", submenu.Url, submenu.Ten);
                                    }
                                    else
                                    {
                                        a += string.Format(@"<a class=""collapse-item"" href=""{0}"">{1}</a>", submenu.Url, submenu.Ten);
                                    }
                                }
                                a = a + string.Format(@" </div>
                                        </div>
                                    </li>");
                            }
                            else
                            {
                                a = a + string.Format($@" <li class=""nav-item"">
                                <a class=""nav-link collapsed"" href=""#"" data-toggle=""collapse"" data-target=""#{menu.Ma}""
                                    aria-expanded=""true"" aria-controls=""{menu.Ma}"">
                                    <i class=""fas fa-fw fa-cog""></i>
                                    <span>{menu.Ten}</span>
                                </a>
                                <div id=""{menu.Ma}"" class=""collapse"" aria-labelledby=""headingTwo"" data-parent=""#accordionSidebar"">
                                    <div class=""bg-white py-2 collapse-inner rounded"">
                                        <h6 class=""collapse-header"">{menu.Ma}</h6>");
                                foreach (var submenu in list.Where(x => x.MenuChaId == menu.Id))
                                {
                                    a += string.Format(@"<a class=""collapse-item"" href=""{0}"">{1}</a>", submenu.Url, submenu.Ten);
                                }
                                a = a + string.Format(@" </div>
                                        </div>
                                    </li>");
                            }
                        }
                        else if (menu.MenuChaId == null)
                        {
                            if (menu.Url == currentUrl)
                            {
                                a += string.Format($@"  
                        <li class=""nav-item active"">
                            <a class=""nav-link"" href=""{menu.Url}"">
                                <i class=""fas fa-fw fa-chart-area""></i>
                                <span>{menu.Ten}</span></a>
                        </li>");
                            }
                            else
                            {
                                a += string.Format($@"  
                        <li class=""nav-item"">
                            <a class=""nav-link"" href=""{menu.Url}"">
                                <i class=""fas fa-fw fa-chart-area""></i>
                                <span>{menu.Ten}</span></a>
                        </li>");
                            }

                        }
                    }


                    MenuLeftNavbars.Text = a;

                }
                catch
                {

                }
            }


        }
    }
}