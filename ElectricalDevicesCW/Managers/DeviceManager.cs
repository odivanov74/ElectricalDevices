using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectricalDevicesCW.Managers
{
    public class DeviceManager
    {
        SqlConnection connection;

        SqlDataAdapter adapterDevice = new SqlDataAdapter();
        SqlDataAdapter adapterDeviceModel = new SqlDataAdapter();
        DataSet deviceData;
        DataSet deviceModelData;


        SqlCommandBuilder commandBuilder;

        public DeviceManager(SqlConnection connection)
        {
            this.connection = connection;
            LoadDeviceModelBaseAsync();
        }

        public async Task<int> LoadDeviceBaseAsync()
        {
            deviceData = new DataSet();
            adapterDevice = new SqlDataAdapter($"Select * from devices;", connection);
            commandBuilder = new SqlCommandBuilder(adapterDevice);
            if (await Task.Run(() => adapterDevice.Fill(deviceData)) > 0)
            {
                return deviceData.Tables[0].Rows[deviceData.Tables[0].Rows.Count - 1].Field<int>("device_id");
            }
            return 0;
        }

        public async Task<int> LoadDeviceModelBaseAsync()
        {
            deviceModelData = new DataSet(); 
            adapterDeviceModel = new SqlDataAdapter($"Select deviceModel_id, model_name, model_type, weight, price, stock_balance, manufacturer_name, suplier_name  from deviceModels " +
                                                    $"inner join manufacturers on manufacturers.manufacturer_id = manufacturer_FK " +
                                                    $"inner join suppliers on suppliers.supplier_id = supplier_FK;", connection);
            commandBuilder = new SqlCommandBuilder(adapterDeviceModel);
            if (await Task.Run(() => adapterDeviceModel.Fill(deviceModelData)) > 0)
            {
                return deviceModelData.Tables[0].Rows[deviceModelData.Tables[0].Rows.Count - 1].Field<int>("deviceModel_id");
            }
            return 0;
        }

        public List<string> GetListDevices()
        {
            List<string> devices = new List<string>();
            for (int i = 0; i < deviceData.Tables[0].Rows.Count; i++)
            {
                devices.Add($"{deviceData.Tables[0].Rows[i].Field<int>("device_id")}." +
                          $"{deviceData.Tables[0].Rows[i].Field<int>("deviceModel_FK")}." +
                          $"{deviceData.Tables[0].Rows[i].Field<string>("serial_number")}." +
                          $"{deviceData.Tables[0].Rows[i].Field<DateTime>("manufacture_date")}." +
                          $"{deviceData.Tables[0].Rows[i].Field<bool>("isDefected")}." +
                          $"{deviceData.Tables[0].Rows[i].Field<int>("order_FK")}");
            }
            return devices;
        }

        public List<string> GetListDeviceModels()
        {
            List<string> deviceModels = new List<string>();
            
            for (int i = 0; i < deviceModelData.Tables[0].Rows.Count; i++)
            {
                deviceModels.Add($"{deviceModelData.Tables[0].Rows[i].Field<int>("deviceModel_id")}." +
                          $"{deviceModelData.Tables[0].Rows[i].Field<string>("model_name")}." +
                          $"{deviceModelData.Tables[0].Rows[i].Field<string>("model_type")}." + 
                          $"{deviceModelData.Tables[0].Rows[i].Field<string>("manufacturer_name")}");
            }
            return deviceModels;
        }

        public string GetInfoDeviceModel(string infoStr)
        {
            string outInfo = "";
            string[] result = infoStr.Split('.');
            int i = int.Parse(result[0])-1;

            outInfo = "Модель: " + deviceModelData.Tables[0].Rows[i].Field<string>("model_name") + Environment.NewLine;
            outInfo += "Тип: " + deviceModelData.Tables[0].Rows[i].Field<string>("model_type") + Environment.NewLine;
            outInfo += "Цена: " + (int)(deviceModelData.Tables[0].Rows[i].Field<decimal>("price")) + " руб." + Environment.NewLine;
            outInfo += "Производитель: " + deviceModelData.Tables[0].Rows[i].Field<string>("manufacturer_name") + Environment.NewLine;
            outInfo += "Вес: " + deviceModelData.Tables[0].Rows[i].Field<int>("weight") +" г." + Environment.NewLine;
            outInfo += "Количество на складе: " + deviceModelData.Tables[0].Rows[i].Field<int>("stock_balance") + " шт." + Environment.NewLine;

            return outInfo;
        }
    }
}
