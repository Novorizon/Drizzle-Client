//using Ability;
using ECS;
using Game.Input;
using MVC;
using MVC.Patterns;
using MVC.UI;
using PureMVC.Patterns.Facade;
using UnityEngine;
using UnityEngine.AddressableAssets.ResourceLocators;
using UnityEngine.SceneManagement;

namespace Game
{
    public class Login : ApplicationEntry
    {
        public InitializePanel staticPanel;
        public bool useServer = false;

        protected override void Launch()
        {
            Facade.RegisterProxy(new HandlerProxy());
            Facade.RegisterProxy(new NetProxy());
            NetProxy netProxy = Facade.RetrieveProxy(NetProxy.NAME) as NetProxy;
            netProxy.Start();

            //µÇÂ¼Ãæ°å»Øµ÷
            if (staticPanel)
            {
                staticPanel.initEndCallback = OnLaunch;
            }
        }

        public override void OnLaunch()
        {
            Debug.LogError("GameEntry OnLaunch");
            base.OnLaunch();

            SendNotification(RemoveMediatorCommand.Name, this, StartupMediator.NAME);


        }

        public void OnLoginSuccess()
        {
            Debug.LogError("GameEntry OnLaunch");
            base.OnLaunch();

            SendNotification(RemoveMediatorCommand.Name, this, StartupMediator.NAME);


        }
        public void OnLoginFailed()
        {
            Debug.LogError("GameEntry OnLaunch");
            base.OnLaunch();

            SendNotification(RemoveMediatorCommand.Name, this, StartupMediator.NAME);


        }

        protected override void OnQuit()
        {
            GameInput.Disable();

            Facade.RemoveProxy(TableProxy.NAME);

            base.OnQuit();
        }

    }
}
