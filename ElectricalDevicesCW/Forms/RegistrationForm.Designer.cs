
namespace ElectricalDevicesCW.Forms
{
    partial class RegistrationForm
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
            this.PasswordNewUser_TextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.LoginNewUser_TextBox = new System.Windows.Forms.TextBox();
            this.Phone_MaskedTextBox = new System.Windows.Forms.MaskedTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.NameNewUser_TextBox = new System.Windows.Forms.TextBox();
            this.Registration_Button = new System.Windows.Forms.Button();
            this.Cancel_Button = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // PasswordNewUser_TextBox
            // 
            this.PasswordNewUser_TextBox.Location = new System.Drawing.Point(97, 59);
            this.PasswordNewUser_TextBox.Margin = new System.Windows.Forms.Padding(2);
            this.PasswordNewUser_TextBox.Name = "PasswordNewUser_TextBox";
            this.PasswordNewUser_TextBox.Size = new System.Drawing.Size(205, 20);
            this.PasswordNewUser_TextBox.TabIndex = 71;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 14);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(32, 13);
            this.label1.TabIndex = 68;
            this.label1.Text = "Имя:";
            // 
            // LoginNewUser_TextBox
            // 
            this.LoginNewUser_TextBox.Location = new System.Drawing.Point(97, 35);
            this.LoginNewUser_TextBox.Margin = new System.Windows.Forms.Padding(2);
            this.LoginNewUser_TextBox.Name = "LoginNewUser_TextBox";
            this.LoginNewUser_TextBox.Size = new System.Drawing.Size(205, 20);
            this.LoginNewUser_TextBox.TabIndex = 69;
            // 
            // Phone_MaskedTextBox
            // 
            this.Phone_MaskedTextBox.Location = new System.Drawing.Point(97, 84);
            this.Phone_MaskedTextBox.Mask = "+0 (000) 000-00-00";
            this.Phone_MaskedTextBox.Name = "Phone_MaskedTextBox";
            this.Phone_MaskedTextBox.Size = new System.Drawing.Size(114, 20);
            this.Phone_MaskedTextBox.TabIndex = 75;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(14, 38);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(36, 13);
            this.label2.TabIndex = 70;
            this.label2.Text = "Login:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(14, 87);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(38, 13);
            this.label3.TabIndex = 74;
            this.label3.Text = "Phone";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(14, 62);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(56, 13);
            this.label4.TabIndex = 72;
            this.label4.Text = "Password:";
            // 
            // NameNewUser_TextBox
            // 
            this.NameNewUser_TextBox.Location = new System.Drawing.Point(97, 11);
            this.NameNewUser_TextBox.Margin = new System.Windows.Forms.Padding(2);
            this.NameNewUser_TextBox.Name = "NameNewUser_TextBox";
            this.NameNewUser_TextBox.Size = new System.Drawing.Size(205, 20);
            this.NameNewUser_TextBox.TabIndex = 73;
            // 
            // Registration_Button
            // 
            this.Registration_Button.Location = new System.Drawing.Point(17, 123);
            this.Registration_Button.Name = "Registration_Button";
            this.Registration_Button.Size = new System.Drawing.Size(133, 23);
            this.Registration_Button.TabIndex = 76;
            this.Registration_Button.Text = "Зарегистрироваться";
            this.Registration_Button.UseVisualStyleBackColor = true;
            this.Registration_Button.Click += new System.EventHandler(this.Registration_Button_Click);
            // 
            // Cancel_Button
            // 
            this.Cancel_Button.Location = new System.Drawing.Point(169, 123);
            this.Cancel_Button.Name = "Cancel_Button";
            this.Cancel_Button.Size = new System.Drawing.Size(133, 23);
            this.Cancel_Button.TabIndex = 77;
            this.Cancel_Button.Text = "Отмена";
            this.Cancel_Button.UseVisualStyleBackColor = true;
            this.Cancel_Button.Click += new System.EventHandler(this.Cancel_Button_Click);
            // 
            // RegistrationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(323, 158);
            this.Controls.Add(this.Cancel_Button);
            this.Controls.Add(this.Registration_Button);
            this.Controls.Add(this.PasswordNewUser_TextBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.LoginNewUser_TextBox);
            this.Controls.Add(this.Phone_MaskedTextBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.NameNewUser_TextBox);
            this.Name = "RegistrationForm";
            this.Text = "RegistrationForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox PasswordNewUser_TextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox LoginNewUser_TextBox;
        private System.Windows.Forms.MaskedTextBox Phone_MaskedTextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox NameNewUser_TextBox;
        private System.Windows.Forms.Button Registration_Button;
        private System.Windows.Forms.Button Cancel_Button;
    }
}