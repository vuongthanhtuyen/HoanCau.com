using CMS.DataAsscess;
using SubSonic;
using TBDCMS.DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CMS.Core.Manager.FriendlyUrlBLL;

namespace CMS.Core.Manager
{
    public class ChuongTrinhDaoTaoBLL
    {
        #region Danh Muc File Dinh Kem
        public static DanhMuc InsertDanhMuc(DanhMuc danhMuc)
        {

            danhMuc = new DanhMucController().Insert(danhMuc);
            if (danhMuc.Type == CategoryType.ChuongTrinhDaoTao && danhMuc.DanhMucChaId != 0)
            {
                return danhMuc;
            }
            FriendlyUrlBLL.Insert(new FriendlyUrl()
            {
                PostId = danhMuc.Id,
                PostType = FriendlyUrlBLL.FriendlyURLTypeHelper.ChuongTrinhDaoTao,
                SlugUrl = danhMuc.Slug,
                Status = BasicStatusHelper.Active

            });
            return danhMuc;
        }
        public static List<DanhMuc> GetAllNoPaging(int langId, int CatType, string keySearch = null)
        {
            if (keySearch != null)
            {
                string sql = string.Format("select * from DanhMuc as  where PostType = {1} and langID = {2} and (Ten like N'%{0}%' or Slug like '%{0}%') and Status != {3}", keySearch, CatType, langId, BasicStatusHelper.Deleted)
; return new InlineQuery().ExecuteTypedList<DanhMuc>(sql);
            }
            return new Select().From(DanhMuc.Schema)
                .Where(DanhMuc.LangIDColumn).IsEqualTo(langId).And(DanhMuc.TypeColumn).IsEqualTo(CatType)
                .And(DanhMuc.StatusColumn).IsNotEqualTo(BasicStatusHelper.Deleted)
                .ExecuteTypedList<DanhMuc>();
        }
        public static DanhMuc UpdateDanhMuc(DanhMuc danhMuc)
        {
            danhMuc = new DanhMucController().Update(danhMuc);
            if (danhMuc.DanhMucChaId == 0)
            {
                var friendlyUrl = FriendlyUrlBLL.GetByPostIdAndTypeId(danhMuc.Id, FriendlyURLTypeHelper.ChuongTrinhDaoTao);
                if (friendlyUrl != null && danhMuc.Slug != friendlyUrl.SlugUrl)
                {
                    friendlyUrl.SlugUrl = danhMuc.Slug;
                    FriendlyUrlBLL.Update(friendlyUrl);
                }
            }

            return danhMuc;
        }
        public static List<DanhMuc> GetAllByParentId(int id)
        {
            var select = new Select().From(DanhMuc.Schema).Where(DanhMuc.DanhMucChaIdColumn).IsEqualTo(id);
            return select.ExecuteTypedList<DanhMuc>();
        }
        public static bool DeleteDanhMuc(int id)
        {
            new Delete().From(NhomBaiViet.Schema)
                .Where(NhomBaiViet.DanhmucIdColumn)
                .IsEqualTo(id).Execute();

            FriendlyUrlBLL.DeleteByPostId(id, FriendlyURLTypeHelper.ChuongTrinhDaoTao);
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

     

     

        #endregion
    }
}
