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
    public partial class OrderForm : Form
    {
        DataBaseService dataBaseService = new DataBaseService();
        int orderSelectedId = 0;        

        public OrderForm()
        {
            InitializeComponent();
            Client_TextBox.Enabled = false;
            Add_Button.Enabled = false;
            Del_Button.Enabled = false;
            Edit_Button.Enabled = false;
        }

        private void Add_Button_Click(object sender, EventArgs e)
        {
            
        }

        private void Edit_Button_Click(object sender, EventArgs e)
        {
            
        }

        private async void Del_Button_Click(object sender, EventArgs e)
        {
            if (Orders_ListBox.SelectedItem == null) return;

            int result = 0;
            string str = await dataBaseService.DeleteOrderAsync(Orders_ListBox.SelectedItem.ToString());
            if (int.TryParse(str, out result) == true)
            {
                RefreshData();
            }
            else
            {
                MessageBox.Show(str);
                str = await dataBaseService.ReadOrderTableAsync();
                if (int.TryParse(str, out result) == false)
                {
                    MessageBox.Show(str);
                    return;
                }
            }            
        }

        private void Orders_ListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Orders_ListBox.SelectedItem == null) return;
            string[] str = Orders_ListBox.SelectedItem.ToString().Split('.');
            orderSelectedId = int.Parse(str[0]);
            Name_TextBox.Text = str[1];
            Order_DateTimePicker.Value = ShopDataManager.Instance.GetOrderDate(orderSelectedId);

            Client_TextBox.Text = HumanDataManager.Instance.GetClientInfo(int.Parse(str[5]));

            Model_ListBox.Items.Clear();
            ShopDataManager.Instance.GetDataListModel(orderSelectedId).ForEach(m => Model_ListBox.Items.Add(m));
            Price_Label.Text = ShopDataManager.Instance.GetTotalCost(orderSelectedId).ToString();
        }

        public async void RefreshData()
        {
            int result = 0;
            string str = "";
            str = await dataBaseService.ReadOrderTableAsync();

            if (int.TryParse(str, out result) == true)
            {
                Orders_ListBox.Items.Clear();
                ShopDataManager.Instance.GetFullDataListOrder().ForEach(o => Orders_ListBox.Items.Add(o));
                Name_TextBox.Text = "";
                
            }
            else MessageBox.Show(str);
        }

        private async void OrderForm_Load(object sender, EventArgs e)
        {
            int result = 0;            

            string str = await dataBaseService.ReadModelOrderTableAsync();
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

            str = await dataBaseService.ReadModelTableAsync();
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

            str = await dataBaseService.ReadClientTableAsync();
            if (int.TryParse(str, out result) == false)
            {
                MessageBox.Show(str);
                return;
            }

            RefreshData();
            Name_TextBox.Enabled = false;
            Add_Button.Enabled = false;
        }

      
    }
}
