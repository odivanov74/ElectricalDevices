using ElectricalDevicesCW.Managers;
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
    public class DataBaseService
    {
        SqlConnection connection;    

        SqlDataAdapter adapterCountry = new SqlDataAdapter();
        SqlDataAdapter adapterManufacturer = new SqlDataAdapter();
        SqlDataAdapter adapterSupplier = new SqlDataAdapter();
        SqlDataAdapter adapterType = new SqlDataAdapter();
        SqlDataAdapter adapterModel = new SqlDataAdapter();
        SqlDataAdapter adapterModelFraction = new SqlDataAdapter();
        SqlDataAdapter adapterModelQuantity = new SqlDataAdapter();
        SqlDataAdapter adapterDevice = new SqlDataAdapter();

        SqlDataAdapter adapterUser = new SqlDataAdapter();
        SqlDataAdapter adapterClient = new SqlDataAdapter();
        SqlDataAdapter adapterOrder = new SqlDataAdapter();
        SqlDataAdapter adapterModelOrder = new SqlDataAdapter();
        SqlDataAdapter adapterBasket = new SqlDataAdapter();
        SqlDataAdapter adapterModelBasket = new SqlDataAdapter();

        SqlCommandBuilder commandBuilder; 

        public DataBaseService()
        {
            connection = new SqlConnection(@"Data Source=DESKTOP-MHB46B8\SQLEXPRESS;Initial catalog=ElectricalDevices;Integrated Security=true;");
        }               

        #region сущность Country
        public async Task<string> ReadCountryTableAsync()
        {
            adapterCountry = new SqlDataAdapter($"Select * from countries;", connection);
            commandBuilder = new SqlCommandBuilder(adapterCountry);
            ModelDataManager.Instance.Countries.Clear();           

            string outStr = "";
            await Task.Run(() =>
            {
                try
                {
                    outStr = adapterCountry.Fill(ModelDataManager.Instance.Countries).ToString();
                }
                catch (Exception ex)
                {
                    outStr = ex.Message;
                }
            });
            return outStr;
        }

        public async Task<string> UpdateCountryAsync(string name, int id)
        {
            await ReadCountryTableAsync();
            if (AvailabilityСheck(ModelDataManager.Instance.Countries, "country_name", name) == true) return "Такая страна уже имеется в базе!";

            string cmd = $"update countries set country_name ='{name}' where country_id = {id};";
            SqlCommand update = new SqlCommand(cmd, connection);

            for (int i = 0; i < ModelDataManager.Instance.Countries.Tables[0].Rows.Count; i++)
            {
                if (ModelDataManager.Instance.Countries.Tables[0].Rows[i].Field<int>("country_id") == id)
                {
                    ModelDataManager.Instance.Countries.Tables[0].Rows[i]["country_name"] = name;
                    break;
                }
            }

            string outStr = "";
            await Task.Run(() =>
            {
                try
                {
                    adapterCountry.UpdateCommand = new SqlCommand(cmd, connection);
                    outStr = (adapterCountry.Update(ModelDataManager.Instance.Countries)).ToString();
                }
                catch (Exception ex)
                {
                    outStr = ex.Message;
                }
            });
            return outStr;            
        }

        public async Task<string> AddCountryAsync(string name)
        {
            await ReadCountryTableAsync();
            if (AvailabilityСheck(ModelDataManager.Instance.Countries, "country_name", name) == true) return "Такая страна уже имеется в базе!";

            string cmdInsert = $"insert into countries values ('{name}');";
            DataRow row = ModelDataManager.Instance.Countries.Tables[0].NewRow();
            row["country_name"] = name;
            ModelDataManager.Instance.Countries.Tables[0].Rows.Add(row);

            string outStr = "";
            await Task.Run(() =>
            {
                try
                {
                    adapterCountry.InsertCommand = new SqlCommand(cmdInsert, connection);
                    outStr = (adapterCountry.Update(ModelDataManager.Instance.Countries)).ToString();
                }
                catch (Exception ex)
                {
                    outStr = ex.Message;
                }
            });
            return outStr;            
        }

        public async Task<string> DeleteCountryAsync(string info)
        {
            string[] result = info.Split('.');
            int id = int.Parse(result[0]);
            await ReadCountryTableAsync();

            string cmdDel = $"delete from countries where country_id ={id};";
            SqlCommand delete = new SqlCommand(cmdDel, connection);

            for (int i = 0; i < ModelDataManager.Instance.Countries.Tables[0].Rows.Count; i++)
            {
                if (ModelDataManager.Instance.Countries.Tables[0].Rows[i].Field<int>("country_id") == id)
                {
                    ModelDataManager.Instance.Countries.Tables[0].Rows[i].Delete();
                    break;
                }
            }

            string outStr = "";
            await Task.Run(() =>
            {
                try
                {
                    adapterCountry.DeleteCommand = new SqlCommand(cmdDel, connection);                    
                    outStr = (adapterCountry.Update(ModelDataManager.Instance.Countries)).ToString();
                }
                catch (Exception ex)
                {
                    outStr = ex.Message;
                }
            });
            return outStr;
        }
        #endregion

        #region сущность Manufacturer
        public async Task<string> ReadManufacturerTableAsync()
        {
            try
            {
                adapterManufacturer = new SqlDataAdapter("Select manufacturer_id, " +
                                                            "manufacturer_name, " +
                                                            "country_FK " +
                                                            "from manufacturers " +
                                                            "inner join countries on country_FK = countries.country_id;", connection);
                commandBuilder = new SqlCommandBuilder(adapterManufacturer);
                ModelDataManager.Instance.Manufacturers.Clear();
                return (await Task.Run(() => adapterManufacturer.Fill(ModelDataManager.Instance.Manufacturers))).ToString();
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public async Task<string> UpdateManufacturerAsync(string name, int country_id, int id)
        {
            await ReadManufacturerTableAsync();
            if (AvailabilityСheck(ModelDataManager.Instance.Manufacturers, "manufacturer_name", name) == true) return "Такой производитель уже имеется в базе!";

            string cmd = $"update manufacturers set manufacturer_name ='{name}', " +
                                                    $"country_FK = {country_id} " +
                                                    $"where manufacturer_id = {id};";
            SqlCommand update = new SqlCommand(cmd, connection);

            for (int i = 0; i < ModelDataManager.Instance.Manufacturers.Tables[0].Rows.Count; i++)
            {
                if (ModelDataManager.Instance.Manufacturers.Tables[0].Rows[i].Field<int>("manufacturer_id") == id)
                {
                    ModelDataManager.Instance.Manufacturers.Tables[0].Rows[i]["manufacturer_name"] = name;
                    ModelDataManager.Instance.Manufacturers.Tables[0].Rows[i]["country_FK"] = country_id;
                    break;
                }
            }

            string outStr = "";
            await Task.Run(() =>
            {
                try
                {
                    adapterManufacturer.UpdateCommand = new SqlCommand(cmd, connection);
                    outStr = (adapterManufacturer.Update(ModelDataManager.Instance.Manufacturers)).ToString();
                }
                catch (Exception ex)
                {
                    outStr = ex.Message;
                }
            });
            return outStr;            
        }        

        public async Task<string> AddManufacturerAsync(string name, int country_id)
        {
            await ReadManufacturerTableAsync();
            if (AvailabilityСheck(ModelDataManager.Instance.Manufacturers, "manufacturer_name", name) == true) return "Такой производитель уже имеется в базе!";

            string cmdInsert = $"insert into manufacturers values ('{name}',{country_id});";
            DataRow row = ModelDataManager.Instance.Manufacturers.Tables[0].NewRow();
            row["manufacturer_name"] = name;
            row["country_FK"] = country_id;
            ModelDataManager.Instance.Manufacturers.Tables[0].Rows.Add(row);

            string outStr = "";
            await Task.Run(() =>
            {
                try
                {
                    adapterManufacturer.InsertCommand = new SqlCommand(cmdInsert, connection);
                    outStr = (adapterManufacturer.Update(ModelDataManager.Instance.Manufacturers)).ToString();
                }
                catch (Exception ex)
                {
                    outStr = ex.Message;
                }
            });
            return outStr;           
        }

        public async Task<string> DeleteManufacturerAsync(string info)
        {
            string[] result = info.Split('.');
            int id = int.Parse(result[0]);
            await ReadManufacturerTableAsync();

            string cmdDel = $"delete from manufacturers where manufacturer_id ={id};";
            SqlCommand delete = new SqlCommand(cmdDel, connection);

            for (int i = 0; i < ModelDataManager.Instance.Manufacturers.Tables[0].Rows.Count; i++)
            {
                if (ModelDataManager.Instance.Manufacturers.Tables[0].Rows[i].Field<int>("manufacturer_id") == id)
                {
                    ModelDataManager.Instance.Manufacturers.Tables[0].Rows[i].Delete();
                    break;
                }
            }

            string outStr = "";
            await Task.Run(() =>
            {
                try
                {
                    adapterManufacturer.DeleteCommand = new SqlCommand(cmdDel, connection);
                    outStr = (adapterManufacturer.Update(ModelDataManager.Instance.Manufacturers)).ToString();
                }
                catch (Exception ex)
                {
                    outStr = ex.Message;
                }
            });
            return outStr;            
        }
        #endregion

        #region сущность Supplier
        public async Task<string> ReadSupplierTableAsync()
        {
            try
            {
                adapterSupplier = new SqlDataAdapter($"Select * from suppliers;", connection);
                commandBuilder = new SqlCommandBuilder(adapterSupplier);
                ModelDataManager.Instance.Suppliers.Clear();
                return (await Task.Run(() => adapterSupplier.Fill(ModelDataManager.Instance.Suppliers))).ToString();
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public async Task<string> UpdateSupplierAsync(string name, int id)
        {
            await ReadSupplierTableAsync();
            if (AvailabilityСheck(ModelDataManager.Instance.Suppliers, "supplier_name", name) == true) return "Такой поставщик уже имеется в базе!";

            string cmd = $"update suppliers set supplier_name ='{name}' where supplier_id = {id};";
            SqlCommand update = new SqlCommand(cmd, connection);

            for (int i = 0; i < ModelDataManager.Instance.Suppliers.Tables[0].Rows.Count; i++)
            {
                if (ModelDataManager.Instance.Suppliers.Tables[0].Rows[i].Field<int>("supplier_id") == id)
                {
                    ModelDataManager.Instance.Suppliers.Tables[0].Rows[i]["supplier_name"] = name;
                    break;
                }
            }

            string outStr = "";
            await Task.Run(() =>
            {
                try
                {
                    adapterSupplier.UpdateCommand = new SqlCommand(cmd, connection);
                    outStr = (adapterSupplier.Update(ModelDataManager.Instance.Suppliers)).ToString();
                }
                catch (Exception ex)
                {
                    outStr = ex.Message;
                }
            });
            return outStr;
        }

        public async Task<string> AddSupplierAsync(string name)
        {
            await ReadSupplierTableAsync();
            if (AvailabilityСheck(ModelDataManager.Instance.Suppliers, "supplier_name", name) == true) return "Такой поставщик уже имеется в базе!";

            string cmdInsert = $"insert into suppliers values ('{name}');";
            DataRow row = ModelDataManager.Instance.Suppliers.Tables[0].NewRow();
            row["supplier_name"] = name;
            ModelDataManager.Instance.Suppliers.Tables[0].Rows.Add(row);

            string outStr = "";
            await Task.Run(() =>
            {
                try
                {
                    adapterSupplier.InsertCommand = new SqlCommand(cmdInsert, connection);
                    outStr = (adapterSupplier.Update(ModelDataManager.Instance.Suppliers)).ToString();
                }
                catch (Exception ex)
                {
                    outStr = ex.Message;
                }
            });
            return outStr;            
        }

        public async Task<string> DeleteSupplierAsync(string info)
        {
            string[] result = info.Split('.');
            int id = int.Parse(result[0]);
            await ReadSupplierTableAsync();

            string cmdDel = $"delete from suppliers where supplier_id = {id};";
            SqlCommand delete = new SqlCommand(cmdDel, connection);

            for (int i = 0; i < ModelDataManager.Instance.Suppliers.Tables[0].Rows.Count; i++)
            {
                if (ModelDataManager.Instance.Suppliers.Tables[0].Rows[i].Field<int>("supplier_id") == id)
                {
                    ModelDataManager.Instance.Suppliers.Tables[0].Rows[i].Delete();
                    break;
                }
            }

            string outStr = "";
            await Task.Run(() =>
            {
                try
                {
                    adapterSupplier.DeleteCommand = new SqlCommand(cmdDel, connection);
                    outStr = (adapterSupplier.Update(ModelDataManager.Instance.Suppliers)).ToString();
                }
                catch (Exception ex)
                {
                    outStr = ex.Message;
                }
            });
            return outStr;
        }
        #endregion

        #region сущность Model
        public async Task<string> ReadModelTableAsync()
        {
            adapterModel = new SqlDataAdapter($"Select *  from models", connection);           
            commandBuilder = new SqlCommandBuilder(adapterModel);
            ModelDataManager.Instance.Models.Clear();

            string outStr = "";
            await Task.Run(() =>
            {
                try
                {
                    outStr = adapterModel.Fill(ModelDataManager.Instance.Models).ToString();
                }
                catch (Exception ex)
                {
                    outStr = ex.Message;
                }
            });
            return outStr;
        }

        public async Task<string> AddModelAsync(string model_name,
                                                        int type_id,
                                                        int weight,
                                                        int price,
                                                        int manufacturer_id,
                                                        int supplier_id)
        {
            await ReadModelTableAsync();
            if (AvailabilityСheck(ModelDataManager.Instance.Models, "model_name", model_name) == true) return "Такая модель устройства уже имеется в базе!";

            string cmdInsert = $"insert into models values ('{model_name}', {type_id}, {weight}, {price}, 0, {manufacturer_id}, {supplier_id}, 0);";

            DataRow row = ModelDataManager.Instance.Models.Tables[0].NewRow();
            row["model_name"] = model_name;
            row["type_FK"] = type_id;
            row["weight"] = weight;
            row["price"] = price;
            row["stock_balance"] = 0;
            row["manufacturer_FK"] = manufacturer_id;
            row["supplier_FK"] = supplier_id;
            row["reserved"] = 0;
            ModelDataManager.Instance.Models.Tables[0].Rows.Add(row);

            string outStr = "";
            await Task.Run(() =>
            {
                try
                {
                    adapterModel.InsertCommand = new SqlCommand(cmdInsert, connection);
                    outStr = (adapterModel.Update(ModelDataManager.Instance.Models)).ToString();
                }
                catch (Exception ex)
                {
                    outStr = ex.Message;
                }
            });
            return outStr;            
        }

        public async Task<string> UpdateModelAsync(string model_name,
                                                        int type_id,
                                                        int weight,
                                                        int price,
                                                        int stock_balance,
                                                        int manufacturer_id,
                                                        int supplier_id,
                                                        int reserved,
                                                        int model_id)
        {
            await ReadModelTableAsync();
            //if (AvailabilityСheck(ModelDataManager.Instance.Models, "model_name", model_name) == true) return "Такая модель устройства уже имеется в базе!";

            string cmd = $"update models set model_name ='{model_name}'," +
                                    $" type_FK = {type_id}," +
                                    $" weight = {weight}," +
                                    $" price = {price}," +
                                    $" stock_balance = {stock_balance}," +
                                    $" manufacturer_FK = {manufacturer_id}," +
                                    $" supplier_FK = {supplier_id}," +
                                    $" reserved = {reserved}" +
                                    $" where model_id = {model_id};";
            SqlCommand update = new SqlCommand(cmd, connection);

            for (int i = 0; i < ModelDataManager.Instance.Models.Tables[0].Rows.Count; i++)
            {
                if (ModelDataManager.Instance.Models.Tables[0].Rows[i].Field<int>("model_id") == model_id)
                {
                    ModelDataManager.Instance.Models.Tables[0].Rows[i]["model_name"] = model_name;
                    ModelDataManager.Instance.Models.Tables[0].Rows[i]["type_FK"] = type_id;
                    ModelDataManager.Instance.Models.Tables[0].Rows[i]["weight"] = weight;
                    ModelDataManager.Instance.Models.Tables[0].Rows[i]["price"] = price;
                    ModelDataManager.Instance.Models.Tables[0].Rows[i]["stock_balance"] = stock_balance;
                    ModelDataManager.Instance.Models.Tables[0].Rows[i]["manufacturer_FK"] = manufacturer_id;
                    ModelDataManager.Instance.Models.Tables[0].Rows[i]["supplier_FK"] = supplier_id;
                    ModelDataManager.Instance.Models.Tables[0].Rows[i]["reserved"] = reserved;
                    break;
                }
            }

            string outStr = "";
            await Task.Run(() =>
            {
                try
                {
                    adapterModel.UpdateCommand = new SqlCommand(cmd, connection);
                    outStr = (adapterModel.Update(ModelDataManager.Instance.Models)).ToString();
                }
                catch (Exception ex)
                {
                    outStr = ex.Message;
                }
            });
            return outStr;            
        }

        public async Task<string> UpdateStockBalanceModelAsync(int stock_balance, int model_id)
        {
            await ReadModelTableAsync();

            string cmd = $"update models set stock_balance = {stock_balance} where model_id = {model_id};";
            SqlCommand update = new SqlCommand(cmd, connection);

            for (int i = 0; i < ModelDataManager.Instance.Models.Tables[0].Rows.Count; i++)
            {
                if (ModelDataManager.Instance.Models.Tables[0].Rows[i].Field<int>("model_id") == model_id)
                {                    
                    ModelDataManager.Instance.Models.Tables[0].Rows[i]["stock_balance"] = stock_balance;                    
                    break;
                }
            }

            string outStr = "";
            await Task.Run(() =>
            {
                try
                {
                    adapterModel.UpdateCommand = new SqlCommand(cmd, connection);
                    outStr = (adapterModel.Update(ModelDataManager.Instance.Models)).ToString();
                }
                catch (Exception ex)
                {
                    outStr = ex.Message;
                }
            });
            return outStr;
        }

        public async Task<string> UpdateReservedToModelAsync(int reserved, int model_id)
        {
            string cmd = $"update models set reserved = {reserved} where model_id = {model_id};";
            SqlCommand update = new SqlCommand(cmd, connection);

            for (int i = 0; i < ModelDataManager.Instance.Models.Tables[0].Rows.Count; i++)
            {
                if (ModelDataManager.Instance.Models.Tables[0].Rows[i].Field<int>("model_id") == model_id)
                {
                    ModelDataManager.Instance.Models.Tables[0].Rows[i]["reserved"] = reserved;
                    break;
                }
            }

            string outStr = "";
            await Task.Run(() =>
            {
                try
                {
                    adapterModel.UpdateCommand = new SqlCommand(cmd, connection);
                    outStr = (adapterModel.Update(ModelDataManager.Instance.Models)).ToString();
                }
                catch (Exception ex)
                {
                    outStr = ex.Message;
                }
            });
            return outStr;
        }

        public async Task<string> DeleteModelAsync(string info)
        {
            string[] result = info.Split('.');
            int id = int.Parse(result[0]);
            await ReadModelTableAsync();

            string cmdDel = $"delete from models where model_id = {id};";
            SqlCommand delete = new SqlCommand(cmdDel, connection);

            for (int i = 0; i < ModelDataManager.Instance.Models.Tables[0].Rows.Count; i++)
            {
                if (ModelDataManager.Instance.Models.Tables[0].Rows[i].Field<int>("model_id") == id)
                {
                    ModelDataManager.Instance.Models.Tables[0].Rows[i].Delete();
                    break;
                }
            }

            string outStr = "";
            await Task.Run(() =>
            {
                try
                {
                    adapterModel.DeleteCommand = new SqlCommand(cmdDel, connection);
                    outStr = (adapterModel.Update(ModelDataManager.Instance.Models)).ToString();
                }
                catch (Exception ex)
                {
                    outStr = ex.Message;
                }
            });
            return outStr;
        }

        //сортировка
        public async Task<string> SortModelTableAsync(string orderBy, string direction, string Entity2 = null, string entityName2 = null, string Entity3 = null, string entityName3 = null)
        {
            string cmd = "Select * from models ";

            if(Entity2 != null)
            {
                cmd += $" inner join {Entity2} on {entityName2}_FK = {entityName2}_id";
            }

            if (Entity3 != null)
            {
                cmd += $" inner join {Entity3} on {entityName3}_FK = {entityName3}_id";
            }

            if (direction == "По убыванию")
            {
                cmd += $" order by {orderBy} desc;";
            }
            else
            {
                cmd += $" order by {orderBy} asc;";
            }

            adapterModel = new SqlDataAdapter(cmd, connection);
            commandBuilder = new SqlCommandBuilder(adapterModel);
            ModelDataManager.Instance.Models.Clear();

            string outStr = "";
            await Task.Run(() =>
            {
                try
                {
                    outStr = adapterModel.Fill(ModelDataManager.Instance.Models).ToString();                    
                }
                catch (Exception ex)
                {
                    outStr = ex.Message;
                }
            });
            return outStr;
        }

        //поиск
        public async Task<string> SearchModelTableAsync(string cmdSearch, string cmdFraction, string cmdQuantity)
        {
            adapterModel = new SqlDataAdapter(cmdSearch, connection);
            commandBuilder = new SqlCommandBuilder(adapterModel);
            ModelDataManager.Instance.Models.Clear();
            int result = 0;
            string outStr = "";
            await Task.Run(() =>
            {
                try
                {
                    outStr = adapterModel.Fill(ModelDataManager.Instance.Models).ToString();
                }
                catch (Exception ex)
                {
                    outStr = ex.Message;
                }
            });
            if (int.TryParse(outStr, out result) == true && result > 0)
            {
                if(cmdFraction != "") outStr = await GetModelFractionTableAsync(cmdFraction);
                if (int.TryParse(outStr, out result) == true && cmdQuantity != "") outStr = await GetModelQuantityTableAsync(cmdQuantity);
            }        

            return outStr;
        }

        public async Task<string> GetModelFractionTableAsync(string cmd)
        {
            adapterModelFraction = new SqlDataAdapter(cmd, connection);
            commandBuilder = new SqlCommandBuilder(adapterModelFraction);
            ModelDataManager.Instance.Fraction.Clear();

            string outStr = "";
            await Task.Run(() =>
            {
                try
                {
                    outStr = adapterModelFraction.Fill(ModelDataManager.Instance.Fraction).ToString();
                }
                catch (Exception ex)
                {
                    outStr = ex.Message;
                }
            });
            return outStr;
        }

        public async Task<string> GetModelQuantityTableAsync(string cmd)
        {
            adapterModelQuantity = new SqlDataAdapter(cmd, connection);
            commandBuilder = new SqlCommandBuilder(adapterModelQuantity);
            ModelDataManager.Instance.Quantity.Clear();

            string outStr = "";
            await Task.Run(() =>
            {
                try
                {
                    outStr = adapterModelQuantity.Fill(ModelDataManager.Instance.Quantity).ToString();
                }
                catch (Exception ex)
                {
                    outStr = ex.Message;
                }
            });
            return outStr;
        }


        #endregion

        #region сущность Device
        public async Task<string> ReadDeviceTableAsync()
        {
            adapterDevice = new SqlDataAdapter($"Select * from devices", connection);
            commandBuilder = new SqlCommandBuilder(adapterDevice);
            ModelDataManager.Instance.Devices.Clear();

            string outStr = "";
            await Task.Run(() =>
            {
                try
                {
                    outStr = adapterDevice.Fill(ModelDataManager.Instance.Devices).ToString();
                }
                catch (Exception ex)
                {
                    outStr = ex.Message;
                }
            });
            return outStr;
        }

        public async Task<string> AddDeviceAsync(int model_id, string serial_number, DateTime manufacture_date, bool isDefected)
        {
            await ReadDeviceTableAsync();
            if (AvailabilityСheck(ModelDataManager.Instance.Devices, "serial_number", serial_number) == true) return "Такое устройство уже имеется в базе!";

            int defect = 0;
            if (isDefected) defect = 1;
            string cmdInsert = $"insert into devices values ({model_id}, '{serial_number}', '{manufacture_date}', null, null, {defect});";

            DataRow row = ModelDataManager.Instance.Devices.Tables[0].NewRow();
            row["model_FK"] = model_id;
            row["serial_number"] = serial_number;
            row["manufacture_date"] = manufacture_date;
            //row["order_FK"] = null;
            //row["basket_FK"] = null;
            row["isDefected"] = defect;
            ModelDataManager.Instance.Devices.Tables[0].Rows.Add(row);

            string outStr = "";
            await Task.Run(() =>
            {
                try
                {
                    adapterDevice.InsertCommand = new SqlCommand(cmdInsert, connection);
                    outStr = (adapterDevice.Update(ModelDataManager.Instance.Devices)).ToString();                    
                }
                catch (Exception ex)
                {
                    outStr = ex.Message;
                }
            });            

            int result = 0;            
            if(int.TryParse(outStr, out result)==true)
            {
                await ReadModelTableAsync();
                await UpdateStockBalanceModelAsync(ModelDataManager.Instance.GetStockBalance(model_id) + 1, model_id);
            }         

            return outStr;
        }

        public async Task<string> UpdateDeviceAsync(int deviceModel_id, string serial_number, DateTime manufacture_date, bool isDefected, int device_id)
        {
            await ReadDeviceTableAsync();
            //if (AvailabilityСheck(DeviceDataManager.Instance.Devices, "serial_number", serial_number) == true) return "Такое устройство уже имеется в базе!";

            int defect = 0;
            if (isDefected) defect = 1;
            string cmd =    $"update devices set Model_FK = {deviceModel_id}, " +
                            $"serial_number ='{serial_number}', " +
                            $"manufacture_date ='{manufacture_date}', " +
                            $"isDefected = {defect} " +                            
                            $"where device_id = {device_id};";
            SqlCommand update = new SqlCommand(cmd, connection);

            for (int i = 0; i < ModelDataManager.Instance.Devices.Tables[0].Rows.Count; i++)
            {
                if (ModelDataManager.Instance.Devices.Tables[0].Rows[i].Field<int>("device_id") == device_id)
                {
                    ModelDataManager.Instance.Devices.Tables[0].Rows[i]["Model_FK"] = deviceModel_id;
                    ModelDataManager.Instance.Devices.Tables[0].Rows[i]["serial_number"] = serial_number;
                    ModelDataManager.Instance.Devices.Tables[0].Rows[i]["manufacture_date"] = manufacture_date;
                    ModelDataManager.Instance.Devices.Tables[0].Rows[i]["isDefected"] = defect;                    
                    break;
                }
            }

            string outStr = "";
            await Task.Run(() =>
            {
                try
                {
                    adapterDevice.UpdateCommand = new SqlCommand(cmd, connection);
                    outStr = (adapterDevice.Update(ModelDataManager.Instance.Devices)).ToString();
                }
                catch (Exception ex)
                {
                    outStr = ex.Message;
                }
            });
            return outStr;
        }

        public async Task<string> UpdateBasketFKToDeviceAsync(int? basket_id, int device_id)
        {
            await ReadDeviceTableAsync();
            string cmd = "";
            if (basket_id != null)
            {
                cmd = $"update devices set basket_FK = {basket_id} where device_id = {device_id};";
                for (int i = 0; i < ModelDataManager.Instance.Devices.Tables[0].Rows.Count; i++)
                {
                    if (ModelDataManager.Instance.Devices.Tables[0].Rows[i].Field<int>("device_id") == device_id)
                    {
                        ModelDataManager.Instance.Devices.Tables[0].Rows[i]["basket_FK"] = basket_id;
                        break;
                    }
                }
            }
            else
            {
                cmd = $"update devices set basket_FK = null where device_id = {device_id};";
                for (int i = 0; i < ModelDataManager.Instance.Devices.Tables[0].Rows.Count; i++)
                {
                    if (ModelDataManager.Instance.Devices.Tables[0].Rows[i].Field<int>("device_id") == device_id)
                    {
                        ModelDataManager.Instance.Devices.Tables[0].Rows[i]["basket_FK"] = DBNull.Value;
                        break;
                    }
                }
            }
            SqlCommand update = new SqlCommand(cmd, connection);            

            string outStr = "";
            await Task.Run(() =>
            {
                try
                {
                    adapterDevice.UpdateCommand = new SqlCommand(cmd, connection);
                    outStr = (adapterDevice.Update(ModelDataManager.Instance.Devices)).ToString();
                }
                catch (Exception ex)
                {
                    outStr = ex.Message;
                }
            });
            return outStr;
        }

        public async Task<string> UpdateOrderFKToDeviceAsync(int order_id, int device_id)
        {
            await ReadDeviceTableAsync();
            string cmd = "";
            cmd = $"update devices set order_FK = {order_id}, basket_FK = null where device_id = {device_id};";
            for (int i = 0; i < ModelDataManager.Instance.Devices.Tables[0].Rows.Count; i++)
            {
                if (ModelDataManager.Instance.Devices.Tables[0].Rows[i].Field<int>("device_id") == device_id)
                {
                    ModelDataManager.Instance.Devices.Tables[0].Rows[i]["order_FK"] = order_id;
                    ModelDataManager.Instance.Devices.Tables[0].Rows[i]["basket_FK"] = DBNull.Value;
                    break;
                }
            }
            SqlCommand update = new SqlCommand(cmd, connection);

            string outStr = "";
            await Task.Run(() =>
            {
                try
                {
                    adapterDevice.UpdateCommand = new SqlCommand(cmd, connection);
                    outStr = (adapterDevice.Update(ModelDataManager.Instance.Devices)).ToString();
                }
                catch (Exception ex)
                {
                    outStr = ex.Message;
                }
            });
            return outStr;
        }

        public async Task<string> DeleteDeviceAsync(string info)
        {
            string[] result = info.Split('.');
            int idDevice = int.Parse(result[0]);
            await ReadDeviceTableAsync();
            await ReadModelTableAsync();
            int idModel = ModelDataManager.Instance.GetModelId(idDevice);

            string cmdDel = $"delete from devices where device_id = {idDevice};";
            SqlCommand delete = new SqlCommand(cmdDel, connection);

            for (int i = 0; i < ModelDataManager.Instance.Devices.Tables[0].Rows.Count; i++)
            {
                if (ModelDataManager.Instance.Devices.Tables[0].Rows[i].Field<int>("device_id") == idDevice)
                {
                    ModelDataManager.Instance.Devices.Tables[0].Rows[i].Delete();
                    break;
                }
            }

            string outStr = "";
            await Task.Run(() =>
            {
                try
                {
                    adapterDevice.DeleteCommand = new SqlCommand(cmdDel, connection);
                    outStr = (adapterDevice.Update(ModelDataManager.Instance.Devices)).ToString();
                }
                catch (Exception ex)
                {
                    outStr = ex.Message;
                }
            });

            int res= 0;
            if (int.TryParse(outStr, out res) == true)
            { 
                await UpdateStockBalanceModelAsync(ModelDataManager.Instance.GetStockBalance(idModel) - 1, idModel);
            }
            return outStr;
        }

        //сортировка
        public async Task<string> SelectDeviceTableAsync(string orderBy, string direction, string Entity2 = null, string entityName2 = null, string Entity3 = null, string entityName3 = null)
        {
            string cmd = "Select * from devices";

            if (Entity2 != null)
            {
                cmd += $" inner join {Entity2} on {entityName2}_FK = {entityName2}_id";
            }

            if (Entity3 != null)
            {
                cmd += $" inner join {Entity3} on {entityName3}_FK = {entityName3}_id";
            }

            if (direction == "По убыванию")
            {
                cmd += $" order by {orderBy} desc;";
            }
            else
            {
                cmd += $" order by {orderBy} asc;";
            }

            adapterDevice = new SqlDataAdapter(cmd, connection);
            commandBuilder = new SqlCommandBuilder(adapterDevice);
            ModelDataManager.Instance.Devices.Clear();

            string outStr = "";
            await Task.Run(() =>
            {
                try
                {
                    outStr = adapterDevice.Fill(ModelDataManager.Instance.Devices).ToString();
                }
                catch (Exception ex)
                {
                    outStr = ex.Message;
                }
            });
            return outStr;
        }

        #endregion

        #region сущность Type
        public async Task<string> ReadTypeTableAsync()
        {
            adapterType = new SqlDataAdapter($"Select * from types;", connection);
            commandBuilder = new SqlCommandBuilder(adapterType);
            ModelDataManager.Instance.Types.Clear();

            string outStr = "";
            await Task.Run(() =>
            {
                try
                {
                    outStr = adapterType.Fill(ModelDataManager.Instance.Types).ToString();
                }
                catch (Exception ex)
                {
                    outStr = ex.Message;
                }
            });
            return outStr;
        }

        public async Task<string> UpdateTypeAsync(string name, int id)
        {
            await ReadTypeTableAsync();
            if (AvailabilityСheck(ModelDataManager.Instance.Types, "type_name", name) == true) return "Такой тип уже имеется в базе!";

            string cmd = $"update types set type_name ='{name}' where type_id = {id};";
            SqlCommand update = new SqlCommand(cmd, connection);

            for (int i = 0; i < ModelDataManager.Instance.Types.Tables[0].Rows.Count; i++)
            {
                if (ModelDataManager.Instance.Types.Tables[0].Rows[i].Field<int>("type_id") == id)
                {
                    ModelDataManager.Instance.Types.Tables[0].Rows[i]["type_name"] = name;
                    break;
                }
            }

            string outStr = "";
            await Task.Run(() =>
            {
                try
                {
                    adapterType.UpdateCommand = new SqlCommand(cmd, connection);
                    outStr = (adapterType.Update(ModelDataManager.Instance.Types)).ToString();
                }
                catch (Exception ex)
                {
                    outStr = ex.Message;
                }
            });
            return outStr;
        }

        public async Task<string> AddTypeAsync(string name)
        {
            await ReadTypeTableAsync();
            if (AvailabilityСheck(ModelDataManager.Instance.Types, "type_name", name) == true) return "Такой тип уже имеется в базе!";

            string cmdInsert = $"insert into types values ('{name}');";
            DataRow row = ModelDataManager.Instance.Types.Tables[0].NewRow();
            row["type_name"] = name;
            ModelDataManager.Instance.Types.Tables[0].Rows.Add(row);

            string outStr = "";
            await Task.Run(() =>
            {
                try
                {
                    adapterType.InsertCommand = new SqlCommand(cmdInsert, connection);
                    outStr = (adapterType.Update(ModelDataManager.Instance.Types)).ToString();
                }
                catch (Exception ex)
                {
                    outStr = ex.Message;
                }
            });
            return outStr;
        }

        public async Task<string> DeleteTypeAsync(string info)
        {
            string[] result = info.Split('.');
            int id = int.Parse(result[0]);
            await ReadTypeTableAsync();

            string cmdDel = $"delete from types where type_id = {id};";
            SqlCommand delete = new SqlCommand(cmdDel, connection);

            for (int i = 0; i < ModelDataManager.Instance.Types.Tables[0].Rows.Count; i++)
            {
                if (ModelDataManager.Instance.Types.Tables[0].Rows[i].Field<int>("type_id") == id)
                {
                    ModelDataManager.Instance.Types.Tables[0].Rows[i].Delete();
                    break;
                }
            }

            string outStr = "";
            await Task.Run(() =>
            {
                try
                {
                    adapterType.DeleteCommand = new SqlCommand(cmdDel, connection);
                    outStr = (adapterType.Update(ModelDataManager.Instance.Types)).ToString();
                }
                catch (Exception ex)
                {
                    outStr = ex.Message;
                }
            });
            return outStr;
        }
        #endregion

        #region сущность User
        public async Task<string> ReadUserTableAsync()
        {
            try
            {
                adapterUser = new SqlDataAdapter($"Select * from users;", connection);
                commandBuilder = new SqlCommandBuilder(adapterUser);
                HumanDataManager.Instance.Users.Clear();
                return (await Task.Run(() => adapterUser.Fill(HumanDataManager.Instance.Users))).ToString();
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public async Task<string> AddUserAsync(string login, string password, string role)
        {
            await ReadUserTableAsync();
            if (AvailabilityСheck(HumanDataManager.Instance.Users, "user_login", login) == true) return "Такой пользователь уже имеется в базе!";

            string cmdInsertUser = $"insert into users values ('{login}', '{password}', '{role}');";

            DataRow row = HumanDataManager.Instance.Users.Tables[0].NewRow();
            row["user_login"] = login;
            row["user_password"] = password;
            row["role"] = role;
            HumanDataManager.Instance.Users.Tables[0].Rows.Add(row);

            string outStr = "";
            try
            {
                await Task.Run(() =>
                {
                    adapterUser.InsertCommand = new SqlCommand(cmdInsertUser, connection);
                    outStr = (adapterUser.Update(HumanDataManager.Instance.Users)).ToString();
                });
            }
            catch (Exception ex)
            {
                outStr = ex.Message;
            }
            return outStr;
        }

        public async Task<string> UpdateUserAsync(int id, string login, string password, string role)
        {
            await ReadUserTableAsync();
            //if (AvailabilityСheck(HumanDataManager.Instance.Users, "user_login", login) == true) return "Такой пользователь уже имеется в базе!";

            string cmd = $"update users set user_login = '{login}'," +
                                    $" user_password = '{password}'," +
                                    $" role = '{role}'" +
                                    $" where user_id = {id};";
            SqlCommand update = new SqlCommand(cmd, connection);

            for (int i = 0; i < HumanDataManager.Instance.Users.Tables[0].Rows.Count; i++)
            {
                if (HumanDataManager.Instance.Users.Tables[0].Rows[i].Field<int>("user_id") == id)
                {
                    HumanDataManager.Instance.Users.Tables[0].Rows[i]["user_login"] = login;
                    HumanDataManager.Instance.Users.Tables[0].Rows[i]["user_password"] = password;
                    HumanDataManager.Instance.Users.Tables[0].Rows[i]["role"] = role;
                    break;
                }
            }

            string outStr = "";
            await Task.Run(() =>
            {
                try
                {
                    adapterUser.UpdateCommand = new SqlCommand(cmd, connection);
                    outStr = (adapterUser.Update(HumanDataManager.Instance.Users)).ToString();
                }
                catch (Exception ex)
                {
                    outStr = ex.Message;
                }
            });
            return outStr;
        }

        public async Task<string> DeleteUserAsync(string info)
        {
            string[] result = info.Split('.');
            int id = int.Parse(result[0]);
            await ReadUserTableAsync();

            string cmdDel = $"delete from users where user_id ={id};";
            SqlCommand delete = new SqlCommand(cmdDel, connection);

            for (int i = 0; i < HumanDataManager.Instance.Users.Tables[0].Rows.Count; i++)
            {
                if (HumanDataManager.Instance.Users.Tables[0].Rows[i].Field<int>("user_id") == id)
                {
                    HumanDataManager.Instance.Users.Tables[0].Rows[i].Delete();
                    break;
                }
            }

            string outStr = "";
            await Task.Run(() =>
            {
                try
                {
                    adapterUser.DeleteCommand = new SqlCommand(cmdDel, connection);
                    outStr = (adapterUser.Update(HumanDataManager.Instance.Users)).ToString();
                }
                catch (Exception ex)
                {
                    outStr = ex.Message;
                }
            });
            return outStr;
        }
        #endregion 

        #region сущность Client
        public async Task<string> ReadClientTableAsync()
        {
            try
            {
                adapterClient = new SqlDataAdapter($"Select * from clients;", connection);
                commandBuilder = new SqlCommandBuilder(adapterClient);
                HumanDataManager.Instance.Clients.Clear();
                return (await Task.Run(() => adapterClient.Fill(HumanDataManager.Instance.Clients))).ToString();
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public async Task<string> AddClientAsync(string name, string phone, int personal_discount, int userId)
        {
            await ReadClientTableAsync();
            if (AvailabilityСheck(HumanDataManager.Instance.Clients, "client_name", name) == true || 
                AvailabilityСheck(HumanDataManager.Instance.Clients, "phone", phone) == true ||
                AvailabilityСheck(HumanDataManager.Instance.Clients, "user_FK", userId) == true) return "Такой клиент уже имеется в базе!";

            string cmdInsert = $"insert into clients values ('{name}', '{phone}', {personal_discount}, {userId});";

            DataRow row = HumanDataManager.Instance.Clients.Tables[0].NewRow();
            row["client_name"] = name;
            row["phone"] = phone;
            row["personal_discount"] = personal_discount;
            row["user_FK"] = userId;
            HumanDataManager.Instance.Clients.Tables[0].Rows.Add(row);

            string outStr = "";
            int result = 0;
            try
            {
                await Task.Run(() =>
                {
                    adapterClient.InsertCommand = new SqlCommand(cmdInsert, connection);
                    outStr = (adapterClient.Update(HumanDataManager.Instance.Clients)).ToString(); 
                });
            }
            catch (Exception ex)
            {
                outStr = ex.Message;
            }

            if (int.TryParse(outStr, out result) == true)
            {
                outStr = await ReadClientTableAsync();
                if (int.TryParse(outStr, out result) == true)
                {
                    outStr = await AddBasketAsync("корзина 1", HumanDataManager.Instance.GetClientId(userId));
                }                    
            }
            return outStr;
        }

        public async Task<string> UpdateClientAsync(int id, string name, string phone, int personal_discount, int userId)
        {
            await ReadClientTableAsync();
           
            string cmd = $"update clients set client_name = '{name}'," +
                                    $" phone = '{phone}'," +
                                    $" personal_discount = {personal_discount}," +
                                    $" user_FK = {userId}" +
                                    $" where client_id = {id};";
            SqlCommand update = new SqlCommand(cmd, connection);

            for (int i = 0; i < HumanDataManager.Instance.Clients.Tables[0].Rows.Count; i++)
            {
                if (HumanDataManager.Instance.Clients.Tables[0].Rows[i].Field<int>("client_id") == id)
                {
                    HumanDataManager.Instance.Clients.Tables[0].Rows[i]["client_name"] = name;
                    HumanDataManager.Instance.Clients.Tables[0].Rows[i]["phone"] = phone;
                    HumanDataManager.Instance.Clients.Tables[0].Rows[i]["personal_discount"] = personal_discount;
                    HumanDataManager.Instance.Clients.Tables[0].Rows[i]["user_FK"] = userId;
                    break;
                }
            }

            string outStr = "";
            await Task.Run(() =>
            {
                try
                {
                    adapterClient.UpdateCommand = new SqlCommand(cmd, connection);
                    outStr = (adapterClient.Update(HumanDataManager.Instance.Clients)).ToString();
                }
                catch (Exception ex)
                {
                    outStr = ex.Message;
                }
            });
            return outStr;
        }

        public async Task<string> DeleteClientAsync(string info)
        {
            string[] result = info.Split('.');
            int id = int.Parse(result[0]);
            await ReadClientTableAsync();

            string cmdDel = $"delete from clients where client_id ={id};";
            SqlCommand delete = new SqlCommand(cmdDel, connection);

            for (int i = 0; i < HumanDataManager.Instance.Clients.Tables[0].Rows.Count; i++)
            {
                if (HumanDataManager.Instance.Clients.Tables[0].Rows[i].Field<int>("user_id") == id)
                {
                    HumanDataManager.Instance.Clients.Tables[0].Rows[i].Delete();
                    break;
                }
            }

            string outStr = "";
            await Task.Run(() =>
            {
                try
                {
                    adapterClient.DeleteCommand = new SqlCommand(cmdDel, connection);
                    outStr = (adapterClient.Update(HumanDataManager.Instance.Clients)).ToString();
                }
                catch (Exception ex)
                {
                    outStr = ex.Message;
                }
            });
            return outStr;
        }
        #endregion

        #region сущность Basket
        public async Task<string> ReadBasketTableAsync()
        {
            try
            {
                adapterBasket = new SqlDataAdapter("Select * from baskets;", connection);
                commandBuilder = new SqlCommandBuilder(adapterBasket);
                ShopDataManager.Instance.Baskets.Clear();
                return (await Task.Run(() => adapterBasket.Fill(ShopDataManager.Instance.Baskets))).ToString();
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public async Task<string> AddBasketAsync(string name, int idClient)
        {
            await ReadBasketTableAsync();
            if (AvailabilityСheck(ShopDataManager.Instance.Baskets, "basket_name", name, "client_FK", idClient) == true) return "Такая корзина у этого клиента уже имеется в базе!";

            string cmdInsert = $"insert into baskets values ('{name}',{idClient});";

            DataRow row = ShopDataManager.Instance.Baskets.Tables[0].NewRow();
            row["basket_name"] = idClient;
            row["client_FK"] = idClient;
            ShopDataManager.Instance.Baskets.Tables[0].Rows.Add(row);

            string outStr = "";
            await Task.Run(() =>
            {
                try
                {
                    adapterBasket.InsertCommand = new SqlCommand(cmdInsert, connection);
                    outStr = (adapterBasket.Update(ShopDataManager.Instance.Baskets)).ToString();
                }
                catch (Exception ex)
                {
                    outStr = ex.Message;
                }
            });            
            return outStr;
        }

        public async Task<string> AddModelToBasketAsync(int idModel, int countModel, int idBasket)
        {
            await ReadDeviceTableAsync();
            int updateDevice = 0;
            string str = "";
            int result = 0;

            if ((ModelDataManager.Instance.GetStockBalance(idModel) - ModelDataManager.Instance.GetNumReserved(idModel)) >= countModel)
            {
                for (int i = 0; i < ModelDataManager.Instance.Devices.Tables[0].Rows.Count; i++)
                {
                    if(ModelDataManager.Instance.Devices.Tables[0].Rows[i].Field<int>("Model_FK") == idModel &&
                       ModelDataManager.Instance.Devices.Tables[0].Rows[i].Field<int?>("basket_FK") == null &&
                       ModelDataManager.Instance.Devices.Tables[0].Rows[i].Field<int?>("order_FK") == null)
                    {
                        str = await UpdateBasketFKToDeviceAsync(idBasket, ModelDataManager.Instance.Devices.Tables[0].Rows[i].Field<int>("device_id"));
                        if(int.TryParse(str,out result)==true)
                        {
                            updateDevice++;                            
                            await UpdateReservedToModelAsync(ModelDataManager.Instance.GetNumReserved(idModel) + 1, idModel);
                            await AddModelToModelBasketAsync(idModel, idBasket);
                            if (updateDevice >= countModel) return str;
                        }
                        else
                        {
                            return str;
                        }
                    }
                    
                }
                return updateDevice.ToString();
            }
            else
            {
                return "нет такого количества устройств в свободном остатке";
            }           
        }

        public async Task<string> DecModelToBasketAsync(int idModel, int idBasket)
        {
            int result = 0;
            string str = await ReadDeviceTableAsync();
            if (int.TryParse(str, out result) == true)
            {
                for (int i = 0; i < ModelDataManager.Instance.Devices.Tables[0].Rows.Count; i++)
                {
                    if (ModelDataManager.Instance.Devices.Tables[0].Rows[i].Field<int>("Model_FK") == idModel &&
                       ModelDataManager.Instance.Devices.Tables[0].Rows[i].Field<int?>("basket_FK") == idBasket)
                    {
                        str = await UpdateBasketFKToDeviceAsync(null, ModelDataManager.Instance.Devices.Tables[0].Rows[i].Field<int>("device_id"));
                        if (int.TryParse(str, out result) == true)
                        {
                            str = await UpdateReservedToModelAsync(ModelDataManager.Instance.GetNumReserved(idModel) - 1, idModel);
                            if (int.TryParse(str, out result) == true)
                            {
                                str = await DecModelToModelBasketAsync(idModel, idBasket);
                                break; //проходим цикл один раз до первого удаления
                            }
                        }
                    }
                }
            }                
            return str;
        }

        public async Task<string> UpdateBasketAsync(string name, int idClient, int idBasket)
        {
            await ReadBasketTableAsync();
            if (AvailabilityСheck(ShopDataManager.Instance.Baskets, "basket_name", name, "client_FK", idClient) == true) return "Такая корзина у этого клиента уже имеется в базе!";

            string cmd = $"update baskets set basket_name ='{name}' where basket_id = {idBasket};";
            SqlCommand update = new SqlCommand(cmd, connection);

            for (int i = 0; i < ShopDataManager.Instance.Baskets.Tables[0].Rows.Count; i++)
            {
                if (ShopDataManager.Instance.Baskets.Tables[0].Rows[i].Field<int>("basket_id") == idBasket)
                {
                    ShopDataManager.Instance.Baskets.Tables[0].Rows[i]["basket_name"] = name;
                    break;
                }
            }

            string outStr = "";
            await Task.Run(() =>
            {
                try
                {
                    adapterBasket.UpdateCommand = new SqlCommand(cmd, connection);
                    outStr = (adapterBasket.Update(ShopDataManager.Instance.Baskets)).ToString();
                }
                catch (Exception ex)
                {
                    outStr = ex.Message;
                }
            });
            return outStr;
        }

        public async Task<string> DeleteBasketAsync(string info)
        {
            string[] result = info.Split('.');
            int id = int.Parse(result[0]);
            await ReadBasketTableAsync();

            string cmdDel = $"delete from baskets where basket_id ={id};";
            SqlCommand delete = new SqlCommand(cmdDel, connection);

            for (int i = 0; i < ShopDataManager.Instance.Baskets.Tables[0].Rows.Count; i++)
            {
                if (ShopDataManager.Instance.Baskets.Tables[0].Rows[i].Field<int>("basket_id") == id)
                {
                    ShopDataManager.Instance.Baskets.Tables[0].Rows[i].Delete();
                    break;
                }
            }

            string outStr = "";
            await Task.Run(() =>
            {
                try
                {
                    adapterBasket.DeleteCommand = new SqlCommand(cmdDel, connection);
                    outStr = (adapterBasket.Update(ShopDataManager.Instance.Baskets)).ToString();
                }
                catch (Exception ex)
                {
                    outStr = ex.Message;
                }
            });
            return outStr;
        }
        #endregion

        #region сущность Order
        public async Task<string> ReadOrderTableAsync()
        {
            adapterOrder = new SqlDataAdapter($"Select * from orders;", connection);
            commandBuilder = new SqlCommandBuilder(adapterOrder);
            ShopDataManager.Instance.Orders.Clear();

            string outStr = "";
            await Task.Run(() =>
            {
                try
                {
                    outStr = adapterOrder.Fill(ShopDataManager.Instance.Orders).ToString();
                }
                catch (Exception ex)
                {
                    outStr = ex.Message;
                }
            });
            return outStr;
        }

        public async Task<string> UpdateOrderAsync(string name, DateTime order_date, int id)
        {
            await ReadOrderTableAsync();
            //if (AvailabilityСheck(OrderDataManager_.Instance.Orders, "order_name", name) == true) return "Такой заказ уже имеется в базе!";

            string cmd =    $"update orders set order_name ='{name}', " +
                            $"order_date = '{order_date}' " +                            
                            $"where order_id = {id};";
            SqlCommand update = new SqlCommand(cmd, connection);

            for (int i = 0; i < ShopDataManager.Instance.Orders.Tables[0].Rows.Count; i++)
            {
                if (ShopDataManager.Instance.Orders.Tables[0].Rows[i].Field<int>("order_id") == id)
                {
                    ShopDataManager.Instance.Orders.Tables[0].Rows[i]["order_name"] = name;
                    ShopDataManager.Instance.Orders.Tables[0].Rows[i]["order_date"] = order_date;                    
                    break;
                }
            }

            string outStr = "";
            await Task.Run(() =>
            {
                try
                {
                    adapterOrder.UpdateCommand = new SqlCommand(cmd, connection);
                    outStr = (adapterOrder.Update(ShopDataManager.Instance.Orders)).ToString();
                }
                catch (Exception ex)
                {
                    outStr = ex.Message;
                }
            });
            return outStr;
        }

        public async Task<string> AddOrderAsync(int idClient)
        {
            await ReadOrderTableAsync();
            string nameOrder = ShopDataManager.Instance.GenerateNewOrderName();
            DateTime dateOrder = DateTime.Now;          
            
            string cmdInsert = $"insert into orders values ('{nameOrder}', '{dateOrder.Date}', {idClient});";

            DataRow row = ShopDataManager.Instance.Orders.Tables[0].NewRow();
            row["order_name"] = nameOrder;
            row["order_date"] = dateOrder.Date;
            row["client_FK"] = idClient;
            ShopDataManager.Instance.Orders.Tables[0].Rows.Add(row);

            string outStr = "";
            await Task.Run(() =>
            {
                try
                {
                    adapterOrder.InsertCommand = new SqlCommand(cmdInsert, connection);
                    outStr = (adapterOrder.Update(ShopDataManager.Instance.Orders)).ToString();
                }
                catch (Exception ex)
                {
                    outStr = ex.Message;
                }
            });

            await ReadOrderTableAsync();



            return outStr;
        }

        public async Task<string> DeleteOrderAsync(string info)
        {
            string[] result = info.Split('.');
            int id = int.Parse(result[0]);
            await ReadOrderTableAsync();

            string cmdDel = $"delete from orders where order_id = {id};";
            SqlCommand delete = new SqlCommand(cmdDel, connection);

            for (int i = 0; i < ShopDataManager.Instance.Orders.Tables[0].Rows.Count; i++)
            {
                if (ShopDataManager.Instance.Orders.Tables[0].Rows[i].Field<int>("order_id") == id)
                {
                    ShopDataManager.Instance.Orders.Tables[0].Rows[i].Delete();
                    break;
                }
            }

            string outStr = "";
            await Task.Run(() =>
            {
                try
                {
                    adapterOrder.DeleteCommand = new SqlCommand(cmdDel, connection);
                    outStr = (adapterOrder.Update(ShopDataManager.Instance.Orders)).ToString();
                }
                catch (Exception ex)
                {
                    outStr = ex.Message;
                }
            });
            return outStr;
        }        

        public async Task<string> AddModelsToNewOrderAsync(List<string> listModelToBasket, int idBasket, int idOrder)
        {
            
            await ReadDeviceTableAsync();            
            string str = "";
            int result = 0;
            int idModel = 0;
            int idDevice = 0;
            int amountModel = 0;
            int k = 0;

            for (int i = 0; i < listModelToBasket.Count; i++)
            {
                string[] aModel = listModelToBasket[i].Split('.');
                idModel = int.Parse(aModel[0]);
                amountModel = int.Parse(aModel[3]);


                for (int j = 0; j < ModelDataManager.Instance.Devices.Tables[0].Rows.Count; j++)
                {
                    if (ModelDataManager.Instance.Devices.Tables[0].Rows[j].Field<int>("Model_FK") == idModel &&
                      ModelDataManager.Instance.Devices.Tables[0].Rows[j].Field<int?>("basket_FK") == idBasket)
                    {
                        idDevice = ModelDataManager.Instance.Devices.Tables[0].Rows[j].Field<int>("device_id");
                        str = await UpdateOrderFKToDeviceAsync(idOrder, idDevice);
                        if (int.TryParse(str, out result) == false)
                        {
                            return str;
                        }
                        k++;
                        if (k == amountModel) break;
                    }
                }
                
                str = await UpdateReservedToModelAsync(0, idModel);
                if (int.TryParse(str, out result) == false)
                {
                    return str;
                }
                str = await UpdateStockBalanceModelAsync(ModelDataManager.Instance.GetStockBalance(idModel) - amountModel, idModel);
                if (int.TryParse(str, out result) == false)
                {
                    return str;
                }
                str = await AddModelToModelOrderAsync(idModel, amountModel, idOrder);
                if (int.TryParse(str, out result) == false)
                {
                    return str;
                }
                str = await DeleteModelFromModelBasketAsync(idModel, idBasket);
                if (int.TryParse(str, out result) == false)
                {
                    return str;
                }
            }
            return str;
        }


        #endregion

        #region ModelBasket
        public async Task<string> ReadModelBasketTableAsync()
        {
            adapterModelBasket = new SqlDataAdapter($"Select * from modelBasket;", connection);
            commandBuilder = new SqlCommandBuilder(adapterModelBasket);
            ShopDataManager.Instance.ModelBasket.Clear();

            string outStr = "";
            await Task.Run(() =>
            {
                try
                {
                    outStr = adapterModelBasket.Fill(ShopDataManager.Instance.ModelBasket).ToString();
                }
                catch (Exception ex)
                {
                    outStr = ex.Message;
                }
            });
            return outStr;
        }

        public async Task<string> AddModelToModelBasketAsync(int idModel, int idBasket)
        {
            string outStr = "";
            await ReadModelBasketTableAsync();
            if (AvailabilityСheck(ShopDataManager.Instance.ModelBasket, "model_id", idModel) == true)
            {
                int numModel = ShopDataManager.Instance.GetNumModelToBasket(idModel);
                string cmd = $"update modelbasket set amount = {numModel + 1} where model_id = {idModel} and basket_id = {idBasket};";
                SqlCommand update = new SqlCommand(cmd, connection);

                for (int i = 0; i < ShopDataManager.Instance.ModelBasket.Tables[0].Rows.Count; i++)
                {
                    if (ShopDataManager.Instance.ModelBasket.Tables[0].Rows[i].Field<int>("model_id") == idModel)
                    {
                        ShopDataManager.Instance.ModelBasket.Tables[0].Rows[i]["amount"] = numModel + 1;
                        break;
                    }
                }
                await Task.Run(() =>
                {
                    try
                    {
                        adapterModelBasket.UpdateCommand = new SqlCommand(cmd, connection);
                        outStr = (adapterModelBasket.Update(ShopDataManager.Instance.ModelBasket)).ToString();
                    }
                    catch (Exception ex)
                    {
                        outStr = ex.Message;
                    }
                });
            }
            else
            {
                string cmdInsert = $"insert into modelbasket values ({idModel},{idBasket},1,1);";
                DataRow row = ShopDataManager.Instance.ModelBasket.Tables[0].NewRow();
                row["model_id"] = idModel;
                row["basket_id"] = idBasket;
                row["amount"] = 1;
                row["instock"] = 1;
                ShopDataManager.Instance.ModelBasket.Tables[0].Rows.Add(row);

                await Task.Run(() =>
                {
                    try
                    {
                        adapterModelBasket.InsertCommand = new SqlCommand(cmdInsert, connection);
                        outStr = (adapterModelBasket.Update(ShopDataManager.Instance.ModelBasket)).ToString();
                    }
                    catch (Exception ex)
                    {
                        outStr = ex.Message;
                    }
                });
            }
            return outStr;
        }

        public async Task<string> DecModelToModelBasketAsync(int idModel, int idBasket)
        {
            string outStr = "";
            await ReadModelBasketTableAsync();
            if (AvailabilityСheck(ShopDataManager.Instance.ModelBasket, "model_id", idModel) == false) return "нет такой модели в базе";
            int numModel = ShopDataManager.Instance.GetNumModelToBasket(idModel);

            if (numModel > 1)
            {
                string cmd = $"update modelbasket set amount = {numModel - 1} where model_id = {idModel} and basket_id = {idBasket};";
                SqlCommand update = new SqlCommand(cmd, connection);

                for (int i = 0; i < ShopDataManager.Instance.ModelBasket.Tables[0].Rows.Count; i++)
                {
                    if (ShopDataManager.Instance.ModelBasket.Tables[0].Rows[i].Field<int>("model_id") == idModel)
                    {
                        ShopDataManager.Instance.ModelBasket.Tables[0].Rows[i]["amount"] = numModel - 1;
                        break;
                    }
                }
                await Task.Run(() =>
                {
                    try
                    {
                        adapterModelBasket.UpdateCommand = new SqlCommand(cmd, connection);
                        outStr = (adapterModelBasket.Update(ShopDataManager.Instance.ModelBasket)).ToString();
                    }
                    catch (Exception ex)
                    {
                        outStr = ex.Message;
                    }
                });
            }
            else //в корзине один экзепляр модели
            {
                outStr = await DeleteModelFromModelBasketAsync(idModel, idBasket);
            }
            return outStr;
        }

        public async Task<string> DeleteModelFromModelBasketAsync(int idModel, int idBasket)
        {

            await ReadModelBasketTableAsync();

            string cmdDel = $"delete from modelbasket where model_id ={idModel};";
            SqlCommand delete = new SqlCommand(cmdDel, connection);

            for (int i = 0; i < ShopDataManager.Instance.ModelBasket.Tables[0].Rows.Count; i++)
            {
                if (ShopDataManager.Instance.ModelBasket.Tables[0].Rows[i].Field<int>("model_id") == idModel)
                {
                    ShopDataManager.Instance.ModelBasket.Tables[0].Rows[i].Delete();
                    break;
                }
            }

            string outStr = "";
            await Task.Run(() =>
            {
                try
                {
                    adapterModelBasket.DeleteCommand = new SqlCommand(cmdDel, connection);
                    outStr = (adapterModelBasket.Update(ShopDataManager.Instance.ModelBasket)).ToString();
                }
                catch (Exception ex)
                {
                    outStr = ex.Message;
                }
            });
            return outStr;
        }
        #endregion

        #region ModelOrder
        public async Task<string> ReadModelOrderTableAsync()
        {
            adapterModelOrder = new SqlDataAdapter($"Select * from modelOrder;", connection);
            commandBuilder = new SqlCommandBuilder(adapterModelOrder);
            ShopDataManager.Instance.ModelOrder.Clear();

            string outStr = "";
            await Task.Run(() =>
            {
                try
                {
                    outStr = adapterModelOrder.Fill(ShopDataManager.Instance.ModelOrder).ToString();
                }
                catch (Exception ex)
                {
                    outStr = ex.Message;
                }
            });
            return outStr;
        }

        public async Task<string> AddModelToModelOrderAsync(int idModel, int amountModel, int idOrder)
        {
            string outStr = "";
            await ReadModelOrderTableAsync();
            string cmdInsert = $"insert into modelOrder values ({idModel},{idOrder},{amountModel});";
            DataRow row = ShopDataManager.Instance.ModelOrder.Tables[0].NewRow();
            row["model_id"] = idModel;
            row["order_id"] = idOrder;
            row["amount"] = amountModel;
            ShopDataManager.Instance.ModelOrder.Tables[0].Rows.Add(row);

            await Task.Run(() =>
            {
                try
                {
                    adapterModelOrder.InsertCommand = new SqlCommand(cmdInsert, connection);
                    outStr = (adapterModelOrder.Update(ShopDataManager.Instance.ModelOrder)).ToString();
                }
                catch (Exception ex)
                {
                    outStr = ex.Message;
                }
            });
            return outStr;
        }
        #endregion

        public bool AvailabilityСheck(DataSet data, string field, string name)
        {
            for (int i = 0; i < data.Tables[0].Rows.Count; i++)
            {
                if (data.Tables[0].Rows[i].Field<string>(field) == name)
                {
                    return true;
                }
            }
            return false;
        }

        public bool AvailabilityСheck(DataSet data, string field, int value)
        {
            for (int i = 0; i < data.Tables[0].Rows.Count; i++)
            {
                if (data.Tables[0].Rows[i].Field<int>(field) == value)
                {
                    return true;
                }
            }
            return false;
        }

        public bool AvailabilityСheck(DataSet data, string field1, string name, string field2, int value)
        {
            for (int i = 0; i < data.Tables[0].Rows.Count; i++)
            {
                if (data.Tables[0].Rows[i].Field<string>(field1) == name && data.Tables[0].Rows[i].Field<int>(field2) == value)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
