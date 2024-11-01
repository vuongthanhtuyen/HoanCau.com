using CMS.Core.Manager;
using CMS.Core.Publich;
using CMS.DataAsscess;
using CMS.WebUI.Controls;
using SweetCMS.Core.Helper;
using SweetCMS.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CMS.WebUI
{
    public partial class DanhSachDuAnTieuBieuPublish : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Binding();
        }
        private void Binding()
        {
          
            List<BaiVietDto> postList = new List<BaiVietDto>();
            int totalRow = 0;
            int pageIndex = PagePublish.GetPageIndex();

            postList = DanhMucPublishBLL.GetPaging(2, pageIndex, null, null, 4, ApplicationContext.Current.ContentCurrentLanguageId, out totalRow);

            //pageIndex = PagingAdminWeb.GetPageIndex();
            //ViewState["LastIndex"] = (pageIndex - 1) * pageSize;
            //PagingAdminWeb.GetPaging(totalRow, pageIndex);
            PagePublish.GetPaging(totalRow, pageIndex,2);
            SlideTop.ShowBreadcrumb("Dự án tiêu biểu");


            DanhSachDuAnTieuBieu.GetAllDuAnTieuBieu(postList);
            Page.Title = "Danh sách dự án tiêu biểu";
        }
    }
}