<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Administration/MasterPage/AdminPage.Master" CodeBehind="PhanQuyenWeb.aspx.cs" Inherits="CMS.WebUI.Administration.PhanQuyenWeb" %>

<%@ Register Src="~/Administration/AdminUserControl/AdminNotification.ascx" TagPrefix="uc1" TagName="AdminNotification" %>
<%@ Register Src="~/Administration/AdminUserControl/PagingAdmin.ascx" TagPrefix="uc1" TagName="PagingAdmin" %>
<%@ Register Src="~/Administration/AdminUserControl/SearchUserControl.ascx" TagPrefix="uc1" TagName="SearchUserControl" %>



<asp:Content ID="HeadContent" ContentPlaceHolderID="head" runat="server">
    <link href="Assets/css/Modal.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="ctSearch" ContentPlaceHolderID="ctSearch" runat="server">
    <uc1:SearchUserControl runat="server" ID="SearchUserControl" />
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
                        <asp:BoundField DataField="Ten" HeaderText="Tên" />
                        <asp:BoundField DataField="Ma" HeaderText="Mã" />
                        <asp:BoundField DataField="TrangThai" HeaderText="Trạng thái" />
                        <asp:TemplateField HeaderText="Active">
                            <ItemTemplate>
                                <asp:CheckBox ID="TrangThai" runat="server"
                                    Checked='<%# Eval("TrangThai") %>' Enabled="False" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="NgayTao" HeaderText="Ngày tạo" DataFormatString="{0:dd/MM/yyyy}" HtmlEncode="false" />

                        <asp:TemplateField HeaderText="Hành Động" HeaderStyle-Width="370px">
                            <ItemTemplate>
                                <asp:LinkButton ID="ChinhSuaChiTiet" runat="server" CssClass="btn btn-primary btn-xs btn-flat"
                                    CommandArgument='<%# Eval("Id") %>' CommandName="ChinhSuaChiTiet" ToolTip="Cập nhật">
                         <span class="fa fa-eye"></span> Cập nhật
                                </asp:LinkButton>

                                <asp:LinkButton ID="Xoa" runat="server" CssClass="btn btn-danger btn-xs btn-flat"
                                    CommandArgument='<%# Eval("Id") %>' ToolTip="Xóa" CommandName="Xoa">
                             <span class="fa fa-trash"></span> Xóa
                                </asp:LinkButton>
                                <asp:LinkButton ID="phanQuyen" runat="server" CssClass="btn btn-primary btn-xs btn-flat"
                                    CommandArgument='<%# Eval("Id") %>' ToolTip="Phân quyền" CommandName="phanQuyen">
                             <span class="fa fa-plus"></span> Phân quyền
                                </asp:LinkButton>

                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="Button3" EventName="Click" />
            </Triggers>
        </asp:UpdatePanel>


    </main>

    <uc1:PagingAdmin runat="server" ID="PagingAdminWeb" />

    <%-- Modal add new --%>
    <div id="myModal" class="modal">
        <div class="modal-content">
            <asp:UpdatePanel ID="UpdatePanelAdd" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <span class="close d-flex justify-content-end" onclick="closeModal()">&times;</span>
                    <h4>Thêm mới vai trò </h4>
                    <asp:Label ID="lblAddErrorMessage" runat="server" CssClass="text-danger pb-2" Text=""></asp:Label>
                    <div class="justify-content-center align-items-center main-edit-modal">
                        <div class="form-group row">
                            <div class="col-md-6">
                                <label for="txtTenVaiTro">Tên vai trò</label>
                                <asp:TextBox ID="txtTenVaiTro" runat="server" CssClass="form-control form-control-user" placeholder="Tên vai trò"></asp:TextBox>
                            </div>
                            <div class="col-md-6">
                                <label for="txtMa">Mã</label>
                                <asp:TextBox ID="txtMa" runat="server" CssClass="form-control form-control-user" placeholder="Mã"></asp:TextBox>
                            </div>
                        </div>
                        <div class="d-flex justify-content-end">
                            <asp:Button ID="btnAdd" runat="server" Text="Thêm Mới" class="btn btn-primary btn-user mx-1"
                                OnClick="btnUserAdd_Click" />
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
            <asp:UpdatePanel ID="UpdatePanelEdit" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <span class="close d-flex justify-content-end" onclick="closeEdit()">&times;</span>
                    <h4>Chỉnh Cập nhật vai trò </h4>
                    <asp:Label ID="lblErrorMessager" runat="server" CssClass="text-danger pb-2" Text=""></asp:Label>
                    <div class="justify-content-center align-items-center main-edit-modal">
                        <div class="form-group">
                            <label for="txtEditTenVaiTro">Tên vai trò</label>
                            <asp:TextBox ID="txtEditTenVaiTro" runat="server" CssClass="form-control form-control-user" placeholder="Tên vai trò"></asp:TextBox>
                        </div>
                        <div class="form-group row">
                            <div class="col-md-9">
                                <label for="txtEditMa">Mã </label>
                                <asp:TextBox ID="txtEditMa" runat="server" CssClass="form-control form-control-user" placeholder="Mã"></asp:TextBox>
                            </div>
                            <div class="col-md-3">
                                <label for="chkEditTrangThai" class="mb-3">Trạng thái</label>
                                <br />
                                <asp:CheckBox ID="chkEditTrangThai" runat="server" CssClass="form-control-user" />
                                <label class="form-check-label" for="chkEditTrangThai">Hoạt động</label>
                            </div>
                        </div>


                    </div>
                    <div class="d-flex justify-content-end">
                        <asp:Button ID="Button1" runat="server" Text="Cập nhật" class="btn btn-primary mx-1 btn-user btn-modal" OnClick="btnEdit_Click" Style="min-width: 50px;" />
                        <asp:Button ID="Button2" runat="server" Text="Hủy" class="btn btn-secondary mx-1 btn-user btn-modal" OnClientClick="closeEdit(); return false;" Style="min-width: 50px;" />
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>


    <%-- Edit Menu Modal --%>
    <div id="menuEditModal" class="modal">
        <div class="modal-content">
            <asp:UpdatePanel ID="UpdatePanelRoleMenu" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <span class="close d-flex justify-content-end" onclick="closeMenuEditModal()">&times;</span>
                    <h4>Chỉnh Cập nhật role</h4>
                    <div class="justify-content-center align-items-center" style="max-height: 400px; overflow-y: auto; overflow-x: hidden;">
                        <asp:GridView ID="GridRoleMenuPermission" runat="server" AutoGenerateColumns="false"
                            CssClass="table table-striped" CellPadding="10" CellSpacing="2"
                            GridLines="None" Width="100%"
                            OnRowDataBound="GridRoleMenuPermission_RowDataBound" DataKeyNames="MenuName">
                            <Columns>
                                <asp:TemplateField HeaderText="Menu">
                                    <ItemTemplate>
                                        <asp:Label ID="lblMenuName" runat="server" Text='<%# Eval("MenuName")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="LoaiQuyen" HeaderText="Quyền" />
                                <asp:TemplateField HeaderText="Trạng thái">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="CoQuyen" runat="server"
                                            Checked='<%# Eval("CoQuyen").ToString()=="1" %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="MenuId" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblMenuId" runat="server" Text='<%# Eval("MenuId")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="LoaiQuyenId" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblLoaiQuyenId" runat="server" Text='<%# Eval("LoaiQuyenId")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                            </Columns>
                        </asp:GridView>

                        <hr>
                    </div>
                    <div class="d-flex justify-content-end">
                        <asp:Button ID="btnEditMenu" runat="server" Text="Cập nhật" class="btn btn-primary mx-1 btn-user btn-modal" OnClick="btnMenuEditSave_Click" />
                        <asp:Button ID="btnCancelEditMenu" runat="server" Text="Hủy" class="btn btn-secondary mx-1 btn-user btn-modal" OnClientClick="closeMenuEditModal(); return false;" />
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
        function openMenuEditModal() {
            document.getElementById("menuEditModal").style.display = "block";
            return false;
        }

        function closeMenuEditModal() {
            document.getElementById("menuEditModal").style.display = "none";

        }
    </script>


</asp:Content>
