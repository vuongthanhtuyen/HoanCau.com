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
	/// Strongly-typed collection for the DanhMuc class.
	/// </summary>
    [Serializable]
	public partial class DanhMucCollection : ActiveList<DanhMuc, DanhMucCollection>
	{	   
		public DanhMucCollection() {}
        
        /// <summary>
		/// Filters an existing collection based on the set criteria. This is an in-memory filter
		/// Thanks to developingchris for this!
        /// </summary>
        /// <returns>DanhMucCollection</returns>
		public DanhMucCollection Filter()
        {
            for (int i = this.Count - 1; i > -1; i--)
            {
                DanhMuc o = this[i];
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
	/// This is an ActiveRecord class which wraps the DanhMuc table.
	/// </summary>
	[Serializable]
	public partial class DanhMuc : ActiveRecord<DanhMuc>, IActiveRecord
	{
		#region .ctors and Default Settings
		
		public DanhMuc()
		{
		  SetSQLProps();
		  InitSetDefaults();
		  MarkNew();
		}
		
		private void InitSetDefaults() { SetDefaults(); }
		
		public DanhMuc(bool useDatabaseDefaults)
		{
			SetSQLProps();
			if(useDatabaseDefaults)
				ForceDefaults();
			MarkNew();
		}
        
		public DanhMuc(object keyID)
		{
			SetSQLProps();
			InitSetDefaults();
			LoadByKey(keyID);
		}
		 
		public DanhMuc(string columnName, object columnValue)
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
				TableSchema.Table schema = new TableSchema.Table("DanhMuc", TableType.Table, DataService.GetInstance("DataAcessProvider"));
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
				
				TableSchema.TableColumn colvarTen = new TableSchema.TableColumn(schema);
				colvarTen.ColumnName = "Ten";
				colvarTen.DataType = DbType.String;
				colvarTen.MaxLength = 50;
				colvarTen.AutoIncrement = false;
				colvarTen.IsNullable = true;
				colvarTen.IsPrimaryKey = false;
				colvarTen.IsForeignKey = false;
				colvarTen.IsReadOnly = false;
				colvarTen.DefaultSetting = @"";
				colvarTen.ForeignKeyTableName = "";
				schema.Columns.Add(colvarTen);
				
				TableSchema.TableColumn colvarDanhMucChaId = new TableSchema.TableColumn(schema);
				colvarDanhMucChaId.ColumnName = "DanhMucChaId";
				colvarDanhMucChaId.DataType = DbType.Int32;
				colvarDanhMucChaId.MaxLength = 0;
				colvarDanhMucChaId.AutoIncrement = false;
				colvarDanhMucChaId.IsNullable = true;
				colvarDanhMucChaId.IsPrimaryKey = false;
				colvarDanhMucChaId.IsForeignKey = true;
				colvarDanhMucChaId.IsReadOnly = false;
				colvarDanhMucChaId.DefaultSetting = @"";
				
					colvarDanhMucChaId.ForeignKeyTableName = "DanhMuc";
				schema.Columns.Add(colvarDanhMucChaId);
				
				TableSchema.TableColumn colvarSlug = new TableSchema.TableColumn(schema);
				colvarSlug.ColumnName = "Slug";
				colvarSlug.DataType = DbType.AnsiString;
				colvarSlug.MaxLength = 500;
				colvarSlug.AutoIncrement = false;
				colvarSlug.IsNullable = true;
				colvarSlug.IsPrimaryKey = false;
				colvarSlug.IsForeignKey = false;
				colvarSlug.IsReadOnly = false;
				colvarSlug.DefaultSetting = @"";
				colvarSlug.ForeignKeyTableName = "";
				schema.Columns.Add(colvarSlug);
				
				TableSchema.TableColumn colvarMoTa = new TableSchema.TableColumn(schema);
				colvarMoTa.ColumnName = "MoTa";
				colvarMoTa.DataType = DbType.String;
				colvarMoTa.MaxLength = 4000;
				colvarMoTa.AutoIncrement = false;
				colvarMoTa.IsNullable = true;
				colvarMoTa.IsPrimaryKey = false;
				colvarMoTa.IsForeignKey = false;
				colvarMoTa.IsReadOnly = false;
				colvarMoTa.DefaultSetting = @"";
				colvarMoTa.ForeignKeyTableName = "";
				schema.Columns.Add(colvarMoTa);
				
				BaseSchema = schema;
				//add this schema to the provider
				//so we can query it later
				DataService.Providers["DataAcessProvider"].AddSchema("DanhMuc",schema);
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
		  
		[XmlAttribute("Ten")]
		[Bindable(true)]
		public string Ten 
		{
			get { return GetColumnValue<string>(Columns.Ten); }
			set { SetColumnValue(Columns.Ten, value); }
		}
		  
		[XmlAttribute("DanhMucChaId")]
		[Bindable(true)]
		public int? DanhMucChaId 
		{
			get { return GetColumnValue<int?>(Columns.DanhMucChaId); }
			set { SetColumnValue(Columns.DanhMucChaId, value); }
		}
		  
		[XmlAttribute("Slug")]
		[Bindable(true)]
		public string Slug 
		{
			get { return GetColumnValue<string>(Columns.Slug); }
			set { SetColumnValue(Columns.Slug, value); }
		}
		  
		[XmlAttribute("MoTa")]
		[Bindable(true)]
		public string MoTa 
		{
			get { return GetColumnValue<string>(Columns.MoTa); }
			set { SetColumnValue(Columns.MoTa, value); }
		}
		
		#endregion
		
		
		#region PrimaryKey Methods		
		
        protected override void SetPrimaryKey(object oValue)
        {
            base.SetPrimaryKey(oValue);
            
            SetPKValues();
        }
        
		
		private SweetCMS.DataAccess.DanhMucCollection colChildDanhMucRecords;
		public SweetCMS.DataAccess.DanhMucCollection ChildDanhMucRecords()
		{
			if(colChildDanhMucRecords == null)
			{
				colChildDanhMucRecords = new SweetCMS.DataAccess.DanhMucCollection().Where(DanhMuc.Columns.DanhMucChaId, Id).Load();
				colChildDanhMucRecords.ListChanged += new ListChangedEventHandler(colChildDanhMucRecords_ListChanged);
			}
			return colChildDanhMucRecords;
		}
				
		void colChildDanhMucRecords_ListChanged(object sender, ListChangedEventArgs e)
		{
            if (e.ListChangedType == ListChangedType.ItemAdded)
            {
		        // Set foreign key value
		        colChildDanhMucRecords[e.NewIndex].DanhMucChaId = Id;
            }
		}
		private SweetCMS.DataAccess.NhomBaiVietCollection colNhomBaiVietRecords;
		public SweetCMS.DataAccess.NhomBaiVietCollection NhomBaiVietRecords()
		{
			if(colNhomBaiVietRecords == null)
			{
				colNhomBaiVietRecords = new SweetCMS.DataAccess.NhomBaiVietCollection().Where(NhomBaiViet.Columns.DanhmucId, Id).Load();
				colNhomBaiVietRecords.ListChanged += new ListChangedEventHandler(colNhomBaiVietRecords_ListChanged);
			}
			return colNhomBaiVietRecords;
		}
				
		void colNhomBaiVietRecords_ListChanged(object sender, ListChangedEventArgs e)
		{
            if (e.ListChangedType == ListChangedType.ItemAdded)
            {
		        // Set foreign key value
		        colNhomBaiVietRecords[e.NewIndex].DanhmucId = Id;
            }
		}
		#endregion
		
			
		
		#region ForeignKey Properties
		
		/// <summary>
		/// Returns a DanhMuc ActiveRecord object related to this DanhMuc
		/// 
		/// </summary>
		public SweetCMS.DataAccess.DanhMuc ParentDanhMuc
		{
			get { return SweetCMS.DataAccess.DanhMuc.FetchByID(this.DanhMucChaId); }
			set { SetColumnValue("DanhMucChaId", value.Id); }
		}
		
		
		#endregion
		
		
		
		//no ManyToMany tables defined (0)
		
        
        
		#region ObjectDataSource support
		
		
		/// <summary>
		/// Inserts a record, can be used with the Object Data Source
		/// </summary>
		public static void Insert(string varTen,int? varDanhMucChaId,string varSlug,string varMoTa)
		{
			DanhMuc item = new DanhMuc();
			
			item.Ten = varTen;
			
			item.DanhMucChaId = varDanhMucChaId;
			
			item.Slug = varSlug;
			
			item.MoTa = varMoTa;
			
		
			if (System.Web.HttpContext.Current != null)
				item.Save(System.Web.HttpContext.Current.User.Identity.Name);
			else
				item.Save(System.Threading.Thread.CurrentPrincipal.Identity.Name);
		}
		
		/// <summary>
		/// Updates a record, can be used with the Object Data Source
		/// </summary>
		public static void Update(int varId,string varTen,int? varDanhMucChaId,string varSlug,string varMoTa)
		{
			DanhMuc item = new DanhMuc();
			
				item.Id = varId;
			
				item.Ten = varTen;
			
				item.DanhMucChaId = varDanhMucChaId;
			
				item.Slug = varSlug;
			
				item.MoTa = varMoTa;
			
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
        
        
        
        public static TableSchema.TableColumn TenColumn
        {
            get { return Schema.Columns[1]; }
        }
        
        
        
        public static TableSchema.TableColumn DanhMucChaIdColumn
        {
            get { return Schema.Columns[2]; }
        }
        
        
        
        public static TableSchema.TableColumn SlugColumn
        {
            get { return Schema.Columns[3]; }
        }
        
        
        
        public static TableSchema.TableColumn MoTaColumn
        {
            get { return Schema.Columns[4]; }
        }
        
        
        
        #endregion
		#region Columns Struct
		public struct Columns
		{
			 public static string Id = @"Id";
			 public static string Ten = @"Ten";
			 public static string DanhMucChaId = @"DanhMucChaId";
			 public static string Slug = @"Slug";
			 public static string MoTa = @"MoTa";
						
		}
		#endregion
		
		#region Update PK Collections
		
        public void SetPKValues()
        {
                if (colChildDanhMucRecords != null)
                {
                    foreach (SweetCMS.DataAccess.DanhMuc item in colChildDanhMucRecords)
                    {
                        if (item.DanhMucChaId == null ||item.DanhMucChaId != Id)
                        {
                            item.DanhMucChaId = Id;
                        }
                    }
               }
		
                if (colNhomBaiVietRecords != null)
                {
                    foreach (SweetCMS.DataAccess.NhomBaiViet item in colNhomBaiVietRecords)
                    {
                        if (item.DanhmucId != Id)
                        {
                            item.DanhmucId = Id;
                        }
                    }
               }
		}
        #endregion
    
        #region Deep Save
		
        public void DeepSave()
        {
            Save();
            
                if (colChildDanhMucRecords != null)
                {
                    colChildDanhMucRecords.SaveAll();
               }
		
                if (colNhomBaiVietRecords != null)
                {
                    colNhomBaiVietRecords.SaveAll();
               }
		}
        #endregion
	}
}
