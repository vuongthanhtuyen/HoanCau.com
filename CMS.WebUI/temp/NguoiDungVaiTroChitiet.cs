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
	/// Strongly-typed collection for the NguoiDungVaiTroChitiet class.
	/// </summary>
    [Serializable]
	public partial class NguoiDungVaiTroChitietCollection : ActiveList<NguoiDungVaiTroChitiet, NguoiDungVaiTroChitietCollection>
	{	   
		public NguoiDungVaiTroChitietCollection() {}
        
        /// <summary>
		/// Filters an existing collection based on the set criteria. This is an in-memory filter
		/// Thanks to developingchris for this!
        /// </summary>
        /// <returns>NguoiDungVaiTroChitietCollection</returns>
		public NguoiDungVaiTroChitietCollection Filter()
        {
            for (int i = this.Count - 1; i > -1; i--)
            {
                NguoiDungVaiTroChitiet o = this[i];
                foreach (SubSonic.Where w in this.wheres)
                {
                    bool remove = false;
                    System.Reflection.PropertyInfo pi = o.GetType().GetProperty(w.ColumnName);
                    if (pi.CanRead)
                    {
                        object val = pi.GetValue(o, null);
                        switch (w.Comparison)
                        {
                            case SubSonic.Comparison.Equals:
                                if (!val.Equals(w.ParameterValue))
                                {
                                    remove = true;
                                }
                                break;
                        }
                    }
                    if (remove)
                    {
                        this.Remove(o);
                        break;
                    }
                }
            }
            return this;
        }
		
		
	}
	/// <summary>
	/// This is an ActiveRecord class which wraps the NguoiDung_VaiTro_Chitiet table.
	/// </summary>
	[Serializable]
	public partial class NguoiDungVaiTroChitiet : ActiveRecord<NguoiDungVaiTroChitiet>, IActiveRecord
	{
		#region .ctors and Default Settings
		
		public NguoiDungVaiTroChitiet()
		{
		  SetSQLProps();
		  InitSetDefaults();
		  MarkNew();
		}
		
		private void InitSetDefaults() { SetDefaults(); }
		
		public NguoiDungVaiTroChitiet(bool useDatabaseDefaults)
		{
			SetSQLProps();
			if(useDatabaseDefaults)
				ForceDefaults();
			MarkNew();
		}
        
		public NguoiDungVaiTroChitiet(object keyID)
		{
			SetSQLProps();
			InitSetDefaults();
			LoadByKey(keyID);
		}
		 
		public NguoiDungVaiTroChitiet(string columnName, object columnValue)
		{
			SetSQLProps();
			InitSetDefaults();
			LoadByParam(columnName,columnValue);
		}
		
		protected static void SetSQLProps() { GetTableSchema(); }
		
		#endregion
		
		#region Schema and Query Accessor	
		public static Query CreateQuery() { return new Query(Schema); }
		public static TableSchema.Table Schema
		{
			get
			{
				if (BaseSchema == null)
					SetSQLProps();
				return BaseSchema;
			}
		}
		
		private static void GetTableSchema() 
		{
			if(!IsSchemaInitialized)
			{
				//Schema declaration
				TableSchema.Table schema = new TableSchema.Table("NguoiDung_VaiTro_Chitiet", TableType.Table, DataService.GetInstance("DataAcessProvider"));
				schema.Columns = new TableSchema.TableColumnCollection();
				schema.SchemaName = @"dbo";
				//columns
				
				TableSchema.TableColumn colvarId = new TableSchema.TableColumn(schema);
				colvarId.ColumnName = "Id";
				colvarId.DataType = DbType.Int32;
				colvarId.MaxLength = 0;
				colvarId.AutoIncrement = true;
				colvarId.IsNullable = false;
				colvarId.IsPrimaryKey = true;
				colvarId.IsForeignKey = false;
				colvarId.IsReadOnly = false;
				colvarId.DefaultSetting = @"";
				colvarId.ForeignKeyTableName = "";
				schema.Columns.Add(colvarId);
				
				TableSchema.TableColumn colvarNguoiDungId = new TableSchema.TableColumn(schema);
				colvarNguoiDungId.ColumnName = "NguoiDungId";
				colvarNguoiDungId.DataType = DbType.Int32;
				colvarNguoiDungId.MaxLength = 0;
				colvarNguoiDungId.AutoIncrement = false;
				colvarNguoiDungId.IsNullable = false;
				colvarNguoiDungId.IsPrimaryKey = false;
				colvarNguoiDungId.IsForeignKey = true;
				colvarNguoiDungId.IsReadOnly = false;
				colvarNguoiDungId.DefaultSetting = @"";
				
					colvarNguoiDungId.ForeignKeyTableName = "NguoiDung";
				schema.Columns.Add(colvarNguoiDungId);
				
				TableSchema.TableColumn colvarVaiTroId = new TableSchema.TableColumn(schema);
				colvarVaiTroId.ColumnName = "VaiTroId";
				colvarVaiTroId.DataType = DbType.Int32;
				colvarVaiTroId.MaxLength = 0;
				colvarVaiTroId.AutoIncrement = false;
				colvarVaiTroId.IsNullable = false;
				colvarVaiTroId.IsPrimaryKey = false;
				colvarVaiTroId.IsForeignKey = true;
				colvarVaiTroId.IsReadOnly = false;
				colvarVaiTroId.DefaultSetting = @"";
				
					colvarVaiTroId.ForeignKeyTableName = "VaiTro";
				schema.Columns.Add(colvarVaiTroId);
				
				BaseSchema = schema;
				//add this schema to the provider
				//so we can query it later
				DataService.Providers["DataAcessProvider"].AddSchema("NguoiDung_VaiTro_Chitiet",schema);
			}
		}
		#endregion
		
		#region Props
		  
		[XmlAttribute("Id")]
		[Bindable(true)]
		public int Id 
		{
			get { return GetColumnValue<int>(Columns.Id); }
			set { SetColumnValue(Columns.Id, value); }
		}
		  
		[XmlAttribute("NguoiDungId")]
		[Bindable(true)]
		public int NguoiDungId 
		{
			get { return GetColumnValue<int>(Columns.NguoiDungId); }
			set { SetColumnValue(Columns.NguoiDungId, value); }
		}
		  
		[XmlAttribute("VaiTroId")]
		[Bindable(true)]
		public int VaiTroId 
		{
			get { return GetColumnValue<int>(Columns.VaiTroId); }
			set { SetColumnValue(Columns.VaiTroId, value); }
		}
		
		#endregion
		
		
			
		
		#region ForeignKey Properties
		
		/// <summary>
		/// Returns a NguoiDung ActiveRecord object related to this NguoiDungVaiTroChitiet
		/// 
		/// </summary>
		public SweetCMS.DataAccess.NguoiDung NguoiDung
		{
			get { return SweetCMS.DataAccess.NguoiDung.FetchByID(this.NguoiDungId); }
			set { SetColumnValue("NguoiDungId", value.Id); }
		}
		
		
		/// <summary>
		/// Returns a VaiTro ActiveRecord object related to this NguoiDungVaiTroChitiet
		/// 
		/// </summary>
		public SweetCMS.DataAccess.VaiTro VaiTro
		{
			get { return SweetCMS.DataAccess.VaiTro.FetchByID(this.VaiTroId); }
			set { SetColumnValue("VaiTroId", value.Id); }
		}
		
		
		#endregion
		
		
		
		//no ManyToMany tables defined (0)
		
        
        
		#region ObjectDataSource support
		
		
		/// <summary>
		/// Inserts a record, can be used with the Object Data Source
		/// </summary>
		public static void Insert(int varNguoiDungId,int varVaiTroId)
		{
			NguoiDungVaiTroChitiet item = new NguoiDungVaiTroChitiet();
			
			item.NguoiDungId = varNguoiDungId;
			
			item.VaiTroId = varVaiTroId;
			
		
			if (System.Web.HttpContext.Current != null)
				item.Save(System.Web.HttpContext.Current.User.Identity.Name);
			else
				item.Save(System.Threading.Thread.CurrentPrincipal.Identity.Name);
		}
		
		/// <summary>
		/// Updates a record, can be used with the Object Data Source
		/// </summary>
		public static void Update(int varId,int varNguoiDungId,int varVaiTroId)
		{
			NguoiDungVaiTroChitiet item = new NguoiDungVaiTroChitiet();
			
				item.Id = varId;
			
				item.NguoiDungId = varNguoiDungId;
			
				item.VaiTroId = varVaiTroId;
			
			item.IsNew = false;
			if (System.Web.HttpContext.Current != null)
				item.Save(System.Web.HttpContext.Current.User.Identity.Name);
			else
				item.Save(System.Threading.Thread.CurrentPrincipal.Identity.Name);
		}
		#endregion
        
        
        
        #region Typed Columns
        
        
        public static TableSchema.TableColumn IdColumn
        {
            get { return Schema.Columns[0]; }
        }
        
        
        
        public static TableSchema.TableColumn NguoiDungIdColumn
        {
            get { return Schema.Columns[1]; }
        }
        
        
        
        public static TableSchema.TableColumn VaiTroIdColumn
        {
            get { return Schema.Columns[2]; }
        }
        
        
        
        #endregion
		#region Columns Struct
		public struct Columns
		{
			 public static string Id = @"Id";
			 public static string NguoiDungId = @"NguoiDungId";
			 public static string VaiTroId = @"VaiTroId";
						
		}
		#endregion
		
		#region Update PK Collections
		
        #endregion
    
        #region Deep Save
		
        #endregion
	}
}
