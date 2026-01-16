using Gameplay.Combat.Data;
using Gameplay.StatusEffects.Buffs.HitBuffsCore;
using Gameplay.StatusEffects.Core;
using UnityEngine;

namespace Gameplay.StatusEffects.Buffs.DefensiveCore
{
    public abstract class DefensiveBuffData : BaseStatusEffectData, IDefensiveBuff
    {
        [SerializeField] private DefensiveBuffPriorityType _priority;
        public DefensiveBuffPriorityType Priority => _priority;
        public abstract int ModifyIngoingDamage(int currentDamage, in DamageContext ctx);

        public abstract float GetDamageReductionMultiplier(HitBuffInstance hitBuff);
        
        public override BaseStatusEffectInstance CreateInstance()
        {
            return DefensiveBuffInstance.Create(this);
        }
    }
}