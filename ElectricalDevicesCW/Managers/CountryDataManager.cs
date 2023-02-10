using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectricalDevicesCW.Managers
{
    public class CountryDataManager
    {

        public DataSet Countries { get; set; } = new DataSet();

        private CountryDataManager() { }

        public static CountryDataManager Instance { get => CountryDataManagerCreate.instance; }

        private class CountryDataManagerCreate
        {
            static CountryDataManagerCreate() { }
            internal static readonly CountryDataManager instance = new CountryDataManager();
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

        public List<string> GetNameListCounties()
        {
            List<string> countries = new List<string>();

            for (int i = 0; i < Countries.Tables[0].Rows.Count; i++)
            {
                countries.Add($"{Countries.Tables[0].Rows[i].Field<string>("country_name")}");
            }
            return countries;
        }
    }
}
