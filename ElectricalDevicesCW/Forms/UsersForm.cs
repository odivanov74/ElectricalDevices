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
    public partial class UsersForm : Form
    {        
        User user;        
        DataBaseService dataBaseService = new DataBaseService();
        int userSelectedId = 0;
        int listBoxSelectedId = 0;

        public UsersForm(User user)
        {
            InitializeComponent();
            this.user = user;
            RefreshScreenData();
            RefreshAllRightList(); 
        } 

        private async void AddUser_Button_Click(object sender, EventArgs e)
        {
            if (Name_TextBox.Text == "Owner" || 
                string.IsNullOrWhiteSpace(Name_TextBox.Text) == true ||
                string.IsNullOrWhiteSpace(Login_TextBox.Text) == true ||
                string.IsNullOrWhiteSpace(Password_TextBox.Text) == true ||
                string.IsNullOrWhiteSpace(Phone_MaskedTextBox.Text) == true ||
                DestinationRight_ListBox.Items.Count == 0)
            {
                MessageBox.Show("Некорректные данные");
                return;
            }
                
            foreach (var item in DestinationRight_ListBox.Items)
            {
                string[] strRights = (item as string).Split('.');
                user.Rights.Add(new Right(int.Parse(strRights[0]), strRights[1]));                
            }

            User newUser = new User(Name_TextBox.Text, 
                                    Login_TextBox.Text, 
                                    Password_TextBox.Text, 
                                    Phone_MaskedTextBox.Text, 
                                    (int)Discount_NumericUpDown.Value,
                                    user.Rights);

            int result = 0;
            string strUser = await dataBaseService.AddUserAsync(newUser);

            if (int.TryParse(strUser, out result) == true)
            {
                RefreshScreenData();
                string strUserRight = await dataBaseService.AddUserRightAsync(newUser);
                if (int.TryParse(strUserRight, out result) == false)
                {
                    MessageBox.Show(strUserRight);
                    return;
                }                
            }
            else
            {
                MessageBox.Show(strUser);
                return;
            }
        }

        private async void EditUser_Button_Click(object sender, EventArgs e)
        {
            if (Name_TextBox.Text == "Owner" ||
                string.IsNullOrWhiteSpace(Name_TextBox.Text) == true ||
                string.IsNullOrWhiteSpace(Login_TextBox.Text) == true ||
                string.IsNullOrWhiteSpace(Password_TextBox.Text) == true ||
                string.IsNullOrWhiteSpace(Phone_MaskedTextBox.Text) == true ||
                DestinationRight_ListBox.Items.Count == 0)
            {
                MessageBox.Show("Некорректные данные");
                return;
            }

            List<Right> rights = new List<Right>();
            foreach (var item in DestinationRight_ListBox.Items)
            {
                string[] resultStr = (item as string).Split('.');
                rights.Add(new Right(int.Parse(resultStr[0]), resultStr[1]));

            }

            User editUser = new User(   userSelectedId, 
                                        Name_TextBox.Text, 
                                        Login_TextBox.Text, 
                                        Password_TextBox.Text, 
                                        Phone_MaskedTextBox.Text, 
                                        (int)Discount_NumericUpDown.Value, 
                                        rights);
            int result = 0;
            string strUser = await dataBaseService.UpdateUserAsync(editUser);

            if (int.TryParse(strUser, out result) == true)
            {
                RefreshScreenData();
                string strUserRight = await dataBaseService.UpdateUserRightAsync(editUser);
                if (int.TryParse(strUserRight, out result) == false)
                {
                    MessageBox.Show(strUserRight);
                    return;
                }
            }
            else
            {
                MessageBox.Show(strUser);
                return;
            }  
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

        private async void Users_ListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Users_ListBox.SelectedItem == null) return;
            DestinationRight_ListBox.Items.Clear();
            listBoxSelectedId = Users_ListBox.SelectedIndex;

            List<string> list = new List<string>();
            string[] userInfo = Users_ListBox.Items[listBoxSelectedId].ToString().Split('.');
            userSelectedId = int.Parse(userInfo[0]);

            string str = await dataBaseService.ReadUserRightTableAsync();
            int result = 0;

            if (int.TryParse(str, out result) == true)
            {
                list = RightDataManager.Instance.GetNameListRightsUser(UserRightDataManager.Instance.GetUserRightsId(userSelectedId));
            }
            else
            {
                MessageBox.Show(str);
                return;
            }


                      
            list.ForEach(r => DestinationRight_ListBox.Items.Add(r));
            Name_TextBox.Text = userInfo[1];
            Login_TextBox.Text = userInfo[2];
            Password_TextBox.Text = userInfo[3];
            Phone_MaskedTextBox.Text = userInfo[4];
            Discount_NumericUpDown.Value = int.Parse(userInfo[5]);           
        }

        public async void RefreshAllRightList()
        {
            string str = await dataBaseService.ReadRightTableAsync();
            int result = 0;

            if (int.TryParse(str, out result) == true)
            {
                RightDataManager.Instance.GetFullDataListRights().ForEach(r => SourceRight_ListBox.Items.Add(r));
            }
            else
            {
                MessageBox.Show(str);
                return;
            }
        }

        private async void DeleteUser_Button_Click(object sender, EventArgs e)
        {
            if (Users_ListBox.SelectedItem == null) return;
            string[] arrayStr = Users_ListBox.SelectedItem.ToString().Split('.');
            if (arrayStr[1] == "Owner") return;
            int result = 0;

            User user = UserDataManager.Instance.GetUser(arrayStr[2], arrayStr[3]);

            string strRights = await dataBaseService.DeleteUserRightTableAsync(user);
            if (int.TryParse(strRights, out result) == true)
            {
                string strUser = await dataBaseService.DeleteUserAsync(Users_ListBox.SelectedItem.ToString());
                if (int.TryParse(strUser, out result) == true)
                {
                    RefreshScreenData();
                }
                else MessageBox.Show(strUser);
            }
            else MessageBox.Show(strRights); 
        }

        public async void RefreshScreenData()
        {
            int result = 0;
            string str = "";
            str = await dataBaseService.ReadUserTableAsync();

            if (int.TryParse(str, out result) == true)
            {
                Users_ListBox.Items.Clear();
                UserDataManager.Instance.GetFullDataListUsers().ForEach(u => Users_ListBox.Items.Add(u));
                DestinationRight_ListBox.Items.Clear();
                Name_TextBox.Text = "";
                Login_TextBox.Text = "";
                Password_TextBox.Text = "";
                Phone_MaskedTextBox.Text = "";
            }
            else
            {
                MessageBox.Show(str);
                return;
            }            
        }
    }
}
