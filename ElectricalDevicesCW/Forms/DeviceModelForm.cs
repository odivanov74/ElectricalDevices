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
    public partial class DeviceModelForm : Form
    { 
        DataBaseService dataBaseService = new DataBaseService();
        int deviceModelSelectedId = 0;

        public DeviceModelForm()
        {
            InitializeComponent();            
        }

        private async void Add_Button_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(Name_TextBox.Text) == true)
            {
                MessageBox.Show("Некорректные данные");
                return;
            }           
            
            int result = 0;
            string str = await dataBaseService.AddDeviceModelAsync(Name_TextBox.Text,
                                                            ModelType_ComboBox.SelectedIndex + 1,
                                                            (int)Weight_NumericUpDown.Value,
                                                            (int)Price_NumericUpDown.Value,
                                                            (int)StockBalance_NumericUpDown.Value,
                                                            Manufacturer_ComboBox.SelectedIndex + 1,
                                                            Supplier_ComboBox.SelectedIndex + 1);
            if (int.TryParse(str, out result) == true)
            {
                RefreshData();
            }
            else MessageBox.Show(str);
        }

        private async void EditDeviceModel_Button_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(Name_TextBox.Text) == true)
            {
                MessageBox.Show("Некорректные данные");
                return;
            }

            int result = 0;
            string str = await dataBaseService.UpdateDeviceModelAsync(Name_TextBox.Text,
                                                            ModelType_ComboBox.SelectedIndex + 1,
                                                            (int)Weight_NumericUpDown.Value,
                                                            (int)Price_NumericUpDown.Value,
                                                            (int)StockBalance_NumericUpDown.Value,
                                                            Manufacturer_ComboBox.SelectedIndex + 1,
                                                            Supplier_ComboBox.SelectedIndex + 1,
                                                            deviceModelSelectedId);
            if (int.TryParse(str, out result) == true)
            {
                RefreshData();
            }
            else MessageBox.Show(str);            
        }       

        private async void DelDeviceModel_Button_Click(object sender, EventArgs e)
        {
            if (DeviceModels_ListBox.SelectedItem == null) return;

            int result = 0;
            string str = await dataBaseService.DeleteDeviceModelAsync(DeviceModels_ListBox.SelectedItem.ToString());
            if (int.TryParse(str, out result) == true)
            {
                RefreshData();
            }
            else MessageBox.Show(str);
        }       

        private void DeviceModels_ListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (DeviceModels_ListBox.SelectedItem == null) return;
            string[] str = DeviceModels_ListBox.SelectedItem.ToString().Split('.');
            deviceModelSelectedId = int.Parse(str[0]);
            Name_TextBox.Text = str[1];
            ModelType_ComboBox.SelectedIndex = int.Parse(str[2])-1;
            Weight_NumericUpDown.Value = int.Parse(str[3]);
            Price_NumericUpDown.Value = int.Parse(str[4]);
            StockBalance_NumericUpDown.Value = int.Parse(str[5]);
            Manufacturer_ComboBox.SelectedIndex = int.Parse(str[6])-1;
            Supplier_ComboBox.SelectedIndex = int.Parse(str[7])-1;
        }        

        public void ClearDeviceModelInfo()
        {
            Name_TextBox.Text = "";
            ModelType_ComboBox.SelectedIndex = 0;
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
            str = await dataBaseService.ReadDeviceModelTableAsync();

            if (int.TryParse(str, out result) == true)
            {
                DeviceModels_ListBox.Items.Clear();
                DeviceModelDataManager.Instance.GetFullDataListDeviceModel().ForEach(d => DeviceModels_ListBox.Items.Add(d));
                ClearDeviceModelInfo();
            }
            else MessageBox.Show(str);
        }

        private async void DeviceModelForm_Load(object sender, EventArgs e)
        {
            int result = 0;
            string str = await dataBaseService.ReadDeviceModelTableAsync();
            if (int.TryParse(str, out result) == false)
            {
                MessageBox.Show(str);
                return;
            }

            str = await dataBaseService.ReadModelTypeTableAsync();
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
            RefreshData();
            ModelTypeDataManager.Instance.GetNameListModelTypes().ForEach(m => ModelType_ComboBox.Items.Add(m));
            ModelType_ComboBox.SelectedIndex = 0;

            ManufacturerDataManager.Instance.GetNameListManufacturers().ForEach(m => Manufacturer_ComboBox.Items.Add(m));
            Manufacturer_ComboBox.SelectedIndex = 0;

            SupplierDataManager.Instance.GetNameListSuppliers().ForEach(s => Supplier_ComboBox.Items.Add(s));
            Supplier_ComboBox.SelectedIndex = 0;

            StockBalance_NumericUpDown.Enabled = false;
        }
    }
}
