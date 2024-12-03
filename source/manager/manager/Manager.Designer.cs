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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.관리자메뉴ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.차량관리ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.주차석관리ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.매장할인관리ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.매출관리ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label1 = new System.Windows.Forms.Label();
            this.menuStrip1.SuspendLayout();
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
            this.매장할인관리ToolStripMenuItem,
            this.매출관리ToolStripMenuItem});
            this.관리자메뉴ToolStripMenuItem.Name = "관리자메뉴ToolStripMenuItem";
            this.관리자메뉴ToolStripMenuItem.Size = new System.Drawing.Size(83, 20);
            this.관리자메뉴ToolStripMenuItem.Text = "관리자 메뉴";
            // 
            // 차량관리ToolStripMenuItem
            // 
            this.차량관리ToolStripMenuItem.Name = "차량관리ToolStripMenuItem";
            this.차량관리ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.차량관리ToolStripMenuItem.Text = "차량 관리";
            this.차량관리ToolStripMenuItem.Click += new System.EventHandler(this.차량관리ToolStripMenuItem_Click);
            // 
            // 주차석관리ToolStripMenuItem
            // 
            this.주차석관리ToolStripMenuItem.Name = "주차석관리ToolStripMenuItem";
            this.주차석관리ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.주차석관리ToolStripMenuItem.Text = "주차석 관리";
            this.주차석관리ToolStripMenuItem.Click += new System.EventHandler(this.주차석관리ToolStripMenuItem_Click);
            // 
            // 매장할인관리ToolStripMenuItem
            // 
            this.매장할인관리ToolStripMenuItem.Name = "매장할인관리ToolStripMenuItem";
            this.매장할인관리ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.매장할인관리ToolStripMenuItem.Text = "매장관리";
            // 
            // 매출관리ToolStripMenuItem
            // 
            this.매출관리ToolStripMenuItem.Name = "매출관리ToolStripMenuItem";
            this.매출관리ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.매출관리ToolStripMenuItem.Text = "매출 관리";
            this.매출관리ToolStripMenuItem.Click += new System.EventHandler(this.매출관리ToolStripMenuItem_Click);
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("한컴 말랑말랑 Regular", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label1.Location = new System.Drawing.Point(165, 124);
            this.label1.Name = "label1";
            this.label1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label1.Size = new System.Drawing.Size(467, 174);
            this.label1.TabIndex = 1;
            this.label1.Text = "관리자님 환영합니다.";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Manager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Manager";
            this.Text = "Form1";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 관리자메뉴ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 차량관리ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 주차석관리ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 매장할인관리ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 매출관리ToolStripMenuItem;
        private System.Windows.Forms.Label label1;
    }
}