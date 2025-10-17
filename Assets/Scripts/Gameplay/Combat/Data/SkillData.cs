using System;
using UnityEngine;

namespace Gameplay.Combat.Data
{
    [Serializable]
    public class SkillData
    {
        [Min(1)] public int BaseDamage = 1;

        public DamageType DamageType = DamageType.Physical;

        [Space] [Min(1)] public int MinHits = 1;

        [Min(1)] public int MaxHits = 1;

        [Range(0, 1)] [Space] public float BaseCritChance;

        public bool CanCrit = true;

        [Range(0, 1)] [Space] public float BaseHitChance = 1;

        public bool IsUnavoidable;

        [Range(0, 1f)] [Space] public float PenetrationRatio;

        public bool IsPiercing;

        [Space] public bool CanBeBuffed = true;

        public bool ConsumeStance = true;
    }
}