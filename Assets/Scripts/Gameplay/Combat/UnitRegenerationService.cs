using Gameplay.Facades;
using Gameplay.Units;
using UnityEngine;

namespace Gameplay.Combat
{
    public class UnitRegenerationService
    {
        private const float InBattleRegenMultiplier = 1f;
        private const float OutOfBattleRegenMultiplier = 0.2f;

        private readonly PlayerUnit _playerUnit;

        public UnitRegenerationService(PlayerUnit playerUnit)
        {
            _playerUnit = playerUnit;
        }
        
        public void RegenerateUnitInBattle(IEntity unit) => RegenerateUnit(unit, InBattleRegenMultiplier);

        public void RegeneratePlayerOutOfBattle() => RegenerateUnit(_playerUnit, OutOfBattleRegenMultiplier);

        private void RegenerateUnit(IEntity unit, float multiplier)
        {
            var healthRegen = unit.UnitBonusStatsData.HealthRegenBonus.Value;
            var manaRegen = unit.UnitBonusStatsData.ManaRegenBonus.Value;
            
            healthRegen = Mathf.CeilToInt(healthRegen * multiplier);
            manaRegen = Mathf.CeilToInt(manaRegen * multiplier);
            
            unit.UnitHealthController.Heal(healthRegen);
            unit.UnitManaController.RegenerateMana(manaRegen);
        }
    }
}