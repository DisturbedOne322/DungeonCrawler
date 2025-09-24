using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Gameplay.Combat.Items
{
    [CreateAssetMenu(fileName = "HealingItem", menuName = "Items/HealingItem")]
    public class HealingItem : BaseItem
    {
        [SerializeField, Space] private int _healAmount;

        protected override UniTask PerformAction(CombatService combatService)
        {
            combatService.HealActiveUnit(_healAmount);
            return UniTask.CompletedTask;
        }

        public override bool CanUse(CombatService combatService)
        {
            int activeUnitHealth = combatService.ActiveUnit.HealthData.CurrentHealth.Value;
            int activeUnitMaxHealth = combatService.ActiveUnit.HealthData.MaxHealth.Value;
            
            return activeUnitHealth < activeUnitMaxHealth;
        }
    }
}