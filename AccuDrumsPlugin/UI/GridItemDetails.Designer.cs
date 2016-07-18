namespace Accudrums.UI {
    partial class GridItemDetails {
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
            this.lblCurrentItem = new System.Windows.Forms.Label();
            this.tbGain = new System.Windows.Forms.TrackBar();
            this.lblGain = new System.Windows.Forms.Label();
            this.lblPanning = new System.Windows.Forms.Label();
            this.tbPanning = new System.Windows.Forms.TrackBar();
            ((System.ComponentModel.ISupportInitialize)(this.tbGain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbPanning)).BeginInit();
            this.SuspendLayout();
            // 
            // lblCurrentItem
            // 
            this.lblCurrentItem.AutoSize = true;
            this.lblCurrentItem.Location = new System.Drawing.Point(201, 9);
            this.lblCurrentItem.Name = "lblCurrentItem";
            this.lblCurrentItem.Size = new System.Drawing.Size(67, 13);
            this.lblCurrentItem.TabIndex = 1;
            this.lblCurrentItem.Text = "Current Item:";
            // 
            // tbGain
            // 
            this.tbGain.Location = new System.Drawing.Point(26, 40);
            this.tbGain.Maximum = 600;
            this.tbGain.Minimum = -600;
            this.tbGain.Name = "tbGain";
            this.tbGain.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.tbGain.Size = new System.Drawing.Size(45, 94);
            this.tbGain.TabIndex = 2;
            this.tbGain.TickStyle = System.Windows.Forms.TickStyle.Both;
            this.tbGain.ValueChanged += new System.EventHandler(this.tbGain_ValueChanged);
            // 
            // lblGain
            // 
            this.lblGain.AutoSize = true;
            this.lblGain.Location = new System.Drawing.Point(33, 24);
            this.lblGain.Name = "lblGain";
            this.lblGain.Size = new System.Drawing.Size(29, 13);
            this.lblGain.TabIndex = 3;
            this.lblGain.Text = "Gain";
            // 
            // lblPanning
            // 
            this.lblPanning.AutoSize = true;
            this.lblPanning.Location = new System.Drawing.Point(124, 24);
            this.lblPanning.Name = "lblPanning";
            this.lblPanning.Size = new System.Drawing.Size(46, 13);
            this.lblPanning.TabIndex = 4;
            this.lblPanning.Text = "Panning";
            // 
            // tbPanning
            // 
            this.tbPanning.Location = new System.Drawing.Point(97, 60);
            this.tbPanning.Maximum = 100;
            this.tbPanning.Minimum = -100;
            this.tbPanning.Name = "tbPanning";
            this.tbPanning.Size = new System.Drawing.Size(104, 45);
            this.tbPanning.TabIndex = 5;
            this.tbPanning.ValueChanged += new System.EventHandler(this.tbPanning_ValueChanged);
            // 
            // GridItemDetails
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tbPanning);
            this.Controls.Add(this.lblPanning);
            this.Controls.Add(this.lblGain);
            this.Controls.Add(this.tbGain);
            this.Controls.Add(this.lblCurrentItem);
            this.Name = "GridItemDetails";
            this.Size = new System.Drawing.Size(633, 149);
            ((System.ComponentModel.ISupportInitialize)(this.tbGain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbPanning)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label lblCurrentItem;
        private System.Windows.Forms.TrackBar tbGain;
        private System.Windows.Forms.Label lblGain;
        private System.Windows.Forms.Label lblPanning;
        private System.Windows.Forms.TrackBar tbPanning;
    }
}
