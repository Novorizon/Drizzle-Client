using System.Collections.Generic;
using Mono.Data.Sqlite;
using System;
using UnityEngine;

namespace Game
{
    public class NPCVO
    {
        public ulong guid;                                        // 
        public int id;                                        // 
        public string name;                                     // 
        public string modelPath;                                     // 
        public int modelId;                                       // 

        public int gender;                                       // 等级
        public int level;                                // 

        public int attack;                                      // 
        public int defence;                                      // 

        //public Attribute speed;                                      // 
        public float speed;                                      // 
        public int health;                                       // 
        public int age;                                      // 

        public int strength;                         // 
        public int agility;                                  // 
        public int intelligence;                           // 


        public Vector3 position;                                       // 等级
        public Vector3 forward;                                // 


        public bool isImmunity;
        public bool isInvincible;
    }
}