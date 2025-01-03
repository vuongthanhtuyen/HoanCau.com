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
	/// Strongly-typed collection for the VaiTroDanhMucLoaiQuyenChiTiet class.
	/// </summary>
    [Serializable]
	public partial class VaiTroDanhMucLoaiQuyenChiTietCollection : ActiveList<VaiTroDanhMucLoaiQuyenChiTiet, VaiTroDanhMucLoaiQuyenChiTietCollection>
	{	   
		public VaiTroDanhMucLoaiQuyenChiTietCollection() {}
        
        /// <summary>
		/// Filters an existing collection based on the set criteria. This is an in-memory filter
		/// Thanks to developingchris for this!
        /// </summary>
        /// <returns>VaiTroDanhMucLoaiQuyenChiTietCollection</returns>
		public VaiTroDanhMucLoaiQuyenChiTietCollection Filter()
        {
            for (int i = this.Count - 1; i > -1; i--)
            {
                VaiTroDanhMucLoaiQuyenChiTiet o = this[i];
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
	/// This is an ActiveRecord class which wraps the VaiTro_DanhMuc_LoaiQuyen_ChiTiet table.
	/// </summary>
	[Serializable]
	public partial class VaiTroDanhMucLoaiQuyenChiTiet : ActiveRecord<VaiTroDanhMucLoaiQuyenChiTiet>, IActiveRecord
	{
		#region .ctors and Default Settings
		
		public VaiTroDanhMucLoaiQuyenChiTiet()
		{
		  SetSQLProps();
		  InitSetDefaults();
		  MarkNew();
		}
		
		private void InitSetDefaults() { SetDefaults(); }
		
		public VaiTroDanhMucLoaiQuyenChiTiet(bool useDatabaseDefaults)
		{
			SetSQLProps();
			if(useDatabaseDefaults)
				ForceDefaults();
			MarkNew();
		}
        
		public VaiTroDanhMucLoaiQuyenChiTiet(object keyID)
		{
			SetSQLProps();
			InitSetDefaults();
			LoadByKey(keyID);
		}
		 
		public VaiTroDanhMucLoaiQuyenChiTiet(string columnName, object columnValue)
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
				TableSchema.Table schema = new TableSchema.Table("VaiTro_DanhMuc_LoaiQuyen_ChiTiet", TableType.Table, DataService.GetInstance("DataAcessProvider"));
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
				
				TableSchema.TableColumn colvarVaiTroId = new TableSchema.TableColumn(schema);
				colvarVaiTroId.ColumnName = "VaiTroId";
				colvarVaiTroId.DataType = DbType.Int32;
				colvarVaiTroId.MaxLength = 0;
				colvarVaiTroId.AutoIncrement = false;
				colvarVaiTroId.IsNullable = true;
				colvarVaiTroId.IsPrimaryKey = false;
				colvarVaiTroId.IsForeignKey = true;
				colvarVaiTroId.IsReadOnly = false;
				colvarVaiTroId.DefaultSetting = @"";
				
					colvarVaiTroId.ForeignKeyTableName = "VaiTro";
				schema.Columns.Add(colvarVaiTroId);
				
				TableSchema.TableColumn colvarLoaiQuyenId = new TableSchema.TableColumn(schema);
				colvarLoaiQuyenId.ColumnName = "LoaiQuyenId";
				colvarLoaiQuyenId.DataType = DbType.Int32;
				colvarLoaiQuyenId.MaxLength = 0;
				colvarLoaiQuyenId.AutoIncrement = false;
				colvarLoaiQuyenId.IsNullable = true;
				colvarLoaiQuyenId.IsPrimaryKey = false;
				colvarLoaiQuyenId.IsForeignKey = true;
				colvarLoaiQuyenId.IsReadOnly = false;
				colvarLoaiQuyenId.DefaultSetting = @"";
				
					colvarLoaiQuyenId.ForeignKeyTableName = "LoaiQuyen";
				schema.Columns.Add(colvarLoaiQuyenId);
				
				TableSchema.TableColumn colvarMenuId = new TableSchema.TableColumn(schema);
				colvarMenuId.ColumnName = "MenuId";
				colvarMenuId.DataType = DbType.Int32;
				colvarMenuId.MaxLength = 0;
				colvarMenuId.AutoIncrement = false;
				colvarMenuId.IsNullable = true;
				colvarMenuId.IsPrimaryKey = false;
				colvarMenuId.IsForeignKey = true;
				colvarMenuId.IsReadOnly = false;
				colvarMenuId.DefaultSetting = @"";
				
					colvarMenuId.ForeignKeyTableName = "MenuAdmin";
				schema.Columns.Add(colvarMenuId);
				
				BaseSchema = schema;
				//add this schema to the provider
				//so we can query it later
				DataService.Providers["DataAcessProvider"].AddSchema("VaiTro_DanhMuc_LoaiQuyen_ChiTiet",schema);
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
		  
		[XmlAttribute("VaiTroId")]
		[Bindable(true)]
		public int? VaiTroId 
		{
			get { return GetColumnValue<int?>(Columns.VaiTroId); }
			set { SetColumnValue(Columns.VaiTroId, value); }
		}
		  
		[XmlAttribute("LoaiQuyenId")]
		[Bindable(true)]
		public int? LoaiQuyenId 
		{
			get { return GetColumnValue<int?>(Columns.LoaiQuyenId); }
			set { SetColumnValue(Columns.LoaiQuyenId, value); }
		}
		  
		[XmlAttribute("MenuId")]
		[Bindable(true)]
		public int? MenuId 
		{
			get { return GetColumnValue<int?>(Columns.MenuId); }
			set { SetColumnValue(Columns.MenuId, value); }
		}
		
		#endregion
		
		
			
		
		#region ForeignKey Properties
		
		/// <summary>
		/// Returns a LoaiQuyen ActiveRecord object related to this VaiTroDanhMucLoaiQuyenChiTiet
		/// 
		/// </summary>
		public SweetCMS.DataAccess.LoaiQuyen LoaiQuyen
		{
			get { return SweetCMS.DataAccess.LoaiQuyen.FetchByID(this.LoaiQuyenId); }
			set { SetColumnValue("LoaiQuyenId", value.Id); }
		}
		
		
		/// <summary>
		/// Returns a MenuAdmin ActiveRecord object related to this VaiTroDanhMucLoaiQuyenChiTiet
		/// 
		/// </summary>
		public SweetCMS.DataAccess.MenuAdmin MenuAdmin
		{
			get { return SweetCMS.DataAccess.MenuAdmin.FetchByID(this.MenuId); }
			set { SetColumnValue("MenuId", value.Id); }
		}
		
		
		/// <summary>
		/// Returns a VaiTro ActiveRecord object related to this VaiTroDanhMucLoaiQuyenChiTiet
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
		public static void Insert(int? varVaiTroId,int? varLoaiQuyenId,int? varMenuId)
		{
			VaiTroDanhMucLoaiQuyenChiTiet item = new VaiTroDanhMucLoaiQuyenChiTiet();
			
			item.VaiTroId = varVaiTroId;
			
			item.LoaiQuyenId = varLoaiQuyenId;
			
			item.MenuId = varMenuId;
			
		
			if (System.Web.HttpContext.Current != null)
				item.Save(System.Web.HttpContext.Current.User.Identity.Name);
			else
				item.Save(System.Threading.Thread.CurrentPrincipal.Identity.Name);
		}
		
		/// <summary>
		/// Updates a record, can be used with the Object Data Source
		/// </summary>
		public static void Update(int varId,int? varVaiTroId,int? varLoaiQuyenId,int? varMenuId)
		{
			VaiTroDanhMucLoaiQuyenChiTiet item = new VaiTroDanhMucLoaiQuyenChiTiet();
			
				item.Id = varId;
			
				item.VaiTroId = varVaiTroId;
			
				item.LoaiQuyenId = varLoaiQuyenId;
			
				item.MenuId = varMenuId;
			
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
        
        
        
        public static TableSchema.TableColumn VaiTroIdColumn
        {
            get { return Schema.Columns[1]; }
        }
        
        
        
        public static TableSchema.TableColumn LoaiQuyenIdColumn
        {
            get { return Schema.Columns[2]; }
        }
        
        
        
        public static TableSchema.TableColumn MenuIdColumn
        {
            get { return Schema.Columns[3]; }
        }
        
        
        
        #endregion
		#region Columns Struct
		public struct Columns
		{
			 public static string Id = @"Id";
			 public static string VaiTroId = @"VaiTroId";
			 public static string LoaiQuyenId = @"LoaiQuyenId";
			 public static string MenuId = @"MenuId";
						
		}
		#endregion
		
		#region Update PK Collections
		
        #endregion
    
        #region Deep Save
		
        #endregion
	}
}
