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
            System.Windows.Forms.ListViewItem listViewItem7 = new System.Windows.Forms.ListViewItem(new string[] {
            "NIKE",
            "50,000원",
            "20% 할인"}, -1);
            System.Windows.Forms.ListViewItem listViewItem8 = new System.Windows.Forms.ListViewItem(new string[] {
            "빵빵이의 빵집",
            "20,000원",
            "10% 할인"}, -1);
            System.Windows.Forms.ListViewItem listViewItem9 = new System.Windows.Forms.ListViewItem(new string[] {
            "모수",
            "150,000원",
            "30% 할인"}, -1);
            this.cmbStore = new System.Windows.Forms.ComboBox();
            this.btnSubmit = new System.Windows.Forms.Button();
            this.txtVehicleNumber = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lvStoreDiscount = new System.Windows.Forms.ListView();
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmbStore
            // 
            this.cmbStore.AllowDrop = true;
            this.cmbStore.Font = new System.Drawing.Font("굴림체", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.cmbStore.FormattingEnabled = true;
            this.cmbStore.Items.AddRange(new object[] {
            "NIKE",
            "빵빵이의 빵집",
            "모수"});
            this.cmbStore.Location = new System.Drawing.Point(21, 25);
            this.cmbStore.Name = "cmbStore";
            this.cmbStore.Size = new System.Drawing.Size(125, 23);
            this.cmbStore.TabIndex = 3;
            // 
            // btnSubmit
            // 
            this.btnSubmit.Font = new System.Drawing.Font("나눔고딕", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnSubmit.Location = new System.Drawing.Point(574, 131);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(99, 81);
            this.btnSubmit.TabIndex = 5;
            this.btnSubmit.Text = "확인";
            this.btnSubmit.UseVisualStyleBackColor = true;
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // txtVehicleNumber
            // 
            this.txtVehicleNumber.Font = new System.Drawing.Font("굴림", 48F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.txtVehicleNumber.Location = new System.Drawing.Point(68, 131);
            this.txtVehicleNumber.Name = "txtVehicleNumber";
            this.txtVehicleNumber.Size = new System.Drawing.Size(489, 81);
            this.txtVehicleNumber.TabIndex = 4;
            this.txtVehicleNumber.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("나눔고딕 ExtraBold", 26.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label2.Location = new System.Drawing.Point(237, 42);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(268, 40);
            this.label2.TabIndex = 7;
            this.label2.Text = "주차 할인권 발급";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cmbStore);
            this.groupBox1.Font = new System.Drawing.Font("나눔고딕", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.groupBox1.Location = new System.Drawing.Point(548, 42);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(174, 67);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "매장 선택";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lvStoreDiscount);
            this.groupBox2.Font = new System.Drawing.Font("나눔고딕 ExtraBold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.groupBox2.Location = new System.Drawing.Point(148, 251);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(457, 145);
            this.groupBox2.TabIndex = 11;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "상가 주차권 할인율 안내";
            // 
            // lvStoreDiscount
            // 
            this.lvStoreDiscount.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader5,
            this.columnHeader6,
            this.columnHeader3});
            this.lvStoreDiscount.GridLines = true;
            this.lvStoreDiscount.HideSelection = false;
            this.lvStoreDiscount.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem7,
            listViewItem8,
            listViewItem9});
            this.lvStoreDiscount.Location = new System.Drawing.Point(18, 20);
            this.lvStoreDiscount.Name = "lvStoreDiscount";
            this.lvStoreDiscount.Size = new System.Drawing.Size(417, 106);
            this.lvStoreDiscount.TabIndex = 4;
            this.lvStoreDiscount.UseCompatibleStateImageBehavior = false;
            this.lvStoreDiscount.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "상가이름";
            this.columnHeader5.Width = 144;
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "할인조건";
            this.columnHeader6.Width = 151;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "할인율";
            this.columnHeader3.Width = 116;
            // 
            // StoreDiscountForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(795, 442);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnSubmit);
            this.Controls.Add(this.txtVehicleNumber);
            this.Name = "StoreDiscountForm";
            this.Text = "StoreDiscountForm";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.StoreDiscountForm_FormClosed);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbStore;
        private System.Windows.Forms.Button btnSubmit;
        private System.Windows.Forms.TextBox txtVehicleNumber;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ListView lvStoreDiscount;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.ColumnHeader columnHeader3;
    }
}