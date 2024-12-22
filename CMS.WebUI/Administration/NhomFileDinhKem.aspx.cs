using CMS.Core.Manager;
using CMS.DataAsscess;
using CMS.WebUI.Administration.Common;
using Newtonsoft.Json;
using SweetCMS.Core.Helper;
using SweetCMS.DataAccess;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using static CMS.WebUI.Common.BaseAdminPage;

namespace CMS.WebUI.Administration
{
    public partial class NhomFileDinhKem : AdminPermistion
    {
        public override string MenuMa { get; set; } = "Quan-ly-file-dinh-kem";
        public static string _ModalTitle = string.Empty;
        public static string _CreateDate = string.Empty;
        public static string _UpdateDate = string.Empty;
        public static string _CreateBy = string.Empty;
        public static string _UpdateBy = string.Empty;
        private static List<BaiVietInDanhMucDto> baivietinDanhMucCurrent = new List<BaiVietInDanhMucDto>();
        private static List<ItemFile> itemAttachments;
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
                    tabBaiViet.Visible = true;
                    btnXoa.Visible = true;
                    _ModalTitle = "Cập nhật danh mục";
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
                    itemAttachments = FileAttactmentBLL.GetAllByTypeAndCateId(FileAttactmentType.FileDinhKem, danhMuc.Id);
                    ltrFileUpload.Text = GetFileUpload(itemAttachments);
                    divSlug.Visible = divThumb.Visible = danhMuc.DanhMucChaId == 0 ? true : false;
                    //int _idParent = 0;
                    txtHiddenIdParent.Value = danhMuc.DanhMucChaId.ToString();

                }
                else
                {
                    txtInfo.Visible = false;
                    tabBaiViet.Visible = false;
                    txtDisplayOrder.Value = "-1";
                    //_ModalTitle = "Thêm mới";
                    _CreateBy = _UpdateBy = _CreateDate = _UpdateDate = string.Empty;
                    ddlStatus.SelectedValue = string.Empty;
                    txtTieuDe.Value = txtSlug.Value = txtEditMota.Text = txtImage.Value = string.Empty;
                    imgThumb.Attributes["src"] = "../UploadImage/addNewImage.png"; // Reset hình ảnh
                    
                    int _idParent = 0;
                   int.TryParse(txtHiddenIdParent.Value, out _idParent);

                    DanhMuc objDanhMuc = DanhMucBaiVietBLL.GetById(_idParent);
                    if (objDanhMuc != null)
                    {
                        _ModalTitle = objDanhMuc.Ten + " - danh mục con";
                        divSlug.Visible =divThumb.Visible = false;
                        txtHiddenIdParent.Value = _idParent.ToString();
                    }
                    else
                    {
                        _ModalTitle = "Thêm mới";
                        divSlug.Visible = divThumb.Visible = true;
                        //txtHiddenIdParent.Value = 0;
                    }
                }
                //lblModalTitle.InnerText = _ModalTitle;
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


        private void BindBaiVietInDanhMuc(int id)
        {
            baivietinDanhMucCurrent = DanhMucBaiVietBLL.GetBaiVietInDanhMuc(id, ApplicationContext.Current.ContentCurrentLanguageId);
            //GridBaiVietInDanhMuc.DataSource = baivietinDanhMucCurrent;
            //GridBaiVietInDanhMuc.DataBind();
        }



        private bool IsAdMenu
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

        private bool IsEditDanhMuc
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
                List<DanhMuc> lst = DanhMucBaiVietBLL.GetAllNoPaging(ApplicationContext.Current.ContentCurrentLanguageId, CategoryType.FileAttactment, Request.QueryString["search"]);
                //lst = lst.Where(x => x.Id != 4).ToList();

