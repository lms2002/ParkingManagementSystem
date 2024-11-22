namespace ParkingManagement
{
    partial class StoreDiscountForm
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
            this.cmbStore = new System.Windows.Forms.ComboBox();
            this.btnSubmit = new System.Windows.Forms.Button();
            this.txtVehicleNumber = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.dgvDiscountInfo = new System.Windows.Forms.DataGridView();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDiscountInfo)).BeginInit();
            this.SuspendLayout();
            // 
            // cmbStore
            // 
            this.cmbStore.AllowDrop = true;
            this.cmbStore.Font = new System.Drawing.Font("굴림체", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.cmbStore.FormattingEnabled = true;
            this.cmbStore.Items.AddRange(new object[] {
            "nike",
            "빵빵이의 빵집",
            "모수"});
            this.cmbStore.Location = new System.Drawing.Point(21, 25);
            this.cmbStore.Name = "cmbStore";
            this.cmbStore.Size = new System.Drawing.Size(125, 23);
            this.cmbStore.TabIndex = 3;
            // 
            // btnSubmit
            // 
            this.btnSubmit.Font = new System.Drawing.Font("굴림", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnSubmit.Location = new System.Drawing.Point(567, 108);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(125, 109);
            this.btnSubmit.TabIndex = 5;
            this.btnSubmit.Text = "확인";
            this.btnSubmit.UseVisualStyleBackColor = true;
            // 
            // txtVehicleNumber
            // 
            this.txtVehicleNumber.Font = new System.Drawing.Font("굴림", 66F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.txtVehicleNumber.Location = new System.Drawing.Point(59, 108);
            this.txtVehicleNumber.Name = "txtVehicleNumber";
            this.txtVehicleNumber.Size = new System.Drawing.Size(489, 109);
            this.txtVehicleNumber.TabIndex = 4;
            this.txtVehicleNumber.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("굴림", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label2.Location = new System.Drawing.Point(57, 42);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(491, 32);
            this.label2.TabIndex = 7;
            this.label2.Text = "주차 할인권 발급 차량번호 입력";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cmbStore);
            this.groupBox1.Font = new System.Drawing.Font("굴림", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.groupBox1.Location = new System.Drawing.Point(583, 22);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(174, 67);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "매장 선택";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("굴림", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label1.Location = new System.Drawing.Point(59, 234);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(116, 19);
            this.label1.TabIndex = 9;
            this.label1.Text = "할인율 안내";
            // 
            // dgvDiscountInfo
            // 
            this.dgvDiscountInfo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDiscountInfo.Location = new System.Drawing.Point(59, 270);
            this.dgvDiscountInfo.Name = "dgvDiscountInfo";
            this.dgvDiscountInfo.RowTemplate.Height = 23;
            this.dgvDiscountInfo.Size = new System.Drawing.Size(633, 135);
            this.dgvDiscountInfo.TabIndex = 10;
            // 
            // StoreDiscountForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.dgvDiscountInfo);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnSubmit);
            this.Controls.Add(this.txtVehicleNumber);
            this.Name = "StoreDiscountForm";
            this.Text = "StoreDiscountForm";
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDiscountInfo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbStore;
        private System.Windows.Forms.Button btnSubmit;
        private System.Windows.Forms.TextBox txtVehicleNumber;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dgvDiscountInfo;
    }
}