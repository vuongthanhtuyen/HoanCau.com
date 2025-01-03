<%@ Page Language="C#" ValidateRequest="false" AutoEventWireup="true" MasterPageFile="~/Administration/MasterPage/AdminPage.Master" CodeBehind="MenuDuoiWeb.aspx.cs" Inherits="CMS.WebUI.Administration.QuanLyCauHinh.MenuDuoiWeb" %>
<%@ Import Namespace="TBDCMS.Core.Helper" %>
<asp:Content ID="test" ContentPlaceHolderID="head" runat="server">
    <link href="/Administration/Assets/plugins/style.min.css" rel="stylesheet" />
</asp:Content>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="card card-primary card-outline">
        <div class="card-header justify-content-between">
            <h3 class="card-title">
                <i class="fas fa-edit"></i>
                Danh sách Menu dưới
            </h3>
            <%--<button type="button" onclick="MakeModal()" class="btn btn-primary col-2 float-sm-right">
                Thêm mới
            </button>--%>
        </div>
        <div class="card-body">
            <div class="col-xs-12 padding-none header-controls-right">
            </div>
            <asp:UpdatePanel ID="UpdatePanelMainTable" UpdateMode="Conditional" runat="server">
                <ContentTemplate>
                    <div class="rightsTreeView"></div>
                    <input runat="server" type="hidden" id="hdfRightsTreeViewData" data-selector="hdfRightsTreeViewData" value="" />

                </ContentTemplate>

            </asp:UpdatePanel>

        </div>
        <!-- /.card -->
        <asp:UpdatePanel ID="UpdatePanelId" runat="server">
            <ContentTemplate>
                <asp:HiddenField ID="hdnRowId" runat="server" />
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <script>

        function MakeModal($id, $parenId) {
            $('[data-selector="txtIdHidden"]').val($id);
            $('[data-selector="txtHidMenuIdParent"]').val($parenId);
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


<asp:Content runat="server" ID="Content3" ContentPlaceHolderID="ModalContent">

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
                                <div class="col-12">
                                    <div class="form-group">
                                        <label for="txtTen">Tên</label>
                                        <asp:TextBox ID="txtTen" runat="server" CssClass="form-control form-control-user" placeholder="Tên danh mục"></asp:TextBox>
                                    </div>
                                    <div class="form-group row">
                                        <div class="col-md-9">
                                            <label for="txtUrl">Url thân thiện</label>
                                            <asp:TextBox ID="txtUrl" runat="server" CssClass="form-control form-control-user" placeholder="Url thân thiện"></asp:TextBox>
                                        </div>
                                        <div class="col-md-3">
                                            <label for="txtEditStt">STT</label>
                                            <asp:TextBox ID="txtStt" runat="server" Text="1" placeholder="Số thứ tự" CssClass="form-control form-control-user"></asp:TextBox>

                                        </div>
                                    </div>
                                    <div class="form-group row">
                                        <div class="col-md-6">
                                            <label for="chkTrangThai" class="mb-3">Trạng thái</label>
                                            <br />
                                            <asp:CheckBox ID="chkTrangThai" runat="server" CssClass="form-control-user" />
                                            <label class="form-check-label" for="chkTrangThai">Hiển thị</label>

                                        </div>
                                        <div class="col-md-6">
                                            <label for="txtNgayTao"> </label>
                                            <label>Ngày tạo: <%= _CreateDate %></label>

                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label for="txtMa">Get Url </label>
                                        <ul class="nav nav-tabs" id="myTab" role="tablist">
                                            <li class="nav-item" role="presentation">
                                                <button class="nav-link active" id="baiViet-tab" data-toggle="tab" data-target="#baiViet" type="button" role="tab" aria-controls="baiViet" aria-selected="true">Bài viết</button>
                                            </li>
                                            <li class="nav-item" role="presentation">
                                                <button class="nav-link" id="danhMuc-tab" data-toggle="tab" data-target="#danhMuc" type="button" role="tab" aria-controls="danhMuc" aria-selected="false">Danh Mục</button>
                                            </li>
                                            <li class="nav-item" role="presentation">
                                                <button class="nav-link" id="urlKhac-tab" data-toggle="tab" data-target="#urlKhac" type="button" role="tab" aria-controls="urlKhac" aria-selected="false">Dự án tiêu biểu</button>
                                            </li>
                                            <li class="nav-item" role="presentation">
                                                <button class="nav-link" id="trangTinh-tab" data-toggle="tab" data-target="#trangTinh" type="button" role="tab" aria-controls="trangTinh" aria-selected="false">Trang tĩnh</button>
                                            </li>

                                        </ul>
                                        <div class="tab-content tab-select-url" id="myTabContent">
                                            <div class="tab-pane fade show active" id="baiViet" role="tabpanel" aria-labelledby="baiViet-tab">
                                                <div class="col-md-12 mt-3">
                                                    <div class="form-group">
                                                        <asp:DropDownList ID="drAddbaiviet" runat="server" CssClass="form-control form-control-user">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="tab-pane fade" style="min-height: 100px" id="danhMuc" role="tabpanel" aria-labelledby="danhMuc-tab">
                                                <div class="col-md-12 mt-3">
                                                    <div class="form-group">
                                                        <asp:DropDownList ID="drAddDanhSach" runat="server" CssClass="form-control form-control-user">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>

                                            </div>

                                            <div class="tab-pane fade" style="min-height: 100px" id="urlKhac" role="tabpanel" aria-labelledby="urlKhac-tab">
                                                <div class="col-md-12 mt-3">

                                                    <div class="form-group">
                                                        <asp:DropDownList ID="drAddDuAnTieuBieu" runat="server" CssClass="form-control form-control-user">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="tab-pane fade" style="min-height: 100px" id="trangTinh" role="tabpanel" aria-labelledby="trangTinh-tab">
                                                <div class="col-md-12 mt-3">
                                                    <div class="form-group">
                                                        <asp:DropDownList ID="drAddTrangTinh" runat="server" CssClass="form-control form-control-user">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>

                                            </div>

                                        </div>
                                    </div>
                                </div>
                            </div>
                            <!-- /.card -->
                        </div>
                        <div class="modal-footer justify-content-end">
                            <input runat="server" id="txtHidMenuIdParent" data-selector="txtHidMenuIdParent" class="hidden" />
                            <input runat="server" id="txtIdHidden" data-selector="txtIdHidden" class="hidden" />
                            <a runat="server" id="btnRefresh" data-selector="btnRefresh" onserverclick="btnRefresh_ServerClick"
                                class="hidden"></a>
                            <a class="btn btn-danger mx-1 btn-user btn-modal " runat="server" visible="false" id="btnXoa" onclick="openDelete()" style="min-width: 50px;"><span class="fa fa-trash"></span>Xóa</a>

                            <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                            <asp:Button ID="btnSave" runat="server" Text="Lưu" class="btn btn-primary mx-1 btn-user btn-modal" OnClick="btnSave_Click" Style="min-width: 50px;" />
                        </div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btnRefresh" EventName="ServerClick" />
                        <asp:AsyncPostBackTrigger ControlID="Button3" />

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
                    <asp:Button ID="Button3" runat="server" Text="Xóa" class="btn btn-danger" OnClick="btnDelete_Click" />
                </div>
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>
</asp:Content>




<asp:Content ID="Content2" ContentPlaceHolderID="ContentScript" runat="server">
    <script src="/Administration/Assets/plugins/jstree.min.js"></script>
    <script src="/Administration/Style/plugins/lightbox-evolution-1.8/js/jquery.lightbox.1.8.min.js"></script>
    <script src="/Administration/Style/dist/js/jquery.imgareaselect.pack.js"></script>

    <script>
        $(function () {
            if ($(".rightsTreeView").length) {
                var jsonData = JSON.parse($('[data-selector="hdfRightsTreeViewData"]').val());
                renderTreeView(jsonData);
            }
            $('[data-selector="hdfRightsTreeViewData"]').on('change', function () {
                var updateJsonData = JSON.parse((this).val());
                renderTreeView(updateJsonData);
            });
        });
        function renderTreeView(jsonData) {
            $(".rightsTreeView").jstree({
                "plugins": [
                    "changed",
                    "search"
                ],
                'search': {
                    'case_insensitive': true,
                    'show_only_matches': true
                },
                'core': {
                    'data': jsonData
                }
            }).on('changed.jstree', function (e, data) {
                var _href = '';
                try {
                    _href = data.node.a_attr.href;
                }
                catch (e) {
                    _href = '';
                }
                if (_href != undefined && _href != '')
                    window.location.href = _href;
            }).on('search.jstree', function (nodes, str, res) {
                if (str.nodes.length === 0) {
                    $('.rightsTreeView').jstree(true).hide_all();
                }
            });

            $('#txtKeysearch').keyup(function () {
                $('.rightsTreeView').jstree(true).show_all();
                $('.rightsTreeView').jstree('search', $(this).val());
            });
        }

    </script>
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

    <script>
        var uploadThumbnailKey = "<%= SecurityHelper.Encrypt("/Uploads/Article/")%>";
        var uploadThumbnailKeyAlbum = "<%= SecurityHelper.Encrypt("/Uploads/Article/Picture/")%>";

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

    <script type="text/javascript">

        $(document).on('change', '#<%= drAddbaiviet.ClientID %>', function () {
            var selectedValue = $(this).val();  // Lấy giá trị của DropDownList
            $('#<%= txtUrl.ClientID %>').val(selectedValue);
            if (!$('#<%= txtTen.ClientID %>').val())
                $('#<%= txtTen.ClientID %>').val($(this).find("option:selected").text());
        });
        $(document).on('change', '#<%= drAddDanhSach.ClientID %>', function () {
            var selectedValue = $(this).val();  // Lấy giá trị của DropDownList
            $('#<%= txtUrl.ClientID %>').val(selectedValue);
            if (!$('#<%= txtTen.ClientID %>').val())
                $('#<%= txtTen.ClientID %>').val($(this).find("option:selected").text());
        });
        $(document).on('change', '#<%= drAddTrangTinh.ClientID %>', function () {
            var selectedValue = $(this).val();  // Lấy giá trị của DropDownList
            $('#<%= txtUrl.ClientID %>').val(selectedValue);
            if (!$('#<%= txtTen.ClientID %>').val())
                $('#<%= txtTen.ClientID %>').val($(this).find("option:selected").text());
        });
        $(document).on('change', '#<%= drAddDuAnTieuBieu.ClientID %>', function () {
            var selectedValue = $(this).val();  // Lấy giá trị của DropDownList
            $('#<%= txtUrl.ClientID %>').val(selectedValue);
            if (!$('#<%= txtTen.ClientID %>').val())
                $('#<%= txtTen.ClientID %>').val($(this).find("option:selected").text());
        });

    </script>



</asp:Content>

