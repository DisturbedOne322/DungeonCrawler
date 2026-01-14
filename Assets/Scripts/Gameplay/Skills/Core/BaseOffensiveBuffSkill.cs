using Gameplay.StatusEffects.Buffs.HitBuffsCore;
using UnityEngine;

namespace Gameplay.Skills.Core
{
    public abstract class BaseOffensiveBuffSkill : BaseSkill
    {
        [SerializeField] protected HitBuffData HitBuffData;
    }
}