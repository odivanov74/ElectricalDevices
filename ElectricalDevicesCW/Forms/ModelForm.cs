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

namespace ElectricalDevicesCW.Forms
{
    public partial class ModelForm : Form
    { 
        DataBaseService dataBaseService = new DataBaseService();
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

            //if (TypeSort_ComboBox.SelectedItem == null) return;
            string direction = Direction_ComboBox.SelectedItem.ToString();


            switch (TypeSort_ComboBox.SelectedItem.ToString())
            {
                case "По дате выпуска":
                    str = await dataBaseService.SortModelTableAsync("manufacture_date", direction, "devices", "model");
                    break;
                case "По поставщику":
                    str = await dataBaseService.SortModelTableAsync("supplier_name", direction, "suppliers", "supplier");
                    break;
                case "По производителю":
                    str = await dataBaseService.SortModelTableAsync("manufacturer_name", direction, "manufacturers", "manufacturer");
                    break;
                case "По стране производства":
                    str = await dataBaseService.SortModelTableAsync("country_name", direction, "manufacturers", "manufacturer", "countries", "country");
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
                ModelDataManager.Instance.GetFullDataListModel().ForEach(m => Models_ListBox.Items.Add(m));
                ClearModelInfo();
            }
        }

        public void RefreshModelListBox()
        {
            Models_ListBox.Items.Clear();
            ModelDataManager.Instance.GetFullDataListModel().ForEach(m => Models_ListBox.Items.Add(m));
            if (Models_ListBox.Items.Count > 0)
            { 
                if(ModelDataManager.Instance.Fraction != null) Models_ListBox.Items.Add($"\n Доля = {ModelDataManager.Instance.GetFractionModel()}%");
                if(ModelDataManager.Instance.Quantity != null) Models_ListBox.Items.Add($"\n Количество = {ModelDataManager.Instance.GetQuantityModel()}");
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
                ModelDataManager.Instance.GetFullDataListModel().ForEach(m => Models_ListBox.Items.Add(m));
                ClearModelInfo();
            }
            else MessageBox.Show(str);
        }

        

