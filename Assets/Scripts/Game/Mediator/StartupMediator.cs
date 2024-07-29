using DataBase;
using MVC;
using MVC.Providers;
using PureMVC.Interfaces;
using PureMVC.Patterns.Mediator;
using System;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using Object = UnityEngine.Object;

namespace Game
{
    public class StartupMediator : Mediator
    {
        public new static string NAME = typeof(StartupMediator).FullName;

        public const string RESOURCE_UPDATE = "RESOURCE_UPDATE";

        private InitializePanel staticPanel;

        public StartupMediator(object viewComponent) : base(NAME, viewComponent)
        {
        }

        public override void OnRegister()
        {
            staticPanel = (InitializePanel)ViewComponent;
            SendNotification(GameConsts.StartupMediatorRegistered);
        }

        public override void OnRemove()
        {
            staticPanel = null;
        }

        public override string[] ListNotificationInterests()
        {
            return new string[]
            {
                GameConsts.StartupMediatorRegistered,
                GameConsts.LOAD_DB_FINISH,
                GameConsts.LOAD_TABLE_FINISH,
                GameConsts.LOAD_SCENE_FINISH,

                GameConsts.CMD_GAME_START,
            };
        }

        public override void HandleNotification(INotification notification)
        {
            switch (notification.Name)
            {

                case GameConsts.StartupMediatorRegistered:
                    //SendNotification(LoadTableCommand.NAME);
                    break;

                case GameConsts.LOAD_TABLE_FINISH:
                    //SendNotification(LoadSceneCommand.NAME, new { name = "map_1001", mode = LoadSceneMode.Additive });
                    break;

                case GameConsts.LOAD_SCENE_FINISH:
                    staticPanel.OnInitializedEnd();
                    break;

                case GameConsts.CMD_GAME_START:

                    break;
            }
        }


        private void UpdateProgress(string tips, float progress)
        {
            if (staticPanel != null)
            {
                staticPanel.SetProgress(tips, progress);
            }
        }

        private void ShowErrorPopWindow(string errorMsg, string btnOk, string btnCancel)
        {
            if (staticPanel != null)
            {
                staticPanel.Pop(errorMsg, () =>
                {
                    //ccProxy.StartConnect();
                },
                btnOk, () =>
                {
#if UNITY_EDITOR
                    UnityEditor.EditorApplication.isPlaying = false;
#else
                    Application.Quit();
#endif
                }, btnCancel);
            }
        }



    }
}
