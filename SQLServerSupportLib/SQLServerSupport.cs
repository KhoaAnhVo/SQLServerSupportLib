using System;
using System.Data;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.IO;


namespace SQLServerSupportLib
{
    public class SQLServerSupport
    {

    }

    /// <summary>
    /// Xử lý giao tiếp với Database
    /// </summary>
    public class QrCodeDataBase
    {
        //Tạo thông tin kết nối đến csdl 
        SqlConnectionStringBuilder SqlBuilder = new SqlConnectionStringBuilder();
        public QrCodeDataBase()
        {
            SqlBuilder.DataSource = "localhost";
            SqlBuilder.UserID = "Admin_Khoa";
            SqlBuilder.Password = "khoanhvo";
            SqlBuilder.InitialCatalog = "Syngenta_test";
        }
        /// <summary>
        /// Excute query cmd form text
        /// </summary>
        /// <param name="cmd_txt">Text Command</param>
        public bool SqlCmdExcuteQuery(StringBuilder cmd_txt)
        {
            bool result;
            try
            {
                using (SqlConnection conn = new SqlConnection(SqlBuilder.ConnectionString))
                {
                    conn.Open(); // thuc hien ket noi voi sql
                    using (SqlCommand cmd = new SqlCommand(cmd_txt.ToString(), conn))
                    {
                        cmd.ExecuteNonQuery();
                        result = true;
                    }
                }
            }
            catch (SqlException ex)
            {
                //MessageBox.Show(ex.ToString());
                result = false;
            }
            return result;
        }
        /// <summary>
        /// Excute query cmd from _.sql file save in direction source folder..\bin\Debug
        /// </summary>
        /// <param name="query_file_name">File .sql name</param>
        /// <returns></returns>
        public bool SqlCmdExcuteQuery(string query_file_name)
        {
            bool result;
            //get direction to source code
            string Path = Environment.CurrentDirectory;
            string cmd_txt = File.ReadAllText(Path + @"\" + query_file_name);
            try
            {
                using (SqlConnection conn = new SqlConnection(SqlBuilder.ConnectionString))
                {
                    conn.Open(); // thuc hien ket noi voi sql
                    using (SqlCommand cmd = new SqlCommand(cmd_txt, conn))
                    {
                        cmd.ExecuteNonQuery();
                        result = true;
                    }
                }
            }
            catch (SqlException ex)
            {
                //MessageBox.Show(ex.ToString());
                result = false;
            }
            return result;
        }
        /// <summary>
        /// Get list of tables in database
        /// </summary>
        /// <returns></returns>
        public List<string> GetListTableDB()
        {
            List<string> result = new List<string>();
            try
            {
                using (SqlConnection conn = new SqlConnection(SqlBuilder.ConnectionString))
                {
                    string cmd_txt = string.Format(@"select TABLE_NAME from information_schema.tables;");
                    conn.Open();
                    using (SqlCommand command = new SqlCommand(cmd_txt, conn))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                result.Add(reader.GetString(0));
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {

            }
            return result;
        }
        //Save dữ liệu lên 
    }
    /// <summary>
    /// Xử lý giao tiếp với Database
    /// </summary>
    class SqlServerMethod : IDisposable
    {
        //Tạo thông tin kết nối đến csdl 
        SqlConnectionStringBuilder SqlBuilder = new SqlConnectionStringBuilder();

        public void SetConnStringDB(string SqlConnectionString)
        {
            SqlBuilder.ConnectionString = SqlConnectionString;
        }

        public SqlServerMethod()
        {
            SqlBuilder.DataSource = "localhost";
            SqlBuilder.UserID = "Admin_Khoa";
            SqlBuilder.Password = "khoanhvo";
            SqlBuilder.InitialCatalog = "Syngenta_test";
        }

        public SqlServerMethod(string ConnectionString)
        {
            SqlBuilder.ConnectionString = ConnectionString;
        }

        /// <summary>
        /// Excute query cmd form text
        /// </summary>
        /// <param name="cmd_txt">Text Command</param>
        public bool ExcuteQuery(StringBuilder cmd_txt)
        {
            bool result;
            try
            {
                using (SqlConnection conn = new SqlConnection(SqlBuilder.ConnectionString))
                {
                    conn.Open(); // thuc hien ket noi voi sql
                    using (SqlCommand cmd = new SqlCommand(cmd_txt.ToString(), conn))
                    {
                        cmd.ExecuteNonQuery();
                        result = true;
                    }
                }
            }
            catch (SqlException ex)
            {
                //MessageBox.Show(ex.ToString());
                result = false;
            }
            return result;
        }

        /// <summary>
        /// Excute query cmd from _.sql file save in direction source folder..\bin\Debug
        /// </summary>
        /// <param name="query_file_name">File .sql name</param>
        /// <returns></returns>
        public bool ExcuteQuery(string query_file_name)
        {
            bool result;
            //get direction to source code
            string Path = Environment.CurrentDirectory;
            string cmd_txt = File.ReadAllText(Path + @"\" + query_file_name);
            try
            {
                using (SqlConnection conn = new SqlConnection(SqlBuilder.ConnectionString))
                {
                    conn.Open(); // thuc hien ket noi voi sql
                    using (SqlCommand cmd = new SqlCommand(cmd_txt, conn))
                    {
                        cmd.ExecuteNonQuery();
                        result = true;
                    }
                }
            }
            catch (SqlException ex)
            {
                //MessageBox.Show(ex.ToString());
                result = false;
            }
            return result;
        }

        /// <summary>
        /// Get list of tables in database
        /// </summary>
        /// <returns></returns>
        public List<string> GetListTableNameDB()
        {
            List<string> result = new List<string>();
            try
            {
                using (SqlConnection conn = new SqlConnection(SqlBuilder.ConnectionString))
                {
                    string cmd_txt = string.Format(@"select TABLE_NAME from information_schema.tables;");
                    conn.Open();
                    using (SqlCommand command = new SqlCommand(cmd_txt, conn))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                result.Add(reader.GetString(0));
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {

            }
            return result;
        }

        //Sql data to Datatable
        public DataTable GetDataSqlToTable(string query)
        {
            DataTable result = new DataTable();
            try
            {
                using (SqlConnection conn = new SqlConnection(SqlBuilder.ConnectionString))
                {
                    //string cmd_txt = string.Format(@"select * from information_schema.tables;");
                    conn.Open();
                    using (SqlCommand command = new SqlCommand(query, conn))
                    {
                        using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                        {
                            adapter.Fill(result);
                        }
                    }
                }
            }
            catch (SqlException ex)
            {

            }
            return result;
        }

        //get all collums in table
        public DataTable GetListCollumName(string tableName)
        {
            var result = new DataTable();
            StringBuilder query = new StringBuilder();
            query.Append("    SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = N'@tablename';    ");
            query.Replace("@tablename", tableName);
            result = GetDataSqlToTable(query.ToString());
            return result;
        }


        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~SqlServerMethod()
        // {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion

    }
}
