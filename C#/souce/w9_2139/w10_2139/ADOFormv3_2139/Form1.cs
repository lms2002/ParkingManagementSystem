﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;
using Oracle.DataAccess.Client;
using ADOFormv3_2139;

namespace ADOFormv1_2139
{
    public partial class Form1 : Form
    {
        //수정하거나 삭제하기 위해 선택된 행의 인덱스를 저장한다.
        private int SelectedRowIndex;

        // Data Provider인 DBAdapter 입니다.
        OracleDataAdapter DBAdapter;

        // DataSet 객체입니다.
        DataSet DS;

        // 추가, 수정, 삭제시에 필요한 명령문을 
        // 자동으로 작성해주는 객체입니다.
        OracleCommandBuilder myCommandBuilder;

        // ataTable 객체입니다.
        DataTable phoneTable;

        // 수정, 삭제할 때 필요한 레코드의 키값을 보관할 필드
        private int SelectedKeyValue;

        private void ClearTextBoxes()
        {
            txtid.Clear();
            txtName.Clear();
            txtPhone.Clear();
            txtMail.Clear();
        }
        private void DB_Open()
        {
            try
            {
                string connectionString = "User Id=lee1; Password=1111; Data Source=(DESCRIPTION =   (ADDRESS = (PROTOCOL = TCP)(HOST = localhost)(PORT = 1521))   (CONNECT_DATA =     (SERVER = DEDICATED)     (SERVICE_NAME = xe)   ) );";

                string commandString = "select * from Phone";

                DBAdapter = new OracleDataAdapter(commandString, connectionString);
                myCommandBuilder = new OracleCommandBuilder(DBAdapter);

                DS = new DataSet();
            }
            catch (DataException DE)
            {
                MessageBox.Show(DE.Message);
            }
        }

        public Form1()
        {
            InitializeComponent();
            DB_Open();//*
        }

        private void DAOpenBtn_Click(object sender, EventArgs e)
        {
            try
            {
                DS.Clear();
                DBAdapter.Fill(DS, "phone");
                //  phoneTable = DS.Tables["Phone"];//*
                DBGrid.DataSource = DS.Tables["phone"].DefaultView;
            }
            catch (DataException DE)
            {
                MessageBox.Show(DE.Message);
            }
            catch (Exception DE)
            {
                MessageBox.Show(DE.Message);
            }

        }
        private void AppendBtn_Click(object sender, EventArgs e)
        {
            try
            {
                MessageBox.Show("텍스트 상자에 모든 데이터 입력하셨으면 추가합니다!");
                // DS.Clear();//*

                //  DBAdapter.Fill(DS, "Phone");//*

                phoneTable = DS.Tables["Phone"];//*
                DataRow newRow = phoneTable.NewRow();
                newRow["id"] = Convert.ToInt32(txtid.Text);
                newRow["PName"] = txtName.Text;
                newRow["Phone"] = txtPhone.Text;
                newRow["Email"] = txtMail.Text;

                phoneTable.Rows.Add(newRow);
                DBAdapter.Update(DS, "Phone");
                DS.AcceptChanges();//*
                ClearTextBoxes();  //*
                DBGrid.DataSource = DS.Tables["Phone"].DefaultView;
            }
            catch (DataException DE)
            {
                MessageBox.Show(DE.Message);
            }
            catch (Exception DE)
            {
                MessageBox.Show(DE.Message);
            }
        }
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                // DataSet DS = new DataSet();//*
                //DBAdapter.Fill(DS, "Phone");

                DataTable phoneTable = DS.Tables["Phone"];

                if (e.RowIndex < 0)
                {
                    // DBGrid의 컬럼 헤더를 클릭하면 컬럼을 정렬하므로
                    // 아무 메시지도 띄우지 않습니다.
                    return;
                }
                else if (e.RowIndex > phoneTable.Rows.Count - 1)
                {
                    MessageBox.Show("해당하는 데이터가 존재하지 않습니다.");
                    return;
                }

                DataRow currRow = phoneTable.Rows[e.RowIndex];
                txtid.Text = currRow["id"].ToString();
                txtName.Text = currRow["PName"].ToString();
                txtPhone.Text = currRow["Phone"].ToString();
                txtMail.Text = currRow["EMail"].ToString();

