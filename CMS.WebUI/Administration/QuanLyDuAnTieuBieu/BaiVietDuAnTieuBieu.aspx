<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BaiVietDuAnTieuBieu.aspx.cs" MasterPageFile="~/Administration/MasterPage/AdminPage.Master" Inherits="CMS.WebUI.Administration.QuanLyDuAnTieuBieu.BaiVietDuAnTieuBieu" %>

<%@ Import Namespace="SweetCMS.Core.Helper" %>
<%@ Register Src="~/Administration/AdminUserControl/PagingAdmin.ascx" TagPrefix="uc1" TagName="PagingAdmin" %>
<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="head" runat="server">
    <link href="/Administration/Style/plugins/lightbox-evolution-1.8/theme/default/jquery.lightbox.css" rel="stylesheet" />
    <link href="/Administration/Style/dist/css/imgareaselect-default.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="card card-primary card-outline">
        <div class="card-header justify-content-between">
            <h3 class="card-title">
                <i class="fas fa-edit"></i>
                Danh sách sự kiện
            </h3>
            <button type="button" onclick="MakeModal()" class="btn btn-primary col-2 float-sm-right">
                Thêm mới
            </button>
        </div>
        <div class="card-body">
            <div class="col-xs-12 padding-none header-controls-right">

                <asp:Label ID="lblResult" CssClass="text-info" runat="server" Text=""></asp:Label>
            </div>
            <br />
            <asp:UpdatePanel ID="UpdatePanelMainTable" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <asp:GridView ID="GridViewTable" runat="server" AutoGenerateColumns="false"
                        CssClass="table table-striped table-bordered" CellPadding="10" CellSpacing="2"
                        GridLines="None" OnRowCommand="GridViewTable_RowCommand">
                        <Columns>
                            <asp:TemplateField HeaderText="STT">
                                <ItemTemplate>
                                    <%# (int)ViewState["LastIndex"] + (Container.DataItemIndex + 1) %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="TieuDe" HeaderText="Tiêu đề" />
                            <asp:TemplateField HeaderText="Nhóm sự kiện">
                                <ItemTemplate>
                                    <asp:LinkButton ID="ChinhSuaDanhMuc" runat="server"
                                        Text='<%# Eval("TenDanhMuc") ?? "Không có nhóm sự kiện"  %>'
                                        CommandArgument='<%# Eval("Id") %>'
                                        CommandName="ChinhSuaDanhMuc">
                                    </asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="ViewCount" HeaderText="Lượt xem" />
                            <asp:TemplateField HeaderText="Hiển thị">
                                <ItemTemplate>
                                    <asp:CheckBox ID="TrangThai" runat="server"
                                        Checked='<%# Eval("TrangThai") %>' Enabled="False" />
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Hành Động" HeaderStyle-Width="320px">
                                <ItemTemplate>
                                    <a class="btn btn-primary" onclick="MakeModal('<%# Eval("Id") %>');">
                                        <i class="fa fa-edit"></i>
                                    </a>
                                    <asp:LinkButton ID="Xoa" runat="server" CssClass="btn btn-danger"
                                        CommandArgument='<%# Eval("Id") %>' ToolTip="Xóa" CommandName="Xoa">
                                    <span class="fa fa-trash"></span> 
                                    </asp:LinkButton>
                                    <asp:LinkButton ID="AlbumAnh" runat="server" CssClass="btn btn-primary"
                                        CommandArgument='<%# Eval("Id") %>' ToolTip="Album" CommandName="AlbumAnh">
                                        <span class="fa fa-plus"></span> Ảnh
                                    </asp:LinkButton>
                                    <asp:LinkButton ID="AlbumVideo" runat="server" CssClass="btn btn-primary"
                                        CommandArgument='<%# Eval("Id") %>' ToolTip="Album" CommandName="AlbumVideo">
                                        <span class="fa fa-plus"></span> Video
                                    </asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </ContentTemplate>
            </asp:UpdatePanel>



            <uc1:PagingAdmin runat="server" ID="PagingAdminWeb" />
            <asp:UpdatePanel ID="UpdatePanelID" runat="server">
                <ContentTemplate>
                    <asp:HiddenField ID="hdnRowId" runat="server" />
                </ContentTemplate>
            </asp:UpdatePanel>
            <div class="text-muted mt-3">
            </div>
        </div>
        <!-- /.card -->
    </div>

    <script>

        function MakeModal($id) {
            $('[data-selector="txtIdHidden"]').val($id);
            $('[data-selector="btnRefresh"]')[0].click();
            $('#myModal').modal('show');
        };

        function closeModal() {
            $('#myModal').modal('hide');
        }
        function openDelete() {
            $('#confirmDeleteModal').modal('show');
            return false;
        }

        function closeDelete() {
            $('#confirmDeleteModal').modal('hide');
        }
        function openDanhMucEditModal(id) {
            $('#danhMucEditModal').modal('show');
            return false;
        }

        function closeDanhMucEditModal() {
            $('#danhMucEditModal').modal('hide');
        }
        function openUploadAlbum(id) {
            $('#uploadAlbum').modal('show');
            return false;
        }

        function closeUploadAlbum() {
            $('#uploadAlbum').modal('hide');
        }

        function openUploadVideo(id) {
            $('#uploadVideo').modal('show');
            return false;
        }
        function closeUploadVideo() {
            $('#uploadVideo').modal('hide');
        }
    </script>
