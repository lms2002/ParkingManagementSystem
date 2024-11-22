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
            this.SuspendLayout();
            // 
            // lblTotalAvailableSpots
            // 
            this.lblTotalAvailableSpots.Font = new System.Drawing.Font("굴림", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblTotalAvailableSpots.Location = new System.Drawing.Point(171, 122);
            this.lblTotalAvailableSpots.Name = "lblTotalAvailableSpots";
            this.lblTotalAvailableSpots.Size = new System.Drawing.Size(511, 39);
            this.lblTotalAvailableSpots.TabIndex = 0;
            this.lblTotalAvailableSpots.Text = "총 잔여 자리";
            this.lblTotalAvailableSpots.Click += new System.EventHandler(this.lblTotalAvailableSpots_Click);
            // 
            // lblRegularAvailableSpots
            // 
            this.lblRegularAvailableSpots.Font = new System.Drawing.Font("굴림", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblRegularAvailableSpots.Location = new System.Drawing.Point(171, 183);
            this.lblRegularAvailableSpots.Name = "lblRegularAvailableSpots";
            this.lblRegularAvailableSpots.Size = new System.Drawing.Size(511, 39);
            this.lblRegularAvailableSpots.TabIndex = 1;
            this.lblRegularAvailableSpots.Text = "일반석 빈 자리";
            this.lblRegularAvailableSpots.Click += new System.EventHandler(this.lblRegularAvailableSpots_Click);
            // 
            // lblDisabledAvailableSpots
            // 
            this.lblDisabledAvailableSpots.Font = new System.Drawing.Font("굴림", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblDisabledAvailableSpots.Location = new System.Drawing.Point(171, 241);
            this.lblDisabledAvailableSpots.Name = "lblDisabledAvailableSpots";
            this.lblDisabledAvailableSpots.Size = new System.Drawing.Size(511, 39);
            this.lblDisabledAvailableSpots.TabIndex = 2;
            this.lblDisabledAvailableSpots.Text = "장애석 빈 자리";
            this.lblDisabledAvailableSpots.Click += new System.EventHandler(this.lblDisabledAvailableSpots_Click);
            // 
            // ParkingStatusForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.lblDisabledAvailableSpots);
            this.Controls.Add(this.lblRegularAvailableSpots);
            this.Controls.Add(this.lblTotalAvailableSpots);
            this.Name = "ParkingStatusForm";
            this.Text = "Parking Entry";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.ParkingStatusForm_FormClosed);
            this.Click += new System.EventHandler(this.ParkingStatusForm_Click);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblTotalAvailableSpots;
        private System.Windows.Forms.Label lblRegularAvailableSpots;
        private System.Windows.Forms.Label lblDisabledAvailableSpots;
    }
}