using DataBase;
using ECS;
using PureMVC.Interfaces;
using PureMVC.Patterns.Facade;
using System;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
namespace HotGame
{
    public class SpawnSystem : SystemBase<Spawn>
    {
        protected override void OnUpdate(int index, Entity entity, Spawn spawn)
        {
            if (spawn.state == SpawnState.None || spawn.state == SpawnState.Finish)
                return;

            spawn.intervalTimer += Time.DeltaTime;

            if (spawn.duration > 0 && spawn.timer > spawn.duration)
            {
                spawn.state = SpawnState.Finish;
                spawn.timer = 0;
                return;
            }

            spawn.intervalTimer += Time.DeltaTime;

            if (spawn.intervalTimer > spawn.interval)
            {
                spawn.state = SpawnState.Start;
                spawn.intervalTimer = 0;
            }
            else
            {
                spawn.state = SpawnState.Pause;
            }
        }

    }
}