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
            ((System.ComponentModel.ISupportInitialize)(this.dgvCalendar)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvCalendar
            // 
            this.dgvCalendar.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvCalendar.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgvCalendar.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCalendar.Location = new System.Drawing.Point(12, 12);
            this.dgvCalendar.Name = "dgvCalendar";
            this.dgvCalendar.RowTemplate.Height = 23;
            this.dgvCalendar.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.dgvCalendar.Size = new System.Drawing.Size(953, 143);
            this.dgvCalendar.TabIndex = 0;
            // 
            // txtMonthlyTotal
            // 
            this.txtMonthlyTotal.Location = new System.Drawing.Point(473, 227);
            this.txtMonthlyTotal.Name = "txtMonthlyTotal";
            this.txtMonthlyTotal.ReadOnly = true;
            this.txtMonthlyTotal.Size = new System.Drawing.Size(100, 21);
            this.txtMonthlyTotal.TabIndex = 1;
            // 
            // lblMonthlyTotal
            // 
            this.lblMonthlyTotal.AutoSize = true;
            this.lblMonthlyTotal.Location = new System.Drawing.Point(394, 230);
            this.lblMonthlyTotal.Name = "lblMonthlyTotal";
            this.lblMonthlyTotal.Size = new System.Drawing.Size(73, 12);
            this.lblMonthlyTotal.TabIndex = 2;
            this.lblMonthlyTotal.Text = "당월 총 매출";
            // 
            // SalesManagement
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(977, 315);
            this.Controls.Add(this.lblMonthlyTotal);
            this.Controls.Add(this.txtMonthlyTotal);
            this.Controls.Add(this.dgvCalendar);
            this.Name = "SalesManagement";
            this.Text = "SalesManagement";
            this.Load += new System.EventHandler(this.SalesManagement_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvCalendar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvCalendar;
        private System.Windows.Forms.TextBox txtMonthlyTotal;
        private System.Windows.Forms.Label lblMonthlyTotal;
    }
}
