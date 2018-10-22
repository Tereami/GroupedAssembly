using System;
using System.Windows.Forms;

namespace GroupedAssembly
{
    public partial class FormEnterName : Form
    {
        public bool GroupedElements;
        public string NameText;
        public bool UntouchBeams;

        public FormEnterName()
        {
            InitializeComponent();
        }

        public string LabelText
        {
            get => label1.Text;
            set => label1.Text = value;
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
            DialogResult = DialogResult.OK;
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}