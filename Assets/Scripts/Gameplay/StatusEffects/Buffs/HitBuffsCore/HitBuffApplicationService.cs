using System.Collections.Generic;
using Gameplay.StatusEffects.Buffs.Core;

namespace Gameplay.StatusEffects.Buffs.HitBuffsCore
{
    public class HitBuffApplicationService : BaseHitBuffApplicationService<HitBuffInstance,
        HitBuffPriorityType>
    {
        protected override List<HitBuffInstance> GetActiveBuffs(in DamageContext damageContext)
        {
            return damageContext.Attacker.UnitActiveStatusEffectsData.ActiveOffensiveBuffs;
        }

        protected override bool CanApplyBuffs(in DamageContext damageContext)
        {
            return damageContext.HitData.CanBeBuffed;
        }

        protected override int ApplyBuff(HitBuffInstance hitBuff, int damage,
            in DamageContext damageContext)
        {
            return hitBuff.HitBuffData.ModifyOutgoingDamage(damage, damageContext);
        }

        protected override HitBuffPriorityType GetPriorityType(HitBuffInstance hitBuff)
        {
            return hitBuff.PriorityType;
        }
    }
}