
namespace ElectricalDevicesCW.Forms
{
    partial class SupplierForm
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
            this.Edit_Button = new System.Windows.Forms.Button();
            this.Delete_Button = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.Add_Button = new System.Windows.Forms.Button();
            this.Name_TextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.Supplier_ListBox = new System.Windows.Forms.ListBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.groupBox1.Controls.Add(this.Edit_Button);
            this.groupBox1.Controls.Add(this.Delete_Button);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.Add_Button);
            this.groupBox1.Controls.Add(this.Name_TextBox);
            this.groupBox1.Location = new System.Drawing.Point(234, 24);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(412, 225);
            this.groupBox1.TabIndex = 82;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Поставщик";
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
            // Name_TextBox
            // 
            this.Name_TextBox.Location = new System.Drawing.Point(69, 18);
            this.Name_TextBox.Margin = new System.Windows.Forms.Padding(2);
            this.Name_TextBox.Name = "Name_TextBox";
            this.Name_TextBox.Size = new System.Drawing.Size(203, 20);
            this.Name_TextBox.TabIndex = 56;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(5, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(118, 13);
            this.label1.TabIndex = 81;
            this.label1.Text = "Список поставщиков:";
            // 
            // Supplier_ListBox
            // 
            this.Supplier_ListBox.FormattingEnabled = true;
            this.Supplier_ListBox.Location = new System.Drawing.Point(8, 24);
            this.Supplier_ListBox.Name = "Supplier_ListBox";
            this.Supplier_ListBox.Size = new System.Drawing.Size(220, 225);
            this.Supplier_ListBox.TabIndex = 80;
            this.Supplier_ListBox.SelectedIndexChanged += new System.EventHandler(this.Supplier_ListBox_SelectedIndexChanged);
            // 
            // SupplierForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(655, 255);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Supplier_ListBox);
            this.Name = "SupplierForm";
            this.Text = "SupplierForm";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button Edit_Button;
        private System.Windows.Forms.Button Delete_Button;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button Add_Button;
        private System.Windows.Forms.TextBox Name_TextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox Supplier_ListBox;
    }
}