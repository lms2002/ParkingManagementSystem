using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace ParkingManagement
{
    public partial class ParkingSpotSelectionForm : Form
    {
        private ParkingManager parkingManager;
        private string selectedVehicleNumber; // 전달받은 차량 번호

        public ParkingSpotSelectionForm(string vehicleNumber)
        {
            InitializeComponent();
            InitializeButtonEvents();

            parkingManager = new ParkingManager("User Id=ParkingAdmin; Password=1111; Data Source=(DESCRIPTION = (ADDRESS = (PROTOCOL = TCP)(HOST = localhost)(PORT = 1521)) (CONNECT_DATA = (SERVER = DEDICATED) (SERVICE_NAME = xe)));");
            parkingManager.OpenDatabase();

            LoadParkingSpotStatus();

            this.FormClosed += ParkingSpotSelectionForm_FormClosed;

            if (!string.IsNullOrEmpty(vehicleNumber))
            {
                selectedVehicleNumber = vehicleNumber;
            }
            else
            {
                MessageBox.Show("차량 번호를 전달받지 못했습니다.", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
        }

        // 각 버튼에 클릭 이벤트 할당
        private void InitializeButtonEvents()
        {
            for (int i = 1; i <= 30; i++)
            {
                Button btn = (Button)this.Controls.Find($"btnSpot{i}", true)[0];
                btn.Click += ParkingSpotButton_Click;

                // 장애인 주차석 표시를 위한 Paint 이벤트 추가
                if (i >= 26 && i <= 30) // 장애인 전용 주차석 (26~30)
                {
                    btn.Paint += Button_Paint_DisabledSpot;
                }
            }
        }

        // 장애인 주차석 표시용 노란색 원 그리기
        private void Button_Paint_DisabledSpot(object sender, PaintEventArgs e)
        {
            Button btn = sender as Button;
            Graphics g = e.Graphics;

            int diameter = 8; // 원의 지름
            int x = btn.Width - diameter - 2;
            int y = 2;

            // 노란색 원 그리기
            using (SolidBrush yellowBrush = new SolidBrush(Color.Yellow))
            {
                g.FillEllipse(yellowBrush, x, y, diameter, diameter);
            }

            // 검은색 테두리 그리기
            using (Pen blackPen = new Pen(Color.Black, 1))
            {
                g.DrawEllipse(blackPen, x, y, diameter, diameter);
            }
        }

        // 주차 공간 상태를 데이터베이스에서 불러와 버튼 색상 설정
        private void LoadParkingSpotStatus()
        {
            try
            {
                DataTable parkingTable = parkingManager.GetParkingTable();

                foreach (DataRow row in parkingTable.Rows)
                {
                    int spotNumber = Convert.ToInt32(row["spot_number"]);
                    bool isOccupied = Convert.ToInt32(row["is_occupied"]) == 1;

                    // 주차 공간 버튼 찾기
                    Button btn = (Button)this.Controls.Find($"btnSpot{spotNumber}", true)[0];
                    btn.BackColor = isOccupied ? Color.Green : DefaultBackColor;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading parking spots: {ex.Message}");
            }
        }


        // 주차 자리 선택 시 일어나는 클릭이벤트
        private void ParkingSpotButton_Click(object sender, EventArgs e)
        {
            Button clickedButton = sender as Button;
            string spotNumberStr = clickedButton.Name.Replace("btnSpot", "");
            int spotNumber = int.Parse(spotNumberStr);

            // 이미 주차된 자리인지 확인
            if (clickedButton.BackColor == Color.Green)
            {
                MessageBox.Show("주차 된 주차석 입니다. 빈 주차석을 선택해주세요", "알림", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return; // 이미 주차된 경우, 추가 작업 없이 중단
            }

            // 장애인 주차석(26~30번) 클릭 시 확인 메시지
            if (spotNumber >= 26 && spotNumber <= 30)
            {
                DialogResult result = MessageBox.Show($"{spotNumber}번 주차석은 장애인 주차석입니다. 주차하시겠습니까?", "장애인 주차석 경고", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (result == DialogResult.No)
                {
                    return; // 사용자가 '취소'를 누르면 함수 종료
                }
            }

            try
            {
                // 현재 시간 기록
                DateTime entryTime = DateTime.Now;

                // vehicle_number로 vehicle_id 조회
                int vehicleId = parkingManager.GetVehicleIdByNumber(selectedVehicleNumber);
                if (vehicleId == -1)
                {
                    MessageBox.Show("차량 ID를 조회하는 중 오류가 발생했습니다. 등록되지 않은 차량일 수 있습니다.", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // ParkingManager를 사용하여 입차 기록 저장
                parkingManager.InsertReceiptRecord(vehicleId, selectedVehicleNumber, entryTime); // 수정됨: vehicle_id와 vehicle_number 함께 저장

                // 주차 공간 상태 업데이트
                parkingManager.UpdateParkingStatus(spotNumber, true, vehicleId); // 수정됨: vehicle_id 사용

                // 버튼 색상 업데이트
                clickedButton.BackColor = Color.Green;

                // 성공 메시지 표시
                MessageBox.Show($"{spotNumber}번 주차 공간에 차량이 등록되었습니다.", "완료", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // ParkingDetailsForm으로 이동
                string connectionString = "User Id=ParkingAdmin; Password=1111; Data Source=(DESCRIPTION = (ADDRESS = (PROTOCOL = TCP)(HOST = localhost)(PORT = 1521)) (CONNECT_DATA = (SERVER = DEDICATED) (SERVICE_NAME = xe)));";
                ParkingDetailsForm detailForm = new ParkingDetailsForm(connectionString, vehicleId, spotNumber); // 수정됨: vehicleId 전달
                detailForm.Show();
                this.Hide(); // 현재 폼 숨기기
            }
            catch (Exception ex)
            {
                MessageBox.Show($"주차 공간 업데이트 중 문제가 발생했습니다: {ex.Message}", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void ParkingSpotSelectionForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            // 폼이 닫히면 애플리케이션 종료
            Application.Exit(); // 애플리케이션 종료
        }
    }
}
