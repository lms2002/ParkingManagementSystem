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
    public partial class VehicleCRUD : Form
    {
        private readonly string connectionString;
        private readonly string commandMode; // 추가, 삭제, 수정 모드
        private readonly int vehicleId; // 선택된 Vehicle ID
        public VehicleCRUD(string connectionString, string commandMode, int vehicleId = -1)
        {
            InitializeComponent();
            this.connectionString = connectionString;
            this.commandMode = commandMode;
            this.vehicleId = vehicleId;
            InitializeForm();
        }
        private void InitializeForm()
        {
            if (commandMode == "추가")
            {
                btnOK.Text = "추가";
                txtId.Text = GenerateNextVehicleId().ToString();
                txtId.ReadOnly = true;
            }
            else if (commandMode == "삭제" || commandMode == "수정")
            {
                LoadVehicleDetails();
                txtId.ReadOnly = true;

                if (commandMode == "삭제")
                {
                    txtNumber.ReadOnly = true;
                    txtType.ReadOnly = true;
                    btnOK.Text = "삭제";
                }
                else if (commandMode == "수정")
                {
                    txtNumber.ReadOnly = false;
                    txtType.ReadOnly = false;
                    btnOK.Text = "수정";
                }
            }
        }

        private int GenerateNextVehicleId()
        {
            using (var connection = new OracleConnection(connectionString))
            {
                connection.Open();
                var query = "SELECT NVL(MAX(vehicle_id), 0) + 1 AS next_id FROM Vehicle";
                var command = new OracleCommand(query, connection);

                return Convert.ToInt32(command.ExecuteScalar());
            }
        }
        private void LoadVehicleDetails()
        {
            using (var connection = new OracleConnection(connectionString))
            {
                connection.Open();
                var query = "SELECT * FROM Vehicle WHERE vehicle_id = :id";
                using (var command = new OracleCommand(query, connection))
                {
                    command.Parameters.Add(new OracleParameter("id", vehicleId));

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            txtId.Text = reader["vehicle_id"].ToString();
                            txtNumber.Text = reader["vehicle_number"].ToString();
                            txtType.Text = reader["vehicle_type"].ToString();
                        }
                    }
                }
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                using (var connection = new OracleConnection(connectionString))
                {
                    connection.Open();
                    if (btnOK.Text == "삭제")
                    {
                        try
                        {
                            using (var transaction = connection.BeginTransaction()) // 트랜잭션 시작
                            {
                                // Receipt 테이블에서 연결된 데이터 삭제
                                var deleteReceiptQuery = "DELETE FROM Receipt WHERE vehicle_id = :vehicle_id";
                                using (var deleteReceiptCommand = new OracleCommand(deleteReceiptQuery, connection))
                                {
                                    deleteReceiptCommand.Parameters.Add(new OracleParameter(":vehicle_id", vehicleId));
                                    deleteReceiptCommand.ExecuteNonQuery();
                                }

                                // StoreDiscount 테이블에서 연결된 데이터 삭제
                                var deleteDiscountQuery = "DELETE FROM StoreDiscount WHERE vehicle_id = :vehicle_id";
                                using (var deleteDiscountCommand = new OracleCommand(deleteDiscountQuery, connection))
                                {
                                    deleteDiscountCommand.Parameters.Add(new OracleParameter(":vehicle_id", vehicleId));
                                    deleteDiscountCommand.ExecuteNonQuery();
                                }

                                // ParkingSpot 테이블에서 연결된 차량 데이터 초기화
                                var updateParkingSpotQuery = "UPDATE ParkingSpot SET vehicle_id = NULL, vehicle_number = NULL, is_occupied = 0 WHERE vehicle_id = :vehicle_id";
                                using (var updateSpotCommand = new OracleCommand(updateParkingSpotQuery, connection))
                                {
                                    updateSpotCommand.Parameters.Add(new OracleParameter(":vehicle_id", vehicleId));
                                    updateSpotCommand.ExecuteNonQuery();
                                }

                                // Vehicle 테이블에서 데이터 삭제
                                var deleteVehicleQuery = "DELETE FROM Vehicle WHERE vehicle_id = :vehicle_id";
                                using (var deleteVehicleCommand = new OracleCommand(deleteVehicleQuery, connection))
                                {
                                    deleteVehicleCommand.Parameters.Add(new OracleParameter(":vehicle_id", vehicleId));
                                    deleteVehicleCommand.ExecuteNonQuery();
                                }

                                transaction.Commit(); // 트랜잭션 커밋
                                MessageBox.Show("차량과 관련된 모든 데이터가 성공적으로 삭제되었습니다.");
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"삭제 중 오류 발생: {ex.Message}");
                        }
                    }
                    else if (btnOK.Text == "수정")
                    {
                        try
                        {
                            using (var transaction = connection.BeginTransaction()) // 트랜잭션 시작
                            {
                                // ParkingSpot 테이블에서 차량 번호 업데이트
                                var updateParkingSpotQuery = "UPDATE ParkingSpot SET vehicle_number = :vehicle_number WHERE vehicle_id = :vehicle_id";
                                using (var updateSpotCommand = new OracleCommand(updateParkingSpotQuery, connection))
                                {
                                    updateSpotCommand.Parameters.Add(new OracleParameter(":vehicle_number", txtNumber.Text.Trim()));
                                    updateSpotCommand.Parameters.Add(new OracleParameter(":vehicle_id", vehicleId));
                                    updateSpotCommand.ExecuteNonQuery();
                                }

                                // Receipt 테이블에서 차량 번호 업데이트
                                var updateReceiptQuery = "UPDATE Receipt SET vehicle_number = :vehicle_number WHERE vehicle_id = :vehicle_id";
                                using (var updateReceiptCommand = new OracleCommand(updateReceiptQuery, connection))
                                {
                                    updateReceiptCommand.Parameters.Add(new OracleParameter(":vehicle_number", txtNumber.Text.Trim()));
                                    updateReceiptCommand.Parameters.Add(new OracleParameter(":vehicle_id", vehicleId));
                                    updateReceiptCommand.ExecuteNonQuery();
                                }

                                // StoreDiscount 테이블은 vehicle_number 컬럼이 없으므로 업데이트 생략

                                // Vehicle 테이블에서 차량 번호 및 차량 유형 업데이트
                                var updateVehicleQuery = "UPDATE Vehicle SET vehicle_number = :vehicle_number, vehicle_type = :vehicle_type WHERE vehicle_id = :vehicle_id";
                                using (var updateVehicleCommand = new OracleCommand(updateVehicleQuery, connection))
                                {
                                    updateVehicleCommand.Parameters.Add(new OracleParameter(":vehicle_number", txtNumber.Text.Trim()));
                                    updateVehicleCommand.Parameters.Add(new OracleParameter(":vehicle_type", txtType.Text.Trim()));
                                    updateVehicleCommand.Parameters.Add(new OracleParameter(":vehicle_id", vehicleId));
                                    updateVehicleCommand.ExecuteNonQuery();
                                }

                                transaction.Commit(); // 트랜잭션 커밋
                                MessageBox.Show("차량 정보와 관련된 데이터가 성공적으로 수정되었습니다.");
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"수정 중 오류 발생: {ex.Message}");
                        }
                    }

                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"작업 중 오류 발생: {ex.Message}");
            }
        }

        private void VehicleCRUD_Load(object sender, EventArgs e)
        {

        }
    }
}
