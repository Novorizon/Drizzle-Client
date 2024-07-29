using ECS;

public enum SpawnState
{
    None = 0,
    Start,
    Pause,
    Finish
}

public class Spawn : IComponentData
{
    public SpawnState state;
    public float intervalTimer;
    public float timer;
    public float interval;//每两次spawn的间隔
    public float duration;//连续spawn时间
}