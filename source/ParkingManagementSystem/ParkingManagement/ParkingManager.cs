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
        public void UpdateParkingStatus(int spotNumber, bool isOccupied, int vehicleId = -1) // 수정됨: vehicleNumber 대신 vehicleId 사용
        {
            try
            {
                if (parkingTable == null)
                {
                    MessageBox.Show("Parking table is not initialized. Please call OpenDatabase() first.", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // vehicleId로 vehicle_number 가져오기
                string vehicleNumber = null;
                if (isOccupied && vehicleId != -1)
                {
                    using (OracleConnection connection = new OracleConnection(connectionString))
                    {
                        connection.Open();
                        string query = "SELECT vehicle_number FROM Vehicle WHERE vehicle_id = :vehicleId";
                        using (OracleCommand command = new OracleCommand(query, connection))
                        {
                            command.Parameters.Add("vehicleId", OracleDbType.Int32).Value = vehicleId;
                            using (OracleDataReader reader = command.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    vehicleNumber = reader["vehicle_number"].ToString();
                                }
                                else
                                {
                                    MessageBox.Show($"차량 ID '{vehicleId}'에 해당하는 차량을 찾을 수 없습니다.", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    return;
                                }
                            }
                        }
                    }
                }

                DataRow row = parkingTable.Rows.Find(spotNumber);
                if (row != null)
                {
                    row["is_occupied"] = isOccupied ? 1 : 0;

                    // 차량 번호 및 vehicle_id 업데이트
                    if (isOccupied && !string.IsNullOrEmpty(vehicleNumber))
                    {
                        row["vehicle_number"] = vehicleNumber; // 수정됨: vehicle_number 저장
                        row["vehicle_id"] = vehicleId; // 수정됨: vehicle_id 저장
                    }
                    else
                    {
                        row["vehicle_number"] = DBNull.Value; // 주차 공간 비우기 시 차량 번호 삭제
                        row["vehicle_id"] = DBNull.Value; // 주차 공간 비우기 시 vehicle_id 삭제
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


        // 차량 ID와 차량 번호로 영수증 기록 저장
        public void InsertReceiptRecord(int vehicleId, string vehicleNumber, DateTime entryTime)
        {
            try
            {
                using (OracleConnection connection = new OracleConnection(connectionString))
                {
                    connection.Open();

                    // Receipt에 데이터 삽입 (vehicle_id와 vehicle_number 모두 저장)
                    string query = @"
INSERT INTO Receipt 
(receipt_id, vehicle_id, vehicle_number, parking_fee_before_discount, discount_amount, total_fee, parking_duration, start_time)
VALUES 
(ReceiptSeq.NEXTVAL, :vehicle_id, :vehicle_number, 0, 0, 0, 0, TO_DATE(:start_time, 'YYYY-MM-DD HH24:MI:SS'))";

                    using (OracleCommand command = new OracleCommand(query, connection))
                    {
                        // 최신 vehicle_id를 이용해 데이터 저장
                        command.Parameters.Add("vehicle_id", OracleDbType.Int32).Value = vehicleId;
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
        public bool IsVehicleParked(int vehicleId)
        {
            try
            {
                if (parkingTable != null)
                {
                    foreach (DataRow row in parkingTable.Rows)
                    {
                        if (row["vehicle_id"] != DBNull.Value && Convert.ToInt32(row["vehicle_id"]) == vehicleId)
                        {
                            return true; // 이미 주차 중인 차량
                        }
                    }
                }
                else
                {
                    // parkingTable이 초기화되지 않았을 경우 메시지 추가
                    MessageBox.Show("Parking table is not initialized. Please call OpenDatabase() first.", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"차량 확인 중 오류 발생: {ex.Message}", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return false;
        }


        // 신규 차량 등록
        public int RegisterVehicle(string vehicleNumber, string vehicleType) // 수정됨: 등록된 차량 ID를 반환하도록 변경
        {
            try
            {
                using (OracleConnection connection = new OracleConnection(connectionString))
                {
                    connection.Open();

                    // 시퀀스를 사용하여 새로운 vehicle_id 생성
                    int vehicleId;
                    string getIdQuery = "SELECT VehicleSeq.NEXTVAL FROM dual";
                    using (OracleCommand getIdCommand = new OracleCommand(getIdQuery, connection))
                    {
                        vehicleId = Convert.ToInt32(getIdCommand.ExecuteScalar());
                    }

                    // 새로 생성된 vehicle_id와 차량 데이터를 Vehicle 테이블에 삽입
                    string insertQuery = @"
                INSERT INTO Vehicle (vehicle_id, vehicle_number, vehicle_type)
                VALUES (:vehicle_id, :vehicle_number, :vehicle_type)";

                    using (OracleCommand insertCommand = new OracleCommand(insertQuery, connection))
                    {
                        insertCommand.Parameters.Add("vehicle_id", OracleDbType.Int32).Value = vehicleId;
                        insertCommand.Parameters.Add("vehicle_number", OracleDbType.Varchar2).Value = vehicleNumber;
                        insertCommand.Parameters.Add("vehicle_type", OracleDbType.Varchar2).Value = vehicleType;

                        insertCommand.ExecuteNonQuery();
                    }

                    MessageBox.Show("차량 번호와 차종이 성공적으로 등록되었습니다.", "등록 완료", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    return vehicleId; // 새로 등록된 vehicle_id 반환
                }
            }
            catch (Exception ex)
            {
                throw new Exception("신규 차량 등록 중 알 수 없는 오류 발생: " + ex.Message);
            }
        }


        // 차량 번호로 차량 ID 조회
        public int GetVehicleIdByNumber(string vehicleNumber)
        {
            try
            {
                using (OracleConnection connection = new OracleConnection(connectionString))
                {
                    connection.Open();

                    // vehicle_number로 vehicle_id 조회, 최신 vehicle_id가 반환되도록 정렬
                    string query = @"
            SELECT vehicle_id 
            FROM Vehicle 
            WHERE vehicle_number = :vehicleNumber
            ORDER BY vehicle_id DESC"; // 최신 vehicle_id 먼저 조회하도록 내림차순 정렬

                    using (OracleCommand command = new OracleCommand(query, connection))
                    {
                        command.Parameters.Add("vehicleNumber", OracleDbType.Varchar2).Value = vehicleNumber;

                        using (OracleDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return Convert.ToInt32(reader["vehicle_id"]); // 최신 차량 ID 반환
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




        // 차량 정보 가져오기
        public VehicleDetails GetVehicleDetails(int vehicleId)
        {
            try
            {
                using (OracleConnection connection = new OracleConnection(connectionString))
                {
                    connection.Open();
                    string query = @"
    SELECT 
        v.vehicle_id,
        v.vehicle_number,
        v.vehicle_type, 
        p.spot_number AS parking_spot, 
        r.start_time
    FROM Vehicle v
    LEFT JOIN ParkingSpot p ON v.vehicle_id = p.vehicle_id
    LEFT JOIN Receipt r ON v.vehicle_id = r.vehicle_id
    WHERE v.vehicle_id = :vehicleId";

                    using (OracleCommand command = new OracleCommand(query, connection))
                    {
                        command.Parameters.Add("vehicleId", OracleDbType.Int32).Value = vehicleId;

                        using (OracleDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return new VehicleDetails
                                {
                                    VehicleId = vehicleId, // 차량 ID 설정 (쿼리에서 직접 가져오기 대신 매개변수 사용)
                                    VehicleNumber = reader["vehicle_number"].ToString(), // 차량 번호 설정
                                    VehicleType = reader["vehicle_type"].ToString(), // 차량 종류 설정
                                    ParkingSpot = reader["parking_spot"] != DBNull.Value ? Convert.ToInt32(reader["parking_spot"]) : -1, // 주차 공간 번호 설정
                                    EntryTime = reader["start_time"] != DBNull.Value
                                        ? Convert.ToDateTime(reader["start_time"])
                                        : DateTime.MinValue // 입차 시간 설정
                                };
                            }
                            else
                            {
                                MessageBox.Show($"차량 ID '{vehicleId}'에 해당하는 데이터를 찾을 수 없습니다.", "정보", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"데이터를 로드하는 중 오류 발생: {ex.Message}", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return null;
        }


        public class VehicleDetails
        {
            public int VehicleId { get; set; } // 차량 ID 추가
            public string VehicleNumber { get; set; } // 차량 번호
            public string VehicleType { get; set; } // 차량 종류
            public int ParkingSpot { get; set; } // 주차 공간 번호
            public DateTime EntryTime { get; set; } // 입차 시간
        }
    }
}