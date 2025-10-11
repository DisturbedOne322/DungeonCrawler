using System;
using UnityEngine;

namespace Gameplay.Combat.Data
{
    [Serializable]
    public struct SkillData
    {
        [Min(1)] 
        public int Damage;
        public DamageType DamageType;

        [Space, Min(1)] 
        public int MinHits;
        [Min(1)]
        public int MaxHits;

        [Range(0,1), Space] 
        public float BaseCritChance;
        public bool CanCrit;
        
        [Range(0,1), Space] 
        public float BaseHitChance;
        public bool IsUnavoidable;
        
        [Space] 
        public bool IsPiercing;
        public bool CanBeBuffed;
        public bool ConsumeStance;
    }
}