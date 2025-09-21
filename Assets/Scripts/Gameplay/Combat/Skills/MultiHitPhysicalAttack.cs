using Cysharp.Threading.Tasks;
using Gameplay.Combat.Data;
using UnityEngine;

namespace Gameplay.Combat.Skills
{
    [CreateAssetMenu(fileName = "MultiHitPhysicalAttack", menuName = "Skills/MultiHitPhysicalAttack")]
    public class MultiHitPhysicalAttack : OffensiveSkill
    {
        [SerializeField, Min(2)] private int _hits = 2;
        
        public override async UniTask UseSkill(CombatService combatService)
        {
            await UniTask.WaitForSeconds(0.5f);

            for (int i = 0; i < _hits; i++)
            {
                combatService.DealDamageTo(combatService.OtherUnit, GetHitDamage(combatService.ActiveUnit.UnitStatsData));
                await UniTask.WaitForSeconds(0.15f);
            }
            
            await UniTask.WaitForSeconds(0.5f);
        }

        public override bool CanUse(CombatService combatService) => true;

        protected override int GetHitDamage(UnitStatsData statsData) => BaseDamage;
    }
}