<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DoiTacPublish.aspx.cs" Inherits="CMS.WebUI.DoiTacPublish1" %>


<%@ Register Src="~/Controls/SlideTop.ascx" TagPrefix="uc1" TagName="SlideTop" %>
<%@ Register Src="~/Controls/DanhSachBaiViet.ascx" TagPrefix="uc1" TagName="DanhSachBaiViet" %>



<asp:Content ID="ContentHead" ContentPlaceHolderID="ContentHead" runat="server">
    <link rel="stylesheet" type="text/css" href="/Assets/css/partner.css?v=f81a959662efae2fc3cc158351e6d90c" />

</asp:Content>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <!-- breadcrumb-->
    <uc1:SlideTop runat="server" ID="SlideTop" />

    <!-- end breadcrumb-->

    <div class="wrapEvent bgColor2 wrapContent3 wrapPartner">
        <div class="container containerItem">
            <div class="contentItem">
                <div class="listItem">
                    <div class="row rowList justify-content-center row-cols-1 row-cols-sm-2 row-cols-lg-3">
                        <asp:Literal ID="ltrShowDoiTac" runat="server"></asp:Literal>

                    </div>
                </div>
            </div>
        </div>
    </div>




</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="contentEnd" runat="server">

</asp:Content>
