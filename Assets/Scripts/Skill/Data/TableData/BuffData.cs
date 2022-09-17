
using System;

namespace Ability
{
    [Serializable]
    public enum BuffLayer : long
    {
        None = 0,

        // addʹ��
        Cumulate = 1 << 1,//����    //Overlap = None << 23,//����
        Replace = 1 << 2,//���»��ɣ��µ�Ч��ȡ���ɵ�

        // updateʹ��
        Continuous = 1 << 11,// ������  һ������һ��ʱ���ڳ����ı�ĳ�����ԣ���Ӧ��Ъ�� 
        Interval = 1 << 12,//�����  һ������һ��ʱ���� ���һ��ʱ��ı�ĳ������



        // removeʹ��
        Reset = 1 << 21,//ɾ��buff�Ժ���Ҫ���øı������
    }

    [Serializable]
    public class BuffData
    {

        public int id;
        public string name;
        public string description;

        public float duration;// ����ʱ��
        public float interval; // ���ʱ��
        public Effect effect;

        public BuffLayer layer;
    }

}

