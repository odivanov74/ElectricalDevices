
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
            this.DestinationRight_ListBox = new System.Windows.Forms.ListBox();
            this.SourceRight_ListBox = new System.Windows.Forms.ListBox();
            this.Name_TextBox = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.Password_TextBox = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.Login_TextBox = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.Add_Button = new System.Windows.Forms.Button();
            this.Phone_MaskedTextBox = new System.Windows.Forms.MaskedTextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.Discount_NumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.Edit_Button = new System.Windows.Forms.Button();
            this.Users_ListBox = new System.Windows.Forms.ListBox();
            this.Delete_Button = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.Discount_NumericUpDown)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // DestinationRight_ListBox
            // 
            this.DestinationRight_ListBox.FormattingEnabled = true;
            this.DestinationRight_ListBox.Location = new System.Drawing.Point(206, 169);
            this.DestinationRight_ListBox.Margin = new System.Windows.Forms.Padding(2);
            this.DestinationRight_ListBox.Name = "DestinationRight_ListBox";
            this.DestinationRight_ListBox.Size = new System.Drawing.Size(199, 121);
            this.DestinationRight_ListBox.TabIndex = 58;
            this.DestinationRight_ListBox.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.DestinationRight_ListBox_MouseDoubleClick);
            // 
            // SourceRight_ListBox
            // 
            this.SourceRight_ListBox.FormattingEnabled = true;
            this.SourceRight_ListBox.Location = new System.Drawing.Point(7, 169);
            this.SourceRight_ListBox.Margin = new System.Windows.Forms.Padding(2);
            this.SourceRight_ListBox.Name = "SourceRight_ListBox";
            this.SourceRight_ListBox.Size = new System.Drawing.Size(195, 121);
            this.SourceRight_ListBox.TabIndex = 57;
            this.SourceRight_ListBox.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.SourceRight_ListBox_MouseDoubleClick);
            // 
            // Name_TextBox
            // 
            this.Name_TextBox.Location = new System.Drawing.Point(88, 28);
            this.Name_TextBox.Margin = new System.Windows.Forms.Padding(2);
            this.Name_TextBox.Name = "Name_TextBox";
            this.Name_TextBox.Size = new System.Drawing.Size(205, 20);
            this.Name_TextBox.TabIndex = 56;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(5, 79);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(56, 13);
            this.label7.TabIndex = 55;
            this.label7.Text = "Password:";
            // 
            // Password_TextBox
            // 
            this.Password_TextBox.Location = new System.Drawing.Point(88, 76);
            this.Password_TextBox.Margin = new System.Windows.Forms.Padding(2);
            this.Password_TextBox.Name = "Password_TextBox";
            this.Password_TextBox.Size = new System.Drawing.Size(205, 20);
            this.Password_TextBox.TabIndex = 54;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(5, 55);
            this.label8.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(36, 13);
            this.label8.TabIndex = 53;
            this.label8.Text = "Login:";
            // 
            // Login_TextBox
            // 
            this.Login_TextBox.Location = new System.Drawing.Point(88, 52);
            this.Login_TextBox.Margin = new System.Windows.Forms.Padding(2);
            this.Login_TextBox.Name = "Login_TextBox";
            this.Login_TextBox.Size = new System.Drawing.Size(205, 20);
            this.Login_TextBox.TabIndex = 52;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(5, 31);
            this.label9.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(32, 13);
            this.label9.TabIndex = 51;
            this.label9.Text = "Имя:";
            // 
            // Add_Button
            // 
            this.Add_Button.Location = new System.Drawing.Point(7, 295);
            this.Add_Button.Name = "Add_Button";
            this.Add_Button.Size = new System.Drawing.Size(133, 39);
            this.Add_Button.TabIndex = 61;
            this.Add_Button.Text = "Добавить";
            this.Add_Button.UseVisualStyleBackColor = true;
            this.Add_Button.Click += new System.EventHandler(this.AddUser_Button_Click);
            // 
            // Phone_MaskedTextBox
            // 
            this.Phone_MaskedTextBox.Location = new System.Drawing.Point(88, 101);
            this.Phone_MaskedTextBox.Mask = "+0 (000) 000-00-00";
            this.Phone_MaskedTextBox.Name = "Phone_MaskedTextBox";
            this.Phone_MaskedTextBox.Size = new System.Drawing.Size(114, 20);
            this.Phone_MaskedTextBox.TabIndex = 63;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(5, 104);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(38, 13);
            this.label5.TabIndex = 62;
            this.label5.Text = "Phone";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(5, 128);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(49, 13);
            this.label6.TabIndex = 65;
            this.label6.Text = "Discount";
            // 
            // Discount_NumericUpDown
            // 
            this.Discount_NumericUpDown.Location = new System.Drawing.Point(88, 126);
            this.Discount_NumericUpDown.Margin = new System.Windows.Forms.Padding(2);
            this.Discount_NumericUpDown.Name = "Discount_NumericUpDown";
            this.Discount_NumericUpDown.Size = new System.Drawing.Size(77, 20);
            this.Discount_NumericUpDown.TabIndex = 64;
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.groupBox1.Controls.Add(this.Delete_Button);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.Edit_Button);
            this.groupBox1.Controls.Add(this.Password_TextBox);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.Discount_NumericUpDown);
            this.groupBox1.Controls.Add(this.Login_TextBox);
            this.groupBox1.Controls.Add(this.Phone_MaskedTextBox);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.Add_Button);
            this.groupBox1.Controls.Add(this.Name_TextBox);
            this.groupBox1.Controls.Add(this.SourceRight_ListBox);
            this.groupBox1.Controls.Add(this.DestinationRight_ListBox);
            this.groupBox1.Location = new System.Drawing.Point(436, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(412, 342);
            this.groupBox1.TabIndex = 66;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "User";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(5, 154);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(116, 13);
            this.label2.TabIndex = 69;
            this.label2.Text = "Полный список прав:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(203, 154);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(116, 13);
            this.label1.TabIndex = 68;
            this.label1.Text = "Права пользователя:";
            // 
            // Edit_Button
            // 
            this.Edit_Button.Location = new System.Drawing.Point(141, 295);
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
            this.Users_ListBox.Size = new System.Drawing.Size(417, 342);
            this.Users_ListBox.TabIndex = 67;
            this.Users_ListBox.SelectedIndexChanged += new System.EventHandler(this.Users_ListBox_SelectedIndexChanged);
            // 
            // Delete_Button
            // 
            this.Delete_Button.Location = new System.Drawing.Point(275, 295);
            this.Delete_Button.Name = "Delete_Button";
            this.Delete_Button.Size = new System.Drawing.Size(133, 39);
            this.Delete_Button.TabIndex = 67;
            this.Delete_Button.Text = "Удалить";
            this.Delete_Button.UseVisualStyleBackColor = true;
            this.Delete_Button.Click += new System.EventHandler(this.DeleteUser_Button_Click);
            // 
            // UsersForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(854, 362);
            this.Controls.Add(this.Users_ListBox);
            this.Controls.Add(this.groupBox1);
            this.Name = "UsersForm";
            this.Text = "UsersForm";
            ((System.ComponentModel.ISupportInitialize)(this.Discount_NumericUpDown)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ListBox DestinationRight_ListBox;
        private System.Windows.Forms.ListBox SourceRight_ListBox;
        private System.Windows.Forms.TextBox Name_TextBox;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox Password_TextBox;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox Login_TextBox;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button Add_Button;
        private System.Windows.Forms.MaskedTextBox Phone_MaskedTextBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown Discount_NumericUpDown;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ListBox Users_ListBox;
        private System.Windows.Forms.Button Edit_Button;
        private System.Windows.Forms.Button Delete_Button;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
    }
}