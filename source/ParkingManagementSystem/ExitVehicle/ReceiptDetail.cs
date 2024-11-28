using System;
using System.Windows.Forms;
using Oracle.DataAccess.Client;

namespace ExitVehicle
{
    public partial class ReceiptDetail : Form
    {
        private string connectionString = "User Id=ParkingAdmin; Password=1111; Data Source=(DESCRIPTION = (ADDRESS = (PROTOCOL = TCP)(HOST = localhost)(PORT = 1521)) (CONNECT_DATA = (SERVER = DEDICATED) (SERVICE_NAME = xe)));";
        private int vehicleId;
        private Timer timer; // 타이머 선언
        private Timer transitionTimer; // 10초 뒤 화면 전환 타이머
        public ReceiptDetail(int vehicleId, string vehicleNumber)
        {
            InitializeComponent();
            // 폼의 시작 위치를 화면 중앙으로 설정
            this.StartPosition = FormStartPosition.CenterScreen;

            this.vehicleId = vehicleId;

            // 폼 로드 시 데이터 로드
            this.Load += ReceiptDetail_Load;

            // lblCurrentTime을 폼 로드 시 바로 현재 시간으로 초기화
            lblCurrentTime.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            InitializeTimerT(); // 타이머 초기화 및 시작
        }

        // Timer 초기화 메서드
        private void InitializeTimerT()
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
        private void ReceiptDetail_Load(object sender, EventArgs e)
        {
            LoadReceiptData(); // 결제 정보 로드
            InitializeTimer(); // 현재 시간 갱신 타이머 설정
            InitializeCountdownTimer(); // 10초 후 화면 전환 타이머 설정
        }


        private void LoadReceiptData()
        {
            var receiptDetails = GetReceiptDetails(vehicleId);

            if (receiptDetails != null)
            {
                txtVehicleNumber.Text = receiptDetails.VehicleNumber;
                txtFeeBeforeDiscount.Text = $"{receiptDetails.FeeBeforeDiscount:C}";
                txtDiscountAmount.Text = $"{receiptDetails.DiscountAmount:C}";
                txtTotalFee.Text = $"{receiptDetails.TotalFee:C}";
                txtDuration.Text = $"{receiptDetails.ParkingDuration}분";
                txtStartTime.Text = receiptDetails.StartTime != DateTime.MinValue
                    ? receiptDetails.StartTime.ToString("yyyy-MM-dd HH:mm")
                    : "시간 없음";
                txtEndTime.Text = receiptDetails.EndTime != DateTime.MinValue
                    ? receiptDetails.EndTime.ToString("yyyy-MM-dd HH:mm")
                    : "시간 없음";
            }
            else
            {
                MessageBox.Show("해당 차량의 결제 정보를 찾을 수 없습니다. Receipt 데이터를 확인하세요.",
            "오류", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                // 기본값을 표시하거나 폼 닫지 않음
                txtVehicleNumber.Text = "데이터 없음";
                txtFeeBeforeDiscount.Text = "₩0";
                txtDiscountAmount.Text = "₩0";
                txtTotalFee.Text = "₩0";
                txtDuration.Text = "0분";
                txtStartTime.Text = "시간 없음";
                txtEndTime.Text = "시간 없음";
            }
        }
        private ReceiptDetails GetReceiptDetails(int vehicleId)
        {
            try
            {
                using (OracleConnection connection = new OracleConnection(connectionString))
                {
                    connection.Open();

                    string query = @"
                    SELECT vehicle_number, parking_fee_before_discount, discount_amount, total_fee, 
                           parking_duration, start_time, end_time
                    FROM (
                        SELECT vehicle_number, parking_fee_before_discount, discount_amount, total_fee, 
                               parking_duration, start_time, end_time
                        FROM Receipt
                        WHERE vehicle_id = :vehicleId
                        ORDER BY start_time DESC
                    )
                    WHERE ROWNUM = 1"; // 첫 번째 결과만 가져오기

                    using (OracleCommand command = new OracleCommand(query, connection))
                    {
                        command.Parameters.Add("vehicleId", OracleDbType.Int32).Value = vehicleId;

                        using (OracleDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return new ReceiptDetails
                                {
                                    VehicleNumber = reader["vehicle_number"].ToString(),
                                    FeeBeforeDiscount = reader["parking_fee_before_discount"] != DBNull.Value
                                        ? Convert.ToDecimal(reader["parking_fee_before_discount"])
                                        : 0,
                                    DiscountAmount = reader["discount_amount"] != DBNull.Value
                                        ? Convert.ToDecimal(reader["discount_amount"])
                                        : 0,
                                    TotalFee = reader["total_fee"] != DBNull.Value
                                        ? Convert.ToDecimal(reader["total_fee"])
                                        : 0,
                                    ParkingDuration = reader["parking_duration"] != DBNull.Value
                                        ? Convert.ToInt32(reader["parking_duration"])
                                        : 0,
                                    StartTime = reader["start_time"] != DBNull.Value
                                        ? Convert.ToDateTime(reader["start_time"])
                                        : DateTime.MinValue,
                                    EndTime = reader["end_time"] != DBNull.Value
                                        ? Convert.ToDateTime(reader["end_time"])
                                        : DateTime.MinValue
                                };
                            }
                            else
                            {
                                MessageBox.Show("해당 차량의 결제 정보를 찾을 수 없습니다.", "오류", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"결제 정보를 로드하는 중 오류가 발생했습니다: {ex.Message}", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return null;
        }




        // 타이머 설정: 현재 시간을 1초마다 갱신
        private void InitializeTimer()
        {
            timer = new Timer();
            timer.Interval = 1000; // 1초마다 갱신
            timer.Tick += (sender, e) =>
            {
                lblCurrentTime.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            };
            timer.Start();
        }

        // 20초 뒤 화면 전환 타이머
        private void InitializeCountdownTimer()
        {
            transitionTimer = new Timer();
            transitionTimer.Interval = 10000; // 10초 (밀리초 단위)
            transitionTimer.Tick += (sender, e) =>
            {
                transitionTimer.Stop(); // 타이머 중지
                transitionTimer.Dispose(); // 타이머 자원 해제

                // 다음 화면으로 이동
                ShowNextForm();
            };
            transitionTimer.Start(); // 타이머 시작
        }

        // 다음 화면으로 이동하는 메서드
        private void ShowNextForm()
        {
            ExitVehicleManagement exitVehicleManagement = new ExitVehicleManagement();
            exitVehicleManagement.Show();

            // 현재 폼 숨기기
            this.Hide();
        }

        public class ReceiptDetails
        {
            public string VehicleNumber { get; set; }
            public decimal FeeBeforeDiscount { get; set; }
            public decimal DiscountAmount { get; set; }
            public decimal TotalFee { get; set; }
            public int ParkingDuration { get; set; }
            public DateTime StartTime { get; set; }
            public DateTime EndTime { get; set; }
        }

        private void ReceiptDetail_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit(); // 애플리케이션 종료
        }

    }
}
