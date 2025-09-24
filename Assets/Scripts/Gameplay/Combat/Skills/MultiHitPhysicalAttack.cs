using Cysharp.Threading.Tasks;
using Gameplay.Combat.Data;
using UnityEngine;

namespace Gameplay.Combat.Skills
{
    [CreateAssetMenu(fileName = "MultiHitPhysicalAttack", menuName = "Skills/MultiHitPhysicalAttack")]
    public class MultiHitPhysicalAttack : OffensiveSkill
    {
        [SerializeField, Min(2)] private int _hits = 2;
        
        public override async UniTask UseAction(CombatService combatService)
        {
            await UniTask.WaitForSeconds(0.5f);

            for (int i = 0; i < _hits; i++)
            {
                combatService.DealDamageToOtherUnit(GetSkillData(combatService.ActiveUnit.UnitStatsData));
                await UniTask.WaitForSeconds(0.15f);
            }
            
            await UniTask.WaitForSeconds(0.5f);
        }

        public override bool CanUse(CombatService combatService) => true;
    }
}