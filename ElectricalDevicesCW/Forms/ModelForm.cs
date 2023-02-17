using ElectricalDevicesCW.Managers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ElectricalDevicesCW.Forms
{
    public partial class ModelForm : Form
    { 
        DataBaseService dataBaseService = new DataBaseService();
        int ModelSelectedId = 0;

        public ModelForm()
        {
            InitializeComponent();
            StockBalance_NumericUpDown.Enabled = false;
            Reserved_NumericUpDown.Enabled = false;
        }

        private async void Add_Button_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(Name_TextBox.Text) == true)
            {
                MessageBox.Show("Некорректные данные");
                return;
            }           
            
            int result = 0;
            string str = await dataBaseService.AddModelAsync(Name_TextBox.Text,
                                                            Type_ComboBox.SelectedIndex + 1,
                                                            (int)Weight_NumericUpDown.Value,
                                                            (int)Price_NumericUpDown.Value,                                                           
                                                            Manufacturer_ComboBox.SelectedIndex + 1,
                                                            Supplier_ComboBox.SelectedIndex + 1);
            if (int.TryParse(str, out result) == true)
            {
                RefreshData();
            }
            else MessageBox.Show(str);
        }

        private async void EditModel_Button_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(Name_TextBox.Text) == true)
            {
                MessageBox.Show("Некорректные данные");
                return;
            }

            int result = 0;
            string str = await dataBaseService.UpdateModelAsync(Name_TextBox.Text,
                                                            Type_ComboBox.SelectedIndex + 1,
                                                            (int)Weight_NumericUpDown.Value,
                                                            (int)Price_NumericUpDown.Value,
                                                            (int)StockBalance_NumericUpDown.Value,
                                                            Manufacturer_ComboBox.SelectedIndex + 1,
                                                            Supplier_ComboBox.SelectedIndex + 1,
                                                            (int)Reserved_NumericUpDown.Value,
                                                            ModelSelectedId);
            if (int.TryParse(str, out result) == true)
            {
                RefreshData();
            }
            else MessageBox.Show(str);            
        }       

        private async void DelModel_Button_Click(object sender, EventArgs e)
        {
            if (Models_ListBox.SelectedItem == null) return;

            int result = 0;
            string str = await dataBaseService.DeleteModelAsync(Models_ListBox.SelectedItem.ToString());
            if (int.TryParse(str, out result) == true)
            {
                RefreshData();
            }
            else MessageBox.Show(str);
        }       

        private void Models_ListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Models_ListBox.SelectedItem == null) return;
            string[] str = Models_ListBox.SelectedItem.ToString().Split('.');
            ModelSelectedId = int.Parse(str[0]);
            Name_TextBox.Text = str[1];
            Type_ComboBox.SelectedIndex = int.Parse(str[2])-1;
            Weight_NumericUpDown.Value = int.Parse(str[3]);
            Price_NumericUpDown.Value = int.Parse(str[4]);
            StockBalance_NumericUpDown.Value = int.Parse(str[5]);
            Manufacturer_ComboBox.SelectedIndex = int.Parse(str[6])-1;
            Supplier_ComboBox.SelectedIndex = int.Parse(str[7])-1;
            Reserved_NumericUpDown.Value = int.Parse(str[8]);
            Saled_NumericUpDown.Value = int.Parse(str[9]);
        }

        private async void Sort_Button_Click(object sender, EventArgs e)
        {
            int result = 0;
            string str = "";

            if (TypeSort_ComboBox.SelectedItem == null) return;
            string direction = Direction_ComboBox.SelectedItem.ToString();


            switch (TypeSort_ComboBox.SelectedItem.ToString())
            {
                case "Без сортировки":
                    RefreshData();
                    break;
                case "По поставщику":
                    str = await dataBaseService.SelectModelTableAsync("supplier_name", direction, "suppliers", "supplier");
                    break;
                case "По производителю":
                    str = await dataBaseService.SelectModelTableAsync("manufacturer_name", direction, "manufacturers", "manufacturer");
                    break;
                //case "По дате продажи":
                //    str = await dataBaseService.SelectModelTableAsync("order_date", direction, "devices", "model", "orders", "order");
                //    break;
                case "По стране производства":
                    str = await dataBaseService.SelectModelTableAsync("country_name", direction, "manufacturers", "manufacturer", "countries", "country");
                    break;
                case "По весу":
                    str = await dataBaseService.SelectModelTableAsync("weight", direction);
                    break;
                case "По наличию":
                    str = await dataBaseService.SelectModelTableAsync("stock_balance", direction);
                    break;
                case "По резервам":
                    str = await dataBaseService.SelectModelTableAsync("reserved", direction);
                    break;
            }
            

            if (int.TryParse(str, out result) == true)
            {
                Models_ListBox.Items.Clear();
                ModelDataManager.Instance.GetFullDataListModel().ForEach(m => Models_ListBox.Items.Add(m));
                ClearModelInfo();
            }           
        }

        public void ClearModelInfo()
        {
            Name_TextBox.Text = "";
            Type_ComboBox.SelectedIndex = 0;
            Weight_NumericUpDown.Value = 1;
            Price_NumericUpDown.Value = 1;
            StockBalance_NumericUpDown.Value = 0;
            Manufacturer_ComboBox.SelectedIndex = 0;
            Supplier_ComboBox.SelectedIndex = 0;            
        }

        public async void RefreshData()
        {
            int result = 0;
            string str = "";
            str = await dataBaseService.ReadModelTableAsync();

            if (int.TryParse(str, out result) == true)
            {
                Models_ListBox.Items.Clear();
                ModelDataManager.Instance.GetFullDataListModel().ForEach(m => Models_ListBox.Items.Add(m));
                ClearModelInfo();
            }
            else MessageBox.Show(str);
        }

        private async void ModelForm_Load(object sender, EventArgs e)
        {
            int result = 0;
            string str = await dataBaseService.ReadModelTableAsync();
            if (int.TryParse(str, out result) == false)
            {
                MessageBox.Show(str);
                return;
            }

            str = await dataBaseService.ReadTypeTableAsync();
            if (int.TryParse(str, out result) == false)
            {
                MessageBox.Show(str);
                return;
            }

            str = await dataBaseService.ReadManufacturerTableAsync();
            if (int.TryParse(str, out result) == false)
            {
                MessageBox.Show(str);
                return;
            }

            str = await dataBaseService.ReadCountryTableAsync();
            if (int.TryParse(str, out result) == false)
            {
                MessageBox.Show(str);
                return;
            }

            str = await dataBaseService.ReadSupplierTableAsync();
            if (int.TryParse(str, out result) == false)
            {
                MessageBox.Show(str);
                return;
            }

            str = await dataBaseService.ReadModelOrderTableAsync();
            if (int.TryParse(str, out result) == false)
            {
                MessageBox.Show(str);
                return;
            }

            RefreshData();
            ModelDataManager.Instance.GetNameListTypes().ForEach(m => Type_ComboBox.Items.Add(m));
            Type_ComboBox.SelectedIndex = 0;

            ModelDataManager.Instance.GetNameListManufacturers().ForEach(m => Manufacturer_ComboBox.Items.Add(m));
            Manufacturer_ComboBox.SelectedIndex = 0;

            ModelDataManager.Instance.GetNameListSuppliers().ForEach(s => Supplier_ComboBox.Items.Add(s));
            Supplier_ComboBox.SelectedIndex = 0;

            
            Direction_ComboBox.SelectedIndex = 0;
            TypeSort_ComboBox.SelectedIndex = 0;
        }        
    }
}
