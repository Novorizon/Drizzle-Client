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
        Dictionary<int, WeaponData> datas;
        public override Type DataType => typeof(WeaponData);

        public Dictionary<int, WeaponData> GetDatas() => datas;


        public WeaponTableAccess()
        {
            Name = "Weapon";
            Loaded = false;
            datas = new Dictionary<int, WeaponData>();
        }


        public override bool Load(SQLiteHelper db)
        {
            if (db == null || string.IsNullOrEmpty(Name))
                return false;

            SqliteDataReader reader = db.ReadFullTable(Name);
            if (reader == null)
                return false;

            WeaponData data = null;

            while (reader.Read())
            {
                data = new WeaponData();

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
                datas.Add(data.id, data);
            }

            reader.Close();
            Loaded = true;

            return true;
        }

        public override TableData GetData(int index)
        {
            if (datas == null || !datas.ContainsKey(index))
                return null;

            return datas[index];
        }



    }

}