namespace manager
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
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.차량추가ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.차량삭제ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.차량수정ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.ContextMenuStrip = this.contextMenuStrip1;
            this.dataGridView1.Location = new System.Drawing.Point(12, 12);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(533, 426);
            this.dataGridView1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(628, 136);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(91, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "뒤 4자리로 검색";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(594, 183);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(158, 21);
            this.textBox1.TabIndex = 2;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(594, 251);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(158, 59);
            this.button1.TabIndex = 3;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.차량추가ToolStripMenuItem,
            this.차량삭제ToolStripMenuItem,
            this.차량수정ToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(127, 70);
            // 
            // 차량추가ToolStripMenuItem
            // 
            this.차량추가ToolStripMenuItem.Name = "차량추가ToolStripMenuItem";
            this.차량추가ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.차량추가ToolStripMenuItem.Text = "차량 추가";
            this.차량추가ToolStripMenuItem.Click += new System.EventHandler(this.차량추가ToolStripMenuItem_Click);
            // 
            // 차량삭제ToolStripMenuItem
            // 
            this.차량삭제ToolStripMenuItem.Name = "차량삭제ToolStripMenuItem";
            this.차량삭제ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.차량삭제ToolStripMenuItem.Text = "차량 삭제";
            this.차량삭제ToolStripMenuItem.Click += new System.EventHandler(this.차량삭제ToolStripMenuItem_Click);
            // 
            // 차량수정ToolStripMenuItem
            // 
            this.차량수정ToolStripMenuItem.Name = "차량수정ToolStripMenuItem";
            this.차량수정ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.차량수정ToolStripMenuItem.Text = "차량 수정";
            this.차량수정ToolStripMenuItem.Click += new System.EventHandler(this.차량수정ToolStripMenuItem_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dataGridView1);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 차량추가ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 차량삭제ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 차량수정ToolStripMenuItem;
    }
}

