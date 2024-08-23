using Database;
using DataBase;
using Mono.Data.Sqlite;
using PureMVC.Patterns.Proxy;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using UnityEngine;

namespace Game
{
    public class TableProxy : Proxy
    {

        public new static string NAME = typeof(TableProxy).FullName;

        //internal SQLiteHelper db;
        private Dictionary<Type, TableAccess> accessors;
        private Dictionary<Type, Type> accessorTypes;

        string name;
        string path;
        bool loaded;
        bool initialized;
        public TableProxy() : base(NAME) { }

        public override void OnRegister()
        {
            Init();
            //db = new SQLiteHelper(path);
            //Register();
        }

        public override void OnRemove()
        {
        }


        public void Init()
        {
            name = "Test.db";
            path = "";

            accessors = new Dictionary<Type, TableAccess>();
            accessorTypes = new Dictionary<Type, Type>();

#if UNITY_EDITOR
            loaded = true;
#else
            loaded = false;
#endif
            initialized = false;


#if UNITY_EDITOR
            path = Application.streamingAssetsPath + "/" + name;
#elif UNITY_STANDALONE_WIN
            path = Application.streamingAssetsPath + "/" + name;  
#elif UNITY_ANDROID
            path ="URL=file:"+ Application.persistentDataPath + "/" + name;  
#elif UNITY_IPHONE
            path = Application.persistentDataPath + "/" + name;  

            //判断路径内数据库是否存在  
            if(!File.Exists(path))  
            {  
                //拷贝数据库  
                //StartCoroutine(CopyDataBase());
                return;
            }  
#endif

            path = "URI=file:" + path;

        }

        public void Register()
        {
            System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
            sw.Start();
            //Assembly assembly = Assembly.GetExecutingAssembly();// Assembly.Load("Assembly-CSharp");
            Assembly assembly = typeof(TableProxy).Assembly;
            try
            {
                var assemblyTypes = assembly.GetTypes();
                for (int i = 0; i < assemblyTypes.Length; i++)
                {
                    var type = assemblyTypes[i];
                    TableAccessAttribute a = System.Attribute.GetCustomAttribute(assemblyTypes[i], typeof(TableAccessAttribute)) as TableAccessAttribute;
                    if (a != null)
                    {
                        RegisterTable(type, a.type);
                    }
                }
            }
            catch (Exception e)
            {
            }

            sw.Stop();
            Debug.LogError("用时：" + sw.ElapsedMilliseconds + "");
        }

        public void RegisterTable<T>(AccessType type = AccessType.Immediately) where T : TableAccess, new()
        {
            if (accessors == null || accessorTypes == null || accessors.ContainsKey(typeof(T)))
                return;

            T accessor = new T();
            accessor.Type = type;

            accessors.Add(typeof(T), accessor);

            if (!accessorTypes.ContainsKey(accessor.DataType))
                accessorTypes.Add(accessor.DataType, typeof(T));
        }

        public void RegisterTable(Type type, AccessType accessType = AccessType.Immediately)
        {
            if (accessors == null || accessorTypes == null || accessors.ContainsKey(type))
                return;

            TableAccess accessor = Activator.CreateInstance(type) as TableAccess;//创建该类型的实例
            accessor.Type = accessType;
            accessors.Add(type, accessor);

            if (!accessorTypes.ContainsKey(type))
                accessorTypes.Add(accessor.DataType, type);
        }


        public bool Load(Action onComplete = null)
        {
            if (accessors == null)
                return false;

            foreach (TableAccess accessor in accessors.Values)
            {
                if (accessor == null || accessor.Loaded)
                    continue;

                if (accessor.Type != AccessType.Immediately)
                    continue;

                Load(accessor);
            }

            onComplete?.Invoke();
            return true;
        }

        public async Task<bool> LoadAsync()
        {
            if (accessors == null)
                return false;

            foreach (TableAccess accessor in accessors.Values)
            {
                if (accessor == null || accessor.Loaded)
                    continue;

                if (accessor.Type != AccessType.Immediately)
                    continue;

              await  LoadAsync(accessor);
            }
            return true;
        }


