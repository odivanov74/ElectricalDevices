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
    public partial class DeviceForm : Form
    {
        DataBaseService dataBaseService = new DataBaseService();
        int deviceSelectedId = 0;


        public DeviceForm()
        {
            InitializeComponent();
            
            ManufactureDate_DateTimePicker.CustomFormat = "dd.MM.yyyy";
            ManufactureDate_DateTimePicker.Format = DateTimePickerFormat.Custom;            
            OrderName_TextBox.Enabled = false;
            BasketName_TextBox.Enabled = false;
            NotDefected_RadioButton.Checked = true;
        }

        private void Devices_ListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Device_ListBox.SelectedItem == null) return;
            string[] str = Device_ListBox.SelectedItem.ToString().Split('.');
            deviceSelectedId = int.Parse(str[0]);
            Model_ComboBox.SelectedIndex = int.Parse(str[1]) - 1;            
            SerialNumber_TextBox.Text = str[2];
            ManufactureDate_DateTimePicker.Value = ModelDataManager.Instance.GetDateManufactureDevice(deviceSelectedId);

            if (ModelDataManager.Instance.GetStatusDefectDevice(deviceSelectedId) == true)
            {
                IsDefected_RadioButton.Checked = true;
                NotDefected_RadioButton.Checked = false;
            }
            else
            {
                IsDefected_RadioButton.Checked = false;
                NotDefected_RadioButton.Checked = true;
            }
            //заказ
            int result = 0;
            if(int.TryParse(str[6],out result)==true)
            {
                OrderName_TextBox.Text = ShopDataManager.Instance.GetOrderName(result);                
            }
            else
            {
                OrderName_TextBox.Text = "";
            }
            //корзина
            if (int.TryParse(str[7], out result) == true)
            {
                BasketName_TextBox.Text = ShopDataManager.Instance.GetOrderName(result);                
            }
            else
            {
                BasketName_TextBox.Text = "";
            }            
        }

        private async void AddDevice_Button_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(SerialNumber_TextBox.Text) == true) return;

            int result = 0;
            string str = await dataBaseService.AddDeviceAsync(  Model_ComboBox.SelectedIndex+1, 
                                                                SerialNumber_TextBox.Text, 
                                                                ManufactureDate_DateTimePicker.Value,                                                                
                                                                IsDefected_RadioButton.Checked);
            if (int.TryParse(str, out result) == true)
            {
                RefreshData();                
            }
            else MessageBox.Show(str);
        }

        private async void Edit_Button_Click(object sender, EventArgs e)
        {
            if (Device_ListBox.SelectedItem == null || string.IsNullOrWhiteSpace(SerialNumber_TextBox.Text) == true) return;

            int result = 0;
            string str = await dataBaseService.UpdateDeviceAsync((int)Model_ComboBox.SelectedIndex + 1,
                                                                SerialNumber_TextBox.Text,
                                                                ManufactureDate_DateTimePicker.Value,
                                                                IsDefected_RadioButton.Checked,
                                                                deviceSelectedId);
            if (int.TryParse(str, out result) == true)
            {
                RefreshData();                
            }
            else MessageBox.Show(str);
        }

        private async void Del_Button_Click(object sender, EventArgs e)
        {
            if (Device_ListBox.SelectedItem == null) return;

            int result = 0;
            string str = await dataBaseService.DeleteDeviceAsync(Device_ListBox.SelectedItem.ToString());
            if (int.TryParse(str, out result) == true)
            {
                RefreshData();                
            }
            else MessageBox.Show(str);
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
                case "По индексу модели":
                    str = await dataBaseService.SelectDeviceTableAsync("model_id", direction, "models", "model");
                    break;
                case "По названию модели":
                    str = await dataBaseService.SelectDeviceTableAsync("model_name", direction, "models", "model");
                    break;
                case "По дате производства":
                    str = await dataBaseService.SelectDeviceTableAsync("manufacture_date", direction);
                    break;
                case "По дате продажи":
                    str = await dataBaseService.SelectDeviceTableAsync("order_date", direction, "orders", "order");
                    break;                
            }


            if (int.TryParse(str, out result) == true)
            {
                Device_ListBox.Items.Clear();
                ModelDataManager.Instance.GetFullDataListDevice().ForEach(d => Device_ListBox.Items.Add(d));
                ClearDeviceInfo();
            }
        }


        public void ClearDeviceInfo()
        {
            Model_ComboBox.SelectedIndex = 0;
            SerialNumber_TextBox.Text = "";
            ManufactureDate_DateTimePicker.Value = DateTime.Today;
            NotDefected_RadioButton.Checked = true;
            OrderName_TextBox.Text = "";
            BasketName_TextBox.Text = "";
        }

        public async void RefreshData()
        {
            int result = 0;
            string str = "";
            str = await dataBaseService.ReadDeviceTableAsync();

            if (int.TryParse(str, out result) == true)
            {
                Device_ListBox.Items.Clear();
                ModelDataManager.Instance.GetFullDataListDevice().ForEach(d => Device_ListBox.Items.Add(d));
                ClearDeviceInfo();
            }
            else MessageBox.Show(str);
        }

        private async void DeviceForm_Load(object sender, EventArgs e)
        {
            int result = 0;
            string str = await dataBaseService.ReadModelTableAsync();
            if (int.TryParse(str, out result) == false)
            {
                MessageBox.Show(str);
                return;
            }

            str = await dataBaseService.ReadOrderTableAsync();
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

            ModelDataManager.Instance.GetNameListModel().ForEach(d => Model_ComboBox.Items.Add(d));
            Model_ComboBox.SelectedIndex = 0;
            TypeSort_ComboBox.SelectedIndex = 0;
            Direction_ComboBox.SelectedIndex = 0;
            RefreshData();
        }        
    }
}
