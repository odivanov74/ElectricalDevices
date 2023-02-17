using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectricalDevicesCW
{
    public class Client
    {
        public int Id { get; set; }
        public string Name { get; set; }        
        public string Phone { get; set; }
        public int PersonalDiscount { get; set; }
        public int UserId { get; set; }

        public Client(int id, string name, string phone, int personalDiscount, int userId)
        {
            Id = id;
            Name = name;
            Phone = phone;
            PersonalDiscount = personalDiscount;
            UserId = userId;
        }
    }
}
