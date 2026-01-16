using Gameplay.Combat.Data;
using Gameplay.StatusEffects.Core;
using UnityEngine;

namespace Gameplay.StatusEffects.Buffs.HitBuffsCore
{
    public abstract class HitBuffData : BaseStatusEffectData, IHitBuff
    {
        [SerializeField] private HitBuffPriorityType _priority;

        public HitBuffPriorityType Priority => _priority;

        public abstract int ModifyOutgoingDamage(int currentDamage, in DamageContext ctx);

        public abstract float GetExpectedDamageMultiplier(HitData hitData);
        
        public override BaseStatusEffectInstance CreateInstance()
        {
            return HitBuffInstance.Create(this);
        }
        
        protected abstract bool AppliesTo(HitData hitData);
    }
}