
namespace ElectricalDevicesCW
{
    partial class UsersForm
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
            this.Password_TextBox = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.Login_TextBox = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.Add_Button = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.Delete_Button = new System.Windows.Forms.Button();
            this.Edit_Button = new System.Windows.Forms.Button();
            this.Users_ListBox = new System.Windows.Forms.ListBox();
            this.Role_ComboBox = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.Name_TextBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.Discount_NumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.Phone_MaskedTextBox = new System.Windows.Forms.MaskedTextBox();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Discount_NumericUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // Password_TextBox
            // 
            this.Password_TextBox.Location = new System.Drawing.Point(144, 55);
            this.Password_TextBox.Margin = new System.Windows.Forms.Padding(2);
            this.Password_TextBox.Name = "Password_TextBox";
            this.Password_TextBox.Size = new System.Drawing.Size(205, 20);
            this.Password_TextBox.TabIndex = 56;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(61, 58);
            this.label8.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(56, 13);
            this.label8.TabIndex = 53;
            this.label8.Text = "Password:";
            // 
            // Login_TextBox
            // 
            this.Login_TextBox.Location = new System.Drawing.Point(144, 31);
            this.Login_TextBox.Margin = new System.Windows.Forms.Padding(2);
            this.Login_TextBox.Name = "Login_TextBox";
            this.Login_TextBox.Size = new System.Drawing.Size(205, 20);
            this.Login_TextBox.TabIndex = 52;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(61, 34);
            this.label9.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(36, 13);
            this.label9.TabIndex = 51;
            this.label9.Text = "Login:";
            // 
            // Add_Button
            // 
            this.Add_Button.Location = new System.Drawing.Point(5, 284);
            this.Add_Button.Name = "Add_Button";
            this.Add_Button.Size = new System.Drawing.Size(133, 39);
            this.Add_Button.TabIndex = 61;
            this.Add_Button.Text = "Добавить";
            this.Add_Button.UseVisualStyleBackColor = true;
            this.Add_Button.Click += new System.EventHandler(this.AddUser_Button_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.groupBox1.Controls.Add(this.Phone_MaskedTextBox);
            this.groupBox1.Controls.Add(this.Role_ComboBox);
            this.groupBox1.Controls.Add(this.Discount_NumericUpDown);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.Delete_Button);
            this.groupBox1.Controls.Add(this.Edit_Button);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.Name_TextBox);
            this.groupBox1.Controls.Add(this.Login_TextBox);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.Add_Button);
            this.groupBox1.Controls.Add(this.Password_TextBox);
            this.groupBox1.Location = new System.Drawing.Point(436, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(412, 329);
            this.groupBox1.TabIndex = 66;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "User-Client";
            // 
            // Delete_Button
            // 
            this.Delete_Button.Location = new System.Drawing.Point(273, 284);
            this.Delete_Button.Name = "Delete_Button";
            this.Delete_Button.Size = new System.Drawing.Size(133, 39);
            this.Delete_Button.TabIndex = 67;
            this.Delete_Button.Text = "Удалить";
            this.Delete_Button.UseVisualStyleBackColor = true;
            this.Delete_Button.Click += new System.EventHandler(this.DeleteUser_Button_Click);
            // 
            // Edit_Button
            // 
            this.Edit_Button.Location = new System.Drawing.Point(139, 284);
            this.Edit_Button.Name = "Edit_Button";
            this.Edit_Button.Size = new System.Drawing.Size(133, 39);
            this.Edit_Button.TabIndex = 66;
            this.Edit_Button.Text = "Изменить";
            this.Edit_Button.UseVisualStyleBackColor = true;
            this.Edit_Button.Click += new System.EventHandler(this.EditUser_Button_Click);
            // 
            // Users_ListBox
            // 
            this.Users_ListBox.FormattingEnabled = true;
            this.Users_ListBox.Location = new System.Drawing.Point(13, 12);
            this.Users_ListBox.Name = "Users_ListBox";
            this.Users_ListBox.Size = new System.Drawing.Size(417, 329);
            this.Users_ListBox.TabIndex = 67;
            this.Users_ListBox.SelectedIndexChanged += new System.EventHandler(this.Users_ListBox_SelectedIndexChanged);
            // 
            // Role_ComboBox
            // 
            this.Role_ComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Role_ComboBox.FormattingEnabled = true;
            this.Role_ComboBox.Location = new System.Drawing.Point(144, 80);
            this.Role_ComboBox.Name = "Role_ComboBox";
            this.Role_ComboBox.Size = new System.Drawing.Size(205, 21);
            this.Role_ComboBox.TabIndex = 74;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(61, 81);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 73;
            this.label1.Text = "Роль:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(61, 177);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(123, 13);
            this.label2.TabIndex = 73;
            this.label2.Text = "Персональная скидка:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(61, 128);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(76, 13);
            this.label3.TabIndex = 51;
            this.label3.Text = "Имя клиента:";
            // 
            // Name_TextBox
            // 
            this.Name_TextBox.Location = new System.Drawing.Point(144, 125);
            this.Name_TextBox.Margin = new System.Windows.Forms.Padding(2);
            this.Name_TextBox.Name = "Name_TextBox";
            this.Name_TextBox.Size = new System.Drawing.Size(205, 20);
            this.Name_TextBox.TabIndex = 52;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(61, 152);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(55, 13);
            this.label4.TabIndex = 53;
            this.label4.Text = "Телефон:";
            // 
            // Discount_NumericUpDown
            // 
            this.Discount_NumericUpDown.Location = new System.Drawing.Point(229, 175);
            this.Discount_NumericUpDown.Maximum = new decimal(new int[] {
            15,
            0,
            0,
            0});
            this.Discount_NumericUpDown.Name = "Discount_NumericUpDown";
            this.Discount_NumericUpDown.Size = new System.Drawing.Size(120, 20);
            this.Discount_NumericUpDown.TabIndex = 74;
            // 
            // Phone_MaskedTextBox
            // 
            this.Phone_MaskedTextBox.Location = new System.Drawing.Point(229, 149);
            this.Phone_MaskedTextBox.Mask = "+0(000)000-00-00";
            this.Phone_MaskedTextBox.Name = "Phone_MaskedTextBox";
            this.Phone_MaskedTextBox.Size = new System.Drawing.Size(120, 20);
            this.Phone_MaskedTextBox.TabIndex = 75;
            // 
            // UsersForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(854, 348);
            this.Controls.Add(this.Users_ListBox);
            this.Controls.Add(this.groupBox1);
            this.Name = "UsersForm";
            this.Text = "UsersForm";
            this.Load += new System.EventHandler(this.UsersForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Discount_NumericUpDown)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TextBox Password_TextBox;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox Login_TextBox;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button Add_Button;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ListBox Users_ListBox;
        private System.Windows.Forms.Button Edit_Button;
        private System.Windows.Forms.Button Delete_Button;
        private System.Windows.Forms.ComboBox Role_ComboBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.MaskedTextBox Phone_MaskedTextBox;
        private System.Windows.Forms.NumericUpDown Discount_NumericUpDown;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox Name_TextBox;
        private System.Windows.Forms.Label label4;
    }
}