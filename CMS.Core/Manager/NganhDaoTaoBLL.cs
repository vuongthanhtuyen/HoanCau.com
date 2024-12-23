using CMS.DataAsscess;
using SubSonic;
using SweetCMS.DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Core.Manager
{
    public class NganhDaoTaoBLL
    {
        public static NganhDaoTao GetById(int id)
        {
            return new NganhDaoTaoController().FetchByID(id).SingleOrDefault();

        }
        public static NganhDaoTao Insert(NganhDaoTao fileAttactment)
        {
            return new NganhDaoTaoController().Insert(fileAttactment);
        }

        public static NganhDaoTao Update(NganhDaoTao fileAttactment)
        {
            return new NganhDaoTaoController().Update(fileAttactment);
        }
        public static bool Delete(int id)
        {
            return new Delete().From(NganhDaoTao.Schema)
                .Where(NganhDaoTao.IdColumn)
                .IsEqualTo(id).Execute() > 0;
        }
        public static DataTable GetPaging(int? PageSize, int? PageIndex, string Key, bool? ASC, out int rowsCount)
        {
            rowsCount = 0;
            StoredProcedure sp = SPs.StoreNganhDaoTaoTimKiemPhanTrang(PageSize, PageIndex, Key, ASC, out rowsCount);
            DataSet ds = sp.GetDataSet();
            if (ds != null && ds.Tables.Count > 0)
            {
                if (sp.OutputValues.Count > 0)
                {
                    int u = -1;
                    if (int.TryParse(sp.OutputValues[0].ToString(), out u))
                        rowsCount = u;
                }
                return ds.Tables[0];
            }
            return null;
        }


        //public static bool DeleteAllAttachmentFileInPost(int postId, int postType)
        //{
        //    try
        //    {
        //        return new Delete().From(NganhDaoTao.Schema)
        //            .Where(NganhDaoTao.CategoryIdColumn).IsEqualTo(postId)
        //            .And(NganhDaoTao.TypeColumn).IsEqualTo(postType)
        //            .Execute() > 0;
        //    }
        //    catch
        //    {
        //        return false;
        //    }
        //}

        //public static List<NganhDaoTao> GetAllByTypeId(int type)
        //{
        //    try
        //    {
        //        return new Select().From(NganhDaoTao.Schema)
        //            .Where(NganhDaoTao.TypeColumn).IsEqualTo(type)
        //            .ExecuteTypedList<NganhDaoTao>();
        //    }
        //    catch
        //    {
        //        return null;
        //    }
        //}
        //public static List<NganhDaoTao> GetNameAndId(int langId)
        //{
        //    return new Select(NganhDaoTao.IdColumn, NganhDaoTao.TitleColumn)
        //       .From(NganhDaoTao.Schema).ExecuteTypedList<NganhDaoTao>();
        //}


        //public static List<ItemFile> GetAllByTypeAndCateId(int type, int cateid)
        //{
        //    var select = new Select().From(NganhDaoTao.Schema).Where(NganhDaoTao.TypeColumn).IsEqualTo(type)
        //        .And(NganhDaoTao.CategoryIdColumn).IsEqualTo(cateid);
        //    return select.ExecuteTypedList<ItemFile>();
        //}


        //public static List<ItemFile> GetAllAttachmentFileByPostAndTypeId(int postId, int typeId)
        //{
        //    try
        //    {

        //        return new Select().From(NganhDaoTao.Schema)
        //            .Where(NganhDaoTao.CategoryIdColumn).IsEqualTo(postId)
        //            .And(NganhDaoTao.TypeColumn).IsEqualTo(typeId)
        //            .ExecuteTypedList<ItemFile>();
        //    }
        //    catch
        //    {
        //        return null;
        //    }
        //}
        //public static List<ItemFileFrontend> GetAllAttachmentFileFrontendByPostAndTypeId(int postId, int typeId)
        //{
        //    try
        //    {

        //        return new Select(NganhDaoTao.AltColumn, NganhDaoTao.AttachmentFileUrlColumn).From(NganhDaoTao.Schema)
        //            .Where(NganhDaoTao.PostIdColumn).IsEqualTo(postId)
        //            .And(NganhDaoTao.AttachmentFileTypeColumn).IsEqualTo(typeId)
        //            .ExecuteTypedList<ItemFileFrontend>();
        //    }
        //    catch
        //    {
        //        return null;
        //    }
        //}
        //public static NganhDaoTao GetAttachmentFilebyPostIdAndTypeAndId(int postId, int typeId, int attId)
        //{
        //    try
        //    {
        //        return new Select().From(NganhDaoTao.Schema)
        //            .Where(NganhDaoTao.CategoryIdColumn).IsEqualTo(postId)
        //            .And(NganhDaoTao.TypeColumn).IsEqualTo(typeId)
        //            .And(NganhDaoTao.IdColumn).IsEqualTo(attId)
        //            .ExecuteSingle<NganhDaoTao>();
        //    }
        //    catch
        //    {
        //        return null;
        //    }
        //}
        public static NganhDaoTao GetAttachmentFileById(int Id)
        {
            return new NganhDaoTaoController().FetchByID(Id).FirstOrDefault();
        }
        public static NganhDaoTao InsertAttachmentFileInPost(NganhDaoTao obj)
        {
            return new NganhDaoTaoController().Insert(obj);
        }
        public static NganhDaoTao UpdateAttachmentFileInPost(NganhDaoTao obj)
        {
            return new NganhDaoTaoController().Update(obj);
        }
        public static bool DeleteAttachmentFile(object AttachmentFileId)
        {
            try
            {
                return new NganhDaoTaoController().Delete(AttachmentFileId);
            }
            catch
            {
                return false;
            }
        }

    }
}
