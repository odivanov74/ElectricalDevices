using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectricalDevicesCW
{
    public class Right
    {
        public int Id { get; set; }
        public string Name { get; set; }
        
        public Right(int id, string name)
        {
            Id = id;
            Name = name;
        }

    }
}
