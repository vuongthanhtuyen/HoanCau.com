using CMS.Core.Manager;
using CMS.DataAsscess;
using CMS.WebUI.Administration.Common;
using SweetCMS.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CMS.WebUI.Administration.QuanLyCauHinh
{
    public partial class MenuDuoiWeb : AdminPermistion
    {
        public override string MenuMa { get; set; } = "Menu-hien-thi-duoi";
        private static List<MenuWebDto> listMenuDto = new List<MenuWebDto>();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindDataByQuyen();
                BindMenuCha();
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
        private void BindGrid(int pageIndex = 1, int pageSize = 10, int menuCha = 0)
        {
            pageIndex = PagingAdminWeb.GetPageIndex();
            int totalRow = 0;
            if (menuCha != 0)
                listMenuDto = MenuWebDuoiBLL.GetPaging(pageSize, pageIndex, null, null, menuCha, out totalRow);
            else
                listMenuDto = MenuWebDuoiBLL.GetPaging(pageSize, pageIndex, Request.QueryString["search"], null, null, out totalRow);
            SearchUserControl.SetSearcKey();

            ViewState["LastIndex"] = (pageIndex - 1) * pageSize;
            PagingAdminWeb.GetPaging(totalRow, pageIndex);
            GridViewTable.DataSource = listMenuDto;
            GridViewTable.DataBind();
            UpdatePanelMainTable.Update();

            //BindListDanhMucCha();
        }


        private void BindMenuCha()
        {
            List<MenuWebDuoi> listMenu = MenuWebDuoiBLL.GetListParentMenu();
            List<ListItem> list = listMenu.Select(x => new ListItem(x.Ten, x.Id.ToString())).ToList();

            ddlAddMenuCha.Items.Clear();
            ddlAddMenuCha.Items.Add(new ListItem("Không có menu", "0"));
            ddlAddMenuCha.Items.AddRange(list.ToArray());

            ddlEditMenuCha.Items.Clear();
            ddlEditMenuCha.Items.Add(new ListItem("Không có menu", "0"));
            ddlEditMenuCha.Items.AddRange(list.ToArray());

            BindAddUrl();
        }

        private void BindAddUrl()
        {
            #region Thêm dữ liệu cho Droplist của trang Add menu
            List<BaiViet> listBaiViet = MenuWebDuoiBLL.GetListBaiViet();
            List<ListItem> listAdd = listBaiViet.Select(x => new ListItem(x.TieuDe, "baivietpublish?id=" + x.Id.ToString())).ToList();
            drAddbaiviet.Items.Clear();
            drAddbaiviet.Items.Add(new ListItem("Không", "0"));
            drAddbaiviet.Items.AddRange(listAdd.ToArray());


            List<DanhMuc> listDanhMuc = MenuWebDuoiBLL.GetListDanhMuc();
            List<ListItem> listItemDM = listDanhMuc.Select(x => new ListItem(x.Ten, "DanhMucPublish?id=" + x.Id.ToString())).ToList();
            drAddDanhSach.Items.Clear();
            drAddDanhSach.Items.Add(new ListItem("Không", "0"));
            drAddDanhSach.Items.AddRange(listItemDM.ToArray());


            List<BaiViet> listDuAnTieuBieu = MenuWebDuoiBLL.GetListDuAnTieuBieu();
            List<ListItem> listItemDATB = listDuAnTieuBieu.Select(x => new ListItem(x.TieuDe, "DuAnTieuBieuPublish?id=" + x.Id.ToString())).ToList();
            drAddDuAnTieuBieu.Items.Clear();
            drAddDuAnTieuBieu.Items.Add(new ListItem("Không", "0"));
            drAddDuAnTieuBieu.Items.AddRange(listItemDATB.ToArray());

            List<ListItem> listItemTrangTinh = new List<ListItem>()
            {
                new ListItem("Trang chủ","Default.aspx"),
                new ListItem("Đối tác","DoiTacPublish.aspx"),
                new ListItem("Ds dự án tiêu biểu","DanhSachDuAnTieuBieuPublish.aspx"),
                new ListItem("Sơ đồ tổ chức","SoDoToChucPubLish.aspx"),
                new ListItem("Lịch sử phát triển","LichSuPhatTrienPublish.aspx"),
                new ListItem("Liên hệ","LienHePublish.aspx"),
            };
            drAddTrangTinh.Items.Clear();
            drAddTrangTinh.Items.Add(new ListItem("Không", "0"));
            drAddTrangTinh.Items.AddRange(listItemTrangTinh.ToArray());
            #endregion

            #region Thêm dữ liệu cho drop menu ở trang Edit
            drEditBaiviet.Items.Clear();
            drEditBaiviet.Items.Add(new ListItem("Không", "0"));
            drEditBaiviet.Items.AddRange(listAdd.ToArray());

            drEditDanhSach.Items.Clear();
            drEditDanhSach.Items.Add(new ListItem("Không", "0"));
            drEditDanhSach.Items.AddRange(listItemDM.ToArray());


            drEditDuAnTieuBieu.Items.Clear();
            drEditDuAnTieuBieu.Items.Add(new ListItem("Không", "0"));
            drEditDuAnTieuBieu.Items.AddRange(listItemDATB.ToArray());

            drEditTrangTinh.Items.Clear();
            drEditTrangTinh.Items.Add(new ListItem("Không", "0"));
            drEditTrangTinh.Items.AddRange(listItemTrangTinh.ToArray());

            #endregion

        }



        protected void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                MenuWebDuoi menu = new MenuWebDuoi();
                lblAddErrorMessage.Text = "";
                if (string.IsNullOrEmpty(txtTen.Text.Trim()) || txtTen.Text.Trim().Length <= 3)
                {
                    lblAddErrorMessage.Text += "Tên Menu không được trống hoặc bé hơn 3 ký tự<br />";
                }

                if (string.IsNullOrEmpty(txtUrl.Text.Trim()) || txtUrl.Text.Trim().Length <= 3)
                {
                    lblAddErrorMessage.Text += "Url không hợp lệ";
                }
                if (lblAddErrorMessage.Text.Length > 0)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "OpenModal", "openModal();", true);
                    UpdatePanelAdd.Update();
                    return;
                }
                if (!VaiTroManagerBll.AllowAdd(CurrentUserId, MenuMa))
                {
                    ShowNotification("Bạn không có quyền truy cập chức năng này", false);
                    return;
                }
                else
                {
                    menu.Ten = txtTen.Text;
                    menu.Slug = txtUrl.Text;
                    menu.MenuChaId = int.Parse(ddlAddMenuCha.SelectedValue) == 0 ? (int?)null : int.Parse(ddlAddMenuCha.SelectedValue);
                    menu.Stt = int.Parse(txtStt.Text);
                    menu.NgayTao = DateTime.Now;
                    menu.HienThi = true;
                    menu = MenuWebDuoiBLL.Insert(menu);
                    BindDataByQuyen();
                    ScriptManager.RegisterStartupScript(this, GetType(), "CloseModal", "closeModal();", true);
                    if (menu != null)
                    {
                        lblAddErrorMessage.Text = "";
                        ShowNotification("Thêm menu thành công");

                        txtTen.Text = string.Empty;
                        txtUrl.Text = string.Empty;
                        txtStt.Text = "1"; // Đặt giá trị mặc định

                        // Gán selectedIndex về 0 (chọn mục đầu tiên của DropDownList)
                        ddlAddMenuCha.SelectedIndex = 0;
                        drAddbaiviet.SelectedIndex = 0;
                        drAddDanhSach.SelectedIndex = 0;
                        drAddDuAnTieuBieu.SelectedIndex = 0;
                        drAddTrangTinh.SelectedIndex = 0;
                        UpdatePanelAdd.Update();


                    }
                    else
                    {
                        ShowNotification("Thêm menu thất bại", false);
                    }


                }
            }
            catch (Exception ex)
            {
                ShowNotification("Thêm menu thất bại! \n Lỗi: " + ex.Message, false);

            }
        }

        protected void GridViewTable_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                int menuId = Convert.ToInt32(e.CommandArgument);
                var menu = MenuWebDuoiBLL.GetById(menuId);
                if (menu != null)
                {
                    hdnRowId.Value = menuId.ToString();
                    if (e.CommandName == "ChinhSuaChiTiet")
                    {
                            if (listMenuDto.Any(x => x.MenuChaId == menuId))
                            {
                                lblEditDrop.Visible = false;
                                ddlEditMenuCha.Visible = false;
                            }
                            else
                            {
                                ddlEditMenuCha.Visible = true;
                                lblEditDrop.Visible = true;
                            }
                        txtEditTen.Text = menu.Ten;
                        txtEditUrl.Text = menu.Slug;
                        txtEditStt.Text = string.IsNullOrEmpty(menu.Stt.ToString()) ? "0" : menu.Stt.ToString();
                        ddlEditMenuCha.SelectedValue = string.IsNullOrEmpty(menu.MenuChaId.ToString()) ? "0" : menu.MenuChaId.ToString();
                        txtEditNgayTao.Text = ((DateTime)menu.NgayTao).ToString("yyyy-MM-dd");
                        chkEditTrangThai.Checked = menu.HienThi ?? false;
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
                    ShowNotification("menu này không tồn tại", false);
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
                    var MenuWebDuoi = MenuWebDuoiBLL.GetById(id);

                    if (MenuWebDuoi == null)
                    {
                        ShowNotification("Lỗi không tìm thấy menu", false);
                        return;
                    }
                    if (!VaiTroManagerBll.AllowDelete(CurrentUserId, MenuMa))
                    {
                        ShowNotification("Bạn không có quyền truy cập chức năng này", false);
                        return;
                    }

                    MenuWebDuoiBLL.Delete(id);
                    BindDataByQuyen();
                    ShowNotification("Đã xóa menu");
                    ScriptManager.RegisterStartupScript(this, GetType(), "closeDelete", "closeDelete();", true);

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
                if (!VaiTroManagerBll.AllowEdit(CurrentUserId, MenuMa))
                {
                    ShowNotification("Bạn không có quyền truy cập chức năng này", false);
                    return;
                }
                int menuId = int.Parse(hdnRowId.Value);
                hdnRowId.Value = "";
                MenuWebDuoi menu = MenuWebDuoiBLL.GetById(menuId);
                menu.Id = menuId; // ID của menu cần cập nhật
                menu.Ten = txtEditTen.Text;
                menu.Slug = txtEditUrl.Text;
                menu.Stt = int.Parse(txtEditStt.Text); // Chuyển đổi sang số nguyên
                menu.MenuChaId = ddlEditMenuCha.SelectedValue != "0" ? (int?)int.Parse(ddlEditMenuCha.SelectedValue) : null;
                menu.HienThi = chkEditTrangThai.Checked;
                menu = MenuWebDuoiBLL.Update(menu);
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