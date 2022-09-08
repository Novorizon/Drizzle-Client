using ECS;
using Game.Input;
using MVC;
using MVC.Patterns;
using MVC.UI;
using UnityEngine.SceneManagement;

namespace Game
{
    public class GameEntry : ApplicationEntry
    {
        public InitializePanel staticPanel;

        protected override void OnLaunch()
        {
            SceneManager.sceneLoaded += SceneManager_sceneLoaded;
            //SceneManager.LoadSceneAsync(1, LoadSceneMode.Additive);

#if UNITY_EDITOR
            GameObjectPool.Instance.ExpiredTime = 60;
#else
            GameObjectPool.Instance.ExpiredTime = 180;
#endif
            GameInput.Enable();
            MainCamera.Init();
            UIManager.Instance.GetCamera(0);

            staticPanel.initEndCallback = OnStart;


            Facade.RegisterProxy(new TableProxy());
            Facade.RegisterCommand(RegisterTableCommand.NAME, () => new RegisterTableCommand());
            Facade.RegisterCommand(LoadSceneCommand.NAME, () => new LoadSceneCommand());

            SendNotification(RegistMediatorCommand.Name, staticPanel, StartupMediator.NAME);

        }

        private void SceneManager_sceneLoaded(Scene arg0, LoadSceneMode arg1)
        {
            SceneManager.SetActiveScene(arg0);
            SceneManager.sceneLoaded -= SceneManager_sceneLoaded;
        }

        public override void OnStart()
        {
            base.OnStart();

            SendNotification(RemoveMediatorCommand.Name, this, StartupMediator.NAME);


            GameInput.Controller.Default.Escape.started += (ctx) =>
            {
                UIManager.Instance.CloseWindowFromStack();
            };

            SendNotification(RegistMediatorCommand.Name, this, QuestMediator.NAME);

            //SendNotification(LoadHeroCommand.NAME);
            SendNotification(LoadNPCCommand.NAME);

            UIManager.Instance.OpenWindow(UIConfig.HUD);
        }

        protected override void OnStop()
        {
            GameInput.Disable();
            //Facade.SendNotification(GameConsts.PERSISTENT_CLEAR_CACHE);

            //Facade.RemoveMediator(StartupMediator.NAME);

            Facade.RemoveProxy(TableProxy.NAME);

            base.OnStop();
        }

        protected override void InitializeCommand()
        {
            Facade.RegisterCommand(LoadSceneCommand.NAME, () => new LoadSceneCommand());
            Facade.RegisterCommand(LoadHeroCommand.NAME, () => new LoadHeroCommand());
            Facade.RegisterCommand(LoadNPCCommand.NAME, () => new LoadNPCCommand());

            SendNotification(LoadHeroCommand.NAME);
        }

        protected override void InitializeProxy()
        {
            Facade.RegisterProxy(new NetProxy());
            Facade.RegisterProxy(new HandlerProxy());

            Facade.RegisterProxy(new CharacterProxy());
            Facade.RegisterProxy(new MessageProxy());
            Facade.RegisterProxy(new AuthProxy());

            Facade.RegisterProxy(new HeroProxy());
            Facade.RegisterProxy(new QuestProxy());
            NetProxy netProxy = Facade.RetrieveProxy(NetProxy.NAME) as NetProxy;
            netProxy.Start();
        }

       
    }
}
