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
    public partial class Form2 : Form
    {
        private readonly string connectionString = "User Id=ParkingAdmin; Password=1111; Data Source=(DESCRIPTION = (ADDRESS = (PROTOCOL = TCP)(HOST = localhost)(PORT = 1521)) (CONNECT_DATA = (SERVER = DEDICATED) (SERVICE_NAME = xe)))";
        private readonly string vehicleNumber;
        public Form2(string vehicleNumber)
        {
            InitializeComponent();
            this.vehicleNumber = vehicleNumber;
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(vehicleNumber))
            {
                textBoxVehicleNumber.Text = vehicleNumber;
                textBoxVehicleNumber.ReadOnly = true; // 수정 불가 설정
            }
            else
            {
                MessageBox.Show("차량 번호를 전달받지 못했습니다.");
            }
        }

        private void buttonMoveVehicle_Click(object sender, EventArgs e)
        {
            // 새 주차 공간 번호 입력 확인
            if (string.IsNullOrWhiteSpace(textBoxNewSpot.Text) || !int.TryParse(textBoxNewSpot.Text, out int newSpotNumber))
            {
                MessageBox.Show("유효한 새 주차 공간 번호를 입력하세요.");
                return;
            }

            if (string.IsNullOrEmpty(vehicleNumber))
            {
                MessageBox.Show("차량 번호가 없습니다.");
                return;
            }

            try
            {
                using (var connection = new OracleConnection(connectionString))
                {
                    connection.Open();

                    // 새 주차 공간이 비어 있는지 확인
                    var checkQuery = "SELECT COUNT(*) FROM ParkingSpot WHERE spot_number = :newSpot AND is_occupied = 0";
                    using (var checkCommand = new OracleCommand(checkQuery, connection))
                    {
                        checkCommand.Parameters.Add(new OracleParameter("newSpot", newSpotNumber));
                        var count = Convert.ToInt32(checkCommand.ExecuteScalar());

                        if (count == 0)
                        {
                            MessageBox.Show("새 주차 공간이 비어 있지 않거나 존재하지 않습니다.");
                            return;
                        }
                    }

                    // 차량을 새 주차 공간으로 이동
                    var moveQuery = @"
                UPDATE ParkingSpot 
                SET is_occupied = 1, 
                    vehicle_id = (SELECT vehicle_id FROM Vehicle WHERE vehicle_number = :vehicleNumber),
                    vehicle_number = :vehicleNumber 
                WHERE spot_number = :newSpot
                  AND is_occupied = 0"; // 추가 조건

                    using (var moveCommand = new OracleCommand(moveQuery, connection))
                    {
                        moveCommand.Parameters.Add(new OracleParameter("vehicleNumber", vehicleNumber));
                        moveCommand.Parameters.Add(new OracleParameter("newSpot", newSpotNumber));
                        moveCommand.ExecuteNonQuery();
                    }

                    // 기존 주차 공간 비우기
                    var clearQuery = @"
                UPDATE ParkingSpot 
                SET is_occupied = 0, 
                    vehicle_id = NULL, 
                    vehicle_number = NULL 
                WHERE vehicle_number = :vehicleNumber
                  AND is_occupied = 1"; // 추가 조건

                    using (var clearCommand = new OracleCommand(clearQuery, connection))
                    {
                        clearCommand.Parameters.Add(new OracleParameter("vehicleNumber", vehicleNumber));
                        clearCommand.ExecuteNonQuery();
                    }

                    MessageBox.Show("차량이 성공적으로 이동되었습니다.");
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"차량 이동 중 오류 발생: {ex.Message}");
            }
        }

    }
}
