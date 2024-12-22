using CMS.DataAsscess;
using SubSonic;
using SweetCMS.DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CMS.Core.Manager.FriendlyUrlBLL;

namespace CMS.Core.Manager
{


    public class FileAttactmentType
    {
        public const byte FileDinhKem = 1;
    }
    public class FileAttactmentBLL
    {

        // xóa tất cả Attachmentfile của một post
        public static bool DeleteAllAttachmentFileInPost(int postId, int postType)
        {
            try
            {
                return new Delete().From(FileAttactment.Schema)
                    .Where(FileAttactment.CategoryIdColumn).IsEqualTo(postId)
                    .And(FileAttactment.TypeColumn).IsEqualTo(postType)
                    .Execute() > 0;
            }
            catch
            {
                return false;
            }
        }

        public static List<FileAttactment> GetAllByTypeId(int type)
        {
            try
            {
                return new Select().From(FileAttactment.Schema)
                    .Where(FileAttactment.TypeColumn).IsEqualTo(type)
                    .ExecuteTypedList<FileAttactment>();
            }
            catch
            {
                return null;
            }
        }
        public static List<FileAttactment> GetNameAndId(int langId)
        {
            return new Select(FileAttactment.IdColumn, FileAttactment.TitleColumn)
               .From(FileAttactment.Schema).ExecuteTypedList<FileAttactment>();
        }

        public static FileAttactment GetById(int id)
        {
            return new FileAttactmentController().FetchByID(id).SingleOrDefault();

        }
        public static FileAttactment Insert(FileAttactment fileAttactment)
        {
            return new FileAttactmentController().Insert(fileAttactment);
        }

        public static FileAttactment Update(FileAttactment fileAttactment)
        {

            return new FileAttactmentController().Update(fileAttactment);
        }
        public static List<ItemFile> GetAllByTypeAndCateId(int type, int cateid)
        {
            var select = new Select().From(FileAttactment.Schema).Where(FileAttactment.TypeColumn).IsEqualTo(type)
                .And(FileAttactment.CategoryIdColumn).IsEqualTo(cateid);
            return select.ExecuteTypedList<ItemFile>();
        }
        public static bool Delete(int id)
        {


            return new Delete().From(FileAttactment.Schema)
                .Where(FileAttactment.IdColumn)
                .IsEqualTo(id).Execute() > 0;
        }

        public static List<ItemFile> GetAllAttachmentFileByPostAndTypeId(int postId, int typeId)
        {
            try
            {

                return new Select().From(FileAttactment.Schema)
                    .Where(FileAttactment.CategoryIdColumn).IsEqualTo(postId)
                    .And(FileAttactment.TypeColumn).IsEqualTo(typeId)
                    .ExecuteTypedList<ItemFile>();
            }
            catch
            {
                return null;
            }
        }
        //public static List<ItemFileFrontend> GetAllAttachmentFileFrontendByPostAndTypeId(int postId, int typeId)
        //{
        //    try
        //    {

        //        return new Select(FileAttactment.AltColumn, FileAttactment.AttachmentFileUrlColumn).From(FileAttactment.Schema)
        //            .Where(FileAttactment.PostIdColumn).IsEqualTo(postId)
        //            .And(FileAttactment.AttachmentFileTypeColumn).IsEqualTo(typeId)
        //            .ExecuteTypedList<ItemFileFrontend>();
        //    }
        //    catch
        //    {
        //        return null;
        //    }
        //}
        public static FileAttactment GetAttachmentFilebyPostIdAndTypeAndId(int postId, int typeId, int attId)
        {
            try
            {
                return new Select().From(FileAttactment.Schema)
                    .Where(FileAttactment.CategoryIdColumn).IsEqualTo(postId)
                    .And(FileAttactment.TypeColumn).IsEqualTo(typeId)
                    .And(FileAttactment.IdColumn).IsEqualTo(attId)
                    .ExecuteSingle<FileAttactment>();
            }
            catch
            {
                return null;
            }
        }
        public static FileAttactment GetAttachmentFileById(int Id)
        {
            return new FileAttactmentController().FetchByID(Id).FirstOrDefault();
        }
        public static FileAttactment InsertAttachmentFileInPost(FileAttactment obj)
        {
            return new FileAttactmentController().Insert(obj);
        }
        public static FileAttactment UpdateAttachmentFileInPost(FileAttactment obj)
        {
            return new FileAttactmentController().Update(obj);
        }
        public static bool DeleteAttachmentFile(object AttachmentFileId)
        {
            try
            {
                return new FileAttactmentController().Delete(AttachmentFileId);
            }
            catch
            {
                return false;
            }
        }

        #region Danh Muc File Dinh Kem
        public static DanhMuc InsertDanhMuc(DanhMuc danhMuc)
        {

            danhMuc = new DanhMucController().Insert(danhMuc);
            if (danhMuc.Type == CategoryType.FileAttactment && danhMuc.DanhMucChaId != 0)
            {
                return danhMuc;
            }
            FriendlyUrlBLL.Insert(new FriendlyUrl()
            {
                PostId = danhMuc.Id,
                PostType = FriendlyUrlBLL.FriendlyURLTypeHelper.FileAttactment,
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
                var friendlyUrl = FriendlyUrlBLL.GetByPostIdAndTypeId(danhMuc.Id, FriendlyURLTypeHelper.FileAttactment);
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
            
            new Delete().From(FileAttactment.Schema)
                .Where(FileAttactment.CategoryIdColumn)
                .IsEqualTo(id).And(FileAttactment.TypeColumn).IsEqualTo(FileAttactmentType.FileDinhKem)
                .Execute();

            FriendlyUrlBLL.DeleteByPostId(id, FriendlyURLTypeHelper.FileAttactment);
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
        #region Front end
        public static List<ItemDanhMucFileDinhKem> GetChildInByParentDanhMuc(object parentId)
        {
            
                string sql = string.Format(@" WITH DanhMucCon AS (
                        SELECT Id, Ten, DanhMucChaId, DisplayOrder, Status
                        FROM DanhMuc
                        WHERE DanhMucChaId = {0} AND Status != '{1}'
                        UNION ALL
                        SELECT dm.Id, dm.Ten, dm.DanhMucChaId, dm.DisplayOrder, dm.Status
                        FROM DanhMuc dm
                        INNER JOIN DanhMucCon dmc ON dm.DanhMucChaId = dmc.Id
                        WHERE dm.Status != '{1}'
                    )
                    SELECT Id, Ten, DanhMucChaId, DisplayOrder
                    FROM DanhMucCon
                    ORDER BY DisplayOrder; ",parentId, BasicStatusHelper.Deleted);
            return new InlineQuery().ExecuteTypedList<ItemDanhMucFileDinhKem>(sql);
           
           
        }
        public static List<ItemFileDto> GetAllFileDinhKemByListCate(int[] listIdOrg)
        {
            try
            {
                string listIdOrgString = string.Join(",", listIdOrg);


                string sql = string.Format(@" select * from FileAttactment 
                where CategoryId in ({0})  and Type = {1}
                 ", listIdOrgString, FileAttactmentType.FileDinhKem);
                return new InlineQuery().ExecuteTypedList<ItemFileDto>(sql);
            }
            catch
            {
                return null;
            }
        }

        #endregion

    }
}