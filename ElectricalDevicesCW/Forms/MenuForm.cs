using ElectricalDevicesCW.Forms;
using ElectricalDevicesCW.Managers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ElectricalDevicesCW
{
    public partial class MenuForm : Form
    {
        //SqlConnection connection;
        User user;
        DataBaseService dataBaseService = new DataBaseService();
        UsersForm usersForm;        
        DeviceModelForm deviceModelForm;
        DeviceForm deviceForm;
        ManufacturerForm manufacturerForm;
        CountryForm countryForm;
        SupplierForm supplierForm;
        ModelTypeForm modelTypeForm;
        OrderForm orderForm;

        public MenuForm(User user)
        {
            InitializeComponent();
            this.user = user;

            List<string> listRight = user.GetNameRights();
            if (listRight.Contains("Add user") || listRight.Contains("Edit user") || listRight.Contains("View user") || listRight.Contains("Delete user"))
                Table_ComboBox.Items.Add("Users");
            if (listRight.Contains("View modelType") || listRight.Contains("Edit modelType") || listRight.Contains("Add modelType") || listRight.Contains("Delete modelType"))
                Table_ComboBox.Items.Add("ModelTypes");
            if (listRight.Contains("View deviceModel") || listRight.Contains("Add deviceModel") || listRight.Contains("Edit deviceModel") || listRight.Contains("Delete deviceModel"))
                Table_ComboBox.Items.Add("DeviceModels");
            if (listRight.Contains("View device") || listRight.Contains("Add device") || listRight.Contains("Edit device") || listRight.Contains("Delete device"))
                Table_ComboBox.Items.Add("Devices");
            if (listRight.Contains("Add manufacturer") || listRight.Contains("Edit manufacturer") || listRight.Contains("View manufacturer") || listRight.Contains("Delete manufacturer"))
                Table_ComboBox.Items.Add("Manufacturers");
            if (listRight.Contains("Add supplier") || listRight.Contains("Edit supplier") || listRight.Contains("View supplier") || listRight.Contains("Delete supplier"))
                Table_ComboBox.Items.Add("Suppliers");
            if (listRight.Contains("Add country") || listRight.Contains("Edit country") || listRight.Contains("View country") || listRight.Contains("Delete country"))
                Table_ComboBox.Items.Add("Countries");
            if (listRight.Contains("Add order") || listRight.Contains("Edit order") || listRight.Contains("View order") || listRight.Contains("Delete order"))
                Table_ComboBox.Items.Add("Orders");
            Table_ComboBox.SelectedIndex = 0;
        }

        private async void StartTask_Button_Click(object sender, EventArgs e)
        {
            int result = 0;
            switch (Table_ComboBox.SelectedItem.ToString())
            {
                case "Users":
                    usersForm = new UsersForm(user);
                    usersForm.StartPosition = FormStartPosition.Manual;
                    usersForm.Location = new Point(Location.X, Location.Y);
                    if(usersForm.ShowDialog() == DialogResult.OK)
                    {

                    }
                    break;

                case "ModelTypes":
                    modelTypeForm = new ModelTypeForm();
                    modelTypeForm.StartPosition = FormStartPosition.Manual;
                    modelTypeForm.Location = new Point(Location.X, Location.Y);
                    if (modelTypeForm.ShowDialog() == DialogResult.OK)
                    {

                    }
                    break;

                case "DeviceModels":
                    deviceModelForm = new DeviceModelForm();
                    deviceModelForm.StartPosition = FormStartPosition.Manual;
                    deviceModelForm.Location = new Point(Location.X, Location.Y);
                    if (deviceModelForm.ShowDialog() == DialogResult.OK)
                    {

                    }
                    break;
                case "Devices":                   
                    deviceForm = new DeviceForm();
                    deviceForm.StartPosition = FormStartPosition.Manual;
                    deviceForm.Location = new Point(Location.X, Location.Y);
                    if (deviceForm.ShowDialog() == DialogResult.OK)
                    {

                    }
                    break;
                case "Manufacturers":                    
                    if (int.TryParse(await dataBaseService.ReadCountryTableAsync(), out result) == true)
                    {
                        if (int.TryParse(await dataBaseService.ReadManufacturerTableAsync(), out result) == true)
                        {
                            manufacturerForm = new ManufacturerForm();
                            manufacturerForm.StartPosition = FormStartPosition.Manual;
                            manufacturerForm.Location = new Point(Location.X, Location.Y);
                            if (manufacturerForm.ShowDialog() == DialogResult.OK)
                            {

                            }
                        }
                    }    
                    break;

                case "Suppliers":                    
                    if (int.TryParse(await dataBaseService.ReadSupplierTableAsync(), out result) == true)
                    {
                        supplierForm = new SupplierForm();
                        supplierForm.StartPosition = FormStartPosition.Manual;
                        supplierForm.Location = new Point(Location.X, Location.Y);
                        if (supplierForm.ShowDialog() == DialogResult.OK)
                        {

                        }
                    }
                        break;
                case "Countries":                   
                    if (int.TryParse(await dataBaseService.ReadCountryTableAsync(), out result) == true)
                    {
                        countryForm = new CountryForm();
                        countryForm.StartPosition = FormStartPosition.Manual;
                        countryForm.Location = new Point(Location.X, Location.Y);
                        if (countryForm.ShowDialog() == DialogResult.OK)
                        {

                        }
                    }                    
                    break;
                case "Orders":
                    orderForm = new OrderForm();
                    orderForm.StartPosition = FormStartPosition.Manual;
                    orderForm.Location = new Point(Location.X, Location.Y);
                    if (orderForm.ShowDialog() == DialogResult.OK)
                    {

                    }
                    break;
            }
        }

        private void Cancel_Button_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
