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
        private ContextMenuStrip contextMenuOccupied; // 차량이 있는 자리용
        private ContextMenuStrip contextMenuEmpty; // 빈 자리용
        private bool isMovingVehicle = false; // 자리 이동 상태 플래그

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
                btn.Click += ParkingSpotButton_Click; // 좌클릭 이벤트
                btn.MouseUp += ParkingSpotButton_MouseUp; // 마우스 우클릭 이벤트
            }
        }

        // 컨텍스트 메뉴 초기화
        private void InitializeContextMenu()
        {
            // 차량이 있는 자리용 컨텍스트 메뉴
            contextMenuOccupied = new ContextMenuStrip();
            ToolStripMenuItem moveMenuItem = new ToolStripMenuItem("자리 이동");
            moveMenuItem.Click += MoveSpotMenuItem_Click; // 자리 이동 이벤트 추가
            ToolStripMenuItem exitMenuItem = new ToolStripMenuItem("출차");
            exitMenuItem.Click += ExitSpotMenuItem_Click; // 출차 이벤트 추가
            contextMenuOccupied.Items.Add(moveMenuItem);
            contextMenuOccupied.Items.Add(exitMenuItem);

            // 빈 자리용 컨텍스트 메뉴
            contextMenuEmpty = new ContextMenuStrip();
            ToolStripMenuItem entryMenuItem = new ToolStripMenuItem("입차");
            entryMenuItem.Click += EntrySpotMenuItem_Click; // 입차 이벤트 추가
            contextMenuEmpty.Items.Add(entryMenuItem);
        }

        // 자리 이동 메뉴 클릭 이벤트
        private void MoveSpotMenuItem_Click(object sender, EventArgs e)
        {
            if (selectedSpotButton == null)
            {
                MessageBox.Show("이동할 차량이 선택되지 않았습니다.", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // 자리 이동 플래그 활성화
            isMovingVehicle = true;

            // 이동 준비 UI 업데이트
            selectedSpotButton.BackColor = Color.Orange; // 선택된 자리 표시
            MessageBox.Show("빈 주차석을 선택하여 차량을 이동하세요.", "안내", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // 출차 메뉴 클릭 이벤트
        private void ExitSpotMenuItem_Click(object sender, EventArgs e)
        {
            if (selectedSpotButton == null)
            {
                MessageBox.Show("출차할 차량이 선택되지 않았습니다.", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int spotNumber = int.Parse(selectedSpotButton.Name.Replace("btnSpot", ""));
            parkingManager.UpdateParkingStatus(spotNumber, false);

            selectedSpotButton.BackColor = SystemColors.Control;
            selectedSpotButton.Text = spotNumber.ToString(); // 빈 자리로 표시
            MessageBox.Show("차량이 출차되었습니다.", "완료", MessageBoxButtons.OK, MessageBoxIcon.Information);

            selectedSpotButton = null; // 선택 초기화
        }

        // 입차 메뉴 클릭 이벤트
        private void EntrySpotMenuItem_Click(object sender, EventArgs e)
        {
            if (selectedSpotButton == null)
            {
                MessageBox.Show("입차할 주차석을 선택하세요.", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int spotNumber = int.Parse(selectedSpotButton.Name.Replace("btnSpot", ""));
            ParkingSpotCRUD entryForm = new ParkingSpotCRUD(parkingManager, spotNumber);
            entryForm.ShowDialog();

            // 입차 후 상태 갱신
            LoadParkingSpotStatus();
        }

        // 좌클릭 이벤트 처리
        private void ParkingSpotButton_Click(object sender, EventArgs e)
        {
            Button clickedButton = sender as Button;

            if (!isMovingVehicle)
            {
                // 자리 이동 상태가 아니면 아무 작업도 수행하지 않음
                return;
            }

            // 빈 자리 확인
            if (clickedButton.BackColor == SystemColors.Control)
            {
                int toSpotNumber = int.Parse(clickedButton.Name.Replace("btnSpot", ""));
                int fromSpotNumber = int.Parse(selectedSpotButton.Name.Replace("btnSpot", ""));

                try
                {
                    // 차량 정보 가져오기
                    int vehicleId = parkingManager.GetVehicleIdBySpotNumber(fromSpotNumber);
                    string vehicleNumber = parkingManager.GetVehicleNumberByVehicleId(vehicleId);

                    if (vehicleId == -1)
                    {
                        MessageBox.Show("이동할 차량을 찾을 수 없습니다.", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    // 데이터베이스 업데이트
                    parkingManager.UpdateParkingStatus(fromSpotNumber, false); // 기존 자리 비우기
                    parkingManager.UpdateParkingStatus(toSpotNumber, true, vehicleId, vehicleNumber); // 새 자리로 이동

                    // UI 업데이트
                    selectedSpotButton.BackColor = SystemColors.Control; // 기존 자리 초기화
                    selectedSpotButton.Text = fromSpotNumber.ToString();

                    clickedButton.BackColor = Color.Green; // 새 자리 설정
                    clickedButton.Text = vehicleNumber;

                    MessageBox.Show($"차량이 {fromSpotNumber}번에서 {toSpotNumber}번으로 이동되었습니다.", "완료", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // 자리 이동 완료
                    isMovingVehicle = false;
                    selectedSpotButton = null; // 선택 초기화
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"자리 이동 중 오류가 발생했습니다: {ex.Message}", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("빈 자리만 선택할 수 있습니다.", "오류", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        // 마우스 우클릭 이벤트 처리
        private void ParkingSpotButton_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                Button clickedButton = sender as Button;

                if (clickedButton.BackColor == Color.Green) // 차량이 있는 자리
                {
                    selectedSpotButton = clickedButton; // 선택된 버튼 저장
                    contextMenuOccupied.Show(Cursor.Position); // 차량이 있는 자리용 메뉴 표시
                }
                else // 빈 자리
                {
                    selectedSpotButton = clickedButton; // 선택된 버튼 저장
                    contextMenuEmpty.Show(Cursor.Position); // 빈 자리용 메뉴 표시
                }
            }
        }

        // 주차 공간 상태를 데이터베이스에서 불러와 버튼 색상 및 텍스트 설정
        private void LoadParkingSpotStatus()
        {
            try
            {
                DataTable parkingTable = parkingManager.GetParkingTable();

                foreach (DataRow row in parkingTable.Rows)
                {
                    int spotNumber = Convert.ToInt32(row["spot_number"]);
                    bool isOccupied = Convert.ToInt32(row["is_occupied"]) == 1;

                    Button btn = (Button)this.Controls.Find($"btnSpot{spotNumber}", true)[0];

                    if (isOccupied)
                    {
                        btn.BackColor = Color.Green;
                        string vehicleNumber = row["vehicle_number"].ToString();
                        btn.Text = vehicleNumber;
                        btn.Font = new Font("Arial", 10, FontStyle.Bold);
                        btn.TextAlign = ContentAlignment.MiddleCenter;
                    }
                    else
                    {
                        btn.BackColor = SystemColors.Control;
                        btn.Text = spotNumber.ToString();
                        btn.Font = new Font("Arial", 10, FontStyle.Regular);
                        btn.TextAlign = ContentAlignment.MiddleCenter;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"주차 공간 상태를 로드하는 중 오류가 발생했습니다: {ex.Message}", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
