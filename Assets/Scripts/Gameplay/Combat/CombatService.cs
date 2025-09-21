using Gameplay.Units;
using UnityEngine;

namespace Gameplay.Combat
{
    public class CombatService
    {
        private readonly CombatData _combatData;
        private readonly CombatFormulaService _combatFormulaService;

        private int _turnCount = -1;
        
        public GameUnit ActiveUnit => _combatData.ActiveUnit;
        public GameUnit OtherUnit => _combatData.OtherUnit;

        public CombatService(CombatData combatData, CombatFormulaService combatFormulaService)
        {
            _combatData = combatData;
            _combatFormulaService = combatFormulaService;
        }
        
        public void StartCombat(GameUnit enemy)
        {
            _turnCount = -1;
            _combatData.ResetCombat(enemy);
        }

        public void StartTurn()
        {
            _turnCount++;
            _combatData.UpdateCurrentTurnUnit(_turnCount);
            ActiveUnit.UnitBuffsData.Guarded.Value = false;
        }

        public void ApplyGuardToActiveUnit() => ActiveUnit.UnitBuffsData.Guarded.Value = true;
        public void ApplyChargeToActiveUnit() => ActiveUnit.UnitBuffsData.Charged.Value = true;
        public void ApplyConcentrateToActiveUnit() => ActiveUnit.UnitBuffsData.Concentrated.Value = true;
        
        public void DealDamageToActiveUnit(int damage)
        {
            int damageTaken = GetFinalDamageTo(ActiveUnit, damage);
            ActiveUnit.UnitHealthController.TakeDamage(damageTaken);
        }
        
        public void DealDamageToOtherUnit(int damage)
        {
            if (ActiveUnit.UnitBuffsData.Charged.Value)
            {
                ActiveUnit.UnitBuffsData.Charged.Value = false;
                damage *= 2;
            }
            
            int damageTaken = GetFinalDamageTo(OtherUnit, damage);
            OtherUnit.UnitHealthController.TakeDamage(damageTaken);
        }

        private int GetFinalDamageTo(GameUnit unit, int rawDamage)
        {
            float damageReductionModifier = CalculateDamageReduction(unit);
            
            float constitutionReducedDamage = rawDamage * damageReductionModifier;

            if (unit.UnitBuffsData.Guarded.Value)
                constitutionReducedDamage /= 2;
            
            return Mathf.RoundToInt(constitutionReducedDamage);
        }

        private float CalculateDamageReduction(GameUnit unit)
        {
            int constitutionStat = unit.UnitStatsData.Constitution.Value;
            return 1 - Mathf.Clamp(constitutionStat, 1, 99) * 1f / 100;
        }
    }
}