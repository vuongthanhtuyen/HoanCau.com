using System; 
using System.Text; 
using System.Data;
using System.Data.SqlClient;
using System.Data.Common;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration; 
using System.Xml; 
using System.Xml.Serialization;
using SubSonic; 
using SubSonic.Utilities;
// <auto-generated />
namespace SweetCMS.DataAccess
{
    /// <summary>
    /// Controller class for TrinhChieuAnh
    /// </summary>
    [System.ComponentModel.DataObject]
    public partial class TrinhChieuAnhController
    {
        // Preload our schema..
        TrinhChieuAnh thisSchemaLoad = new TrinhChieuAnh();
        private string userName = String.Empty;
        protected string UserName
        {
            get
            {
				if (userName.Length == 0) 
				{
    				if (System.Web.HttpContext.Current != null)
    				{
						userName=System.Web.HttpContext.Current.User.Identity.Name;
					}
					else
					{
						userName=System.Threading.Thread.CurrentPrincipal.Identity.Name;
					}
				}
				return userName;
            }
        }
        [DataObjectMethod(DataObjectMethodType.Select, true)]
        public TrinhChieuAnhCollection FetchAll()
        {
            TrinhChieuAnhCollection coll = new TrinhChieuAnhCollection();
            Query qry = new Query(TrinhChieuAnh.Schema);
            coll.LoadAndCloseReader(qry.ExecuteReader());
            return coll;
        }
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public TrinhChieuAnhCollection FetchByID(object Id)
        {
            TrinhChieuAnhCollection coll = new TrinhChieuAnhCollection().Where("Id", Id).Load();
            return coll;
        }
		
		[DataObjectMethod(DataObjectMethodType.Select, false)]
        public TrinhChieuAnhCollection FetchByQuery(Query qry)
        {
            TrinhChieuAnhCollection coll = new TrinhChieuAnhCollection();
            coll.LoadAndCloseReader(qry.ExecuteReader()); 
            return coll;
        }
        [DataObjectMethod(DataObjectMethodType.Delete, true)]
        public bool Delete(object Id)
        {
            return (TrinhChieuAnh.Delete(Id) == 1);
        }
        [DataObjectMethod(DataObjectMethodType.Delete, false)]
        public bool Destroy(object Id)
        {
            return (TrinhChieuAnh.Destroy(Id) == 1);
        }
        
        
    	
	    /// <summary>
	    /// Inserts a record, can be used with the Object Data Source
	    /// </summary>
        [DataObjectMethod(DataObjectMethodType.Insert, true)]
	    public TrinhChieuAnh Insert(TrinhChieuAnh slide)
	    {
		    slide.Save(UserName);
            return slide;
	    }
    	
	    /// <summary>
	    /// Updates a record, can be used with the Object Data Source
	    /// </summary>
        [DataObjectMethod(DataObjectMethodType.Update, true)]
        public TrinhChieuAnh Update(TrinhChieuAnh slide)
        {
            slide.Save(UserName);
            return slide;
        }
    }
}
