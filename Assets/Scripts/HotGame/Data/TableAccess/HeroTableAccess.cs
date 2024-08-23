using System.Collections.Generic;
using Mono.Data.Sqlite;
using System;
using Database;
using static Game.GameConsts;
using System.Reflection;
using System.Linq;
using System.Threading.Tasks;
using System.Data.Common;

namespace DataBase
{
    [TableAccess]
    public class HeroTableAccess : TableAccess
    {
        public override Type DataType => typeof(HeroData);

        public HeroTableAccess()
        {
            Name = "hero";
            Loaded = false;
        }

        public override TableData Reader(in SqliteDataReader reader)
        {
            HeroData data = new HeroData();
            data.id = GetInt32(reader, "id");
            int name = GetInt32(reader, "name");
            data.name = "test";
            int description = GetInt32(reader, "description");
            data.description = "test";
            data.type = GetInt32(reader, "type");
            return data;
        }
    }

}