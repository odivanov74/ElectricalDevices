
namespace ElectricalDevicesCW.Forms
{
    partial class ModelForm
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
            this.Models_ListBox = new System.Windows.Forms.ListBox();
            this.Del_Button = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.Reserved_NumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.Supplier_ComboBox = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.Manufacturer_ComboBox = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.Type_ComboBox = new System.Windows.Forms.ComboBox();
            this.Price_NumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.Weight_NumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.Edit_Button = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.StockBalance_NumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.label8 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.Add_Button = new System.Windows.Forms.Button();
            this.Name_TextBox = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.TypeSort_ComboBox = new System.Windows.Forms.ComboBox();
            this.Direction_ComboBox = new System.Windows.Forms.ComboBox();
            this.Sort_Button = new System.Windows.Forms.Button();
            this.Saled_NumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.label11 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Reserved_NumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Price_NumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Weight_NumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.StockBalance_NumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Saled_NumericUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 47);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(94, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Список моделей:";
            // 
            // Models_ListBox
            // 
            this.Models_ListBox.FormattingEnabled = true;
            this.Models_ListBox.Location = new System.Drawing.Point(12, 63);
            this.Models_ListBox.Name = "Models_ListBox";
            this.Models_ListBox.Size = new System.Drawing.Size(483, 225);
            this.Models_ListBox.TabIndex = 6;
            this.Models_ListBox.SelectedIndexChanged += new System.EventHandler(this.Models_ListBox_SelectedIndexChanged);
            // 
            // Del_Button
            // 
            this.Del_Button.Location = new System.Drawing.Point(272, 215);
            this.Del_Button.Name = "Del_Button";
            this.Del_Button.Size = new System.Drawing.Size(133, 39);
            this.Del_Button.TabIndex = 14;
            this.Del_Button.Text = "Удалить";
            this.Del_Button.UseVisualStyleBackColor = true;
            this.Del_Button.Click += new System.EventHandler(this.DelModel_Button_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.groupBox1.Controls.Add(this.Saled_NumericUpDown);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.Reserved_NumericUpDown);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.Supplier_ComboBox);
            this.groupBox1.Controls.Add(this.Del_Button);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.Manufacturer_ComboBox);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.Type_ComboBox);
            this.groupBox1.Controls.Add(this.Price_NumericUpDown);
            this.groupBox1.Controls.Add(this.Weight_NumericUpDown);
            this.groupBox1.Controls.Add(this.Edit_Button);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.StockBalance_NumericUpDown);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.Add_Button);
            this.groupBox1.Controls.Add(this.Name_TextBox);
            this.groupBox1.Location = new System.Drawing.Point(501, 24);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(412, 266);
            this.groupBox1.TabIndex = 67;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Model";
            // 
            // Reserved_NumericUpDown
            // 
            this.Reserved_NumericUpDown.Increment = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.Reserved_NumericUpDown.Location = new System.Drawing.Point(213, 129);
            this.Reserved_NumericUpDown.Margin = new System.Windows.Forms.Padding(2);
            this.Reserved_NumericUpDown.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.Reserved_NumericUpDown.Name = "Reserved_NumericUpDown";
            this.Reserved_NumericUpDown.Size = new System.Drawing.Size(61, 20);
            this.Reserved_NumericUpDown.TabIndex = 78;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(162, 131);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(47, 13);
            this.label4.TabIndex = 77;
            this.label4.Text = "Резерв:";
            // 
            // Supplier_ComboBox
            // 
            this.Supplier_ComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Supplier_ComboBox.FormattingEnabled = true;
            this.Supplier_ComboBox.Location = new System.Drawing.Point(99, 188);
            this.Supplier_ComboBox.Name = "Supplier_ComboBox";
            this.Supplier_ComboBox.Size = new System.Drawing.Size(154, 21);
            this.Supplier_ComboBox.TabIndex = 76;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(5, 191);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(68, 13);
            this.label3.TabIndex = 75;
            this.label3.Text = "Поставщик:";
            // 
            // Manufacturer_ComboBox
            // 
            this.Manufacturer_ComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Manufacturer_ComboBox.FormattingEnabled = true;
            this.Manufacturer_ComboBox.Location = new System.Drawing.Point(99, 161);
            this.Manufacturer_ComboBox.Name = "Manufacturer_ComboBox";
            this.Manufacturer_ComboBox.Size = new System.Drawing.Size(154, 21);
            this.Manufacturer_ComboBox.TabIndex = 74;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(5, 164);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(89, 13);
            this.label2.TabIndex = 73;
            this.label2.Text = "Производитель:";
            // 
            // Type_ComboBox
            // 
            this.Type_ComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Type_ComboBox.FormattingEnabled = true;
            this.Type_ComboBox.Location = new System.Drawing.Point(88, 54);
            this.Type_ComboBox.Name = "Type_ComboBox";
            this.Type_ComboBox.Size = new System.Drawing.Size(205, 21);
            this.Type_ComboBox.TabIndex = 72;
            // 
            // Price_NumericUpDown
            // 
            this.Price_NumericUpDown.Location = new System.Drawing.Point(88, 104);
            this.Price_NumericUpDown.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.Price_NumericUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.Price_NumericUpDown.Name = "Price_NumericUpDown";
            this.Price_NumericUpDown.Size = new System.Drawing.Size(120, 20);
            this.Price_NumericUpDown.TabIndex = 71;
            this.Price_NumericUpDown.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // Weight_NumericUpDown
            // 
            this.Weight_NumericUpDown.Location = new System.Drawing.Point(88, 80);
            this.Weight_NumericUpDown.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.Weight_NumericUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.Weight_NumericUpDown.Name = "Weight_NumericUpDown";
            this.Weight_NumericUpDown.Size = new System.Drawing.Size(120, 20);
            this.Weight_NumericUpDown.TabIndex = 70;
            this.Weight_NumericUpDown.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // Edit_Button
            // 
            this.Edit_Button.Location = new System.Drawing.Point(139, 215);
            this.Edit_Button.Name = "Edit_Button";
            this.Edit_Button.Size = new System.Drawing.Size(133, 39);
            this.Edit_Button.TabIndex = 66;
            this.Edit_Button.Text = "Изменить";
            this.Edit_Button.UseVisualStyleBackColor = true;
            this.Edit_Button.Click += new System.EventHandler(this.EditModel_Button_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(5, 131);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(63, 13);
            this.label6.TabIndex = 65;
            this.label6.Text = "На складе:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(5, 31);
            this.label9.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(60, 13);
            this.label9.TabIndex = 51;
            this.label9.Text = "Название:";
            // 
            // StockBalance_NumericUpDown
            // 
            this.StockBalance_NumericUpDown.Increment = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.StockBalance_NumericUpDown.Location = new System.Drawing.Point(88, 129);
            this.StockBalance_NumericUpDown.Margin = new System.Windows.Forms.Padding(2);
            this.StockBalance_NumericUpDown.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.StockBalance_NumericUpDown.Name = "StockBalance_NumericUpDown";
            this.StockBalance_NumericUpDown.Size = new System.Drawing.Size(61, 20);
            this.StockBalance_NumericUpDown.TabIndex = 64;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(5, 55);
            this.label8.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(29, 13);
            this.label8.TabIndex = 53;
            this.label8.Text = "Тип:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(5, 107);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(36, 13);
            this.label5.TabIndex = 62;
            this.label5.Text = "Цена:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(5, 82);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(29, 13);
            this.label7.TabIndex = 55;
            this.label7.Text = "Вес:";
            // 
            // Add_Button
            // 
            this.Add_Button.Location = new System.Drawing.Point(6, 215);
            this.Add_Button.Name = "Add_Button";
            this.Add_Button.Size = new System.Drawing.Size(133, 39);
            this.Add_Button.TabIndex = 61;
            this.Add_Button.Text = "Добавить";
            this.Add_Button.UseVisualStyleBackColor = true;
            this.Add_Button.Click += new System.EventHandler(this.Add_Button_Click);
            // 
            // Name_TextBox
            // 
            this.Name_TextBox.Location = new System.Drawing.Point(88, 28);
            this.Name_TextBox.Margin = new System.Windows.Forms.Padding(2);
            this.Name_TextBox.Name = "Name_TextBox";
            this.Name_TextBox.Size = new System.Drawing.Size(205, 20);
            this.Name_TextBox.TabIndex = 56;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(12, 7);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(70, 13);
            this.label10.TabIndex = 69;
            this.label10.Text = "Сортировка:";
            // 
            // TypeSort_ComboBox
            // 
            this.TypeSort_ComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.TypeSort_ComboBox.FormattingEnabled = true;
            this.TypeSort_ComboBox.Items.AddRange(new object[] {
            "Без сортировки",
            "По поставщику",
            "По производителю",
            "По стране производства",
            "По весу",
            "По наличию",
            "По резервам"});
            this.TypeSort_ComboBox.Location = new System.Drawing.Point(12, 23);
            this.TypeSort_ComboBox.Name = "TypeSort_ComboBox";
            this.TypeSort_ComboBox.Size = new System.Drawing.Size(209, 21);
            this.TypeSort_ComboBox.TabIndex = 68;
            // 
            // Direction_ComboBox
            // 
            this.Direction_ComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Direction_ComboBox.FormattingEnabled = true;
            this.Direction_ComboBox.Items.AddRange(new object[] {
            "По возрастанию",
            "По убыванию"});
            this.Direction_ComboBox.Location = new System.Drawing.Point(227, 23);
            this.Direction_ComboBox.Name = "Direction_ComboBox";
            this.Direction_ComboBox.Size = new System.Drawing.Size(129, 21);
            this.Direction_ComboBox.TabIndex = 70;
            // 
            // Sort_Button
            // 
            this.Sort_Button.Location = new System.Drawing.Point(362, 23);
            this.Sort_Button.Name = "Sort_Button";
            this.Sort_Button.Size = new System.Drawing.Size(133, 21);
            this.Sort_Button.TabIndex = 79;
            this.Sort_Button.Text = "Сортировать";
            this.Sort_Button.UseVisualStyleBackColor = true;
            this.Sort_Button.Click += new System.EventHandler(this.Sort_Button_Click);
            // 
            // Saled_NumericUpDown
            // 
            this.Saled_NumericUpDown.Increment = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.Saled_NumericUpDown.Location = new System.Drawing.Point(344, 129);
            this.Saled_NumericUpDown.Margin = new System.Windows.Forms.Padding(2);
            this.Saled_NumericUpDown.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.Saled_NumericUpDown.Name = "Saled_NumericUpDown";
            this.Saled_NumericUpDown.Size = new System.Drawing.Size(61, 20);
            this.Saled_NumericUpDown.TabIndex = 80;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(286, 131);
            this.label11.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(54, 13);
            this.label11.TabIndex = 79;
            this.label11.Text = "Продано:";
            // 
            // ModelForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(926, 302);
            this.Controls.Add(this.Sort_Button);
            this.Controls.Add(this.Direction_ComboBox);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.TypeSort_ComboBox);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Models_ListBox);
            this.Name = "ModelForm";
            this.Text = "ModelForm";
            this.Load += new System.EventHandler(this.ModelForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Reserved_NumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Price_NumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Weight_NumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.StockBalance_NumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Saled_NumericUpDown)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox Models_ListBox;
        private System.Windows.Forms.Button Del_Button;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button Edit_Button;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.NumericUpDown StockBalance_NumericUpDown;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button Add_Button;
        private System.Windows.Forms.TextBox Name_TextBox;
        private System.Windows.Forms.NumericUpDown Weight_NumericUpDown;
        private System.Windows.Forms.ComboBox Supplier_ComboBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox Manufacturer_ComboBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox Type_ComboBox;
        private System.Windows.Forms.NumericUpDown Price_NumericUpDown;
        private System.Windows.Forms.NumericUpDown Reserved_NumericUpDown;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox TypeSort_ComboBox;
        private System.Windows.Forms.ComboBox Direction_ComboBox;
        private System.Windows.Forms.Button Sort_Button;
        private System.Windows.Forms.NumericUpDown Saled_NumericUpDown;
        private System.Windows.Forms.Label label11;
    }
}