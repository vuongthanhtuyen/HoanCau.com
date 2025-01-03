using CMS.Core.Manager;
using CMS.DataAsscess;
using CMS.WebUI.Administration.Common;
using TBDCMS.Core.Helper;
using TBDCMS.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CMS.WebUI.Administration
{
    public partial class DoiTacWeb : AdminPermistion
    {
        public override string MenuMa { get; set; } = "Menu-hien-thi-duoi";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindDataByQuyen();
                if (!IsAlive()) Response.Redirect("/Administration/Login.aspx");

            }
        }

        private void BindGrid(int pageIndex = 1, int pageSize = 10)
        {
            pageIndex = PagingAdminWeb.GetPageIndex();
            List<DoiTac> trinhChieuAnhList = new List<DoiTac>();
            int totalRow = 0;
            trinhChieuAnhList = DoiTacBLL.GetPaging(pageSize, pageIndex, Request.QueryString["search"], true, out totalRow);

            PagingAdminWeb.GetPaging(totalRow, pageIndex);
            GridViewTable.DataSource = trinhChieuAnhList;
            GridViewTable.DataBind();
        }



        public static string _ModalTitle = string.Empty;
        public static string _CreateDate = string.Empty;


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

        protected void btnRefresh_ServerClick(object sender, EventArgs e)
        {
            int _id = 0;

            try
            {
                if (int.TryParse(txtIdHidden.Value, out _id))
                {

                    var objDoiTac = DoiTacBLL.GetById(_id);
                    txtTen.Text = objDoiTac.Ten;
                    txtLienKetUrl.Text = objDoiTac.LienKetUrl;
                    imgThumb.Src = Helpers.GetThumbnailUrl(objDoiTac.HinhAnhUrl);
                    chkTrangThai.Checked = objDoiTac.TrangThai ?? false;
                    txtInfo.Visible = true;
                    _CreateDate = objDoiTac.NgayTao.ToString("yyyy-MM-dd");
                    _ModalTitle = "Cập nhật";
                }
                else
                {
                    txtInfo.Visible = false;
                    _ModalTitle = "Thêm mới";
                    _CreateDate = txtTen.Text = txtLienKetUrl.Text  = string.Empty;
                    imgThumb.Attributes["src"] = "/Administration/UploadImage/addNewImage.png"; // Reset hình ảnh
                    chkTrangThai.Checked = true;
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
                int objDoiTacId = Convert.ToInt32(e.CommandArgument);
                var objDoiTac = DoiTacBLL.GetById(objDoiTacId);
                if (objDoiTac != null)
                {
                    txtIdHidden.Value = objDoiTacId.ToString();
                    if (e.CommandName == "Xoa")
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "openDelete", "openDelete();", true);
                    }
                    UpdatePanelModal.Update();
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
                DoiTac objDoiTac = new DoiTac();
                if (int.TryParse(txtIdHidden.Value, out id))
                {
                    if (id > 0)
                    {
                        objDoiTac = DoiTacBLL.GetById(id);
                        isAdd = false;
                    }
                }
                //if (string.IsNullOrEmpty(txtTieuDeMot.Value.Trim()) || txtTieuDeMot.Value.Trim().Length <= 3)
                //{
                //    AddErrorPrompt(txtTieuDeMot.ClientID, "Không được bỏ trống trường này");
                //}

                //if (string.IsNullOrEmpty(txtTieuDeHai.Value.Trim()) || txtTieuDeHai.Value.Trim().Length <= 3)
                //{
                //    AddErrorPrompt(txtTieuDeHai.ClientID, "Không được bỏ trống trường này");
                //}

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
                objDoiTac.Ten = txtTen.Text.Trim();
                //objDoiTac.NoiDungHai = txtTieuDeHai.Value.Trim();
                objDoiTac.LienKetUrl = txtLienKetUrl.Text.Trim();
                //int _display = -1;
                //int.TryParse(txtStt.Value, out _display);
                //objDoiTac.Stt = _display;
                if (!string.IsNullOrEmpty(txtImage.Value.Trim()))
                {
                    objDoiTac.HinhAnhUrl = Helpers.ConvertToSavePath(txtImage.Value.Trim(), true);
                }

                objDoiTac.TrangThai = chkTrangThai.Checked;
                if (isAdd)
                {
                    objDoiTac.NgayTao = DateTime.Now;
                    objDoiTac = DoiTacBLL.Insert(objDoiTac);
                    LichSuHeThongBLL.LogAction(LichSuHeThongType.INSERT, LichSuHeThongGroup.QuanLyDoiTac, objDoiTac.Ten);

                }
                else
                {
                    objDoiTac = DoiTacBLL.Update(objDoiTac);
                    LichSuHeThongBLL.LogAction(LichSuHeThongType.UPDATE, LichSuHeThongGroup.QuanLyDoiTac, objDoiTac.Ten);

                    txtIdHidden.Value = "0";
                }

                if (objDoiTac != null)
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
                int objDoiTacId = 0;

                if (int.TryParse(txtIdHidden.Value, out objDoiTacId))
                {

                    if (objDoiTacId > 0)
                    {
                        txtIdHidden.Value = "";
                        var obj = DoiTacBLL.GetById(objDoiTacId);

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


                        if (DoiTacBLL.Delete(objDoiTacId))
                        {
                            BindDataByQuyen();
                            LichSuHeThongBLL.LogAction(LichSuHeThongType.DELETE, LichSuHeThongGroup.QuanLyDoiTac, obj.Ten);

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




    }

}






//        AdminPermistion
//    {
//        public override string MenuMa { get; set; } = "Doi-tac";  // Ghi đè và gán giá trị mới
//        protected void Page_Load(object sender, EventArgs e)
//        {
//            if (!IsPostBack)
//            {
//                BindDataByQuyen();
//                if (!IsAlive()) Response.Redirect("/Administration/Login.aspx");

//            }
//        }


//        private void BindDataByQuyen()
//        {
//            btnOpenModal.Visible = CheckPermission(MenuMa, Them);
//            if (CheckPermission(MenuMa, Xem))
//            {
//                BindGrid();

//                foreach (GridViewRow row in GridViewTable.Rows)
//                {
//                    if (row.RowType == DataControlRowType.DataRow)
//                    {
//                        // Tìm LinkButton trong hàng
//                        LinkButton btnEdit = (LinkButton)row.FindControl("ChinhSuaChiTiet");
//                        LinkButton btnDelete = (LinkButton)row.FindControl("Xoa");

//                        if (btnEdit != null)
//                        {
//                            btnEdit.Visible = CheckPermission(MenuMa, Sua);
//                            btnDelete.Visible = CheckPermission(MenuMa, Xoa);
//                        }
//                    }
//                }
//            }
//            //UpdatePanelTablleMain.Update();
//        }
//        private void BindGrid(int pageIndex = 1, int pageSize = 10)
//        {
//            pageIndex = PagingAdminWeb.GetPageIndex();
//            List<DoiTac> doiTacList = new List<DoiTac>();
//            int totalRow = 0;
//            doiTacList = DoiTacBLL.GetPaging(pageSize, pageIndex, Request.QueryString["search"], null, out totalRow);

//            ViewState["LastIndex"] = (pageIndex - 1) * pageSize;
//            PagingAdminWeb.GetPaging(totalRow, pageIndex);
//            GridViewTable.DataSource = doiTacList;
//            GridViewTable.DataBind();
//        }

//        #region
//        protected void btnAdd_Click(object sender, EventArgs e)
//        {
//            try
//            {
//                DoiTac doiTac = new DoiTac();
//                lblAddErrorMessage.Text = "";
//                if (string.IsNullOrEmpty(txtTen.Text.Trim()) || txtTen.Text.Trim().Length <= 3)
//                {
//                    lblAddErrorMessage.Text += "Tên công ty không được để trống <br />";
//                    ScriptManager.RegisterStartupScript(this, GetType(), "OpenModal", "openModal();", true);
//                    return;
//                }
//                else
//                {
//                    if (!VaiTroManagerBll.AllowAdd(ApplicationContext.Current.CurrentUserID, MenuMa))
//                    {
//                        ShowNotification("Bạn không có quyền truy cập chức năng này", false);
//                        return;
//                    }
//                    doiTac.Ten = txtTen.Text;
//                    doiTac.LienKetUrl = txtLienKetUrl.Text;
//                    doiTac.TrangThai = true;
//                    doiTac.NgayTao =DateTime.Now;
//                    doiTac.HinhAnhUrl = txtThumbnailUrl.GetStringFileUrl();

//                    doiTac = DoiTacBLL.Insert(doiTac);
//                    ScriptManager.RegisterStartupScript(this, GetType(), "CloseModal", "closeModal();", true);

//                    ShowNotification("Lưu đối tác thành công");

//                }
//            }

//            catch (Exception ex)
//            {
//                ShowNotification("Lưu đối tác thất bại! \n Lỗi: " + ex.Message, false);

//            }
//        }
//        protected void btnEdit_Click(object sender, EventArgs e)
//        {
//            try
//            {
//                if (!VaiTroManagerBll.AllowEdit(ApplicationContext.Current.CurrentUserID, MenuMa))
//                {
//                    ShowNotification("Bạn không có quyền truy cập chức năng này", false);
//                    return;
//                }
//                int doiTacid = int.Parse(hdnRowId.Value); // Lấy ID người dùng đã chỉnh sửa
//                hdnRowId.Value = "";
//                // Lấy thông tin người dùng từ cơ sở dữ liệu
//                DoiTac doiTac = DoiTacBLL.GetById(doiTacid);
//                doiTac.Ten = txtEditTenCongTy.Text; // Cập nhật tên đối tác từ textbox
//                doiTac.LienKetUrl = txtEditLienKetUrl.Text; // Cập nhật URL liên kết từ textbox
//                doiTac.TrangThai = chkEditTrangThai.Checked; // Cập nhật trạng thái từ checkbox
//                doiTac.HinhAnhUrl = ImportImageEdit.GetStringFileUrl();

//                DoiTacBLL.Update(doiTac);
//                ScriptManager.RegisterStartupScript(this, GetType(), "closeEdit", "closeEdit();", true);

//                ShowNotification("Cập nhật thành công", true);
//            }
//            catch (Exception ex)
//            {
//                ShowNotification("Cập nhật đối tác thất bại! \n " + ex.Message, false);
//            }
//        }
//        protected void btnDelete_Click(object sender, EventArgs e)
//        {
//            try
//            {
//                if (!VaiTroManagerBll.AllowDelete(ApplicationContext.Current.CurrentUserID, MenuMa))
//                {
//                    ShowNotification("Bạn không có quyền truy cập chức năng này", false);
//                    return;
//                }

//                int doiTacId = int.Parse(hdnRowId.Value);
//                hdnRowId.Value = "";
//                if (!string.IsNullOrEmpty(doiTacId.ToString()))
//                {
//                    var nguoiDung = DoiTacBLL.GetById(doiTacId);

//                    if (nguoiDung == null)
//                    {
//                        ShowNotification("Lỗi không tìm thấy đối tác", false);
//                    }

//                    else if (DoiTacBLL.Delete(doiTacId))
//                    {
//                        ShowNotification("Đã xóa đối tác");
//                    }
//                    else
//                    {
//                        ShowNotification("Xóa dối tác không thành công", false);
//                    }
//                }

//            }
//            catch (Exception ex)
//            {
//                ShowNotification("Xóa thất bại! \n " + ex.Message, false);
//            }
//        }

//        protected void GridViewTable_RowCommand(object sender, GridViewCommandEventArgs e)
//        {
//            try
//            {
//                int doiTacId = Convert.ToInt32(e.CommandArgument);
//                var doiTac = DoiTacBLL.GetById(doiTacId);
//                if (doiTac != null)
//                {
//                    hdnRowId.Value = doiTacId.ToString();
//                    if(e.CommandName == "ChinhSuaChiTiet")
//                    {
//                        txtEditLienKetUrl.Text = doiTac.LienKetUrl;
//                        txtEditTenCongTy.Text = doiTac.Ten;
//                        txtEditNgayTao.Text = ((DateTime)doiTac.NgayTao).ToString("yyyy-MM-dd");
//                        chkEditTrangThai.Checked = doiTac.TrangThai ?? false;
//                        ImportImageEdit.SetFileImage(doiTac.HinhAnhUrl);
//                        ScriptManager.RegisterStartupScript(this, GetType(), "openEdit", "openEdit();", true);
//                    }
//                    else if (e.CommandName == "Xoa")
//                    {
//                        ScriptManager.RegisterStartupScript(this, GetType(), "openDelete", "openDelete();", true);
//                    }
//                }
//                else
//                {
//                    ShowNotification("Đối tác này không tồn tại", false);
//                }
//            }
//            catch (Exception ex)
//            {
//                ShowNotification(ex.Message, false);
//            }

//        }
//        #endregion

//        // Thực hiện việc xóa user bằng userId
//        private void ShowNotification(string message, bool isSuccess = true)
//        {
//            AdminNotificationUserControl.LoadMessage(message, isSuccess);
//        }

//      }
//}