using System.Collections.Generic;
using Gameplay.Buffs.Core;

namespace Gameplay.Buffs.DefensiveCore
{
    public class DefensiveBuffCalculationProcessor : BaseBuffCalculationProcessor<DefensiveBuffInstance, DefensiveBuffPriorityType>
    {
        protected override List<DefensiveBuffInstance> GetActiveBuffs(in DamageContext damageContext) => damageContext.Defender.UnitActiveBuffsData.ActiveDefensiveBuffs;

        protected override bool CanApplyBuffs(in DamageContext damageContext) => true;

        protected override int ApplyBuff(DefensiveBuffInstance buff, int damage, in DamageContext damageContext) => buff.DefensiveBuffData.ModifyIngoingDamage(damage, damageContext);

        protected override DefensiveBuffPriorityType GetPriorityType(DefensiveBuffInstance buff) => buff.PriorityType;
    }
}