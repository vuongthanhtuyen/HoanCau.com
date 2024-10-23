using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CMS.WebUI.Administration.AdminUserControl
{
    public partial class ImportImage : System.Web.UI.UserControl
    {
       
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        public string GetStringFileUrl()
        {
            try
            {
                //UpdatePanel1.Update();
                if (fileUploadName == null || string.IsNullOrEmpty(fileUploadName.FileName)) return null;

                string fileName = fileUploadName.FileName.ToString();
                if (!System.IO.File.Exists(Server.MapPath(fileName)))
                {

                    string randomString = Guid.NewGuid().ToString();
                    // Cộng chuỗi ngẫu nhiên vào chuỗi hiện tại
                    string resultString = randomString + fileName;
                    string FileSaveLocation = @"/Administration/UploadImage/" + resultString;
                    FileSaveLocation = Server.MapPath(FileSaveLocation);
                    fileUploadName.PostedFile.SaveAs(FileSaveLocation);
                    
                    return resultString; // fileUpload.FileName.ToString() + "Gửi file thành công";
                    
                }
                else {
                    //UpdatePanel1.Update();
                    return fileName;
                }
                
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
                    avatarImage.ImageUrl = fileUrl;
                }
                else
                    avatarImage.ImageUrl = @"/Administration/UploadImage/No_Image_Available.jpg";
                //UpdatePanel1.Update();
            }
            catch (Exception ex) { }
        }
    }
}