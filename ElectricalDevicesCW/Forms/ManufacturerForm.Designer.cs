
namespace ElectricalDevicesCW.Forms
{
    partial class ManufacturerForm
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.Country_ComboBox = new System.Windows.Forms.ComboBox();
            this.DelManufacturer_Button = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.Edit_Button = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.AddManufacturer_Button = new System.Windows.Forms.Button();
            this.ManufacturerName_TextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.Manufacturer_ListBox = new System.Windows.Forms.ListBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.groupBox1.Controls.Add(this.Country_ComboBox);
            this.groupBox1.Controls.Add(this.DelManufacturer_Button);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.Edit_Button);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.AddManufacturer_Button);
            this.groupBox1.Controls.Add(this.ManufacturerName_TextBox);
            this.groupBox1.Location = new System.Drawing.Point(501, 21);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(412, 225);
            this.groupBox1.TabIndex = 75;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Производитель";
            // 
            // Country_ComboBox
            // 
            this.Country_ComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Country_ComboBox.FormattingEnabled = true;
            this.Country_ComboBox.Location = new System.Drawing.Point(69, 43);
            this.Country_ComboBox.Name = "Country_ComboBox";
            this.Country_ComboBox.Size = new System.Drawing.Size(224, 21);
            this.Country_ComboBox.TabIndex = 72;
            // 
            // DelManufacturer_Button
            // 
            this.DelManufacturer_Button.Location = new System.Drawing.Point(275, 181);
            this.DelManufacturer_Button.Name = "DelManufacturer_Button";
            this.DelManufacturer_Button.Size = new System.Drawing.Size(133, 39);
            this.DelManufacturer_Button.TabIndex = 74;
            this.DelManufacturer_Button.Text = "Удалить";
            this.DelManufacturer_Button.UseVisualStyleBackColor = true;
            this.DelManufacturer_Button.Click += new System.EventHandler(this.DelManufacturer_Button_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(5, 44);
            this.label8.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(46, 13);
            this.label8.TabIndex = 53;
            this.label8.Text = "Страна:";
            // 
            // Edit_Button
            // 
            this.Edit_Button.Location = new System.Drawing.Point(139, 181);
            this.Edit_Button.Name = "Edit_Button";
            this.Edit_Button.Size = new System.Drawing.Size(133, 39);
            this.Edit_Button.TabIndex = 66;
            this.Edit_Button.Text = "Изменить";
            this.Edit_Button.UseVisualStyleBackColor = true;
            this.Edit_Button.Click += new System.EventHandler(this.Edit_Button_Click);
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
            // AddManufacturer_Button
            // 
            this.AddManufacturer_Button.Location = new System.Drawing.Point(5, 181);
            this.AddManufacturer_Button.Name = "AddManufacturer_Button";
            this.AddManufacturer_Button.Size = new System.Drawing.Size(133, 39);
            this.AddManufacturer_Button.TabIndex = 61;
            this.AddManufacturer_Button.Text = "Добавить ";
            this.AddManufacturer_Button.UseVisualStyleBackColor = true;
            this.AddManufacturer_Button.Click += new System.EventHandler(this.AddManufacturer_Button_Click);
            // 
            // ManufacturerName_TextBox
            // 
            this.ManufacturerName_TextBox.Location = new System.Drawing.Point(69, 18);
            this.ManufacturerName_TextBox.Margin = new System.Windows.Forms.Padding(2);
            this.ManufacturerName_TextBox.Name = "ManufacturerName_TextBox";
            this.ManufacturerName_TextBox.Size = new System.Drawing.Size(224, 20);
            this.ManufacturerName_TextBox.TabIndex = 56;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(133, 13);
            this.label1.TabIndex = 73;
            this.label1.Text = "Список производителей:";
            // 
            // Manufacturer_ListBox
            // 
            this.Manufacturer_ListBox.FormattingEnabled = true;
            this.Manufacturer_ListBox.Location = new System.Drawing.Point(12, 21);
            this.Manufacturer_ListBox.Name = "Manufacturer_ListBox";
            this.Manufacturer_ListBox.Size = new System.Drawing.Size(483, 225);
            this.Manufacturer_ListBox.TabIndex = 72;
            this.Manufacturer_ListBox.SelectedIndexChanged += new System.EventHandler(this.Manufacturer_ListBox_SelectedIndexChanged);
            // 
            // ManufacturerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(920, 252);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Manufacturer_ListBox);
            this.Name = "ManufacturerForm";
            this.Text = "ManufacturerForm";
            this.Load += new System.EventHandler(this.ManufacturerForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox Country_ComboBox;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button Edit_Button;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button AddManufacturer_Button;
        private System.Windows.Forms.TextBox ManufacturerName_TextBox;
        private System.Windows.Forms.Button DelManufacturer_Button;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox Manufacturer_ListBox;
    }
}