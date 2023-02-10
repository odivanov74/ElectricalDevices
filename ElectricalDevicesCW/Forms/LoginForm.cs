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
        DataBaseService dataBaseService = new DataBaseService();

        ShopForm shopForm;
        MenuForm menuForm;
        RegistrationForm registrationForm;

        public LoginForm()
        {
            InitializeComponent();
            //connection = new SqlConnection(@"Data Source=DESKTOP-MHB46B8\SQLEXPRESS;Initial catalog=ElectricalDevices_v7;Integrated Security=true;");

        }        

        private void Login_button_Click(object sender, EventArgs e)
        {
            User user = UserDataManager.Instance.GetUser(LoginInput_textBox.Text, PasswordInput_textBox.Text);
            if (user == null)
            {
                MessageBox.Show("Неверный login или password, повторите ввод!");
                return;
            }
            else if(user.Rights.Count==1 && user.Rights[0].Name == "View deviceModel")
            {               
                shopForm = new ShopForm(user);
                shopForm.StartPosition = FormStartPosition.Manual;
                shopForm.Location = new Point(Location.X,Location.Y);
                if(shopForm.ShowDialog() == DialogResult.OK)
                {
                    
                }
            }
            else
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

            str = await dataBaseService.ReadUserRightTableAsync();
            if (int.TryParse(str, out result) == false)
            {
                MessageBox.Show(str);
                return;
            }

            str = await dataBaseService.ReadRightTableAsync();
            if (int.TryParse(str, out result) == false)
            {
                MessageBox.Show(str);
                return;
            }
        }
    }
}
