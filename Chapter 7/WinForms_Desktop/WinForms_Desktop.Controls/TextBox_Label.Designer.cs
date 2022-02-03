namespace WinForms_Desktop.Controls
{
    partial class TextBox_Label
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
            this.tbl_label = new System.Windows.Forms.Label();
            this.tbl_text = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // tbl_label
            // 
            this.tbl_label.AutoSize = true;
            this.tbl_label.Location = new System.Drawing.Point(3, 7);
            this.tbl_label.Name = "tbl_label";
            this.tbl_label.Size = new System.Drawing.Size(35, 13);
            this.tbl_label.TabIndex = 0;
            this.tbl_label.Text = "label1";
            // 
            // tbl_text
            // 
            this.tbl_text.Location = new System.Drawing.Point(93, 3);
            this.tbl_text.Name = "tbl_text";
            this.tbl_text.Size = new System.Drawing.Size(146, 20);
            this.tbl_text.TabIndex = 1;
            // 
            // TextBox_Label
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tbl_text);
            this.Controls.Add(this.tbl_label);
            this.Name = "TextBox_Label";
            this.Size = new System.Drawing.Size(248, 27);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label tbl_label;
        private System.Windows.Forms.TextBox tbl_text;
    }
}
