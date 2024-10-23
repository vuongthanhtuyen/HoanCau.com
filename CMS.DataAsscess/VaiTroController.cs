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
    /// Controller class for VaiTro
    /// </summary>
    [System.ComponentModel.DataObject]
    public partial class VaiTroController
    {
        // Preload our schema..
        VaiTro thisSchemaLoad = new VaiTro();
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
        public VaiTroCollection FetchAll()
        {
            VaiTroCollection coll = new VaiTroCollection();
            Query qry = new Query(VaiTro.Schema);
            coll.LoadAndCloseReader(qry.ExecuteReader());
            return coll;
        }
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public VaiTroCollection FetchByID(object Id)
        {
            VaiTroCollection coll = new VaiTroCollection().Where("Id", Id).Load();
            return coll;
        }
		
		[DataObjectMethod(DataObjectMethodType.Select, false)]
        public VaiTroCollection FetchByQuery(Query qry)
        {
            VaiTroCollection coll = new VaiTroCollection();
            coll.LoadAndCloseReader(qry.ExecuteReader()); 
            return coll;
        }
        [DataObjectMethod(DataObjectMethodType.Delete, true)]
        public bool Delete(object Id)
        {
            return (VaiTro.Delete(Id) == 1);
        }
        [DataObjectMethod(DataObjectMethodType.Delete, false)]
        public bool Destroy(object Id)
        {
            return (VaiTro.Destroy(Id) == 1);
        }
        
        
    	
	    /// <summary>
	    /// Inserts a record, can be used with the Object Data Source
	    /// </summary>
        [DataObjectMethod(DataObjectMethodType.Insert, true)]
        public VaiTro Insert(VaiTro vaiTro)
        {
            vaiTro.Save(UserName);
            return vaiTro;
        }

        /// <summary>
        /// Updates a record, can be used with the Object Data Source
        /// </summary>
        [DataObjectMethod(DataObjectMethodType.Update, true)]
	    public VaiTro Update(VaiTro vaiTro)
	    {
		  	vaiTro.Save(UserName);
            return vaiTro;  
	    }
    }
}
