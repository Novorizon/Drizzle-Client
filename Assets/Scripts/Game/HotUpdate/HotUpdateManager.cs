using ECS;
using HybridCLR;
using MVC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using UnityEngine;

namespace Game
{
    public class HotUpdateManager : SingletonBehaviour<HotUpdateManager>
    {
        private static readonly bool useReflection = false;
        private const string hotAssemblyName = "HotGame";
        #region 反射需要用到的变量
        private const string hotClassName = "Game.HotGame";

        private static readonly Dictionary<int, Type> types = new Dictionary<int, Type>();
        private static Assembly assembly;
        private static Action<float> UpdateHot;
        private static Action<float> LateUpdateHot;
        private static Action LaunchHot;
        private static Action QuitHot;
        #endregion

        private static List<string> AotMetaAssemblyFiles { get; } = new List<string>()
        {
            "mscorlib.dll",
            "System.dll",
            "System.Core.dll",
        };

        public async void Initialize(System.Action callback = null)
        {
            //元数据和热更程序集，数量少的时候，加载使用异步并不比同步性能更好。如果热更程序集更多体积更大，可使用异步
            Task taskLoadMeta = LoadMetaDataAsync();
            Task<Assembly> loadAssembly = GetAssembly(hotAssemblyName);

            //与异步过程没有时序问题的同步过程，可以在此处

            await Task.WhenAll(taskLoadMeta, loadAssembly);

            //assembly = await GetAssembly(hotAssemblyName);
            assembly = loadAssembly.Result;
            if (assembly == null)
                return;

            //Debug.LogError("Game " + assembly.CodeBase);

            //ECS支持
            World.RefreshDefaultWorld(assembly);
            //TypeManager.Initialize();
            //ReferencePool.LoadType(assembly);
            //TypeManager.AddComponentTypes(assembly);
            //DefaultWorldInitialization.AddSystems(assembly);


            //热更新   1 反射 2 启动脚本  。推荐2
            if (useReflection)//创建反射来加载热更新场景
            {
                Type hotClass = assembly.GetType(hotClassName);
                MethodInfo getUpdateDelegate = hotClass.GetMethod("GetUpdateDelegate");
                UpdateHot = (Action<float>)getUpdateDelegate.Invoke(null, null);

                MethodInfo getLateUpdateDelegate = hotClass.GetMethod("GetLateUpdateDelegate");
                LateUpdateHot = (Action<float>)getLateUpdateDelegate.Invoke(null, null);

                MethodInfo launchDelegate = hotClass.GetMethod("GetLaunchDelegate");
                LaunchHot = (Action)launchDelegate.Invoke(null, null);
                LaunchHot?.Invoke();

                MethodInfo quitDelegate = hotClass.GetMethod("GetQuitDelegate");
                QuitHot = (Action)quitDelegate.Invoke(null, null);
            }
            else
            {
                //ResourceManager.Instance.LoadAssetAsync<GameObject>("HotGameEntry", (asset, _) =>
                //{
                //    if (asset)
                //    {
                //        GameObject.Instantiate(asset);
                //    }
                //});

                //这样写后面的只能等异步执行完了才执行
                //GameObject asset = await ResourceManager.Instance.LoadAssetAsync<GameObject>("HotGameEntry");


                Task<GameObject> task = ResourceManager.Instance.LoadAssetAsync<GameObject>("HotGameEntry");
                // DoSomethingElse();
                GameObject asset = await task;

                if (asset)
                {
                    GameObject.Instantiate(asset);
                }
            }


            callback?.Invoke();

        }


        #region 反射需要用到的函数
        private void Update()
        {
            if (UpdateHot == null)
                return;

            UpdateHot(Time.deltaTime);
        }


        private void LateUpdate()
        {
            if (LateUpdateHot == null)
                return;

            LateUpdateHot(Time.fixedDeltaTime);
        }

        private void OnDestroy()
        {
            QuitHot?.Invoke();
        }
        #endregion


        protected override void OnInitialized()
        {
        }

        //热更新程序集一定要和aot一样，编辑器是ScriptAssemblies，运行时streamingAssets
        public async Task<Assembly> GetAssembly(string assemblyName)
        {
#if UNITY_EDITOR
            await Task.CompletedTask;
            Assembly assembly = System.AppDomain.CurrentDomain.GetAssemblies().First(a => a.GetName().Name == assemblyName);
#else
            //Assembly assembly = Assembly.Load(File.ReadAllBytes($"{Application.streamingAssetsPath}/" + assemblyName + ".dll.bytes"));
            byte[] bytes = await ResourceManager.Instance.GetStreamingAssets(assemblyName + ".dll.bytes");
            Assembly assembly = Assembly.Load(bytes);
#endif
            return assembly;
        }


        public Type GetType(string typeName)
        {
            if (assembly == null || string.IsNullOrEmpty(typeName))
            {
                return null;
            }

            Type type = assembly.GetType(typeName);

            return type;
        }

        public void Invoke(Type type, string methodName, params object[] args)
        {
            if (type == null || string.IsNullOrEmpty(methodName))
            {
                return;
            }

            MethodInfo method = type.GetMethod(methodName);
            method.Invoke(type, args);
        }

        private async Task LoadMetaDataForAssemblyAsync(string assemblyName, HomologousImageMode mode)
        {
            byte[] bytes = await ResourceManager.Instance.GetStreamingAssets(assemblyName + ".bytes");

            // 加载 assembly 对应的 dll，会自动为它 hook。一旦 aot 泛型函数的 native 函数不存在，用解释器版本代码
            LoadImageErrorCode err = RuntimeApi.LoadMetadataForAOTAssembly(bytes, mode);
            //Debug.LogError($"LoadMetadata:{assemblyName}. mode:{mode} ret:{err}");
        }

        private async Task LoadMetaDataAsync()
        {
            HomologousImageMode mode = HomologousImageMode.SuperSet;
            List<Task> tasks = new List<Task>();
            foreach (var assemblyName in AotMetaAssemblyFiles)
            {
                //LoadMetaDataForAssembly(assemblyName, mode);
                tasks.Add(LoadMetaDataForAssemblyAsync(assemblyName, mode));
            }

            await Task.WhenAll(tasks);
        }

    }

}
