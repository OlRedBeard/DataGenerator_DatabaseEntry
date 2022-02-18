using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.SQLite;

namespace DataLayers
{
    public class SQLiteDatalayer
    {
        string connectionString = ConfigurationManager.ConnectionStrings["Sqlite"].ConnectionString;
    }
}
