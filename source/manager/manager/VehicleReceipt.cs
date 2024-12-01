using Oracle.DataAccess.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace manager
{
    public partial class VehicleReceipt : Form
    {
        private readonly string connectionString;
        public VehicleReceipt(string connectionString)
        {
            InitializeComponent();
            this.connectionString = connectionString;
        }
        public void LoadReceipts(int vehicleId)
        {
            // Receipt 데이터를 조회하는 SQL 쿼리
            string query = @"
                SELECT 
                    receipt_id AS ""영수증 ID"",
                    vehicle_id AS ""차량 ID"",
                    vehicle_number AS ""차량 번호"",
                    parking_fee_before_discount AS ""할인 전 요금"",
                    discount_amount AS ""할인 금액"",
                    total_fee AS ""최종 요금"",
                    parking_duration AS ""주차 시간"",
                    start_time AS ""주차 시작"",
                    end_time AS ""주차 종료""
                FROM Receipt
                WHERE vehicle_id = :vehicle_id";

            try
            {
                // Oracle 데이터베이스 연결
                using (var connection = new OracleConnection(connectionString))
                {
                    connection.Open();
                    using (var command = new OracleCommand(query, connection))
                    {
                        // 바인딩 변수 설정
                        command.Parameters.Add(new OracleParameter("vehicle_id", vehicleId));

                        // 데이터를 읽고 DataTable에 저장
                        using (var adapter = new OracleDataAdapter(command))
                        {
                            var dataTable = new DataTable();
                            adapter.Fill(dataTable);

                            // DataGridView에 데이터 바인딩
                            dgvReceipt.DataSource = dataTable;
                        }
                    }
                }
                LoadSummaryData(vehicleId);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"데이터를 로드하는 중 오류가 발생했습니다: {ex.Message}", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void LoadSummaryData(int vehicleId)
        {
            // SQL 쿼리 정의
            string query = @"
        SELECT 
            COUNT(*) AS TotalParkingCount,
            NVL(SUM(discount_amount), 0) AS TotalDiscountAmount,
            NVL(SUM(total_fee), 0) AS TotalFee,
            NVL(SUM(parking_duration), 0) AS TotalDuration
        FROM Receipt
        WHERE vehicle_id = :vehicle_id";

            try
            {
                using (var connection = new OracleConnection(connectionString))
                {
                    connection.Open();
                    using (var command = new OracleCommand(query, connection))
                    {
                        // 바인딩 변수 설정
                        command.Parameters.Add(new OracleParameter("vehicle_id", vehicleId));

                        using (var reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                // 텍스트 박스에 값 설정
                                txtTotalParkingCount.Text = reader["TotalParkingCount"].ToString();
                                txtTotalDiscountAmount.Text = reader["TotalDiscountAmount"].ToString();
                                txtTotalFee.Text = string.Format("{0:N0}", reader["TotalFee"]); // 10,000 형식
                                txtTotalDuration.Text = $"{reader["TotalDuration"]} 분"; // "120 분" 형식
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"요약 데이터를 로드하는 중 오류가 발생했습니다: {ex.Message}", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
