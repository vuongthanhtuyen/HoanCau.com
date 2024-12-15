<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Administration/MasterPage/AdminPage.Master" CodeBehind="SlideWeb.aspx.cs" Inherits="CMS.WebUI.Administration.QuanLyCauHinh.SlideWeb" %>

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
        <%--        <asp:UpdatePanel ID="UpdatePanelMainTable" UpdateMode="Conditional" runat="server">
            <ContentTemplate>--%>
        <asp:GridView ID="GridViewTable" runat="server" AutoGenerateColumns="false"
            CssClass="table table-striped table-bordered" CellPadding="10" CellSpacing="2"
            GridLines="None" OnRowCommand="GridViewTable_RowCommand">
            <Columns>
                <asp:BoundField DataField="Stt" HeaderText="STT" />
                <asp:BoundField DataField="NoiDungMot" HeaderText="Tiêu đề 1" />
                <asp:BoundField DataField="NoiDungHai" HeaderText="Tiêu đề 2" />
                <asp:TemplateField HeaderText="Logo">
                    <ItemTemplate>
                        <img src='<%# Eval("HinhAnhUrl", "/Administration/UploadImage/{0}") %>'
                            alt="Company Logo" style="width: 100px; height: 100px; object-fit: cover;" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="LienKetUrl" HeaderText="Liên kết" />
                <asp:BoundField DataField="NgayTao" HeaderText="Ngày tạo" DataFormatString="{0:dd/MM/yyyy}" HtmlEncode="false" />
                <asp:TemplateField HeaderText="Hiển thị">
                    <ItemTemplate>
                        <asp:CheckBox ID="TrangThai" runat="server"
                            Checked='<%# Eval("TrangThai") %>' Enabled="False" />
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
        <%--            </ContentTemplate>
            <Triggers>
    <asp:AsyncPostBackTrigger ControlID="Button1" EventName="Click" />

</Triggers>
        </asp:UpdatePanel>--%>
    </main>


    <uc1:PagingAdmin runat="server" ID="PagingAdminWeb" />

    <%-- Modal add new Bài viết --%>
    <div id="myModal" class="modal">
        <div class="modal-content">
            <span class="close d-flex justify-content-end" onclick="closeModal()">&times;</span>
            <h4>Lưu slide </h4>
            <asp:Label ID="lblAddErrorMessage" runat="server" CssClass="text-danger pb-2" Text=""></asp:Label>
            <div class="row justify-content-center main-edit-modal">
                <!-- Form group for Post Title and Slug -->
                <div class="col-md-11">
                    <div class="form-group">
                        <label for="txtTieuDeMot">Tiêu đề 1</label>
                        <asp:TextBox ID="txtTieuDeMot" runat="server" CssClass="form-control form-control-user" placeholder="Tiêu đề 1"></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <label for="txtTieuDeHai">Tiêu đề 2</label>
                        <asp:TextBox ID="txtTieuDeHai" runat="server" CssClass="form-control form-control-user" placeholder="Tiêu đề 2"></asp:TextBox>
                    </div>
                    <div class="form-group">
                    </div>

                    <div class="form-group row">
                        <div class="col-md-8">
                            <label for="txtLienKetUrl">Liên kết trang mới</label>
                            <asp:TextBox ID="txtLienKetUrl" runat="server" CssClass="form-control form-control-user" placeholder="Liên kết "></asp:TextBox>
                        </div>
                        <div class="col-md-4">
                            <label for="txtEditStt">Số thứ tự</label>
                            <asp:TextBox ID="txtStt" runat="server" placeholder="Số thứ tự" CssClass="form-control form-control-user"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group">
                        <label for="txtThumbnailUrl">Ảnh Thumbnail</label>
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

    <%--   <asp:UpdatePanel ID="UpdatePanelId" runat="server">
        <ContentTemplate>
        </ContentTemplate>
    </asp:UpdatePanel>--%>

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
    <script>

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
