using Gameplay.StatusEffects.Buffs.OffensiveCore;
using UnityEngine;
using UnityEngine.Serialization;

namespace Gameplay.Skills.Core
{
    public abstract class OffensiveBuffSkill : BaseSkill
    {
        [FormerlySerializedAs("_offensiveStatusEffectData")] [FormerlySerializedAs("OffensiveBuffData")] [SerializeField]
        protected OffensiveBuffData _offensiveBuffData;
    }
}