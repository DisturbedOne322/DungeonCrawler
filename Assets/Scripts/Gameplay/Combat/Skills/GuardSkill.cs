using Cysharp.Threading.Tasks;
using Data;
using UnityEngine;

namespace Gameplay.Combat.Skills
{
    [CreateAssetMenu(fileName = "GuardSkill", menuName = "Skills/General/GuardSkill")]
    public class GuardSkill : BaseSkill
    {
        protected override async UniTask PerformAction(CombatService combatService)
        {
            combatService.ApplyGuardToActiveUnit();
        }

        public override bool CanUse(CombatService combatService) => true;
    }
}