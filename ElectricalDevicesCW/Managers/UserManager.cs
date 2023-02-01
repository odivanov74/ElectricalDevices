using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ElectricalDevicesCW
{
    public class UserManager
    {
        //DataBaseService dataBase;
        SqlDataAdapter adapterUser = new SqlDataAdapter();
        SqlDataAdapter adapterRight = new SqlDataAdapter();
        SqlDataAdapter adapterUserRight = new SqlDataAdapter();

        SqlCommandBuilder commandBuilder;
        SqlConnection connection;

        DataSet userData;
        DataSet rightData;
        DataSet userRightData;

        List<User> currentUsers = new List<User>();

        public UserManager(SqlConnection connection)
        {            
            //this.dataBase = dataBase;
            this.connection = connection;
            LoadUserBaseAsync();            
        }

        public List<string> GetListUser()
        {
            List<string> users = new List<string>();
            for (int i = 0; i < userData.Tables[0].Rows.Count; i++)
            {
                users.Add($"{userData.Tables[0].Rows[i].Field<int>("user_id")}." +
                          $"{userData.Tables[0].Rows[i].Field<string>("user_name")}." +
                          $"{userData.Tables[0].Rows[i].Field<string>("user_login")}." +
                          $"{userData.Tables[0].Rows[i].Field<string>("user_password")}." +
                          $"{userData.Tables[0].Rows[i].Field<string>("phone")}." +
                          $"{userData.Tables[0].Rows[i].Field<int>("personal_discount")}");
            }
            return users;
        }

        public async Task<List<string>> GetListRightUser(int userId)
        {
            if(await LoadUserRightBaseAsync(userId)==true)
            {
                List<string> rights = new List<string>();
                for (int i = 0; i < rightData.Tables[0].Rows.Count; i++)
                {
                    rights.Add($"{rightData.Tables[0].Rows[i].Field<int>("right_id")}. " +
                              $"{rightData.Tables[0].Rows[i].Field<string>("right_name")}");
                }
                return rights;
            }
            return null;
        }

        public async Task<int> LoadUserBaseAsync()
        {
            userData = new DataSet();
            adapterUser = new SqlDataAdapter($"Select * from users;", connection);
            commandBuilder = new SqlCommandBuilder(adapterUser);                    
            if(await Task.Run(() => adapterUser.Fill(userData))>0)
            {
                return userData.Tables[0].Rows[userData.Tables[0].Rows.Count - 1].Field<int>("user_id");
            }
            return 0;
        }

        public async Task<bool> LoadUserRightBaseAsync(int userId)
        {
            rightData = new DataSet();
            adapterRight = new SqlDataAdapter($"Select rights.right_id, right_name from rights " +
                                            $"inner join userRight as UR on rights.right_id = UR.right_id " +
                                            $"inner join users on users.user_id = UR.user_id " +
                                            $"where users.user_id = '{userId}'; ",
                                            connection);
            commandBuilder = new SqlCommandBuilder(adapterRight);
            try
            {
                await Task.Run(() => adapterRight.Fill(rightData));
                return true;
            }
            catch (Exception)
            {
                return false;
            }                   
        }

        public async Task<bool> LoadTableUserRightAsync()
        {
            userRightData = new DataSet();
            adapterRight = new SqlDataAdapter($"Select * from userRight;", connection);
            commandBuilder = new SqlCommandBuilder(adapterRight);
            try
            {
                await Task.Run(() => adapterRight.Fill(userRightData));
                return true;
            }
            catch(Exception)
            {
                return false;
            }
        }

        public async Task<List<string>> LoadAllRightAsync()
        {
            DataSet data = new DataSet();
            adapterRight = new SqlDataAdapter($"Select * from rights;", connection);
            commandBuilder = new SqlCommandBuilder(adapterRight);
            
            await Task.Run(() => adapterRight.Fill(data));            

            List<string> rights = new List<string>();
            for (int i = 0; i < data.Tables[0].Rows.Count; i++)
            {
                rights.Add(data.Tables[0].Rows[i].Field<string>("right_name"));
            }
            return rights;
        }

        public async Task<User> CheckLoginAsync(string login, string password)
        {            
            for (int i = 0; i < userData.Tables[0].Rows.Count; i++)
            {
                if (userData.Tables[0].Rows[i].Field<string>("user_login") == login && userData.Tables[0].Rows[i].Field<string>("user_password") == password)
                {
                    int id = userData.Tables[0].Rows[i].Field<int>("user_id");
                    string userName = userData.Tables[0].Rows[i].Field<string>("user_name");
                    string userLogin = userData.Tables[0].Rows[i].Field<string>("user_login");
                    string userPassword = userData.Tables[0].Rows[i].Field<string>("user_password");
                    string phone = userData.Tables[0].Rows[i].Field<string>("phone");
                    int discount = userData.Tables[0].Rows[i].Field<int>("personal_discount");                    

                    rightData = new DataSet();
                    SqlDataAdapter adapter = new SqlDataAdapter($"Select rights.right_id, right_name from rights " +
                                                    $"inner join userRight as UR on rights.right_id = UR.right_id " +
                                                    $"inner join users on users.user_id = UR.user_id " +
                                                    $"where users.user_name = '{userName}'; ",
                                                    connection);
                    commandBuilder = new SqlCommandBuilder(adapter);
                    await Task.Run(() => adapter.Fill(rightData));

                    currentUsers.Add(new User(id, userName, userLogin, userPassword, phone, discount, rightData));                    
                    return currentUsers.Last();
                }
            }
            return null;
        }
 
        public async Task<bool> AddUserAsync(User user)
        {
            if(await AddNewUserAsync(user)==true)
            {
               return await AddNewUserRightsAsync(user);
            }
            return false;            
        }

        public async Task<bool> AddNewUserAsync(User user)
        {
            for (int i = 0; i < userData.Tables[0].Rows.Count; i++)
            {
                if(userData.Tables[0].Rows[i].Field<string>("user_login")==user.Login || userData.Tables[0].Rows[i].Field<string>("user_name") == user.Name)
                {
                    return false;
                }
            }
            string cmdInsertUser = $"insert into users values ('{user.Name}', '{user.Login}', '{user.Password}', '{user.Phone}', {user.PersonalDiscount});";

            DataRow rowUsers = userData.Tables[0].NewRow();
            rowUsers["user_name"] = user.Name;
            rowUsers["user_login"] = user.Login;
            rowUsers["user_password"] = user.Password;
            rowUsers["phone"] = user.Phone;
            rowUsers["personal_discount"] = user.PersonalDiscount;
            userData.Tables[0].Rows.Add(rowUsers);

            try
            {
                await Task.Run(() =>
                {
                    adapterUser.InsertCommand = new SqlCommand(cmdInsertUser, connection);
                    adapterUser.Update(userData);
                    
                });
                return true;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }            
        }

        public async Task<bool> AddNewUserRightsAsync(User user)
        {
            string cmdInsertUserRight = "insert into userRight values ";
            int userId = user.Id;
            if (userId == 0) userId = await LoadUserBaseAsync();

            for (int i = 0; i < user.Rights.Count; i++)
            {
                cmdInsertUserRight += $"({userId},{user.Rights[i].Id})";
                if (i < user.Rights.Count - 1) cmdInsertUserRight += ",";
            }
            cmdInsertUserRight += ";";

            userRightData = new DataSet();
            DataTable table = new DataTable("Table");
            table.Columns.Add("user_id");
            table.Columns.Add("right_id");
            userRightData.Tables.Add(table);

            for (int i = 0; i < user.Rights.Count; i++)
            {
                DataRow rowRight = userRightData.Tables[0].NewRow();
                rowRight["user_id"] = userData.Tables[0].Rows.Count;
                rowRight["right_id"] = user.Rights[i].Id;
                userRightData.Tables[0].Rows.Add(rowRight);
            }

            try
            {
                await Task.Run(() =>
                {
                    SqlCommand command = new SqlCommand(cmdInsertUserRight, connection);
                    adapterRight.InsertCommand = command;
                    adapterRight.Update(userRightData);                    
                });
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }            
        }

        public async Task<bool> EditUserAsync(User user)
        {

            string cmd =  $"update users set user_name ='{user.Name}'," +
                                    $" user_login = '{user.Login}'," +
                                    $" user_password = '{user.Password}'," +
                                    $" phone = '{user.Phone}'," +
                                    $" personal_discount = {user.PersonalDiscount}" +
                                    $" where user_id = {user.Id};";
            SqlCommand update = new SqlCommand(cmd, connection);

            for (int i = 0; i < userData.Tables[0].Rows.Count; i++)
            {
                if(userData.Tables[0].Rows[i].Field<int>("user_id")==user.Id)
                {                    
                    userData.Tables[0].Rows[i]["user_name"] = user.Name;
                    userData.Tables[0].Rows[i]["user_login"] = user.Login;
                    userData.Tables[0].Rows[i]["user_password"] = user.Password;
                    userData.Tables[0].Rows[i]["phone"] = user.Phone;
                    userData.Tables[0].Rows[i]["personal_discount"] = user.PersonalDiscount;
                    break;
                }
            }
            try
            {
                await Task.Run(() =>
                {
                    adapterUser.UpdateCommand = new SqlCommand(cmd, connection);
                    adapterUser.Update(userData).ToString();                    
                });
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
            
        }

        public async Task<bool> EditUserRightAsync(User user, string userInfo="")
        {
            try
            {
                if (await DeleteUserRightAsync(userInfo) == true)
                {
                    await AddNewUserRightsAsync(user);                    
                }
                return true;
            }            
            catch(Exception)
            {
                return false;
            }
        }

        public async Task<bool> DeleteUserRightAsync(string userInfo)
        {
            await LoadTableUserRightAsync();
            string[] result = userInfo.Split('.');
            int userId = int.Parse(result[0]);
            string cmdDel = $"delete from userRight where userRight.user_id ={userId};";
            SqlCommand delete = new SqlCommand(cmdDel, connection);

            for (int i = 0; i < userRightData.Tables[0].Rows.Count; i++)
            {
                if (userRightData.Tables[0].Rows[i].Field<int>("user_id") == userId)
                {
                    userRightData.Tables[0].Rows[i].Delete();
                    break;
                }
            }
            try
            {
                await Task.Run(() =>
                {
                    adapterRight.DeleteCommand = new SqlCommand(cmdDel, connection);
                    adapterRight.Update(userRightData);

                });                
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }

        public async Task<bool> DelUserAsync(string userInfo)
        {
            string[] result = userInfo.Split('.');
            int userId = int.Parse(result[0]);
            string cmdDel = $"delete from users where user_id ={userId};";
            SqlCommand delete = new SqlCommand(cmdDel, connection);

            for (int i = 0; i < userData.Tables[0].Rows.Count; i++)
            {
                if (userData.Tables[0].Rows[i].Field<int>("user_id") == userId)
                {
                    userData.Tables[0].Rows[i].Delete();
                    break;
                }
            }

            try
            {
                await Task.Run(() =>
                {
                    adapterUser.DeleteCommand = new SqlCommand(cmdDel, connection);
                    adapterUser.Update(userData);

                });
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }

        }
    }
}
