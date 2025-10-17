using System.Collections.Generic;
using Gameplay.StatusEffects.Buffs.Core;

namespace Gameplay.StatusEffects.Buffs.DefensiveCore
{
    public class DefensiveBuffCalculationProcessor : BaseBuffCalculationProcessor<DefensiveBuffInstance,
        DefensiveBuffPriorityType>
    {
        protected override List<DefensiveBuffInstance> GetActiveBuffs(in DamageContext damageContext)
        {
            return damageContext.Defender.UnitActiveBuffsData.ActiveDefensiveBuffs;
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