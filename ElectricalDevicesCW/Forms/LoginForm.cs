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
        DataBaseService dataBaseService = new DataBaseService();

        ShopForm shopForm;
        MenuForm menuForm;
        RegistrationForm registrationForm;

        public LoginForm()
        {
            InitializeComponent(); 
        }        

        private void Login_button_Click(object sender, EventArgs e)
        {
            User user = HumanDataManager.Instance.GetUser(LoginInput_textBox.Text, PasswordInput_textBox.Text);
            if (user == null)
            {
                MessageBox.Show("Неверный login или password, повторите ввод!");
                return;
            }

            if(user.Role=="client")
            {                
                shopForm = new ShopForm(HumanDataManager.Instance.GetClient(user.Id));
                shopForm.StartPosition = FormStartPosition.Manual;
                shopForm.Location = new Point(Location.X,Location.Y);
                if(shopForm.ShowDialog() == DialogResult.OK)
                {
                    
                }
            }
            else if (user.Role == "administrator" || user.Role == "manager")
            {                
                menuForm = new MenuForm(user);
                menuForm.StartPosition = FormStartPosition.Manual;
                menuForm.Location = new Point(Location.X, Location.Y);                
                if (menuForm.ShowDialog() == DialogResult.OK)
                {
                    
                }
            }
            LoginInput_textBox.Text = "";
            PasswordInput_textBox.Text = "";
        }

        private void Registration_LinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            registrationForm = new RegistrationForm();
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

        private async void LoginForm_Load(object sender, EventArgs e)
        {
            int result = 0;            
            string str = await dataBaseService.ReadUserTableAsync();
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
        }
    }
}
