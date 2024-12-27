<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="ContentPage.aspx.cs" Inherits="CMS.WebUI.ContentPage" %>

<%@ Register Src="~/Controls/ControlContentPage/BaiVietPublish.ascx" TagPrefix="uc1" TagName="BaiVietPublishControl" %>
<%@ Register Src="~/Controls/ControlContentPage/Category.ascx" TagPrefix="uc1" TagName="CategoryControl" %>
<%@ Register Src="~/Controls/ControlContentPage/BaiVietGioiThieu.ascx" TagPrefix="uc1" TagName="BaiVietGioiThieu" %>
<%@ Register Src="~/Controls/ControlContentPage/DuAnTieuBieu.ascx" TagPrefix="uc1" TagName="DuAnTieuBieuControl" %>
<%@ Register Src="~/Controls/ControlContentPage/FileDinhKemControl.ascx" TagPrefix="uc1" TagName="FileDinhKemControl" %>
<%@ Register Src="~/Controls/DanhSachThanhVien.ascx" TagPrefix="uc1" TagName="DanhSachThanhVien" %>
<%@ Register Src="~/Controls/SlideTop.ascx" TagPrefix="uc1" TagName="SlideTop" %>
<%@ Register Src="~/Controls/ChiTietThanhVien.ascx" TagPrefix="uc1" TagName="ChiTietThanhVien"  %>
<%@ Register Src="~/Controls/ChuongTrinhDaoTao.ascx" TagPrefix="uc1" TagName="ChuongTrinhDaoTao" %>






<asp:Content ID="ContentHead" ContentPlaceHolderID="ContentHead" runat="server">
    <asp:Literal ID="ltrHead" runat="server" EnableViewState="false"></asp:Literal>
</asp:Content>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    
    <uc1:SlideTop runat="server" ID="ctrlSlideTop" Visible ="false" />
    <uc1:BaiVietPublishControl runat="server" id="ctrlBaiVietPublish" Visible ="false" />
    <uc1:CategoryControl runat="server" id="ctrlCategory" Visible ="false"/>
    <uc1:BaiVietGioiThieu runat="server" ID="ctrlBaiVietGioiThieu" Visible ="false" />
    <uc1:DuAnTieuBieuControl runat="server" id="ctrlDuAnTieuBieu" Visible ="false"/>
    <uc1:FileDinhKemControl runat="server" id="ctrlFileDinhKemControl" Visible ="false" />
    <uc1:DanhSachThanhVien runat="server" id="ctrlDanhSachThanhVien" Visible ="false"/>
    <uc1:ChiTietThanhVien runat="server" ID="ctrlChiTietThanhVien"  Visible="false" />
    <uc1:ChuongTrinhDaoTao runat="server" id="ctrlChuongTrinhDaoTao" Visible="false" />
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="contentEnd" runat="server">

    <asp:Literal ID="ltrBelow" runat="server" EnableViewState="false"></asp:Literal>
</asp:Content>
