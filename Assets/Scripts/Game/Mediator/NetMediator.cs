using ECS;
using Game;
using MVC.UI;
using PureMVC.Interfaces;
using PureMVC.Patterns.Mediator;
using UnityEngine;

namespace Game
{
    public class NetMediator : Mediator
    {
        new public static string NAME = typeof(NetMediator).FullName;

        public NetMediator(object viewComponent) : base(NAME, viewComponent) { }

        public override void OnRegister()
        {
            base.OnRegister();
        }

        public override string[] ListNotificationInterests()
        {
            return new string[]
            {
                GameConsts.NET_CONNECT,
                GameConsts.NET_CONNECTED,
                GameConsts.NET_RECONNECT,
                GameConsts.NET_DISCONNECT,

                GameConsts.AUTH_LOGIN,
            };
        }

        public override void HandleNotification(INotification notification)
        {
            //QuestState state =(QuestState) notification.Body;
            switch (notification.Name)
            {
                case GameConsts.NET_CONNECT:
                    NetProxy netProxy = Facade.RetrieveProxy(NetProxy.NAME) as NetProxy;
                    netProxy.Start();
                    break;

                case GameConsts.NET_CONNECTED:
                    AuthProxy authProxy = Facade.RetrieveProxy(AuthProxy.NAME) as AuthProxy;
                    authProxy.Login();
                    break;
            }
        }
    }
}