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
            this.pluginEditorView = new Accudrums.UI.PluginEditorView();
            this.lblNote = new System.Windows.Forms.Label();
            this.lblKick = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(106, 55);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(216, 25);
            this.label1.TabIndex = 0;
            this.label1.Text = "Hier komt Accudrums";
            // 
            // pluginEditorView
            // 
            this.pluginEditorView.Location = new System.Drawing.Point(111, 172);
            this.pluginEditorView.Name = "pluginEditorView";
            this.pluginEditorView.Size = new System.Drawing.Size(215, 99);
            this.pluginEditorView.TabIndex = 1;
            // 
            // lblNote
            // 
            this.lblNote.AutoSize = true;
            this.lblNote.Location = new System.Drawing.Point(111, 113);
            this.lblNote.Name = "lblNote";
            this.lblNote.Size = new System.Drawing.Size(36, 13);
            this.lblNote.TabIndex = 2;
            this.lblNote.Text = "Note: ";
            // 
            // lblKick
            // 
            this.lblKick.AutoSize = true;
            this.lblKick.Location = new System.Drawing.Point(111, 126);
            this.lblKick.Name = "lblKick";
            this.lblKick.Size = new System.Drawing.Size(57, 13);
            this.lblKick.TabIndex = 3;
            this.lblKick.Text = "KICK: OFF";
            // 
            // AccudrumsBase
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDark;
            this.Controls.Add(this.lblKick);
            this.Controls.Add(this.lblNote);
            this.Controls.Add(this.pluginEditorView);
            this.Controls.Add(this.label1);
            this.Name = "AccudrumsBase";
            this.Size = new System.Drawing.Size(463, 379);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private PluginEditorView pluginEditorView;
        private System.Windows.Forms.Label lblNote;
        private System.Windows.Forms.Label lblKick;
    }
}
