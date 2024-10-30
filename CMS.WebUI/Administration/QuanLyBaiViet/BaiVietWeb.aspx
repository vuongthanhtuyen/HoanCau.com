<%@ Page Language="C#" ValidateRequest="false" AutoEventWireup="true" MasterPageFile="~/Administration/MasterPage/AdminPage.Master" CodeBehind="BaiVietWeb.aspx.cs" Inherits="CMS.WebUI.Administration.QuanLyBaiViet.BaiVietWeb" %>

<%@ Register Src="~/Administration/AdminUserControl/AdminNotification.ascx" TagPrefix="uc1" TagName="AdminNotification" %>
<%@ Register Src="~/Administration/AdminUserControl/PagingAdmin.ascx" TagPrefix="uc1" TagName="PagingAdmin" %>
<%@ Register Src="~/Administration/AdminUserControl/ImportImage.ascx" TagPrefix="uc1" TagName="ImportImage" %>
<%@ Register Src="~/Administration/AdminUserControl/SummernoteEditor.ascx" TagPrefix="uc1" TagName="SummernoteEditor" %>
<%@ Register Src="~/Administration/AdminUserControl/ImportImageEdit.ascx" TagPrefix="uc1" TagName="ImportImageEdit" %>
<%@ Register Src="~/Administration/AdminUserControl/SearchUserControl.ascx" TagPrefix="uc1" TagName="SearchUserControl" %>



<asp:Content ID="ctSearch" ContentPlaceHolderID="ctSearch" runat="server">
    <uc1:SearchUserControl runat="server" ID="SearchUserControl" />
</asp:Content>

