namespace ADOform_v7_2163
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
            this.DBGrid = new System.Windows.Forms.DataGridView();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.선택한행업데이트ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.새로운데이터추가ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.선택한행삭제ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.DBGrid)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // DBGrid
            // 
            this.DBGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DBGrid.ContextMenuStrip = this.contextMenuStrip1;
            this.DBGrid.Location = new System.Drawing.Point(251, 223);
            this.DBGrid.Name = "DBGrid";
            this.DBGrid.RowHeadersWidth = 62;
            this.DBGrid.RowTemplate.Height = 30;
            this.DBGrid.Size = new System.Drawing.Size(868, 449);
            this.DBGrid.TabIndex = 0;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.선택한행업데이트ToolStripMenuItem,
            this.새로운데이터추가ToolStripMenuItem,
            this.선택한행삭제ToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(241, 100);
            // 
            // 선택한행업데이트ToolStripMenuItem
            // 
            this.선택한행업데이트ToolStripMenuItem.Name = "선택한행업데이트ToolStripMenuItem";
            this.선택한행업데이트ToolStripMenuItem.Size = new System.Drawing.Size(240, 32);
            this.선택한행업데이트ToolStripMenuItem.Text = "선택한 행 업데이트";
            this.선택한행업데이트ToolStripMenuItem.Click += new System.EventHandler(this.선택한형헙데이트ToolStripMenuItem_Click);
            // 
            // 새로운데이터추가ToolStripMenuItem
            // 
            this.새로운데이터추가ToolStripMenuItem.Name = "새로운데이터추가ToolStripMenuItem";
            this.새로운데이터추가ToolStripMenuItem.Size = new System.Drawing.Size(240, 32);
            this.새로운데이터추가ToolStripMenuItem.Text = "새로운 데이터 추가";
            this.새로운데이터추가ToolStripMenuItem.Click += new System.EventHandler(this.새로운데이터추가ToolStripMenuItem_Click);
            // 
            // 선택한행삭제ToolStripMenuItem
            // 
            this.선택한행삭제ToolStripMenuItem.Name = "선택한행삭제ToolStripMenuItem";
            this.선택한행삭제ToolStripMenuItem.Size = new System.Drawing.Size(240, 32);
            this.선택한행삭제ToolStripMenuItem.Text = "선택한 행 삭제";
            this.선택한행삭제ToolStripMenuItem.Click += new System.EventHandler(this.선택한형삭제ToolStripMenuItem_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1206, 812);
            this.Controls.Add(this.DBGrid);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.DBGrid)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView DBGrid;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 선택한행업데이트ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 새로운데이터추가ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 선택한행삭제ToolStripMenuItem;
    }
}

