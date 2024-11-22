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
            this.lblTotalAvailableSpots = new System.Windows.Forms.Label();
            this.lblRegularAvailableSpots = new System.Windows.Forms.Label();
            this.lblDisabledAvailableSpots = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lblCurrentTime = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblTotalAvailableSpots
            // 
            this.lblTotalAvailableSpots.Font = new System.Drawing.Font("굴림", 19.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblTotalAvailableSpots.Location = new System.Drawing.Point(114, 212);
            this.lblTotalAvailableSpots.Name = "lblTotalAvailableSpots";
            this.lblTotalAvailableSpots.Size = new System.Drawing.Size(584, 49);
            this.lblTotalAvailableSpots.TabIndex = 0;
            this.lblTotalAvailableSpots.Text = "총 잔여 자리";
            this.lblTotalAvailableSpots.Click += new System.EventHandler(this.lblTotalAvailableSpots_Click);
            // 
            // lblRegularAvailableSpots
            // 
            this.lblRegularAvailableSpots.Font = new System.Drawing.Font("굴림", 19.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblRegularAvailableSpots.Location = new System.Drawing.Point(114, 289);
            this.lblRegularAvailableSpots.Name = "lblRegularAvailableSpots";
            this.lblRegularAvailableSpots.Size = new System.Drawing.Size(584, 49);
            this.lblRegularAvailableSpots.TabIndex = 1;
            this.lblRegularAvailableSpots.Text = "일반석 빈 자리";
            this.lblRegularAvailableSpots.Click += new System.EventHandler(this.lblRegularAvailableSpots_Click);
            // 
            // lblDisabledAvailableSpots
            // 
            this.lblDisabledAvailableSpots.Font = new System.Drawing.Font("굴림", 19.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblDisabledAvailableSpots.Location = new System.Drawing.Point(114, 361);
            this.lblDisabledAvailableSpots.Name = "lblDisabledAvailableSpots";
            this.lblDisabledAvailableSpots.Size = new System.Drawing.Size(584, 49);
            this.lblDisabledAvailableSpots.TabIndex = 2;
            this.lblDisabledAvailableSpots.Text = "장애석 빈 자리";
            this.lblDisabledAvailableSpots.Click += new System.EventHandler(this.lblDisabledAvailableSpots_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("굴림", 22.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label1.Location = new System.Drawing.Point(90, 57);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(690, 37);
            this.label1.TabIndex = 3;
            this.label1.Text = "화면 터치 시 다음 화면으로 이동합니다";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // lblCurrentTime
            // 
            this.lblCurrentTime.AutoSize = true;
            this.lblCurrentTime.Font = new System.Drawing.Font("굴림", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblCurrentTime.Location = new System.Drawing.Point(699, 503);
            this.lblCurrentTime.Name = "lblCurrentTime";
            this.lblCurrentTime.Size = new System.Drawing.Size(81, 17);
            this.lblCurrentTime.TabIndex = 4;
            this.lblCurrentTime.Text = "현재 시간";
            this.lblCurrentTime.Click += new System.EventHandler(this.lblCurrentTime_Click);
            // 
            // ParkingStatusForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(914, 562);
            this.Controls.Add(this.lblCurrentTime);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblDisabledAvailableSpots);
            this.Controls.Add(this.lblRegularAvailableSpots);
            this.Controls.Add(this.lblTotalAvailableSpots);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "ParkingStatusForm";
            this.Text = "Parking Entry";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.ParkingStatusForm_FormClosed);
            this.Click += new System.EventHandler(this.ParkingStatusForm_Click);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTotalAvailableSpots;
        private System.Windows.Forms.Label lblRegularAvailableSpots;
        private System.Windows.Forms.Label lblDisabledAvailableSpots;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblCurrentTime;
    }
}