                if (lst != null && lst.Count > 0)
                {
                    Func<int, List<ItemTreeView>> func = null;
                    func = (parentId) =>
                    {
                        List<ItemTreeView> lstTreeChild = new List<ItemTreeView>();
                        List<DanhMuc> lstChild = lst.Where(t => t.DanhMucChaId == parentId).OrderBy(x => x.DisplayOrder).ToList();
                        if (lstChild != null && lstChild.Count > 0)
                        {
                            foreach (var item in lstChild)
                            {
                                if (item != null)
                                {
                                    ItemTreeView itemTree = new ItemTreeView();
                                    itemTree.DanhMucId = item.Id;
                                    //itemTree.text = string.Format("{0} {1} {2}", item.Ten, GetStatusText(BasicStatusHelper.Active), "<a class=\"btn btn-danger p-0\" style=\" font-size:14px;\"  href=\"/Administration/QuanLyCauHinh/DanhMucTrenWeb.aspx?modal=openDelete&iMenu=" + item.Id + "\", item.Id\" ><span class=\"fa fa-trash\"></span> Xóa</a>");
                                    itemTree.text = string.Format("{0} {1} ", item.Ten, GetStatusText(item.Status));
                                    itemTree.icon = "fa fa-link";
                                    itemTree.state = new ItemState { opened = true };
                                    if (true || true)
                                        itemTree.a_attr = new { href = string.Format("javascript:MakeModal('{0}',0)", item.Id) };
                                    else
                                        itemTree.a_attr = null;
                                    itemTree.children = func(item.Id);
                                    lstTreeChild.Add(itemTree);
                                }
                                ItemTreeView _addChild = new ItemTreeView();
                                _addChild.DanhMucId = 0;
                                _addChild.text = "Thêm";
                                _addChild.icon = "fa fa-plus";
                                _addChild.state = new ItemState { opened = true };
                                if (IsAdMenu)
                                    _addChild.a_attr = new { href = string.Format("javascript:MakeModal('',{0})", item.Id) };
                                else
                                    _addChild.a_attr = null;
                                _addChild.children = null;
                                lstTreeChild.Add(_addChild);
                            }
                            //ItemTreeView addChild = new ItemTreeView();
                            //addChild.DanhMucId = parentId;
                            //addChild.text = "Thêm mới";
                            //addChild.icon = "fa fa-plus";
                            //addChild.state = new ItemState { opened = true };
                            //if (IsEditDanhMuc)
                            //    addChild.a_attr = new { href = string.Format("javascript:MakeModal('',{0})", parentId) };
                            //else
                            //    addChild.a_attr = null;
                            //addChild.children = null;
                            //lstTreeChild.Add(addChild);
                        }
                        else if (lst.Where(x => x.DanhMucChaId == 0 && parentId == x.Id).Count() > 0)
                        {
                            ItemTreeView addChild = new ItemTreeView();
                            addChild.DanhMucId = parentId;
                            addChild.text = "Thêm mới";
                            addChild.icon = "fa fa-plus";
                            addChild.state = new ItemState { opened = true };
                            if (IsEditDanhMuc)
                                addChild.a_attr = new { href = string.Format("javascript:MakeModal('',{0})", parentId) };
                            else
                                addChild.a_attr = null;
                            addChild.children = null;
                            lstTreeChild.Add(addChild);
                        }
                        //ItemTreeView addChild = new ItemTreeView();
                        //addChild.DanhMucId = parentId;
                        //addChild.text = "Thêm mới";
                        //addChild.icon = "fa fa-plus";
                        //addChild.state = new ItemState { opened = true };
                        //if (IsEditDanhMuc)
                        //    addChild.a_attr = new { href = string.Format("javascript:MakeModal('',{0})", parentId) };
                        //else
                        //    addChild.a_attr = null;
                        //addChild.children = null;
                        //lstTreeChild.Add(addChild);

                        return lstTreeChild;
                    };
                    lstTree = func(0);
                }
              
