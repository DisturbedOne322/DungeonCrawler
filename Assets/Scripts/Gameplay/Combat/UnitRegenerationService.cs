using Data.Constants;
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

        public void RegenerateUnitInBattle(IGameUnit unit)
        {
            RegenerateUnit(unit);
        }

        public void RegeneratePlayerOutOfBattle()
        {
            RegenerateUnit(_playerUnit);
        }

        private void RegenerateUnit(IGameUnit unit)
        {
            var healthRegen = unit.UnitBonusStatsData.HealthRegenBonus.Value;
            var manaRegen = unit.UnitBonusStatsData.ManaRegenBonus.Value;
            
            healthRegen = Mathf.CeilToInt(healthRegen);
            manaRegen = Mathf.CeilToInt(manaRegen);

            unit.UnitHealthController.AddHealth(healthRegen);
            unit.UnitManaController.AddMana(manaRegen);
        }
    }
}