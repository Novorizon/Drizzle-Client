using DataBase;
using ECS;
using PureMVC.Interfaces;
using PureMVC.Patterns.Facade;
using System;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
namespace Game
{
    [Serializable]
    public class NPC : IComponentData
    {
        public Relationship Value;
    }
}