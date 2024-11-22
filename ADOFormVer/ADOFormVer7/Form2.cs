﻿using Oracle.DataAccess.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace ADOform_v7_2163
{
    public partial class Form2 : Form
    {
        private OracleConnection odpConn = new
        OracleConnection();
        Form1 _parent;

        public Form2(Form1 inform1)
        {
            InitializeComponent();
            _parent = inform1;
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            if (_parent.getstrCommand == "삭제")
            {
                btnOK.Text = "삭제";
                txtid.Enabled = false;
                initialTextBoxes();
            }
            else if (_parent.getstrCommand == "업데이트")
            {
                btnOK.Text = "업데이트";
                txtid.Enabled = false;
                txtName.Enabled = false;
                initialTextBoxes();
            }
            else btnOK.Text = "추가";
        }
        private void initialTextBoxes()//사용자 함수 정의
        {
            odpConn.ConnectionString = "User Id=hong1;Password=2163;Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=localhost)(PORT=1521))(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME=xe)))";
            odpConn.Open();
            int getID = _parent.getintID; //**
            string strqry = "SELECT * FROM phone WHERE id=" + getID;
            //쿼리문 작성:id가 getID(폼1에서 선택한 id)인 튜플 검색
            // "SELECT * FROM phone WHERE id= getID(변수)“를 수정
            OracleCommand OraCmd = new OracleCommand(strqry, odpConn); OracleDataReader odr = OraCmd.ExecuteReader();
            while (odr.Read())
            {
                txtid.Text = Convert.ToString(odr.GetValue(0));
                txtName.Text = Convert.ToString(odr.GetValue(1));
                txtPhone.Text = Convert.ToString(odr.GetValue(2));
                txtMail.Text = Convert.ToString(odr.GetValue(3));

            }
            odr.Close();
            odpConn.Close();
        }

        private int INSERTRow()
        {
            odpConn.ConnectionString = "User Id=hong1;Password=2163;Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=localhost)(PORT=1521))(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME=xe)))";
            odpConn.Open();

            int inid = Convert.ToInt32(txtid.Text.Trim());
            string inName = txtName.Text.Trim();
            string inPhone = txtPhone.Text.Trim();
            string inMail = txtMail.Text.Trim();

            string strqry = "INSERT INTO phone (id, pname, phone, email) VALUES (:id, :name, :phone, :mail)";
            OracleCommand OraCmd = new OracleCommand(strqry, odpConn);

            // 파라미터 추가
            OraCmd.Parameters.Add(new OracleParameter("id", inid));
            OraCmd.Parameters.Add(new OracleParameter("name", inName));
            OraCmd.Parameters.Add(new OracleParameter("phone", inPhone));
            OraCmd.Parameters.Add(new OracleParameter("mail", inMail));

            int result = OraCmd.ExecuteNonQuery();
            odpConn.Close();
            return result;
        }

        private int DELETERow()
        {
            odpConn.ConnectionString = "User Id=hong1;Password=2163;Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=localhost)(PORT=1521))(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME=xe)))";
            odpConn.Open();

            int getID = _parent.getintID;
            string strqry = "DELETE FROM phone WHERE id = :id";
            OracleCommand OraCmd = new OracleCommand(strqry, odpConn);

            // 파라미터 추가
            OraCmd.Parameters.Add(new OracleParameter("id", getID));

            int result = OraCmd.ExecuteNonQuery();
            odpConn.Close();
            return result;
        }

        private int UPDATERow()
        {
            odpConn.ConnectionString = "User Id=hong1;Password=2163;Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=localhost)(PORT=1521))(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME=xe)))";
            odpConn.Open();

            int inid = Convert.ToInt32(txtid.Text.Trim());
            string inPhone = txtPhone.Text.Trim();
            string inMail = txtMail.Text.Trim();

            string strqry = "UPDATE phone SET phone = :phone, email = :mail WHERE id = :id";
            OracleCommand OraCmd = new OracleCommand(strqry, odpConn);

            // 파라미터 추가
            OraCmd.Parameters.Add(new OracleParameter("phone", inPhone));
            OraCmd.Parameters.Add(new OracleParameter("mail", inMail));
            OraCmd.Parameters.Add(new OracleParameter("id", inid));

            int result = OraCmd.ExecuteNonQuery();
            odpConn.Close();
            return result;
        }

        private void btnOK_Click_1(object sender, EventArgs e)
        {
            if (btnOK.Text == "추가")
            {
                if (INSERTRow() > 0)
                {
                    MessageBox.Show("정상적으로 데이터 행이 추가됨.");
                }
                else MessageBox.Show("데이터 행이 추가되지 않음!");
                this.Close();
            }
            else if (btnOK.Text == "삭제")
            {
                if (DELETERow() > 0)
                {
                    MessageBox.Show("정상적으로 데이터 행이 삭제됨!");
                }
                else MessageBox.Show("데이터 행이 삭제되지 않음!");
                this.Close();
            }
            else
            {
                if (UPDATERow() > 0)
                {
                    MessageBox.Show("정상적으로 데이터가 업데이트됨!");
                }
                else MessageBox.Show("데이터 행이 업데이트되지 않음!");
                this.Close();
            }
        }
    }
}

