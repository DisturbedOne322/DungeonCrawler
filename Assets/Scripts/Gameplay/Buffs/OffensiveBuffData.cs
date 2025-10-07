using Gameplay.Combat.Modifiers;
using UnityEngine;

namespace Gameplay.Buffs
{
    public abstract class OffensiveBuffData : BaseBuffData, IOffensiveBuff
    {
        [SerializeField] private OffensiveBuffPriorityType _priority;
        
        public OffensiveBuffPriorityType Priority => _priority;
        
        public abstract int ModifyOutgoingDamage(int currentDamage, in DamageContext ctx);
    }
}