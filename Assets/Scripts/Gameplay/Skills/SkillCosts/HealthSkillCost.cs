using System;
using Gameplay.Combat.Data;
using Gameplay.Combat.Services;
using Gameplay.Skills.Core;
using UnityEngine;

namespace Gameplay.Skills.SkillCosts
{
    [Serializable]
    public class HealthSkillCost : ISkillCost
    {
        [SerializeField] private SkillData _selfDamageData;

        public bool CanPay(CombatService combatService)
        {
            return combatService.ActiveUnit.UnitHealthController.HasEnoughHp(_selfDamageData.BaseDamage);
        }

        public void Pay(CombatService combatService)
        {
            var stream = combatService.CreateHitsStream(_selfDamageData);
            combatService.DealDamageToActiveUnit(stream, 0);
        }
    }
}