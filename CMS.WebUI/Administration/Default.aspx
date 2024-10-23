<%@ Page Language="C#" ValidateRequest="false" AutoEventWireup="true" MasterPageFile="~/Administration/MasterPage/AdminPage.Master" CodeBehind="Default.aspx.cs" Inherits="CMS.WebUI.Administration.Default" %>

<%@ Register Src="~/Administration/AdminUserControl/ImportImage.ascx" TagPrefix="uc1" TagName="ImportImage" %>
<%@ Register Src="~/Administration/AdminUserControl/SummernoteEditor.ascx" TagPrefix="uc1" TagName="SummernoteEditor" %>
<%@ Register Src="~/Administration/AdminUserControl/DuAnTieuBieuUpLoad.ascx" TagPrefix="uc1" TagName="DuAnTieuBieuUpLoad" %>
<%@ Register Src="~/Administration/AdminUserControl/SearchUserControl.ascx" TagPrefix="uc1" TagName="SearchUserControl" %>

<asp:Content ID="test" ContentPlaceHolderID="head" runat="server">
    <link href="Assets/plugins/style.min.css" rel="stylesheet" />

</asp:Content>


<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
   <h1 class="text-center">Chào bạn đến trang quản trị HoanCau.com</h1>
    <div class="box">
        <div class="box-body">
            <div class="row">
                <div class="col-md-6 col-xs-12 form-group">
                    <label>Tìm kiếm</label>
                    <input type="text" id="txtKeysearch" placeholder="Nhập từ khóa tìm kiếm" class="form-control" />
                </div>
                <div class="col-md-12 form-group">
                    <div class="rightsTreeView"></div>
                    <input runat="server" type="hidden" id="hdfRightsTreeViewData" data-selector="hdfRightsTreeViewData" value="" />
                </div>
                <div class="clearfix"></div>
            </div>
        </div>
    </div>



    <asp:Label ID="lblLabel" runat="server" Text="Thông báo"></asp:Label>


</asp:Content>

<asp:Content ID="HeadContent" ContentPlaceHolderID="ContentScript" runat="server">

<%--<script src="https://cdnjs.cloudflare.com/ajax/libs/jquery/1.12.1/jquery.min.js"></script>  <!-- 5 include the minified jstree source -->
<script src="https://cdnjs.cloudflare.com/ajax/libs/jstree/3.2.1/jstree.min.js"></script>--%>
    <script src="/Administration/Assets/plugins/jstree.min.js"></script>  

    <script>
        $(function () {
            if ($(".rightsTreeView").length) {
                var jsonData = JSON.parse($('[data-selector="hdfRightsTreeViewData"]').val());
                renderTreeView(jsonData);
            }
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
