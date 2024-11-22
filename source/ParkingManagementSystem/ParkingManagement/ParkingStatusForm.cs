using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Oracle.DataAccess.Client;

namespace ParkingManagement
{
    public partial class ParkingStatusForm : Form
    {
        private ParkingManager parkingManager;
        public ParkingStatusForm()
        {
            InitializeComponent();
            parkingManager = new ParkingManager(); // ParkingManager 초기화
            LoadParkingStatus(); // 주차 공간 상태 로드
        }

        private void ParkingStatusForm_Click(object sender, EventArgs e)
        {
            // 주차 공간을 선택한 후 차량 번호 입력 폼으로 전환
            ShowVehicleInputForm();
        }
        // 라벨 클릭 시 차량 번호 입력 폼으로 이동하는 공통 메서드
        private void ShowVehicleInputForm()
        {
            VehicleNumberInputForm vehicleInputForm = new VehicleNumberInputForm();
            vehicleInputForm.Show();  // 차량 번호 입력 폼 띄우기
            this.Hide();  // 현재 폼은 숨김
        }

        // 주차 상태를 라벨에 업데이트하는 메서드
        private void UpdateParkingStatusLabels()
        {
            DataTable parkingTable = parkingManager.GetParkingTable();
            if (parkingTable == null)
            {
                MessageBox.Show("주차 데이터를 로드하지 못했습니다.", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // 라벨 업데이트
            UpdateStatusLabel(parkingTable, lblTotalAvailableSpots, "총 잔여 자리", row => !Convert.ToBoolean(row["is_occupied"]));
            UpdateStatusLabel(parkingTable, lblRegularAvailableSpots, "일반석 빈 자리", row => !Convert.ToBoolean(row["is_occupied"]) && Convert.ToInt32(row["spot_number"]) <= 25);
            UpdateStatusLabel(parkingTable, lblDisabledAvailableSpots, "장애석 빈 자리", row => !Convert.ToBoolean(row["is_occupied"]) && Convert.ToInt32(row["spot_number"]) >= 26);
        }
        // 라벨 업데이트를 처리하는 사용자 정의 메서드
        private void UpdateStatusLabel(DataTable parkingTable, Label label, string labelText, Func<DataRow, bool> condition)
        {
            int count = parkingTable.AsEnumerable().Count(row => condition(row));
            label.Text = $"{labelText}: {count}";
        }

        // 주차 공간 상태 로드
        private void LoadParkingStatus()
        {
            try
            {
                // VehicleNumberInputForm에서 ParkingManager 초기화
                string connectionString = "User Id=ParkingAdmin; Password=1111; Data Source=(DESCRIPTION = (ADDRESS = (PROTOCOL = TCP)(HOST = localhost)(PORT = 1521)) (CONNECT_DATA = (SERVER = DEDICATED) (SERVICE_NAME = xe)));";
                parkingManager = new ParkingManager(connectionString);
                parkingManager.OpenDatabase(); // OpenDatabase 호출 시 별도 매개변수 필요 없음


                // 주차 공간 상태 로드 및 라벨 업데이트
                UpdateParkingStatusLabels();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"주차 상태를 로드하는 중 오류 발생: {ex.Message}", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        // 각 라벨 클릭 시 차량 번호 입력 폼으로 전환
        private void lblTotalAvailableSpots_Click(object sender, EventArgs e) => ShowVehicleInputForm();
        private void lblRegularAvailableSpots_Click(object sender, EventArgs e) => ShowVehicleInputForm();
        private void lblDisabledAvailableSpots_Click(object sender, EventArgs e) => ShowVehicleInputForm();

        private void ParkingStatusForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            // 폼이 닫히면 애플리케이션 종료
            Application.Exit(); // 애플리케이션 종료
        }
    }
}
