using Cysharp.Threading.Tasks;
using Gameplay.Combat.Data;
using UnityEngine;

namespace Gameplay.Combat.Skills
{
    [CreateAssetMenu(fileName = "PhysicalAttackSkill", menuName = "Skills/PhysicalAttackSkill")]
    public class PhysicalAttackSkill : OffensiveSkill
    {
        [SerializeField] private float _strengthScaling = 1.25f;
        [SerializeField] private int _fixedHealthPrice = 5;
        
        public override async UniTask UseSkill(CombatService service)
        {
            await UniTask.WaitForSeconds(0.5f);
            
            service.DealDamageToActiveUnit(_fixedHealthPrice, true);
            service.DealDamageToOtherUnit(GetHitDamage(service.ActiveUnit.UnitStatsData));
            
            await UniTask.WaitForSeconds(0.5f);
        }

        public override bool CanUse(CombatService combatService)
        {
            int currentHealth = combatService.ActiveUnit.HealthData.CurrentHealth.Value;
            return currentHealth > _fixedHealthPrice;
        }

        protected override int GetHitDamage(UnitStatsData statsData)
        {
            int strength = statsData.Strength.Value;
            int additionalPower = Mathf.RoundToInt(_strengthScaling * strength);
            
            int finalAttackPower = BaseDamage + additionalPower;
            return finalAttackPower;
        }
    }
}