using ECS;
using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{

    public class SkillVO
    {
        public ulong guid;

        public int id;

        public float cd;
        public float mana;
        public SkillType type;

        public float channel;//ʩ������ʱ��
        public bool channelStop;

        public float foreswing;//ʩ��ǰҡʱ��
        public bool foreswingStop;

        public float duration;//ʩ��ʱ��
        public bool durationStop;

        public float backswing;//ʩ����ҡʱ�䣬
        public bool backswingStop;

        public EntityLayerMask supposeLayer;//Ŀ������ ���� �Լ� ���� 
        //public RangeShape shape;//������Χ��״
        public float distance; //������Χ�뾶
        public float angle;  //������Χ�Ƕ�

        public bool needTarget;//ָ���� 

        public int maxTargets;//Ŀ������

        public Entity castEffect;
        public Entity effect;
        public Entity targetEffect;
        public List<int> buffs;//ʩ�ӵ�buff
    }
}
