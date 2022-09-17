
using ECS;
using System;
using System.Collections.Generic;

namespace Game
{
    [Serializable]
    public class Halo
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
        public float interval;//间隔性Halo的间隔时间
        public float intervalTimer;//间隔性Halo的计时器
    }

    [Serializable]
    public class HaloComponent : IComponentData
    {
        public List<Halo> halos;
    }
}