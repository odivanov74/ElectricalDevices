using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectricalDevicesCW.Managers
{
    public class ShopDataManager
    {
        public DataSet Orders { get; set; } = new DataSet();
        public DataSet ModelOrder { get; set; } = new DataSet();
        public DataSet Baskets { get; set; } = new DataSet();
        public DataSet ModelBasket { get; set; } = new DataSet();

        private ShopDataManager() { }

        public static ShopDataManager Instance { get => ShopDataManagerCreate.instance; }

        private class ShopDataManagerCreate
        {
            static ShopDataManagerCreate() { }
            internal static readonly ShopDataManager instance = new ShopDataManager();
        }


        public List<string> GetFullDataListOrder()
        {
            List<string> orders = new List<string>();

            for (int i = 0; i < Orders.Tables[0].Rows.Count; i++)
            {
                orders.Add($"{Orders.Tables[0].Rows[i].Field<int>("order_id")}." +
                          $"{Orders.Tables[0].Rows[i].Field<string>("order_name")}." +
                          $"{Orders.Tables[0].Rows[i].Field<DateTime>("order_date")}." +
                          $"{Orders.Tables[0].Rows[i].Field<int>("client_FK")}");
            }
            return orders;
        }

        public DateTime GetOrderDate(int idOrder)
        {
            DateTime dt = new DateTime();

            for (int i = 0; i < Orders.Tables[0].Rows.Count; i++)
            {
                if (Orders.Tables[0].Rows[i].Field<int>("order_id") == idOrder)
                {
                    dt = Orders.Tables[0].Rows[i].Field<DateTime>("order_date");
                    break;
                }
            }
            return dt;
        }

        public string GetOrderName(int idOrder)
        {
            string name = "";

            for (int i = 0; i < Orders.Tables[0].Rows.Count; i++)
            {
                if (Orders.Tables[0].Rows[i].Field<int>("order_id") == idOrder)
                {
                    name = Orders.Tables[0].Rows[i].Field<string>("order_name");
                    break;
                }
            }
            return name;
        }

        public string GenerateNewOrderName()
        {
            string outStr = "";
            int count = 0;
            if (Orders.Tables[0].Rows.Count > 0)
            {
                string str = Orders.Tables[0].Rows[Orders.Tables[0].Rows.Count - 1].Field<string>("order_name");
                string[] aStr = str.Split('-');
                count = int.Parse(aStr[1]);
                outStr = "ORDER-" + (++count).ToString();
            }
            else
            {
                outStr = "ORDER-1";
            }
            return outStr;
        }

        public int GetLastOrderId()
        {
            return Orders.Tables[0].Rows[Orders.Tables[0].Rows.Count - 1].Field<int>("order_id");             
        }

        public List<string> GetDataListModel(int idOrder)
        {
            List<string> devices = new List<string>();
            string modelName = "";
            int idModel = 0;
            int idManufacturer = 0;
            int idType = 0;

            for (int i = 0; i < ModelOrder.Tables[0].Rows.Count; i++)
            {
                if (ModelOrder.Tables[0].Rows[i].Field<int>("order_id") == idOrder)
                {
                    idModel = ModelOrder.Tables[0].Rows[i].Field<int>("model_id");
                    idType = ModelDataManager.Instance.GetTypeId(idModel);
                    idManufacturer = ModelDataManager.Instance.GetManufacturerId(idModel);
                    modelName = ModelDataManager.Instance.GetNameModel(idModel);
                    devices.Add($"{modelName} " +
                               $"{ModelDataManager.Instance.GetNameType(idType)} " +
                               $"{ModelDataManager.Instance.GetNameManufacturer(idManufacturer)} " +
                               $"{ModelOrder.Tables[0].Rows[i].Field<int>("amount")} шт. " +
                               $"{ModelDataManager.Instance.GetPriceModel(idModel)} руб.");
                }
            }
            return devices;
        }       

        public List<string> GetFullListBasket(int idClient)
        {
            List<string> baskets = new List<string>();
            for (int i = 0; i < Baskets.Tables[0].Rows.Count; i++)
            {
                if (Baskets.Tables[0].Rows[i].Field<int>("client_FK") == idClient)
                {
                    baskets.Add($"{Baskets.Tables[0].Rows[i].Field<int>("basket_id")}."+
                   $"{Baskets.Tables[0].Rows[i].Field<string>("basket_name")}");
                }
            }            
            return baskets;
        }

        public List<string> GetFullDataListBasket(int idBasket)
        {
            List<string> modelBaskets = new List<string>();
            int idModel = 0;
            int amount = 0;

            for (int i = 0; i < ModelBasket.Tables[0].Rows.Count; i++)
            {
                if (ModelBasket.Tables[0].Rows[i].Field<int>("basket_id") == idBasket)
                {
                    idModel = ModelBasket.Tables[0].Rows[i].Field<int>("model_id");
                    amount = ModelBasket.Tables[0].Rows[i].Field<int>("amount");
                    modelBaskets.Add(   $"{idModel}." +
                                        $"{ModelDataManager.Instance.GetNameModel(idModel)}." +
                                        $"{ModelDataManager.Instance.GetPriceModel(idModel)}." +
                                        $"{amount}");
                }
            }
            
            return modelBaskets;
        }

        public int GetPriceBasket(int idBasket)
        {
            int price = 0;
            int idModel = 0;
            int amount = 0;
            int priceModel = 0;

            for (int i = 0; i < ModelBasket.Tables[0].Rows.Count; i++)
            {
                if (ModelBasket.Tables[0].Rows[i].Field<int>("basket_id") == idBasket)
                {
                    idModel = ModelBasket.Tables[0].Rows[i].Field<int>("model_id");
                    amount = ModelBasket.Tables[0].Rows[i].Field<int>("amount");
                    priceModel = ModelDataManager.Instance.GetPriceModel(idModel);

                    price += priceModel * amount;
                }
            }
            return price;
        }

        public int GetTotalCost(int idOrder)
        {
            int price = 0;
            int idModel = 0;            

            for (int i = 0; i < ModelOrder.Tables[0].Rows.Count; i++)
            {
                if (ModelOrder.Tables[0].Rows[i].Field<int>("order_id") == idOrder)
                {
                    idModel = ModelOrder.Tables[0].Rows[i].Field<int>("model_id");                    
                    price += ModelDataManager.Instance.GetPriceModel(idModel)* ModelOrder.Tables[0].Rows[i].Field<int>("amount");
                }
            }
            return price;
        }

        public int GetNumModelToBasket(int idModel)
        {
            int result = 0;
            for (int i = 0; i < ModelBasket.Tables[0].Rows.Count; i++)
            {
                if(ModelBasket.Tables[0].Rows[i].Field<int>("model_id") == idModel)
                {
                    result = ModelBasket.Tables[0].Rows[i].Field<int>("amount");
                    break;
                }
            }
            return result;
        }

        public int GetNumSaleModelToModelOrder(int idModel)
        {
            int count = 0;
            for (int i = 0; i < ModelOrder.Tables[0].Rows.Count; i++)
            {
                if (ModelOrder.Tables[0].Rows[i].Field<int>("model_id") == idModel)
                {
                    count++;
                }
            }
            return count;
        }

    }
}
