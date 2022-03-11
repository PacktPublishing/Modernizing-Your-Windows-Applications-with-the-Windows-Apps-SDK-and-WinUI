
namespace WinForms_Desktop
{
    partial class Form1
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.btn_cancel = new System.Windows.Forms.Button();
            this.btn_save = new System.Windows.Forms.Button();
            this.tbl_zip = new WinForms_Desktop.Controls.TextBox_Label();
            this.tbl_city = new WinForms_Desktop.Controls.TextBox_Label();
            this.tbl_address = new WinForms_Desktop.Controls.TextBox_Label();
            this.mtbl_salary = new WinForms_Desktop.Controls.MaskedTextBox_Label();
            this.mtbl_dob = new WinForms_Desktop.Controls.MaskedTextBox_Label();
            this.mtbl_hiringdate = new WinForms_Desktop.Controls.MaskedTextBox_Label();
            this.tbl_role = new WinForms_Desktop.Controls.TextBox_Label();
            this.line3 = new WinForms_Desktop.Controls.Line();
            this.line2 = new WinForms_Desktop.Controls.Line();
            this.tbl_lastname = new WinForms_Desktop.Controls.TextBox_Label();
            this.tbl_firstname = new WinForms_Desktop.Controls.TextBox_Label();
            this.line1 = new WinForms_Desktop.Controls.Line();
            this.Gender = new System.Windows.Forms.Label();
            this.cb_gender = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.DGV_Employees = new System.Windows.Forms.DataGridView();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DGV_Employees)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(12, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(776, 426);
            this.tabControl1.TabIndex = 2;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.DGV_Employees);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(768, 400);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Overview";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.btn_cancel);
            this.tabPage2.Controls.Add(this.btn_save);
            this.tabPage2.Controls.Add(this.tbl_zip);
            this.tabPage2.Controls.Add(this.tbl_city);
            this.tabPage2.Controls.Add(this.tbl_address);
            this.tabPage2.Controls.Add(this.mtbl_salary);
            this.tabPage2.Controls.Add(this.mtbl_dob);
            this.tabPage2.Controls.Add(this.mtbl_hiringdate);
            this.tabPage2.Controls.Add(this.tbl_role);
            this.tabPage2.Controls.Add(this.line3);
            this.tabPage2.Controls.Add(this.line2);
            this.tabPage2.Controls.Add(this.tbl_lastname);
            this.tabPage2.Controls.Add(this.tbl_firstname);
            this.tabPage2.Controls.Add(this.line1);
            this.tabPage2.Controls.Add(this.Gender);
            this.tabPage2.Controls.Add(this.cb_gender);
            this.tabPage2.Controls.Add(this.label1);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(768, 400);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Item";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // btn_cancel
            // 
            this.btn_cancel.Location = new System.Drawing.Point(611, 355);
            this.btn_cancel.Name = "btn_cancel";
            this.btn_cancel.Size = new System.Drawing.Size(75, 23);
            this.btn_cancel.TabIndex = 2012;
            this.btn_cancel.Text = "Reset Changes";
            this.btn_cancel.UseVisualStyleBackColor = true;
            this.btn_cancel.Click += new System.EventHandler(this.btn_cancel_Click);
            // 
            // btn_save
            // 
            this.btn_save.Location = new System.Drawing.Point(516, 355);
            this.btn_save.Name = "btn_save";
            this.btn_save.Size = new System.Drawing.Size(75, 23);
            this.btn_save.TabIndex = 2011;
            this.btn_save.Text = "Save";
            this.btn_save.UseVisualStyleBackColor = true;
            this.btn_save.Click += new System.EventHandler(this.btn_save_Click);
            // 
            // tbl_zip
            // 
            this.tbl_zip.Label = "label1";
            this.tbl_zip.Location = new System.Drawing.Point(354, 277);
            this.tbl_zip.Name = "tbl_zip";
            this.tbl_zip.Size = new System.Drawing.Size(248, 27);
            this.tbl_zip.TabIndex = 1;
            // 
            // tbl_city
            // 
            this.tbl_city.Label = "label1";
            this.tbl_city.Location = new System.Drawing.Point(14, 278);
            this.tbl_city.Name = "tbl_city";
            this.tbl_city.Size = new System.Drawing.Size(248, 27);
            this.tbl_city.TabIndex = 1;
            // 
            // tbl_address
            // 
            this.tbl_address.Label = "label1";
            this.tbl_address.Location = new System.Drawing.Point(14, 244);
            this.tbl_address.Name = "tbl_address";
            this.tbl_address.Size = new System.Drawing.Size(248, 27);
            this.tbl_address.TabIndex = 1;
            // 
            // mtbl_salary
            // 
            this.mtbl_salary.Label = "label1";
            this.mtbl_salary.Location = new System.Drawing.Point(14, 176);
            this.mtbl_salary.Mask = "00/00/0000";
            this.mtbl_salary.Name = "mtbl_salary";
            this.mtbl_salary.Size = new System.Drawing.Size(247, 27);
            this.mtbl_salary.TabIndex = 0;
            this.mtbl_salary.ValidatingType = typeof(System.DateTime);
            // 
            // mtbl_dob
            // 
            this.mtbl_dob.Label = "label1";
            this.mtbl_dob.Location = new System.Drawing.Point(354, 40);
            this.mtbl_dob.Mask = "00/00/0000";
            this.mtbl_dob.Name = "mtbl_dob";
            this.mtbl_dob.Size = new System.Drawing.Size(247, 27);
            this.mtbl_dob.TabIndex = 0;
            this.mtbl_dob.ValidatingType = typeof(System.DateTime);
            // 
            // mtbl_hiringdate
            // 
            this.mtbl_hiringdate.Label = "label1";
            this.mtbl_hiringdate.Location = new System.Drawing.Point(354, 141);
            this.mtbl_hiringdate.Mask = "00/00/0000";
            this.mtbl_hiringdate.Name = "mtbl_hiringdate";
            this.mtbl_hiringdate.Size = new System.Drawing.Size(247, 27);
            this.mtbl_hiringdate.TabIndex = 0;
            this.mtbl_hiringdate.ValidatingType = typeof(System.DateTime);
            // 
            // tbl_role
            // 
            this.tbl_role.Label = "label1";
            this.tbl_role.Location = new System.Drawing.Point(14, 142);
            this.tbl_role.Name = "tbl_role";
            this.tbl_role.Size = new System.Drawing.Size(248, 27);
            this.tbl_role.TabIndex = 1;
            // 
            // line3
            // 
            this.line3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.line3.Location = new System.Drawing.Point(14, 118);
            this.line3.MaximumSize = new System.Drawing.Size(2147483647, 2);
            this.line3.MinimumSize = new System.Drawing.Size(1, 2);
            this.line3.Name = "line3";
            this.line3.Size = new System.Drawing.Size(1, 2);
            this.line3.TabIndex = 2008;
            // 
            // line2
            // 
            this.line2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.line2.Location = new System.Drawing.Point(14, 116);
            this.line2.MaximumSize = new System.Drawing.Size(2147483647, 2);
            this.line2.MinimumSize = new System.Drawing.Size(1, 2);
            this.line2.Name = "line2";
            this.line2.Size = new System.Drawing.Size(1, 2);
            this.line2.TabIndex = 2005;
            // 
            // tbl_lastname
            // 
            this.tbl_lastname.Label = "";
            this.tbl_lastname.Location = new System.Drawing.Point(14, 75);
            this.tbl_lastname.Name = "tbl_lastname";
            this.tbl_lastname.Size = new System.Drawing.Size(248, 27);
            this.tbl_lastname.TabIndex = 2006;
            // 
            // tbl_firstname
            // 
            this.tbl_firstname.Label = "";
            this.tbl_firstname.Location = new System.Drawing.Point(14, 41);
            this.tbl_firstname.Name = "tbl_firstname";
            this.tbl_firstname.Size = new System.Drawing.Size(248, 27);
            this.tbl_firstname.TabIndex = 2005;
            // 
            // line1
            // 
            this.line1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.line1.Location = new System.Drawing.Point(14, 114);
            this.line1.MaximumSize = new System.Drawing.Size(2147483647, 2);
            this.line1.MinimumSize = new System.Drawing.Size(1, 2);
            this.line1.Name = "line1";
            this.line1.Size = new System.Drawing.Size(1, 2);
            this.line1.TabIndex = 2004;
            // 
            // Gender
            // 
            this.Gender.AutoSize = true;
            this.Gender.Location = new System.Drawing.Point(354, 81);
            this.Gender.Name = "Gender";
            this.Gender.Size = new System.Drawing.Size(42, 13);
            this.Gender.TabIndex = 2003;
            this.Gender.Text = "Gender";
            // 
            // cb_gender
            // 
            this.cb_gender.FormattingEnabled = true;
            this.cb_gender.Location = new System.Drawing.Point(446, 78);
            this.cb_gender.Name = "cb_gender";
            this.cb_gender.Size = new System.Drawing.Size(145, 21);
            this.cb_gender.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(6, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(194, 27);
            this.label1.TabIndex = 0;
            this.label1.Text = "Employee details";
            // 
            // DGV_Employees
            // 
            this.DGV_Employees.AllowUserToAddRows = false;
            this.DGV_Employees.AllowUserToDeleteRows = false;
            this.DGV_Employees.AllowUserToOrderColumns = true;
            this.DGV_Employees.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DGV_Employees.Location = new System.Drawing.Point(3, 3);
            this.DGV_Employees.Name = "DGV_Employees";
            this.DGV_Employees.ReadOnly = true;
            this.DGV_Employees.Size = new System.Drawing.Size(762, 391);
            this.DGV_Employees.TabIndex = 0;
            this.DGV_Employees.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.employeeDataGridView_CellClick);
            this.DGV_Employees.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.employeeDataGridView_CellFormatting);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.tabControl1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DGV_Employees)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion


        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cb_gender;
        private System.Windows.Forms.Label Gender;
        private WinForms_Desktop.Controls.TextBox_Label tbl_lastname;
        private WinForms_Desktop.Controls.TextBox_Label tbl_firstname;
        private WinForms_Desktop.Controls.Line line1;
        private Controls.Line line2;
        private Controls.Line line3;
        private Controls.TextBox_Label tbl_role;
        private Controls.MaskedTextBox_Label mtbl_dob;
        private Controls.MaskedTextBox_Label mtbl_hiringdate;
        private Controls.MaskedTextBox_Label mtbl_salary;
        private Controls.TextBox_Label tbl_zip;
        private Controls.TextBox_Label tbl_city;
        private Controls.TextBox_Label tbl_address;
        private System.Windows.Forms.Button btn_cancel;
        private System.Windows.Forms.Button btn_save;
        private System.Windows.Forms.DataGridView DGV_Employees;
    }
}

