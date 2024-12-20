using CMS.Core.Manager;
using CMS.DataAsscess;
using CMS.WebUI.Administration.AdminUserControl;
using CMS.WebUI.Administration.Common;
using SweetCMS.Core.Helper;
using SweetCMS.DataAccess;
using System;
using System.Collections.Generic;
using System.Globalization;
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
                if (!IsAlive()) Response.Redirect("/Administration/Login.aspx");

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

                    var objSlide = SlideBLL.GetById(_id);
                    txtTieuDeMot.Value = objSlide.NoiDungMot;
                    txtTieuDeHai.Value = objSlide.NoiDungHai;
                    txtLienKetUrl.Value = objSlide.LienKetUrl;
                    imgThumb.Src = Helpers.GetThumbnailUrl(objSlide.HinhAnhUrl);
                    chkTrangThai.Checked = objSlide.TrangThai ?? false;
                    txtInfo.Visible = true;
                    _CreateDate = objSlide.NgayTao.ToString("yyyy-MM-dd");
                    _ModalTitle = "Cập nhật";
                }
                else
                {
                    txtInfo.Visible = false;
                    _ModalTitle = "Thêm mới";
                    _CreateDate = txtTieuDeMot.Value = txtTieuDeHai.Value = txtLienKetUrl.Value = string.Empty;
                    imgThumb.Attributes["src"] = "../UploadImage/addNewImage.png"; // Reset hình ảnh
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
                int objSlideId = Convert.ToInt32(e.CommandArgument);
                var objSlide = SlideBLL.GetById(objSlideId);
                if (objSlide != null)
                {
                    txtIdHidden.Value = objSlideId.ToString();
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
                TrinhChieuAnh objSlide = new TrinhChieuAnh();
                if (int.TryParse(txtIdHidden.Value, out id))
                {
                    if (id > 0)
                    {
                        objSlide = SlideBLL.GetById(id);
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
                objSlide.NoiDungMot = txtTieuDeMot.Value.Trim();
                objSlide.NoiDungHai = txtTieuDeHai.Value.Trim();
                objSlide.LienKetUrl = txtLienKetUrl.Value.Trim();
                int _display = -1;
                int.TryParse(txtStt.Value, out _display);
                objSlide.Stt = _display;
                if (!string.IsNullOrEmpty(txtImage.Value.Trim()))
                {
                    objSlide.HinhAnhUrl = Helpers.ConvertToSavePath(txtImage.Value.Trim(), true);
                }

                objSlide.TrangThai = chkTrangThai.Checked;
                if (isAdd)
                {
                    objSlide.NgayTao = DateTime.Now;
                    objSlide = SlideBLL.Insert(objSlide);
                    LichSuHeThongBLL.LogAction(LichSuHeThongType.INSERT, LichSuHeThongGroup.QuanLySlide, objSlide.NoiDungMot);

                }
                else
                {
                    objSlide = SlideBLL.Update(objSlide);
                    LichSuHeThongBLL.LogAction(LichSuHeThongType.UPDATE, LichSuHeThongGroup.QuanLySlide, objSlide.NoiDungMot);

                    txtIdHidden.Value = "0";
                }

                if (objSlide != null)
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
                int objSlideId = 0;

                if (int.TryParse(txtIdHidden.Value, out objSlideId))
                {

                    if (objSlideId > 0)
                    {
                        txtIdHidden.Value = "";
                        var obj = SlideBLL.GetById(objSlideId);

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


                        if (SlideBLL.Delete(objSlideId))
                        {
                            BindDataByQuyen();
                            LichSuHeThongBLL.LogAction(LichSuHeThongType.DELETE, LichSuHeThongGroup.QuanLySlide, obj.NoiDungMot);

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