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

namespace CMS.WebUI.Administration.QuanLyDuAnTieuBieu
{
    public partial class BaiVietDuAnTieuBieu : AdminPermistion
    {
        public override string MenuMa { get; set; } = "Danh-Muc-bai-du-an-tieu-bieu";  // Ghi đè và gán giá trị mới
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindDataByQuyen();
                AdminNotificationUserControl.Visible = false;
                
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
                        LinkButton btnThemAlbum = (LinkButton)row.FindControl("AlbumAnh");
                        LinkButton btnDelete = (LinkButton)row.FindControl("Xoa");
                        LinkButton lblChinhSuaDanhMuc = (LinkButton)row.FindControl("ChinhSuaDanhMuc");

                        if (btnEdit != null)
                        {
                            btnEdit.Visible = CheckPermission(MenuMa, Sua);
                            btnThemAlbum.Visible = CheckPermission(MenuMa, Sua);
                            btnDelete.Visible = CheckPermission(MenuMa, Xoa);
                            lblChinhSuaDanhMuc.Enabled = CheckPermission(MenuMa, Sua);
                        }
                    }
                }
            }
        }
        private void BindGrid(int pageIndex = 1, int pageSize = 10)
        {
            pageIndex = PagingAdminWeb.GetPageIndex();
            List<BaiVietDto> postList = new List<BaiVietDto>();
            int totalRow = 0;
            postList = BaiVietBLL.GetPaging(pageSize, pageIndex, Request.QueryString["search"], null, 4, ApplicationContext.Current.ContentCurrentLanguageId, out totalRow);
            SearchUserControl.SetSearcKey();
            ViewState["LastIndex"] = (pageIndex - 1) * pageSize;
            PagingAdminWeb.GetPaging(totalRow, pageIndex);
            GridViewTable.DataSource = postList;
            GridViewTable.DataBind();
        }


        #region Thêm sửa xóa
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                BaiViet baiViet = new BaiViet();
                lblAddErrorMessage.Text = "";
                string noiDungChinh = SummernoteEditor.GetContent();
                if (string.IsNullOrEmpty(txtTieuDe.Text.Trim()) || txtTieuDe.Text.Trim().Length <= 3)
                {
                    lblAddErrorMessage.Text += "Tiêu đề không được để trống <br />";
                }
                if (string.IsNullOrEmpty(noiDungChinh.Trim()) || noiDungChinh.Length <= 3)
                {
                    lblAddErrorMessage.Text += "Nội dung chính <br /> ";
                }
                if (string.IsNullOrEmpty(txtSlug.Text.Trim()) || txtSlug.Text.Trim().Length <= 3 ||
                    txtSlug.Text.Contains(" "))
                {
                    lblAddErrorMessage.Text += "Slug không hợp lệ<br />";
                }
                if (lblAddErrorMessage.Text.Length > 0)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "OpenModal", "openModal();", true);
                    return;
                }
                if (FriendlyUrlBLL.GetByMa(txtSlug.Text) != null)
                {
                    lblAddErrorMessage.Text = "Slug đã tồn tại trong cơ sở dữ liệu";
                    ScriptManager.RegisterStartupScript(this, GetType(), "OpenModal", "OpenModal();", true);
                    return;
                }
                if (!VaiTroManagerBll.AllowAdd(ApplicationContext.Current.CurrentUserID, MenuMa))
                {
                    ShowNotification("Bạn không có quyền truy cập chức năng này", false);
                    return;
                }

                else
                {
                    baiViet.TieuDe = txtTieuDe.Text;
                    baiViet.NoiDungChinh = noiDungChinh;
                    baiViet.Slug = txtSlug.Text;
                    baiViet.ThumbnailUrl = txtThumbnailUrl.GetStringFileUrl();
                    baiViet.MoTaNgan = txtMoTaNgan.GetContent();
                    baiViet.NgayTao = DateTime.Now;
                    baiViet.TacGiaId = int.Parse(Session["UserId"].ToString());
                    baiViet.TrangThai = true;
                    baiViet.ViewCount = 1;
                    baiViet.ChinhSuaGanNhat = DateTime.Now;

                    baiViet = BaiVietBLL.InsertDuAnTieuBieu(baiViet);
                    BindDataByQuyen();
                    ScriptManager.RegisterStartupScript(this, GetType(), "CloseModal", "closeModal();", true);
                    if (baiViet != null)
                    {
                        lblAddErrorMessage.Text = "";
                        ShowNotification("Thêm mới bài viết thành công");


                        txtTieuDe.Text = string.Empty;
                        txtSlug.Text = string.Empty;
                        SummernoteEditor.GetEmpty(); // Nếu SummernoteEditor có thuộc tính tương tự
                        txtMoTaNgan.GetEmpty();



                    }
                    else
                    {
                        ShowNotification("Thêm mới bài viết thất bại", false);
                    }

                }
            }

            catch (Exception ex)
            {
                ShowNotification("Thêm mới bài viết thất bại! \n Lỗi: " + ex.Message, false);

            }
        }
        protected void btnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                lblErrorEditMessage.Text = "";
                if (string.IsNullOrEmpty(txtEditTieuDe.Text.Trim()) || txtEditTieuDe.Text.Trim().Length <= 3)
                {
                    lblErrorEditMessage.Text += "Tiêu đề không được để trống <br />";
                }
                if (string.IsNullOrEmpty(txtEditNoiDungChinh.GetContent().Trim()) || txtEditNoiDungChinh.GetContent().Length <= 3)
                {
                    lblErrorEditMessage.Text += "Nội dung chính không hợp lệ <br /> ";
                }
                if (string.IsNullOrEmpty(txtEditSlug.Text.Trim()) || txtEditSlug.Text.Trim().Length <= 3 ||
                    txtEditSlug.Text.Contains(" "))
                {
                    lblErrorEditMessage.Text += "Slug không hợp lệ<br />";
                }
                if (lblErrorEditMessage.Text.Length > 0)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "openEdit", "openEdit();", true);
                    return;
                }
                if (!VaiTroManagerBll.AllowEdit(ApplicationContext.Current.CurrentUserID, MenuMa))
                {
                    ShowNotification("Bạn không có quyền truy cập chức năng này", false);
                    return;
                }

                int baiVietId = int.Parse(hdnRowId.Value); // Lấy ID người dùng đã chỉnh sửa

                // Lấy thông tin người dùng từ cơ sở dữ liệu
                BaiViet baiViet = BaiVietBLL.GetById(baiVietId);
                if (baiViet.Slug != txtEditSlug.Text)
                {
                    if (FriendlyUrlBLL.GetByMa(txtEditSlug.Text) != null)
                    {
                        lblErrorEditMessage.Text = "Slug đã tồn tại trong cơ sở dữ liệu";
                        ScriptManager.RegisterStartupScript(this, GetType(), "openEdit", "openEdit();", true);
                        return;
                    }
                }



                baiViet.TieuDe = txtEditTieuDe.Text;
                baiViet.NoiDungChinh = txtEditNoiDungChinh.GetContent(); // Giả sử phương thức GetContent() trả về nội dung
                baiViet.Slug = txtEditSlug.Text;
                baiViet.ThumbnailUrl = txtEditThumbnailUrl.GetStringFileUrl();
                baiViet.TrangThai = chkEditTrangThai.Checked;
                baiViet.ChinhSuaGanNhat = DateTime.Now;
                baiViet.MoTaNgan = txtEditMoTaNgan.GetContent();
                hdnRowId.Value = "";
                BaiVietBLL.Update(baiViet);
                BindDataByQuyen();
                ScriptManager.RegisterStartupScript(this, GetType(), "closeEdit", "closeEdit();", true);

                ShowNotification("Cập nhật thành công", true);
            }
            catch (Exception ex)
            {
                ShowNotification("Cập nhật bài viết thất bại! \n " + ex.Message, false);
            }
        }
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                int baiVietId = int.Parse(hdnRowId.Value);
                hdnRowId.Value = "";
                if (!string.IsNullOrEmpty(baiVietId.ToString()))
                {
                    var nguoiDung = BaiVietBLL.GetById(baiVietId);

                    if (nguoiDung == null)
                    {
                        ShowNotification("Lỗi không tìm thấy bài viết", false);
                    }
                    if (!VaiTroManagerBll.AllowDelete(ApplicationContext.Current.CurrentUserID, MenuMa))
                    {
                        ShowNotification("Bạn không có quyền truy cập chức năng này", false);
                        return;
                    }

                    else if (BaiVietBLL.Delete(baiVietId))
                    {
                        ShowNotification("Đã xóa bài viết");
                        BindDataByQuyen();
                    }
                    else
                    {
                        ShowNotification("Xóa bài viết không thành công", false);
                    }
                }

            }
            catch (Exception ex)
            {
                ShowNotification("Xóa thất bại! \n " + ex.Message, false);
            }
        }


        #endregion


        protected void GridViewTable_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                int baiVietId = Convert.ToInt32(e.CommandArgument);
                var baiViet = BaiVietBLL.GetById(baiVietId);
                if (baiViet != null)
                {
                    hdnRowId.Value = baiVietId.ToString();
                    if (e.CommandName == "ChinhSuaDanhMuc")
                    {
                        var danhMuclist = BaiVietBLL.GetAllDanhMucBaiVietById(baiVietId, ApplicationContext.Current.CurrentLanguageId);
                        GridViewDanhMuc.DataSource = danhMuclist;
                        GridViewDanhMuc.DataBind();
                        // Đăng ký đoạn mã JavaScript
                        ScriptManager.RegisterStartupScript(this, GetType(), "openDanhMucEditModal", "openDanhMucEditModal();", true);
                    }
                    else if (e.CommandName == "ChinhSuaChiTiet")
                    {
                        txtEditTieuDe.Text = baiViet.TieuDe;
                        txtEditNoiDungChinh.SetContent(baiViet.NoiDungChinh);
                        txtEditMoTaNgan.SetContent(baiViet.MoTaNgan);
                        txtEditSlug.Text = baiViet.Slug;
                        txtEditThumbnailUrl.SetFileImage(baiViet.ThumbnailUrl);
                        chkEditTrangThai.Checked = baiViet.TrangThai ?? false;
                        txtEditNgayTao.Text = ((DateTime)baiViet.NgayTao).ToString("yyyy-MM-dd");
                        txtEditMoTaNgan.SetContent(baiViet.MoTaNgan);
                        ScriptManager.RegisterStartupScript(this, GetType(), "openEdit", "openEdit();", true);
                    }
                    else if (e.CommandName == "Xoa")
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "openDelete", "openDelete();", true);
                    }
                    else if (e.CommandName == "AlbumAnh")
                    {
                        BindAlbumDuAnTieuBieu(baiVietId);
                        ScriptManager.RegisterStartupScript(this, GetType(), "openUploadAlbum", "openUploadAlbum();", true);
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



        // Thực hiện việc xóa user bằng userId
        private void ShowNotification(string message, bool isSuccess = true)
        {
            AdminNotificationUserControl.LoadMessage(message, isSuccess);
        }

        protected void btnDanhMucEditSave_Click(object sender, EventArgs e)
        {
            try
            {
                int danhMucId = int.Parse(hdnRowId.Value);
                hdnRowId.Value = "";
                List<DanhMucBaiVietDto> listNewUpdate = new List<DanhMucBaiVietDto>();
                foreach (GridViewRow MenuPermissionUpdate in GridViewDanhMuc.Rows)
                {
                    if (MenuPermissionUpdate != null)
                    {
                        Label lblCategoryId = (Label)MenuPermissionUpdate.FindControl("lblDanhMucId");// Lấy hàng thứ 3 (index 2)
                        CheckBox chkIsHaveCategory = (CheckBox)MenuPermissionUpdate.FindControl("chkIsHaveDanhMuc");// Lấy hàng thứ 3 (index 2)

                        if (!string.IsNullOrEmpty(lblCategoryId.Text))
                        {
                            DanhMucBaiVietDto newUpdate = new DanhMucBaiVietDto()

                            {
                                Id = int.Parse(lblCategoryId.Text),
                                IsHaveDanhMuc = chkIsHaveCategory.Checked ? 1 : 0
                            };
                            listNewUpdate.Add(newUpdate);
                        }

                        else
                        {
                            ShowNotification("Không gửi được dữ liệu lên máy chủ ", false);
                            return;
                        }
                    }
                    else
                    {
                        ShowNotification("Không thể kiểm tra dữ liệu trong hàng", false);

                    }
                }
                int result = BaiVietBLL.UpdateCategoryByPostId(listNewUpdate, danhMucId);
                ShowNotification("Cập nhật danh mục thành công");
                BindDataByQuyen();

            }
            catch (Exception ex)
            {

                ShowNotification("Cập nhật danh mục thất bại! \n " + ex.Message, false);
            }

        }



        protected void btnAlbumAddImage_Click(object sender, EventArgs e)
        {
            try
            {
                if (BaiVietBLL.InsertHinhAnhDuAnTieuBieu(DuAnTieuBieuUpLoad.GetStringFileUrl(), int.Parse(hdnRowId.Value)) != null)
                {
                    BindAlbumDuAnTieuBieu(int.Parse(hdnRowId.Value));
                    //UpdatePanel1.Update();
                    ScriptManager.RegisterStartupScript(this, GetType(), "openUploadAlbum", "openUploadAlbum();", true);
                }
            }
            catch (Exception ex)
            {
                ShowNotification("Lỗi khi thêm ảnh: " + ex.Message, false);
            }
        }
        protected void rptAlbum_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "DeleteImage")
                {
                    // Lấy giá trị ID từ CommandArgument
                    int imageId = Convert.ToInt32(e.CommandArgument);

                    // Thực hiện hành động xóa ảnh dựa trên imageId
                    if (BaiVietBLL.DeleteHinhAnhDuAnTieuBieu(imageId))
                    {
                        BindAlbumDuAnTieuBieu(int.Parse(hdnRowId.Value));
                        UpdatePanel1.Update();
                        ScriptManager.RegisterStartupScript(this, GetType(), "openUploadAlbum", "openUploadAlbum();", true);
                    }
                }
            }
            catch (Exception ex)
            {
                ShowNotification("Lỗi khi xóa ảnh: " + ex.Message, false);
            }
        }


        // Không xóa
        private bool DeleteImage(string fileName)
        {
            string FileSaveLocation = @"/Administration/UploadImage/" + fileName;
            FileSaveLocation = Server.MapPath(FileSaveLocation);
            if (System.IO.File.Exists(FileSaveLocation))
            {
                try
                {
                    System.IO.File.Delete(FileSaveLocation);
                    return true;
                }
                catch { }
            }
            return false;
        }
        private void BindAlbumDuAnTieuBieu(int id)
        {
            List<NhomHinhAnh> album = BaiVietBLL.GetHinhAnhByIdDuAnTieuBieu(id);

            rptAlbum.DataSource = album;
            rptAlbum.DataBind();
        }
    }
}