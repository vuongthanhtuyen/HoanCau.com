<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Administration/MasterPage/AdminPage.Master" CodeBehind="NguoiDungWeb.aspx.cs" Inherits="CMS.WebUI.Administration.NguoiDungWeb" %>

<%@ Register Src="~/Administration/AdminUserControl/AdminNotification.ascx" TagPrefix="uc1" TagName="AdminNotification" %>
<%@ Register Src="~/Administration/AdminUserControl/PagingAdmin.ascx" TagPrefix="uc1" TagName="PagingAdmin" %>


<asp:Content ID="HeadContent" ContentPlaceHolderID="head" runat="server">
    <link href="Assets/css/Modal.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
     
    <main>
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
        <asp:UpdatePanel ID="UpdatePanelTablleMain" UpdateMode="Conditional" runat="server">
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
                        <asp:BoundField DataField="HoVaTen" HeaderText="Họ và tên" />
                        <asp:BoundField DataField="Email" HeaderText="Email" />
                        <asp:BoundField DataField="SoDienThoai" HeaderText="Số điện thoại" />
                        <asp:BoundField DataField="TenTruyCap" HeaderText="Tên truy cập" />
                        <asp:BoundField DataField="ChinhSuaGanNhat" HeaderText="Ngày cập nhật" DataFormatString="{0:dd/MM/yyyy}" HtmlEncode="false" />
                        <asp:TemplateField HeaderText="Vai trò">
                            <ItemTemplate>
                                <asp:LinkButton ID="chinhSuaVaiTro" runat="server"
                                    Text='<%# Eval("VaiTroChiTiet") %>' CommandArgument='<%# Eval("Id") %>'
                                    CommandName="ChinhSuaVaiTro">
                                </asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Hành Động" HeaderStyle-Width="250px">
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
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID ="Button3" EventName="Click" />
            </Triggers>
        </asp:UpdatePanel>
    </main>


    <uc1:PagingAdmin runat="server" ID="PagingAdminWeb" />

    <%-- Modal add new user --%>
    <div id="myModal" class="modal">
        <div class="modal-content">

            <asp:UpdatePanel ID="UpdatePanelAdd" UpdateMode="Conditional" runat="server">
                <ContentTemplate>
                    <span class="close d-flex justify-content-end" onclick="closeModal()">&times;</span>
                    <h4>Lưu người dùng </h4>
                    <asp:Label ID="lblAddErrorMessage" runat="server" CssClass="text-danger pb-2" Text=""></asp:Label>
                    <div class="justify-content-center align-items-center main-edit-modal">
                        <div class="form-group row">
                            <div class="col-md-6">
                                <label for="txtHoVaTen">Họ và tên</label>
                                <asp:TextBox ID="txtHoVaTen" runat="server" CssClass="form-control form-control-user" placeholder="Họ và tên"></asp:TextBox>
                            </div>
                            <div class="col-md-6">
                                <label for="txtEmail">Email</label>
                                <asp:TextBox ID="txtEmail" runat="server" TextMode="Email" CssClass="form-control form-control-user" placeholder="Email"></asp:TextBox>
                            </div>
                        </div>
                        <div class="form-group row">
                            <div class="col-md-6">
                                <label for="txtSoDienThoai">Số điện thoại</label>
                                <asp:TextBox ID="txtSoDienThoai" runat="server" CssClass="form-control form-control-user" placeholder="Số điện thoại"></asp:TextBox>
                            </div>
                            <div class="col-md-6">
                                <label for="txtNgaySinh">Ngày sinh</label>
                                <asp:TextBox ID="txtNgaySinh" runat="server" TextMode="Date" CssClass="form-control form-control-user" placeholder="Ngày sinh"></asp:TextBox>
                            </div>
                        </div>

                        <div class="form-group row">
                            <div class="col-md-6">
                                <label for="txtTenTruyCap">Tên đăng nhập</label>
                                <asp:TextBox ID="txtTenTruyCap" runat="server" CssClass="form-control form-control-user" placeholder="Tên đăng nhập"></asp:TextBox>
                            </div>
                            <div class="col-md-6">
                                <label for="txtMatKhau">Mật khẩu</label>
                                <asp:TextBox ID="txtMatKhau" runat="server" TextMode="Password" CssClass="form-control form-control-user" placeholder="Mật khẩu"></asp:TextBox>
                            </div>
                        </div>

                        <div class="form-group">
                            <label for="txtDiaChi">Địa chỉ</label>
                            <asp:TextBox ID="txtDiaChi" runat="server" CssClass="form-control form-control-user" placeholder="Địa chỉ"></asp:TextBox>
                        </div>
                        <div class="form-group">
                            <label for="txtAvatarUrl">URL ảnh đại diện</label>
                            <asp:TextBox ID="txtAvatarUrl" runat="server" CssClass="form-control form-control-user" placeholder="URL ảnh đại diện"></asp:TextBox>
                        </div>
                    </div>
                    <div class="d-flex justify-content-end">
                        <asp:Button ID="btnUserAdd" runat="server" Text="Lưu" class="btn btn-primary btn-user mx-1"
                            OnClick="btnUserAdd_Click" />
                        <asp:Button ID="btnCancel" runat="server" Text="Hủy" class="btn btn-secondary btn-user mx-1"
                            OnClientClick="closeModal(); return false;" />
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>

    <%-- Edit User Modal --%>
    <div id="myEditModal" class="modal">
        <div class="modal-content">
            <asp:UpdatePanel ID="UpdatePanelEdit" UpdateMode="Conditional" runat="server">
                <ContentTemplate>
                    <span class="close d-flex justify-content-end" onclick="closeEdit()">&times;</span>
                    <h4>Chỉnh Cập nhật người dùng </h4>
                    <asp:Label ID="lblEditMessager" runat="server" CssClass="text-danger pb-2" Text=""></asp:Label>
                    <div class="justify-content-center align-items-center main-edit-modal">
                        <div class="form-group row">
                            <div class="col-md-6">
                                <label for="txtEditHoVaTen">Họ và tên</label>
                                <asp:TextBox ID="txtEditHoVaTen" runat="server" CssClass="form-control form-control-user" placeholder="Họ và tên"></asp:TextBox>
                            </div>
                            <div class="col-md-6">
                                <label for="txtEditEmail">Email</label>
                                <asp:TextBox ID="txtEditEmail" runat="server" TextMode="Email" CssClass="form-control form-control-user" placeholder="Email"></asp:TextBox>
                            </div>
                        </div>
                        <div class="form-group row">
                            <div class="col-md-6">
                                <label for="txtEditSoDienThoai">Số điện thoại</label>
                                <asp:TextBox ID="txtEditSoDienThoai" runat="server" CssClass="form-control form-control-user" placeholder="Số điện thoại"></asp:TextBox>
                            </div>
                            <div class="col-md-6">
                                <label for="txtEditNgaySinh">Ngày sinh</label>
                                <asp:TextBox ID="txtEditNgaySinh" runat="server" TextMode="Date" CssClass="form-control form-control-user" placeholder="Ngày sinh"></asp:TextBox>
                            </div>
                        </div>

                        <div class="form-group row">
                            <div class="col-md-6">
                                <label for="txtEditTenTruyCap">Tên đăng nhập</label>
                                <asp:TextBox ID="txtEditTenTruyCap" runat="server" CssClass="form-control form-control-user" placeholder="Tên đăng nhập"></asp:TextBox>
                            </div>
                            <div class="col-md-6">
                                <label for="txtEditMatKhau">Mật khẩu</label>
                                <asp:TextBox ID="txtEditMatKhau" runat="server" TextMode="Password" CssClass="form-control form-control-user" placeholder="Mật khẩu"></asp:TextBox>
                            </div>
                        </div>

                        <div class="form-group row">
                            <div class="col-md-9">
                                <label for="txtEditDiaChi">Địa chỉ</label>
                                <asp:TextBox ID="txtEditDiaChi" runat="server" CssClass="form-control form-control-user" placeholder="Địa chỉ"></asp:TextBox>
                            </div>
                            <div class="col-md-3">
                                <label for="chkEditTrangThai" class="mb-3">Trạng thái</label>
                                <br />
                                <asp:CheckBox ID="chkEditTrangThai" runat="server" CssClass="form-control-user" />
                                <label class="form-check-label" for="chkEditTrangThai">Hoạt động</label>
                            </div>
                        </div>

                        <div class="form-group">
                            <label for="txtEditAvatarUrl">URL ảnh đại diện</label>
                            <asp:TextBox ID="txtEditAvatarUrl" runat="server" CssClass="form-control form-control-user" placeholder="URL ảnh đại diện"></asp:TextBox>
                        </div>
                    </div>
                    <div class="d-flex justify-content-end">
                        <asp:Button ID="Button1" runat="server" Text="Cập nhật" class="btn btn-primary mx-1 btn-user btn-modal" OnClick="btnUserEdit_Click" Style="min-width: 50px;" />
                        <asp:Button ID="Button2" runat="server" Text="Hủy" class="btn btn-secondary mx-1 btn-user btn-modal" OnClientClick="closeEdit(); return false;" Style="min-width: 50px;" />
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>


    <%-- Edit Role Modal --%>
    <div id="roleEditModal" class="modal">
        <div class="modal-content">
            <asp:UpdatePanel ID="UpdatePanelUpdateRole" UpdateMode="Conditional" runat="server">
                <ContentTemplate>
                    <span class="close d-flex justify-content-end" onclick="closeRoleEditModal()">&times;</span>
                    <h4>Chỉnh Cập nhật role</h4>
                    <div class="justify-content-center align-items-center" style="max-height: 400px; overflow-y: auto; overflow-x: hidden;">
                        <asp:TextBox ID="txtEditRoleIdUser" runat="server" CssClass="form-control" placeholder="IdUser" Visible="false"></asp:TextBox>

                        <asp:GridView ID="GridViewRole" runat="server" AutoGenerateColumns="false"
                            CssClass="table table-striped" CellPadding="10" CellSpacing="2"
                            GridLines="None">

                            <Columns>
                                <asp:BoundField DataField="Id" HeaderText="Id vai trò" />
                                <asp:BoundField DataField="Ten" HeaderText="Tên" />
                                <asp:BoundField DataField="Ma" HeaderText="Mã" />
                                <asp:TemplateField HeaderText="Có vai trò">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="CoVaiTro" runat="server"
                                            Checked='<%# Eval("CoVaiTro").ToString() == "1" %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>

                        <hr>
                    </div>
                    <div class="d-flex justify-content-end">
                        <asp:Button ID="btnEditRole" runat="server" Text="Cập nhật" class="btn btn-primary mx-1 btn-user btn-modal" OnClick="btnRoleEditSave_Click" />
                        <asp:Button ID="btnCancelEditRole" runat="server" Text="Hủy" class="btn btn-secondary mx-1 btn-user btn-modal" OnClientClick="closeRoleEditModal(); return false;" />
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>


    <%-- Modal delete comfirm --%>
    <div id="confirmDeleteModal" class="modal">
        <div class="modal-content">
            <span class="close d-flex justify-content-end" onclick="closeDelete(); return false;">&times;</span>
            <h5>Bạn có chắc chắn muốn xóa người dùng này?</h5>
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
        function openRoleEditModal(id) {
            document.getElementById("<%= lblResult.ClientID %>").innerText = "";
            document.getElementById("roleEditModal").style.display = "block";
            return false;
        }

        function closeRoleEditModal() {
            document.getElementById("roleEditModal").style.display = "none";

        }
    </script>


</asp:Content>
