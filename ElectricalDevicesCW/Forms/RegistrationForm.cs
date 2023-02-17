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
    public partial class RegistrationForm : Form
    {        
        DataBaseService dataBaseService = new DataBaseService();

        public RegistrationForm()
        {
            InitializeComponent();            
        }

        private async void Registration_Button_Click(object sender, EventArgs e)
        {
            int result = 0;
            string strAddUser = await dataBaseService.AddUserAsync(Login_TextBox.Text, Password_TextBox.Text,"client");
            if (int.TryParse(strAddUser, out result) == true)
            {
                string strReadUser = await dataBaseService.ReadUserTableAsync();
                if (int.TryParse(strReadUser, out result) == true)
                {
                    string strClient = await dataBaseService.AddClientAsync(Name_TextBox.Text, Phone_MaskedTextBox.Text, 0, HumanDataManager.Instance.GetUserId(Login_TextBox.Text, Password_TextBox.Text));
                    if (int.TryParse(strClient, out result) == false)
                    {
                        MessageBox.Show(strClient);
                        return;
                    }
                }
                else
                {
                    MessageBox.Show(strReadUser);
                    return;
                }
            }
            else
            {
                MessageBox.Show(strAddUser);
                return;
            }
            DialogResult = DialogResult.OK;
        }

        private void Cancel_Button_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }
    }
}
