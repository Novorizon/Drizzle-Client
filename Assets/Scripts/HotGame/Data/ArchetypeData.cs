using System.Collections.Generic;
using Mono.Data.Sqlite;
using System;
using ECS;
using Game;

namespace DataBase
{
    public class ArchetypeData
    {
        private bool Updated;

        public ArchetypeData()
        {
            Updated = false;
        }



        static public EntityArchetype Hero = new EntityArchetype(
            typeof(LocalToWorld),
            typeof(Position),
            typeof(Rotation),
            typeof(Scale),
            typeof(CopyTransformFromGameObject),
            typeof(Speed),
            typeof(MoveDirection),
            typeof(FaceDirection),
            typeof(PlayerController)
            );

        static public EntityArchetype Bullet = new EntityArchetype
              (
              typeof(LocalToWorld),
              typeof(Position),
              typeof(Rotation),
              typeof(Scale),
              typeof(CopyTransformFromGameObject),
              typeof(Speed),
              typeof(MoveDirection),
              typeof(FaceDirection),
              typeof(Bullet),
              typeof(LifeTime),
              typeof(MoveDirection)
              );
    }
}