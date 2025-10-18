using Gameplay.Facades;
using Gameplay.StatusEffects.Core;
using UnityEngine;

namespace Gameplay.StatusEffects.Buffs.DefensiveCore
{
    public abstract class DefensiveBuffData : BaseStatusEffectData, IDefensiveBuff
    {
        [SerializeField] private DefensiveBuffPriorityType _priority;
        public DefensiveBuffPriorityType Priority => _priority;
        public abstract int ModifyIngoingDamage(int currentDamage, in DamageContext ctx);

        public override BaseStatusEffectInstance CreateInstance() => DefensiveBuffInstance.Create(this);
    }
}