<asp:Content ID="HeadContent" ContentPlaceHolderID="head" runat="server">
    <%--    <link href="Assets/css/Modal.css" rel="stylesheet" />--%>
    <link href="../Assets/css/Modal.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManger1" runat="Server" />
    <main>
        <div>
            <div class="col-xs-12 padding-none header-controls-right">
                <span class="notifications"></span>
                <button class="btn btn-primary btn-sm btn-flat padding-fa mr-4 " id="btnOpenModal" type="button"
                    onclick="openModal()" runat="server">
                    <i class="fa fa-plus"></i>Thêm mới</button>

                <asp:Label ID="lblResult" CssClass="text-info" runat="server" Text=""></asp:Label>
            </div>
        </div>

        <br />
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
                <asp:TemplateField HeaderText="Danh mục">
                    <ItemTemplate>
                        <asp:LinkButton ID="ChinhSuaDanhMuc" runat="server"
                            Text='<%# Eval("TenDanhMuc") %>' CommandArgument='<%# Eval("Id") %>'
                            CommandName="ChinhSuaDanhMuc">
                        </asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="TacGia" HeaderText="Tác giả" />
                <asp:BoundField DataField="ViewCount" HeaderText="Lượt xem" />

                <asp:TemplateField HeaderText="Hiển thị">
                    <ItemTemplate>
                        <asp:CheckBox ID="TrangThai" runat="server"
                            Checked='<%# Eval("TrangThai") %>' Enabled="False" />
                    </ItemTemplate>
                </asp:TemplateField>


                <asp:BoundField DataField="ChinhSuaGanNhat" HeaderText="Ngày chỉnh" DataFormatString="{0:dd/MM/yyyy}" HtmlEncode="false" />


                <asp:TemplateField HeaderText="Hành Động" HeaderStyle-Width="230px">
                    <ItemTemplate>
                        <asp:LinkButton ID="ChinhSuaChiTiet" runat="server" CssClass="btn btn-primary btn-xs btn-flat"
                            CommandArgument='<%# Eval("Id") %>' CommandName="ChinhSuaChiTiet" ToolTip="Cập nhật">
                                         <span class="fa fa-eye"></span> Cập nhật
                        </asp:LinkButton>

                        <asp:LinkButton ID="Xoa" runat="server" CssClass="btn btn-danger btn-xs btn-flat"
                            CommandArgument='<%# Eval("Id") %>' ToolTip="Xóa" CommandName="Xoa">
                                             <span class="fa fa-trash"></span> Xóa
                        </asp:LinkButton>

                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </main>


    <uc1:PagingAdmin runat="server" ID="PagingAdminWeb" />

    <%-- Modal add new Bài viết --%>
    <div id="myModal" class="modal">
        <div class="modal-content-post">
            <span class="close d-flex justify-content-end" onclick="closeModal()">&times;</span>
            <h4>Thêm mới bài viết </h4>
            <asp:Label ID="lblAddErrorMessage" runat="server" CssClass="text-danger pb-2" Text=""></asp:Label>
            <div class="row justify-content-center main-edit-modal">
                <!-- Form group for Post Title and Slug -->
                <div class="col-md-8">
                    <div class="form-group row">
                        <div class="col-md-6">
                            <label for="txtTieuDe">Tiêu đề bài viết</label>
                            <asp:TextBox ID="txtTieuDe" runat="server" CssClass="form-control form-control-user" placeholder="Tiêu đề bài viết"></asp:TextBox>
                        </div>
                        <div class="col-md-6">
                            <label for="txtSlug">Slug</label>
                            <asp:TextBox ID="txtSlug" runat="server" CssClass="form-control form-control-user" placeholder="Slug"></asp:TextBox>
                        </div>
                    </div>

                    <!-- Form group for Short Description and Main Content -->
                    <div class="form-group">
                        <label for="txtMoTaNgan">Mô tả ngắn</label>

                        <asp:TextBox ID="txtMoTaNgan" runat="server" CssClass="form-control form-control-user" TextMode="MultiLine" placeholder="Mô tả ngắn"></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <label for="txtNoiDungChinh">Nội dung chính</label>
                        <uc1:SummernoteEditor runat="server" ID="SummernoteEditor" />
                    </div>

                </div>
                <div class="col-md-3">
                    <div class="form-group row">
                        <label for="txtThumbnailUrl">URL ảnh đại diện (Thumbnail)</label>
                        <uc1:ImportImage runat="server" ID="ImportImage" />
                    </div>

                </div>
            </div>

            <div class="d-flex justify-content-end">
                <asp:Button ID="btnUserAdd" runat="server" Text="Thêm Mới" class="btn btn-primary btn-user mx-1"
                    OnClick="btnAdd_Click" />
                <asp:Button ID="btnCancel" runat="server" Text="Hủy" class="btn btn-secondary btn-user mx-1"
                    OnClientClick="closeModal(); return false;" />
            </div>

        </div>
    </div>

    <%-- Edit User Modal --%>
    <div id="myEditModal" class="modal">
        <div class="modal-content-post">

            <span class="close d-flex justify-content-end" onclick="closeEdit()">&times;</span>
            <h4>Chỉnh Cập nhật bài viết </h4>
            <asp:Label ID="lblEditMessager" runat="server" CssClass="text-danger pb-2" Text=""></asp:Label>
            <div class="row justify-content-center main-edit-modal">
                <!-- Form group for Post Title and Slug -->
                <div class="col-md-8">
                    <div class="form-group row">
                        <div class="col-md-6">
                            <label for="txtEditTieuDe">Tiêu đề bài viết</label>
                            <asp:TextBox ID="txtEditTieuDe" runat="server" CssClass="form-control form-control-user" placeholder="Tiêu đề bài viết"></asp:TextBox>
                        </div>
                        <div class="col-md-6">
                            <label for="txtEditSlug">Slug</label>
                            <asp:TextBox ID="txtEditSlug" runat="server" CssClass="form-control form-control-user" placeholder="Slug"></asp:TextBox>
                        </div>
                    </div>

                    <!-- Form group for Short Description and Main Content -->
                    <div class="form-group">
                        <label for="txtEditMoTaNgan">Mô tả ngắn</label>
                        <asp:TextBox ID="txtEditMoTaNgan" runat="server" CssClass="form-control form-control-user" TextMode="MultiLine" placeholder="Mô tả ngắn"></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <label for="txtEditNoiDungChinh">Nội dung chính</label>
                        <uc1:SummernoteEditor runat="server" ID="txtEditNoiDungChinh" />
                    </div>

                    <!-- Form group for Post Status and View Count -->
                    <div class="form-group row">
                        <div class="col-md-6">
                            <label for="chkEditTrangThai" class="mb-3">Trạng thái</label>
                            <br />
                            <asp:CheckBox ID="chkEditTrangThai" runat="server" CssClass="form-control-user" />
                            <label class="form-check-label" for="chkEditTrangThai">Hiển thị</label>

                        </div>
                        <div class="col-md-6">
                            <label for="txtEditViewCount">Lượt xem</label>
                            <asp:TextBox ID="txtEditViewCount" runat="server" CssClass="form-control form-control-user" placeholder="Lượt xem" Text="0" ReadOnly="true"></asp:TextBox>
                        </div>
                    </div>

                    <!-- Form group for Creation Date and Last Edited Date -->
                    <div class="form-group row">
                        <div class="col-md-6">
                            <label for="txtEditNgayTao">Ngày tạo</label>
                            <asp:TextBox ID="txtEditNgayTao" runat="server" CssClass="form-control form-control-user" TextMode="Date" ReadOnly="true"></asp:TextBox>
                        </div>
                        <div class="col-md-6">
                            <label for="txtEditChinhSuaGanNhat">Chỉnh Cập nhật gần nhất</label>
                            <asp:TextBox ID="txtEditChinhSuaGanNhat" runat="server" CssClass="form-control form-control-user" TextMode="Date"></asp:TextBox>
                        </div>
                    </div>
                </div>

                <!-- Form group for Thumbnail URL -->
                <div class="col-md-3">
                    <div class="form-group row">
                        <label for="txtEditThumbnailUrl">URL ảnh đại diện (Thumbnail)</label>
                        <uc1:ImportImageEdit runat="server" ID="txtEditThumbnailUrl" />
                    </div>
                </div>
            </div>

            <div class="d-flex justify-content-end">
                <asp:Button ID="Button1" runat="server" Text="Cập nhật" class="btn btn-primary mx-1 btn-user btn-modal" OnClick="btnEdit_Click" Style="min-width: 50px;" />
                <asp:Button ID="Button2" runat="server" Text="Hủy" class="btn btn-secondary mx-1 btn-user btn-modal" OnClientClick="closeEdit(); return false;" Style="min-width: 50px;" />
            </div>

        </div>
    </div>


    <%-- Edit Role Modal --%>
    <div id="danhMucEditModal" class="modal">
        <div class="modal-content main-edit-modal">
            <span class="close d-flex justify-content-end" onclick="closeDanhMucEditModal()">&times;</span>
            <h4>Chỉnh Cập nhật danh mục</h4>
            <div class="justify-content-center align-items-center mt-4">
                <asp:TextBox ID="txtEditRoleIdUser" runat="server" CssClass="form-control" placeholder="IdUser" Visible="false"></asp:TextBox>
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
                <hr>
            </div>
            <div class="d-flex justify-content-end">
                <asp:Button ID="btnEditDanhMuc" runat="server" Text="Cập nhật" class="btn btn-primary mx-1 btn-user btn-modal" OnClick="btnDanhMucEditSave_Click" />
                <asp:Button ID="btnCancelEditDanhMuc" runat="server" Text="Hủy" class="btn btn-secondary mx-1 btn-user btn-modal" OnClientClick="closeDanhMucEditModal(); return false;" />
            </div>
        </div>
    </div>


    <%-- Modal delete comfirm --%>
    <div id="confirmDeleteModal" class="modal">
        <div class="modal-content">
            <span class="close d-flex justify-content-end" onclick="closeDelete(); return false;">&times;</span>
            <h5>Bạn có chắc chắn muốn xóa bài viết này?</h5>
            <asp:Label ID="Label2" runat="server" CssClass="text-danger pb-2" Text=""></asp:Label>
            <div class="d-flex justify-content-end">
                <asp:Button ID="Button3" runat="server" Text="Xóa" class="btn btn-primary mx-1 btn-user btn-modal" OnClick="btnDelete_Click" />
                <asp:Button ID="Button4" runat="server" Text="Hủy" class="btn btn-secondary mx-1 btn-user btn-modal" OnClientClick="closeDelete(); return false;" />
            </div>
        </div>
    </div>
    <asp:HiddenField ID="hdnRowId" runat="server" />


    <uc1:AdminNotification runat="server" ID="AdminNotificationUserControl" />


    <script>
        function openModal() {
            document.getElementById("myModal").style.display = "block";
            document.getElementById("<%= lblResult.ClientID %>").innerText = "";
            return false;
        }
        function closeModal() {
            document.getElementById("myModal").style.display = "none";
            document.getElementById("<%= lblAddErrorMessage.ClientID %>").innerText = "";

        }

        function openDelete() {
            document.getElementById("confirmDeleteModal").style.display = "block";
            document.getElementById("<%= lblResult.ClientID %>").innerText = "";

            return false;
        }

        function closeDelete() {
            document.getElementById("confirmDeleteModal").style.display = "none";

        }

        function openEdit() {
            document.getElementById("<%= lblResult.ClientID %>").innerText = "";
            document.getElementById("myEditModal").style.display = "block";
            return false;
        }

        function closeEdit() {
            document.getElementById("myEditModal").style.display = "none";

        }
        function openDanhMucEditModal(id) {
            document.getElementById("danhMucEditModal").style.display = "block";
            return false;
        }

        function closeDanhMucEditModal() {
            document.getElementById("danhMucEditModal").style.display = "none";

        }
    </script>


</asp:Content>
