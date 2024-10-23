<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DanhMucPublish.aspx.cs" Inherits="CMS.WebUI.DanhMucPublish" %>

<%@ Register Src="~/Controls/SlideTop.ascx" TagPrefix="uc1" TagName="SlideTop" %>
<%@ Register Src="~/Controls/DanhSachBaiViet.ascx" TagPrefix="uc1" TagName="DanhSachBaiViet" %>



<asp:Content ID="ContentHead" ContentPlaceHolderID="ContentHead" runat="server">
    <link rel="stylesheet" type="text/css" href="/Assets/css/news-list.css?v=f81a959662efae2fc3cc158351e6d90c" />
</asp:Content>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <!-- breadcrumb-->
    <uc1:SlideTop runat="server" ID="SlideTop" />

    <!-- end breadcrumb-->

    <div class="wrapNews wrapContent3 bgColor2">
        <div class="container-xxl containerItem">
            <div class="contentItem">
                <div class="row rowList justify-content-center">
                    <uc1:DanhSachBaiViet runat="server" ID="DanhSachBaiViet" />

                   
                </div>
            </div>
        </div>
    </div>




</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="contentEnd" runat="server">

    <script type="text/javascript" src="https://platform-api.sharethis.com/js/sharethis.js?v=f81a959662efae2fc3cc158351e6d90c#property=646b0f87d8c6d2001a06c301&product=inline-share-buttons&source=platform" async="async"></script>

</asp:Content>
