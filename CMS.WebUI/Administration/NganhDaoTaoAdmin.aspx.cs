using CMS.Core.Manager;
using CMS.DataAsscess;
using CMS.WebUI.Administration.AdminUserControl;
using CMS.WebUI.Administration.Common;
using Microsoft.Ajax.Utilities;
using Newtonsoft.Json;
using TBDCMS.Core.Helper;
using TBDCMS.DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CMS.WebUI.Administration
{
    public partial class NganhDaoTaoAdmin : AdminPermistion
    {
        public override string MenuMa { get; set; } = "Danh-sach-bai-viet";
        public static string _ModalTitle = string.Empty;
        public static string _CreateDate = string.Empty;
        public static string _UpdateDate = string.Empty;
        public static string _CreateBy = string.Empty;
        public static string _UpdateBy = string.Empty;
        private static List<ItemFile> itemAttachments;

        //private const int idCurrentUser = 31;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindDataByQuyen();
                BindStatus();
                BindParentDDL();
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
            DataTable dt =   NganhDaoTaoBLL.GetPaging(pageSize, pageIndex, Request.QueryString["search"], null, out totalRow);

            if (dt == null || dt.Rows.Count == 0)
            {
                GridViewTable.DataSource = null;
                GridViewTable.DataBind();
                //custlinkPager.Visible = false;
            }
            else
            {
              
                //GridViewTable.VirtualItemCount = totalRows;
                GridViewTable.DataSource = dt;
                GridViewTable.DataBind();
                //GridViewTable.PageIndex = CurrentPageIndex;
                //custlinkPager.PageSize = GridViewTable.PageSize;
                //custlinkPager.TotalItems = totalRows;
                //custlinkPager.InitLoad();
            }
            //upMain.Update();

            ViewState["LastIndex"] = (pageIndex - 1) * pageSize;
            PagingAdminWeb.GetPaging(totalRow, pageIndex);
            //GridViewTable.DataSource = postList;
            GridViewTable.DataBind();
            UpdatePanelMainTable.Update();

        }

        private void BindParentDDL()
        {
            ddlSearchCategory.Items.Clear();
            ddlSearchCategory.Items.Add(new ListItem("Tất cả", "0"));
            BindCateogryData(0, string.Empty);
        }
        private void BindCateogryData(int parentId, string prefix)
        {
            List<DanhMuc> lstCats = DanhMucBaiVietBLL.GetAllNoPaging(ApplicationContext.Current.ContentCurrentLanguageId, CategoryType.ChuongTrinhDaoTao, null);

            if (lstCats != null && lstCats.Count > 0)
            {
                foreach (var itemCat in lstCats)
                {
                    ListItem item = new ListItem( itemCat.Ten, itemCat.Id.ToString());
                    ddlSearchCategory.Items.Add(item);
                    
                }
                //ddlQuestionGroup.DataSource = ddlSearchCategory;
            }
        }

        protected void btnRefresh_ServerClick(object sender, EventArgs e)
        {
            int _id = 0;

            try
            {
                if (int.TryParse(txtIdHidden.Value, out _id))
                {

                    var objNganhDaoTao = NganhDaoTaoBLL.GetById(_id);

                    txtMaNganh.Value = objNganhDaoTao.MaNganh;
                    txtTieuDe.Value = objNganhDaoTao.TenNganh;
                    txtSoNamDaoTao.Value = objNganhDaoTao.SoNamDaoTao;
                    txtSoTinChi.Value = objNganhDaoTao.SoTinChi.ToString();
                    txtMoTaNgan.Value = objNganhDaoTao.MoTaNgan;
                    txtNoiDungChinh.Text = (Server.HtmlDecode(objNganhDaoTao.NoiDung));
                    ddlSearchCategory.SelectedValue = objNganhDaoTao.NhomNganh.ToString();
                    txtDieuKienNhapHoc.Value = objNganhDaoTao.DieuKienNhapHoc;
                    txtHocPhi.Value = objNganhDaoTao.HocPhi; // Định dạng số thập phân
                    txtSlug.Value = objNganhDaoTao.Slug;
                    txtDisplayOrder.Value = objNganhDaoTao.DisplayOrder.ToString();
                    txtViewCount.Value = objNganhDaoTao.ViewCount.ToString();
                    imgThumb.Src = Helpers.GetThumbnailUrl(objNganhDaoTao.ThumbnailUrl);
                    txtInfo.Visible = true;
                    _CreateDate = objNganhDaoTao.CreateDate.ToString("dd/MM/yyyy");
                    _UpdateDate = objNganhDaoTao.UpdateDate.ToString("dd/MM/yyyy");
                    txtMoTaNgan.Value = Server.HtmlDecode(objNganhDaoTao.MoTaNgan);
                    txtViewCount.Value = objNganhDaoTao.ViewCount.ToString();
                    _ModalTitle = objNganhDaoTao.TenNganh;
                    _CreateBy = objNganhDaoTao.CreateBy;
                    _UpdateBy = objNganhDaoTao.UpdateBy;
                    ddlStatus.SelectedValue = objNganhDaoTao.Status ?? ArticleStatusHelper.New;
                    
                    itemAttachments = FileAttactmentBLL.GetAllByTypeAndCateId(FileAttactmentType.NganhDaoTao, objNganhDaoTao.Id);
                    ltrFileUpload.Text = GetFileUpload(itemAttachments);
                }
                else
                {
                    txtMaNganh.Value = txtTieuDe.Value = txtMoTaNgan.Value = txtNoiDungChinh.Text = txtSlug.Value = string.Empty;
                    txtSoNamDaoTao.Value = txtSoTinChi.Value = "1";
                    ddlSearchCategory.SelectedValue = txtViewCount.Value = "0";
                    txtDisplayOrder.Value = "-1";
                   
                    imgThumb.Src = string.Empty;
                    txtInfo.Visible = false;
                    _CreateDate = _UpdateDate = _CreateBy = _UpdateBy = string.Empty;
                    _ModalTitle = "Thêm mới";
                    ddlStatus.SelectedValue = string.Empty;
                    imgThumb.Attributes["src"] = "../UploadImage/addNewImage.png"; // Reset hình ảnh
                    txtViewCount.Value = txtDisplayOrder.Value = "0";
                }
                lblModalTitle.InnerText = _ModalTitle;
                UpdatePanelModal.Update();
            }
            catch (Exception ex)
            {
                OpenMessageBox(MessageBoxType.Error, ex.Message);
            }
        }
        private string GetFileUpload(List<ItemFile> listFileExtends, bool isPostBack = false)
        {
            StringBuilder fileUpload = new StringBuilder();
            if (!isPostBack)
            {
                foreach (var item in listFileExtends)
                {
                    fileUpload.AppendFormat(templateFileUpload.InnerHtml, item.Id, item.Title, item.FileUrl);
                }
            }
            else
            {

                foreach (var item in listFileExtends)
                {
                    fileUpload.AppendFormat(templateFileUpload.InnerHtml, item.AttachmentFileIdString, item.Title, item.FileUrl);

                }
            }
            return fileUpload.ToString();
        }
        protected void GridViewTable_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                int objNganhDaoTaoId = Convert.ToInt32(e.CommandArgument);
                var objNganhDaoTao = NganhDaoTaoBLL.GetById(objNganhDaoTaoId);
                if (objNganhDaoTao != null)
                {
                    hdnRowId.Value = objNganhDaoTaoId.ToString();
                    if (e.CommandName == "ChinhSuaDanhMuc")
                    {
                        var danhMuclist = BaiVietBLL.GetAllDanhMucBaiVietById(objNganhDaoTaoId, ApplicationContext.Current.ContentCurrentLanguageId, CategoryType.Article);
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
                NganhDaoTao objNganhDaoTao = new NganhDaoTao();
                if (int.TryParse(txtIdHidden.Value, out id))
                {
                    if (id > 0)
                    {
                        objNganhDaoTao = NganhDaoTaoBLL.GetById(id);
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

                objNganhDaoTao.MaNganh = txtMaNganh.Value.Trim();
                objNganhDaoTao.TenNganh = txtTieuDe.Value.Trim();
                objNganhDaoTao.SoNamDaoTao = txtSoNamDaoTao.Value.Trim();
                objNganhDaoTao.SoTinChi = int.TryParse(txtSoTinChi.Value.Trim(), out int soTinChi) ? soTinChi : 0;
                objNganhDaoTao.MoTaNgan = txtMoTaNgan.Value.Trim();
                objNganhDaoTao.NoiDung = Server.HtmlEncode(txtNoiDungChinh.Text.Trim());
                objNganhDaoTao.NhomNganh = int.TryParse(ddlSearchCategory.SelectedValue, out int nhomNganh) ? nhomNganh : 0;
                objNganhDaoTao.DieuKienNhapHoc = txtDieuKienNhapHoc.Value.Trim();
                objNganhDaoTao.Slug = txtSlug.Value.Trim();
                objNganhDaoTao.HocPhi = txtHocPhi.Value.Trim();
                int displayOrder = 0;
                int.TryParse(txtDisplayOrder.Value.Trim(), out displayOrder);
                objNganhDaoTao.DisplayOrder = displayOrder ;
                int viewCount = 0;
                int.TryParse(txtViewCount.Value.Trim(), out viewCount);

                objNganhDaoTao.ViewCount = viewCount;
                objNganhDaoTao.ThumbnailUrl = imgThumb.Src.Trim();
                objNganhDaoTao.Status = ddlStatus.SelectedValue;
                objNganhDaoTao.UpdateDate = DateTime.Now;
                objNganhDaoTao.UpdateBy = NguoiDungManagerBLL.GetById(ApplicationContext.Current.CurrentUserID).TenTruyCap ?? string.Empty;

                if (!string.IsNullOrEmpty(txtImage.Value.Trim()))
                {
                    objNganhDaoTao.ThumbnailUrl = Helpers.ConvertToSavePath(txtImage.Value.Trim(), true);
                }
                if (isAdd)
                {
                   
                    objNganhDaoTao.CreateBy = NguoiDungManagerBLL.GetById(ApplicationContext.Current.CurrentUserID).TenTruyCap ?? string.Empty;
                    objNganhDaoTao.CreateDate = DateTime.Now;
                    objNganhDaoTao = NganhDaoTaoBLL.Insert(objNganhDaoTao);
                    LichSuHeThongBLL.LogAction(LichSuHeThongType.INSERT, LichSuHeThongGroup.QuanLyChuongTrinhDaoTao, objNganhDaoTao.TenNganh);
                }
                else
                {
                    objNganhDaoTao = NganhDaoTaoBLL.Update(objNganhDaoTao);
                    hdnRowId.Value = "0";
                    LichSuHeThongBLL.LogAction(LichSuHeThongType.UPDATE, LichSuHeThongGroup.QuanLyChuongTrinhDaoTao, objNganhDaoTao.TenNganh);
                   
                }
                #region update AttachmentFile
                List<ItemFile> listAtt = JsonConvert.DeserializeObject<List<ItemFile>>(txtlistFileUploadJson.Value);
                if (listAtt != null && listAtt.Count > 0)
                {
                    int idAttachfile = 0;

                    foreach (ItemFile newAtt in listAtt)
                    {
                        if (int.TryParse(newAtt.AttachmentFileIdString, out idAttachfile))
                        {
                            var updateObj = FileAttactmentBLL.GetAttachmentFilebyPostIdAndTypeAndId(objNganhDaoTao.Id, FileAttactmentType.NganhDaoTao, idAttachfile);
                            if (updateObj != null)
                            {
                                updateObj.Title = newAtt.Title;
                                updateObj.FileUrl = newAtt.FileUrl;
                                updateObj = FileAttactmentBLL.UpdateAttachmentFileInPost(updateObj);
                                itemAttachments = itemAttachments.Where(t => t.Id != idAttachfile).ToList();
                            }

                        }
                        else
                        {
                            FileAttactment attachmentFile = new FileAttactment()
                            {
                                CategoryId = objNganhDaoTao.Id,
                                Type = FileAttactmentType.NganhDaoTao,
                                FileUrl = newAtt.FileUrl,
                                Title = newAtt.Title,
                            };
                            FileAttactmentBLL.InsertAttachmentFileInPost(attachmentFile);
                        }
                        //var itemAtt = AttachmentFileManager.GetAttachmentFilebyPostIdAndTypeAndId(objFAQ.FAQId, AttachmentFileType.FAQ, att.AttachmentFileId);

                    }
                    // xóa nếu cũ không có trong mới
                    foreach (ItemFile att in itemAttachments)
                    {

                        FileAttactmentBLL.DeleteAttachmentFile(att.Id);
                    }
                }
                else
                {
                    // xóa đi những file đang thuộc nhưng đã bị xóa
                    if (itemAttachments != null && itemAttachments.Count > 0)
                    {
                        FileAttactmentBLL.DeleteAllAttachmentFileInPost(objNganhDaoTao.Id, FileAttactmentType.NganhDaoTao);
                    }
                }
                #endregion

                if (objNganhDaoTao != null)
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
                        var obj = NganhDaoTaoBLL.GetById(baiVietId);

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
                        if (NganhDaoTaoBLL.Delete(baiVietId))
                        {
                            BindDataByQuyen();
                            LichSuHeThongBLL.LogAction(LichSuHeThongType.DELETE, LichSuHeThongGroup.QuanLyChuongTrinhDaoTao, obj.TenNganh);
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