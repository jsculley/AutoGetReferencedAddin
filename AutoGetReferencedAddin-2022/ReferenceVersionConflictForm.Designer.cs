namespace org.duckdns.buttercup.autogetreferenced
{
    partial class ReferenceVersionConflictForm
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.bannerLabel = new System.Windows.Forms.Label();
            this.continueButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.conflictDataGridView = new System.Windows.Forms.DataGridView();
            this.filenameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.requestedVersionDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.foundVersionDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Details = new System.Windows.Forms.DataGridViewButtonColumn();
            this.versionConflictInfoBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.buttonHelpLabel = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.conflictDataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.versionConflictInfoBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.Window;
            this.panel1.Controls.Add(this.bannerLabel);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(648, 50);
            this.panel1.TabIndex = 3;
            // 
            // bannerLabel
            // 
            this.bannerLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.bannerLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bannerLabel.Location = new System.Drawing.Point(0, 0);
            this.bannerLabel.Name = "bannerLabel";
            this.bannerLabel.Size = new System.Drawing.Size(648, 50);
            this.bannerLabel.TabIndex = 1;
            this.bannerLabel.Text = "Conflicting Version References Detected";
            this.bannerLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // continueButton
            // 
            this.continueButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.continueButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.continueButton.Location = new System.Drawing.Point(480, 304);
            this.continueButton.Name = "continueButton";
            this.continueButton.Size = new System.Drawing.Size(75, 23);
            this.continueButton.TabIndex = 4;
            this.continueButton.Text = "Continue";
            this.toolTip1.SetToolTip(this.continueButton, "Continue opening the file");
            this.continueButton.UseVisualStyleBackColor = true;
            // 
            // cancelButton
            // 
            this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location = new System.Drawing.Point(561, 304);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 5;
            this.cancelButton.Text = "Cancel";
            this.toolTip1.SetToolTip(this.cancelButton, "Cancel opening the file");
            this.cancelButton.UseVisualStyleBackColor = true;
            // 
            // conflictDataGridView
            // 
            this.conflictDataGridView.AllowUserToAddRows = false;
            this.conflictDataGridView.AllowUserToDeleteRows = false;
            this.conflictDataGridView.AllowUserToResizeRows = false;
            this.conflictDataGridView.AutoGenerateColumns = false;
            this.conflictDataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.conflictDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.conflictDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.filenameDataGridViewTextBoxColumn,
            this.requestedVersionDataGridViewTextBoxColumn,
            this.foundVersionDataGridViewTextBoxColumn,
            this.Details});
            this.conflictDataGridView.DataSource = this.versionConflictInfoBindingSource;
            this.conflictDataGridView.Dock = System.Windows.Forms.DockStyle.Top;
            this.conflictDataGridView.Location = new System.Drawing.Point(0, 50);
            this.conflictDataGridView.MultiSelect = false;
            this.conflictDataGridView.Name = "conflictDataGridView";
            this.conflictDataGridView.ReadOnly = true;
            this.conflictDataGridView.RowHeadersVisible = false;
            this.conflictDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.conflictDataGridView.Size = new System.Drawing.Size(648, 248);
            this.conflictDataGridView.TabIndex = 6;
            this.conflictDataGridView.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.conflictDataGridView_CellContentClick);
            this.conflictDataGridView.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.conflictDataGridView_DataBindingComplete);
            // 
            // filenameDataGridViewTextBoxColumn
            // 
            this.filenameDataGridViewTextBoxColumn.DataPropertyName = "Filename";
            this.filenameDataGridViewTextBoxColumn.HeaderText = "Filename";
            this.filenameDataGridViewTextBoxColumn.Name = "filenameDataGridViewTextBoxColumn";
            this.filenameDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // requestedVersionDataGridViewTextBoxColumn
            // 
            this.requestedVersionDataGridViewTextBoxColumn.DataPropertyName = "RequestedVersion";
            this.requestedVersionDataGridViewTextBoxColumn.HeaderText = "Requested Version";
            this.requestedVersionDataGridViewTextBoxColumn.Name = "requestedVersionDataGridViewTextBoxColumn";
            this.requestedVersionDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // foundVersionDataGridViewTextBoxColumn
            // 
            this.foundVersionDataGridViewTextBoxColumn.DataPropertyName = "FoundVersion";
            this.foundVersionDataGridViewTextBoxColumn.HeaderText = "Found Version";
            this.foundVersionDataGridViewTextBoxColumn.Name = "foundVersionDataGridViewTextBoxColumn";
            this.foundVersionDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // Details
            // 
            this.Details.HeaderText = "";
            this.Details.Name = "Details";
            this.Details.ReadOnly = true;
            this.Details.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Details.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.Details.Text = "Details";
            this.Details.UseColumnTextForButtonValue = true;
            // 
            // versionConflictInfoBindingSource
            // 
            this.versionConflictInfoBindingSource.DataSource = typeof(VersionConflictInfo);
            // 
            // buttonHelpLabel
            // 
            this.buttonHelpLabel.Location = new System.Drawing.Point(5, 304);
            this.buttonHelpLabel.Name = "buttonHelpLabel";
            this.buttonHelpLabel.Size = new System.Drawing.Size(445, 26);
            this.buttonHelpLabel.TabIndex = 7;
            this.buttonHelpLabel.Text = "Press \'Continue\' to open the file using the found referenced versions or \'Cancel\'" +
    " to cancel the opening of the file.";
            // 
            // ReferenceVersionConflictForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(648, 339);
            this.Controls.Add(this.buttonHelpLabel);
            this.Controls.Add(this.conflictDataGridView);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.continueButton);
            this.Controls.Add(this.panel1);
            this.Name = "ReferenceVersionConflictForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Reference Version Conflict";
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.conflictDataGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.versionConflictInfoBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label bannerLabel;
        private System.Windows.Forms.Button continueButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.DataGridView conflictDataGridView;
        private System.Windows.Forms.Label buttonHelpLabel;
        private System.Windows.Forms.BindingSource versionConflictInfoBindingSource;
        private System.Windows.Forms.DataGridViewTextBoxColumn filenameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn requestedVersionDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn foundVersionDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewButtonColumn Details;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}