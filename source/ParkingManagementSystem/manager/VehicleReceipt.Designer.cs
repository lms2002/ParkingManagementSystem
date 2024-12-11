namespace manager
{
    partial class VehicleReceipt
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
            this.dgvReceipt = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtTotalParkingCount = new System.Windows.Forms.TextBox();
            this.txtTotalFee = new System.Windows.Forms.TextBox();
            this.txtTotalDuration = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtTotalDiscountAmount = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvReceipt)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvReceipt
            // 
            this.dgvReceipt.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvReceipt.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgvReceipt.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvReceipt.Location = new System.Drawing.Point(64, 66);
            this.dgvReceipt.Name = "dgvReceipt";
            this.dgvReceipt.RowTemplate.Height = 23;
            this.dgvReceipt.Size = new System.Drawing.Size(660, 300);
            this.dgvReceipt.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(46, 401);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(73, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "총 주차 횟수";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(415, 401);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(45, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "총 요금";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(572, 401);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(69, 12);
            this.label3.TabIndex = 3;
            this.label3.Text = "총 이용시간";
            // 
            // txtTotalParkingCount
            // 
            this.txtTotalParkingCount.Location = new System.Drawing.Point(125, 398);
            this.txtTotalParkingCount.Name = "txtTotalParkingCount";
            this.txtTotalParkingCount.Size = new System.Drawing.Size(100, 21);
            this.txtTotalParkingCount.TabIndex = 4;
            // 
            // txtTotalFee
            // 
            this.txtTotalFee.Location = new System.Drawing.Point(466, 398);
            this.txtTotalFee.Name = "txtTotalFee";
            this.txtTotalFee.Size = new System.Drawing.Size(100, 21);
            this.txtTotalFee.TabIndex = 5;
            // 
            // txtTotalDuration
            // 
            this.txtTotalDuration.Location = new System.Drawing.Point(647, 398);
            this.txtTotalDuration.Name = "txtTotalDuration";
            this.txtTotalDuration.Size = new System.Drawing.Size(100, 21);
            this.txtTotalDuration.TabIndex = 6;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(234, 401);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(69, 12);
            this.label4.TabIndex = 7;
            this.label4.Text = "총 할인금액";
            // 
            // txtTotalDiscountAmount
            // 
            this.txtTotalDiscountAmount.Location = new System.Drawing.Point(309, 398);
            this.txtTotalDiscountAmount.Name = "txtTotalDiscountAmount";
            this.txtTotalDiscountAmount.Size = new System.Drawing.Size(100, 21);
            this.txtTotalDiscountAmount.TabIndex = 8;
            // 
            // VehicleReceipt
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.txtTotalDiscountAmount);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtTotalDuration);
            this.Controls.Add(this.txtTotalFee);
            this.Controls.Add(this.txtTotalParkingCount);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dgvReceipt);
            this.Name = "VehicleReceipt";
            this.Text = "VehicleReceipt";
            ((System.ComponentModel.ISupportInitialize)(this.dgvReceipt)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvReceipt;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtTotalParkingCount;
        private System.Windows.Forms.TextBox txtTotalFee;
        private System.Windows.Forms.TextBox txtTotalDuration;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtTotalDiscountAmount;
    }
}