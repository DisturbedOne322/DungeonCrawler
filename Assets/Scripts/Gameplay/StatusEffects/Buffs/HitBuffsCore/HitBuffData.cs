using Gameplay.StatusEffects.Core;
using UnityEngine;

namespace Gameplay.StatusEffects.Buffs.HitBuffsCore
{
    public abstract class HitBuffData : BaseStatusEffectData, IHitBuff
    {
        [SerializeField] private HitBuffPriorityType _priority;

        public HitBuffPriorityType Priority => _priority;
        
        public override BaseStatusEffectInstance CreateInstance() => HitBuffInstance.Create(this);
        
        public abstract int ModifyOutgoingDamage(int currentDamage, in DamageContext ctx);
    }
}