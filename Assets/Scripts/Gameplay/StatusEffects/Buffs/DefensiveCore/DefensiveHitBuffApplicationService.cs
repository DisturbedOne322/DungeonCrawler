using System.Collections.Generic;
using Gameplay.Combat.Data;
using Gameplay.StatusEffects.Buffs.Core;

namespace Gameplay.StatusEffects.Buffs.DefensiveCore
{
    public class DefensiveHitBuffApplicationService : BaseHitBuffApplicationService<DefensiveBuffInstance,
        DefensiveBuffPriorityType>
    {
        protected override List<DefensiveBuffInstance> GetActiveBuffs(in DamageContext damageContext)
        {
            return damageContext.Defender.UnitActiveStatusEffectsData.ActiveDefensiveBuffs;
        }

        protected override bool CanApplyBuffs(in DamageContext damageContext)
        {
            return true;
        }

        protected override int ApplyBuff(DefensiveBuffInstance defensiveBuff, int damage,
            in DamageContext damageContext)
        {
            return defensiveBuff.DefensiveBuffData.ModifyIngoingDamage(damage, damageContext);
        }

        protected override DefensiveBuffPriorityType GetPriorityType(DefensiveBuffInstance defensiveBuff)
        {
            return defensiveBuff.PriorityType;
        }
    }
}