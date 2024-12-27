<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ChuongTrinhDaoTao.ascx.cs" Inherits="CMS.WebUI.Controls.ChuongTrinhDaoTao" %>

<style>
    .timeline-header a {
        color: #283388;
    }

    .bg-blue {
        background-color: #283388 !important;
    }

    .timeline > div {
        padding-top: 30px;
    }

    .timeline-footer {
        text-align: right;
    }

    .btn-primary {
        color: #fff;
        background-color: #283388;
        border-color: #283388;
    }
</style>
<div class="wrapEvent bgColor2 wrapContent3 wrapPartner">
    <div class="container containerItem">
        <div class="contentItem">
            <div class="listItem">
                <section class="content">
                    <div class="container-fluid">

                        <!-- Timelime example  -->
                        <div class="row">
                            <div class="col-md-12">
                                <!-- The time line -->
                                <div class="timeline">
                                    <!-- timeline time label -->
                                    <asp:Literal runat="server" ID="ltrMain" EnableViewState="false"></asp:Literal>
                                    <div runat="server" visible="false" enableviewstate="false" id="templateList">
                                        <div class="time-label">
                                            <span class="bg-red">{0}</span>
                                        </div>
                                        {1}
                                    </div>
                                    <div runat="server" visible="false" enableviewstate="false" id="templateItem">
                                        <div>
                                            <i class="fas fa-arrow-right  bg-blue"></i>
                                            <div class="timeline-item">
                                                <span class="time"><i class="fas fa-clock"></i>{4}</span>
                                                <h3 class="timeline-header"><a href="{1}">{0}</a></h3>

                                                <div class="timeline-body row">
                                                    <a class="col-md-4" href="{1}">
                                                        <img src="{2}" style="max-width: 300px;" alt="...">
                                                    </a>
                                                    <div class="col-md-8">
                                                        <div class="row">
                                                            <div class="col-md-4 col-sm-6 col-12">
                                                                <div class="info-box">
                                                                    <span class="info-box-icon bg-info"><i class="far fa-calendar-alt"></i></span>

                                                                    <div class="info-box-content">
                                                                        <span class="info-box-text">Thời gian học</span>
                                                                        <span class="info-box-number">{4}</span>
                                                                    </div>
                                                                    <!-- /.info-box-content -->
                                                                </div>
                                                                <!-- /.info-box -->
                                                            </div>
                                                            <!-- /.col -->
                                                            <div class="col-md-4 col-sm-6 col-12">
                                                                <div class="info-box">
                                                                    <span class="info-box-icon bg-success"><i class="fas fa-graduation-cap"></i></span>

                                                                    <div class="info-box-content">
                                                                        <span class="info-box-text">Tổng số tín chỉ</span>
                                                                        <span class="info-box-number">{5}</span>
                                                                    </div>
                                                                    <!-- /.info-box-content -->
                                                                </div>
                                                                <!-- /.info-box -->
                                                            </div>
                                                            <!-- /.col -->
                                                            <div class="col-md-4 col-sm-6 col-12">
                                                                <div class="info-box">
                                                                    <span class="info-box-icon bg-warning"><i class="fas fa-eye"></i></span>

                                                                    <div class="info-box-content">
                                                                        <span class="info-box-text">Tổng lượt xem</span>
                                                                        <span class="info-box-number">{6}</span>
                                                                    </div>
                                                                    <!-- /.info-box-content -->
                                                                </div>
                                                                <!-- /.info-box -->
                                                            </div>
                                                            <!-- /.col -->

                                                            <!-- /.col -->
                                                        </div>
                                                        {3}
                                                    </div>

                                                </div>
                                                <div class="timeline-footer">


                                                    <a class="btn btn-primary btn-sm" href="{1}">Xem thêm</a>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <!-- END timeline item -->
                                    <div>
                                        <i class="fas fa-clock bg-gray"></i>
                                    </div>
                                </div>
                            </div>
                            <!-- /.col -->
                        </div>
                    </div>
                    <!-- /.timeline -->

                </section>
                <div class="row rowList justify-content-center row-cols-1 row-cols-sm-2">
                </div>
            </div>

        </div>
    </div>
</div>
<!-- end partner-->

