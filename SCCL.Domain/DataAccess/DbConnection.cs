using System.Data.SqlClient;

namespace SCCL.Domain.DataAccess
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