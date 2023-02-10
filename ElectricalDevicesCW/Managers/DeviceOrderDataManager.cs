using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectricalDevicesCW.Managers
{
    public class DeviceOrderDataManager
    {
        public DataSet DeviceOrders { get; set; } = new DataSet();

        private DeviceOrderDataManager() { }

        public static DeviceOrderDataManager Instance { get => DeviceOrderDataManagerCreate.instance; }

        private class DeviceOrderDataManagerCreate
        {
            static DeviceOrderDataManagerCreate() { }
            internal static readonly DeviceOrderDataManager instance = new DeviceOrderDataManager();
        }

        public List<string> GetFullDataListDeviceOrder()
        {
            List<string> deviceOrders = new List<string>();

            for (int i = 0; i < DeviceOrders.Tables[0].Rows.Count; i++)
            {
                deviceOrders.Add($"{DeviceOrders.Tables[0].Rows[i].Field<int>("device_id")}." +
                                    $"{DeviceOrders.Tables[0].Rows[i].Field<int>("order_id")}." +
                                    $"{DeviceOrders.Tables[0].Rows[i].Field<int>("amount")}");
            }
            return deviceOrders;
        }

        public int GetCountDeviceModel(int idDevice, int idOrder)
        {
            int count = 0;
            int deviceModelFK = DeviceDataManager.Instance.GetDeviceModelId(idDevice);

            for (int i = 0; i < DeviceOrders.Tables[0].Rows.Count; i++)
            {
                if(DeviceDataManager.Instance.GetDeviceModelId(idDevice) == deviceModelFK && DeviceOrders.Tables[0].Rows[i].Field<int>("order_id") == idOrder)
                {
                    count++;
                }
            }
            return count;
        }

        public List<string> GetDataListDevice(int idOrder)
        {
            List<string> devices = new List<string>();
            string modelName = "";
            List<string> modelNames = new List<string>();
            int idDevice = 0;
            int idDeviceModel = 0;
            int idModelType = 0;

            for (int i = 0; i < DeviceOrders.Tables[0].Rows.Count; i++)
            {
                if(DeviceOrders.Tables[0].Rows[i].Field<int>("order_id") == idOrder)
                {
                    idDevice = DeviceOrders.Tables[0].Rows[i].Field<int>("device_id");                    
                    idDeviceModel = DeviceDataManager.Instance.GetDeviceModelId(idDevice);
                    idModelType = DeviceModelDataManager.Instance.GetModelTypeId(idDeviceModel);
                    modelName = DeviceModelDataManager.Instance.GetNameDeviceModel(idDeviceModel);

                    if(modelNames.Contains(modelName)==false)
                    {
                        modelNames.Add(modelName);
                        devices.Add($"{modelName} " +
                                $"{ModelTypeDataManager.Instance.GetNameModelType(DeviceDataManager.Instance.GetDeviceModelId(idDevice))} " +
                                $"{GetCountDeviceModel(idDevice, idOrder)} шт." +
                                $"{DeviceModelDataManager.Instance.GetPriceDeviceModel(idModelType)} руб.");
                    }                    
                }                
            }
            return devices;
        }

        public int GetTotalCost(int idOrder)
        {
            int price = 0;
            int idDevice = 0;
            int idDeviceModel = 0;
            int idModelType = 0;

            for (int i = 0; i < DeviceOrders.Tables[0].Rows.Count; i++)
            {
                if (DeviceOrders.Tables[0].Rows[i].Field<int>("order_id") == idOrder)
                {
                    idDevice = DeviceOrders.Tables[0].Rows[i].Field<int>("device_id");
                    idDeviceModel = DeviceDataManager.Instance.GetDeviceModelId(idDevice);
                    idModelType = DeviceModelDataManager.Instance.GetModelTypeId(idDeviceModel);
                    price += DeviceModelDataManager.Instance.GetPriceDeviceModel(idModelType);                    
                }
            }
            return price;
        }

    }
}
