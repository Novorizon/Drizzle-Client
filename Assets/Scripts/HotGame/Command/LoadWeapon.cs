using DataBase;
using ECS;
using Game;
using MVC;
using PureMVC.Interfaces;
using PureMVC.Patterns.Command;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace HotGame
{
    public class LoadWeaponData
    {
        public int id;
        public GameObject dummy;
        public string description;
    }
    public class LoadWeapon : SimpleCommand
    {
        public const string NAME = "LoadWeapon";
        public override void Execute(INotification notification)
        {
            if (notification.Body == null)
                return;

            LoadWeaponData data = (LoadWeaponData)notification.Body;
            int id = data.id;
            GameObject dummy = data.dummy;

            if (id == 0)
                return;

            TableProxy tableProxy = Facade.RetrieveProxy(TableProxy.NAME) as TableProxy;
            WeaponData weaponData = tableProxy.GetData<WeaponData>(id);


            if (weaponData == null)
                return;

            Entity entity = EntityManager.Create();

            Weapon weapon = EntityManager.AddComponentData<Weapon>(entity);
            weapon.id = weaponData.id;
            weapon.trackCount = weaponData.trackCount;
            weapon.id = weaponData.id;
            weapon.id = weaponData.id;

            Spawn spawn = EntityManager.AddComponentData<Spawn>(entity);
            spawn.interval = weaponData.interval;
            spawn.duration =-1;
            spawn.state =  SpawnState.Start;

            ResourceManager.Instance.LoadAssetAsync<GameObject>(weaponData.asset, OnLoaded, data.dummy);
        }

        private void OnLoaded(GameObject asset, object userdata)
        {
            GameObject dummy = userdata as GameObject;
            asset.transform.parent = dummy.transform;
            //SendNotification(GameConsts.LOAD_TABLE_FINISH);
        }
    }
}
