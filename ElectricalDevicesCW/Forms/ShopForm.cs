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
        DataBaseService dataBaseService = new DataBaseService();

        public ShopForm(User user)
        {
            InitializeComponent();
            this.user = user;            
            UserName_Label.Text = "Пользователь:" + user.Name;
            
        }

        private void InBasket_button_Click(object sender, EventArgs e)
        {

        }        

        public void RefreshData()
        {
            ProductModels_ListBox.Items.Clear();
            DeviceModelDataManager.Instance.GetDataListDeviceModel().ForEach(m => ProductModels_ListBox.Items.Add(m));
        }

        private void ProductModels_ListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            DeviceInfo_Label.Text = DeviceModelDataManager.Instance.GetInfoDeviceModel(ProductModels_ListBox.SelectedItem.ToString());
        }

        private async void ShopForm_Load(object sender, EventArgs e)
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

            
            RefreshData();
        }     
    }
}
