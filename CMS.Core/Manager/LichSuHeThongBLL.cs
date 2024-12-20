using SweetCMS.Core.Helper;
using SweetCMS.Core.Manager;
using SweetCMS.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Core.Manager
{

    public class LichSuHeThongType
    {
        public const string LOGIN = "Login";
        public const string LOGOUT = "Log out";
        public const string DELETE = "Deleted";
        public const string UPDATE = "Updated";
        public const string INSERT = "Created";
        public const string ACTIVE = "Active";
        public const string INACTIVE = "InActive";
        public const string PUBLISH_ARTICLE = "Published article";
        public const string PUBLISH_DRAFT = "Published draft";
        public const string UNPUBLISH_ARTICLE = "Unpublished article";
    }

    public class LichSuHeThongGroup
    {
        public const string UserAuthentication = "Phân quyền người dùng";
        public const string QuanLyVaiTro = "QuanLyVaiTro";
        public const string QuanlyNguoiDung = "Quản lý người dùng";
        public const string CompanyManagement = "Quản lý công ty";
        public const string QuanLyBaiViet = "Quản lý bài viết";
        public const string ImageManagement = "Quản lý thư viện hình ảnh";
        public const string VideoManagement = "Quản lý thư viện video";
        public const string QuanLyLienHe = "Quản lý liên hệ";
        public const string TopMenuManagement = "Quản lý menu trên";
        public const string BottomMenuManagement = "Quản lý menu dưới";
        public const string QuanLySlide = "Quản lý Slide";
        
    }

    public class LichSuHeThongBLL
    {
        public static void LogAction(string activity, string objGroup, string objName)
        {
            //if (ApplicationContext.Current.User != null && SettingManager.GetSettingValueBoolean(SettingNames.SaveLog, true))
                LogAction("Admin", activity, objGroup, objName);
        }

        public static void LogAction(string currentUserName, string activity,
            string objGroup, string objName)
        {

            LichSuHeThong logging = new LichSuHeThong();
            logging.NguoiTao = currentUserName;
            logging.HanhDong = activity;
            logging.CreateDate = DateTime.Now;
            logging.TenNhom = objGroup;
            logging.TenDoiTuong = objName;

            AddNewLog(logging);

        }

        public static LichSuHeThong AddNewLog(LichSuHeThong log)
        {
            return new LichSuHeThongController().Insert(log);
        }

    }
}
