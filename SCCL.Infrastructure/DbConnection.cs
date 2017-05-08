using System.Data.SqlClient;

namespace SCCL.Infrastructure
{
    internal class DbConnection
    {
        internal static SqlConnection GetConnection()
        {
            const string connString = @"Data Source=localhost;Initial Catalog=SCSYSDB;Integrated Security=True";
            var conn = new SqlConnection(connString);
            return conn;
        }
    }
}