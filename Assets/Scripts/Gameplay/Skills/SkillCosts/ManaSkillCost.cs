using System;
using Gameplay.Combat.Services;
using Gameplay.Skills.Core;
using UnityEngine;

namespace Gameplay.Skills.SkillCosts
{
    [Serializable]
    public class ManaSkillCost : ISkillCost
    {
        [SerializeField] private int _manaCost;

        public bool CanPay(CombatService combatService)
        {
            return combatService.ActiveUnit.UnitManaData.CurrentMana.Value >= _manaCost;
        }

        public void Pay(CombatService combatService)
        {
            combatService.ActiveUnit.UnitManaController.UseMana(_manaCost);
        }
    }
}