<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DuAnTieuBieuUpLoad.ascx.cs" Inherits="CMS.WebUI.Administration.AdminUserControl.DuAnTieuBieuUpLoad" %>
<div class="d-flex flex-column align-items-center text-center">
    <div>
        <asp:FileUpload ID="fileUploadDuAnTieuBieu" runat="server" onchange="previewImageDuAnTieuBieu(this)" Style="display: none;" />

        <asp:Image
            onclick="triggerUploadDuAnTieuBieu()"
            CssClass="mt-2 avatar custom-image"
            ID="imagePreview"
            Style="background-color: antiquewhite; max-height: 200px; width: 250px; border: 3px solid #0af; transition: background ease-out 200ms; object-fit: cover; display: block;"
            runat="server"
            ImageUrl="/Administration/UploadImage/addNewImage.png" />
        
        <div class="custom-emoji" onclick="triggerUploadDuAnTieuBieu()" style="cursor: pointer;left: 100px;bottom: 30px;position: relative;">
            <svg xmlns="http://www.w3.org/2000/svg" width="20" height="20" fill="currentColor" class="bi bi-pen" viewBox="0 0 16 16">
                <path d="m13.498.795.149-.149a1.207 1.207 0 1 1 1.707 1.708l-.149.148a1.5 1.5 0 0 1-.059 2.059L4.854 14.854a.5.5 0 0 1-.233.131l-4 1a.5.5 0 0 1-.606-.606l1-4a.5.5 0 0 1 .131-.232l9.642-9.642a.5.5 0 0 0-.642.056L6.854 4.854a.5.5 0 1 1-.708-.708L9.44.854A1.5 1.5 0 0 1 11.5.796a1.5 1.5 0 0 1 1.998-.001m-.644.766a.5.5 0 0 0-.707 0L1.95 11.756l-.764 3.057 3.057-.764L14.44 3.854a.5.5 0 0 0 0-.708z" />
            </svg>
        </div>
    </div>
    
</div>
    <script type="text/javascript">
    function triggerUploadDuAnTieuBieu() {
        document.getElementById('<%= fileUploadDuAnTieuBieu.ClientID %>').click();
    }

        function previewImageDuAnTieuBieu(fileUpload) {
        const file = fileUpload.files[0];
        if (file) {
            const reader = new FileReader();
            reader.onload = function (e) {
                document.getElementById('<%= imagePreview.ClientID %>').src = e.target.result;
            }
            reader.readAsDataURL(file);
        }
    }
    </script>