using System;
using System.Diagnostics;
using System.Windows.Forms;
using Oracle.DataAccess.Client;

namespace ParkingManagement
{
    public partial class VehicleNumberInputForm : Form
    {
        private ParkingManager parkingManager;
        public VehicleNumberInputForm()
        {
            InitializeComponent();

            // KeyDown 이벤트 연결
            txtVehicleNumber.KeyDown += txtVehicleNumber_KeyDown;

            // ParkingManager 초기화 및 데이터베이스 연결
            string connectionString = "User Id=ParkingAdmin; Password=1111; Data Source=(DESCRIPTION = (ADDRESS = (PROTOCOL = TCP)(HOST = localhost)(PORT = 1521)) (CONNECT_DATA = (SERVER = DEDICATED) (SERVICE_NAME = xe)));";
            parkingManager = new ParkingManager(connectionString);
            parkingManager.OpenDatabase();
        }

        // 텍스트박스 클릭 시 가상 키보드 실행
        private void txtVehicleNumber_Click(object sender, EventArgs e)
        {
            StartVirtualKeyboard();  // 가상 키보드 실행
        }

        private void txtVehicleNumber_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)  // Enter 키를 누른 경우
            {
                e.SuppressKeyPress = true;  // Enter 키가 텍스트박스에 입력되지 않도록 방지
                CloseVirtualKeyboard();    // 가상 키보드 닫기
                btnSubmit_Click(sender, e);  // Submit 버튼 클릭 이벤트 호출
            }
        }
        // 가상 키보드 실행
        private void StartVirtualKeyboard()
        {
            try
            {
                Process.Start("osk.exe");  // osk.exe (가상 키보드) 실행
            }
            catch (Exception ex)
            {
                MessageBox.Show("가상 키보드를 실행하는데 문제가 발생했습니다: " + ex.Message);
            }
        }
        // 가상 키보드 닫기
        private void CloseVirtualKeyboard()
        {
            try
            {
                // 모든 osk.exe 프로세스를 종료
                foreach (var process in Process.GetProcessesByName("osk"))
                {
                    process.Kill();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("가상 키보드를 닫는 동안 문제가 발생했습니다: " + ex.Message);
            }
        }

        // 차량 번호 유효성 검사
        private bool ValidateInputs(string vehicleNumber, string vehicleType)
        {
            if (string.IsNullOrEmpty(vehicleNumber))
            {
                MessageBox.Show("차량 번호를 입력해주세요.", "입력 오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (string.IsNullOrEmpty(vehicleType))
            {
                MessageBox.Show("차종을 선택해주세요.", "입력 오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;
        }

        // 차량 번호 입력 후 Submit 클릭
        private void btnSubmit_Click(object sender, EventArgs e)
        {
            string vehicleNumber = txtVehicleNumber.Text?.Trim(); // 차량 번호 입력
            string vehicleType = cmbVehicleType.SelectedItem?.ToString(); // 차종 선택

            // 입력값 유효성 검사
            if (!ValidateInputs(vehicleNumber, vehicleType))
            {
                return;
            }

            try
            {
                // 차량 중복 확인
                if (parkingManager.IsVehicleParked(vehicleNumber))
                {
                    MessageBox.Show("현재 차량이 이미 주차 중입니다.", "중복 오류", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // 신규 차량 등록
                parkingManager.RegisterVehicle(vehicleNumber, vehicleType);

                // ParkingSpotSelectionForm으로 이동
                ParkingSpotSelectionForm parkingSpotSelectionForm = new ParkingSpotSelectionForm(vehicleNumber);
                parkingSpotSelectionForm.Show();
                this.Hide();
            }
            catch (Exception ex)
            {
                // 예외 메시지 출력
                MessageBox.Show("차량 정보를 저장하는데 문제가 발생했습니다: " + ex.Message, "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void VehicleNumberInputForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            // 폼이 닫히면 애플리케이션 종료
            Application.Exit(); // 애플리케이션 종료
        }
    }
}