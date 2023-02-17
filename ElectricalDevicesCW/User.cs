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
        public string Login { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }

        public User(string login, string password, string role)
        {
            Login = login;
            Password = password;
            Role = role;            
        }

        public User(int id, string login, string password, string role)
        {
            Id = id;            
            Login = login;
            Password = password;
            Role = role;            
        }
    }
}
