using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CMS.WebUI.Administration.AdminUserControl
{
    public partial class AdminNotification : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        
        public void LoadMessage(string message , bool isSuccess = true)
        {
            myModalNotification.Attributes.Add("style", "display: block;");

            if (isSuccess) { 
                lblErrorMessage.Text = "";
                lblSuccessMessage.Text = message;
            }
            else
            {
                lblErrorMessage.Text = message;
                lblSuccessMessage.Text = "";

            }

            UpdatePanelNotification.Update();
        }

        //public void LoadMessage(string message , bool isSuccess = true)
        //{
        //    myModalNotification.Visible = true;
        //    if (isSuccess) { 
        //        lblErrorMessage.Text = "";
        //        lblSuccessMessage.Text = message;
        //    }
        //    else
        //    {
        //        lblErrorMessage.Text = message;
        //        lblSuccessMessage.Text = "";

        //    }
        //}
    }
}