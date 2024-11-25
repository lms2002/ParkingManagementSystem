using Oracle.DataAccess.Client;
using System;
using System.Data;
using System.Windows.Forms;

namespace Management
{
    public partial class Form1 : Form
    {
        public string getstrCommand; // ContextMenuStrip에서 선택한 명령어
        public string getVehicleNumber; // 선택한 차량 번호
        public Form1()
        {
            InitializeComponent();
        }

        private void searchB1_Click(object sender, EventArgs e)
        {
            string connectionString = "User Id=ParkingAdmin; Password=1111; Data Source=(DESCRIPTION = (ADDRESS = (PROTOCOL = TCP)(HOST = localhost)(PORT = 1521)) (CONNECT_DATA = (SERVER = DEDICATED) (SERVICE_NAME = xe)));";
            OracleConnection connection = new OracleConnection(connectionString);

            try
            {
                connection.Open();

                // SQL 쿼리: 모든 차량 정보 검색
                string query = "SELECT * FROM Vehicle";
                OracleDataAdapter adapter = new OracleDataAdapter(query, connection);

                DataSet dataSet = new DataSet();
                adapter.Fill(dataSet, "Vehicle");

                // DataGridView에 데이터 바인딩
                dataGridVehicles.DataSource = dataSet.Tables["Vehicle"];
            }
            catch (Exception ex)
            {
                MessageBox.Show("오류 발생: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }

        private void searchB2_Click(object sender, EventArgs e)
        {
            string connectionString = "User Id=ParkingAdmin; Password=1111; Data Source=(DESCRIPTION = (ADDRESS = (PROTOCOL = TCP)(HOST = localhost)(PORT = 1521)) (CONNECT_DATA = (SERVER = DEDICATED) (SERVICE_NAME = xe)));";
            OracleConnection connection = new OracleConnection(connectionString);

            try
            {
                connection.Open();

                // SQL 쿼리: 차량 번호 검색
                string query = "SELECT * FROM Vehicle WHERE vehicle_number = :vehicle_number";
                OracleCommand command = new OracleCommand(query, connection);
                command.Parameters.Add("vehicle_number", OracleDbType.Varchar2, 10);
                command.Parameters["vehicle_number"].Value = txtSearch.Text.Trim();

                // 검색 결과를 ListBox에 추가
                OracleDataReader reader = command.ExecuteReader();
                listBoxResults.Items.Clear(); // 기존 항목 초기화
                while (reader.Read())
                {
                    listBoxResults.Items.Add("차량 번호: " + reader["vehicle_number"].ToString());
                    listBoxResults.Items.Add("차량 유형: " + reader["vehicle_type"].ToString());
                    listBoxResults.Items.Add("주차 요금: " + reader["parking_fee"].ToString());
                    listBoxResults.Items.Add("-------------------------");
                }

                reader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("오류 발생: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }

        private void 선택형업데이트ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            getstrCommand = "추가";
            Form2 form2 = new Form2(this);
            form2.ShowDialog();
        }

        private void 선택차량삭제ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtSearch.Text))
            {
                getstrCommand = "삭제";
                getVehicleNumber = txtSearch.Text.Trim();
                Form2 form2 = new Form2(this);
                form2.ShowDialog();
            }
            else
            {
                MessageBox.Show("삭제할 차량 번호를 입력하세요.");
            }
        }

        private void 선택차량수정ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtSearch.Text))
            {
                getstrCommand = "업데이트";
                getVehicleNumber = txtSearch.Text.Trim();
                Form2 form2 = new Form2(this);
                form2.ShowDialog();
            }
            else
            {
                MessageBox.Show("수정할 차량 번호를 입력하세요.");
            }
        }
    }
}