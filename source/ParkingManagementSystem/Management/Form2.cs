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
        private OracleConnection oracleConnection = new OracleConnection();
        Form1 parentForm;

        public Form2(Form1 form1)
        {
            InitializeComponent();
            parentForm = form1;
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            if (parentForm.GetCommandType == "Delete")
            {
                btnOK.Text = "Delete";
                txtVehicleNumber.Enabled = false;
                InitializeTextBoxes();
            }
            else if (parentForm.GetCommandType == "Update")
            {
                btnOK.Text = "Update";
                txtVehicleNumber.Enabled = true;
                InitializeTextBoxes();
            }
            else
            {
                btnOK.Text = "Add";
            }
        }
        private void InitializeTextBoxes() // Custom function to initialize text boxes
        {
            oracleConnection.ConnectionString = "User Id=ParkingAdmin;Password=1111;Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=localhost)(PORT=1521))(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME=xe)))";
            oracleConnection.Open();
            string query = "SELECT * FROM Vehicle WHERE vehicle_id = :id";
            OracleCommand oracleCommand = new OracleCommand(query, oracleConnection);
            oracleCommand.Parameters.Add("number", OracleDbType.Varchar2, 10).Value = txtVehicleNumber.Text.Trim();
            OracleDataReader dataReader = oracleCommand.ExecuteReader();
            while (dataReader.Read())
            {
                txtVehicleNumber.Text = Convert.ToString(dataReader.GetValue(1));
                txtVehicleType.Text = Convert.ToString(dataReader.GetValue(2));
            }
            dataReader.Close();
            oracleConnection.Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (btnOK.Text == "Add")
            {
                if (InsertRow() > 0)
                {
                    MessageBox.Show("Vehicle added successfully.");
                }
                else
                {
                    MessageBox.Show("Failed to add vehicle.");
                }
                this.Close();
            }
            else if (btnOK.Text == "Delete")
            {
                if (DeleteRow() > 0)
                {
                    MessageBox.Show("Vehicle deleted successfully.");
                }
                else
                {
                    MessageBox.Show("Failed to delete vehicle.");
                }
                this.Close();
            }
            else
            {
                if (UpdateRow() > 0)
                {
                    MessageBox.Show("Vehicle updated successfully.");
                }
                else
                {
                    MessageBox.Show("Failed to update vehicle.");
                }
                this.Close();
            }
        }
        private int InsertRow() // Custom function to add a new vehicle
        {
            oracleConnection.ConnectionString = "User Id=ParkingAdmin;Password=1111;Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=localhost)(PORT=1521))(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME=xe)))";
            oracleConnection.Open();
            string query = "INSERT INTO Vehicle (vehicle_number, vehicle_type) VALUES (:number, :type)";
            OracleCommand oracleCommand = new OracleCommand(query, oracleConnection);

            oracleCommand.Parameters.Add("number", OracleDbType.Varchar2, 10).Value = txtVehicleNumber.Text.Trim();
            oracleCommand.Parameters.Add("type", OracleDbType.Varchar2, 10).Value = txtVehicleType.Text.Trim();
            return oracleCommand.ExecuteNonQuery();
        }
        private int DeleteRow() // Custom function to delete a vehicle
        {
            oracleConnection.ConnectionString = "User Id=ParkingAdmin;Password=1111;Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=localhost)(PORT=1521))(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME=xe)))";
            oracleConnection.Open();
            string query = "DELETE FROM Vehicle WHERE vehicle_number = :number";
            OracleCommand oracleCommand = new OracleCommand(query, oracleConnection);
            oracleCommand.Parameters.Add("number", OracleDbType.Varchar2, 10).Value = txtVehicleNumber.Text.Trim();
            return oracleCommand.ExecuteNonQuery();
        }
        private int UpdateRow() // Custom function to update vehicle details
        {
            oracleConnection.ConnectionString = "User Id=ParkingAdmin;Password=1111;Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=localhost)(PORT=1521))(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME=xe)))";
            oracleConnection.Open();
            string query = "UPDATE Vehicle SET vehicle_type = :type WHERE vehicle_number = :number";
            OracleCommand oracleCommand = new OracleCommand(query, oracleConnection);
            oracleCommand.Parameters.Add("number", OracleDbType.Varchar2, 10).Value = txtVehicleNumber.Text.Trim();
            oracleCommand.Parameters.Add("type", OracleDbType.Varchar2, 10).Value = txtVehicleType.Text.Trim();
            oracleCommand.Parameters.Add("id", OracleDbType.Int32).Value = parentForm.GetVehicleId;
            return oracleCommand.ExecuteNonQuery();
        }
    }
}
