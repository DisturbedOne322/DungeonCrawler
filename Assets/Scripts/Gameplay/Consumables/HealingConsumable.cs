using Cysharp.Threading.Tasks;
using Gameplay.Combat.Services;
using UnityEngine;

namespace Gameplay.Consumables
{
    [CreateAssetMenu(fileName = "HealingItem", menuName = "Gameplay/Items/HealingItem")]
    public class HealingConsumable : BaseConsumable
    {
        [SerializeField] [Space] private int _healAmount;

        protected override UniTask PerformAction(CombatService combatService)
        {
            combatService.HealActiveUnit(_healAmount);
            return UniTask.CompletedTask;
        }

        public override bool CanUse(CombatService combatService)
        {
            var activeUnitHealth = combatService.ActiveUnit.UnitHealthData.CurrentHealth.Value;
            var activeUnitMaxHealth = combatService.ActiveUnit.UnitHealthData.MaxHealth.Value;

            return activeUnitHealth < activeUnitMaxHealth;
        }
    }
}