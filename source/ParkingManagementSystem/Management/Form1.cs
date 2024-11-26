using Oracle.DataAccess.Client;
using System;
using System.Data;
using System.Windows.Forms;

namespace Management
{
    public partial class Form1 : Form
    {
        private int vehicleId; // Vehicle ID field
        private string commandType; // Command type field (Add, Update, Delete)
        private OracleConnection oracleConnection = new OracleConnection();

        public int GetVehicleId
        {
            get { return vehicleId; }
        }

        public string GetCommandType
        {
            get { return commandType; }
        }

        private void ShowVehicleData() // Custom function to display vehicle data
        {
            try
            {
                oracleConnection.ConnectionString = "User Id=hong1;Password=1111;Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=localhost)(PORT=1521))(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME=xe)))";
                oracleConnection.Open();
                OracleDataAdapter oracleDataAdapter = new OracleDataAdapter();
                oracleDataAdapter.SelectCommand = new OracleCommand("SELECT * FROM Vehicle", oracleConnection);
                DataTable dataTable = new DataTable();
                oracleDataAdapter.Fill(dataTable);
                oracleConnection.Close();
                DBGrid.DataSource = dataTable;
                DBGrid.AutoResizeColumns();
                DBGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                DBGrid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                DBGrid.AllowUserToAddRows = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error occurred: " + ex.ToString());
                oracleConnection.Close();
            }
        }

        private void SearchVehicleByNumber(string lastFourDigits) // Custom function to search vehicle by last 4 digits
        {
            try
            {
                oracleConnection.ConnectionString = "User Id=hong1;Password=1111;Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=localhost)(PORT=1521))(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME=xe)))";
                oracleConnection.Open();
                OracleDataAdapter oracleDataAdapter = new OracleDataAdapter();
                oracleDataAdapter.SelectCommand = new OracleCommand("SELECT * FROM Vehicle WHERE SUBSTR(vehicle_number, -4) = :lastFourDigits", oracleConnection);
                oracleDataAdapter.SelectCommand.Parameters.Add("lastFourDigits", OracleDbType.Varchar2).Value = lastFourDigits;
                DataTable dataTable = new DataTable();
                oracleDataAdapter.Fill(dataTable);
                oracleConnection.Close();
                DBGrid.DataSource = dataTable;
                DBGrid.AutoResizeColumns();
                DBGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                DBGrid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                DBGrid.AllowUserToAddRows = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error occurred: " + ex.ToString());
                oracleConnection.Close();
            }
        }
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ShowVehicleData();
        }

        private void 선택차량수정ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            commandType = "Update";
            vehicleId = Convert.ToInt32(DBGrid.SelectedCells[0].Value);
            Form2 form2 = new Form2(this);
            form2.ShowDialog();
            form2.Dispose();
            ShowVehicleData();
        }

        private void 선택차량삭제ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            commandType = "Delete";
            vehicleId = Convert.ToInt32(DBGrid.SelectedCells[0].Value);
            Form2 form2 = new Form2(this);
            form2.ShowDialog();
            form2.Dispose();
            ShowVehicleData();
        }

        private void 선택형업데이트ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            commandType = "Add";
            Form2 form2 = new Form2(this);
            form2.ShowDialog();
            form2.Dispose();
            ShowVehicleData();
        }

        private void searchByVehicleNumber_Click(object sender, EventArgs e)
        {
            string lastFourDigits = txtLastFourDigits.Text.Trim();
            if (!string.IsNullOrEmpty(lastFourDigits))
            {
                SearchVehicleByNumber(lastFourDigits);
            }
            else
            {
                MessageBox.Show("Please enter the last 4 digits of the vehicle number.");
            }
        }

        private void searchAllVehicles_Click(object sender, EventArgs e)
        {
            ShowVehicleData();
        }
    }
}