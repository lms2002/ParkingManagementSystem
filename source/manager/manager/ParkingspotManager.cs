using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace manager
{
    public partial class ParkingspotManager : Form
    {
        private ParkingManager parkingManager; // 주차 관리 객체
        private Button selectedSpotButton; // 선택된 주차 버튼 저장

        public ParkingspotManager()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;

            InitializeParkingSpots();
            InitializeContextMenu();

            // ParkingManager 초기화
            parkingManager = new ParkingManager("User Id=ParkingAdmin; Password=1111; Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=localhost)(PORT=1521)) (CONNECT_DATA=(SERVER=DEDICATED) (SERVICE_NAME=xe)));");
            parkingManager.OpenDatabase();

            LoadParkingSpotStatus();
        }

        // 주차석 버튼 초기화
        private void InitializeParkingSpots()
        {
            for (int i = 1; i <= 30; i++)
            {
                Button btn = (Button)this.Controls.Find($"btnSpot{i}", true)[0];
                btn.Click += ParkingSpotButton_Click;
                btn.MouseUp += ParkingSpotButton_MouseUp; // 마우스 우클릭 이벤트 추가
            }
        }

        // ContextMenuStrip 초기화
        private void InitializeContextMenu()
        {
            contextMenuParkingSpot = new ContextMenuStrip();
            ToolStripMenuItem moveMenuItem = new ToolStripMenuItem("자리 이동");
            moveMenuItem.Click += MoveSpotMenuItem_Click; // 자리 이동 이벤트 추가
            contextMenuParkingSpot.Items.Add(moveMenuItem);
        }

        // 마우스 우클릭 이벤트 처리
        private void ParkingSpotButton_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                Button clickedButton = sender as Button;
                if (clickedButton.BackColor == Color.Green) // 주차된 자리만 우클릭 가능
                {
                    selectedSpotButton = clickedButton; // 현재 버튼 저장
                    contextMenuParkingSpot.Show(Cursor.Position); // 컨텍스트 메뉴 표시
                }
                else
                {
                    MessageBox.Show("빈 주차석입니다. 차량이 등록된 자리만 선택할 수 있습니다.", "알림", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        // 자리 이동 메뉴 클릭 이벤트
        private void MoveSpotMenuItem_Click(object sender, EventArgs e)
        {
            if (selectedSpotButton == null)
            {
                MessageBox.Show("이동할 차량이 선택되지 않았습니다.", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            MessageBox.Show("빈 주차석을 선택하여 차량을 이동하세요.", "안내", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // 주차 자리 클릭 이벤트
        private void ParkingSpotButton_Click(object sender, EventArgs e)
        {
            Button clickedButton = sender as Button;
            string spotNumberStr = clickedButton.Name.Replace("btnSpot", "");
            int spotNumber = int.Parse(spotNumberStr);

            // 자리 이동 중인지 확인
            if (selectedSpotButton != null && clickedButton.BackColor != Color.Green)
            {
                // 이동 처리
                int fromSpotNumber = int.Parse(selectedSpotButton.Name.Replace("btnSpot", ""));
                int vehicleId = parkingManager.GetVehicleIdByNumber(selectedSpotButton.Text); // 차량 번호로 ID 조회

                if (vehicleId == -1)
                {
                    MessageBox.Show("차량 정보를 가져오는 중 오류가 발생했습니다.", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                try
                {
                    // 이동 전 주차석 상태 업데이트
                    parkingManager.UpdateParkingStatus(fromSpotNumber, false);

                    // 이동 후 주차석 상태 업데이트
                    parkingManager.UpdateParkingStatus(spotNumber, true, vehicleId);

                    // 버튼 색상 업데이트
                    selectedSpotButton.BackColor = DefaultBackColor;
                    clickedButton.BackColor = Color.Green;

                    MessageBox.Show($"차량이 {fromSpotNumber}번에서 {spotNumber}번으로 이동되었습니다.", "완료", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    selectedSpotButton = null; // 선택 초기화
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"자리 이동 중 오류가 발생했습니다: {ex.Message}", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
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
    }
}
