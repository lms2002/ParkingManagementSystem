using StoreDiscount;
using System;
using System.Data;
using System.Windows.Forms;

namespace ParkingManagement
{
    public partial class StoreDiscountForm : Form
    {
        private StoreManager storeManager;

        public StoreDiscountForm()
        {
            InitializeComponent();
            // 폼의 시작 위치를 화면 중앙으로 설정
            this.StartPosition = FormStartPosition.CenterScreen;

            storeManager = new StoreManager();
        }

        // btnSubmit 버튼 클릭 이벤트
        private void btnSubmit_Click(object sender, EventArgs e)
        {
            string storeName = cmbStore.SelectedItem?.ToString();
            string vehicleNumber = txtVehicleNumber.Text;

            if (string.IsNullOrEmpty(storeName) || string.IsNullOrEmpty(vehicleNumber))
            {
                MessageBox.Show("매장 이름과 차량 번호를 입력하세요.", "입력 오류", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                int? vehicleId = GetVehicleId(vehicleNumber);

                if (vehicleId == null)
                {
                    MessageBox.Show("해당 차량 번호를 가진 차량이 없습니다.", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (IsVehicleDiscounted(vehicleId.Value))
                {
                    MessageBox.Show("이 차량은 이미 쿠폰을 등록했습니다.", "중복 등록", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                int discountPercentage = GetDiscountPercentage(storeName);
                UpdateStoreDiscount(storeName, discountPercentage, vehicleId.Value);

                MessageBox.Show("할인 정보가 성공적으로 저장되었습니다.", "성공", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // 입력 필드 초기화
                txtVehicleNumber.Clear();
                cmbStore.SelectedIndex = -1; // 콤보박스를 초기 상태로 설정
            }
            catch (Exception ex)
            {
                MessageBox.Show($"오류 발생: {ex.Message}", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // 차량이 이미 쿠폰을 등록했는지 확인
        private bool IsVehicleDiscounted(int vehicleId)
        {
            string query = $"SELECT COUNT(*) FROM StoreDiscount WHERE vehicle_id = {vehicleId}";
            storeManager.Initialize(query);
            DataTable resultTable = storeManager.DataSet.Tables[0];

            return Convert.ToInt32(resultTable.Rows[0][0]) > 0;
        }

        // 상점 이름에 따른 할인율 반환
        private int GetDiscountPercentage(string storeName)
        {
            switch (storeName)
            {
                case "NIKE": return 20;
                case "빵빵이의 빵집": return 10;
                case "모수": return 30;
                default: return 0;
            }
        }

        private int GetDiscountCondition(string storeName)
        {
            switch (storeName)
            {
                case "NIKE":
                    return 50000;
                case "빵빵이의 빵집":
                    return 20000;
                case "모수":
                    return 150000;
                default:
                    return 0;
            }
        }

        // vehicle_number로 vehicle_id 검색
        private int? GetVehicleId(string vehicleNumber)
        {
            string query = $"SELECT vehicle_id FROM ParkingSpot WHERE vehicle_number = '{vehicleNumber}'";
            DataTable resultTable = new DataTable();

            try
            {
                storeManager.Initialize(query); // SQL 쿼리 실행
                resultTable = storeManager.DataSet.Tables[0];

                if (resultTable.Rows.Count > 0)
                {
                    return Convert.ToInt32(resultTable.Rows[0]["vehicle_id"]);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"vehicle_id 검색 중 오류 발생: {ex.Message}", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return null;
        }

        // StoreDiscount 테이블 업데이트
        private void UpdateStoreDiscount(string storeName, int discountPercentage, int vehicleId)
        {
            // SQL 쿼리를 문자열 보간으로 구성
            string query = $@"
MERGE INTO StoreDiscount sd
USING (SELECT '{storeName}' AS store_name FROM dual) source
ON (sd.store_name = source.store_name AND sd.vehicle_id = {vehicleId})
WHEN MATCHED THEN
    UPDATE SET sd.discount_percentage = {discountPercentage}, 
               sd.discount_condition = {GetDiscountCondition(storeName)}
WHEN NOT MATCHED THEN
    INSERT (discount_id, vehicle_id, store_name, discount_percentage, discount_condition, discount_date)
    VALUES (DiscountSeq.NEXTVAL, {vehicleId}, '{storeName}', {discountPercentage}, {GetDiscountCondition(storeName)}, SYSDATE)";

            try
            {
                // 쿼리 실행
                storeManager.ExecuteCommand(query);
            }
            catch (Exception ex)
            {
                // 오류 메시지 출력
                MessageBox.Show($"StoreDiscount 업데이트 중 오류 발생: {ex.Message}", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void StoreDiscountForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}
