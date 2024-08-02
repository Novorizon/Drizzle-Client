using System.Collections.Generic;
using Mono.Data.Sqlite;
using System;
using ECS;
using Game;

namespace HotGame
{
    public class ArchetypeData
    {
        private bool Updated;

        public ArchetypeData()
        {
            Updated = false;
        }



        static public EntityArchetype Hero = new EntityArchetype(
            typeof(CopyInitialTransformFromGameObject),
            typeof(CopyTransformFromGameObject),
            typeof(CopyTransformToGameObject),

            typeof(LocalToWorld),
            typeof(Position),
            typeof(Rotation),
            typeof(Scale),
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
              //typeof(Bullet),
              typeof(LifeTime),
              typeof(MoveDirection)
              );
    }
}