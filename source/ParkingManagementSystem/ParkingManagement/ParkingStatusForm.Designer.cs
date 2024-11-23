namespace ParkingManagement
{
    partial class ParkingStatusForm
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
            System.Windows.Forms.ListViewItem listViewItem11 = new System.Windows.Forms.ListViewItem(new string[] {
            "NIKE",
            "50,000원",
            "20% 할인"}, -1);
            System.Windows.Forms.ListViewItem listViewItem12 = new System.Windows.Forms.ListViewItem(new string[] {
            "빵빵이의 빵집",
            "20,000원",
            "10% 할인"}, -1);
            System.Windows.Forms.ListViewItem listViewItem13 = new System.Windows.Forms.ListViewItem(new string[] {
            "모수",
            "150,000원",
            "30% 할인"}, -1);
            System.Windows.Forms.ListViewItem listViewItem14 = new System.Windows.Forms.ListViewItem(new string[] {
            "일반 차량(Standard)",
            "10분당 1,000원"}, -1);
            System.Windows.Forms.ListViewItem listViewItem15 = new System.Windows.Forms.ListViewItem(new string[] {
            "경차(Compact)",
            "10분당 500원"}, -1);
            this.lblTotalAvailableSpots = new System.Windows.Forms.Label();
            this.lblRegularAvailableSpots = new System.Windows.Forms.Label();
            this.lblDisabledAvailableSpots = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lblCurrentTime = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lvStoreDiscount = new System.Windows.Forms.ListView();
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lvParkingFee = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblTotalAvailableSpots
            // 
            this.lblTotalAvailableSpots.Font = new System.Drawing.Font("굴림", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblTotalAvailableSpots.Location = new System.Drawing.Point(146, 92);
            this.lblTotalAvailableSpots.Name = "lblTotalAvailableSpots";
            this.lblTotalAvailableSpots.Size = new System.Drawing.Size(511, 26);
            this.lblTotalAvailableSpots.TabIndex = 0;
            this.lblTotalAvailableSpots.Text = "총 잔여 자리";
            this.lblTotalAvailableSpots.Click += new System.EventHandler(this.lblTotalAvailableSpots_Click);
            // 
            // lblRegularAvailableSpots
            // 
            this.lblRegularAvailableSpots.Font = new System.Drawing.Font("굴림", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblRegularAvailableSpots.Location = new System.Drawing.Point(146, 130);
            this.lblRegularAvailableSpots.Name = "lblRegularAvailableSpots";
            this.lblRegularAvailableSpots.Size = new System.Drawing.Size(511, 26);
            this.lblRegularAvailableSpots.TabIndex = 1;
            this.lblRegularAvailableSpots.Text = "일반석 빈 자리";
            this.lblRegularAvailableSpots.Click += new System.EventHandler(this.lblRegularAvailableSpots_Click);
            // 
            // lblDisabledAvailableSpots
            // 
            this.lblDisabledAvailableSpots.Font = new System.Drawing.Font("굴림", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblDisabledAvailableSpots.Location = new System.Drawing.Point(146, 168);
            this.lblDisabledAvailableSpots.Name = "lblDisabledAvailableSpots";
            this.lblDisabledAvailableSpots.Size = new System.Drawing.Size(511, 26);
            this.lblDisabledAvailableSpots.TabIndex = 2;
            this.lblDisabledAvailableSpots.Text = "장애석 빈 자리";
            this.lblDisabledAvailableSpots.Click += new System.EventHandler(this.lblDisabledAvailableSpots_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("굴림", 22.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label1.Location = new System.Drawing.Point(123, 34);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(564, 30);
            this.label1.TabIndex = 3;
            this.label1.Text = "화면 터치 시 다음 화면으로 이동합니다";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // lblCurrentTime
            // 
            this.lblCurrentTime.AutoSize = true;
            this.lblCurrentTime.Font = new System.Drawing.Font("굴림", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblCurrentTime.Location = new System.Drawing.Point(619, 411);
            this.lblCurrentTime.Name = "lblCurrentTime";
            this.lblCurrentTime.Size = new System.Drawing.Size(68, 14);
            this.lblCurrentTime.TabIndex = 4;
            this.lblCurrentTime.Text = "현재 시간";
            this.lblCurrentTime.Click += new System.EventHandler(this.lblCurrentTime_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lvStoreDiscount);
            this.groupBox2.Location = new System.Drawing.Point(420, 219);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(338, 172);
            this.groupBox2.TabIndex = 9;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "상가 주차권 할인";
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
            listViewItem11,
            listViewItem12,
            listViewItem13});
            this.lvStoreDiscount.Location = new System.Drawing.Point(18, 20);
            this.lvStoreDiscount.Name = "lvStoreDiscount";
            this.lvStoreDiscount.Size = new System.Drawing.Size(289, 140);
            this.lvStoreDiscount.TabIndex = 4;
            this.lvStoreDiscount.UseCompatibleStateImageBehavior = false;
            this.lvStoreDiscount.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "상가이름";
            this.columnHeader5.Width = 99;
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "할인조건";
            this.columnHeader6.Width = 100;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "할인율";
            this.columnHeader3.Width = 85;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lvParkingFee);
            this.groupBox1.Location = new System.Drawing.Point(58, 219);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(323, 172);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "주차요금";
            // 
            // lvParkingFee
            // 
            this.lvParkingFee.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
            this.lvParkingFee.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lvParkingFee.GridLines = true;
            this.lvParkingFee.HideSelection = false;
            this.lvParkingFee.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem14,
            listViewItem15});
            this.lvParkingFee.Location = new System.Drawing.Point(27, 20);
            this.lvParkingFee.Name = "lvParkingFee";
            this.lvParkingFee.Size = new System.Drawing.Size(275, 140);
            this.lvParkingFee.TabIndex = 4;
            this.lvParkingFee.UseCompatibleStateImageBehavior = false;
            this.lvParkingFee.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "차량 타입";
            this.columnHeader1.Width = 133;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "기준 요금";
            this.columnHeader2.Width = 113;
            // 
            // ParkingStatusForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.lblCurrentTime);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblDisabledAvailableSpots);
            this.Controls.Add(this.lblRegularAvailableSpots);
            this.Controls.Add(this.lblTotalAvailableSpots);
            this.Name = "ParkingStatusForm";
            this.Text = "Parking Entry";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.ParkingStatusForm_FormClosed);
            this.Click += new System.EventHandler(this.ParkingStatusForm_Click);
            this.groupBox2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTotalAvailableSpots;
        private System.Windows.Forms.Label lblRegularAvailableSpots;
        private System.Windows.Forms.Label lblDisabledAvailableSpots;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblCurrentTime;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ListView lvStoreDiscount;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ListView lvParkingFee;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
    }
}