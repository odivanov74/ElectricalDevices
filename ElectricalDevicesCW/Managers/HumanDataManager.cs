using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectricalDevicesCW.Managers
{   
    public class HumanDataManager
    {
        public DataSet Users { get; set; } = new DataSet();
        public DataSet Clients { get; set; } = new DataSet(); 

        private HumanDataManager() { }

        public static HumanDataManager Instance { get => HumanDataManagerCreate.instance; }

        private class HumanDataManagerCreate
        {
            static HumanDataManagerCreate() { }
            internal static readonly HumanDataManager instance = new HumanDataManager();
        }

        public List<string> GetFullDataListUsers()
        {
            List<string> users = new List<string>();
            string clientInfo = "";

            for (int i = 0; i < Users.Tables[0].Rows.Count; i++)
            {
                for (int j = 0; j < Clients.Tables[0].Rows.Count; j++)
                {
                    if (Clients.Tables[0].Rows[j].Field<int>("user_FK") == Users.Tables[0].Rows[i].Field<int>("user_id"))
                    {
                        clientInfo = $".{Clients.Tables[0].Rows[j].Field<string>("client_name")}." +
                                     $"{Clients.Tables[0].Rows[j].Field<string>("phone")}." +
                                     $"{Clients.Tables[0].Rows[j].Field<int>("personal_discount")}";
                        break;
                    }
                }
                users.Add($"{Users.Tables[0].Rows[i].Field<int>("user_id")}." +                                   
                                    $"{Users.Tables[0].Rows[i].Field<string>("user_login")}." +
                                    $"{Users.Tables[0].Rows[i].Field<string>("user_password")}." +                                   
                                    $"{Users.Tables[0].Rows[i].Field<string>("role")}" +
                                    clientInfo);

                clientInfo = "";
            }
            return users;
        }

        public List<string> GetRoleList()
        {
            List<string> roles = new List<string>();

            roles.Add("administrator");
            roles.Add("manager");
            roles.Add("client");

            return roles;
        }

        public int GetLastUserId()
        {
            int id = Users.Tables[0].Rows[Users.Tables[0].Rows.Count-1].Field<int>("user_id");
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

            for (int i = 0; i < Users.Tables[0].Rows.Count; i++)
            {
                if(id == Users.Tables[0].Rows[i].Field<int>("user_id"))
                {
                    user = new User(Users.Tables[0].Rows[i].Field<int>("user_id"),                                    
                                    Users.Tables[0].Rows[i].Field<string>("user_login"),
                                    Users.Tables[0].Rows[i].Field<string>("user_password"),
                                    Users.Tables[0].Rows[i].Field<string>("role"));
                }
            }

            return user;
        }

        public Client GetClient(int userId)
        {
            Client client = null;
            for (int i = 0; i < Clients.Tables[0].Rows.Count; i++)
            {
                if (Clients.Tables[0].Rows[i].Field<int>("user_FK")== userId)
                {
                    client = new Client(Clients.Tables[0].Rows[i].Field<int>("client_id"),
                                        Clients.Tables[0].Rows[i].Field<string>("client_name"),
                                        Clients.Tables[0].Rows[i].Field<string>("phone"),
                                        Clients.Tables[0].Rows[i].Field<int>("personal_discount"),
                                        userId);
                    break;
                }
            }
            return client;
        }

        public string GetClientInfo(int clientId)
        {
            string outStr = "";
            for (int i = 0; i < Clients.Tables[0].Rows.Count; i++)
            {
                if (Clients.Tables[0].Rows[i].Field<int>("client_id") == clientId)
                {
                    outStr = $"{Clients.Tables[0].Rows[i].Field<string>("client_name")} {Clients.Tables[0].Rows[i].Field<string>("phone")}";
                    break;
                }
            }
            return outStr;               
        }

        public int GetPersonalDiscount(int clientId)
        {
            int personalDiscount = 0;
            for (int i = 0; i < Clients.Tables[0].Rows.Count; i++)
            {
                if(Clients.Tables[0].Rows[i].Field<int>("client_id") == clientId)
                {
                    personalDiscount = Clients.Tables[0].Rows[i].Field<int>("personal_discount");
                    break;
                }
            }
            return personalDiscount;
        }

        public int GetClientId(int userId)
        {
            int idClient = 0;
            for (int i = 0; i < Clients.Tables[0].Rows.Count; i++)
            {
                if(Clients.Tables[0].Rows[i].Field<int>("user_FK") == userId)
                {
                    idClient = Clients.Tables[0].Rows[i].Field<int>("client_id");
                    break;
                }
            }
            return idClient;
        }        
    }
}
