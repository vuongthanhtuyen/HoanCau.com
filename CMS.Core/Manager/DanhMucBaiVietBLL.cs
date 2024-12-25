using CMS.DataAsscess;
using SubSonic;
using SweetCMS.DataAccess;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;
using static CMS.Core.Manager.FriendlyUrlBLL;

namespace CMS.Core.Manager
{
    public class CategoryType
    {
        public const byte None = 0;
        public const byte Article = 1;
        public const byte KeyProject = 2;
        public const byte Project = 10;
        public const byte Video = 11;
        public const byte FileAttactment = 12;
        public const byte ChuongTrinhDaoTao = 20;
        public const byte NhomCoCau = 30;
        //public const int Category = 0;
        //public const int Article = 1;
        //public const int Project = 2;
        //public const int Info = 3;


    }
    public class DanhMucBaiVietBLL
    {
    
        public static List<DanhMucDto> GetPaging(int? PageSize, int? PageIndex, string Key, bool? ASC, int? DanhMucChaId, out int rowsCount)
        {
            rowsCount = 0;
            StoredProcedure sp = SPs.StoreDanhMucTimKiemPhanTrang(PageSize, PageIndex, Key, ASC, DanhMucChaId, out rowsCount);
            DataSet ds = sp.GetDataSet();
            if (ds != null && ds.Tables.Count > 0)
            {
                if (sp.OutputValues.Count > 0)
                    rowsCount = Convert.ToInt32(sp.OutputValues[0]);

            }
            return sp.ExecuteTypedList<DanhMucDto>();
        }
        //public static List<DanhMuc> GetDanhMucByLangIdAndDanhMucType(int langId, int CatType)
        //{
            

        //        //select* from DanhMuc as d
        //        //inner join FriendlyUrl as f on d.Id = f.PostId
        //        //where PostType = 0 and langID = 2

        //}

        public static List<DanhMuc> GetNameAndId(int langId)
        {
             return new Select(DanhMuc.IdColumn, DanhMuc.TenColumn)
                .From(DanhMuc.Schema).Where(DanhMuc.LangIDColumn)
                .IsEqualTo(langId).ExecuteTypedList<DanhMuc>();
        }

        public static DanhMuc GetById(int id)
        {
            return new DanhMucController().FetchByID(id).SingleOrDefault();

        }
        public static DanhMuc Insert(DanhMuc danhMuc)
        {

            danhMuc = new DanhMucController().Insert(danhMuc);
            if(danhMuc.Type == CategoryType.FileAttactment && danhMuc.DanhMucChaId != 0)
            {
                return danhMuc;
            }    
            FriendlyUrlBLL.Insert(new FriendlyUrl()
            {
                PostId = danhMuc.Id,
                PostType = FriendlyUrlBLL.FriendlyURLTypeHelper.Category,
                SlugUrl = danhMuc.Slug,
                Status = BasicStatusHelper.Active
            });
            return danhMuc;
        }
        public static List<DanhMuc> GetAllNoPaging(int langId, int CatType, string keySearch = null)
        {
            if(keySearch != null)
            {
                string sql = string.Format("select * from DanhMuc as  where PostType = {1} and langID = {2} and (Ten like N'%{0}%' or Slug like '%{0}%') and Status != {3}", keySearch, CatType, langId, BasicStatusHelper.Deleted)
;               return new InlineQuery().ExecuteTypedList<DanhMuc>(sql);
            }
            return new Select().From(DanhMuc.Schema)
                .Where(DanhMuc.LangIDColumn).IsEqualTo(langId).And(DanhMuc.TypeColumn).IsEqualTo(CatType)
                .And(DanhMuc.StatusColumn).IsNotEqualTo(BasicStatusHelper.Deleted)
                .ExecuteTypedList<DanhMuc>();
        }
        public static DanhMuc Update(DanhMuc danhMuc)
        {
            danhMuc = new DanhMucController().Update(danhMuc);
            if (danhMuc.Type == CategoryType.FileAttactment && danhMuc.DanhMucChaId != 0)
            {
                return danhMuc;
            }
            var friendlyUrl = FriendlyUrlBLL.GetByPostIdAndTypeId(danhMuc.Id, FriendlyUrlBLL.FriendlyURLTypeHelper.Category);
            if(danhMuc.Slug != friendlyUrl.SlugUrl)
            {
                friendlyUrl.SlugUrl = danhMuc.Slug;
                //friendlyUrl.Status = BasicStatusHelper.Active;
                //friendlyUrl.PostType = FriendlyURLTypeHelper.Category;
                FriendlyUrlBLL.Update(friendlyUrl);
            }
            return danhMuc;
        }
        public static List<DanhMuc> GetAllByParentId(int id)
        {
            var select = new Select().From(DanhMuc.Schema).Where(DanhMuc.DanhMucChaIdColumn).IsEqualTo(id);
            return select.ExecuteTypedList<DanhMuc>();
        }
        public static bool Delete(int id)
        {
            new Delete().From(NhomBaiViet.Schema)
                .Where(NhomBaiViet.DanhmucIdColumn)
                .IsEqualTo(id).Execute();

            FriendlyUrlBLL.DeleteByPostId(id, FriendlyURLTypeHelper.Category);
            new Update(DanhMuc.Schema)
                .Set(DanhMuc.DanhMucChaIdColumn).EqualTo(0)
                .Where(DanhMuc.DanhMucChaIdColumn).IsEqualTo(id).Execute();
            new Update(DanhMuc.Schema)
                .Set(DanhMuc.SlugColumn).EqualTo(string.Empty)
                .Where(DanhMuc.DanhMucChaIdColumn).IsEqualTo(id).Execute();
            return new Update(DanhMuc.Schema)
                .Set(DanhMuc.StatusColumn).EqualTo(BasicStatusHelper.Deleted)
                .Where(DanhMuc.IdColumn).IsEqualTo(id).Execute() > 0; 
        }

