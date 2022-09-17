using ECS;
using System.Collections.Generic;

namespace Ability
{
    public class EffectVO
    {
        public int id;
        public string name;
        public string description;

        public float value; //Ч����ֵ  ��������� todo
        public bool dispersable; // �ɱ���ɢ

        public Type type;//Ч������
        public Attribute attribute; //���Ч�������� �ټ����������

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
