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
        SqlDataAdapter adapterModelType = new SqlDataAdapter();
        SqlDataAdapter adapterModelDevice = new SqlDataAdapter();
        SqlDataAdapter adapterDevice = new SqlDataAdapter();

        SqlDataAdapter adapterOrder = new SqlDataAdapter();
        SqlDataAdapter adapterDeviceOrder = new SqlDataAdapter();

        SqlDataAdapter adapterRight = new SqlDataAdapter();
        SqlDataAdapter adapterUserRight = new SqlDataAdapter();
        SqlDataAdapter adapterUser = new SqlDataAdapter();
        

        SqlCommandBuilder commandBuilder; 

        public DataBaseService()
        {
            connection = new SqlConnection(@"Data Source=DESKTOP-MHB46B8\SQLEXPRESS;Initial catalog=ElectricalDevices_v7;Integrated Security=true;");
        }

        public async Task<string> ReadUserTableAsync()
        {
            try
            {
                adapterUser = new SqlDataAdapter($"Select * from users;", connection);
                commandBuilder = new SqlCommandBuilder(adapterUser);
                UserDataManager.Instance.Users.Clear();
                return (await Task.Run(() => adapterUser.Fill(UserDataManager.Instance.Users))).ToString();
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public async Task<string> AddUserAsync(User user)
        {
            await ReadUserTableAsync();
            if (AvailabilityСheck(UserDataManager.Instance.Users, "user_name", user.Name) == true || 
                AvailabilityСheck(UserDataManager.Instance.Users, "user_login", user.Login) == true) return "Такой пользователь уже имеется в базе!";

            string cmdInsertUser = $"insert into users values ('{user.Name}', '{user.Login}', '{user.Password}', '{user.Phone}', {user.PersonalDiscount});";

            DataRow rowUsers = UserDataManager.Instance.Users.Tables[0].NewRow();
            rowUsers["user_name"] = user.Name;
            rowUsers["user_login"] = user.Login;
            rowUsers["user_password"] = user.Password;
            rowUsers["phone"] = user.Phone;
            rowUsers["personal_discount"] = user.PersonalDiscount;
            UserDataManager.Instance.Users.Tables[0].Rows.Add(rowUsers);

            string outStr = "";
            try
            {
                await Task.Run(() =>
                {
                    adapterUser.InsertCommand = new SqlCommand(cmdInsertUser, connection);
                    outStr = (adapterUser.Update(UserDataManager.Instance.Users)).ToString();
                });               
            }
            catch (Exception ex)
            {
                outStr = ex.Message;
            }       
            return outStr;
        }

        public async Task<string> UpdateUserAsync(User user)
        {
            await ReadUserTableAsync();
            if (AvailabilityСheck(UserDataManager.Instance.Users, "user_name", user.Name) == true ||
               AvailabilityСheck(UserDataManager.Instance.Users, "user_login", user.Login) == true) return "Такой пользователь уже имеется в базе!";

            string cmd = $"update users set user_name ='{user.Name}'," +
                                    $" user_login = '{user.Login}'," +
                                    $" user_password = '{user.Password}'," +
                                    $" phone = '{user.Phone}'," +
                                    $" personal_discount = {user.PersonalDiscount}" +
                                    $" where user_id = {user.Id};";
            SqlCommand update = new SqlCommand(cmd, connection);

            for (int i = 0; i < UserDataManager.Instance.Users.Tables[0].Rows.Count; i++)
            {
                if (UserDataManager.Instance.Users.Tables[0].Rows[i].Field<int>("user_id") == user.Id)
                {
                    UserDataManager.Instance.Users.Tables[0].Rows[i]["user_name"] = user.Name;
                    UserDataManager.Instance.Users.Tables[0].Rows[i]["user_login"] = user.Login;
                    UserDataManager.Instance.Users.Tables[0].Rows[i]["user_password"] = user.Password;
                    UserDataManager.Instance.Users.Tables[0].Rows[i]["phone"] = user.Phone;
                    UserDataManager.Instance.Users.Tables[0].Rows[i]["personal_discount"] = user.PersonalDiscount;
                    break;
                }
            }

            string outStr = "";
            await Task.Run(() =>
            {
                try
                {
                    adapterUser.UpdateCommand = new SqlCommand(cmd, connection);
                    outStr = (adapterUser.Update(UserDataManager.Instance.Users)).ToString();
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

            for (int i = 0; i < UserDataManager.Instance.Users.Tables[0].Rows.Count; i++)
            {
                if (UserDataManager.Instance.Users.Tables[0].Rows[i].Field<int>("user_id") == id)
                {
                    UserDataManager.Instance.Users.Tables[0].Rows[i].Delete();
                    break;
                }
            }

            string outStr = "";
            await Task.Run(() =>
            {
                try
                {
                    adapterUser.DeleteCommand = new SqlCommand(cmdDel, connection);
                    outStr = (adapterUser.Update(UserDataManager.Instance.Users)).ToString();
                }
                catch (Exception ex)
                {
                    outStr = ex.Message;
                }
            });
            return outStr;
        }


        public async Task<string> ReadRightTableAsync()
        {
            adapterRight = new SqlDataAdapter($"Select * from rights;", connection);
            commandBuilder = new SqlCommandBuilder(adapterRight);
            RightDataManager.Instance.Rights.Clear();

            string outStr = "";
            await Task.Run(() =>
            {
                try
                {
                    outStr = adapterRight.Fill(RightDataManager.Instance.Rights).ToString();
                }
                catch (Exception ex)
                {
                    outStr = ex.Message;
                }
            });
            return outStr;
        }


        public async Task<string> ReadUserRightTableAsync()
        {
            adapterUserRight = new SqlDataAdapter($"Select * from userRight;", connection);
            commandBuilder = new SqlCommandBuilder(adapterUserRight);
            UserRightDataManager.Instance.UserRights.Clear();

            string outStr = "";
            await Task.Run(() =>
            {
                try
                {
                    outStr = adapterUserRight.Fill(UserRightDataManager.Instance.UserRights).ToString();
                }
                catch (Exception ex)
                {
                    outStr = ex.Message;
                }
            });
            return outStr;
        }

        public async Task<string> AddUserRightAsync(User user)
        {
            string cmdInsert = "insert into userRight values ";
            int userId = user.Id;
            if(userId == 0) userId = UserDataManager.Instance.GetLastUserId();

            for (int i = 0; i < user.Rights.Count; i++)
            {
                cmdInsert += $"({userId},{user.Rights[i].Id})";
                if (i < user.Rights.Count - 1) cmdInsert += ",";
            }
            cmdInsert += ";";

            for (int i = 0; i < user.Rights.Count; i++)
            {
                DataRow row = UserRightDataManager.Instance.UserRights.Tables[0].NewRow();
                row["user_id"] = userId;
                row["right_id"] = user.Rights[i].Id;
                UserRightDataManager.Instance.UserRights.Tables[0].Rows.Add(row);
            }

            string outStr = "";
            await Task.Run(() =>
            {
                try
                {
                    adapterUserRight.InsertCommand = new SqlCommand(cmdInsert, connection);
                    outStr = (adapterUserRight.Update(UserRightDataManager.Instance.UserRights)).ToString();
                }
                catch (Exception ex)
                {
                    outStr = ex.Message;
                }
            });
            return outStr;           
        }

        public async Task<string> UpdateUserRightAsync(User user)
        {
            int result = 0;            
            string deleteStr = await DeleteUserRightTableAsync(user);

            if (int.TryParse(deleteStr, out result) == true)
            {
                string updateStr = await AddUserRightAsync(user);
                if (int.TryParse(updateStr, out result) == true)
                {
                    return updateStr;
                }
                else return updateStr;
            }
            else return deleteStr;
        }

        public async Task<string> DeleteUserRightTableAsync(User user)
        {
            int id = user.Id;
            await ReadUserRightTableAsync();          
            
            string cmdDel = $"delete from userRight where userRight.user_id ={id};";
            SqlCommand delete = new SqlCommand(cmdDel, connection);

            for (int i = 0; i < UserRightDataManager.Instance.UserRights.Tables[0].Rows.Count; i++)
            {
                if (UserRightDataManager.Instance.UserRights.Tables[0].Rows[i].Field<int>("user_id") == id)
                {
                    UserRightDataManager.Instance.UserRights.Tables[0].Rows[i].Delete();
                    break;
                }
            }

            string outStr = "";
            await Task.Run(() =>
            {
                try
                {
                    adapterUserRight.DeleteCommand = new SqlCommand(cmdDel, connection);
                    outStr = (adapterUserRight.Update(UserRightDataManager.Instance.UserRights)).ToString();
                }
                catch (Exception ex)
                {
                    outStr = ex.Message;
                }
            });
            return outStr;
        }


        public async Task<string> ReadCountryTableAsync()
        {
            adapterCountry = new SqlDataAdapter($"Select * from countries;", connection);
            commandBuilder = new SqlCommandBuilder(adapterCountry);
            CountryDataManager.Instance.Countries.Clear();           

            string outStr = "";
            await Task.Run(() =>
            {
                try
                {
                    outStr = adapterCountry.Fill(CountryDataManager.Instance.Countries).ToString();
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
            if (AvailabilityСheck(CountryDataManager.Instance.Countries, "country_name", name) == true) return "Такая страна уже имеется в базе!";

            string cmd = $"update countries set country_name ='{name}' where country_id = {id};";
            SqlCommand update = new SqlCommand(cmd, connection);

            for (int i = 0; i < CountryDataManager.Instance.Countries.Tables[0].Rows.Count; i++)
            {
                if (CountryDataManager.Instance.Countries.Tables[0].Rows[i].Field<int>("country_id") == id)
                {
                    CountryDataManager.Instance.Countries.Tables[0].Rows[i]["country_name"] = name;
                    break;
                }
            }

            string outStr = "";
            await Task.Run(() =>
            {
                try
                {
                    adapterCountry.UpdateCommand = new SqlCommand(cmd, connection);
                    outStr = (adapterCountry.Update(CountryDataManager.Instance.Countries)).ToString();
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
            if (AvailabilityСheck(CountryDataManager.Instance.Countries, "country_name", name) == true) return "Такая страна уже имеется в базе!";

            string cmdInsert = $"insert into countries values ('{name}');";
            DataRow row = CountryDataManager.Instance.Countries.Tables[0].NewRow();
            row["country_name"] = name;
            CountryDataManager.Instance.Countries.Tables[0].Rows.Add(row);

            string outStr = "";
            await Task.Run(() =>
            {
                try
                {
                    adapterCountry.InsertCommand = new SqlCommand(cmdInsert, connection);
                    outStr = (adapterCountry.Update(CountryDataManager.Instance.Countries)).ToString();
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

            for (int i = 0; i < CountryDataManager.Instance.Countries.Tables[0].Rows.Count; i++)
            {
                if (CountryDataManager.Instance.Countries.Tables[0].Rows[i].Field<int>("country_id") == id)
                {
                    CountryDataManager.Instance.Countries.Tables[0].Rows[i].Delete();
                    break;
                }
            }

            string outStr = "";
            await Task.Run(() =>
            {
                try
                {
                    adapterCountry.DeleteCommand = new SqlCommand(cmdDel, connection);                    
                    outStr = (adapterCountry.Update(CountryDataManager.Instance.Countries)).ToString();
                }
                catch (Exception ex)
                {
                    outStr = ex.Message;
                }
            });
            return outStr;
        }


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
                ManufacturerDataManager.Instance.Manufacturers.Clear();
                return (await Task.Run(() => adapterManufacturer.Fill(ManufacturerDataManager.Instance.Manufacturers))).ToString();
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public async Task<string> UpdateManufacturerAsync(string name, int country_id, int id)
        {
            await ReadManufacturerTableAsync();
            if (AvailabilityСheck(ManufacturerDataManager.Instance.Manufacturers, "manufacturer_name", name) == true) return "Такой производитель уже имеется в базе!";

            string cmd = $"update manufacturers set manufacturer_name ='{name}', " +
                                                    $"country_FK = {country_id} " +
                                                    $"where manufacturer_id = {id};";
            SqlCommand update = new SqlCommand(cmd, connection);

            for (int i = 0; i < ManufacturerDataManager.Instance.Manufacturers.Tables[0].Rows.Count; i++)
            {
                if (ManufacturerDataManager.Instance.Manufacturers.Tables[0].Rows[i].Field<int>("manufacturer_id") == id)
                {
                    ManufacturerDataManager.Instance.Manufacturers.Tables[0].Rows[i]["manufacturer_name"] = name;
                    ManufacturerDataManager.Instance.Manufacturers.Tables[0].Rows[i]["country_FK"] = country_id;
                    break;
                }
            }

            string outStr = "";
            await Task.Run(() =>
            {
                try
                {
                    adapterManufacturer.UpdateCommand = new SqlCommand(cmd, connection);
                    outStr = (adapterManufacturer.Update(ManufacturerDataManager.Instance.Manufacturers)).ToString();
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
            if (AvailabilityСheck(ManufacturerDataManager.Instance.Manufacturers, "manufacturer_name", name) == true) return "Такой производитель уже имеется в базе!";

            string cmdInsert = $"insert into manufacturers values ('{name}',{country_id});";
            DataRow row = ManufacturerDataManager.Instance.Manufacturers.Tables[0].NewRow();
            row["manufacturer_name"] = name;
            row["country_FK"] = country_id;
            ManufacturerDataManager.Instance.Manufacturers.Tables[0].Rows.Add(row);

            string outStr = "";
            await Task.Run(() =>
            {
                try
                {
                    adapterManufacturer.InsertCommand = new SqlCommand(cmdInsert, connection);
                    outStr = (adapterManufacturer.Update(ManufacturerDataManager.Instance.Manufacturers)).ToString();
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

            for (int i = 0; i < ManufacturerDataManager.Instance.Manufacturers.Tables[0].Rows.Count; i++)
            {
                if (ManufacturerDataManager.Instance.Manufacturers.Tables[0].Rows[i].Field<int>("manufacturer_id") == id)
                {
                    ManufacturerDataManager.Instance.Manufacturers.Tables[0].Rows[i].Delete();
                    break;
                }
            }

            string outStr = "";
            await Task.Run(() =>
            {
                try
                {
                    adapterManufacturer.DeleteCommand = new SqlCommand(cmdDel, connection);
                    outStr = (adapterManufacturer.Update(ManufacturerDataManager.Instance.Manufacturers)).ToString();
                }
                catch (Exception ex)
                {
                    outStr = ex.Message;
                }
            });
            return outStr;            
        }


        public async Task<string> ReadSupplierTableAsync()
        {
            try
            {
                adapterSupplier = new SqlDataAdapter($"Select * from suppliers;", connection);
                commandBuilder = new SqlCommandBuilder(adapterSupplier);
                SupplierDataManager.Instance.Suppliers.Clear();
                return (await Task.Run(() => adapterSupplier.Fill(SupplierDataManager.Instance.Suppliers))).ToString();
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public async Task<string> UpdateSupplierAsync(string name, int id)
        {
            await ReadSupplierTableAsync();
            if (AvailabilityСheck(SupplierDataManager.Instance.Suppliers, "supplier_name", name) == true) return "Такой поставщик уже имеется в базе!";

            string cmd = $"update suppliers set supplier_name ='{name}' where supplier_id = {id};";
            SqlCommand update = new SqlCommand(cmd, connection);

            for (int i = 0; i < SupplierDataManager.Instance.Suppliers.Tables[0].Rows.Count; i++)
            {
                if (SupplierDataManager.Instance.Suppliers.Tables[0].Rows[i].Field<int>("supplier_id") == id)
                {
                    SupplierDataManager.Instance.Suppliers.Tables[0].Rows[i]["supplier_name"] = name;
                    break;
                }
            }

            string outStr = "";
            await Task.Run(() =>
            {
                try
                {
                    adapterSupplier.UpdateCommand = new SqlCommand(cmd, connection);
                    outStr = (adapterSupplier.Update(SupplierDataManager.Instance.Suppliers)).ToString();
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
            if (AvailabilityСheck(SupplierDataManager.Instance.Suppliers, "supplier_name", name) == true) return "Такой поставщик уже имеется в базе!";

            string cmdInsert = $"insert into suppliers values ('{name}');";
            DataRow row = SupplierDataManager.Instance.Suppliers.Tables[0].NewRow();
            row["supplier_name"] = name;
            SupplierDataManager.Instance.Suppliers.Tables[0].Rows.Add(row);

            string outStr = "";
            await Task.Run(() =>
            {
                try
                {
                    adapterSupplier.InsertCommand = new SqlCommand(cmdInsert, connection);
                    outStr = (adapterSupplier.Update(SupplierDataManager.Instance.Suppliers)).ToString();
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

            for (int i = 0; i < SupplierDataManager.Instance.Suppliers.Tables[0].Rows.Count; i++)
            {
                if (SupplierDataManager.Instance.Suppliers.Tables[0].Rows[i].Field<int>("supplier_id") == id)
                {
                    SupplierDataManager.Instance.Suppliers.Tables[0].Rows[i].Delete();
                    break;
                }
            }

            string outStr = "";
            await Task.Run(() =>
            {
                try
                {
                    adapterSupplier.DeleteCommand = new SqlCommand(cmdDel, connection);
                    outStr = (adapterSupplier.Update(SupplierDataManager.Instance.Suppliers)).ToString();
                }
                catch (Exception ex)
                {
                    outStr = ex.Message;
                }
            });
            return outStr;
        }



        public async Task<string> ReadDeviceModelTableAsync()
        {
            adapterModelDevice = new SqlDataAdapter($"Select *  from deviceModels", connection);
            //adapterModelDevice = new SqlDataAdapter($"Select deviceModel_id, model_name, modelType_FK, weight, price, stock_balance, manufacturer_FK, supplier_FK  from deviceModels " +
            //                                       $"inner join modelTypes on modelTypes.modelType_id = modelType_FK " +
            //                                       $"inner join manufacturers on manufacturers.manufacturer_id = manufacturer_FK " +
            //                                       $"inner join suppliers on suppliers.supplier_id = supplier_FK;", connection);
            commandBuilder = new SqlCommandBuilder(adapterModelDevice);
            DeviceModelDataManager.Instance.DeviceModels.Clear();

            string outStr = "";
            await Task.Run(() =>
            {
                try
                {
                    outStr = adapterModelDevice.Fill(DeviceModelDataManager.Instance.DeviceModels).ToString();
                }
                catch (Exception ex)
                {
                    outStr = ex.Message;
                }
            });
            return outStr;
        }

        public async Task<string> AddDeviceModelAsync(string deviceModel_name,
                                                        int modelType_id,
                                                        int weight,
                                                        int price,
                                                        int stock_balance,
                                                        int manufacturer_id,
                                                        int supplier_id)
        {
            await ReadDeviceModelTableAsync();
            if (AvailabilityСheck(DeviceModelDataManager.Instance.DeviceModels, "model_name", deviceModel_name) == true) return "Такая модель устройства уже имеется в базе!";

            string cmdInsert = $"insert into DeviceModels values ('{deviceModel_name}', {modelType_id}, {weight}, {price}, {stock_balance}, {manufacturer_id}, {supplier_id});";

            DataRow row = DeviceModelDataManager.Instance.DeviceModels.Tables[0].NewRow();
            row["model_name"] = deviceModel_name;
            row["modelType_FK"] = modelType_id;
            row["weight"] = weight;
            row["price"] = price;
            row["stock_balance"] = stock_balance;
            row["manufacturer_FK"] = manufacturer_id;
            row["supplier_FK"] = supplier_id;
            DeviceModelDataManager.Instance.DeviceModels.Tables[0].Rows.Add(row);

            string outStr = "";
            await Task.Run(() =>
            {
                try
                {
                    adapterModelDevice.InsertCommand = new SqlCommand(cmdInsert, connection);
                    outStr = (adapterModelDevice.Update(DeviceModelDataManager.Instance.DeviceModels)).ToString();
                }
                catch (Exception ex)
                {
                    outStr = ex.Message;
                }
            });
            return outStr;            
        }

        public async Task<string> UpdateDeviceModelAsync(string deviceModel_name,
                                                        int modelType_id,
                                                        int weight,
                                                        int price,
                                                        int stock_balance,
                                                        int manufacturer_id,
                                                        int supplier_id,
                                                        int deviceModel_id)
        {
            await ReadDeviceModelTableAsync();
            if (AvailabilityСheck(DeviceModelDataManager.Instance.DeviceModels, "model_name", deviceModel_name) == true) return "Такая модель устройства уже имеется в базе!";

            string cmd = $"update DeviceModels set model_name ='{deviceModel_name}'," +
                                    $" modelType_FK = {modelType_id}," +
                                    $" weight = {weight}," +
                                    $" price = {price}," +                                    
                                    $" manufacturer_FK = {manufacturer_id}," +
                                    $" supplier_FK = {supplier_id}" +
                                    $" where deviceModel_id = {deviceModel_id};";
            SqlCommand update = new SqlCommand(cmd, connection);

            for (int i = 0; i < DeviceModelDataManager.Instance.DeviceModels.Tables[0].Rows.Count; i++)
            {
                if (DeviceModelDataManager.Instance.DeviceModels.Tables[0].Rows[i].Field<int>("deviceModel_id") == deviceModel_id)
                {
                    DeviceModelDataManager.Instance.DeviceModels.Tables[0].Rows[i]["model_name"] = deviceModel_name;
                    DeviceModelDataManager.Instance.DeviceModels.Tables[0].Rows[i]["modelType_FK"] = modelType_id;
                    DeviceModelDataManager.Instance.DeviceModels.Tables[0].Rows[i]["weight"] = weight;
                    DeviceModelDataManager.Instance.DeviceModels.Tables[0].Rows[i]["price"] = price;
                    DeviceModelDataManager.Instance.DeviceModels.Tables[0].Rows[i]["manufacturer_FK"] = manufacturer_id;
                    DeviceModelDataManager.Instance.DeviceModels.Tables[0].Rows[i]["supplier_FK"] = supplier_id;
                    break;
                }
            }

            string outStr = "";
            await Task.Run(() =>
            {
                try
                {
                    adapterModelDevice.UpdateCommand = new SqlCommand(cmd, connection);
                    outStr = (adapterModelDevice.Update(DeviceModelDataManager.Instance.DeviceModels)).ToString();
                }
                catch (Exception ex)
                {
                    outStr = ex.Message;
                }
            });
            return outStr;            
        }

        public async Task<string> DeleteDeviceModelAsync(string info)
        {
            string[] result = info.Split('.');
            int id = int.Parse(result[0]);
            await ReadDeviceModelTableAsync();

            string cmdDel = $"delete from deviceModels where deviceModel_id = {id};";
            SqlCommand delete = new SqlCommand(cmdDel, connection);

            for (int i = 0; i < DeviceModelDataManager.Instance.DeviceModels.Tables[0].Rows.Count; i++)
            {
                if (DeviceModelDataManager.Instance.DeviceModels.Tables[0].Rows[i].Field<int>("deviceModel_id") == id)
                {
                    DeviceModelDataManager.Instance.DeviceModels.Tables[0].Rows[i].Delete();
                    break;
                }
            }

            string outStr = "";
            await Task.Run(() =>
            {
                try
                {
                    adapterModelDevice.DeleteCommand = new SqlCommand(cmdDel, connection);
                    outStr = (adapterModelDevice.Update(DeviceModelDataManager.Instance.DeviceModels)).ToString();
                }
                catch (Exception ex)
                {
                    outStr = ex.Message;
                }
            });
            return outStr;
        }


        public async Task<string> ReadDeviceTableAsync()
        {
            adapterDevice = new SqlDataAdapter($"Select * from devices", connection);
            commandBuilder = new SqlCommandBuilder(adapterDevice);
            DeviceDataManager.Instance.Devices.Clear();

            string outStr = "";
            await Task.Run(() =>
            {
                try
                {
                    outStr = adapterDevice.Fill(DeviceDataManager.Instance.Devices).ToString();
                }
                catch (Exception ex)
                {
                    outStr = ex.Message;
                }
            });
            return outStr;
        }

        public async Task<string> AddDeviceAsync(int deviceModel_id, string serial_number, DateTime manufacture_date, bool isDefected)
        {
            await ReadDeviceTableAsync();


            if (AvailabilityСheck(DeviceDataManager.Instance.Devices, "serial_number", serial_number) == true) return "Такое устройство уже имеется в базе!";

            int defect = 0;
            if (isDefected) defect = 1;
            string cmdInsert = $"insert into devices values ({deviceModel_id}, '{serial_number}', '{manufacture_date}', {defect});";

            DataRow row = DeviceDataManager.Instance.Devices.Tables[0].NewRow();
            row["deviceModel_FK"] = deviceModel_id;
            row["serial_number"] = serial_number;
            row["manufacture_date"] = manufacture_date;
            row["isDefected"] = defect;            
            DeviceDataManager.Instance.Devices.Tables[0].Rows.Add(row);

            string outStr = "";
            await Task.Run(() =>
            {
                try
                {
                    adapterDevice.InsertCommand = new SqlCommand(cmdInsert, connection);
                    outStr = (adapterDevice.Update(DeviceDataManager.Instance.Devices)).ToString();
                }
                catch (Exception ex)
                {
                    outStr = ex.Message;
                }
            });
            return outStr;
        }

        public async Task<string> UpdateDeviceAsync(int deviceModel_id, string serial_number, DateTime manufacture_date, bool isDefected, int device_id)
        {
            await ReadDeviceTableAsync();
            //if (AvailabilityСheck(DeviceDataManager.Instance.Devices, "serial_number", serial_number) == true) return "Такое устройство уже имеется в базе!";

            int defect = 0;
            if (isDefected) defect = 1;
            string cmd =    $"update devices set deviceModel_FK = {deviceModel_id}, " +
                            $"serial_number ='{serial_number}', " +
                            $"manufacture_date ='{manufacture_date}', " +
                            $"isDefected = {defect} " +                            
                            $"where device_id = {device_id};";
            SqlCommand update = new SqlCommand(cmd, connection);

            for (int i = 0; i < DeviceDataManager.Instance.Devices.Tables[0].Rows.Count; i++)
            {
                if (DeviceDataManager.Instance.Devices.Tables[0].Rows[i].Field<int>("device_id") == device_id)
                {
                    DeviceDataManager.Instance.Devices.Tables[0].Rows[i]["deviceModel_FK"] = deviceModel_id;
                    DeviceDataManager.Instance.Devices.Tables[0].Rows[i]["serial_number"] = serial_number;
                    DeviceDataManager.Instance.Devices.Tables[0].Rows[i]["manufacture_date"] = manufacture_date;
                    DeviceDataManager.Instance.Devices.Tables[0].Rows[i]["isDefected"] = defect;                    
                    break;
                }
            }

            string outStr = "";
            await Task.Run(() =>
            {
                try
                {
                    adapterDevice.UpdateCommand = new SqlCommand(cmd, connection);
                    outStr = (adapterDevice.Update(DeviceDataManager.Instance.Devices)).ToString();
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
            int id = int.Parse(result[0]);
            await ReadDeviceTableAsync();

            string cmdDel = $"delete from devices where device_id = {id};";
            SqlCommand delete = new SqlCommand(cmdDel, connection);

            for (int i = 0; i < DeviceDataManager.Instance.Devices.Tables[0].Rows.Count; i++)
            {
                if (DeviceDataManager.Instance.Devices.Tables[0].Rows[i].Field<int>("device_id") == id)
                {
                    DeviceDataManager.Instance.Devices.Tables[0].Rows[i].Delete();
                    break;
                }
            }

            string outStr = "";
            await Task.Run(() =>
            {
                try
                {
                    adapterDevice.DeleteCommand = new SqlCommand(cmdDel, connection);
                    outStr = (adapterDevice.Update(DeviceDataManager.Instance.Devices)).ToString();
                }
                catch (Exception ex)
                {
                    outStr = ex.Message;
                }
            });
            return outStr;
        }


        public async Task<string> ReadModelTypeTableAsync()
        {
            adapterModelType = new SqlDataAdapter($"Select * from modelTypes;", connection);
            commandBuilder = new SqlCommandBuilder(adapterModelType);
            ModelTypeDataManager.Instance.ModelTypes.Clear();

            string outStr = "";
            await Task.Run(() =>
            {
                try
                {
                    outStr = adapterModelType.Fill(ModelTypeDataManager.Instance.ModelTypes).ToString();
                }
                catch (Exception ex)
                {
                    outStr = ex.Message;
                }
            });
            return outStr;
        }

        public async Task<string> UpdateModelTypeAsync(string name, int id)
        {
            await ReadModelTypeTableAsync();
            if (AvailabilityСheck(ModelTypeDataManager.Instance.ModelTypes, "modelType_name", name) == true) return "Такой тип уже имеется в базе!";

            string cmd = $"update modelTypes set modelType_name ='{name}' where modelType_id = {id};";
            SqlCommand update = new SqlCommand(cmd, connection);

            for (int i = 0; i < ModelTypeDataManager.Instance.ModelTypes.Tables[0].Rows.Count; i++)
            {
                if (ModelTypeDataManager.Instance.ModelTypes.Tables[0].Rows[i].Field<int>("modelType_id") == id)
                {
                    ModelTypeDataManager.Instance.ModelTypes.Tables[0].Rows[i]["modelType_name"] = name;
                    break;
                }
            }

            string outStr = "";
            await Task.Run(() =>
            {
                try
                {
                    adapterModelType.UpdateCommand = new SqlCommand(cmd, connection);
                    outStr = (adapterModelType.Update(ModelTypeDataManager.Instance.ModelTypes)).ToString();
                }
                catch (Exception ex)
                {
                    outStr = ex.Message;
                }
            });
            return outStr;
        }

        public async Task<string> AddModelTypeAsync(string name)
        {
            await ReadModelTypeTableAsync();
            if (AvailabilityСheck(ModelTypeDataManager.Instance.ModelTypes, "modelType_name", name) == true) return "Такой тип уже имеется в базе!";

            string cmdInsert = $"insert into modelTypes values ('{name}');";
            DataRow row = ModelTypeDataManager.Instance.ModelTypes.Tables[0].NewRow();
            row["modelType_name"] = name;
            ModelTypeDataManager.Instance.ModelTypes.Tables[0].Rows.Add(row);

            string outStr = "";
            await Task.Run(() =>
            {
                try
                {
                    adapterModelType.InsertCommand = new SqlCommand(cmdInsert, connection);
                    outStr = (adapterModelType.Update(ModelTypeDataManager.Instance.ModelTypes)).ToString();
                }
                catch (Exception ex)
                {
                    outStr = ex.Message;
                }
            });
            return outStr;
        }

        public async Task<string> DeleteModelTypeAsync(string info)
        {
            string[] result = info.Split('.');
            int id = int.Parse(result[0]);
            await ReadModelTypeTableAsync();

            string cmdDel = $"delete from modelTypes where modelType_id = {id};";
            SqlCommand delete = new SqlCommand(cmdDel, connection);

            for (int i = 0; i < ModelTypeDataManager.Instance.ModelTypes.Tables[0].Rows.Count; i++)
            {
                if (ModelTypeDataManager.Instance.ModelTypes.Tables[0].Rows[i].Field<int>("modelType_id") == id)
                {
                    ModelTypeDataManager.Instance.ModelTypes.Tables[0].Rows[i].Delete();
                    break;
                }
            }

            string outStr = "";
            await Task.Run(() =>
            {
                try
                {
                    adapterModelType.DeleteCommand = new SqlCommand(cmdDel, connection);
                    outStr = (adapterModelType.Update(ModelTypeDataManager.Instance.ModelTypes)).ToString();
                }
                catch (Exception ex)
                {
                    outStr = ex.Message;
                }
            });
            return outStr;
        }


        public async Task<string> ReadOrderTableAsync()
        {
            adapterOrder = new SqlDataAdapter($"Select * from orders;", connection);
            commandBuilder = new SqlCommandBuilder(adapterOrder);
            OrderDataManager_.Instance.Orders.Clear();

            string outStr = "";
            await Task.Run(() =>
            {
                try
                {
                    outStr = adapterOrder.Fill(OrderDataManager_.Instance.Orders).ToString();
                }
                catch (Exception ex)
                {
                    outStr = ex.Message;
                }
            });
            return outStr;
        }

        public async Task<string> UpdateOrderAsync(string name, DateTime order_date, DateTime shiping_date, int id)
        {
            await ReadOrderTableAsync();
            //if (AvailabilityСheck(OrderDataManager_.Instance.Orders, "order_name", name) == true) return "Такой заказ уже имеется в базе!";

            string cmd =    $"update orders set order_name ='{name}', " +
                            $"order_date = '{order_date}', " +
                            $"shiping_date = '{shiping_date}' " +
                            $"where order_id = {id};";
            SqlCommand update = new SqlCommand(cmd, connection);

            for (int i = 0; i < OrderDataManager_.Instance.Orders.Tables[0].Rows.Count; i++)
            {
                if (OrderDataManager_.Instance.Orders.Tables[0].Rows[i].Field<int>("order_id") == id)
                {
                    OrderDataManager_.Instance.Orders.Tables[0].Rows[i]["order_name"] = name;
                    OrderDataManager_.Instance.Orders.Tables[0].Rows[i]["order_date"] = order_date;
                    OrderDataManager_.Instance.Orders.Tables[0].Rows[i]["shiping_date"] = shiping_date;
                    break;
                }
            }

            string outStr = "";
            await Task.Run(() =>
            {
                try
                {
                    adapterOrder.UpdateCommand = new SqlCommand(cmd, connection);
                    outStr = (adapterOrder.Update(OrderDataManager_.Instance.Orders)).ToString();
                }
                catch (Exception ex)
                {
                    outStr = ex.Message;
                }
            });
            return outStr;
        }

        public async Task<string> AddOrderAsync(string name, DateTime order_date, DateTime shiping_date)
        {
            await ReadOrderTableAsync();
            if (AvailabilityСheck(OrderDataManager_.Instance.Orders, "order_name", name) == true) return "Такой заказ уже имеется в базе!";
            
            string cmdInsert = $"insert into orders values ('{name}', '{order_date}', '{shiping_date}');";

            DataRow row = OrderDataManager_.Instance.Orders.Tables[0].NewRow();
            row["order_name"] = name;
            row["order_date"] = order_date;
            row["shiping_date"] = shiping_date;
            OrderDataManager_.Instance.Orders.Tables[0].Rows.Add(row);

            string outStr = "";
            await Task.Run(() =>
            {
                try
                {
                    adapterOrder.InsertCommand = new SqlCommand(cmdInsert, connection);
                    outStr = (adapterOrder.Update(OrderDataManager_.Instance.Orders)).ToString();
                }
                catch (Exception ex)
                {
                    outStr = ex.Message;
                }
            });
            return outStr;
        }

        public async Task<string> DeleteOrderAsync(string info)
        {
            string[] result = info.Split('.');
            int id = int.Parse(result[0]);
            await ReadOrderTableAsync();

            string cmdDel = $"delete from orders where order_id = {id};";
            SqlCommand delete = new SqlCommand(cmdDel, connection);

            for (int i = 0; i < OrderDataManager_.Instance.Orders.Tables[0].Rows.Count; i++)
            {
                if (OrderDataManager_.Instance.Orders.Tables[0].Rows[i].Field<int>("order_id") == id)
                {
                    OrderDataManager_.Instance.Orders.Tables[0].Rows[i].Delete();
                    break;
                }
            }

            string outStr = "";
            await Task.Run(() =>
            {
                try
                {
                    adapterOrder.DeleteCommand = new SqlCommand(cmdDel, connection);
                    outStr = (adapterOrder.Update(OrderDataManager_.Instance.Orders)).ToString();
                }
                catch (Exception ex)
                {
                    outStr = ex.Message;
                }
            });
            return outStr;
        }


        public async Task<string> ReadDeviceOrderTableAsync()
        {
            adapterDeviceOrder = new SqlDataAdapter($"Select * from deviceOrder;", connection);
            commandBuilder = new SqlCommandBuilder(adapterDeviceOrder);
            DeviceOrderDataManager.Instance.DeviceOrders.Clear();

            string outStr = "";
            await Task.Run(() =>
            {
                try
                {
                    outStr = adapterDeviceOrder.Fill(DeviceOrderDataManager.Instance.DeviceOrders).ToString();
                }
                catch (Exception ex)
                {
                    outStr = ex.Message;
                }
            });
            return outStr;
        }

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
    }
}
