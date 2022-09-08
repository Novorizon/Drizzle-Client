using DataBase;
using ECS;
using PureMVC.Interfaces;
using PureMVC.Patterns.Facade;
using System.Collections.Generic;
using UnityEngine;
namespace Game
{
    public class QuestSystem : SystemBase<Quest>
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


        protected override void OnUpdate(int index, Entity entity, Quest quest)
        {
            ItemProxy proxy = Facade.RetrieveProxy(ItemProxy.NAME) as ItemProxy;
            if (proxy == null)
                return;
            QuestProxy questProxy = Facade.RetrieveProxy(QuestProxy.NAME) as QuestProxy;
            if (questProxy == null)
                return;

            if (quest.state == QuestState.Start)
            {
                List<ItemUnit> items = quest.items;
                for (int i = 0; i < items.Count; i++)
                {
                    ItemUnit item = items[i];
                    ItemVO vo = proxy.GetData(item.id);
                    if (vo != null)
                    {
                        if (vo.count < item.count)
                        {
                            return;
                        }
                    }
                }

                quest.state = QuestState.Finish;
                QuestVO questVO = questProxy.GetData(quest.id);
                questVO.state = QuestState.Finish;

                SendNotification(HudMediator.NAME, quest.state);//通知任务完成 更新 
            }
            else if (quest.state == QuestState.Finish)
            {

            }
            else if (quest.state == QuestState.Abort)
            {
                EntityManager.DestroyEntity(entity);

            }
            else if (quest.state == QuestState.Reward)
            {
                EntityManager.DestroyEntity(entity);
            }
        }

    }
}