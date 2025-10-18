using Gameplay.StatusEffects.Buffs.DefensiveCore;
using UnityEngine;

namespace Gameplay.Skills.Core
{
    public abstract class BaseDefensiveBuffSkill : BaseSkill
    {
        [SerializeField] protected DefensiveBuffData DefensiveBuffData;
    }
}