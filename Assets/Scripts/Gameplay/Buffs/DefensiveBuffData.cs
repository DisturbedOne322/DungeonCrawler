using Gameplay.Combat.Modifiers;
using UnityEngine;

namespace Gameplay.Buffs
{
    public abstract class DefensiveBuffData : BaseBuffData, IDefensiveBuff
    {
        [SerializeField] private DefensiveBuffPriorityType _priority;
        public DefensiveBuffPriorityType Priority => _priority;
        public abstract int ModifyIngoingDamage(int currentDamage, in DamageContext ctx);
    }
}