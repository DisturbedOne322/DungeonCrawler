using Cysharp.Threading.Tasks;
using Gameplay.Combat.Data;
using UnityEngine;

namespace Gameplay.Combat.Skills
{
    [CreateAssetMenu(fileName = "BaseAttackSkill", menuName = "Skills/BaseAttackSkill")]
    public class BaseAttackSkill : OffensiveSkill
    {
        [SerializeField] private float _strengthScaling = 1;
        
        public override async UniTask UseAction(CombatService combatService)
        {
            await UniTask.WaitForSeconds(0.5f);
            
            combatService.DealDamageToOtherUnit(GetHitDamage(combatService.ActiveUnit.UnitStatsData));
            
            await UniTask.WaitForSeconds(0.5f);
        }
        
        public override bool CanUse(CombatService combatService) => true;
        protected override int GetHitDamage(UnitStatsData statsData)
        {
            int strength = statsData.Strength.Value;
            int additionalPower = Mathf.RoundToInt(_strengthScaling * strength);
            
            int finalAttackPower = BaseDamage + additionalPower;
            return finalAttackPower;
        }
    }
}