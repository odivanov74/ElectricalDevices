using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectricalDevicesCW.Managers
{
    public class SupplierDataManager
    {
        public DataSet Suppliers { get; set; } = new DataSet();

        private SupplierDataManager() { }

        public static SupplierDataManager Instance { get => SupplierDataManagerCreate.instance; }

        private class SupplierDataManagerCreate
        {
            static SupplierDataManagerCreate() { }
            internal static readonly SupplierDataManager instance = new SupplierDataManager();
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

        public string GetNameSupplier(int id)
        {
            return Suppliers.Tables[0].Rows[id-1].Field<string>("supplier_name");
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
    }
}
