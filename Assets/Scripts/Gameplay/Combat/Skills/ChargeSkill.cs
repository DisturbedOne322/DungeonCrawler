using Cysharp.Threading.Tasks;
using Data;
using UnityEngine;

namespace Gameplay.Combat.Skills
{
    [CreateAssetMenu(fileName = "ChargeSkill", menuName = "Skills/General/ChargeSkill")]
    public class ChargeSkill : BaseSkill
    {
        protected override UniTask PerformAction(CombatService combatService)
        {
            combatService.ApplyChargeToActiveUnit();
            return UniTask.CompletedTask;
        }

        public override bool CanUse(CombatService combatService) =>
            !combatService.ActiveUnit.UnitBuffsData.Charged.Value;
    }
}