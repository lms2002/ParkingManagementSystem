using Oracle.DataAccess.Client;
using System;
using System.Data;
using System.Windows.Forms;

namespace manager
{
    public partial class Manager : Form
    {
        private readonly string connectionString = "User Id=ParkingAdmin; Password=1111; Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=localhost)(PORT=1521))(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME=xe)))";

        public Manager()
        {
            InitializeComponent();
            LoadParkingSpotData();
            DisplayCurrentTime();
        }

        private void LoadParkingSpotData()
        {
            try
            {
                using (OracleConnection connection = new OracleConnection(connectionString))
                {
                    connection.Open();
                    string query = @"
                SELECT 
                    COUNT(*) AS TotalSpots,
                    COUNT(CASE WHEN is_disabled = 0 AND is_occupied = 0 THEN 1 END) AS StandardAvailable,
                    COUNT(CASE WHEN is_disabled = 1 AND is_occupied = 0 THEN 1 END) AS DisabledAvailable
                FROM ParkingSpot";

                    using (OracleCommand command = new OracleCommand(query, connection))
                    using (OracleDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            lblTotalSpots.Text = $"총 잔여 자리: {reader["TotalSpots"]}";
                            lblStandardAvailable.Text = $"일반석 빈 자리: {reader["StandardAvailable"]}";
                            lblDisabledAvailable.Text = $"장애석 빈 자리: {reader["DisabledAvailable"]}";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DisplayCurrentTime()
        {
            lblCurrentTime.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        }

        private void 차량관리ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Vehiclemanager form1 = new Vehiclemanager();
            form1.FormClosed += (s, args) => form1.Dispose(); // 자원 정리
            form1.Show();
        }

        private void 매출관리ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SalesManagement form1 = new SalesManagement(connectionString);
            form1.FormClosed += (s, args) => form1.Dispose(); // 자원 정리
            form1.Show();
        }

        private void 주차석관리ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ParkingspotManager form1 = new ParkingspotManager();
            form1.FormClosed += (s, args) => form1.Dispose(); // 자원 정리
            form1.Show();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            DisplayCurrentTime();
        }
    }
}
