using System.Collections.Generic;
using Mono.Data.Sqlite;
using System;

namespace Ability
{
    [Serializable]
    public class TrackingProjectile : EventOperation, IEventOperation
    {
        public EventTarget Target;
        public string EffectName;

        public bool dispersable; // 可被驱散
        public bool Dodgeable; // 可被闪避
        public bool ProvidesVision; // 提供视野 ：导弹 精灵之火 吞噬
        public float VisionRadius; //视野半径 
        public float MoveSpeed; //  
    }
}