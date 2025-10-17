using Gameplay.StatusEffects.Core;
using UnityEngine;

namespace Gameplay.StatusEffects.Buffs.OffensiveCore
{
    public abstract class OffensiveStatusEffectData : BaseStatusEffectData, IOffensiveBuff
    {
        [SerializeField] private OffensiveBuffPriorityType _priority;

        public OffensiveBuffPriorityType Priority => _priority;

        public abstract int ModifyOutgoingDamage(int currentDamage, in DamageContext ctx);
    }
}