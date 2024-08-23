using System.Collections.Generic;
using Mono.Data.Sqlite;
using System;
using Database;

namespace DataBase
{
    [TableAccess]
    public class DefaultTableAccess : TableAccess
    {
        public override Type DataType => typeof(DefaultData);


        public DefaultTableAccess()
        {
            Name = "DefaultInfo";
            Loaded = false;
        }

        public override TableData Reader(in SqliteDataReader reader)
        {
            DefaultData data = new DefaultData();

            data.id = GetInt32(reader, "id");
            data.name = GetString(reader, "name");
            data.modelId = GetInt32(reader, "modelId");
            data.speed = GetFloat(reader, "speed");
            string positions = GetString(reader, "position");
            string[] position = positions.Split(',');
            if (position.Length == 3)
            {
                float.TryParse(position[0], out float x);
                float.TryParse(position[1], out float y);
                float.TryParse(position[2], out float z);
                data.position = new UnityEngine.Vector3(x, y, z);
            }

            string forwards = GetString(reader, "forward");
            string[] forward = forwards.Split(',');
            if (forward.Length == 3)
            {
                float.TryParse(forward[0], out float x);
                float.TryParse(forward[1], out float y);
                float.TryParse(forward[2], out float z);
                data.forward = new UnityEngine.Vector3(x, y, z);
            }
            return data;
        }


    }

}