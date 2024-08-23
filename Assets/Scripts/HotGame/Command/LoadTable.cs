using DataBase;
using Game;
using MVC;
using PureMVC.Interfaces;
using PureMVC.Patterns.Command;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace HotGame
{
    public class RegisterTable : SimpleCommand
    {
        public const string NAME = "RegisterTable";
        public override void Execute(INotification notification)
        {
            //Stopwatch sw = new Stopwatch();
            //sw.Start();
            TableProxy tableProxy = Facade.RetrieveProxy(TableProxy.NAME) as TableProxy;
            tableProxy.RegisterTable<HeroTableAccess>();
            tableProxy.RegisterTable<ModelTableAccess>();
            tableProxy.RegisterTable<DefaultTableAccess>();
            tableProxy.RegisterTable<WeaponTableAccess>();
            //sw.Stop();
            //UnityEngine.Debug.LogError("”√ ±2£∫" + sw.ElapsedMilliseconds + "");

            //tableProxy.Load();
        }
    }
}
