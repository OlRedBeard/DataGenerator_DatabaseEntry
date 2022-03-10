using System;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;

namespace DataLayers
{
    public class SQLServerDataLayer
    {
        string connectionString = ConfigurationManager.ConnectionStrings["SQLServer"].ConnectionString;
        public static bool connected = false;

        public SQLServerDataLayer()
        {
            TestConnection();
        }

        public bool TestConnection()
        {
            SqlConnection conn = new SqlConnection(connectionString);

            try
            {
                conn.Open();
                connected = true;
            }
            catch
            {
                connected = false;
            }
            finally
            {
                conn.Close();
            }

            return connected;
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
                    query = $"INSERT INTO Water VALUES ('{id}', {tStamp}, {uom1}, {uom2})";
                    break;
            }

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    connected = true;
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (SqlException se)
            {
                if (se.Number != 2627)
                    connected = false;
            }
        }
    }
}
