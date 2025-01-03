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
    /// Controller class for MenuWebDuoi
    /// </summary>
    [System.ComponentModel.DataObject]
    public partial class MenuWebDuoiController
    {
        // Preload our schema..
        MenuWebDuoi thisSchemaLoad = new MenuWebDuoi();
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
        public MenuWebDuoiCollection FetchAll()
        {
            MenuWebDuoiCollection coll = new MenuWebDuoiCollection();
            Query qry = new Query(MenuWebDuoi.Schema);
            coll.LoadAndCloseReader(qry.ExecuteReader());
            return coll;
        }
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public MenuWebDuoiCollection FetchByID(object Id)
        {
            MenuWebDuoiCollection coll = new MenuWebDuoiCollection().Where("Id", Id).Load();
            return coll;
        }
		
		[DataObjectMethod(DataObjectMethodType.Select, false)]
        public MenuWebDuoiCollection FetchByQuery(Query qry)
        {
            MenuWebDuoiCollection coll = new MenuWebDuoiCollection();
            coll.LoadAndCloseReader(qry.ExecuteReader()); 
            return coll;
        }
        [DataObjectMethod(DataObjectMethodType.Delete, true)]
        public bool Delete(object Id)
        {
            return (MenuWebDuoi.Delete(Id) == 1);
        }
        [DataObjectMethod(DataObjectMethodType.Delete, false)]
        public bool Destroy(object Id)
        {
            return (MenuWebDuoi.Destroy(Id) == 1);
        }
        
        
    	
	    /// <summary>
	    /// Inserts a record, can be used with the Object Data Source
	    /// </summary>
        [DataObjectMethod(DataObjectMethodType.Insert, true)]
	    public MenuWebDuoi Insert(MenuWebDuoi menuWebDuoi)
	    {
            menuWebDuoi.Save(UserName);
            return menuWebDuoi;
	    }
    	
	    /// <summary>
	    /// Updates a record, can be used with the Object Data Source
	    /// </summary>
        [DataObjectMethod(DataObjectMethodType.Update, true)]
        public MenuWebDuoi Update(MenuWebDuoi menuWebDuoi)
        {
            menuWebDuoi.Save(UserName);
            return menuWebDuoi;
        }
    }
}
