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
            this.checkBoxUntouchBeams = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(78, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Укажите имя:";
            // 
            // txtBoxName
            // 
            this.txtBoxName.Location = new System.Drawing.Point(12, 25);
            this.txtBoxName.Name = "txtBoxName";
            this.txtBoxName.Size = new System.Drawing.Size(270, 20);
            this.txtBoxName.TabIndex = 1;
            // 
            // btnOk
            // 
            this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOk.Location = new System.Drawing.Point(207, 142);
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
            this.btnCancel.Location = new System.Drawing.Point(126, 142);
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
            this.checkBoxGroupElements.Checked = true;
            this.checkBoxGroupElements.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxGroupElements.Location = new System.Drawing.Point(10, 81);
            this.checkBoxGroupElements.Name = "checkBoxGroupElements";
            this.checkBoxGroupElements.Size = new System.Drawing.Size(156, 17);
            this.checkBoxGroupElements.TabIndex = 4;
            this.checkBoxGroupElements.Text = "Сгруппировать элементы";
            this.checkBoxGroupElements.UseVisualStyleBackColor = true;
            // 
            // checkBoxUntouchBeams
            // 
            this.checkBoxUntouchBeams.AutoSize = true;
            this.checkBoxUntouchBeams.Checked = true;
            this.checkBoxUntouchBeams.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxUntouchBeams.Location = new System.Drawing.Point(10, 104);
            this.checkBoxUntouchBeams.Name = "checkBoxUntouchBeams";
            this.checkBoxUntouchBeams.Size = new System.Drawing.Size(113, 17);
            this.checkBoxUntouchBeams.TabIndex = 5;
            this.checkBoxUntouchBeams.Text = "Открепить балки";
            this.checkBoxUntouchBeams.UseVisualStyleBackColor = true;
            // 
            // FormEnterName
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(294, 174);
            this.Controls.Add(this.checkBoxUntouchBeams);
            this.Controls.Add(this.checkBoxGroupElements);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.txtBoxName);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "FormEnterName";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Укажите имя";
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
        private System.Windows.Forms.CheckBox checkBoxUntouchBeams;
    }
}