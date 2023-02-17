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
    public partial class BasketForm : Form
    {
        DataBaseService dataBaseService = new DataBaseService();
        int basketSelectedId = 0;
        Client client;

        public BasketForm(Client client)
        {
            InitializeComponent();
            this.client = client;
            RefreshData();
        }

        private async void Add_Button_Click(object sender, EventArgs e)
        {
            int result = 0;
            if (string.IsNullOrWhiteSpace(Name_TextBox.Text) == true) return;
            string str = await dataBaseService.AddBasketAsync(Name_TextBox.Text, client.Id);

            if (int.TryParse(str, out result) == true)
            {
                RefreshData();
            }
            else MessageBox.Show(str);
        }

        private async void Edit_Button_Click(object sender, EventArgs e)
        {
            int result = 0;
            if (string.IsNullOrWhiteSpace(Name_TextBox.Text) == true) return;
            string str = await dataBaseService.UpdateBasketAsync(Name_TextBox.Text, client.Id, basketSelectedId);

            if (int.TryParse(str, out result) == true)
            {
                RefreshData();
            }
            else MessageBox.Show(str);
        }

        private async void Delete_Button_Click(object sender, EventArgs e)
        {
            if (Basket_ListBox.SelectedItem == null || Basket_ListBox.Items.Count == 1) return;
            int result = 0;
            string str = await dataBaseService.DeleteBasketAsync(Basket_ListBox.SelectedItem.ToString());
            if (int.TryParse(str, out result) == true)
            {
                RefreshData();
            }
            else MessageBox.Show(str);
        }

        private void Basket_ListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Basket_ListBox.SelectedItem == null) return;
            string[] str = Basket_ListBox.SelectedItem.ToString().Split('.');
            basketSelectedId = int.Parse(str[0]);
            Name_TextBox.Text = str[1];
        }

        public async void RefreshData()
        {
            int result = 0;
            string str = "";
            str = await dataBaseService.ReadBasketTableAsync();

            if (int.TryParse(str, out result) == true)
            {
                Basket_ListBox.Items.Clear();
                ShopDataManager.Instance.GetFullListBasket(client.Id).ForEach(b => Basket_ListBox.Items.Add(b));
                Name_TextBox.Text = "";
            }
            else MessageBox.Show(str);
        }

        private void BasketForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            DialogResult = DialogResult.OK;
        }
    }
}
