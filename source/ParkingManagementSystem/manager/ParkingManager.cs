using System;
using System.Data;
using System.Windows.Forms;
using Oracle.DataAccess.Client;
using Oracle.DataAccess.Types;

namespace manager
{
    public class ParkingManager
    {
        private string connectionString;
        private OracleDataAdapter dBAdapter;
        private DataSet dS;
        private DataTable parkingTable;

        public ParkingManager(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public void OpenDatabase()
        {
            try
            {
                string query = "SELECT * FROM ParkingSpot";
                dBAdapter = new OracleDataAdapter(query, connectionString);
                dS = new DataSet();
                dBAdapter.Fill(dS, "ParkingSpot");
                parkingTable = dS.Tables["ParkingSpot"];
                parkingTable.PrimaryKey = new DataColumn[] { parkingTable.Columns["spot_number"] };
            }
            catch (Exception ex)
            {
                MessageBox.Show($"DB 연결 중 오류 발생: {ex.Message}", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public DataTable GetParkingTable()
        {
            return parkingTable;
        }

        public void UpdateParkingStatus(int spotNumber, bool isOccupiedParam, int vehicleIdParam = -1, string vehicleNumberParam = null)
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
                    int occupiedStatus = isOccupiedParam ? 1 : 0;
                    int vehicleIdentifier = vehicleIdParam;
                    string vehiclePlateNumber = vehicleNumberParam ?? string.Empty;

                    row["is_occupied"] = occupiedStatus;
                    row["vehicle_id"] = vehicleIdentifier != -1 ? (object)vehicleIdentifier : DBNull.Value;
                    row["vehicle_number"] = !string.IsNullOrEmpty(vehiclePlateNumber) ? (object)vehiclePlateNumber : DBNull.Value;

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


        public int GetVehicleIdBySpotNumber(int spotNumber)
        {
            string query = "SELECT vehicle_id FROM ParkingSpot WHERE spot_number = :spotNumber";
            try
            {
                using (OracleConnection connection = new OracleConnection(connectionString))
                {
                    connection.Open();
                    using (var command = new OracleCommand(query, connection))
                    {
                        command.Parameters.Add("spotNumber", OracleDbType.Int32).Value = spotNumber;
                        var result = command.ExecuteScalar();
                        return result != null ? Convert.ToInt32(result) : -1;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"차량 ID 조회 중 오류 발생: {ex.Message}", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return -1;
            }
        }

        public string GetVehicleNumberByVehicleId(int vehicleId)
        {
            string query = "SELECT vehicle_number FROM Vehicle WHERE vehicle_id = :vehicleId";
            try
            {
                using (OracleConnection connection = new OracleConnection(connectionString))
                {
                    connection.Open();
                    using (var command = new OracleCommand(query, connection))
                    {
                        command.Parameters.Add("vehicleId", OracleDbType.Int32).Value = vehicleId;
                        var result = command.ExecuteScalar();
                        return result?.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"차량 번호 조회 중 오류 발생: {ex.Message}", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return string.Empty;
            }
        }

        public void HandleReceiptEntry(int vehicleId, string vehicleNumber)
        {
            string query = "INSERT INTO Receipt (receipt_id, vehicle_id, vehicle_number, start_time, parking_fee_before_discount, discount_amount, total_fee, parking_duration) " +
                           "VALUES (ReceiptSeq.NEXTVAL, :vehicleId, :vehicleNumber, :startTime, 0, 0, 0, 0)";

            try
            {
                using (OracleConnection connection = new OracleConnection(connectionString))
                {
                    connection.Open();
                    using (OracleCommand command = new OracleCommand(query, connection))
                    {
                        command.Parameters.Add("vehicleId", OracleDbType.Int32).Value = vehicleId;
                        command.Parameters.Add("vehicleNumber", OracleDbType.Varchar2).Value = vehicleNumber;
                        command.Parameters.Add("startTime", OracleDbType.Date).Value = DateTime.Now;

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"영수증 생성 중 오류 발생: {ex.Message}");
            }
        }


        public void HandleReceiptExit(int vehicleId)
        {
            string query = "UPDATE Receipt SET parking_fee_before_discount = :parkingFee, " +
                           "discount_amount = :discount, total_fee = :totalFee, parking_duration = :duration, " +
                           "end_time = :endTime WHERE vehicle_id = :vehicleId";

            try
            {
                using (OracleConnection connection = new OracleConnection(connectionString))
                {
                    connection.Open();

                    DateTime startTime = GetReceiptStartTime(vehicleId);
                    DateTime endTime = DateTime.Now;
                    int duration = (int)(endTime - startTime).TotalHours + 1; // 최소 1시간으로 계산
                    double parkingFee = duration * 100; // 시간당 100원
                    double discount = 0; // 할인 없음
                    double totalFee = parkingFee - discount;

                    using (OracleCommand command = new OracleCommand(query, connection))
                    {
                        command.Parameters.Add("parkingFee", OracleDbType.Double).Value = parkingFee;
                        command.Parameters.Add("discount", OracleDbType.Double).Value = discount;
                        command.Parameters.Add("totalFee", OracleDbType.Double).Value = totalFee;
                        command.Parameters.Add("duration", OracleDbType.Int32).Value = duration;
                        command.Parameters.Add("endTime", OracleDbType.Date).Value = endTime;
                        command.Parameters.Add("vehicleId", OracleDbType.Int32).Value = vehicleId;

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"영수증 완료 처리 중 오류 발생: {ex.Message}");
            }
        }


        private DateTime GetReceiptStartTime(int vehicleId)
        {
            string query = "SELECT start_time FROM Receipt WHERE vehicle_id = :vehicleId AND end_time IS NULL";

            try
            {
                using (OracleConnection connection = new OracleConnection(connectionString))
                {
                    connection.Open();

                    using (OracleCommand command = new OracleCommand(query, connection))
                    {
                        command.Parameters.Add("vehicleId", OracleDbType.Int32).Value = vehicleId;

                        var result = command.ExecuteScalar();
                        if (result != null)
                        {
                            return Convert.ToDateTime(result);
                        }
                        else
                        {
                            throw new Exception("영수증 시작 시간을 찾을 수 없습니다.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"영수증 시작 시간 조회 중 오류 발생: {ex.Message}");
            }
        }
        public int AddVehicle(string vehicleNumber, string vehicleType)
        {
            string query = "INSERT INTO Vehicle (vehicle_id, vehicle_number, vehicle_type) " +
                           "VALUES (VehicleSeq.NEXTVAL, :vehicleNumber, :vehicleType) " +
                           "RETURNING vehicle_id INTO :vehicleId";

            try
            {
                using (OracleConnection connection = new OracleConnection(connectionString))
                {
                    connection.Open();
                    using (OracleCommand command = new OracleCommand(query, connection))
                    {
                        command.Parameters.Add("vehicleNumber", OracleDbType.Varchar2).Value = vehicleNumber;
                        command.Parameters.Add("vehicleType", OracleDbType.Varchar2).Value = vehicleType;

                        // RETURNING 값 처리
                        OracleParameter vehicleIdParam = new OracleParameter("vehicleId", OracleDbType.Decimal);
                        vehicleIdParam.Direction = ParameterDirection.Output;
                        command.Parameters.Add(vehicleIdParam);

                        command.ExecuteNonQuery();

                        // 반환된 OracleDecimal 값을 int로 변환
                        int vehicleId = Convert.ToInt32(((OracleDecimal)vehicleIdParam.Value).Value);
                        return vehicleId;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"차량 추가 중 오류 발생: {ex.Message}");
            }
        }
    }
}
