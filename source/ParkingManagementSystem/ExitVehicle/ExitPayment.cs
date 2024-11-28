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

                    // Receipt 테이블에서 입차 시간 조회
                    string query = @"SELECT start_time
                             FROM Receipt
                             WHERE vehicle_id = :vehicleId";

                    using (OracleCommand command = new OracleCommand(query, connection))
                    {
                        command.Parameters.Add("vehicleId", OracleDbType.Int32).Value = vehicleId;

                        using (OracleDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                // 입차 시간 가져오기
                                DateTime startTime = reader.GetDateTime(0);
                                DateTime currentTime = DateTime.Now; // 현재 시간을 출차 시간으로 설정

                                // 주차 요금 계산
                                decimal totalFee = CalculateParkingFee(startTime, currentTime, vehicleId);

                                // 화면에 표시
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
            // 총 분 계산
            TimeSpan duration = endTime - startTime;
            int totalMinutes = (int)Math.Ceiling(duration.TotalMinutes); // 올림 처리 (1초~59초 = 1분)
            decimal parkingFee = totalMinutes * 1000; // 1분당 1000원 요금

            try
            {
                using (OracleConnection connection = new OracleConnection(connectionString))
                {
                    connection.Open();

                    // StoreDiscount에서 할인율 확인
                    string query = @"SELECT NVL(discount_percentage, 0)
                             FROM StoreDiscount
                             WHERE vehicle_id = :vehicleId";

                    using (OracleCommand command = new OracleCommand(query, connection))
                    {
                        command.Parameters.Add("vehicleId", OracleDbType.Int32).Value = vehicleId;
                        object result = command.ExecuteScalar();

                        if (result != null)
                        {
                            // 할인율 적용
                            decimal discountPercentage = Convert.ToDecimal(result);
                            decimal discountFactor = 1 - (discountPercentage / 100);
                            return parkingFee * discountFactor; // 할인율 적용 후 반환
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"할인 요금을 계산하는 중 오류가 발생했습니다: {ex.Message}", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            // StoreDiscount 테이블에 차량 정보가 없는 경우, 원래 요금을 반환
            return parkingFee;
        }


        private void btnCompletePayment_Click(object sender, EventArgs e)
        {
            try
            {
                using (OracleConnection connection = new OracleConnection(connectionString))
                {
                    connection.Open();

                    // 1. Receipt에서 기존 데이터 가져오기
                    string selectQuery = @"
            SELECT start_time 
            FROM Receipt 
            WHERE vehicle_id = :vehicleId";

                    DateTime startTime;

                    using (OracleCommand selectCommand = new OracleCommand(selectQuery, connection))
                    {
                        selectCommand.Parameters.Add("vehicleId", OracleDbType.Int32).Value = vehicleId;

                        using (OracleDataReader reader = selectCommand.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                startTime = reader.GetDateTime(0); // 입차 시간
                            }
                            else
                            {
                                MessageBox.Show("해당 차량의 데이터를 찾을 수 없습니다.", "오류", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }
                        }
                    }

                    // 2. 요금 계산
                    DateTime endTime = DateTime.Now; // 출차 시간
                    TimeSpan duration = endTime - startTime;
                    int parkingDuration = (int)Math.Ceiling(duration.TotalMinutes); // 총 주차 시간 (분 단위)

                    if (parkingDuration <= 0)
                    {
                        MessageBox.Show("주차 시간이 올바르지 않습니다.", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    decimal parkingFeeBeforeDiscount = parkingDuration * 1000; // 1분당 1000원 요금
                    decimal discountPercentage = 0;
                    decimal discountAmount = 0;
                    decimal totalFee = parkingFeeBeforeDiscount;

                    // 3. 할인율 가져오기
                    string discountQuery = @"
            SELECT NVL(discount_percentage, 0) 
            FROM StoreDiscount 
            WHERE vehicle_id = :vehicleId";

                    using (OracleCommand discountCommand = new OracleCommand(discountQuery, connection))
                    {
                        discountCommand.Parameters.Add("vehicleId", OracleDbType.Int32).Value = vehicleId;
                        object result = discountCommand.ExecuteScalar();

                        if (result != null)
                        {
                            discountPercentage = Convert.ToDecimal(result);
                            discountAmount = parkingFeeBeforeDiscount * (discountPercentage / 100); // 할인 금액
                            totalFee = parkingFeeBeforeDiscount - discountAmount; // 최종 요금
                        }
                    }

                    // 4. Receipt 테이블 업데이트
                    string updateReceiptQuery = @"
            UPDATE Receipt 
            SET parking_fee_before_discount = :parkingFeeBeforeDiscount,
                discount_amount = :discountAmount,
                total_fee = :totalFee,
                parking_duration = :parkingDuration,
                end_time = :endTime
            WHERE vehicle_id = :vehicleId";

                    using (OracleCommand updateReceiptCommand = new OracleCommand(updateReceiptQuery, connection))
                    {
                        updateReceiptCommand.Parameters.Add("parkingFeeBeforeDiscount", OracleDbType.Decimal).Value = parkingFeeBeforeDiscount;
                        updateReceiptCommand.Parameters.Add("discountAmount", OracleDbType.Decimal).Value = discountAmount;
                        updateReceiptCommand.Parameters.Add("totalFee", OracleDbType.Decimal).Value = totalFee;
                        updateReceiptCommand.Parameters.Add("parkingDuration", OracleDbType.Int32).Value = parkingDuration;
                        updateReceiptCommand.Parameters.Add("endTime", OracleDbType.Date).Value = endTime;
                        updateReceiptCommand.Parameters.Add("vehicleId", OracleDbType.Int32).Value = vehicleId;

                        int rowsUpdated = updateReceiptCommand.ExecuteNonQuery();

                        if (rowsUpdated <= 0)
                        {
                            MessageBox.Show("Receipt 테이블 업데이트 실패!", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }

                    // 5. 파킹스팟 초기화
                    string updateParkingSpotQuery = @"
    UPDATE ParkingSpot 
    SET IS_OCCUPIED = 0, 
        VEHICLE_ID = NULL, 
        VEHICLE_NUMBER = NULL
    WHERE VEHICLE_ID = :vehicleId";

                    using (OracleCommand updateParkingSpotCommand = new OracleCommand(updateParkingSpotQuery, connection))
                    {
                        updateParkingSpotCommand.Parameters.Add("vehicleId", OracleDbType.Int32).Value = vehicleId;
                        int rowsUpdated = updateParkingSpotCommand.ExecuteNonQuery();

                        if (rowsUpdated <= 0)
                        {
                            MessageBox.Show("파킹스팟 초기화 실패!", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }


                    MessageBox.Show("출차 및 결제가 완료되었습니다.", "결제 완료", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // 다음 화면으로 이동
                    ReceiptDetail receiptDetail = new ReceiptDetail(vehicleId, vehicleNumber);
                    receiptDetail.Show();
                    this.Hide(); // 현재 화면 숨기기
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
