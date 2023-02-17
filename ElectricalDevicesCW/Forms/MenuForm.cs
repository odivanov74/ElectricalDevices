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
        User user;
        DataBaseService dataBaseService = new DataBaseService();
        UsersForm usersForm;        
        ModelForm deviceModelForm;
        DeviceForm deviceForm;
        ManufacturerForm manufacturerForm;
        CountryForm countryForm;
        SupplierForm supplierForm;
        TypeForm TypeForm;
        OrderForm orderForm;

        public MenuForm(User user)
        {
            InitializeComponent();
            this.user = user;

            Table_ComboBox.Items.Add("Types");
            Table_ComboBox.Items.Add("Models");
            Table_ComboBox.Items.Add("Devices");
            Table_ComboBox.Items.Add("Manufacturers");
            Table_ComboBox.Items.Add("Suppliers");
            Table_ComboBox.Items.Add("Countries");
            Table_ComboBox.Items.Add("Orders");

            if (user.Role == "administrator")
            {
                Table_ComboBox.Items.Add("Users");
            }

            Table_ComboBox.SelectedIndex = 0;
        }

        private void StartTask_Button_Click(object sender, EventArgs e)
        {            
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

                case "Types":
                    TypeForm = new TypeForm();
                    TypeForm.StartPosition = FormStartPosition.Manual;
                    TypeForm.Location = new Point(Location.X, Location.Y);
                    if (TypeForm.ShowDialog() == DialogResult.OK)
                    {

                    }
                    break;

                case "Models":
                    deviceModelForm = new ModelForm();
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
                    manufacturerForm = new ManufacturerForm();
                    manufacturerForm.StartPosition = FormStartPosition.Manual;
                    manufacturerForm.Location = new Point(Location.X, Location.Y);
                    if (manufacturerForm.ShowDialog() == DialogResult.OK)
                    {

                    }
                    break;

                case "Suppliers":
                    supplierForm = new SupplierForm();
                    supplierForm.StartPosition = FormStartPosition.Manual;
                    supplierForm.Location = new Point(Location.X, Location.Y);
                    if (supplierForm.ShowDialog() == DialogResult.OK)
                    {

                    }
                    break;
                case "Countries":
                    countryForm = new CountryForm();
                    countryForm.StartPosition = FormStartPosition.Manual;
                    countryForm.Location = new Point(Location.X, Location.Y);
                    if (countryForm.ShowDialog() == DialogResult.OK)
                    {

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
