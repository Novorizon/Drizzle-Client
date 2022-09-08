using DataBase;
using MVC;
using PureMVC.Interfaces;
using PureMVC.Patterns.Command;
using System.Diagnostics;
using UnityEngine;

namespace Game
{
    public class RegisterTableCommand : SimpleCommand
    {
        public const string NAME = "RegisterTableCommand";
        public override void Execute(INotification notification)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            TableProxy tableProxy = Facade.RetrieveProxy(TableProxy.NAME) as TableProxy;
            tableProxy.RegisterTable<HeroTableAccess>();
            tableProxy.RegisterTable<ModelTableAccess>();
            tableProxy.RegisterTable<DefaultTableAccess>();
            sw.Stop();
            SendNotification(GameConsts.LOAD_DB);
            //UnityEngine.Debug.LogError("”√ ±2£∫" + sw.ElapsedMilliseconds + "");
        }
    }
}
