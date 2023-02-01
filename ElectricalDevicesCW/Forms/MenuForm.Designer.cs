
namespace ElectricalDevicesCW
{
    partial class MenuForm
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
            this.Table_ComboBox = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.StartTask_Button = new System.Windows.Forms.Button();
            this.Cancel_Button = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Table_ComboBox
            // 
            this.Table_ComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Table_ComboBox.FormattingEnabled = true;
            this.Table_ComboBox.Location = new System.Drawing.Point(123, 31);
            this.Table_ComboBox.Name = "Table_ComboBox";
            this.Table_ComboBox.Size = new System.Drawing.Size(235, 21);
            this.Table_ComboBox.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 34);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(97, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Выбрать таблицу:";
            // 
            // StartTask_Button
            // 
            this.StartTask_Button.Location = new System.Drawing.Point(41, 73);
            this.StartTask_Button.Name = "StartTask_Button";
            this.StartTask_Button.Size = new System.Drawing.Size(128, 23);
            this.StartTask_Button.TabIndex = 2;
            this.StartTask_Button.Text = "Продолжить";
            this.StartTask_Button.UseVisualStyleBackColor = true;
            this.StartTask_Button.Click += new System.EventHandler(this.StartTask_Button_Click);
            // 
            // Cancel_Button
            // 
            this.Cancel_Button.Location = new System.Drawing.Point(191, 73);
            this.Cancel_Button.Name = "Cancel_Button";
            this.Cancel_Button.Size = new System.Drawing.Size(128, 23);
            this.Cancel_Button.TabIndex = 3;
            this.Cancel_Button.Text = "Отмена";
            this.Cancel_Button.UseVisualStyleBackColor = true;
            this.Cancel_Button.Click += new System.EventHandler(this.Cancel_Button_Click);
            // 
            // MenuForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(408, 108);
            this.Controls.Add(this.Cancel_Button);
            this.Controls.Add(this.StartTask_Button);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Table_ComboBox);
            this.Name = "MenuForm";
            this.Text = "MenuForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox Table_ComboBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button StartTask_Button;
        private System.Windows.Forms.Button Cancel_Button;
    }
}