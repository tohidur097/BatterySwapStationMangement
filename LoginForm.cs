using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace BatterySwapStationManagement
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            using (var conn = Database.GetConnection())
            {
                string query = "SELECT * FROM Users WHERE Username=@user AND Password=@pass";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@user", txtUsername.Text);
                cmd.Parameters.AddWithValue("@pass", txtPassword.Text);

                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    string role = reader["Role"].ToString();
                    MessageBox.Show("Login successful!");
                    DashboardForm dashboard = new DashboardForm(role);
                    dashboard.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Invalid credentials!");
                }
            }
        }
    }
}
