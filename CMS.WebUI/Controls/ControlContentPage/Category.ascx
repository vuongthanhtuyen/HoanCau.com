<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Category.ascx.cs" Inherits="CMS.WebUI.Controls.ControlContentPage.Category" %>
<%@ Register Src="~/Controls/SlideTop.ascx" TagPrefix="uc1" TagName="SlideTop" %>
<%@ Register Src="~/Controls/DanhSachBaiViet.ascx" TagPrefix="uc1" TagName="DanhSachBaiViet" %>


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

