using System.Collections.Generic;
using Mono.Data.Sqlite;
using System;
using ECS;
using Game;

namespace DataBase
{
    public class StageData
    {
        private bool Updated;

        public StageData()
        {
            Updated = false;
        }



        public int id;//µ±Ç°Stage id
        public string name;
        public string description;
        public List<int> npcs;

    }
}