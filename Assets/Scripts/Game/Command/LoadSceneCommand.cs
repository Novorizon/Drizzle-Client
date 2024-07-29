using Cinemachine;
using ECS;
using MVC;
using PureMVC.Interfaces;
using PureMVC.Patterns.Command;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game
{
    //public struct LoadSceneData
    //{
    //    public string name;
    //    public LoadSceneMode mode;
    //    public LoadSceneData(string name, LoadSceneMode mode = LoadSceneMode.Single)
    //    {
    //        this.name = name;
    //        this.mode = mode;
    //    }
    //}
    public class LoadSceneCommand : SimpleCommand
    {
        public const string NAME = "LoadSceneCommand";
        public override void Execute(INotification notification)
        {
            Debug.LogError("Time.frameCount" + Time.frameCount);
            Debug.LogError("LoadSceneCommand");
            if (notification.Body != null)
            {
                dynamic parameters = notification.Body;
                string name = parameters.name;
                LoadSceneMode mode = parameters.mode;
                if (name != null)
                {
                    ResourceManager.Instance.LoadSceneAsync(name, OnSceneLoaded, mode, true, null);
                }
            }
        }

        public void OnSceneLoaded(Scene scene, object userdata)
        {
            SceneManager.SetActiveScene(scene);

            //var VirtualCamera = GameObject.FindObjectOfType<CinemachineVirtualCamera>();
            //if (VirtualCamera != null)
            //{
            //    var vcam = VirtualCamera.GetComponent<CinemachineVirtualCamera>();
            //    if (vcam != null)
            //    {
            //        //var entity = EntityManager.Create(VirtualCamera.gameObject);
            //        //EntityManager.Instance.AddComponentData<>(entity);
            //    }
            //}

            SendNotification(GameConsts.LOAD_SCENE_FINISH);
        }
    }
}
