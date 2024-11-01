using CMS.Core.Manager;
using CMS.WebUI.Common;
using SweetCMS.Core.Helper;
using SweetCMS.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CMS.WebUI
{
    public partial class LienHePublish : BaseAdminPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                SlideTop.ShowBreadcrumb("Liên hệ");
                Page.Title = "Liên hệ";
                Form();
            }


        }


        private void Form()
        {
            string lblPhone, lblAddress, lblAddressView, lblTitle, lbltxtFullname, lbltxtEmail, lbltxtPhone, lbltxtTitle, lbltxtMessage, lblbtnSent;
            if(ApplicationContext.Current.CurrentLanguageId == 1)
            {
                lblPhone = "ĐIỆN THOẠI";
                lblAddress = "ĐỊA CHỈ";
                lblAddressView = "Xem bản đồ";
                lblTitle = "GỬI TIN NHẮN";
                lbltxtFullname = "Nhập họ và tên";
                lbltxtPhone = "Nhập số điện thoại";
                lbltxtEmail = "Nhập email";
                lbltxtPhone = "Nhập số điện thoại";
                lbltxtTitle = "Nhập chủ đề";
                lbltxtMessage = "Nhập tin nhắn";
                lblbtnSent = "GỬI NGAY";
            }
            else
            {
                lblPhone = "PHONE";
                lblAddress = "ADDRESS";
                lblAddressView = "View map";
                lblTitle = "SEND MESSAGE";
                lbltxtFullname = "Enter full name";
                lbltxtPhone = "Enter phone number";
                lbltxtEmail = "Enter email";
                lbltxtTitle = "Enter subject";
                lbltxtMessage = "Enter message";
                lblbtnSent = "SEND NOW";
            }
            //StringBuilder sbContent = new StringBuilder();
            //    sbContent.AppendFormat(templateItemHotContact.InnerHtml, lblPhone, lblAddress, lblAddressView, lblTitle, lbltxtFullname, lbltxtEmail, lbltxtPhone, lbltxtTitle, lbltxtMessage, lblbtnSent);
            //ltrFormContact.Text = sbContent.ToString();
        }

        protected void btnSend_ServerClick(object sender, EventArgs e)
        {
            try
            {
                LienHe lienHe = new LienHe();
                lblAddErrorMessage.Text = "";
                if (string.IsNullOrEmpty(txtHoVaTen.Text.Trim()))
                {
                    AddErrorPrompt(txtHoVaTen.ClientID, "Không được bỏ trống trường này");

                }
                if (string.IsNullOrEmpty(txtTinNhan.Text.Trim()))
                {
                    AddErrorPrompt(txtTinNhan.ClientID, "Không được bỏ trống trường này");
                }
                if (string.IsNullOrEmpty(txtSoDienThoai.Text.Trim()))
                {
                    AddErrorPrompt(txtSoDienThoai.ClientID, "Không được bỏ trống trường này");

                }
                if (!IsValid)
                {
                    ShowErrorPrompt();
                    return;
                }
                else
                {
                    lienHe.HoVaTen = txtHoVaTen.Text;
                    lienHe.Email = txtEmail.Text;
                    lienHe.ChuDe = txtChuDe.Text;
                    lienHe.SoDienThoai = txtSoDienThoai.Text;
                    lienHe.NgayTao = DateTime.Now;
                    lienHe.TinNhan = txtTinNhan.Text;
                    lienHe.DaTraLoi = false;
                    lienHe = LienHeBLL.Insert(lienHe);

                    txtHoVaTen.Text = string.Empty;
                    txtEmail.Text = string.Empty;
                    txtSoDienThoai.Text = string.Empty;
                    txtChuDe.Text = string.Empty;
                    txtTinNhan.Text = string.Empty;

                    lblAddErrorMessage.Text = "Đã gửi liên hệ";
                    UpdatePanelContact.Update();
                }
            }

            catch (Exception ex)
            {
                lblAddErrorMessage.Text = "Không gửi được liên hệ";

            }
        }
    }
}