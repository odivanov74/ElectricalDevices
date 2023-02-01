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
        //DataSet data;

        //DataSet userData;
        SqlDataAdapter adapter;
        SqlCommandBuilder commandBuilder;
        
        

        public DataBaseService(SqlConnection connection)
        {
            this.connection = connection;            
        }

        public async Task<IEnumerable<string>> GetTableListAsync()
        {
           
            List<string> temp = new List<string>();
            await connection.OpenAsync();
            SqlCommand command = new SqlCommand("select * from ElectricalDevices_v3.information_schema.tables", connection);
            SqlDataReader reader = await command.ExecuteReaderAsync();
            while (await reader.ReadAsync() == true)
            {
                temp.Add(reader.GetString(2));
            }
            connection.Close();
            return temp;
        }

        public async Task<DataSet> ReadDataTableAsync(SqlDataAdapter adapter, SqlCommandBuilder commandBuilder)
        {
            try
            {
                DataSet data = new DataSet();
                //adapter = new SqlDataAdapter($"Select * from {tableName};", connection);
                //commandBuilder = new SqlCommandBuilder(adapter);
                await Task.Run(() => adapter.Fill(data));                
                return data;
            }
            catch
            {
                return null;
            }
        }

        public async Task<bool> UpdateDataAsync(SqlDataAdapter adapter, DataSet data)
        {
            try
            {
                await Task.Run(() => adapter.Update(data));
                return true;
            }
            catch(Exception)
            {
                return false;
            }
                        
        }

        public async Task<DataSet> ReadUserRightAsync(string userName)
        {            
            DataSet rightData = new DataSet();
            adapter = new SqlDataAdapter(   $"Select rights.right_id, right_name from rights " +
                                            $"inner join userRight as UR on rights.right_id = UR.right_id " +
                                            $"inner join users on users.user_id = UR.user_id " +
                                            $"where users.user_name = '{userName}'; ",
                                            connection);
            commandBuilder = new SqlCommandBuilder(adapter);
            await Task.Run(() => adapter.Fill(rightData));
            
            return rightData;
        }        

        //public async void AddTypeDevice(string modelName, string modelType, int weight, int price, int stockBalance, int manufacturerId, int supplierId)
        //{
        //    await Task.Run(() => 
        //    {
        //        SqlCommand command = new SqlCommand($"insert into devicemodels (model_name, model_type, weight, price, stock_balance,manufacturer_FK,supplier_FK ) " +
        //                                                                      $"values ('{modelName}', '{modelType}', {weight}, {price}, {stockBalance}, {manufacturerId}, {supplierId})", connection);              
        //        DataRow row = data.Tables[0].NewRow();
        //        row["model_Name"] = modelName;
        //        row["model_Type"] = modelType;
        //        row["weight"] = weight;
        //        row["price"] = price;
        //        row["stock_Balance"] = stockBalance;
        //        row["manufacturer_FK"] = manufacturerId;
        //        row["supplier_FK"] = supplierId;
        //        data.Tables[0].Rows.Add(row);

        //        adapter.InsertCommand = command;

        //        adapter.Update(data);
        //    });
        //}

        public async Task<bool> AddUserAsync(string cmdInsertUser, string cmdInsertUserRight, DataSet rightData, DataSet userData)
        {
            try
            {
                await Task.Run(() =>
                {
                    adapter.InsertCommand = new SqlCommand(cmdInsertUser, connection);
                    adapter.Update(userData);
                });

                await Task.Run(() =>
                {
                    SqlCommand command = new SqlCommand(cmdInsertUserRight, connection);
                    adapter.InsertCommand = command;
                    adapter.Update(rightData);

                });
                return true;
            }
            catch(Exception)
            {
                return false;
            }
            
        }



    }
}
