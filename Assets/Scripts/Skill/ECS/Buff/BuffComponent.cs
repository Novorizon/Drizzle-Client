
using ECS;
using System;
using System.Collections.Generic;

namespace Game
{
    [Serializable]
    public class Buff
    {
        public int id;
        public Entity entity;  //buff的目标，用来获取目标的属性

        public int effectId;//buff的显示效果
        public Entity effect;//buff的显示效果

        public BuffState state;
        public BuffType type;
        public BuffTo to; //buff作用的属性

        public float value;//

        public float time;//持续时间
        public float timer;//计时器
        public float interval;//间隔性buff的间隔时间
        public float intervalTimer;//间隔性buff的计时器
    }

    [Serializable]
    public class BuffComponent : IComponentData
    {
        //public Dictionary<ulong,Buff> buffs;
        public List<Buff> buffs;
    }
    [Serializable]
    public struct BuffToAdd : IComponentData
    {
        public List<int> buffs;//施加的buff
    }
    [Serializable]
    public struct BuffEffect
    {
        public List<Effect> effects;
    }
    [Serializable]
    public struct Effect
    {
        public int id;
        public Entity entity;
        public float time;

        public Entity owner;
    }
}