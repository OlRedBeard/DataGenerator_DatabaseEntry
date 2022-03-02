using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.SQLite;
using System.Data;
using System.IO;

namespace DataLayers
{
    public class SQLiteDatalayer
    {
        string connectionString = ConfigurationManager.ConnectionStrings["Sqlite"].ConnectionString;

        public DataTable GetTableData(string tableName)
        {
            DataTable ret = new DataTable();

            string query = $"SELECT * FROM {tableName}";

            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(connectionString))
                {
                    conn.Open();
                    SQLiteCommand cmd = new SQLiteCommand(query, conn);
                    SQLiteDataAdapter adapter = new SQLiteDataAdapter(cmd);
                    adapter.Fill(ret);
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                ret = null;
            }
            finally
            {
                
            }

            return ret;
        }

        public void ClearTableData(string tableName)
        {
            string query = $"DELETE * FROM {tableName}";

            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(connectionString))
                {
                    conn.Open();
                    SQLiteCommand cmd = new SQLiteCommand(query, conn);
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
            catch
            {

            }
        }

        public void WriteLine(string id, string type, DateTime tStamp, decimal uom1, decimal uom2)
        {
            string query = "";

            switch (type)
            {
                case "ELECTRIC":
                    query = $"INSERT INTO Electric VALUES ('{id}', '{tStamp}', {uom1}, {uom2})";
                    break;
                case "GPS":
                    query = $"INSERT INTO GPS VALUES ('{id}', '{tStamp}', {uom1}, {uom2})";
                    break;
                case "GAS":
                    query = $"INSERT INTO Gas VALUES ('{id}', '{tStamp}', {uom1}, {uom2})";
                    break;
                case "H2O":
                    query = $"INSERT INTO Water VALUES ('{id}', '{tStamp}', {uom1}, {uom2})";
                    break;
            }

            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(connectionString))
                {
                    conn.Open();
                    SQLiteCommand cmd = new SQLiteCommand(query, conn);
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
            catch
            {

            }
        }
    }
}
