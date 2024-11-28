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


namespace Parkingmanager
{
    public partial class Form1 : Form
    {
        private readonly string connectionString = "User Id=ParkingAdmin; Password=1111; Data Source=(DESCRIPTION = (ADDRESS = (PROTOCOL = TCP)(HOST = localhost)(PORT = 1521)) (CONNECT_DATA = (SERVER = DEDICATED) (SERVICE_NAME = xe)))";
        public Form1()
        {
            InitializeComponent();
            LoadParkingData();
            InitializeContextMenu();
        }
        private void LoadParkingData()
        {
            try
            {
                using (var connection = new OracleConnection(connectionString))
                {
                    connection.Open();
                    var query = "SELECT spot_number, is_disabled, is_occupied, vehicle_id, vehicle_number FROM ParkingSpot ORDER BY spot_number";
                    var adapter = new OracleDataAdapter(query, connection);
                    var dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    dataGridView1.DataSource = dataTable;
                    dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"데이터 로드 중 오류 발생: {ex.Message}");
            }
        }
        private void MoveVehicle(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("이동할 차량이 있는 주차 공간을 선택하세요.");
                return;
            }

            var selectedVehicleNumber = dataGridView1.SelectedRows[0].Cells["vehicle_number"].Value?.ToString();

            if (string.IsNullOrEmpty(selectedVehicleNumber))
            {
                MessageBox.Show("선택된 주차 공간에 차량이 없습니다.");
                return;
            }

            using (var form2 = new Form2(selectedVehicleNumber))
            {
                form2.ShowDialog();
            }

            LoadParkingData(); // 작업 완료 후 데이터 갱신
        }
        private void InitializeContextMenu()
        {
            var contextMenu = new ContextMenuStrip();
            contextMenu.Items.Add("차량 이동", null, MoveVehicle);
            contextMenu.Items.Add("차량 삭제", null, DeleteVehicle);
            dataGridView1.ContextMenuStrip = contextMenu;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // 텍스트 박스가 비어 있으면 전체 데이터를 로드
            if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                LoadParkingData();
                MessageBox.Show("전체 주차 상태를 다시 불러옵니다.");
                return;
            }

            try
            {
                using (var connection = new OracleConnection(connectionString))
                {
                    connection.Open();
                    var query = "SELECT spot_number, is_disabled, is_occupied, vehicle_id, vehicle_number FROM ParkingSpot WHERE vehicle_number LIKE :vehicle_number ORDER BY spot_number";
                    using (var command = new OracleCommand(query, connection))
                    {
                        command.Parameters.Add(new OracleParameter("vehicle_number", "%" + textBox1.Text.Trim()));

                        var adapter = new OracleDataAdapter(command);
                        var dataTable = new DataTable();
                        adapter.Fill(dataTable);

                        dataGridView1.DataSource = dataTable;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"검색 중 오류 발생: {ex.Message}");
            }
        }
        private void DeleteVehicle(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("삭제할 차량이 있는 주차 공간을 선택하세요.");
                return;
            }

            // 선택된 주차 공간의 spot_number 가져오기
            var selectedSpotNumber = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["spot_number"].Value);
            var vehicleNumber = dataGridView1.SelectedRows[0].Cells["vehicle_number"].Value?.ToString();

            if (string.IsNullOrEmpty(vehicleNumber))
            {
                MessageBox.Show("선택된 주차 공간에 차량이 없습니다.");
                return;
            }

            try
            {
                using (var connection = new OracleConnection(connectionString))
                {
                    connection.Open();

                    // 차량 삭제 쿼리 실행
                    var deleteQuery = @"
                UPDATE ParkingSpot
                SET is_occupied = 0, vehicle_id = NULL, vehicle_number = NULL
                WHERE spot_number = :spotNumber";

                    using (var command = new OracleCommand(deleteQuery, connection))
                    {
                        command.Parameters.Add(new OracleParameter("spotNumber", selectedSpotNumber));
                        command.ExecuteNonQuery();
                    }

                    MessageBox.Show("차량이 성공적으로 삭제되었습니다.");
                }

                // DataGridView 갱신
                LoadParkingData();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"차량 삭제 중 오류 발생: {ex.Message}");
            }
        }
    }
}
