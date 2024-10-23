<%@ Page Language="C#" ValidateRequest="false" AutoEventWireup="true" MasterPageFile="~/Administration/MasterPage/AdminPage.Master" CodeBehind="DanhMucBaiViet.aspx.cs" Inherits="CMS.WebUI.Administration.QuanLyBaiViet.DanhMucBaiViet" %>


<%@ Register Src="~/Administration/AdminUserControl/AdminNotification.ascx" TagPrefix="uc1" TagName="AdminNotification" %>
<%@ Register Src="~/Administration/AdminUserControl/SearchUserControl.ascx" TagPrefix="uc1" TagName="SearchUserControl" %>



<asp:Content ID="ctSearch" ContentPlaceHolderID="ctSearch" runat="server">
    <uc1:SearchUserControl runat="server" ID="SearchUserControl" />
</asp:Content>



<asp:Content ID="test" ContentPlaceHolderID="head" runat="server">
    <link href="../Assets/plugins/style.min.css" rel="stylesheet" />
</asp:Content>


<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManger1" runat="Server" />
    <main style="min-height: 500px;">
        <div>
            <div class="col-xs-12 padding-none header-controls-right">
                <span class="notifications"></span>
                <button class="btn btn-primary btn-sm btn-flat padding-fa mr-4" id="btnOpenModal" type="button"
                    onclick="openModal()" runat="server">
                    <i class="fa fa-plus"></i>Thêm mới</button>

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
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="Button3" EventName="Click" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>
                    <div class="clearfix"></div>
                </div>
            </div>
        </div>

    </main>

    <%-- Modal add new --%>
    <div id="myModal" class="modal">
        <div class="modal-content">
            <asp:UpdatePanel ID="UpdatePanelAdd" UpdateMode="Conditional" runat="server">
                <ContentTemplate>
                    <span class="close d-flex justify-content-end" onclick="closeModal()">&times;</span>
                    <h4>Thêm mới một category </h4>
                    <asp:Label ID="lblAddErrorMessage" runat="server" CssClass="text-danger pb-2" Text=""></asp:Label>
                    <div class="justify-content-center align-items-center main-edit-modal">
                        <div class="form-group">
                            <label for="ddlStatus">Danh Mục cha</label>
                            <asp:DropDownList ID="ddlDanhMuc" runat="server" CssClass="form-control form-control-user">
                                <asp:ListItem Value="1">Hoạt động</asp:ListItem>
                                <asp:ListItem Value="0">Không hoạt động</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div class="form-group row">
                            <div class="col-md-6">
                                <label for="txtTen">Tên danh mục</label>
                                <asp:TextBox ID="txtTen" runat="server" CssClass="form-control form-control-user" placeholder="Tên danh mục"></asp:TextBox>
                            </div>
                            <div class="col-md-6">
                                <label for="txtMa">Mã</label>
                                <asp:TextBox ID="txtMa" runat="server" CssClass="form-control form-control-user" placeholder="Mã"></asp:TextBox>
                            </div>
                        </div>
                        <div class="form-group">
                            <label for="txtMota">Mô tả danh mục</label>
                            <asp:TextBox ID="txtMota" runat="server" CssClass="form-control form-control-user" placeholder="Nội dung" Height="200px" TextMode="MultiLine"></asp:TextBox>
                        </div>

                        <div class="d-flex justify-content-end">
                            <asp:Button ID="btnAdd" runat="server" Text="Thêm Mới" class="btn btn-primary btn-user mx-1"
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
                    <h4>Chỉnh Cập nhật vai trò </h4>
                    <asp:Label ID="Label1" runat="server" CssClass="text-danger pb-2" Text=""></asp:Label>
                    <div class="justify-content-center align-items-center main-edit-modal" style="max-height: 500px; overflow-y: auto; overflow-x: hidden;">
                        <ul class="nav nav-tabs" id="myTab" role="tablist">
                            <li class="nav-item" role="presentation">
                                <button class="nav-link active" id="baiViet-tab" data-toggle="tab" data-target="#baiViet" type="button" role="tab" aria-controls="baiViet" aria-selected="true">Bài viết</button>
                            </li>
                            <li class="nav-item" role="presentation">
                                <button class="nav-link" id="danhMuc-tab" data-toggle="tab" data-target="#danhMuc" type="button" role="tab" aria-controls="danhMuc" aria-selected="false">Danh Mục</button>
                            </li>
                        </ul>
                        <div class="tab-content tab-select-url" id="myTabContent">
                            <div class="tab-pane fade show active" id="baiViet" role="tabpanel" aria-labelledby="baiViet-tab">
                                <div class="col-md-12 mt-3">
                                    <div class="form-group">
                                        <label for="ddlStatus">Danh Mục cha</label>
                                        <asp:DropDownList ID="ddlEditDanhMuc" runat="server" CssClass="form-control form-control-user">
                                            <asp:ListItem Value="1">Hoạt động</asp:ListItem>
                                            <asp:ListItem Value="0">Không hoạt động</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="form-group row">
                                        <div class="col-md-6">
                                            <label for="txtEditTen">Tên danh mục</label>
                                            <asp:TextBox ID="txtEditTen" runat="server" CssClass="form-control form-control-user" placeholder="Tên danh mục"></asp:TextBox>
                                        </div>
                                        <div class="col-md-6">
                                            <label for="txtEditMa">Mã</label>
                                            <asp:TextBox ID="txtEditMa" runat="server" CssClass="form-control form-control-user" placeholder="Mã"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label for="txtEditMota">Mô tả danh mục</label>
                                        <asp:TextBox ID="txtEditMota" runat="server" CssClass="form-control form-control-user" placeholder="Nội dung" Height="200px" TextMode="MultiLine"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="tab-pane fade" style="min-height: 100px" id="danhMuc" role="tabpanel" aria-labelledby="danhMuc-tab">
                                <div class="col-md-12 mt-3">
                                    <div class="form-group">
                                        <asp:GridView ID="GridBaiVietInDanhMuc" runat="server" AutoGenerateColumns="false"
                                            CssClass="table table-striped"  CellPadding="10" CellSpacing="2"
                                            GridLines="None" Width="100%" style="border: 1px solid #cccccc;">
                                            <Columns >
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
                        <div class="d-flex justify-content-end">

                            <a class="btn btn-danger btn-xs btn-flat" onclick="openDelete()" style="min-width: 50px;"><span class="fa fa-trash"></span>Xóa</a>
                            <asp:Button ID="Button1" runat="server" Text="Cập nhật" class="btn btn-primary mx-1 btn-user btn-modal" OnClick="btnEdit_Click" Style="min-width: 50px;" />
                            <asp:Button ID="Button2" runat="server" Text="Hủy" class="btn btn-secondary mx-1 btn-user btn-modal" OnClientClick="closeEdit(); return false;" Style="min-width: 50px;" />
                        </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
