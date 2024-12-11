using System;
using System.Data;
using System.Windows.Forms;
using Oracle.DataAccess.Client;

namespace manager
{
    public partial class SalesManagement : Form
    {
        private readonly string connectionString;
        private DateTime selectedMonth;
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
                MessageBox.Show("매출 데이터를 가져오는 중 오류가 발생했습니다.", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Console.WriteLine(ex.ToString());
            }

            return (dailySales, monthlyTotal);
        }

        // DataGridView 초기화
        private void InitializeCalendar()
        {
            dgvCalendar.Rows.Clear();
            dgvCalendar.Columns.Clear();

            string[] weekDays = { "일", "월", "화", "수", "목", "금", "토" };
            foreach (var day in weekDays)
            {
                dgvCalendar.Columns.Add(day, day);
            }

            dgvCalendar.Columns.Add("주 합계", "주 합계");
        }

        // 매출 데이터 표시
        private void DisplaySalesData(string targetMonth)
        {
            var (dailySales, monthlyTotal) = GetSalesData(targetMonth);

            InitializeCalendar();

            DateTime firstDayOfMonth = DateTime.Parse(targetMonth + "-01");
            int daysInMonth = DateTime.DaysInMonth(firstDayOfMonth.Year, firstDayOfMonth.Month);
            int startDayOfWeek = (int)firstDayOfMonth.DayOfWeek;

            int currentRowIndex = dgvCalendar.Rows.Add();
            int weekTotal = 0;

            for (int i = 1; i <= daysInMonth; i++)
            {
                DateTime currentDate = firstDayOfMonth.AddDays(i - 1);
                int currentColumn = (startDayOfWeek + i - 1) % 7;

                var salesRow = dailySales.Select($"sale_date = '{currentDate:yyyy-MM-dd}'");
                int dailyTotal = salesRow.Length > 0 ? Convert.ToInt32(salesRow[0]["daily_total"]) : 0;

                dgvCalendar[currentColumn, currentRowIndex].Value = $"{i}일 - {dailyTotal:N0}원";
                weekTotal += dailyTotal;

                if (currentColumn == 6 || i == daysInMonth)
                {
                    dgvCalendar["주 합계", currentRowIndex].Value = weekTotal.ToString("N0");

                    if (i != daysInMonth) currentRowIndex = dgvCalendar.Rows.Add();
                    weekTotal = 0;
                }
            }

            txtMonthlyTotal.Text = $"{monthlyTotal:N0}원";

            // 레이블 업데이트
            UpdateMonthYearLabel();
        }

        private void UpdateMonthYearLabel()
        {
            lblMonthYear.Text = $"{selectedMonth:yyyy년 MM월}";
        }


        // 폼 로드 이벤트
        private void SalesManagement_Load(object sender, EventArgs e)
        {
            selectedMonth = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            DisplaySalesData(selectedMonth.ToString("yyyy-MM"));
        }

        private void btnPreviousMonth_Click(object sender, EventArgs e)
        {
            selectedMonth = selectedMonth.AddMonths(-1);
            DisplaySalesData(selectedMonth.ToString("yyyy-MM"));
        }

        private void btnNextMonth_Click(object sender, EventArgs e)
        {
            selectedMonth = selectedMonth.AddMonths(1);
            DisplaySalesData(selectedMonth.ToString("yyyy-MM"));
        }
    }
}
