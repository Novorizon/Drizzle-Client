using DataBase;
using ECS;
using Game;
using MVC;
using PureMVC.Interfaces;
using PureMVC.Patterns.Facade;
using System;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
namespace HotGame
{
    public class NpcSpawnSystem : SystemBase<Stage, Spawn>
    {
        //public void SendNotification(string notificationName, object body = null, string type = null)
        //{
        //    Facade.SendNotification(notificationName, body, type);
        //}

        protected IFacade Facade { get { return PureMVC.Patterns.Facade.Facade.GetInstance(() => new Facade()); } }

        TableProxy tableProxy;
        HeroProxy heroProxy;
        StageProxy stageProxy;
        NpcProxy npcProxy;

        protected override void OnStartRunning()
        {
            tableProxy = Facade.RetrieveProxy(TableProxy.NAME) as TableProxy;
            heroProxy = Facade.RetrieveProxy(HeroProxy.NAME) as HeroProxy;
            stageProxy = Facade.RetrieveProxy(StageProxy.NAME) as StageProxy;
            npcProxy = Facade.RetrieveProxy(NpcProxy.NAME) as NpcProxy;
        }

        protected override void OnUpdate(int index, Entity entity, Stage stage, Spawn spawn)
        {
            if (spawn.state == SpawnState.Start)
            {
                if (tableProxy == null)
                    return;

                if (heroProxy == null)
                    return;


                if (stageProxy == null)
                    return;

                //获得NPC数据 数量、模型 、出生位置 出生密度
                int currentStage = heroProxy.Stage;
                StageVO vo = stageProxy.GetData(currentStage);

                //for(int i=0;i< vo.npcs.Count;i++)
                //{
                //    NPCData npcData = tableProxy.GetData<NPCData>(vo.npcs[i]);
                //    if (npcData == null)
                //        continue;
                //}

                ///先按照单个npc做
                int npcId = vo.npcs[0];
                NPCData data = tableProxy.GetData<NPCData>(npcId);
                NpcVO voNpc = npcProxy.GetData(npcId);

                //获取NPC技能 TODO

                //获得NPC原型
                ArchetypeProxy proxy = Facade.RetrieveProxy(ArchetypeProxy.NAME) as ArchetypeProxy;
                EntityArchetype archetype = proxy.GetData(ArchetypeProxy.Archetype.NPC);


                //通过原型创建NPC
                GameObject gameObject = GameObjectPool.Spawn(voNpc.asset);
                Entity npc = EntityManager.Create(gameObject, archetype);

                //设置NPC组件

                //加载武器
                GameObject dummy = gameObject.gameObject;
                LoadWeaponData loadWeaponData = new LoadWeaponData();
                loadWeaponData.id = 1001;
                loadWeaponData.dummy = dummy;
                SendNotification(LoadWeapon.NAME, loadWeaponData);

            }
        }

    }
}