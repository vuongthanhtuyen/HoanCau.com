using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CMS.WebUI.Administration.AdminUserControl
{
    public partial class SearchUserControl : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //c
        }
        protected void btnSeachFor_click (object sender, EventArgs e)
        {
            string url = Request.Url.AbsolutePath;
            if (!string.IsNullOrEmpty(txtSearch.Text))
            {
                url +="?search=" + txtSearch.Text;
                
            }
            Response.Redirect(url);
        }
        public void SetSearcKey ()
        {
            txtSearch.Text = Request.QueryString["search"];
            //txtSearch.Text = searchKey;
        }
    }
}