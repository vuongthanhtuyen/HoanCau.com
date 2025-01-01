<%@ Page Language="C#" ValidateRequest="false" AutoEventWireup="true" MasterPageFile="~/Administration/MasterPage/AdminPage.Master" CodeBehind="DanhMucDuAnTieuBieu.aspx.cs" Inherits="CMS.WebUI.Administration.QuanLyDuAnTieuBieu.DanhMucDuAnTieuBieu" %>

<%@ Import Namespace="SweetCMS.Core.Helper" %>
<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="head" runat="server">
    <link href="/Administration/Style/plugins/lightbox-evolution-1.8/theme/default/jquery.lightbox.css" rel="stylesheet" />
    <link href="/Administration/Style/dist/css/imgareaselect-default.css" rel="stylesheet" />    
    <link href="/Administration/Assets/plugins/jstree/theme-default/style.min.css" rel="stylesheet" />

</asp:Content>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="card card-primary card-outline">
        <div class="card-header justify-content-between">
            <h3 class="card-title">
                <i class="fas fa-edit"></i>
                Danh sách nhóm sự kiện
            </h3>
            <button type="button" onclick="MakeModal()" class="btn btn-primary col-2 float-sm-right">
                Thêm mới
            </button>
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
<asp:Content ID="Content1" ContentPlaceHolderID="ContentScript" runat="server">
    <script src="/Administration/Assets/plugins/jstree/jstree.min.js"></script>
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
</asp:Content>

<asp:Content runat="server" ID="Content3" ContentPlaceHolderID="ModalContent">

    <div class="modal fade" id="myModal" style="display: none;" aria-hidden="true">
        <div class="modal-dialog modal-xl">
            <div class="modal-content">
                <asp:UpdatePanel ID="UpdatePanelModal" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <div class="modal-header">
                            <h4 runat="server"> <%= _ModalTitle %></h4>
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                <span aria-hidden="true">×</span>
                            </button>
                        </div>
                        <div class="modal-body">
                            <div class="col-12">
                                <div class="row ">
                                    <div class="card card-primary card-outline card-outline-tabs">
                                        <div class="card-header p-0 border-bottom-0">
                                            <ul class="nav nav-tabs" id="custom-tabs-four-tab" role="tablist">
                                                <li class="nav-item">
                                                    <a class="nav-link active" id="default-setting-category-tab" data-toggle="pill" href="#default-setting-category" role="tab" aria-controls="default-setting-category" aria-selected="true">Thông tin chung</a>
                                                </li>
                                  

                                            </ul>
                                        </div>
                                        <div class="card-body">
                                            <div class="tab-content" id="custom-tabs-four-tabContent">
                                                <div class="tab-pane fade active show" id="default-setting-category" role="tabpanel" aria-labelledby="default-setting-category-tab">
                                                    <div class="row">
                                                        <div class="col-md-8">
                                                            <div class="row">


                                                                <div class="form-group" runat="server" visible="false">
                                                                    <label for="ddlStatus">Danh Mục cha</label>
                                                                    <asp:DropDownList ID="ddlEditDanhMuc" runat="server" CssClass="form-control form-control-user">
                                                                        <asp:ListItem Value="1">Hoạt động</asp:ListItem>
                                                                        <asp:ListItem Value="0">Không hoạt động</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </div>
                                                                <div class="col-md-12">
                                                                    <div class="form-group">
                                                                        <label for="txtTieuDe ">Tiêu đề</label>
                                                                        <input id="txtTieuDe" onkeyup="CreateFriendlyUrl(this);" runat="server" class="validate[required] form-control form-control-user" placeholder="Tiêu đề bài viết" />
                                                                    </div>
                                                                </div>

                                                                <div class="col-md-12">
                                                                    <div class="form-group">
                                                                        <label for="txtFriendlyURL">Url thân thiện</label>
                                                                        <input data-selector="txtFriendlyURL" id="txtSlug" runat="server" class="validate[required] form-control form-control-user" placeholder="Slug" />
                                                                    </div>
                                                                </div>
                                                                <div class="col-md-6">
                                                                    <div class="form-group">
                                                                        <label for="txtFriendlyURL">Trạng thái</label>
                                                                        <asp:DropDownList ID="ddlStatus" runat="server" CssClass="form-control form-control-user">
                                                                        </asp:DropDownList>

                                                                    </div>
                                                                </div>
                                                                <div class="col-md-6">
                                                                    <label for="txtDisplayOrder">Thứ tự hiển thị</label>
                                                                    <input id="txtDisplayOrder" runat="server" class="form-control form-control-user" placeholder="Thứ tự hiển thị" value="-1" type="number" />
                                                                </div>
                                                                <div class="col-md-12">
                                                                    <div class="form-group">
                                                                        <label for="txtEditMota">Mô tả danh mục</label>
                                                                        <asp:TextBox ID="txtEditMota" runat="server" CssClass=" form-control form-control-user" placeholder="Nội dung" Height="200px" TextMode="MultiLine"></asp:TextBox>
                                                                    </div>
                                                                </div>
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
                                                                <div class="form-group" id="txtInfo" runat="server" visible="false">
                                                                    <label>Người tạo: <%= _CreateBy %></label> <br />
                                                                    <label>Ngày tạo: <%= _CreateDate %></label> <br />
                                                                    <label>Người cập nhật: <%= _UpdateDate %></label> <br />
                                                                    <label>Ngày cập nhật: <%= _UpdateDate %></label> <br />

                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="tab-pane fade" runat ="server" visible="false" role="tabpanel" aria-labelledby="article-in-category-tab">
                                                    <div class="row">
                                                        <div class="col-md-12">
                                                            <div class="form-group">
                                                                <asp:GridView ID="GridBaiVietInDanhMuc" runat="server" AutoGenerateColumns="false"
                                                                    CssClass="table table-striped" CellPadding="10" CellSpacing="2"
                                                                    GridLines="None" Width="100%" Style="border: 1px solid #cccccc;">
                                                                    <Columns>
                                                                        <asp:TemplateField HeaderText="STT">
                                                                            <ItemTemplate>
                                                                                <%# Container.DataItemIndex + 1 %>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:BoundField DataField="TieuDe" HeaderText="Tiêu đề" />
                                                                        <asp:TemplateField HeaderText="Trạng thái">
                                                                            <ItemTemplate>
                                                                                <asp:CheckBox ID="DaChon" runat="server"
                                                                                    Checked='<%# Eval("DaChon").ToString()=="1" %>' />
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="BaiVietId" Visible="false">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblBaiVietId" runat="server" Text='<%# Eval("BaiVietId")%>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="DanhMucId" Visible="false">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblDanhMucId" runat="server" Text='<%# Eval("DanhMucId")%>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                    </Columns>
                                                                </asp:GridView>
                                                                <hr>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>

                                            </div>
                                        </div>
                                        <!-- /.card -->
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="modal-footer justify-content-end">
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

