using System.Collections.Generic;
using Mono.Data.Sqlite;
using System;
using Database;

namespace DataBase
{
    [TableAccess]
    public class NpcTableAccess : TableAccess
    {
        public override Type DataType => typeof(NPCData);


        public NpcTableAccess()
        {
            Name = "NPC";
            Loaded = false;
        }


        public override TableData Reader(in SqliteDataReader reader)
        {
            NPCData data = new NPCData();

            data.id = GetInt32(reader, "id");
            data.name = GetString(reader, "name");
            data.description = GetString(reader, "description");
            data.type = (NPCType)GetInt32(reader, "type");

            data.modelId = GetInt32(reader, "modelId");
            data.model = GetString(reader, "model");
            data.avatar = GetString(reader, "avatar");
            datas.Add(data.id, data);
            return data;
        }
    }

}