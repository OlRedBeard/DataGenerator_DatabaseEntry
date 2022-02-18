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
                    SQLiteCommand cmd = new SQLiteCommand(query, conn);
                    SQLiteDataAdapter adapter = new SQLiteDataAdapter(cmd);
                    adapter.Fill(ret);
                }
            }
            catch (Exception ex)
            {
                ret = null;
            }

            return ret;
        }

        public void WriteDataFromFile(string fileName)
        {
            string query = "";
            string[] fileLines = File.ReadAllLines(fileName);
            foreach (string line in fileLines)
            {
                string id = line.Split(',')[0];
                string type = line.Split(',')[1];
                DateTime tStamp = Convert.ToDateTime(line.Split(',')[2]);
                decimal uom1 = Convert.ToDecimal(line.Split(',')[4]);
                decimal uom2 = Convert.ToDecimal(line.Split(',')[6]);

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
                    case "H20":
                        query = $"INSERT INTO Water VALUES ('{id}', '{tStamp}', {uom1}, {uom2})";
                        break;
                }
            }
        }
    }
}
