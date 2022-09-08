using ECS;

namespace Game
{
    public enum InteractLayer
    {
        Selected = 0,
        Attack = 1,
        Dialogue = 2,
        Pickup = 3,
        Follow = 4,
    }
    public class Interactive : IComponentData
    {
        public InteractLayer Value;
        public bool IsSelected { get; set; }
    }
}
