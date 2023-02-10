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
            OrderDate_DateTimePicker.CustomFormat = "dd.MM.yyyy";
            OrderDate_DateTimePicker.Format = DateTimePickerFormat.Custom;
            OrderName_TextBox.Enabled = false;
            OrderDate_DateTimePicker.Enabled = false;
        }

        private void Devices_ListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Devices_ListBox.SelectedItem == null) return;
            string[] str = Devices_ListBox.SelectedItem.ToString().Split('.');
            deviceSelectedId = int.Parse(str[0]);
            Model_ComboBox.SelectedIndex = int.Parse(str[1]) - 1;            
            SerialNumber_TextBox.Text = str[2];
            ManufactureDate_DateTimePicker.Value = DeviceDataManager.Instance.GetDateManufactureDevice(deviceSelectedId);
            if (DeviceDataManager.Instance.GetStatusDevice(deviceSelectedId) == true)
            {
                IsDefected_RadioButton.Checked = true;
                NotDefected_RadioButton.Checked = false;
            }
            else
            {
                IsDefected_RadioButton.Checked = false;
                NotDefected_RadioButton.Checked = true;
            }
            
            OrderName_TextBox.Text = str[5];
            OrderDate_DateTimePicker.Value = DeviceDataManager.Instance.GetDateManufactureDevice(deviceSelectedId); //временно
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
            if (Devices_ListBox.SelectedItem == null || string.IsNullOrWhiteSpace(SerialNumber_TextBox.Text) == true) return;

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

        private async void DelDevice_Button_Click(object sender, EventArgs e)
        {
            if (Devices_ListBox.SelectedItem == null) return;

            int result = 0;
            string str = await dataBaseService.DeleteDeviceAsync(Devices_ListBox.SelectedItem.ToString());
            if (int.TryParse(str, out result) == true)
            {
                RefreshData();
            }
            else MessageBox.Show(str);
        }

        public async void RefreshData()
        {
            int result = 0;
            string str = "";
            str = await dataBaseService.ReadDeviceTableAsync();

            if (int.TryParse(str, out result) == true)
            {
                Devices_ListBox.Items.Clear();
                DeviceDataManager.Instance.GetFullDataListDevice().ForEach(d => Devices_ListBox.Items.Add(d));                
            }
            else MessageBox.Show(str);
        }

        private async void DeviceForm_Load(object sender, EventArgs e)
        {
            int result = 0;
            string str = await dataBaseService.ReadDeviceModelTableAsync();
            if (int.TryParse(str, out result) == false)
            {
                MessageBox.Show(str);
                return;
            }

            DeviceModelDataManager.Instance.GetNameListDeviceModel().ForEach(d => Model_ComboBox.Items.Add(d));
            Model_ComboBox.SelectedIndex = 0;
            RefreshData();
        }
    }
}
