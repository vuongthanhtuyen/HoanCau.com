<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Administration/MasterPage/AdminPage.Master" CodeBehind="SlideWeb.aspx.cs" Inherits="CMS.WebUI.Administration.QuanLyCauHinh.SlideWeb" %>


<%@ Import Namespace="SweetCMS.Core.Helper" %>
<%@ Register Src="~/Administration/AdminUserControl/PagingAdmin.ascx" TagPrefix="uc1" TagName="PagingAdmin" %>
<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="head" runat="server">
    <link href="/Administration/Style/plugins/lightbox-evolution-1.8/theme/default/jquery.lightbox.css" rel="stylesheet" />
    <link href="/Administration/Style/dist/css/imgareaselect-default.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class="card card-primary card-outline">
        <div class="card-header justify-content-between">
            <h3 class="card-title">
                <i class="fas fa-edit"></i>
                Danh sách slide
            </h3>
            <button type="button" onclick="MakeModal()" class="btn btn-primary col-2 float-sm-right">
                Thêm mới
            </button>
        </div>
        <div class="card-body">
            <div class="col-xs-12 padding-none header-controls-right">

                <asp:Label ID="Label1" CssClass="text-info" runat="server" Text=""></asp:Label>
            </div>
            <br />
            <asp:UpdatePanel ID="UpdatePanelMainTable" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <asp:GridView ID="GridViewTable" runat="server" AutoGenerateColumns="false"
                        CssClass="table table-striped table-bordered" CellPadding="10" CellSpacing="2"
                        GridLines="None" OnRowCommand="GridViewTable_RowCommand">
                        <Columns>
                            <asp:BoundField DataField="Stt" HeaderText="STT" />
                            <asp:BoundField DataField="NoiDungMot" HeaderText="Tiêu đề 1" />
                            <asp:BoundField DataField="NoiDungHai" HeaderText="Tiêu đề 2" />
                            <asp:TemplateField HeaderText="Logo">
                                <ItemTemplate>
                                    <img src='<%# Helpers.GetThumbnailUrl( Eval("HinhAnhUrl").ToString()) %>'
                                        alt="Slide" style="width: 100px; height: 100px; object-fit: cover;" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="LienKetUrl" HeaderText="Liên kết" />
                            <asp:BoundField DataField="NgayTao" HeaderText="Ngày tạo" DataFormatString="{0:dd/MM/yyyy}" HtmlEncode="false" />
                            <asp:TemplateField HeaderText="Hiển thị">
                                <ItemTemplate>
                                    <asp:CheckBox ID="TrangThai" runat="server"
                                        Checked='<%# Eval("TrangThai") %>' Enabled="False" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Hành Động" HeaderStyle-Width="250px">
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
                <Triggers>
                   
                </Triggers>
            </asp:UpdatePanel>



            <uc1:PagingAdmin runat="server" ID="PagingAdminWeb" />

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

<asp:Content runat="server" ID="Content2" ContentPlaceHolderID="ModalContent">

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
                            <div class="row">
                                <!-- Form group for Post Title and Slug -->
                                <div class="col-md-8">
                                    <div class=" row">
                                        <div class="col-md-12">
                                            <div class="form-group">
                                                <label for="txtTieuDeMot">Tiêu đề 1</label>
                                                <input type="text" runat="server" id="txtTieuDeMot" class="form-control form-control-user" placeholder="Tiêu đề 1" />
                                            </div>
                                        </div>
                                        <div class="col-md-12">
                                            <div class="form-group">
                                                <label for="txtTieuDeHai">Tiêu đề 2</label>
                                                <input type="text" runat="server" id="txtTieuDeHai" class="form-control form-control-user" placeholder="Tiêu đề 2" />
                                            </div>
                                        </div>

                                        <div class="col-md-12">
                                            <div class="form-group">
                                                <label for="txtLienKetUrl">Liên kết trang mới</label>
                                                <input type="url" runat="server" id="txtLienKetUrl" class="form-control form-control-user" placeholder="Liên kết" />
                                            </div>
                                        </div>

                                        <div class="col-md-12">
                                            <div class="form-group">
                                                <label for="txtStt">Số thứ tự</label>
                                                <input type="number" runat="server" id="txtStt" class="form-control form-control-user" placeholder="Số thứ tự" />
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <div class="col-md-4">
                                    <div class=" row">
                                        <div class="col-md-12">
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
                                        </div>

                                        <div class="col-md-6">
                                            <div class="form-group ">
                                                <label for="chkTrangThai" class="mb-3">Nổi bật</label>
                                                <br />
                                                <asp:CheckBox ID="chkTrangThai" runat="server" CssClass="form-control-user" Checked="true" />
                                                <label class="form-check-label" for="chkTrangThai">Hiển thị</label>
                                            </div>
                                        </div>
                                        <div class="col-md-6">
                                            <div class="form-group" id="txtInfo" runat="server" visible="false">
                                                <label>Ngày tạo: <%= _CreateDate %></label>
                                            </div>

                                        </div>
                                    </div>

                                </div>
                            </div>
                        </div>
                        
                        <div class="modal-footer justify-content-end">
                            <input runat="server" id="txtIdHidden" data-selector="txtIdHidden" class="hidden" />
                            <a runat="server" id="btnRefresh" data-selector="btnRefresh" onserverclick="btnRefresh_ServerClick"
                                class="hidden"></a>

                            <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                            <asp:Button ID="btnSendServer" runat="server" Text="Lưu" class="btn btn-primary btn-user mx-1"
                                OnClick="btnSendServer_Click" />
                        </div>

                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btnRefresh" EventName="ServerClick" />
                         <asp:AsyncPostBackTrigger ControlID="btnXoa" />

                    </Triggers>
                </asp:UpdatePanel>
            </div>
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
                    <asp:Button ID="btnXoa" runat="server" Text="Xóa" class="btn btn-danger" OnClick="btnDelete_Click" />
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


