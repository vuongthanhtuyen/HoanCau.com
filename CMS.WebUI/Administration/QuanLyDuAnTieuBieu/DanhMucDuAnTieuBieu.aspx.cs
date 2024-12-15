using CMS.Core.Manager;
using CMS.DataAsscess;
using CMS.WebUI.Administration.Common;
using SweetCMS.Core.Helper;
using SweetCMS.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CMS.WebUI.Administration.QuanLyDuAnTieuBieu
{
    public partial class DanhMucDuAnTieuBieu : AdminPermistion
    {
        public override string MenuMa { get; set; } = "Danh-Muc-bai-du-an-tieu-bieu";
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
        }
        private void BindGrid(int pageIndex = 1, int pageSize = 10)
        {
            pageIndex = PagingAdminWeb.GetPageIndex();
            List<DanhMucDto> list = new List<DanhMucDto>();
            int totalRow = 0;
            list = DanhMucBaiVietBLL.GetPaging(pageSize, pageIndex, Request.QueryString["search"], null, CategoryType.KeyProject, out totalRow);
             

            ViewState["LastIndex"] = (pageIndex - 1) * pageSize;
            PagingAdminWeb.GetPaging(totalRow, pageIndex);
            GridViewTable.DataSource = list;
            GridViewTable.DataBind();
            UpdatePanelMainTable.Update();
        }


        protected void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                DanhMuc danhMuc = new DanhMuc();
                lblAddErrorMessage.Text = "";
                if (string.IsNullOrEmpty(txtTen.Text.Trim()) || txtTen.Text.Trim().Length <= 3)
                {
                    lblAddErrorMessage.Text += "Tên danh mục không được trống hoặc bé hơn 3 ký tự<br />";
                }

                if (string.IsNullOrEmpty(txtMa.Text.Trim()) || txtMa.Text.Trim().Length <= 3 ||
                    txtMa.Text.Contains(" "))
                {
                    lblAddErrorMessage.Text += "Mã không hợp lệ";
                }

                if (lblAddErrorMessage.Text.Length > 0)
                {
                    UpdatePanelAdd.Update();
                    ScriptManager.RegisterStartupScript(this, GetType(), "OpenModal", "openModal();", true);
                    return;
                }
                else

                {
                    if (FriendlyUrlBLL.GetByMa(txtMa.Text) != null)
                    {
                        lblAddErrorMessage.Text = "Url thân thiện đãn tồn tại, nhập url khác";
                        UpdatePanelAdd.Update();
                        ScriptManager.RegisterStartupScript(this, GetType(), "OpenModal", "openModal();", true);
                        return;
                    }
                    if (!VaiTroManagerBll.AllowAdd(ApplicationContext.Current.CurrentUserID, MenuMa))
                    {
                        ShowNotification("Bạn không có quyền truy cập chức năng này", false);
                        return;
                    }
                    else
                    {
                        danhMuc.Ten = txtTen.Text;
                        danhMuc.Slug = txtMa.Text;
                        danhMuc.MoTa = txtMota.Text;
                        danhMuc.Type = CategoryType.KeyProject;
                        danhMuc.CreateDate = DateTime.Now;
                        danhMuc.UpdateDate = DateTime.Now;
                        danhMuc.CreateBy = danhMuc.UpdateBy = NguoiDungManagerBLL.GetById(ApplicationContext.Current.CurrentUserID).TenTruyCap;
                        danhMuc = DanhMucBaiVietBLL.Insert(danhMuc);
                        BindDataByQuyen();
                        ScriptManager.RegisterStartupScript(this, GetType(), "CloseModal", "closeModal();", true);
                        if (danhMuc != null)
                        {
                            lblAddErrorMessage.Text = "";
                            ShowNotification("Thêm danh mục thành công");
                        }
                        else
                        {
                            ShowNotification("Thêm danh mục thất bại", false);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ShowNotification("Thêm danh mục thất bại! \n Lỗi: " + ex.Message, false);
            }
        }
        protected void GridViewTable_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                int danhMucId = Convert.ToInt32(e.CommandArgument);
                var danhMuc = DanhMucBaiVietBLL.GetById(danhMucId);
                if (danhMuc != null)
                {
                    hdnRowId.Value = danhMucId.ToString();
                    if (e.CommandName == "ChinhSuaChiTiet")
                    {
                        List<DanhMuc> listDanhMuc = DanhMucBaiVietBLL.GetNameAndId(ApplicationContext.Current.CurrentLanguageId);
                        txtEditTen.Text = danhMuc.Ten;
                        txtEditMa.Text = danhMuc.Slug;
                        txtEditMota.Text = danhMuc.MoTa;
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
                    ShowNotification("Danh mục này không tồn tại", false);
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
                int id = int.Parse(hdnRowId.Value);
                hdnRowId.Value = "";
                if (!string.IsNullOrEmpty(id.ToString()))
                {
                    var danhMuc = DanhMucBaiVietBLL.GetById(id);

                    if (danhMuc == null)
                    {
                        ShowNotification("Lỗi không tìm thấy danh mục", false);
                        return;
                    }
                    if (!VaiTroManagerBll.AllowDelete(ApplicationContext.Current.CurrentUserID, MenuMa))
                    {
                        ShowNotification("Bạn không có quyền truy cập chức năng này", false);
                        return;
                    }

                    DanhMucBaiVietBLL.Delete(id);
                    BindDataByQuyen();
                    ShowNotification("Đã xóa danh mục");
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
                
                if (!VaiTroManagerBll.AllowEdit(ApplicationContext.Current.CurrentUserID, MenuMa))
                {
                    ShowNotification("Bạn không có quyền truy cập chức năng này", false);
                    return;
                }

                int danhMucId = int.Parse(hdnRowId.Value);
                hdnRowId.Value = "";
                DanhMuc danhMuc = DanhMucBaiVietBLL.GetById(danhMucId);
                if (danhMuc.Slug != txtEditMa.Text) {
                    var friendlyUrl = FriendlyUrlBLL.GetByMa(txtEditMa.Text);
                    if (friendlyUrl != null) {
                        lblEditErrorMessage.Text = "Url này đã tồn tại, vui lòng nhập một url khác";
                        ScriptManager.RegisterStartupScript(this, GetType(), "openEdit", "openEdit();", true);
                        return;
                    }

                }



                danhMuc.Ten = txtEditTen.Text;
                danhMuc.Slug = txtEditMa.Text;
                danhMuc.MoTa = txtEditMota.Text;
                danhMuc = DanhMucBaiVietBLL.Update(danhMuc);
                BindDataByQuyen();
                ScriptManager.RegisterStartupScript(this, GetType(), "closeEdit", "closeEdit();", true);
                ShowNotification("Cập nhật thành công", true);
            }
            catch (Exception ex)
            {
                ShowNotification("Cập nhật thất bại! \n " + ex.Message, false);
            }
        }
    
        
        private void ShowNotification(string message, bool isSuccess = true)
        {
            AdminNotificationUserControl.LoadMessage(message, isSuccess);
        }
    }
}