using System.Data.SqlClient;

namespace BatterySwapStationManagement
{
    public class Database
    {
        private static string connectionString =
            @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=BatterySwapDB;Integrated Security=True;TrustServerCertificate=True;";

        public static SqlConnection GetConnection()
        {
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();
            return conn;
        }
    }
}