                hdfRightsTreeViewData.Value = JsonConvert.SerializeObject(new { DanhMucId = 0, text = "Danh sách file", children = lstTree, icon = "fa fa-list-ul", state = new { opened = true } });
                UpdatePanelMainTable.Update();

            }
            catch
            {

            }
        }


        private class ItemTreeView
        {
            public int DanhMucId { get; set; }
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
                int _idParent = 0;
                int.TryParse(txtHiddenIdParent.Value, out _idParent);

                DanhMuc danhMuc = new DanhMuc();
                #region check valid 
                if (string.IsNullOrEmpty(txtTieuDe.Value.Trim()) || txtTieuDe.Value.Trim().Length < 3)
                {
                    AddErrorPrompt(txtTieuDe.ClientID, "Không được bỏ trống trường này");
                }

                if (_idParent == 0) {
                    if (string.IsNullOrEmpty(txtSlug.Value.Trim()))
                    {
                        AddErrorPrompt(txtSlug.ClientID, "Không được bỏ trống trường này");
                    }
                    else if (FriendlyUrlBLL.CheckExists(txtSlug.Value.Trim(), catId))
                    {
                        AddErrorPrompt(txtSlug.ClientID, "Url này đã được sử dụng trong hệ thống");
                        txtSlug.Focus();
                    }
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
                
                danhMuc.Type = CategoryType.FileAttactment;
                
                danhMuc.MoTa = txtEditMota.Text;
                danhMuc.Status = string.IsNullOrEmpty(ddlStatus.SelectedValue)? BasicStatusHelper.Active : ddlStatus.SelectedValue;
                int _display = -1;
                int.TryParse(txtDisplayOrder.Value, out _display);
                danhMuc.DisplayOrder = _display;
                danhMuc.UpdateDate = DateTime.Now;
                danhMuc.UpdateBy = NguoiDungManagerBLL.GetById(ApplicationContext.Current.CurrentUserID).TenTruyCap;
                    danhMuc.Slug = _idParent==0? txtSlug.Value: string.Empty;

                if (isAdd)
                {
                    //int _idParent = 0;
                    //int.TryParse(txtHiddenIdParent.Value, out _idParent);
                    danhMuc.DanhMucChaId = _idParent;
                    danhMuc.CreateDate = DateTime.Now;
                    danhMuc.CreateBy = NguoiDungManagerBLL.GetById(ApplicationContext.Current.CurrentUserID).TenTruyCap;
                    danhMuc.LangID = ApplicationContext.Current.ContentCurrentLanguageId;
                    danhMuc = FileAttactmentBLL.InsertDanhMuc(danhMuc);
                    //ShowNotification("Lưu thành công", false);
                    OpenMessageBox(MessageBoxType.Success, MessageBoxString.Success);
                    LichSuHeThongBLL.LogAction(LichSuHeThongType.INSERT, LichSuHeThongGroup.QuanLyFileDinhKem, danhMuc.Ten);
                }
                else
                {
                    danhMuc = FileAttactmentBLL.UpdateDanhMuc(danhMuc);
                    OpenMessageBox(MessageBoxType.Success, MessageBoxString.Success);
                    #region update AttachmentFile
                    List<ItemFile> listAtt = JsonConvert.DeserializeObject<List<ItemFile>>(txtlistFileUploadJson.Value);
                    if (listAtt != null && listAtt.Count > 0)
                    {
                        int id = 0;

                        foreach (ItemFile newAtt in listAtt)
                        {
                            if (int.TryParse(newAtt.AttachmentFileIdString, out id))
                            {
                                var updateObj = FileAttactmentBLL.GetAttachmentFilebyPostIdAndTypeAndId(danhMuc.Id, FileAttactmentType.FileDinhKem, id);
                                if (updateObj != null)
                                {
                                    updateObj.Title = newAtt.Title;
                                    updateObj.FileUrl = newAtt.FileUrl;
                                    updateObj = FileAttactmentBLL.UpdateAttachmentFileInPost(updateObj);
                                    itemAttachments = itemAttachments.Where(t => t.Id != id).ToList();
                                }

                            }
                            else
                            {
                                FileAttactment attachmentFile = new FileAttactment()
                                {
                                    CategoryId = danhMuc.Id,
                                    Type = FileAttactmentType.FileDinhKem,
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
                            FileAttactmentBLL.DeleteAllAttachmentFileInPost(danhMuc.Id, FileAttactmentType.FileDinhKem);
                        }
                    }
                    #endregion
                }

                #region Update panel
                tabBaiViet.Visible = true;
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

                    FileAttactmentBLL.DeleteDanhMuc(id);
                    LichSuHeThongBLL.LogAction(LichSuHeThongType.DELETE, LichSuHeThongGroup.QuanLyFileDinhKem, danhMuc.Ten);
                    
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