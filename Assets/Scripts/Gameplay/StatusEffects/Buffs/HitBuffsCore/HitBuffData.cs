using Gameplay.StatusEffects.Core;
using UnityEngine;

namespace Gameplay.StatusEffects.Buffs.HitBuffsCore
{
    public abstract class HitBuffData : BaseStatusEffectData, IHitBuff
    {
        [SerializeField] private HitBuffPriorityType _priority;

        public HitBuffPriorityType Priority => _priority;

        public abstract int ModifyOutgoingDamage(int currentDamage, in DamageContext ctx);

        public override BaseStatusEffectInstance CreateInstance()
        {
            return HitBuffInstance.Create(this);
        }
    }
}