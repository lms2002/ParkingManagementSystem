namespace Management
{
    partial class Management
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
            this.rightDBGrid = new System.Windows.Forms.DataGridView();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.차량ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.updateMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.선택차량수정ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.차량추가정보ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.searchButton = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.suffixTextBox = new System.Windows.Forms.TextBox();
            this.leftDBGrid = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.rightDBGrid)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.leftDBGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // rightDBGrid
            // 
            this.rightDBGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.rightDBGrid.ContextMenuStrip = this.contextMenuStrip1;
            this.rightDBGrid.Location = new System.Drawing.Point(405, 47);
            this.rightDBGrid.Margin = new System.Windows.Forms.Padding(2);
            this.rightDBGrid.Name = "rightDBGrid";
            this.rightDBGrid.RowHeadersWidth = 62;
            this.rightDBGrid.RowTemplate.Height = 30;
            this.rightDBGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.rightDBGrid.Size = new System.Drawing.Size(461, 400);
            this.rightDBGrid.TabIndex = 11;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.차량ToolStripMenuItem,
            this.차량추가정보ToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(181, 70);
            // 
            // 차량ToolStripMenuItem
            // 
            this.차량ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addMenuItem,
            this.updateMenuItem,
            this.선택차량수정ToolStripMenuItem});
            this.차량ToolStripMenuItem.Name = "차량ToolStripMenuItem";
            this.차량ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.차량ToolStripMenuItem.Text = "차량 업데이트";
            // 
            // addMenuItem
            // 
            this.addMenuItem.Name = "addMenuItem";
            this.addMenuItem.Size = new System.Drawing.Size(180, 22);
            this.addMenuItem.Text = "선택차량 추가";
            this.addMenuItem.Click += new System.EventHandler(this.선택형업데이트ToolStripMenuItem_Click);
            // 
            // updateMenuItem
            // 
            this.updateMenuItem.Name = "updateMenuItem";
            this.updateMenuItem.Size = new System.Drawing.Size(180, 22);
            this.updateMenuItem.Text = "선택차량 삭제";
            this.updateMenuItem.Click += new System.EventHandler(this.선택차량삭제ToolStripMenuItem_Click);
            // 
            // 선택차량수정ToolStripMenuItem
            // 
            this.선택차량수정ToolStripMenuItem.Name = "선택차량수정ToolStripMenuItem";
            this.선택차량수정ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.선택차량수정ToolStripMenuItem.Text = "선택차량 수정";
            this.선택차량수정ToolStripMenuItem.Click += new System.EventHandler(this.선택차량수정ToolStripMenuItem_Click);
            // 
            // 차량추가정보ToolStripMenuItem
            // 
            this.차량추가정보ToolStripMenuItem.Name = "차량추가정보ToolStripMenuItem";
            this.차량추가정보ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.차량추가정보ToolStripMenuItem.Text = "차량 추가 정보";
            // 
            // searchButton
            // 
            this.searchButton.Location = new System.Drawing.Point(86, 472);
            this.searchButton.Margin = new System.Windows.Forms.Padding(2);
            this.searchButton.Name = "searchButton";
            this.searchButton.Size = new System.Drawing.Size(167, 43);
            this.searchButton.TabIndex = 10;
            this.searchButton.Text = "차 번호 검색";
            this.searchButton.UseVisualStyleBackColor = true;
            this.searchButton.Click += new System.EventHandler(this.searchByVehicleNumber_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.suffixTextBox);
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
            // suffixTextBox
            // 
            this.suffixTextBox.Location = new System.Drawing.Point(113, 43);
            this.suffixTextBox.Margin = new System.Windows.Forms.Padding(2);
            this.suffixTextBox.Name = "suffixTextBox";
            this.suffixTextBox.Size = new System.Drawing.Size(103, 21);
            this.suffixTextBox.TabIndex = 4;
            // 
            // leftDBGrid
            // 
            this.leftDBGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.leftDBGrid.ContextMenuStrip = this.contextMenuStrip1;
            this.leftDBGrid.Location = new System.Drawing.Point(63, 169);
            this.leftDBGrid.Margin = new System.Windows.Forms.Padding(2);
            this.leftDBGrid.Name = "leftDBGrid";
            this.leftDBGrid.RowHeadersWidth = 62;
            this.leftDBGrid.RowTemplate.Height = 30;
            this.leftDBGrid.Size = new System.Drawing.Size(247, 259);
            this.leftDBGrid.TabIndex = 12;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 561);
            this.Controls.Add(this.leftDBGrid);
            this.Controls.Add(this.rightDBGrid);
            this.Controls.Add(this.searchButton);
            this.Controls.Add(this.groupBox1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.rightDBGrid)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.leftDBGrid)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView rightDBGrid;
        private System.Windows.Forms.Button searchButton;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox suffixTextBox;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 차량ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 차량추가정보ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addMenuItem;
        private System.Windows.Forms.ToolStripMenuItem updateMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 선택차량수정ToolStripMenuItem;
        private System.Windows.Forms.DataGridView leftDBGrid;
    }
}

