using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectricalDevicesCW.Managers
{
    public class DeviceModelDataManager
    {
        public DataSet DeviceModels { get; set; } = new DataSet();

        private DeviceModelDataManager() { }

        public static DeviceModelDataManager Instance { get => DeviceModelDataManagerCreate.instance; }

        private class DeviceModelDataManagerCreate
        {
            static DeviceModelDataManagerCreate() { }
            internal static readonly DeviceModelDataManager instance = new DeviceModelDataManager();
        }

        public List<string> GetFullDataListDeviceModel()
        {
            List<string> deviceModels = new List<string>();

            for (int i = 0; i < DeviceModels.Tables[0].Rows.Count; i++)
            {
                deviceModels.Add($"{DeviceModels.Tables[0].Rows[i].Field<int>("deviceModel_id")}." +
                                    $"{DeviceModels.Tables[0].Rows[i].Field<string>("model_name")}." +
                                    $"{DeviceModels.Tables[0].Rows[i].Field<int>("modelType_FK")}." +
                                    $"{DeviceModels.Tables[0].Rows[i].Field<int>("weight")}." +
                                    $"{(int)(DeviceModels.Tables[0].Rows[i].Field<decimal>("price"))}." +
                                    $"{DeviceModels.Tables[0].Rows[i].Field<int>("stock_balance")}." +
                                    $"{DeviceModels.Tables[0].Rows[i].Field<int>("manufacturer_FK")}." +
                                    $"{DeviceModels.Tables[0].Rows[i].Field<int>("supplier_FK")}");
            }
            return deviceModels;
        }

        public List<string> GetDataListDeviceModel()
        {
            List<string> deviceModels = new List<string>();

            for (int i = 0; i < DeviceModels.Tables[0].Rows.Count; i++)
            {
                deviceModels.Add($"{DeviceModels.Tables[0].Rows[i].Field<int>("deviceModel_id")}." +
                                    $"{DeviceModels.Tables[0].Rows[i].Field<string>("model_name")}." +
                                    $"{ModelTypeDataManager.Instance.GetNameModelType(DeviceModels.Tables[0].Rows[i].Field<int>("modelType_FK"))}." +                                    
                                    $"{(int)(DeviceModels.Tables[0].Rows[i].Field<decimal>("price"))}." +                                    
                                    $"{ManufacturerDataManager.Instance.GetNameManufacturer(DeviceModels.Tables[0].Rows[i].Field<int>("manufacturer_FK"))}");
            }
            return deviceModels;
        }

        public List<string> GetNameListDeviceModel()
        {
            List<string> deviceModels = new List<string>();

            for (int i = 0; i < DeviceModels.Tables[0].Rows.Count; i++)
            {
                deviceModels.Add($"{DeviceModels.Tables[0].Rows[i].Field<string>("model_name")}");
            }
            return deviceModels;
        }

        public string GetInfoDeviceModel(string infoStr)
        {
            string outInfo = "";
            string[] result = infoStr.Split('.');
            int i = int.Parse(result[0]) - 1;

            outInfo = "Модель: " + DeviceModels.Tables[0].Rows[i].Field<string>("model_name") + Environment.NewLine;
            outInfo += "Тип: " + ModelTypeDataManager.Instance.GetNameModelType(DeviceModels.Tables[0].Rows[i].Field<int>("modelType_FK")) + Environment.NewLine;
            outInfo += "Цена: " + (int)(DeviceModels.Tables[0].Rows[i].Field<decimal>("price")) + " руб." + Environment.NewLine;
            outInfo += "Производитель: " + ManufacturerDataManager.Instance.GetNameManufacturer(DeviceModels.Tables[0].Rows[i].Field<int>("manufacturer_FK")) + Environment.NewLine;
            outInfo += "Вес: " + DeviceModels.Tables[0].Rows[i].Field<int>("weight") + " г." + Environment.NewLine;
            outInfo += "Количество на складе: " + DeviceModels.Tables[0].Rows[i].Field<int>("stock_balance") + " шт." + Environment.NewLine;

            return outInfo;
        }

        public string GetNameDeviceModel(int idDeviceModel)
        { 
            return DeviceModels.Tables[0].Rows[idDeviceModel-1].Field<string>("model_name");
        }

        public int GetPriceDeviceModel(int idDeviceModel)
        {
            return (int)DeviceModels.Tables[0].Rows[idDeviceModel - 1].Field<decimal>("price");
        }

        public int GetModelTypeId(int idDeviceModel)
        {
            return DeviceModels.Tables[0].Rows[idDeviceModel - 1].Field<int>("modelType_FK");
        }
    }
}
