<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Administration/MasterPage/AdminPage.Master" CodeBehind="DoiTacWeb.aspx.cs" Inherits="CMS.WebUI.Administration.DoiTacWeb" %>

<%@ Register Src="~/Administration/AdminUserControl/AdminNotification.ascx" TagPrefix="uc1" TagName="AdminNotification" %>
<%@ Register Src="~/Administration/AdminUserControl/PagingAdmin.ascx" TagPrefix="uc1" TagName="PagingAdmin" %>
<%@ Register Src="~/Administration/AdminUserControl/ImportImage.ascx" TagPrefix="uc1" TagName="ImportImage" %>
<%@ Register Src="~/Administration/AdminUserControl/SummernoteEditor.ascx" TagPrefix="uc1" TagName="SummernoteEditor" %>
<%@ Register Src="~/Administration/AdminUserControl/DuAnTieuBieuUpLoad.ascx" TagPrefix="uc1" TagName="DuAnTieuBieuUpLoad" %>
<%@ Register Src="~/Administration/AdminUserControl/ImportImageEdit.ascx" TagPrefix="uc1" TagName="ImportImageEdit" %>





<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

     
    <main>
        <div>
            <div class="col-xs-12 padding-none header-controls-right">
                <span class="notifications"></span>
                <button class="btn btn-primary btn-sm btn-flat padding-fa mr-4 " id="btnOpenModal" type="button"
                    onclick="openModal()" runat="server">
                    <i class="fa fa-plus"></i>Lưu</button>
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
                <asp:BoundField DataField="Ten" HeaderText="Tên công ty" />
                <asp:TemplateField HeaderText="Logo">
                    <ItemTemplate>
                        <img src='<%# Eval("HinhAnhUrl", "/Administration/UploadImage/{0}") %>'
                            alt="Company Logo" style="width: 100px; height: 100px; object-fit: cover;" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="LienKetUrl" HeaderText="Liên kết công ty" />
                <asp:BoundField DataField="NgayTao" HeaderText="Ngày tạo" DataFormatString="{0:dd/MM/yyyy}" HtmlEncode="false" />
                <asp:TemplateField HeaderText="Hiển thị">
                    <ItemTemplate>
                        <asp:CheckBox ID="TrangThai" runat="server"
                            Checked='<%# Eval("TrangThai") %>' Enabled="False" />
                    </ItemTemplate>
                </asp:TemplateField>
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
        <div class="modal-content">
            <span class="close d-flex justify-content-end" onclick="closeModal()">&times;</span>
            <h4>Lưu bài viết </h4>
            <asp:Label ID="lblAddErrorMessage" runat="server" CssClass="text-danger pb-2" Text=""></asp:Label>
            <div class="row justify-content-center main-edit-modal">
                <!-- Form group for Post Title and Slug -->
                <div class="col-md-11">
                    <div class="form-group">
                        <label for="txtTen">Tên Công ty</label>
                        <asp:TextBox ID="txtTen" runat="server" CssClass="form-control form-control-user" placeholder="Tên công ty đối tác"></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <label for="txtLienKetUrl">Liên kết trang công ty</label>
                        <asp:TextBox ID="txtLienKetUrl" runat="server" CssClass="form-control form-control-user" placeholder="Liên kết "></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <label for="txtThumbnailUrl">Logo công ty</label>
                        <uc1:ImportImage runat="server" ID="txtThumbnailUrl" />
                    </div>
                </div>
            </div>

            <div class="d-flex justify-content-end">
                <asp:Button ID="btnUserAdd" runat="server" Text="Lưu" class="btn btn-primary btn-user mx-1"
                    OnClick="btnAdd_Click" />
                <asp:Button ID="btnCancel" runat="server" Text="Hủy" class="btn btn-secondary btn-user mx-1"
                    OnClientClick="closeModal(); return false;" />
            </div>

        </div>
    </div>

    <%-- Edit User Modal --%>
    <div id="myEditModal" class="modal">
        <div class="modal-content">
            <span class="close d-flex justify-content-end" onclick="closeEdit()">&times;</span>
            <h4>Chỉnh Cập nhật bài viết </h4>
            <asp:Label ID="Label1" runat="server" CssClass="text-danger pb-2" Text=""></asp:Label>
            <div class="row justify-content-center main-edit-modal">
                <!-- Form group for Post Title and Slug -->
                <div class="col-md-11">

                    <div class="form-group">
                        <label for="txtEditTenCongTy">Tên công ty </label>
                        <asp:TextBox ID="txtEditTenCongTy" runat="server" CssClass="form-control form-control-user" placeholder="Tên công ty"></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <label for="txtEditLienKetUrl">Liên kết </label>
                        <asp:TextBox ID="txtEditLienKetUrl" runat="server" CssClass="form-control form-control-user" placeholder="Liên kết Url"></asp:TextBox>
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
                    <!-- Form group for Thumbnail URL -->
                    <div>
                        <div class="form-group">
                            <label for="txtEditThumbnailUrl">URL ảnh đại diện (Thumbnail)</label>
                            <uc1:ImportImageEdit runat="server" ID="ImportImageEdit" />

                        </div>
                    </div>
                </div>
            </div>
                <div class="d-flex justify-content-end">
                    <asp:Button ID="Button1" runat="server" Text="Cập nhật" class="btn btn-primary mx-1 btn-user btn-modal" OnClick="btnEdit_Click" Style="min-width: 50px;" />
                    <asp:Button ID="Button2" runat="server" Text="Hủy" class="btn btn-secondary mx-1 btn-user btn-modal" OnClientClick="closeEdit(); return false;" Style="min-width: 50px;" />
                </div>
            
        </div>
    </div>



    <%-- Modal delete comfirm --%>
    <div id="confirmDeleteModal" class="modal">
        <div class="modal-content">
            <span class="close d-flex justify-content-end" onclick="closeDelete(); return false;">&times;</span>
            <h5>Bạn có chắc chắn muốn xóa đối tác này?</h5>
            <asp:Label ID="Label2" runat="server" CssClass="text-danger pb-2" Text=""></asp:Label>
            <div class="d-flex justify-content-end">
                <asp:Button ID="Button3" runat="server" Text="Xóa" class="btn btn-primary mx-1 btn-user btn-modal" OnClick="btnDelete_Click" />
                <asp:Button ID="Button4" runat="server" Text="Hủy" class="btn btn-secondary mx-1 btn-user btn-modal" OnClientClick="closeDelete(); return false;" />
            </div>
        </div>
    </div>


    <uc1:AdminNotification runat="server" ID="AdminNotificationUserControl" />

    <asp:HiddenField ID="hdnRowId" runat="server" />

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
</asp:Content>
