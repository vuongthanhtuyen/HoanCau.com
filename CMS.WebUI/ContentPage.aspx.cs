using CMS.Core.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using static CMS.Core.Manager.FriendlyUrlBLL;

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

                        }
                        if (objFriendlyURL.PostType == FriendlyURLTypeHelper.Category)
                        {
                            ltrHead.Text = string.Format(@"<link rel=""stylesheet"" type=""text/css"" href=""/Assets/css/news-list.css?v=f81a959662efae2fc3cc158351e6d90c"" />");
                            ltrBelow.Text = string.Format(@"<script type=""text/javascript"" src=""https://platform-api.sharethis.com/js/sharethis.js?v=f81a959662efae2fc3cc158351e6d90c#property=646b0f87d8c6d2001a06c301&product=inline-share-buttons&source=platform"" async=""async""></script>");
                            ctrlCategory.Binding(SlugUrl);
                            ctrlBaiVietGioiThieu.Visible = false;
                            ctrlBaiVietPublish.Visible = false;
                            ctrlDuAnTieuBieu.Visible = false;
                        }
                        if (objFriendlyURL.PostType == FriendlyURLTypeHelper.Info)
                        {
                            ltrHead.Text = string.Format(@"<link rel=""stylesheet"" type=""text/css"" href=""Assets/css/news-detail.css?v=f81a959662efae2fc3cc158351e6d90c"" />");
                            ltrBelow.Text = string.Format(@"<script type=""text/javascript"" src=""https://platform-api.sharethis.com/js/sharethis.js?v=f81a959662efae2fc3cc158351e6d90c#property=646b0f87d8c6d2001a06c301&product=inline-share-buttons&source=platform"" async=""async""></script>");

                            ctrlBaiVietGioiThieu.Binding(SlugUrl);
                            ctrlCategory.Visible = false;
                            ctrlBaiVietPublish.Visible = false;
                            ctrlDuAnTieuBieu.Visible = false;
                        }
                        if (objFriendlyURL.PostType == FriendlyURLTypeHelper.Project)
                        {
                            ltrHead.Text = string.Format(@"<link rel=""stylesheet"" type=""text/css"" href=""/Assets/css/project-detail.css?v=f81a959662efae2fc3cc158351e6d90c"" />");
                            ltrBelow.Text = string.Format(@"<script src=""/Assets/js/project-detail.js?v=f81a959662efae2fc3cc158351e6d90c""></script>");
                            ctrlDuAnTieuBieu.Binding(SlugUrl);
                            ctrlCategory.Visible = false;
                            ctrlBaiVietPublish.Visible = false;
                            ctrlBaiVietGioiThieu.Visible = false;

                        }
                        if (objFriendlyURL.PostType == FriendlyURLTypeHelper.FileAttactment)
                        {
                            ltrHead.Text = string.Format(@"<link rel=""stylesheet"" type=""text/css"" href=""/Assets/css/news-list.css?v=f81a959662efae2fc3cc158351e6d90c"" />");
                            ltrBelow.Text = string.Format(@"<script src=""/Assets/js/news-list.js?v=f81a959662efae2fc3cc158351e6d90c""></script>");
                            ctrlDuAnTieuBieu.Visible = false;
                            ctrlCategory.Visible = false;
                            ctrlBaiVietPublish.Visible = false;
                            ctrlBaiVietGioiThieu.Visible = false;
                            ctrlFileDinhKemControl.Visible = true;
                            ctrlFileDinhKemControl.IdDanhMuc = objFriendlyURL.PostId;

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