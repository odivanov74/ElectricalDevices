using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectricalDevicesCW.Managers
{
    public class UserRightDataManager
    {
        public DataSet UserRights { get; set; } = new DataSet();

        private UserRightDataManager() { }

        public static UserRightDataManager Instance { get => UserRightDataManagerCreate.instance; }

        private class UserRightDataManagerCreate
        {
            static UserRightDataManagerCreate() { }
            internal static readonly UserRightDataManager instance = new UserRightDataManager();
        }

        public List<string> GetFullDataListUserRights()
        {
            List<string> userRights = new List<string>();

            for (int i = 0; i < UserRights.Tables[0].Rows.Count; i++)
            {
                userRights.Add($"{UserRights.Tables[0].Rows[i].Field<int>("user_id")}." +
                    $"{UserRights.Tables[0].Rows[i].Field<int>("right_id")}");
            }
            return userRights;
        }

        public List<int> GetUserRightsId(int userId)
        {
            List<int> userRightId = new List<int>();

            for (int i = 0; i < UserRights.Tables[0].Rows.Count; i++)
            {
                if(UserRights.Tables[0].Rows[i].Field<int>("user_id") == userId)
                    userRightId.Add(UserRights.Tables[0].Rows[i].Field<int>("right_id"));
            }
            return userRightId;
        }
    }
}
