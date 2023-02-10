using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectricalDevicesCW.Managers
{
    public class ManufacturerDataManager
    {
        public DataSet Manufacturers { get; set; } = new DataSet();           

        private ManufacturerDataManager() { }

        public static ManufacturerDataManager Instance { get => ManufacturerDataManagerCreate.instance; }        

        private class ManufacturerDataManagerCreate
        {
            static ManufacturerDataManagerCreate() { }
            internal static readonly ManufacturerDataManager instance = new ManufacturerDataManager();
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

        public string GetNameManufacturer(int id)
        {
            return Manufacturers.Tables[0].Rows[id-1].Field<string>("manufacturer_name");

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
    }
}
