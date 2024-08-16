using Cinemachine;
using DataBase;
using ECS;
using Game;
using MVC;
using PureMVC.Interfaces;
using PureMVC.Patterns.Command;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

namespace HotGame
{
    //public class LoadSceneData
    //{
    //    public string name;
    //    public LoadSceneMode mode;
    //    public LoadSceneData(string name, LoadSceneMode mode = LoadSceneMode.Single)
    //    {
    //        this.name = name;
    //        this.mode = mode;
    //    }
    //}
    public class LoadStage : SimpleCommand
    {
        public const string NAME = "LoadStage";
        public override async void Execute(INotification notification)
        {
            TableProxy tableProxy = Facade.RetrieveProxy(TableProxy.NAME) as TableProxy;
            StageProxy stageProxy = Facade.RetrieveProxy(StageProxy.NAME) as StageProxy;
            HeroProxy heroProxy = Facade.RetrieveProxy(HeroProxy.NAME) as HeroProxy;
            int currentStage = heroProxy.Stage;
            StageData stageData = tableProxy.GetData<StageData>(currentStage);



            if (stageData.mapName != null)
            {
                //加载地图
                var handle = Addressables.LoadSceneAsync(stageData.mapName, LoadSceneMode.Additive);

                ///异步执行
                
                //设置表现层数据
                StageVO vo = new StageVO();
                vo.state = StageState.Start;
                vo.id = currentStage;
                vo.npcs = stageData.npcs;
                stageProxy.SetData(vo);

                //设置entity组件数据
                Spawn spawn = new Spawn();
                spawn.interval = stageData.interval;
                spawn.duration = stageData.duration;
                spawn.state = SpawnState.Start;

                Stage stage = new Stage();
                stage.id = currentStage;
                stage.state = StageState.Start;

                //设置属性，切换装备，无需重新加载hero

                ///异步执行结束
                await handle.Task;

                if (handle.Status == AsyncOperationStatus.Succeeded)
                {
                    //网格化地图信息，或者加载地图的网格信息

                    //设置hero位置


                    //阻塞线程
                    //TaskManager.Delay(stageData.delay).Wait();
                    //Entity entity = EntityManager.Create();
                    //EntityManager.AddComponentData(entity, stage);
                    //EntityManager.AddComponentData(entity, spawn);


                    //不阻塞线程
                    TaskManager.Delay(stageData.delay, () =>
                    {
                        Entity entity = EntityManager.Create();
                        EntityManager.AddComponentData(entity, stage);
                        EntityManager.AddComponentData(entity, spawn);

                    });
                }
                else
                {
                    Debug.LogError("Failed to load scene.");
                }
            }
        }

    }
}
