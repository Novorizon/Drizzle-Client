using Game;
using MVC;
using MVC.Patterns;
using PureMVC.Interfaces;
using PureMVC.Patterns.Facade;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets.ResourceLocators;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.SceneManagement;

namespace HotGame
{
    public class HotGame
    {
        public static void SendNotification(string notificationName, object body = null, string type = null)
        {
            Facade.SendNotification(notificationName, body, type);
        }

        private static IFacade Facade => PureMVC.Patterns.Facade.Facade.GetInstance(() => new PureMVC.Patterns.Facade.Facade());

        //不能是异步函数
        public static  void Launch()
        {
            Debug.LogError("Launch");
            Initialize();
            OnLaunch();
        }

        public static void Update(float deltaTime)
        {
            //Debug.LogError("Update");
        }


        public static void LateUpdate(float deltaTime)
        {
            //Debug.LogError("LateUpdate");
        }

        public static void Quit()
        {
            //Debug.LogError("Quit");
        }

        public static Action<float> GetUpdateDelegate()
        {
            return Update;
        }

        public static Action<float> GetLateUpdateDelegate()
        {
            return LateUpdate;
        }


        public static Action GetLaunchDelegate()
        {
            return Launch;
        }

        public static Action GetQuitDelegate()
        {
            return Quit;
        }


        public static void OnLaunch()
        {
            SendNotification(LoadTable.NAME);
            ResourceManager.Instance.LoadSceneAsync("map_1001", OnSceneLoaded, LoadSceneMode.Additive, true, null);
            //SendNotification(LoadScene.NAME, new { name = "map_1001", mode = LoadSceneMode.Additive });
            SendNotification(LoadHero.NAME);
        }

        static protected  void Initialize()
        {
            InitializeProxy();
            InitializeCommand();
            InitializeMediator();
        }

        static protected  void InitializeCommand()
        {


        }

        static protected  void InitializeMediator()
        {

        }

        static protected  void InitializeProxy()
        {
        }

        static public void OnSceneLoaded(Scene scene, object userdata)
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
