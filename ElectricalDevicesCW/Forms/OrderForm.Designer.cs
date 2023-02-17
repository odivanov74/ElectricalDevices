
namespace ElectricalDevicesCW.Forms
{
    partial class OrderForm
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
            this.Price_Label = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.Del_Button = new System.Windows.Forms.Button();
            this.Order_DateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.Edit_Button = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.Add_Button = new System.Windows.Forms.Button();
            this.Name_TextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.Orders_ListBox = new System.Windows.Forms.ListBox();
            this.label4 = new System.Windows.Forms.Label();
            this.Model_ListBox = new System.Windows.Forms.ListBox();
            this.Client_TextBox = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.groupBox1.Controls.Add(this.Client_TextBox);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.Model_ListBox);
            this.groupBox1.Controls.Add(this.Price_Label);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.Del_Button);
            this.groupBox1.Controls.Add(this.Order_DateTimePicker);
            this.groupBox1.Controls.Add(this.Edit_Button);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.Add_Button);
            this.groupBox1.Controls.Add(this.Name_TextBox);
            this.groupBox1.Location = new System.Drawing.Point(501, 22);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(412, 290);
            this.groupBox1.TabIndex = 74;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Заказ";
            // 
            // Price_Label
            // 
            this.Price_Label.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Price_Label.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Price_Label.Location = new System.Drawing.Point(279, 218);
            this.Price_Label.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.Price_Label.Name = "Price_Label";
            this.Price_Label.Size = new System.Drawing.Size(127, 24);
            this.Price_Label.TabIndex = 89;
            this.Price_Label.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(231, 223);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(44, 13);
            this.label3.TabIndex = 88;
            this.label3.Text = "Сумма:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 76);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(46, 13);
            this.label2.TabIndex = 87;
            this.label2.Text = "Клиент:";
            // 
            // Del_Button
            // 
            this.Del_Button.Location = new System.Drawing.Point(274, 245);
            this.Del_Button.Name = "Del_Button";
            this.Del_Button.Size = new System.Drawing.Size(133, 39);
            this.Del_Button.TabIndex = 70;
            this.Del_Button.Text = "Удалить";
            this.Del_Button.UseVisualStyleBackColor = true;
            this.Del_Button.Click += new System.EventHandler(this.Del_Button_Click);
            // 
            // Order_DateTimePicker
            // 
            this.Order_DateTimePicker.Location = new System.Drawing.Point(96, 45);
            this.Order_DateTimePicker.Name = "Order_DateTimePicker";
            this.Order_DateTimePicker.Size = new System.Drawing.Size(172, 20);
            this.Order_DateTimePicker.TabIndex = 77;
            // 
            // Edit_Button
            // 
            this.Edit_Button.Location = new System.Drawing.Point(139, 245);
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
            this.label9.Location = new System.Drawing.Point(5, 23);
            this.label9.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(32, 13);
            this.label9.TabIndex = 51;
            this.label9.Text = "Имя:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(5, 49);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(75, 13);
            this.label7.TabIndex = 55;
            this.label7.Text = "Дата заказа:";
            // 
            // Add_Button
            // 
            this.Add_Button.Location = new System.Drawing.Point(4, 245);
            this.Add_Button.Name = "Add_Button";
            this.Add_Button.Size = new System.Drawing.Size(133, 39);
            this.Add_Button.TabIndex = 61;
            this.Add_Button.Text = "Добавить";
            this.Add_Button.UseVisualStyleBackColor = true;
            this.Add_Button.Click += new System.EventHandler(this.Add_Button_Click);
            // 
            // Name_TextBox
            // 
            this.Name_TextBox.Location = new System.Drawing.Point(96, 20);
            this.Name_TextBox.Margin = new System.Windows.Forms.Padding(2);
            this.Name_TextBox.Name = "Name_TextBox";
            this.Name_TextBox.Size = new System.Drawing.Size(172, 20);
            this.Name_TextBox.TabIndex = 56;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(92, 13);
            this.label1.TabIndex = 73;
            this.label1.Text = "Список заказов:";
            // 
            // Orders_ListBox
            // 
            this.Orders_ListBox.FormattingEnabled = true;
            this.Orders_ListBox.Location = new System.Drawing.Point(12, 22);
            this.Orders_ListBox.Name = "Orders_ListBox";
            this.Orders_ListBox.Size = new System.Drawing.Size(483, 290);
            this.Orders_ListBox.TabIndex = 72;
            this.Orders_ListBox.SelectedIndexChanged += new System.EventHandler(this.Orders_ListBox_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 100);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(70, 13);
            this.label4.TabIndex = 91;
            this.label4.Text = "Устройства:";
            // 
            // Model_ListBox
            // 
            this.Model_ListBox.FormattingEnabled = true;
            this.Model_ListBox.Location = new System.Drawing.Point(9, 116);
            this.Model_ListBox.Name = "Model_ListBox";
            this.Model_ListBox.Size = new System.Drawing.Size(398, 95);
            this.Model_ListBox.TabIndex = 90;
            // 
            // Client_TextBox
            // 
            this.Client_TextBox.Location = new System.Drawing.Point(96, 73);
            this.Client_TextBox.Margin = new System.Windows.Forms.Padding(2);
            this.Client_TextBox.Name = "Client_TextBox";
            this.Client_TextBox.Size = new System.Drawing.Size(310, 20);
            this.Client_TextBox.TabIndex = 92;
            // 
            // OrderForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(920, 319);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Orders_ListBox);
            this.Name = "OrderForm";
            this.Text = "OrderForm";
            this.Load += new System.EventHandler(this.OrderForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button Del_Button;
        private System.Windows.Forms.DateTimePicker Order_DateTimePicker;
        private System.Windows.Forms.Button Edit_Button;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button Add_Button;
        private System.Windows.Forms.TextBox Name_TextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox Orders_ListBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label Price_Label;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox Client_TextBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ListBox Model_ListBox;
    }
}