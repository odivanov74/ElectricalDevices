using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectricalDevicesCW.Managers
{
    public class ModelTypeDataManager
    {
        public DataSet ModelTypes { get; set; } = new DataSet();

        private ModelTypeDataManager() { }

        public static ModelTypeDataManager Instance { get => ModelTypeDataManagerCreate.instance; }

        private class ModelTypeDataManagerCreate
        {
            static ModelTypeDataManagerCreate() { }
            internal static readonly ModelTypeDataManager instance = new ModelTypeDataManager();
        }

        public List<string> GetFullDataListModelTypes()
        {
            List<string> modelTypes = new List<string>();

            for (int i = 0; i < ModelTypes.Tables[0].Rows.Count; i++)
            {
                modelTypes.Add($"{ModelTypes.Tables[0].Rows[i].Field<int>("modelType_id")}." +
                    $"{ModelTypes.Tables[0].Rows[i].Field<string>("modelType_name")}");
            }
            return modelTypes;
        }

        public string GetNameModelType(int idModel)
        {            
            return ModelTypes.Tables[0].Rows[idModel-1].Field<string>("modelType_name");
        }

        public List<string> GetNameListModelTypes()
        {
            List<string> modelTypes = new List<string>();

            for (int i = 0; i < ModelTypes.Tables[0].Rows.Count; i++)
            {
                modelTypes.Add($"{ModelTypes.Tables[0].Rows[i].Field<string>("modelType_name")}");
            }
            return modelTypes;
        }
    }
}
