using Cysharp.Threading.Tasks;
using Data;
using UnityEngine;

namespace Gameplay.Combat.Skills
{
    [CreateAssetMenu(fileName = "MultiHitPhysicalAttack", menuName = "Skills/MultiHitPhysicalAttack")]
    public class MultiHitPhysicalAttack : OffensiveSkill
    {
        [SerializeField, Min(2)] private int _hits = 2;
        
        protected override async UniTask PerformAction(CombatService combatService)
        {
            for (int i = 0; i < _hits - 1; i++)
            {
                await combatService.DealDamageToOtherUnit(GetSkillData(combatService.ActiveUnit), false);
                await UniTask.WaitForSeconds(0.15f);
            }
            
            await combatService.DealDamageToOtherUnit(GetSkillData(combatService.ActiveUnit));
            await UniTask.WaitForSeconds(0.15f);
        }

        public override bool CanUse(CombatService combatService) => true;
    }
}