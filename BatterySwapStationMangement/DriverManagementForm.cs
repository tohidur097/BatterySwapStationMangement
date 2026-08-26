using BatterySwapStationManagement;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace BatterySwapStationMangement
{
    public partial class DriverManagementForm : Form
    {
        private string selectedDriverId = null;

        public DriverManagementForm()
        {
            InitializeComponent();
            LoadDrivers();
        }

        // ডাটাবেজ থেকে ডাটা লোড করার মেথড
        private void LoadDrivers(string searchQuery = "")
        {
            try
            {
                using (var conn = Database.GetConnection())
                {
                    string query = "SELECT DriverId, Name, Phone, CardNumber, VehicleType, Balance, IsActive FROM Drivers";
                    if (!string.IsNullOrWhiteSpace(searchQuery))
                    {
                        query += " WHERE Name LIKE @search OR Phone LIKE @search OR CardNumber LIKE @search";
                    }

                    SqlCommand cmd = new SqlCommand(query, conn);
                    if (!string.IsNullOrWhiteSpace(searchQuery))
                    {
                        cmd.Parameters.AddWithValue("@search", "%" + searchQuery.Trim() + "%");
                    }

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    dataGridViewDrivers.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading drivers: " + ex.Message);
            }
        }

        // Add Logic
        private void ExecuteAdd()
        {
            if (string.IsNullOrWhiteSpace(txtName.Text) || string.IsNullOrWhiteSpace(txtPhone.Text) || string.IsNullOrWhiteSpace(txtCardNumber.Text))
            {
                MessageBox.Show("Name, Phone, and Card Number are required!");
                return;
            }

            decimal.TryParse(txtBalance.Text, out decimal balance);

            try
            {
                using (var conn = Database.GetConnection())
                {
                    string query = @"INSERT INTO Drivers (Name, Phone, CardNumber, VehicleType, Balance, IsActive) 
                                     VALUES (@name, @phone, @card, @vehicle, @balance, 1)";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@name", txtName.Text.Trim());
                    cmd.Parameters.AddWithValue("@phone", txtPhone.Text.Trim());
                    cmd.Parameters.AddWithValue("@card", txtCardNumber.Text.Trim());
                    cmd.Parameters.AddWithValue("@vehicle", txtVehicleType.Text.Trim());
                    cmd.Parameters.AddWithValue("@balance", balance);
                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Driver added successfully!");
                    ClearInputs();
                    LoadDrivers();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        // Update Logic
        private void ExecuteUpdate()
        {
            if (string.IsNullOrEmpty(selectedDriverId))
            {
                MessageBox.Show("Please select a driver from the list first!");
                return;
            }

            decimal.TryParse(txtBalance.Text, out decimal balance);

            try
            {
                using (var conn = Database.GetConnection())
                {
                    string query = @"UPDATE Drivers SET Name=@name, Phone=@phone, CardNumber=@card, VehicleType=@vehicle, Balance=@balance WHERE DriverId=@id";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@name", txtName.Text.Trim());
                    cmd.Parameters.AddWithValue("@phone", txtPhone.Text.Trim());
                    cmd.Parameters.AddWithValue("@card", txtCardNumber.Text.Trim());
                    cmd.Parameters.AddWithValue("@vehicle", txtVehicleType.Text.Trim());
                    cmd.Parameters.AddWithValue("@balance", balance);
                    cmd.Parameters.AddWithValue("@id", selectedDriverId);
                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Updated successfully!");
                    ClearInputs();
                    LoadDrivers();
                }
            }
            catch (Exception ex) { MessageBox.Show("Error: " + ex.Message); }
        }

        // Delete Logic
        private void ExecuteDelete()
        {
            if (string.IsNullOrEmpty(selectedDriverId))
            {
                MessageBox.Show("Please select a driver from the list to delete!");
                return;
            }

            try
            {
                using (var conn = Database.GetConnection())
                {
                    string query = "DELETE FROM Drivers WHERE DriverId = @id";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@id", selectedDriverId);
                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Deleted successfully!");
                    ClearInputs();
                    LoadDrivers();
                }
            }
            catch (Exception ex) { MessageBox.Show("Error: " + ex.Message); }
        }

        // Table Click Logic
        private void dataGridViewDrivers_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridViewDrivers.Rows[e.RowIndex];
                selectedDriverId = row.Cells["DriverId"].Value.ToString();
                txtName.Text = row.Cells["Name"].Value.ToString();
                txtPhone.Text = row.Cells["Phone"].Value.ToString();
                txtCardNumber.Text = row.Cells["CardNumber"].Value.ToString();
                txtVehicleType.Text = row.Cells["VehicleType"].Value.ToString();
                txtBalance.Text = row.Cells["Balance"].Value.ToString();
            }
        }

        // Clear and Back
        private void ClearInputs()
        {
            txtName.Clear(); txtPhone.Clear(); txtCardNumber.Clear();
            txtVehicleType.Clear(); txtBalance.Clear(); txtSearch.Clear();
            selectedDriverId = null;
        }

        // Button Event Handlers (ডিজাইনার থেকে যুক্ত হওয়া সব ভ্যারিয়েন্ট)
        private void btnAdd_Click(object sender, EventArgs e) => ExecuteAdd();
        private void btnAdd_Click_1(object sender, EventArgs e) => ExecuteAdd();
        private void button1_Click(object sender, EventArgs e) => ExecuteAdd();

        private void btnUpdate_Click(object sender, EventArgs e) => ExecuteUpdate();
        private void btnUpdate_Click_1(object sender, EventArgs e) => ExecuteUpdate();
        private void button2_Click(object sender, EventArgs e) => ExecuteUpdate();

        private void btnDelete_Click(object sender, EventArgs e) => ExecuteDelete();
        private void button3_Click(object sender, EventArgs e) => ExecuteDelete();

        private void btnSearch_Click(object sender, EventArgs e) => LoadDrivers(txtSearch.Text);
        private void btnClear_Click(object sender, EventArgs e) => ClearInputs();
        private void btnClear_Click_1(object sender, EventArgs e) => ClearInputs();
        private void btnBack_Click(object sender, EventArgs e) => this.Close();

        // লাইভ সার্চ (Search বক্স টাইপ করার সাথে সাথেই ফিল্টার হবে)
        private void txtSearch_TextChanged(object sender, EventArgs e) => LoadDrivers(txtSearch.Text);

        private void label6_Click(object sender, EventArgs e) { }
        private void DriverManagementForm_Load(object sender, EventArgs e)
        {
            this.Text = "Driver Management";
        }
    }
}