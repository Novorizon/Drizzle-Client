using System.Collections.Generic;
using Mono.Data.Sqlite;
using System;
using UnityEngine;

namespace Game
{
    public class ItemVO
    {
        public object Key => UID;
        public long UID;                                        // 

        public int id;                                        // 
        public int count;                                     // 


        public ItemVO()
        {
           
        }


        public ItemVO Clone()
        {
            ItemVO newClone = new ItemVO();
            return newClone;
        }
    }
}