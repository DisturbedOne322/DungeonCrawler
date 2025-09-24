using Cysharp.Threading.Tasks;
using Data;
using UnityEngine;

namespace Gameplay.Combat.Skills
{
    [CreateAssetMenu(fileName = "ConcentrateSkill", menuName = "Skills/General/ConcentrateSkill")]
    public class ConcentrateSkill : BaseSkill
    {
        protected override async UniTask PerformAction(CombatService combatService)
        {
            combatService.ApplyConcentrateToActiveUnit();
        }

        public override bool CanUse(CombatService combatService) =>
            !combatService.ActiveUnit.UnitBuffsData.Concentrated.Value;
    }
}