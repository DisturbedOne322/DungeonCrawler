using Gameplay.Buffs.Core;
using UnityEngine;

namespace Gameplay.Buffs.OffensiveCore
{
    public abstract class OffensiveBuffData : BaseBuffData, IOffensiveBuff
    {
        [SerializeField] private OffensiveBuffPriorityType _priority;
        
        public OffensiveBuffPriorityType Priority => _priority;
        
        public abstract int ModifyOutgoingDamage(int currentDamage, in DamageContext ctx);
    }
}