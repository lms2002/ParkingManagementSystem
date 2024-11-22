using System;
using System.Diagnostics;
using System.Windows.Forms;
using Oracle.DataAccess.Client;

namespace ExitVehicle
{
    public partial class ExitVehicleManagement : Form
    {
        private string connectionString = "User Id=ParkingAdmin; Password=1111; Data Source=(DESCRIPTION = (ADDRESS = (PROTOCOL = TCP)(HOST = localhost)(PORT = 1521)) (CONNECT_DATA = (SERVER = DEDICATED) (SERVICE_NAME = xe)));";

        public ExitVehicleManagement()
        {
            InitializeComponent();
            this.FormClosed += ExitVehicleManagement_FormClosed;

            // 이벤트 핸들러 연결
            txtVehicleNumber.Click += txtVehicleNumber_Click;
            txtVehicleNumber.KeyDown += txtVehicleNumber_KeyDown;
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

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            string vehicleNumber = txtVehicleNumber.Text?.Trim();

            if (string.IsNullOrEmpty(vehicleNumber))
            {
                MessageBox.Show("차량 번호를 입력해주세요.", "입력 오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                if (IsVehicleParked(vehicleNumber))
                {
                    ExitPayment exitPayment = new ExitPayment(vehicleNumber);
                    exitPayment.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("입력한 차량 번호는 현재 주차 중이 아닙니다.", "오류", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"차량 정보를 처리하는 중 오류가 발생했습니다: {ex.Message}", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool IsVehicleParked(string vehicleNumber)
        {
            try
            {
                string query = @"SELECT COUNT(*) FROM ParkingSpot WHERE UPPER(TRIM(vehicle_number)) = :vehicle_number AND is_occupied = 1";

                using (OracleConnection connection = new OracleConnection(connectionString))
                {
                    connection.Open();

                    using (OracleCommand command = new OracleCommand(query, connection))
                    {
                        command.Parameters.Add("vehicle_number", OracleDbType.Varchar2).Value = vehicleNumber.Trim().ToUpper();
                        object result = command.ExecuteScalar();
                        return result != null && int.Parse(result.ToString()) > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"차량 정보 조회 중 오류가 발생했습니다: {ex.Message}", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }



        private void ExitVehicleManagement_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit(); // 애플리케이션 종료
        }
    }
}
