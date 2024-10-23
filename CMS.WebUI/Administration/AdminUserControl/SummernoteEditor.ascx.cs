using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CMS.WebUI.Administration.AdminUserControl
{
    public partial class SummernoteEditor : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        public string GetContent()
        {
            try
            {
                return inputNoiDung.Value;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        public void SetContent(string content)
        {
            try
            {
                inputNoiDung.Value = content;
            }
            catch (Exception ex)
            {
                inputNoiDung.Value = "Lỗi không lấy được nội dunng";
            }
        }
        public void GetEmpty()
        {
            try
            {
                inputNoiDung.Value = string.Empty;
            }
            catch (Exception ex)
            {
                inputNoiDung.Value = "Lỗi không lấy được nội dunng";
            }
        }

    }
}