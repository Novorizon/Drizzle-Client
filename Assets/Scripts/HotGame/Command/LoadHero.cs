
using Cinemachine;
using DataBase;
using ECS;
using Game;
using MVC;
using PureMVC.Interfaces;
using PureMVC.Patterns.Command;
using UnityEngine;

namespace HotGame
{
    public class LoadHero : SimpleCommand
    {
        public const string NAME = "LoadHero";
        public override void Execute(INotification notification)
        {
            LoadProfile();
        }

        public void LoadProfile()
        {
            TableProxy tableProxy = Facade.RetrieveProxy(TableProxy.NAME) as TableProxy;
            if (tableProxy == null)
                return;

            HeroProxy proxy = Facade.RetrieveProxy(HeroProxy.NAME) as HeroProxy;
            if (proxy == null)
                return;
            proxy.GetPrefs();

            QuestProxy questProxy = Facade.RetrieveProxy(QuestProxy.NAME) as QuestProxy;
            if (questProxy == null)
                return;

            int isCreated = PlayerPrefs.GetInt("CharacterCreated", 0);
            if (isCreated == 0)
            {
                DefaultData defaultData = tableProxy.GetData<DefaultData>(10001);
                if (defaultData == null)
                    return;
                proxy.SetData(defaultData);
                //questProxy.SetData(defaultData);
            }

            HeroVO data = proxy.GetData();
            if (data == null)
                return;

            ModelData model = tableProxy.GetData<ModelData>(data.modelId);
            if (model == null)
                return;

            ArchetypeProxy archetypeProxy = Facade.RetrieveProxy(ArchetypeProxy.NAME) as ArchetypeProxy;
            EntityArchetype archetype = archetypeProxy.GetData(ArchetypeProxy.Archetype.Hero);

            Entity hero = EntityManager.Create(archetype);

            EntityManager.GetComponentData<Position>(hero).Value = data.position;
            EntityManager.GetComponentData<Rotation>(hero).Value = Quaternion.identity;
            EntityManager.GetComponentData<Scale>(hero).Value = Vector3.one;

            EntityManager.GetComponentData<Speed>(hero).Value = data.speed * 10;
            EntityManager.GetComponentData<MoveDirection>(hero).Value = Vector3.zero;
            EntityManager.GetComponentData<FaceDirection>(hero).Value = Vector3.forward;// data.forward;
                                                                                        //Debug.LogError(asset.name);

           

            EntityManager.LoadGameObject(hero, model.name, data.position, Quaternion.identity, (e, _) =>
            {
                var VirtualCamera = GameObject.FindObjectOfType<CinemachineVirtualCamera>();
                if (VirtualCamera != null)
                {
                    VirtualCamera.Follow = e.gameObject.transform;
                    VirtualCamera.LookAt = e.gameObject.transform;
                }

                //装备武器 武器可以单独的一个entity，不需给Hero绑一个组件，如果某system的逻辑需要筛选带武器的单位，再给单位加组件
                //GameObject dummy=GetDummy(e.gameObject);
                GameObject dummy = e.gameObject;
                LoadWeaponData loadWeaponData = new LoadWeaponData();
                loadWeaponData.id = 1001;
                loadWeaponData.dummy = dummy;
                SendNotification(LoadWeapon.NAME, loadWeaponData);
            });


            data.entity = hero;
            data.guid = hero.GUID;

            //Entity entity = EntityManager.Create();
            //EntityManager.AddComponentData<Ability.AbilityComponent>(entity);

            //Entity buff = EntityManager.Create();
            //EntityManager.AddComponentData<Ability.BuffComponent>(buff);
        }
    }
}
