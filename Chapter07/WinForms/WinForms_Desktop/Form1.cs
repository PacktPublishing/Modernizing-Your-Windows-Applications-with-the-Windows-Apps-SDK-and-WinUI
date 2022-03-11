using System;
using System.ComponentModel;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinForms_Desktop.Data;
using WinForms_Desktop.Model;

namespace WinForms_Desktop
{
    public partial class Form1 : Form
    {
        private Employee selectedEmployee;
        private IEmployeeRepository repository;
        private BindingSource source;

        public Form1()
        {
            this.repository = new InMemoryEmployeeRepository();
            this.source = new BindingSource();

            InitializeComponent();
            DataSourceRefresh();
            cb_gender.DataSource = Enum.GetValues(typeof(Gender));
            tbl_firstname.Label = "First name:";
            tbl_firstname.TabIndex = 0;
            tbl_lastname.Label = "Last name";
            tbl_lastname.TabIndex = 1;
            mtbl_dob.Label = "Date of birth:";
            mtbl_dob.TabIndex = 2;
            tbl_role.Label = "Role";
            tbl_role.TabIndex = 4;
            mtbl_hiringdate.Label = "Hiring date:";
            mtbl_hiringdate.TabIndex = 5;
            mtbl_salary.Label = "Salary:";
            mtbl_salary.TabIndex = 6;
            mtbl_salary.Mask = "###.##";
            mtbl_salary.ValidatingType = typeof(decimal);
            tbl_address.Label = "Street + Number:";
            tbl_address.TabIndex = 7;
            tbl_city.Label = "City:";
            tbl_city.TabIndex = 8;
            tbl_zip.Label = "Zip code:";
            tbl_zip.TabIndex = 9;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            //DGV_Employees.Refresh();
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);
        }

        private void employeeContextBindingSource_CurrentChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void DataSourceRefresh()
        {
            DGV_Employees.DataSource = null;

            this.source.Clear();
            foreach (var employee in repository.GetAllEmployees())
            {
                this.source.Add(employee);
            }

            DGV_Employees.DataSource = this.source;
        }

        private async void employeeDataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) // sorting returns -1
            {
                var value = (int) DGV_Employees.Rows[e.RowIndex].Cells[0].Value;
                selectedEmployee = this.repository.GetById(value);
                LoadEmployee();
            }
        }

        private void employeeDataGridView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (DGV_Employees.Columns[e.ColumnIndex].Name.Equals("genderDataGridViewTextBoxColumn"))
            {
                var enumValue = (Gender)e.Value;
                e.Value = Enum.GetName(typeof(Gender), enumValue);
            }
        }

        private void LoadEmployee()
        {
            if (selectedEmployee == null) selectedEmployee = new Employee();
            //{
                tbl_firstname.Text = selectedEmployee.FirstName;
                tbl_lastname.Text = selectedEmployee.LastName;
                mtbl_dob.Text = selectedEmployee.DateOfBirth.Date.ToShortDateString();
                cb_gender.SelectedItem = selectedEmployee.Gender;
                tbl_role.Text = selectedEmployee.Role;
                mtbl_hiringdate.Text = selectedEmployee.DateOfHire.ToShortDateString();
                mtbl_salary.Text = selectedEmployee.Salary.ToString();
                tbl_address.Text = selectedEmployee.Address;                                                                                                                                 
                tbl_city.Text = selectedEmployee.City;
                tbl_zip.Text = selectedEmployee.ZipCode;
            //}
        }

        private bool StoreEmployee()
        {
            bool noErrors = true;
            if (selectedEmployee == null) selectedEmployee = new Employee();

            selectedEmployee.FirstName = tbl_firstname.Text.Trim();
            selectedEmployee.LastName = tbl_lastname.Text.Trim();
            selectedEmployee.Gender = (Gender)cb_gender.SelectedIndex;
            selectedEmployee.Role = tbl_role.Text.Trim();
            selectedEmployee.Address = tbl_address.Text.Trim();
            selectedEmployee.City = tbl_city.Text.Trim();
            selectedEmployee.ZipCode = tbl_zip.Text.Trim();

            if (Decimal.TryParse(mtbl_salary.Text, out decimal salary)) selectedEmployee.Salary = salary;
            else
            {
                MessageBox.Show("Failed to parse salary!");
                noErrors = false;
            }

            if (DateTime.TryParse(mtbl_dob.Text, out DateTime dob)) selectedEmployee.DateOfBirth = dob;
            else
            {
                MessageBox.Show("Failed to parse date of birth!");
                noErrors = false;
            }

            if (DateTime.TryParse(mtbl_hiringdate.Text, out DateTime hiringdate)) selectedEmployee.DateOfBirth = hiringdate;
            else
            {
                MessageBox.Show("Failed to parse date of hiring!");
                noErrors = false;
            }

            return noErrors;
        }

        private async void btn_save_Click(object sender, EventArgs e)
        {
            const string message = "Do you want to save changes?";
            const string caption = "Save";
            const MessageBoxButtons msgBoxButtons = MessageBoxButtons.OKCancel;

            if(MessageBox.Show(message, caption, msgBoxButtons) == DialogResult.OK)
            {
                if (StoreEmployee())
                {
                    this.repository.AddOrUpdate(selectedEmployee);
                    DataSourceRefresh();
                    this.DGV_Employees.Refresh();
                }
                else MessageBox.Show("Please correct errors before saving");
            }
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            const string message = "Do you really want to reset changes?";
            const string caption = "Reset";
            const MessageBoxButtons msgBoxButtons = MessageBoxButtons.OKCancel;

            if (MessageBox.Show(message, caption, msgBoxButtons) == DialogResult.OK)
            {
                LoadEmployee();
            }
        }
    }
}
