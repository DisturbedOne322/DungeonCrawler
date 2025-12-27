using System;
using System.Collections.Generic;
using Gameplay.Combat.Data;
using Gameplay.StatusEffects.Debuffs.Core;
using UnityEngine;

namespace Gameplay.Dungeon
{
    [Serializable]
    public class TrapData
    {
        [Range(0,1f)] public float DamageChance = 0.5f;
        public List<SkillData> Skills = new List<SkillData>();
        public List<StatDebuffData> Debuffs = new();
    }
}