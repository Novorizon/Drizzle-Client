using Cinemachine;
using ECS;
using MVC;
using PureMVC.Interfaces;
using PureMVC.Patterns.Command;
using System;
using System.Xml.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game
{
    public class LoadSceneData
    {
        public string name;
        public LoadSceneMode mode;
        public LoadSceneData(string name, LoadSceneMode mode = LoadSceneMode.Single)
        {
            this.name = name;
            this.mode = mode;
        }
    }
    public class LoadScene : SimpleCommand
    {
        public const string NAME = "LoadScene";
        public override void Execute(INotification notification)
        {
            Debug.LogError("Time.frameCount" + Time.frameCount);
            Debug.LogError("LoadScene");
            if (notification.Body != null)
            {
                LoadSceneData data = (LoadSceneData)notification.Body;
                string name = data.name;
                LoadSceneMode mode = data.mode;
                if (name != null)
                {
                    ResourceManager.Instance.LoadSceneAsync(name, OnSceneLoaded, mode, true, null);
                }
            }
            else
                Debug.LogError("notification is null");
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
