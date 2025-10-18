using Gameplay.StatusEffects.Core;
using UnityEngine;

namespace Gameplay.StatusEffects.Buffs.OffensiveCore
{
    public abstract class OffensiveBuffData : BaseStatusEffectData, IOffensiveBuff
    {
        [SerializeField] private OffensiveBuffPriorityType _priority;

        public OffensiveBuffPriorityType Priority => _priority;

        public abstract int ModifyOutgoingDamage(int currentDamage, in DamageContext ctx);
    }
}