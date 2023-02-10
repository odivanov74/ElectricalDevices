using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectricalDevicesCW.Managers
{
    class OrderDataManager_
    {
        public DataSet Orders { get; set; } = new DataSet();

        private OrderDataManager_() { }

        public static OrderDataManager_ Instance { get => OrderDataManagerCreate.instance; }

        private class OrderDataManagerCreate
        {
            static OrderDataManagerCreate() { }
            internal static readonly OrderDataManager_ instance = new OrderDataManager_();
        }

        public List<string> GetFullDataListOrder()
        {
            List<string> orders = new List<string>();

            for (int i = 0; i < Orders.Tables[0].Rows.Count; i++)
            {
                orders.Add($"{Orders.Tables[0].Rows[i].Field<int>("order_id")}." +
                          $"{Orders.Tables[0].Rows[i].Field<string>("order_name")}." +
                          $"{Orders.Tables[0].Rows[i].Field<DateTime>("order_date")}." +
                          $"{Orders.Tables[0].Rows[i].Field<DateTime>("shiping_date")}");
            }
            return orders;
        }

        public DateTime GetOrderDate(int id)
        {
            DateTime dt = new DateTime();

            for (int i = 0; i < Orders.Tables[0].Rows.Count; i++)
            {
                if (Orders.Tables[0].Rows[i].Field<int>("order_id") == id)
                {
                    dt = Orders.Tables[0].Rows[i].Field<DateTime>("order_date");
                    break;
                }
            }
            return dt;
        }

        public DateTime GetShipingDate(int id)
        {
            DateTime dt = new DateTime();

            for (int i = 0; i < Orders.Tables[0].Rows.Count; i++)
            {
                if (Orders.Tables[0].Rows[i].Field<int>("order_id") == id)
                {
                    dt = Orders.Tables[0].Rows[i].Field<DateTime>("shiping_date");
                    break;
                }
            }
            return dt;
        }

        public string GenerateNewOrderName()
        {
            string str = Orders.Tables[0].Rows[Orders.Tables[0].Rows.Count-1].Field<string>("order_name");
            string[] aStr = str.Split('-');
            int count = int.Parse(aStr[1]);
            return aStr[0] + "-" + (++count).ToString();
        }
    }
}