                SelectedRowIndex = Convert.ToInt32(currRow["id"]);
            }
            catch (DataException DE)
            {
                MessageBox.Show(DE.Message);
            }
            catch (Exception DE)
            {
                MessageBox.Show(DE.Message);
            }
        }
        private void UpdateBtn_Click(object sender, EventArgs e)
        {
            try
            {
                //DS.Clear();    
                //DBAdapter.Fill(DS, "Phone");


                phoneTable = DS.Tables["Phone"];//*
                DataColumn[] PrimaryKey = new DataColumn[1];
                PrimaryKey[0] = phoneTable.Columns["id"];
                phoneTable.PrimaryKey = PrimaryKey;

                DataRow currRow = phoneTable.Rows.Find(SelectedRowIndex);


                currRow.BeginEdit();
                currRow["id"] = txtid.Text;
                currRow["PName"] = txtName.Text;
                currRow["Phone"] = txtPhone.Text;
                currRow["EMail"] = txtMail.Text;
                currRow.EndEdit();

                DataSet UpdatedSet = DS.GetChanges(DataRowState.Modified);
                if (UpdatedSet.HasErrors)
                {
                    MessageBox.Show("변경된 데이터에 문제가 있습니다.");
                }
                else
                {
                    DBAdapter.Update(UpdatedSet, "Phone");
                    DS.AcceptChanges();
                }

                DBGrid.DataSource = DS.Tables["Phone"].DefaultView;

            }
            catch (DataException DE)
            {
                MessageBox.Show(DE.Message);
            }
            catch (Exception DE)
            {
                MessageBox.Show(DE.Message);
            }
        }

        private void DeleteBtn_Click(object sender, EventArgs e)
        {
            try
            {
                // DS.Clear();
                // DBAdapter.Fill(DS, "Phone");


                phoneTable = DS.Tables["Phone"];//*
                DataColumn[] PrimaryKey = new DataColumn[1];
                PrimaryKey[0] = phoneTable.Columns["id"];
                phoneTable.PrimaryKey = PrimaryKey;

                DataRow currRow = phoneTable.Rows.Find(SelectedRowIndex);
                currRow.Delete();

                DBAdapter.Update(DS.GetChanges(DataRowState.Deleted), "Phone");
                DBGrid.DataSource = DS.Tables["Phone"].DefaultView;
            }
            catch (DataException DE)
            {
                MessageBox.Show(DE.Message);
            }
            catch (Exception DE)
            {
                MessageBox.Show(DE.Message);
            }
        }

        private void DBGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                string connectionString = "User Id=lee1; Password=1111; Data Source=(DESCRIPTION = (ADDRESS = (PROTOCOL = TCP)(HOST = localhost)(PORT = 1521)) (CONNECT_DATA = (SERVER = DEDICATED)(SERVICE_NAME = xe)) ); ";
                string commandString = "select * from Phone";
                OracleDataAdapter DBAdapter = new OracleDataAdapter(commandString, connectionString);
                DataSet DS = new DataSet();
                DBAdapter.Fill(DS, "Phone");
                DataTable phoneTable = DS.Tables["Phone"];
                if (e.RowIndex < 0)
                {
                    return;
                }
                else if (e.RowIndex > phoneTable.Rows.Count - 1)
                {
                    MessageBox.Show("해당하는 데이터가 존재하지 않습니다.");
                    return;
                }
                DataRow currRow = phoneTable.Rows[e.RowIndex];
                txtid.Text = currRow["id"].ToString();
                txtName.Text = currRow["PName"].ToString();
                txtPhone.Text = currRow["Phone"].ToString();
                txtMail.Text = currRow["EMail"].ToString();
                SelectedRowIndex = Convert.ToInt32(currRow["id"]);
            }
            catch (DataException DE)
            { MessageBox.Show(DE.Message); }
            catch (Exception DE)
            { MessageBox.Show(DE.Message); }
        }

        private void SearchBtn_Click(object sender, EventArgs e)
        {
            Form2 frm2 = new Form2();
            frm2.Owner = this;
            frm2.ShowDialog();
            frm2.Dispose();
        }
        public String TxtS
        {
            get { return txtSearch.Text; }
            set { txtSearch.Text = value.ToString(); }
        }
    }

}
