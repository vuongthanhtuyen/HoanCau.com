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
    /// Controller class for LichSuPhatTrien
    /// </summary>
    [System.ComponentModel.DataObject]
    public partial class LichSuPhatTrienController
    {
        // Preload our schema..
        LichSuPhatTrien thisSchemaLoad = new LichSuPhatTrien();
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
        public LichSuPhatTrienCollection FetchAll()
        {
            LichSuPhatTrienCollection coll = new LichSuPhatTrienCollection();
            Query qry = new Query(LichSuPhatTrien.Schema);
            coll.LoadAndCloseReader(qry.ExecuteReader());
            return coll;
        }
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public LichSuPhatTrienCollection FetchByID(object Id)
        {
            LichSuPhatTrienCollection coll = new LichSuPhatTrienCollection().Where("Id", Id).Load();
            return coll;
        }
		
		[DataObjectMethod(DataObjectMethodType.Select, false)]
        public LichSuPhatTrienCollection FetchByQuery(Query qry)
        {
            LichSuPhatTrienCollection coll = new LichSuPhatTrienCollection();
            coll.LoadAndCloseReader(qry.ExecuteReader()); 
            return coll;
        }
        [DataObjectMethod(DataObjectMethodType.Delete, true)]
        public bool Delete(object Id)
        {
            return (LichSuPhatTrien.Delete(Id) == 1);
        }
        [DataObjectMethod(DataObjectMethodType.Delete, false)]
        public bool Destroy(object Id)
        {
            return (LichSuPhatTrien.Destroy(Id) == 1);
        }
        
        
    	
	    /// <summary>
	    /// Inserts a record, can be used with the Object Data Source
	    /// </summary>
        [DataObjectMethod(DataObjectMethodType.Insert, true)]
	    public void Insert(string Nam,string TieuDe,string MoTa,string HinhAnhUrl,bool? TrangThai,DateTime? NgayTao)
	    {
		    LichSuPhatTrien item = new LichSuPhatTrien();
		    
            item.Nam = Nam;
            
            item.TieuDe = TieuDe;
            
            item.MoTa = MoTa;
            
            item.HinhAnhUrl = HinhAnhUrl;
            
            item.TrangThai = TrangThai;
            
            item.NgayTao = NgayTao;
            
	    
		    item.Save(UserName);
	    }
    	
	    /// <summary>
	    /// Updates a record, can be used with the Object Data Source
	    /// </summary>
        [DataObjectMethod(DataObjectMethodType.Update, true)]
	    public void Update(int Id,string Nam,string TieuDe,string MoTa,string HinhAnhUrl,bool? TrangThai,DateTime? NgayTao)
	    {
		    LichSuPhatTrien item = new LichSuPhatTrien();
	        item.MarkOld();
	        item.IsLoaded = true;
		    
			item.Id = Id;
				
			item.Nam = Nam;
				
			item.TieuDe = TieuDe;
				
			item.MoTa = MoTa;
				
			item.HinhAnhUrl = HinhAnhUrl;
				
			item.TrangThai = TrangThai;
				
			item.NgayTao = NgayTao;
				
	        item.Save(UserName);
	    }
    }
}
