using ElectricalDevicesCW.Managers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ElectricalDevicesCW.Forms
{
    public partial class SupplierForm : Form
    {
        DataBaseService dataBaseService = new DataBaseService();
        int supplierSelectedId = 0;
        public SupplierForm()
        {
            InitializeComponent();            
        }

        private async void Add_Button_Click(object sender, EventArgs e)
        {            
            if (string.IsNullOrWhiteSpace(Name_TextBox.Text) == true) return;

            int result = 0;
            string str = await dataBaseService.AddSupplierAsync(Name_TextBox.Text);
            if (int.TryParse(str, out result) == true)
            {
                RefreshData();
            }
            else MessageBox.Show(str);
        }

        private async void Edit_Button_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(Name_TextBox.Text) == true) return;

            int result = 0;
            string str = await dataBaseService.UpdateSupplierAsync(Name_TextBox.Text, supplierSelectedId);
            if (int.TryParse(str, out result) == true)
            {
                RefreshData();
            }
            else MessageBox.Show(str);
        }

        private async void Delete_Button_Click(object sender, EventArgs e)
        {
            if (Supplier_ListBox.SelectedItem == null) return;

            int result = 0;
            string str = await dataBaseService.DeleteSupplierAsync(Supplier_ListBox.SelectedItem.ToString());
            if (int.TryParse(str, out result) == true)
            {
                RefreshData();
            }
            else MessageBox.Show(str);
        }

        private void Supplier_ListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Supplier_ListBox.SelectedItem == null) return;
            string[] str = Supplier_ListBox.SelectedItem.ToString().Split('.');
            supplierSelectedId = int.Parse(str[0]);
            Name_TextBox.Text = str[1];
        }

        public async void RefreshData()
        {
            int result = 0;
            string str = "";
            str = await dataBaseService.ReadSupplierTableAsync();

            if (int.TryParse(str, out result) == true)
            {
                Supplier_ListBox.Items.Clear();
                ModelDataManager.Instance.GetFullDataListSuppliers().ForEach(s => Supplier_ListBox.Items.Add(s));
                Name_TextBox.Text = "";
            }
            else MessageBox.Show(str);             
        }

        private async void SupplierForm_Load(object sender, EventArgs e)
        {
            int result = 0;
            string str = await dataBaseService.ReadSupplierTableAsync();
            if (int.TryParse(str, out result) == false)
            {
                MessageBox.Show(str);
                return;
            }
            RefreshData();
        }
    }
}
