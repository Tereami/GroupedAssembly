//Данный код опубликован под лицензией Creative Commons Attribution-NonCommercial-ShareAlike.
//Разрешено редактировать, изменять и брать данный код за основу для производных в некоммерческих целях,
//при условии указания авторства и если производные лицензируются на тех же условиях.
//Программа поставляется "как есть". Автор не несет ответственности за возможные последствия её использования.
//Зуев Александр, 2019, все права защищены.
//This code is listed under the Creative Commons Attribution-NonCommercial-ShareAlike license.
//You may redistribute, remix, tweak, and build upon this work non-commercially, 
//as long as you credit the author by linking back and license your new creations under the same terms.
//This code is provided 'as is'. Author disclaims any implied warranty. 
//Zuev Aleksandr, 2019, all rigths reserved.


using System;
using System.Windows.Forms;

namespace GroupedAssembly
{
    public partial class FormEnterName : Form
    {
        public bool GroupedElements;
        public string NameText;
        public bool UntouchBeamsEnds;
        public bool UntouchBeamsPlane;

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
            UntouchBeamsEnds = checkBoxUntouchBeamsEnds.Checked;
            UntouchBeamsPlane = checkBoxUntouchBeamsPlane.Checked;

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