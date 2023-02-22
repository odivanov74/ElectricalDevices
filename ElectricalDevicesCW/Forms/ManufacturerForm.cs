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
    public partial class ManufacturerForm : Form
    {
        DataBaseService dataBaseService = new DataBaseService();
        int manufacturerSelectedId = 0;

        public ManufacturerForm()
        {
            InitializeComponent();
        }

        private void Manufacturer_ListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Manufacturer_ListBox.SelectedItem == null) return;
            string[] str = Manufacturer_ListBox.SelectedItem.ToString().Split('.');
            manufacturerSelectedId = int.Parse(str[0]);
            ManufacturerName_TextBox.Text = str[1];
            Country_ComboBox.SelectedIndex = int.Parse(str[2])-1;
        }

        private async void DelManufacturer_Button_Click(object sender, EventArgs e)
        {
            if (Manufacturer_ListBox.SelectedItem == null) return;
            int result = 0;
            string str = await dataBaseService.DeleteManufacturerAsync(Manufacturer_ListBox.SelectedItem.ToString());
            if (int.TryParse(str, out result) == true)
            {
                RefreshScreenData();
            }
            else MessageBox.Show(str);
        }

        private async void AddManufacturer_Button_Click(object sender, EventArgs e)
        {
            int result = 0;
            if (string.IsNullOrWhiteSpace(ManufacturerName_TextBox.Text) == true) return;
            string str = await dataBaseService.AddManufacturerAsync(ManufacturerName_TextBox.Text, (int)Country_ComboBox.SelectedIndex+1);

            if (int.TryParse(str, out result) == true)
            {
                RefreshScreenData();
            }
            else MessageBox.Show(str);            
        }

        private async void Edit_Button_Click(object sender, EventArgs e)
        {
            int result = 0;
            if (string.IsNullOrWhiteSpace(ManufacturerName_TextBox.Text) == true) return;
            string str = await dataBaseService.UpdateManufacturerAsync( ManufacturerName_TextBox.Text, (int)Country_ComboBox.SelectedIndex+1, manufacturerSelectedId);
            if (int.TryParse(str, out result))
            {
                RefreshScreenData();                
            }
            else MessageBox.Show(str);
        }

        public async void RefreshScreenData()
        {
            int result = 0;
            string str = "";
            str = await dataBaseService.ReadManufacturerTableAsync();

            if (int.TryParse(str, out result) == true)
            {
                Manufacturer_ListBox.Items.Clear();
                ModelDataManager.Instance.GetFullDataListManufacturers().ForEach(m => Manufacturer_ListBox.Items.Add(m));
                ManufacturerName_TextBox.Text = "";
                if (Country_ComboBox.Items.Count > 0) Country_ComboBox.SelectedIndex = 0;
            }
            else MessageBox.Show(str);                 
        }

        private async void ManufacturerForm_Load(object sender, EventArgs e)
        {
            int result = 0;
            string str = await dataBaseService.ReadCountryTableAsync();
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
            ModelDataManager.Instance.GetNameListCountries().ForEach(m => Country_ComboBox.Items.Add(m));
            RefreshScreenData();
        }
    }
}
