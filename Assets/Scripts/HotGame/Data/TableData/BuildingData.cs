using System.Collections.Generic;
using Mono.Data.Sqlite;
using System;
using Game;
using Unity.Mathematics;
using UnityEngine;

namespace DataBase
{
    public enum BuildingType
    {
        Single,
        Double
    }
    public class BuildingData : TableData
    {
        public int id;
        public string name;
        public string description { get; set; }

        public string asset;//½¨ÖþÄ£ÐÍ
        public float scale = 1;

        public BuildingType type = BuildingType.Single;
        public float sizeX;//
        public float sizeZ;//


        private bool Updated;

        public BuildingData()
        {
            Updated = false;
        }

        public void UpdateHeroToLevel()
        {
            if (!Updated) //this must not be called more than once, otherwise something is wrong
            {
                Updated = true;
            }
        }

        public BuildingData Clone()
        {
            BuildingData newClone = new BuildingData();
            //deep copy
            newClone.id = id;
            newClone.name = name;
            return newClone;
        }
    }
}