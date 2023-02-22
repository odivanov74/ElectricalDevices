using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectricalDevicesCW.Managers
{
    public class ModelDataManager
    {
        public DataSet Models { get; set; } = new DataSet();
        public DataSet Fraction { get; set; } = new DataSet();
        public DataSet Quantity { get; set; } = new DataSet();
        public DataSet Devices { get; set; } = new DataSet();
        public DataSet Countries { get; set; } = new DataSet();
        public DataSet Suppliers { get; set; } = new DataSet();
        public DataSet Types { get; set; } = new DataSet();
        public DataSet Manufacturers { get; set; } = new DataSet();

        private ModelDataManager() { }

        public static ModelDataManager Instance { get => ModelDataManagerCreate.instance; }

        private class ModelDataManagerCreate
        {
            static ModelDataManagerCreate() { }
            internal static readonly ModelDataManager instance = new ModelDataManager();
        }

        public string GetValueField(string fieldName)
        {            
            object value = Models.Tables[0].Rows[0].Field<object>(fieldName);
            if(value==null) return "Среднее значение = 0";
            return "Среднее значение = " + value.ToString();
        }

        public List<string> GetFullDataListModel()
        {
            List<string> models = new List<string>();
            int idModel = 0;           

            for (int i = 0; i < Models.Tables[0].Rows.Count; i++)
            {
                idModel = Models.Tables[0].Rows[i].Field<int>("model_id");
                int count = ShopDataManager.Instance.GetNumSaleModelToModelOrder(idModel);
                models.Add($"{idModel}." +
                           $"{Models.Tables[0].Rows[i].Field<string>("model_name")}." +
                           $"{Models.Tables[0].Rows[i].Field<int>("type_FK")}." +
                           $"{Models.Tables[0].Rows[i].Field<int>("weight")}." +
                           $"{(int)(Models.Tables[0].Rows[i].Field<decimal>("price"))}." +
                           $"{Models.Tables[0].Rows[i].Field<int>("stock_balance")}." +
                           $"{Models.Tables[0].Rows[i].Field<int>("manufacturer_FK")}." +
                           $"{Models.Tables[0].Rows[i].Field<int>("supplier_FK")}." +
                           $"{Models.Tables[0].Rows[i].Field<int>("reserved")}." +
                           $"{count}");
            }
            return models;
        }

        public List<string> GetDataListModel()
        {
            List<string> models = new List<string>();

            for (int i = 0; i < Models.Tables[0].Rows.Count; i++)
            {
                models.Add($"{Models.Tables[0].Rows[i].Field<int>("model_id")}." +
                                    $"{Models.Tables[0].Rows[i].Field<string>("model_name")}." +
                                    $"{ModelDataManager.Instance.GetNameType(Models.Tables[0].Rows[i].Field<int>("type_FK"))}." +                                    
                                    $"{(int)(Models.Tables[0].Rows[i].Field<decimal>("price"))}." +                                    
                                    $"{ModelDataManager.Instance.GetNameManufacturer(Models.Tables[0].Rows[i].Field<int>("manufacturer_FK"))}");
            }
            return models;
        }

        public List<string> GetNameListModel()
        {
            List<string> models = new List<string>();

            for (int i = 0; i < Models.Tables[0].Rows.Count; i++)
            {
                models.Add($"{Models.Tables[0].Rows[i].Field<string>("model_name")}");
            }
            return models;
        }

        public string GetInfoModel(string infoStr)
        {
            string outInfo = "";
            string[] result = infoStr.Split('.');
            int i = int.Parse(result[0]) - 1;

            outInfo = "Модель: " + Models.Tables[0].Rows[i].Field<string>("model_name") + Environment.NewLine;
            outInfo += "Тип: " + ModelDataManager.Instance.GetNameType(Models.Tables[0].Rows[i].Field<int>("type_FK")) + Environment.NewLine;
            outInfo += "Цена: " + (int)(Models.Tables[0].Rows[i].Field<decimal>("price")) + " руб." + Environment.NewLine;
            outInfo += "Производитель: " + ModelDataManager.Instance.GetNameManufacturer(Models.Tables[0].Rows[i].Field<int>("manufacturer_FK")) + Environment.NewLine;
            outInfo += "Вес: " + Models.Tables[0].Rows[i].Field<int>("weight") + " г." + Environment.NewLine;
            outInfo += "Количество на складе: " + Models.Tables[0].Rows[i].Field<int>("stock_balance") + " шт." + Environment.NewLine;
            outInfo += "Зарезервировано: " + Models.Tables[0].Rows[i].Field<int>("reserved") + " шт." + Environment.NewLine;

            return outInfo;
        }

        public string GetNameModel(int idModel)
        { 
            return Models.Tables[0].Rows[idModel-1].Field<string>("model_name");
        }

        public int GetModelId(string name)
        {
            int idModel = 0;
            for (int i = 0; i < Models.Tables[0].Rows.Count; i++)
            {
                if(Models.Tables[0].Rows[i].Field<string>("model_name") == name)
                {
                    idModel = i + 1;
                    break;
                }
            }
            return idModel;
        }

        public int GetPriceModel(int idModel)
        {
            int price = 0;
            for (int i = 0; i < Models.Tables[0].Rows.Count; i++)
            {
                if(Models.Tables[0].Rows[i].Field<int>("model_id")== idModel)
                {
                    price = (int)Models.Tables[0].Rows[i].Field<decimal>("price");
                    break;
                }
            }
            return price;
        }

        public int GetFractionModel()
        {
            int fr = 0;
            for (int i = 0; i < ModelDataManager.Instance.Fraction.Tables[0].Rows.Count; i++)
            {
                fr += (int)ModelDataManager.Instance.Fraction.Tables[0].Rows[i].Field<object>("Fraction");
            }
            return fr;
        }

        public int GetQuantityModel()
        {
            return ModelDataManager.Instance.Quantity.Tables[0].Rows[0].Field<int>("Quantity");
        }

        public List<string> GetNameListType()
        {
            List<string> types = new List<string>();

            for (int i = 0; i < Types.Tables[0].Rows.Count; i++)
            {
                types.Add($"{Types.Tables[0].Rows[i].Field<string>("type_name")}");
            }
            return types;
        }

        public int GetTypeId(int idModel)
        {
            int typeModel = 0;
            for (int i = 0; i < Models.Tables[0].Rows.Count; i++)
            {
                if (Models.Tables[0].Rows[i].Field<int>("model_id") == idModel)
                {
                    typeModel = Models.Tables[0].Rows[i].Field<int>("type_FK");
                    break;
                }
            }
            return typeModel;
        }


        public int GetManufacturerId(int idModel)
        {
            int manufacturerId = 0;
            for (int i = 0; i < Models.Tables[0].Rows.Count; i++)
            {
                if (Models.Tables[0].Rows[i].Field<int>("model_id") == idModel)
                {
                    manufacturerId = Models.Tables[0].Rows[i].Field<int>("manufacturer_FK");
                    break;
                }
            }
            return manufacturerId;
        }

        public int GetFullQuantityModel(int idModel)
        {
            return GetStockBalance(idModel) + ShopDataManager.Instance.GetNumSaleModelToModelOrder(idModel);
        }

        public int GetStockBalance(int idModel)
        {
            int stockBalance = 0;
            for (int i = 0; i < Models.Tables[0].Rows.Count; i++)
            {
                if (Models.Tables[0].Rows[i].Field<int>("model_id") == idModel)
                {
                    stockBalance = Models.Tables[0].Rows[i].Field<int>("stock_balance");
                    break;
                }
            }
            return stockBalance;
        }

        public int GetNumReserved(int idModel)
        {
            int numReserved = 0;
            for (int i = 0; i < Models.Tables[0].Rows.Count; i++)
            {
                if (Models.Tables[0].Rows[i].Field<int>("model_id") == idModel)
                {
                    numReserved = Models.Tables[0].Rows[i].Field<int>("reserved");
                    break;
                }
            }
            return numReserved;
        }

        public List<string> GetFullDataListCounties()
        {
            List<string> countries = new List<string>();

            for (int i = 0; i < Countries.Tables[0].Rows.Count; i++)
            {
                countries.Add($"{Countries.Tables[0].Rows[i].Field<int>("country_id")}." +
                    $"{Countries.Tables[0].Rows[i].Field<string>("country_name")}");
            }
            return countries;
        }

        public List<string> GetNameListCountries()
        {
            List<string> countries = new List<string>();

            for (int i = 0; i < Countries.Tables[0].Rows.Count; i++)
            {
                countries.Add($"{Countries.Tables[0].Rows[i].Field<string>("country_name")}");
            }
            return countries;
        }




        public List<string> GetFullDataListSuppliers()
        {
            List<string> countries = new List<string>();

            for (int i = 0; i < Suppliers.Tables[0].Rows.Count; i++)
            {
                countries.Add($"{Suppliers.Tables[0].Rows[i].Field<int>("supplier_id")}." +
                    $"{Suppliers.Tables[0].Rows[i].Field<string>("supplier_name")}");
            }
            return countries;
        }

        public List<string> GetNameListSuppliers()
        {
            List<string> countries = new List<string>();

            for (int i = 0; i < Suppliers.Tables[0].Rows.Count; i++)
            {
                countries.Add($"{Suppliers.Tables[0].Rows[i].Field<string>("supplier_name")}");
            }
            return countries;
        }



        public List<string> GetFullDataListTypes()
        {
            List<string> types = new List<string>();

            for (int i = 0; i < Types.Tables[0].Rows.Count; i++)
            {
                types.Add($"{Types.Tables[0].Rows[i].Field<int>("type_id")}." +
                    $"{Types.Tables[0].Rows[i].Field<string>("type_name")}");
            }
            return types;
        }

        public string GetNameType(int idType)
        {
            string nameType = "";
            for (int i = 0; i < Types.Tables[0].Rows.Count; i++)
            {
                if(Types.Tables[0].Rows[i].Field<int>("type_id") == idType)
                {
                    nameType = Types.Tables[0].Rows[i].Field<string>("type_name");
                    break;
                }
            }
                return nameType;
        }

        public List<string> GetNameListTypes()
        {
            List<string> types = new List<string>();

            for (int i = 0; i < Types.Tables[0].Rows.Count; i++)
            {
                types.Add($"{Types.Tables[0].Rows[i].Field<string>("type_name")}");
            }
            return types;
        }



        public List<string> GetFullDataListManufacturers()
        {
            List<string> manufacturers = new List<string>();

            for (int i = 0; i < Manufacturers.Tables[0].Rows.Count; i++)
            {
                manufacturers.Add($"{Manufacturers.Tables[0].Rows[i].Field<int>("manufacturer_id")}." +
                                    $"{Manufacturers.Tables[0].Rows[i].Field<string>("manufacturer_name")}." +
                                    $"{Manufacturers.Tables[0].Rows[i].Field<int>("country_FK")}");
            }
            return manufacturers;
        }

        public string GetNameManufacturer(int idManufacturer)
        {
            string nameManufacturer = "";
            for (int i = 0; i < Manufacturers.Tables[0].Rows.Count; i++)
            {
                if (Manufacturers.Tables[0].Rows[i].Field<int>("manufacturer_id") == idManufacturer)
                {
                    nameManufacturer = Manufacturers.Tables[0].Rows[i].Field<string>("manufacturer_name");
                    break;
                }
            }
            return nameManufacturer;
        }

        public List<string> GetNameListManufacturers()
        {
            List<string> manufacturers = new List<string>();

            for (int i = 0; i < Manufacturers.Tables[0].Rows.Count; i++)
            {
                manufacturers.Add($"{Manufacturers.Tables[0].Rows[i].Field<string>("manufacturer_name")}");
            }
            return manufacturers;
        }



        public List<string> GetFullDataListDevice()
        {
            List<string> devices = new List<string>();

            for (int i = 0; i < Devices.Tables[0].Rows.Count; i++)
            {
                devices.Add($"{Devices.Tables[0].Rows[i].Field<int>("device_id")}." +
                          $"{Devices.Tables[0].Rows[i].Field<int>("model_FK")}." +
                          $"{Devices.Tables[0].Rows[i].Field<string>("serial_number")}." +
                          $"{Devices.Tables[0].Rows[i].Field<DateTime>("manufacture_date")}." +
                          $"{Devices.Tables[0].Rows[i].Field<int?>("order_FK")}." +
                           $"{Devices.Tables[0].Rows[i].Field<int?>("basket_FK")}." +
                          $"{Devices.Tables[0].Rows[i].Field<bool>("isDefected")}");
            }
            return devices;
        }

        public DateTime GetDateManufactureDevice(int id)
        {
            DateTime dt = new DateTime();

            for (int i = 0; i < Devices.Tables[0].Rows.Count; i++)
            {
                if (Devices.Tables[0].Rows[i].Field<int>("device_id") == id)
                {
                    dt = Devices.Tables[0].Rows[i].Field<DateTime>("manufacture_date");
                    break;
                }
            }
            return dt;
        }

        public bool GetStatusDefectDevice(int id)
        {
            bool defectStatus = false;
            for (int i = 0; i < Devices.Tables[0].Rows.Count; i++)
            {
                if (Devices.Tables[0].Rows[i].Field<int>("device_id") == id)
                {
                    defectStatus = Devices.Tables[0].Rows[i].Field<bool>("isDefected");
                    break;
                }
            }
            return defectStatus;
        }

        public int GetModelId(int idDevice)
        {
            int fK = 0;
            for (int i = 0; i < Devices.Tables[0].Rows.Count; i++)
            {
                if (Devices.Tables[0].Rows[i].Field<int>("device_id") == idDevice)
                {
                    fK = Devices.Tables[0].Rows[i].Field<int>("Model_FK");
                    break;
                }
            }
            return fK;
        }
    }
}
