using CMS.Core.Manager;
using CMS.DataAsscess;
using CMS.WebUI.Administration.AdminUserControl;
using CMS.WebUI.Administration.Common;
using SweetCMS.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CMS.WebUI.Administration
{
    public partial class LienHeWeb : AdminPermistion
    {
        public override string MenuMa { get; set; } = "Lien-He";  // Ghi đè và gán giá trị mới
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

                        if (btnEdit != null)
                        {
                            btnEdit.Visible = CheckPermission(MenuMa, Sua);
                            btnDelete.Visible = CheckPermission(MenuMa, Xoa);
                        }
                    }
                }
            }
            UpdatePanelMainTable.Update();
        }
        private void BindGrid(int pageIndex = 1, int pageSize = 10)
        {
            pageIndex = PagingAdminWeb.GetPageIndex();
            List<LienHe> LienHeList = new List<LienHe>();
            int totalRow = 0;
            LienHeList = LienHeBLL.GetPaging(pageSize, pageIndex, Request.QueryString["search"], true, out totalRow);
            SearchUserControl.SetSearcKey();

            ViewState["LastIndex"] = (pageIndex - 1) * pageSize;
            PagingAdminWeb.GetPaging(totalRow, pageIndex);
            GridViewTable.DataSource = LienHeList;
            GridViewTable.DataBind();
            UpdatePanelMainTable.Update();
        }

        #region
       
        protected void btnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                if (!VaiTroManagerBll.AllowEdit(CurrentUserId, MenuMa))
                {
                    ShowNotification("Bạn không có quyền truy cập chức năng này", false);
                    return;
                }
                int lienHeId = int.Parse(hdnRowId.Value); // Lấy ID người dùng đã chỉnh sửa
                hdnRowId.Value = "";
                // Lấy thông tin người dùng từ cơ sở dữ liệu
                LienHe lienHe = LienHeBLL.GetById(lienHeId);
                lienHe.HoVaTen = txtEditHoVaTen.Text;
                lienHe.Email = txtEditEmail.Text;
                lienHe.SoDienThoai = txtEditSoDienThoai.Text;
                lienHe.ChuDe = txtEditChuDe.Text;
                lienHe.TinNhan = txtEditNoiDung.Text;
                lienHe.NgayTao = !string.IsNullOrEmpty(txtEditNgayTao.Text) ? DateTime.Parse(txtEditNgayTao.Text) : (DateTime?)null;
                if (chkEditTrangThai.Checked)
                {
                    lienHe.NgayTraLoi = DateTime.Now;
                }
                lienHe.DaTraLoi = chkEditTrangThai.Checked;
                LienHeBLL.Update(lienHe);
                BindDataByQuyen();
                ScriptManager.RegisterStartupScript(this, GetType(), "closeEdit", "closeEdit();", true);
                ShowNotification("Cập nhật thành công", true);
            }
            catch (Exception ex)
            {
                ShowNotification("Cập nhật lienHe thất bại! \n " + ex.Message, false);
            }
        }
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                int lienHeId = int.Parse(hdnRowId.Value);
                hdnRowId.Value = "";
                if (!string.IsNullOrEmpty(lienHeId.ToString()))
                {
                    var lienHe = LienHeBLL.GetById(lienHeId);

                    if (lienHe == null)
                    {
                        ShowNotification("Lỗi không tìm thấy lienHe", false);
                    }
                    if (!VaiTroManagerBll.AllowDelete(CurrentUserId, MenuMa))
                    {
                        ShowNotification("Bạn không có quyền truy cập chức năng này", false);
                        return;
                    }

                    LienHeBLL.Delete(lienHeId);
                    ShowNotification("Đã xóa lien He");
                    BindDataByQuyen();
                    ScriptManager.RegisterStartupScript(this, GetType(), "closeDelete", "closeDelete();", true);
                }
            }
            catch (Exception ex)
            {
                ShowNotification("Xóa thất bại! \n " + ex.Message, false);
            }
        }

        protected void GridViewTable_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                int lienHeId = Convert.ToInt32(e.CommandArgument);
                var lienHe = LienHeBLL.GetById(lienHeId);
                if (lienHe != null)
                {
                    hdnRowId.Value = lienHeId.ToString();
                    if (e.CommandName == "ChinhSuaChiTiet")
                    {
                        txtEditHoVaTen.Text = lienHe.HoVaTen;
                        txtEditEmail.Text = lienHe.Email;
                        txtEditSoDienThoai.Text = lienHe.SoDienThoai;
                        txtEditChuDe.Text = lienHe.ChuDe;
                        txtEditNoiDung.Text = lienHe.TinNhan;

                        // Với trường ngày tháng, kiểm tra nếu có giá trị thì gán, ngược lại để trống
                        txtEditNgayTao.Text = lienHe.NgayTao.HasValue ? lienHe.NgayTao.Value.ToString("yyyy-MM-dd") : string.Empty;
                        chkEditTrangThai.Checked = lienHe.DaTraLoi.HasValue && lienHe.DaTraLoi.Value;
                        txtEditNgayTraLoi.Text = lienHe.NgayTraLoi.HasValue ? lienHe.NgayTraLoi.Value.ToString("yyyy-MM-dd") : string.Empty;
                        UpdatePaneEdit.Update();
                        ScriptManager.RegisterStartupScript(this, GetType(), "openEdit", "openEdit();", true);
                    }
                    else if (e.CommandName == "Xoa")
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "openDelete", "openDelete();", true);
                    }
                }
                else
                {
                    ShowNotification("lienHe này không tồn tại", false);
                }
            }
            catch (Exception ex)
            {
                ShowNotification(ex.Message, false);
            }

        }
        #endregion

        // Thực hiện việc xóa user bằng userId
        private void ShowNotification(string message, bool isSuccess = true)
        {
            AdminNotificationUserControl.LoadMessage(message, isSuccess);
        }
    }
}