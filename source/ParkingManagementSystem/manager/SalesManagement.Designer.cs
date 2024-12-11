namespace manager
{
    partial class SalesManagement
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.dgvCalendar = new System.Windows.Forms.DataGridView();
            this.txtMonthlyTotal = new System.Windows.Forms.TextBox();
            this.lblMonthlyTotal = new System.Windows.Forms.Label();
            this.lblMonthYear = new System.Windows.Forms.Label();
            this.btnPreviousMonth = new System.Windows.Forms.Button();
            this.btnNextMonth = new System.Windows.Forms.Button();
            this.txtDailyVehicleCount = new System.Windows.Forms.TextBox();
            this.lblSelectedDate = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCalendar)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvCalendar
            // 
            this.dgvCalendar.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvCalendar.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgvCalendar.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCalendar.Location = new System.Drawing.Point(12, 68);
            this.dgvCalendar.Name = "dgvCalendar";
            this.dgvCalendar.RowTemplate.Height = 23;
            this.dgvCalendar.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.dgvCalendar.Size = new System.Drawing.Size(953, 143);
            this.dgvCalendar.TabIndex = 0;
            this.dgvCalendar.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvCalendar_CellClick);
            // 
            // txtMonthlyTotal
            // 
            this.txtMonthlyTotal.Location = new System.Drawing.Point(592, 256);
            this.txtMonthlyTotal.Name = "txtMonthlyTotal";
            this.txtMonthlyTotal.ReadOnly = true;
            this.txtMonthlyTotal.Size = new System.Drawing.Size(100, 21);
            this.txtMonthlyTotal.TabIndex = 1;
            // 
            // lblMonthlyTotal
            // 
            this.lblMonthlyTotal.AutoSize = true;
            this.lblMonthlyTotal.Location = new System.Drawing.Point(513, 259);
            this.lblMonthlyTotal.Name = "lblMonthlyTotal";
            this.lblMonthlyTotal.Size = new System.Drawing.Size(73, 12);
            this.lblMonthlyTotal.TabIndex = 2;
            this.lblMonthlyTotal.Text = "당월 총 매출";
            // 
            // lblMonthYear
            // 
            this.lblMonthYear.AutoSize = true;
            this.lblMonthYear.Font = new System.Drawing.Font("Yu Gothic UI", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMonthYear.Location = new System.Drawing.Point(397, 11);
            this.lblMonthYear.Name = "lblMonthYear";
            this.lblMonthYear.Size = new System.Drawing.Size(71, 37);
            this.lblMonthYear.TabIndex = 3;
            this.lblMonthYear.Text = "달력";
            // 
            // btnPreviousMonth
            // 
            this.btnPreviousMonth.Location = new System.Drawing.Point(259, 25);
            this.btnPreviousMonth.Name = "btnPreviousMonth";
            this.btnPreviousMonth.Size = new System.Drawing.Size(75, 23);
            this.btnPreviousMonth.TabIndex = 4;
            this.btnPreviousMonth.Text = "<<";
            this.btnPreviousMonth.UseVisualStyleBackColor = true;
            this.btnPreviousMonth.Click += new System.EventHandler(this.btnPreviousMonth_Click);
            // 
            // btnNextMonth
            // 
            this.btnNextMonth.Location = new System.Drawing.Point(630, 25);
            this.btnNextMonth.Name = "btnNextMonth";
            this.btnNextMonth.Size = new System.Drawing.Size(75, 23);
            this.btnNextMonth.TabIndex = 5;
            this.btnNextMonth.Text = ">>";
            this.btnNextMonth.UseVisualStyleBackColor = true;
            this.btnNextMonth.Click += new System.EventHandler(this.btnNextMonth_Click);
            // 
            // txtDailyVehicleCount
            // 
            this.txtDailyVehicleCount.Location = new System.Drawing.Point(330, 256);
            this.txtDailyVehicleCount.Name = "txtDailyVehicleCount";
            this.txtDailyVehicleCount.ReadOnly = true;
            this.txtDailyVehicleCount.Size = new System.Drawing.Size(100, 21);
            this.txtDailyVehicleCount.TabIndex = 6;
            // 
            // lblSelectedDate
            // 
            this.lblSelectedDate.AutoSize = true;
            this.lblSelectedDate.Location = new System.Drawing.Point(227, 259);
            this.lblSelectedDate.Name = "lblSelectedDate";
            this.lblSelectedDate.Size = new System.Drawing.Size(97, 12);
            this.lblSelectedDate.TabIndex = 7;
            this.lblSelectedDate.Text = "선택된 날짜 없음";
            // 
            // SalesManagement
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(977, 315);
            this.Controls.Add(this.lblSelectedDate);
            this.Controls.Add(this.txtDailyVehicleCount);
            this.Controls.Add(this.btnNextMonth);
            this.Controls.Add(this.btnPreviousMonth);
            this.Controls.Add(this.lblMonthYear);
            this.Controls.Add(this.lblMonthlyTotal);
            this.Controls.Add(this.txtMonthlyTotal);
            this.Controls.Add(this.dgvCalendar);
            this.Name = "SalesManagement";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "달력";
            this.Load += new System.EventHandler(this.SalesManagement_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvCalendar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvCalendar;
        private System.Windows.Forms.TextBox txtMonthlyTotal;
        private System.Windows.Forms.Label lblMonthlyTotal;
        private System.Windows.Forms.Label lblMonthYear;
        private System.Windows.Forms.Button btnPreviousMonth;
        private System.Windows.Forms.Button btnNextMonth;
        private System.Windows.Forms.TextBox txtDailyVehicleCount;
        private System.Windows.Forms.Label lblSelectedDate;
    }
}
