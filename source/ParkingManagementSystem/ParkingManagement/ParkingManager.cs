using System;
using System.Data;
using Oracle.DataAccess.Client;
using System.Windows.Forms;

namespace ParkingManagement
{
    internal class ParkingManager
    {
        // 필드
        private string connectionString;
        private OracleDataAdapter dBAdapter;
        private DataSet dS;
        private DataTable parkingTable;
        public ParkingManager() { } // 매개변수 없는 기본 생성자 추가

        // 생성자
        public ParkingManager(string connectionString)
        {
            this.connectionString = connectionString;
        }

        // 주차 데이터베이스 열기
        public void OpenDatabase()
        {
            try
            {
                string query = "SELECT * FROM ParkingSpot";

                // DataAdapter 및 DataSet 초기화
                dBAdapter = new OracleDataAdapter(query, connectionString);
                dS = new DataSet();
                dBAdapter.Fill(dS, "ParkingSpot");

                // DataTable 가져오기
                parkingTable = dS.Tables["ParkingSpot"];
                parkingTable.PrimaryKey = new DataColumn[] { parkingTable.Columns["spot_number"] }; // 기본 키 설정

            }
            catch (Exception ex)
            {
                MessageBox.Show($"DB 연결 중 오류 발생: {ex.Message}", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // 주차 공간 데이터 가져오기
        public DataTable GetParkingTable()
        {
            return parkingTable;
        }

        // 주차 상태 업데이트
        public void UpdateParkingStatus(int spotNumber, bool isOccupied, string vehicleNumber = null)
        {
            try
            {
                if (parkingTable == null)
                {
                    MessageBox.Show("Parking table is not initialized. Please call OpenDatabase() first.", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                DataRow row = parkingTable.Rows.Find(spotNumber);
                if (row != null)
                {
                    row["is_occupied"] = isOccupied ? 1 : 0;

                    // 차량 번호 업데이트
                    if (isOccupied && !string.IsNullOrEmpty(vehicleNumber))
                    {
                        row["vehicle_number"] = vehicleNumber;
                    }
                    else
                    {
                        row["vehicle_number"] = DBNull.Value;
                    }

                    // UpdateCommand 설정
                    OracleCommandBuilder commandBuilder = new OracleCommandBuilder(dBAdapter);

                    // Update 데이터베이스
                    dBAdapter.Update(dS, "ParkingSpot");
                    dS.AcceptChanges();
                }
                else
                {
                    MessageBox.Show($"주차 공간 {spotNumber}번을 찾을 수 없습니다.", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"주차 상태 업데이트 중 오류 발생: {ex.Message}", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void InsertReceiptRecord(string vehicleNumber, DateTime entryTime)
        {
            try
            {
                using (OracleConnection connection = new OracleConnection(connectionString))
                {
                    connection.Open();

                    string query = @"
        INSERT INTO Receipt 
        (receipt_id, vehicle_number, parking_fee_before_discount, discount_amount, total_fee, parking_duration, start_time)
        VALUES 
        (receipt_seq.NEXTVAL, :vehicle_number, 0, 0, 0, 0, TO_DATE(:start_time, 'YYYY-MM-DD HH24:MI:SS'))";

                    using (OracleCommand command = new OracleCommand(query, connection))
                    {
                        command.Parameters.Add("vehicle_number", OracleDbType.Varchar2).Value = vehicleNumber;
                        command.Parameters.Add("start_time", OracleDbType.Varchar2).Value = entryTime.ToString("yyyy-MM-dd HH:mm:ss");
                        command.BindByName = true;

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"입차 기록 저장 중 오류 발생: {ex.Message}", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        // 특정 차량이 주차 중인지 확인
        public bool IsVehicleParked(string vehicleNumber)
        {
            try
            {
                if (parkingTable != null)
                {
                    foreach (DataRow row in parkingTable.Rows)
                    {
                        if (row["vehicle_number"] != DBNull.Value && row["vehicle_number"].ToString() == vehicleNumber)
                        {
                            return true; // 이미 주차 중인 차량
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"차량 확인 중 오류 발생: {ex.Message}", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return false;
        }

        // 신규 차량 등록
        public void RegisterVehicle(string vehicleNumber, string vehicleType)
        {
            try
            {
                using (OracleConnection connection = new OracleConnection(connectionString))
                {
                    connection.Open();

                    string query = "INSERT INTO Vehicle (vehicle_number, vehicle_type) VALUES (:vehicle_number, :vehicle_type)";
                    using (OracleCommand command = new OracleCommand(query, connection))
                    {
                        command.Parameters.Add("vehicle_number", OracleDbType.Varchar2).Value = vehicleNumber;
                        command.Parameters.Add("vehicle_type", OracleDbType.Varchar2).Value = vehicleType;

                        command.ExecuteNonQuery();
                    }

                    MessageBox.Show("차량 번호와 차종이 등록되었습니다.", "등록 완료", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("신규 차량 등록 중 오류 발생: " + ex.Message);
            }
        }
    }
}
