
namespace ElectricalDevicesCW.Forms
{
    partial class CountryForm
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
            this.Delete_Button = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.Country_ListBox = new System.Windows.Forms.ListBox();
            this.CountryName_TextBox = new System.Windows.Forms.TextBox();
            this.Add_Button = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.Edit_Button = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // Delete_Button
            // 
            this.Delete_Button.Location = new System.Drawing.Point(272, 179);
            this.Delete_Button.Name = "Delete_Button";
            this.Delete_Button.Size = new System.Drawing.Size(133, 39);
            this.Delete_Button.TabIndex = 78;
            this.Delete_Button.Text = "Удалить";
            this.Delete_Button.UseVisualStyleBackColor = true;
            this.Delete_Button.Click += new System.EventHandler(this.Delete_Button_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(79, 13);
            this.label1.TabIndex = 77;
            this.label1.Text = "Список стран:";
            // 
            // Country_ListBox
            // 
            this.Country_ListBox.FormattingEnabled = true;
            this.Country_ListBox.Location = new System.Drawing.Point(12, 22);
            this.Country_ListBox.Name = "Country_ListBox";
            this.Country_ListBox.Size = new System.Drawing.Size(220, 225);
            this.Country_ListBox.TabIndex = 76;
            this.Country_ListBox.SelectedIndexChanged += new System.EventHandler(this.Country_ListBox_SelectedIndexChanged);
            // 
            // CountryName_TextBox
            // 
            this.CountryName_TextBox.Location = new System.Drawing.Point(69, 18);
            this.CountryName_TextBox.Margin = new System.Windows.Forms.Padding(2);
            this.CountryName_TextBox.Name = "CountryName_TextBox";
            this.CountryName_TextBox.Size = new System.Drawing.Size(203, 20);
            this.CountryName_TextBox.TabIndex = 56;
            // 
            // Add_Button
            // 
            this.Add_Button.Location = new System.Drawing.Point(5, 179);
            this.Add_Button.Name = "Add_Button";
            this.Add_Button.Size = new System.Drawing.Size(133, 39);
            this.Add_Button.TabIndex = 61;
            this.Add_Button.Text = "Добавить";
            this.Add_Button.UseVisualStyleBackColor = true;
            this.Add_Button.Click += new System.EventHandler(this.Add_Button_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(5, 21);
            this.label9.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(60, 13);
            this.label9.TabIndex = 51;
            this.label9.Text = "Название:";
            // 
            // Edit_Button
            // 
            this.Edit_Button.Location = new System.Drawing.Point(139, 179);
            this.Edit_Button.Name = "Edit_Button";
            this.Edit_Button.Size = new System.Drawing.Size(133, 39);
            this.Edit_Button.TabIndex = 66;
            this.Edit_Button.Text = "Изменить";
            this.Edit_Button.UseVisualStyleBackColor = true;
            this.Edit_Button.Click += new System.EventHandler(this.Edit_Button_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.groupBox1.Controls.Add(this.Edit_Button);
            this.groupBox1.Controls.Add(this.Delete_Button);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.Add_Button);
            this.groupBox1.Controls.Add(this.CountryName_TextBox);
            this.groupBox1.Location = new System.Drawing.Point(238, 22);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(412, 225);
            this.groupBox1.TabIndex = 79;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Страна";
            // 
            // CountryForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(658, 253);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Country_ListBox);
            this.Name = "CountryForm";
            this.Text = "CountryForm";            
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button Delete_Button;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox Country_ListBox;
        private System.Windows.Forms.TextBox CountryName_TextBox;
        private System.Windows.Forms.Button Add_Button;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button Edit_Button;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}