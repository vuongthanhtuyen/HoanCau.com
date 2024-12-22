<%@ Page Language="C#" ValidateRequest="false" MasterPageFile="~/Administration/MasterPage/AdminPage.Master" AutoEventWireup="true" CodeBehind="NhomFileDinhKem.aspx.cs" Inherits="CMS.WebUI.Administration.NhomFileDinhKem" %>

<%@ Import Namespace="SweetCMS.Core.Helper" %>
<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="head" runat="server">
    <link href="/Administration/Style/plugins/lightbox-evolution-1.8/theme/default/jquery.lightbox.css" rel="stylesheet" />
    <link href="/Administration/Style/dist/css/imgareaselect-default.css" rel="stylesheet" />
    <link href="/Administration/Assets/plugins/style.min.css" rel="stylesheet" />

</asp:Content>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="card card-primary card-outline">
        <div class="card-header justify-content-between">
            <h3 class="card-title">
                <i class="fas fa-edit"></i>
                Danh sách file đính kèm
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

        function MakeModal($id, $parenId) {
            $('[data-selector="txtIdHidden"]').val($id);
            $('[data-selector="txtHiddenIdParent"]').val($parenId);
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
                            <h4><%= _ModalTitle %></h4>
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
                                            <li class="nav-item" visible="false" runat="server" id="tabBaiViet">
                                                <a class="nav-link" id="article-in-category-tab" data-toggle="pill" href="#article-in-category" role="tab" aria-controls="article-in-category" aria-selected="false">Thêm file đính kèm</a>
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
                                                                    <label for="txtTieuDe ">Tiêu đề danh mục file</label>
                                                                    <input id="txtTieuDe" onkeyup="CreateFriendlyUrl(this);" runat="server" class="validate[required] form-control form-control-user" placeholder="Tiêu đề bài viết" />
                                                                </div>
                                                            </div>

                                                            <div class="col-md-12" id="divSlug"  runat="server" visible="false">
                                                                <div class="form-group">
                                                                    <label for="txtFriendlyURL">Slug</label>
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
                                                            <div class="col-md-12" runat="server" visible="false">
                                                                <div class="form-group">
                                                                    <label for="txtEditMota">Mô tả danh mục</label>
                                                                    <asp:TextBox ID="txtEditMota" runat="server" CssClass=" form-control form-control-user" placeholder="Nội dung" Height="200px" TextMode="MultiLine"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-4">

                                                        <div class="form-group"  id="divThumb"  runat="server" visible="false">
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
                                                        <div class="form-group" id="txtInfo" runat="server" visible="false">
                                                            <label>Người tạo: <%= _CreateBy %></label> <br />
                                                            <label>Ngày tạo: <%= _CreateDate %></label>
                                                            <label>Người cập nhật: <%= _UpdateDate %></label>
                                                            <label>Ngày cập nhật: <%= _UpdateDate %></label>

                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="tab-pane fade" id="article-in-category" role="tabpanel" aria-labelledby="article-in-category-tab">
                                                <input type="hidden" id="txtlistFileUploadJson" data-selector="txtlistFileUploadJson" runat="server" value="" class="form-control" />
                                                <input type="hidden" id="txtAllFileUploadElement" data-selector="txtAllFileUploadElement" runat="server" value="" class="form-control" />
                                                <div class="row">
                                                    <div class="col-md-6">
                                                        <label class="box-title">Danh sách file đính kèm</label>
                                                    </div>
                                                    <div class="col-md-6 text-right">
                                                        <a class="btn btn-primary padding-fa" onclick="AddNewItemFileUpload();">Thêm mới  </a>
                                                    </div>
                                                    <div class="col-md-12">
                                                        <div class="box-body" id="listUploadTableBody">
                                                            <asp:Literal runat="server" ID="ltrFileUpload" EnableViewState="false"></asp:Literal>
                                                            <div runat="server" visible="false" enableviewstate="false" id="templateFileUpload">

                                                                <div id="fileUploadElement{0}">
                                                                    <div class="row">
                                                                        <div class="col-md-6 ">
                                                                            <div class="form-group">
                                                                                <label class="control-label">Tiêu đề</label>
                                                                                <label style="display: inline-block; visibility: hidden; font-weight: normal;" id="lblErrorAlt{0}" class="text-red"><em>(Tiêu đề từ 1-250 ký tự)</em></label>

                                                                                <div>
                                                                                    <input type="text" class="form-control   validate[required]" data-id="{0}" value="{1}" data-selector="txtTitleData{0}" id="txtTitleData{0}" placeholder="Nhập tiêu đề" />
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                        <div class="col-md-5 col-xs-12">
                                                                            <div class="form-group">
                                                                                <label class="control-label">Đường dẫn</label>
                                                                                <label style="display: inline-block; visibility: hidden; font-weight: normal;" id="lblErrorUrl{0}" class="text-red"><em>(Đường dẫn từ 1-250 ký tự)</em></label>
                                                                                <div class="input-group input-group">
                                                                                    <input class="form-control  validate[required]" placeholder="Đường dẫn file" value="{2}" id="txtUrlFileUploadData{0}" data-selector="txtUrlFileUploadData{0}" type="text" />
                                                                                    <span class="input-group-btn">
                                                                                        <a class="btn btn-primary btn-open-modal padding-fa" onclick="OpenSelectAttachmentFile('txtUrlFileUploadData{0}');" data-toggle="modal" data-target="#image-detail">Tải lên
                                                                                        </a>
                                                                                    </span>
                                                                                </div>
                                                                                <div>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                        <div class="col-md-1 col-xs-12">
                                                                            <div class="form-group">
                                                                                <label class="control-label">Xóa </label>
                                                                                <div>
                                                                                    <a class="btn btn-danger" onclick="DeleteFileUpload('{0}');"><span class="fa fa-trash"></span>
                                                                                    </a>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>

                                                        </div>
                                                    </div>
                                                </div>
                                            </div>

                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="modal-footer justify-content-end">
                            <input runat="server" id="txtIdHidden" data-selector="txtIdHidden" class="hidden" />
                            <input runat="server" id="txtHiddenIdParent" data-selector="txtHiddenIdParent" class="hidden" />

                            <a runat="server" id="btnRefresh" data-selector="btnRefresh" onserverclick="btnRefresh_ServerClick"
                                class="hidden"></a>
                            <a class="btn btn-danger mx-1 btn-user btn-modal " runat="server" visible="false" id="btnXoa" onclick="openDelete()" style="min-width: 50px;"><span class="fa fa-trash"></span>Xóa</a>

                            <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                            <asp:Button ID="btnSave" runat="server" Text="Lưu" class="btn btn-primary mx-1 btn-user btn-modal" OnClientClick="CheckValid()" OnClick="btnSave_Click" Style="min-width: 50px;" />
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


<asp:Content ID="Content1" ContentPlaceHolderID="ContentScript" runat="server">
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
        function SetDefaultModal(IsValid) {
            if (IsValid) {
                $('.libasic').addClass('active');
                $('.lifile').removeClass('active');
                $('#basicsetting').addClass('active');
                $('#attachmentfile').removeClass('active');
            }
            else {
                $('.libasic').removeClass('active');
                $('.lifile').addClass('active');
                $('#basicsetting').removeClass('active');
                $('#attachmentfile').addClass('active');
            }
        }
        function CheckValid() {
            var validated = $("#checkModalValidate").validationEngine('validate', { promptPosition: "topLeft", scroll: false });

            if (GetData() === true) {
                //console.log("Đã đúng dữ liệu");
                $('[data-selector="txtAllFileUploadElement"]').val(document.getElementById("listUploadTableBody").innerHTML);
                var data = document.getElementById("listUploadTableBody").innerHTML;
                $('[data-selector="btnSaveFAQ"]')[0].click();
                SetDefaultModal(true);
            }
            else {
                SetDefaultModal(false);
            }

            if (validated) {

                DisableContentChanged();
                //SetDefaultModal();
            }
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
        // File Upload
        var idAttachMent = 0;
        function AddNewItemFileUpload() {
            idAttachMent++;
            var idData = "add" + idAttachMent;
            var insert =
                `
                <div id="fileUploadElement${idData}">
                <div class="row">
                    <div class="col-md-6 ">
                        <div class="form-group">
                            <label class="control-label">Tiêu đề</label>
                             <label style="display: inline-block; visibility: hidden; font-weight: normal;" id="lblErrorAlt${idData}"  class="text-red"><em>(Tiêu đề từ 1-250 ký tự)</em></label>

                            <div>
                                <input type="text" class="form-control   validate[required]" data-id="${idData}" data-selector="txtTitleData${idData}" id="txtTitleData${idData}" placeholder="Nhập tiêu đề" />
                            </div>
                        </div>
                    </div>
                    <div class="col-md-5 col-xs-12">
                        <div class="form-group">
                            <label class="control-label">Đường dẫn</label>
                            <label style="display: inline-block; visibility: hidden; font-weight: normal;" id="lblErrorUrl${idData}"  class="text-red"><em>(Đường dẫn từ 1-250 ký tự)</em></label>
                            <div class="input-group input-group">
                                <input class="form-control  validate[required]" placeholder="Đường dẫn file" id="txtUrlFileUploadData${idData}" data-selector="txtUrlFileUploadData${idData}" type="text" />
                                <span class="input-group-btn">
                                    <a class="btn btn-primary btn-open-modal padding-fa" onclick="OpenSelectAttachmentFile('txtUrlFileUploadData${idData}');" data-toggle="modal" data-target="#image-detail">Tải lên
                                    </a>
                                </span>
                            </div>
                            <div>
                            </div>
                        </div>
                    </div>

                    <div class="col-md-1 col-xs-12">
                        <div class="form-group">
                            <label class="control-label"> Xóa </label>
                            <div>
                                <a class="btn btn-danger" onclick="DeleteFileUpload('${idData}');"> <span class="fa fa-trash"></span>
                                                </a>
                            </div>
                        </div>
                    </div>
                </div>
                </div>
 
`
            document.getElementById("listUploadTableBody").insertAdjacentHTML('afterbegin', insert);
        }
        function GetData() {
            // Lấy tất cả các thẻ có data-selector bắt đầu với 'txttitle'
            var isValid = true;
            var listfileUploads = $('[data-selector^="txtTitleData"]').map(function () {
                var id = $(this).attr('data-id');
                var url = $(`[data-selector="txtUrlFileUploadData${id}"]`).val();
                var alt = $(this).val();

                if (url === "" || url.length > 250) {
                    $(`#lblErrorUrl${id}`).css('visibility', 'visible')
                    isValid = false;
                }
                else {
                    $(`#lblErrorUrl${id}`).css('visibility', 'hidden');
                }
                if (alt === "" || alt.length > 250) {
                    $(`#lblErrorAlt${id}`).css('visibility', 'visible');
                    isValid = false;
                }
                else {
                    $(`#lblErrorAlt${id}`).css('visibility', 'hidden');
                }
                var fileUpload = {
                    AttachmentFileIdString: id,
                    FileUrl: url,
                    Title: alt,
                }
                return fileUpload;
            }).get();

            if (isValid) {
                $('[data-selector="txtlistFileUploadJson"]').val(JSON.stringify(listfileUploads))
            }
            return isValid;
        };


        function DeleteFileUpload(index) {
            $(`#fileUploadElement${index}`).remove();
        }
    </script>

    <script>
        var uploadFAQAttachmentFileKey = "<%=SecurityHelper.Encrypt("/uploads/AttachmentFiles/")%>";
        var uploadThumbnailKey = "<%= SecurityHelper.Encrypt("/Uploads/Article/")%>";

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
        //var OpenSelectImageAlbum = function () {
        //    var txtid = $('[data-selector="txtImageUrl"]').attr('id');
        //    var ws = getWindowSize();
        //    $.lightbox('/RichFilemanager/default.aspx?field_name=' + txtid
        //        + '&key=' + uploadThumbnailKeyAlbum
        //        + '&selectFun=setImageAlbumUrl',
        //        {
        //            iframe: true,
        //            width: ws.width - 60,
        //            height: ws.height - 40,
        //        });
        //};
        //var setImageAlbumUrl = function (txtid, url) {
        //    document.getElementById(txtid).value = url;
        //    $('[data-selector="imgImageUrl"]').attr('src', url);
        //    //call next function
        //};
        var OpenSelectAttachmentFile = function (DataSelector) {
            var txtid = $(`[data-selector="${DataSelector}"]`).attr('id');
            var ws = getWindowSize();
            $.lightbox('/RichFilemanager/default.aspx?field_name=' + txtid
                + '&key=' + uploadFAQAttachmentFileKey
                + '&selectFun=selectedAttachmentFileFile',
                {
                    iframe: true,
                    width: ws.width - 60,
                    height: ws.height - 40,
                });
        };
        var selectedAttachmentFileFile = function (txtid, imPre, url) {
            console.log(txtid, imPre, url);
            document.getElementById(txtid).value = url;
        };

    </script>
</asp:Content>

