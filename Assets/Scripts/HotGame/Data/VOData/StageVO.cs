using System.Collections.Generic;
using Mono.Data.Sqlite;
using System;
using ECS;
using Game;

namespace HotGame
{
    public class StageVO
    {
        private bool Updated;

        public StageVO()
        {
            Updated = false;
        }



        public int id;//µ±Ç°Stage id
        public string name;
        public string description;
        public List<int> npcs;


        public StageState state;
    }
}