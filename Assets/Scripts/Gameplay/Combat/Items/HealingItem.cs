using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Gameplay.Combat.Items
{
    [CreateAssetMenu(fileName = "HealingItem", menuName = "Items/HealingItem")]
    public class HealingItem : BaseItem
    {
        [SerializeField, Space] private int _healAmount;

        public override async UniTask UseAction(CombatService combatService)
        {
            await UniTask.WaitForSeconds(0.5f);
            combatService.ActiveUnit.UnitHealthController.Heal(_healAmount);
            await UniTask.WaitForSeconds(0.5f);
        }

        public override bool CanUse(CombatService combatService)
        {
            int activeUnitHealth = combatService.ActiveUnit.HealthData.CurrentHealth.Value;
            int activeUnitMaxHealth = combatService.ActiveUnit.HealthData.MaxHealth.Value;
            
            return activeUnitHealth < activeUnitMaxHealth;
        }
    }
}