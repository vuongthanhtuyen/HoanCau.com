<%@ Page Language="C#" MasterPageFile="~/Administration/MasterPage/AdminPage.Master" AutoEventWireup="true" CodeBehind="CaiDatHienThi.aspx.cs" Inherits="CMS.WebUI.Administration.QuanLyCauHinh.CaiDatHienThi" %>


<%@ Import Namespace="SweetCMS.Core.Helper" %>
<%@ Register Src="~/Administration/AdminUserControl/PagingAdmin.ascx" TagPrefix="uc1" TagName="PagingAdmin" %>
<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>


<asp:Content ID="HeadContent" ContentPlaceHolderID="head" runat="server">
    <link href="/Administration/Style/plugins/lightbox-evolution-1.8/theme/default/jquery.lightbox.css" rel="stylesheet" />
    <link href="/Administration/Style/dist/css/imgareaselect-default.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="card card-primary card-outline card-outline-tabs">

        <div class="card-header p-0 border-bottom-0">
            <div class="row">
                <div class="col-md-11">
                    <ul class="nav nav-tabs" id="custom-tabs-four-tab" role="tablist">
                        <li class="nav-item">
                            <a class="nav-link active" id="custom-tabs-four-home-tab" data-toggle="pill" href="#custom-tabs-four-home" role="tab" aria-controls="custom-tabs-four-home" aria-selected="false">Trang chủ</a>
                        </li>
                        <li class="nav-item">
                            <a class="nav-link" id="custom-tabs-four-profile-tab" data-toggle="pill" href="#custom-tabs-four-profile" role="tab" aria-controls="custom-tabs-four-profile" aria-selected="false">Thông tin liên hệ</a>
                        </li>
                        <li class="nav-item">
                            <a class="nav-link" id="custom-tabs-four-messages-tab" data-toggle="pill" href="#custom-tabs-four-messages" role="tab" aria-controls="custom-tabs-four-messages" aria-selected="false">Messages</a>
                        </li>
                        <li class="nav-item float-sm-right">
                            <a class="nav-link " id="custom-tabs-four-settings-tab" data-toggle="pill" href="#custom-tabs-four-settings" role="tab" aria-controls="custom-tabs-four-settings" aria-selected="true">Settings</a>
                        </li>
                    </ul>
                </div>
                <div class="col-md-1 btn-primary btn">
                    <asp:Button ID="btnSave" runat="server" Text="Lưu" Style="all: unset;" OnClick="btnSave_Click" />
                </div>
            </div>
        </div>
        <div class="card-body" style="min-height: 70vh">
            <div class="tab-content" id="custom-tabs-four-tabContent">
                <div class="tab-pane fade active show" id="custom-tabs-four-home" role="tabpanel" aria-labelledby="custom-tabs-four-home-tab">
                    <div class="row">
                        <%-- Giới thiệu --%>

                        <div class="col-md-12">
                            <div class="card card-outline card-primary">
                                <div class="card-header">
                                    <h3 class="card-title">Giới thiệu</h3>

                                    <div class="card-tools">
                                        <button type="button" class="btn btn-tool" data-card-widget="collapse" fdprocessedid="y41igr">
                                            <i class="fas fa-minus"></i>
                                        </button>
                                    </div>
                                    <!-- /.card-tools -->
                                </div>
                                <!-- /.card-header -->
                                <div class="card-body" style="display: block;">
                                    <div class="row">
                                        <!-- Form group for Post Title and Slug -->
                                        <div class="col-md-6">
                                            <div class=" row">
                                                <div class="col-md-12">
                                                    <div class="form-group">
                                                        <input id="chkGioiThieuHienThi" runat="server" type="checkbox" checked />
                                                        <label>Hiển thị  </label>
                                                    </div>
                                                </div>
                                                <div class="col-md-12">
                                                    <div class="form-group">
                                                        <label for="txtTGioiThieuieuDeHome">Tiêu đề</label>
                                                        <input type="text" runat="server" id="txtGioiThieuTieuDeHome" class="form-control form-control-user" placeholder="Nhập nội dung" />
                                                    </div>
                                                </div>

                                                <div class="col-md-12">
                                                    <div class="form-group text-center  justify-content-center">
                                                        <label class="control-label">Hình minh họa</label>
                                                        <label style="margin-left: 5px;" class="small">(Click vào hình ảnh để thay đổi hình minh họa)</label>
                                                        <div class="row text-center d-flex justify-content-center">
                                                            <div class="img-thumbnail">
                                                                <img data-selector="imgThumb" id="imgThumb" runat="server" onclick="OpenSelectImage();" src="../UploadImage/addNewImage.png"
                                                                    style="cursor: pointer; max-width: 300px" class="imgthumb img-responsive" />
                                                            </div>
                                                            <input style="display: none;" data-selector="txtImage" type="text" id="txtImage" runat="server" class="form-control" />
                                                            <div id="divRemoveThumb" runat="server" visible="false">
                                                                <a onclick="RemoveThumbnail();" title="Xóa hình mình họa" class="btn btn-default btn-flat btn-sm">Xóa hình minh họa</a>
                                                            </div>
                                                        </div>
                                                        <div class="clearfix"></div>
                                                    </div>
                                                </div>

                                                <div class="col-md-12">
                                                    <div class="form-group text-center  justify-content-center">
                                                        <label class="control-label">Hình nền</label>
                                                        <label style="margin-left: 5px;" class="small">(Click vào hình ảnh để thay đổi hình nền)</label>
                                                        <div class="row text-center d-flex justify-content-center">
                                                            <div class="img-thumbnail">
                                                                <img data-selector="imgHinhNen" id="imgHinhNen" runat="server" onclick="OpenHinhNenImage();" src="../UploadImage/addNewImage.png"
                                                                    style="cursor: pointer; max-width: 300px" class="imgthumb img-responsive" />
                                                            </div>
                                                            <input style="display: none;" data-selector="txtHinhNen" type="text" id="txtHinhNen" runat="server" class="form-control" />
                                                        
                                                        </div>
                                                        <div class="clearfix"></div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="col-md-6">
                                            <div class="col-md-12">
                                                <div class="form-group">
                                                    <label for="txtGioiThieuContentHome">Nội dung</label>
                                                    <CKEditor:CKEditorControl ID="txtGioiThieuContentHome" Width="100%" CssClass="ck-editor"
                                                        Toolbar="Full" BodyId="StaticPageContent" Language="en-US" AutoParagraph="false"
                                                        BasePath="/Administration/Style/plugins/ckeditor/" runat="server" Height="300">
                                                    </CKEditor:CKEditorControl>
                                                </div>
                                            </div>
                                        </div>


                                    </div>
                                </div>
                                <!-- /.card-body -->
                            </div>
                            <!-- /.card -->
                        </div>

                        <!-- /.col -->
                        <%-- khám phá --%>
                        <div class="col-md-12">
                            <div class="card card-outline card-primary">
                                <div class="card-header">
                                    <h3 class="card-title">Khám phá</h3>

                                    <div class="card-tools">
                                        <button type="button" class="btn btn-tool" data-card-widget="collapse" fdprocessedid="y41igr">
                                            <i class="fas fa-minus"></i>
                                        </button>
                                    </div>
                                    <!-- /.card-tools -->
                                </div>
                                <!-- /.card-header -->
                                <div class="card-body" style="display: block;">
                                    <div class="row">
                                        <!-- Form group for Post Title and Slug -->
                                        <div class="col-md-6">
                                            <div class=" row">
                                                <div class="col-md-12">
                                                    <div class="form-group">
                                                        <input id="chkKhamPhaHienThiHome" runat="server" type="checkbox" checked />
                                                        <label>Hiển thị  </label>

                                                    </div>
                                                </div>
                                                <div class="col-md-12">
                                                    <div class="form-group">
                                                        <label for="txtKhamPhaTieuDeHome">Tiêu đề</label>
                                                        <input type="text" runat="server" id="txtKhamPhaTieuDeHome" class="form-control form-control-user" placeholder="Nhập nội dung" />
                                                    </div>
                                                </div>
                                                <div class="col-md-12">
                                                    <div class="form-group">
                                                        <label for="txtKhamPhaNoiDungHome">Nội dung</label>
                                                        <textarea runat="server" id="txtKhamPhaNoiDungHome" class="form-control form-control-user" placeholder="Nhập nội dung" rows="5"></textarea>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="col-md-6">
                                            <div class="row">

                                                <div class="col-md-12">
                                                    <div class="form-group">
                                                        <label for="txtKhamPhaTamNhinHome">Tầm nhìn</label>
                                                        <textarea runat="server" id="txtKhamPhaTamNhinHome" class="form-control form-control-user" placeholder="Nhập nội dung" rows="2"></textarea>
                                                    </div>
                                                </div>

                                                <div class="col-md-12">
                                                    <div class="form-group">
                                                        <label for="txtKhamPhaSuMenhHome">Sứ mệnh</label>
                                                        <textarea runat="server" id="txtKhamPhaSuMenhHome" class="form-control form-control-user" placeholder="Nhập nội dung" rows="2"></textarea>
                                                    </div>
                                                </div>
                                                <div class="col-md-12">
                                                    <div class="form-group">
                                                        <label for="txtKhamPhaGiaTriCotLoiHome">Giá trị cốt lõi</label>
                                                        <textarea runat="server" id="txtKhamPhaGiaTriCotLoiHome" class="form-control form-control-user" placeholder="Nhập nội dung" rows="2"></textarea>
                                                    </div>
                                                </div>

                                            </div>
                                        </div>


                                    </div>
                                </div>
                                <!-- /.card-body -->
                            </div>
                            <!-- /.card -->
                        </div>
                        <!-- /.col -->
                        <%-- Ngành đào tạo --%>
                        <div class="col-md-12">
                            <div class="card card-outline card-primary">
                                <div class="card-header">
                                    <h3 class="card-title">Ngành đào tạo</h3>

                                    <div class="card-tools">
                                        <button type="button" class="btn btn-tool" data-card-widget="collapse" fdprocessedid="y41igr">
                                            <i class="fas fa-minus"></i>
                                        </button>
                                    </div>
                                    <!-- /.card-tools -->
                                </div>
                                <!-- /.card-header -->
                                <div class="card-body" style="display: block;">
                                    <div class="row">
                                        <!-- Form group for Post Title and Slug -->
                                        <div class="col-md-6">
                                            <div class=" row">
                                                <div class="col-md-12">
                                                    <div class="form-group">
                                                        <input id="chkNganhDaoTaoHienThiHome" runat="server" type="checkbox" checked />
                                                        <label>Hiển thị  </label>
                                                    </div>
                                                </div>
                                                <div class="col-md-12">
                                                    <div class="form-group">
                                                        <label for="txtNganhDaoTaoSoLuongHome">Số lượng hiển thị</label>
                                                        <input type="number" value="5" runat="server" id="txtNganhDaoTaoSoLuongHome" class="form-control form-control-user" placeholder="Nhập nội dung" />
                                                    </div>
                                                </div>
                                                <div class="col-md-12">
                                                    <label>Nhóm ngành hiển thị</label>
                                                    <asp:DropDownList runat="server" ID="ddlNganhDaoTaoNhomHone" CssClass="form-control custom-select">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="col-md-6">
                                        </div>


                                    </div>
                                </div>
                                <!-- /.card-body -->
                            </div>
                            <!-- /.card -->
                        </div>
                        <!-- /.col -->
                        <%-- quy mô --%>
                        <div class="col-md-12">
                            <div class="card card-outline card-primary">
                                <div class="card-header">
                                    <h3 class="card-title">Quy mô</h3>

                                    <div class="card-tools">
                                        <button type="button" class="btn btn-tool" data-card-widget="collapse" fdprocessedid="y41igr">
                                            <i class="fas fa-minus"></i>
                                        </button>
                                    </div>
                                    <!-- /.card-tools -->
                                </div>
                                <!-- /.card-header -->
                                <div class="card-body" style="display: block;">
                                    <div class="row">
                                        <!-- Form group for Post Title and Slug -->

                                        <div class="col-md-12">
                                            <div class="form-group">
                                                <input id="chkQuyMoHienThiHome" runat="server" type="checkbox" checked />
                                                <label>Hiển thị  </label>
                                            </div>
                                        </div>
                                        <div class="col-md-6">
                                            <div class="form-group">
                                                <label for="txtSoLuongTieuDe1Home">Tiêu đề</label>
                                                <input type="text" runat="server" id="txtSoLuongTieuDe1Home" class="form-control form-control-user" placeholder="Nhập nội dung" />
                                            </div>
                                        </div>
                                        <div class="col-md-6">
                                            <div class="form-group">
                                                <label for="txtQuyMoSoLuongHome">Quy Mô</label>
                                                <input type="number" value="5" runat="server" id="txtQuyMoSoLuongHome" class="form-control form-control-user" placeholder="Nhập nội dung" />
                                            </div>
                                        </div>

                                        <div class="col-md-6">
                                            <div class="form-group">
                                                <label for="txtQuyMoTieuDe2Home">Tiêu đề</label>
                                                <input type="text" runat="server" id="txtQuyMoTieuDe2Home" class="form-control form-control-user" placeholder="Nhập nội dung" />
                                            </div>
                                        </div>
                                        <div class="col-md-6">
                                            <div class="form-group">
                                                <label for="txtQuyMosoLuong2Home">Quy Mô</label>
                                                <input type="number" value="5" runat="server" id="txtQuyMosoLuong2Home" class="form-control form-control-user" placeholder="Nhập nội dung" />
                                            </div>
                                        </div>


                                        <div class="col-md-6">
                                            <div class="form-group">
                                                <label for="txtQuyMoTieuDe3Home">Tiêu đề</label>
                                                <input type="text" runat="server" id="txtQuyMoTieuDe3Home" class="form-control form-control-user" placeholder="Nhập nội dung" />
                                            </div>
                                        </div>
                                        <div class="col-md-6">
                                            <div class="form-group">
                                                <label for="txtQuyMosoLuong3Home">Quy Mô</label>
                                                <input type="number" value="5" runat="server" id="txtQuyMosoLuong3Home" class="form-control form-control-user" placeholder="Nhập nội dung" />
                                            </div>
                                        </div>

                                        <div class="col-md-6">
                                            <div class="form-group">
                                                <label for="txtQuyMoTieuDe4Home">Tiêu đề</label>
                                                <input type="text" runat="server" id="txtQuyMoTieuDe4Home" class="form-control form-control-user" placeholder="Nhập nội dung" />
                                            </div>
                                        </div>
                                        <div class="col-md-6">
                                            <div class="form-group">
                                                <label for="txtQuyMosoLuong4Home">Quy Mô</label>
                                                <input type="number" value="5" runat="server" id="txtQuyMosoLuong4Home" class="form-control form-control-user" placeholder="Nhập nội dung" />
                                            </div>
                                        </div>

                                        <div class="col-md-6">
                                            <div class="form-group">
                                                <label for="txtQuyMoTieuDe5Home">Tiêu đề</label>
                                                <input type="text" runat="server" id="txtQuyMoTieuDe5Home" class="form-control form-control-user" placeholder="Nhập nội dung" />
                                            </div>
                                        </div>
                                        <div class="col-md-6">
                                            <div class="form-group">
                                                <label for="txtQuyMosoLuong5Home">Quy Mô</label>
                                                <input type="number" value="5" runat="server" id="txtQuyMosoLuong5Home" class="form-control form-control-user" placeholder="Nhập nội dung" />
                                            </div>
                                        </div>

                                    </div>

                                </div>
                                <!-- /.card-body -->
                            </div>
                            <!-- /.card -->
                        </div>
                        <!-- /.col -->

                        <%-- Sự kiện nổi bật --%>
                        <div class="col-md-12">
                            <div class="card card-outline card-primary">
                                <div class="card-header">
                                    <h3 class="card-title">Sự kiện</h3>

                                    <div class="card-tools">
                                        <button type="button" class="btn btn-tool" data-card-widget="collapse" fdprocessedid="y41igr">
                                            <i class="fas fa-minus"></i>
                                        </button>
                                    </div>
                                </div>
                                <div class="card-body" style="display: block;">
                                    <div class="row">

                                        <div class="col-md-12">
                                            <div class="form-group">
                                                <input id="chkSuKienHienThi" runat="server" type="checkbox" checked />
                                                <label>Hiển thị  </label>
                                            </div>
                                        </div>
                                        <div class="col-md-6">
                                            <div class="form-group">
                                                <label for="txtSuKienSoLuongHome">Số lượng</label>
                                                <input type="number" value="5" runat="server" id="txtSuKienSoLuongHome" class="form-control form-control-user" placeholder="Nhập nội dung" />
                                            </div>
                                        </div>
                                        <div class="col-md-6">
                                            <label>Nhóm sự kiện</label>
                                            <asp:DropDownList runat="server" ID="ddlSuKienNhomHone" CssClass="form-control custom-select">
                                            </asp:DropDownList>
                                        </div>
                                    </div>

                                </div>
                            </div>
                        </div>



                        <%-- Thành viên --%>
                        <div class="col-md-12">
                            <div class="card card-outline card-primary">
                                <div class="card-header">
                                    <h3 class="card-title">Thành viên</h3>

                                    <div class="card-tools">
                                        <button type="button" class="btn btn-tool" data-card-widget="collapse" fdprocessedid="y41igr">
                                            <i class="fas fa-minus"></i>
                                        </button>
                                    </div>
                                </div>
                                <div class="card-body" style="display: block;">
                                    <div class="row">

                                        <div class="col-md-12">

                                            <div class="form-group">
                                                <input id="chkThanhVienHienThi" runat="server" type="checkbox" checked />
                                                <label>Hiển thị  </label>


                                            </div>
                                        </div>
                                        <div class="col-md-6">
                                            <div class="form-group">
                                                <label for="txtThanhVienSoLuongHome">Số lượng</label>
                                                <input type="number" value="5" runat="server" id="txtThanhVienSoLuongHome" class="form-control form-control-user" placeholder="Nhập nội dung" />
                                            </div>
                                        </div>
                                        <div class="col-md-6">
                                            <label>Nhóm thàn viên</label>
                                            <asp:DropDownList runat="server" ID="ddlThanhVienNhomHone" CssClass="form-control custom-select">
                                            </asp:DropDownList>
                                        </div>
                                    </div>

                                </div>
                            </div>
                        </div>

                        <%-- Tin tức --%>
                        <div class="col-md-12">
                            <div class="card card-outline card-primary">
                                <div class="card-header">
                                    <h3 class="card-title">Tin tức</h3>

                                    <div class="card-tools">
                                        <button type="button" class="btn btn-tool" data-card-widget="collapse" fdprocessedid="y41igr">
                                            <i class="fas fa-minus"></i>
                                        </button>
                                    </div>
                                </div>
                                <div class="card-body" style="display: block;">
                                    <div class="row">

                                        <div class="col-md-12">
                                            <div class="form-group">
                                                <input id="chkTinTucHienThi" runat="server" type="checkbox" checked />
                                                <label>Hiển thị  </label>

                                            </div>
                                        </div>
                                        <div class="col-md-6">
                                            <div class="form-group">
                                                <label for="txtTinTucSoLuongHome">Số lượng</label>
                                                <input type="number" value="5" runat="server" id="txtTinTucSoLuongHome" class="form-control form-control-user" placeholder="Nhập nội dung" />
                                            </div>
                                        </div>
                                        <div class="col-md-6">
                                            <label>Nhóm thàn viên</label>
                                            <asp:DropDownList runat="server" ID="ddlTinTucNhomHone" CssClass="form-control custom-select">
                                            </asp:DropDownList>
                                        </div>
                                    </div>

                                </div>
                            </div>
                        </div>
                        <%-- Đối tác --%>
                        <div class="col-md-12">
                            <div class="card card-outline card-primary">
                                <div class="card-header">
                                    <h3 class="card-title">Đối tác</h3>

                                    <div class="card-tools">
                                        <button type="button" class="btn btn-tool" data-card-widget="collapse" fdprocessedid="y41igr">
                                            <i class="fas fa-minus"></i>
                                        </button>
                                    </div>
                                </div>
                                <div class="card-body" style="display: block;">
                                    <div class="row">

                                        <div class="col-md-6">
                                            <div class="form-group">
                                                <input id="chkDoiTacHienThi" runat="server" type="checkbox" checked />
                                                <label>Hiển thị  </label>
                                            </div>
                                        </div>
                                        <div class="col-md-6">
                                            <div class="form-group">
                                                <label for="txtDoiTacSoLuongHome">Số lượng</label>
                                                <input type="number" value="5" runat="server" id="txtDoiTacTieuDeHome" class="form-control form-control-user" placeholder="Nhập nội dung" />
                                            </div>
                                        </div>

                                    </div>

                                </div>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="tab-pane fade" id="custom-tabs-four-profile" role="tabpanel" aria-labelledby="custom-tabs-four-profile-tab">
                    <div class="col-md-12">
                        <div class=" row">
                            <div class="col-md-6">
                                <div class=" row">
                                    <div class="col-md-12">
                                        <div class="form-group">
                                            <label for="txtMenuDuoiDiaChiHome">Địa chỉ</label>
                                            <input type="text" runat="server" id="txtMenuDuoiDiaChiHome" class="form-control form-control-user" placeholder="Nhập địa chỉ" />
                                        </div>
                                    </div>
                                    <div class="col-md-12">
                                        <div class="form-group">
                                            <label for="txtMenuDuoiEmailHome">Email</label>
                                            <input type="email" runat="server" id="txtMenuDuoiEmailHome" class="form-control form-control-user" placeholder="Nhập email" />
                                        </div>
                                    </div>
                                    <div class="col-md-12">
                                        <div class="form-group">
                                            <label for="txtMenuDuoiSoDienThoaiHome">Số điện thoại</label>
                                            <input type="text" runat="server" id="txtMenuDuoiSoDienThoaiHome" class="form-control form-control-user" placeholder="Nhập số điện thoại" />
                                        </div>
                                    </div>
                                    <div class="col-md-12">
                                        <div class="form-group">
                                            <label for="txtMenuDuoiSoDienThoaiHoTroHome">Số điện thoại hỗ trợ sinh viên</label>
                                            <input type="text" runat="server" id="txtMenuDuoiSoDienThoaiHoTroHome" class="form-control form-control-user" placeholder="Nhập số điện thoại hỗ trợ" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="col-md-12">
                                    <div class="form-group">
                                        <label for="txtMenuDuoiLinkFacebookHome">Link Facebook</label>
                                        <input type="text" runat="server" id="txtMenuDuoiLinkFacebookHome" class="form-control form-control-user" placeholder="Nhập link Facebook" />
                                    </div>
                                </div>
                                <div class="col-md-12">
                                    <div class="form-group">
                                        <label for="txtMenuDuoiLinkTikTokHome">Link TikTok</label>
                                        <input type="text" runat="server" id="txtMenuDuoiLinkTikTokHome" class="form-control form-control-user" placeholder="Nhập link TikTok" />
                                    </div>
                                </div>
                                <div class="col-md-12">
                                    <div class="form-group">
                                        <label for="txtMenuDuoiLinkYouTubeHome">Link YouTube</label>
                                        <input type="text" runat="server" id="txtMenuDuoiLinkYouTubeHome" class="form-control form-control-user" placeholder="Nhập link YouTube" />
                                    </div>
                                </div>
                                <div class="col-md-12">
                                    <div class="form-group">
                                        <label for="txtMenuDuoiLinkZaloHome">Link Zalo</label>
                                        <input type="text" runat="server" id="txtMenuDuoiLinkZaloHome" class="form-control form-control-user" placeholder="Nhập link Zalo" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="tab-pane fade" id="custom-tabs-four-messages" role="tabpanel" aria-labelledby="custom-tabs-four-messages-tab">
                    Morbi turpis dolor, vulputate vitae felis non, tincidunt congue mauris. Phasellus volutpat augue id mi placerat mollis. Vivamus faucibus eu massa eget condimentum. Fusce nec hendrerit sem, ac tristique nulla. Integer vestibulum orci odio. Cras nec augue ipsum. Suspendisse ut velit condimentum, mattis urna a, malesuada nunc. Curabitur eleifend facilisis velit finibus tristique. Nam vulputate, eros non luctus efficitur, ipsum odio volutpat massa, sit amet sollicitudin est libero sed ipsum. Nulla lacinia, ex vitae gravida fermentum, lectus ipsum gravida arcu, id fermentum metus arcu vel metus. Curabitur eget sem eu risus tincidunt eleifend ac ornare magna.
                </div>
                <div class="tab-pane fade " id="custom-tabs-four-settings" role="tabpanel" aria-labelledby="custom-tabs-four-settings-tab">
                    Pellentesque vestibulum commodo nibh nec blandit. Maecenas neque magna, iaculis tempus turpis ac, ornare sodales tellus. Mauris eget blandit dolor. Quisque tincidunt venenatis vulputate. Morbi euismod molestie tristique. Vestibulum consectetur dolor a vestibulum pharetra. Donec interdum placerat urna nec pharetra. Etiam eget dapibus orci, eget aliquet urna. Nunc at consequat diam. Nunc et felis ut nisl commodo dignissim. In hac habitasse platea dictumst. Praesent imperdiet accumsan ex sit amet facilisis.
                </div>
            </div>
        </div>
    </div>
