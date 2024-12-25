using CMS.Core.Manager;
using CMS.DataAsscess;
using CMS.WebUI.Administration.AdminUserControl;
using CMS.WebUI.Administration.Common;
using Microsoft.Ajax.Utilities;
using SweetCMS.Core.Helper;
using SweetCMS.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using static CMS.WebUI.Common.BaseAdminPage;

namespace CMS.WebUI.Administration
{
    public partial class ThanhVien : AdminPermistion
    {
        public override string MenuMa { get; set; } = "Danh-sach-bai-viet";
        public static string _ModalTitle = string.Empty;
        public static string _CreateDate = string.Empty;
        public static string _UpdateDate = string.Empty;
        public static string _CreateBy = string.Empty;
        public static string _UpdateBy = string.Empty;

        //private const int idCurrentUser = 31;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindDataByQuyen();
                BindStatus();
                //AdminNotificationUserControl.Visible = false;
                if (!IsAlive()) Response.Redirect("/Administration/Login.aspx");

            }
        }
        private void BindStatus()
        {
            try
            {
                ddlStatus.Items.Clear();
                ddlStatus.Items.Add(new ListItem("Tất cả", string.Empty));
                ddlStatus.Items.Add(new ListItem("Hoạt động", BasicStatusHelper.Active.ToString()));
                ddlStatus.Items.Add(new ListItem("Khóa", BasicStatusHelper.InActive.ToString()));
                BindParent();
            }
            catch (Exception exc)
            {

                //ProcessException(exc);
            }
        }
        private void BindParent()
        {
            try
            {
                ddlParent.Items.Clear();
                ddlParent.Items.Add(new ListItem("Không", string.Empty));
               List<DanhMuc> danhMucs =   DanhMucBaiVietBLL.GetAllDanhMucByType(CategoryType.NhomCoCau);
                if (danhMucs != null && danhMucs.Count > 0)
                {
                    foreach (var itemCat in danhMucs)
                    {
                        ListItem item = new ListItem(itemCat.Ten, itemCat.Id.ToString());
                        ddlParent.Items.Add(item);

                    }
                    //ddlQuestionGroup.DataSource = ddlSearchCategory;
                }
            }
            catch (Exception exc)
            {

                //ProcessException(exc);
            }
        }
        private void BindDataByQuyen()
        {
            //btnOpenModal.Visible = CheckPermission(MenuMa, Them);
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
                        LinkButton lblChinhSuaDanhMuc = (LinkButton)row.FindControl("ChinhSuaDanhMuc");

                        if (btnEdit != null)
                        {
                            btnEdit.Visible = CheckPermission(MenuMa, Sua);
                            btnDelete.Visible = CheckPermission(MenuMa, Xoa);
                            lblChinhSuaDanhMuc.Enabled = CheckPermission(MenuMa, Sua);
                        }
                    }
                }
            }
            UpdatePanelMainTable.Update();
        }

        private void BindGrid(int pageIndex = 1, int pageSize = 10)
        {
            pageIndex = PagingAdminWeb.GetPageIndex();
            List<BaiVietDto> postList = new List<BaiVietDto>();
            int totalRow = 0;
            postList = BaiVietBLL.GetPaging(pageSize, pageIndex, Request.QueryString["search"], null, TypeBaiViet.ThanhVien, ApplicationContext.Current.ContentCurrentLanguageId, out totalRow);

            ViewState["LastIndex"] = (pageIndex - 1) * pageSize;
            PagingAdminWeb.GetPaging(totalRow, pageIndex);
            GridViewTable.DataSource = postList;
            GridViewTable.DataBind();
            UpdatePanelMainTable.Update();

        }
        protected void btnRefresh_ServerClick(object sender, EventArgs e)
        {
            int _id = 0;

            try
            {
                if (int.TryParse(txtIdHidden.Value, out _id))
                {

                    var objThanhVien = BaiVietBLL.GetById(_id);
                    txtTieuDe.Value = objThanhVien.TieuDe;
                    txtNoiDungChinh.Text = (Server.HtmlDecode(objThanhVien.NoiDungChinh));
                    txtSlug.Value = objThanhVien.Slug;
                    imgThumb.Src = Helpers.GetThumbnailUrl(objThanhVien.ThumbnailUrl);
                    chkTrangThai.Checked = objThanhVien.TrangThai ?? false;
                    txtInfo.Visible = true;
                    _CreateDate = objThanhVien.NgayTao.ToString("yyyy-MM-dd");
                    _UpdateDate = objThanhVien.ChinhSuaGanNhat.ToString("yyyy-MM-dd");
                    txtMoTaNgan.Value = Server.HtmlDecode(objThanhVien.MoTaNgan);
                    txtViewCount.Value = objThanhVien.ViewCount.ToString();
                    _ModalTitle = objThanhVien.TieuDe;
                    _CreateBy = objThanhVien.CreateBy;
                    _UpdateBy = objThanhVien.UpdateBy;
                    ddlStatus.SelectedValue = objThanhVien.Status ?? ArticleStatusHelper.New;
                    txtDisplayOrder.Value = objThanhVien.DisplayOrder.ToString();
                    txtNgayDangCongKhai.Value = objThanhVien.NgayDang.ToString("yyyy-MM-dd");
                    var objNhom = NhomCoCauBLL.GetByIdBaiViet(objThanhVien.Id);
                    if(objNhom != null)
                    {
                        ddlParent.SelectedValue = objNhom.DanhmucId.ToString();
                    }
                    else
                    {
                        ddlParent.SelectedValue = string.Empty;
                    }
                }
                else
                {
                    txtInfo.Visible = false;
                    _ModalTitle = "Thêm mới";
                    _CreateBy = _UpdateBy = _CreateDate = _UpdateDate = string.Empty;
                    ddlStatus.SelectedValue = string.Empty;
                    txtTieuDe.Value = txtSlug.Value = txtMoTaNgan.Value = txtImage.Value = txtNoiDungChinh.Text = string.Empty;
                    imgThumb.Attributes["src"] = "../UploadImage/addNewImage.png"; // Reset hình ảnh
                    txtViewCount.Value = txtDisplayOrder.Value = "0";
                    chkTrangThai.Checked = true; // Đặt trạng thái mặc định là checked
                    txtNgayDangCongKhai.Value = DateTime.Now.ToString("yyyy-MM-dd");

                    ddlParent.SelectedValue = string.Empty;
                }
                lblModalTitle.InnerText = _ModalTitle;
                UpdatePanelModal.Update();
            }
            catch (Exception ex)
            {
                OpenMessageBox(MessageBoxType.Error, ex.Message);
            }
        }
        protected void GridViewTable_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                int objThanhVienId = Convert.ToInt32(e.CommandArgument);
                var objThanhVien = BaiVietBLL.GetById(objThanhVienId);
                if (objThanhVien != null)
                {
                    hdnRowId.Value = objThanhVienId.ToString();
                    if (e.CommandName == "ChinhSuaDanhMuc")
                    {
                        var danhMuclist = BaiVietBLL.GetAllDanhMucBaiVietById(objThanhVienId, ApplicationContext.Current.ContentCurrentLanguageId, CategoryType.Article);
                        GridViewDanhMuc.DataSource = danhMuclist;
                        GridViewDanhMuc.DataBind();
                        UpdatepanelEidtRole.Update();
                        ScriptManager.RegisterStartupScript(this, GetType(), "openDanhMucEditModal", "openDanhMucEditModal();", true);
                    }
                    else if (e.CommandName == "Xoa")
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "openDelete", "openDelete();", true);
                    }
                }
                else
                {
                    OpenMessageBox(MessageBoxType.Error, MessageBoxString.Error);
                }
            }
            catch (Exception ex)
            {
                OpenMessageBox(MessageBoxType.Error, MessageBoxString.Error);
            }

        }
        protected void btnSendServer_Click(object sender, EventArgs e)
        {
            try
            {
                int id = 0;
                bool isAdd = true;
                BaiViet objThanhVien = new BaiViet();
                if (int.TryParse(txtIdHidden.Value, out id))
                {
                    if (id > 0)
                    {
                        objThanhVien = BaiVietBLL.GetById(id);
                        isAdd = false;
                    }
                }
                string noiDungChinh = Server.HtmlEncode(txtNoiDungChinh.Text);
                if (string.IsNullOrEmpty(txtTieuDe.Value.Trim()) || txtTieuDe.Value.Trim().Length <= 3)
                {
                    AddErrorPrompt(txtTieuDe.ClientID, "Không được bỏ trống trường này");
                }

                if (string.IsNullOrEmpty(txtSlug.Value.Trim()) || txtSlug.Value.Trim().Length <= 3 ||
                    txtSlug.Value.Contains(" "))
                {
                    AddErrorPrompt(txtSlug.ClientID, "Không được bỏ trống trường này");
                }
                else if (FriendlyUrlBLL.CheckExists(txtSlug.Value.Trim(), id))
                {
                    AddErrorPrompt(txtSlug.ClientID, "Url này đã được sử dụng trong hệ thống");
                    txtSlug.Focus();
                }
                if (!IsValid)
                {
                    ShowErrorPrompt();
                    return;
                }
                if (isAdd)
                {
                    if (!VaiTroManagerBll.AllowAdd(ApplicationContext.Current.CurrentUserID, MenuMa))
                    {
                        OpenMessageBox(MessageBoxType.Error, MessageBoxString.ErrorPermission);
                        return;
                    }
                }
                else
                {
                    if (!VaiTroManagerBll.AllowEdit(ApplicationContext.Current.CurrentUserID, MenuMa))
                    {
                        OpenMessageBox(MessageBoxType.Error, MessageBoxString.ErrorPermission);
                        return;
                    }
                }
                objThanhVien.TieuDe = txtTieuDe.Value.Trim();
                objThanhVien.NoiDungChinh = noiDungChinh.Trim();
                objThanhVien.Slug = txtSlug.Value.Trim();
                if (!string.IsNullOrEmpty(txtImage.Value.Trim()))
                {
                    objThanhVien.ThumbnailUrl = Helpers.ConvertToSavePath(txtImage.Value.Trim(), true);
                }
                objThanhVien.Status = string.IsNullOrEmpty(ddlStatus.SelectedValue) ? BasicStatusHelper.Active : ddlStatus.SelectedValue;
                objThanhVien.MoTaNgan = Server.HtmlEncode(txtMoTaNgan.Value.Trim());
                objThanhVien.ViewCount = int.Parse(txtViewCount.Value);
                objThanhVien.TrangThai = chkTrangThai.Checked;
                objThanhVien.DisplayOrder = int.Parse(txtDisplayOrder.Value.Trim());
                DateTime ngayDang = DateTime.Now;
                //DateTime.TryParse(txtNgayDangCongKhai.Value, out ngayDang);
                objThanhVien.NgayDang = ngayDang;
                objThanhVien.ChinhSuaGanNhat = DateTime.Now;
                objThanhVien.UpdateBy = NguoiDungManagerBLL.GetById(ApplicationContext.Current.CurrentUserID).TenTruyCap ?? string.Empty;
                int _idParent = 0;
                int.TryParse(ddlParent.SelectedValue, out _idParent);
                if (isAdd)
                {
                    objThanhVien.TypeBaiViet = TypeBaiViet.ThanhVien;
                    objThanhVien.CreateBy = NguoiDungManagerBLL.GetById(ApplicationContext.Current.CurrentUserID).TenTruyCap ?? string.Empty;
                    objThanhVien.NgayTao = DateTime.Now;
                    objThanhVien.TacGiaId = ApplicationContext.Current.CurrentUserID;
                    objThanhVien.LangID = ApplicationContext.Current.ContentCurrentLanguageId;
                    objThanhVien = NhomCoCauBLL.InsertThanhVien(objThanhVien);
                   
                    LichSuHeThongBLL.LogAction(LichSuHeThongType.INSERT, LichSuHeThongGroup.QuanLyThanhVien, objThanhVien.TieuDe);
                }
                else
                {

                    
                    objThanhVien.ChinhSuaGanNhat = DateTime.Now;
                    objThanhVien = NhomCoCauBLL.UpdateThanhVien(objThanhVien);
                    hdnRowId.Value = "0";
                    LichSuHeThongBLL.LogAction(LichSuHeThongType.UPDATE, LichSuHeThongGroup.QuanLyThanhVien, objThanhVien.TieuDe);
                }


                #region Nhóm cơ cấu
                var objNhom = NhomCoCauBLL.GetByIdBaiViet(objThanhVien.Id);
                if(objNhom == null)
                {
                    if (_idParent > 0)
                    {
                        NhomCoCauBLL.InsertNhomBaiViet(_idParent, objThanhVien.Id);
                    }
                }
                else
                {
                    if (_idParent > 0) {
                        objNhom.DanhmucId = _idParent;
                        NhomCoCauBLL.UpdateNhomBaiViet(objNhom);
                    }
                    else
                    {
                        NhomCoCauBLL.DeleteNhomBaiViet(objThanhVien.Id);
                    }
                }


                #region Nhóm cơ cấu
               
                #endregion
                #endregion

                if (objThanhVien != null)
                {
                    BindDataByQuyen();
                    OpenMessageBox(MessageBoxType.Success, MessageBoxString.Success);

                }
                else
                {
                    OpenMessageBox(MessageBoxType.Error, MessageBoxString.Error);
                }
            }

            catch (Exception ex)
            {


                OpenMessageBox(MessageBoxType.Error, MessageBoxString.Error);

            }
        }
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                int objThanhVienId = 0;

                if (int.TryParse(hdnRowId.Value, out objThanhVienId))
                {

                    if (objThanhVienId > 0)
                    {
                        hdnRowId.Value = "";
                        var obj = BaiVietBLL.GetById(objThanhVienId);

                        if (obj == null)
                        {
                            OpenMessageBox(MessageBoxType.Error, MessageBoxString.Error);
                            return;
                        }
                        if (!VaiTroManagerBll.AllowDelete(ApplicationContext.Current.CurrentUserID, MenuMa))
                        {
                            OpenMessageBox(MessageBoxType.Error, MessageBoxString.ErrorPermission);
                            return;
                        }
                        if (NhomCoCauBLL.DeleteThanhVien(objThanhVienId))
                        {
                            BindDataByQuyen();
                            LichSuHeThongBLL.LogAction(LichSuHeThongType.DELETE, LichSuHeThongGroup.QuanLyThanhVien, obj.TieuDe);
                            OpenMessageBox(MessageBoxType.Success, MessageBoxString.SuccessDelete);
                        }
                        else
                        {
                            OpenMessageBox(MessageBoxType.Error, MessageBoxString.Error);
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                OpenMessageBox(MessageBoxType.Error, MessageBoxString.Error);
            }
        }


        protected void btnDanhMucEditSave_Click(object sender, EventArgs e)
        {
            try
            {
                int danhMucId = int.Parse(hdnRowId.Value);
                hdnRowId.Value = "";
                if (!VaiTroManagerBll.AllowEdit(ApplicationContext.Current.CurrentUserID, MenuMa))
                {
                    OpenMessageBox(MessageBoxType.Error, MessageBoxString.ErrorPermission);
                    return;
                }

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
                            OpenMessageBox(MessageBoxType.Error, MessageBoxString.Error);
                            return;
                        }
                    }
                    else
                    {
                        OpenMessageBox(MessageBoxType.Error, MessageBoxString.Error);

                    }
                }

                int result = BaiVietBLL.UpdateCategoryByPostId(listNewUpdate, danhMucId);
                //LichSuHeThongBLL.LogAction(LichSuHeThongType.INSERT, LichSuHeThongGroup.QuanLyThanhVien, .TieuDe);
                BindDataByQuyen();
                OpenMessageBox(MessageBoxType.Success, MessageBoxString.Success);

            }
            catch (Exception ex)
            {
                OpenMessageBox(MessageBoxType.Error, MessageBoxString.Error);
            }

        }

    }
}