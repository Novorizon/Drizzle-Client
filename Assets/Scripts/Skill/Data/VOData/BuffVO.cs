using ECS;

namespace Ability
{
    public enum BuffState
    {
        None,
        Added,
        Removed,
    }

    public class BuffVO
    {
        public int id;
        public string name;
        public string description;

        public float duration;// 持续时间
        public float interval; // 间隔时间
        public EffectVO effect;
        public float timer;//持续时间
        public float intervalTimer;//间隔性buff的间隔时间

        public BuffLayer layer;
        public BuffState state;

        public float value;//总共修改的数值，移除buff可能需要

        public Entity target;
        public BuffVO(BuffData data)
        {
            this.id  = data. id;
            this.name  = data. name;
            this.description  = data. description;
            this.duration  = data. duration;
            this.interval  = data. interval;
            this.effect  =new EffectVO( data. effect);
            this.timer  =0;
            this.intervalTimer  = 0;
        }
    }
}
