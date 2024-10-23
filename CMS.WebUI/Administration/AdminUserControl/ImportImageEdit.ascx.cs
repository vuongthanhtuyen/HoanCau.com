using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CMS.WebUI.Administration.AdminUserControl
{
    public partial class ImportImageEdit : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //ScriptManager _script = ScriptManager.GetCurrent(this.Page);
            //_script.RegisterPostBackControl(fileUploadNameEdit);
        }
        public string GetStringFileUrl()
        {
            try
            {
                if (fileUploadNameEdit == null || string.IsNullOrEmpty(fileUploadNameEdit.FileName))
                {
                    string imageUrl = avatarImageEdit.ImageUrl;
                    string fileName = Path.GetFileName(imageUrl);

                    if (fileName == "No_Image_Available.jpg")
                    {
                        return null;
                    }
                    else
                    {
                        return fileName;
                    }
                    
                }
                else
                {
                    string fileName = fileUploadNameEdit.FileName.ToString();
                    string randomString = Guid.NewGuid().ToString();
                    // Cộng chuỗi ngẫu nhiên vào chuỗi hiện tại
                    string resultString = randomString + fileName;
                    string FileSaveLocation = @"/Administration/UploadImage/" + resultString;
                    FileSaveLocation = Server.MapPath(FileSaveLocation);
                    fileUploadNameEdit.PostedFile.SaveAs(FileSaveLocation);
                    return resultString; // fileUpload.FileName.ToString() + "Gửi file thành công";
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
                    avatarImageEdit.ImageUrl = fileUrl;
                }
                else
                    avatarImageEdit.ImageUrl = @"/Administration/UploadImage/No_Image_Available.jpg";
            }
            catch (Exception ex) { }
        }
    }
}