using ECS;
using Game;
using Unity.Mathematics;
using UnityEngine;


public class Weapon : IComponentData
{
    //public SpawnState state= SpawnState.None;
    public WeaponType type= WeaponType.Single;
    //public WeaponAttackShape shape;

    public int id;
    //public Entity bullet;//子弹
    public GameObject gameObject;//子弹模型

    public float3 position;
    public quaternion rotation;
    public float3 forward;
    public float scale=1;

    public int trackCount=1;//弹道数量，每波子弹数量
    public float bulletSpeed=5;//子弹速度
    public float bulletLifeTime=3;//子弹存在时间

    public float duration;//每波子弹间隔
    public float lifetime;
    public float timer;
    public bool fire;

    public float attack;
    //public float speed;
    public float range;
    public float penetration;

    //用于武器攻击范围检测
    public float angle;

    public int bulletEffectId;//子弹伤害特效
    public float bulletEffectSpan;//子弹射击特效时间

    public int weaponEffectId; //武器射击特效
    public float weaponEffectSpan;//武器射击特效时间
}