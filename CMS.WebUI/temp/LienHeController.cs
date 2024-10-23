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
    /// Controller class for LienHe
    /// </summary>
    [System.ComponentModel.DataObject]
    public partial class LienHeController
    {
        // Preload our schema..
        LienHe thisSchemaLoad = new LienHe();
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
        public LienHeCollection FetchAll()
        {
            LienHeCollection coll = new LienHeCollection();
            Query qry = new Query(LienHe.Schema);
            coll.LoadAndCloseReader(qry.ExecuteReader());
            return coll;
        }
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public LienHeCollection FetchByID(object Id)
        {
            LienHeCollection coll = new LienHeCollection().Where("Id", Id).Load();
            return coll;
        }
		
		[DataObjectMethod(DataObjectMethodType.Select, false)]
        public LienHeCollection FetchByQuery(Query qry)
        {
            LienHeCollection coll = new LienHeCollection();
            coll.LoadAndCloseReader(qry.ExecuteReader()); 
            return coll;
        }
        [DataObjectMethod(DataObjectMethodType.Delete, true)]
        public bool Delete(object Id)
        {
            return (LienHe.Delete(Id) == 1);
        }
        [DataObjectMethod(DataObjectMethodType.Delete, false)]
        public bool Destroy(object Id)
        {
            return (LienHe.Destroy(Id) == 1);
        }
        
        
    	
	    /// <summary>
	    /// Inserts a record, can be used with the Object Data Source
	    /// </summary>
        [DataObjectMethod(DataObjectMethodType.Insert, true)]
	    public void Insert(string HoVaTen,string Email,string SoDienThoai,string ChuDe,string TinNhan,DateTime? NgayTao,bool? DaTraLoi,DateTime? NgayTraLoi)
	    {
		    LienHe item = new LienHe();
		    
            item.HoVaTen = HoVaTen;
            
            item.Email = Email;
            
            item.SoDienThoai = SoDienThoai;
            
            item.ChuDe = ChuDe;
            
            item.TinNhan = TinNhan;
            
            item.NgayTao = NgayTao;
            
            item.DaTraLoi = DaTraLoi;
            
            item.NgayTraLoi = NgayTraLoi;
            
	    
		    item.Save(UserName);
	    }
    	
	    /// <summary>
	    /// Updates a record, can be used with the Object Data Source
	    /// </summary>
        [DataObjectMethod(DataObjectMethodType.Update, true)]
	    public void Update(int Id,string HoVaTen,string Email,string SoDienThoai,string ChuDe,string TinNhan,DateTime? NgayTao,bool? DaTraLoi,DateTime? NgayTraLoi)
	    {
		    LienHe item = new LienHe();
	        item.MarkOld();
	        item.IsLoaded = true;
		    
			item.Id = Id;
				
			item.HoVaTen = HoVaTen;
				
			item.Email = Email;
				
			item.SoDienThoai = SoDienThoai;
				
			item.ChuDe = ChuDe;
				
			item.TinNhan = TinNhan;
				
			item.NgayTao = NgayTao;
				
			item.DaTraLoi = DaTraLoi;
				
			item.NgayTraLoi = NgayTraLoi;
				
	        item.Save(UserName);
	    }
    }
}