
using UnityEngine;

public static class ProtoExtension
{
    public static Vector3 Vector3(this Game.Protobuffer.Vector3 data)
    {
        return new Vector3(data.X,data.Y,data.Z);
    }
}