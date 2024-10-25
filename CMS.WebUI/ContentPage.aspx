<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="ContentPage.aspx.cs" Inherits="CMS.WebUI.ContentPage" %>

<%@ Register Src="~/Controls/ControlContentPage/BaiVietPublish.ascx" TagPrefix="uc1" TagName="BaiVietPublishControl" %>
<%@ Register Src="~/Controls/ControlContentPage/Category.ascx" TagPrefix="uc1" TagName="CategoryControl" %>
<%@ Register Src="~/Controls/ControlContentPage/BaiVietGioiThieu.ascx" TagPrefix="uc1" TagName="BaiVietGioiThieu" %>
<%@ Register Src="~/Controls/ControlContentPage/DuAnTieuBieu.ascx" TagPrefix="uc1" TagName="DuAnTieuBieuControl" %>


<asp:Content ID="ContentHead" ContentPlaceHolderID="ContentHead" runat="server">
    <asp:Literal ID="ltrHead" runat="server" EnableViewState="false"></asp:Literal>
</asp:Content>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    
    <uc1:BaiVietPublishControl runat="server" id="ctrlBaiVietPublish" />
    <uc1:CategoryControl runat="server" id="ctrlCategory" />
    <uc1:BaiVietGioiThieu runat="server" ID="ctrlBaiVietGioiThieu" />
    <uc1:DuAnTieuBieuControl runat="server" id="ctrlDuAnTieuBieu" />
    
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="contentEnd" runat="server">
    <asp:Literal ID="ltrBelow" runat="server" EnableViewState="false"></asp:Literal>
</asp:Content>
