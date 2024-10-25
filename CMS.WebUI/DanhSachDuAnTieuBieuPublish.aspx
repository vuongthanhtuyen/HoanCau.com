<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DanhSachDuAnTieuBieuPublish.aspx.cs" Inherits="CMS.WebUI.DanhSachDuAnTieuBieuPublish" %>


<%@ Register Src="~/Controls/SlideTop.ascx" TagPrefix="uc1" TagName="SlideTop" %>
<%@ Register Src="~/Controls/DanhSachBaiViet.ascx" TagPrefix="uc1" TagName="DanhSachBaiViet" %>
<%@ Register Src="~/Controls/PagePublish.ascx" TagPrefix="uc1" TagName="PagePublish" %>
<%@ Register Src="~/Controls/DanhSachDuAnTieuBieu.ascx" TagPrefix="uc1" TagName="DanhSachDuAnTieuBieu" %>





<asp:Content ID="ContentHead" ContentPlaceHolderID="ContentHead" runat="server">
    <link rel="stylesheet" type="text/css" href="/Assets/css/project-list.css?v=f81a959662efae2fc3cc158351e6d90c" />
</asp:Content>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <!-- breadcrumb-->
    <uc1:SlideTop runat="server" ID="SlideTop" />

    <!-- end breadcrumb-->
    <div class="wrapProject bgColor2 wrapContent3">
        <div class="container-xxl containerItem">
            <div class="contentItem">
                <div class="wrapList">
                    <div class="row rowList row2">
                        <uc1:DanhSachDuAnTieuBieu runat="server" id="DanhSachDuAnTieuBieu" />
                    </div>
                </div>
                <uc1:PagePublish runat="server" ID="PagePublish" />
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="contentEnd" runat="server">

    <script src="/Assets/js/lib.js?v=f81a959662efae2fc3cc158351e6d90c"></script>

</asp:Content>
