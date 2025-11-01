using Constants;
using Gameplay.Facades;
using Gameplay.Units;
using UnityEngine;

namespace Gameplay.Combat
{
    public class UnitRegenerationService
    {
        private readonly PlayerUnit _playerUnit;

        public UnitRegenerationService(PlayerUnit playerUnit)
        {
            _playerUnit = playerUnit;
        }
        
        public void RegenerateUnitInBattle(IEntity unit) => RegenerateUnit(unit, 1);

        public void RegeneratePlayerOutOfBattle() => RegenerateUnit(_playerUnit, GameplayConstants.RegenerationRateOutOfCombat);

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