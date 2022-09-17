
using System;

namespace Ability
{
    [Serializable]
    public enum BuffLayer : long
    {
        None = 0,

        // add使用
        Cumulate = 1 << 1,//叠加    //Overlap = None << 23,//叠加
        Replace = 1 << 2,//以新换旧，新的效果取代旧的

        // update使用
        Continuous = 1 << 11,// 持续型  一般用作一段时间内持续改变某种属性，对应间歇型 
        Interval = 1 << 12,//间断型  一般用作一段时间内 间隔一段时间改变某种属性



        // remove使用
        Reset = 1 << 21,//删除buff以后需要重置改变的属性
    }

    [Serializable]
    public class BuffData
    {

        public int id;
        public string name;
        public string description;

        public float duration;// 持续时间
        public float interval; // 间隔时间
        public Effect effect;

        public BuffLayer layer;
    }

}