</asp:Content>

<asp:Content runat="server" ID="Content1" ContentPlaceHolderID="ModalContent">
    <%-- Add or update --%>
    <div class="modal fade" id="myModal" style="display: none;" aria-hidden="true">
        <div class="modal-dialog modal-xl">
            <div class="modal-content">
                <asp:UpdatePanel ID="UpdatePanelModal" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <div class="modal-header">

                            <h4 runat="server" id="lblModalTitle"></h4>
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                <span aria-hidden="true">×</span>
                            </button>
                        </div>
                        <div class="modal-body">
                            <div class="row justify-content-center main-edit-modal">
                                <!-- Form group for Post Title and Slug -->
                                <div class="col-md-8">
                                    <div class="form-group row">
                                        <div class="col-md-6">
                                            <label for="txtTieuDe ">Tiêu đề sự kiện</label>
                                            <input id="txtTieuDe" onkeyup="CreateFriendlyUrl(this);" runat="server" class="validate[required] form-control form-control-user" placeholder="Tiêu đề sự kiện" />
                                        </div>
                                        <div class="col-md-6">
                                            <label for="txtFriendlyURL">Slug</label>
                                            <input data-selector="txtFriendlyURL" id="txtSlug" runat="server" class="validate[required] form-control form-control-user" placeholder="Slug" />
                                        </div>
                                    </div>
                                    <!-- Form group for Short Description and Main Content -->

                                    <div class="form-group">
                                        <label for="txtMoTaNgan">Mô tả ngắn</label>
                                        <CKEditor:CKEditorControl ID="txtMoTaNgan" Width="100%" CssClass="ck-editor"
                                            Toolbar="Full" BodyId="StaticPageContent" Language="en-US" AutoParagraph="false"
                                            BasePath="/Administration/Style/plugins/ckeditor/" runat="server" Height="300">
                                        </CKEditor:CKEditorControl>
                                    </div>
                                    <div class="form-group">
                                        <label for="txtNoiDungChinh">Nội dung chính</label>
                                        <CKEditor:CKEditorControl ID="txtNoiDungChinh" Width="100%" CssClass="ck-editor"
                                            Toolbar="Full" BodyId="StaticPageContent" Language="en-US" AutoParagraph="false"
                                            BasePath="/Administration/Style/plugins/ckeditor/" runat="server" Height="300">
                                        </CKEditor:CKEditorControl>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label class="control-label">Hình minh họa</label>
                                        <label style="margin-left: 5px;" class="small">(Click vào hình ảnh để thay đổi hình minh họa)</label>
                                        <div class="row text-center d-flex justify-content-center">
                                            <div class="img-thumbnail">
                                                <img data-selector="imgThumb" id="imgThumb" runat="server" onclick="OpenSelectImage();" src="../UploadImage/addNewImage.png"
                                                    style="cursor: pointer; max-width: 300px" class="imgthumb img-responsive" />
                                            </div>
                                            <input style="display: none;" data-selector="txtImage" type="text" id="txtImage" runat="server" class="form-control" />
                                            <div id="divRemoveThumb" runat="server" visible="false">
                                                <a onclick="RemoveThumbnail();" title="Xóa hình mình họa" class="btn btn-default btn-flat btn-sm">Xóa hình minh họa</a>
                                            </div>
                                        </div>
                                        <div class="clearfix"></div>
                                    </div>
                                    <div class="form-group row">
                                        <div class="col-md-6">
                                            <label for="chkTrangThai" class="mb-3">Nổi bật</label>
                                            <br />
                                            <asp:CheckBox ID="chkTrangThai" runat="server" CssClass="form-control-user" Checked="true" />
                                            <label class="form-check-label" for="chkTrangThai">Hiển thị</label>
                                        </div>
                                        <div class="col-md-6">
                                            <label for="txtViewCount">Trạng thái</label>

                                            <asp:DropDownList ID="ddlStatus" runat="server" CssClass="form-control form-control-user">
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-md-6">
                                            <label for="txtViewCount">Lượt xem</label>
                                            <input id="txtViewCount" runat="server" textmode="Number" class="form-control form-control-user" placeholder="Lượt xem" text="0" />
                                        </div>
                                        <div class="col-md-6">
                                            <label for="txtDisplayOrder">Thứ tự hiển thị</label>
                                            <input id="txtDisplayOrder" runat="server" class="form-control form-control-user" placeholder="Thứ tự hiển thị" value="-1" type="number" />
                                        </div>
                                        <div class="col-md-12" runat="server" visible="false">
                                            <label for="txtDisplayOrder">Ngày đăng (công khai)</label>
                                            <input id="txtNgayDangCongKhai" runat="server" class="form-control form-control-user" type="date" />
                                        </div>

                                    </div>
                                    <div class="form-group" id="txtInfo" runat="server" visible="false">
                                        <label>Người tạo: <%= _CreateBy %></label>
                                        <br />
                                        <label>Ngày tạo: <%= _CreateDate %></label><br />
                                        <label>Người cập nhật: <%= _UpdateBy %></label><br />
                                        <label>Ngày cập nhật: <%= _UpdateDate %></label>

                                    </div>
                                </div>
                            </div>

                            <div class="d-flex justify-content-end">
                            </div>

                        </div>
                        <div class="modal-footer justify-content-between">
                            <input runat="server" id="txtIdHidden" data-selector="txtIdHidden" class="hidden" />
                            <a runat="server" id="btnRefresh" data-selector="btnRefresh" onserverclick="btnRefresh_ServerClick"
                                class="hidden"></a>

                            <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                            <asp:Button ID="btnSendServer" runat="server" Text="Lưu" class="btn btn-primary btn-user mx-1"
                                OnClick="btnSendServer_Click" />
                        </div>

                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="Button3" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>

    <%-- Edit Role Modal --%>

    <div class="modal fade" id="danhMucEditModal" style="display: none;" aria-hidden="true">
        <div class="modal-dialog modal-lg">
            <asp:UpdatePanel ID="UpdatepanelEidtRole" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <div class="modal-content">
                        <div class="modal-header">
                            <h4 class="modal-title">Thêm danh mục cho sự kiện</h4>
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                <span aria-hidden="true">×</span>
                            </button>
                        </div>
                        <div class="modal-body">
                            <input id="txtEditRoleIdUser" runat="server" class="form-control" placeholder="IdUser" visible="false" />
                            <asp:GridView ID="GridViewDanhMuc" runat="server" AutoGenerateColumns="false"
                                CssClass="table table-striped table-bordered" CellPadding="10" CellSpacing="2"
                                GridLines="None">
                                <Columns>
                                    <asp:TemplateField HeaderText="Id" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDanhMucId" runat="server" Text='<%# Eval("Id")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <%--<asp:BoundField DataField="Id" HeaderText="Id" />--%>
                                    <asp:BoundField DataField="Ten" HeaderText="Tên danh mục" />
                                    <asp:TemplateField HeaderText="Chọn danh mục">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkIsHaveDanhMuc" runat="server"
                                                Checked='<%# Eval("IsHaveDanhMuc").ToString() == "1" %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                        <div class="modal-footer justify-content-end">
                            <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                            <asp:Button ID="btnEditDanhMuc" runat="server" Text="Lưu" class="btn btn-primary" OnClick="btnDanhMucEditSave_Click" />

                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>
 
    <%-- Modal delete comfirm --%>

    <div class="modal fade" id="confirmDeleteModal" style="display: none;" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h4 class="modal-title">Thông báo</h4>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">×</span>
                    </button>
                </div>
                <div class="modal-body">
                    <p>Bạn có chắc muốn xóa đối tượng này? </p>
                </div>
                <div class="modal-footer justify-content-end">
                    <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                    <asp:Button ID="Button3" runat="server" Text="Xóa" class="btn btn-danger" OnClick="btnDelete_Click" />
                </div>
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>
 
    <%-- Modal Upload Album --%>

    <div class="modal fade" id="uploadAlbum" style="display: none;" aria-hidden="true">
        <div class="modal-dialog modal-xl">
            <div class="modal-content">
                <div class="modal-header">
                    <h4 runat="server" id="titleAlbum"></h4>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">×</span>
                    </button>
                </div>
                <div class="modal-body" style="min-height: 700px;">
                    <asp:UpdatePanel ID="updateAlbum" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <div class="row justify-content-center">

                                <div class="col-md-8" style="border-right: 1px solid #ccc;">
                                    <div class="form-group">
                                        <label for="txtEditNoiDungChinh">Danh sách hình ảnh</label>
                                        <br />

                                        <asp:Repeater ID="rptAlbum" runat="server" OnItemCommand="rptAlbum_ItemCommand">
                                            <ItemTemplate>
                                                <div class="image-container" style="display: inline-block;">
                                                    <img class="image-duAnTieuBieu" src='<%# Eval("HinhAnhUrl") %>'
                                                        style="max-height: 200px; width: 250px; border: 3px solid #0af; object-fit: cover;" />

                                                    <asp:Button ID="btnDeleteImage" runat="server" Text="Delete"
                                                        CssClass="btn-delete-album btn btn-danger"
                                                        CommandArgument='<%# Eval("Id") %>' CommandName="DeleteImage" />
                                                </div>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </div>
                                </div>

                                <!-- Form group for Thumbnail URL -->
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label class="control-label">Hình minh họa</label>
                                        <label style="margin-left: 5px;" class="small">(Click vào hình ảnh để thay đổi hình minh họa)</label>
                                        <div class="row text-center d-flex justify-content-center">
                                            <div class="img-thumbnail">
                                                <img data-selector="imgThumbAlbum" id="img1" runat="server" onclick="OpenSelectAlbum();" src="/Administration/UploadImage/addNewImage.png"
                                                    style="cursor: pointer; max-width: 300px" class="imgthumb img-responsive" />
                                            </div>
                                            <input style="display: none;" data-selector="txtAlbum" type="text" id="txtAlbum" runat="server" class="form-control" />
                                            <div id="div1" runat="server" visible="false">
                                                <a onclick="RemoveThumbnail();" title="Xóa hình mình họa" class="btn btn-default btn-flat btn-sm">Xóa hình minh họa</a>
                                            </div>
                                        </div>
                                        <div class="clearfix"></div>
                                    </div>

                                    <div class="form-group d-flex justify-content-end">
                                        <asp:Button ID="btnAddAlbum" runat="server" Text="Thêm ảnh vào album" OnClick="btnAlbumAddImage_Click" class="btn btn-primary mx-1 btn-user" Style="min-width: 50px;" />
                                    </div>
                                </div>
                            </div>

                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <div class="modal-footer justify-content-end">
                    <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                </div>
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>


    <%-- Video --%>
    <div class="modal fade" id="uploadVideo" style="display: none;" aria-hidden="true">
        <div class="modal-dialog modal-xl">
            <div class="modal-content">
                <div class="modal-header">
                    <h4 runat="server" id="txtVideo"></h4>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">×</span>
                    </button>
                </div>
                <div class="modal-body" style="min-height: 600px;">
                    <asp:UpdatePanel ID="UpdateVideo" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <div class="row justify-content-center">

                                <div class="col-md-8" style="border-right: 1px solid #ccc;">
                                    <div class="form-group">
                                        <label for="txtEditNoiDungChinh">Danh sách Video</label>
                                        <br />
                                        <asp:Repeater ID="RepeaterVideo" runat="server" OnItemCommand="rptVideo_ItemCommand">
                                            <ItemTemplate>
                                                <div class="image-container" style="display: inline-block;">
                                                    <img class="image-duAnTieuBieu" src='<%# Eval("ThumbnailUrl") %>'
                                                        style="max-height: 200px; width: 250px; border: 3px solid #0af; object-fit: cover;" />

                                                    <asp:Button ID="btnDeleteVideo" runat="server" Text="Delete"
                                                        CssClass="btn-delete-album btn btn-danger"
                                                        CommandArgument='<%# Eval("Id") %>' CommandName="DeleteVideo" />
                                                </div>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </div>
                                </div>

                                <!-- Form group for Thumbnail URL -->
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label for="txtTitleVideo">Tiêu đề</label>
                                        <input id="txtTitleVideo" runat="server" class="form-control form-control-user" placeholder="Nhập title" />
                                    </div>
                                    <div class="form-group" >
                                        <label class="control-label">Link</label>
                                        <div class="input-group js-upload-server">
                                            <input runat="server" id="txtServerLink" data-selector="txtServerLink" type="text" placeholder="Nhập link"  class="form-control">
                                            <span class="input-group-btn" data-selector="btnUploadVideo" runat="server" id="btnUploadVideo">
                                                <a href="javascript:;" class="btn btn-primary btn-flat" onclick="OpenSelectVideo();">Chọn video!</a>
                                            </span>
                                        </div>

                                        <label runat="server" id="lblSmallText" data-selector="labelSmallText" class="small">(Chọn file .mp3/ .mp4 tải lên máy chủ)</label>
                                    </div>
                                    <div class="form-group">
                                        <label class="control-label">Hình minh họa</label>
                                        <label style="margin-left: 5px;" class="small">(Click vào hình ảnh để thay đổi hình minh họa)</label>
                                        <div class="row text-center d-flex justify-content-center">
                                            <div class="img-thumbnail ">
                                                <img data-selector="imgAlbumVideoUrl" id="img2" runat="server" onclick="OpenSelectThumVideo();" src="/Administration/UploadImage/addNewImage.png"
                                                    style="cursor: pointer; max-width: 300px" class="imgthumb img-responsive" />
                                            </div>
                                            <input style="display: none;" data-selector="txtThumVideo" type="text" id="txtThumVideo" runat="server" class="form-control" />
                                            <div id="txt" runat="server" visible="false">
                                                <a onclick="RemoveThumbnail();" title="Xóa hình mình họa" class="btn btn-default btn-flat btn-sm">Xóa hình minh họa</a>
                                            </div>
                                        </div>
                                        <div class="clearfix"></div>
                                    </div>

                                    <div class="form-group d-flex justify-content-end">
                                        <asp:Button ID="btnAlbumVideoImage" runat="server" Text="Thêm Video" OnClick="btnAlbumVideoImage_Click" class="btn btn-primary mx-1 btn-user" Style="min-width: 50px;" />
                                    </div>
                                </div>
                            </div>

                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <div class="modal-footer justify-content-end">
                    <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                </div>
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>






