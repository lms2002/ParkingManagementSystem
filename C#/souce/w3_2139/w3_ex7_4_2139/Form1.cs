﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace w3_ex7_4_2139
{
    public partial class Form1 : Form
    {
        private void SetButtonText()
        {
            button1.Text = "Form.FormBorderStyle = " + FormBorderStyle.ToString() + "\n" +
            "Form.Size = " + Size.ToString() + "\n" + "Form.ClientSize = " + ClientSize.ToString();
        }
        private void Form1_Load(object sender, System.EventArgs e)
        {
            SetButtonText();
        }
        private void button1_Click(object sender, System.EventArgs e)
        {
            if (FormBorderStyle == FormBorderStyle.SizableToolWindow)
                FormBorderStyle = FormBorderStyle.None;
            else
                FormBorderStyle++;
            SetButtonText();
        }
        public Form1()
        {
            InitializeComponent();
        }
    }
}
