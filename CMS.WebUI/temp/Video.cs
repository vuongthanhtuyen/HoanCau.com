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
	/// Strongly-typed collection for the Video class.
	/// </summary>
    [Serializable]
	public partial class VideoCollection : ActiveList<Video, VideoCollection>
	{	   
		public VideoCollection() {}
        
        /// <summary>
		/// Filters an existing collection based on the set criteria. This is an in-memory filter
		/// Thanks to developingchris for this!
        /// </summary>
        /// <returns>VideoCollection</returns>
		public VideoCollection Filter()
        {
            for (int i = this.Count - 1; i > -1; i--)
            {
                Video o = this[i];
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
	/// This is an ActiveRecord class which wraps the Video table.
	/// </summary>
	[Serializable]
	public partial class Video : ActiveRecord<Video>, IActiveRecord
	{
		#region .ctors and Default Settings
		
		public Video()
		{
		  SetSQLProps();
		  InitSetDefaults();
		  MarkNew();
		}
		
		private void InitSetDefaults() { SetDefaults(); }
		
		public Video(bool useDatabaseDefaults)
		{
			SetSQLProps();
			if(useDatabaseDefaults)
				ForceDefaults();
			MarkNew();
		}
        
		public Video(object keyID)
		{
			SetSQLProps();
			InitSetDefaults();
			LoadByKey(keyID);
		}
		 
		public Video(string columnName, object columnValue)
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
				TableSchema.Table schema = new TableSchema.Table("Video", TableType.Table, DataService.GetInstance("DataAcessProvider"));
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
				
				TableSchema.TableColumn colvarThumbnailUrl = new TableSchema.TableColumn(schema);
				colvarThumbnailUrl.ColumnName = "ThumbnailUrl";
				colvarThumbnailUrl.DataType = DbType.String;
				colvarThumbnailUrl.MaxLength = 255;
				colvarThumbnailUrl.AutoIncrement = false;
				colvarThumbnailUrl.IsNullable = false;
				colvarThumbnailUrl.IsPrimaryKey = false;
				colvarThumbnailUrl.IsForeignKey = false;
				colvarThumbnailUrl.IsReadOnly = false;
				
						colvarThumbnailUrl.DefaultSetting = @"('')";
				colvarThumbnailUrl.ForeignKeyTableName = "";
				schema.Columns.Add(colvarThumbnailUrl);
				
				TableSchema.TableColumn colvarTitle = new TableSchema.TableColumn(schema);
				colvarTitle.ColumnName = "Title";
				colvarTitle.DataType = DbType.String;
				colvarTitle.MaxLength = 500;
				colvarTitle.AutoIncrement = false;
				colvarTitle.IsNullable = false;
				colvarTitle.IsPrimaryKey = false;
				colvarTitle.IsForeignKey = false;
				colvarTitle.IsReadOnly = false;
				
						colvarTitle.DefaultSetting = @"('')";
				colvarTitle.ForeignKeyTableName = "";
				schema.Columns.Add(colvarTitle);
				
				TableSchema.TableColumn colvarSlugUrl = new TableSchema.TableColumn(schema);
				colvarSlugUrl.ColumnName = "SlugUrl";
				colvarSlugUrl.DataType = DbType.String;
				colvarSlugUrl.MaxLength = 500;
				colvarSlugUrl.AutoIncrement = false;
				colvarSlugUrl.IsNullable = false;
				colvarSlugUrl.IsPrimaryKey = false;
				colvarSlugUrl.IsForeignKey = false;
				colvarSlugUrl.IsReadOnly = false;
				colvarSlugUrl.DefaultSetting = @"";
				colvarSlugUrl.ForeignKeyTableName = "";
				schema.Columns.Add(colvarSlugUrl);
				
				TableSchema.TableColumn colvarVideoLink = new TableSchema.TableColumn(schema);
				colvarVideoLink.ColumnName = "VideoLink";
				colvarVideoLink.DataType = DbType.String;
				colvarVideoLink.MaxLength = 255;
				colvarVideoLink.AutoIncrement = false;
				colvarVideoLink.IsNullable = false;
				colvarVideoLink.IsPrimaryKey = false;
				colvarVideoLink.IsForeignKey = false;
				colvarVideoLink.IsReadOnly = false;
				
						colvarVideoLink.DefaultSetting = @"('')";
				colvarVideoLink.ForeignKeyTableName = "";
				schema.Columns.Add(colvarVideoLink);
				
				TableSchema.TableColumn colvarSummary = new TableSchema.TableColumn(schema);
				colvarSummary.ColumnName = "Summary";
				colvarSummary.DataType = DbType.String;
				colvarSummary.MaxLength = 1000;
				colvarSummary.AutoIncrement = false;
				colvarSummary.IsNullable = false;
				colvarSummary.IsPrimaryKey = false;
				colvarSummary.IsForeignKey = false;
				colvarSummary.IsReadOnly = false;
				
						colvarSummary.DefaultSetting = @"('')";
				colvarSummary.ForeignKeyTableName = "";
				schema.Columns.Add(colvarSummary);
				
				TableSchema.TableColumn colvarDisplayOrder = new TableSchema.TableColumn(schema);
				colvarDisplayOrder.ColumnName = "DisplayOrder";
				colvarDisplayOrder.DataType = DbType.Int32;
				colvarDisplayOrder.MaxLength = 0;
				colvarDisplayOrder.AutoIncrement = false;
				colvarDisplayOrder.IsNullable = false;
				colvarDisplayOrder.IsPrimaryKey = false;
				colvarDisplayOrder.IsForeignKey = false;
				colvarDisplayOrder.IsReadOnly = false;
				
						colvarDisplayOrder.DefaultSetting = @"((0))";
				colvarDisplayOrder.ForeignKeyTableName = "";
				schema.Columns.Add(colvarDisplayOrder);
				
				TableSchema.TableColumn colvarCategoryId = new TableSchema.TableColumn(schema);
				colvarCategoryId.ColumnName = "CategoryId";
				colvarCategoryId.DataType = DbType.Int32;
				colvarCategoryId.MaxLength = 0;
				colvarCategoryId.AutoIncrement = false;
				colvarCategoryId.IsNullable = true;
				colvarCategoryId.IsPrimaryKey = false;
				colvarCategoryId.IsForeignKey = true;
				colvarCategoryId.IsReadOnly = false;
				colvarCategoryId.DefaultSetting = @"";
				
					colvarCategoryId.ForeignKeyTableName = "DanhMuc";
				schema.Columns.Add(colvarCategoryId);
				
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
				
				TableSchema.TableColumn colvarCreateBy = new TableSchema.TableColumn(schema);
				colvarCreateBy.ColumnName = "CreateBy";
				colvarCreateBy.DataType = DbType.String;
				colvarCreateBy.MaxLength = 50;
				colvarCreateBy.AutoIncrement = false;
				colvarCreateBy.IsNullable = false;
				colvarCreateBy.IsPrimaryKey = false;
				colvarCreateBy.IsForeignKey = false;
				colvarCreateBy.IsReadOnly = false;
				colvarCreateBy.DefaultSetting = @"";
				colvarCreateBy.ForeignKeyTableName = "";
				schema.Columns.Add(colvarCreateBy);
				
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
				
				TableSchema.TableColumn colvarUpdateBy = new TableSchema.TableColumn(schema);
				colvarUpdateBy.ColumnName = "UpdateBy";
				colvarUpdateBy.DataType = DbType.String;
				colvarUpdateBy.MaxLength = 50;
				colvarUpdateBy.AutoIncrement = false;
				colvarUpdateBy.IsNullable = false;
				colvarUpdateBy.IsPrimaryKey = false;
				colvarUpdateBy.IsForeignKey = false;
				colvarUpdateBy.IsReadOnly = false;
				colvarUpdateBy.DefaultSetting = @"";
				colvarUpdateBy.ForeignKeyTableName = "";
				schema.Columns.Add(colvarUpdateBy);
				
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
				
				BaseSchema = schema;
				//add this schema to the provider
				//so we can query it later
				DataService.Providers["DataAcessProvider"].AddSchema("Video",schema);
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
		  
		[XmlAttribute("ThumbnailUrl")]
		[Bindable(true)]
		public string ThumbnailUrl 
		{
			get { return GetColumnValue<string>(Columns.ThumbnailUrl); }
			set { SetColumnValue(Columns.ThumbnailUrl, value); }
		}
		  
		[XmlAttribute("Title")]
		[Bindable(true)]
		public string Title 
		{
			get { return GetColumnValue<string>(Columns.Title); }
			set { SetColumnValue(Columns.Title, value); }
		}
		  
		[XmlAttribute("SlugUrl")]
		[Bindable(true)]
		public string SlugUrl 
		{
			get { return GetColumnValue<string>(Columns.SlugUrl); }
			set { SetColumnValue(Columns.SlugUrl, value); }
		}
		  
		[XmlAttribute("VideoLink")]
		[Bindable(true)]
		public string VideoLink 
		{
			get { return GetColumnValue<string>(Columns.VideoLink); }
			set { SetColumnValue(Columns.VideoLink, value); }
		}
		  
		[XmlAttribute("Summary")]
		[Bindable(true)]
		public string Summary 
		{
			get { return GetColumnValue<string>(Columns.Summary); }
			set { SetColumnValue(Columns.Summary, value); }
		}
		  
		[XmlAttribute("DisplayOrder")]
		[Bindable(true)]
		public int DisplayOrder 
		{
			get { return GetColumnValue<int>(Columns.DisplayOrder); }
			set { SetColumnValue(Columns.DisplayOrder, value); }
		}
		  
		[XmlAttribute("CategoryId")]
		[Bindable(true)]
		public int? CategoryId 
		{
			get { return GetColumnValue<int?>(Columns.CategoryId); }
			set { SetColumnValue(Columns.CategoryId, value); }
		}
		  
		[XmlAttribute("Status")]
		[Bindable(true)]
		public string Status 
		{
			get { return GetColumnValue<string>(Columns.Status); }
			set { SetColumnValue(Columns.Status, value); }
		}
		  
		[XmlAttribute("CreateBy")]
		[Bindable(true)]
		public string CreateBy 
		{
			get { return GetColumnValue<string>(Columns.CreateBy); }
			set { SetColumnValue(Columns.CreateBy, value); }
		}
		  
		[XmlAttribute("CreateDate")]
		[Bindable(true)]
		public DateTime CreateDate 
		{
			get { return GetColumnValue<DateTime>(Columns.CreateDate); }
			set { SetColumnValue(Columns.CreateDate, value); }
		}
		  
		[XmlAttribute("UpdateBy")]
		[Bindable(true)]
		public string UpdateBy 
		{
			get { return GetColumnValue<string>(Columns.UpdateBy); }
			set { SetColumnValue(Columns.UpdateBy, value); }
		}
		  
		[XmlAttribute("UpdateDate")]
		[Bindable(true)]
		public DateTime UpdateDate 
		{
			get { return GetColumnValue<DateTime>(Columns.UpdateDate); }
			set { SetColumnValue(Columns.UpdateDate, value); }
		}
		
		#endregion
		
		
			
		
		#region ForeignKey Properties
		
		/// <summary>
		/// Returns a DanhMuc ActiveRecord object related to this Video
		/// 
		/// </summary>
		public SweetCMS.DataAccess.DanhMuc DanhMuc
		{
			get { return SweetCMS.DataAccess.DanhMuc.FetchByID(this.CategoryId); }
			set { SetColumnValue("CategoryId", value.Id); }
		}
		
		
		#endregion
		
		
		
		//no ManyToMany tables defined (0)
		
        
        
		#region ObjectDataSource support
		
		
		/// <summary>
		/// Inserts a record, can be used with the Object Data Source
		/// </summary>
		public static void Insert(string varThumbnailUrl,string varTitle,string varSlugUrl,string varVideoLink,string varSummary,int varDisplayOrder,int? varCategoryId,string varStatus,string varCreateBy,DateTime varCreateDate,string varUpdateBy,DateTime varUpdateDate)
		{
			Video item = new Video();
			
			item.ThumbnailUrl = varThumbnailUrl;
			
			item.Title = varTitle;
			
			item.SlugUrl = varSlugUrl;
			
			item.VideoLink = varVideoLink;
			
			item.Summary = varSummary;
			
			item.DisplayOrder = varDisplayOrder;
			
			item.CategoryId = varCategoryId;
			
			item.Status = varStatus;
			
			item.CreateBy = varCreateBy;
			
			item.CreateDate = varCreateDate;
			
			item.UpdateBy = varUpdateBy;
			
			item.UpdateDate = varUpdateDate;
			
		
			if (System.Web.HttpContext.Current != null)
				item.Save(System.Web.HttpContext.Current.User.Identity.Name);
			else
				item.Save(System.Threading.Thread.CurrentPrincipal.Identity.Name);
		}
		
		/// <summary>
		/// Updates a record, can be used with the Object Data Source
		/// </summary>
		public static void Update(int varId,string varThumbnailUrl,string varTitle,string varSlugUrl,string varVideoLink,string varSummary,int varDisplayOrder,int? varCategoryId,string varStatus,string varCreateBy,DateTime varCreateDate,string varUpdateBy,DateTime varUpdateDate)
		{
			Video item = new Video();
			
				item.Id = varId;
			
				item.ThumbnailUrl = varThumbnailUrl;
			
				item.Title = varTitle;
			
				item.SlugUrl = varSlugUrl;
			
				item.VideoLink = varVideoLink;
			
				item.Summary = varSummary;
			
				item.DisplayOrder = varDisplayOrder;
			
				item.CategoryId = varCategoryId;
			
				item.Status = varStatus;
			
				item.CreateBy = varCreateBy;
			
				item.CreateDate = varCreateDate;
			
				item.UpdateBy = varUpdateBy;
			
				item.UpdateDate = varUpdateDate;
			
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
        
        
        
        public static TableSchema.TableColumn ThumbnailUrlColumn
        {
            get { return Schema.Columns[1]; }
        }
        
        
        
        public static TableSchema.TableColumn TitleColumn
        {
            get { return Schema.Columns[2]; }
        }
        
        
        
        public static TableSchema.TableColumn SlugUrlColumn
        {
            get { return Schema.Columns[3]; }
        }
        
        
        
        public static TableSchema.TableColumn VideoLinkColumn
        {
            get { return Schema.Columns[4]; }
        }
        
        
        
        public static TableSchema.TableColumn SummaryColumn
        {
            get { return Schema.Columns[5]; }
        }
        
        
        
        public static TableSchema.TableColumn DisplayOrderColumn
        {
            get { return Schema.Columns[6]; }
        }
        
        
        
        public static TableSchema.TableColumn CategoryIdColumn
        {
            get { return Schema.Columns[7]; }
        }
        
        
        
        public static TableSchema.TableColumn StatusColumn
        {
            get { return Schema.Columns[8]; }
        }
        
        
        
        public static TableSchema.TableColumn CreateByColumn
        {
            get { return Schema.Columns[9]; }
        }
        
        
        
        public static TableSchema.TableColumn CreateDateColumn
        {
            get { return Schema.Columns[10]; }
        }
        
        
        
        public static TableSchema.TableColumn UpdateByColumn
        {
            get { return Schema.Columns[11]; }
        }
        
        
        
        public static TableSchema.TableColumn UpdateDateColumn
        {
            get { return Schema.Columns[12]; }
        }
        
        
        
        #endregion
		#region Columns Struct
		public struct Columns
		{
			 public static string Id = @"Id";
			 public static string ThumbnailUrl = @"ThumbnailUrl";
			 public static string Title = @"Title";
			 public static string SlugUrl = @"SlugUrl";
			 public static string VideoLink = @"VideoLink";
			 public static string Summary = @"Summary";
			 public static string DisplayOrder = @"DisplayOrder";
			 public static string CategoryId = @"CategoryId";
			 public static string Status = @"Status";
			 public static string CreateBy = @"CreateBy";
			 public static string CreateDate = @"CreateDate";
			 public static string UpdateBy = @"UpdateBy";
			 public static string UpdateDate = @"UpdateDate";
						
		}
		#endregion
		
		#region Update PK Collections
		
        #endregion
    
        #region Deep Save
		
        #endregion
	}
}
