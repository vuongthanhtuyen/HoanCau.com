using CMS.Core.Publich;
using TBDCMS.Core.Helper;
using TBDCMS.Core.Manager;
using TBDCMS.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CMS.WebUI
{
    public partial class SiteMaster : MasterPage
    {

        protected static string _address = string.Empty;
        protected static string _hotline = string.Empty;

        protected static string _email = string.Empty;
        protected static string _hotlineHelperStudent = string.Empty;
        protected static string _facebookUrl = string.Empty;
        protected static string _tikTokUrl = string.Empty;
        protected static string _youtubeUrl = string.Empty;
        protected static string _zaloUrl = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) {
                _address = DefaultSettingBLL.GetByValueString(KeyName.txtMenuDuoiDiaChiHome);
                _hotline = DefaultSettingBLL.GetByValueString(KeyName.txtMenuDuoiSoDienThoaiHome);
                _hotlineHelperStudent = DefaultSettingBLL.GetByValueString(KeyName.txtMenuDuoiSoDienThoaiHoTroHome);
                _email = DefaultSettingBLL.GetByValueString(KeyName.txtMenuDuoiEmailHome);

                _facebookUrl = !string.IsNullOrEmpty(DefaultSettingBLL.GetByValueString(KeyName.txtMenuDuoiLinkFacebookHome)) ? DefaultSettingBLL.GetByValueString(KeyName.txtMenuDuoiLinkFacebookHome) : "javascript:;";
                _youtubeUrl = !string.IsNullOrEmpty(DefaultSettingBLL.GetByValueString(KeyName.txtMenuDuoiLinkYouTubeHome)) ? DefaultSettingBLL.GetByValueString(KeyName.txtMenuDuoiLinkYouTubeHome) : "javascript:;";
                _tikTokUrl = !string.IsNullOrEmpty(DefaultSettingBLL.GetByValueString(KeyName.txtMenuDuoiLinkTikTokHome)) ? DefaultSettingBLL.GetByValueString(KeyName.txtMenuDuoiLinkTikTokHome) : "javascript:;";
                _zaloUrl = !string.IsNullOrEmpty(DefaultSettingBLL.GetByValueString(KeyName.txtMenuDuoiLinkZaloHome)) ? DefaultSettingBLL.GetByValueString(KeyName.txtMenuDuoiLinkZaloHome) : "javascript:;";
            }
            MenuWebTren();
            MenuWebDuoi();
            ////ltrMenuWebTren.Text= "Nội dung vui vẻ :))";

        }

        








        private void MenuWebTren()
        {
            List<MenuWebTren> menu = MenuPulishBLL.GetAllMenuWebTren(ApplicationContext.Current.CurrentLanguageId);
            string menuTren = "";
            List<MenuWebTren> menuParent = menu.Where(m => m.MenuChaId == 0).ToList();
            foreach (var m in menuParent)
            {
                List<MenuWebTren> menuChild = menu.Where(mn => mn.MenuChaId == m.Id).ToList();
                if (menuChild != null && menuChild.Count > 0)
                {
                    menuTren += string.Format($@"
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
                        menuTren += string.Format($@"
                            <li class=""listItemMenuMainHeaderBottom""><a class=""listLinkMenuMainHeaderBottom"" href=""{child.Slug}"" title=""{child.Ten}"">{child.Ten}</a></li>
                            ");
                    }
                    menuTren += string.Format($@"
                                        </ul>
                                    </div>
                                </li>    
                    ");
                }
                else
                {
                    menuTren += string.Format($@"
                        <li class=""listItemMenuMainHeaderBottom"">
                                <a class=""listLinkMenuMainHeaderBottom"" href=""{m.Slug}"" title=""{m.Ten}"">{m.Ten}</a>
                        </li>
                    ");
                }

            }
            ltrMenuWebTren.Text = menuTren;
        }
        private void MenuWebDuoi()
        {
            List<MenuWebDuoi> menu = MenuPulishBLL.GetAllMenuWebDuoi();
            string menuDuoi = "";
            List<MenuWebDuoi> menuParent = menu.Where(m => m.MenuChaId == 0).ToList();
            foreach (var m in menuParent)
            {
                List<MenuWebDuoi> menuChild = menu.Where(mn => mn.MenuChaId == m.Id).ToList();
                string menuDuoiChild = "";
               
                foreach (var child in menuChild)
                {
                    menuDuoiChild += string.Format($@"  <a class=""linkItem"" href=""{child.Slug}"" title=""{child.Ten}"">{child.Ten}</a> ");
                }
                menuDuoi += string.Format($@"
                                  <div class=""col-sm-4 col-xl colItem"">
                                    <div class=""contentCol"">
                                        <div class=""titleFooter"">{m.Ten}</div>
                                        <div class=""wrapMenu"">
                                            {menuDuoiChild}
                                        </div>
                                    </div>
                                </div>
                ");
             }
            ltrMenuWebDuoi.Text = menuDuoi;
        }

    }
}