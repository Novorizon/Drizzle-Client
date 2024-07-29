using UnityEngine;
using UnityEngine.AddressableAssets;

[System.Serializable]
public class AssetReferenceTextAsset : AssetReference
{
    public AssetReferenceTextAsset(string guid) : base(guid) { }

    public override bool ValidateAsset(Object obj)
    {
        return obj is TextAsset;
    }

    public override bool ValidateAsset(string path)
    {
        return path.EndsWith(".db");
    }
}
