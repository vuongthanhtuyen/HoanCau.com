using CMS.Core.Manager;
using CMS.DataAsscess;
using CMS.WebUI.Administration.Common;
using Newtonsoft.Json;
using SweetCMS.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using static CMS.WebUI.Common.BaseAdminPage;

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
                GetModal();
                IsAlive();
                if(!IsAlive()) Response.Redirect("~/Administration/Login.aspx", false);
            }
        }

        private void GetModal()
        {
            string modal = Request.QueryString["modal"];

            if (modal == "openModal")
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "OpenModal", "openModal();", true);
                return;
            }
            string stIdCategory = Request.QueryString["idCategory"];
            if (int.TryParse(stIdCategory, out int id))
            {
                hdnRowId.Value = id.ToString();

                if (modal == "openEdit")
                {
                    var danhMuc = DanhMucBaiVietBLL.GetById(id);
                    List<DanhMuc> listDanhMuc = DanhMucBaiVietBLL.GetNameAndId();
                    List<ListItem> list = listDanhMuc.Where(x => x.Id != danhMuc.Id).Select(x => new ListItem(x.Ten, x.Id.ToString())).ToList();
                    ddlEditDanhMuc.Items.Clear();
                    ddlEditDanhMuc.Items.Add(new ListItem("Không có danh mục", "0"));
                    ddlEditDanhMuc.Items.AddRange(list.ToArray());

                    txtEditTen.Text = danhMuc.Ten;
                    txtEditMa.Text = danhMuc.Slug;
                    txtEditMota.Text = danhMuc.MoTa;
                    ddlEditDanhMuc.SelectedIndex = danhMuc.DanhMucChaId ?? 0;
                    UpdatePanelEdit.Update();
                    baivietinDanhMucCurrent = DanhMucBaiVietBLL.GetBaiVietInDanhMuc(id);
                    GridBaiVietInDanhMuc.DataSource = baivietinDanhMucCurrent;
                    GridBaiVietInDanhMuc.DataBind();
                    //UpdatePanelThemBaiViet.Update();
                    ScriptManager.RegisterStartupScript(this, GetType(), "openEdit", "openEdit();", true);
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
        private void BindDataByQuyen()
        {

            BindGrid();
        }
        private void BindGrid(int pageIndex = 1, int pageSize = 10)
        {
            BindDataTree();
            SearchUserControl.SetSearcKey();
            BindListDanhMucCha();

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
                List<DanhMuc> lst = DanhMucBaiVietBLL.GetAllNoPaging(Request.QueryString["search"]);
                lst = lst.Where(x => x.Id != 4).ToList();

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
                                    itemTree.text = string.Format("{0} {1} {2}", item.Ten, GetStatusText(BasicStatusHelper.Active), "<a class=\"btn btn-danger p-0\" style=\" font-size:14px;\"  href=\"/Administration/QuanLyBaiViet/DanhMucBaiViet.aspx?modal=openDelete&idCategory=" + item.Id + "\", item.Id\" ><span class=\"fa fa-trash\"></span> Xóa</a>");
                                    //itemTree.text = string.Format("{0} {1} ({2})", item.Ten, "Active", true);
                                    itemTree.icon = "fa fa-link";
                                    itemTree.state = new ItemState { opened = true };
                                    if (true || true)
                                        itemTree.a_attr = new { href = string.Format("/Administration/QuanLyBaiViet/DanhMucBaiViet.aspx?modal=openEdit&idCategory={0}", item.Id) };
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
                //if (IsEditMenu)
                //{
                //    ItemTreeView addChild = new ItemTreeView();
                //    addChild.MenuId = 0;
                //    addChild.text = "Thêm mới";
                //    addChild.icon = "fa fa-plus";
                //    addChild.state = new ItemState { opened = true };
                //    if (IsEditMenu)
                //        addChild.a_attr = new { href = "/Administration/QuanLyBaiViet/DanhMucBaiViet?modal=openModal" };
                //    else
                //        addChild.a_attr = null;
                //    addChild.children = null;
                //    lstTree.Add(addChild);
                //}
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

            List<DanhMuc> listDanhMuc = DanhMucBaiVietBLL.GetNameAndId();
            List<ListItem> list = listDanhMuc.Select(x => new ListItem(x.Ten, x.Id.ToString())).ToList();
            ddlDanhMuc.Items.Clear();
            ddlDanhMuc.Items.Add(new ListItem("Không có danh mục", "0"));
            ddlDanhMuc.Items.AddRange(list.ToArray());
        }
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                DanhMuc danhMuc = new DanhMuc();
                lblAddErrorMessage.Text = "";
                if (string.IsNullOrEmpty(txtTen.Text.Trim()) || txtTen.Text.Trim().Length <= 3)
                {
                    lblAddErrorMessage.Text += "Tên danh mục không được trống hoặc bé hơn 3 ký tự<br />";
                }

                if (string.IsNullOrEmpty(txtMa.Text.Trim()) || txtMa.Text.Trim().Length <= 3 ||
                    txtMa.Text.Contains(" "))
                {
                    lblAddErrorMessage.Text += "Mã không hợp lệ";
                }

                if (lblAddErrorMessage.Text.Length > 0)
                {
                    UpdatePanelAdd.Update();
                    ScriptManager.RegisterStartupScript(this, GetType(), "OpenModal", "openModal();", true);

                }
                else
                {
                    if (FriendlyUrlBLL.GetByMa(txtMa.Text) != null)
                    {
                        lblAddErrorMessage.Text = "Url thân thiện đã tồn tại, vui lòng nhập mã khác";
                        UpdatePanelAdd.Update();
                        return;
                    }
                    if (!VaiTroManagerBll.AllowAdd(CurrentUserId, MenuMa))
                    {
                        ShowNotification("Bạn không có quyền truy cập chức năng này", false);
                        ScriptManager.RegisterStartupScript(this, GetType(), "closeModal", "closeModal();", true);

                    }
                    else
                    {


                        danhMuc.Ten = txtTen.Text;
                        danhMuc.Slug = txtMa.Text;
                        danhMuc.DanhMucChaId = int.Parse(ddlDanhMuc.SelectedValue);
                        danhMuc.MoTa = txtMota.Text;

                        danhMuc = DanhMucBaiVietBLL.Insert(danhMuc);
                        Response.Redirect(Request.Url.AbsolutePath);
                        //ScriptManager.RegisterStartupScript(this, GetType(), "CloseModal", "closeModal();", true);
                        //if (danhMuc != null)
                        //{
                        //    lblAddErrorMessage.Text = "";
                        //    ShowNotification("Thêm danh mục thành công");
                        //    txtTen.Text = string.Empty;
                        //    txtMa.Text = string.Empty;
                        //    txtMota.Text = string.Empty;
                        //    ddlDanhMuc.SelectedIndex = 0;
                        //    UpdatePanelAdd.Update();
                        //}
                        //else
                        //{
                        //    ShowNotification("Thêm danh mục thất bại", false);
                        //}
                    }

                }
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
                int id = int.Parse(hdnRowId.Value);
                if (!string.IsNullOrEmpty(id.ToString()))
                {
                    var danhMuc = DanhMucBaiVietBLL.GetById(id);

                    if (danhMuc == null)
                    {
                        ShowNotification("Lỗi không tìm thấy danh mục", false);
                        ScriptManager.RegisterStartupScript(this, GetType(), "closeDelete", "closeDelete();", true);

                        return;
                    }
                    if (!VaiTroManagerBll.AllowDelete(CurrentUserId, MenuMa))
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

                #region Cập nhật danh mục
                if (!VaiTroManagerBll.AllowEdit(CurrentUserId, MenuMa))
                {
                    ShowNotification("Bạn không có quyền truy cập chức năng này", false);
                    return;
                }
                int danhMucId = int.Parse(hdnRowId.Value);
                
                DanhMuc danhMuc = DanhMucBaiVietBLL.GetById(danhMucId);


                if (danhMuc.Slug != txtEditMa.Text)
                {
                    var friendlyUrl = FriendlyUrlBLL.GetByMa(txtEditMa.Text);
                    if (friendlyUrl != null)
                    {

                        lblEditErrorMessage.Text = "Url thân thiện này đã tồn tại, vui lòng nhập url khác!";
                        ScriptManager.RegisterStartupScript(this, GetType(), "openEdit", "openEdit();", true);

                        return;
                    }
                }

                danhMuc.Ten = txtEditTen.Text;
                danhMuc.Slug = txtEditMa.Text;
                danhMuc.MoTa = txtEditMota.Text;
                danhMuc.DanhMucChaId = int.Parse(ddlEditDanhMuc.SelectedValue);
                danhMuc = DanhMucBaiVietBLL.Update(danhMuc);
                hdnRowId.Value = "";
                #endregion


                #region Cập nhật thêm bài viết cho danh mục

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

                  
                    ShowNotification(DanhMucBaiVietBLL.UpdateDanhMucBaiViet(listBaiVietInDanhMuc, baivietinDanhMucCurrent, danhMucId));
                    baivietinDanhMucCurrent = listBaiVietInDanhMuc;
                }
                catch (Exception ex)
                {
                    ShowNotification("Cập nhật danh mục thất bại,\n lỗi:   " + ex.Message, false);
                }
                #endregion

                Response.Redirect(Request.Url.AbsolutePath);

                //BindDataByQuyen();
                //ScriptManager.RegisterStartupScript(this, GetType(), "closeEdit", "closeEdit();", true);

                //ShowNotification("Cập nhật thành công", true);
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