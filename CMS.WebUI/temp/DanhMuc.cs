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
				colvarTen.MaxLength = 500;
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
				colvarDanhMucChaId.IsForeignKey = false;
				colvarDanhMucChaId.IsReadOnly = false;
				colvarDanhMucChaId.DefaultSetting = @"";
				colvarDanhMucChaId.ForeignKeyTableName = "";
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
				
				TableSchema.TableColumn colvarLangID = new TableSchema.TableColumn(schema);
				colvarLangID.ColumnName = "langID";
				colvarLangID.DataType = DbType.Int32;
				colvarLangID.MaxLength = 0;
				colvarLangID.AutoIncrement = false;
				colvarLangID.IsNullable = true;
				colvarLangID.IsPrimaryKey = false;
				colvarLangID.IsForeignKey = false;
				colvarLangID.IsReadOnly = false;
				
						colvarLangID.DefaultSetting = @"((1))";
				colvarLangID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarLangID);
				
				TableSchema.TableColumn colvarCreateDate = new TableSchema.TableColumn(schema);
				colvarCreateDate.ColumnName = "CreateDate";
				colvarCreateDate.DataType = DbType.DateTime;
				colvarCreateDate.MaxLength = 0;
				colvarCreateDate.AutoIncrement = false;
				colvarCreateDate.IsNullable = false;
				colvarCreateDate.IsPrimaryKey = false;
				colvarCreateDate.IsForeignKey = false;
				colvarCreateDate.IsReadOnly = false;
				
						colvarCreateDate.DefaultSetting = @"(getdate())";
				colvarCreateDate.ForeignKeyTableName = "";
				schema.Columns.Add(colvarCreateDate);
				
				TableSchema.TableColumn colvarUpdateDate = new TableSchema.TableColumn(schema);
				colvarUpdateDate.ColumnName = "UpdateDate";
				colvarUpdateDate.DataType = DbType.DateTime;
				colvarUpdateDate.MaxLength = 0;
				colvarUpdateDate.AutoIncrement = false;
				colvarUpdateDate.IsNullable = false;
				colvarUpdateDate.IsPrimaryKey = false;
				colvarUpdateDate.IsForeignKey = false;
				colvarUpdateDate.IsReadOnly = false;
				
						colvarUpdateDate.DefaultSetting = @"(getdate())";
				colvarUpdateDate.ForeignKeyTableName = "";
				schema.Columns.Add(colvarUpdateDate);
				
				TableSchema.TableColumn colvarCreateBy = new TableSchema.TableColumn(schema);
				colvarCreateBy.ColumnName = "CreateBy";
				colvarCreateBy.DataType = DbType.String;
				colvarCreateBy.MaxLength = 50;
				colvarCreateBy.AutoIncrement = false;
				colvarCreateBy.IsNullable = false;
				colvarCreateBy.IsPrimaryKey = false;
				colvarCreateBy.IsForeignKey = false;
				colvarCreateBy.IsReadOnly = false;
				
						colvarCreateBy.DefaultSetting = @"('Administrator')";
				colvarCreateBy.ForeignKeyTableName = "";
				schema.Columns.Add(colvarCreateBy);
				
				TableSchema.TableColumn colvarUpdateBy = new TableSchema.TableColumn(schema);
				colvarUpdateBy.ColumnName = "UpdateBy";
				colvarUpdateBy.DataType = DbType.String;
				colvarUpdateBy.MaxLength = 50;
				colvarUpdateBy.AutoIncrement = false;
				colvarUpdateBy.IsNullable = false;
				colvarUpdateBy.IsPrimaryKey = false;
				colvarUpdateBy.IsForeignKey = false;
				colvarUpdateBy.IsReadOnly = false;
				
						colvarUpdateBy.DefaultSetting = @"('Administrator')";
				colvarUpdateBy.ForeignKeyTableName = "";
				schema.Columns.Add(colvarUpdateBy);
				
				TableSchema.TableColumn colvarStatus = new TableSchema.TableColumn(schema);
				colvarStatus.ColumnName = "Status";
				colvarStatus.DataType = DbType.AnsiString;
				colvarStatus.MaxLength = 20;
				colvarStatus.AutoIncrement = false;
				colvarStatus.IsNullable = false;
				colvarStatus.IsPrimaryKey = false;
				colvarStatus.IsForeignKey = false;
				colvarStatus.IsReadOnly = false;
				
						colvarStatus.DefaultSetting = @"('Active')";
				colvarStatus.ForeignKeyTableName = "";
				schema.Columns.Add(colvarStatus);
				
				TableSchema.TableColumn colvarType = new TableSchema.TableColumn(schema);
				colvarType.ColumnName = "Type";
				colvarType.DataType = DbType.Int32;
				colvarType.MaxLength = 0;
				colvarType.AutoIncrement = false;
				colvarType.IsNullable = false;
				colvarType.IsPrimaryKey = false;
				colvarType.IsForeignKey = false;
				colvarType.IsReadOnly = false;
				
						colvarType.DefaultSetting = @"((1))";
				colvarType.ForeignKeyTableName = "";
				schema.Columns.Add(colvarType);
				
				TableSchema.TableColumn colvarThumbnailUrl = new TableSchema.TableColumn(schema);
				colvarThumbnailUrl.ColumnName = "ThumbnailUrl";
				colvarThumbnailUrl.DataType = DbType.AnsiString;
				colvarThumbnailUrl.MaxLength = 255;
				colvarThumbnailUrl.AutoIncrement = false;
				colvarThumbnailUrl.IsNullable = false;
				colvarThumbnailUrl.IsPrimaryKey = false;
				colvarThumbnailUrl.IsForeignKey = false;
				colvarThumbnailUrl.IsReadOnly = false;
				
						colvarThumbnailUrl.DefaultSetting = @"('')";
				colvarThumbnailUrl.ForeignKeyTableName = "";
				schema.Columns.Add(colvarThumbnailUrl);
				
				TableSchema.TableColumn colvarDisplayOrder = new TableSchema.TableColumn(schema);
				colvarDisplayOrder.ColumnName = "DisplayOrder";
				colvarDisplayOrder.DataType = DbType.Int32;
				colvarDisplayOrder.MaxLength = 0;
				colvarDisplayOrder.AutoIncrement = false;
				colvarDisplayOrder.IsNullable = false;
				colvarDisplayOrder.IsPrimaryKey = false;
				colvarDisplayOrder.IsForeignKey = false;
				colvarDisplayOrder.IsReadOnly = false;
				
						colvarDisplayOrder.DefaultSetting = @"((-1))";
				colvarDisplayOrder.ForeignKeyTableName = "";
				schema.Columns.Add(colvarDisplayOrder);
				
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
		  
		[XmlAttribute("LangID")]
		[Bindable(true)]
		public int? LangID 
		{
			get { return GetColumnValue<int?>(Columns.LangID); }
			set { SetColumnValue(Columns.LangID, value); }
		}
		  
		[XmlAttribute("CreateDate")]
		[Bindable(true)]
		public DateTime CreateDate 
		{
			get { return GetColumnValue<DateTime>(Columns.CreateDate); }
			set { SetColumnValue(Columns.CreateDate, value); }
		}
		  
		[XmlAttribute("UpdateDate")]
		[Bindable(true)]
		public DateTime UpdateDate 
		{
			get { return GetColumnValue<DateTime>(Columns.UpdateDate); }
			set { SetColumnValue(Columns.UpdateDate, value); }
		}
		  
		[XmlAttribute("CreateBy")]
		[Bindable(true)]
		public string CreateBy 
		{
			get { return GetColumnValue<string>(Columns.CreateBy); }
			set { SetColumnValue(Columns.CreateBy, value); }
		}
		  
		[XmlAttribute("UpdateBy")]
		[Bindable(true)]
		public string UpdateBy 
		{
			get { return GetColumnValue<string>(Columns.UpdateBy); }
			set { SetColumnValue(Columns.UpdateBy, value); }
		}
		  
		[XmlAttribute("Status")]
		[Bindable(true)]
		public string Status 
		{
			get { return GetColumnValue<string>(Columns.Status); }
			set { SetColumnValue(Columns.Status, value); }
		}
		  
		[XmlAttribute("Type")]
		[Bindable(true)]
		public int Type 
		{
			get { return GetColumnValue<int>(Columns.Type); }
			set { SetColumnValue(Columns.Type, value); }
		}
		  
		[XmlAttribute("ThumbnailUrl")]
		[Bindable(true)]
		public string ThumbnailUrl 
		{
			get { return GetColumnValue<string>(Columns.ThumbnailUrl); }
			set { SetColumnValue(Columns.ThumbnailUrl, value); }
		}
		  
		[XmlAttribute("DisplayOrder")]
		[Bindable(true)]
		public int DisplayOrder 
		{
			get { return GetColumnValue<int>(Columns.DisplayOrder); }
			set { SetColumnValue(Columns.DisplayOrder, value); }
		}
		
		#endregion
		
		
		#region PrimaryKey Methods		
		
        protected override void SetPrimaryKey(object oValue)
        {
            base.SetPrimaryKey(oValue);
            
            SetPKValues();
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
		private SweetCMS.DataAccess.VideoCollection colVideoRecords;
		public SweetCMS.DataAccess.VideoCollection VideoRecords()
		{
			if(colVideoRecords == null)
			{
				colVideoRecords = new SweetCMS.DataAccess.VideoCollection().Where(Video.Columns.CategoryId, Id).Load();
				colVideoRecords.ListChanged += new ListChangedEventHandler(colVideoRecords_ListChanged);
			}
			return colVideoRecords;
		}
				
		void colVideoRecords_ListChanged(object sender, ListChangedEventArgs e)
		{
            if (e.ListChangedType == ListChangedType.ItemAdded)
            {
		        // Set foreign key value
		        colVideoRecords[e.NewIndex].CategoryId = Id;
            }
		}
		#endregion
		
			
		
		//no foreign key tables defined (0)
		
		
		
		//no ManyToMany tables defined (0)
		
        
        
		#region ObjectDataSource support
		
		
		/// <summary>
		/// Inserts a record, can be used with the Object Data Source
		/// </summary>
		public static void Insert(string varTen,int? varDanhMucChaId,string varSlug,string varMoTa,int? varLangID,DateTime varCreateDate,DateTime varUpdateDate,string varCreateBy,string varUpdateBy,string varStatus,int varType,string varThumbnailUrl,int varDisplayOrder)
		{
			DanhMuc item = new DanhMuc();
			
			item.Ten = varTen;
			
			item.DanhMucChaId = varDanhMucChaId;
			
			item.Slug = varSlug;
			
			item.MoTa = varMoTa;
			
			item.LangID = varLangID;
			
			item.CreateDate = varCreateDate;
			
			item.UpdateDate = varUpdateDate;
			
			item.CreateBy = varCreateBy;
			
			item.UpdateBy = varUpdateBy;
			
			item.Status = varStatus;
			
			item.Type = varType;
			
			item.ThumbnailUrl = varThumbnailUrl;
			
			item.DisplayOrder = varDisplayOrder;
			
		
			if (System.Web.HttpContext.Current != null)
				item.Save(System.Web.HttpContext.Current.User.Identity.Name);
			else
				item.Save(System.Threading.Thread.CurrentPrincipal.Identity.Name);
		}
		
		/// <summary>
		/// Updates a record, can be used with the Object Data Source
		/// </summary>
		public static void Update(int varId,string varTen,int? varDanhMucChaId,string varSlug,string varMoTa,int? varLangID,DateTime varCreateDate,DateTime varUpdateDate,string varCreateBy,string varUpdateBy,string varStatus,int varType,string varThumbnailUrl,int varDisplayOrder)
		{
			DanhMuc item = new DanhMuc();
			
				item.Id = varId;
			
				item.Ten = varTen;
			
				item.DanhMucChaId = varDanhMucChaId;
			
				item.Slug = varSlug;
			
				item.MoTa = varMoTa;
			
				item.LangID = varLangID;
			
				item.CreateDate = varCreateDate;
			
				item.UpdateDate = varUpdateDate;
			
				item.CreateBy = varCreateBy;
			
				item.UpdateBy = varUpdateBy;
			
				item.Status = varStatus;
			
				item.Type = varType;
			
				item.ThumbnailUrl = varThumbnailUrl;
			
				item.DisplayOrder = varDisplayOrder;
			
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
        
        
        
        public static TableSchema.TableColumn LangIDColumn
        {
            get { return Schema.Columns[5]; }
        }
        
        
        
        public static TableSchema.TableColumn CreateDateColumn
        {
            get { return Schema.Columns[6]; }
        }
        
        
        
        public static TableSchema.TableColumn UpdateDateColumn
        {
            get { return Schema.Columns[7]; }
        }
        
        
        
        public static TableSchema.TableColumn CreateByColumn
        {
            get { return Schema.Columns[8]; }
        }
        
        
        
        public static TableSchema.TableColumn UpdateByColumn
        {
            get { return Schema.Columns[9]; }
        }
        
        
        
        public static TableSchema.TableColumn StatusColumn
        {
            get { return Schema.Columns[10]; }
        }
        
        
        
        public static TableSchema.TableColumn TypeColumn
        {
            get { return Schema.Columns[11]; }
        }
        
        
        
        public static TableSchema.TableColumn ThumbnailUrlColumn
        {
            get { return Schema.Columns[12]; }
        }
        
        
        
        public static TableSchema.TableColumn DisplayOrderColumn
        {
            get { return Schema.Columns[13]; }
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
			 public static string LangID = @"langID";
			 public static string CreateDate = @"CreateDate";
			 public static string UpdateDate = @"UpdateDate";
			 public static string CreateBy = @"CreateBy";
			 public static string UpdateBy = @"UpdateBy";
			 public static string Status = @"Status";
			 public static string Type = @"Type";
			 public static string ThumbnailUrl = @"ThumbnailUrl";
			 public static string DisplayOrder = @"DisplayOrder";
						
		}
		#endregion
		
		#region Update PK Collections
		
        public void SetPKValues()
        {
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
		
                if (colVideoRecords != null)
                {
                    foreach (SweetCMS.DataAccess.Video item in colVideoRecords)
                    {
                        if (item.CategoryId == null ||item.CategoryId != Id)
                        {
                            item.CategoryId = Id;
                        }
                    }
               }
		}
        #endregion
    
        #region Deep Save
		
        public void DeepSave()
        {
            Save();
            
                if (colNhomBaiVietRecords != null)
                {
                    colNhomBaiVietRecords.SaveAll();
               }
		
                if (colVideoRecords != null)
                {
                    colVideoRecords.SaveAll();
               }
		}
        #endregion
	}
}
