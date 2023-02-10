
namespace ElectricalDevicesCW.Forms
{
    partial class DeviceForm
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
            this.OrderDate_DateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.Del_Button = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.OrderName_TextBox = new System.Windows.Forms.TextBox();
            this.IsDefected_RadioButton = new System.Windows.Forms.RadioButton();
            this.ManufactureDate_DateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.Model_ComboBox = new System.Windows.Forms.ComboBox();
            this.Edit_Button = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.Add_Button = new System.Windows.Forms.Button();
            this.SerialNumber_TextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.Devices_ListBox = new System.Windows.Forms.ListBox();
            this.NotDefected_RadioButton = new System.Windows.Forms.RadioButton();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.groupBox1.Controls.Add(this.NotDefected_RadioButton);
            this.groupBox1.Controls.Add(this.OrderDate_DateTimePicker);
            this.groupBox1.Controls.Add(this.Del_Button);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.OrderName_TextBox);
            this.groupBox1.Controls.Add(this.IsDefected_RadioButton);
            this.groupBox1.Controls.Add(this.ManufactureDate_DateTimePicker);
            this.groupBox1.Controls.Add(this.Model_ComboBox);
            this.groupBox1.Controls.Add(this.Edit_Button);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.Add_Button);
            this.groupBox1.Controls.Add(this.SerialNumber_TextBox);
            this.groupBox1.Location = new System.Drawing.Point(492, 22);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(412, 225);
            this.groupBox1.TabIndex = 71;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Устройство";
            // 
            // OrderDate_DateTimePicker
            // 
            this.OrderDate_DateTimePicker.Location = new System.Drawing.Point(234, 153);
            this.OrderDate_DateTimePicker.Name = "OrderDate_DateTimePicker";
            this.OrderDate_DateTimePicker.Size = new System.Drawing.Size(172, 20);
            this.OrderDate_DateTimePicker.TabIndex = 82;
            // 
            // Del_Button
            // 
            this.Del_Button.Location = new System.Drawing.Point(276, 179);
            this.Del_Button.Name = "Del_Button";
            this.Del_Button.Size = new System.Drawing.Size(133, 39);
            this.Del_Button.TabIndex = 70;
            this.Del_Button.Text = "Удалить";
            this.Del_Button.UseVisualStyleBackColor = true;
            this.Del_Button.Click += new System.EventHandler(this.DelDevice_Button_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(212, 156);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(21, 13);
            this.label3.TabIndex = 81;
            this.label3.Text = "от:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(5, 156);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 13);
            this.label2.TabIndex = 79;
            this.label2.Text = "Заказ:";
            // 
            // OrderName_TextBox
            // 
            this.OrderName_TextBox.Location = new System.Drawing.Point(48, 153);
            this.OrderName_TextBox.Margin = new System.Windows.Forms.Padding(2);
            this.OrderName_TextBox.Name = "OrderName_TextBox";
            this.OrderName_TextBox.Size = new System.Drawing.Size(161, 20);
            this.OrderName_TextBox.TabIndex = 80;
            // 
            // IsDefected_RadioButton
            // 
            this.IsDefected_RadioButton.AutoSize = true;
            this.IsDefected_RadioButton.Location = new System.Drawing.Point(18, 126);
            this.IsDefected_RadioButton.Name = "IsDefected_RadioButton";
            this.IsDefected_RadioButton.Size = new System.Drawing.Size(83, 17);
            this.IsDefected_RadioButton.TabIndex = 78;
            this.IsDefected_RadioButton.TabStop = true;
            this.IsDefected_RadioButton.Text = "Дефектное";
            this.IsDefected_RadioButton.UseVisualStyleBackColor = true;
            // 
            // ManufactureDate_DateTimePicker
            // 
            this.ManufactureDate_DateTimePicker.Location = new System.Drawing.Point(121, 73);
            this.ManufactureDate_DateTimePicker.Name = "ManufactureDate_DateTimePicker";
            this.ManufactureDate_DateTimePicker.Size = new System.Drawing.Size(172, 20);
            this.ManufactureDate_DateTimePicker.TabIndex = 77;
            // 
            // Model_ComboBox
            // 
            this.Model_ComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Model_ComboBox.FormattingEnabled = true;
            this.Model_ComboBox.Location = new System.Drawing.Point(88, 19);
            this.Model_ComboBox.Name = "Model_ComboBox";
            this.Model_ComboBox.Size = new System.Drawing.Size(205, 21);
            this.Model_ComboBox.TabIndex = 72;
            // 
            // Edit_Button
            // 
            this.Edit_Button.Location = new System.Drawing.Point(141, 179);
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
            this.label9.Location = new System.Drawing.Point(5, 50);
            this.label9.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(96, 13);
            this.label9.TabIndex = 51;
            this.label9.Text = "Серийный номер:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(5, 20);
            this.label8.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(49, 13);
            this.label8.TabIndex = 53;
            this.label8.Text = "Модель:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(5, 77);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(110, 13);
            this.label7.TabIndex = 55;
            this.label7.Text = "Дата производства:";
            // 
            // Add_Button
            // 
            this.Add_Button.Location = new System.Drawing.Point(6, 179);
            this.Add_Button.Name = "Add_Button";
            this.Add_Button.Size = new System.Drawing.Size(133, 39);
            this.Add_Button.TabIndex = 61;
            this.Add_Button.Text = "Добавить";
            this.Add_Button.UseVisualStyleBackColor = true;
            this.Add_Button.Click += new System.EventHandler(this.AddDevice_Button_Click);
            // 
            // SerialNumber_TextBox
            // 
            this.SerialNumber_TextBox.Location = new System.Drawing.Point(105, 47);
            this.SerialNumber_TextBox.Margin = new System.Windows.Forms.Padding(2);
            this.SerialNumber_TextBox.Name = "SerialNumber_TextBox";
            this.SerialNumber_TextBox.Size = new System.Drawing.Size(188, 20);
            this.SerialNumber_TextBox.TabIndex = 56;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(0, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(101, 13);
            this.label1.TabIndex = 69;
            this.label1.Text = "Список устройств:";
            // 
            // Devices_ListBox
            // 
            this.Devices_ListBox.FormattingEnabled = true;
            this.Devices_ListBox.Location = new System.Drawing.Point(3, 22);
            this.Devices_ListBox.Name = "Devices_ListBox";
            this.Devices_ListBox.Size = new System.Drawing.Size(483, 225);
            this.Devices_ListBox.TabIndex = 68;
            this.Devices_ListBox.SelectedIndexChanged += new System.EventHandler(this.Devices_ListBox_SelectedIndexChanged);
            // 
            // NotDefected_RadioButton
            // 
            this.NotDefected_RadioButton.AutoSize = true;
            this.NotDefected_RadioButton.Location = new System.Drawing.Point(18, 103);
            this.NotDefected_RadioButton.Name = "NotDefected_RadioButton";
            this.NotDefected_RadioButton.Size = new System.Drawing.Size(81, 17);
            this.NotDefected_RadioButton.TabIndex = 83;
            this.NotDefected_RadioButton.TabStop = true;
            this.NotDefected_RadioButton.Text = "Исправное";
            this.NotDefected_RadioButton.UseVisualStyleBackColor = true;
            // 
            // DeviceForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(909, 253);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Devices_ListBox);
            this.Name = "DeviceForm";
            this.Text = "DeviceForm";
            this.Load += new System.EventHandler(this.DeviceForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox Model_ComboBox;
        private System.Windows.Forms.Button Edit_Button;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button Add_Button;
        private System.Windows.Forms.TextBox SerialNumber_TextBox;
        private System.Windows.Forms.Button Del_Button;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox Devices_ListBox;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.DateTimePicker ManufactureDate_DateTimePicker;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox OrderName_TextBox;
        private System.Windows.Forms.RadioButton IsDefected_RadioButton;
        private System.Windows.Forms.DateTimePicker OrderDate_DateTimePicker;
        private System.Windows.Forms.RadioButton NotDefected_RadioButton;
    }
}