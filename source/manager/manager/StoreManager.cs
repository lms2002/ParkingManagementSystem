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
    public partial class StoreManager : Form
    {
        private readonly string connectionString;
        public StoreManager(string connectionString)
        {
            InitializeComponent();
            this.connectionString = connectionString;
            LoadStoreNames();
        }
        private void LoadStoreNames()
        {
            // ListView에 store_name만 표시
            string query = "SELECT DISTINCT store_name FROM StoreDiscount ORDER BY store_name";
            lvStoreList.Items.Clear();

            try
            {
                using (var connection = new OracleConnection(connectionString))
                {
                    connection.Open();
                    using (var command = new OracleCommand(query, connection))
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            lvStoreList.Items.Add(reader["store_name"].ToString());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"데이터를 로드하는 중 오류가 발생했습니다: {ex.Message}", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void LoadStoreDetails(string storeName)
        {
            // DataGridView에 store_name에 해당하는 상세 정보 표시
            string query = @"
                SELECT 
                    discount_percentage AS ""할인율"",
                    discount_condition AS ""조건"",
                    discount_date AS ""등록 날짜""
                FROM StoreDiscount
                WHERE store_name = :store_name";

            try
            {
                using (var connection = new OracleConnection(connectionString))
                {
                    connection.Open();
                    using (var command = new OracleCommand(query, connection))
                    {
                        command.Parameters.Add(new OracleParameter("store_name", storeName));
                        using (var adapter = new OracleDataAdapter(command))
                        {
                            var dataTable = new DataTable();
                            adapter.Fill(dataTable);
                            dgvStoreDetails.DataSource = dataTable;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"상세 데이터를 로드하는 중 오류가 발생했습니다: {ex.Message}", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void lvStoreList_SelectedIndexChanged(object sender, EventArgs e)
        {
            // ListView 항목 선택 시 DataGridView 데이터 로드
            if (lvStoreList.SelectedItems.Count > 0)
            {
                string selectedStore = lvStoreList.SelectedItems[0].Text;
                LoadStoreDetails(selectedStore);
            }
        }

        private void cmsStoreOptions_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            // ContextMenuStrip 메뉴 클릭 이벤트
            if (lvStoreList.SelectedItems.Count == 0)
            {
                MessageBox.Show("먼저 상점을 선택하세요.", "경고", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string selectedStore = lvStoreList.SelectedItems[0].Text;

            if (e.ClickedItem.Name == "btnAdd")
            {
                using (var addForm = new StoreDiscountEditForm(connectionString, "Add", null))
                {
                    addForm.ShowDialog();
                    LoadStoreNames(); // 새로고침
                }
            }
            else if (e.ClickedItem.Name == "btnEdit")
            {
                using (var editForm = new StoreDiscountEditForm(connectionString, "Edit", selectedStore))
                {
                    editForm.ShowDialog();
                    LoadStoreDetails(selectedStore); // 새로고침
                }
            }
            else if (e.ClickedItem.Name == "btnDelete")
            {
                DeleteStoreDiscount(selectedStore);
            }
        }
        private void DeleteStoreDiscount(string storeName)
        {
            // 삭제 작업
            string query = "DELETE FROM StoreDiscount WHERE store_name = :store_name";

            try
            {
                using (var connection = new OracleConnection(connectionString))
                {
                    connection.Open();
                    using (var command = new OracleCommand(query, connection))
                    {
                        command.Parameters.Add(new OracleParameter("store_name", storeName));
                        var result = command.ExecuteNonQuery();

                        if (result > 0)
                        {
                            MessageBox.Show("할인 정보가 삭제되었습니다.", "정보", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LoadStoreNames(); // 새로고침
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"삭제하는 중 오류가 발생했습니다: {ex.Message}", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
