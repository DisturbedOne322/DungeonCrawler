using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Gameplay.Combat.Skills
{
    [CreateAssetMenu(fileName = "GuardSkill", menuName = "Skills/General/GuardSkill")]
    public class GuardSkill : BaseSkill
    {
        public override async UniTask UseSkill(CombatService combatService)
        {
            combatService.ApplyGuardToActiveUnit();
            await UniTask.WaitForSeconds(0.5f); 
        }

        public override bool CanUse(CombatService combatService) => true;
    }
}