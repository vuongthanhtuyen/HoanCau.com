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
	/// Strongly-typed collection for the LienHe class.
	/// </summary>
    [Serializable]
	public partial class LienHeCollection : ActiveList<LienHe, LienHeCollection>
	{	   
		public LienHeCollection() {}
        
        /// <summary>
		/// Filters an existing collection based on the set criteria. This is an in-memory filter
		/// Thanks to developingchris for this!
        /// </summary>
        /// <returns>LienHeCollection</returns>
		public LienHeCollection Filter()
        {
            for (int i = this.Count - 1; i > -1; i--)
            {
                LienHe o = this[i];
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
	/// This is an ActiveRecord class which wraps the LienHe table.
	/// </summary>
	[Serializable]
	public partial class LienHe : ActiveRecord<LienHe>, IActiveRecord
	{
		#region .ctors and Default Settings
		
		public LienHe()
		{
		  SetSQLProps();
		  InitSetDefaults();
		  MarkNew();
		}
		
		private void InitSetDefaults() { SetDefaults(); }
		
		public LienHe(bool useDatabaseDefaults)
		{
			SetSQLProps();
			if(useDatabaseDefaults)
				ForceDefaults();
			MarkNew();
		}
        
		public LienHe(object keyID)
		{
			SetSQLProps();
			InitSetDefaults();
			LoadByKey(keyID);
		}
		 
		public LienHe(string columnName, object columnValue)
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
				TableSchema.Table schema = new TableSchema.Table("LienHe", TableType.Table, DataService.GetInstance("DataAcessProvider"));
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
				
				TableSchema.TableColumn colvarHoVaTen = new TableSchema.TableColumn(schema);
				colvarHoVaTen.ColumnName = "HoVaTen";
				colvarHoVaTen.DataType = DbType.String;
				colvarHoVaTen.MaxLength = 200;
				colvarHoVaTen.AutoIncrement = false;
				colvarHoVaTen.IsNullable = false;
				colvarHoVaTen.IsPrimaryKey = false;
				colvarHoVaTen.IsForeignKey = false;
				colvarHoVaTen.IsReadOnly = false;
				colvarHoVaTen.DefaultSetting = @"";
				colvarHoVaTen.ForeignKeyTableName = "";
				schema.Columns.Add(colvarHoVaTen);
				
				TableSchema.TableColumn colvarEmail = new TableSchema.TableColumn(schema);
				colvarEmail.ColumnName = "Email";
				colvarEmail.DataType = DbType.AnsiString;
				colvarEmail.MaxLength = 200;
				colvarEmail.AutoIncrement = false;
				colvarEmail.IsNullable = false;
				colvarEmail.IsPrimaryKey = false;
				colvarEmail.IsForeignKey = false;
				colvarEmail.IsReadOnly = false;
				colvarEmail.DefaultSetting = @"";
				colvarEmail.ForeignKeyTableName = "";
				schema.Columns.Add(colvarEmail);
				
				TableSchema.TableColumn colvarSoDienThoai = new TableSchema.TableColumn(schema);
				colvarSoDienThoai.ColumnName = "SoDienThoai";
				colvarSoDienThoai.DataType = DbType.AnsiString;
				colvarSoDienThoai.MaxLength = 50;
				colvarSoDienThoai.AutoIncrement = false;
				colvarSoDienThoai.IsNullable = true;
				colvarSoDienThoai.IsPrimaryKey = false;
				colvarSoDienThoai.IsForeignKey = false;
				colvarSoDienThoai.IsReadOnly = false;
				colvarSoDienThoai.DefaultSetting = @"";
				colvarSoDienThoai.ForeignKeyTableName = "";
				schema.Columns.Add(colvarSoDienThoai);
				
				TableSchema.TableColumn colvarChuDe = new TableSchema.TableColumn(schema);
				colvarChuDe.ColumnName = "ChuDe";
				colvarChuDe.DataType = DbType.String;
				colvarChuDe.MaxLength = 400;
				colvarChuDe.AutoIncrement = false;
				colvarChuDe.IsNullable = true;
				colvarChuDe.IsPrimaryKey = false;
				colvarChuDe.IsForeignKey = false;
				colvarChuDe.IsReadOnly = false;
				colvarChuDe.DefaultSetting = @"";
				colvarChuDe.ForeignKeyTableName = "";
				schema.Columns.Add(colvarChuDe);
				
				TableSchema.TableColumn colvarTinNhan = new TableSchema.TableColumn(schema);
				colvarTinNhan.ColumnName = "TinNhan";
				colvarTinNhan.DataType = DbType.String;
				colvarTinNhan.MaxLength = 4000;
				colvarTinNhan.AutoIncrement = false;
				colvarTinNhan.IsNullable = false;
				colvarTinNhan.IsPrimaryKey = false;
				colvarTinNhan.IsForeignKey = false;
				colvarTinNhan.IsReadOnly = false;
				colvarTinNhan.DefaultSetting = @"";
				colvarTinNhan.ForeignKeyTableName = "";
				schema.Columns.Add(colvarTinNhan);
				
				TableSchema.TableColumn colvarNgayTao = new TableSchema.TableColumn(schema);
				colvarNgayTao.ColumnName = "NgayTao";
				colvarNgayTao.DataType = DbType.DateTime;
				colvarNgayTao.MaxLength = 0;
				colvarNgayTao.AutoIncrement = false;
				colvarNgayTao.IsNullable = true;
				colvarNgayTao.IsPrimaryKey = false;
				colvarNgayTao.IsForeignKey = false;
				colvarNgayTao.IsReadOnly = false;
				
						colvarNgayTao.DefaultSetting = @"(getdate())";
				colvarNgayTao.ForeignKeyTableName = "";
				schema.Columns.Add(colvarNgayTao);
				
				TableSchema.TableColumn colvarDaTraLoi = new TableSchema.TableColumn(schema);
				colvarDaTraLoi.ColumnName = "DaTraLoi";
				colvarDaTraLoi.DataType = DbType.Boolean;
				colvarDaTraLoi.MaxLength = 0;
				colvarDaTraLoi.AutoIncrement = false;
				colvarDaTraLoi.IsNullable = true;
				colvarDaTraLoi.IsPrimaryKey = false;
				colvarDaTraLoi.IsForeignKey = false;
				colvarDaTraLoi.IsReadOnly = false;
				
						colvarDaTraLoi.DefaultSetting = @"((0))";
				colvarDaTraLoi.ForeignKeyTableName = "";
				schema.Columns.Add(colvarDaTraLoi);
				
				TableSchema.TableColumn colvarNgayTraLoi = new TableSchema.TableColumn(schema);
				colvarNgayTraLoi.ColumnName = "NgayTraLoi";
				colvarNgayTraLoi.DataType = DbType.DateTime;
				colvarNgayTraLoi.MaxLength = 0;
				colvarNgayTraLoi.AutoIncrement = false;
				colvarNgayTraLoi.IsNullable = true;
				colvarNgayTraLoi.IsPrimaryKey = false;
				colvarNgayTraLoi.IsForeignKey = false;
				colvarNgayTraLoi.IsReadOnly = false;
				colvarNgayTraLoi.DefaultSetting = @"";
				colvarNgayTraLoi.ForeignKeyTableName = "";
				schema.Columns.Add(colvarNgayTraLoi);
				
				BaseSchema = schema;
				//add this schema to the provider
				//so we can query it later
				DataService.Providers["DataAcessProvider"].AddSchema("LienHe",schema);
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
		  
		[XmlAttribute("HoVaTen")]
		[Bindable(true)]
		public string HoVaTen 
		{
			get { return GetColumnValue<string>(Columns.HoVaTen); }
			set { SetColumnValue(Columns.HoVaTen, value); }
		}
		  
		[XmlAttribute("Email")]
		[Bindable(true)]
		public string Email 
		{
			get { return GetColumnValue<string>(Columns.Email); }
			set { SetColumnValue(Columns.Email, value); }
		}
		  
		[XmlAttribute("SoDienThoai")]
		[Bindable(true)]
		public string SoDienThoai 
		{
			get { return GetColumnValue<string>(Columns.SoDienThoai); }
			set { SetColumnValue(Columns.SoDienThoai, value); }
		}
		  
		[XmlAttribute("ChuDe")]
		[Bindable(true)]
		public string ChuDe 
		{
			get { return GetColumnValue<string>(Columns.ChuDe); }
			set { SetColumnValue(Columns.ChuDe, value); }
		}
		  
		[XmlAttribute("TinNhan")]
		[Bindable(true)]
		public string TinNhan 
		{
			get { return GetColumnValue<string>(Columns.TinNhan); }
			set { SetColumnValue(Columns.TinNhan, value); }
		}
		  
		[XmlAttribute("NgayTao")]
		[Bindable(true)]
		public DateTime? NgayTao 
		{
			get { return GetColumnValue<DateTime?>(Columns.NgayTao); }
			set { SetColumnValue(Columns.NgayTao, value); }
		}
		  
		[XmlAttribute("DaTraLoi")]
		[Bindable(true)]
		public bool? DaTraLoi 
		{
			get { return GetColumnValue<bool?>(Columns.DaTraLoi); }
			set { SetColumnValue(Columns.DaTraLoi, value); }
		}
		  
		[XmlAttribute("NgayTraLoi")]
		[Bindable(true)]
		public DateTime? NgayTraLoi 
		{
			get { return GetColumnValue<DateTime?>(Columns.NgayTraLoi); }
			set { SetColumnValue(Columns.NgayTraLoi, value); }
		}
		
		#endregion
		
		
			
		
		//no foreign key tables defined (0)
		
		
		
		//no ManyToMany tables defined (0)
		
        
        
		#region ObjectDataSource support
		
		
		/// <summary>
		/// Inserts a record, can be used with the Object Data Source
		/// </summary>
		public static void Insert(string varHoVaTen,string varEmail,string varSoDienThoai,string varChuDe,string varTinNhan,DateTime? varNgayTao,bool? varDaTraLoi,DateTime? varNgayTraLoi)
		{
			LienHe item = new LienHe();
			
			item.HoVaTen = varHoVaTen;
			
			item.Email = varEmail;
			
			item.SoDienThoai = varSoDienThoai;
			
			item.ChuDe = varChuDe;
			
			item.TinNhan = varTinNhan;
			
			item.NgayTao = varNgayTao;
			
			item.DaTraLoi = varDaTraLoi;
			
			item.NgayTraLoi = varNgayTraLoi;
			
		
			if (System.Web.HttpContext.Current != null)
				item.Save(System.Web.HttpContext.Current.User.Identity.Name);
			else
				item.Save(System.Threading.Thread.CurrentPrincipal.Identity.Name);
		}
		
		/// <summary>
		/// Updates a record, can be used with the Object Data Source
		/// </summary>
		public static void Update(int varId,string varHoVaTen,string varEmail,string varSoDienThoai,string varChuDe,string varTinNhan,DateTime? varNgayTao,bool? varDaTraLoi,DateTime? varNgayTraLoi)
		{
			LienHe item = new LienHe();
			
				item.Id = varId;
			
				item.HoVaTen = varHoVaTen;
			
				item.Email = varEmail;
			
				item.SoDienThoai = varSoDienThoai;
			
				item.ChuDe = varChuDe;
			
				item.TinNhan = varTinNhan;
			
				item.NgayTao = varNgayTao;
			
				item.DaTraLoi = varDaTraLoi;
			
				item.NgayTraLoi = varNgayTraLoi;
			
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
        
        
        
        public static TableSchema.TableColumn HoVaTenColumn
        {
            get { return Schema.Columns[1]; }
        }
        
        
        
        public static TableSchema.TableColumn EmailColumn
        {
            get { return Schema.Columns[2]; }
        }
        
        
        
        public static TableSchema.TableColumn SoDienThoaiColumn
        {
            get { return Schema.Columns[3]; }
        }
        
        
        
        public static TableSchema.TableColumn ChuDeColumn
        {
            get { return Schema.Columns[4]; }
        }
        
        
        
        public static TableSchema.TableColumn TinNhanColumn
        {
            get { return Schema.Columns[5]; }
        }
        
        
        
        public static TableSchema.TableColumn NgayTaoColumn
        {
            get { return Schema.Columns[6]; }
        }
        
        
        
        public static TableSchema.TableColumn DaTraLoiColumn
        {
            get { return Schema.Columns[7]; }
        }
        
        
        
        public static TableSchema.TableColumn NgayTraLoiColumn
        {
            get { return Schema.Columns[8]; }
        }
        
        
        
        #endregion
		#region Columns Struct
		public struct Columns
		{
			 public static string Id = @"Id";
			 public static string HoVaTen = @"HoVaTen";
			 public static string Email = @"Email";
			 public static string SoDienThoai = @"SoDienThoai";
			 public static string ChuDe = @"ChuDe";
			 public static string TinNhan = @"TinNhan";
			 public static string NgayTao = @"NgayTao";
			 public static string DaTraLoi = @"DaTraLoi";
			 public static string NgayTraLoi = @"NgayTraLoi";
						
		}
		#endregion
		
		#region Update PK Collections
		
        #endregion
    
        #region Deep Save
		
        #endregion
	}
}
