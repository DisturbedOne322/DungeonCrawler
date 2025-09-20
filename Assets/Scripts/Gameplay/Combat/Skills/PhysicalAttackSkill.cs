using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Gameplay.Combat.Skills
{
    [CreateAssetMenu(fileName = "PhysicalAttackSkill", menuName = "Skills/PhysicalAttackSkill")]
    public class PhysicalAttackSkill : OffensiveSkill
    {
        [SerializeField] private float _strengthScaling = 1.25f;
        [SerializeField] private int _fixedHealthPrice = 5;
        
        public override async UniTask UseSkill(CombatData combatData)
        {
            await UniTask.WaitForSeconds(0.5f);
            
            combatData.OtherUnit.UnitHealthController.TakeDamage(GetHitDamage(combatData));
            combatData.ActiveUnit.UnitHealthController.TakeDamage(_fixedHealthPrice);
            
            await UniTask.WaitForSeconds(0.5f);
        }

        public override bool CanUse(CombatData combatData) =>
            combatData.ActiveUnit.HealthData.CurrentHealth.Value >= _fixedHealthPrice;

        protected override int GetHitDamage(CombatData combatData)
        {
            int strength = combatData.ActiveUnit.UnitStatsData.Strength.Value;
            int additionalPower = Mathf.RoundToInt(_strengthScaling * strength);
            
            int finalAttackPower = BaseDamage + additionalPower;
            return finalAttackPower;
        }
    }
}