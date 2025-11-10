using Gameplay.StatusEffects.Buffs.HitBuffsCore;
using UnityEngine;
using UnityEngine.Serialization;

namespace Gameplay.Skills.Core
{
    public abstract class BaseOffensiveBuffSkill : BaseSkill
    {
        [FormerlySerializedAs("OffensiveBuffData")] [SerializeField]
        protected HitBuffData _hitBuffData;
    }
}