        private void ValueSearch_ComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {            
            if (ValueSearch_ComboBox.SelectedItem == null) return;
            ParamSearch_ComboBox.Items.Clear();
            ParamSearch_ComboBox.Items.Add("-");
            switch (ValueSearch_ComboBox.SelectedItem.ToString())
            {
                case "Стоимость":
                case "Вес":
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
                case "Дата производства":
                case "Дата продажи":
                    ParamSearch_ComboBox.Items.Add("Значение");
                    ParamSearch_ComboBox.Items.Add("Больше");
                    ParamSearch_ComboBox.Items.Add("Меньше");
                    ParamSearch_ComboBox.Items.Add("Диапазон");                   
                    break;
                case "Тип":
                    ModelDataManager.Instance.GetNameListType().ForEach(t => ParamSearch_ComboBox.Items.Add(t));                    
                    break;
                case "Производитель":
                    ModelDataManager.Instance.GetNameListManufacturers().ForEach(m => ParamSearch_ComboBox.Items.Add(m));                    
                    break;
                case "Страна производства":
                    ModelDataManager.Instance.GetNameListCountries().ForEach(s => ParamSearch_ComboBox.Items.Add(s));                   
                    break;
                case "Поставщик":
                    ModelDataManager.Instance.GetNameListSuppliers().ForEach(s => ParamSearch_ComboBox.Items.Add(s));                    
                    break;
                case "Брак":
                    ParamSearch_ComboBox.Items.Add("Значение");                    
                    //Value_GroupBox.Visible = false;
                    //Date_GroupBox.Visible = false;
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
                case "Тип":
                    ModelDataManager.Instance.GetNameListType().ForEach(t => ParamSearch2_ComboBox.Items.Add(t));                    
                    break;
                case "Производитель":
                    ModelDataManager.Instance.GetNameListManufacturers().ForEach(m => ParamSearch2_ComboBox.Items.Add(m));                    
                    break;
                case "Страна производства":
                    ModelDataManager.Instance.GetNameListCountries().ForEach(s => ParamSearch2_ComboBox.Items.Add(s));                   
                    break;
                case "Поставщик":
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
                case "Тип":
                    ModelDataManager.Instance.GetNameListType().ForEach(t => ParamSearch3_ComboBox.Items.Add(t));                    
                    break;
                case "Производитель":
                    ModelDataManager.Instance.GetNameListManufacturers().ForEach(m => ParamSearch3_ComboBox.Items.Add(m));                    
                    break;
                case "Страна производства":
                    ModelDataManager.Instance.GetNameListCountries().ForEach(s => ParamSearch3_ComboBox.Items.Add(s));                    
                    break;
                case "Поставщик":
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



        private async void Search_Button_Click(object sender, EventArgs e)
        {
            if (ParamSearch_ComboBox.SelectedItem.ToString() == "-") return;

            #region подоготовительные действия
            int result = 0;
            int resultValue = 0;
            int startValue = 0;
            int endValue = 0;
            string func = "";
            string sign = "";
            string startDate = "";
            string endDate = "";
            string str = ""; 
            string innerStr2 = ""; 
            string Str2 = "";
            string whereStr2 = "";            

            string innerStr3 = "";
            string Str3 = "";            

            string cmdSearch = "";
            string cmdFraction = "";
            string cmdQuantity = "";

            string defected = "";
            string valueName1 = "";
            string valueName2 = "";

            string whereStr = "";
            string andStr = "";

            if (ValueSearch_ComboBox.SelectedItem.ToString() == "Стоимость")
            {
                valueName1 = "price";
                valueName2 = "weight";
            }
            else
            {
                valueName1 = "weight";
                valueName2 = "price";
            }            

            if (ParamSearch_ComboBox.SelectedItem.ToString() == "Максимум")
            {
                func = "max";
            }
            else if (ParamSearch_ComboBox.SelectedItem.ToString() == "Минимум")
            {
                func = "min";
            }
            else if (ParamSearch_ComboBox.SelectedItem.ToString() == "Больше среднего" ||
                     ParamSearch_ComboBox.SelectedItem.ToString() == "Меньше среднего")
            {
                func = "avg";
            }

            if (ParamSearch_ComboBox.SelectedItem.ToString() == "Больше" || ParamSearch_ComboBox.SelectedItem.ToString() == "Больше среднего")
            {
                sign = ">";
            }
            else if (ParamSearch_ComboBox.SelectedItem.ToString() == "Значение" || 
                     ParamSearch_ComboBox.SelectedItem.ToString() == "Максимум" || 
                     ParamSearch_ComboBox.SelectedItem.ToString() == "Минимум")
            {
                sign = "=";
            }
            else sign = "<";

            if (ParamSearch2_ComboBox.SelectedItem != null)
            {
                if (ParamSearch2_ComboBox.SelectedItem.ToString() != "-" && 
                    ParamSearch_ComboBox.SelectedItem.ToString() != "-")
                {
                    if (ValueSearch2_ComboBox.SelectedItem.ToString() == "Производитель")
                    {
                        innerStr2 = "inner join manufacturers on manufacturer_FK = manufacturer_id";
                        Str2 = $"manufacturer_name = '{ParamSearch2_ComboBox.SelectedItem}'";                        
                        
                    }
                    else if (ValueSearch2_ComboBox.SelectedItem.ToString() == "Страна производства")
                    {
                        if (ValueSearch_ComboBox.SelectedItem.ToString() == "Производитель")
                        {
                            innerStr2 = "inner join countries on country_FK = country_id";
                        }
                        else
                        {
                            innerStr2 = "inner join manufacturers on manufacturer_FK = manufacturer_id inner join countries on country_FK = country_id";
                        }
                        Str2 = $"country_name = '{ParamSearch2_ComboBox.SelectedItem}'";
                    }
                    else if (ValueSearch2_ComboBox.SelectedItem.ToString() == "Поставщик")
                    {
                        innerStr2 = "inner join suppliers on supplier_FK = supplier_id";
                        Str2 = $"supplier_name = '{ParamSearch2_ComboBox.SelectedItem}'";                        
                        
                    }
                    else
                    {
                        innerStr2 = "inner join types on type_FK = type_id";
                        Str2 = $"type_name = '{ParamSearch2_ComboBox.SelectedItem}'";                        
                        
                    }
                }
            }

            if (ParamSearch3_ComboBox.SelectedItem != null)
            {
                if (ParamSearch3_ComboBox.SelectedItem.ToString() != "-" && 
                    ParamSearch2_ComboBox.SelectedItem.ToString() != "-" && 
                    ParamSearch_ComboBox.SelectedItem.ToString() != "-")
                {
                    if (ValueSearch3_ComboBox.SelectedItem.ToString() == "Производитель" && 
                        ValueSearch2_ComboBox.SelectedItem.ToString() != "Производитель" &&
                        ValueSearch_ComboBox.SelectedItem.ToString() != "Производитель")
                    {
                        if (ValueSearch_ComboBox.SelectedItem.ToString() != "Страна производства" && ValueSearch2_ComboBox.SelectedItem.ToString() != "Страна производства")
                        {
                            innerStr3 = "inner join manufacturers on manufacturer_FK = manufacturer_id";
                        }
                        
                        Str3 = $"manufacturer_name = '{ParamSearch3_ComboBox.SelectedItem}'";                       
                    }

                    if (ValueSearch3_ComboBox.SelectedItem.ToString() == "Страна производства" && 
                        ValueSearch2_ComboBox.SelectedItem.ToString() != "Страна производства" &&
                        ValueSearch_ComboBox.SelectedItem.ToString() != "Страна производства")
                    {
                        if(ValueSearch_ComboBox.SelectedItem.ToString() == "Производитель" || ValueSearch2_ComboBox.SelectedItem.ToString() == "Производитель") 
                        {
                            innerStr3 = "inner join countries on country_FK = country_id";                            
                        }
                        else
                        {
                            innerStr3 = "inner join manufacturers on manufacturer_FK = manufacturer_id inner join countries on country_FK = country_id";                            
                        }                        
                        Str3 = $"country_name = '{ParamSearch3_ComboBox.SelectedItem}'";
                    }

                    if (ValueSearch3_ComboBox.SelectedItem.ToString() == "Поставщик" && 
                        ValueSearch2_ComboBox.SelectedItem.ToString() != "Поставщик" &&
                        ValueSearch_ComboBox.SelectedItem.ToString() != "Поставщик")
                    {
                        innerStr3 = "inner join suppliers on supplier_FK = supplier_id ";
                        Str3 = $"supplier_name = '{ParamSearch3_ComboBox.SelectedItem}'";                        
                    }


                    if (ValueSearch3_ComboBox.SelectedItem.ToString() == "Тип" &&
                        ValueSearch2_ComboBox.SelectedItem.ToString() != "Тип" &&
                        ValueSearch_ComboBox.SelectedItem.ToString() != "Тип")
                    {
                        innerStr3 = "inner join types on type_FK = type_id ";
                        Str3 = $"type_name = '{ParamSearch3_ComboBox.SelectedItem}'";                       
                    }
                }
            }

            if (isDefected_RadioButton.Checked) defected = "1"; else defected = "0";

            if (All_RadioButton.Checked)
            {
                if(Str2 != "")
                {
                    if (Str3 != "")
                    {
                        whereStr = $"where {Str2} and {Str3}";
                        andStr = $"and {Str2} and {Str3}";
                    }
                    else
                    {
                        whereStr = $"where {Str2}";
                        andStr = $"and {Str2}";
                    }                 
                }                   
            }
            else if (InStock_RadioButton.Checked)
            {
                if (Str2 != "")
                {
                    if (Str3 != "")
                    {
                        whereStr = $"where {Str2} and {Str3} and order_FK is null";
                        andStr = $"and {Str2} and {Str3} and order_FK is null";
                    }
                    else
                    {
                        whereStr = $"where {Str2} and order_FK is null";
                        andStr = $"and {Str2} and order_FK is null";
                    }
                }
                else
                {
                    whereStr = $"where order_FK is null";
                    andStr = $"and order_FK is null";
                }         
            }
            else
            {
                if (Str2 != "")
                {
                    if (Str3 != "")
                    {
                        whereStr = $"where {Str2} and {Str3} and order_FK is not null";
                        andStr = $"and {Str2} and {Str3} and order_FK is not null";
                    }
                    else
                    {
                        whereStr = $"where {Str2} and order_FK is not null";
                        andStr = $"and {Str2} and order_FK is not null";
                    }
                }
                else
                {
                    whereStr = $"where order_FK is not null";
                    andStr = $"and order_FK is not null";
                }
            }

            #endregion
            switch (ValueSearch_ComboBox.SelectedItem.ToString())
            {
                case "Стоимость":
                case "Вес":
                    switch (ParamSearch_ComboBox.SelectedItem.ToString())
                    {
                        case "Значение":
                        case "Больше":
                        case "Меньше":
                            if (int.TryParse(Value_TextBox.Text, out resultValue) == false) return;
                            cmdSearch =   $"Select distinct model_id, model_name, type_FK, weight, price, stock_balance, manufacturer_FK, supplier_FK, reserved from models " +
                                          $"inner join devices on model_FK = model_id " +
                                          $"{innerStr2} {innerStr3} where {valueName1} {sign} {resultValue} {andStr};"; 
                            cmdFraction = $"select (count(device_id)*100/(select count(device_id) from models " +
                                                                          $"inner join devices on model_FK = model_id {innerStr2} {innerStr3} " +
                                                                          $"{whereStr})) as Fraction " +
                                          $"from models " +
                                          $"inner join devices on model_FK = model_id {innerStr2} {innerStr3} " +
                                          $"where {valueName1} {sign} {resultValue} {andStr};";
                            cmdQuantity = $"select count(device_id) as Quantity from models " +
                                          $"inner join devices on model_FK = model_id {innerStr2} {innerStr3} " +
                                          $"where {valueName1} {sign} {resultValue} {andStr};";                            
                            break;
                        case "Максимум":
                        case "Минимум":
                        case "Больше среднего":
                        case "Меньше среднего":
                            cmdSearch =   $"Select distinct model_id, model_name, type_FK, weight, price, stock_balance, manufacturer_FK, supplier_FK, reserved from models " +
                                          $"inner join devices on model_FK = model_id {innerStr2} {innerStr3} " +
                                          $"where {valueName1} {sign} (select {func}({valueName1}) from models {innerStr2} {innerStr3} {whereStr}) {andStr};";
                            cmdFraction = $"select (count(device_id)*100/(select count(device_id) from models " +
                                                                          $"inner join devices on model_FK = model_id {innerStr2} {innerStr3} " +
                                                                          $"{whereStr})) as Fraction " +
                                          $"from models " +
                                          $"inner join devices on model_FK = model_id {innerStr2} {innerStr3} " +
                                          $"where {valueName1} {sign} (select {func}({valueName1}) from models {innerStr2} {innerStr3} {whereStr}) {andStr};";
                            cmdQuantity = $"select count(device_id) as Quantity from models " +
                                          $"inner join devices on model_FK = model_id {innerStr2} {innerStr3} " +
                                          $"where {valueName1} {sign} (select {func}({valueName1}) from models {innerStr2} {innerStr3} {whereStr}) {andStr};";                                                       
                            break;                        
                        case "Среднее":
                            cmdSearch = $"Select avg({valueName1}) as Среднее_значение from models " +
                                        $"inner join devices on model_FK = model_id {innerStr2} {innerStr3} {whereStr};";
                            str = await dataBaseService.SearchModelTableAsync(cmdSearch,"","");
                            Models_ListBox.Items.Clear();
                            Models_ListBox.Items.Add(ModelDataManager.Instance.GetValueField("Среднее_значение"));
                            break;                       
                        case "Диапазон":                             
                            if (int.TryParse(StartValue_TextBox.Text, out startValue) == false || int.TryParse(EndValue_TextBox.Text, out endValue) == false) return;
                            cmdSearch =   $"Select distinct model_id, model_name, type_FK, weight, price, stock_balance, manufacturer_FK, supplier_FK, reserved from models " +                                          
                                          $"inner join devices on model_FK = model_id { innerStr2} { innerStr3}" +
                                          $"where {valueName1} >= {startValue} and {valueName1} <= {endValue} {andStr};";
                            cmdFraction = $"select (count(device_id)*100/(select count(device_id) from models " +
                                                                           $"inner join devices on model_FK = model_id {innerStr2} {innerStr3} " +
                                                                           $"{whereStr})) as Fraction " +
                                           $"from models " +
                                           $"inner join devices on model_FK = model_id {innerStr2} {innerStr3} " +
                                           $"where {valueName1} >= {startValue} and {valueName1} <= {endValue} {andStr};";

                            cmdQuantity = $"select count(device_id) as Quantity from models " +
                                          $"inner join devices on model_FK = model_id {innerStr2} {innerStr3} " +
                                          $"where {valueName1} >= {startValue} and {valueName1} <= {endValue} {andStr};";
                            break;
                    }
                    if (ParamSearch_ComboBox.SelectedItem.ToString() != "Среднее")
                    {
                        str = await dataBaseService.SearchModelTableAsync(cmdSearch, cmdFraction, cmdQuantity);
                        RefreshModelListBox();
                    }
                    break;                
                case "Дата производства":
                    switch (ParamSearch_ComboBox.SelectedItem.ToString())
                    {
                        case "Значение":
                        case "Больше":
                        case "Меньше":
                            string date = Date_DateTimePicker.Value.Year.ToString() + "-" + Date_DateTimePicker.Value.Month.ToString() + "-" + Date_DateTimePicker.Value.Day.ToString();
                            cmdSearch =   $"Select distinct model_id, model_name, type_FK, weight, price, stock_balance, manufacturer_FK, supplier_FK, reserved from models " +
                                          $"inner join devices on model_FK = model_id {innerStr2} {innerStr3} " +
                                          $"where manufacture_date {sign} '{date}' {andStr};";
                            cmdFraction = $"select (count(device_id)*100/(select count(device_id) from models " +
                                                                          $"inner join devices on model_FK = model_id {innerStr2} {innerStr3} " +
                                                                          $"{whereStr})) as Fraction " +
                                          $"from models " +
                                          $"inner join devices on model_FK = model_id {innerStr2} {innerStr3} " +
                                          $"where manufacture_date {sign} '{date}' {andStr};";
                            cmdQuantity = $"select count(device_id) as Quantity from models " +
                                          $"inner join devices on model_FK = model_id {innerStr2} {innerStr3} " +
                                          $"where manufacture_date {sign} '{date}' {andStr};";                            
                            break;                        
                        case "Диапазон":
                            startDate = StartDate_DateTimePicker.Value.Year.ToString() + "-" + StartDate_DateTimePicker.Value.Month.ToString() + "-" + StartDate_DateTimePicker.Value.Day.ToString();
                            endDate = EndDate_DateTimePicker.Value.Year.ToString() + "-" + EndDate_DateTimePicker.Value.Month.ToString() + "-" + EndDate_DateTimePicker.Value.Day.ToString();
                            cmdSearch =   $"Select distinct model_id, model_name, type_FK, weight, price, stock_balance, manufacturer_FK, supplier_FK, reserved from models " +
                                          $"inner join devices on model_FK = model_id {innerStr2} {innerStr3} " +
                                          $"where manufacture_date >= '{startDate}' and manufacture_date <= '{endDate}' {andStr};";
                            cmdFraction = $"select (count(device_id)*100/(select count(device_id) from models " +
                                                                          $"inner join devices on model_FK = model_id {innerStr2} {innerStr3} " +
                                                                          $"{whereStr})) as Fraction " +
                                          $"from models " +
                                          $"inner join devices on model_FK = model_id { innerStr2} { innerStr3} " +
                                          $"where manufacture_date>='{startDate}' and manufacture_date<='{endDate}' {andStr};";

                            cmdQuantity = $"select count(device_id) as Quantity from models " +
                                          $"inner join devices on model_FK = model_id {innerStr2} {innerStr3} " +
                                          $"where manufacture_date >= '{startDate}' and manufacture_date <= '{endDate}' {andStr};";

                            break;
                    }
                    str = await dataBaseService.SearchModelTableAsync(cmdSearch, cmdFraction, cmdQuantity);
                    RefreshModelListBox();
                    break;
                case "Дата продажи":
                    switch (ParamSearch_ComboBox.SelectedItem.ToString())
                    {
                        case "Значение":
                        case "Больше":
                        case "Меньше":
                            string date = Date_DateTimePicker.Value.Year.ToString() + "-" + Date_DateTimePicker.Value.Month.ToString() + "-" + Date_DateTimePicker.Value.Day.ToString();
                            cmdSearch =   $"Select distinct models.model_id, model_name, type_FK, weight, price, stock_balance, manufacturer_FK, supplier_FK, reserved from models " +
                                          $"inner join modelOrder on modelOrder.model_id = models.model_id " +
                                          $"inner join orders on orders.order_id = modelOrder.order_id {innerStr2} {innerStr3} " +
                                          $"where order_date {sign} '{date}' {andStr};";
                            cmdFraction = $"select (count(device_id)*100/(select count(device_id) from models " +
                                                                          $"inner join devices on model_FK = models.model_id {innerStr2} {innerStr3} " +
                                                                          $"{whereStr})) as Fraction " +
                                          $"from models " +
                                          $"inner join modelOrder on modelOrder.model_id = models.model_id " +
                                          $"inner join orders on orders.order_id = modelOrder.order_id " +
                                          $"inner join devices on model_FK = models.model_id {innerStr2} {innerStr3} " +
                                          $"where order_date {sign} '{date}' {andStr};";  //{inStock} - не нужен возможно
                            cmdQuantity = $"select count(device_id) as Quantity from models " +
                                          $"inner join modelOrder on modelOrder.model_id = models.model_id " +
                                          $"inner join orders on orders.order_id = modelOrder.order_id " +
                                          $"inner join devices on model_FK = models.model_id {innerStr2} {innerStr3} " +
                                          $"where order_date {sign} '{date}' {andStr};";



                            break;                        
                        case "Диапазон":
                            startDate = StartDate_DateTimePicker.Value.Year.ToString() + "-" + StartDate_DateTimePicker.Value.Month.ToString() + "-" + StartDate_DateTimePicker.Value.Day.ToString();
                            endDate = EndDate_DateTimePicker.Value.Year.ToString() + "-" + EndDate_DateTimePicker.Value.Month.ToString() + "-" + EndDate_DateTimePicker.Value.Day.ToString();
                            cmdSearch =   $"Select distinct models.model_id, model_name, type_FK, weight, price, stock_balance, manufacturer_FK, supplier_FK, reserved from models " +
                                          $"inner join modelOrder on modelOrder.model_id = models.model_id " +
                                          $"inner join orders on orders.order_id = modelOrder.order_id {innerStr2} {innerStr3} " +
                                          $"where order_date >= '{startDate}' and order_date <= '{endDate}' {andStr};";
                            cmdFraction = $"select (count(device_id)*100/(select count(device_id) from models " +
                                                                          $"inner join devices on model_FK = models.model_id {innerStr2} {innerStr3} " +
                                                                          $"{whereStr})) as Fraction " +
                                          $"from models " +
                                          $"inner join modelOrder on modelOrder.model_id = models.model_id " +
                                          $"inner join orders on orders.order_id = modelOrder.order_id " +
                                          $"inner join devices on model_FK = models.model_id {innerStr2} {innerStr3} " +
                                          $"where order_date >= '{startDate}' and order_date <= '{endDate}' {andStr};";  //{inStock} - не нужен возможно
                            cmdQuantity = $"select count(device_id) as Quantity from models " +
                                          $"inner join modelOrder on modelOrder.model_id = models.model_id " +
                                          $"inner join orders on orders.order_id = modelOrder.order_id " +
                                          $"inner join devices on model_FK = models.model_id {innerStr2} {innerStr3} " +
                                          $"where order_date >= '{startDate}' and order_date <= '{endDate}' {andStr};";

                            break;
                    }
                    str = await dataBaseService.SearchModelTableAsync(cmdSearch, cmdFraction, cmdQuantity);
                    RefreshModelListBox();
                    break;
                case "Брак":
                    cmdSearch =   $"Select distinct model_id, model_name, type_FK, weight, price, stock_balance, manufacturer_FK, supplier_FK, reserved from models " +
                                  $"inner join devices on model_FK = model_id {innerStr2} {innerStr3} " +
                                  $"where isDefected = {defected} {andStr};";
                    cmdFraction = $"select (count(device_id)*100/(select count(device_id) from models " +
                                                                            $"inner join devices on model_FK = model_id " +
                                                                            $"{innerStr2} {innerStr3} {whereStr})) as Fraction from models " +
                                  $"inner join devices on model_FK = model_id " +
                                  $"{innerStr2} {innerStr3} " +
                                  $"where isDefected = {defected} {andStr};";
                    cmdQuantity = $"select count(device_id) as Quantity from models " +
                                  $"inner join devices on model_FK = model_id " +
                                  $"{innerStr2} {innerStr3} where isDefected = {defected} {andStr};";

                    str = await dataBaseService.SearchModelTableAsync(cmdSearch, cmdFraction, cmdQuantity);
                    RefreshModelListBox();
                    break;
                case "Тип":
                    cmdSearch = $"Select distinct model_id, model_name, type_FK, weight, price, stock_balance, manufacturer_FK, supplier_FK, reserved from models " +
                                  $"inner join devices on model_FK = model_id " +
                                  $"inner join types on type_FK = type_id {innerStr2} {innerStr3} " +
                                  $"where type_name = '{ParamSearch_ComboBox.SelectedItem}' {andStr};";
                    cmdFraction = $"select (count(device_id)*100/(select count(device_id) from models " +
                                                                  $"inner join devices on model_FK = model_id " +                                                                           
                                                                  $"{innerStr2} {innerStr3} {whereStr})) as Fraction from models " +
                                  $"inner join devices on model_FK = model_id " +
                                  $"inner join types on type_FK = type_id {innerStr2} {innerStr3} " +
                                  $"where type_name = '{ParamSearch_ComboBox.SelectedItem}' {andStr} {andStr};";
                    cmdQuantity = $"select count(device_id) as Quantity from models " +
                                  $"inner join devices on model_FK = model_id " +
                                  $"inner join types on type_FK = type_id {innerStr2} {innerStr3} " +
                                  $"where type_name = '{ParamSearch_ComboBox.SelectedItem}' {andStr};";

                    str = await dataBaseService.SearchModelTableAsync(cmdSearch, cmdFraction, cmdQuantity);
                    RefreshModelListBox();
                    break;
                case "Производитель":
                    cmdSearch = $"Select distinct model_id, model_name, type_FK, weight, price, stock_balance, manufacturer_FK, supplier_FK, reserved from models " +
                                  $"inner join devices on model_FK = model_id " +
                                  $"inner join manufacturers on manufacturer_FK = manufacturer_id {innerStr2} {innerStr3} " +
                                  $"where manufacturer_name = '{ParamSearch_ComboBox.SelectedItem}' {andStr};";
                    cmdFraction = $"select (count(device_id)*100/(select count(device_id) from models " +
                                                                  $"inner join devices on model_FK = model_id " +
                                                                  $"inner join manufacturers on manufacturer_FK = manufacturer_id " +
                                                                  $"{innerStr2} {innerStr3} {whereStr})) as Fraction from models " +
                                  $"inner join devices on model_FK = model_id " +
                                  $"inner join manufacturers on manufacturer_FK = manufacturer_id {innerStr2} {innerStr3} " +
                                  $"where manufacturer_name = '{ParamSearch_ComboBox.SelectedItem}' {andStr};";
                    cmdQuantity = $"select count(device_id) as Quantity from models " +
                                  $"inner join devices on model_FK = model_id " +
                                  $"inner join manufacturers on manufacturer_FK = manufacturer_id {innerStr2} {innerStr3} " +
                                  $"where manufacturer_name = '{ParamSearch_ComboBox.SelectedItem}' {andStr};";

                    str = await dataBaseService.SearchModelTableAsync(cmdSearch, cmdFraction, cmdQuantity);
                    RefreshModelListBox();
                    break;
                case "Поставщик":
                    cmdSearch = $"Select distinct model_id, model_name, type_FK, weight, price, stock_balance, manufacturer_FK, supplier_FK, reserved from models " +
                                  $"inner join devices on model_FK = model_id " +
                                  $"inner join suppliers on supplier_FK = supplier_id {innerStr2} {innerStr3} " +
                                  $"where supplier_name = '{ParamSearch_ComboBox.SelectedItem}' {andStr};";
                    cmdFraction = $"select (count(device_id)*100/(select count(device_id) from models " +
                                                                           $"inner join devices on model_FK = model_id " +
                                                                           $"{innerStr2} {innerStr3} {whereStr})) as Fraction from models " +
                                  $"inner join devices on model_FK = model_id " +
                                  $"inner join suppliers on supplier_FK = supplier_id {innerStr2} {innerStr3} " +
                                  $"where supplier_name = '{ParamSearch_ComboBox.SelectedItem}' {andStr};";
                    cmdQuantity = $"select count(device_id) as Quantity from models " +
                                  $"inner join devices on model_FK = model_id " +
                                  $"inner join suppliers on supplier_FK = supplier_id {innerStr2} {innerStr3} " +
                                  $"where supplier_name = '{ParamSearch_ComboBox.SelectedItem}' {andStr};";

                    str = await dataBaseService.SearchModelTableAsync(cmdSearch, cmdFraction, cmdQuantity);
                    RefreshModelListBox();
                    break;
                case "Страна производства":
                    cmdSearch = $"Select distinct model_id, model_name, type_FK, weight, price, stock_balance, manufacturer_FK, supplier_FK, reserved from models " +
                                  $"inner join devices on model_FK = model_id " +
                                  $"inner join manufacturers on manufacturer_FK = manufacturer_id " +
                                  $"inner join countries on country_FK = country_id {innerStr2} {innerStr3} " +
                                  $"where country_name = '{ParamSearch_ComboBox.SelectedItem}' {andStr};";
                    cmdFraction = $"select (count(device_id)*100/(select count(device_id) from models " +
                                                                           $"inner join devices on model_FK = model_id " +
                                                                           $"inner join manufacturers on manufacturer_FK = manufacturer_id " +
                                                                           $"{innerStr2} {innerStr3} {whereStr})) as Fraction from models " +
                                  $"inner join devices on model_FK = model_id " +
                                  $"inner join manufacturers on manufacturer_FK = manufacturer_id " +
                                  $"inner join countries on country_FK = country_id {innerStr2} {innerStr3} " +
                                  $"where country_name = '{ParamSearch_ComboBox.SelectedItem}' {andStr};";
                    cmdQuantity = $"select count(device_id) as Quantity from models " +
                                  $"inner join devices on model_FK = model_id " +
                                  $"inner join manufacturers on manufacturer_FK = manufacturer_id " +
                                  $"inner join countries on country_FK = country_id {innerStr2} {innerStr3} " +
                                  $"where country_name = '{ParamSearch_ComboBox.SelectedItem}' {andStr};";

                    str = await dataBaseService.SearchModelTableAsync(cmdSearch, cmdFraction, cmdQuantity);
                    RefreshModelListBox();
                    break;
            }
            if (int.TryParse(str,out result)==false)
            {
                MessageBox.Show(str);
            }

            Search_TextBox.Text = cmdSearch;
            Fraction_TextBox.Text = cmdFraction;
            Quantity_TextBox.Text = cmdQuantity;            
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
            ParamSearch_ComboBox.SelectedIndex = 0;

            Search2_GroupBox.Visible = false;
            Search3_GroupBox.Visible = false;
            if (ParamSearch2_ComboBox.SelectedIndex != -1) ParamSearch2_ComboBox.SelectedIndex = 0;
            if (ParamSearch3_ComboBox.SelectedIndex != -1) ParamSearch3_ComboBox.SelectedIndex = 0;
        }



        private void AddSearch_Button_Click(object sender, EventArgs e)
        {
            if (ParamSearch_ComboBox.SelectedIndex == 0) return;
            Search2_GroupBox.Visible = true;
            ValueSearch2_ComboBox.SelectedIndex = 0;
        }

        private void AddSearch2_Button_Click(object sender, EventArgs e)
        {
            if (ParamSearch2_ComboBox.SelectedIndex == 0) return;
            Search3_GroupBox.Visible = true;
            ValueSearch3_ComboBox.SelectedIndex = 0;
        }

        private void SubSearch2_Button_Click(object sender, EventArgs e)
        {
            Search2_GroupBox.Visible = false;
            Search3_GroupBox.Visible = false;
            ParamSearch2_ComboBox.SelectedIndex = 0;
            if(ParamSearch3_ComboBox.SelectedIndex != -1) ParamSearch3_ComboBox.SelectedIndex = 0;
        }

        private void SubSearch3_Button_Click(object sender, EventArgs e)
        {
            Search3_GroupBox.Visible = false;
            ParamSearch3_ComboBox.SelectedIndex = 0;
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
                        case "Стоимость":
                        case "Вес":
                            valueGroupBox.Visible = true;
                            valueTextBox.Visible = true;
                            dateGroupBox.Visible = false;
                            notDefectedRadioButton.Visible = false;
                            defectedRadioButton.Visible = false;

                            break;
                        case "Дата производства":
                        case "Дата продажи":
                            valueGroupBox.Visible = false;
                            dateGroupBox.Visible = true;
                            dateTimePicker.Visible = true;
                            notDefectedRadioButton.Visible = false;
                            defectedRadioButton.Visible = false;
                            break;
                        case "Брак":
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
                        case "Стоимость":
                        case "Вес":
                            valueGroupBox.Visible = true;
                            valueTextBox.Visible = false;
                            dateGroupBox.Visible = false;
                            notDefectedRadioButton.Visible = false;
                            defectedRadioButton.Visible = false;
                            break;
                        case "Дата производства":
                        case "Дата продажи":
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

            RefreshData();
            ModelDataManager.Instance.GetNameListTypes().ForEach(m => Type_ComboBox.Items.Add(m));
            Type_ComboBox.SelectedIndex = 0;

            ModelDataManager.Instance.GetNameListManufacturers().ForEach(m => Manufacturer_ComboBox.Items.Add(m));
            Manufacturer_ComboBox.SelectedIndex = 0;

            ModelDataManager.Instance.GetNameListSuppliers().ForEach(s => Supplier_ComboBox.Items.Add(s));
            Supplier_ComboBox.SelectedIndex = 0;
            //сортировка
            Direction_ComboBox.SelectedIndex = 0;
            TypeSort_ComboBox.SelectedIndex = 0;
            //поиск
            ValueSearch_ComboBox.SelectedIndex = 0;

            Search2_GroupBox.Visible = false;
            Search3_GroupBox.Visible = false;

            NotDefected_RadioButton.Visible = false;
            isDefected_RadioButton.Visible = false;

            All_RadioButton.Checked = true;
        }

    
    }
}
