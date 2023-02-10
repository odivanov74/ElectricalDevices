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
        }

        private async void Add_Button_Click(object sender, EventArgs e)
        {
            //if (string.IsNullOrWhiteSpace(Name_TextBox.Text) == true) OrderDataManager_.Instance.GenerateNewOrderName();

            int result = 0;
            string str = await dataBaseService.AddOrderAsync(OrderDataManager_.Instance.GenerateNewOrderName(), Order_DateTimePicker.Value, Shiping_DateTimePicker.Value);
            if (int.TryParse(str, out result) == true)
            {
                RefreshData();
            }
            else MessageBox.Show(str);
        }

        private async void Edit_Button_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(Name_TextBox.Text) == true) return;

            int result = 0;
            string str = await dataBaseService.UpdateOrderAsync(Name_TextBox.Text, Order_DateTimePicker.Value, Shiping_DateTimePicker.Value, orderSelectedId);
            if (int.TryParse(str, out result) == true)
            {
                RefreshData();
            }
            else MessageBox.Show(str);
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
            Order_DateTimePicker.Value = OrderDataManager_.Instance.GetOrderDate(orderSelectedId);
            Shiping_DateTimePicker.Value = OrderDataManager_.Instance.GetShipingDate(orderSelectedId);

            Devices_ListBox.Items.Clear();
            DeviceOrderDataManager.Instance.GetDataListDevice(orderSelectedId).ForEach(d => Devices_ListBox.Items.Add(d));
            Price_Label.Text = DeviceOrderDataManager.Instance.GetTotalCost(orderSelectedId).ToString();
        }

        public async void RefreshData()
        {
            int result = 0;
            string str = "";
            str = await dataBaseService.ReadOrderTableAsync();

            if (int.TryParse(str, out result) == true)
            {
                Orders_ListBox.Items.Clear();
                OrderDataManager_.Instance.GetFullDataListOrder().ForEach(o => Orders_ListBox.Items.Add(o));
                Name_TextBox.Text = "";
                
            }
            else MessageBox.Show(str);
        }

        private async void OrderForm_Load(object sender, EventArgs e)
        {
            int result = 0;
            //string str = await dataBaseService.ReadOrderTableAsync();
            //if (int.TryParse(str, out result) == false)
            //{
            //    MessageBox.Show(str);
            //    return;
            //}

            string str = await dataBaseService.ReadDeviceOrderTableAsync();
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

            str = await dataBaseService.ReadModelTypeTableAsync();
            if (int.TryParse(str, out result) == false)
            {
                MessageBox.Show(str);
                return;
            }

            str = await dataBaseService.ReadDeviceModelTableAsync();
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
