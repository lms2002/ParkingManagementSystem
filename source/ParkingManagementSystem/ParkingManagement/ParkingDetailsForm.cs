using System;
using System.Windows.Forms;

namespace ParkingManagement
{
    public partial class ParkingDetailsForm : Form
    {
        private ParkingManager parkingManager;
        private int vehicleId; // 차량 ID 추가
        private string vehicleNumber; // 차량 번호 저장
        private int spotNumber;
        private Timer timer; // 현재 시간 갱신 타이머
        private Timer transitionTimer; // 20초 뒤 화면 전환 타이머

        public ParkingDetailsForm(string connectionString, int vehicleId, int spotNumber)
        {
            InitializeComponent();
            this.vehicleId = vehicleId;
            this.spotNumber = spotNumber;
            parkingManager = new ParkingManager(connectionString);

            // 차량 번호를 vehicleId를 이용해 조회
            LoadVehicleDetails();

            // lblCurrentTime을 폼 로드 시 바로 현재 시간으로 초기화
            lblCurrentTime.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            InitializeTimer();

            // 카운트다운 타이머 초기화
            InitializeCountdownTimer();
        }
        private void LoadVehicleDetails()
        {
            try
            {
                // 차량 ID를 이용해 차량 정보를 조회
                var vehicleDetails = parkingManager.GetVehicleDetails(vehicleId);
                if (vehicleDetails != null)
                {
                    string vehicleNumber = vehicleDetails.VehicleNumber; // 차량 번호 표시용 지역 변수로 사용
                    lblVehicleNumber.Text = vehicleNumber;
                    lblVehicleType.Text = vehicleDetails.VehicleType; // 차량 종류 표시
                    lblParkingSpot.Text = vehicleDetails.ParkingSpot.ToString(); // 주차 공간 번호 표시
                    lblEntryTime.Text = vehicleDetails.EntryTime != DateTime.MinValue
                        ? vehicleDetails.EntryTime.ToString("yyyy년 MM월 dd일 HH시 mm분") // 시간 포함 출력
                        : "입차 시간 없음";
                }
                else
                {
                    MessageBox.Show("차량 정보를 찾을 수 없습니다.", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"데이터를 로드하는 중 오류 발생: {ex.Message}", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



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
        private void InitializeCountdownTimer()
        {
            transitionTimer = new Timer();
            transitionTimer.Interval = 20000; // 20초 (밀리초 단위)
            transitionTimer.Tick += (sender, e) =>
            {
                transitionTimer.Stop(); // 타이머 중지
                transitionTimer.Dispose(); // 리소스 해제

                // 다음 화면으로 이동
                ShowNextForm();
            };
            transitionTimer.Start(); // 타이머 시작
        }


        private void ShowNextForm()
        {
            // 다음 화면 (예: NextForm)을 표시
            ParkingStatusForm nextForm = new ParkingStatusForm();
            nextForm.Show();

            // 현재 폼 숨기기 또는 닫기
            this.Hide();
        }



        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            if (timer != null)
            {
                timer.Stop();
                timer.Dispose();
            }

            if (transitionTimer != null)
            {
                transitionTimer.Stop();
                transitionTimer.Dispose();
            }

            base.OnFormClosed(e);
        }

        private void ParkingDetailsForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            // 폼이 닫히면 애플리케이션 종료
            Application.Exit(); // 애플리케이션 종료
        }
    }
}
