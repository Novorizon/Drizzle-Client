//using Ability;
using ECS;
using Game.Input;
using MVC;
using MVC.Patterns;
using MVC.UI;
using PureMVC.Patterns.Facade;
using UnityEngine;
//using UnityEngine.AddressableAssets.ResourceLocators;
using UnityEngine.SceneManagement;

namespace Game
{
    public class HotGameEntry1 : ApplicationEntry
    {
        public InitializePanel staticPanel;
        public bool useServer = false;
        public bool usePrefab = true;

        protected override void Launch()
        {
            //1
            base.Launch();
            //4

            //begin 与基类异步同时进行
            GameInput.Enable();
            MainCamera.Init();
            UIManager.Instance.GetCamera(0);
            SendNotification(RegistMediatorCommand.Name, staticPanel, StartupMediator.NAME);

#if UNITY_EDITOR
            GameObjectPool.Instance.ExpiredTime = 60;
#else
            GameObjectPool.Instance.ExpiredTime = 180;
#endif
            //end
            Debug.LogError("Launch");
        }

        public override void OnLaunch()
        {
            //6
            //GameInput.Controller.Default.Escape.started += (ctx) => { UIManager.Instance.CloseWindowFromStack(); };
            SendNotification(LoadHero.NAME);
            SendNotification(RemoveMediatorCommand.Name, this, StartupMediator.NAME);

            //加载prefab调用热更函数
            //if (usePrefab)
            //{
            //    ResourceManager.Instance.LoadAssetAsync<GameObject>("HotGameEntry", (asset, _) =>
            //    {
            //        GameObject go = Instantiate(asset);
            //    });
            //}
            //else
            //{
            //    //HotUpdateManager.Instance.Initialize();
            //}

            UIManager.Instance.OpenWindow(UIConfig.HUD);
        }

        protected override void OnQuit()
        {
            GameInput.Disable();

            Facade.RemoveProxy(TableProxy.NAME);

            base.OnQuit();
        }

        protected override void InitializeCommand()
        {
            Facade.RegisterCommand(RegistMediatorCommand.Name, () => new RegistMediatorCommand());
            Facade.RegisterCommand(RemoveMediatorCommand.Name, () => new RemoveMediatorCommand());

            Facade.RegisterCommand(LoadTable.NAME, () => new LoadTable());
            Facade.RegisterCommand(LoadScene.NAME, () => new LoadScene());

            Facade.RegisterCommand(LoadHero.NAME, () => new LoadHero());
            //Facade.RegisterCommand(LoadNPCCommand.NAME, () => new LoadNPCCommand());

        }

        protected override void InitializeMediator()
        {
            //Facade.RegisterMediator(new AbilityMediator(null));
            //SendNotification(RegistMediatorCommand.Name, this, QuestMediator.NAME);

        }

        protected override void InitializeProxy()
        {

            Facade.RegisterProxy(new TableProxy());
            Facade.RegisterProxy(new HandlerProxy());


            Facade.RegisterProxy(new CharacterProxy());
            Facade.RegisterProxy(new MessageProxy());
            Facade.RegisterProxy(new AuthProxy());

            Facade.RegisterProxy(new HeroProxy());
            Facade.RegisterProxy(new QuestProxy());


            //Facade.RegisterProxy(new AbilityProxy());
            //Facade.RegisterProxy(new EffectProxy());

        }



    }
}
