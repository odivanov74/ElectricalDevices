using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectricalDevicesCW.Managers
{
    public class RightDataManager
    {
        public DataSet Rights { get; set; } = new DataSet();

        private RightDataManager() { }

        public static RightDataManager Instance { get => RightDataManagerCreate.instance; }

        private class RightDataManagerCreate
        {
            static RightDataManagerCreate() { }
            internal static readonly RightDataManager instance = new RightDataManager();
        }

        public List<string> GetFullDataListRights()
        {
            List<string> rights = new List<string>();

            for (int i = 0; i < Rights.Tables[0].Rows.Count; i++)
            {
                rights.Add($"{Rights.Tables[0].Rows[i].Field<string>("right_name")}");
            }
            return rights;
        }

        public List<string> GetNameListRightsUser(List<int> userRightsId)
        {
            List<string> rights = new List<string>();

            for (int i = 0; i < userRightsId.Count; i++)
            {
                for (int j = 0; j < Rights.Tables[0].Rows.Count; j++)
                {
                    if (userRightsId[i] == Rights.Tables[0].Rows[j].Field<int>("right_id"))
                    {
                        rights.Add($"{Rights.Tables[0].Rows[j].Field<string>("right_name")}");
                        break;
                    }
                }                
            }
            return rights;
        }
    }
}
