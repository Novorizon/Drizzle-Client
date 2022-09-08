using Cinemachine;
using DataBase;
using ECS;
using MVC;
using PureMVC.Interfaces;
using PureMVC.Patterns.Command;
using UnityEngine;

namespace Game
{
    public class LoadHeroCommand : SimpleCommand
    {
        public const string NAME = "LoadHeroCommand";
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

            EntityArchetype entityArchetype = new EntityArchetype(
                typeof(LocalToWorld),
                typeof(Position),
                typeof(Rotation),
                typeof(Scale),
                typeof(CopyTransformFromGameObject),
                typeof(Speed),
                typeof(MoveDirection),
                typeof(FaceDirection),
                typeof(PlayerController)
                );

            Entity hero = EntityManager.Create();

            EntityManager.AddComponentData<CopyInitialTransformFromGameObject>(hero);
            EntityManager.AddComponentData<CopyTransformToGameObject>(hero);
            EntityManager.AddComponentData<LocalToWorld>(hero);
            EntityManager.AddComponentData<Position>(hero).Value = data.position;
            EntityManager.AddComponentData<Rotation>(hero).Value = Quaternion.identity;
            EntityManager.AddComponentData<Scale>(hero).Value = Vector3.one;

            EntityManager.AddComponentData<Speed>(hero).Value = data.speed * 10;
            EntityManager.AddComponentData<MoveDirection>(hero).Value = Vector3.zero;
            EntityManager.AddComponentData<FaceDirection>(hero).Value = Vector3.forward;// data.forward;

            EntityManager.AddComponentData<PlayerController>(hero);
            //Debug.LogError(asset.name);


            EntityManager.LoadGameObject(hero, model.name, data.position, Quaternion.identity, (e, _) =>
            {
                //EntityManager.AddComponentData<PlayerController>(hero);

                var VirtualCamera = GameObject.FindObjectOfType<CinemachineVirtualCamera>();
                if (VirtualCamera != null)
                {
                    VirtualCamera.Follow = e.gameObject.transform;
                    VirtualCamera.LookAt = e.gameObject.transform;
                }
            });

                //SendNotification();//×°±¸




        }
    }
}
