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
        } 

        private async void AddUser_Button_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(Password_TextBox.Text) == true ||
                string.IsNullOrWhiteSpace(Login_TextBox.Text) == true ||
                (Role_ComboBox.SelectedItem.ToString() == "client" && string.IsNullOrWhiteSpace(Name_TextBox.Text) == true) ||
                (Role_ComboBox.SelectedItem.ToString() == "client" && string.IsNullOrWhiteSpace(Phone_MaskedTextBox.Text) == true))
            {
                MessageBox.Show("Некорректные данные");
                return;
            } 

            int result = 0;
            string strUser = await dataBaseService.AddUserAsync(Login_TextBox.Text,
                                    Password_TextBox.Text,
                                    Role_ComboBox.SelectedItem.ToString());
            if (int.TryParse(strUser, out result) == true)
            {
                string strReadUser = await dataBaseService.ReadUserTableAsync();
                if (int.TryParse(strReadUser, out result) == true)
                {
                    if (Role_ComboBox.SelectedItem.ToString() == "client")
                    {
                        string strClient = await dataBaseService.AddClientAsync(Name_TextBox.Text,
                                                                                Phone_MaskedTextBox.Text,
                                                                                (int)Discount_NumericUpDown.Value,
                                                                                HumanDataManager.Instance.GetLastUserId());
                        if (int.TryParse(strClient, out result) == false)
                        {
                            MessageBox.Show(strClient);
                        }
                    }
                    RefreshData();
                }
                else
                {
                    MessageBox.Show(strReadUser);
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
            if (string.IsNullOrWhiteSpace(Password_TextBox.Text) == true ||
                string.IsNullOrWhiteSpace(Login_TextBox.Text) == true ||
                (Role_ComboBox.SelectedItem.ToString() == "client" && string.IsNullOrWhiteSpace(Name_TextBox.Text) == true) ||
                (Role_ComboBox.SelectedItem.ToString() == "client" && string.IsNullOrWhiteSpace(Phone_MaskedTextBox.Text) == true))
            {
                MessageBox.Show("Некорректные данные");
                return;
            }

            if (Login_TextBox.Text == "Admin") return;


            int result = 0;
            string strUser = await dataBaseService.UpdateUserAsync( userSelectedId,
                                                                    Login_TextBox.Text,
                                                                    Password_TextBox.Text,
                                                                    Role_ComboBox.SelectedItem.ToString());
            if (int.TryParse(strUser, out result) == true)
            {
                if (Role_ComboBox.SelectedItem.ToString() == "client")
                {
                    string strClient = await dataBaseService.UpdateClientAsync(HumanDataManager.Instance.GetClientId(userSelectedId),
                                                                                Name_TextBox.Text,
                                                                                Phone_MaskedTextBox.Text,
                                                                                (int)Discount_NumericUpDown.Value,
                                                                                userSelectedId);
                    if (int.TryParse(strClient, out result) == false)
                    {
                        MessageBox.Show(strClient);
                    }
                }
                RefreshData();                
            }
            else
            {
                MessageBox.Show(strUser);
                return;
            }  
        }       
                
        private void Users_ListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Users_ListBox.SelectedItem == null) return;
            listBoxSelectedId = Users_ListBox.SelectedIndex;

            List<string> list = new List<string>();
            string[] userInfo = Users_ListBox.Items[listBoxSelectedId].ToString().Split('.');
            userSelectedId = int.Parse(userInfo[0]);

            Login_TextBox.Text = userInfo[1];
            Password_TextBox.Text = userInfo[2];
            Role_ComboBox.SelectedItem = userInfo[3];

            if(Role_ComboBox.SelectedItem.ToString() == "client")
            {
                Name_TextBox.Text = userInfo[4];
                Phone_MaskedTextBox.Text = userInfo[5];
                Discount_NumericUpDown.Value = int.Parse(userInfo[6]);
                Role_ComboBox.Enabled = false;
            }
            else
            {
                Name_TextBox.Text = "";
                Phone_MaskedTextBox.Text = "";
                Discount_NumericUpDown.Value = 0;
                Role_ComboBox.Enabled = true;
            }
        }

        private async void DeleteUser_Button_Click(object sender, EventArgs e)
        {
            if (Users_ListBox.SelectedItem == null) return;
            string[] arrayStr = Users_ListBox.SelectedItem.ToString().Split('.');
            if (arrayStr[1] == "Admin") return;
            int result = 0;

            User user = HumanDataManager.Instance.GetUser(arrayStr[1], arrayStr[2]);

            string strUser = await dataBaseService.DeleteUserAsync(Users_ListBox.SelectedItem.ToString());
            if (int.TryParse(strUser, out result) == true)
            {
                RefreshData();
            }
            else MessageBox.Show(strUser);
        }

        public async void RefreshData()
        {
            int result = 0;
            string str = "";
            str = await dataBaseService.ReadUserTableAsync();

            if (int.TryParse(str, out result) == true)
            {
                str = await dataBaseService.ReadClientTableAsync();
                if (int.TryParse(str, out result) == true)
                {
                    Users_ListBox.Items.Clear();
                    HumanDataManager.Instance.GetFullDataListUsers().ForEach(u => Users_ListBox.Items.Add(u));
                }              
                
                Password_TextBox.Text = "";
                Login_TextBox.Text = "";
                Name_TextBox.Text = "";
                Phone_MaskedTextBox.Text = "";
                Discount_NumericUpDown.Value = 0;
                Role_ComboBox.Enabled = true;
            }
            else
            {
                MessageBox.Show(str);
                return;
            }            
        }

        private async void UsersForm_Load(object sender, EventArgs e)
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


            HumanDataManager.Instance.GetRoleList().ForEach(r => Role_ComboBox.Items.Add(r));
            Role_ComboBox.SelectedIndex = 0;

            RefreshData();
        }
    }
}
