using Oracle.DataAccess.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace manager
{
    public partial class Manager : Form
    {
        private readonly string connectionString = "User Id=ParkingAdmin; Password=1111; Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=localhost)(PORT=1521))(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME=xe)))";
        public Manager()
        {
            InitializeComponent();
        }

        private void 차량관리ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (Vehiclemanager form1 = new Vehiclemanager())
            {
                form1.ShowDialog();
            }
        }

        private void 매출관리ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (SalesManagement form1 = new SalesManagement(connectionString))
            {
                form1.ShowDialog();
            }
        }

        private void 주차석관리ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (ParkingspotManager form1 = new ParkingspotManager())
            {
                form1.ShowDialog();
            }
        }

        private void Manager_Load(object sender, EventArgs e)
        {
            LoadStoreDiscounts();
        }
        private void LoadStoreDiscounts()
        {
            string query = @"
                SELECT 
                    discount_id AS StoreID,
                    store_name AS StoreName,
                    discount_percentage AS DiscountPercentage,
                    discount_condition AS DiscountCondition
                FROM StoreDiscount
                ORDER BY store_name";

            try
            {
                using (var connection = new OracleConnection(connectionString))
                {
                    connection.Open();
                    using (var command = new OracleCommand(query, connection))
                    using (var reader = command.ExecuteReader())
                    {
                        lvStoreDiscount.Items.Clear(); // 기존 항목 초기화
                        while (reader.Read())
                        {
                            var item = new ListViewItem(reader["StoreID"].ToString());
                            item.SubItems.Add(reader["StoreName"].ToString());
                            item.SubItems.Add(reader["DiscountPercentage"].ToString());
                            item.SubItems.Add(reader["DiscountCondition"].ToString());
                            lvStoreDiscount.Items.Add(item);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"데이터를 로드하는 중 오류가 발생했습니다: {ex.Message}", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
