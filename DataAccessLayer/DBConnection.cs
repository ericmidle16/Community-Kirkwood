/// <summary>
/// Creator:Skyann Heintz
/// Created: 2025/02/02
/// Summary: Database connection class.
/// Last Upaded By:
/// Last Updated:
/// What Was Changed:
/// </summary>
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    internal static class DBConnection
    {
        /// <summary>
        /// Creator:Skyann Heintz
        /// Created: 2025/02/02
        /// Summary: Links the database to the code so that
        /// the database can be used.
        /// Last Upaded By:
        /// Last Updated:
        /// What Was Changed:
        /// </summary>
        internal static SqlConnection GetConnection()
        {
            SqlConnection conn = null;
            string connectionString = @"Data Source=localhost;Initial Catalog=communityDb;Integrated Security=True;Encrypt=True;Trust Server Certificate=True";
            conn = new SqlConnection(connectionString);
            return conn;
        }
    }
}