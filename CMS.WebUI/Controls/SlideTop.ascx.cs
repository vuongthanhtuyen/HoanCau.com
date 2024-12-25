using SubSonic.Sugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using static CMS.WebUI.Common.ExtendWeb;

namespace CMS.WebUI.Controls
{
    public partial class SlideTop : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        public string ImageThumbnail
        {
            get;set;
            
        }
        public void ShowBreadcrumb( string title,string urlImage = null,  List<Breadcrumb> breadcrumbs = null)
        {
            if (urlImage == null)
            {
                urlImage = "Administration/UploadImage/4f999d87-1362-46ad-953b-f8910e8bdf0bnha-trang.jpg";
            }
            string item = "";
            if(breadcrumbs !=null && breadcrumbs.Count > 0)
            {
                foreach (Breadcrumb b in breadcrumbs)
                {
                    item += string.Format($@"
                            <a class=""linkItem"" href=""{b.Url}"" title=""{b.Title}"">{b.Title}
                                <svg xmlns=""http://www.w3.org/2000/svg"" viewBox=""0 0 448 512"">
                                    <path d=""M313.941 216H12c-6.627 0-12 5.373-12 12v56c0 6.627 5.373 12 12 12h301.941v46.059c0 21.382 25.851 32.09 40.971 16.971l86.059-86.059c9.373-9.373 9.373-24.569 0-33.941l-86.059-86.059c-15.119-15.119-40.971-4.411-40.971 16.971V216z""></path>
                                </svg>
                            </a>");
                }
            }
            string showView = string.Format($@"<div class=""wrapImgItem"">
                <div class=""wrapImgResize"">
                    <img src=""{ImageThumbnail}"" alt=""{title}"" /></div>
                </div>
                <div class=""contentText"">
                    <div class=""container-xl"">
                        <div class=""clearfix contentItem"">
                            <div class=""wrapTitle"">
                                <h1 class=""titleMain"">{title}</h1>
                            </div>
                            <div class=""wrapMenu"">
                                <a class=""linkItem"" href=""Default.aspx"" title=""Trang Chủ"">Trang Chủ
                                    <svg xmlns=""http://www.w3.org/2000/svg"" viewBox=""0 0 448 512"">
                                        <path d=""M313.941 216H12c-6.627 0-12 5.373-12 12v56c0 6.627 5.373 12 12 12h301.941v46.059c0 21.382 25.851 32.09 40.971 16.971l86.059-86.059c9.373-9.373 9.373-24.569 0-33.941l-86.059-86.059c-15.119-15.119-40.971-4.411-40.971 16.971V216z""></path>
                                    </svg>
                                </a>
                                 {item}
                                <a class=""linkItem active"" href=""javascript:void(0);"" title=""{title}"">{title}
                                    <svg xmlns=""http://www.w3.org/2000/svg"" viewBox=""0 0 448 512"">
                                        <path d=""M313.941 216H12c-6.627 0-12 5.373-12 12v56c0 6.627 5.373 12 12 12h301.941v46.059c0 21.382 25.851 32.09 40.971 16.971l86.059-86.059c9.373-9.373 9.373-24.569 0-33.941l-86.059-86.059c-15.119-15.119-40.971-4.411-40.971 16.971V216z""></path>
                                    </svg>
                                </a>
                            </div>
                        </div>
                    </div>
                </div>");

            ltrShow.Text = showView;
        }
    }
}