</asp:Content>

<asp:Content runat="server" ID="Content2" ContentPlaceHolderID="ModalContent">
</asp:Content>


<asp:Content runat="server" ID="scriptEnd" ContentPlaceHolderID="ContentScript">

    <script src="/Administration/Style/plugins/lightbox-evolution-1.8/js/jquery.lightbox.1.8.min.js"></script>
    <script src="/Administration/Style/dist/js/jquery.imgareaselect.pack.js"></script>
    <script>
        var uploadThumbnailKey = "<%=SecurityHelper.Encrypt("/Uploads/Article/")%>";
        var uploadThumbnailKeyAlbum = "<%=SecurityHelper.Encrypt("/Uploads/Article/Picture/")%>";


        var getWindowSize = function () {
            var w = 0; var h = 0;
            //IE
            if (!window.innerWidth) {
                if (!(document.documentElement.clientWidth === 0)) {
                    //strict mode
                    w = document.documentElement.clientWidth;
                    h = document.documentElement.clientHeight;
                } else {
                    //quirks mode
                    w = document.body.clientWidth; h = document.body.clientHeight;
                }
            } else {
                //w3c
                w = window.innerWidth; h = window.innerHeight;
            }
            return {
                width: w, height: h
            };
        };
        var OpenSelectImage = function () {
            var txtid = $('[data-selector="txtImage"]').attr('id');
            var ws = getWindowSize();
            $.lightbox('/RichFilemanager/default.aspx?field_name=' + txtid
                + '&key=' + uploadThumbnailKey
                + '&selectFun=setImageUrl',
                {
                    iframe: true,
                    width: ws.width - 60,
                    height: ws.height - 40,
                });
        };
        var setImageUrl = function (txtid, url) {
            document.getElementById(txtid).value = url;
            $('[data-selector="imgThumb"]').attr('src', url);
            //call next function
        };


        var OpenHinhNenImage = function () {
            var txtid = $('[data-selector="txtHinhNen"]').attr('id');
            var ws = getWindowSize();
            $.lightbox('/RichFilemanager/default.aspx?field_name=' + txtid
                + '&key=' + uploadThumbnailKey
                + '&selectFun=setHinhNenUrl',
                {
                    iframe: true,
                    width: ws.width - 60,
                    height: ws.height - 40,
                });
        };
        var setHinhNenUrl = function (txtid, url) {
            document.getElementById(txtid).value = url;
            $('[data-selector="imgHinhNen"]').attr('src', url);
            //call next function
        };
    </script>
</asp:Content>


