using UnityEngine;
#if UNITY_EDITOR
using Sirenix.OdinInspector;
#endif

public partial class BuildToolkitWindow
{
#if UNITY_EDITOR

    [OnInspectorGUI("OnInspectorGUI", append: false)]
    private void OnInspectorGUI()
    {
        GUILayout.BeginVertical();
        GUILayout.Space(30);
        GUILayout.EndVertical();

        GUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();

        //if (GUILayout.Button(EditorGUIUtility.IconContent("SettingsIcon"), GUILayout.Width(50), GUILayout.Height(30)))
        //{

        //    //UpdateMode();
        //}
        GUILayout.FlexibleSpace();
        GUILayout.EndHorizontal();


    }
#endif
}
