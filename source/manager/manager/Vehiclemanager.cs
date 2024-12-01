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

namespace manager
{
    public partial class Vehiclemanager : Form
    {
        private readonly string connectionString = "User Id=ParkingAdmin; Password=1111; Data Source=(DESCRIPTION = (ADDRESS = (PROTOCOL = TCP)(HOST = localhost)(PORT = 1521)) (CONNECT_DATA = (SERVER = DEDICATED) (SERVICE_NAME = xe)))";
        private string commandMode; // 추가, 삭제, 수정 모드
        private int selectedVehicleId; // 선택된 Vehicle ID
        public Vehiclemanager()
        {
            InitializeComponent();
            LoadDataGrid();
        }
        private void LoadDataGrid()
        {
            try
            {
                using (var connection = new OracleConnection(connectionString))
                {
                    connection.Open();
                    var query = "SELECT * FROM Vehicle ORDER BY vehicle_id";
                    var adapter = new OracleDataAdapter(query, connection);
                    var dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    dataGridView1.DataSource = dataTable;
                    dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                    dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                    dataGridView1.AllowUserToAddRows = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"데이터 로드 중 오류 발생: {ex.Message}");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string lastFourDigits = textBox1.Text.Trim();
            if (string.IsNullOrWhiteSpace(lastFourDigits))
            {
                MessageBox.Show("Please enter the last 4 digits of the vehicle number.");
                return;
            }

            using (OracleConnection connection = new OracleConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "SELECT * FROM Vehicle WHERE SUBSTR(vehicle_number, -4) = :lastFourDigits";
                    OracleCommand command = new OracleCommand(query, connection);
                    command.Parameters.Add(new OracleParameter(":lastFourDigits", lastFourDigits));

                    OracleDataAdapter adapter = new OracleDataAdapter(command);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    dataGridView1.DataSource = dataTable;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error searching data: " + ex.Message);
                }
            }
        }

        private void 차량추가ToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            commandMode = "추가";
            using (var form2 = new VehicleCRUD(connectionString, commandMode))
            {
                form2.ShowDialog();
            }
            LoadDataGrid(); // 데이터 새로고침
        }

        private void 차량삭제ToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("삭제할 차량을 선택하세요.");
                return;
            }

            selectedVehicleId = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["vehicle_id"].Value);
            commandMode = "삭제";

            using (var form2 = new VehicleCRUD(connectionString, commandMode, selectedVehicleId))
            {
                form2.ShowDialog();
            }
            LoadDataGrid(); // 데이터 새로고침
        }

        private void 차량수정ToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("수정할 차량을 선택하세요.");
                return;
            }

            selectedVehicleId = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["vehicle_id"].Value);
            commandMode = "수정";

            using (var form2 = new VehicleCRUD(connectionString, commandMode, selectedVehicleId))
            {
                form2.ShowDialog();
            }
            LoadDataGrid(); // 데이터 새로고침
        }

        private void 차량영수증ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // DataGridView에서 선택된 차량 ID 가져오기
            if (dataGridView1.SelectedRows.Count > 0)
            {
                // 선택된 행의 vehicle_id 가져오기
                int vehicleId = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["vehicle_id"].Value);

                // VehicleReceipt 폼 생성 및 데이터 로드
                VehicleReceipt vehicleReceiptForm = new VehicleReceipt(connectionString);
                vehicleReceiptForm.LoadReceipts(vehicleId);

                // 폼 표시
                vehicleReceiptForm.Show();
            }
            else
            {
                MessageBox.Show("차량을 선택하세요.", "알림", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
