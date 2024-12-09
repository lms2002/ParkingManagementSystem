using System;
using System.Windows.Forms;
using Oracle.DataAccess.Client;

namespace manager
{
    public partial class ParkingSpotCRUD : Form
    {
        private ParkingManager parkingManager;
        private int selectedSpotNumber;

        public ParkingSpotCRUD(ParkingManager manager, int spotNumber)
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            parkingManager = manager;
            selectedSpotNumber = spotNumber;

            lblSpotNumber.Text = $"주차 번호: {spotNumber}번"; // 선택된 주차 번호 표시
        }

        private void btnAddVehicle_Click_1(object sender, EventArgs e)
        {
            string vehicleNumber = txtVehicleNumber.Text.Trim();
            string vehicleType = txtVehicleType.Text.Trim();

            if (string.IsNullOrEmpty(vehicleNumber) || string.IsNullOrEmpty(vehicleType))
            {
                MessageBox.Show("차량 번호와 차량 타입을 입력하세요.", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (vehicleType != "Standard" && vehicleType != "Compact")
            {
                MessageBox.Show("차량 타입은 'Standard' 또는 'Compact'로 입력해야 합니다.", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                // 차량 정보 DB에 삽입
                int vehicleId = parkingManager.AddVehicle(vehicleNumber, vehicleType);

                // 선택된 주차 공간에 차량 입차 처리
                parkingManager.UpdateParkingStatus(selectedSpotNumber, true, vehicleId, vehicleNumber);

                MessageBox.Show($"차량이 {selectedSpotNumber}번 주차 공간에 입차되었습니다.", "완료", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // 폼 닫기
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"입차 처리 중 오류가 발생했습니다: {ex.Message}", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
