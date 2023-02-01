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
        UserManager userManager;
        public RegistrationForm(UserManager userManager)
        {
            InitializeComponent();
            this.userManager = userManager;
        }

        private async void Registration_Button_Click(object sender, EventArgs e)
        {
            List<Right> rights = new List<Right>();
            rights.Add(new Right(12, "View type product"));
            User newUser = new User(NameNewUser_TextBox.Text, LoginNewUser_TextBox.Text, PasswordNewUser_TextBox.Text, Phone_MaskedTextBox.Text, 0, rights);
            if(await userManager.AddUserAsync(newUser)==false)
            {
                MessageBox.Show("Регистрация не выполнена, возможно такой пользователь уже есть");
            }
            else DialogResult = DialogResult.OK;
        }

        private void Cancel_Button_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }
    }
}
