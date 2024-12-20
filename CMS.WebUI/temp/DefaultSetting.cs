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
	/// Strongly-typed collection for the DefaultSetting class.
	/// </summary>
    [Serializable]
	public partial class DefaultSettingCollection : ActiveList<DefaultSetting, DefaultSettingCollection>
	{	   
		public DefaultSettingCollection() {}
        
        /// <summary>
		/// Filters an existing collection based on the set criteria. This is an in-memory filter
		/// Thanks to developingchris for this!
        /// </summary>
        /// <returns>DefaultSettingCollection</returns>
		public DefaultSettingCollection Filter()
        {
            for (int i = this.Count - 1; i > -1; i--)
            {
                DefaultSetting o = this[i];
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
	/// This is an ActiveRecord class which wraps the DefaultSetting table.
	/// </summary>
	[Serializable]
	public partial class DefaultSetting : ActiveRecord<DefaultSetting>, IActiveRecord
	{
		#region .ctors and Default Settings
		
		public DefaultSetting()
		{
		  SetSQLProps();
		  InitSetDefaults();
		  MarkNew();
		}
		
		private void InitSetDefaults() { SetDefaults(); }
		
		public DefaultSetting(bool useDatabaseDefaults)
		{
			SetSQLProps();
			if(useDatabaseDefaults)
				ForceDefaults();
			MarkNew();
		}
        
		public DefaultSetting(object keyID)
		{
			SetSQLProps();
			InitSetDefaults();
			LoadByKey(keyID);
		}
		 
		public DefaultSetting(string columnName, object columnValue)
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
				TableSchema.Table schema = new TableSchema.Table("DefaultSetting", TableType.Table, DataService.GetInstance("DataAcessProvider"));
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
				
				TableSchema.TableColumn colvarKeyString = new TableSchema.TableColumn(schema);
				colvarKeyString.ColumnName = "KeyString";
				colvarKeyString.DataType = DbType.String;
				colvarKeyString.MaxLength = 100;
				colvarKeyString.AutoIncrement = false;
				colvarKeyString.IsNullable = false;
				colvarKeyString.IsPrimaryKey = false;
				colvarKeyString.IsForeignKey = false;
				colvarKeyString.IsReadOnly = false;
				
						colvarKeyString.DefaultSetting = @"('')";
				colvarKeyString.ForeignKeyTableName = "";
				schema.Columns.Add(colvarKeyString);
				
				TableSchema.TableColumn colvarValueX = new TableSchema.TableColumn(schema);
				colvarValueX.ColumnName = "Value";
				colvarValueX.DataType = DbType.String;
				colvarValueX.MaxLength = 500;
				colvarValueX.AutoIncrement = false;
				colvarValueX.IsNullable = false;
				colvarValueX.IsPrimaryKey = false;
				colvarValueX.IsForeignKey = false;
				colvarValueX.IsReadOnly = false;
				
						colvarValueX.DefaultSetting = @"('')";
				colvarValueX.ForeignKeyTableName = "";
				schema.Columns.Add(colvarValueX);
				
				BaseSchema = schema;
				//add this schema to the provider
				//so we can query it later
				DataService.Providers["DataAcessProvider"].AddSchema("DefaultSetting",schema);
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
		  
		[XmlAttribute("KeyString")]
		[Bindable(true)]
		public string KeyString 
		{
			get { return GetColumnValue<string>(Columns.KeyString); }
			set { SetColumnValue(Columns.KeyString, value); }
		}
		  
		[XmlAttribute("ValueX")]
		[Bindable(true)]
		public string ValueX 
		{
			get { return GetColumnValue<string>(Columns.ValueX); }
			set { SetColumnValue(Columns.ValueX, value); }
		}
		
		#endregion
		
		
			
		
		//no foreign key tables defined (0)
		
		
		
		//no ManyToMany tables defined (0)
		
        
        
		#region ObjectDataSource support
		
		
		/// <summary>
		/// Inserts a record, can be used with the Object Data Source
		/// </summary>
		public static void Insert(string varKeyString,string varValueX)
		{
			DefaultSetting item = new DefaultSetting();
			
			item.KeyString = varKeyString;
			
			item.ValueX = varValueX;
			
		
			if (System.Web.HttpContext.Current != null)
				item.Save(System.Web.HttpContext.Current.User.Identity.Name);
			else
				item.Save(System.Threading.Thread.CurrentPrincipal.Identity.Name);
		}
		
		/// <summary>
		/// Updates a record, can be used with the Object Data Source
		/// </summary>
		public static void Update(int varId,string varKeyString,string varValueX)
		{
			DefaultSetting item = new DefaultSetting();
			
				item.Id = varId;
			
				item.KeyString = varKeyString;
			
				item.ValueX = varValueX;
			
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
        
        
        
        public static TableSchema.TableColumn KeyStringColumn
        {
            get { return Schema.Columns[1]; }
        }
        
        
        
        public static TableSchema.TableColumn ValueXColumn
        {
            get { return Schema.Columns[2]; }
        }
        
        
        
        #endregion
		#region Columns Struct
		public struct Columns
		{
			 public static string Id = @"Id";
			 public static string KeyString = @"KeyString";
			 public static string ValueX = @"Value";
						
		}
		#endregion
		
		#region Update PK Collections
		
        #endregion
    
        #region Deep Save
		
        #endregion
	}
}