        public bool Load<T>() where T : TableAccess
        {
            if (accessors == null)
                return false;

            if (!accessors.ContainsKey(typeof(T)))
                return false;

            TableAccess accessor = (T)accessors[typeof(T)];
            if (accessor == null || accessor.Loaded)
                return false;

            Load(accessor);

            return true;
        }


        public async Task<bool> LoadAsync<T>() where T : TableAccess, new()
        {
            if (accessors == null)
                return false;

            if (!accessors.ContainsKey(typeof(T)))
                return false;

            TableAccess accessor = (T)accessors[typeof(T)];
            if (accessor == null || accessor.Loaded)
                return false;

            return await LoadAsync(accessor);
        }

        public bool Load(TableAccess accessor)
        {
            using (var db = new SQLiteHelper(path))
            {
                SqliteDataReader reader = db.ReadFullTable(accessor.Name);
                if (reader == null)
                    return false;

                TableData data = null;

                while (reader.Read())
                {
                    data = accessor.Reader(reader);
                    accessor.SetData(data);
                }
            }

            accessor.Loaded = true;

            return true;
        }
        public async Task<bool> LoadAsync(TableAccess accessor)
        {
            using (var db = new SQLiteHelper(path))
            {
                SqliteDataReader reader =await db.ReadFullTableAsync(accessor.Name);
                if (reader == null)
                    return false;

                TableData data = null;

                while (await reader.ReadAsync())
                {
                    data = accessor.Reader(reader);
                    accessor.SetData(data);
                }
            }

            accessor.Loaded = true;

            return true;
        }

        public T GetAccess<T>() where T : TableAccess, new()
        {
            if (!accessors.ContainsKey(typeof(T)))
                return null;

            return (T)accessors[typeof(T)];
        }

        //public T GetData<T>(int id) where T : TableData
        //{
        //    if (accessorTypes == null || !accessorTypes.ContainsKey(typeof(T)))
        //        return null;

        //    Type type = accessorTypes[typeof(T)];

        //    if (accessors == null || !accessors.ContainsKey(type))
        //        return null;

        //    TableAccess accessor = accessors[type];

        //    if (accessor != null)
        //    {
        //        return (T)accessor.GetData(db, id);
        //    }

        //    return null;
        //}


        public T GetData<T>(int id) where T : TableData
        {
            if (accessorTypes == null || !accessorTypes.ContainsKey(typeof(T)))
                return null;

            Type type = accessorTypes[typeof(T)];
            if (accessors == null || !accessors.ContainsKey(type))
                return null;

            TableAccess accessor = accessors[type];
            if (accessor == null)
                return null;

            T data = accessor.GetData(id) as T;
            if (data != null)
            {
                return data;
            }

            using (var db = new SQLiteHelper(path))
            {
                SqliteDataReader reader = db.ReadFullTable(accessor.Name);
                if (reader == null)
                    return null;

                if (reader.Read())
                {
                    data = accessor.Reader(reader) as T;
                    accessor.SetData(data);
                }
                reader.Close();
            }
            return data;
        }

        public List<TableData> GetData<T>(List<int> indices) where T : TableData
        {
            if (accessorTypes == null || !accessorTypes.ContainsKey(typeof(T)))
                return null;

            Type type = accessorTypes[typeof(T)];
            if (accessors == null || !accessors.ContainsKey(type))
                return null;

            TableAccess accessor = accessors[type];
            if (accessor == null)
                return null;

            IReadOnlyDictionary<int, TableData> datas = accessor.GetDatas();
            List<TableData> existingInDatas = indices
                .Where(id => datas.ContainsKey(id))
                .Select(id => datas[id])
                .ToList();

            // 获取不在 datas 中的元素的索引
            List<int> missingInDatas = indices
                .Where(id => !datas.ContainsKey(id))
                .Select(id => indices.IndexOf(id))
                .ToList();

            var list = string.Join(",", missingInDatas);
            var query = $"select * from " + accessor.Name + " where id in ({list})";

            using (var db = new SQLiteHelper(path))
            {
                SqliteDataReader reader = db.ReadFullTable(accessor.Name);
                if (reader == null)
                    return existingInDatas;

                while (reader.Read())
                {
                    TableData data = accessor.Reader(reader);
                    accessor.SetData(data);
                    existingInDatas.Add(data);
                }
                reader.Close();
                return existingInDatas;
            }
        }
    }
}
