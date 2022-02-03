using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinForms_Desktop.Controls
{
    public partial class MaskedTextBox_Label : UserControl
    {
        public string Label
        {
            get { return mtbl_label.Text; }
            set { mtbl_label.Text = value; }
        }

        public string Text
        {
            get { return mtbl_text.Text; }
            set { mtbl_text.Text = value; }
        }

        public int TabIndex
        {
            get { return mtbl_text.TabIndex; }
            set { mtbl_text.TabIndex = value; }
        }

        public string Mask
        {
            get { return mtbl_text.Mask; }
            set { mtbl_text.Mask = value; }
        }

        private Type myVar;

        public Type ValidatingType
        {
            get { return mtbl_text.ValidatingType; }
            set { mtbl_text.ValidatingType = value; }
        }



        public MaskedTextBox_Label()
        {
            InitializeComponent();

            Mask = "00/00/0000"; //default short datetime
            ValidatingType = typeof(DateTime);
        }
    }
}
