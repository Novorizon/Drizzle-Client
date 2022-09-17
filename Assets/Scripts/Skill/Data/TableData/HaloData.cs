
using System;

namespace Game 
{
    [Serializable]
    public class HaloData
    {
        public int id;

        public string  effect;//buff的显示效果
        public BuffType type;
        public BuffTo to; //buff作用的属性

        public float value;//

        public float time;//持续时间
        public float interval;//间隔性buff的间隔时间
    }
}