</asp:Content>


<asp:Content runat="server" ID="scriptEnd" ContentPlaceHolderID="ContentScript">

    <script>

        function CheckValid() {
            var validated = $("#<%= UpdatePanelModal.ClientID%>").validationEngine('validate', { promptPosition: "TopLeft", scroll: false });
            if (validated)
                return validated;
        };
        function DisableContentChanged() {
            window.onbeforeunload = null;
        };

        function CreateFriendlyUrl(tag) {
            var str = $(tag).val()
                .normalize('NFD')
                .replace(/[\u0300-\u036f]/g, '') // Loại bỏ dấu
                .replace(/[^\w\s]+/g, '')        // Loại bỏ ký tự đặc biệt
                .replace(/\s+/g, '-')           // Thay khoảng trắng bằng dấu gạch ngang
                .toLowerCase();                 // Chuyển về chữ thường
            $('[data-selector="txtFriendlyURL"]').val(str);
        }

    </script>

    <script src="/Administration/Style/plugins/lightbox-evolution-1.8/js/jquery.lightbox.1.8.min.js"></script>
    <script src="/Administration/Style/dist/js/jquery.imgareaselect.pack.js"></script>
    <script>
        var uploadThumbnailKey = "<%=SecurityHelper.Encrypt("/Uploads/Article/")%>";
        var uploadThumbnailKeyAlbum = "<%=SecurityHelper.Encrypt("/Uploads/Article/Picture/")%>";


        var getWindowSize = function () {
            var w = 0; var h = 0;
            //IE
            if (!window.innerWidth) {
                if (!(document.documentElement.clientWidth === 0)) {
                    //strict mode
                    w = document.documentElement.clientWidth;
                    h = document.documentElement.clientHeight;
                } else {
                    //quirks mode
                    w = document.body.clientWidth; h = document.body.clientHeight;
                }
            } else {
                //w3c
                w = window.innerWidth; h = window.innerHeight;
            }
            return {
                width: w, height: h
            };
        };
        var OpenSelectImage = function () {
            var txtid = $('[data-selector="txtImage"]').attr('id');
            var ws = getWindowSize();
            $.lightbox('/RichFilemanager/default.aspx?field_name=' + txtid
                + '&key=' + uploadThumbnailKey
                + '&selectFun=setImageUrl',
                {
                    iframe: true,
                    width: ws.width - 60,
                    height: ws.height - 40,
                });
        };
        var setImageUrl = function (txtid, url) {
            document.getElementById(txtid).value = url;
            $('[data-selector="imgThumb"]').attr('src', url);
            //call next function
        };


        var OpenSelectAlbum = function () {
            var txtid = $('[data-selector="txtAlbum"]').attr('id');
            var ws = getWindowSize();
            $.lightbox('/RichFilemanager/default.aspx?field_name=' + txtid
                + '&key=' + uploadThumbnailKey
                + '&selectFun=setAlbumUrl',
                {
                    iframe: true,
                    width: ws.width - 60,
                    height: ws.height - 40,
                });
        };
        var setAlbumUrl = function (txtid, url) {
            document.getElementById(txtid).value = url;
            $('[data-selector="imgThumbAlbum"]').attr('src', url);
            //call next function
        };

        /*--Video--*/
        var OpenSelectVideo = function () {
            var txtid = $('[data-selector="txtServerLink"]').attr('id');
            var ws = getWindowSize();
            $.lightbox('/RichFilemanager/default.aspx?field_name=' + txtid
                + '&key=' + uploadThumbnailKey
                + '&selectFun=selectedVideo',
                {
                    iframe: true,
                    width: ws.width - 60,
                    height: ws.height - 40,
                });
        };
        var selectedVideo = function (txtid, url) {
            document.getElementById(txtid).value = url;
        };





        var OpenSelectThumVideo = function () {
            var txtid = $('[data-selector="txtThumVideo"]').attr('id');
            var ws = getWindowSize();
            $.lightbox('/RichFilemanager/default.aspx?field_name=' + txtid
                + '&key=' + uploadThumbnailKey
                + '&selectFun=setThumVideoUrl',
                {
                    iframe: true,
                    width: ws.width - 60,
                    height: ws.height - 40,
                });
        };
        var setThumVideoUrl = function (txtid, url) {
            document.getElementById(txtid).value = url;
            $('[data-selector="imgAlbumVideoUrl"]').attr('src', url);
            //call next function
        };


    </script>
</asp:Content>
