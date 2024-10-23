<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PagingAdmin.ascx.cs" Inherits="CMS.WebUI.Administration.AdminUserControl.PagingAdmin" %>
<style>
     ul.pagination li {
     cursor: pointer;
 }
</style>

<asp:HiddenField ID="hdPageIndex" runat="server" />
<div class="d-flex justify-content-center align-items-center" style="width: 100%; position: relative;">
    <nav aria-label="...">
        <ul class="pagination">
           <asp:Literal ID="ltlPaging" runat="server"></asp:Literal>
        </ul>
    </nav>
</div>
