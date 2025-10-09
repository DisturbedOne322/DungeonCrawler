using System.Collections.Generic;
using Gameplay.Buffs.Core;

namespace Gameplay.Buffs.OffensiveCore
{
    public class OffensiveBuffCalculationProcessor : BaseBuffCalculationProcessor<OffensiveBuffInstance, OffensiveBuffPriorityType>
    {
        protected override List<OffensiveBuffInstance> GetActiveBuffs(in DamageContext damageContext) => damageContext.Attacker.UnitActiveBuffsData.ActiveOffensiveBuffs;

        protected override bool CanApplyBuffs(in DamageContext damageContext) => damageContext.SkillData.CanBeBuffed;

        protected override int ApplyBuff(OffensiveBuffInstance buff, int damage, in DamageContext damageContext) => buff.OffensiveBuffData.ModifyOutgoingDamage(damage, damageContext);

        protected override OffensiveBuffPriorityType GetPriorityType(OffensiveBuffInstance buff) => buff.PriorityType;
    }
}