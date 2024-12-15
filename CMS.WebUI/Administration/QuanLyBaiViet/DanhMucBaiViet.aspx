<%@ Page Language="C#" ValidateRequest="false" AutoEventWireup="true" MasterPageFile="~/Administration/MasterPage/AdminPage.Master" CodeBehind="DanhMucBaiViet.aspx.cs" Inherits="CMS.WebUI.Administration.QuanLyBaiViet.DanhMucBaiViet" %>


<%@ Register Src="~/Administration/AdminUserControl/AdminNotification.ascx" TagPrefix="uc1" TagName="AdminNotification" %>



<asp:Content ID="test" ContentPlaceHolderID="head" runat="server">
</asp:Content>


<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
     
    <main style="min-height: 500px;">
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

    <%-- Modal --%>
    <div id="myModal" class="modal">
        <div class="modal-content">
            <asp:UpdatePanel ID="UpdatePanelEdit" UpdateMode="Conditional" runat="server">
                <ContentTemplate>
                    <span class="close d-flex justify-content-end" onclick="closeModal()">&times;</span>
                    <h4 runat="server" id="lblTitle">Lưu </h4>
                    <asp:HiddenField ID="HiddenIDDanhMuc" runat="server" />
                    <asp:Label ID="lblErrorMessage" runat="server" CssClass="text-danger pb-2" Text=""></asp:Label>
                    <div class="justify-content-center align-items-center main-edit-modal" style="max-height: 500px; overflow-y: auto; overflow-x: hidden;">
                        <ul class="nav nav-tabs" id="myTab" role="tablist">
                            <li class="nav-item" role="presentation">
                                <button class="nav-link active" id="baiViet-tab" data-toggle="tab" data-target="#baiViet" type="button" role="tab" aria-controls="baiViet" aria-selected="true">Danh Mục</button>
                            </li>
                            <li class="nav-item" runat="server" id="tabBaiViet" visible="false" role="presentation">
                                <button class="nav-link" id="danhMuc-tab" data-toggle="tab" data-target="#danhMuc" type="button" role="tab" aria-controls="danhMuc" aria-selected="false">Bài viết</button>
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
                                            <asp:TextBox ID="txtEditTen" runat="server" CssClass="validate[required] form-control form-control-user" placeholder="Tên danh mục"></asp:TextBox>
                                        </div>
                                        <div class="col-md-6">
                                            <label for="txtEditMa">Url thân thiện</label>
                                            <asp:TextBox ID="txtEditMa" runat="server" CssClass="validate[required] form-control form-control-user" placeholder="Nhập url thân thiện"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label for="txtEditMota">Mô tả danh mục</label>
                                        <asp:TextBox ID="txtEditMota" runat="server" CssClass=" form-control form-control-user" placeholder="Nội dung" Height="200px" TextMode="MultiLine"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="tab-pane fade" style="min-height: 100px" id="danhMuc" role="tabpanel" aria-labelledby="danhMuc-tab">
                                <div class="col-md-12 mt-3">
                                    <div class="form-group">
                                        <asp:GridView ID="GridBaiVietInDanhMuc" runat="server" AutoGenerateColumns="false"
                                            CssClass="table table-striped" CellPadding="10" CellSpacing="2"
                                            GridLines="None" Width="100%" Style="border: 1px solid #cccccc;">
                                            <Columns>
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

                            <a class="btn btn-danger btn-xs btn-flat" runat="server" visible="false" id="btnXoa" onclick="openDelete()" style="min-width: 50px;"><span class="fa fa-trash"></span>Xóa</a>
                            <asp:Button ID="btnSave" runat="server" Text="Lưu" class="btn btn-primary mx-1 btn-user btn-modal" OnClick="btnSave_Click" Style="min-width: 50px;" />
                            <asp:Button ID="btnCancal" runat="server" Text="Hủy" class="btn btn-secondary mx-1 btn-user btn-modal" OnClientClick="closeModal(); return false;" Style="min-width: 50px;" />
                        </div>
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
            return false;
        }
        function closeModal() {
            document.getElementById("myModal").style.display = "none";
            history.pushState(null, '', window.location.pathname)
            return false;
        }

        function openDelete() {
            document.getElementById("confirmDeleteModal").style.display = "block";
            return false;
        }
        function closeDelete() {
            document.getElementById("confirmDeleteModal").style.display = "none";
            return false;
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
    <script src="/Assets/validation-engine/js/languages/jquery.validationEngine-vi.js" type="text/javascript" charset="utf-8"></script>
    <script src="/Assets/validation-engine/js/jquery.validationEngine.js" type="text/javascript" charset="utf-8"></script>

    <script>

        function CheckValid() {
            var validated = $("#<%= UpdatePanelEdit.ClientID%>").validationEngine('validate', { promptPosition: "topLeft", scroll: false });
            if (validated)
                return validated;
        };
        function DisableContentChanged() {
            window.onbeforeunload = null;
        };

    </script>
</asp:Content>
