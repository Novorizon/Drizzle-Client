//using Ability;
using Game.Input;
using MVC;
using MVC.Patterns;
using MVC.UI;
using UnityEngine;

namespace Game
{
    public class GameEntry : ApplicationEntry
    {
        public InitializePanel staticPanel;
        public bool isAot= false;

        protected override void Launch()
        {
            //1
            base.Launch();
            //4

            //begin 与基类异步同时进行
            GameInput.Enable();
            MainCamera.Init();
            UIManager.Instance.Initialize();

            SendNotification(RegistMediatorCommand.Name, staticPanel, StartupMediator.NAME);

#if UNITY_EDITOR
            GameObjectPool.Instance.ExpiredTime = 60;
#else
            GameObjectPool.Instance.ExpiredTime = 180;
#endif
            //end
        }

        public override void OnLaunch()
        {
            SendNotification(RemoveMediatorCommand.Name, this, StartupMediator.NAME);

            GameInput.Controller.Default.Escape.started += (ctx) => { UIManager.Instance.CloseWindowFromStack(); };


            //6
            if(isAot)
            {
                #region 加载table、Scene、Hero等，如果热更，不再在这里执行
                //数据
                //SendNotification(LoadTableCommand.NAME);

                //资源
                //SendNotification(LoadSceneCommand.NAME, new { name = "map_1001", mode = LoadSceneMode.Additive });
                //SendNotification(LoadHeroCommand.NAME);

                //UIManager.Instance.OpenWindow(UIConfig.HUD);
                #endregion
            }
            else
            {
                //测试 HybridCLR热更
                HotUpdateManager.Instance.Initialize();//使用热更管理器，两种途径，1直接创建Prefab来执行Entry脚本，2反射和委托（不要对update使用invoke）
            }


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

            Facade.RegisterCommand(LoadTableCommand.NAME, () => new LoadTableCommand());
            Facade.RegisterCommand(LoadSceneCommand.NAME, () => new LoadSceneCommand());

            //Facade.RegisterCommand(LoadHeroCommand.NAME, () => new LoadHeroCommand());

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



            //Facade.RegisterProxy(new AbilityProxy());
            //Facade.RegisterProxy(new EffectProxy());

        }



    }
}
