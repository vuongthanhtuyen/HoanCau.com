using CMS.Core.Manager;
using CMS.DataAsscess;
using CMS.WebUI.Administration.AdminUserControl;
using CMS.WebUI.Administration.Common;
using SweetCMS.Core.Helper;
using SweetCMS.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CMS.WebUI.Administration
{
    public partial class NguoiDungWeb : AdminPermistion
    {
        public override string MenuMa { get; set; } = "Nguoi-dung";  // Ghi đè và gán giá trị mới
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindDataByQuyen();
                if (!IsAlive()) Response.Redirect("/Administration/Login.aspx");

            }
        }

        private void BindDataByQuyen()
        {
            btnOpenModal.Visible = CheckPermission(MenuMa, Them);
            if (CheckPermission(MenuMa, Xem))
            {
                BindGrid();

                foreach (GridViewRow row in GridViewTable.Rows)
                {
                    if (row.RowType == DataControlRowType.DataRow)
                    {
                        // Tìm LinkButton trong hàng
                        LinkButton btnEdit = (LinkButton)row.FindControl("ChinhSuaChiTiet");
                        LinkButton btnDelete = (LinkButton)row.FindControl("Xoa");
                        LinkButton chinhSuaVaiTro = (LinkButton)row.FindControl("chinhSuaVaiTro");

                        if (btnEdit != null)
                        {
                            btnEdit.Visible = CheckPermission(MenuMa, Sua);
                            btnDelete.Visible = CheckPermission(MenuMa, Xoa);
                            chinhSuaVaiTro.Enabled = CheckPermission(MenuMa, Sua);
                        }

                    }
                }
            }
            UpdatePanelTablleMain.Update();
        }

        private void BindGrid(int pageIndex = 1, int pageSize = 10)
        {
            pageIndex = PagingAdminWeb.GetPageIndex();
            List<NguoiDungDto> postList = new List<NguoiDungDto>();
            int totalRow = 0;
            postList = NguoiDungManagerBLL.GetNguoiDungPaging(pageSize, pageIndex, Request.QueryString["search"], null, out totalRow);
            
            if (ApplicationContext.Current.CurrentUserID != 31)
            {
                postList = postList.Where(x => x.Id != 31).ToList();
            }
            SearchUserControl.SetSearcKey();
            ViewState["LastIndex"] = (pageIndex - 1) * pageSize;
            PagingAdminWeb.GetPaging(totalRow, pageIndex);
            GridViewTable.DataSource = postList;
            GridViewTable.DataBind();
        }



        protected void GridViewTable_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                int nguoiDungId = Convert.ToInt32(e.CommandArgument);
                var nguoiDung = NguoiDungManagerBLL.GetById(nguoiDungId);
                if (nguoiDung != null)
                {
                    hdnRowId.Value = nguoiDungId.ToString();
                    if (e.CommandName == "ChinhSuaVaiTro")
                    {
                        var roleOfUsers = NguoiDungManagerBLL.GetAllVaiTroByNguoiDungId(nguoiDungId);
                        GridViewRole.DataSource = roleOfUsers;
                        GridViewRole.DataBind();
                        BindDataByQuyen();
                        UpdatePanelUpdateRole.Update();
                        // Đăng ký đoạn mã JavaScript
                        ScriptManager.RegisterStartupScript(this, GetType(), "openRoleEditModal", "openRoleEditModal();", true);
                    }
                    else if (e.CommandName == "ChinhSuaChiTiet")
                    {
                        txtEditHoVaTen.Text = nguoiDung.HoVaTen;
                        txtEditEmail.Text = nguoiDung.Email;
                        txtEditSoDienThoai.Text = nguoiDung.SoDienThoai;
                        txtEditNgaySinh.Text = ((DateTime)nguoiDung.NgaySinh).ToString("yyyy-MM-dd"); // Đảm bảo định dạng cho TextBox ngày
                        txtEditTenTruyCap.Text = nguoiDung.TenTruyCap;
                        txtEditMatKhau.Text = nguoiDung.MatKhau; // Nếu cần hiển thị mật khẩu, nếu không, bạn có thể để trống
                        txtEditDiaChi.Text = nguoiDung.DiaChi;
                        txtEditAvatarUrl.Text = nguoiDung.AvataUrl;
                        chkEditTrangThai.Checked = nguoiDung.TrangThai ?? false;
                        ScriptManager.RegisterStartupScript(this, GetType(), "openEdit", "openEdit();", true);

                        UpdatePanelEdit.Update();
                    }
                    else if (e.CommandName == "Xoa")
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "openDelete", "openDelete();", true);
                    }
                }
                else
                {
                    ShowNotification("Người dùng này không tồn tại", false);
                }
            }
            catch (Exception ex)
            {
                ShowNotification(ex.Message, false);
            }

        }


        #region Thêm sửa xóa
        protected void btnUserAdd_Click(object sender, EventArgs e)
        {
            try
            {
                NguoiDung nguoiDung = new NguoiDung();
                lblAddErrorMessage.Text = "";
                if (string.IsNullOrEmpty(txtHoVaTen.Text.Trim()) || txtHoVaTen.Text.Trim().Length <= 3)
                {
                    lblAddErrorMessage.Text += "Fullname không được trống hoặc bé hơn 3 ký tự<br />";
                }
                if (string.IsNullOrEmpty(txtEmail.Text.Trim()) || txtEmail.Text.Trim().Length <= 3)
                {
                    lblAddErrorMessage.Text += "Email không hợp lệ<br /> ";
                }
                if (string.IsNullOrEmpty(txtTenTruyCap.Text.Trim()) || txtTenTruyCap.Text.Trim().Length <= 3 ||
                    txtTenTruyCap.Text.Contains(" "))
                {
                    lblAddErrorMessage.Text += "Username không hợp lệ<br />";
                }
                if (string.IsNullOrEmpty(txtMatKhau.Text.Trim()) || txtMatKhau.Text.Trim().Length < 1)
                {
                    lblAddErrorMessage.Text += "Mật khẩu không hợp lệ<br /> ";
                }
                if (lblAddErrorMessage.Text.Length > 0)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "OpenModal", "openModal();", true);
                    return;
                }
                else
                {
                    if (!VaiTroManagerBll.AllowAdd(ApplicationContext.Current.CurrentUserID, MenuMa))
                    {
                        ShowNotification("Bạn không có quyền truy cập chức năng này", false);
                        return;
                    }
                    if (NguoiDungManagerBLL.GetNguoiDungByTenTruyCap(txtTenTruyCap.Text) != null)
                    {
                        lblAddErrorMessage.Text = "Username đã tồn tại, chọn tên khác ";
                        ScriptManager.RegisterStartupScript(this, GetType(), "OpenModal", "openModal();", true);
                    }
                    else
                    {

                        nguoiDung.HoVaTen = txtHoVaTen.Text;
                        nguoiDung.Email = txtEmail.Text;
                        nguoiDung.SoDienThoai = txtSoDienThoai.Text;
                        nguoiDung.DiaChi = txtDiaChi.Text;
                        nguoiDung.TenTruyCap = txtTenTruyCap.Text;
                        nguoiDung.MatKhau = txtMatKhau.Text;
                        nguoiDung.NgaySinh = DateTime.Parse(txtNgaySinh.Text);
                        nguoiDung.AvataUrl = txtAvatarUrl.Text;
                        nguoiDung.TrangThai = true;
                        nguoiDung = NguoiDungManagerBLL.Insert(nguoiDung);
                        BindDataByQuyen();
                        UpdatePanelTablleMain.Update();

                        ScriptManager.RegisterStartupScript(this, GetType(), "CloseModal", "closeModal();", true);

                        if (nguoiDung != null)
                        {
                            lblAddErrorMessage.Text = "";
                            ShowNotification("Thêm mới người dùng thành công");
                            txtHoVaTen.Text = string.Empty;
                            txtEmail.Text = string.Empty;
                            txtSoDienThoai.Text = string.Empty;
                            txtNgaySinh.Text = string.Empty;
                            txtTenTruyCap.Text = string.Empty;
                            txtMatKhau.Text = string.Empty;
                            txtDiaChi.Text = string.Empty;
                            txtAvatarUrl.Text = string.Empty;
                            UpdatePanelAdd.Update();
                        }
                        else
                        {
                            ShowNotification("Thêm mới người dùng thất bại", false);
                        }
                    }

                }
            }

            catch (Exception ex)
            {
                ShowNotification("Thêm mới người dùng thất bại! \n Lỗi: " + ex.Message, false);

            }
        }


        protected void btnUserEdit_Click(object sender, EventArgs e)
        {
            try
            {
                int userId = int.Parse(hdnRowId.Value); // Lấy ID người dùng đã chỉnh sửa
                lblEditMessager.Text = "";
                if (string.IsNullOrEmpty(txtEditHoVaTen.Text.Trim()) || txtEditHoVaTen.Text.Trim().Length <= 3)
                {
                    lblEditMessager.Text += "Fullname không được trống hoặc bé hơn 3 ký tự<br />";
                }
                if (string.IsNullOrEmpty(txtEditEmail.Text.Trim()) || txtEditEmail.Text.Trim().Length <= 3)
                {
                    lblEditMessager.Text += "Email không hợp lệ<br /> ";
                }
                if (string.IsNullOrEmpty(txtEditTenTruyCap.Text.Trim()) || txtEditTenTruyCap.Text.Trim().Length <= 1 ||
                    txtEditTenTruyCap.Text.Contains(" "))
                {
                    lblEditMessager.Text += "Username không hợp lệ<br />";
                }
                if (lblEditMessager.Text.Length > 0)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "OpenModal", "openModal();", true);
                    return;
                }
                else
                {
                    if (!VaiTroManagerBll.AllowEdit(ApplicationContext.Current.CurrentUserID, MenuMa))
                    {
                        ShowNotification("Bạn không có quyền truy cập chức năng này", false);
                        return;
                    }

                    NguoiDung nguoiDung = NguoiDungManagerBLL.GetById(userId);
                    if (nguoiDung.TenTruyCap != txtEditTenTruyCap.Text)
                    {
                        if (NguoiDungManagerBLL.GetNguoiDungByTenTruyCap(txtEditTenTruyCap.Text) != null)
                        {
                            lblEditMessager.Text = "Username đã tồn tại, chọn tên khác ";
                            ScriptManager.RegisterStartupScript(this, GetType(), "OpenModal", "openModal();", true);
                            return;

                        }
                    }
                    // Lấy thông tin người dùng từ cơ sở dữ liệu
                    nguoiDung.HoVaTen = txtEditHoVaTen.Text;
                    nguoiDung.Email = txtEditEmail.Text;
                    nguoiDung.SoDienThoai = txtEditSoDienThoai.Text;
                    nguoiDung.NgaySinh = DateTime.Parse(txtEditNgaySinh.Text);
                    nguoiDung.TenTruyCap = txtEditTenTruyCap.Text;
                    if (!string.IsNullOrEmpty(txtEditMatKhau.Text))
                    {
                        nguoiDung.MatKhau = txtEditMatKhau.Text;
                    }
                    nguoiDung.DiaChi = txtEditDiaChi.Text;
                    nguoiDung.AvataUrl = txtEditAvatarUrl.Text;
                    nguoiDung.TrangThai = chkEditTrangThai.Checked;
                    nguoiDung.ChinhSuaGanNhat = DateTime.Now;
                    // Cập nhật vào cơ sở dữ liệu
                    NguoiDungManagerBLL.Update(nguoiDung);
                    BindDataByQuyen();
                    UpdatePanelTablleMain.Update();
                    hdnRowId.Value = "";
                    ScriptManager.RegisterStartupScript(this, GetType(), "closeEdit", "closeEdit();", true);

                    ShowNotification("Cập nhật thành công", true);


                }

            }
            catch (Exception ex)
            {
                ShowNotification("Cập nhật người dùng thất bại! \n " + ex.Message, false);
            }
        }
        protected void btnRoleEditSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (!VaiTroManagerBll.AllowEdit(ApplicationContext.Current.CurrentUserID, MenuMa))
                {
                    ShowNotification("Bạn không có quyền truy cập chức năng này", false);
                    return;
                }
                int nguoiDungId = int.Parse(hdnRowId.Value);
                hdnRowId.Value = "";
                string listIdRole = "";
                foreach (GridViewRow row in GridViewRole.Rows)
                {
                    if (row.RowType == DataControlRowType.DataRow)
                    {
                        // Lấy giá trị từ cột "IdRole" (Id)
                        string idRole = row.Cells[0].Text;

                        CheckBox chkIsHaveRole = (CheckBox)row.FindControl("CoVaiTro");

                        if (chkIsHaveRole != null && chkIsHaveRole.Checked)
                        {
                            bool isHaveRole = chkIsHaveRole.Checked;

                            if (!string.IsNullOrEmpty(listIdRole))
                            {
                                listIdRole += ",";
                            }
                            listIdRole += idRole;
                        }
                    }
                }


                ScriptManager.RegisterStartupScript(this, GetType(), "closeRoleEditModal", "closeRoleEditModal();", true);
                NguoiDungManagerBLL.UpdateVaiTroChoNguoiDung(nguoiDungId, listIdRole);
                BindDataByQuyen();
                UpdatePanelTablleMain.Update();
                ShowNotification("Cập nhật quyền cho người dùng thành công");

            }
            catch (Exception ex)
            {

                ShowNotification("Cập nhật người dùng thất bại! \n " + ex.Message, false);
            }


        }




        protected void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                int userId = int.Parse(hdnRowId.Value);
                hdnRowId.Value = "";
                if (!string.IsNullOrEmpty(userId.ToString()))
                {
                    var nguoiDung = NguoiDungManagerBLL.GetById(userId);

                    if (nguoiDung == null)
                    {
                        ShowNotification("Lỗi không tìm thấy người dùng", false);
                        return;
                    }
                    else if (NguoiDungManagerBLL.NguoiDungCheckAdmin(userId) != null)
                    {
                        ShowNotification("Không thể xóa người dùng là Admin", false);
                        return;
                    }
                    if (!VaiTroManagerBll.AllowDelete(ApplicationContext.Current.CurrentUserID, MenuMa))
                    {
                        ShowNotification("Bạn không có quyền truy cập chức năng này", false);
                        return;
                    }

                    else if (NguoiDungManagerBLL.Delete(userId))
                    {
                        ShowNotification("Đã xóa user");
                        BindDataByQuyen();
                        UpdatePanelTablleMain.Update();
                        ScriptManager.RegisterStartupScript(this, GetType(), "closeDelete", "closeDelete();", true);
                    }
                    else
                    {
                        ShowNotification("Xóa user không thành công", false);
                        return;
                    }
                }

            }
            catch (Exception ex)
            {
                ShowNotification("Xóa thất bại! \n " + ex.Message, false);
            }
        }

        #endregion

        private void ShowNotification(string message, bool isSuccess = true)
        {
            AdminNotificationUserControl.LoadMessage(message, isSuccess);
        }

    }
}