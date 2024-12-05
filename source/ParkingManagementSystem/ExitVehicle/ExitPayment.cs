using System;
using System.Windows.Forms;
using Oracle.DataAccess.Client;

namespace ExitVehicle
{
    public partial class ExitPayment : Form
    {
        private string connectionString = "User Id=ParkingAdmin; Password=1111; Data Source=(DESCRIPTION = (ADDRESS = (PROTOCOL = TCP)(HOST = localhost)(PORT = 1521)) (CONNECT_DATA = (SERVER = DEDICATED) (SERVICE_NAME = xe)));";
        private int vehicleId;
        private string vehicleNumber;
        private Timer timer; // 시간을 표시하기 위한 Timer 추가

        // vehicleId와 vehicleNumber를 모두 받는 생성자로 수정
        public ExitPayment(int vehicleId, string vehicleNumber)
        {
            InitializeComponent();
            // 폼의 시작 위치를 화면 중앙으로 설정
            this.StartPosition = FormStartPosition.CenterScreen;

            if (vehicleId <= 0)
            {
                throw new ArgumentException("차량 ID가 유효하지 않습니다.");
            }

            if (string.IsNullOrEmpty(vehicleNumber))
            {
                throw new ArgumentException("차량 번호가 유효하지 않습니다.");
            }

            this.vehicleId = vehicleId; // vehicleId 설정
            this.vehicleNumber = vehicleNumber.Trim().ToUpper();
            this.Load += ExitPayment_Load;

            // lblCurrentTime을 폼 로드 시 바로 현재 시간으로 초기화
            lblCurrentTime.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            InitializeTimer(); // 타이머 초기화 및 시작
        }

