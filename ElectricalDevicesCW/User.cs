using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectricalDevicesCW
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }
        public int PersonalDiscount { get; set; }
        //public DataSet RightData { get; set; }
        public List<Right> Rights { get; set; } = new List<Right>();

        public User(string name, string login, string password, string phone, int discount, List<Right> rights)
        {            
            Name = name;
            Login = login;
            Password = password;
            Phone = phone;
            PersonalDiscount = discount;
            Rights = rights;
        }

        public User(int id, string name, string login, string password, string phone, int discount, List<Right> rights)
        {
            Id = id;
            Name = name;
            Login = login;
            Password = password;
            Phone = phone;
            PersonalDiscount = discount;
            Rights = rights;
        }

        //public User(int id, string name, string login, string password, string phone, int discount, DataSet rightData)
        //{
        //    Id = id;
        //    Name = name;
        //    Login = login;
        //    Password = password;
        //    Phone = phone;
        //    PersonalDiscount = discount;
        //    RightData = rightData;
        //} 
        
        public List<string> GetNameRights()
        {
            List<string> listNameRight = new List<string>();
            for (int i = 0; i < Rights.Count; i++)
            {
                listNameRight.Add(Rights[i].Name);
            }
            return listNameRight;
        }

        public List<int> GetIdRights()
        {
            List<int> listIdRight = new List<int>();
            for (int i = 0; i < Rights.Count; i++)
            {
                listIdRight.Add(Rights[i].Id);
            }
            return listIdRight;
        }
    }
}