</div>



    <%-- Modal delete comfirm --%>
    <div id="confirmDeleteModal" class="modal">
        <div class="modal-content">
            <span class="close d-flex justify-content-end" onclick="closeDelete(); return false;">&times;</span>
            <h5>Bạn có chắc chắn muốn xóa danh mục này?</h5>
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
            document.getElementById("<%= lblAddErrorMessage.ClientID %>").innerText = "";
            return false;
        }

        function openDelete() {
            document.getElementById("confirmDeleteModal").style.display = "block";
            document.getElementById("<%= lblResult.ClientID %>").innerText = "";

            return false;
        }

        function closeDelete() {
            document.getElementById("confirmDeleteModal").style.display = "none";
            return false;
        }

        function openEdit() {
            document.getElementById("<%= lblResult.ClientID %>").innerText = "";
            document.getElementById("myEditModal").style.display = "block";
            return false;
        }

        function closeEdit() {
            document.getElementById("myEditModal").style.display = "none";

        }
       
    </script>


</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentScript" runat="server">

    <%--<script src="https://cdnjs.cloudflare.com/ajax/libs/jquery/1.12.1/jquery.min.js"></script>  <!-- 5 include the minified jstree source -->
<script src="https://cdnjs.cloudflare.com/ajax/libs/jstree/3.2.1/jstree.min.js"></script>--%>
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

    </script>

</asp:Content>
