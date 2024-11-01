using CMS.Core.Manager;
using CMS.DataAsscess;
using CMS.WebUI.Administration.Common;
using Newtonsoft.Json;
using SweetCMS.Core.Helper;
using SweetCMS.DataAccess;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Security.Policy;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CMS.WebUI.Administration.QuanLyCauHinh
{
    public partial class MenuTrenWeb : AdminPermistion
    {
        public override string MenuMa { get; set; } = "Menu-hien-thi-tren";
        private List<MenuWebTren> listMenuDto = new List<MenuWebTren>();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindDataTree();
                BindMenuCha();
                GetModal();
                if (!IsAlive()) Response.Redirect("~/Administration/Login.aspx", false);

            }
        }


        private void GetModal()
        {
            string modal = Request.QueryString["modal"];

            if (modal == "openModal")
            {
                string MenuIdParent = Request.QueryString["MenuIdParent"];
                ddlAddMenuCha.SelectedValue = MenuIdParent ?? "0";
                ScriptManager.RegisterStartupScript(this, GetType(), "OpenModal", "openModal();", true);
                return;
            }
            string stidMenu = Request.QueryString["idMenu"];
            if (int.TryParse(stidMenu, out int id))
            {
                hdnRowId.Value = id.ToString();

                if (modal == "openEdit")
                {
                    var menu = MenuWebTrenBLL.GetById(id);
                    if (listMenuDto.Any(x => x.MenuChaId == id))
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

                    //UpdatePanelThemBaiViet.Update();
                    return;
                }
                if (modal == "openDelete")
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "openDelete", "openDelete();", true);
                    return;
                }
                else
                {
                    ShowNotification("Danh mục này không tồn tại", false);
                }

            }

        }

        private bool IsAddMenu
        {
            get
            {
                try
                {
                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }

        private bool IsEditMenu
        {
            get
            {
                try
                {
                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }



        private void BindDataTree()
        {
            try
            {
                List<ItemTreeView> lstTree = new List<ItemTreeView>();
                int totalRow = 0;
                listMenuDto = MenuWebTrenBLL.GetAllByLangId(ApplicationContext.Current.ContentCurrentLanguageId);
                List<MenuWebTren> lst = listMenuDto;
                if (lst != null && lst.Count > 0)
                {
                    Func<int, List<ItemTreeView>> func = null;
                    func = (parentId) =>
                    {
                        List<ItemTreeView> lstTreeChild = new List<ItemTreeView>();
                        List<MenuWebTren> lstChild = lst.Where(t => t.MenuChaId == parentId).ToList();
                        if (lstChild != null && lstChild.Count > 0)
                        {
                            foreach (var item in lstChild)
                            {
                                if (item != null)
                                {
                                    ItemTreeView itemTree = new ItemTreeView();
                                    itemTree.MenuId = item.Id;
                                    itemTree.text = string.Format("{0} {1} {2}", item.Ten, GetStatusText(BasicStatusHelper.Active), "<a class=\"btn btn-danger p-0\" style=\" font-size:14px;\"  href=\"/Administration/QuanLyCauHinh/MenuTrenWeb.aspx?modal=openDelete&idMenu=" + item.Id + "\", item.Id\" ><span class=\"fa fa-trash\"></span> Xóa</a>");
                                    //itemTree.text = string.Format("{0} {1} ({2})", item.Ten, "Active", true);
                                    itemTree.icon = "fa fa-link";
                                    itemTree.state = new ItemState { opened = true };
                                    if (true || true)
                                        itemTree.a_attr = new { href = string.Format("/Administration/QuanLyCauHinh/MenuTrenWeb.aspx?modal=openEdit&idMenu={0}", item.Id) };
                                    else
                                        itemTree.a_attr = null;
                                    itemTree.children = func(item.Id);
                                    lstTreeChild.Add(itemTree);
                                }
                            }
                            ItemTreeView addChild = new ItemTreeView();
                            addChild.MenuId = parentId;
                            addChild.text = "Thêm mới";
                            addChild.icon = "fa fa-plus";
                            addChild.state = new ItemState { opened = true };
                            if (IsEditMenu)
                                addChild.a_attr = new { href = "/Administration/QuanLyCauHinh/MenuTrenWeb?modal=openModal&MenuIdParent=" + parentId };
                            else
                                addChild.a_attr = null;
                            addChild.children = null;
                            lstTreeChild.Add(addChild);
                        }
                        else if (lst.Where(x => x.MenuChaId == 0 && parentId == x.Id).Count() > 0)
                        {
                            ItemTreeView addChild = new ItemTreeView();
                            addChild.MenuId = parentId;
                            addChild.text = "Thêm mới";
                            addChild.icon = "fa fa-plus";
                            addChild.state = new ItemState { opened = true };
                            if (IsEditMenu)
                                addChild.a_attr = new { href = "/Administration/QuanLyCauHinh/MenuTrenWeb?modal=openModal&MenuIdParent=" + parentId };
                            else
                                addChild.a_attr = null;
                            addChild.children = null;
                            lstTreeChild.Add(addChild);
                        }


                        return lstTreeChild;
                    };
                    lstTree = func(0);
                }
                //if (IsEditMenu)
                //{
                //    ItemTreeView addChild = new ItemTreeView();
                //    addChild.MenuId = 0;
                //    addChild.text = "Thêm mới";
                //    addChild.icon = "fa fa-plus";
                //    addChild.state = new ItemState { opened = true };
                //    if (IsEditMenu)
                //        addChild.a_attr = new { href = "/Administration/QuanLyCauHinh/MenuTrenWeb?modal=openModal" };
                //    else
                //        addChild.a_attr = null;
                //    addChild.children = null;
                //    lstTree.Add(addChild);
                //}
                hdfRightsTreeViewData.Value = JsonConvert.SerializeObject(new { MenuId = 0, text = "Danh sách Menu trên", children = lstTree, icon = "fa fa-list-ul", state = new { opened = true } });
                UpdatePanelMainTable.Update();
                SearchUserControl.SetSearcKey();

            }
            catch
            {

            }
        }


        private class ItemTreeView
        {
            public int MenuId { get; set; }
            public string text { get; set; }
            public ItemState state { get; set; }
            public string icon { get; set; }
            public object a_attr { get; set; }
            public List<ItemTreeView> children { get; set; }
        }
        private class ItemState
        {
            public bool opened { get; set; }
        }


        private void BindMenuCha()
        {
            List<MenuWebTren> listMenu = MenuWebTrenBLL.GetListParentMenu(ApplicationContext.Current.ContentCurrentLanguageId);
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
            List<BaiViet> listBaiViet = MenuWebTrenBLL.GetListBaiViet(ApplicationContext.Current.ContentCurrentLanguageId);
            List<ListItem> listAdd = listBaiViet.Select(x => new ListItem(x.TieuDe, x.Slug)).ToList();
            drAddbaiviet.Items.Clear();
            drAddbaiviet.Items.Add(new ListItem("Không", "0"));
            drAddbaiviet.Items.AddRange(listAdd.ToArray());


            List<DanhMuc> listDanhMuc = MenuWebTrenBLL.GetListDanhMuc(ApplicationContext.Current.ContentCurrentLanguageId);
            List<ListItem> listItemDM = listDanhMuc.Select(x => new ListItem(x.Ten, x.Slug)).ToList();
            drAddDanhSach.Items.Clear();
            drAddDanhSach.Items.Add(new ListItem("Không", "0"));
            drAddDanhSach.Items.AddRange(listItemDM.ToArray());


            List<BaiViet> listDuAnTieuBieu = MenuWebTrenBLL.GetListDuAnTieuBieu(ApplicationContext.Current.ContentCurrentLanguageId);
            List<ListItem> listItemDATB = listDuAnTieuBieu.Select(x => new ListItem(x.TieuDe, x.Slug)).ToList();
            drAddDuAnTieuBieu.Items.Clear();
            drAddDuAnTieuBieu.Items.Add(new ListItem("Không", "0"));
            drAddDuAnTieuBieu.Items.AddRange(listItemDATB.ToArray());
            List<ListItem> listItemTrangTinh = new List<ListItem>();

            if (ApplicationContext.Current.ContentCurrentLanguageId == 1)
            {
                listItemTrangTinh = new List<ListItem>
                    {
                        new ListItem("Trang chủ","home"),
                        new ListItem("Đối tác","doi-tac"),
                        new ListItem("Ds dự án tiêu biểu","danh-dach-du-an-tieu-bieu"),
                        new ListItem("Lịch sử phát triển","lich-su-phat-trien"),
                        new ListItem("Liên hệ","lien-he"),
                    };
            }
            else
            {
                listItemTrangTinh = new List<ListItem>
                    {
                        new ListItem("Trang chủ","home-en"),
                        new ListItem("Đối tác","parner"),
                        new ListItem("Ds dự án tiêu biểu","featured-project"),
                        new ListItem("Lịch sử phát triển","history-of-development"),
                        new ListItem("Liên hệ","history-of-development"),
                    };
            }

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
                MenuWebTren menu = new MenuWebTren();
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
                    UpdatePanelAdd.Update();
                    ScriptManager.RegisterStartupScript(this, GetType(), "OpenModal", "openModal();", true);
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
                    menu.MenuChaId = int.Parse(ddlAddMenuCha.SelectedValue);
                    menu.Stt = int.Parse(txtStt.Text);
                    menu.NgayTao = DateTime.Now;
                    menu.HienThi = true;
                    menu.LangID = ApplicationContext.Current.ContentCurrentLanguageId;
                    menu = MenuWebTrenBLL.Insert(menu);
                    ScriptManager.RegisterStartupScript(this, GetType(), "CloseModal", "closeModal();", true);
                    if (menu != null)
                    {
                        lblAddErrorMessage.Text = "";
                        BindDataTree();
                        ShowNotification("Thêm menu thành công");
                        ddlAddMenuCha.SelectedIndex = 0;
                        drAddbaiviet.SelectedIndex = 0;
                        drAddDanhSach.SelectedIndex = 0;
                        drAddDuAnTieuBieu.SelectedIndex = 0;
                        drAddTrangTinh.SelectedIndex = 0;

                        txtTen.Text = string.Empty;
                        txtUrl.Text = string.Empty;
                        txtStt.Text = "1";
                        UpdatePanelAdd.Update();
                        Response.Redirect(Request.Url.AbsolutePath);

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




        protected void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                int id = int.Parse(hdnRowId.Value);
                hdnRowId.Value = "";
                if (!string.IsNullOrEmpty(id.ToString()))
                {
                    var menuWebTren = MenuWebTrenBLL.GetById(id);

                    if (menuWebTren == null)
                    {
                        ShowNotification("Lỗi không tìm thấy menu", false);
                        return;
                    }
                    if (!VaiTroManagerBll.AllowDelete(CurrentUserId, MenuMa))
                    {
                        ShowNotification("Bạn không có quyền truy cập chức năng này", false);
                        return;
                    }

                    MenuWebTrenBLL.Delete(id);
                    Response.Redirect(Request.Url.AbsolutePath);


                    //BindDataTree();
                    //ShowNotification("Đã xóa menu");
                    //ScriptManager.RegisterStartupScript(this, GetType(), "closeDelete", "closeDelete();", true);

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
                MenuWebTren menu = MenuWebTrenBLL.GetById(menuId);
                menu.Ten = txtEditTen.Text;
                menu.Slug = txtEditUrl.Text;
                menu.Stt = int.Parse(txtEditStt.Text);
                menu.MenuChaId = int.Parse(ddlEditMenuCha.SelectedValue);
                menu.HienThi = chkEditTrangThai.Checked;
                menu = MenuWebTrenBLL.Update(menu);

                Response.Redirect(Request.Url.AbsolutePath);
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