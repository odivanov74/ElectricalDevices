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
    public partial class LoginForm : Form
    {

        SqlConnection connection;
        //DataBaseService dataBase;
        UserManager userManager;
        DeviceManager deviceManager;

        ShopForm shopForm;
        MenuForm menuForm;
        RegistrationForm registrationForm;

        public LoginForm()
        {
            InitializeComponent();
            connection = new SqlConnection(@"Data Source=DESKTOP-MHB46B8\SQLEXPRESS;Initial catalog=ElectricalDevices_v3;Integrated Security=true;");
            //dataBase = new DataBaseService(connection);
            userManager = new UserManager(connection);
            
        }        

        private async void Login_button_Click(object sender, EventArgs e)
        {
            User user = await userManager.CheckLoginAsync(LoginInput_textBox.Text, PasswordInput_textBox.Text);
            if (user == null)
            {
                MessageBox.Show("Неверный login или password, повторите ввод!");
                return;
            }
            else if(user.RightData.Tables[0].Rows.Count==1 && user.RightData.Tables[0].Rows[0].Field<string>("right_name") == "View type product")
            {
                deviceManager = new DeviceManager(connection);
                shopForm = new ShopForm(user, deviceManager);
                shopForm.StartPosition = FormStartPosition.Manual;
                shopForm.Location = new Point(Location.X,Location.Y);
                if(shopForm.ShowDialog() == DialogResult.OK)
                {

                }
            }
            else
            {
                menuForm = new MenuForm(user, userManager, deviceManager);
                menuForm.StartPosition = FormStartPosition.Manual;
                menuForm.Location = new Point(Location.X, Location.Y);                
                if (menuForm.ShowDialog() == DialogResult.OK)
                {

                }
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            registrationForm = new RegistrationForm(userManager);
            registrationForm.StartPosition = FormStartPosition.Manual;
            registrationForm.Location = new Point(Location.X, Location.Y);
            if (registrationForm.ShowDialog() == DialogResult.OK)
            {

            }
        }

        private void Cancel_button_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
