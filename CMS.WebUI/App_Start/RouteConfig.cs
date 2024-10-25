using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Routing;
using CMS.Core.Manager;
using Microsoft.AspNet.FriendlyUrls;

namespace CMS.WebUI
{
    public static class RouteConfig
    {
        // check url ảo rồi trả về url thật 
          
            
        public static void RegisterRoutes(RouteCollection routes)
        {
            //List<SweetCMS.DataAccess.FriendlyUrl> friendlyUrls = FriendlyUrlBLL.GetAll();
            
            var settings = new FriendlyUrlSettings();
            settings.AutoRedirectMode = RedirectMode.Permanent;
            routes.EnableFriendlyUrls(settings);
            //routes.MapPageRoute("Trang chu", "", "~/Default.aspx");
            //routes.MapPageRoute("Danh Muc Du An Tieu Bieu", "danh-sach-du-an-tieu-bieu", "~/DanhSachDuAnTieuBieuPublish.aspx");


            //routes.MapPageRoute("Bai Viet", "{MetaTitle}", "Baiviet?id={?}");
        }
    }
}
