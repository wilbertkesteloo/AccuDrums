namespace Accudrums.UI {
    partial class AccudrumsBase {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.label1 = new System.Windows.Forms.Label();
            this.lblCurrentKit = new System.Windows.Forms.Label();
            this.pnlButtonGrid = new System.Windows.Forms.FlowLayoutPanel();
            this.gridItemDetails1 = new Accudrums.UI.GridItemDetails();
            this.lstKits = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(315, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(119, 25);
            this.label1.TabIndex = 0;
            this.label1.Text = "Accudrums";
            // 
            // lblCurrentKit
            // 
            this.lblCurrentKit.AutoSize = true;
            this.lblCurrentKit.Location = new System.Drawing.Point(305, 45);
            this.lblCurrentKit.Name = "lblCurrentKit";
            this.lblCurrentKit.Size = new System.Drawing.Size(25, 13);
            this.lblCurrentKit.TabIndex = 5;
            this.lblCurrentKit.Text = "Kit: ";
            // 
            // pnlButtonGrid
            // 
            this.pnlButtonGrid.Location = new System.Drawing.Point(25, 86);
            this.pnlButtonGrid.Name = "pnlButtonGrid";
            this.pnlButtonGrid.Size = new System.Drawing.Size(760, 210);
            this.pnlButtonGrid.TabIndex = 6;
            // 
            // gridItemDetails1
            // 
            this.gridItemDetails1.Location = new System.Drawing.Point(25, 320);
            this.gridItemDetails1.Name = "gridItemDetails1";
            this.gridItemDetails1.Size = new System.Drawing.Size(760, 149);
            this.gridItemDetails1.TabIndex = 7;
            // 
            // lstKits
            // 
            this.lstKits.FormattingEnabled = true;
            this.lstKits.Location = new System.Drawing.Point(25, 36);
            this.lstKits.Name = "lstKits";
            this.lstKits.Size = new System.Drawing.Size(121, 21);
            this.lstKits.TabIndex = 8;
            // 
            // AccudrumsBase
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDark;
            this.Controls.Add(this.lstKits);
            this.Controls.Add(this.gridItemDetails1);
            this.Controls.Add(this.pnlButtonGrid);
            this.Controls.Add(this.lblCurrentKit);
            this.Controls.Add(this.label1);
            this.Name = "AccudrumsBase";
            this.Size = new System.Drawing.Size(800, 481);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblCurrentKit;
        private System.Windows.Forms.FlowLayoutPanel pnlButtonGrid;
        private GridItemDetails gridItemDetails1;
        private System.Windows.Forms.ComboBox lstKits;
    }
}
