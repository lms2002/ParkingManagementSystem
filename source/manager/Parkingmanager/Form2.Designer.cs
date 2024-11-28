namespace Parkingmanager
{
    partial class Form2
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
            this.textBoxNewSpot = new System.Windows.Forms.TextBox();
            this.buttonMoveVehicle = new System.Windows.Forms.Button();
            this.textBoxVehicleNumber = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // textBoxNewSpot
            // 
            this.textBoxNewSpot.Location = new System.Drawing.Point(415, 191);
            this.textBoxNewSpot.Name = "textBoxNewSpot";
            this.textBoxNewSpot.Size = new System.Drawing.Size(100, 21);
            this.textBoxNewSpot.TabIndex = 1;
            // 
            // buttonMoveVehicle
            // 
            this.buttonMoveVehicle.Location = new System.Drawing.Point(327, 283);
            this.buttonMoveVehicle.Name = "buttonMoveVehicle";
            this.buttonMoveVehicle.Size = new System.Drawing.Size(75, 23);
            this.buttonMoveVehicle.TabIndex = 2;
            this.buttonMoveVehicle.Text = "button1";
            this.buttonMoveVehicle.UseVisualStyleBackColor = true;
            this.buttonMoveVehicle.Click += new System.EventHandler(this.buttonMoveVehicle_Click);
            // 
            // textBoxVehicleNumber
            // 
            this.textBoxVehicleNumber.Location = new System.Drawing.Point(415, 111);
            this.textBoxVehicleNumber.Name = "textBoxVehicleNumber";
            this.textBoxVehicleNumber.Size = new System.Drawing.Size(100, 21);
            this.textBoxVehicleNumber.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(235, 114);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(96, 12);
            this.label1.TabIndex = 4;
            this.label1.Text = "Vehicle_number";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(235, 194);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(78, 12);
            this.label2.TabIndex = 5;
            this.label2.Text = "spot_number";
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBoxVehicleNumber);
            this.Controls.Add(this.buttonMoveVehicle);
            this.Controls.Add(this.textBoxNewSpot);
            this.Name = "Form2";
            this.Text = "Form2";
            this.Load += new System.EventHandler(this.Form2_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox textBoxNewSpot;
        private System.Windows.Forms.Button buttonMoveVehicle;
        private System.Windows.Forms.TextBox textBoxVehicleNumber;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}