using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace BatterySwapStationManagement
{
    public partial class BatteryInventoryForm : Form
    {
        private string selectedBatteryId = null;

        public BatteryInventoryForm()
        {
            InitializeComponent();
            LoadInventory();
        }

        private void LoadInventory()
        {
            try
            {
                using (var conn = Database.GetConnection())
                {
                    string query = "SELECT * FROM Batteries";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    SqlDataReader reader = cmd.ExecuteReader();

                    listViewInventory.Items.Clear();
                    while (reader.Read())
                    {
                        listViewInventory.Items.Add(new ListViewItem(new[]
                        {
                            reader["Id"].ToString(),
                            reader["Status"].ToString(),
                            reader["ChargeLevel"].ToString()
                        }));
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading inventory: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

   
        private void btnAddBattery_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtStatus.Text) || string.IsNullOrWhiteSpace(txtCharge.Text))
            {
                MessageBox.Show("Please fill in all fields!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!int.TryParse(txtCharge.Text, out int chargeLevel))
            {
                MessageBox.Show("Charge level must be a valid number!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                using (var conn = Database.GetConnection())
                {
                    string query = "INSERT INTO Batteries (Status, ChargeLevel) VALUES (@status, @charge)";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@status", txtStatus.Text.Trim());
                    cmd.Parameters.AddWithValue("@charge", chargeLevel);
                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Battery added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ClearInputs();
                    LoadInventory();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error adding battery: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        
        private void listViewInventory_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            if (listViewInventory.SelectedItems.Count > 0)
            {
                ListViewItem item = listViewInventory.SelectedItems[0];
                selectedBatteryId = item.SubItems[0].Text; 
                txtStatus.Text = item.SubItems[1].Text;    
                txtCharge.Text = item.SubItems[2].Text;    
            }
        }

       
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            
            if (string.IsNullOrEmpty(selectedBatteryId))
            {
                MessageBox.Show("Please click on a row in the list first!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtStatus.Text) || string.IsNullOrWhiteSpace(txtCharge.Text))
            {
                MessageBox.Show("Please fill in all fields!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!int.TryParse(txtCharge.Text, out int chargeLevel))
            {
                MessageBox.Show("Charge level must be a valid number!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                using (var conn = Database.GetConnection())
                {
                    string query = "UPDATE Batteries SET Status = @status, ChargeLevel = @charge WHERE Id = @id";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@status", txtStatus.Text.Trim());
                    cmd.Parameters.AddWithValue("@charge", chargeLevel);
                    cmd.Parameters.AddWithValue("@id", selectedBatteryId);
                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Battery updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ClearInputs();
                    LoadInventory();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating battery: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        
        private void btnDelete_Click(object sender, EventArgs e)
        {
           
            if (string.IsNullOrEmpty(selectedBatteryId))
            {
                MessageBox.Show("Please click on a row in the list first!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var confirm = MessageBox.Show($"Are you sure you want to delete battery ID: {selectedBatteryId}?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (confirm == DialogResult.Yes)
            {
                try
                {
                    using (var conn = Database.GetConnection())
                    {
                        string query = "DELETE FROM Batteries WHERE Id = @id";
                        SqlCommand cmd = new SqlCommand(query, conn);
                        cmd.Parameters.AddWithValue("@id", selectedBatteryId);
                        cmd.ExecuteNonQuery();

                        MessageBox.Show("Battery removed successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ClearInputs();
                        LoadInventory();
                    }
                }
                catch (SqlException)
                {
                    MessageBox.Show("Cannot delete this battery because it is linked to active transactions!", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void ClearInputs()
        {
            txtStatus.Clear();
            txtCharge.Clear();
            selectedBatteryId = null;
        }

        private void BatteryInventoryForm_Load(object sender, EventArgs e)
        {

        }
    }
}
