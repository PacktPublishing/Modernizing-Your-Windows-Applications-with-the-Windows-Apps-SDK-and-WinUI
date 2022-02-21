using System.Windows.Forms;

namespace WinForms_Desktop.Controls
{
    public partial class TextBox_Label: UserControl
    {
        public string Label
        {
            get { return tbl_label.Text; }
            set { tbl_label.Text = value; }
        }

        public string Text
        {
            get { return tbl_text.Text; }
            set { tbl_text.Text = value; }
        }

        public int TabIndex
        {
            get { return tbl_text.TabIndex; }
            set { tbl_text.TabIndex = value; }
        }


        public TextBox_Label()
        {
            InitializeComponent();
        }
    }
}
