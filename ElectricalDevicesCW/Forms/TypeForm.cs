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
    public partial class TypeForm : Form
    {
        DataBaseService dataBaseService = new DataBaseService();
        int TypeSelectedId = 0;

        public TypeForm()
        {
            InitializeComponent();
            RefreshScreenData();
        }

        private async void Add_Button_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(Name_TextBox.Text) == true) return;

            int result = 0;
            string str = await dataBaseService.AddTypeAsync(Name_TextBox.Text);
            if (int.TryParse(str, out result) == true)
            {
                RefreshScreenData();
            }
            else MessageBox.Show(str);
        }

        private async void Edit_Button_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(Name_TextBox.Text) == true) return;

            int result = 0;
            string str = await dataBaseService.UpdateTypeAsync(Name_TextBox.Text, TypeSelectedId);
            if (int.TryParse(str, out result) == true)
            {
                RefreshScreenData();
            }
            else MessageBox.Show(str);
        }

        private async void Delete_Button_Click(object sender, EventArgs e)
        {
            if (Type_ListBox.SelectedItem == null) return;

            int result = 0;
            string str = await dataBaseService.DeleteTypeAsync(Type_ListBox.SelectedItem.ToString());
            if (int.TryParse(str, out result) == true)
            {
                RefreshScreenData();
            }
            else MessageBox.Show(str);
        }

        private void Type_ListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Type_ListBox.SelectedItem == null) return;
            string[] str = Type_ListBox.SelectedItem.ToString().Split('.');
            TypeSelectedId = int.Parse(str[0]);
            Name_TextBox.Text = str[1];
        }

        public async void RefreshScreenData()
        {
            int result = 0;
            string str = "";
            str = await dataBaseService.ReadTypeTableAsync();

            if (int.TryParse(str, out result) == true)
            {
                Type_ListBox.Items.Clear();
                ModelDataManager.Instance.GetFullDataListTypes().ForEach(s => Type_ListBox.Items.Add(s));
                Name_TextBox.Text = "";
            }
            else MessageBox.Show(str);
        }
    }
}