        public static DanhMuc IsExistsSlug(string slug)
        {
            Select select = new Select();
            select.From(DanhMuc.Schema).Where(DanhMuc.SlugColumn).IsEqualTo(slug);
            return select.ExecuteSingle<DanhMuc>();

        }
        public static List<BaiVietInDanhMucDto> GetBaiVietInDanhMuc(int danhMucId, int langId)
        {
            string sql = string.Format($@" select b.Id as BaiVietId, b.TieuDe, n.DanhmucId as DanhMucId,
                case 
	                when n.Id is null then 0
	                else 1
	                end as DaChon
                from BaiViet as b 
                left join NhomBaiViet as n on b.Id = n.BaiVietId and n.DanhmucId = {danhMucId} and b.LangID = {langId} where TypeBaiViet = {TypeBaiViet.BaiViet} and Status != '{BasicStatusHelper.Deleted}' ");
            return new InlineQuery().ExecuteTypedList<BaiVietInDanhMucDto>(sql);

        }
        public static string UpdateDanhMucBaiViet(List<BaiVietInDanhMucDto> baiVietInDanhMucUpdate, List<BaiVietInDanhMucDto> baiVietInDanhMucCurent, int danhMucId)
        {

            try
            {
                for (int i = 0; i < baiVietInDanhMucCurent.Count(); i++)
                {
                    if (baiVietInDanhMucCurent[i].DaChon != baiVietInDanhMucUpdate[i].DaChon)
                    {
                        if (baiVietInDanhMucUpdate[i].DaChon == 1)
                        {
                            new NhomBaiVietController().Insert(danhMucId, baiVietInDanhMucUpdate[i].BaiVietId);
                        }
                        else
                        {
                            new Delete().From(NhomBaiViet.Schema).Where(NhomBaiViet.DanhmucIdColumn).IsEqualTo(danhMucId)
                                .And(NhomBaiViet.BaiVietIdColumn).IsEqualTo(baiVietInDanhMucUpdate[i].BaiVietId).Execute();

                        }
                    }
                }
                return "Đã update thành công";
            }catch(Exception ex)
            {
                return "Update thất bại, lỗi: " + ex.Message;
            }

          
        }
        #region
        public static DanhMuc InsertDuAnTieuBieu(DanhMuc danhMuc)
        {
            danhMuc.DanhMucChaId = 4;
            return new DanhMucController().Insert(danhMuc);
        }
        #endregion


        public static List<DanhMuc> GetAllDanhMucByType(int type)
        {
            Select select = new Select();
            select.From(DanhMuc.Schema).Where(DanhMuc.TypeColumn).IsEqualTo(type);
            return select.ExecuteTypedList<DanhMuc>();
        }

    }
}
