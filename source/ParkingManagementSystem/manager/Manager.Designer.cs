namespace manager
{
    partial class Manager
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem(new string[] {
            "NIKE",
            "50,000원",
            "20% 할인"}, -1);
            System.Windows.Forms.ListViewItem listViewItem2 = new System.Windows.Forms.ListViewItem(new string[] {
            "빵빵이의 빵집",
            "20,000원",
            "10% 할인"}, -1);
            System.Windows.Forms.ListViewItem listViewItem3 = new System.Windows.Forms.ListViewItem(new string[] {
            "모수",
            "150,000원",
            "30% 할인"}, -1);
            System.Windows.Forms.ListViewItem listViewItem4 = new System.Windows.Forms.ListViewItem(new string[] {
            "일반 차량(Standard)",
            "10분당 1,000원"}, -1);
            System.Windows.Forms.ListViewItem listViewItem5 = new System.Windows.Forms.ListViewItem(new string[] {
            "경차(Compact)",
            "10분당 500원"}, -1);
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.관리자메뉴ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.차량관리ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.주차석관리ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.매출관리ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lvStoreDiscount = new System.Windows.Forms.ListView();
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lvParkingFee = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lblCurrentTime = new System.Windows.Forms.Label();
            this.lblDisabledAvailable = new System.Windows.Forms.Label();
            this.lblStandardAvailable = new System.Windows.Forms.Label();
            this.lblTotalSpots = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.menuStrip1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.관리자메뉴ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(800, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 관리자메뉴ToolStripMenuItem
            // 
            this.관리자메뉴ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.차량관리ToolStripMenuItem,
            this.주차석관리ToolStripMenuItem,
            this.매출관리ToolStripMenuItem});
            this.관리자메뉴ToolStripMenuItem.Name = "관리자메뉴ToolStripMenuItem";
            this.관리자메뉴ToolStripMenuItem.Size = new System.Drawing.Size(83, 20);
            this.관리자메뉴ToolStripMenuItem.Text = "관리자 메뉴";
            // 
            // 차량관리ToolStripMenuItem
            // 
            this.차량관리ToolStripMenuItem.Name = "차량관리ToolStripMenuItem";
            this.차량관리ToolStripMenuItem.Size = new System.Drawing.Size(138, 22);
            this.차량관리ToolStripMenuItem.Text = "차량 관리";
            this.차량관리ToolStripMenuItem.Click += new System.EventHandler(this.차량관리ToolStripMenuItem_Click);
            // 
            // 주차석관리ToolStripMenuItem
            // 
            this.주차석관리ToolStripMenuItem.Name = "주차석관리ToolStripMenuItem";
            this.주차석관리ToolStripMenuItem.Size = new System.Drawing.Size(138, 22);
            this.주차석관리ToolStripMenuItem.Text = "주차석 관리";
            this.주차석관리ToolStripMenuItem.Click += new System.EventHandler(this.주차석관리ToolStripMenuItem_Click);
            // 
            // 매출관리ToolStripMenuItem
            // 
            this.매출관리ToolStripMenuItem.Name = "매출관리ToolStripMenuItem";
            this.매출관리ToolStripMenuItem.Size = new System.Drawing.Size(138, 22);
            this.매출관리ToolStripMenuItem.Text = "매출 관리";
            this.매출관리ToolStripMenuItem.Click += new System.EventHandler(this.매출관리ToolStripMenuItem_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lvStoreDiscount);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.999999F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.groupBox2.Location = new System.Drawing.Point(414, 216);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(338, 172);
            this.groupBox2.TabIndex = 15;
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
            listViewItem1,
            listViewItem2,
            listViewItem3});
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
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.999999F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.groupBox1.Location = new System.Drawing.Point(52, 216);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(323, 172);
            this.groupBox1.TabIndex = 14;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "주차요금";
            // 
            // lvParkingFee
            // 
            this.lvParkingFee.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
            this.lvParkingFee.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.999999F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lvParkingFee.GridLines = true;
            this.lvParkingFee.HideSelection = false;
            this.lvParkingFee.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem4,
            listViewItem5});
            this.lvParkingFee.Location = new System.Drawing.Point(20, 20);
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
            // lblCurrentTime
            // 
            this.lblCurrentTime.AutoSize = true;
            this.lblCurrentTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblCurrentTime.Location = new System.Drawing.Point(594, 406);
            this.lblCurrentTime.Name = "lblCurrentTime";
            this.lblCurrentTime.Size = new System.Drawing.Size(64, 18);
            this.lblCurrentTime.TabIndex = 13;
            this.lblCurrentTime.Text = "현재 시간";
            // 
            // lblDisabledAvailable
            // 
            this.lblDisabledAvailable.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblDisabledAvailable.Location = new System.Drawing.Point(279, 155);
            this.lblDisabledAvailable.Name = "lblDisabledAvailable";
            this.lblDisabledAvailable.Size = new System.Drawing.Size(318, 26);
            this.lblDisabledAvailable.TabIndex = 12;
            this.lblDisabledAvailable.Text = "장애석 빈 자리";
            this.lblDisabledAvailable.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblStandardAvailable
            // 
            this.lblStandardAvailable.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblStandardAvailable.Location = new System.Drawing.Point(279, 117);
            this.lblStandardAvailable.Name = "lblStandardAvailable";
            this.lblStandardAvailable.Size = new System.Drawing.Size(318, 26);
            this.lblStandardAvailable.TabIndex = 11;
            this.lblStandardAvailable.Text = "일반석 빈 자리";
            this.lblStandardAvailable.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblTotalSpots
            // 
            this.lblTotalSpots.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblTotalSpots.Location = new System.Drawing.Point(279, 79);
            this.lblTotalSpots.Name = "lblTotalSpots";
            this.lblTotalSpots.Size = new System.Drawing.Size(318, 26);
            this.lblTotalSpots.TabIndex = 10;
            this.lblTotalSpots.Text = "총 잔여 자리";
            this.lblTotalSpots.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(28, 46);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(146, 57);
            this.label1.TabIndex = 16;
            this.label1.Text = "관리자";
            // 
            // Manager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.lblCurrentTime);
            this.Controls.Add(this.lblDisabledAvailable);
            this.Controls.Add(this.lblStandardAvailable);
            this.Controls.Add(this.lblTotalSpots);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Manager";
            this.Text = "관리자";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 관리자메뉴ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 차량관리ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 주차석관리ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 매출관리ToolStripMenuItem;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ListView lvStoreDiscount;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ListView lvParkingFee;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.Label lblCurrentTime;
        private System.Windows.Forms.Label lblDisabledAvailable;
        private System.Windows.Forms.Label lblStandardAvailable;
        private System.Windows.Forms.Label lblTotalSpots;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label label1;
    }
}