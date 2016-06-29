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
            this.lblNote = new System.Windows.Forms.Label();
            this.pnlButtonGrid = new System.Windows.Forms.Panel();
            this.lblCurrentKit = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(172, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(119, 25);
            this.label1.TabIndex = 0;
            this.label1.Text = "Accudrums";
            // 
            // lblNote
            // 
            this.lblNote.AutoSize = true;
            this.lblNote.Location = new System.Drawing.Point(305, 67);
            this.lblNote.Name = "lblNote";
            this.lblNote.Size = new System.Drawing.Size(36, 13);
            this.lblNote.TabIndex = 2;
            this.lblNote.Text = "Note: ";
            // 
            // pnlButtonGrid
            // 
            this.pnlButtonGrid.Location = new System.Drawing.Point(12, 83);
            this.pnlButtonGrid.Name = "pnlButtonGrid";
            this.pnlButtonGrid.Size = new System.Drawing.Size(437, 250);
            this.pnlButtonGrid.TabIndex = 4;
            // 
            // lblCurrentKit
            // 
            this.lblCurrentKit.AutoSize = true;
            this.lblCurrentKit.Location = new System.Drawing.Point(177, 44);
            this.lblCurrentKit.Name = "lblCurrentKit";
            this.lblCurrentKit.Size = new System.Drawing.Size(25, 13);
            this.lblCurrentKit.TabIndex = 5;
            this.lblCurrentKit.Text = "Kit: ";
            // 
            // AccudrumsBase
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDark;
            this.Controls.Add(this.lblCurrentKit);
            this.Controls.Add(this.pnlButtonGrid);
            this.Controls.Add(this.lblNote);
            this.Controls.Add(this.label1);
            this.Name = "AccudrumsBase";
            this.Size = new System.Drawing.Size(463, 379);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblNote;
        private System.Windows.Forms.Panel pnlButtonGrid;
        private System.Windows.Forms.Label lblCurrentKit;
    }
}
