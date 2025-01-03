using CMS.Core.Manager;
using CMS.DataAsscess;
using CMS.WebUI.Administration.Common;
using Newtonsoft.Json;
using TBDCMS.Core.Helper;
using TBDCMS.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CMS.WebUI.Administration.QuanLyDuAnTieuBieu
{
    public partial class DanhMucDuAnTieuBieu : AdminPermistion
    {
        public override string MenuMa { get; set; } = "Danh-Muc-bai-du-an-tieu-bieu";
        public static string _ModalTitle = string.Empty;
        public static string _CreateDate = string.Empty;
        public static string _UpdateDate = string.Empty;
        public static string _CreateBy = string.Empty;
        public static string _UpdateBy = string.Empty;
        private static List<BaiVietInDanhMucDto> baivietinDanhMucCurrent = new List<BaiVietInDanhMucDto>();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindStatus();
                BindDataTree();

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
                    _ModalTitle = "Cập nhật nhóm sự kiện";
                    var danhMuc = DanhMucBaiVietBLL.GetById(_id);
                    txtTieuDe.Value = danhMuc.Ten;
                    txtSlug.Value = danhMuc.Slug;
                    ddlStatus.SelectedValue = danhMuc.Status;
                    txtEditMota.Text = danhMuc.MoTa;
                    //ddlEditDanhMuc.SelectedIndex = danhMuc.DanhMucChaId ?? 0;
                    imgThumb.Src = Helpers.GetThumbnailUrl(danhMuc.ThumbnailUrl);
                    BindBaiVietInDanhMuc(_id);
                    txtInfo.Visible = true;
                    _CreateDate = danhMuc.CreateDate.ToString("yyyy-MM-dd");
                    _UpdateDate = danhMuc.UpdateDate.ToString("yyyy-MM-dd");
                    _ModalTitle = danhMuc.Ten;
                    _CreateBy = danhMuc.CreateBy;
                    _UpdateBy = danhMuc.UpdateBy;
                    txtDisplayOrder.Value = danhMuc.DisplayOrder.ToString();

                    //ScriptManager.RegisterStartupScript(this, GetType(), "MakeModal", "MakeModal();", true);

                }
                else
                {
                    txtInfo.Visible = false;
                    //tabBaiViet.Visible = false;
                    txtDisplayOrder.Value = "-1";
                    txtTieuDe.Value = txtSlug.Value = string.Empty;
                    _ModalTitle = "Thêm mới";
                    _CreateBy = _UpdateBy = _CreateDate = _UpdateDate = string.Empty;
                    ddlStatus.SelectedValue = string.Empty;
                    txtTieuDe.Value = txtSlug.Value = txtEditMota.Text = txtImage.Value = string.Empty;
                    imgThumb.Attributes["src"] = "../UploadImage/addNewImage.png"; // Reset hình ảnh
                                                                                   // Đặt trạng thái mặc định là checked
                }

                UpdatePanelModal.Update();
            }
            catch (Exception ex)
            {
                OpenMessageBox(MessageBoxType.Error, ex.Message);
            }
        }

        private void BindDataByQuyen()
        {

            //BindListDanhMucCha();

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
                List<DanhMuc> lst = DanhMucBaiVietBLL.GetAllNoPaging(ApplicationContext.Current.ContentCurrentLanguageId, CategoryType.NhomSuKien, Request.QueryString["search"]);
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

                                        itemTree.a_attr = new { href = string.Format("javascript:MakeModal('{0}')", item.Id) };
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
                        addChild.a_attr = new { href = "javascript:MakeModal('')" };
                    else
                        addChild.a_attr = null;
                    addChild.children = null;
                    lstTree.Add(addChild);
                }
                hdfRightsTreeViewData.Value = JsonConvert.SerializeObject(new { MenuId = 0, text = "Danh sách sự kiện", children = lstTree, icon = "fa fa-list-ul", state = new { opened = true } });
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



        private void BindStatus()
        {
            try
            {
                ddlStatus.Items.Clear();
                ddlStatus.Items.Add(new ListItem("Tất cả", string.Empty));
                ddlStatus.Items.Add(new ListItem("Hoạt động", BasicStatusHelper.Active.ToString()));
                ddlStatus.Items.Add(new ListItem("Khóa", BasicStatusHelper.InActive.ToString()));
            }
            catch (Exception exc)
            {

                //ProcessException(exc);
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                string catIdstring = txtIdHidden.Value;
                int catId = 0;
                int.TryParse(catIdstring, out catId);


                DanhMuc danhMuc = new DanhMuc();
                #region check valid 
                if (string.IsNullOrEmpty(txtTieuDe.Value.Trim()) || txtTieuDe.Value.Trim().Length < 3)
                {
                    AddErrorPrompt(txtTieuDe.ClientID, "Không được bỏ trống trường này");
                }

                if (string.IsNullOrEmpty(txtSlug.Value.Trim()) || txtSlug.Value.Trim().Length < 3 ||
                    txtSlug.Value.Contains(" "))
                {
                    AddErrorPrompt(txtSlug.ClientID, "Không được bỏ trống trường này");
                }
                else if (FriendlyUrlBLL.CheckExists(txtSlug.Value.Trim(), catId))
                {
                    AddErrorPrompt(txtSlug.ClientID, "Url này đã được sử dụng trong hệ thống");
                    txtSlug.Focus();
                }
                if (!IsValid)
                {
                    //txtImage.Value = txtImage.Value;
                    imgThumb.Src = txtImage.Value;
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
                    danhMuc = DanhMucBaiVietBLL.GetById(catId);

                    if (danhMuc == null)
                    {
                        OpenMessageBox(MessageBoxType.Error, MessageBoxString.Error);
                        return;
                    }
                    isAdd = false;
                }
                if (!string.IsNullOrEmpty(txtImage.Value.Trim()))
                {
                    danhMuc.ThumbnailUrl = Helpers.ConvertToSavePath(txtImage.Value.Trim(), true);
                }
                danhMuc.Ten = txtTieuDe.Value;
                danhMuc.Slug = txtSlug.Value;
                danhMuc.Type = CategoryType.NhomSuKien;
                danhMuc.DanhMucChaId = 0;
                danhMuc.MoTa = txtEditMota.Text;
                danhMuc.Status = ddlStatus.SelectedValue;
                int _display = -1;
                int.TryParse(txtDisplayOrder.Value, out _display);
                danhMuc.DisplayOrder = _display;
                danhMuc.UpdateDate = DateTime.Now;
                danhMuc.UpdateBy = NguoiDungManagerBLL.GetById(ApplicationContext.Current.CurrentUserID).TenTruyCap;

                if (isAdd)
                {
                    danhMuc.CreateDate = DateTime.Now;
                    danhMuc.CreateBy = NguoiDungManagerBLL.GetById(ApplicationContext.Current.CurrentUserID).TenTruyCap;
                    danhMuc.LangID = ApplicationContext.Current.ContentCurrentLanguageId;
                    danhMuc = DanhMucBaiVietBLL.Insert(danhMuc);
                    //ShowNotification("Lưu thành công", false);
                    OpenMessageBox(MessageBoxType.Success, MessageBoxString.Success);
                    LichSuHeThongBLL.LogAction(LichSuHeThongType.INSERT, LichSuHeThongGroup.QuanLyBaiViet, danhMuc.Ten);
                }
                else
                {
                    danhMuc = DanhMucBaiVietBLL.Update(danhMuc);
                    OpenMessageBox(MessageBoxType.Success, MessageBoxString.Success);
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
                        LichSuHeThongBLL.LogAction(LichSuHeThongType.UPDATE, LichSuHeThongGroup.QuanLyBaiViet, danhMuc.Ten);
                        baivietinDanhMucCurrent = listBaiVietInDanhMuc;
                    }
                    catch (Exception ex)
                    {
                        OpenMessageBox(MessageBoxType.Error, MessageBoxString.Error);
                    }
                }

                #region Update panel
                //tabBaiViet.Visible = true;
                btnXoa.Visible = VaiTroManagerBll.AllowDelete(ApplicationContext.Current.CurrentUserID, MenuMa);
                BindBaiVietInDanhMuc(danhMuc.Id);
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
                txtIdHidden.Value = danhMuc.Id.ToString();
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
                    var danhMuc = DanhMucBaiVietBLL.GetById(id);

                    if (danhMuc == null)
                    {
                        OpenMessageBox(MessageBoxType.Error, MessageBoxString.Error);

                        return;
                    }
                    if (!VaiTroManagerBll.AllowDelete(ApplicationContext.Current.CurrentUserID, MenuMa))
                    {
                        OpenMessageBox(MessageBoxType.Error, MessageBoxString.ErrorPermission);
                        return;
                    }

                    DanhMucBaiVietBLL.Delete(id);
                    LichSuHeThongBLL.LogAction(LichSuHeThongType.DELETE, LichSuHeThongGroup.QuanLyBaiViet, danhMuc.Ten);
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

    }
}