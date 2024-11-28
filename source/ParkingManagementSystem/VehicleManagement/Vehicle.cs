using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Oracle.DataAccess.Client;

namespace VehicleManagement
{
    public partial class Vehicle : Form
    {
        private int vehicleId; // 선택한 차량 ID
        private string command; // 작업 명령 (추가, 수정, 삭제)
        private OracleConnection conn = new OracleConnection("User Id=ParkingAdmin; Password=1111; Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=localhost)(PORT=1521))(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME=xe)));");

        public int GetVehicleId { get { return vehicleId; } }
        public string Command { get { return command; } }
        public Vehicle()
        {
            InitializeComponent();
        }

        private void Vehicle_Load(object sender, EventArgs e)
        {
            LoadAllVehicles();
        }
        private void LoadAllVehicles()
        {
            try
            {
                conn.Open();
                OracleDataAdapter adapter = new OracleDataAdapter("SELECT * FROM Vehicle", conn);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                conn.Close();

                rightDBGrid.DataSource = dt;
                rightDBGrid.AutoResizeColumns();
                rightDBGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                rightDBGrid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                rightDBGrid.AllowUserToAddRows = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("에러 발생: " + ex.Message);
                conn.Close();
            }
        }
        private void SearchVehiclesBySuffix(string suffix)
        {
            try
            {
                conn.Open();
                string query = "SELECT * FROM Vehicle WHERE SUBSTR(vehicle_number, -4) = :suffix";
                OracleDataAdapter adapter = new OracleDataAdapter(query, conn);
                adapter.SelectCommand.Parameters.Add("suffix", OracleDbType.Varchar2).Value = suffix;
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                conn.Close();

                leftDBGrid.DataSource = dt;
                leftDBGrid.AutoResizeColumns();
                leftDBGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
            catch (Exception ex)
            {
                MessageBox.Show("에러 발생: " + ex.Message);
                conn.Close();
            }
        }

        private void searchButton_Click(object sender, EventArgs e)
        {
            string suffix = suffixTextBox.Text.Trim();
            if (!string.IsNullOrEmpty(suffix))
            {
                SearchVehiclesBySuffix(suffix);
            }
        }

        private void addMenuItem_Click(object sender, EventArgs e)
        {
            command = "추가";
            VehicleCRUD vehicleCRUD = new VehicleCRUD(this);
            vehicleCRUD.ShowDialog();
            vehicleCRUD.Dispose();
            LoadAllVehicles();
        }

        private void updateMenuItem_Click(object sender, EventArgs e)
        {
            command = "수정";
            vehicleId = Convert.ToInt32(rightDBGrid.SelectedCells[0].Value);
            VehicleCRUD vehicleCRUD = new VehicleCRUD(this);
            vehicleCRUD.ShowDialog();
            vehicleCRUD.Dispose();
            LoadAllVehicles();
        }

        private void deleteMenuItem_Click(object sender, EventArgs e)
        {
            command = "삭제";
            vehicleId = Convert.ToInt32(rightDBGrid.SelectedCells[0].Value);
            VehicleCRUD vehicleCRUD = new VehicleCRUD(this);
            vehicleCRUD.ShowDialog();
            vehicleCRUD.Dispose();
            LoadAllVehicles();
        }
    }
}