        // Timer 초기화 메서드
        private void InitializeTimer()
        {
            timer = new Timer
            {
                Interval = 1000 // 1초마다 갱신
            };
            timer.Tick += (sender, e) =>
            {
                lblCurrentTime.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            };
            timer.Start(); // 타이머 시작
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

                    string query = @"
                SELECT start_time 
                FROM (
                    SELECT start_time 
                    FROM Receipt 
                    WHERE vehicle_id = :vehicleId
                    ORDER BY start_time DESC
                ) t
                WHERE ROWNUM = 1";

                    using (OracleCommand command = new OracleCommand(query, connection))
                    {
                        command.Parameters.Add("vehicleId", OracleDbType.Int32).Value = vehicleId;

                        using (OracleDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                DateTime startTime = reader.GetDateTime(0);
                                DateTime currentTime = DateTime.Now;

                                decimal totalFee = CalculateParkingFee(startTime, currentTime, vehicleId);

                                lblStartTime.Text = $"{startTime:yyyy-MM-dd HH:mm}";
                                lblEndTime.Text = $"{currentTime:yyyy-MM-dd HH:mm}";
                                lblTotalFee.Text = $"{totalFee:C}";
                            }
                            else
                            {
                                MessageBox.Show("입차 정보를 찾을 수 없습니다.", "오류", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                this.Close();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"입차 정보를 로드하는 중 오류가 발생했습니다: {ex.Message}", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private decimal CalculateParkingFee(DateTime startTime, DateTime endTime, int vehicleId)
        {
            TimeSpan duration = endTime - startTime;
            int totalMinutes = (int)Math.Ceiling(duration.TotalMinutes);
            decimal parkingFee = 0;

            try
            {
                using (OracleConnection connection = new OracleConnection(connectionString))
                {
                    connection.Open();

                    string typeQuery = @"SELECT vehicle_type FROM Vehicle WHERE vehicle_id = :vehicleId";
                    using (OracleCommand typeCommand = new OracleCommand(typeQuery, connection))
                    {
                        typeCommand.Parameters.Add("vehicleId", OracleDbType.Int32).Value = vehicleId;
                        object vehicleTypeResult = typeCommand.ExecuteScalar();

                        if (vehicleTypeResult != null)
                        {
                            string vehicleType = vehicleTypeResult.ToString();
                            decimal ratePerMinute = vehicleType == "Compact" ? 50 : 100;
                            parkingFee = totalMinutes * ratePerMinute;
                        }
                        else
                        {
                            MessageBox.Show("차량 유형 정보를 찾을 수 없습니다.", "오류", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return 0;
                        }
                    }

                    string discountQuery = @"SELECT NVL(discount_percentage, 0) FROM StoreDiscount WHERE vehicle_id = :vehicleId";
                    using (OracleCommand discountCommand = new OracleCommand(discountQuery, connection))
                    {
                        discountCommand.Parameters.Add("vehicleId", OracleDbType.Int32).Value = vehicleId;
                        object result = discountCommand.ExecuteScalar();

                        if (result != null)
                        {
                            decimal discountPercentage = Convert.ToDecimal(result);
                            if (discountPercentage > 0)
                            {
                                decimal discountFactor = 1 - (discountPercentage / 100);
                                parkingFee *= discountFactor;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"요금 계산 중 오류가 발생했습니다: {ex.Message}", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return parkingFee;
        }




        private void btnCompletePayment_Click(object sender, EventArgs e)
        {
            try
            {
                using (OracleConnection connection = new OracleConnection(connectionString))
                {
                    connection.Open();

                    string selectQuery = @"
                SELECT start_time 
                FROM (
                    SELECT start_time 
                    FROM Receipt 
                    WHERE vehicle_id = :vehicleId
                    ORDER BY start_time DESC
                ) t
                WHERE ROWNUM = 1";

                    DateTime startTime;

                    using (OracleCommand selectCommand = new OracleCommand(selectQuery, connection))
                    {
                        selectCommand.Parameters.Add("vehicleId", OracleDbType.Int32).Value = vehicleId;

                        using (OracleDataReader reader = selectCommand.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                startTime = reader.GetDateTime(0);
                            }
                            else
                            {
                                MessageBox.Show("해당 차량의 데이터를 찾을 수 없습니다.", "오류", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }
                        }
                    }

                    // 요금 계산
                    DateTime endTime = DateTime.Now;
                    decimal parkingFeeBeforeDiscount = CalculateParkingFee(startTime, endTime, vehicleId);

                    // 할인율 및 매장 정보 가져오기
                    string storeName = null;
                    decimal discountPercentage = 0;

                    string discountQuery = @"
                SELECT NVL(discount_percentage, 0) AS discount_percentage, store_name
                FROM StoreDiscount
                WHERE vehicle_id = :vehicleId";

                    using (OracleCommand discountCommand = new OracleCommand(discountQuery, connection))
                    {
                        discountCommand.Parameters.Add("vehicleId", OracleDbType.Int32).Value = vehicleId;
                        using (OracleDataReader reader = discountCommand.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                discountPercentage = reader["discount_percentage"] != DBNull.Value
                                    ? Convert.ToDecimal(reader["discount_percentage"])
                                    : 0;
                                storeName = reader["store_name"] != DBNull.Value
                                    ? reader["store_name"].ToString()
                                    : null;
                            }
                        }
                    }

                    decimal discountAmount = parkingFeeBeforeDiscount * (discountPercentage / 100);
                    decimal totalFee = parkingFeeBeforeDiscount - discountAmount;

                    // Receipt 업데이트
                    string updateReceiptQuery = @"
                UPDATE Receipt 
                SET parking_fee_before_discount = :parkingFeeBeforeDiscount,
                    discount_amount = :discountAmount,
                    total_fee = :totalFee,
                    parking_duration = :parkingDuration,
                    end_time = :endTime,
                    store_name = :storeName
                WHERE vehicle_id = :vehicleId";

                    using (OracleCommand updateReceiptCommand = new OracleCommand(updateReceiptQuery, connection))
                    {
                        updateReceiptCommand.Parameters.Add("parkingFeeBeforeDiscount", OracleDbType.Decimal).Value = parkingFeeBeforeDiscount;
                        updateReceiptCommand.Parameters.Add("discountAmount", OracleDbType.Decimal).Value = discountAmount;
                        updateReceiptCommand.Parameters.Add("totalFee", OracleDbType.Decimal).Value = totalFee;
                        updateReceiptCommand.Parameters.Add("parkingDuration", OracleDbType.Int32).Value = (int)Math.Ceiling((endTime - startTime).TotalMinutes);
                        updateReceiptCommand.Parameters.Add("endTime", OracleDbType.Date).Value = endTime;
                        updateReceiptCommand.Parameters.Add("storeName", OracleDbType.Varchar2).Value = storeName ?? "--";
                        updateReceiptCommand.Parameters.Add("vehicleId", OracleDbType.Int32).Value = vehicleId;

                        int rowsUpdated = updateReceiptCommand.ExecuteNonQuery();

                        if (rowsUpdated <= 0)
                        {
                            MessageBox.Show("Receipt 테이블 업데이트 실패!", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }

                    // ParkingSpot 초기화
                    string updateParkingSpotQuery = @"
                UPDATE ParkingSpot 
                SET IS_OCCUPIED = 0, 
                    VEHICLE_ID = NULL, 
                    VEHICLE_NUMBER = NULL
                WHERE VEHICLE_ID = :vehicleId";

                    using (OracleCommand updateParkingSpotCommand = new OracleCommand(updateParkingSpotQuery, connection))
                    {
                        updateParkingSpotCommand.Parameters.Add("vehicleId", OracleDbType.Int32).Value = vehicleId;
                        updateParkingSpotCommand.ExecuteNonQuery();
                    }

                    MessageBox.Show("출차 및 결제가 완료되었습니다.", "결제 완료", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // 다음 화면 이동
                    ReceiptDetail receiptDetail = new ReceiptDetail(vehicleId, vehicleNumber);
                    receiptDetail.Show();
                    this.Hide();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"출차 및 결제 처리 중 오류가 발생했습니다: {ex.Message}", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ExitPayment_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit(); // 애플리케이션 종료
        }
    }
}
