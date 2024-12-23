<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Administration/MasterPage/AdminPage.Master" CodeBehind="NganhDaoTaoAdmin.aspx.cs" Inherits="CMS.WebUI.Administration.NganhDaoTaoAdmin" %>


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
                Danh sách bài viết
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

                            <asp:BoundField DataField="MaNganh" HeaderText="Mã ngành" />
                            <asp:BoundField DataField="TenNganh" HeaderText="Tên ngành" />
                            <asp:TemplateField HeaderText="Nhóm ngành">
                                <ItemTemplate>
                                    <asp:LinkButton ID="ChinhSuaDanhMuc" runat="server"
                                        Text='<%# Eval("TenNhomNganh") ?? "Không có"  %>'
                                        CommandArgument='<%# Eval("Id") %>'
                                        CommandName="ChinhSuaDanhMuc">
                                    </asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:BoundField DataField="SoTinChi" HeaderText="Số tín chỉ" />
                            <asp:BoundField DataField="SoNamDaoTao" HeaderText="Số năm" />
                            <asp:BoundField DataField="ViewCount" HeaderText="Lượt xem" />

                            <asp:BoundField DataField="UpdateDate" HeaderText="Ngày cập nhật" DataFormatString="{0:dd/MM/yyyy}" HtmlEncode="false" />
                            <asp:TemplateField HeaderText="Hành Động" HeaderStyle-Width="130px">
                                <ItemTemplate>
                                    <a class="btn btn-primary" onclick="MakeModal('<%# Eval("Id") %>');">
                                        <i class="fa fa-edit"></i>
                                    </a>
                                    <asp:LinkButton ID="Xoa" runat="server" CssClass="btn btn-danger"
                                        CommandArgument='<%# Eval("Id") %>' ToolTip="Xóa" CommandName="Xoa">
                                         <span class="fa fa-trash"></span> 
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
    </script>
</asp:Content>

<asp:Content runat="server" ID="Content1" ContentPlaceHolderID="ModalContent">

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
                            <div class="col-12">
                                <div class="card card-primary card-outline card-outline-tabs">
                                    <div class="card-header p-0 border-bottom-0">
                                        <ul class="nav nav-tabs" id="custom-tabs-four-tab" role="tablist">
                                            <li class="nav-item">
                                                <a class="nav-link active" id="default-setting-category-tab" data-toggle="pill" href="#default-setting-category" role="tab" aria-controls="default-setting-category" aria-selected="true">Cài đặt chung</a>
                                            </li>
                                            <li class="nav-item" runat="server" id="tabBaiViet">
                                                <a class="nav-link" id="article-in-category-tab" data-toggle="pill" href="#article-in-category" role="tab" aria-controls="article-in-category" aria-selected="false">Thêm file đính kèm</a>
                                            </li>

                                        </ul>
                                    </div>
                                    <div class="card-body">
                                        <div class="tab-content" id="custom-tabs-four-tabContent">
                                            <div class="tab-pane fade active show" id="default-setting-category" role="tabpanel" aria-labelledby="default-setting-category-tab">
                                                <div class="row">
                                                    <div class="col-md-8">
                                                        <div class="form-group row">
                                                            <div class="col-md-6">
                                                                <label for="txtTieuDe ">Tên chuyên ngành</label>
                                                                <input id="txtTieuDe" onkeyup="CreateFriendlyUrl(this);" runat="server" class="validate[required] form-control form-control-user" placeholder="Tên chuyên ngành" />
                                                            </div>
                                                            <div class="col-md-6">
                                                                <label for="txtFriendlyURL">Url thân thiện</label>
                                                                <input data-selector="txtFriendlyURL" id="txtSlug" runat="server" class="validate[required] form-control form-control-user" placeholder="Slug" />
                                                            </div>
                                                        </div>
                                                        <div class="form-group">
                                                            <label for="txtMoTaNgan">Mô tả ngắn</label>
                                                            <textarea id="txtMoTaNgan" runat="server" class="form-control form-control-user" placeholder="Mô tả ngắn" rows="4"></textarea>
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
                                                            <div class="row text-center">
                                                                <div class="img-thumbnail d-flex justify-content-center">
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
                                                        </div>
                                                        <div class="form-group" id="txtInfo" runat="server" visible="false">
                                                            <label>Người tạo: <%= _CreateBy %></label>
                                                            <br />
                                                            <label>Ngày tạo: <%= _CreateDate %></label>
                                                            <label>Người cập nhật: <%= _UpdateBy %></label>
                                                            <label>Ngày cập nhật: <%= _UpdateDate %></label>

                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="tab-pane fade" id="article-in-category" role="tabpanel" aria-labelledby="article-in-category-tab">
                                                <div class="row">
                                                    <div class="col-md-6">
                                                        <label>Nhóm</label>
                                                        <asp:DropDownList runat="server" ID="ddlSearchCategory" CssClass="form-control custom-select">
                                                        </asp:DropDownList>
                                                    </div>
                                                    <div class="col-md-6">
                                                        <label for="txtHocPhi">Học phí</label>
                                                        <input data-selector="txtHocPhi" id="txtHocPhi" runat="server"
                                                            class="validate[required] form-control" placeholder="Học phí" type="number" />
                                                    </div>
                                                    <div class="col-md-6">
                                                        <label for="txtMaNganh">Mã ngành</label>
                                                        <input data-selector="txtMaNganh" id="txtMaNganh" runat="server"
                                                            class="validate[required] form-control" placeholder="Mã ngành" maxlength="20" />
                                                    </div>
                                                    <div class="col-md-6">
                                                        <label for="txtSoNamDaoTao">Số năm đào tạo</label>
                                                        <input data-selector="txtSoNamDaoTao" id="txtSoNamDaoTao" runat="server"
                                                            class="validate[required] form-control" placeholder="Số năm đào tạo" maxlength="50" />
                                                    </div>
                                                    <div class="col-md-6">
                                                        <label for="txtSoTinChi">Số tín chỉ</label>
                                                        <input data-selector="txtSoTinChi" id="txtSoTinChi" runat="server"
                                                            class="validate[required] form-control" placeholder="Số tín chỉ" type="number" />
                                                    </div>
                                                    <div class="col-md-12">
                                                        <label for="txtDieuKienNhapHoc">Điều kiện nhập học</label>
                                                        <textarea data-selector="txtDieuKienNhapHoc" id="txtDieuKienNhapHoc" runat="server"
                                                            class="validate[required] form-control" placeholder="Điều kiện nhập học" rows="3"></textarea>
                                                    </div>

                                                </div>
                                            </div>

                                        </div>
                                    </div>
                                </div>
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
                            <h4 class="modal-title">Thêm danh mục cho bài viết</h4>
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
        var setImageUrl = function (txtid, url) {
            document.getElementById(txtid).value = url;
            $('[data-selector="imgThumb"]').attr('src', url);
            //call next function
        };

        /*--Image album--*/
        var OpenSelectImageAlbum = function () {
            var txtid = $('[data-selector="txtImageUrl"]').attr('id');
            var ws = getWindowSize();
            $.lightbox('/RichFilemanager/default.aspx?field_name=' + txtid
                + '&key=' + uploadThumbnailKeyAlbum
                + '&selectFun=setImageAlbumUrl',
                {
                    iframe: true,
                    width: ws.width - 60,
                    height: ws.height - 40,
                });
        };
        var setImageAlbumUrl = function (txtid, url) {
            document.getElementById(txtid).value = url;
            $('[data-selector="imgImageUrl"]').attr('src', url);
            //call next function
        };


    </script>
</asp:Content>
