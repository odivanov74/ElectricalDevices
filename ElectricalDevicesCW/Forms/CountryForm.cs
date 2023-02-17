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
    public partial class CountryForm : Form
    {
        DataBaseService dataBaseService = new DataBaseService();
        int countrySelectedId = 0;

        public CountryForm()
        {
            InitializeComponent();
            RefreshData();            
        }

        private async void Delete_Button_Click(object sender, EventArgs e)
        {
            if (Country_ListBox.SelectedItem == null) return;
            int result = 0;
            string str = await dataBaseService.DeleteCountryAsync(Country_ListBox.SelectedItem.ToString());
            if (int.TryParse(str, out result) == true)
            {
                RefreshData();
            }
            else MessageBox.Show(str);
        }

        private async void Add_Button_Click(object sender, EventArgs e)
        {
            int result = 0;
            if (string.IsNullOrWhiteSpace(CountryName_TextBox.Text) == true) return;
            string str = await dataBaseService.AddCountryAsync(CountryName_TextBox.Text);
            
            if (int.TryParse(str, out result) == true)
            {
                RefreshData();
            }
            else MessageBox.Show(str);
        }

        private async void Edit_Button_Click(object sender, EventArgs e)
        {
            int result = 0;
            if (string.IsNullOrWhiteSpace(CountryName_TextBox.Text) == true) return;
            string str = await dataBaseService.UpdateCountryAsync(CountryName_TextBox.Text, countrySelectedId);
            
            if (int.TryParse(str, out result) == true)
            {
                RefreshData();
            }                        
            else MessageBox.Show(str);
        }    

        private void Country_ListBox_SelectedIndexChanged(object sender, EventArgs e)
        {            
            if (Country_ListBox.SelectedItem == null) return;
            string[] str = Country_ListBox.SelectedItem.ToString().Split('.');
            countrySelectedId = int.Parse(str[0]);
            CountryName_TextBox.Text = str[1];
        }

        public async void RefreshData()
        {
            int result = 0;
            string str = "";          
            str = await dataBaseService.ReadCountryTableAsync();

            if (int.TryParse(str, out result) == true)
            {
                Country_ListBox.Items.Clear();
                ModelDataManager.Instance.GetFullDataListCounties().ForEach(m => Country_ListBox.Items.Add(m));
                CountryName_TextBox.Text = "";
            }
            else MessageBox.Show(str);
        }

    }
}
