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
            InitializeListView();
            LoadListView(); // 전체 데이터 초기 로드
        }


        private void InitializeListView()
        {
            listView1.View = View.Details;
            listView1.FullRowSelect = true;
            listView1.GridLines = true;

        }
        private void LoadListView(string filter = null)
        {
            listView1.Items.Clear(); // 기존 데이터 초기화
            try
            {
                using (var connection = new OracleConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT * FROM Vehicle";

                    if (!string.IsNullOrEmpty(filter))
                    {
                        query += " WHERE SUBSTR(vehicle_number, -4) = :lastFourDigits";
                    }

                    using (var command = new OracleCommand(query, connection))
                    {
                        if (!string.IsNullOrEmpty(filter))
                        {
                            command.Parameters.Add(new OracleParameter(":lastFourDigits", filter));
                        }

                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var item = new ListViewItem(reader["vehicle_id"].ToString());
                                item.SubItems.Add(reader["vehicle_number"].ToString());
                                item.SubItems.Add(reader["vehicle_type"].ToString());
                                listView1.Items.Add(item);
                            }
                        }
                    }
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
                LoadListView(); // 전체 데이터 다시 로드
            }
            else
            {
                LoadListView(lastFourDigits); // 필터 적용 데이터 로드
            }

            // 검색 후 텍스트박스 내용 초기화
            textBox1.Text = string.Empty;
        }

        private void 차량삭제ToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count == 0)
            {
                MessageBox.Show("삭제할 차량을 선택하세요.");
                return;
            }

            int selectedVehicleId = Convert.ToInt32(listView1.SelectedItems[0].SubItems[0].Text);

            using (var form2 = new VehicleCRUD(connectionString, "삭제", selectedVehicleId))
            {
                form2.ShowDialog();
            }
            LoadListView(); // 데이터 새로고침
        }

        private void 차량수정ToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count == 0)
            {
                MessageBox.Show("수정할 차량을 선택하세요.");
                return;
            }

            int selectedVehicleId = Convert.ToInt32(listView1.SelectedItems[0].SubItems[0].Text);

            using (var form2 = new VehicleCRUD(connectionString, "수정", selectedVehicleId))
            {
                form2.ShowDialog();
            }
            LoadListView(); // 데이터 새로고침
        }

        private void 차량영수증ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                int vehicleId = Convert.ToInt32(listView1.SelectedItems[0].SubItems[0].Text);

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
        private void DeleteVehicleWithDependencies(int vehicleId)
        {
            using (OracleConnection connection = new OracleConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    // ParkingSpot 테이블에서 연결된 차량 데이터 해제
                    string updateParkingSpotQuery = "UPDATE ParkingSpot SET vehicle_id = NULL, vehicle_number = NULL, is_occupied = 0 WHERE vehicle_id = :vehicleId";
                    using (OracleCommand updateSpotCommand = new OracleCommand(updateParkingSpotQuery, connection))
                    {
                        updateSpotCommand.Parameters.Add(new OracleParameter(":vehicleId", vehicleId));
                        updateSpotCommand.ExecuteNonQuery();
                    }

                    // Receipt 테이블에서 연결된 차량 데이터 삭제
                    string deleteReceiptQuery = "DELETE FROM Receipt WHERE vehicle_id = :vehicleId";
                    using (OracleCommand deleteReceiptCommand = new OracleCommand(deleteReceiptQuery, connection))
                    {
                        deleteReceiptCommand.Parameters.Add(new OracleParameter(":vehicleId", vehicleId));
                        deleteReceiptCommand.ExecuteNonQuery();
                    }

                    // Vehicle 테이블에서 차량 데이터 삭제
                    string deleteVehicleQuery = "DELETE FROM Vehicle WHERE vehicle_id = :vehicleId";
                    using (OracleCommand deleteVehicleCommand = new OracleCommand(deleteVehicleQuery, connection))
                    {
                        deleteVehicleCommand.Parameters.Add(new OracleParameter(":vehicleId", vehicleId));
                        deleteVehicleCommand.ExecuteNonQuery();
                    }

                    MessageBox.Show("차량과 관련 데이터를 성공적으로 삭제했습니다.", "성공", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"삭제 중 오류 발생: {ex.Message}", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private void UpdateVehicleWithDependencies(int vehicleId, string newVehicleNumber, string newVehicleType)
        {
            // 차량 번호 길이 제한 체크
            if (newVehicleNumber.Length > 10)
            {
                MessageBox.Show("차량 번호는 최대 10자까지 입력 가능합니다.", "입력 오류", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (OracleConnection connection = new OracleConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    // ParkingSpot 테이블에서 차량 번호 업데이트
                    string updateParkingSpotQuery = "UPDATE ParkingSpot SET vehicle_number = :newVehicleNumber WHERE vehicle_id = :vehicleId";
                    using (OracleCommand updateSpotCommand = new OracleCommand(updateParkingSpotQuery, connection))
                    {
                        updateSpotCommand.Parameters.Add(new OracleParameter(":newVehicleNumber", newVehicleNumber));
                        updateSpotCommand.Parameters.Add(new OracleParameter(":vehicleId", vehicleId));
                        updateSpotCommand.ExecuteNonQuery();
                    }

                    // Vehicle 테이블에서 차량 번호 및 차량 유형 업데이트
                    string updateVehicleQuery = "UPDATE Vehicle SET vehicle_number = :newVehicleNumber, vehicle_type = :newVehicleType WHERE vehicle_id = :vehicleId";
                    using (OracleCommand updateVehicleCommand = new OracleCommand(updateVehicleQuery, connection))
                    {
                        updateVehicleCommand.Parameters.Add(new OracleParameter(":newVehicleNumber", newVehicleNumber));
                        updateVehicleCommand.Parameters.Add(new OracleParameter(":newVehicleType", newVehicleType));
                        updateVehicleCommand.Parameters.Add(new OracleParameter(":vehicleId", vehicleId));
                        updateVehicleCommand.ExecuteNonQuery();
                    }

                    MessageBox.Show("차량 정보가 성공적으로 수정되었습니다.", "성공", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"수정 중 오류 발생: {ex.Message}", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
