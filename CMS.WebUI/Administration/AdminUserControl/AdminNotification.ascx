<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AdminNotification.ascx.cs" Inherits="CMS.WebUI.Administration.AdminUserControl.AdminNotification" %>
<asp:UpdatePanel ID="UpdatePanelNotification" runat="server" UpdateMode="Conditional">
    <ContentTemplate>
        <div id="myModalNotification" class="modal" runat="server" >
    <div class="modal-content">
        <span class="close d-flex justify-content-end" onclick="closeModalNotification(); return false; ">&times;</span>
        <h4>Thông báo </h4>
        <asp:Label ID="lblErrorMessage" runat="server" CssClass="text-danger pb-2" Text=""></asp:Label>
        <asp:Label ID="lblSuccessMessage" runat="server" CssClass="text-primary pb-2" Text="Thành công"></asp:Label>

        <div class="d-flex justify-content-end">
            <asp:Button ID="btnUserAdd" runat="server" Text="OK" class="btn btn-primary btn-user mx-1"
                Style="min-width: 80px;" OnClientClick="closeModalNotification(); return false;" />
        </div>
    </div>
</div>
    </ContentTemplate>
</asp:UpdatePanel>

<script>
    function closeModalNotification() {
        // Ẩn modal
        //document.getElementById("myModalNotification").style.display = "none";
        document.getElementById('<%= myModalNotification.ClientID%>').style.display = "none";
        //window.location.href = window.location.href;

    }

</script>

