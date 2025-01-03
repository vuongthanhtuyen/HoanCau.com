using CMS.Core.ApplicationCache;
using SubSonic;
using TBDCMS.Core.ApplicationCache;
using TBDCMS.Core.Helper;
using TBDCMS.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace TBDCMS.Core.Manager
{
    public class KeyName
    {
        #region Trang Chu
        // Giới thiệu
        public static string chkGioiThieuHienThiHome = "chkGioiThieuHienThiHome";
        public static string txtGioiThieuTieuDeHome = "txtGioiThieuTieuDeHome";
        public static string txtImageHome = "txtImageHome";
        public static string txtGioiThieuContentHome = "txtGioiThieuContentHome";
        public static string txtHinhNen = "txtHinhNen";

        // Khám phá
        public static string chkKhamPhaHienThiHome = "chkKhamPhaHienThiHome";
        public static string txtKhamPhaTieuDeHome = "txtKhamPhaTieuDeHome";
        public static string txtKhamPhaNoiDungHome = "txtKhamPhaNoiDungHome";
        public static string txtKhamPhaTamNhinHome = "txtKhamPhaTamNhinHome";
        public static string txtKhamPhaSuMenhHome = "txtKhamPhaSuMenhHome";
        public static string txtKhamPhaGiaTriCotLoiHome = "txtKhamPhaGiaTriCotLoiHome";

        // Ngành đào tạo
        public static string chkNganhDaoTaoHienThiHome = "chkNganhDaoTaoHienThiHome";
        public static string txtNganhDaoTaoSoLuongHome = "txtNganhDaoTaoSoLuongHome";
        public static string ddlNganhDaoTaoNhomHone = "ddlNganhDaoTaoNhomHone";

        // Quy mô 
        public static string chkQuyMoHienThiHome = "chkQuyMoHienThiHome";
        public static string txtSoLuongTieuDe1Home = "txtSoLuongTieuDe1Home";
        public static string txtQuyMoSoLuongHome = "txtQuyMoSoLuongHome";
        public static string txtQuyMoTieuDe2Home = "txtQuyMoTieuDe2Home";
        public static string txtQuyMosoLuong2Home = "txtQuyMosoLuong2Home";
        public static string txtQuyMoTieuDe3Home = "txtQuyMoTieuDe3Home";
        public static string txtQuyMosoLuong3Home = "txtQuyMosoLuong3Home";
        public static string txtQuyMoTieuDe4Home = "txtQuyMoTieuDe4Home";
        public static string txtQuyMosoLuong4Home = "txtQuyMosoLuong4Home";
        public static string txtQuyMoTieuDe5Home = "txtQuyMoTieuDe5Home";
        public static string txtQuyMosoLuong5Home = "txtQuyMosoLuong5Home";

        // Sự kiện 
        public static string chkSuKienHienThi = "chkSuKienHienThi";
        public static string txtSuKienSoLuongHome = "txtSuKienSoLuongHome";
        public static string ddlSuKienNhomHone = "ddlSuKienNhomHone";

        // Thành viên (Members)
        public static string chkThanhVienHienThi = "chkThanhVienHienThi";
        public static string txtThanhVienSoLuongHome = "txtThanhVienSoLuongHome";
        public static string ddlThanhVienNhomHone = "ddlThanhVienNhomHone";

        // Tin tức (News)
        public static string chkTinTucHienThi = "chkTinTucHienThi";
        public static string txtTinTucSoLuongHome = "txtTinTucSoLuongHome";
        public static string ddlTinTucNhomHone = "ddlTinTucNhomHone";

        // Đối tác (Partners)
        public static string chkDoiTacHienThi = "chkDoiTacHienThi";
        public static string txtDoiTacTieuDeHome = "txtDoiTacTieuDeHome";

        #endregion

        #region Thông Tin liên hệ
        public static string txtMenuDuoiDiaChiHome = "txtMenuDuoiDiaChiHome";
        public static string txtMenuDuoiEmailHome = "txtMenuDuoiEmailHome";
        public static string txtMenuDuoiSoDienThoaiHome = "txtMenuDuoiSoDienThoaiHome";
        public static string txtMenuDuoiSoDienThoaiHoTroHome = "txtMenuDuoiSoDienThoaiHoTroHome";
        public static string txtMenuDuoiLinkFacebookHome = "txtMenuDuoiLinkFacebookHome";
        public static string txtMenuDuoiLinkTikTokHome = "txtMenuDuoiLinkTikTokHome";
        public static string txtMenuDuoiLinkYouTubeHome = "txtMenuDuoiLinkYouTubeHome";
        public static string txtMenuDuoiLinkZaloHome = "txtMenuDuoiLinkZaloHome";

        #endregion

    }
    public class DefaultSettingBLL
    {
        public static DefaultSetting Insert(string keyName, string ValueName)
        {

            DefaultSetting defaultSetting = new DefaultSetting();
            defaultSetting.Value = ValueName;
            defaultSetting.KeyString = keyName;
            return new DefaultSettingController().Insert(defaultSetting);
        }
        public static DefaultSetting Update(int id, string keyName, string ValueName)
        {
            return new DefaultSettingController().Update(id, keyName, ValueName);
        }
        public static DefaultSetting GetByValue(string key)
        {
            return new Select().From(DefaultSetting.Schema)
                .Where(DefaultSetting.KeyStringColumn).IsEqualTo(key)
                .ExecuteSingle<DefaultSetting>();
        }
        public static string GetByValueString(string key)
        {
            try
            {
                var setting = new Select().From(DefaultSetting.Schema)
                .Where(DefaultSetting.KeyStringColumn).IsEqualTo(key)
                .ExecuteSingle<DefaultSetting>();

                if(setting == null)
                {
                    return string.Empty;
                }
                return setting.Value;
            }
            catch (Exception ex)
            {
                return string.Empty;
            }
        }

    }
}
