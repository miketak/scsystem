using System.Data.SqlClient;

namespace SCCL.Infrastructure
{
    internal class DbConnection
    {
        internal static SqlConnection GetConnection()
        {
            const string connString = @"Data Source=localhost;Initial Catalog=SCSYSDB;Integrated Security=True";
            //const string connString = @"Data Source=184.168.47.15;Integrated Security=False;User ID=mtakrama;Password=Mtstap43@1;Connect Timeout=15;Encrypt=False;Packet Size=4096";
            //const string connString = @"Server=tcp:scsystem.database.windows.net,1433;Initial Catalog=scsysdb;Persist Security Info=False;User ID=mtakrama;Password=password@1;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
            var conn = new SqlConnection(connString);
            return conn;
        }
    }
}