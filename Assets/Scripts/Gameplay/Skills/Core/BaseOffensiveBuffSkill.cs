using Gameplay.StatusEffects.Buffs.HitBuffsCore;
using UnityEngine;
using UnityEngine.Serialization;

namespace Gameplay.Skills.Core
{
    public abstract class BaseOffensiveBuffSkill : BaseSkill
    {
        [SerializeField]
        protected HitBuffData HitBuffData;
    }
}