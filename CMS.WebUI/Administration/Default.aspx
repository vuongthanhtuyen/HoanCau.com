<%@ Page Language="C#" ValidateRequest="false" AutoEventWireup="true" MasterPageFile="~/Administration/MasterPage/AdminPage.Master" CodeBehind="Default.aspx.cs" Inherits="CMS.WebUI.Administration.Default" %>

<%@ Register Src="~/Administration/AdminUserControl/ImportImage.ascx" TagPrefix="uc1" TagName="ImportImage" %>
<%@ Register Src="~/Administration/AdminUserControl/SummernoteEditor.ascx" TagPrefix="uc1" TagName="SummernoteEditor" %>
<%@ Register Src="~/Administration/AdminUserControl/DuAnTieuBieuUpLoad.ascx" TagPrefix="uc1" TagName="DuAnTieuBieuUpLoad" %>

<asp:Content ID="test" ContentPlaceHolderID="head" runat="server">
    <link href="Assets/plugins/style.min.css" rel="stylesheet" />

</asp:Content>


<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
   <h1 class="text-center">Chào bạn đến trang quản trị HoanCau.com</h1>
   


</asp:Content>

<asp:Content ID="HeadContent" ContentPlaceHolderID="ContentScript" runat="server">

<%--<script src="https://cdnjs.cloudflare.com/ajax/libs/jquery/1.12.1/jquery.min.js"></script>  <!-- 5 include the minified jstree source -->
<script src="https://cdnjs.cloudflare.com/ajax/libs/jstree/3.2.1/jstree.min.js"></script>--%>


</asp:Content>
