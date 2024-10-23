<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Administration/MasterPage/AdminPage.Master" CodeBehind="DanhMucDuAnTieuBieu.aspx.cs" Inherits="CMS.WebUI.Administration.QuanLyDuAnTieuBieu.DanhMucDuAnTieuBieu" %>


<%@ Register Src="~/Administration/AdminUserControl/AdminNotification.ascx" TagPrefix="uc1" TagName="AdminNotification" %>
<%@ Register Src="~/Administration/AdminUserControl/PagingAdmin.ascx" TagPrefix="uc1" TagName="PagingAdmin" %>
<%@ Register Src="~/Administration/AdminUserControl/SearchUserControl.ascx" TagPrefix="uc1" TagName="SearchUserControl" %>



<asp:Content ID="ctSearch" ContentPlaceHolderID="ctSearch" runat="server">
    <uc1:SearchUserControl runat="server" ID="SearchUserControl" />
</asp:Content>

<asp:Content ID="HeadContent" ContentPlaceHolderID="head" runat="server">
    <link href="Assets/css/Modal.css" rel="stylesheet" />
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
        <asp:UpdatePanel ID="UpdatePanelMainTable" runat="server" UpdateMode="Conditional">
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
                        <asp:BoundField DataField="TenDanhMucCha" HeaderText="Danh mục cha" />
                        <asp:BoundField DataField="Ten" HeaderText="Tên" />
                        <asp:BoundField DataField="Slug" HeaderText="Mã" />
                        <asp:BoundField DataField="MoTa" HeaderText="Mô Tả ngắn" />
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
                    <EmptyDataTemplate>
                        <asp:Label ID="Label3" runat="server" Text="Không có dữ liệu"></asp:Label>
                    </EmptyDataTemplate>
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
                    <h4>Thêm mới một category </h4>
                    <asp:Label ID="lblAddErrorMessage" runat="server" CssClass="text-danger pb-2" Text=""></asp:Label>
                    <div class="justify-content-center align-items-center main-edit-modal">
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
            <asp:UpdatePanel ID="UpdatePanelEdit" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <span class="close d-flex justify-content-end" onclick="closeEdit()">&times;</span>
                    <h4>Chỉnh Cập nhật vai trò </h4>
                    <asp:Label ID="Label1" runat="server" CssClass="text-danger pb-2" Text=""></asp:Label>
                    <div class="justify-content-center align-items-center main-edit-modal">

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
                    <div class="d-flex justify-content-end">
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

    </script>


</asp:Content>
