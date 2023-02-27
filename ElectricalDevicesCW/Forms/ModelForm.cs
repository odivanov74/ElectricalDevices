using ElectricalDevicesCW.Managers;
using ElectricalDevicesCW.Services;
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

namespace ElectricalDevicesCW.Forms
{
    public partial class ModelForm : Form
    { 
        DataBaseService dataBaseService = new DataBaseService();
        SearchService searchService = new SearchService();

        int ModelSelectedId = 0;

        public ModelForm()
        {
            InitializeComponent();
            Date_DateTimePicker.CustomFormat = "yyyy.MM.dd";
            Date_DateTimePicker.Format = DateTimePickerFormat.Custom;

            Date2_DateTimePicker.CustomFormat = "yyyy.MM.dd";
            Date2_DateTimePicker.Format = DateTimePickerFormat.Custom;

            Date3_DateTimePicker.CustomFormat = "yyyy.MM.dd";
            Date3_DateTimePicker.Format = DateTimePickerFormat.Custom;
        }

        private async void Add_Button_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(Name_TextBox.Text) == true)
            {
                MessageBox.Show("Некорректные данные");
                return;
            }           
            
            int result = 0;
            string str = await dataBaseService.AddModelAsync(Name_TextBox.Text,
                                                            Type_ComboBox.SelectedIndex + 1,
                                                            (int)Weight_NumericUpDown.Value,
                                                            (int)Price_NumericUpDown.Value,                                                           
                                                            Manufacturer_ComboBox.SelectedIndex + 1,
                                                            Supplier_ComboBox.SelectedIndex + 1);
            if (int.TryParse(str, out result) == true)
            {
                RefreshData();
            }
            else MessageBox.Show(str);
        }

        private async void EditModel_Button_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(Name_TextBox.Text) == true)
            {
                MessageBox.Show("Некорректные данные");
                return;
            }

            int result = 0;
            string str = await dataBaseService.UpdateModelAsync(Name_TextBox.Text,
                                                            Type_ComboBox.SelectedIndex + 1,
                                                            (int)Weight_NumericUpDown.Value,
                                                            (int)Price_NumericUpDown.Value,
                                                            (int)StockBalance_NumericUpDown.Value,
                                                            Manufacturer_ComboBox.SelectedIndex + 1,
                                                            Supplier_ComboBox.SelectedIndex + 1,
                                                            (int)Reserved_NumericUpDown.Value,
                                                            (int)Saled_NumericUpDown.Value,
                                                            ModelSelectedId);
            if (int.TryParse(str, out result) == true)
            {
                RefreshData();
            }
            else MessageBox.Show(str);            
        }       

        private async void DelModel_Button_Click(object sender, EventArgs e)
        {
            if (Models_ListBox.SelectedItem == null) return;

            int result = 0;
            string str = await dataBaseService.DeleteModelAsync(Models_ListBox.SelectedItem.ToString());
            if (int.TryParse(str, out result) == true)
            {
                RefreshData();
            }
            else MessageBox.Show(str);
        }

        private async void Sort_Button_Click(object sender, EventArgs e)
        {
            int result = 0;
            string str = "";            
            string direction = Direction_ComboBox.SelectedItem.ToString();

            switch (TypeSort_ComboBox.SelectedItem.ToString())
            {
                case "По дате выпуска":
                    str = await dataBaseService.SortModelTableAsync("manufacture_date", direction, "device", "model");
                    break;
                case "По поставщику":
                    str = await dataBaseService.SortModelTableAsync("supplier_name", direction, "supplier", "supplier");
                    break;
                case "По производителю":
                    str = await dataBaseService.SortModelTableAsync("manufacturer_name", direction, "manufacturer", "manufacturer");
                    break;
                case "По стране производства":
                    str = await dataBaseService.SortModelTableAsync("country_name", direction, "manufacturer", "manufacturer", "country", "country");
                    break;
                case "По весу":
                    str = await dataBaseService.SortModelTableAsync("weight", direction);
                    break;
                case "По наличию":
                    str = await dataBaseService.SortModelTableAsync("stock_balance", direction);
                    break;
                case "По резервам":
                    str = await dataBaseService.SortModelTableAsync("reserved", direction);
                    break;
            }


            if (int.TryParse(str, out result) == true)
            {
                Models_ListBox.Items.Clear();
                ModelDataManager.Instance.GetFullDataListModel(TypeSort_ComboBox.SelectedItem.ToString()).ForEach(m => Models_ListBox.Items.Add(m));
                ClearModelInfo();
            }
        }

        private async void Search_Button_Click(object sender, EventArgs e)
        {
            int result = 0;
            string str = "";

            string valueSelect1 = ValueSearch_ComboBox.SelectedItem.ToString();
            string valueSelect2 = ValueSearch2_ComboBox.SelectedItem.ToString();
            string valueSelect3 = ValueSearch3_ComboBox.SelectedItem.ToString();

            string paramString1 = ParamSearch_ComboBox.SelectedItem.ToString();
            string paramString2 = ParamSearch2_ComboBox.SelectedItem.ToString();
            string paramString3 = ParamSearch3_ComboBox.SelectedItem.ToString();

            string partAreaSearch = UpPartFraction_ComboBox.SelectedItem.ToString();
            string allAreaSearch = DownPartFraction_ComboBox.SelectedItem.ToString();

            str = await searchService.Search(valueSelect1, paramString1,
                                             valueSelect2, paramString2,
                                             valueSelect3, paramString3,
                                             partAreaSearch, allAreaSearch,
                                             Part1_CheckBox.Checked, Part2_CheckBox.Checked, Part3_CheckBox.Checked,
                                             All1_CheckBox.Checked, All2_CheckBox.Checked, All3_CheckBox.Checked,
                                             Value_TextBox.Text, StartValue_TextBox.Text, EndValue_TextBox.Text,
                                             Date_DateTimePicker, StartDate_DateTimePicker, EndDate_DateTimePicker,
                                             isDefected_RadioButton.Checked);

            if (int.TryParse(str, out result) == true)
            {
                if (paramString1 == "Среднее")
                {
                    Models_ListBox.Items.Clear();
                    Models_ListBox.Items.Add(ModelDataManager.Instance.GetValueField("Среднее_значение"));
                }
                else
                {
                    RefreshModelListBox();
                }

            }
            else
            {
                MessageBox.Show(str);
                Models_ListBox.Items.Clear();
            }

            Search_TextBox.Text = "Поиск:" + Environment.NewLine + searchService.CmdSearch;
            Fraction_TextBox.Text = "Доля:" + Environment.NewLine +
                                    searchService.CmdPartFraction + Environment.NewLine +
                                    "От:" + Environment.NewLine + searchService.CmdAllFraction;
            Quantity_TextBox.Text = "Количество:" + Environment.NewLine + searchService.CmdQuantity;
        }

        private void ResetSort_Button_Click(object sender, EventArgs e)
        {
            RefreshData();
            TypeSort_ComboBox.SelectedIndex = 0;
            Direction_ComboBox.SelectedIndex = 0;
        }

        private void ResetSearch_Button_Click(object sender, EventArgs e)
        {
            RefreshData();
            ValueSearch_ComboBox.SelectedIndex = 0;
            ValueSearch2_ComboBox.SelectedIndex = 0;
            ValueSearch3_ComboBox.SelectedIndex = 0;

            ParamSearch_ComboBox.SelectedIndex = 0;
            ParamSearch2_ComboBox.SelectedIndex = 0;
            ParamSearch3_ComboBox.SelectedIndex = 0;

            Search2_GroupBox.Visible = false;
            Search3_GroupBox.Visible = false;
        }



        private void AddSearch_Button_Click(object sender, EventArgs e)
        {
            if (ParamSearch_ComboBox.SelectedIndex == 0) return;
            Search2_GroupBox.Visible = true;
            ValueSearch2_ComboBox.SelectedIndex = 0;
            Part2_CheckBox.Checked = true;
        }

        private void AddSearch2_Button_Click(object sender, EventArgs e)
        {
            if (ParamSearch2_ComboBox.SelectedIndex == 0) return;
            Search3_GroupBox.Visible = true;
            ValueSearch3_ComboBox.SelectedIndex = 0;
            Part3_CheckBox.Checked = true;
        }

        private void SubSearch2_Button_Click(object sender, EventArgs e)
        {
            ValueSearch2_ComboBox.SelectedIndex = 0;
            ValueSearch3_ComboBox.SelectedIndex = 0;

            ParamSearch2_ComboBox.SelectedIndex = 0;
            ParamSearch3_ComboBox.SelectedIndex = 0;

            All2_CheckBox.Checked = false;
            All3_CheckBox.Checked = false;

            Search2_GroupBox.Visible = false;
            Search3_GroupBox.Visible = false;
        }

        private void SubSearch3_Button_Click(object sender, EventArgs e)
        {
            ValueSearch3_ComboBox.SelectedIndex = 0;
            ParamSearch3_ComboBox.SelectedIndex = 0;
            All3_CheckBox.Checked = false;
            Search3_GroupBox.Visible = false;
        }



        public void RefreshModelListBox()
        {
            Models_ListBox.Items.Clear();
            ModelDataManager.Instance.GetFullDataListModel("").ForEach(m => Models_ListBox.Items.Add(m));
            if (Models_ListBox.Items.Count > 0)
            {
                //if(ModelDataManager.Instance.PartFraction != null) 
                int partFraction = ModelDataManager.Instance.GetPartFractionModel();
                int allFraction = ModelDataManager.Instance.GetAllFractionModel();
                Models_ListBox.Items.Add($"\n Доля = {partFraction}/{allFraction} = " + (((float)partFraction/allFraction)*100).ToString("#.##")+"%");
                if (ModelDataManager.Instance.Quantity != null) Models_ListBox.Items.Add($"\n Количество = {ModelDataManager.Instance.GetQuantityModel()}");
            }
            ClearModelInfo();
        }

        private void Models_ListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            int result = 0;
            if (Models_ListBox.SelectedItem == null) return;
            string[] str = Models_ListBox.SelectedItem.ToString().Split('.');
            if (int.TryParse(str[0], out result) == false) return;
            ModelSelectedId = result;
            Name_TextBox.Text = str[1];
            Type_ComboBox.SelectedIndex = int.Parse(str[2])-1;
            Weight_NumericUpDown.Value = int.Parse(str[3]);
            Price_NumericUpDown.Value = int.Parse(str[4]);
            StockBalance_NumericUpDown.Value = int.Parse(str[5]);
            Manufacturer_ComboBox.SelectedIndex = int.Parse(str[6])-1;
            Supplier_ComboBox.SelectedIndex = int.Parse(str[7])-1;
            Reserved_NumericUpDown.Value = int.Parse(str[8]);
            Saled_NumericUpDown.Value = int.Parse(str[9]);
            Country_TextBox.Text = ModelDataManager.Instance.GetNameCountry(int.Parse(str[6]));
            //ManufactureDate_DateTimePicker.Value = ModelDataManager.Instance.GetDateManufactureDevice(deviceSelectedId);
        }    


        

        private void ValueSearch_ComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {            
            if (ValueSearch_ComboBox.SelectedItem == null) return;
            ParamSearch_ComboBox.Items.Clear();
            ParamSearch_ComboBox.Items.Add("-");
            switch (ValueSearch_ComboBox.SelectedItem.ToString())
            {
                case "price":
                case "weight":
                    ParamSearch_ComboBox.Items.Add("Значение");
                    ParamSearch_ComboBox.Items.Add("Максимум");
                    ParamSearch_ComboBox.Items.Add("Минимум");
                    ParamSearch_ComboBox.Items.Add("Среднее");
                    ParamSearch_ComboBox.Items.Add("Больше");
                    ParamSearch_ComboBox.Items.Add("Больше среднего");
                    ParamSearch_ComboBox.Items.Add("Меньше");
                    ParamSearch_ComboBox.Items.Add("Меньше среднего");
                    ParamSearch_ComboBox.Items.Add("Диапазон");                    
                    break;
                case "manufacture_date":
                case "order_date":
                    ParamSearch_ComboBox.Items.Add("Значение");
                    ParamSearch_ComboBox.Items.Add("Больше");
                    ParamSearch_ComboBox.Items.Add("Меньше");
                    ParamSearch_ComboBox.Items.Add("Диапазон");                   
                    break;
                case "type":
                    ModelDataManager.Instance.GetNameListType().ForEach(t => ParamSearch_ComboBox.Items.Add(t));                    
                    break;
                case "manufacturer":
                    ModelDataManager.Instance.GetNameListManufacturers().ForEach(m => ParamSearch_ComboBox.Items.Add(m));                    
                    break;
                case "country":
                    ModelDataManager.Instance.GetNameListCountries().ForEach(s => ParamSearch_ComboBox.Items.Add(s));                   
                    break;
                case "supplier":
                    ModelDataManager.Instance.GetNameListSuppliers().ForEach(s => ParamSearch_ComboBox.Items.Add(s));                    
                    break;
                case "defected":
                    ParamSearch_ComboBox.Items.Add("Значение");
                    break;
                case "quantity":
                    ParamSearch_ComboBox.Items.Add("Значение");                    
                    ParamSearch_ComboBox.Items.Add("Максимум");
                    ParamSearch_ComboBox.Items.Add("Минимум");
                    ParamSearch_ComboBox.Items.Add("Больше");
                    ParamSearch_ComboBox.Items.Add("Меньше");
                    ParamSearch_ComboBox.Items.Add("Диапазон");
                    break;
            }
            ParamSearch_ComboBox.SelectedIndex = 0;
        }

        private void ValueSearch2_ComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {            
            if (ValueSearch2_ComboBox.SelectedItem == null) return;
            ParamSearch2_ComboBox.Items.Clear();
            ParamSearch2_ComboBox.Items.Add("-");
            switch (ValueSearch2_ComboBox.SelectedItem.ToString())
            {
                case "type":
                    ModelDataManager.Instance.GetNameListType().ForEach(t => ParamSearch2_ComboBox.Items.Add(t));                    
                    break;
                case "manufacturer":
                    ModelDataManager.Instance.GetNameListManufacturers().ForEach(m => ParamSearch2_ComboBox.Items.Add(m));                    
                    break;
                case "country":
                    ModelDataManager.Instance.GetNameListCountries().ForEach(s => ParamSearch2_ComboBox.Items.Add(s));                   
                    break;
                case "supplier":
                    ModelDataManager.Instance.GetNameListSuppliers().ForEach(s => ParamSearch2_ComboBox.Items.Add(s));                    
                    break;
                default:
                    Value2_GroupBox.Visible = false;
                    Date2_GroupBox.Visible = false;
                    break;
            }
            ParamSearch2_ComboBox.SelectedIndex = 0;
        }

        private void ValueSearch3_ComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ValueSearch3_ComboBox.SelectedItem == null) return;
            ParamSearch3_ComboBox.Items.Clear();
            ParamSearch3_ComboBox.Items.Add("-");
            switch (ValueSearch3_ComboBox.SelectedItem.ToString())
            {
                case "type":
                    ModelDataManager.Instance.GetNameListType().ForEach(t => ParamSearch3_ComboBox.Items.Add(t));                    
                    break;
                case "manufacturer":
                    ModelDataManager.Instance.GetNameListManufacturers().ForEach(m => ParamSearch3_ComboBox.Items.Add(m));                    
                    break;
                case "country":
                    ModelDataManager.Instance.GetNameListCountries().ForEach(s => ParamSearch3_ComboBox.Items.Add(s));                    
                    break;
                case "supplier":
                    ModelDataManager.Instance.GetNameListSuppliers().ForEach(s => ParamSearch3_ComboBox.Items.Add(s));                   
                    break;
                default:
                    Value3_GroupBox.Visible = false;
                    Date3_GroupBox.Visible = false;
                    break;
            }
            ParamSearch3_ComboBox.SelectedIndex = 0;
        }

        private void ParamSearch_ComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            VisibleControl(ParamSearch_ComboBox, ValueSearch_ComboBox, Value_GroupBox, Date_GroupBox, Value_TextBox, Date_DateTimePicker, NotDefected_RadioButton, isDefected_RadioButton);
        }

        private void ParamSearch2_ComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            VisibleControl(ParamSearch2_ComboBox, ValueSearch2_ComboBox, Value2_GroupBox, Date2_GroupBox, Value2_TextBox, Date2_DateTimePicker);
        }

        private void ParamSearch3_ComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            VisibleControl(ParamSearch3_ComboBox, ValueSearch3_ComboBox, Value3_GroupBox, Date3_GroupBox, Value3_TextBox, Date3_DateTimePicker);
        }


        public void ClearModelInfo()
        {
            Name_TextBox.Text = "";
            Type_ComboBox.SelectedIndex = 0;
            Weight_NumericUpDown.Value = 1;
            Price_NumericUpDown.Value = 1;
            StockBalance_NumericUpDown.Value = 0;
            Reserved_NumericUpDown.Value = 0;
            Saled_NumericUpDown.Value = 0;
            Manufacturer_ComboBox.SelectedIndex = 0;
            Supplier_ComboBox.SelectedIndex = 0;
        }

        public async void RefreshData()
        {
            int result = 0;
            string str = "";
            str = await dataBaseService.ReadModelTableAsync();

            if (int.TryParse(str, out result) == true)
            {
                Models_ListBox.Items.Clear();
                ModelDataManager.Instance.GetFullDataListModel("").ForEach(m => Models_ListBox.Items.Add(m));
                ClearModelInfo();
            }
            else MessageBox.Show(str);
        }

        public void VisibleControl(ComboBox paramComboBox, ComboBox valueComboBox, GroupBox valueGroupBox, 
                                    GroupBox dateGroupBox, TextBox valueTextBox, DateTimePicker dateTimePicker, 
                                    RadioButton notDefectedRadioButton = null, RadioButton defectedRadioButton= null)
        {
            switch (paramComboBox.SelectedItem.ToString())
            {
                case "Значение":
                case "Больше":
                case "Меньше":
                    switch (valueComboBox.SelectedItem.ToString())
                    {
                        case "price":
                        case "weight":
                        case "quantity":
                            valueGroupBox.Visible = true;
                            valueTextBox.Visible = true;
                            dateGroupBox.Visible = false;
                            notDefectedRadioButton.Visible = false;
                            defectedRadioButton.Visible = false;

                            break;
                        case "manufacture_date":
                        case "order_date":
                            valueGroupBox.Visible = false;
                            dateGroupBox.Visible = true;
                            dateTimePicker.Visible = true;
                            notDefectedRadioButton.Visible = false;
                            defectedRadioButton.Visible = false;
                            break;
                        case "defected":
                            valueGroupBox.Visible = false;
                            dateGroupBox.Visible = false;
                            notDefectedRadioButton.Visible = true;
                            defectedRadioButton.Visible = true;
                            notDefectedRadioButton.Checked = true;
                            break;
                    }
                    break;
                case "Диапазон":
                    switch (ValueSearch_ComboBox.SelectedItem.ToString())
                    {
                        case "price":
                        case "weight":
                        case "quantity":
                            valueGroupBox.Visible = true;
                            valueTextBox.Visible = false;
                            dateGroupBox.Visible = false;
                            notDefectedRadioButton.Visible = false;
                            defectedRadioButton.Visible = false;
                            break;
                        case "manufacture_date":
                        case "order_date":
                            valueGroupBox.Visible = false;
                            dateGroupBox.Visible = true;
                            dateTimePicker.Visible = false;
                            notDefectedRadioButton.Visible = false;
                            defectedRadioButton.Visible = false;
                            break;
                    }
                    break;

                    

                default:
                    valueGroupBox.Visible = false;
                    dateGroupBox.Visible = false;
                    if (notDefectedRadioButton != null)
                    {
                        notDefectedRadioButton.Visible = false;
                        defectedRadioButton.Visible = false;
                    }                  
                    break;
            }
        }

        private async void ModelForm_Load(object sender, EventArgs e)
        {
            int result = 0;
            string str = await dataBaseService.ReadModelTableAsync();
            if (int.TryParse(str, out result) == false)
            {
                MessageBox.Show(str);
                return;
            }

            str = await dataBaseService.ReadTypeTableAsync();
            if (int.TryParse(str, out result) == false)
            {
                MessageBox.Show(str);
                return;
            }

            str = await dataBaseService.ReadManufacturerTableAsync();
            if (int.TryParse(str, out result) == false)
            {
                MessageBox.Show(str);
                return;
            }

            str = await dataBaseService.ReadCountryTableAsync();
            if (int.TryParse(str, out result) == false)
            {
                MessageBox.Show(str);
                return;
            }

            str = await dataBaseService.ReadSupplierTableAsync();
            if (int.TryParse(str, out result) == false)
            {
                MessageBox.Show(str);
                return;
            }

            str = await dataBaseService.ReadModelOrderTableAsync();
            if (int.TryParse(str, out result) == false)
            {
                MessageBox.Show(str);
                return;
            }

            StockBalance_NumericUpDown.Enabled = false;
            Reserved_NumericUpDown.Enabled = false;
            Saled_NumericUpDown.Enabled = false;

            RefreshData();
            ModelDataManager.Instance.GetNameListTypes().ForEach(m => Type_ComboBox.Items.Add(m));
            Type_ComboBox.SelectedIndex = 0;

            ModelDataManager.Instance.GetNameListManufacturers().ForEach(m => Manufacturer_ComboBox.Items.Add(m));
            Manufacturer_ComboBox.SelectedIndex = 0;

            ModelDataManager.Instance.GetNameListSuppliers().ForEach(s => Supplier_ComboBox.Items.Add(s));
            Supplier_ComboBox.SelectedIndex = 0;
            
            Direction_ComboBox.SelectedIndex = 0;
            TypeSort_ComboBox.SelectedIndex = 0;
            
            ValueSearch_ComboBox.SelectedIndex = 0;
            Part1_CheckBox.Checked = true;

            Search2_GroupBox.Visible = false;
            Search3_GroupBox.Visible = false;

            NotDefected_RadioButton.Visible = false;
            isDefected_RadioButton.Visible = false;

            UpPartFraction_ComboBox.SelectedIndex = 0;
            DownPartFraction_ComboBox.SelectedIndex = 0;

            ValueSearch2_ComboBox.Items.Add("-");
            ValueSearch2_ComboBox.SelectedIndex = 0;

            ValueSearch3_ComboBox.Items.Add("-");
            ValueSearch3_ComboBox.SelectedIndex = 0;

            Search_TextBox.Visible = false;
            Fraction_TextBox.Visible = false;
            Quantity_TextBox.Visible = false;
        }

        private void CmdVisible_Button_Click(object sender, EventArgs e)
        {
            if(Search_TextBox.Visible)
            {
                Search_TextBox.Visible = false;
                Fraction_TextBox.Visible = false;
                Quantity_TextBox.Visible = false;
            }
            else
            {
                Search_TextBox.Visible = true;
                Fraction_TextBox.Visible = true;
                Quantity_TextBox.Visible = true;
            }
        }        
    }
}
