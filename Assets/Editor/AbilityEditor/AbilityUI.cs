using Ability;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace AbilityEditor
{

    [Serializable]
    public class AbilityAsset : ScriptableObject
    {
        [HideLabel, PreviewField(50), HorizontalGroup("pre", 50, LabelWidth = 50), VerticalGroup("pre/avatar")]
        public Sprite avatar;

        [HideLabel, VerticalGroup("pre/id")]
        public int id;

        [HideLabel, VerticalGroup("pre/id")]
        public new string name;

        [ShowIf("IsNPC")] public int maxLevel;
        [ShowIf("IsNPC")] public int abilityCastPoint;
        [ShowIf("IsNPC")] public int abilityCastRange;    // 施放范围
        [ShowIf("IsNPC")] public int abilityCooldown;    // 冷却时间
        [ShowIf("IsNPC")] public int abilityManaCost;    // 耗蓝
    }


    public partial class AbilityEditor
    {
        WorldNPC worldNpc;

        [HideLabel, PreviewField(50), HorizontalGroup("pre", 50, LabelWidth = 50), VerticalGroup("pre/avatar")]
        public Sprite avatar;

        [HideLabel, VerticalGroup("pre/id")]
        public int id;

        [HideLabel, VerticalGroup("pre/id")]
        public new string name;

        [LabelText("冷却时间"), HorizontalGroup("cd"), ShowIf("IsNPC"),]
        public float cd;
        [LabelText("冷却时间"), HorizontalGroup("mana"), ShowIf("IsNPC"),]
        public float mana;
        [LabelText("最大等级"), HorizontalGroup("maxLevel"), ShowIf("IsNPC"),]
        public int maxLevel;

        [LabelText("被动"), HorizontalGroup("AbilityBehavior", LabelWidth = 50), ShowIf("IsNPC"),]
        public bool IsPassive;
        [LabelText("自动"), HorizontalGroup("AbilityBehavior", LabelWidth = 50), ShowIf("IsNPC"),]
        public bool IsAutoCast;
        [LabelText("普通"), HorizontalGroup("AbilityBehavior", LabelWidth = 50), ShowIf("IsNPC"),]
        public bool IsAttack;


        [LabelText("吟唱时间"), HorizontalGroup("channel"), ShowIf("IsNPC"),]
        public float channel;//施法吟唱时间
        [LabelText("吟唱需站立"), HorizontalGroup("needStop"), ShowIf("IsNPC"),]
        public bool needStop;

        [LabelText("前摇时间"), HorizontalGroup("foreswing"), ShowIf("IsNPC"),]
        public float foreswing;//施法前摇时间
        [LabelText("前摇需站立"), HorizontalGroup("foreswingStop"), ShowIf("IsNPC"),]
        public bool foreswingStop;


        [LabelText("后摇时间"), HorizontalGroup("backswing"), ShowIf("IsNPC"),]
        public float backswing;//施法后摇时间，
        [LabelText("后摇需站立"), HorizontalGroup("backswingStop"), ShowIf("IsNPC"),]
        public bool backswingStop;


        // 技能行为：主动被动自动，普通攻击， 
        [LabelText("技能行为"), HorizontalGroup("Behavior"), ShowIf("IsNPC"),]
        public Behavior behavior;

        // 作用目标：任何单位 友单 友军 敌单 敌军 所有单位
        [LabelText("作用目标"), HorizontalGroup("TargetTeam"), ShowIf("IsNPC"),]
        public TargetTeam targetTeam;

        [LabelText("目标类型"), HorizontalGroup("TargetType"), ShowIf("IsNPC"),]
        public TargetType targetType;

        public Ability.Range range;
        public float distance;
        public float angle;
        public bool isAOE;
        public int maxTargets;


        public int limitLevel;
        public int limitHP;
        public bool ignoreImmunity;
        public bool ignoreInvincible;

        // 技能效果
        //public List<Ability.Effect> effects;
        public Dictionary<AbilityState, List<Effect>> effects;


        //是否带有buff
        public List<Ability.Buff> buffs;
        //public AbilityData data;



        private void OnSelectionChanged(SelectionChangedType type)
        {
            //if (type == SelectionChangedType.ItemAdded)
            //{
            //    WorldNPCAsset asset = (WorldNPCAsset)tree.Selection.SelectedValue;
            //    if (asset == null)
            //        return;

            //    WorldNPC world = data.npcs.Find(a => a.id == asset.id);
            //    if (world == null)
            //        return;

            //    worldNpc = world;
            //    NPCData npc = tableProxy.GetData<NPCData>(worldNpc.npcId);

            //    id = npc.id;
            //    name = npc.name;
            //    description = npc.description;
            //    level = npc.level;
            //    this.type = npc.type;

            //    avatar = AssetDatabase.LoadAssetAtPath<Sprite>(npc.avatar);
            //    worldNpc.model = AssetDatabase.LoadAssetAtPath<GameObject>(npc.model);
            //    if (!worldNpc.display)
            //    {
            //        GameObject.DestroyImmediate(worldNpc.display);
            //        worldNpc.display = GameObject.Instantiate(worldNpc.model);
            //    }
            //    model = worldNpc.display;
            //}

        }



    }
}