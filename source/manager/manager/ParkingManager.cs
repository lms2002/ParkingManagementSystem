using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Oracle.DataAccess.Client;

namespace manager
{
    internal class ParkingManager
    {
        // 필드
        private string connectionString;
        private OracleDataAdapter dBAdapter;
        private DataSet dS;
        private DataTable parkingTable;

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
        public void UpdateParkingStatus(int spotNumber, bool isOccupied, int vehicleId = -1)
        {
            try
            {
                if (parkingTable == null)
                {
                    throw new Exception("Parking table is not initialized. Please call OpenDatabase() first.");
                }

                DataRow row = parkingTable.Rows.Find(spotNumber);
                if (row != null)
                {
                    row["is_occupied"] = isOccupied ? 1 : 0;

                    if (isOccupied && vehicleId != -1)
                    {
                        row["vehicle_id"] = vehicleId;
                    }
                    else
                    {
                        row["vehicle_id"] = DBNull.Value;
                    }

                    // UpdateCommand 설정
                    OracleCommandBuilder commandBuilder = new OracleCommandBuilder(dBAdapter);
                    dBAdapter.Update(dS, "ParkingSpot");
                    dS.AcceptChanges();
                }
                else
                {
                    throw new Exception($"주차 공간 {spotNumber}번을 찾을 수 없습니다.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"주차 상태 업데이트 중 오류 발생: {ex.Message}");
            }
        }



        // 차량 번호로 차량 ID 조회
        public int GetVehicleIdBySpotNumber(int spotNumber)
        {
            try
            {
                using (OracleConnection connection = new OracleConnection(connectionString))
                {
                    connection.Open();

                    string query = "SELECT vehicle_id FROM ParkingSpot WHERE spot_number = :spotNumber AND is_occupied = 1";
                    using (OracleCommand command = new OracleCommand(query, connection))
                    {
                        command.Parameters.Add("spotNumber", OracleDbType.Int32).Value = spotNumber;

                        using (OracleDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return Convert.ToInt32(reader["vehicle_id"]);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"차량 ID를 조회하는 중 오류 발생: {ex.Message}", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return -1; // 조회 실패 시 -1 반환
        }
    }
}
