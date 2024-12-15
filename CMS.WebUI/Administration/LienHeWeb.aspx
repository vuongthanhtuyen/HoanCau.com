<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Administration/MasterPage/AdminPage.Master" CodeBehind="LienHeWeb.aspx.cs" Inherits="CMS.WebUI.Administration.LienHeWeb" %>


<%@ Register Src="~/Administration/AdminUserControl/AdminNotification.ascx" TagPrefix="uc1" TagName="AdminNotification" %>
<%@ Register Src="~/Administration/AdminUserControl/PagingAdmin.ascx" TagPrefix="uc1" TagName="PagingAdmin" %>


<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

     
    <main>
        <div>
            <div class="col-xs-12 padding-none header-controls-right">
                <asp:Label ID="lblResult" CssClass="text-info" runat="server" Text=""></asp:Label>
            </div>
        </div>

        <br />
        <asp:UpdatePanel ID="UpdatePanelMainTable" UpdateMode="Conditional" runat="server">
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
                        <asp:BoundField DataField="ChuDe" HeaderText="Chủ đề" />

                        <asp:BoundField DataField="NgayTao" HeaderText="Ngày tạo" DataFormatString="{0:dd/MM/yyyy}" HtmlEncode="false" />
                        <asp:TemplateField HeaderText="Đã trả lời">
                            <ItemTemplate>
                                <asp:CheckBox ID="DaTraLoi" runat="server"
                                    Checked='<%# Eval("DaTraLoi") %>' Enabled="False" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="NgayTraLoi" HeaderText="Ngày trả lời" DataFormatString="{0:dd/MM/yyyy}" HtmlEncode="false" />

                        <asp:TemplateField HeaderText="Hành Động" HeaderStyle-Width="280px">
                            <ItemTemplate>
                                <asp:LinkButton ID="ChinhSuaChiTiet" runat="server" CssClass="btn btn-primary btn-xs btn-flat"
                                    CommandArgument='<%# Eval("Id") %>' CommandName="ChinhSuaChiTiet" ToolTip="Sửa">
                        <span class="fa fa-eye"></span> Sửa / chi tiết
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
                <asp:AsyncPostBackTrigger ControlID="Button3" EventName="Click" />
            </Triggers>
        </asp:UpdatePanel>
    </main>


    <uc1:PagingAdmin runat="server" ID="PagingAdminWeb" />


    <%-- Edit Một liên hệ Modal --%>
    <div id="myEditModal" class="modal">
        <div class="modal-content">
            <asp:UpdatePanel ID="UpdatePaneEdit" UpdateMode="Conditional" runat="server">
                <ContentTemplate>
                    <span class="close d-flex justify-content-end" onclick="closeEdit()">&times;</span>
                    <h4>Chỉnh sửa slide </h4>
                    <asp:Label ID="Label1" runat="server" CssClass="text-danger pb-2" Text=""></asp:Label>
                    <div class="row justify-content-center main-edit-modal">
                        <!-- Form group for Post Title and Slug -->
                        <div class="col-md-11">
                            <div class="form-group row">
                                <div class="col-md-7">
                                    <label for="txtEditHoVaTen">Họ và tên</label>
                                    <asp:TextBox ID="txtEditHoVaTen" runat="server" CssClass="form-control form-control-user" placeholder="Họ và tên "></asp:TextBox>
                                </div>
                                <div class="col-md-5">
                                    <label for="txtEditSoDienThoai">Số điện thoại</label>
                                    <asp:TextBox ID="txtEditSoDienThoai" runat="server" CssClass="form-control form-control-user" placeholder="Số điện thoại"></asp:TextBox>
                                </div>
                            </div>

                            <div class="form-group row">
                                <div class="col-md-7">
                                    <label for="txtEditEmail">Email</label>
                                    <asp:TextBox ID="txtEditEmail" runat="server" CssClass="form-control form-control-user" placeholder="Email"></asp:TextBox>
                                </div>
                                <div class="col-md-5">
                                    <label for="txtEditNgayTao">Ngày tạo</label>
                                    <asp:TextBox ID="txtEditNgayTao" runat="server" CssClass="form-control form-control-user" TextMode="Date" ReadOnly="true"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group">
                                <label for="txtEditChuDe">Chủ đề</label>
                                <asp:TextBox ID="txtEditChuDe" runat="server" CssClass="form-control form-control-user" placeholder="Chủ đề"></asp:TextBox>
                            </div>
                            <div class="form-group">
                                <label for="txtEditNoiDung">Nội Dung</label>
                                <asp:TextBox ID="txtEditNoiDung" TextMode="MultiLine" runat="server" CssClass="form-control form-control-user" placeholder="Nội dung"></asp:TextBox>
                            </div>
                            <div class="form-group row">
                                <div class="col-md-6">
                                    <label for="chkEditTrangThai" class="mb-3">Trạng thái</label>
                                    <br />
                                    <asp:CheckBox ID="chkEditTrangThai" runat="server" CssClass="form-control-user" />
                                    <label class="form-check-label" for="chkEditTrangThai">Đã trả lời</label>

                                </div>
                                <div class="col-md-6">
                                    <label for="txtEditNgayTraLoi">Ngày trả lời</label>
                                    <asp:TextBox ID="txtEditNgayTraLoi" runat="server" CssClass="form-control form-control-user" TextMode="Date" ReadOnly="true"></asp:TextBox>
                                </div>
                            </div>
                            <!-- Form group for Thumbnail URL -->

                        </div>
                    </div>
                    <div class="d-flex justify-content-end">
                        <asp:Button ID="Button1" runat="server" Text="Sửa" class="btn btn-primary mx-1 btn-user btn-modal" OnClick="btnEdit_Click" Style="min-width: 50px;" />
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
            <h5>Bạn có chắc chắn muốn xóa liên hệ này?</h5>
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
