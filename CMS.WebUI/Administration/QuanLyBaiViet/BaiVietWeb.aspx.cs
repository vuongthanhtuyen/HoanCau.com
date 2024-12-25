using CMS.Core.Manager;
using CMS.DataAsscess;
using CMS.WebUI.Administration.AdminUserControl;
using CMS.WebUI.Administration.Common;
using Microsoft.Ajax.Utilities;
using SweetCMS.Core.Helper;
using SweetCMS.DataAccess;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.DynamicData;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CMS.WebUI.Administration.QuanLyBaiViet
{
    public partial class BaiVietWeb : AdminPermistion
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
                ddlStatus.Items.Add(new ListItem("Mới", ArticleStatusHelper.New.ToString()));
                ddlStatus.Items.Add(new ListItem("Nháp", ArticleStatusHelper.Draft.ToString()));
                ddlStatus.Items.Add(new ListItem("Đã đăng", ArticleStatusHelper.Published.ToString()));
                ddlStatus.Items.Add(new ListItem("Khóa", ArticleStatusHelper.UnPublished.ToString()));
                ddlStatus.Items.Add(new ListItem("Chờ duyệt", ArticleStatusHelper.WaitForApprove.ToString()));
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
            postList = BaiVietBLL.GetPaging(pageSize, pageIndex, Request.QueryString["search"], null, TypeBaiViet.BaiViet, ApplicationContext.Current.ContentCurrentLanguageId, out totalRow);

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

                    var baiViet = BaiVietBLL.GetById(_id);
                    txtTieuDe.Value = baiViet.TieuDe;
                    txtNoiDungChinh.Text = (Server.HtmlDecode(baiViet.NoiDungChinh));
                    txtSlug.Value = baiViet.Slug;
                    imgThumb.Src = Helpers.GetThumbnailUrl(baiViet.ThumbnailUrl);
                    chkTrangThai.Checked = baiViet.TrangThai ?? false;
                    txtInfo.Visible = true;
                    _CreateDate = baiViet.NgayTao.ToString("yyyy-MM-dd");
                    _UpdateDate = baiViet.ChinhSuaGanNhat.ToString("yyyy-MM-dd");
                    txtMoTaNgan.Value = Server.HtmlDecode(baiViet.MoTaNgan);
                    txtViewCount.Value = baiViet.ViewCount.ToString();
                    _ModalTitle = baiViet.TieuDe;
                    _CreateBy = baiViet.CreateBy;
                    _UpdateBy = baiViet.UpdateBy;
                    ddlStatus.SelectedValue = baiViet.Status ?? ArticleStatusHelper.New;
                    txtDisplayOrder.Value = baiViet.DisplayOrder.ToString();
                    txtNgayDangCongKhai.Value = baiViet.NgayDang.ToString("yyyy-MM-dd");
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
                int baiVietId = Convert.ToInt32(e.CommandArgument);
                var baiViet = BaiVietBLL.GetById(baiVietId);
                if (baiViet != null)
                {
                    hdnRowId.Value = baiVietId.ToString();
                    if (e.CommandName == "ChinhSuaDanhMuc")
                    {
                        var danhMuclist = BaiVietBLL.GetAllDanhMucBaiVietById(baiVietId, ApplicationContext.Current.ContentCurrentLanguageId, CategoryType.NhomCoCau);
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
                BaiViet baiViet = new BaiViet();
                if (int.TryParse(txtIdHidden.Value, out id))
                {
                    if (id > 0)
                    {
                        baiViet = BaiVietBLL.GetById(id);
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
                baiViet.TieuDe = txtTieuDe.Value.Trim();
                baiViet.NoiDungChinh = noiDungChinh.Trim();
                baiViet.Slug = txtSlug.Value.Trim();
                if (!string.IsNullOrEmpty(txtImage.Value.Trim()))
                {
                    baiViet.ThumbnailUrl = Helpers.ConvertToSavePath(txtImage.Value.Trim(), true);
                }
                baiViet.Status = string.IsNullOrEmpty(ddlStatus.SelectedValue) ? ArticleStatusHelper.New : ddlStatus.SelectedValue;
                baiViet.MoTaNgan = Server.HtmlEncode(txtMoTaNgan.Value.Trim());
                baiViet.ViewCount = int.Parse(txtViewCount.Value);
                baiViet.TrangThai = chkTrangThai.Checked;
                baiViet.DisplayOrder = int.Parse(txtDisplayOrder.Value.Trim());
                DateTime ngayDang = DateTime.Now;
                DateTime.TryParse(txtNgayDangCongKhai.Value, out ngayDang);
                baiViet.NgayDang = ngayDang;
                baiViet.UpdateBy = NguoiDungManagerBLL.GetById(ApplicationContext.Current.CurrentUserID).TenTruyCap ?? string.Empty;
                if (isAdd)
                {
                    baiViet.TypeBaiViet = TypeBaiViet.BaiViet;
                    baiViet.CreateBy = NguoiDungManagerBLL.GetById(ApplicationContext.Current.CurrentUserID).TenTruyCap ?? string.Empty;
                    baiViet.NgayTao = DateTime.Now;
                    baiViet.TacGiaId = ApplicationContext.Current.CurrentUserID;
                    baiViet.ChinhSuaGanNhat = DateTime.Now;
                    baiViet.LangID = ApplicationContext.Current.ContentCurrentLanguageId;
                    baiViet = BaiVietBLL.Insert(baiViet);
                    LichSuHeThongBLL.LogAction(LichSuHeThongType.INSERT, LichSuHeThongGroup.QuanLyBaiViet, baiViet.TieuDe);
                }
                else
                {
                    baiViet.ChinhSuaGanNhat = DateTime.Now;
                    baiViet = BaiVietBLL.Update(baiViet);
                    hdnRowId.Value = "0";
                    LichSuHeThongBLL.LogAction(LichSuHeThongType.UPDATE, LichSuHeThongGroup.QuanLyBaiViet, baiViet.TieuDe);
                }

                if (baiViet != null)
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
                int baiVietId = 0;

                if (int.TryParse(hdnRowId.Value, out baiVietId))
                {

                    if (baiVietId > 0)
                    {
                        hdnRowId.Value = "";
                        var obj = BaiVietBLL.GetById(baiVietId);

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
                        if (BaiVietBLL.Delete(baiVietId))
                        {
                            BindDataByQuyen();
                            LichSuHeThongBLL.LogAction(LichSuHeThongType.DELETE, LichSuHeThongGroup.QuanLyBaiViet, obj.TieuDe);
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
                //LichSuHeThongBLL.LogAction(LichSuHeThongType.INSERT, LichSuHeThongGroup.QuanLyBaiViet, .TieuDe);
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