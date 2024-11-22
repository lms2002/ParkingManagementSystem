using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows.Forms;
using Oracle.DataAccess.Client;
namespace ADOform_v7_2163
{
    public partial class Form1 : Form
    {
        private int intID; //ID 필드 설정
        private string strCommand;
        //데이터 삭제, 추가, 업데이트 인지를 설정할 문자열 필드
        private OracleConnection odpConn = new
         OracleConnection();
        public int getintID
        { get { return intID; } }
        public string getstrCommand
        { get { return strCommand; } }
        private void showDataGridView()
        {
            try
            {
                using (OracleConnection conn = new OracleConnection("User Id=hong1;Password=2163;Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=localhost)(PORT=1521))(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME=xe)))"))
                {
                    conn.Open();
                    using (OracleDataAdapter oda = new OracleDataAdapter("SELECT * FROM phone", conn))
                    {
                        DataTable dt = new DataTable();
                        oda.Fill(dt);
                        DBGrid.DataSource = dt;
                        DBGrid.AutoResizeColumns();
                        DBGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                        DBGrid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                        DBGrid.AllowUserToAddRows = false;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("에러 발생 : " + ex.Message);
            }
        }
        public Form1() //자동 생성되니 참고 만 할 것
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            showDataGridView();
        }

        private void 선택한형헙데이트ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            strCommand = "업데이트";
            intID = Convert.ToInt32(DBGrid.SelectedCells[0].Value);
            using (Form2 form2 = new Form2(this))
            {
                form2.ShowDialog();
            }
            showDataGridView();
        }

        private void 선택한형삭제ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            strCommand = "삭제";
            intID = Convert.ToInt32(DBGrid.SelectedCells[0].Value);
            using (Form2 form2 = new Form2(this))
            {
                form2.ShowDialog();
            }
            showDataGridView();
        }

        private void 새로운데이터추가ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            strCommand = "추가";
            using (Form2 form2 = new Form2(this))
            {
                form2.ShowDialog();
            }
            showDataGridView();
        }
    }
}