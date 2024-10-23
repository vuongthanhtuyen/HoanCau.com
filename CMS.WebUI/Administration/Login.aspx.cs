using CMS.Core.Manager;
using CMS.DataAsscess;
using CMS.WebUI.Administration.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CMS.WebUI.Administration
{
    public partial class Login : AdminPermistion
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["IdUser"] = 0;
            Session["MenuPermission"] = null;
        }
        protected void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtUsername.Text.Length < 1)
                {
                    lblErrorMessage.Text = "Tên đăng nhập không hợp lệ";
                    return;
                }
                else if (txtPassword.Text.Length < 1)
                {
                    lblErrorMessage.Text = "Mật khẩu không hợp lệ";
                    return;
                }
                else
                {
                    var login = LoginBLL.Login(txtUsername.Text, txtPassword.Text);
                    if (login != null)
                    {
                        // Lưu user Id vào session
                        Session["UserId"] = login.Id;
                        CurrentUserId= login.Id;
                        // Lưu Quyền của User vào session
                        List<MenuPermisstion> listMenuPermission = LoginBLL.GetListMenuPermisstionByUser(login.Id);
                        Session["MenuPermission"] = listMenuPermission;
                        Response.Redirect("~/Administration/Default.aspx", false);
                        return;
                    }
                    else
                    {
                        lblErrorMessage.Text = "Tài khoản hoặc mật khẩu không đúng";
                    }
                    //Response.Redirect("default.aspx");
                }
            }
            catch (Exception ex)
            {
                lblErrorMessage.Text = "Lỗi " + ex.Message;

            }

        }
    }
}