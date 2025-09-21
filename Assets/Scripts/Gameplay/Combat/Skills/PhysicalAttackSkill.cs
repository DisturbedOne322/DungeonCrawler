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
            
            service.DealDamageTo(service.ActiveUnit, _fixedHealthPrice);
            service.DealDamageTo(service.OtherUnit, GetHitDamage(service.ActiveUnit.UnitStatsData));
            
            await UniTask.WaitForSeconds(0.5f);
        }

        public override bool CanUse(CombatService combatService) =>
            combatService.ActiveUnit.HealthData.CurrentHealth.Value >= _fixedHealthPrice;

        protected override int GetHitDamage(UnitStatsData statsData)
        {
            int strength = statsData.Strength.Value;
            int additionalPower = Mathf.RoundToInt(_strengthScaling * strength);
            
            int finalAttackPower = BaseDamage + additionalPower;
            return finalAttackPower;
        }
    }
}