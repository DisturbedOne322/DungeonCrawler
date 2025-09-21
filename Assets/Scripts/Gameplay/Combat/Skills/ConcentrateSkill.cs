using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Gameplay.Combat.Skills
{
    [CreateAssetMenu(fileName = "ConcentrateSkill", menuName = "Skills/General/ConcentrateSkill")]
    public class ConcentrateSkill : BaseSkill
    {
        public override async UniTask UseSkill(CombatService combatService)
        {
            combatService.ApplyConcentrateToActiveUnit();
            await UniTask.WaitForSeconds(0.5f);
        }

        public override bool CanUse(CombatService combatService) =>
            !combatService.ActiveUnit.UnitBuffsData.Concentrated.Value;
    }
}