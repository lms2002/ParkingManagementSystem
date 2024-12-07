namespace manager
{
    partial class StoreManager
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
            this.dgvStoreDetails = new System.Windows.Forms.DataGridView();
            this.lvStoreList = new System.Windows.Forms.ListView();
            this.cmsStoreOptions = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.btnAdd = new System.Windows.Forms.ToolStripMenuItem();
            this.btnDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.btnEdit = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.dgvStoreDetails)).BeginInit();
            this.cmsStoreOptions.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvStoreDetails
            // 
            this.dgvStoreDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvStoreDetails.ContextMenuStrip = this.cmsStoreOptions;
            this.dgvStoreDetails.Location = new System.Drawing.Point(211, 43);
            this.dgvStoreDetails.Name = "dgvStoreDetails";
            this.dgvStoreDetails.RowTemplate.Height = 23;
            this.dgvStoreDetails.Size = new System.Drawing.Size(547, 233);
            this.dgvStoreDetails.TabIndex = 0;
            // 
            // lvStoreList
            // 
            this.lvStoreList.HideSelection = false;
            this.lvStoreList.Location = new System.Drawing.Point(36, 43);
            this.lvStoreList.Name = "lvStoreList";
            this.lvStoreList.Size = new System.Drawing.Size(117, 233);
            this.lvStoreList.TabIndex = 1;
            this.lvStoreList.UseCompatibleStateImageBehavior = false;
            this.lvStoreList.SelectedIndexChanged += new System.EventHandler(this.lvStoreList_SelectedIndexChanged);
            // 
            // cmsStoreOptions
            // 
            this.cmsStoreOptions.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnAdd,
            this.btnDelete,
            this.btnEdit});
            this.cmsStoreOptions.Name = "cmsStoreOptions";
            this.cmsStoreOptions.Size = new System.Drawing.Size(99, 70);
            this.cmsStoreOptions.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.cmsStoreOptions_ItemClicked);
            // 
            // btnAdd
            // 
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(98, 22);
            this.btnAdd.Text = "추가";
            // 
            // btnDelete
            // 
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(98, 22);
            this.btnDelete.Text = "삭제";
            // 
            // btnEdit
            // 
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(98, 22);
            this.btnEdit.Text = "수정";
            // 
            // StoreManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 315);
            this.Controls.Add(this.lvStoreList);
            this.Controls.Add(this.dgvStoreDetails);
            this.Name = "StoreManager";
            this.Text = "StoreManager";
            ((System.ComponentModel.ISupportInitialize)(this.dgvStoreDetails)).EndInit();
            this.cmsStoreOptions.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvStoreDetails;
        private System.Windows.Forms.ListView lvStoreList;
        private System.Windows.Forms.ContextMenuStrip cmsStoreOptions;
        private System.Windows.Forms.ToolStripMenuItem btnAdd;
        private System.Windows.Forms.ToolStripMenuItem btnDelete;
        private System.Windows.Forms.ToolStripMenuItem btnEdit;
    }
}