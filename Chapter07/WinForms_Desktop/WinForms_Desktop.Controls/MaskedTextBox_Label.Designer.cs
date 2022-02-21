
namespace WinForms_Desktop.Controls
{
    partial class MaskedTextBox_Label
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.mtbl_text = new System.Windows.Forms.MaskedTextBox();
            this.mtbl_label = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // mtbl_text
            // 
            this.mtbl_text.Location = new System.Drawing.Point(94, 3);
            this.mtbl_text.Name = "mtbl_text";
            this.mtbl_text.Size = new System.Drawing.Size(144, 20);
            this.mtbl_text.TabIndex = 0;
            // 
            // mtbl_label
            // 
            this.mtbl_label.AutoSize = true;
            this.mtbl_label.Location = new System.Drawing.Point(3, 6);
            this.mtbl_label.Name = "mtbl_label";
            this.mtbl_label.Size = new System.Drawing.Size(35, 13);
            this.mtbl_label.TabIndex = 1;
            this.mtbl_label.Text = "label1";
            // 
            // MaskedTextBox_Label
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.mtbl_label);
            this.Controls.Add(this.mtbl_text);
            this.Name = "MaskedTextBox_Label";
            this.Size = new System.Drawing.Size(247, 27);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MaskedTextBox mtbl_text;
        private System.Windows.Forms.Label mtbl_label;
    }
}
