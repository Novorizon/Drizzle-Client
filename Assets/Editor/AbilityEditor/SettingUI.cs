using Ability;
using JsonExtension;
using Newtonsoft.Json;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace AbilityEditor
{


    public partial class AbilityEditor
    {

        // 技能效果
        [ShowIf("IsSettings"),] public List<AbilityData> datas;




        [Button("Import"), ShowIf("IsSettings"),]
        public void Import()
        {
            id = 0;
            name = "Default";

            AbilityAsset asset = ScriptableObject.CreateInstance<AbilityAsset>();
            asset.id = 0;

            AssetDatabase.CreateAsset(asset, "Assets/Editor/AbilityEditor/Ability/" + name + ".asset");

        }



        [Button("Export"), ShowIf("IsSettings"),]
        public void Export()
        {
            AbilityData data = new AbilityData();
            data.id = id;
            data.name = name;
            data.limitHP = limitHP;
            data.icon = "";
            data.cd= cd;
            data.distance = distance;
            data.mana = mana;
            data.maxLevel = maxLevel;


            data.channel = channel;
            data.foreswing = foreswing;
            data.backswing = backswing;


            if (IsPassive)
                data.behavior = Behavior.ABILITY_BEHAVIOR_PASSIVE;
            else if (IsAutoCast)
                data.behavior = Behavior.ABILITY_BEHAVIOR_PASSIVE;

            data.effects = effects;
            data.buffs = buffs;

            var settings = new JsonSerializerSettings()
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
            };
            var jsonResolver = new PropertySerializerContractResolver();
            jsonResolver.IgnoreProperty(typeof(Vector2Int), "magnitude");
            jsonResolver.IgnoreProperty(typeof(Vector2Int), "sqrMagnitude");
            jsonResolver.IgnoreProperty(typeof(Vector2), "magnitude");
            jsonResolver.IgnoreProperty(typeof(Vector2), "sqrMagnitude");
            jsonResolver.IgnoreProperty(typeof(Vector3), "normalized");
            jsonResolver.IgnoreProperty(typeof(Vector3), "magnitude");
            jsonResolver.IgnoreProperty(typeof(Vector3), "sqrMagnitude");
            jsonResolver.IgnoreProperty(typeof(Quaternion), "eulerAngles");
            settings.ContractResolver = jsonResolver;
            var json = JsonConvert.SerializeObject(data, settings);

            File.WriteAllText("Assets/Editor/AbilityEditor/Ability/" + id + ".json", json);
        }



    }
}