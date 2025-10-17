using System.Collections.Generic;
using Gameplay.StatusEffects.Buffs.Core;

namespace Gameplay.StatusEffects.Buffs.OffensiveCore
{
    public class OffensiveBuffCalculationProcessor : BaseBuffCalculationProcessor<OffensiveBuffInstance,
        OffensiveBuffPriorityType>
    {
        protected override List<OffensiveBuffInstance> GetActiveBuffs(in DamageContext damageContext)
        {
            return damageContext.Attacker.UnitActiveBuffsData.ActiveOffensiveBuffs;
        }

        protected override bool CanApplyBuffs(in DamageContext damageContext)
        {
            return damageContext.SkillData.CanBeBuffed;
        }

        protected override int ApplyBuff(OffensiveBuffInstance offensiveBuff, int damage,
            in DamageContext damageContext)
        {
            return offensiveBuff.OffensiveStatusEffectData.ModifyOutgoingDamage(damage, damageContext);
        }

        protected override OffensiveBuffPriorityType GetPriorityType(OffensiveBuffInstance offensiveBuff)
        {
            return offensiveBuff.PriorityType;
        }
    }
}