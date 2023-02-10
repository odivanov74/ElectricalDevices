using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectricalDevicesCW.Managers
{
    public class DeviceDataManager
    {
        public DataSet Devices { get; set; } = new DataSet();

        private DeviceDataManager() { }

        public static DeviceDataManager Instance { get => DeviceDataManagerCreate.instance; }

        private class DeviceDataManagerCreate
        {
            static DeviceDataManagerCreate() { }
            internal static readonly DeviceDataManager instance = new DeviceDataManager();
        }

        public List<string> GetFullDataListDevice()
        {
            List<string> devices = new List<string>();

            for (int i = 0; i < Devices.Tables[0].Rows.Count; i++)
            {
                devices.Add($"{Devices.Tables[0].Rows[i].Field<int>("device_id")}." +
                          $"{Devices.Tables[0].Rows[i].Field<int>("deviceModel_FK")}." +
                          $"{Devices.Tables[0].Rows[i].Field<string>("serial_number")}." +
                          $"{Devices.Tables[0].Rows[i].Field<DateTime>("manufacture_date")}." +
                          $"{Devices.Tables[0].Rows[i].Field<bool>("isDefected")}");
            }
            return devices;
        }

        public DateTime GetDateManufactureDevice(int id)
        {
            DateTime dt = new DateTime();

            for (int i = 0; i < Devices.Tables[0].Rows.Count; i++)
            {
                if(Devices.Tables[0].Rows[i].Field<int>("device_id")==id)
                {
                    dt = Devices.Tables[0].Rows[i].Field<DateTime>("manufacture_date");
                    break;
                }
            }
            return dt;
        }

        public bool GetStatusDevice(int id)
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

        public int GetDeviceModelId(int id)
        {
            int fK = 0;
            for (int i = 0; i < Devices.Tables[0].Rows.Count; i++)
            {
                if (Devices.Tables[0].Rows[i].Field<int>("device_id") == id)
                {
                    fK = Devices.Tables[0].Rows[i].Field<int>("deviceModel_FK");
                    break;
                }
            }
            return fK;
        }
    }
}
