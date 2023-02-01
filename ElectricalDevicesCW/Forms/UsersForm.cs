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
    public partial class UsersForm : Form
    {
        UserManager userManager;
        User user;
        int userIdSelected = 0;

        public UsersForm(User user, UserManager userManager)
        {
            InitializeComponent();
            this.user = user;
            this.userManager = userManager;        
            RefreshUsersListBox();
            RefreshAllRightList();
        } 

        private async void AddUser_Button_Click(object sender, EventArgs e)
        { 
            foreach (var item in DestinationRight_ListBox.Items)
            {
                string[] result = (item as string).Split('.');
                user.Rights.Add(new Right(int.Parse(result[0]), result[1]));                
            }

            User newUser = new User(NameNewUser_TextBox.Text, LoginNewUser_TextBox.Text, PasswordNewUser_TextBox.Text, Phone_MaskedTextBox.Text, (int)Discount_NumericUpDown.Value, user.Rights);
            await userManager.AddUserAsync(newUser);

            await userManager.LoadUserBaseAsync();
            RefreshUsersListBox();
            ClearUserInfo();
        }

        private async void EditUser_Button_Click(object sender, EventArgs e)
        {
            if (Users_ListBox.SelectedItem.ToString().Split('.')[1] == "Owner") return;
            List<Right> rights = new List<Right>();
            foreach (var item in DestinationRight_ListBox.Items)
            {
                string[] result = (item as string).Split('.');
                rights.Add(new Right(int.Parse(result[0]), result[1]));
            }

            User editUser = new User(userIdSelected, NameNewUser_TextBox.Text, LoginNewUser_TextBox.Text, PasswordNewUser_TextBox.Text, Phone_MaskedTextBox.Text, (int)Discount_NumericUpDown.Value, rights);
            if (await userManager.EditUserAsync(editUser) == true)
            {
                await userManager.EditUserRightAsync(editUser);
            }

            await userManager.LoadUserBaseAsync();
            RefreshUsersListBox();
            ClearUserInfo();
        }

        private void SourceRight_ListBox_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (SourceRight_ListBox.SelectedIndex != -1)
            {
                foreach (string item in DestinationRight_ListBox.Items)
                {
                    if (SourceRight_ListBox.SelectedItem.ToString() == item.ToString()) return;
                }
                DestinationRight_ListBox.Items.Add($"{SourceRight_ListBox.SelectedIndex+1}.{SourceRight_ListBox.SelectedItem.ToString()}");
            }
        }

        private void DestinationRight_ListBox_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (DestinationRight_ListBox.SelectedIndex != -1)
            {
                DestinationRight_ListBox.Items.Remove(DestinationRight_ListBox.SelectedItem);
            }
        }

        public void RefreshUsersListBox()
        {
            Users_ListBox.Items.Clear();
            userManager.GetListUser().ForEach(u => Users_ListBox.Items.Add(u));
        }

        private async void Users_ListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            DestinationRight_ListBox.Items.Clear();
            if (Users_ListBox.SelectedItem == null) return;
            string[] str = Users_ListBox.SelectedItem.ToString().Split('.');
            userIdSelected = int.Parse(str[0]);
            List<string> list = await userManager.GetListRightUser(userIdSelected);
            list.ForEach(r => DestinationRight_ListBox.Items.Add(r));
            NameNewUser_TextBox.Text = str[1];
            LoginNewUser_TextBox.Text = str[2];
            PasswordNewUser_TextBox.Text = str[3];
            Phone_MaskedTextBox.Text = str[4];
            Discount_NumericUpDown.Value = int.Parse(str[5]);
            AddUser_Button.Enabled = false;
            EditUser_Button.Enabled = true;
        }

        public async void RefreshAllRightList()
        {
            IEnumerable<string> list = await userManager.LoadAllRightAsync();
            SourceRight_ListBox.Items.AddRange(list.ToArray());
        }

        private async void DeleteUser_Button_Click(object sender, EventArgs e)
        {
            if (Users_ListBox.SelectedItem.ToString().Split('.')[1] == "Owner") return;
            string userInfo = Users_ListBox.SelectedItem.ToString();
            if (await userManager.DeleteUserRightAsync(userInfo) == true)
            {
                await userManager.DelUserAsync(userInfo);
                RefreshUsersListBox();
                ClearUserInfo();
            }
        }

        public void ClearUserInfo()
        {
            NameNewUser_TextBox.Text = "";
            LoginNewUser_TextBox.Text = "";
            PasswordNewUser_TextBox.Text = "";
            Phone_MaskedTextBox.Text = "";
            Discount_NumericUpDown.Value = 0;
            AddUser_Button.Enabled = true;
            EditUser_Button.Enabled = false;
            userIdSelected = 0;
            DestinationRight_ListBox.Items.Clear();
        }

        private void ClearInfo_Button_Click(object sender, EventArgs e)
        {
            ClearUserInfo();
        }
    }
}
