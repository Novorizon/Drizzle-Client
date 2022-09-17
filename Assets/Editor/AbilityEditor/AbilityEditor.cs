using UnityEditor;
using Sirenix.OdinInspector.Editor;
using UnityEngine.SceneManagement;
using UnityEngine;
using System;
using UnityEditor.SceneManagement;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using PureMVC.Patterns.Proxy;
using PureMVC.Patterns.Facade;
using Game;
using DataBase;
using System.Reflection;
using PureMVC.Interfaces;
using System.Linq.Expressions;
using System.Diagnostics;
using Mono.Data.Sqlite;
using Database;
using System.Linq;
using Sirenix.OdinInspector;

namespace AbilityEditor
{
    public partial class AbilityEditor
    {
        static string dbName = "Test.db";
        string path = "URI=file:" + Application.streamingAssetsPath + "/" + dbName;
        SQLiteHelper db;

        TableProxy tableProxy;

        bool updated = false;


        [EditorMode] bool IsSettings = true;
        [EditorMode] bool IsNPC = false;
        [EditorMode] bool IsStatic = false;
        [EditorMode] bool IsQuest = false;

        public void SendNotification(string notificationName, object body = null, string type = null) => Facade.SendNotification(notificationName, body, type);

        protected IFacade Facade => PureMVC.Patterns.Facade.Facade.GetInstance(() => new Facade());



        protected int GetInt32(SqliteDataReader reader, string field)
        {
            if (reader.IsDBNull(reader.GetOrdinal(field)))
                return 0;

            return reader.GetInt32(reader.GetOrdinal(field));
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


        void UpdateMode()
        {
            Type type = typeof(AbilityEditor);
            FieldInfo[] Infos = type.GetFields(BindingFlags.NonPublic | BindingFlags.Instance);

            string Name = mode switch
            {
                EditorMode.Settings => GetVariableName(() => IsSettings),
                EditorMode.NPC => GetVariableName(() => IsNPC),
                EditorMode.Static => GetVariableName(() => IsStatic),
                EditorMode.Quest => GetVariableName(() => IsQuest),
                _ => IsSettings.ToString(),
            };

            for (int i = 0; i < Infos.Length; i++)
            {
                EditorModeAttribute a = System.Attribute.GetCustomAttribute(Infos[i], typeof(EditorModeAttribute)) as EditorModeAttribute;
                if (a != null)
                {
                    Infos[i].SetValue(this, false);
                    if (Name == Infos[i].Name)
                    {
                        Infos[i].SetValue(this, true);
                    }
                }
            }

        }



        static bool IsNumber(string input)
        {
            Regex reg = new Regex("^[1-9]\\d*$");
            return reg.IsMatch(input);
        }
        string GetVariableName<T>(Expression<Func<T>> expr)
        {
            var body = (MemberExpression)expr.Body;

            return body.Member.Name;
        }
    }
}