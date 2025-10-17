using Gameplay.StatusEffects.Buffs.OffensiveCore;
using UnityEngine;
using UnityEngine.Serialization;

namespace Gameplay.Skills.Core
{
    public abstract class OffensiveBuffSkill : BaseSkill
    {
        [FormerlySerializedAs("OffensiveBuffData")] [SerializeField]
        protected OffensiveStatusEffectData _offensiveStatusEffectData;
    }
}