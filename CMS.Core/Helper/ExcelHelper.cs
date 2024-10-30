using Excel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace SweetCMS.Core.Helper
{
    public class ExcelHelper
    {
        #region Attributes

        private string filename;
        private string connectionString;
        
        #endregion

        #region properties

        public string Filename
        {
            get { return filename; }
            set { filename = value; }
        }
        public string ConnectionString
        {
            get { return connectionString; }
            set { connectionString = value; }
        }

        #endregion

        #region Methods
        public ExcelHelper()
        {

            filename = "";
            connectionString = "";
        }
        public string ExcelConnectString2003()// chuoi ket noi cho excel 2003
        {
            return @"Provider=Microsoft.ACE.OLEDB.12.0;" +
                  @"Data Source={0};" +
                  @"Extended Properties=" + Convert.ToChar(34).ToString() +
                  @"Excel 12.0 Xml;HDR=YES" + Convert.ToChar(34).ToString();
            //return @"Provider=Microsoft.Jet.OLEDB.4.0;" +
            //       @"Data Source={0};" +
            //       @"Extended Properties=" + Convert.ToChar(34).ToString() +
            //       @"Excel 8.0;HDR=Yes;IMEX=1" + Convert.ToChar(34).ToString();
        }

        public string ExcelConnectString2007()// chuoi ket noi cho excel 2007
        {
            return @"Provider=Microsoft.ACE.OLEDB.12.0;" +
                   @"Data Source={0};" +
                   @"Extended Properties=" + Convert.ToChar(34).ToString() +
                   @"Excel 12.0 Xml;HDR=YES" + Convert.ToChar(34).ToString();
        }
        public void GetConnectionString()// lua chon mot trong hai chuoi ket noi den excel 2003 hay 2007
        {
            string filename_extension = filename;
            if (filename_extension == "")
            {
                return;
            }
            else
            {
                string[] s = filename_extension.Split(Convert.ToChar(46));      //46 ma thap phan cua dau .
                if (s[s.Length - 1] == "xls")
                    connectionString = string.Format(ExcelConnectString2003(), filename);
                else
                    if (s[s.Length - 1] == "xlsx")
                    connectionString = string.Format(ExcelConnectString2007(), filename);
                else
                    return;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="cmdText"> cau lenh sql</param>
        /// <returns>datatable</returns>
        public DataTable ReadExcel(string cmdText)
        {
            try
            {
                DataTable table = new DataTable();
                GetConnectionString();//lay ve chuoi ket noi excel 
                using (OleDbConnection conn = new OleDbConnection(connectionString))
                {
                    conn.Open();
                    OleDbCommand cmd = new OleDbCommand(cmdText, conn);
                    OleDbDataAdapter adapter = new OleDbDataAdapter(cmd);
                    adapter.Fill(table);
                    adapter.Dispose();
                    conn.Close();
                }
                return table;
            }
            catch (Exception ex)
            {

                return null;
            }
        }

        /*using ExcelReaderFactory*/
        public DataTable ReadExcelBySheet(string SheetName)
        {
            DataSet ds = GetDataExcel2007toLater(this.filename, true);
            if (ds == null || ds.Tables.Count == 0)
                ds = GetDataExcelto2003(this.filename, true);
            if (ds != null && ds.Tables.Count > 0)
                return ds.Tables[0];
            return null;
        }

        #endregion
        /// <summary>
        /// Lay tat ca cac sheet trong excel
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public DataTable GetAllSheet()
        {
            GetConnectionString();
            using (OleDbConnection connection = new
                      OleDbConnection(connectionString))
            {
                connection.Open();
                DataTable schemaTable = connection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                connection.Close();
                return schemaTable;
            }
            return null;
        }

        public static DataSet GetDataExcel2007toLater(string filePath)
        {
            return GetDataExcel2007toLater(filePath, false);
        }

        public static DataSet GetDataExcel2007toLater(string filePath, bool isFirstRowAsColumnNames)
        {
            if (File.Exists(filePath) == false)
                return null;
            DataSet result = null;
            try
            {
                FileStream stream = File.Open(filePath, FileMode.Open, FileAccess.Read);

                //1. Reading from a binary Excel file ('97-2003 format; *.xls)
                //IExcelDataReader excelReader = ExcelReaderFactory.CreateBinaryReader(stream);

                //2. Reading from a OpenXml Excel file (2007 format; *.xlsx)
                IExcelDataReader excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream);

                excelReader.IsFirstRowAsColumnNames = isFirstRowAsColumnNames;

                //3. DataSet - The result of each spreadsheet will be created in the result.Tables
                result = excelReader.AsDataSet();

                /*
                //4. DataSet - Create column names from first row
                excelReader.IsFirstRowAsColumnNames = true;
                DataSet result = excelReader.AsDataSet();
                */

                /*
                //5. Data Reader methods
                while (excelReader.Read())
                {
                    //excelReader.GetInt32(0);
                }
                */

                //6. Free resources (IExcelDataReader is IDisposable)
                excelReader.Close();
                excelReader.Dispose();
            }
            catch (Exception ex) { }

            return result;
        }

        public static DataSet GetDataExcelto2003(string filePath)
        {
            return GetDataExcelto2003(filePath, false);
        }

        public static DataSet GetDataExcelto2003(string filePath, bool isFirstRowAsColumnNames)
        {
            if (File.Exists(filePath) == false)
                return null;

            FileStream stream = File.Open(filePath, FileMode.Open, FileAccess.Read);

            //1. Reading from a binary Excel file ('97-2003 format; *.xls)
            IExcelDataReader excelReader = ExcelReaderFactory.CreateBinaryReader(stream);

            //2. Reading from a OpenXml Excel file (2007 format; *.xlsx)
            //IExcelDataReader excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream);

            excelReader.IsFirstRowAsColumnNames = isFirstRowAsColumnNames;

            //3. DataSet - The result of each spreadsheet will be created in the result.Tables
            DataSet result = excelReader.AsDataSet();

            /*
            //4. DataSet - Create column names from first row
            excelReader.IsFirstRowAsColumnNames = true;
            DataSet result = excelReader.AsDataSet();
            */

            /*
            //5. Data Reader methods
            while (excelReader.Read())
            {
                //excelReader.GetInt32(0);
            }
            */

            //6. Free resources (IExcelDataReader is IDisposable)
            excelReader.Close();

            return result;
        }

        public static void ExportDataTableToExcel(DataTable table, string[] Headers, string FileName)
        {
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.ClearContent();
            HttpContext.Current.Response.ClearHeaders();
            HttpContext.Current.Response.Buffer = true;
            HttpContext.Current.Response.ContentType = "application/ms-excel";
            HttpContext.Current.Response.Write(@"<!DOCTYPE HTML PUBLIC ""-//W3C//DTD HTML 4.0 Transitional//EN"">");
            //response.ContentType = "application/vnd.ms-excel";
            HttpContext.Current.Response.Charset = "Unicode";
            HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("Unicode");
            HttpContext.Current.Response.BinaryWrite(System.Text.Encoding.Unicode.GetPreamble());
            //response.Charset = "UTF-8";
            HttpContext.Current.Response.AppendHeader("content-disposition", "attachment; filename=" + FileName + ".xls");
            HttpContext.Current.Response.AppendHeader("ACCEPT-CHARSET", "UTF-8");
            HttpContext.Current.Response.AppendHeader("ACCEPT-ENCODING", "UTF-8");
            HttpContext.Current.Response.HeaderEncoding = System.Text.Encoding.UTF8;

            //sets font
            HttpContext.Current.Response.Write("<font style='font-size:10.5pt; font-family:Calibri;'>");
            HttpContext.Current.Response.Write("<BR><BR><BR>");
            //sets the table border, cell spacing, border color, font of the text, background, foreground, font height
            HttpContext.Current.Response.Write(@"<Table border='1' cellSpacing='0' cellPadding='0'
              style='font-size:10.5pt; background:white;'> <TR>");
            //am getting my grid's column headers

            for (int j = 0; j < Headers.Length; j++)
            {      //write in new column
                HttpContext.Current.Response.Write("<Td>");
                //Get column headers  and make it as bold in excel columns
                HttpContext.Current.Response.Write("<B>");
                HttpContext.Current.Response.Write(Headers[j].ToString());
                HttpContext.Current.Response.Write("</B>");
                HttpContext.Current.Response.Write("</Td>");
            }
            HttpContext.Current.Response.Write("</TR>");
            foreach (DataRow row in table.Rows)
            {//write in new row
                HttpContext.Current.Response.Write("<TR>");
                for (int i = 0; i < table.Columns.Count; i++)
                {
                    HttpContext.Current.Response.Write("<Td>");
                    HttpContext.Current.Response.Write(row[i].ToString());
                    HttpContext.Current.Response.Write("</Td>");
                }

                HttpContext.Current.Response.Write("</TR>");
            }
            HttpContext.Current.Response.Write("</Table>");
            HttpContext.Current.Response.Write("</font>");
            HttpContext.Current.Response.Flush();
            HttpContext.Current.Response.End();
        }
    }
}
