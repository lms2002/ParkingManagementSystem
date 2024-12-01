using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Oracle.DataAccess.Client;

namespace manager
{
    public partial class VehicleCRUD : Form
    {
        private readonly string connectionString;
        private readonly string commandMode; // 추가, 삭제, 수정 모드
        private readonly int vehicleId; // 선택된 Vehicle ID (삭제/수정 시)
        public VehicleCRUD(string connectionString, string commandMode, int vehicleId = -1)
        {
            InitializeComponent();
            this.connectionString = connectionString;
            this.commandMode = commandMode;
            this.vehicleId = vehicleId;
            InitializeForm();
        }
        private void InitializeForm()
        {
            if (commandMode == "추가")
            {
                btnOK.Text = "추가";
                txtId.Text = GenerateNextVehicleId().ToString();
                txtId.ReadOnly = true;
            }
            else if (commandMode == "삭제" || commandMode == "수정")
            {
                LoadVehicleDetails();
                txtId.ReadOnly = true;
                if (commandMode == "삭제")
                {
                    txtNumber.ReadOnly = true;
                    txtType.ReadOnly = true;
                    btnOK.Text = "삭제";
                }
                else if (commandMode == "수정")
                {
                    txtNumber.ReadOnly = false;
                    txtType.ReadOnly = false;
                    btnOK.Text = "수정";
                }
            }
        }

        private int GenerateNextVehicleId()
        {
            using (var connection = new OracleConnection(connectionString))
            {
                connection.Open();
                var query = "SELECT NVL(MAX(vehicle_id), 0) + 1 AS next_id FROM Vehicle";
                var command = new OracleCommand(query, connection);

                return Convert.ToInt32(command.ExecuteScalar());
            }
        }
        private void LoadVehicleDetails()
        {
            using (var connection = new OracleConnection(connectionString))
            {
                connection.Open();
                var query = "SELECT * FROM Vehicle WHERE vehicle_id = :id";
                using (var command = new OracleCommand(query, connection))
                {
                    // 바인드 변수 이름과 SQL 쿼리의 :id가 동일한지 확인
                    command.Parameters.Add(new OracleParameter("id", vehicleId));

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            txtId.Text = reader["vehicle_id"].ToString();
                            txtNumber.Text = reader["vehicle_number"].ToString();
                            txtType.Text = reader["vehicle_type"].ToString();
                        }
                    }
                }
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                using (var connection = new OracleConnection(connectionString))
                {
                    connection.Open();

                    if (btnOK.Text == "추가")
                    {
                        var query = "INSERT INTO Vehicle (vehicle_id, vehicle_number, vehicle_type) VALUES (:vehicle_id, :vehicle_number, :vehicle_type)";
                        using (var command = new OracleCommand(query, connection))
                        {
                            // 변수 이름이 SQL 쿼리와 일치해야 함
                            command.Parameters.Add(new OracleParameter(":vehicle_id", OracleDbType.Int32)).Value = int.Parse(txtId.Text.Trim());
                            command.Parameters.Add(new OracleParameter(":vehicle_number", OracleDbType.Varchar2)).Value = txtNumber.Text.Trim();
                            command.Parameters.Add(new OracleParameter(":vehicle_type", OracleDbType.Varchar2)).Value = txtType.Text.Trim();

                            command.ExecuteNonQuery();
                        }
                        MessageBox.Show("차량이 추가되었습니다.");
                    }
                    else if (commandMode == "삭제")
                    {
                        var query = "DELETE FROM Vehicle WHERE vehicle_id = :id";
                        using (var command = new OracleCommand(query, connection))
                        {
                            command.Parameters.Add(new OracleParameter("id", vehicleId));
                            command.ExecuteNonQuery();
                        }
                        MessageBox.Show("차량이 삭제되었습니다.");
                    }
                    else if (btnOK.Text == "수정")
                    {
                        var query = "UPDATE Vehicle SET vehicle_number = :vehicle_number, vehicle_type = :vehicle_type WHERE vehicle_id = :vehicle_id";
                        using (var command = new OracleCommand(query, connection))
                        {
                            // 바인딩 변수 이름과 SQL 쿼리가 일치하도록 수정
                            command.Parameters.Add(new OracleParameter("vehicle_number", txtNumber.Text.Trim()));
                            command.Parameters.Add(new OracleParameter("vehicle_type", txtType.Text.Trim()));
                            command.Parameters.Add(new OracleParameter("vehicle_id", OracleDbType.Int32)).Value = vehicleId;

                            command.ExecuteNonQuery();
                        }
                        MessageBox.Show("차량 정보가 수정되었습니다.");
                    }

                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"작업 중 오류 발생: {ex.Message}");
            }
        }

        private void VehicleCRUD_Load(object sender, EventArgs e)
        {

        }
    }
}
