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

namespace CMS.WebUI.Administration.QuanLyCauHinh
{
    public partial class SlideWeb : AdminPermistion
    {
        public override string MenuMa { get; set; } = "Menu-hien-thi-duoi";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindDataByQuyen();
                AdminNotificationUserControl.Visible = false;
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
            List<TrinhChieuAnh> trinhChieuAnhList = new List<TrinhChieuAnh>();
            int totalRow = 0;
            trinhChieuAnhList = SlideBLL.GetPaging(pageSize, pageIndex, Request.QueryString["search"], true, out totalRow);
             
            PagingAdminWeb.GetPaging(totalRow, pageIndex);
            GridViewTable.DataSource = trinhChieuAnhList;
            GridViewTable.DataBind();
        }

        #region
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                TrinhChieuAnh slide = new TrinhChieuAnh();
                lblAddErrorMessage.Text = "";
                if (string.IsNullOrEmpty(txtTieuDeMot.Text.Trim()) || string.IsNullOrEmpty(txtTieuDeHai.Text.Trim()))
                {
                    lblAddErrorMessage.Text += "Tiêu đề không được để trống <br />";
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
                    slide.NoiDungMot = txtTieuDeMot.Text;
                    slide.NoiDungHai = txtTieuDeHai.Text;
                    slide.LienKetUrl = txtLienKetUrl.Text;
                    slide.TrangThai = true;
                    slide.NgayTao = DateTime.Now;
                    slide.Stt = int.Parse(txtStt.Text);
                    slide.HinhAnhUrl = txtThumbnailUrl.GetStringFileUrl();
                    
                    slide = SlideBLL.Insert(slide);
                    ScriptManager.RegisterStartupScript(this, GetType(), "CloseModal", "closeModal();", true);
                    BindDataByQuyen();
                    //UpdatePanelMainTable.Update();
                    ShowNotification("Lưu Slide thành công");
                    txtTieuDeMot.Text = string.Empty;
                    txtTieuDeHai.Text = string.Empty;
                    txtLienKetUrl.Text = string.Empty;
                    txtStt.Text = string.Empty;
                    //chkEditTrangThai.Checked = false;

                }
            }

            catch (Exception ex)
            {
                ShowNotification("Lưu slide thất bại! \n Lỗi: " + ex.Message, false);

            }
        }
        protected void btnEdit_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    if (!VaiTroManagerBll.AllowEdit(ApplicationContext.Current.CurrentUserID, MenuMa))
            //    {
            //        ShowNotification("Bạn không có quyền truy cập chức năng này", false);
            //        return;
            //    }
            //    int slideId = int.Parse(hdnRowId.Value); // Lấy ID người dùng đã chỉnh sửa
            //    hdnRowId.Value = "";
            //    // Lấy thông tin người dùng từ cơ sở dữ liệu
            //    TrinhChieuAnh slide = SlideBLL.GetById(slideId);
            //    //UpdatePanelEdit.Update();
            //    slide.NoiDungMot = txtEditTieuDeMot.Text; // Cập nhật tên slide từ textbox
            //    slide.NoiDungHai = txtEditTieuDeHai.Text; // Cập nhật tên slide từ textbox
            //    slide.LienKetUrl = txtEditLienKetUrl.Text; // Cập nhật URL liên kết từ textbox
            //    slide.TrangThai = chkEditTrangThai.Checked; // Cập nhật trạng thái từ checkbox
            //    slide.HinhAnhUrl = ImportImageEdit.GetStringFileUrl();
            //    slide.Stt = int.Parse(txtEditStt.Text);
            //    SlideBLL.Update(slide);
            //    BindDataByQuyen();
            //    //UpdatePanelMainTable.Update();
            //    ScriptManager.RegisterStartupScript(this, GetType(), "closeEdit", "closeEdit();", true);

            //    ShowNotification("Cập nhật thành công", true);
            //    txtEditTieuDeMot.Text = string.Empty;
            //    txtEditTieuDeHai.Text = string.Empty;
            //    txtEditLienKetUrl.Text = string.Empty;
            //    txtEditStt.Text = string.Empty;
            //    chkEditTrangThai.Checked = false;
            //}
            //catch (Exception ex)
            //{
            //    ShowNotification("Cập nhật slide thất bại! \n " + ex.Message, false);
            //}
        }
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                int slideId = int.Parse(hdnRowId.Value);
                hdnRowId.Value = "";
                if (!string.IsNullOrEmpty(slideId.ToString()))
                {
                    var slide = SlideBLL.GetById(slideId);

                    if (slide == null)
                    {
                        ShowNotification("Lỗi không tìm thấy slide", false);
                    }
                    if (!VaiTroManagerBll.AllowDelete(ApplicationContext.Current.CurrentUserID, MenuMa))
                    {
                        ShowNotification("Bạn không có quyền truy cập chức năng này", false);
                        return;
                    }

                    SlideBLL.Delete(slideId);
                    BindDataByQuyen();
                    //UpdatePanelMainTable.Update();
                    ShowNotification("Đã xóa slide");
                   
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
                int slideId = Convert.ToInt32(e.CommandArgument);
                var slide = SlideBLL.GetById(slideId);
                if (slide != null)
                {
                    hdnRowId.Value = slideId.ToString();
                    //if (e.CommandName == "ChinhSuaChiTiet")
                    //{                          
                    //    txtEditLienKetUrl.Text = slide.LienKetUrl;
                    //    txtEditTieuDeHai.Text = slide.NoiDungHai;
                    //    txtEditTieuDeMot.Text = slide.NoiDungMot;
                    //    txtEditStt.Text = slide.Stt.ToString();
                    //    txtEditNgayTao.Text = ((DateTime)slide.NgayTao).ToString("yyyy-MM-dd");
                    //    chkEditTrangThai.Checked = slide.TrangThai ?? false;
                    //    ImportImageEdit.SetFileImage(slide.HinhAnhUrl);
                    //    //UpdatePanelMainTable.Update();

                    //    ScriptManager.RegisterStartupScript(this, GetType(), "openEdit", "openEdit();", true);
                    //}
                    //else if (e.CommandName == "Xoa")
                    //{
                    //    ScriptManager.RegisterStartupScript(this, GetType(), "openDelete", "openDelete();", true);
                    //}
                }
                else
                {
                    ShowNotification("slide này không tồn tại", false);
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
            AdminNotificationUserControl.Visible = true;
            //AdminNotificationUserControl.LoadMessage(message, isSuccess);
        }

    }

}