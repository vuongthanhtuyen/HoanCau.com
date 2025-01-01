using CMS.Core;
using CMS.Core.Manager;
using SweetCMS.Core.Helper;
using SweetCMS.Core.Manager;
using SweetCMS.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CMS.WebUI.Administration.QuanLyCauHinh
{
    public partial class CaiDatHienThi : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindCateogryData();
                BindTrangChu();
                BindLienHe();
               
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                SettingTrangChuValue();
                SettingThongTinLienHeValue();
            }
            catch (Exception ex)
            {
            }
        }
        private void BindTrangChu()
        {
            DefaultSetting setting = new DefaultSetting();

            #region  Giới thiệu
            setting = DefaultSettingBLL.GetByValue(KeyName.chkGioiThieuHienThiHome);
            if (setting != null)
            {
                chkGioiThieuHienThi.Checked = setting.Value == StatusSetting.ValueTrue ? true : false;
            }
            setting = DefaultSettingBLL.GetByValue(KeyName.txtGioiThieuTieuDeHome);
            if (setting != null)
            {
                txtGioiThieuTieuDeHome.Value = setting.Value;
            }
            setting = DefaultSettingBLL.GetByValue(KeyName.txtImageHome);
            if (setting != null)
            {
                imgThumb.Src = Helpers.GetThumbnailUrl(setting.Value);
                txtImage.Value = setting.Value;
            }
            setting = DefaultSettingBLL.GetByValue(KeyName.txtGioiThieuContentHome);
            if (setting != null)
            {
                txtGioiThieuContentHome.Text = Server.HtmlDecode(setting.Value);
            }
            setting = DefaultSettingBLL.GetByValue(KeyName.txtHinhNen);
            if (setting != null)
            {
                imgHinhNen.Src = Helpers.GetThumbnailUrl(setting.Value);
                txtHinhNen.Value = setting.Value;
            }
            #endregion

            #region  Khám phá
            setting = DefaultSettingBLL.GetByValue(KeyName.chkKhamPhaHienThiHome);
            if (setting != null)
            {
                chkKhamPhaHienThiHome.Checked = setting.Value == StatusSetting.ValueTrue ? true : false;
            }

            setting = DefaultSettingBLL.GetByValue(KeyName.txtKhamPhaTieuDeHome);
            if (setting != null)
            {
                txtKhamPhaTieuDeHome.Value = setting.Value;
            }

            setting = DefaultSettingBLL.GetByValue(KeyName.txtKhamPhaNoiDungHome);
            if (setting != null)
            {
                txtKhamPhaNoiDungHome.InnerHtml = setting.Value; // Assuming it's HTML content
            }

            setting = DefaultSettingBLL.GetByValue(KeyName.txtKhamPhaTamNhinHome);
            if (setting != null)
            {
                txtKhamPhaTamNhinHome.Value = setting.Value;
            }

            setting = DefaultSettingBLL.GetByValue(KeyName.txtKhamPhaSuMenhHome);
            if (setting != null)
            {
                txtKhamPhaSuMenhHome.Value = setting.Value;
            }

            setting = DefaultSettingBLL.GetByValue(KeyName.txtKhamPhaGiaTriCotLoiHome);
            if (setting != null)
            {
                txtKhamPhaGiaTriCotLoiHome.Value = setting.Value;
            }
            #endregion

            #region Ngành đào tạo

            setting = DefaultSettingBLL.GetByValue(KeyName.chkNganhDaoTaoHienThiHome);
            if (setting != null)
            {
                chkNganhDaoTaoHienThiHome.Checked = setting.Value == StatusSetting.ValueTrue ? true : false;
            }

            setting = DefaultSettingBLL.GetByValue(KeyName.txtNganhDaoTaoSoLuongHome);
            if (setting != null)
            {
                txtNganhDaoTaoSoLuongHome.Value = setting.Value;
            }

            setting = DefaultSettingBLL.GetByValue(KeyName.ddlNganhDaoTaoNhomHone);
            if (setting != null)
            {
                ddlNganhDaoTaoNhomHone.SelectedValue = setting.Value; // Assuming it's a dropdown
            }
            #endregion

            #region Quy mô
            setting = DefaultSettingBLL.GetByValue(KeyName.chkQuyMoHienThiHome);
            if (setting != null)
            {
                chkQuyMoHienThiHome.Checked = setting.Value == StatusSetting.ValueTrue ? true : false;
            }

            setting = DefaultSettingBLL.GetByValue(KeyName.txtSoLuongTieuDe1Home);
            if (setting != null)
            {
                txtSoLuongTieuDe1Home.Value = setting.Value;
            }

            setting = DefaultSettingBLL.GetByValue(KeyName.txtQuyMoSoLuongHome);
            if (setting != null)
            {
                txtQuyMoSoLuongHome.Value = setting.Value;
            }

            setting = DefaultSettingBLL.GetByValue(KeyName.txtQuyMoTieuDe2Home);
            if (setting != null)
            {
                txtQuyMoTieuDe2Home.Value = setting.Value;
            }

            setting = DefaultSettingBLL.GetByValue(KeyName.txtQuyMosoLuong2Home);
            if (setting != null)
            {
                txtQuyMosoLuong2Home.Value = setting.Value;
            }

            setting = DefaultSettingBLL.GetByValue(KeyName.txtQuyMoTieuDe3Home);
            if (setting != null)
            {
                txtQuyMoTieuDe3Home.Value = setting.Value;
            }

            setting = DefaultSettingBLL.GetByValue(KeyName.txtQuyMosoLuong3Home);
            if (setting != null)
            {
                txtQuyMosoLuong3Home.Value = setting.Value;
            }

            setting = DefaultSettingBLL.GetByValue(KeyName.txtQuyMoTieuDe4Home);
            if (setting != null)
            {
                txtQuyMoTieuDe4Home.Value = setting.Value;
            }

            setting = DefaultSettingBLL.GetByValue(KeyName.txtQuyMosoLuong4Home);
            if (setting != null)
            {
                txtQuyMosoLuong4Home.Value = setting.Value;
            }

            setting = DefaultSettingBLL.GetByValue(KeyName.txtQuyMoTieuDe5Home);
            if (setting != null)
            {
                txtQuyMoTieuDe5Home.Value = setting.Value;
            }

            setting = DefaultSettingBLL.GetByValue(KeyName.txtQuyMosoLuong5Home);
            if (setting != null)
            {
                txtQuyMosoLuong5Home.Value = setting.Value;
            }
            #endregion

            #region Sự kiện
            setting = DefaultSettingBLL.GetByValue(KeyName.chkSuKienHienThi);
            if (setting != null)
            {
                chkSuKienHienThi.Checked = setting.Value == StatusSetting.ValueTrue ? true : false;
            }

            setting = DefaultSettingBLL.GetByValue(KeyName.txtSuKienSoLuongHome);
            if (setting != null)
            {
                txtSuKienSoLuongHome.Value = setting.Value;
            }

            setting = DefaultSettingBLL.GetByValue(KeyName.ddlSuKienNhomHone);
            if (setting != null)
            {
                ddlSuKienNhomHone.SelectedValue = setting.Value;
            }
            #endregion

            #region Thành viên (Members)
            setting = DefaultSettingBLL.GetByValue(KeyName.chkThanhVienHienThi);
            if (setting != null)
            {
                chkThanhVienHienThi.Checked = setting.Value == StatusSetting.ValueTrue ? true : false;
            }

            setting = DefaultSettingBLL.GetByValue(KeyName.txtThanhVienSoLuongHome);
            if (setting != null)
            {
                txtThanhVienSoLuongHome.Value = setting.Value;
            }

            setting = DefaultSettingBLL.GetByValue(KeyName.ddlThanhVienNhomHone);
            if (setting != null)
            {
                ddlThanhVienNhomHone.SelectedValue = setting.Value;
            }
            #endregion

            #region Tin tức (News)
            setting = DefaultSettingBLL.GetByValue(KeyName.chkTinTucHienThi);
            if (setting != null)
            {
                chkTinTucHienThi.Checked = setting.Value == StatusSetting.ValueTrue ? true : false;
            }

            setting = DefaultSettingBLL.GetByValue(KeyName.txtTinTucSoLuongHome);
            if (setting != null)
            {
                txtTinTucSoLuongHome.Value = setting.Value;
            }

            setting = DefaultSettingBLL.GetByValue(KeyName.ddlTinTucNhomHone);
            if (setting != null)
            {
                ddlTinTucNhomHone.SelectedValue = setting.Value;
            }
            #endregion

            #region Đối tác (Partners)
            setting = DefaultSettingBLL.GetByValue(KeyName.chkDoiTacHienThi);
            if (setting != null)
            {
                chkDoiTacHienThi.Checked = setting.Value == StatusSetting.ValueTrue ? true : false;
            }

            setting = DefaultSettingBLL.GetByValue(KeyName.txtDoiTacTieuDeHome);
            if (setting != null)
            {
                txtDoiTacTieuDeHome.Value = setting.Value;
            }
            #endregion

        }
        private void BindLienHe()
        {
            DefaultSetting setting = new DefaultSetting();
            setting = DefaultSettingBLL.GetByValue(KeyName.txtMenuDuoiDiaChiHome);
            if (setting != null)
            {
                txtMenuDuoiDiaChiHome.Value = setting.Value;
            }
            setting = DefaultSettingBLL.GetByValue(KeyName.txtMenuDuoiEmailHome);
            if (setting != null)
            {
                txtMenuDuoiEmailHome.Value = setting.Value;
            }
            setting = DefaultSettingBLL.GetByValue(KeyName.txtMenuDuoiSoDienThoaiHome);
            if (setting != null)
            {
                txtMenuDuoiSoDienThoaiHome.Value = setting.Value;
            }
            setting = DefaultSettingBLL.GetByValue(KeyName.txtMenuDuoiSoDienThoaiHoTroHome);
            if (setting != null)
            {
                txtMenuDuoiSoDienThoaiHoTroHome.Value = setting.Value;
            }
            setting = DefaultSettingBLL.GetByValue(KeyName.txtMenuDuoiLinkFacebookHome);
            if (setting != null)
            {
                txtMenuDuoiLinkFacebookHome.Value = setting.Value;
            }
            setting = DefaultSettingBLL.GetByValue(KeyName.txtMenuDuoiLinkTikTokHome);
            if (setting != null)
            {
                txtMenuDuoiLinkTikTokHome.Value = setting.Value;
            }
            setting = DefaultSettingBLL.GetByValue(KeyName.txtMenuDuoiLinkYouTubeHome);
            if (setting != null)
            {
                txtMenuDuoiLinkYouTubeHome.Value = setting.Value;
            }
            setting = DefaultSettingBLL.GetByValue(KeyName.txtMenuDuoiLinkZaloHome);
            if (setting != null)
            {
                txtMenuDuoiLinkZaloHome.Value = setting.Value;
            }

        }
        private void SettingTrangChuValue()
        {
            #region Giới Thiệu
            DefaultSetting setting = DefaultSettingBLL.GetByValue(KeyName.chkGioiThieuHienThiHome);
            if (setting != null)
                DefaultSettingBLL.Update(setting.Id, KeyName.chkGioiThieuHienThiHome, chkGioiThieuHienThi.Checked.ToString());
            else
                DefaultSettingBLL.Insert(KeyName.chkGioiThieuHienThiHome, chkGioiThieuHienThi.Checked.ToString());

            setting = DefaultSettingBLL.GetByValue(KeyName.txtGioiThieuTieuDeHome);
            if (setting != null)
                DefaultSettingBLL.Update(setting.Id, KeyName.txtGioiThieuTieuDeHome, txtGioiThieuTieuDeHome.Value);
            else
                DefaultSettingBLL.Insert(KeyName.txtGioiThieuTieuDeHome, txtGioiThieuTieuDeHome.Value);

            setting = DefaultSettingBLL.GetByValue(KeyName.txtImageHome);
            if (setting != null)
                DefaultSettingBLL.Update(setting.Id, KeyName.txtImageHome, txtImage.Value);
            else
                DefaultSettingBLL.Insert(KeyName.txtImageHome, txtImage.Value);
            imgThumb.Src = Helpers.GetThumbnailUrl(setting.Value);

            setting = DefaultSettingBLL.GetByValue(KeyName.txtHinhNen);
            if (setting != null)
                DefaultSettingBLL.Update(setting.Id, KeyName.txtHinhNen, txtHinhNen.Value);
            else
                DefaultSettingBLL.Insert(KeyName.txtHinhNen, txtHinhNen.Value);
            imgHinhNen.Src = Helpers.GetThumbnailUrl(setting.Value);

            setting = DefaultSettingBLL.GetByValue(KeyName.txtGioiThieuContentHome);
            if (setting != null)
                DefaultSettingBLL.Update(setting.Id, KeyName.txtGioiThieuContentHome, Server.HtmlEncode(txtGioiThieuContentHome.Text));
            else
                DefaultSettingBLL.Insert(KeyName.txtGioiThieuContentHome, Server.HtmlEncode(txtGioiThieuContentHome.Text));
            #endregion

            #region Khám phá
            setting = DefaultSettingBLL.GetByValue(KeyName.chkKhamPhaHienThiHome);
            if (setting != null)
                DefaultSettingBLL.Update(setting.Id, KeyName.chkKhamPhaHienThiHome, chkKhamPhaHienThiHome.Checked ? StatusSetting.ValueTrue : StatusSetting.ValueFalse);
            else
                DefaultSettingBLL.Insert(KeyName.chkKhamPhaHienThiHome, chkKhamPhaHienThiHome.Checked ? StatusSetting.ValueTrue : StatusSetting.ValueFalse);

            setting = DefaultSettingBLL.GetByValue(KeyName.txtKhamPhaTieuDeHome);
            if (setting != null)
                DefaultSettingBLL.Update(setting.Id, KeyName.txtKhamPhaTieuDeHome, txtKhamPhaTieuDeHome.Value);
            else
                DefaultSettingBLL.Insert(KeyName.txtKhamPhaTieuDeHome, txtKhamPhaTieuDeHome.Value);

            setting = DefaultSettingBLL.GetByValue(KeyName.txtKhamPhaNoiDungHome);
            if (setting != null)
                DefaultSettingBLL.Update(setting.Id, KeyName.txtKhamPhaNoiDungHome, txtKhamPhaNoiDungHome.Value);
            else
                DefaultSettingBLL.Insert(KeyName.txtKhamPhaNoiDungHome, txtKhamPhaNoiDungHome.Value);

            setting = DefaultSettingBLL.GetByValue(KeyName.txtKhamPhaTamNhinHome);
            if (setting != null)
                DefaultSettingBLL.Update(setting.Id, KeyName.txtKhamPhaTamNhinHome, txtKhamPhaTamNhinHome.Value);
            else
                DefaultSettingBLL.Insert(KeyName.txtKhamPhaTamNhinHome, txtKhamPhaTamNhinHome.Value);

            setting = DefaultSettingBLL.GetByValue(KeyName.txtKhamPhaSuMenhHome);
            if (setting != null)
                DefaultSettingBLL.Update(setting.Id, KeyName.txtKhamPhaSuMenhHome, txtKhamPhaSuMenhHome.Value);
            else
                DefaultSettingBLL.Insert(KeyName.txtKhamPhaSuMenhHome, txtKhamPhaSuMenhHome.Value);

            setting = DefaultSettingBLL.GetByValue(KeyName.txtKhamPhaGiaTriCotLoiHome);
            if (setting != null)
                DefaultSettingBLL.Update(setting.Id, KeyName.txtKhamPhaGiaTriCotLoiHome, txtKhamPhaGiaTriCotLoiHome.Value);
            else
                DefaultSettingBLL.Insert(KeyName.txtKhamPhaGiaTriCotLoiHome, txtKhamPhaGiaTriCotLoiHome.Value);
            #endregion

            #region Ngành đào tạo
            setting = DefaultSettingBLL.GetByValue(KeyName.chkNganhDaoTaoHienThiHome);
            if (setting != null)
                DefaultSettingBLL.Update(setting.Id, KeyName.chkNganhDaoTaoHienThiHome, chkNganhDaoTaoHienThiHome.Checked ? StatusSetting.ValueTrue : StatusSetting.ValueFalse);
            else
                DefaultSettingBLL.Insert(KeyName.chkNganhDaoTaoHienThiHome, chkNganhDaoTaoHienThiHome.Checked ? StatusSetting.ValueTrue : StatusSetting.ValueFalse);

            setting = DefaultSettingBLL.GetByValue(KeyName.txtNganhDaoTaoSoLuongHome);
            if (setting != null)
                DefaultSettingBLL.Update(setting.Id, KeyName.txtNganhDaoTaoSoLuongHome, txtNganhDaoTaoSoLuongHome.Value);
            else
                DefaultSettingBLL.Insert(KeyName.txtNganhDaoTaoSoLuongHome, txtNganhDaoTaoSoLuongHome.Value);

            setting = DefaultSettingBLL.GetByValue(KeyName.ddlNganhDaoTaoNhomHone);
            if (setting != null)
                DefaultSettingBLL.Update(setting.Id, KeyName.ddlNganhDaoTaoNhomHone, ddlNganhDaoTaoNhomHone.SelectedValue);
            else
                DefaultSettingBLL.Insert(KeyName.ddlNganhDaoTaoNhomHone, ddlNganhDaoTaoNhomHone.SelectedValue);
            #endregion

            #region Quy mô
            setting = DefaultSettingBLL.GetByValue(KeyName.chkQuyMoHienThiHome);
            if (setting != null)
                DefaultSettingBLL.Update(setting.Id, KeyName.chkQuyMoHienThiHome, chkQuyMoHienThiHome.Checked ? StatusSetting.ValueTrue : StatusSetting.ValueFalse);
            else
                DefaultSettingBLL.Insert(KeyName.chkQuyMoHienThiHome, chkQuyMoHienThiHome.Checked ? StatusSetting.ValueTrue : StatusSetting.ValueFalse);

            setting = DefaultSettingBLL.GetByValue(KeyName.txtSoLuongTieuDe1Home);
            if (setting != null)
                DefaultSettingBLL.Update(setting.Id, KeyName.txtSoLuongTieuDe1Home, txtSoLuongTieuDe1Home.Value);
            else
                DefaultSettingBLL.Insert(KeyName.txtSoLuongTieuDe1Home, txtSoLuongTieuDe1Home.Value);

            setting = DefaultSettingBLL.GetByValue(KeyName.txtQuyMoSoLuongHome);
            if (setting != null)
                DefaultSettingBLL.Update(setting.Id, KeyName.txtQuyMoSoLuongHome, txtQuyMoSoLuongHome.Value);
            else
                DefaultSettingBLL.Insert(KeyName.txtQuyMoSoLuongHome, txtQuyMoSoLuongHome.Value);

            setting = DefaultSettingBLL.GetByValue(KeyName.txtQuyMoTieuDe2Home);
            if (setting != null)
                DefaultSettingBLL.Update(setting.Id, KeyName.txtQuyMoTieuDe2Home, txtQuyMoTieuDe2Home.Value);
            else
                DefaultSettingBLL.Insert(KeyName.txtQuyMoTieuDe2Home, txtQuyMoTieuDe2Home.Value);

            setting = DefaultSettingBLL.GetByValue(KeyName.txtQuyMosoLuong2Home);
            if (setting != null)
                DefaultSettingBLL.Update(setting.Id, KeyName.txtQuyMosoLuong2Home, txtQuyMosoLuong2Home.Value);
            else
                DefaultSettingBLL.Insert(KeyName.txtQuyMosoLuong2Home, txtQuyMosoLuong2Home.Value);

            setting = DefaultSettingBLL.GetByValue(KeyName.txtQuyMoTieuDe3Home);
            if (setting != null)
                DefaultSettingBLL.Update(setting.Id, KeyName.txtQuyMoTieuDe3Home, txtQuyMoTieuDe3Home.Value);
            else
                DefaultSettingBLL.Insert(KeyName.txtQuyMoTieuDe3Home, txtQuyMoTieuDe3Home.Value);

            setting = DefaultSettingBLL.GetByValue(KeyName.txtQuyMosoLuong3Home);
            if (setting != null)
                DefaultSettingBLL.Update(setting.Id, KeyName.txtQuyMosoLuong3Home, txtQuyMosoLuong3Home.Value);
            else
                DefaultSettingBLL.Insert(KeyName.txtQuyMosoLuong3Home, txtQuyMosoLuong3Home.Value);

            setting = DefaultSettingBLL.GetByValue(KeyName.txtQuyMoTieuDe4Home);
            if (setting != null)
                DefaultSettingBLL.Update(setting.Id, KeyName.txtQuyMoTieuDe4Home, txtQuyMoTieuDe4Home.Value);
            else
                DefaultSettingBLL.Insert(KeyName.txtQuyMoTieuDe4Home, txtQuyMoTieuDe4Home.Value);

            setting = DefaultSettingBLL.GetByValue(KeyName.txtQuyMosoLuong4Home);
            if (setting != null)
                DefaultSettingBLL.Update(setting.Id, KeyName.txtQuyMosoLuong4Home, txtQuyMosoLuong4Home.Value);
            else
                DefaultSettingBLL.Insert(KeyName.txtQuyMosoLuong4Home, txtQuyMosoLuong4Home.Value);

            setting = DefaultSettingBLL.GetByValue(KeyName.txtQuyMoTieuDe5Home);
            if (setting != null)
                DefaultSettingBLL.Update(setting.Id, KeyName.txtQuyMoTieuDe5Home, txtQuyMoTieuDe5Home.Value);
            else
                DefaultSettingBLL.Insert(KeyName.txtQuyMoTieuDe5Home, txtQuyMoTieuDe5Home.Value);

            setting = DefaultSettingBLL.GetByValue(KeyName.txtQuyMosoLuong5Home);
            if (setting != null)
                DefaultSettingBLL.Update(setting.Id, KeyName.txtQuyMosoLuong5Home, txtQuyMosoLuong5Home.Value);
            else
                DefaultSettingBLL.Insert(KeyName.txtQuyMosoLuong5Home, txtQuyMosoLuong5Home.Value);
            #endregion

            #region Sự kiện
            setting = DefaultSettingBLL.GetByValue(KeyName.chkSuKienHienThi);
            if (setting != null)
                DefaultSettingBLL.Update(setting.Id, KeyName.chkSuKienHienThi, chkSuKienHienThi.Checked ? StatusSetting.ValueTrue : StatusSetting.ValueFalse);
            else
                DefaultSettingBLL.Insert(KeyName.chkSuKienHienThi, chkSuKienHienThi.Checked ? StatusSetting.ValueTrue : StatusSetting.ValueFalse);

            setting = DefaultSettingBLL.GetByValue(KeyName.txtSuKienSoLuongHome);
            if (setting != null)
                DefaultSettingBLL.Update(setting.Id, KeyName.txtSuKienSoLuongHome, txtSuKienSoLuongHome.Value);
            else
                DefaultSettingBLL.Insert(KeyName.txtSuKienSoLuongHome, txtSuKienSoLuongHome.Value);

            setting = DefaultSettingBLL.GetByValue(KeyName.ddlSuKienNhomHone);
            if (setting != null)
                DefaultSettingBLL.Update(setting.Id, KeyName.ddlSuKienNhomHone, ddlSuKienNhomHone.SelectedValue);
            else
                DefaultSettingBLL.Insert(KeyName.ddlSuKienNhomHone, ddlSuKienNhomHone.SelectedValue);
            #endregion

            #region Thành viên
            setting = DefaultSettingBLL.GetByValue(KeyName.chkThanhVienHienThi);
            if (setting != null)
                DefaultSettingBLL.Update(setting.Id, KeyName.chkThanhVienHienThi, chkThanhVienHienThi.Checked ? StatusSetting.ValueTrue : StatusSetting.ValueFalse);
            else
                DefaultSettingBLL.Insert(KeyName.chkThanhVienHienThi, chkThanhVienHienThi.Checked ? StatusSetting.ValueTrue : StatusSetting.ValueFalse);

            setting = DefaultSettingBLL.GetByValue(KeyName.txtThanhVienSoLuongHome);
            if (setting != null)
                DefaultSettingBLL.Update(setting.Id, KeyName.txtThanhVienSoLuongHome, txtThanhVienSoLuongHome.Value);
            else
                DefaultSettingBLL.Insert(KeyName.txtThanhVienSoLuongHome, txtThanhVienSoLuongHome.Value);

            setting = DefaultSettingBLL.GetByValue(KeyName.ddlThanhVienNhomHone);
            if (setting != null)
                DefaultSettingBLL.Update(setting.Id, KeyName.ddlThanhVienNhomHone, ddlThanhVienNhomHone.SelectedValue);
            else
                DefaultSettingBLL.Insert(KeyName.ddlThanhVienNhomHone, ddlThanhVienNhomHone.SelectedValue);
            #endregion

            #region Tin tức
            setting = DefaultSettingBLL.GetByValue(KeyName.chkTinTucHienThi);
            if (setting != null)
                DefaultSettingBLL.Update(setting.Id, KeyName.chkTinTucHienThi, chkTinTucHienThi.Checked ? StatusSetting.ValueTrue : StatusSetting.ValueFalse);
            else
                DefaultSettingBLL.Insert(KeyName.chkTinTucHienThi, chkTinTucHienThi.Checked ? StatusSetting.ValueTrue : StatusSetting.ValueFalse);

            setting = DefaultSettingBLL.GetByValue(KeyName.txtTinTucSoLuongHome);
            if (setting != null)
                DefaultSettingBLL.Update(setting.Id, KeyName.txtTinTucSoLuongHome, txtTinTucSoLuongHome.Value);
            else
                DefaultSettingBLL.Insert(KeyName.txtTinTucSoLuongHome, txtTinTucSoLuongHome.Value);

            setting = DefaultSettingBLL.GetByValue(KeyName.ddlTinTucNhomHone);
            if (setting != null)
                DefaultSettingBLL.Update(setting.Id, KeyName.ddlTinTucNhomHone, ddlTinTucNhomHone.SelectedValue);
            else
                DefaultSettingBLL.Insert(KeyName.ddlTinTucNhomHone, ddlTinTucNhomHone.SelectedValue);
            #endregion

            #region Đối tác
            setting = DefaultSettingBLL.GetByValue(KeyName.chkDoiTacHienThi);
            if (setting != null)
                DefaultSettingBLL.Update(setting.Id, KeyName.chkDoiTacHienThi, chkDoiTacHienThi.Checked ? StatusSetting.ValueTrue : StatusSetting.ValueFalse);
            else
                DefaultSettingBLL.Insert(KeyName.chkDoiTacHienThi, chkDoiTacHienThi.Checked ? StatusSetting.ValueTrue : StatusSetting.ValueFalse);

            setting = DefaultSettingBLL.GetByValue(KeyName.txtDoiTacTieuDeHome);
            if (setting != null)
                DefaultSettingBLL.Update(setting.Id, KeyName.txtDoiTacTieuDeHome, txtDoiTacTieuDeHome.Value);
            else
                DefaultSettingBLL.Insert(KeyName.txtDoiTacTieuDeHome, txtDoiTacTieuDeHome.Value);
            #endregion

        }
        private void SettingThongTinLienHeValue()
        {
            #region Giới Thiệu
            DefaultSetting setting = DefaultSettingBLL.GetByValue(KeyName.txtMenuDuoiDiaChiHome);

            if (setting != null)
                DefaultSettingBLL.Update(setting.Id, KeyName.txtMenuDuoiDiaChiHome, txtMenuDuoiDiaChiHome.Value);
            else
                DefaultSettingBLL.Insert(KeyName.txtMenuDuoiDiaChiHome, txtMenuDuoiDiaChiHome.Value);

            setting = DefaultSettingBLL.GetByValue(KeyName.txtMenuDuoiEmailHome);
            if (setting != null)
                DefaultSettingBLL.Update(setting.Id, KeyName.txtMenuDuoiEmailHome, txtMenuDuoiEmailHome.Value);
            else
                DefaultSettingBLL.Insert(KeyName.txtMenuDuoiEmailHome, txtMenuDuoiEmailHome.Value);

            setting = DefaultSettingBLL.GetByValue(KeyName.txtMenuDuoiSoDienThoaiHome);
            if (setting != null)
                DefaultSettingBLL.Update(setting.Id, KeyName.txtMenuDuoiSoDienThoaiHome, txtMenuDuoiSoDienThoaiHome.Value);
            else
                DefaultSettingBLL.Insert(KeyName.txtMenuDuoiSoDienThoaiHome, txtMenuDuoiSoDienThoaiHome.Value);

            setting = DefaultSettingBLL.GetByValue(KeyName.txtMenuDuoiSoDienThoaiHoTroHome);
            if (setting != null)
                DefaultSettingBLL.Update(setting.Id, KeyName.txtMenuDuoiSoDienThoaiHoTroHome, txtMenuDuoiSoDienThoaiHoTroHome.Value);
            else
                DefaultSettingBLL.Insert(KeyName.txtMenuDuoiSoDienThoaiHoTroHome, txtMenuDuoiSoDienThoaiHoTroHome.Value);

            setting = DefaultSettingBLL.GetByValue(KeyName.txtMenuDuoiLinkFacebookHome);
            if (setting != null)
                DefaultSettingBLL.Update(setting.Id, KeyName.txtMenuDuoiLinkFacebookHome, txtMenuDuoiLinkFacebookHome.Value);
            else
                DefaultSettingBLL.Insert(KeyName.txtMenuDuoiLinkFacebookHome, txtMenuDuoiLinkFacebookHome.Value);

            setting = DefaultSettingBLL.GetByValue(KeyName.txtMenuDuoiLinkTikTokHome);
            if (setting != null)
                DefaultSettingBLL.Update(setting.Id, KeyName.txtMenuDuoiLinkTikTokHome, txtMenuDuoiLinkTikTokHome.Value);
            else
                DefaultSettingBLL.Insert(KeyName.txtMenuDuoiLinkTikTokHome, txtMenuDuoiLinkTikTokHome.Value);

            setting = DefaultSettingBLL.GetByValue(KeyName.txtMenuDuoiLinkYouTubeHome);
            if (setting != null)
                DefaultSettingBLL.Update(setting.Id, KeyName.txtMenuDuoiLinkYouTubeHome, txtMenuDuoiLinkYouTubeHome.Value);
            else
                DefaultSettingBLL.Insert(KeyName.txtMenuDuoiLinkYouTubeHome, txtMenuDuoiLinkYouTubeHome.Value);

            setting = DefaultSettingBLL.GetByValue(KeyName.txtMenuDuoiLinkZaloHome);
            if (setting != null)
                DefaultSettingBLL.Update(setting.Id, KeyName.txtMenuDuoiLinkZaloHome, txtMenuDuoiLinkZaloHome.Value);
            else
                DefaultSettingBLL.Insert(KeyName.txtMenuDuoiLinkZaloHome, txtMenuDuoiLinkZaloHome.Value);
            #endregion
        }

        private void BindCateogryData()
        {

            List<DanhMuc> lstCats = DanhMucBaiVietBLL.GetAllDanhMuc();
            foreach (DanhMuc itemCat in lstCats.Where(x=> x.Type == CategoryType.ChuongTrinhDaoTao))
            {
                ddlNganhDaoTaoNhomHone.Items.Add(new ListItem(itemCat.Ten, itemCat.Id.ToString()));
            }
            
            foreach (DanhMuc itemCat in lstCats.Where(x => x.Type == CategoryType.NhomSuKien))
            {
                ddlSuKienNhomHone.Items.Add(new ListItem(itemCat.Ten, itemCat.Id.ToString()));
            }
            
            foreach (DanhMuc itemCat in lstCats.Where(x => x.Type == CategoryType.NhomCoCau))
            {
                ddlThanhVienNhomHone.Items.Add(new ListItem(itemCat.Ten, itemCat.Id.ToString()));
            }
           
            foreach (DanhMuc itemCat in lstCats.Where(x => x.Type == CategoryType.Article))
            {
                ddlTinTucNhomHone.Items.Add(new ListItem(itemCat.Ten, itemCat.Id.ToString()));
            }
        }

    }
}