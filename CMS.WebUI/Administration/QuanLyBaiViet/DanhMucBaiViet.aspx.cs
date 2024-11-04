using CMS.Core.Manager;
using CMS.DataAsscess;
using CMS.WebUI.Administration.Common;
using Newtonsoft.Json;
using SweetCMS.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using SweetCMS.Core.Helper;
using System.Web.UI;
using System.Web.UI.WebControls;
using static CMS.WebUI.Common.BaseAdminPage;
using System.Web.DynamicData;

namespace CMS.WebUI.Administration.QuanLyBaiViet
{
    public partial class DanhMucBaiViet : AdminPermistion
    {
        public override string MenuMa { get; set; } = "Nhom-bai-bai-viet";
        private static List<BaiVietInDanhMucDto> baivietinDanhMucCurrent = new List<BaiVietInDanhMucDto>();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindDataByQuyen();

                IsAlive();
                if (!IsAlive()) Response.Redirect("~/Administration/Login.aspx", false);
            }
        }

        private void GetModal()
        {
            string CatId = Request.QueryString["CatId"];
            if (CatId == "0")
            {
                HiddenIDDanhMuc.Value = "0";
                UpdatePanelEdit.Update();
                ScriptManager.RegisterStartupScript(this, GetType(), "OpenModal", "openModal();", true);
                return;
            }
            if (int.TryParse(CatId, out int id))
            {
                HiddenIDDanhMuc.Value = id.ToString();
                tabBaiViet.Visible = true;
                btnXoa.Visible = true;
                lblTitle.InnerText = "Cập nhật danh mục";
                var danhMuc = DanhMucBaiVietBLL.GetById(id);
                txtEditTen.Text = danhMuc.Ten;
                txtEditMa.Text = danhMuc.Slug;
                txtEditMota.Text = danhMuc.MoTa;
                ddlEditDanhMuc.SelectedIndex = danhMuc.DanhMucChaId ?? 0;
                UpdatePanelEdit.Update();
                BindBaiVietInDanhMuc(id);
                //UpdatePanelThemBaiViet.Update();
                ScriptManager.RegisterStartupScript(this, GetType(), "OpenModal", "openModal();", true);
                return;
            }
            //else
            //{
            //    ShowNotification("Danh mục này không tồn tại", false);
            //}
        }
        private void BindDataByQuyen()
        {
            BindDataTree();
            BindListDanhMucCha();
            SearchUserControl.SetSearcKey();
            GetModal();
        }

        private void BindBaiVietInDanhMuc(int id)
        {
            baivietinDanhMucCurrent = DanhMucBaiVietBLL.GetBaiVietInDanhMuc(id, ApplicationContext.Current.ContentCurrentLanguageId);
            GridBaiVietInDanhMuc.DataSource = baivietinDanhMucCurrent;
            GridBaiVietInDanhMuc.DataBind();
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
                List<DanhMuc> lst = DanhMucBaiVietBLL.GetAllNoPaging(ApplicationContext.Current.ContentCurrentLanguageId, FriendlyUrlBLL.FriendlyURLTypeHelper.Category, Request.QueryString["search"]);
                //lst = lst.Where(x => x.Id != 4).ToList();

                if (lst != null && lst.Count > 0)
                {
                    Func<int, List<ItemTreeView>> func = null;
                    func = (parentId) =>
                    {
                        List<ItemTreeView> lstTreeChild = new List<ItemTreeView>();
                        List<DanhMuc> lstChild = lst.Where(t => t.DanhMucChaId == parentId).ToList();
                        if (lstChild != null && lstChild.Count > 0)
                        {
                            foreach (var item in lstChild)
                            {
                                if (item != null)
                                {
                                    ItemTreeView itemTree = new ItemTreeView();
                                    itemTree.MenuId = item.Id;
                                    itemTree.text = string.Format("{0} {1} {2}", item.Ten, GetStatusText(BasicStatusHelper.Active), null);
                                    //itemTree.text = string.Format("{0} {1} ({2})", item.Ten, "Active", true);
                                    itemTree.icon = "fa fa-link";
                                    itemTree.state = new ItemState { opened = true };
                                    if (IsEditMenu)
                                        itemTree.a_attr = new { href = string.Format("/Administration/QuanLyBaiViet/DanhMucBaiViet.aspx?CatId={0}", item.Id) };
                                    else
                                        itemTree.a_attr = null;
                                    itemTree.children = func(item.Id);
                                    lstTreeChild.Add(itemTree);
                                }
                            }
                        }

                        return lstTreeChild;
                    };
                    lstTree = func(0);
                }
                if (IsAddMenu)
                {
                    ItemTreeView addChild = new ItemTreeView();
                    addChild.MenuId = 0;
                    addChild.text = "Thêm mới";
                    addChild.icon = "fa fa-plus";
                    addChild.state = new ItemState { opened = true };
                    if (IsAddMenu)
                        addChild.a_attr = new { href = "/Administration/QuanLyBaiViet/DanhMucBaiViet?CatId=0" };
                    else
                        addChild.a_attr = null;
                    addChild.children = null;
                    lstTree.Add(addChild);
                }
                hdfRightsTreeViewData.Value = JsonConvert.SerializeObject(new { MenuId = 0, text = "Danh sách danh mục", children = lstTree, icon = "fa fa-list-ul", state = new { opened = true } });
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


        private void BindListDanhMucCha()
        {

            List<DanhMuc> listDanhMuc = DanhMucBaiVietBLL.GetNameAndId(ApplicationContext.Current.ContentCurrentLanguageId);
            List<ListItem> list = listDanhMuc.Select(x => new ListItem(x.Ten, x.Id.ToString())).ToList();
            ddlEditDanhMuc.Items.Clear();
            ddlEditDanhMuc.Items.Add(new ListItem("Không có danh mục", "0"));
            ddlEditDanhMuc.Items.AddRange(list.ToArray());
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                string catIdstring = HiddenIDDanhMuc.Value;
                int catId = 0;
                int.TryParse(catIdstring, out catId);
                

                DanhMuc danhMuc = new DanhMuc();
                #region check valid 
                if (string.IsNullOrEmpty(txtEditTen.Text.Trim()) || txtEditTen.Text.Trim().Length < 3)
                {
                    AddErrorPrompt(txtEditTen.ClientID, "Không được bỏ trống trường này");
                }

                if (string.IsNullOrEmpty(txtEditMa.Text.Trim()) || txtEditMa.Text.Trim().Length < 3 ||
                    txtEditMa.Text.Contains(" "))
                {
                    AddErrorPrompt(txtEditMa.ClientID, "Không được bỏ trống trường này");
                }
                if (FriendlyUrlBLL.CheckExists(txtEditMa.Text.Trim(), catId))
                {
                    AddErrorPrompt(txtEditMa.ClientID, "Url này đã được sử dụng trong hệ thống");
                    txtEditMa.Focus();
                }
                if (!IsValid)
                {
                    ShowErrorPrompt();
                    return;
                }
                #endregion
                if (!VaiTroManagerBll.AllowAdd(ApplicationContext.Current.CurrentUserID, MenuMa))
                {
                    ShowNotification("Bạn không có quyền truy cập chức năng này", false);
                    ScriptManager.RegisterStartupScript(this, GetType(), "closeModal", "closeModal();", true);
                    return;
                }

                bool isAdd = true;
                if (catId > 0)
                {
                    danhMuc = DanhMucBaiVietBLL.GetById(catId);

                    if (danhMuc == null)
                    {
                        ShowNotification("Danh mục không tồn tại", false);
                        return;
                    }
                    isAdd = false;
                }

                danhMuc.Ten = txtEditTen.Text;
                danhMuc.Slug = txtEditMa.Text;
                danhMuc.DanhMucChaId = int.Parse(ddlEditDanhMuc.SelectedValue);
                danhMuc.MoTa = txtEditMota.Text;
                if (isAdd)
                {
                    danhMuc.LangID = ApplicationContext.Current.ContentCurrentLanguageId;
                    danhMuc = DanhMucBaiVietBLL.Insert(danhMuc);
                    //ShowNotification("Thêm mới thành công", false);

                }
                else
                {
                    danhMuc = DanhMucBaiVietBLL.Update(danhMuc);
                    try
                    {
                        List<BaiVietInDanhMucDto> listBaiVietInDanhMuc = new List<BaiVietInDanhMucDto>();
                        foreach (GridViewRow row in GridBaiVietInDanhMuc.Rows)
                        {
                            // Lấy giá trị từ các ô trong GridView, ví dụ:
                            int lblBaiVietId = int.Parse(((Label)row.FindControl("lblBaiVietId")).Text);
                            //int? lblDanhMucId = ((Label)row.FindControl("lblDanhMucId")).Text == null ? (int?)null : int.Parse(((Label)row.FindControl("lblBaiVietId")).Text);
                            bool daChon = ((CheckBox)row.FindControl("DaChon")).Checked;
                            listBaiVietInDanhMuc.Add(new BaiVietInDanhMucDto
                            {
                                BaiVietId = lblBaiVietId,
                                //DanhMucId = lblDanhMucId,
                                DaChon = daChon ? 1 : 0
                            });
                        }
                        DanhMucBaiVietBLL.UpdateDanhMucBaiViet(listBaiVietInDanhMuc, baivietinDanhMucCurrent, danhMuc.Id);
                        baivietinDanhMucCurrent = listBaiVietInDanhMuc;
                    }
                    catch (Exception ex)
                    {
                        ShowNotification("Cập nhật danh mục thất bại,\n lỗi:   " + ex.Message, false);
                    }
                }

                #region Update panel
                lblTitle.InnerText = "Cập nhật danh mục";
                tabBaiViet.Visible = true;
                btnXoa.Visible = VaiTroManagerBll.AllowDelete(ApplicationContext.Current.CurrentUserID, MenuMa);
                BindBaiVietInDanhMuc(danhMuc.Id);
                // Đoạn mã trong code-behind
                string script = $@"
                     window.params = window.params || new URLSearchParams(window.location.search);
                    params.set('CatId', '{danhMuc.Id}');" +
                    @" history.pushState(null, '', `${window.location.pathname}?${window.params.toString()}`);


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
                HiddenIDDanhMuc.Value = danhMuc.Id.ToString();
                BindDataTree();

                UpdatePanelEdit.Update();
                #endregion
            }

            catch (Exception ex)
            {
                ShowNotification("Thêm danh mục thất bại! \n Lỗi: " + ex.Message, false);

            }
        }


        protected void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                string catIdstring = HiddenIDDanhMuc.Value;
                int id = 0;
                int.TryParse(catIdstring, out id);
                if (id <= 0)
                {
                    id = int.Parse(HiddenIDDanhMuc.Value);
                }

                if (!string.IsNullOrEmpty(id.ToString()) && id > 0)
                {
                    var danhMuc = DanhMucBaiVietBLL.GetById(id);

                    if (danhMuc == null)
                    {
                        ShowNotification("Lỗi không tìm thấy danh mục", false);
                        ScriptManager.RegisterStartupScript(this, GetType(), "closeDelete", "closeDelete();", true);

                        return;
                    }
                    if (!VaiTroManagerBll.AllowDelete(ApplicationContext.Current.CurrentUserID, MenuMa))
                    {
                        ShowNotification("Bạn không có quyền truy cập chức năng này", false);
                        ScriptManager.RegisterStartupScript(this, GetType(), "closeDelete", "closeDelete();", true);

                        return;
                    }

                    DanhMucBaiVietBLL.Delete(id);
                    hdnRowId.Value = "";
                    Response.Redirect(Request.Url.AbsolutePath);
                    //BindDataTree();
                    //ShowNotification("Đã xóa danh mục");
                    //ScriptManager.RegisterStartupScript(this, GetType(), "closeDelete", "closeDelete();", true);

                }
                else
                {
                    ShowNotification("Lỗi không tìm thấy danh mục", false);
                    ScriptManager.RegisterStartupScript(this, GetType(), "closeDelete", "closeDelete();", true);

                    return;
                }
            }
            catch (Exception ex)
            {
                ShowNotification("Xóa thất bại! \n " + ex.Message, false);
            }

        }
        private void ShowNotification(string message, bool isSuccess = true)
        {
            AdminNotificationUserControl.LoadMessage(message, isSuccess);
        }
    }
}