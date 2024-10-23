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
    /// Controller class for NguoiDung_VaiTro_Chitiet
    /// </summary>
    [System.ComponentModel.DataObject]
    public partial class NguoiDungVaiTroChitietController
    {
        // Preload our schema..
        NguoiDungVaiTroChitiet thisSchemaLoad = new NguoiDungVaiTroChitiet();
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
        public NguoiDungVaiTroChitietCollection FetchAll()
        {
            NguoiDungVaiTroChitietCollection coll = new NguoiDungVaiTroChitietCollection();
            Query qry = new Query(NguoiDungVaiTroChitiet.Schema);
            coll.LoadAndCloseReader(qry.ExecuteReader());
            return coll;
        }
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public NguoiDungVaiTroChitietCollection FetchByID(object Id)
        {
            NguoiDungVaiTroChitietCollection coll = new NguoiDungVaiTroChitietCollection().Where("Id", Id).Load();
            return coll;
        }
		
		[DataObjectMethod(DataObjectMethodType.Select, false)]
        public NguoiDungVaiTroChitietCollection FetchByQuery(Query qry)
        {
            NguoiDungVaiTroChitietCollection coll = new NguoiDungVaiTroChitietCollection();
            coll.LoadAndCloseReader(qry.ExecuteReader()); 
            return coll;
        }
        [DataObjectMethod(DataObjectMethodType.Delete, true)]
        public bool Delete(object Id)
        {
            return (NguoiDungVaiTroChitiet.Delete(Id) == 1);
        }
        [DataObjectMethod(DataObjectMethodType.Delete, false)]
        public bool Destroy(object Id)
        {
            return (NguoiDungVaiTroChitiet.Destroy(Id) == 1);
        }
        
        
    	
	    /// <summary>
	    /// Inserts a record, can be used with the Object Data Source
	    /// </summary>
        [DataObjectMethod(DataObjectMethodType.Insert, true)]
	    public void Insert(int NguoiDungId,int VaiTroId)
	    {
		    NguoiDungVaiTroChitiet item = new NguoiDungVaiTroChitiet();
		    
            item.NguoiDungId = NguoiDungId;
            
            item.VaiTroId = VaiTroId;
            
	    
		    item.Save(UserName);
	    }
    	
	    /// <summary>
	    /// Updates a record, can be used with the Object Data Source
	    /// </summary>
        [DataObjectMethod(DataObjectMethodType.Update, true)]
	    public void Update(int Id,int NguoiDungId,int VaiTroId)
	    {
		    NguoiDungVaiTroChitiet item = new NguoiDungVaiTroChitiet();
	        item.MarkOld();
	        item.IsLoaded = true;
		    
			item.Id = Id;
				
			item.NguoiDungId = NguoiDungId;
				
			item.VaiTroId = VaiTroId;
				
	        item.Save(UserName);
	    }
    }
}
