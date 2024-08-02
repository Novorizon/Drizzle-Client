using MVC;
using MVC.Patterns;
using PureMVC.Interfaces;
using PureMVC.Patterns.Facade;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using UnityEngine.AddressableAssets.ResourceLocators;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine;
using Game.Input;
using MVC.UI;
using UnityEngine.SceneManagement;
using HybridCLR;
using System.IO;
using DataBase;
using Unity.Mathematics;
using Game;

namespace HotGame
{
    public class HotGameEntry : MonoBehaviour, INotifier
    {
        public void SendNotification(string notificationName, object body = null, string type = null)
        {
            Facade.SendNotification(notificationName, body, type);
        }

        /// <summary>Return the Singleton Facade instance</summary>
        protected IFacade Facade => PureMVC.Patterns.Facade.Facade.GetInstance(() => new Facade());

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }
        private void Start()
        {
            Launch();

        }
        private void Update()
        {
            //Debug.LogError("Update");

        }

        private void LateUpdate()
        {
            //Debug.LogError("LateUpdate");

        }

        private void OnDestroy()
        {
            //Debug.LogError("OnDestroy");

        }

        //不能是异步函数
        protected void Launch()
        {
            //Debug.LogError("Launch15");

            Initialize();

            //AsyncOperationHandle<IResourceLocator> handle = ResourceManager.Instance.Initialize();
            //Task task = Task.Run(() => Initialize());
            //await Task.WhenAll(task);
            //await Task.CompletedTask;

            OnLaunch();
        }

        public void OnLaunch()
        {

            //Debug.LogError("OnLaunch");

            //编辑器可运行，匿名类型，class，ValueTuple，Tuple
            //打包正常运行，class
            //编辑器不可运行，struct notification.Body无法强转成struct
            //LoadSceneData data = new LoadSceneData("map_1001", LoadSceneMode.Additive);
            //SendNotification(LoadSceneCommand.NAME, data);

            SendNotification(LoadTable.NAME);
            SendNotification(LoadHero.NAME);
            Quest quest=new Quest();
            quest.id = 0;
            //Debug.LogError(quest.id);

            ResourceManager.Instance.LoadSceneAsync("map_1001", OnSceneLoaded, LoadSceneMode.Additive, true, null);
            UIManager.Instance.OpenWindow(UIConfig.HUD);
        }
        protected void Initialize()
        {
            InitializeProxy();
            InitializeCommand();
            InitializeMediator();
        }

        protected void InitializeCommand()
        {

            Facade.RegisterCommand(LoadTable.NAME, () => new LoadTable());
            Facade.RegisterCommand(LoadScene.NAME, () => new LoadScene());

            Facade.RegisterCommand(LoadHero.NAME, () => new LoadHero());
            Facade.RegisterCommand(LoadWeapon.NAME, () => new LoadWeapon());

        }

        protected void InitializeMediator()
        {

        }

        protected void InitializeProxy()
        {
            Facade.RegisterProxy(new ArchetypeProxy());


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
        }
    }

}
