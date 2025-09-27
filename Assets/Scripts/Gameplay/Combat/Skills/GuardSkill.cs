using Cysharp.Threading.Tasks;
using Data;
using UnityEngine;

namespace Gameplay.Combat.Skills
{
    [CreateAssetMenu(fileName = "GuardSkill", menuName = "Gameplay/Skills/General/GuardSkill")]
    public class GuardSkill : BaseSkill
    {
        protected override UniTask PerformAction(CombatService combatService)
        {
            combatService.ApplyGuardToActiveUnit();
            return UniTask.CompletedTask;
        }

        public override bool CanUse(CombatService combatService) => true;
    }
}