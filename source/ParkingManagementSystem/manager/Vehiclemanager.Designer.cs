﻿namespace manager
{
    partial class Vehiclemanager
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
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.차량관리ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.차량삭제ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.차량수정ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.차량영수증ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.listView1 = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.차량관리ToolStripMenuItem,
            this.차량영수증ToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(167, 48);
            // 
            // 차량관리ToolStripMenuItem
            // 
            this.차량관리ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.차량삭제ToolStripMenuItem,
            this.차량수정ToolStripMenuItem});
            this.차량관리ToolStripMenuItem.Name = "차량관리ToolStripMenuItem";
            this.차량관리ToolStripMenuItem.Size = new System.Drawing.Size(166, 22);
            this.차량관리ToolStripMenuItem.Text = "차량 관리";
            // 
            // 차량삭제ToolStripMenuItem
            // 
            this.차량삭제ToolStripMenuItem.Name = "차량삭제ToolStripMenuItem";
            this.차량삭제ToolStripMenuItem.Size = new System.Drawing.Size(126, 22);
            this.차량삭제ToolStripMenuItem.Text = "차량 삭제";
            this.차량삭제ToolStripMenuItem.Click += new System.EventHandler(this.차량삭제ToolStripMenuItem_Click_1);
            // 
            // 차량수정ToolStripMenuItem
            // 
            this.차량수정ToolStripMenuItem.Name = "차량수정ToolStripMenuItem";
            this.차량수정ToolStripMenuItem.Size = new System.Drawing.Size(126, 22);
            this.차량수정ToolStripMenuItem.Text = "차량 수정";
            this.차량수정ToolStripMenuItem.Click += new System.EventHandler(this.차량수정ToolStripMenuItem_Click_1);
            // 
            // 차량영수증ToolStripMenuItem
            // 
            this.차량영수증ToolStripMenuItem.Name = "차량영수증ToolStripMenuItem";
            this.차량영수증ToolStripMenuItem.Size = new System.Drawing.Size(166, 22);
            this.차량영수증ToolStripMenuItem.Text = "차량 영수증 관리";
            this.차량영수증ToolStripMenuItem.Click += new System.EventHandler(this.차량영수증ToolStripMenuItem_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(606, 138);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(119, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "차량 뒤 4자리로 검색";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(594, 173);
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
            this.button1.Text = "조회";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // listView1
            // 
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3});
            this.listView1.ContextMenuStrip = this.contextMenuStrip1;
            this.listView1.FullRowSelect = true;
            this.listView1.GridLines = true;
            this.listView1.HideSelection = false;
            this.listView1.Location = new System.Drawing.Point(12, 12);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(530, 426);
            this.listView1.TabIndex = 4;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "차량키";
            this.columnHeader1.Width = 79;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "차량 번호";
            this.columnHeader2.Width = 204;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "차량 종류";
            this.columnHeader3.Width = 242;
            // 
            // Vehiclemanager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label1);
            this.Name = "Vehiclemanager";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "차량목록";
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 차량관리ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 차량삭제ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 차량수정ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 차량영수증ToolStripMenuItem;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
    }
}

