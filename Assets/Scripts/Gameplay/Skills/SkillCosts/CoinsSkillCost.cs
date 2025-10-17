using System;
using Gameplay.Combat.Services;
using Gameplay.Skills.Core;
using UnityEngine;

namespace Gameplay.Skills.SkillCosts
{
    [Serializable]
    public class CoinsSkillCost : ISkillCost
    {
        [SerializeField] private int _cost;

        public bool CanPay(CombatService combatService)
        {
            return combatService.ActiveUnit.UnitInventoryData.Coins.Value >= _cost;
        }

        public void Pay(CombatService combatService)
        {
            combatService.ActiveUnit.UnitInventoryData.Coins.Value -= _cost;
        }
    }
}