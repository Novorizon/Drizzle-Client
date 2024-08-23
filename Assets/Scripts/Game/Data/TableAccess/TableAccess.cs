using Database;
using Mono.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.Common;
using System.Threading.Tasks;

namespace DataBase
{
    public enum AccessType
    {
        Immediately = 1,//立即全量加载
        Cached = 1 << 2,//按需读并缓存

    }

    public abstract class TableAccess
    {
        public string Name { get; set; }
        public AccessType Type;
        public bool Loaded { get; set; }

        public abstract Type DataType { get; }

        public Dictionary<int, TableData> datas=new Dictionary<int, TableData>();
        private IReadOnlyDictionary<int, TableData> readOnlyDatas;

        public IReadOnlyDictionary<int, TableData> GetDatas()
        {
            if (readOnlyDatas == null)
            {
                readOnlyDatas = new ReadOnlyDictionary<int, TableData>(datas);
            }
            return readOnlyDatas;
        }



        public virtual TableData GetData(int index)
        {
            if (datas != null && datas.ContainsKey(index))
                return datas[index];

            return null;
        }

        public virtual void SetData(TableData data)
        {
            if (datas == null)
            {
                datas = new Dictionary<int, TableData>();
                datas.Add(data.id, data);
            }
            else
            {
                datas[data.id] = data;
            }

        }

        public virtual TableData GetData(string key)
        {
            return null;
        }

        public virtual TableData Reader(in SqliteDataReader reader)
        {
            return null;
        }
        public virtual async Task<TableData> ReaderAsync(DbDataReader reader)
        {
            await reader.ReadAsync();
            return null;
        }



        protected int GetInt32(SqliteDataReader reader, string field)
        {
            if (reader.IsDBNull(reader.GetOrdinal(field)))
                return 0;

            return reader.GetInt32(reader.GetOrdinal(field));
        }

        protected async Task<int> GetInt32Async(SqliteDataReader reader, string field)
        {
            int value = reader.GetOrdinal(field);
            if (await reader.IsDBNullAsync(value))
                return 0;

            return await reader.GetFieldValueAsync<int>(value);
        }

        protected long GetInt64(SqliteDataReader reader, string field)
        {
            if (reader.IsDBNull(reader.GetOrdinal(field)))
                return 0;

            return reader.GetInt64(reader.GetOrdinal(field));
        }

        protected float GetFloat(SqliteDataReader reader, string field)
        {
            if (reader.IsDBNull(reader.GetOrdinal(field)))
                return 0;

            return reader.GetFloat(reader.GetOrdinal(field));
        }

        protected string GetString(SqliteDataReader reader, string field)
        {
            if (reader.IsDBNull(reader.GetOrdinal(field)))
                return null;

            return reader.GetString(reader.GetOrdinal(field));
        }
    }
}