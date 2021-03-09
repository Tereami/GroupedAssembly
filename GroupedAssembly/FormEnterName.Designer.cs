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


namespace GroupedAssembly
{
    partial class FormEnterName
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
            this.label1 = new System.Windows.Forms.Label();
            this.txtBoxName = new System.Windows.Forms.TextBox();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.checkBoxGroupElements = new System.Windows.Forms.CheckBox();
            this.checkBoxUntouchBeamsEnds = new System.Windows.Forms.CheckBox();
            this.checkBoxUntouchBeamsPlane = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(32, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Имя:";
            // 
            // txtBoxName
            // 
            this.txtBoxName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtBoxName.Location = new System.Drawing.Point(12, 51);
            this.txtBoxName.Name = "txtBoxName";
            this.txtBoxName.Size = new System.Drawing.Size(235, 20);
            this.txtBoxName.TabIndex = 1;
            // 
            // btnOk
            // 
            this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOk.Location = new System.Drawing.Point(91, 160);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 2;
            this.btnOk.Text = "ОК";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(172, 160);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "Отмена";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // checkBoxGroupElements
            // 
            this.checkBoxGroupElements.AutoSize = true;
            this.checkBoxGroupElements.Location = new System.Drawing.Point(12, 83);
            this.checkBoxGroupElements.Name = "checkBoxGroupElements";
            this.checkBoxGroupElements.Size = new System.Drawing.Size(156, 17);
            this.checkBoxGroupElements.TabIndex = 4;
            this.checkBoxGroupElements.Text = "Сгруппировать элементы";
            this.checkBoxGroupElements.UseVisualStyleBackColor = true;
            // 
            // checkBoxUntouchBeamsEnds
            // 
            this.checkBoxUntouchBeamsEnds.AutoSize = true;
            this.checkBoxUntouchBeamsEnds.Location = new System.Drawing.Point(12, 106);
            this.checkBoxUntouchBeamsEnds.Name = "checkBoxUntouchBeamsEnds";
            this.checkBoxUntouchBeamsEnds.Size = new System.Drawing.Size(148, 17);
            this.checkBoxUntouchBeamsEnds.TabIndex = 5;
            this.checkBoxUntouchBeamsEnds.Text = "Открепить концы балок";
            this.checkBoxUntouchBeamsEnds.UseVisualStyleBackColor = true;
            // 
            // checkBoxUntouchBeamsPlane
            // 
            this.checkBoxUntouchBeamsPlane.AutoSize = true;
            this.checkBoxUntouchBeamsPlane.Location = new System.Drawing.Point(12, 129);
            this.checkBoxUntouchBeamsPlane.Name = "checkBoxUntouchBeamsPlane";
            this.checkBoxUntouchBeamsPlane.Size = new System.Drawing.Size(195, 17);
            this.checkBoxUntouchBeamsPlane.TabIndex = 5;
            this.checkBoxUntouchBeamsPlane.Text = "Отсоединить балки от плоскости";
            this.checkBoxUntouchBeamsPlane.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(168, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Создание сборки из элементов";
            // 
            // FormEnterName
            // 
            this.AcceptButton = this.btnOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(259, 195);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.checkBoxUntouchBeamsPlane);
            this.Controls.Add(this.checkBoxUntouchBeamsEnds);
            this.Controls.Add(this.checkBoxGroupElements);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.txtBoxName);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "FormEnterName";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Группа-сборка";
            this.Load += new System.EventHandler(this.FormEnterName_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtBoxName;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.CheckBox checkBoxGroupElements;
        private System.Windows.Forms.CheckBox checkBoxUntouchBeamsEnds;
        private System.Windows.Forms.CheckBox checkBoxUntouchBeamsPlane;
        private System.Windows.Forms.Label label2;
    }
}