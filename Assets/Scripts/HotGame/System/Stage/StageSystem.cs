using DataBase;
using ECS;
using Game;
using PureMVC.Interfaces;
using PureMVC.Patterns.Facade;
using System.Collections.Generic;
using UnityEngine;
namespace HotGame
{
    public class StageSystem : SystemBase<Stage>
    {
        //public void SendNotification(string notificationName, object body = null, string type = null)
        //{
        //    Facade.SendNotification(notificationName, body, type);
        //}

        protected IFacade Facade
        {
            get
            {
                return PureMVC.Patterns.Facade.Facade.GetInstance(() => new PureMVC.Patterns.Facade.Facade());
            }
        }


        protected override void OnUpdate(int index, Entity entity, Stage stage)
        {
            ItemProxy proxy = Facade.RetrieveProxy(ItemProxy.NAME) as ItemProxy;
            if (proxy == null)
                return;
            QuestProxy questProxy = Facade.RetrieveProxy(QuestProxy.NAME) as QuestProxy;
            if (questProxy == null)
                return;

            if (stage.state == StageState.Start)
            { 
                stage.state = StageState.Finish;
                QuestVO questVO = questProxy.GetData(stage.id);
                questVO.state = QuestState.Finish;

                //SendNotification(HudMediator.NAME, stage.state);//通知任务完成 更新 
            }
            else if (stage.state == StageState.Finish)
            {

            }
            else if (stage.state == StageState.Abort)
            {
                //EntityManager.DestroyEntity(entity);

            }
            else if (stage.state == StageState.Reward)
            {
                //EntityManager.DestroyEntity(entity);
            }
        }

    }
}