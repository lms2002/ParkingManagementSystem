using System;
using System.Data;
using System.Windows.Forms;
using Oracle.DataAccess.Client;

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
                MessageBox.Show($"주차 상태 업데이트 중 오류 발생: {ex.Message}", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

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

            return -1;
        }
        public int AddVehicle(string vehicleNumber, string vehicleType)
        {
            string query = "INSERT INTO Vehicle (vehicle_id, vehicle_number, vehicle_type) VALUES (VehicleSeq.NEXTVAL, :vehicleNumber, :vehicleType) RETURNING vehicle_id INTO :vehicleId";

            try
            {
                using (OracleConnection connection = new OracleConnection(connectionString))
                {
                    connection.Open();

                    using (OracleCommand command = new OracleCommand(query, connection))
                    {
                        command.Parameters.Add("vehicleNumber", OracleDbType.Varchar2).Value = vehicleNumber;
                        command.Parameters.Add("vehicleType", OracleDbType.Varchar2).Value = vehicleType;

                        OracleParameter vehicleIdParam = new OracleParameter("vehicleId", OracleDbType.Int32);
                        vehicleIdParam.Direction = ParameterDirection.Output;
                        command.Parameters.Add(vehicleIdParam);

                        command.ExecuteNonQuery();

                        return Convert.ToInt32(vehicleIdParam.Value); // 생성된 vehicle_id 반환
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"차량 추가 중 오류 발생: {ex.Message}");
            }
        }

        public string GetVehicleNumberByVehicleId(int vehicleId)
        {
            string query = "SELECT vehicle_number FROM Vehicle WHERE vehicle_id = :vehicle_id";
            try
            {
                using (OracleConnection connection = new OracleConnection(connectionString))
                {
                    connection.Open();

                    using (var command = new OracleCommand(query, connection))
                    {
                        command.Parameters.Add(new OracleParameter("vehicle_id", vehicleId));

                        var result = command.ExecuteScalar();
                        return result != null ? result.ToString() : string.Empty;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"차량 번호를 조회하는 중 오류 발생: {ex.Message}", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return string.Empty;
        }

    }
}
