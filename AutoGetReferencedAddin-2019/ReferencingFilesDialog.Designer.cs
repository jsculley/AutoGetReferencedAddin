namespace org.duckdns.buttercup.autogetreferenced
{
    partial class ReferencingFilesDialog
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.bannerLabel = new System.Windows.Forms.Label();
            this.referencingDocumentPathsListBox = new System.Windows.Forms.ListBox();
            this.closeButton = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.Window;
            this.panel1.Controls.Add(this.bannerLabel);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(392, 100);
            this.panel1.TabIndex = 3;
            // 
            // bannerLabel
            // 
            this.bannerLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.bannerLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bannerLabel.Location = new System.Drawing.Point(0, 0);
            this.bannerLabel.Name = "bannerLabel";
            this.bannerLabel.Size = new System.Drawing.Size(392, 100);
            this.bannerLabel.TabIndex = 3;
            this.bannerLabel.Text = "The open documents below are referencing different versions of \'";
            this.bannerLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // referencingDocumentPathsListBox
            // 
            this.referencingDocumentPathsListBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.referencingDocumentPathsListBox.FormattingEnabled = true;
            this.referencingDocumentPathsListBox.Location = new System.Drawing.Point(12, 106);
            this.referencingDocumentPathsListBox.Name = "referencingDocumentPathsListBox";
            this.referencingDocumentPathsListBox.Size = new System.Drawing.Size(368, 199);
            this.referencingDocumentPathsListBox.TabIndex = 4;
            // 
            // closeButton
            // 
            this.closeButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.closeButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.closeButton.Location = new System.Drawing.Point(307, 314);
            this.closeButton.Name = "closeButton";
            this.closeButton.Size = new System.Drawing.Size(73, 23);
            this.closeButton.TabIndex = 5;
            this.closeButton.Text = "Close";
            this.closeButton.UseVisualStyleBackColor = true;
            this.closeButton.Click += new System.EventHandler(this.closeButton_Click);
            // 
            // ReferencingFilesDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(392, 349);
            this.Controls.Add(this.closeButton);
            this.Controls.Add(this.referencingDocumentPathsListBox);
            this.Controls.Add(this.panel1);
            this.Name = "ReferencingFilesDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Referencing Documents";
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label bannerLabel;
        private System.Windows.Forms.ListBox referencingDocumentPathsListBox;
        private System.Windows.Forms.Button closeButton;
    }
}