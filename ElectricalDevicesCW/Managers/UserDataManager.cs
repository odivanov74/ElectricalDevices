using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectricalDevicesCW.Managers
{
    public class UserDataManager
    {
        public DataSet Users { get; set; } = new DataSet();

        private UserDataManager() { }

        public static UserDataManager Instance { get => UserDataManagerCreate.instance; }

        private class UserDataManagerCreate
        {
            static UserDataManagerCreate() { }
            internal static readonly UserDataManager instance = new UserDataManager();
        }

        public List<string> GetFullDataListUsers()
        {
            List<string> users = new List<string>();

            for (int i = 0; i < Users.Tables[0].Rows.Count; i++)
            {
                users.Add($"{Users.Tables[0].Rows[i].Field<int>("user_id")}." +
                                    $"{Users.Tables[0].Rows[i].Field<string>("user_name")}." +
                                    $"{Users.Tables[0].Rows[i].Field<string>("user_login")}." +
                                    $"{Users.Tables[0].Rows[i].Field<string>("user_password")}." +
                                    $"{Users.Tables[0].Rows[i].Field<string>("phone")}." +
                                    $"{Users.Tables[0].Rows[i].Field<int>("personal_discount")}");
            }
            return users;
        } 
        
        public int GetLastUserId()
        {
            int id = Users.Tables[0].Rows[Users.Tables[0].Rows.Count - 1].Field<int>("user_id");
            return id;
        }

        public int GetUserId(string login, string password)
        {
            for (int i = 0; i < Users.Tables[0].Rows.Count; i++)
            {
                if(Users.Tables[0].Rows[i].Field<string>("user_login") == login && Users.Tables[0].Rows[i].Field<string>("user_password") == password)
                {
                    return Users.Tables[0].Rows[i].Field<int>("user_id");
                }
            }
            return 0;
        }

        public User GetUser(string login, string password)
        {           
            int id = GetUserId(login, password);
            if (id == 0) return null;
            
            User user = null;

            List<int> rightIds = UserRightDataManager.Instance.GetUserRightsId(id);
            List<string> rightNames = RightDataManager.Instance.GetNameListRightsUser(rightIds);
            List<Right> rights = new List<Right>();

            for (int i = 0; i < rightIds.Count; i++)
            {
                rights.Add(new Right(rightIds[i], rightNames[i]));
            }

            for (int i = 0; i < Users.Tables[0].Rows.Count; i++)
            {
                if(id == Users.Tables[0].Rows[i].Field<int>("user_id"))
                {
                    user = new User(Users.Tables[0].Rows[i].Field<int>("user_id"),
                                    Users.Tables[0].Rows[i].Field<string>("user_name"),
                                    Users.Tables[0].Rows[i].Field<string>("user_login"),
                                    Users.Tables[0].Rows[i].Field<string>("user_password"),
                                    Users.Tables[0].Rows[i].Field<string>("phone"),
                                    Users.Tables[0].Rows[i].Field<int>("personal_discount"),
                                    rights);
                }
            }

            return user;
        }

        //public async Task<User> CheckLoginAsync(string login, string password)
        //{
        //    User user = GetUser(login, password);

        //    for (int i = 0; i < Users.Tables[0].Rows.Count; i++)
        //    {
        //        if (Users.Tables[0].Rows[i].Field<string>("user_login") == login && userData.Tables[0].Rows[i].Field<string>("user_password") == password)
        //        {
        //            int id = Users.Tables[0].Rows[i].Field<int>("user_id");
        //            string userName = Users.Tables[0].Rows[i].Field<string>("user_name");
        //            string userLogin = Users.Tables[0].Rows[i].Field<string>("user_login");
        //            string userPassword = Users.Tables[0].Rows[i].Field<string>("user_password");
        //            string phone = Users.Tables[0].Rows[i].Field<string>("phone");
        //            int discount = Users.Tables[0].Rows[i].Field<int>("personal_discount");

        //            rightData = new DataSet();
        //            SqlDataAdapter adapter = new SqlDataAdapter($"Select rights.right_id, right_name from rights " +
        //                                            $"inner join userRight as UR on rights.right_id = UR.right_id " +
        //                                            $"inner join users on users.user_id = UR.user_id " +
        //                                            $"where users.user_name = '{userName}'; ",
        //                                            connection);
        //            commandBuilder = new SqlCommandBuilder(adapter);
        //            await Task.Run(() => adapter.Fill(rightData));

        //            currentUsers.Add(new User(id, userName, userLogin, userPassword, phone, discount, rightData));
        //            return currentUsers.Last();
        //        }
        //    }
        //    return null;
        //}
    }
}
