using Gameplay.Units;
using UnityEngine;

namespace Gameplay.Combat
{
    public class CombatService
    {
        private readonly CombatData _combatData;

        private int _turnCount = -1;
        
        public GameUnit ActiveUnit => _combatData.ActiveUnit;
        public GameUnit OtherUnit => _combatData.OtherUnit;

        public CombatService(CombatData combatData)
        {
            _combatData = combatData;
        }
        
        public void StartCombat(GameUnit enemy)
        {
            _turnCount = -1;
            _combatData.ResetCombat(enemy);
        }

        public void ProcessTurn()
        {
            _turnCount++;
            _combatData.UpdateCurrentTurnUnit(_turnCount);
        }
        
        public void DealDamageTo(GameUnit unit, int damage) => unit.UnitHealthController.TakeDamage(GetFinalDamage(unit, damage));

        private int GetFinalDamage(GameUnit unit, int rawDamage)
        {
            float damageReductionModifier = CalculateDamageReduction(unit);
            return Mathf.RoundToInt(rawDamage * damageReductionModifier);
        }

        private float CalculateDamageReduction(GameUnit unit)
        {
            int constitutionStat = unit.UnitStatsData.Constitution.Value;
            return 1 - Mathf.Clamp(constitutionStat, 1, 99) * 1f / 100;
        }
    }
}