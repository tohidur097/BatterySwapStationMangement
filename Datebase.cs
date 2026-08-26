using System.Data.SqlClient;

namespace BatterySwapStationManagement
{
    public class Database
    {
        private static string connectionString =
            "Server=FARHAN19\\SQLEXPRESS;Database=BatterySwapDB;Trusted_Connection=True;";

        public static SqlConnection GetConnection()
        {
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();
            return conn;
        }
    }
}
