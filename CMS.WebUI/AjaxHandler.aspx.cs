using CMS.Core.Publich;
using SweetCMS.Core.Helper;
using SweetCMS.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CMS.WebUI
{
    public partial class AjaxHandler : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        [WebMethod]
        public static string ChangLangueId(string langId)
        {
            // return new menu 
            ApplicationContext.Current.CurrentLanguageId = int.Parse(langId);
            string content = MenuWebTren(int.Parse(langId));
            //JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
            //string jsonResult = javaScriptSerializer.Serialize(content);
            return content;

        }

        private static string MenuWebTren(int langID)
        {
            List<MenuWebTren> menu = MenuPulishBLL.GetAllMenuWebTren(langID);
            StringBuilder menuBuilder = new StringBuilder();
            List<MenuWebTren> menuParent = menu.Where(m => m.MenuChaId == 0).ToList();
            foreach (var m in menuParent)
            {
                List<MenuWebTren> menuChild = menu.Where(mn => mn.MenuChaId == m.Id).ToList();
                if (menuChild != null && menuChild.Count > 0)
                {
                    menuBuilder.Append($@"
                                <li class=""listItemMenuMainHeaderBottom menuSubHeaderBottom"">
                                    <a class=""listLinkMenuMainHeaderBottom"" href=""{m.Slug}"" title=""{m.Ten}"">{m.Ten}</a>
                                    <a class=""btnDropdowMenuSubHeaderBottom"" href=""javascript:void(0);"" title=""{m.Ten}"">
                                    <svg aria-hidden=""true"" focusable=""false"" role=""img"" xmlns=""http://www.w3.org/2000/svg"" viewbox=""0 0 320 512"">
                                        <path fill=""currentColor"" d=""M151.5 347.8L3.5 201c-4.7-4.7-4.7-12.3 0-17l19.8-19.8c4.7-4.7 12.3-4.7 17 0L160 282.7l119.7-118.5c4.7-4.7 12.3-4.7 17 0l19.8 19.8c4.7 4.7 4.7 12.3 0 17l-148 146.8c-4.7 4.7-12.3 4.7-17 0z""></path>
                                    </svg></a>
                                    <div class=""wrapMenuMainHeaderBottom"">
                                        <ul class=""listMenuMainHeaderBottom"">
 
                    ");
                    foreach (var child in menuChild)
                    {
                        menuBuilder.Append($@"
                            <li class=""listItemMenuMainHeaderBottom""><a class=""listLinkMenuMainHeaderBottom"" href=""{child.Slug}"" title=""{child.Ten}"">{child.Ten}</a></li>
                            ");
                    }
                    menuBuilder.Append($@"
                                        </ul>
                                    </div>
                                </li>    
                    ");
                }
                else
                {
                    menuBuilder.Append($@"
                        <li class=""listItemMenuMainHeaderBottom"">
                                <a class=""listLinkMenuMainHeaderBottom"" href=""{m.Slug}"" title=""{m.Ten}"">{m.Ten}</a>
                        </li>
                    ");
                }

            }
            return menuBuilder.ToString();
        }


    }
}