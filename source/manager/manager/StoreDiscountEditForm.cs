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
    public partial class StoreDiscountEditForm : Form
    {
        private readonly string connectionString;
        private readonly string mode; // Add or Edit
        private readonly string storeName;
        public StoreDiscountEditForm(string connectionString, string mode, string storeName)
        {
            InitializeComponent();
            this.connectionString = connectionString;
            this.mode = mode;
            this.storeName = storeName;

            if (mode == "Edit")
            {
                LoadStoreDiscount();
                txtStoreName.ReadOnly = true; // store_name 수정 불가
            }
        }
        private void LoadStoreDiscount()
        {
            string query = "SELECT discount_percentage, discount_condition FROM StoreDiscount WHERE store_name = :store_name";

            try
            {
                using (var connection = new OracleConnection(connectionString))
                {
                    connection.Open();
                    using (var command = new OracleCommand(query, connection))
                    {
                        command.Parameters.Add(new OracleParameter("store_name", storeName));
                        using (var reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                txtDiscountPercentage.Text = reader["discount_percentage"].ToString();
                                txtDiscountCondition.Text = reader["discount_condition"].ToString();
                                txtStoreName.Text = storeName;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"데이터를 로드하는 중 오류가 발생했습니다: {ex.Message}", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string query = mode == "Add"
            ? @"INSERT INTO StoreDiscount (store_name, discount_percentage, discount_condition, discount_date)
                 VALUES (:store_name, :discount_percentage, :discount_condition, SYSDATE)"
            : @"UPDATE StoreDiscount 
                 SET discount_percentage = :discount_percentage, 
                     discount_condition = :discount_condition, 
                     discount_date = SYSDATE 
                 WHERE store_name = :store_name";

            try
            {
                using (var connection = new OracleConnection(connectionString))
                {
                    connection.Open();
                    using (var command = new OracleCommand(query, connection))
                    {
                        command.Parameters.Add(new OracleParameter("store_name", txtStoreName.Text));
                        command.Parameters.Add(new OracleParameter("discount_percentage", txtDiscountPercentage.Text));
                        command.Parameters.Add(new OracleParameter("discount_condition", txtDiscountCondition.Text));

                        command.ExecuteNonQuery();
                        MessageBox.Show("저장되었습니다.", "정보", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Close();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"저장하는 중 오류가 발생했습니다: {ex.Message}", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
