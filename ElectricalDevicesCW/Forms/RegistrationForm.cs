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
            List<Right> rights = new List<Right>();
            rights.Add(new Right(9, "View deviceModel"));
            User newUser = new User(NameNewUser_TextBox.Text, LoginNewUser_TextBox.Text, PasswordNewUser_TextBox.Text, Phone_MaskedTextBox.Text, 0, rights);

            int result = 0;
            string strAddUser = await dataBaseService.AddUserAsync(newUser);
            if (int.TryParse(strAddUser, out result) == true)
            {                
                string strReadUser = await dataBaseService.ReadUserTableAsync();
                if (int.TryParse(strReadUser, out result) == true)
                {
                    string strUserRight = await dataBaseService.AddUserRightAsync(newUser);
                    if (int.TryParse(strUserRight, out result) == false)
                    {
                        MessageBox.Show(strUserRight);
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
