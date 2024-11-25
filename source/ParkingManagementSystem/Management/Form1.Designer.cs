namespace Management
{
    partial class Form1
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.dataGridVehicles = new System.Windows.Forms.DataGridView();
            this.searchByVehicleNumber = new System.Windows.Forms.Button();
            this.searchAllVehicles = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.차량ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.선택형업데이트ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.선택차량삭제ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.선택차량수정ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.차량추가정보ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.listBoxResults = new System.Windows.Forms.ListBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridVehicles)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridVehicles
            // 
            this.dataGridVehicles.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridVehicles.ContextMenuStrip = this.contextMenuStrip1;
            this.dataGridVehicles.Location = new System.Drawing.Point(405, 47);
            this.dataGridVehicles.Margin = new System.Windows.Forms.Padding(2);
            this.dataGridVehicles.Name = "dataGridVehicles";
            this.dataGridVehicles.RowHeadersWidth = 62;
            this.dataGridVehicles.RowTemplate.Height = 30;
            this.dataGridVehicles.Size = new System.Drawing.Size(461, 400);
            this.dataGridVehicles.TabIndex = 11;
            // 
            // searchByVehicleNumber
            // 
            this.searchByVehicleNumber.Location = new System.Drawing.Point(86, 472);
            this.searchByVehicleNumber.Margin = new System.Windows.Forms.Padding(2);
            this.searchByVehicleNumber.Name = "searchByVehicleNumber";
            this.searchByVehicleNumber.Size = new System.Drawing.Size(167, 43);
            this.searchByVehicleNumber.TabIndex = 10;
            this.searchByVehicleNumber.Text = "차 번호 검색";
            this.searchByVehicleNumber.UseVisualStyleBackColor = true;
            this.searchByVehicleNumber.Click += new System.EventHandler(this.searchB2_Click);
            // 
            // searchAllVehicles
            // 
            this.searchAllVehicles.Location = new System.Drawing.Point(561, 485);
            this.searchAllVehicles.Margin = new System.Windows.Forms.Padding(2);
            this.searchAllVehicles.Name = "searchAllVehicles";
            this.searchAllVehicles.Size = new System.Drawing.Size(167, 43);
            this.searchAllVehicles.TabIndex = 9;
            this.searchAllVehicles.Text = "모든 차량 검색";
            this.searchAllVehicles.UseVisualStyleBackColor = true;
            this.searchAllVehicles.Click += new System.EventHandler(this.searchB1_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtSearch);
            this.groupBox1.Location = new System.Drawing.Point(63, 47);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox1.Size = new System.Drawing.Size(247, 94);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "4자리로 찾기";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(21, 46);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(79, 12);
            this.label2.TabIndex = 6;
            this.label2.Text = "뒤 4자리 입력";
            // 
            // txtSearch
            // 
            this.txtSearch.Location = new System.Drawing.Point(113, 43);
            this.txtSearch.Margin = new System.Windows.Forms.Padding(2);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(103, 21);
            this.txtSearch.TabIndex = 4;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.차량ToolStripMenuItem,
            this.차량추가정보ToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(155, 48);
            // 
            // 차량ToolStripMenuItem
            // 
            this.차량ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.선택형업데이트ToolStripMenuItem,
            this.선택차량삭제ToolStripMenuItem,
            this.선택차량수정ToolStripMenuItem});
            this.차량ToolStripMenuItem.Name = "차량ToolStripMenuItem";
            this.차량ToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.차량ToolStripMenuItem.Text = "차량 업데이트";
            // 
            // 선택형업데이트ToolStripMenuItem
            // 
            this.선택형업데이트ToolStripMenuItem.Name = "선택형업데이트ToolStripMenuItem";
            this.선택형업데이트ToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
            this.선택형업데이트ToolStripMenuItem.Text = "선택차량 추가";
            this.선택형업데이트ToolStripMenuItem.Click += new System.EventHandler(this.선택형업데이트ToolStripMenuItem_Click);
            // 
            // 선택차량삭제ToolStripMenuItem
            // 
            this.선택차량삭제ToolStripMenuItem.Name = "선택차량삭제ToolStripMenuItem";
            this.선택차량삭제ToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
            this.선택차량삭제ToolStripMenuItem.Text = "선택차량 삭제";
            this.선택차량삭제ToolStripMenuItem.Click += new System.EventHandler(this.선택차량삭제ToolStripMenuItem_Click);
            // 
            // 선택차량수정ToolStripMenuItem
            // 
            this.선택차량수정ToolStripMenuItem.Name = "선택차량수정ToolStripMenuItem";
            this.선택차량수정ToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
            this.선택차량수정ToolStripMenuItem.Text = "선택차량 수정";
            this.선택차량수정ToolStripMenuItem.Click += new System.EventHandler(this.선택차량수정ToolStripMenuItem_Click);
            // 
            // 차량추가정보ToolStripMenuItem
            // 
            this.차량추가정보ToolStripMenuItem.Name = "차량추가정보ToolStripMenuItem";
            this.차량추가정보ToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.차량추가정보ToolStripMenuItem.Text = "차량 추가 정보";
            // 
            // listBoxResults
            // 
            this.listBoxResults.FormattingEnabled = true;
            this.listBoxResults.ItemHeight = 12;
            this.listBoxResults.Location = new System.Drawing.Point(63, 169);
            this.listBoxResults.Name = "listBoxResults";
            this.listBoxResults.Size = new System.Drawing.Size(247, 256);
            this.listBoxResults.TabIndex = 12;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 561);
            this.Controls.Add(this.listBoxResults);
            this.Controls.Add(this.dataGridVehicles);
            this.Controls.Add(this.searchByVehicleNumber);
            this.Controls.Add(this.searchAllVehicles);
            this.Controls.Add(this.groupBox1);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridVehicles)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridVehicles;
        private System.Windows.Forms.Button searchByVehicleNumber;
        private System.Windows.Forms.Button searchAllVehicles;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 차량ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 차량추가정보ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 선택형업데이트ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 선택차량삭제ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 선택차량수정ToolStripMenuItem;
        private System.Windows.Forms.ListBox listBoxResults;
    }
}

