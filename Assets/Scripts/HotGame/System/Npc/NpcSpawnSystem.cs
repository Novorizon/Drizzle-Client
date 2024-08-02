using DataBase;
using ECS;
using Game;
using PureMVC.Interfaces;
using PureMVC.Patterns.Facade;
using System;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
namespace HotGame
{
    public class NpcSpawnSystem : SystemBase< NPC, Spawn>
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


        protected override void OnUpdate(int index, Entity entity, NPC npc, Spawn spawn)
        {
            HeroProxy proxy = Facade.RetrieveProxy(HeroProxy.NAME) as HeroProxy;
            if (proxy == null)
                return;

            int currentStage = proxy.Stage;
            QuestProxy questProxy = Facade.RetrieveProxy(QuestProxy.NAME) as QuestProxy;
            if (questProxy == null)
                return;

            //获得NPC数据 模型 出生位置


            //获取NPC技能

            //EntityArchetypeProxy proxy = Facade.RetrieveProxy(EntityArchetypeProxy.NAME);
            //EntityArchetype archetype = proxy.GetData();
            EntityArchetype archetype = default;
            //创建NPC
          
        }

    }
}