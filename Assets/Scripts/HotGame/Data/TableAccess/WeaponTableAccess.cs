using System.Collections.Generic;
using Mono.Data.Sqlite;
using System;
using Database;
using Game;

namespace DataBase
{
    [TableAccess]
    public class WeaponTableAccess : TableAccess
    {
        public override Type DataType => typeof(WeaponData);


        public WeaponTableAccess()
        {
            Name = "Weapon";
            Loaded = false;
        }


        public override TableData Reader(in SqliteDataReader reader)
        {
            WeaponData data = new WeaponData();

            data.id = GetInt32(reader, "id");
            data.name = GetString(reader, "name");
            data.description = GetString(reader, "description");
            data.type = (WeaponType)GetInt32(reader, "type");

            data.asset = GetString(reader, "asset");
            data.bulletAsset = GetString(reader, "bulletAsset");
            data.interval = GetFloat(reader, "interval");
            data.trackCount = GetInt32(reader, "trackCount");
            data.angle = GetFloat(reader, "angle");
            data.speed = GetFloat(reader, "speed");
            data.lifetime = GetFloat(reader, "lifetime");
            return data;
        }

    }

}