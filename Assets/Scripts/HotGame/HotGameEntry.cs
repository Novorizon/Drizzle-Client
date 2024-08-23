using MVC;
using PureMVC.Interfaces;
using PureMVC.Patterns.Facade;
using System.Threading.Tasks;
using UnityEngine;
using MVC.UI;
using UnityEngine.SceneManagement;
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

        public async void OnLaunch()
        {
            //Assembly assembly = Assembly.GetExecutingAssembly();
            //Console.WriteLine(assembly.FullName);
            //Debug.LogError("HotGame " + assembly.CodeBase);

            //编辑器可运行，匿名类型，class，ValueTuple，Tuple
            //打包正常运行，class
            //编辑器不可运行，struct notification.Body无法强转成struct
            //LoadSceneData data = new LoadSceneData("map_1001", LoadSceneMode.Additive);
            //SendNotification(LoadSceneCommand.NAME, data);


            SendNotification(RegisterTable.NAME);

            //异步读表和加载场景
            TableProxy tableProxy = Facade.RetrieveProxy(TableProxy.NAME) as TableProxy;
            Task loadTable = tableProxy.LoadAsync();
            Task<Scene> loadScene = ResourceManager.Instance.LoadSceneAsync("map_1001", LoadSceneMode.Additive);
            await Task.WhenAll(loadTable, loadScene);

            if(loadTable.IsCompleted && loadScene.Result.isLoaded)
            {
                SendNotification(LoadHero.NAME);
                UIManager.Instance.OpenWindow(UIConfig.HUD);
            }

            //ResourceManager.Instance.LoadSceneAsync("map_1001", OnSceneLoaded, LoadSceneMode.Additive, true, null);
        }
        protected void Initialize()
        {
            InitializeProxy();
            InitializeCommand();
            InitializeMediator();
        }

        protected void InitializeCommand()
        {
            Facade.RegisterCommand(RegisterTable.NAME, () => new RegisterTable());
            Facade.RegisterCommand(LoadScene.NAME, () => new LoadScene());

            Facade.RegisterCommand(LoadHero.NAME, () => new LoadHero());
            Facade.RegisterCommand(LoadWeapon.NAME, () => new LoadWeapon());

        }

        protected void InitializeMediator()
        {

        }

        protected void InitializeProxy()
        {
            Facade.RegisterProxy(new HeroProxy());
            Facade.RegisterProxy(new ArchetypeProxy());

            Facade.RegisterProxy(new QuestProxy());
            Facade.RegisterProxy(new WeaponProxy());

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
