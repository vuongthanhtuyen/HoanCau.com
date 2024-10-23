using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CMS.WebUI.Controls
{
    public partial class PagePublish : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }


        public int GetPageIndex(int pageIndex = 1)
        {
            string page = Request.QueryString["page"];
            if (!string.IsNullOrEmpty(page))
            {
                int.TryParse(Request.QueryString["page"], out pageIndex);
            }
            return pageIndex;
        }
        public void GetPaging(int totalRow, int pageIndex = 1, int pageSize = 10)
        {

            string url = Request.Url.AbsolutePath;

            string pageShow = "";

            string pageHref = string.Format($@"javascript:void(0);");


            int countPage = (totalRow % pageSize != 0) ? (totalRow / pageSize + 1) : (totalRow / pageSize);

            if (pageIndex > 1)
            {
                pageHref = string.Format($@"{url}?page={pageIndex - 1}");
            }
            pageShow = string.Format($@"
                    <a class=""linkPagging prev"" href=""{pageHref}"" title=""Previus"">
                            <span>
                                <svg xmlns=""http://www.w3.org/2000/svg"" viewBox=""0 0 448 512"">
                                    <path d=""M9.4 233.4c-12.5 12.5-12.5 32.8 0 45.3l160 160c12.5 12.5 32.8 12.5 45.3 0s12.5-32.8 0-45.3L109.2 288 416 288c17.7 0 32-14.3 32-32s-14.3-32-32-32l-306.7 0L214.6 118.6c12.5-12.5 12.5-32.8 0-45.3s-32.8-12.5-45.3 0l-160 160z"" />
                                </svg>
                            </span>
                        </a>
                    ");
            for (int i = 1; i <= countPage; i++)
            {
                if (i == pageIndex)
                {
                    pageShow += string.Format($@" 
                                    <a class=""linkPagging active"" href=""javascript:void(0);"">
                                        <span>{i / 10}{i % 10}</span>
                                    </a>");
                }
                else
                {
                    pageShow += string.Format($@" 
                                    <a class=""linkPagging"" href=""{url}?page={i}"">
                                        <span>{i / 10}{i % 10}</span>
                                    </a>");
                }

            }

            if (pageIndex < countPage)
            {
                pageHref = string.Format($@"{url}?page={pageIndex + 1}");
            }
            else
                pageHref = string.Format($@"javascript:void(0);");

            pageShow += string.Format($@"
                    <a class=""linkPagging next"" href=""{pageHref}"" title=""Next"">
                        <span>
                            <svg xmlns=""http://www.w3.org/2000/svg"" viewBox=""0 0 448 512"">
                                <path d=""M438.6 278.6c12.5-12.5 12.5-32.8 0-45.3l-160-160c-12.5-12.5-32.8-12.5-45.3 0s-12.5 32.8 0 45.3L338.8 224 32 224c-17.7 0-32 14.3-32 32s14.3 32 32 32l306.7 0L233.4 393.4c-12.5 12.5-12.5 32.8 0 45.3s32.8 12.5 45.3 0l160-160z"" />
                            </svg>
                        </span>
                    </a>
                    ");



            ltrPage.Text = pageShow;
        }

    }
}