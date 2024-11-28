using System;
using System.Diagnostics;
using System.Windows.Forms;
using Oracle.DataAccess.Client;

namespace ExitVehicle
{
    public partial class ExitVehicleManagement : Form
    {
        private string connectionString = "User Id=ParkingAdmin; Password=1111; Data Source=(DESCRIPTION = (ADDRESS = (PROTOCOL = TCP)(HOST = localhost)(PORT = 1521)) (CONNECT_DATA = (SERVER = DEDICATED) (SERVICE_NAME = xe)));";
        private int vehicleId;
        private Timer timer; // 시간을 표시하기 위한 Timer 추가

        public ExitVehicleManagement(int vehicleId = 0)
        {
            InitializeComponent();
            // 폼의 시작 위치를 화면 중앙으로 설정
            this.StartPosition = FormStartPosition.CenterScreen;

            this.FormClosed += ExitVehicleManagement_FormClosed;

            // 이벤트 핸들러 연결
            txtVehicleNumber.Click += txtVehicleNumber_Click;
            txtVehicleNumber.KeyDown += txtVehicleNumber_KeyDown;

            // vehicle_id 저장
            this.vehicleId = vehicleId;

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

        private void txtVehicleNumber_Click(object sender, EventArgs e)
        {
            StartVirtualKeyboard(); // 화상 키보드 실행
        }

        private void txtVehicleNumber_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)  // Enter 키를 누른 경우
            {
                e.SuppressKeyPress = true;  // Enter 키 입력 방지
                CloseVirtualKeyboard();    // 화상 키보드 닫기
                btnSubmit_Click(sender, e);  // Submit 버튼 클릭 이벤트 호출
            }
        }

        private void StartVirtualKeyboard()
        {
            try
            {
                Process.Start("osk.exe");  // 화상 키보드 실행
            }
            catch (Exception ex)
            {
                MessageBox.Show($"화상 키보드를 실행하는 동안 오류가 발생했습니다: {ex.Message}", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CloseVirtualKeyboard()
        {
            try
            {
                foreach (var process in Process.GetProcessesByName("osk"))
                {
                    process.Kill(); // 실행 중인 화상 키보드 종료
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"화상 키보드를 닫는 동안 오류가 발생했습니다: {ex.Message}", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSubmit_Click(object sender, EventArgs e) //입력된 차량번호를 ExitPayment 전송
        {
            string vehicleNumberInput = txtVehicleNumber.Text?.Trim();

            if (string.IsNullOrEmpty(vehicleNumberInput))
            {
                MessageBox.Show("차량 번호를 입력해주세요.", "입력 오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // IsVehicleParked 메서드를 사용하여 주차 여부 및 정보를 확인
            var (isParked, vehicleId, vehicleNumber) = IsVehicleParked(vehicleNumberInput);

            if (isParked)
            {
                // 주차 중인 경우 ExitPayment 화면으로 이동
                ExitPayment exitPayment = new ExitPayment(vehicleId, vehicleNumber);
                exitPayment.Show();
                this.Hide(); // 현재 화면 숨기기
            }
            else
            {
                MessageBox.Show("현재 주차 중인 차량이 아닙니다.", "오류", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private (bool isParked, int vehicleId, string vehicleNumber) IsVehicleParked(string vehicleNumberInput)
        {
            try
            {
                string query = @"
    SELECT vehicle_id, vehicle_number 
    FROM ParkingSpot 
    WHERE UPPER(TRIM(vehicle_number)) = :vehicle_number AND is_occupied = 1";

                using (OracleConnection connection = new OracleConnection(connectionString))
                {
                    connection.Open();

                    using (OracleCommand command = new OracleCommand(query, connection))
                    {
                        command.Parameters.Add("vehicle_number", OracleDbType.Varchar2).Value = vehicleNumberInput.Trim().ToUpper();

                        using (OracleDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                // vehicle_id를 Decimal로 가져온 후 int로 변환
                                int vehicleId = reader.IsDBNull(0) ? 0 : Convert.ToInt32(reader.GetDecimal(0));
                                string vehicleNumber = reader.IsDBNull(1) ? null : reader.GetString(1);

                                return (true, vehicleId, vehicleNumber);
                            }
                        }
                    }
                }

                return (false, 0, null); // 차량이 주차 중이 아닌 경우
            }
            catch (Exception ex)
            {
                MessageBox.Show($"차량 정보 조회 중 오류가 발생했습니다: {ex.Message}\n{ex.StackTrace}", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return (false, 0, null);
            }
        }





        private void ExitVehicleManagement_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit(); // 애플리케이션 종료
        }
    }
}
