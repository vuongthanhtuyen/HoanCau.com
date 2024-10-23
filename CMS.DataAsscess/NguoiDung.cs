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
	/// Strongly-typed collection for the NguoiDung class.
	/// </summary>
    [Serializable]
	public partial class NguoiDungCollection : ActiveList<NguoiDung, NguoiDungCollection>
	{	   
		public NguoiDungCollection() {}
        
        /// <summary>
		/// Filters an existing collection based on the set criteria. This is an in-memory filter
		/// Thanks to developingchris for this!
        /// </summary>
        /// <returns>NguoiDungCollection</returns>
		public NguoiDungCollection Filter()
        {
            for (int i = this.Count - 1; i > -1; i--)
            {
                NguoiDung o = this[i];
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
	/// This is an ActiveRecord class which wraps the NguoiDung table.
	/// </summary>
	[Serializable]
	public partial class NguoiDung : ActiveRecord<NguoiDung>, IActiveRecord
	{
		#region .ctors and Default Settings
		
		public NguoiDung()
		{
		  SetSQLProps();
		  InitSetDefaults();
		  MarkNew();
		}
		
		private void InitSetDefaults() { SetDefaults(); }
		
		public NguoiDung(bool useDatabaseDefaults)
		{
			SetSQLProps();
			if(useDatabaseDefaults)
				ForceDefaults();
			MarkNew();
		}
        
		public NguoiDung(object keyID)
		{
			SetSQLProps();
			InitSetDefaults();
			LoadByKey(keyID);
		}
		 
		public NguoiDung(string columnName, object columnValue)
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
				TableSchema.Table schema = new TableSchema.Table("NguoiDung", TableType.Table, DataService.GetInstance("DataAcessProvider"));
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
				colvarHoVaTen.MaxLength = 50;
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
				colvarEmail.MaxLength = 50;
				colvarEmail.AutoIncrement = false;
				colvarEmail.IsNullable = true;
				colvarEmail.IsPrimaryKey = false;
				colvarEmail.IsForeignKey = false;
				colvarEmail.IsReadOnly = false;
				colvarEmail.DefaultSetting = @"";
				colvarEmail.ForeignKeyTableName = "";
				schema.Columns.Add(colvarEmail);
				
				TableSchema.TableColumn colvarSoDienThoai = new TableSchema.TableColumn(schema);
				colvarSoDienThoai.ColumnName = "SoDienThoai";
				colvarSoDienThoai.DataType = DbType.AnsiString;
				colvarSoDienThoai.MaxLength = 20;
				colvarSoDienThoai.AutoIncrement = false;
				colvarSoDienThoai.IsNullable = true;
				colvarSoDienThoai.IsPrimaryKey = false;
				colvarSoDienThoai.IsForeignKey = false;
				colvarSoDienThoai.IsReadOnly = false;
				colvarSoDienThoai.DefaultSetting = @"";
				colvarSoDienThoai.ForeignKeyTableName = "";
				schema.Columns.Add(colvarSoDienThoai);
				
				TableSchema.TableColumn colvarNgaySinh = new TableSchema.TableColumn(schema);
				colvarNgaySinh.ColumnName = "NgaySinh";
				colvarNgaySinh.DataType = DbType.DateTime;
				colvarNgaySinh.MaxLength = 0;
				colvarNgaySinh.AutoIncrement = false;
				colvarNgaySinh.IsNullable = true;
				colvarNgaySinh.IsPrimaryKey = false;
				colvarNgaySinh.IsForeignKey = false;
				colvarNgaySinh.IsReadOnly = false;
				colvarNgaySinh.DefaultSetting = @"";
				colvarNgaySinh.ForeignKeyTableName = "";
				schema.Columns.Add(colvarNgaySinh);
				
				TableSchema.TableColumn colvarTenTruyCap = new TableSchema.TableColumn(schema);
				colvarTenTruyCap.ColumnName = "TenTruyCap";
				colvarTenTruyCap.DataType = DbType.AnsiString;
				colvarTenTruyCap.MaxLength = 50;
				colvarTenTruyCap.AutoIncrement = false;
				colvarTenTruyCap.IsNullable = false;
				colvarTenTruyCap.IsPrimaryKey = false;
				colvarTenTruyCap.IsForeignKey = false;
				colvarTenTruyCap.IsReadOnly = false;
				colvarTenTruyCap.DefaultSetting = @"";
				colvarTenTruyCap.ForeignKeyTableName = "";
				schema.Columns.Add(colvarTenTruyCap);
				
				TableSchema.TableColumn colvarMatKhau = new TableSchema.TableColumn(schema);
				colvarMatKhau.ColumnName = "MatKhau";
				colvarMatKhau.DataType = DbType.AnsiString;
				colvarMatKhau.MaxLength = 50;
				colvarMatKhau.AutoIncrement = false;
				colvarMatKhau.IsNullable = false;
				colvarMatKhau.IsPrimaryKey = false;
				colvarMatKhau.IsForeignKey = false;
				colvarMatKhau.IsReadOnly = false;
				colvarMatKhau.DefaultSetting = @"";
				colvarMatKhau.ForeignKeyTableName = "";
				schema.Columns.Add(colvarMatKhau);
				
				TableSchema.TableColumn colvarDiaChi = new TableSchema.TableColumn(schema);
				colvarDiaChi.ColumnName = "DiaChi";
				colvarDiaChi.DataType = DbType.String;
				colvarDiaChi.MaxLength = 200;
				colvarDiaChi.AutoIncrement = false;
				colvarDiaChi.IsNullable = true;
				colvarDiaChi.IsPrimaryKey = false;
				colvarDiaChi.IsForeignKey = false;
				colvarDiaChi.IsReadOnly = false;
				colvarDiaChi.DefaultSetting = @"";
				colvarDiaChi.ForeignKeyTableName = "";
				schema.Columns.Add(colvarDiaChi);
				
				TableSchema.TableColumn colvarAvataUrl = new TableSchema.TableColumn(schema);
				colvarAvataUrl.ColumnName = "AvataUrl";
				colvarAvataUrl.DataType = DbType.AnsiString;
				colvarAvataUrl.MaxLength = 500;
				colvarAvataUrl.AutoIncrement = false;
				colvarAvataUrl.IsNullable = true;
				colvarAvataUrl.IsPrimaryKey = false;
				colvarAvataUrl.IsForeignKey = false;
				colvarAvataUrl.IsReadOnly = false;
				colvarAvataUrl.DefaultSetting = @"";
				colvarAvataUrl.ForeignKeyTableName = "";
				schema.Columns.Add(colvarAvataUrl);
				
				TableSchema.TableColumn colvarTrangThai = new TableSchema.TableColumn(schema);
				colvarTrangThai.ColumnName = "TrangThai";
				colvarTrangThai.DataType = DbType.Boolean;
				colvarTrangThai.MaxLength = 0;
				colvarTrangThai.AutoIncrement = false;
				colvarTrangThai.IsNullable = true;
				colvarTrangThai.IsPrimaryKey = false;
				colvarTrangThai.IsForeignKey = false;
				colvarTrangThai.IsReadOnly = false;
				
						colvarTrangThai.DefaultSetting = @"((1))";
				colvarTrangThai.ForeignKeyTableName = "";
				schema.Columns.Add(colvarTrangThai);
				
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
				
				TableSchema.TableColumn colvarChinhSuaGanNhat = new TableSchema.TableColumn(schema);
				colvarChinhSuaGanNhat.ColumnName = "ChinhSuaGanNhat";
				colvarChinhSuaGanNhat.DataType = DbType.DateTime;
				colvarChinhSuaGanNhat.MaxLength = 0;
				colvarChinhSuaGanNhat.AutoIncrement = false;
				colvarChinhSuaGanNhat.IsNullable = true;
				colvarChinhSuaGanNhat.IsPrimaryKey = false;
				colvarChinhSuaGanNhat.IsForeignKey = false;
				colvarChinhSuaGanNhat.IsReadOnly = false;
				
						colvarChinhSuaGanNhat.DefaultSetting = @"(getdate())";
				colvarChinhSuaGanNhat.ForeignKeyTableName = "";
				schema.Columns.Add(colvarChinhSuaGanNhat);
				
				BaseSchema = schema;
				//add this schema to the provider
				//so we can query it later
				DataService.Providers["DataAcessProvider"].AddSchema("NguoiDung",schema);
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
		  
		[XmlAttribute("NgaySinh")]
		[Bindable(true)]
		public DateTime? NgaySinh 
		{
			get { return GetColumnValue<DateTime?>(Columns.NgaySinh); }
			set { SetColumnValue(Columns.NgaySinh, value); }
		}
		  
		[XmlAttribute("TenTruyCap")]
		[Bindable(true)]
		public string TenTruyCap 
		{
			get { return GetColumnValue<string>(Columns.TenTruyCap); }
			set { SetColumnValue(Columns.TenTruyCap, value); }
		}
		  
		[XmlAttribute("MatKhau")]
		[Bindable(true)]
		public string MatKhau 
		{
			get { return GetColumnValue<string>(Columns.MatKhau); }
			set { SetColumnValue(Columns.MatKhau, value); }
		}
		  
		[XmlAttribute("DiaChi")]
		[Bindable(true)]
		public string DiaChi 
		{
			get { return GetColumnValue<string>(Columns.DiaChi); }
			set { SetColumnValue(Columns.DiaChi, value); }
		}
		  
		[XmlAttribute("AvataUrl")]
		[Bindable(true)]
		public string AvataUrl 
		{
			get { return GetColumnValue<string>(Columns.AvataUrl); }
			set { SetColumnValue(Columns.AvataUrl, value); }
		}
		  
		[XmlAttribute("TrangThai")]
		[Bindable(true)]
		public bool? TrangThai 
		{
			get { return GetColumnValue<bool?>(Columns.TrangThai); }
			set { SetColumnValue(Columns.TrangThai, value); }
		}
		  
		[XmlAttribute("NgayTao")]
		[Bindable(true)]
		public DateTime? NgayTao 
		{
			get { return GetColumnValue<DateTime?>(Columns.NgayTao); }
			set { SetColumnValue(Columns.NgayTao, value); }
		}
		  
		[XmlAttribute("ChinhSuaGanNhat")]
		[Bindable(true)]
		public DateTime? ChinhSuaGanNhat 
		{
			get { return GetColumnValue<DateTime?>(Columns.ChinhSuaGanNhat); }
			set { SetColumnValue(Columns.ChinhSuaGanNhat, value); }
		}
		
		#endregion
		
		
		#region PrimaryKey Methods		
		
        protected override void SetPrimaryKey(object oValue)
        {
            base.SetPrimaryKey(oValue);
            
            SetPKValues();
        }
        
		
		private SweetCMS.DataAccess.BaiVietCollection colBaiVietRecords;
		public SweetCMS.DataAccess.BaiVietCollection BaiVietRecords()
		{
			if(colBaiVietRecords == null)
			{
				colBaiVietRecords = new SweetCMS.DataAccess.BaiVietCollection().Where(BaiViet.Columns.TacGiaId, Id).Load();
				colBaiVietRecords.ListChanged += new ListChangedEventHandler(colBaiVietRecords_ListChanged);
			}
			return colBaiVietRecords;
		}
				
		void colBaiVietRecords_ListChanged(object sender, ListChangedEventArgs e)
		{
            if (e.ListChangedType == ListChangedType.ItemAdded)
            {
		        // Set foreign key value
		        colBaiVietRecords[e.NewIndex].TacGiaId = Id;
            }
		}
		private SweetCMS.DataAccess.NguoiDungVaiTroChitietCollection colNguoiDungVaiTroChitietRecords;
		public SweetCMS.DataAccess.NguoiDungVaiTroChitietCollection NguoiDungVaiTroChitietRecords()
		{
			if(colNguoiDungVaiTroChitietRecords == null)
			{
				colNguoiDungVaiTroChitietRecords = new SweetCMS.DataAccess.NguoiDungVaiTroChitietCollection().Where(NguoiDungVaiTroChitiet.Columns.NguoiDungId, Id).Load();
				colNguoiDungVaiTroChitietRecords.ListChanged += new ListChangedEventHandler(colNguoiDungVaiTroChitietRecords_ListChanged);
			}
			return colNguoiDungVaiTroChitietRecords;
		}
				
		void colNguoiDungVaiTroChitietRecords_ListChanged(object sender, ListChangedEventArgs e)
		{
            if (e.ListChangedType == ListChangedType.ItemAdded)
            {
		        // Set foreign key value
		        colNguoiDungVaiTroChitietRecords[e.NewIndex].NguoiDungId = Id;
            }
		}
		#endregion
		
			
		
		//no foreign key tables defined (0)
		
		
		
		//no ManyToMany tables defined (0)
		
        
        
		#region ObjectDataSource support
		
		
		/// <summary>
		/// Inserts a record, can be used with the Object Data Source
		/// </summary>
		public static void Insert(string varHoVaTen,string varEmail,string varSoDienThoai,DateTime? varNgaySinh,string varTenTruyCap,string varMatKhau,string varDiaChi,string varAvataUrl,bool? varTrangThai,DateTime? varNgayTao,DateTime? varChinhSuaGanNhat)
		{
			NguoiDung item = new NguoiDung();
			
			item.HoVaTen = varHoVaTen;
			
			item.Email = varEmail;
			
			item.SoDienThoai = varSoDienThoai;
			
			item.NgaySinh = varNgaySinh;
			
			item.TenTruyCap = varTenTruyCap;
			
			item.MatKhau = varMatKhau;
			
			item.DiaChi = varDiaChi;
			
			item.AvataUrl = varAvataUrl;
			
			item.TrangThai = varTrangThai;
			
			item.NgayTao = varNgayTao;
			
			item.ChinhSuaGanNhat = varChinhSuaGanNhat;
			
		
			if (System.Web.HttpContext.Current != null)
				item.Save(System.Web.HttpContext.Current.User.Identity.Name);
			else
				item.Save(System.Threading.Thread.CurrentPrincipal.Identity.Name);
		}
		
		/// <summary>
		/// Updates a record, can be used with the Object Data Source
		/// </summary>
		public static void Update(int varId,string varHoVaTen,string varEmail,string varSoDienThoai,DateTime? varNgaySinh,string varTenTruyCap,string varMatKhau,string varDiaChi,string varAvataUrl,bool? varTrangThai,DateTime? varNgayTao,DateTime? varChinhSuaGanNhat)
		{
			NguoiDung item = new NguoiDung();
			
				item.Id = varId;
			
				item.HoVaTen = varHoVaTen;
			
				item.Email = varEmail;
			
				item.SoDienThoai = varSoDienThoai;
			
				item.NgaySinh = varNgaySinh;
			
				item.TenTruyCap = varTenTruyCap;
			
				item.MatKhau = varMatKhau;
			
				item.DiaChi = varDiaChi;
			
				item.AvataUrl = varAvataUrl;
			
				item.TrangThai = varTrangThai;
			
				item.NgayTao = varNgayTao;
			
				item.ChinhSuaGanNhat = varChinhSuaGanNhat;
			
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
        
        
        
        public static TableSchema.TableColumn NgaySinhColumn
        {
            get { return Schema.Columns[4]; }
        }
        
        
        
        public static TableSchema.TableColumn TenTruyCapColumn
        {
            get { return Schema.Columns[5]; }
        }
        
        
        
        public static TableSchema.TableColumn MatKhauColumn
        {
            get { return Schema.Columns[6]; }
        }
        
        
        
        public static TableSchema.TableColumn DiaChiColumn
        {
            get { return Schema.Columns[7]; }
        }
        
        
        
        public static TableSchema.TableColumn AvataUrlColumn
        {
            get { return Schema.Columns[8]; }
        }
        
        
        
        public static TableSchema.TableColumn TrangThaiColumn
        {
            get { return Schema.Columns[9]; }
        }
        
        
        
        public static TableSchema.TableColumn NgayTaoColumn
        {
            get { return Schema.Columns[10]; }
        }
        
        
        
        public static TableSchema.TableColumn ChinhSuaGanNhatColumn
        {
            get { return Schema.Columns[11]; }
        }
        
        
        
        #endregion
		#region Columns Struct
		public struct Columns
		{
			 public static string Id = @"Id";
			 public static string HoVaTen = @"HoVaTen";
			 public static string Email = @"Email";
			 public static string SoDienThoai = @"SoDienThoai";
			 public static string NgaySinh = @"NgaySinh";
			 public static string TenTruyCap = @"TenTruyCap";
			 public static string MatKhau = @"MatKhau";
			 public static string DiaChi = @"DiaChi";
			 public static string AvataUrl = @"AvataUrl";
			 public static string TrangThai = @"TrangThai";
			 public static string NgayTao = @"NgayTao";
			 public static string ChinhSuaGanNhat = @"ChinhSuaGanNhat";
						
		}
		#endregion
		
		#region Update PK Collections
		
        public void SetPKValues()
        {
                if (colBaiVietRecords != null)
                {
                    foreach (SweetCMS.DataAccess.BaiViet item in colBaiVietRecords)
                    {
                        if (item.TacGiaId == null ||item.TacGiaId != Id)
                        {
                            item.TacGiaId = Id;
                        }
                    }
               }
		
                if (colNguoiDungVaiTroChitietRecords != null)
                {
                    foreach (SweetCMS.DataAccess.NguoiDungVaiTroChitiet item in colNguoiDungVaiTroChitietRecords)
                    {
                        if (item.NguoiDungId != Id)
                        {
                            item.NguoiDungId = Id;
                        }
                    }
               }
		}
        #endregion
    
        #region Deep Save
		
        public void DeepSave()
        {
            Save();
            
                if (colBaiVietRecords != null)
                {
                    colBaiVietRecords.SaveAll();
               }
		
                if (colNguoiDungVaiTroChitietRecords != null)
                {
                    colNguoiDungVaiTroChitietRecords.SaveAll();
               }
		}
        #endregion
	}
}