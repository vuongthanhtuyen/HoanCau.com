using CMS.DataAsscess;
using CMS.WebUI.Administration.Common;
using TBDCMS.Core.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CMS.WebUI.Administration
{
    public partial class Logout : AdminPermistion
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //string script = "LogOutModal();";

            //// Đăng ký đoạn mã JavaScript
            //ScriptManager.RegisterStartupScript(this, GetType(), "LogOutModal", script, true);
            Session["UserId"] = 0;
            Session["MenuPermission"] = null;
            Response.Redirect("~/Administration/Login.aspx", false);
            menuPermisstions.Clear();
            ApplicationContext.Current.CurrentUserID = 0;
        }
    }
}