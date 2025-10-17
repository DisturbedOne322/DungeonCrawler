using Gameplay.StatusEffects.Buffs.DefensiveCore;
using UnityEngine;
using UnityEngine.Serialization;

namespace Gameplay.Skills.Core
{
    public abstract class DefensiveBuffSkill : BaseSkill
    {
        [FormerlySerializedAs("_defensiveStatusEffectData")] [FormerlySerializedAs("DefensiveBuffData")] [SerializeField]
        protected DefensiveBuffData _defensiveBuffData;
    }
}