using Gameplay.Combat.Data;
using Gameplay.Units;

namespace Gameplay.Combat
{
    public class CombatBuffsProcessor
    {
        private readonly CombatData _combatData;
        
        public void ApplyGuardToUnit(GameUnit unit) => unit.UnitBuffsData.Guarded.Value = true;
        public void ApplyChargeToUnit(GameUnit unit) => unit.UnitBuffsData.Charged.Value = true;
        public void ApplyConcentrateToUnit(GameUnit unit) => unit.UnitBuffsData.Concentrated.Value = true;

        public int GetBuffedDamage(GameUnit caster, OffensiveSkillData skillData, bool consumeCharge = true)
        {
            int damage = skillData.Damage;

            switch (skillData.DamageType)
            {
                case DamageType.Physical:
                    return ProcessPhysicalAttack(caster, damage, consumeCharge);
                case DamageType.Magical:
                    return ProcessMagicalAttack(caster, damage, consumeCharge);
            }
            
            return damage;
        }

        private int ProcessPhysicalAttack(GameUnit caster, int damage, bool consumeCharge = true)
        {
            if (caster.UnitBuffsData.Charged.Value)
            {
                if(consumeCharge)
                    caster.UnitBuffsData.Charged.Value = false;
                
                damage *= 2;
            }

            return damage;
        }
        
        private int ProcessMagicalAttack(GameUnit caster, int damage, bool consumeCharge = true)
        {
            if (caster.UnitBuffsData.Concentrated.Value)
            {
                if(consumeCharge)
                    caster.UnitBuffsData.Concentrated.Value = false;
                
                damage *= 2;
            }

            return damage;
        }
    }
}