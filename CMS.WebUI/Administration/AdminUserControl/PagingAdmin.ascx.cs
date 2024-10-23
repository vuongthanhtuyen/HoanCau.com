using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CMS.WebUI.Administration.AdminUserControl
{
    public partial class PagingAdmin : System.Web.UI.UserControl
    {
        public event EventHandler HiddenButtonClicked;

        protected void HiddenButton_Click(object sender, EventArgs e)
        {
            // Kích hoạt sự kiện khi button được click
            if (HiddenButtonClicked != null)
            {
                HiddenButtonClicked(this, e);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            //string request = Request.QueryString["id"];
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
                pageShow = string.Format($@"
                    <li class=""page-item"">
                        <a class=""page-link"" href=""{url}?page={pageIndex-1}"" aria-label=""Previous"">
                            <span aria-hidden=""true"">&laquo;</span>
                            <span class=""sr-only"">Previous</span>
                        </a>
                    </li>
                    ");
            }
            else
            {
                pageShow = string.Format($@"
                    <li class=""page-item disabled"">
                        <a class=""page-link"" href=""javascript:void(0);"" aria-label=""Previous"">
                            <span aria-hidden=""true"">&laquo;</span>
                            <span class=""sr-only"">Previous</span>
                        </a>
                    </li>
                ");
            }

         
            for (int i = 1; i <= countPage; i++)
            {
                if (i == pageIndex)
                {
                    pageShow += string.Format($@" 
                        <li class=""page-item active"">
                            <a class=""page-link"" href=""javascript:void(0);"">{i}</a>
                        </li>");
                }
                else
                {
                    pageShow += string.Format($@"
                                    <li class=""page-item"">
                                        <a class=""page-link"" href=""{url}?page={i}"">{i}</a>
                                    </li>");
                }

            }

            if (pageIndex < countPage)
            {
                pageShow += string.Format($@"
                    <li class=""page-item"">
                      <a class=""page-link"" href=""{url}?page={pageIndex + 1}"" aria-label=""Next"">
                        <span aria-hidden=""true"">&raquo;</span>
                        <span class=""sr-only"">Next</span>
                      </a>
                    </li>
                    ");
            }
            else
            {
                pageShow += string.Format($@"
                    <li class=""page-item disabled"">
                      <a class=""page-link"" href=""javascript:void(0);"" aria-label=""Next"">
                        <span aria-hidden=""true"">&raquo;</span>
                        <span class=""sr-only"">Next</span>
                      </a>
                    </li>
                    ");

            }




            ltlPaging.Text = pageShow;
        }
    }
}



 