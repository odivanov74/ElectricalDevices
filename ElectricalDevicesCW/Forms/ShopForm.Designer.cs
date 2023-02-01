
namespace ElectricalDevicesCW
{
    partial class ShopForm
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
            this.NumDevices_NumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.InBasket_button = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.ProductModels_ListBox = new System.Windows.Forms.ListBox();
            this.UserName_Label = new System.Windows.Forms.Label();
            this.DeviceInfo_Label = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.NumDevices_NumericUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // NumDevices_NumericUpDown
            // 
            this.NumDevices_NumericUpDown.Location = new System.Drawing.Point(12, 362);
            this.NumDevices_NumericUpDown.Name = "NumDevices_NumericUpDown";
            this.NumDevices_NumericUpDown.Size = new System.Drawing.Size(207, 20);
            this.NumDevices_NumericUpDown.TabIndex = 7;
            // 
            // InBasket_button
            // 
            this.InBasket_button.Location = new System.Drawing.Point(12, 388);
            this.InBasket_button.Name = "InBasket_button";
            this.InBasket_button.Size = new System.Drawing.Size(207, 36);
            this.InBasket_button.TabIndex = 6;
            this.InBasket_button.Text = "В корзину";
            this.InBasket_button.UseVisualStyleBackColor = true;
            this.InBasket_button.Click += new System.EventHandler(this.InBasket_button_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(91, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Список товаров:";
            // 
            // ProductModels_ListBox
            // 
            this.ProductModels_ListBox.FormattingEnabled = true;
            this.ProductModels_ListBox.Location = new System.Drawing.Point(12, 40);
            this.ProductModels_ListBox.Name = "ProductModels_ListBox";
            this.ProductModels_ListBox.Size = new System.Drawing.Size(597, 316);
            this.ProductModels_ListBox.TabIndex = 4;
            this.ProductModels_ListBox.SelectedIndexChanged += new System.EventHandler(this.ProductModels_ListBox_SelectedIndexChanged);
            // 
            // UserName_Label
            // 
            this.UserName_Label.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.UserName_Label.Location = new System.Drawing.Point(576, 24);
            this.UserName_Label.Name = "UserName_Label";
            this.UserName_Label.Size = new System.Drawing.Size(284, 13);
            this.UserName_Label.TabIndex = 8;
            this.UserName_Label.Text = "User";
            this.UserName_Label.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // DeviceInfo_Label
            // 
            this.DeviceInfo_Label.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.DeviceInfo_Label.Location = new System.Drawing.Point(616, 41);
            this.DeviceInfo_Label.Name = "DeviceInfo_Label";
            this.DeviceInfo_Label.Size = new System.Drawing.Size(248, 315);
            this.DeviceInfo_Label.TabIndex = 9;
            // 
            // ShopForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(876, 431);
            this.Controls.Add(this.DeviceInfo_Label);
            this.Controls.Add(this.UserName_Label);
            this.Controls.Add(this.NumDevices_NumericUpDown);
            this.Controls.Add(this.InBasket_button);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ProductModels_ListBox);
            this.Name = "ShopForm";
            this.Text = "ShopForm";
            ((System.ComponentModel.ISupportInitialize)(this.NumDevices_NumericUpDown)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NumericUpDown NumDevices_NumericUpDown;
        private System.Windows.Forms.Button InBasket_button;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox ProductModels_ListBox;
        private System.Windows.Forms.Label UserName_Label;
        private System.Windows.Forms.Label DeviceInfo_Label;
    }
}