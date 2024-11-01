using CMS.Core.Manager;
using CMS.Core.Publich;
using CMS.DataAsscess;
using CMS.WebUI.Administration.AdminUserControl;
using SubSonic.Sugar;
using SweetCMS.Core.Helper;
using SweetCMS.DataAccess;
using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CMS.WebUI
{
    public partial class test : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //string firstname = Request.QueryString["firstname"];
            //string lastname = Request.QueryString["lastname"];
            //Response.Write();
            //BindGrid();
           

        }


        private void BindGrid(int pageIndex = 1, int pageSize = 10)
        {
            //pageIndex = PagingAdminWeb.GetPageIndex();
            //List<BaiVietDto> postList = new List<BaiVietDto>();
            //int totalRow = 0;
            //postList = BaiVietBLL.GetPaging(pageSize, pageIndex, Request.QueryString["search"], null, null, out totalRow);

            //ViewState["LastIndex"] = (pageIndex - 1) * pageSize;
            //PagingAdminWeb.GetPaging(totalRow, pageIndex);
            //GridViewTable.DataSource = postList;
            //GridViewTable.DataBind();
        }
        protected void GridViewTable_RowCommand(object sender, GridViewCommandEventArgs e)
        {
          

        }
        protected void btnSend_Click(object sender, EventArgs e)
        {
            // xóa ảnh trong dường dẫn nếu không được sử dụng

                string folderPath = @"/Administration/UploadImage/"; // Replace with your folder path
            folderPath = Server.MapPath(folderPath);
                // Check if folder exists
                if (Directory.Exists(folderPath))
                {
                    // Get all files in the folder
                    string[] files = Directory.GetFiles(folderPath);

                    lblmessage.Text = files.Count().ToString();
                    //foreach (string filePath in files)
                    //{
                    //    // Get just the file name
                    //    string fileName = Path.GetFileName(filePath);

                    //    // Perform a check or action based on the file name
                    //    if (fileName.StartsWith("example")) // Example condition
                    //    {
                    //        Console.WriteLine($"Processing {fileName}...");
                    //        // Add your processing code here
                    //    }
                    //    else
                    //    {
                    //        Console.WriteLine($"{fileName} does not match the criteria.");
                    //    }
                    //}
                }
                else
                {
                lblmessage.Text = "Folder does not exist.";
                }
            


            //lblmessage.Text = "Đường dẫn ảnh: " + Helpers.ConvertToSavePath(txtImage.Value.Trim(), true);

        }


        [WebMethod]
        public static string GetGreeting(string firstname, string lastname)
        {
            return $"Xin chào {firstname} - {lastname}";
        }
        [WebMethod]
        public static string BindInDecription(string txtDecription)
        {
            
            return "Đã lưu vào cơ sở dữ liệu" +  txtDecription ;
        }

        [WebMethod]
        public static string GetPost(string id)
        {
            var post = BaiVietPublishBLL.GetById(int.Parse(id)) ;
            JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer() ;
            string jsonResult = javaScriptSerializer.Serialize(post) ;
            return jsonResult;

        }
        [WebMethod]
        public static string GetLangID(string langId)
        {
            ApplicationContext.Current.CurrentLanguageId = int.Parse(langId);
            string content = string.Empty;
            if (langId == "1")
            {
                content = "Xin chào, thế giới!"; // Nội dung tiếng Việt
            }
            else if (langId == "2")
            {
                content = "Hello, World!"; // Nội dung tiếng Anh
            }
            JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer() ;
            string jsonResult = javaScriptSerializer.Serialize(content) ;
            return jsonResult;

        }

        //[WebMethod]
        //public static string GetPosts(string pageIndex)
        //{
        //    //List<BaiVietDto> postList = new List<BaiVietDto>();
        //    //int totalRow = 0;
        //    //postList = BaiVietBLL.GetPaging(null, int.Parse(pageIndex), null, null, null, out totalRow);
        //    //JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer() ;
        //    //string jsonRessult = javaScriptSerializer.Serialize( postList);
        //    //return jsonRessult;
        //}

    }
}