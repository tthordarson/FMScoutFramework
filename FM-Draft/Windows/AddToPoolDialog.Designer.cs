namespace FM_Draft
{
    partial class AddToPoolDialog
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
            this.gridView = new System.Windows.Forms.DataGridView();
            this.includeInDraftCheckbox = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Fullname = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Object = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CurrentAbility = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PotientialAbility = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DateOfBirth = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.gridView)).BeginInit();
            this.SuspendLayout();
            // 
            // gridView
            // 
            this.gridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.includeInDraftCheckbox,
            this.Fullname,
            this.Object,
            this.CurrentAbility,
            this.PotientialAbility,
            this.DateOfBirth});
            this.gridView.Location = new System.Drawing.Point(0, 40);
            this.gridView.Name = "gridView";
            this.gridView.Size = new System.Drawing.Size(1288, 452);
            this.gridView.TabIndex = 0;
            // 
            // includeInDraftCheckbox
            // 
            this.includeInDraftCheckbox.HeaderText = "Include in Draft";
            this.includeInDraftCheckbox.Name = "includeInDraftCheckbox";
            // 
            // Fullname
            // 
            this.Fullname.DataPropertyName = "Fullname";
            this.Fullname.HeaderText = "Full Name";
            this.Fullname.Name = "Fullname";
            // 
            // Object
            // 
            this.Object.DataPropertyName = "Object";
            this.Object.HeaderText = "Column1";
            this.Object.Name = "Object";
            this.Object.Visible = false;
            // 
            // CurrentAbility
            // 
            this.CurrentAbility.DataPropertyName = "CurrentAbility";
            this.CurrentAbility.HeaderText = "Current Ability";
            this.CurrentAbility.Name = "CurrentAbility";
            // 
            // PotientialAbility
            // 
            this.PotientialAbility.DataPropertyName = "PotentialAbility";
            this.PotientialAbility.HeaderText = "Potential Ability";
            this.PotientialAbility.Name = "PotientialAbility";
            // 
            // DateOfBirth
            // 
            this.DateOfBirth.DataPropertyName = "DateOfBirth";
            this.DateOfBirth.HeaderText = "Date of Birth";
            this.DateOfBirth.Name = "DateOfBirth";
            // 
            // AddToPoolDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1300, 504);
            this.Controls.Add(this.gridView);
            this.Name = "AddToPoolDialog";
            this.Text = "AddToPoolDialog";
            this.Load += new System.EventHandler(this.AddToPoolDialog_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView gridView;
        private System.Windows.Forms.DataGridViewCheckBoxColumn includeInDraftCheckbox;
        private System.Windows.Forms.DataGridViewTextBoxColumn Fullname;
        private System.Windows.Forms.DataGridViewTextBoxColumn Object;
        private System.Windows.Forms.DataGridViewTextBoxColumn CurrentAbility;
        private System.Windows.Forms.DataGridViewTextBoxColumn PotientialAbility;
        private System.Windows.Forms.DataGridViewTextBoxColumn DateOfBirth;
    }
}