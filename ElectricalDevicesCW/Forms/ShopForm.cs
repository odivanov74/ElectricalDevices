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

namespace ElectricalDevicesCW
{
    public partial class ShopForm : Form
    {
        User user;
        DeviceManager deviceManager;

        public ShopForm(User user, DeviceManager deviceManager)
        {
            InitializeComponent();
            this.user = user;
            this.deviceManager = deviceManager;
            UserName_Label.Text = "Пользователь:" + user.Name;
            RefreshDeviceModelListBox();
        }

        private void InBasket_button_Click(object sender, EventArgs e)
        {

        }

        public void RefreshDevicesListBox()
        {
            ProductModels_ListBox.Items.Clear();
            deviceManager.GetListDevices().ForEach(u => ProductModels_ListBox.Items.Add(u));
        }

        public void RefreshDeviceModelListBox()
        {
            ProductModels_ListBox.Items.Clear();
            deviceManager.GetListDeviceModels().ForEach(d => ProductModels_ListBox.Items.Add(d));
        }

        private void ProductModels_ListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            DeviceInfo_Label.Text = deviceManager.GetInfoDeviceModel(ProductModels_ListBox.SelectedItem.ToString());
        }
    }
}
