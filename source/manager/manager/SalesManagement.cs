using System;
using System.Data;
using System.Windows.Forms;
using Oracle.DataAccess.Client;

namespace manager
{
    public partial class SalesManagement : Form
    {
        private readonly string connectionString;

        public SalesManagement(string connectionString)
        {
            InitializeComponent();
            this.connectionString = connectionString;
        }

        // 매출 데이터 가져오기
        private (DataTable dailySales, int monthlyTotal) GetSalesData(string targetMonth)
        {
            DataTable dailySales = new DataTable();
            int monthlyTotal = 0;

            // 일별 매출과 당월 총 매출 쿼리
            string salesQuery = @"
                SELECT 
                    TO_CHAR(start_time, 'YYYY-MM-DD') AS sale_date,
                    SUM(total_fee) AS daily_total,
                    SUM(SUM(total_fee)) OVER () AS monthly_total
                FROM Receipt
                WHERE TO_CHAR(start_time, 'YYYY-MM') = :target_month
                GROUP BY TO_CHAR(start_time, 'YYYY-MM-DD')
                ORDER BY sale_date";

            try
            {
                using (var connection = new OracleConnection(connectionString))
                {
                    connection.Open();

                    using (var command = new OracleCommand(salesQuery, connection))
                    {
                        command.Parameters.Add(new OracleParameter("target_month", targetMonth));
                        using (var adapter = new OracleDataAdapter(command))
                        {
                            adapter.Fill(dailySales);

                            // 당월 총 매출 계산 (첫 번째 행의 monthly_total 컬럼 사용)
                            if (dailySales.Rows.Count > 0)
                            {
                                monthlyTotal = Convert.ToInt32(dailySales.Rows[0]["monthly_total"]);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // 사용자 친화적인 메시지 표시
                MessageBox.Show("매출 데이터를 가져오는 중 오류가 발생했습니다. 관리자에게 문의하세요.", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);

                // 디버깅용 예외 로그
                Console.WriteLine(ex.ToString());
            }

            return (dailySales, monthlyTotal);
        }

        // DataGridView 초기화
        private void InitializeCalendar()
        {
            dgvCalendar.Rows.Clear();
            dgvCalendar.Columns.Clear();

            // 요일 열 추가
            string[] weekDays = { "일", "월", "화", "수", "목", "금", "토" };
            foreach (var day in weekDays)
            {
                dgvCalendar.Columns.Add(day, day);
            }

            // 주 합계 열 추가
            dgvCalendar.Columns.Add("주 합계", "주 합계");
        }

        // 매출 데이터 표시
        private void DisplaySalesData(string targetMonth)
        {
            var (dailySales, monthlyTotal) = GetSalesData(targetMonth);

            // DataGridView 초기화
            InitializeCalendar();

            // 달력 데이터 구성
            DateTime firstDayOfMonth = DateTime.Parse(targetMonth + "-01");
            int daysInMonth = DateTime.DaysInMonth(firstDayOfMonth.Year, firstDayOfMonth.Month);
            int startDayOfWeek = (int)firstDayOfMonth.DayOfWeek;

            int currentRowIndex = dgvCalendar.Rows.Add(); // 첫 번째 행 추가
            int weekTotal = 0;

            for (int i = 1; i <= daysInMonth; i++)
            {
                DateTime currentDate = firstDayOfMonth.AddDays(i - 1);
                int currentColumn = (startDayOfWeek + i - 1) % 7;

                // 해당 날짜 매출 가져오기
                var salesRow = dailySales.Select($"sale_date = '{currentDate:yyyy-MM-dd}'");
                int dailyTotal = salesRow.Length > 0 ? Convert.ToInt32(salesRow[0]["daily_total"]) : 0;

                // 날짜와 매출 설정
                dgvCalendar[currentColumn, currentRowIndex].Value = $"{i}일 - {dailyTotal:N0}원";

                // 주 매출 합계 계산
                weekTotal += dailyTotal;

                // 주가 끝나면 주 매출 합계를 추가
                if (currentColumn == 6 || i == daysInMonth)
                {
                    dgvCalendar["주 합계", currentRowIndex].Value = weekTotal.ToString("N0");

                    // 다음 행 추가
                    if (i != daysInMonth) currentRowIndex = dgvCalendar.Rows.Add();
                    weekTotal = 0; // 새로운 주 매출 합계 초기화
                }
            }

            // 당월 총 매출 표시
            txtMonthlyTotal.Text = $"{monthlyTotal:N0}원";
        }

        // 폼 로드 이벤트
        private void SalesManagement_Load(object sender, EventArgs e)
        {
            string targetMonth = DateTime.Now.ToString("yyyy-MM");
            DisplaySalesData(targetMonth);
        }
    }
}
