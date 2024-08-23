using DataBase;
using MVC;
using PureMVC.Interfaces;
using PureMVC.Patterns.Command;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game
{
    public class LoadTableCommand : SimpleCommand
    {
        public const string NAME = "LoadTableCommand";
        public override void Execute(INotification notification)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            TableProxy tableProxy = Facade.RetrieveProxy(TableProxy.NAME) as TableProxy;
            //tableProxy.RegisterTable<HeroTableAccess>();
            //tableProxy.RegisterTable<ModelTableAccess>();
            //tableProxy.RegisterTable<DefaultTableAccess>();
            sw.Stop();
            //UnityEngine.Debug.LogError("”√ ±2£∫" + sw.ElapsedMilliseconds + "");

        }
    }
}
