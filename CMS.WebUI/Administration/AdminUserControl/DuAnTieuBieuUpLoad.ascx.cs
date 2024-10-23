using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CMS.WebUI.Administration.AdminUserControl
{
    public partial class DuAnTieuBieuUpLoad : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public string GetStringFileUrl()
        {
            try
            {
                if (fileUploadDuAnTieuBieu == null || string.IsNullOrEmpty(fileUploadDuAnTieuBieu.FileName)) return null;
                string FileName = fileUploadDuAnTieuBieu.FileName.ToString();

                string randomString = Guid.NewGuid().ToString();

                // Cộng chuỗi ngẫu nhiên vào chuỗi hiện tại
                string resultString = randomString + FileName;

                string FileSaveLocation = @"/Administration/UploadImage/" + resultString;
                FileSaveLocation = Server.MapPath(FileSaveLocation);
                fileUploadDuAnTieuBieu.PostedFile.SaveAs(FileSaveLocation);
                return resultString; // fileUpload.FileName.ToString() + "Gửi file thành công";
            }
            catch (Exception ex) { return ex.Message; }
        }


        public void SetFileImage(string fileName)
        {
            try
            {
                string fileUrl = @"/Administration/UploadImage/" + fileName;
                if (System.IO.File.Exists(Server.MapPath(fileUrl)))
                {
                    imagePreview.ImageUrl = fileUrl;
                }
                else
                    imagePreview.ImageUrl = @"/Administration/UploadImage/No_Image_Available.jpg";
            }
            catch (Exception ex) { }
        }


    }
}