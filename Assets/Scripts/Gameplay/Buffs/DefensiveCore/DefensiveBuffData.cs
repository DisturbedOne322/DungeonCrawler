using Gameplay.Buffs.Core;
using UnityEngine;

namespace Gameplay.Buffs.DefensiveCore
{
    public abstract class DefensiveBuffData : BaseBuffData, IDefensiveBuff
    {
        [SerializeField] private DefensiveBuffPriorityType _priority;
        public DefensiveBuffPriorityType Priority => _priority;
        public abstract int ModifyIngoingDamage(int currentDamage, in DamageContext ctx);
    }
}