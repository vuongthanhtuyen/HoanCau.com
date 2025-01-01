<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ChiTietNganhDaoTao.ascx.cs" Inherits="CMS.WebUI.Controls.ChiTietNganhDaoTao" %>

<div class="wrapEvent bgColor2 wrapContent3 wrapPartner">
    <div class="container containerItem">
        <div class="contentItem">
            <div class="listItem">
                <asp:Literal runat="server" ID="ltrMain" EnableViewState="false"></asp:Literal>
                <div runat="server" visible="false" enableviewstate="false" id="templateDetail">

                    <div class="container-car-detail">
                        <div data-aos="fade-up" class="container-menu-detail aos-init">
                        </div>
                        <div class="container-white">
                            <div class="product-detail">
                                <div class="row margin-0-on-mobile-tablet">
                                    <div class="col-xl-6 col-sm-12 col-md-12 col-lg-12 col-12">
                                        <div class="product-detail-img ">
                                            <img src="{3}" alt="{0}" class="lazy" style="">
                                        </div>
                                    </div>
                                    <div class="col-xl-6 col-sm-12 col-md-12 col-lg-12">
                                        <div class="product-detail-info">
                                            <h1 class="text-title mb-32 text-left">{0} </h1>
                                            <p class="text product-detail-text mb-32">{1}</p>

                                            <div class="box-tech-compare">
                                                {2}
                                            </div>


                                        </div>
                                        <div class="product-concept-car">
                                            <div class="row margin-0-on-mobile-tablet">
                                                <div class="col-4 col-xl-4 col-sm-4 col-md-4 col-lg-4">
                                                    <div class="concept-car-info concept-car-border-right ">
                                                        <p class="concept-car-name">Mã ngành</p>
                                                        <p class="concept-car-value">{4}</p>

                                                    </div>


                                                </div>
                                                <div class="col-4 col-xl-4 col-sm-4 col-md-4 col-lg-4 ">
                                                    <div class="concept-car-info concept-car-border-right ">
                                                        <p class="concept-car-name">Số tín chỉ</p>
                                                        <p class="concept-car-value">{5}</p>
                                                    </div>


                                                </div>
                                                <div class="col-4 col-xl-4 col-sm-4 col-md-4 col-lg-4">
                                                    <div class="concept-car-info ">
                                                        <p class="concept-car-name">Thời gian học</p>
                                                        <p class="concept-car-value">{6}</p>
                                                    </div>


                                                </div>
                                            </div>
                                        </div>
                                        <div class="product-concept-car-bellow">
                                            <div class="row margin-0-on-mobile-tablet">
                                                <div class="col-5">
                                                    <div class="concept-car-info concept-car-border-right">
                                                        <p class="concept-car-name">Học phí</p>
                                                        <p class="concept-car-value">{7}</p>
                                                    </div>


                                                </div>
                                                <div class="col-7">
                                                    <div class="concept-car-info">
                                                        <p class="concept-car-name">Điều kiện nhập học</p>
                                                        <p class="concept-car-value">{8}</p>



                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>


                        <div class="container-white" id="scrollToCarDiscovery">
                        </div>



                        <div class="container-white">

                            <div class="container-exterior mt-32" id="data-exterior">
                                {9}

                            </div>
                        </div>
                    </div>
                    <div class="row rowList justify-content-center row-cols-1 row-cols-sm-2">
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>
