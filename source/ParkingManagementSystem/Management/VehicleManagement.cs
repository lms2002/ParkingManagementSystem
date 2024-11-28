using Management;
using Oracle.DataAccess.Client;
using System;
using System.Windows.Forms;

namespace VehicleManagement
{
    public partial class VehicleManagement : Form
    {
        private OracleConnection conn = new OracleConnection("User Id=ParkingAdmin; Password=1111; Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=localhost)(PORT=1521))(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME=xe)));");
        private Management.Management parent;

        public VehicleManagement(Management.Management parentForm)
        {
            InitializeComponent();
            parent = parentForm;
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            if (parent.Command == "삭제")
            {
                btnOK.Text = "삭제";
                txtId.Enabled = false;
                txtNumber.Enabled = false;
                txtType.Enabled = false;
                LoadVehicleDetails();
            }
            else if (parent.Command == "수정")
            {
                btnOK.Text = "수정";
                txtId.Enabled = false;
                LoadVehicleDetails();
            }
            else
            {
                btnOK.Text = "추가";
            }
        }

        private void LoadVehicleDetails()
        {
            try
            {
                conn.Open();
                string query = "SELECT * FROM Vehicle WHERE vehicle_id = :id";
                OracleCommand cmd = new OracleCommand(query, conn);
                cmd.Parameters.Add("id", OracleDbType.Int32).Value = parent.GetVehicleId;
                OracleDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    txtId.Text = reader["vehicle_id"].ToString();
                    txtNumber.Text = reader["vehicle_number"].ToString();
                    txtType.Text = reader["vehicle_type"].ToString();
                }
                reader.Close();
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("에러 발생: " + ex.Message);
                conn.Close();
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (btnOK.Text == "추가")
            {
                if (InsertVehicle() > 0)
                {
                    MessageBox.Show("정상적으로 추가되었습니다.");
                }
                else
                {
                    MessageBox.Show("추가 실패!");
                }
            }
            else if (btnOK.Text == "삭제")
            {
                if (DeleteVehicle() > 0)
                {
                    MessageBox.Show("정상적으로 삭제되었습니다.");
                }
                else
                {
                    MessageBox.Show("삭제 실패!");
                }
            }
            else if (btnOK.Text == "수정")
            {
                if (UpdateVehicle() > 0)
                {
                    MessageBox.Show("정상적으로 수정되었습니다.");
                }
                else
                {
                    MessageBox.Show("수정 실패!");
                }
            }
            this.Close();
        }

        private int InsertVehicle()
        {
            try
            {
                conn.Open();
                string query = "INSERT INTO Vehicle (vehicle_id, vehicle_number, vehicle_type) VALUES (VehicleSeq.NEXTVAL, :number, :type)";
                OracleCommand cmd = new OracleCommand(query, conn);
                cmd.Parameters.Add("number", OracleDbType.Varchar2).Value = txtNumber.Text.Trim();
                cmd.Parameters.Add("type", OracleDbType.Varchar2).Value = txtType.Text.Trim();
                int result = cmd.ExecuteNonQuery();
                conn.Close();
                return result;
            }
            catch (Exception ex)
            {
                MessageBox.Show("에러 발생: " + ex.Message);
                conn.Close();
                return 0;
            }
        }

        private int DeleteVehicle()
        {
            try
            {
                conn.Open();
                string query = "DELETE FROM Vehicle WHERE vehicle_id = :id";
                OracleCommand cmd = new OracleCommand(query, conn);
                cmd.Parameters.Add("id", OracleDbType.Int32).Value = parent.GetVehicleId;
                int result = cmd.ExecuteNonQuery();
                conn.Close();
                return result;
            }
            catch (Exception ex)
            {
                MessageBox.Show("에러 발생: " + ex.Message);
                conn.Close();
                return 0;
            }
        }

        private int UpdateVehicle()
        {
            try
            {
                conn.Open();
                string query = "UPDATE Vehicle SET vehicle_number = :number, vehicle_type = :type WHERE vehicle_id = :id";
                OracleCommand cmd = new OracleCommand(query, conn);
                cmd.Parameters.Add("number", OracleDbType.Varchar2).Value = txtNumber.Text.Trim();
                cmd.Parameters.Add("type", OracleDbType.Varchar2).Value = txtType.Text.Trim();
                cmd.Parameters.Add("id", OracleDbType.Int32).Value = parent.GetVehicleId;
                int result = cmd.ExecuteNonQuery();
                conn.Close();
                return result;
            }
            catch (Exception ex)
            {
                MessageBox.Show("에러 발생: " + ex.Message);
                conn.Close();
                return 0;
            }
        }
    }
}
