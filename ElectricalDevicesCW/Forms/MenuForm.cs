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
    public partial class MenuForm : Form
    {
        User user;
        //DataBaseService dataBaseService;
        UsersForm usersForm;
        UserManager userManager;
        DeviceManager deviceManager;

        public MenuForm(User user, UserManager userManager, DeviceManager deviceManager)
        {
            InitializeComponent();
            this.userManager = userManager;
            this.deviceManager = deviceManager;
            this.user = user;
            List<string> listRight = user.GetNameRights();
            if (listRight.Contains("Add new user") || listRight.Contains("Edit user") || listRight.Contains("View users") || listRight.Contains("Delete users"))
                Table_ComboBox.Items.Add("Users");
            if (listRight.Contains("Add new type product") || listRight.Contains("Edit type product") || listRight.Contains("View type product") || listRight.Contains("Delete type product"))
                Table_ComboBox.Items.Add("Product types");
            if (listRight.Contains("Add new product") || listRight.Contains("Edit product") || listRight.Contains("View product") || listRight.Contains("Delete product"))
                Table_ComboBox.Items.Add("Products");
            if (listRight.Contains("Add new manufacturer") || listRight.Contains("Edit manufacturer") || listRight.Contains("View manufacturer") || listRight.Contains("Delete manufacturer"))
                Table_ComboBox.Items.Add("Manufacturers");
            if (listRight.Contains("Add new supplier") || listRight.Contains("Edit supplier") || listRight.Contains("View supplier") || listRight.Contains("Delete supplier"))
                Table_ComboBox.Items.Add("Suppliers");
            Table_ComboBox.SelectedIndex = 0;
        }

        private void StartTask_Button_Click(object sender, EventArgs e)
        {
            switch(Table_ComboBox.SelectedItem.ToString())
            {
                case "Users":
                    usersForm = new UsersForm(user, userManager);
                    usersForm.StartPosition = FormStartPosition.Manual;
                    usersForm.Location = new Point(Location.X, Location.Y);
                    if(usersForm.ShowDialog() == DialogResult.OK)
                    {

                    }
                    break;
                case "Product types":
                    MessageBox.Show("Работа над интерфейсом \"Product types\" на завершена");
                    break;
                case "Products":
                    MessageBox.Show("Работа над интерфейсом \"Products\" на завершена");
                    break;
                case "Manufacturers":
                    MessageBox.Show("Работа над интерфейсом \"Manufacturers\" на завершена");
                    break;
                case "Suppliers":
                    MessageBox.Show("Работа над интерфейсом \"Suppliers\" на завершена");
                    break;
            }
        }

        private void Cancel_Button_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
