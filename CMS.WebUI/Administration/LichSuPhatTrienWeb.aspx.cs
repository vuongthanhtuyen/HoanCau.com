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
    public partial class LichSuPhatTrienWeb : AdminPermistion
    {
        public override string MenuMa { get; set; } = "Lich-su-phat-trien";  // Ghi đè và gán giá trị mới

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindDataByQuyen();
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

                        if (btnEdit != null)
                        {
                            btnEdit.Visible = CheckPermission(MenuMa, Sua);
                            btnDelete.Visible = CheckPermission(MenuMa, Xoa);
                        }
                    }
                }
            }
            //UpdatePanelTablleMain.Update();
        }
        private void BindGrid(int pageIndex = 1, int pageSize = 10)
        {
            pageIndex = PagingAdminWeb.GetPageIndex();
            List<LichSuPhatTrien> LichSuPhatTrienList = new List<LichSuPhatTrien>();
            int totalRow = 0;
            LichSuPhatTrienList = LichSuPhatTrienBLL.GetPaging(pageSize, pageIndex, Request.QueryString["search"], true, out totalRow);
            ViewState["LastIndex"] = (pageIndex - 1) * pageSize;
            SearchUserControl.SetSearcKey();
            PagingAdminWeb.GetPaging(totalRow, pageIndex);
            GridViewTable.DataSource = LichSuPhatTrienList;
            GridViewTable.DataBind();
        }


        #region
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                LichSuPhatTrien lichSuPhatTrien_ob = new LichSuPhatTrien();
                lblAddErrorMessage.Text = "";
                if (string.IsNullOrEmpty(txtTieuDe.Text.Trim()) || txtTieuDe.Text.Trim().Length <= 3)
                {
                    lblAddErrorMessage.Text += "Tiêu đề không được để trống <br />";
                    ScriptManager.RegisterStartupScript(this, GetType(), "OpenModal", "openModal();", true);
                    return;
                }
                else
                {
                    if (!VaiTroManagerBll.AllowAdd(CurrentUserId, MenuMa))
                    {
                        ShowNotification("Bạn không có quyền truy cập chức năng này", false);
                        return;
                    }
                    lichSuPhatTrien_ob.TieuDe = txtTieuDe.Text;
                    lichSuPhatTrien_ob.Nam = txtNam.Text;
                    lichSuPhatTrien_ob.MoTa = txtMoTa.GetContent();
                    lichSuPhatTrien_ob.TrangThai = true;
                    lichSuPhatTrien_ob.NgayTao = DateTime.Now;
                    lichSuPhatTrien_ob.HinhAnhUrl = txtThumbnailUrl.GetStringFileUrl();

                    lichSuPhatTrien_ob = LichSuPhatTrienBLL.Insert(lichSuPhatTrien_ob);
                    ScriptManager.RegisterStartupScript(this, GetType(), "CloseModal", "closeModal();", true);

                    ShowNotification("Thêm mới thành công");

                }
            }

            catch (Exception ex)
            {
                ShowNotification("Thêm mới thất bại! \n Lỗi: " + ex.Message, false);

            }
        }
        protected void btnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                if (!VaiTroManagerBll.AllowEdit(CurrentUserId, MenuMa))
                {
                    ShowNotification("Bạn không có quyền truy cập chức năng này", false);
                    return;
                }
                int lichSuPhatTrien_obId = int.Parse(hdnRowId.Value); // Lấy ID người dùng đã chỉnh sửa
                hdnRowId.Value = "";
                // Lấy thông tin người dùng từ cơ sở dữ liệu
                LichSuPhatTrien lichSuPhatTrien_ob = LichSuPhatTrienBLL.GetById(lichSuPhatTrien_obId);
                lichSuPhatTrien_ob.TieuDe = txtEditTieuDe.Text; // Cập nhật tên lichSuPhatTrien_ob từ textbox
                lichSuPhatTrien_ob.Nam = txtEditNam.Text; // Cập nhật tên lichSuPhatTrien_ob từ textbox
                lichSuPhatTrien_ob.MoTa = txtEditMoTa.GetContent(); // Cập nhật URL liên kết từ textbox
                lichSuPhatTrien_ob.TrangThai = chkEditTrangThai.Checked; // Cập nhật trạng thái từ checkbox
                lichSuPhatTrien_ob.HinhAnhUrl = ImportImageEdit.GetStringFileUrl();
                LichSuPhatTrienBLL.Update(lichSuPhatTrien_ob);
                ScriptManager.RegisterStartupScript(this, GetType(), "closeEdit", "closeEdit();", true);

                ShowNotification("Cập nhật thành công", true);
            }
            catch (Exception ex)
            {
                ShowNotification("Cập nhật thất bại! \n " + ex.Message, false);
            }
        }
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                int lichSuPhatTrien_obId = int.Parse(hdnRowId.Value);
                hdnRowId.Value = "";
                if (!string.IsNullOrEmpty(lichSuPhatTrien_obId.ToString()))
                {
                    var lichSuPhatTrien_ob = LichSuPhatTrienBLL.GetById(lichSuPhatTrien_obId);

                    if (lichSuPhatTrien_ob == null)
                    {
                        ShowNotification("Lỗi không tìm thấy đối tượng", false);
                    }
                    if (!VaiTroManagerBll.AllowDelete(CurrentUserId, MenuMa))
                    {
                        ShowNotification("Bạn không có quyền truy cập chức năng này", false);
                        return;
                    }


                    LichSuPhatTrienBLL.Delete(lichSuPhatTrien_obId);
                    ShowNotification("Đã xóa");
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
                int lichSuPhatTrien_obId = Convert.ToInt32(e.CommandArgument);
                var lichSuPhatTrien_ob = LichSuPhatTrienBLL.GetById(lichSuPhatTrien_obId);
                if (lichSuPhatTrien_ob != null)
                {
                    hdnRowId.Value = lichSuPhatTrien_obId.ToString();
                    if (e.CommandName == "ChinhSuaChiTiet")
                    {
                        txtEditTieuDe.Text = lichSuPhatTrien_ob.TieuDe;
                        txtEditNam.Text = lichSuPhatTrien_ob.Nam;
                        txtEditMoTa.SetContent(lichSuPhatTrien_ob.MoTa);
                        txtEditNgayTao.Text = ((DateTime)lichSuPhatTrien_ob.NgayTao).ToString("yyyy-MM-dd");
                        chkEditTrangThai.Checked = lichSuPhatTrien_ob.TrangThai ?? false;
                        ImportImageEdit.SetFileImage(lichSuPhatTrien_ob.HinhAnhUrl);
                        ScriptManager.RegisterStartupScript(this, GetType(), "openEdit", "openEdit();", true);
                    }
                    else if (e.CommandName == "Xoa")
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "openDelete", "openDelete();", true);
                    }
                }
                else
                {
                    ShowNotification("lichSuPhatTrien_ob này không tồn tại", false);
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