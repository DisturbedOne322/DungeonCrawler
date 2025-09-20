using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Gameplay.Combat.Skills
{
    [CreateAssetMenu(fileName = "MultiHitPhysicalAttack", menuName = "Skills/MultiHitPhysicalAttack")]
    public class MultiHitPhysicalAttack : OffensiveSkill
    {
        [SerializeField, Min(2)] private int _hits = 2;
        
        public override async UniTask UseSkill(CombatData combatData)
        {
            await UniTask.WaitForSeconds(0.5f);

            for (int i = 0; i < _hits; i++)
            {
                combatData.OtherUnit.UnitHealthController.TakeDamage(GetHitDamage(combatData));
                await UniTask.WaitForSeconds(0.15f);
            }
            
            await UniTask.WaitForSeconds(0.5f);
        }

        public override bool CanUse(CombatData combatData) => true;

        protected override int GetHitDamage(CombatData combatData) => BaseDamage;
    }
}