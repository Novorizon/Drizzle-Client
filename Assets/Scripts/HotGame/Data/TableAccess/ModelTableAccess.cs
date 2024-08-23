using Mono.Data.Sqlite;
using System;

namespace DataBase
{
    [TableAccess]
    public class ModelTableAccess : TableAccess
    {
        public override Type DataType => typeof(ModelData);
  

        public ModelTableAccess()
        {
            Name = "Model";
            Loaded = false;
        }


        public override TableData Reader(in SqliteDataReader reader)
        {
            ModelData data = new ModelData();

            data.id = GetInt32(reader, "id");
            data.name = GetString(reader, "name");
            data.description = GetString(reader, "description");
            data.path = GetString(reader, "path");
            return data;
        }

    }

}