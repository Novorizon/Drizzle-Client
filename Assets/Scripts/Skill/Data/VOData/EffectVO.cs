using ECS;
using System.Collections.Generic;

namespace Ability
{
    public class EffectVO
    {
        public int id;
        public string name;
        public string description;

        public float value; //效果数值  如果是属性 todo
        public bool dispersable; // 可被驱散

        public Type type;//效果类型
        public Attribute attribute; //如果效果是属性 再检查属性类型

        public Entity target;
        //public List<Entity> targets;

        public EffectVO(Effect effect)
        {
            this.id = effect.id;
            this.name = effect.name;
            this.description = effect.description;
            this.value = effect.value;
            this.dispersable = effect.dispersable;
            this.type = effect.type;
            this.attribute = effect.attribute;
        }
    }
}
