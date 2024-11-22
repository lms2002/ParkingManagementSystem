using System;
using System.Windows.Forms;
using Oracle.DataAccess.Client;

namespace ExitVehicle
{
    public partial class ExitPayment : Form
    {
        private string connectionString = "User Id=ParkingAdmin; Password=1111; Data Source=(DESCRIPTION = (ADDRESS = (PROTOCOL = TCP)(HOST = localhost)(PORT = 1521)) (CONNECT_DATA = (SERVER = DEDICATED) (SERVICE_NAME = xe)));";
        private string vehicleNumber;

        public ExitPayment(string vehicleNumber)
        {
            InitializeComponent();

            if (string.IsNullOrEmpty(vehicleNumber))
            {
                throw new ArgumentException("차량 번호가 유효하지 않습니다.");
            }

            this.vehicleNumber = vehicleNumber.Trim().ToUpper();
            this.Load += ExitPayment_Load;
        }

        private void ExitPayment_Load(object sender, EventArgs e)
        {
            LoadPaymentDetails();
        }

        private void LoadPaymentDetails()
        {
            try
            {
                using (OracleConnection connection = new OracleConnection(connectionString))
                {
                    connection.Open();

                    UpdateExitTime();

                    string query = @"
                SELECT v.start_time, 
                       v.end_time, 
                       NVL(s.discount_percentage, 0) AS discount_percentage
                FROM Receipt v
                LEFT JOIN StoreDiscount s ON v.vehicle_number = s.vehicle_number
                WHERE UPPER(TRIM(v.vehicle_number)) = :vehicleNumber";

                    using (OracleCommand command = new OracleCommand(query, connection))
                    {
                        command.Parameters.Add("vehicleNumber", OracleDbType.Varchar2).Value = vehicleNumber.Trim().ToUpper();

                        using (OracleDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                DateTime startTime = reader.GetDateTime(0);
                                DateTime endTime = reader.GetDateTime(1);
                                decimal discountPercentage = reader.GetDecimal(2);

                                decimal parkingFee = CalculateParkingFee(startTime, endTime);
                                decimal discountedFee = parkingFee * (1 - discountPercentage / 100);

                                lblStartTime.Text = $"입차 시간: {startTime}";
                                lblEndTime.Text = $"출차 시간: {endTime}";
                                lblTotalFee.Text = $"총 요금: {discountedFee:C}";
                            }
                            else
                            {
                                MessageBox.Show("해당 차량 정보를 찾을 수 없습니다.", "오류", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                this.Close();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("결제 정보를 로드하는 중 오류가 발생했습니다: " + ex.Message, "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UpdateExitTime()
        {
            try
            {
                // 출차 시간 업데이트 전 해당 레코드 확인 쿼리
                string checkQuery = @"
        SELECT COUNT(*)
        FROM Receipt
        WHERE UPPER(TRIM(vehicle_number)) = :vehicleNumber AND end_time IS NULL";

                string updateQuery = @"
        UPDATE Receipt
        SET end_time = :endTime
        WHERE UPPER(TRIM(vehicle_number)) = :vehicleNumber AND end_time IS NULL";

                using (OracleConnection connection = new OracleConnection(connectionString))
                {
                    connection.Open();

                    // 먼저 레코드 존재 여부 확인
                    using (OracleCommand checkCommand = new OracleCommand(checkQuery, connection))
                    {
                        checkCommand.Parameters.Add("vehicleNumber", OracleDbType.Varchar2).Value = vehicleNumber.Trim().ToUpper();
                        object checkResult = checkCommand.ExecuteScalar();

                        if (checkResult == null || int.Parse(checkResult.ToString()) == 0)
                        {
                            // 레코드가 없으면 메시지 표시 후 종료
                            MessageBox.Show("출차 가능한 기록을 찾을 수 없습니다. 이미 출차된 차량일 수 있습니다.", "오류", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }

                    // 출차 시간 업데이트
                    using (OracleCommand updateCommand = new OracleCommand(updateQuery, connection))
                    {
                        updateCommand.Parameters.Add("endTime", OracleDbType.TimeStamp).Value = DateTime.Now;
                        updateCommand.Parameters.Add("vehicleNumber", OracleDbType.Varchar2).Value = vehicleNumber.Trim().ToUpper();

                        int rowsAffected = updateCommand.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("출차 시간이 성공적으로 업데이트되었습니다.", "성공", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("출차 시간이 업데이트되지 않았습니다. 조건을 확인해주세요.", "오류", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("출차 시간 업데이트 중 오류가 발생했습니다: " + ex.Message, "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }





        private decimal CalculateParkingFee(DateTime startTime, DateTime endTime)
        {
            // 총 분 계산
            TimeSpan duration = endTime - startTime;
            int totalMinutes = (int)Math.Ceiling(duration.TotalMinutes); // 올림 처리 (1초~59초 = 1분)

            // 1분당 1000원 요금
            return totalMinutes * 1000;
        }

        private void btnPay_Click(object sender, EventArgs e)
        {
            try
            {
                using (OracleConnection connection = new OracleConnection(connectionString))
                {
                    connection.Open();

                    // 주차 공간 상태 업데이트
                    string query = @"
                    UPDATE ParkingSpot
                    SET is_occupied = 0, vehicle_number = NULL
                    WHERE UPPER(vehicle_number) = :vehicleNumber";

                    using (OracleCommand command = new OracleCommand(query, connection))
                    {
                        command.Parameters.Add("vehicleNumber", OracleDbType.Varchar2).Value = vehicleNumber;
                        command.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("결제가 완료되었습니다.", "결제 완료", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close(); // 결제 창 닫기
            }
            catch (Exception ex)
            {
                MessageBox.Show("결제 처리 중 오류가 발생했습니다: " + ex.Message, "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
