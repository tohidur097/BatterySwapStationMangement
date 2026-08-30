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
    public partial class BillingForm : Form
    {
        public BillingForm()
        {
            InitializeComponent();
            LoadTransactions(); 
        }

        private void LoadTransactions()
        {
            try
            {
                using (var conn = Database.GetConnection())
                {
                    string query = "SELECT T.Id, U.Username, B.Status, T.Amount, T.Date " +
                                   "FROM Transactions T " +
                                   "JOIN Users U ON T.UserId = U.Id " +
                                   "JOIN Batteries B ON T.BatteryId = B.Id " +
                                   "ORDER BY T.Id DESC";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    dataGridViewBilling.DataSource = dt; 
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading transactions: " + ex.Message);
            }
        }

        private void btnGenerateBill_Click(object sender, EventArgs e)
        {
            
            if (!int.TryParse(txtUserId.Text, out int userId))
            {
                MessageBox.Show("Invalid User ID.");
                return;
            }
            if (!int.TryParse(txtBatteryId.Text, out int batteryId))
            {
                MessageBox.Show("Invalid Battery ID.");
                return;
            }
            if (!decimal.TryParse(txtAmount.Text, out decimal amount))
            {
                MessageBox.Show("Invalid Amount.");
                return;
            }

            using (var conn = Database.GetConnection())
            {
                
                using (var checkUser = new SqlCommand("SELECT COUNT(1) FROM Users WHERE Id = @id", conn))
                {
                    checkUser.Parameters.AddWithValue("@id", userId);
                    var userExists = (int)checkUser.ExecuteScalar() > 0;
                    if (!userExists)
                    {
                        MessageBox.Show($"User with Id {userId} does not exist.");
                        return;
                    }
                }

               
                using (var checkBattery = new SqlCommand("SELECT COUNT(1) FROM Batteries WHERE Id = @id", conn))
                {
                    checkBattery.Parameters.AddWithValue("@id", batteryId);
                    var batteryExists = (int)checkBattery.ExecuteScalar() > 0;
                    if (!batteryExists)
                    {
                        MessageBox.Show($"Battery with Id {batteryId} does not exist.");
                        return;
                    }
                }

                string query = "INSERT INTO Transactions (UserId, BatteryId, Amount, Date) VALUES (@user, @battery, @amount, @date)";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@user", userId);
                    cmd.Parameters.AddWithValue("@battery", batteryId);
                    cmd.Parameters.AddWithValue("@amount", amount);
                    cmd.Parameters.AddWithValue("@date", DateTime.Now);
                    try
                    {
                        cmd.ExecuteNonQuery();
                    }
                    catch (SqlException ex)
                    {
                        MessageBox.Show("Database error: " + ex.Message);
                        return;
                    }
                }
            }

            MessageBox.Show("Bill generated successfully!");

          
            txtUserId.Clear();
            txtBatteryId.Clear();
            txtAmount.Clear();

            LoadTransactions(); 
        }
    }
}
