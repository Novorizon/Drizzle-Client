using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum StageState
{
    None = 0,
    Start,
    Finish,//完成
    Abort,//废弃
    Failure,//失败
    Expired,//过期
    Reward,//领取奖励

}