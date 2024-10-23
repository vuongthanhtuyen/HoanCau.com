<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SearchUserControl.ascx.cs" Inherits="CMS.WebUI.Administration.AdminUserControl.SearchUserControl" %>
<asp:TextBox ID="txtSearch"
            placeholder="Search for..."
            aria-label="Search"
            aria-describedby="basic-addon2"
            CssClass="form-control bg-light border-0 small"
            runat="server"></asp:TextBox>

        <div class="input-group-append">
            <asp:Button CssClass="btn btn-primary fas fa-search fa-sm" ID="btnSearchFor" runat="server" Text="Tìm kiếm" OnClick="btnSeachFor_click" />
        </div>

<script>
    window.onload = function () {
        var txtSearch = document.getElementById('<%= txtSearch.ClientID %>');
        var btnSearchFor = document.getElementById('<%= btnSearchFor.ClientID %>');

        txtSearch.addEventListener("keypress", function (event) {
            if (event.key === "Enter") {
                event.preventDefault();
                btnSearchFor.click();
            }
        });
    };
</script>

        

