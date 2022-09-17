using UnityEditor;
using Sirenix.OdinInspector.Editor;
using UnityEngine.SceneManagement;
using UnityEngine;
using System;
using UnityEditor.SceneManagement;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Reflection;
using System.Diagnostics;
using Sirenix.OdinInspector;
using Game;
using DataBase;
using Database;
using System.Linq;
using Sirenix.Utilities.Editor;

namespace AbilityEditor
{
    [AttributeUsage(AttributeTargets.All, AllowMultiple = false, Inherited = true)]
    [Conditional("UNITY_EDITOR")]
    public class EditorModeAttribute : System.Attribute
    {
    }

    [Serializable]
    public partial class AbilityEditor : OdinEditorWindow
    {
        public static AbilityEditor Instance;


        [HideInInspector,]
        bool changed;



        EditorMode mode = EditorMode.Settings;


        [MenuItem("Tools/AbilityEditor")]
        public static void ShowWindow()
        {
            GetWindow<AbilityEditor>("World编辑器");
        }


        protected override void OnGUI()
        {
            GUILayout.BeginVertical();
            GUILayout.Space(30);
            GUILayout.EndVertical();

            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();

            if (GUILayout.Button(EditorGUIUtility.IconContent("Terrain Icon", "Terrain."), GUILayout.Width(50), GUILayout.Height(30)))
            {
                mode = EditorMode.Settings;
                UpdateMode();
            }
            if (GUILayout.Button(EditorGUIUtility.IconContent("Prefab Icon"), GUILayout.Width(50), GUILayout.Height(30)))
            {
                mode = EditorMode.NPC;

                UpdateMode();
            }
            if (GUILayout.Button(EditorGUIUtility.IconContent("BuildSettings.Web.Small"), GUILayout.Width(50), GUILayout.Height(30)))
            {
                mode = EditorMode.Static;

                UpdateMode();
            }
            if (GUILayout.Button(EditorGUIUtility.IconContent("SettingsIcon"), GUILayout.Width(50), GUILayout.Height(30)))
            {
                mode = EditorMode.Quest;

                UpdateMode();
            }
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
            base.OnGUI();

        }

        protected override void OnEnable()
        {


            if (Instance != null)
            {
                Instance.Close();
                Instance = null;
            }
            Instance = this;


            hasUnsavedChanges = false;
            autoRepaintOnSceneChange = true;
            saveChangesMessage = "Click \"Cancel\" and export data to Json. \nOr click \"Save\" or \"Discard\" to close Level Editor.";

            db = new SQLiteHelper();
            Facade.RegisterProxy(new TableProxy());
            Facade.RegisterProxy(new NpcProxy());
            tableProxy = Facade.RetrieveProxy(TableProxy.NAME) as TableProxy;

            tableProxy.RegisterTable<ModelTableAccess>();
            tableProxy.RegisterTable<NPCTableAccess>();


            db.Open(path);
            tableProxy.Load();
            db.Close();

            EditorApplication.update += Update;

        }

        private void OnDisable()
        {
            EditorApplication.update -= Update;


            hasUnsavedChanges = false;
            autoRepaintOnSceneChange = false;
        }


        private void OnSceneGUI(SceneView sceneView)
        {
        }

        private void Update()
        {
            if (Instance == null)
                return;
        }

    }
}