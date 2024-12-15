<%@ Page Language="C#" ValidateRequest="false" AutoEventWireup="true" MasterPageFile="~/Administration/MasterPage/AdminPage.Master" CodeBehind="MenuDuoiWeb.aspx.cs" Inherits="CMS.WebUI.Administration.QuanLyCauHinh.MenuDuoiWeb" %>


<%@ Register Src="~/Administration/AdminUserControl/AdminNotification.ascx" TagPrefix="uc1" TagName="AdminNotification" %>
<%@ Register Src="~/Administration/AdminUserControl/PagingAdmin.ascx" TagPrefix="uc1" TagName="PagingAdmin" %>

<asp:Content ID="test" ContentPlaceHolderID="head" runat="server">
    <link href="../Assets/plugins/style.min.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

     
    <main style="min-height: 500px;">
        <div>
            <div class="col-xs-12 padding-none header-controls-right">
                <span class="notifications"></span>
                <button class="btn btn-primary btn-sm btn-flat padding-fa mr-4" id="btnOpenModal" type="button"
                    onclick="openModal()" runat="server">
                    <i class="fa fa-plus"></i>Lưu</button>

                <asp:Label ID="lblResult" CssClass="text-info" runat="server" Text=""></asp:Label>
            </div>
        </div>

        <br />

        <div class="box">
            <div class="box-body">
                <div class="row">

                    <div class="col-md-12 form-group">
                        <asp:UpdatePanel ID="UpdatePanelMainTable" UpdateMode="Conditional" runat="server">
                            <ContentTemplate>
                                <div class="rightsTreeView"></div>
                                <input runat="server" type="hidden" id="hdfRightsTreeViewData" data-selector="hdfRightsTreeViewData" value="" />
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                    <div class="clearfix"></div>
                </div>
            </div>
        </div>

    </main>


    <uc1:PagingAdmin runat="server" ID="PagingAdminWeb" />

    <%-- Modal add new --%>
    <div id="myModal" class="modal">
        <div class="modal-content">
            <asp:UpdatePanel ID="UpdatePanelAdd" UpdateMode="Conditional" runat="server">
                <ContentTemplate>
                    <span class="close d-flex justify-content-end" onclick="closeModal()">&times;</span>
                    <h4>Lưu Menu </h4>
                    <asp:Label ID="lblAddErrorMessage" runat="server" CssClass="text-danger pb-2" Text=""></asp:Label>
                    <div class="justify-content-center align-items-center main-edit-modal">
                        <div class="form-group">
                            <label for="ddlAddMenuCha">Chọn menu Cha</label>
                            <asp:DropDownList ID="ddlAddMenuCha" runat="server" CssClass="form-control form-control-user">
                            </asp:DropDownList>
                        </div>
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
                                <asp:TextBox ID="txtStt" Text="1" runat="server" placeholder="Số thứ tự" CssClass="form-control form-control-user"></asp:TextBox>

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

                        <div class="d-flex justify-content-end">
                            <asp:Button ID="btnAdd" runat="server" Text="Lưu" class="btn btn-primary btn-user mx-1"
                                OnClick="btnAdd_Click" />
                            <asp:Button ID="btnCancel" runat="server" Text="Hủy" class="btn btn-secondary btn-user mx-1"
                                OnClientClick="closeModal(); return false;" />
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>

    <%-- Edit Modal --%>
    <div id="myEditModal" class="modal">
        <div class="modal-content">
            <asp:UpdatePanel ID="UpdatePanelEdit" UpdateMode="Conditional" runat="server">
                <ContentTemplate>
                    <span class="close d-flex justify-content-end" onclick="closeEdit()">&times;</span>
                    <h4>Chỉnh Cập nhật menu </h4>
                    <asp:Label ID="Label1" runat="server" CssClass="text-danger pb-2" Text=""></asp:Label>
                    <div class="justify-content-center align-items-center main-edit-modal">

                        <div class="form-group">
                            <asp:Label for="ddlEditMenuCha" ID="lblEditDrop" runat="server" Text="Chọn menu cha"></asp:Label>
                            <asp:DropDownList ID="ddlEditMenuCha" runat="server" CssClass="form-control form-control-user">
                            </asp:DropDownList>
                        </div>
                        <div class="form-group">
                            <label for="txtEditTen">Tên</label>
                            <asp:TextBox ID="txtEditTen" runat="server" CssClass="form-control form-control-user" placeholder="Tên danh mục"></asp:TextBox>
                        </div>
                        <div class="form-group row">
                            <div class="col-md-6">
                                <label for="chkEditTrangThai" class="mb-3">Trạng thái</label>
                                <br />
                                <asp:CheckBox ID="chkEditTrangThai" runat="server" CssClass="form-control-user" />
                                <label class="form-check-label" for="chkEditTrangThai">Hiển thị</label>

                            </div>
                            <div class="col-md-6">
                                <label for="txtEditNgayTao">Ngày tạo</label>
                                <asp:TextBox ID="txtEditNgayTao" runat="server" CssClass="form-control form-control-user" TextMode="Date" ReadOnly="true"></asp:TextBox>
                            </div>
                        </div>

                        <div class="form-group row">
                            <div class="col-md-9">
                                <label for="txtEditUrl">Url thân thiện</label>
                                <asp:TextBox ID="txtEditUrl" runat="server" CssClass="form-control form-control-user" placeholder="Url thân thiện"></asp:TextBox>
                            </div>
                            <div class="col-md-3">
                                <label for="txtEditStt">STT</label>
                                <asp:TextBox ID="txtEditStt" runat="server" placeholder="Số thứ tự" Text="1" CssClass="form-control form-control-user"></asp:TextBox>
                            </div>
                        </div>

                        <div class="form-group">
                            <label for="txtEditMa">Get Url </label>
                            <ul class="nav nav-tabs" id="myTabEdit" role="tablist">
                                <li class="nav-item" role="presentation">
                                    <button class="nav-link active" id="baiVietEdit-tab" data-toggle="tab" data-target="#baiVietEdit" type="button" role="tab" aria-controls="baiVietEdit" aria-selected="true">Bài viết</button>
                                </li>
                                <li class="nav-item" role="presentation">
                                    <button class="nav-link" id="danhMucEdit-tab" data-toggle="tab" data-target="#danhMucEdit" type="button" role="tab" aria-controls="danhMucEdit" aria-selected="false">Danh Mục</button>
                                </li>
                                <li class="nav-item" role="presentation">
                                    <button class="nav-link" id="urlKhacEdit-tab" data-toggle="tab" data-target="#urlKhacEdit" type="button" role="tab" aria-controls="urlKhacEdit" aria-selected="false">Dự án tiêu biểu</button>
                                </li>
                                <li class="nav-item" role="presentation">
                                    <button class="nav-link" id="trangTinhEdit-tab" data-toggle="tab" data-target="#trangTinhEdit" type="button" role="tab" aria-controls="trangTinhEdit" aria-selected="false">Trang tĩnh</button>
                                </li>
                            </ul>
                            <div class="tab-content tab-select-url" id="myTabEditContent">
                                <div class="tab-pane fade show active" id="baiVietEdit" role="tabpanel" aria-labelledby="baiVietEdit-tab">
                                    <div class="col-md-12 mt-3">
                                        <div class="form-group">
                                            <asp:DropDownList ID="drEditBaiviet" runat="server" CssClass="form-control form-control-user">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                <div class="tab-pane fade" style="min-height: 100px" id="danhMucEdit" role="tabpanel" aria-labelledby="danhMucEdit-tab">
                                    <div class="col-md-12 mt-3">
                                        <div class="form-group">
                                            <asp:DropDownList ID="drEditDanhSach" runat="server" CssClass="form-control form-control-user">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>

                                <div class="tab-pane fade" style="min-height: 100px" id="urlKhacEdit" role="tabpanel" aria-labelledby="urlKhacEdit-tab">
                                    <div class="col-md-12 mt-3">
                                        <div class="form-group">
                                            <asp:DropDownList ID="drEditDuAnTieuBieu" runat="server" CssClass="form-control form-control-user">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                <div class="tab-pane fade" style="min-height: 100px" id="trangTinhEdit" role="tabpanel" aria-labelledby="trangTinhEdit-tab">
                                    <div class="col-md-12 mt-3">
                                        <div class="form-group">
                                            <asp:DropDownList ID="drEditTrangTinh" runat="server" CssClass="form-control form-control-user">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>


                    </div>
                    <div class="d-flex justify-content-end">
                       <a class="btn btn-danger btn-xs btn-flat" onclick="openDelete()" style="min-width: 50px;"><span class="fa fa-trash"></span>Xóa</a>
                        <asp:Button ID="Button1" runat="server" Text="Cập nhật" class="btn btn-primary mx-1 btn-user btn-modal" OnClick="btnEdit_Click" Style="min-width: 50px;" />
                        <asp:Button ID="Button2" runat="server" Text="Hủy" class="btn btn-secondary mx-1 btn-user btn-modal" OnClientClick="closeEdit(); return false;" Style="min-width: 50px;" />
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>

    <%-- Modal delete comfirm --%>
    <div id="confirmDeleteModal" class="modal">
        <div class="modal-content">
            <span class="close d-flex justify-content-end" onclick="closeDelete(); return false;">&times;</span>
            <h5>Bạn có chắc chắn muốn xóa menu này?</h5>
            <asp:Label ID="Label2" runat="server" CssClass="text-danger pb-2" Text=""></asp:Label>
            <div class="d-flex justify-content-end">
                <asp:Button ID="Button3" runat="server" Text="Xóa" class="btn btn-primary mx-1 btn-user btn-modal" OnClick="btnDelete_Click" />
                <asp:Button ID="Button4" runat="server" Text="Hủy" class="btn btn-secondary mx-1 btn-user btn-modal" OnClientClick="closeDelete(); return false;" />
            </div>
        </div>
    </div>


    <uc1:AdminNotification runat="server" ID="AdminNotificationUserControl" />

    <asp:UpdatePanel ID="UpdatePanelId" runat="server">
        <ContentTemplate>
            <asp:HiddenField ID="hdnRowId" runat="server" />
        </ContentTemplate>
    </asp:UpdatePanel>
    <script>
        function openModal() {
            document.getElementById("myModal").style.display = "block";
            return false;
        }
        function closeModal() {
            document.getElementById("myModal").style.display = "none";

        }

        function openDelete() {
            document.getElementById("confirmDeleteModal").style.display = "block";

            return false;
        }

        function closeDelete() {
            document.getElementById("confirmDeleteModal").style.display = "none";

        }

        function openEdit() {
            document.getElementById("myEditModal").style.display = "block";
            return false;
        }

        function closeEdit() {
            document.getElementById("myEditModal").style.display = "none";

        }
    </script>
    <script type="text/javascript">
        // Lấy giá trị của DropDownList khi thay đổi và gán vào TextBox
        document.getElementById('<%= drAddbaiviet.ClientID %>').addEventListener('change', function () {
            var selectedValue = this.value;
            // Lấy giá trị của DropDownList
            document.getElementById('<%= txtUrl.ClientID %>').value = selectedValue;  // Gán vào TextBox
            document.getElementById('<%= txtTen.ClientID %>').value = this.selectedOptions[0].text;;  // Gán vào TextBox
        });
        document.getElementById('<%= drAddDanhSach.ClientID %>').addEventListener('change', function () {
            var selectedValue = this.value;  // Lấy giá trị của DropDownList
            document.getElementById('<%= txtUrl.ClientID %>').value = selectedValue;  // Gán vào TextBox
            document.getElementById('<%= txtTen.ClientID %>').value = this.selectedOptions[0].text;;  // Gán vào TextBox

        });
        document.getElementById('<%= drAddTrangTinh.ClientID %>').addEventListener('change', function () {
            var selectedValue = this.value;  // Lấy giá trị của DropDownList
            document.getElementById('<%= txtUrl.ClientID %>').value = selectedValue;  // Gán vào TextBox
            document.getElementById('<%= txtTen.ClientID %>').value = this.selectedOptions[0].text;;  // Gán vào TextBox

        });
        document.getElementById('<%= drAddDuAnTieuBieu.ClientID %>').addEventListener('change', function () {
            var selectedValue = this.value;  // Lấy giá trị của DropDownList
            document.getElementById('<%= txtUrl.ClientID %>').value = selectedValue;  // Gán vào TextBox
            document.getElementById('<%= txtTen.ClientID %>').value = this.selectedOptions[0].text;;  // Gán vào TextBox

        });

    </script>
    <script type="text/javascript">
        // Lấy giá trị của DropDownList khi thay đổi và gán vào TextBox
        document.getElementById('<%= drEditBaiviet.ClientID %>').addEventListener('change', function () {
            var selectedValue = this.value;  // Lấy giá trị của DropDownList
            document.getElementById('<%= txtEditUrl.ClientID %>').value = selectedValue;  // Gán vào TextBox
            document.getElementById('<%= txtEditTen.ClientID %>').value = this.selectedOptions[0].text;;  // Gán vào TextBox

        });
        document.getElementById('<%= drEditDanhSach.ClientID %>').addEventListener('change', function () {
            var selectedValue = this.value;  // Lấy giá trị của DropDownList
            document.getElementById('<%= txtEditUrl.ClientID %>').value = selectedValue;  // Gán vào TextBox
            document.getElementById('<%= txtEditTen.ClientID %>').value = this.selectedOptions[0].text;;  // Gán vào TextBox

        });
        document.getElementById('<%= drEditTrangTinh.ClientID %>').addEventListener('change', function () {
            var selectedValue = this.value;  // Lấy giá trị của DropDownList
            document.getElementById('<%= txtEditUrl.ClientID %>').value = selectedValue;  // Gán vào TextBox
            document.getElementById('<%= txtEditTen.ClientID %>').value = this.selectedOptions[0].text;;  // Gán vào TextBox

        });
        document.getElementById('<%= drEditDuAnTieuBieu.ClientID %>').addEventListener('change', function () {
            var selectedValue = this.value;  // Lấy giá trị của DropDownList
            document.getElementById('<%= txtEditUrl.ClientID %>').value = selectedValue;  // Gán vào TextBox
            document.getElementById('<%= txtEditTen.ClientID %>').value = this.selectedOptions[0].text;;  // Gán vào TextBox

        });

    </script>

</asp:Content>
<asp:Content ID ="contentScrit" ContentPlaceHolderID="ContentScript" runat="server">
        <script src="/Administration/Assets/plugins/jstree.min.js"></script>

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
            setTimeout(function () {
                document.querySelectorAll("a.btn-danger").forEach(function (deleteButton) {
                    deleteButton.addEventListener("click", function (event) {
                        event.stopPropagation();
                        //event.preventDefault();
                        openDelete();
                    });
                });
            }, 500); 
        </script>
</asp:Content>