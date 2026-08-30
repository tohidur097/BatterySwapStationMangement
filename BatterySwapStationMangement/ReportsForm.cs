using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BatterySwapStationManagement
{
    public partial class ReportsForm : Form
    {
        public ReportsForm()
        {
            InitializeComponent();
            LoadReports();
        }

        private void LoadReports()
        {
            using (var conn = Database.GetConnection())
            {
                
                string query = "SELECT SUM(Amount) FROM Transactions WHERE CAST(Date AS DATE) = CAST(GETDATE() AS DATE)";
                SqlCommand cmd = new SqlCommand(query, conn);
                object result = cmd.ExecuteScalar();
                lblTodayEarnings.Text = "Today's Earnings: " + (result != DBNull.Value ? result.ToString() : "0");

                
                query = "SELECT COUNT(*) FROM Transactions WHERE CAST(Date AS DATE) = CAST(GETDATE() AS DATE)";
                cmd = new SqlCommand(query, conn);
                result = cmd.ExecuteScalar();
                lblTodaySwaps.Text = "Today's Swaps: " + result.ToString();

               
                query = "SELECT Status, COUNT(*) FROM Batteries GROUP BY Status";
                cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();

                chartUsage.Series[0].Points.Clear();
                while (reader.Read())
                {
                    chartUsage.Series[0].Points.AddXY(reader[0].ToString(), Convert.ToInt32(reader[1]));
                }
            }
        }
    }
}
