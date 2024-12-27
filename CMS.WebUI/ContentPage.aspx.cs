using CMS.Core.Manager;
using CMS.WebUI.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using static CMS.Core.Manager.FriendlyUrlBLL;
using static CMS.WebUI.Common.ExtendWeb;

namespace CMS.WebUI
{
    public partial class ContentPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty(SlugUrl))
                {
                    var objFriendlyURL = FriendlyUrlBLL.GetByMa(SlugUrl);
                    if (objFriendlyURL != null)
                    {


                        if (objFriendlyURL.PostType == FriendlyURLTypeHelper.Article)
                        {
                            ltrHead.Text = string.Format(@"<link rel=""stylesheet"" type=""text/css"" href=""Assets/css/news-detail.css?v=f81a959662efae2fc3cc158351e6d90c"" />");
                            ltrBelow.Text = string.Format(@"<script type=""text/javascript"" src=""https://platform-api.sharethis.com/js/sharethis.js?v=f81a959662efae2fc3cc158351e6d90c#property=646b0f87d8c6d2001a06c301&product=inline-share-buttons&source=platform"" async=""async""></script>");

                            ctrlBaiVietPublish.Binding(SlugUrl);
                            ctrlCategory.Visible = false;
                            ctrlBaiVietGioiThieu.Visible = false;
                            ctrlDuAnTieuBieu.Visible = false;
                            ctrlDanhSachThanhVien.Visible = false;
                            ctrlBaiVietPublish.Visible = true;

                        }
                        else if (objFriendlyURL.PostType == FriendlyURLTypeHelper.Category)
                        {
                            ltrHead.Text = string.Format(@"<link rel=""stylesheet"" type=""text/css"" href=""/Assets/css/news-list.css?v=f81a959662efae2fc3cc158351e6d90c"" />");
                            ltrBelow.Text = string.Format(@"<script type=""text/javascript"" src=""https://platform-api.sharethis.com/js/sharethis.js?v=f81a959662efae2fc3cc158351e6d90c#property=646b0f87d8c6d2001a06c301&product=inline-share-buttons&source=platform"" async=""async""></script>");
                            ctrlCategory.Binding(SlugUrl);
                            ctrlBaiVietGioiThieu.Visible = false;
                            ctrlBaiVietPublish.Visible = false;
                            ctrlDanhSachThanhVien.Visible = false;
                            ctrlDuAnTieuBieu.Visible = false;
                        }
                        else if (objFriendlyURL.PostType == FriendlyURLTypeHelper.Info)
                        {
                            ltrHead.Text = string.Format(@"<link rel=""stylesheet"" type=""text/css"" href=""Assets/css/news-detail.css?v=f81a959662efae2fc3cc158351e6d90c"" />");
                            ltrBelow.Text = string.Format(@"<script type=""text/javascript"" src=""https://platform-api.sharethis.com/js/sharethis.js?v=f81a959662efae2fc3cc158351e6d90c#property=646b0f87d8c6d2001a06c301&product=inline-share-buttons&source=platform"" async=""async""></script>");

                            ctrlBaiVietGioiThieu.Binding(SlugUrl);
                            ctrlCategory.Visible = false;
                            ctrlBaiVietPublish.Visible = false;
                            ctrlDanhSachThanhVien.Visible = false;
                            ctrlDuAnTieuBieu.Visible = false;
                        }
                        else if (objFriendlyURL.PostType == FriendlyURLTypeHelper.Project)
                        {
                            ltrHead.Text = string.Format(@"<link rel=""stylesheet"" type=""text/css"" href=""/Assets/css/project-detail.css?v=f81a959662efae2fc3cc158351e6d90c"" />");
                            ltrBelow.Text = string.Format(@"<script src=""/Assets/js/project-detail.js?v=f81a959662efae2fc3cc158351e6d90c""></script>");
                            ctrlDuAnTieuBieu.Binding(SlugUrl);
                            ctrlCategory.Visible = false;
                            ctrlBaiVietPublish.Visible = false;
                            ctrlDanhSachThanhVien.Visible = false;
                            ctrlBaiVietGioiThieu.Visible = false;

                        }
                        else if (objFriendlyURL.PostType == FriendlyURLTypeHelper.FileAttactment)
                        {
                            ltrHead.Text = string.Format(@"<link rel=""stylesheet"" type=""text/css"" href=""/Assets/css/news-list.css?v=f81a959662efae2fc3cc158351e6d90c"" />     <link rel=""stylesheet"" href=""/Administration/Assets/adminTemplate/plugins/jquery-ui/jquery-ui.min.css"">
                               <link rel=""stylesheet"" href=""/Administration/Assets/adminTemplate/plugins/fontawesome-free/css/all.min.css"">
                            <link rel=""stylesheet"" href=""https://code.ionicframework.com/ionicons/2.0.1/css/ionicons.min.css"">
                            <link rel=""stylesheet"" href=""/Administration/Assets/adminTemplate/plugins/jquery-ui/jquery-ui.min.css"">
                            <link rel=""stylesheet"" href=""/Administration/Assets/adminTemplate/dist/css/adminlte.min.css?v=3.2.0"">
                            <link rel=""stylesheet"" href=""/Administration/Assets/adminTemplate/plugins/overlayScrollbars/css/OverlayScrollbars.min.css"">
                            <link rel=""stylesheet"" href=""/Administration/Assets/adminTemplate/plugins/datatables-bs4/css/dataTables.bootstrap4.min.css"">
                            <link rel=""stylesheet"" href=""/Administration/Style/plugins/validationEngine/validationEngine.jquery.css"">
                            <link rel=""stylesheet"" href=""/Administration/Assets/adminTemplate/plugins/sweetalert2-theme-bootstrap-4/bootstrap-4.min.css"">
                            <link rel=""stylesheet"" href=""/Administration/Assets/adminTemplate/plugins/toastr/toastr.min.css"">");
                            ltrBelow.Text = string.Format(@"<script src=""/Assets/js/news-list.js?v=f81a959662efae2fc3cc158351e6d90c""></script> 
                            
                            <script src=""/Administration/Assets/adminTemplate/plugins/jquery-ui/jquery-ui.min.js""></script>
                            <script src=""/Administration/Assets/adminTemplate/plugins/jquery/jquery.min.js""></script>
                            <script src=""/Administration/Assets/adminTemplate/plugins/bootstrap/js/bootstrap.bundle.min.js""></script>
                            <script src=""/Administration/Assets/adminTemplate/plugins/daterangepicker/daterangepicker.js""></script>
                            <script src=""/Administration/Assets/adminTemplate/plugins/overlayScrollbars/js/jquery.overlayScrollbars.min.js""></script>
                            <script src=""/Administration/Assets/adminTemplate/dist/js/adminlte.js?v=3.2.0""></script>
                            <script src=""/Administration/Assets/adminTemplate/plugins/sweetalert2/sweetalert2.min.js""></script>
                            <script src=""/Administration/Assets/adminTemplate/plugins/toastr/toastr.min.js""></script>
                            ");
                            ctrlDuAnTieuBieu.Visible = false;
                            ctrlCategory.Visible = false;
                            ctrlBaiVietPublish.Visible = false;
                            ctrlBaiVietGioiThieu.Visible = false;
                            ctrlFileDinhKemControl.Visible = true;
                            ctrlFileDinhKemControl.IdDanhMuc = objFriendlyURL.PostId;
                            ctrlDanhSachThanhVien.Visible = false;


                        }
                        else if (objFriendlyURL.PostType == FriendlyURLTypeHelper.NhomCoCau)
                        {
                            var objThanhVien = DanhMucBaiVietBLL.GetById(objFriendlyURL.PostId);

                            ltrHead.Text = string.Format(@"<link rel=""stylesheet"" type=""text/css"" href=""/Assets/css/team.css?v=f81a959662efae2fc3cc158351e6d90c"" />
                            <link rel=""stylesheet"" href=""https://fonts.googleapis.com/css?family=Source+Sans+Pro:300,400,400i,700&display=fallback"">                            
                            <link rel=""stylesheet"" href=""/Administration/Assets/adminTemplate/dist/css/adminlte.min.css?v=3.2.0"">
                           <link rel=""stylesheet"" href=""/Administration/Assets/adminTemplate/plugins/fontawesome-free/css/all.min.css"">                           ");
                            ltrBelow.Text = string.Format(@"<script src=""/Administration/Assets/adminTemplate/plugins/jquery/jquery.min.js""></script>
                            <script src=""/Administration/Assets/adminTemplate/plugins/bootstrap/js/bootstrap.bundle.min.js""></script>
                            <script src=""/Administration/Assets/adminTemplate/dist/js/adminlte.js?v=3.2.0""></script>
                            ");
                            ctrlDanhSachThanhVien.IdDanhMuc = objThanhVien.Id;
                            ctrlDuAnTieuBieu.Visible = false;
                            ctrlCategory.Visible = false;
                            ctrlBaiVietPublish.Visible = false;
                            ctrlBaiVietGioiThieu.Visible = false;
                            ctrlFileDinhKemControl.Visible = false;
                            ctrlFileDinhKemControl.IdDanhMuc = objFriendlyURL.PostId;
                            ctrlDanhSachThanhVien.Visible = true;
                            ctrlSlideTop.Visible = true;
                            ctrlSlideTop.ImageThumbnail = objThanhVien.ThumbnailUrl;
                            ctrlSlideTop.ShowBreadcrumb(objThanhVien.Ten);


                        }
                        else if (objFriendlyURL.PostType == FriendlyURLTypeHelper.ThanhVien)
                        {
                            var objThanhVien = BaiVietBLL.GetById(objFriendlyURL.PostId);
                            if (objThanhVien != null)
                            {
                                ltrHead.Text = string.Format(@"<link rel=""stylesheet"" type=""text/css"" href=""/Assets/css/news-list.css?v=f81a959662efae2fc3cc158351e6d90c"" />");
                                ltrBelow.Text = string.Format(@"<script src=""/Assets/js/news-list.js?v=f81a959662efae2fc3cc158351e6d90c""></script>");
                                ctrlDuAnTieuBieu.Visible = false;
                                ctrlCategory.Visible = false;
                                ctrlBaiVietPublish.Visible = false;
                                ctrlBaiVietGioiThieu.Visible = false;
                                ctrlFileDinhKemControl.Visible = false;
                                ctrlFileDinhKemControl.IdDanhMuc = objFriendlyURL.PostId;
                                ctrlDanhSachThanhVien.Visible = false;
                                ctrlSlideTop.Visible =true;
                                ctrlSlideTop.ShowBreadcrumb(objThanhVien.TieuDe);
                                ctrlChiTietThanhVien.ThanhVien = objThanhVien;
                                ctrlChiTietThanhVien.Visible = true;

                            }

                        }
                        else if (objFriendlyURL.PostType == FriendlyURLTypeHelper.ChuongTrinhDaoTao)
                        {
                            var objChuongTrinhDaoTao = DanhMucBaiVietBLL.GetById(objFriendlyURL.PostId);

                            ltrHead.Text = string.Format(@"
                            <link rel=""stylesheet"" href=""https://fonts.googleapis.com/css?family=Source+Sans+Pro:300,400,400i,700&display=fallback"">                            
                            <link rel=""stylesheet"" href=""/Administration/Assets/adminTemplate/dist/css/adminlte.min.css?v=3.2.0"">
                           <link rel=""stylesheet"" href=""/Administration/Assets/adminTemplate/plugins/fontawesome-free/css/all.min.css"">                           ");
                            ltrBelow.Text = string.Format(@"<script src=""/Administration/Assets/adminTemplate/plugins/jquery/jquery.min.js""></script>
                            <script src=""/Administration/Assets/adminTemplate/plugins/bootstrap/js/bootstrap.bundle.min.js""></script>
                            <script src=""/Administration/Assets/adminTemplate/dist/js/adminlte.js?v=3.2.0""></script>
                            ");
                            ctrlDanhSachThanhVien.IdDanhMuc = objChuongTrinhDaoTao.Id;
                            ctrlDuAnTieuBieu.Visible = false;
                            ctrlCategory.Visible = false;
                            ctrlBaiVietPublish.Visible = false;
                            ctrlBaiVietGioiThieu.Visible = false;
                            ctrlFileDinhKemControl.Visible = false;
                            ctrlFileDinhKemControl.IdDanhMuc = objFriendlyURL.PostId;
                            ctrlDanhSachThanhVien.Visible = false;
                            ctrlChuongTrinhDaoTao.IdDanhMuc = objChuongTrinhDaoTao.Id;
                            ctrlChuongTrinhDaoTao.Visible = true;
                            ctrlSlideTop.Visible = true;
                            ctrlSlideTop.ImageThumbnail = objChuongTrinhDaoTao.ThumbnailUrl;
                            ctrlSlideTop.ShowBreadcrumb(objChuongTrinhDaoTao.Ten);

                        }

                    }

                }
            }



            //CheckSlug();
        }

        private string SlugUrl
        {
            get
            {
                return Request.QueryString["slugPost"];
            }
        }
        //private void CheckSlug()
        //{
        //    string slugPost = SlugUrl;
        //    if (slugPost != null)
        //    {

        //    }
        //    else
        //    {
        //        BaiVietPublish.Visible = false;
        //        Response.Write("Không lấy được slugSlug " + slugPost);
        //    }
        //}

    }
}