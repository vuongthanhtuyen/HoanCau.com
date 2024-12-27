<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ChiTietThanhVien.ascx.cs" Inherits="CMS.WebUI.Controls.ChiTietThanhVien" %>

<style>
    .section {
        padding: 100px 0;
        position: relative;
    }

    .card {
        border: none;
    }
</style>

<section class="section">
    <div class="container">
        <asp:Literal runat="server" ID="ltrMain" EnableViewState="false"></asp:Literal>
        <div runat="server" visible="false" enableviewstate="false" id="templateDetail">
            <div class="row align-items-center">
                <div class="col-lg-5 col-md-6 col-12">
                    <div class="card team team-primary team-two text-center">
                        <div class="card-img team-image d-inline-block mx-auto rounded overflow-hidden">
                            <img src="{3}" class="img-fluid" alt="">
                        </div>
                    </div>
                </div>
                <!--end col-->

                <div class="col-lg-7 col-md-6 col-12 mt-4 pt-2 mt-sm-0 pt-sm-0">
                    <div class="section-title ms-lg-5">
                        <h4 class="title">{0}</h4>
                        <h6 class="text-primary fw-normal">{1}</h6>
                        <p class="text-muted mt-4">{2}</p>
                    </div>
                </div>
            </div>
            <!--end col-->
        </div>
        <!--end row-->
    </div>
    <!--end container-->
</section>
