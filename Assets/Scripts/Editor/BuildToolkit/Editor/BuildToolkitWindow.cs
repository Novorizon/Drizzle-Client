using System.Collections.Generic;
using Sirenix.OdinInspector.Editor;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;


using Sirenix.OdinInspector;
using Newtonsoft.Json;
using System.IO;
using static UnityEditor.PlayerSettings;

public partial class BuildToolkitWindow : OdinEditorWindow
{

    [MenuItem("Tools/BuildToolkit")]
    public static bool ShowWindow()
    {
        Scene scene = SceneManager.GetActiveScene();

        if (scene != null)
        {
            GetWindow<BuildToolkitWindow>("BuildToolkit").autoRepaintOnSceneChange = true;
            //GetWindow<ParticleMonitorWindow>().minSize = new Vector2(200, 100);
            //GetWindow<ParticleMonitorWindow>().position = new Rect(0f, 0f, 320f, 550f);
            //EditorApplication.update += Update;
            return true;
        }
        return false;
    }

    private void OnEnable()
    {
        hasUnsavedChanges = false;
        saveChangesMessage = "Click \"Cancel\" and export data to Json. \nOr click \"Save\" or \"Discard\" to close Level Editor.";
    }

    private void OnDisable()
    {
        Scene scene = SceneManager.GetActiveScene();
        if (scene != null && scene.name.Equals("ParticleMonitor"))
        {
            GetWindow<BuildToolkitWindow>("BuildToolkit").autoRepaintOnSceneChange = false;
            EditorApplication.update -= Update;
        }
        hasUnsavedChanges = false;
    }

    static public void Update()
    {
        Debug.LogError("aaaaaaaaaaaa");
    }


}
