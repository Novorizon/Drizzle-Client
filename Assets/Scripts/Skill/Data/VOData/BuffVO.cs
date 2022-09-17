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

        public float duration;// ����ʱ��
        public float interval; // ���ʱ��
        public EffectVO effect;
        public float timer;//����ʱ��
        public float intervalTimer;//�����buff�ļ��ʱ��

        public BuffLayer layer;
        public BuffState state;

        public float value;//�ܹ��޸ĵ���ֵ���Ƴ�buff������Ҫ

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
