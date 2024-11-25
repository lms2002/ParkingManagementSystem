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

namespace Management
{
    public partial class Form2 : Form
    {
        private OracleConnection odpConn = new OracleConnection();
        private Form1 _parent;

        public Form2(Form1 inform1)
        {
            InitializeComponent();
            _parent = inform1;
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            // 명령어에 따라 폼 초기화
            if (_parent.getstrCommand == "삭제")
            {
                btnOK.Text = "삭제";
                txtVehicleNumber.Enabled = false;
                LoadVehicleData();
            }
            else if (_parent.getstrCommand == "업데이트")
            {
                btnOK.Text = "업데이트";
                txtVehicleNumber.Enabled = false;
                LoadVehicleData();
            }
            else
            {
                btnOK.Text = "추가";
            }
        }

        // 차량 데이터 로드 (수정/삭제 시 사용)
        private void LoadVehicleData()
        {
            odpConn.ConnectionString = "User Id=ParkingAdmin; Password=1111; Data Source=(DESCRIPTION = (ADDRESS = (PROTOCOL = TCP)(HOST = localhost)(PORT = 1521)) (CONNECT_DATA = (SERVER = DEDICATED) (SERVICE_NAME = xe)));";
            odpConn.Open();

            string query = "SELECT * FROM Vehicle WHERE vehicle_number = :vehicle_number";
            OracleCommand cmd = new OracleCommand(query, odpConn);
            cmd.Parameters.Add("vehicle_number", OracleDbType.Varchar2).Value = _parent.getVehicleNumber;

            OracleDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                txtVehicleNumber.Text = reader["vehicle_number"].ToString();
                txtVehicleType.Text = reader["vehicle_type"].ToString();
                txtParkingFee.Text = reader["parking_fee"].ToString();
            }

            reader.Close();
            odpConn.Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (btnOK.Text == "추가")
            {
                if (InsertVehicle() > 0)
                    MessageBox.Show("차량이 추가되었습니다.");
                else
                    MessageBox.Show("차량 추가 실패!");
            }
            else if (btnOK.Text == "삭제")
            {
                if (DeleteVehicle() > 0)
                    MessageBox.Show("차량이 삭제되었습니다.");
                else
                    MessageBox.Show("차량 삭제 실패!");
            }
            else if (btnOK.Text == "업데이트")
            {
                if (UpdateVehicle() > 0)
                    MessageBox.Show("차량이 수정되었습니다.");
                else
                    MessageBox.Show("차량 수정 실패!");
            }

            this.Close();
        }

        private int InsertVehicle()
        {
            odpConn.ConnectionString = "User Id=ParkingAdmin; Password=1111; Data Source=(DESCRIPTION = (ADDRESS = (PROTOCOL = TCP)(HOST = localhost)(PORT = 1521)) (CONNECT_DATA = (SERVER = DEDICATED) (SERVICE_NAME = xe)));";
            odpConn.Open();

            string query = "INSERT INTO Vehicle (vehicle_number, vehicle_type, parking_fee) VALUES (:vehicle_number, :vehicle_type, :parking_fee)";
            OracleCommand cmd = new OracleCommand(query, odpConn);
            cmd.Parameters.Add("vehicle_number", OracleDbType.Varchar2).Value = txtVehicleNumber.Text.Trim();
            cmd.Parameters.Add("vehicle_type", OracleDbType.Varchar2).Value = txtVehicleType.Text.Trim();
            cmd.Parameters.Add("parking_fee", OracleDbType.Int32).Value = txtParkingFee.Text.Trim();

            int result = cmd.ExecuteNonQuery();
            odpConn.Close();
            return result;
        }

        private int DeleteVehicle()
        {
            odpConn.ConnectionString = "User Id=ParkingAdmin; Password=1111; Data Source=(DESCRIPTION = (ADDRESS = (PROTOCOL = TCP)(HOST = localhost)(PORT = 1521)) (CONNECT_DATA = (SERVER = DEDICATED) (SERVICE_NAME = xe)));";
            odpConn.Open();

            string query = "DELETE FROM Vehicle WHERE vehicle_number = :vehicle_number";
            OracleCommand cmd = new OracleCommand(query, odpConn);
            cmd.Parameters.Add("vehicle_number", OracleDbType.Varchar2).Value = _parent.getVehicleNumber;

            int result = cmd.ExecuteNonQuery();
            odpConn.Close();
            return result;
        }

        private int UpdateVehicle()
        {
            odpConn.ConnectionString = "User Id=ParkingAdmin; Password=1111; Data Source=(DESCRIPTION = (ADDRESS = (PROTOCOL = TCP)(HOST = localhost)(PORT = 1521)) (CONNECT_DATA = (SERVER = DEDICATED) (SERVICE_NAME = xe)));";
            odpConn.Open();

            string query = "UPDATE Vehicle SET vehicle_type = :vehicle_type, parking_fee = :parking_fee WHERE vehicle_number = :vehicle_number";
            OracleCommand cmd = new OracleCommand(query, odpConn);
            cmd.Parameters.Add("vehicle_type", OracleDbType.Varchar2).Value = txtVehicleType.Text.Trim();
            cmd.Parameters.Add("parking_fee", OracleDbType.Int32).Value = txtParkingFee.Text.Trim();
            cmd.Parameters.Add("vehicle_number", OracleDbType.Varchar2).Value = _parent.getVehicleNumber;

            int result = cmd.ExecuteNonQuery();
            odpConn.Close();
            return result;
        }
    }
}
