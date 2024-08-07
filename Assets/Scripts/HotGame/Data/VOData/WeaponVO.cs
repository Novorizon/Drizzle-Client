using System.Collections.Generic;
using Mono.Data.Sqlite;
using ECS;
using UnityEngine;

namespace Game
{
    public class WeaponVO
    {
        public ulong guid;
        public int id;                                        // 
        public GameObject bullet;//×Óµ¯Ä£ÐÍ

        public WeaponVO()
        {
           
        }


        public WeaponVO Clone()
        {
            WeaponVO newClone = new WeaponVO();
            return newClone;
        }
    }
}