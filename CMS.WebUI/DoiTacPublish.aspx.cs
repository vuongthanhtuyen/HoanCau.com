using CMS.Core.Manager;
using CMS.WebUI.Controls;
using SweetCMS.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CMS.WebUI
{
    public partial class DoiTacPublish1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) {

                Binding();
                Page.Title = "Đối tác";
                
            }
        }
        private void Binding()
        {
            //pageIndex = PagingAdminWeb.GetPageIndex();
            List<DoiTac> doiTacList = new List<DoiTac>();
            int totalRow = 0;
            doiTacList = DoiTacBLL.GetPaging(5, 1, null, null, out totalRow);
            //ViewState["LastIndex"] = (pageIndex - 1) * pageSize;
            //PagingAdminWeb.GetPaging(totalRow, pageIndex);
            SlideTop.ShowBreadcrumb("Đối tác");

            string doiTacShow = "";
            foreach (var doiTac in doiTacList)
            {
                doiTacShow += string.Format($@"
                    <div class=""col colItem"">
                        <div class=""contentItem"">
                            <div class=""wrapImg""><a class=""wrapImgResize img16And9"" href=""{doiTac.LienKetUrl}"" target=""_blank"" title=""{doiTac.Ten}""><img src=""/Administration/UploadImage/{doiTac.HinhAnhUrl}"" alt=""{doiTac.Ten}"" /></a></div>
                            <div class=""wrapText"">
                                <h3 class=""titleItem""><a class=""linkTitleItem"" href=""{doiTac.LienKetUrl}"" target=""_blank"" title=""{doiTac.Ten}"">{doiTac.Ten}</a></h3>
                                <div class=""textDes""></div>
                            </div>
                        </div>
                    </div>");

            }
            ltrShowDoiTac.Text = doiTacShow;
        }


 }

}