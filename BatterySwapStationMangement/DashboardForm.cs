using BatterySwapStationMangement;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BatterySwapStationManagement
{
    public partial class DashboardForm : Form
    {
        private string userRole;

        public DashboardForm(string role)
        {
            InitializeComponent();
            userRole = role;
            lblRole.Text = "Logged in as: " + role;

            
            if (role == "Employee")
            {
                btnInventory.Enabled = false;   
                btnReports.Enabled = false;     
            }
        }

        private void btnInventory_Click(object sender, EventArgs e)
        {
            BatteryInventoryForm inventory = new BatteryInventoryForm();
            inventory.Show();
        }

        private void btnBilling_Click(object sender, EventArgs e)
        {
            BillingForm billing = new BillingForm();
            billing.Show();
        }

        private void btnReports_Click(object sender, EventArgs e)
        {
            ReportsForm reports = new ReportsForm();
            reports.Show();
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            LoginForm login = new LoginForm();
            login.Show();
            this.Close();
        }

        private void btnDriverManagement_Click(object sender, EventArgs e)
        {
            DriverManagementForm driverForm = new DriverManagementForm();
            driverForm.Show(); 
        }
    }
}
