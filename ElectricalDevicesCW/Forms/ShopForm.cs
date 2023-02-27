using ElectricalDevicesCW.Forms;
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
        Client client;        
        DataBaseService dataBaseService = new DataBaseService();
        BasketForm basketForm;
        int idSelectedString = -1;

        public ShopForm(Client client)
        {
            InitializeComponent();
            this.client = client;            
            UserName_Label.Text = "Клиент:" + client.Name;
            
        }

        private async void InBasket_button_Click(object sender, EventArgs e)
        {
            if (idSelectedString == -1) return;
            int result = 0;
            string nameModel = ProductModels_ListBox.Items[idSelectedString].ToString().Split('.')[1];
            int idModel = ModelDataManager.Instance.GetModelId(nameModel);
            int idBasket = int.Parse(BasketNames_ComboBox.SelectedItem.ToString().Split('.')[0]);

            string outStr = await dataBaseService.AddModelToBasketAsync(idModel, (int)NumDevices_NumericUpDown.Value, idBasket);
            if(int.TryParse(outStr, out result)==false)
            {
                MessageBox.Show(outStr);
            }
            NumDevices_NumericUpDown.Value = 1;
            RefreshData();
        }        

        public void RefreshData()
        {
            ProductModels_ListBox.Items.Clear();
            ModelDataManager.Instance.GetDataListModel().ForEach(m => ProductModels_ListBox.Items.Add(m));

            RefreshBasketContent();

            if (DeviceInfo_Label.Text != "") DeviceInfo_Label.Text = ModelDataManager.Instance.GetInfoModel(ProductModels_ListBox.Items[idSelectedString].ToString());
        }

        private async void ProductModels_ListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            await RefreshModelEntities();
            idSelectedString = ProductModels_ListBox.SelectedIndex;
            DeviceInfo_Label.Text = ModelDataManager.Instance.GetInfoModel(ProductModels_ListBox.SelectedItem.ToString());
            NumDevices_NumericUpDown.Value = 1;
        }

        public async Task<bool> RefreshModelEntities()
        {
            int result = 0;
            string str = await dataBaseService.ReadModelTableAsync();
            if (int.TryParse(str, out result) == false)
            {
                MessageBox.Show(str);
                return false;
            }

            str = await dataBaseService.ReadTypeTableAsync();
            if (int.TryParse(str, out result) == false)
            {
                MessageBox.Show(str);
                return false;
            }

            str = await dataBaseService.ReadManufacturerTableAsync();
            if (int.TryParse(str, out result) == false)
            {
                MessageBox.Show(str);
                return false;
            }

            str = await dataBaseService.ReadCountryTableAsync();
            if (int.TryParse(str, out result) == false)
            {
                MessageBox.Show(str);
                return false;
            }
            return true;
        }

        

        private async void AddBasket_Button_Click(object sender, EventArgs e)
        {
            basketForm = new BasketForm(client);
            basketForm.StartPosition = FormStartPosition.Manual;
            basketForm.Location = new Point(Location.X, Location.Y);
            if (basketForm.ShowDialog() == DialogResult.OK)
            {
                int result = 0;
                string str = await dataBaseService.ReadBasketTableAsync();
                if (int.TryParse(str, out result) == false)
                {
                    MessageBox.Show(str);
                    return;
                }
                BasketNames_ComboBox.Items.Clear();
                ShopDataManager.Instance.GetFullListBasket(client.Id).ForEach(b => BasketNames_ComboBox.Items.Add(b));
                BasketNames_ComboBox.SelectedIndex = 0;
            }
        }

        public void RefreshBasketContent()
        {
            Basket_ListBox.Items.Clear();
            int idBasket = int.Parse(BasketNames_ComboBox.SelectedItem.ToString().Split('.')[0]);
            ShopDataManager.Instance.GetFullDataListBasket(idBasket).ForEach(b => Basket_ListBox.Items.Add(b));
            int totalPrice = ShopDataManager.Instance.GetPriceBasket(idBasket);
            int personalDiscount = HumanDataManager.Instance.GetPersonalDiscount(client.Id);
            DiscountSum_Label.Text = ((double)totalPrice * personalDiscount / 100).ToString();
            PriceBasket_Label.Text = ((double)totalPrice * (100-personalDiscount) / 100).ToString();
        }

        private void BasketNames_ComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshBasketContent();
        }

        private async void Basket_ListBox_DoubleClick(object sender, EventArgs e)
        {
            if (Basket_ListBox.SelectedItem == null) return;
            string[] arrayStr = Basket_ListBox.SelectedItem.ToString().Split('.');
            int idModel = int.Parse(arrayStr[0]);
            int idBasket = int.Parse(BasketNames_ComboBox.SelectedItem.ToString().Split('.')[0]);
            await dataBaseService.DecModelToBasketAsync(idModel, idBasket);
            RefreshBasketContent();
            if (DeviceInfo_Label.Text != "") DeviceInfo_Label.Text = ModelDataManager.Instance.GetInfoModel(ProductModels_ListBox.Items[idSelectedString].ToString());
        }

        private async void Buy_Button_Click(object sender, EventArgs e)
        {
            int result = 0;
            int idBasket = int.Parse(BasketNames_ComboBox.SelectedItem.ToString().Split('.')[0]);
            string str = await dataBaseService.AddOrderAsync(client.Id);
            if (int.TryParse(str, out result) == true)
            {
                str = await dataBaseService.ReadOrderTableAsync();
                if (int.TryParse(str, out result) == true)
                {
                    List<string> list = new List<string>();
                    foreach (var item in Basket_ListBox.Items)
                    {
                        list.Add(item.ToString());
                    }
                    str = await dataBaseService.AddModelsToNewOrderAsync(list, idBasket, ShopDataManager.Instance.GetLastOrderId());
                    if (int.TryParse(str, out result) == true)
                    {
                        RefreshData();
                    }
                    else
                    {
                        MessageBox.Show(str);
                    }
                }
                else
                {
                    MessageBox.Show(str);
                }
            }
            else
            {
                MessageBox.Show(str);
            }
        }     

        private async void ShopForm_Load(object sender, EventArgs e)
        {
            await RefreshModelEntities();
            int result = 0;
            string str = await dataBaseService.ReadBasketTableAsync();
            if (int.TryParse(str, out result) == false)
            {
                MessageBox.Show(str);
                return;
            }

            str = await dataBaseService.ReadModelBasketTableAsync();
            if (int.TryParse(str, out result) == false)
            {
                MessageBox.Show(str);
                return;
            }

            str = await dataBaseService.ReadDeviceTableAsync();
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

            str = await dataBaseService.ReadOrderTableAsync();
            if (int.TryParse(str, out result) == false)
            {
                MessageBox.Show(str);
                return;
            }
           
            ShopDataManager.Instance.GetFullListBasket(client.Id).ForEach(b => BasketNames_ComboBox.Items.Add(b));
            BasketNames_ComboBox.SelectedIndex = 0;
            RefreshData();
        }

        
    }
}
