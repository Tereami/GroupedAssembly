using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GroupedAssembly
{
    public partial class FormEnterName : Form
    {
        public string NameText;
        public bool GroupedElements;
        public bool UntouchBeams;

        public string LabelText
        {
            get { return label1.Text; }
            set { label1.Text = value; }
        }

        public FormEnterName()
        {
            
            InitializeComponent();
        }

        private void FormEnterName_Load(object sender, EventArgs e)
        {
            AcceptButton = btnOk;
            CancelButton = btnCancel;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            NameText = txtBoxName.Text;
            GroupedElements = checkBoxGroupElements.Checked;
            UntouchBeams = checkBoxUntouchBeams.Checked;
            this.DialogResult = DialogResult.OK;
            this.Close();

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
