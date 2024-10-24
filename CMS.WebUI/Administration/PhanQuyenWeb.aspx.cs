using CMS.Core.Manager;
using CMS.DataAsscess;
using CMS.WebUI.Administration.AdminUserControl;
using CMS.WebUI.Administration.Common;
using SweetCMS.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.DynamicData;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CMS.WebUI.Administration
{
    public partial class PhanQuyenWeb : AdminPermistion
    {
        public override string MenuMa { get; set; } = "Phan-quyen";  // Ghi đè và gán giá trị mới
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
                        LinkButton btnPhanQuyen = (LinkButton)row.FindControl("phanQuyen");
                        LinkButton btnDelete = (LinkButton)row.FindControl("Xoa");

                        if (btnEdit != null)
                        {
                            btnEdit.Visible = CheckPermission(MenuMa, Sua);
                            btnPhanQuyen.Visible = CheckPermission(MenuMa, Sua);
                            btnDelete.Visible = CheckPermission(MenuMa, Xoa);
                        }
                    }
                }
            }
            UpdatePanelTablleMain.Update();
        }


        private void BindGrid(int pageIndex = 1, int pageSize = 10)
        {
            pageIndex = PagingAdminWeb.GetPageIndex();
            List<VaiTro> list = new List<VaiTro>();
            int totalRow = 0;


            list = VaiTroManagerBll.GetPaging(pageSize, pageIndex, Request.QueryString["search"], null, out totalRow);
            SearchUserControl.SetSearcKey();
            int idUser = int.Parse(Session["UserId"].ToString());
            if (idUser != 31)
            {
                list = list.Where(x => x.Id != 1).ToList();
            }

            ViewState["LastIndex"] = (pageIndex - 1) * pageSize;
            PagingAdminWeb.GetPaging(totalRow, pageIndex);
            GridViewTable.DataSource = list;
            GridViewTable.DataBind();
            UpdatePanelTablleMain.Update();
        }
        protected void btnUserAdd_Click(object sender, EventArgs e)
        {
            try
            {
                VaiTro vaiTro = new VaiTro();
                lblAddErrorMessage.Text = "";
                if (string.IsNullOrEmpty(txtTenVaiTro.Text.Trim()) || txtTenVaiTro.Text.Trim().Length <= 1)
                {
                    lblAddErrorMessage.Text += "Tên vai trò không được trống hoặc bé hơn 3 ký tự<br />";
                }

                if (string.IsNullOrEmpty(txtMa.Text.Trim()) || txtMa.Text.Trim().Length <= 1 ||
                    txtMa.Text.Contains(" "))
                {
                    lblAddErrorMessage.Text += "Mã không hợp lệ";
                }

                if (lblAddErrorMessage.Text.Length > 0)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "OpenModal", "openModal();", true);
                    return;
                }
                else
                {
                    if (VaiTroManagerBll.IsExistsVaiTroMa(txtMa.Text) != null)
                    {
                        lblAddErrorMessage.Text = "Mã này đã tồn tại, chọn tên khác ";
                        UpdatePanelAdd.Update();

                        ScriptManager.RegisterStartupScript(this, GetType(), "OpenModal", "openModal();", true);
                    }
                    else
                    {
                        if(!VaiTroManagerBll.AllowAdd(CurrentUserId, MenuMa))
                        {
                            ShowNotification("Bạn không có quyền truy cập chức năng này", false);
                            return;
                        }
                        vaiTro.Ten = txtTenVaiTro.Text;
                        vaiTro.Ma = txtMa.Text;

                        vaiTro.NgayTao = DateTime.Now;
                        vaiTro.TrangThai = true;
                        vaiTro = VaiTroManagerBll.Insert(vaiTro);
                        BindDataByQuyen();

                        ScriptManager.RegisterStartupScript(this.Page, GetType(), "CloseModal", "closeModal();", true);
                        if (vaiTro != null)
                        {
                            lblAddErrorMessage.Text = "";
                            ShowNotification("Thêm mới quyền thành công");
                            txtTenVaiTro.Text = string.Empty;
                            txtMa.Text = string.Empty;
                            UpdatePanelAdd.Update();
                        }
                        else
                        {
                            ShowNotification("Thêm mới quyền thất bại", false);
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                ShowNotification("Thêm mới vai trò thất bại! \n Lỗi: " + ex.Message, false);

            }
        }

        protected void GridViewTable_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                int vaiTroId = Convert.ToInt32(e.CommandArgument);
                var vaiTro = VaiTroManagerBll.GetById(vaiTroId);
                if (vaiTro != null)
                {
                    hdnRowId.Value = vaiTroId.ToString();
                    if (e.CommandName == "phanQuyen")
                    {

                        var roleOfUsers = VaiTroManagerBll.GetAllMenuQuyenCheck(vaiTroId);
                        GridRoleMenuPermission.DataSource = roleOfUsers;
                        GridRoleMenuPermission.DataBind();
                        UpdatePanelRoleMenu.Update();
                        // Đăng ký đoạn mã JavaScript
                        ScriptManager.RegisterStartupScript(this, GetType(), "openMenuEditModal", "openMenuEditModal();", true);
                    }
                    else if (e.CommandName == "ChinhSuaChiTiet")
                    {
                        txtEditTenVaiTro.Text = vaiTro.Ten;
                        txtEditMa.Text = vaiTro.Ma;
                        chkEditTrangThai.Checked = vaiTro.TrangThai ?? false;
                        UpdatePanelEdit.Update();
                        ScriptManager.RegisterStartupScript(this, GetType(), "openEdit", "openEdit();", true);
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




        protected void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                int vaiTroId = int.Parse(hdnRowId.Value);
                hdnRowId.Value = "";
                if (!string.IsNullOrEmpty(vaiTroId.ToString()))
                {
                    var vaiTro = VaiTroManagerBll.GetById(vaiTroId);

                    if (vaiTro == null)
                    {
                        ShowNotification("Lỗi không tìm thấy đối tượng", false);
                        return;
                    }
                    if (!VaiTroManagerBll.AllowDelete(CurrentUserId, MenuMa))
                    {
                        ShowNotification("Bạn không có quyền truy cập chức năng này", false);
                        return;
                    }
                    if (VaiTroManagerBll.Delete(vaiTroId))
                    {
                        BindDataByQuyen();

                        ShowNotification("Đã xóa");

                    }
                    else
                    {
                        ShowNotification("xóa không thành công");
                    }
                    ScriptManager.RegisterStartupScript(this, GetType(), "closeDelete", "closeDelete();", true);

                }
            }
            catch (Exception ex)
            {
                ShowNotification("Xóa thất bại! \n " + ex.Message, false);
            }

        }



        protected void btnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                lblErrorMessager.Text = "";
                if (string.IsNullOrEmpty(txtEditTenVaiTro.Text.Trim()) || txtEditTenVaiTro.Text.Trim().Length <= 1)
                {
                    lblErrorMessager.Text += "Tên vai trò không được trống hoặc bé hơn 1 ký tự<br />";
                }

                if (string.IsNullOrEmpty(txtEditMa.Text.Trim()) || txtEditMa.Text.Trim().Length <= 1 ||
                    txtEditMa.Text.Contains(" "))
                {
                    lblErrorMessager.Text += "Mã không hợp lệ";
                }

                if (lblErrorMessager.Text.Length > 0)
                {
                    return;
                }
                else
                {
                    if (!VaiTroManagerBll.AllowEdit(CurrentUserId, MenuMa))
                    {
                        ShowNotification("Bạn không có quyền truy cập chức năng này", false);
                        return;
                    }
                    int vaiTroId = int.Parse(hdnRowId.Value);
                    VaiTro vaiTro = VaiTroManagerBll.GetById(vaiTroId);

                    if (txtEditMa.Text != vaiTro.Ma)
                    {
                        if (VaiTroManagerBll.IsExistsVaiTroMa(txtEditMa.Text) != null)
                        {
                            lblErrorMessager.Text = "Mã đã tồn tại trong cơ sở dữ liệu";
                            return;
                        }
                    }

                    hdnRowId.Value = "";

                    vaiTro.Ten = txtEditTenVaiTro.Text;
                    vaiTro.Ma = txtEditMa.Text;
                    vaiTro.TrangThai = chkEditTrangThai.Checked;
                    vaiTro = VaiTroManagerBll.Update(vaiTro);
                    BindDataByQuyen();

                    ScriptManager.RegisterStartupScript(this, GetType(), "closeEdit", "closeEdit();", true);
                    ShowNotification("Cập nhật thành công", true);
                }






            }
            catch (Exception ex)
            {
                ShowNotification("Cập nhật thất bại! \n " + ex.Message, false);
            }
        }
        protected void btnMenuEditSave_Click(object sender, EventArgs e)
        {
            try
            {

                List<MenuQuyenCheck> listNewUpdate = new List<MenuQuyenCheck>();
                int vaiTroId = int.Parse(hdnRowId.Value);
                hdnRowId.Value = "";
                foreach (GridViewRow MenuPermissionUpdate in GridRoleMenuPermission.Rows)
                {

                    if (MenuPermissionUpdate != null)
                    {
                        Label lblMenuId = (Label)MenuPermissionUpdate.FindControl("lblMenuId");
                        Label lblPermissionId = (Label)MenuPermissionUpdate.FindControl("lblLoaiQuyenId");
                        CheckBox chkIsHaveRole = (CheckBox)MenuPermissionUpdate.FindControl("CoQuyen");

                        if (!string.IsNullOrEmpty(lblMenuId.Text) && !string.IsNullOrEmpty(lblPermissionId.Text))
                        {
                            MenuQuyenCheck newUpdate = new MenuQuyenCheck()
                            {
                                MenuId = int.Parse(lblMenuId.Text),
                                LoaiQuyenId = int.Parse(lblPermissionId.Text),
                                CoQuyen = chkIsHaveRole.Checked ? 1 : 0
                            };
                            listNewUpdate.Add(newUpdate);

                        }
                        else
                        {
                            ShowNotification("Không gửi được dữ liệu lên máy chủ! ", false);
                            return;
                        }
                    }
                    else
                    {
                        ShowNotification("Không thể kiểm tra dữ liệu trong hàng", false);

                    }
                }

                if (!VaiTroManagerBll.AllowEdit(CurrentUserId, MenuMa))
                {
                    ShowNotification("Bạn không có quyền truy cập chức năng này", false);
                    return;
                }


                int result = VaiTroManagerBll.UpdateAllMenuLoaiQyen(listNewUpdate, vaiTroId);
                if (result > 0)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "closeMenuEditModal", "closeMenuEditModal();", true);
                    ShowNotification("Chỉnh sửa thành công");
                }
                else
                    ShowNotification("Chỉnh sửa không thành công", false);
            }
            catch (Exception ex)
            {
                ShowNotification("Cập nhật quyền cho vai trò thất bại! \n " + ex.Message, false);
            }
        }

        protected void GridRoleMenuPermission_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            for (int rowIndex = GridRoleMenuPermission.Rows.Count - 2; rowIndex >= 0; rowIndex--)
            {
                GridViewRow gvRowCurrent = GridRoleMenuPermission.Rows[rowIndex];
                GridViewRow gvPreviousRow = GridRoleMenuPermission.Rows[rowIndex + 1];
                Label lblMenuName = (Label)gvPreviousRow.FindControl("lblMenuName"); // Thay "lblMenuName" bằng ID của control bạn sử dụng
                string previousMenuName = lblMenuName.Text;

                Label lblMenuNameCurrent = (Label)gvRowCurrent.FindControl("lblMenuName"); // Thay "lblMenuName" bằng ID của control bạn sử dụng
                string MenuNameCurrent = lblMenuNameCurrent.Text;



                if (MenuNameCurrent == previousMenuName)
                {
                    if (gvPreviousRow.Cells[0].RowSpan < 2)
                    {
                        gvRowCurrent.Cells[0].RowSpan = 2;
                    }
                    else
                    {
                        gvRowCurrent.Cells[0].RowSpan =
                                gvPreviousRow.Cells[0].RowSpan + 1;
                    }
                    gvPreviousRow.Cells[0].Visible = false;

                }
            }

        }
        private void ShowNotification(string message, bool isSuccess = true)
        {

            AdminNotificationUserControl.LoadMessage(message, isSuccess);
        }
    }
}