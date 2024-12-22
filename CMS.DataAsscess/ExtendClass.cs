using SweetCMS.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.DataAsscess
{
    public class NguoiDungDto 
    {
        public string VaiTroChiTiet { get; set; }
        public int Id { get; set; } // Cột khóa chính, tự động tăng

        public string HoVaTen { get; set; } // Không cho phép null

        public string Email { get; set; } // Có thể null

        public string SoDienThoai { get; set; } // Có thể null

        public DateTime? NgaySinh { get; set; } // Có thể null

        public string TenTruyCap { get; set; } // Không cho phép null, phải unique

        public string MatKhau { get; set; } // Không cho phép null

        public string DiaChi { get; set; } // Có thể null

        public string AvataUrl { get; set; } // Có thể null

        public bool? TrangThai { get; set; } // Có thể null, default là 1

        public DateTime NgayTao { get; set; }

        public DateTime? ChinhSuaGanNhat { get; set; } 

    }
    public class VaiTroUserDto
    {
        public int Id { get; set; }
        public string Ten { get; set; }
        public string Ma { get; set; }
        public int CoVaiTro { get; set; }
    }

    public class DanhMucDto
    {
        public int Id { get; set; }
        public string Ten { get; set; }
        public int DanhMucChaId { get; set; }
        public string Slug { get; set; }
        public string MoTa { get; set; }
        public string TenDanhMucCha { get; set; }
    }

    public class BaiVietDto
    {
        // Các thuộc tính của bảng BaiViet
        public int Id { get; set; }
        public string TieuDe { get; set; }
        public string Slug { get; set; }
        public string MoTaNgan { get; set; }
        public string NoiDungChinh { get; set; }
        public int? TacGiaId { get; set; }
        public int? ViewCount { get; set; }
        public string ThumbnailUrl { get; set; }
        public DateTime NgayTao { get; set; }
        public DateTime? ChinhSuaGanNhat { get; set; }
        public bool TrangThai { get; set; }
        public string CreateBy { get; set; } // u.HoVaTen
        public string TenDanhMuc { get; set; } // ISNULL(STRING_AGG(c.Ten, ', '), N'Không')
        public int DanhMucId { get; set; } // ISNULL(STRING_AGG(c.Ten, ', '), N'Không')
        public int DanhMucChaId { get; set; } // ISNULL(STRING_AGG(c.Ten, ', '), N'Không')
    }
    public class MenuWebDto
    {
        // Các thuộc tính tương ứng với các cột trong bảng MenuWebTren
        public int Id { get; set; }
        public string Ten { get; set; }
        public int? MenuChaId { get; set; } // Dùng kiểu nullable int cho MenuChaId có thể có giá trị NULL
        public string Slug { get; set; }
        public string MoTa { get; set; }
        public bool HienThi { get; set; }
        public int Stt { get; set; }
        public DateTime NgayTao { get; set; }
        public string MenuChaTen { get; set; }
    }


    public class BaiVietInDanhMucDto
    {
        public int BaiVietId { get; set; }
        public int? DanhMucId { get; set; }
        public string TieuDe { get; set; }
        public int? DaChon { get; set; } // Dùng kiểu nullable int cho MenuChaId có thể có giá trị NULL
    }

    public class LoginReturnId
    {
        public int Id { get; set; }
    }


    public class ParentMenuInfo
    {
        public string ParentMenu { get; set; }
        public string ParentMa { get; set; }
        public override bool Equals(object obj)
        {
            if (obj is ParentMenuInfo other)
            {
                return ParentMenu == other.ParentMenu && ParentMa == other.ParentMa;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return (ParentMenu, ParentMa).GetHashCode();
        }

    }


    [Serializable]
    public class MenuPermisstion
    {
        public string MenuMa { get; set; }
        public string PermissionMa { get; set; }
    }


    public class MenuQuyenCheck
    {
        public int MenuId { get; set; }
        public int LoaiQuyenId { get; set; }

        public string MenuName { get; set; }
        public string LoaiQuyen { get; set; }
        public int CoQuyen { get; set; }
    }

    public class PostAndAuthorName
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; } = null;
        public string CategoryName { get; set; }
        public string Description { get; set; }
        public string Content { get; set; }
        public int ViewCount { get; set; }
        public DateTime DatetimeCreate { get; set; }
        public bool Active { get; set; }
        public int AuthorID { get; set; }
        //public int CategoryId { get; set; }
        public string AuthorFullName { get; set; }
    }

    public class DanhMucBaiVietDto
    {
        public int Id { get; set; }
        public string Ten { get; set; }
        public string Slug { get; set; }
        public int IsHaveDanhMuc { get; set; }
    }
    public class ItemFile
    {
        public int Id { get; set; }
        public string AttachmentFileIdString { get; set; }
        public string Title { get; set; }
        public string FileUrl { get; set; }

    }
    public class ItemDanhMucFileDinhKem
    {
        public int Id { get; set; }
        public string Ten { get; set; }
        public int DanhMucChaId  { get; set; }
        public int DisplayOrder { get; set; }

    }


    public class ItemFileDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string FileUrl { get; set; }
        public int CategoryId { get; set; }
    }

}
