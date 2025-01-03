using CMS.Core.Manager;
using CMS.DataAsscess;
using CMS.WebUI.Administration.Common;
using Newtonsoft.Json;
using TBDCMS.Core.Helper;
using TBDCMS.DataAccess;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Policy;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CMS.WebUI.Administration.QuanLyCauHinh
{
    public partial class MenuTrenWeb : AdminPermistion
    {
        public override string MenuMa { get; set; } = "Menu-hien-thi-tren";
        public static string _ModalTitle = string.Empty;
        public static string _CreateDate = string.Empty;
        public static string _UpdateDate = string.Empty;
        public static string _CreateBy = string.Empty;
        public static string _UpdateBy = string.Empty;
        private List<MenuWebTren> listMenuDto = new List<MenuWebTren>();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //BindStatus();
                BindDataTree();
                BindAddUrl();
                IsAlive();
                if (!IsAlive()) Response.Redirect("~/Administration/Login.aspx", false);
            }
        }


        protected void btnRefresh_ServerClick(object sender, EventArgs e)
        {
            int _id = 0;

            try
            {
                if (int.TryParse(txtIdHidden.Value, out _id))
                {

                    txtIdHidden.Value = _id.ToString();
                    //tabBaiViet.Visible = true;
                    btnXoa.Visible = true;
                    _ModalTitle = "Cập nhật menu";
                    var objMenu = MenuWebTrenBLL.GetById(_id);
                    txtTen.Text = objMenu.Ten;
                    txtUrl.Text = objMenu.Slug;
                    txtStt.Text = objMenu.Stt.ToString();
                    chkTrangThai.Checked = objMenu.HienThi;
                    //ddlEditMenuWebTren.SelectedIndex = objMenu.MenuWebTrenChaId ?? 0;
                    //txtInfo.Visible = true;
                    _CreateDate = objMenu.NgayTao.ToString("yyyy-MM-dd");
                    //_UpdateDate = objMenu.UpdateDate.ToString("yyyy-MM-dd");
                    //_ModalTitle = objMenu.Ten;
                    //_CreateBy = objMenu.CreateBy;
                    //_UpdateBy = objMenu.UpdateBy;
                    //txtDisplayOrder.Value = objMenu.DisplayOrder.ToString();

                    //ScriptManager.RegisterStartupScript(this, GetType(), "MakeModal", "MakeModal();", true);

                }
                else
                {
                    int _idParent = 0;
                    int.TryParse(txtHidMenuIdParent.Value, out _idParent);
                   
                        MenuWebTren objMenu = MenuWebTrenBLL.GetById(_idParent);
                        if (objMenu != null)
                        {
                            _ModalTitle = objMenu.Ten+"-Menu con";

                            txtHidMenuIdParent.Value = _idParent.ToString();
                        }
                        else
                        {
                            _ModalTitle = "Thêm mới";
                            //txtHidMenuIdParent.Value = 0;
                        }

                        _CreateBy = _UpdateBy = _CreateDate = _UpdateDate = string.Empty;
                        txtTen.Text = txtUrl.Text = string.Empty;
                        txtStt.Text = "0";
                   
                    //txtTieuDe.Value = txtSlug.Value = txtEditMota.Text = txtImage.Value = string.Empty;
                    //imgThumb.Attributes["src"] = "../UploadImage/addNewImage.png"; // Reset hình ảnh
                                                                                   // Đặt trạng thái mặc định là checked
                }
                lblModalTitle.InnerText = _ModalTitle;
                UpdatePanelModal.Update();
            }
            catch (Exception ex)
            {
                OpenMessageBox(MessageBoxType.Error, ex.Message);
            }
        }

  



        private bool IsAddMenu
        {
            get
            {
                try
                {
                    return VaiTroManagerBll.AllowAdd(ApplicationContext.Current.CurrentUserID, MenuMa);
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
                    return VaiTroManagerBll.AllowEdit(ApplicationContext.Current.CurrentUserID, MenuMa);
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
                        List<MenuWebTren> lstChild = lst.Where(t => t.MenuChaId == parentId).OrderBy(x=>x.Stt).ToList();
                        if (lstChild != null && lstChild.Count > 0)
                        {
                            foreach (var item in lstChild)
                            {
                                if (item != null)
                                {
                                    ItemTreeView itemTree = new ItemTreeView();
                                    itemTree.MenuId = item.Id;
                                    //itemTree.text = string.Format("{0} {1} {2}", item.Ten, GetStatusText(BasicStatusHelper.Active), "<a class=\"btn btn-danger p-0\" style=\" font-size:14px;\"  href=\"/Administration/QuanLyCauHinh/MenuTrenWeb.aspx?modal=openDelete&idMenu=" + item.Id + "\", item.Id\" ><span class=\"fa fa-trash\"></span> Xóa</a>");
                                    itemTree.text = string.Format("{0} {1} ", item.Ten, GetStatusText(item.HienThi ? BasicStatusHelper.Active : BasicStatusHelper.InActive));
                                    itemTree.icon = "fa fa-link";
                                    itemTree.state = new ItemState { opened = true };
                                    if (true || true)
                                        itemTree.a_attr = new { href = string.Format("javascript:MakeModal('{0}',0)", item.Id) };
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
                                addChild.a_attr = new { href = string.Format("javascript:MakeModal('',{0})", parentId) };
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
                                addChild.a_attr = new { href = string.Format("javascript:MakeModal('',{0})", parentId) };
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
                //    addChild.text = "Lưu";
                //    addChild.icon = "fa fa-plus";
                //    addChild.state = new ItemState { opened = true };
                //    if (IsEditMenu)
                //        addChild.a_attr = new { href = "/Administration/QuanLyCauHinh/MenuTrenWeb?modal=openModal" };
                //    else
                //        addChild.a_attr = null;
                //    addChild.children = null;
                //    lstTree.Add(addChild);
                //}
                hdfRightsTreeViewData.Value = JsonConvert.SerializeObject(new { MenuId = 0, text = "Danh sách menu", children = lstTree, icon = "fa fa-list-ul", state = new { opened = true } });
                UpdatePanelMainTable.Update();

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



   
        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                string catIdstring = txtIdHidden.Value;
                int catId = 0;
                int.TryParse(catIdstring, out catId);


                MenuWebTren objMenu = new MenuWebTren();
                #region check valid 
                if (string.IsNullOrEmpty(txtTen.Text.Trim()) || txtTen.Text.Trim().Length < 3)
                {
                    AddErrorPrompt(txtTen.ClientID, "Không được bỏ trống trường này");
                }

               
                if (!IsValid)
                {
                    ShowErrorPrompt();
                    return;
                }
                #endregion
                if (!VaiTroManagerBll.AllowAdd(ApplicationContext.Current.CurrentUserID, MenuMa))
                {
                    OpenMessageBox(MessageBoxType.Error, MessageBoxString.ErrorPermission);
                    return;
                }
                bool isAdd = true;
                if (catId > 0)
                {
                    objMenu = MenuWebTrenBLL.GetById(catId);

                    if (objMenu == null)
                    {
                        OpenMessageBox(MessageBoxType.Error, MessageBoxString.Error);
                        return;
                    }
                    isAdd = false;
                }
               
                
                objMenu.Ten = txtTen.Text;
                objMenu.Slug = txtUrl.Text;

                objMenu.MoTa = string.Empty;
                objMenu.HienThi = chkTrangThai.Checked;
                int _display = -1;
                int.TryParse(txtStt.Text, out _display);
                objMenu.Stt = _display;
                objMenu.NgayTao = DateTime.Now;

                if (isAdd)
                {
                   
                    objMenu.LangID = ApplicationContext.Current.ContentCurrentLanguageId;
                    int _idParent = 0;
                    int.TryParse(txtHidMenuIdParent.Value, out _idParent); 
                    objMenu.MenuChaId = _idParent;
                    objMenu = MenuWebTrenBLL.Insert(objMenu);
                    //ShowNotification("Lưu thành công", false);
                    OpenMessageBox(MessageBoxType.Success, MessageBoxString.Success);
                    LichSuHeThongBLL.LogAction(LichSuHeThongType.INSERT, LichSuHeThongGroup.TopMenuManagement, objMenu.Ten);
                }
                else
                {
                    objMenu = MenuWebTrenBLL.Update(objMenu);
                    OpenMessageBox(MessageBoxType.Success, MessageBoxString.Success);
                        LichSuHeThongBLL.LogAction(LichSuHeThongType.UPDATE, LichSuHeThongGroup.TopMenuManagement, objMenu.Ten);
                       
                   
                }

                #region Update panel
                
                btnXoa.Visible = VaiTroManagerBll.AllowDelete(ApplicationContext.Current.CurrentUserID, MenuMa);
               
                // Đoạn mã trong code-behind
                string script =
                    @"
                    $(function () {
                        if ($("".rightsTreeView"").length) {
                            var jsonData = JSON.parse($('[data-selector=""hdfRightsTreeViewData""]').val());
                            renderTreeView(jsonData);
                        }
                        $('[data-selector=""hdfRightsTreeViewData""]').on('change', function () {
                            var updateJsonData = JSON.parse((this).val());
                            renderTreeView(updateJsonData);
                        });
                    });
                    ";

                // Đảm bảo JavaScript chạy bên trong UpdatePanel
                ScriptManager.RegisterStartupScript(this, this.GetType(), "UpdateUrl", script, true);
                txtIdHidden.Value = objMenu.Id.ToString();
                BindDataTree();

                UpdatePanelModal.Update();
                #endregion
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
                string catIdstring = txtIdHidden.Value;
                int id = 0;
                int.TryParse(catIdstring, out id);
                if (id <= 0)
                {
                    id = int.Parse(txtIdHidden.Value);
                }

                if (!string.IsNullOrEmpty(id.ToString()) && id > 0)
                {
                    var objMenu = MenuWebTrenBLL.GetById(id);

                    if (objMenu == null)
                    {
                        OpenMessageBox(MessageBoxType.Error, MessageBoxString.Error);

                        return;
                    }
                    if (!VaiTroManagerBll.AllowDelete(ApplicationContext.Current.CurrentUserID, MenuMa))
                    {
                        OpenMessageBox(MessageBoxType.Error, MessageBoxString.ErrorPermission);
                        return;
                    }

                    MenuWebTrenBLL.Delete(id);
                    LichSuHeThongBLL.LogAction(LichSuHeThongType.DELETE, LichSuHeThongGroup.TopMenuManagement, objMenu.Ten);
                    hdnRowId.Value = "";
                    OpenMessageBox(MessageBoxType.Success, MessageBoxString.SuccessDelete);
                    string script =
                  @"
                    $(function () {
                        if ($("".rightsTreeView"").length) {
                            var jsonData = JSON.parse($('[data-selector=""hdfRightsTreeViewData""]').val());
                            renderTreeView(jsonData);
                        }
                        $('[data-selector=""hdfRightsTreeViewData""]').on('change', function () {
                            var updateJsonData = JSON.parse((this).val());
                            renderTreeView(updateJsonData);
                        });
                    });
                    ";

                    // Đảm bảo JavaScript chạy bên trong UpdatePanel
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "UpdateUrl", script, true);
                    BindDataTree();
                    UpdatePanelMainTable.Update();

                }
                else
                {
                    OpenMessageBox(MessageBoxType.Error, MessageBoxString.Error);


                    return;
                }
            }
            catch (Exception ex)
            {
                OpenMessageBox(MessageBoxType.Error, MessageBoxString.Error);
            }

        }



        //private void BindMenuCha()
        //{
        //    List<MenuWebTren> listMenu = MenuWebTrenBLL.GetListParentMenu(ApplicationContext.Current.ContentCurrentLanguageId);
        //    List<ListItem> list = listMenu.Select(x => new ListItem(x.Ten, x.Id.ToString())).ToList();

        //    ddlAddMenuCha.Items.Clear();
        //    ddlAddMenuCha.Items.Add(new ListItem("Không có menu", "0"));
        //    ddlAddMenuCha.Items.AddRange(list.ToArray());

        //    BindAddUrl();
        //}

        private void BindAddUrl()
        {
            #region Thêm dữ liệu cho Droplist của trang Add menu
            List<BaiViet> listBaiViet = MenuWebTrenBLL.GetListBaiViet(ApplicationContext.Current.ContentCurrentLanguageId);
            List<ListItem> listAdd = listBaiViet.Select(x => new ListItem(x.TieuDe, x.Slug)).ToList();
            drAddbaiviet.Items.Clear();
            drAddbaiviet.Items.Add(new ListItem("Không", "0"));
            drAddbaiviet.Items.AddRange(listAdd.ToArray());


            List<DanhMuc> listMenuWebTren = MenuWebTrenBLL.GetListDanhMuc(ApplicationContext.Current.ContentCurrentLanguageId);
            List<ListItem> listItemDM = listMenuWebTren.Select(x => new ListItem(x.Ten, x.Slug)).ToList();
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

        

        }
    }
}



