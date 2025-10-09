using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Gameplay.Combat.Skills
{
    [CreateAssetMenu(fileName = "ChargeSkill", menuName = "Gameplay/Skills/General/ChargeSkill")]
    public class ChargeSkill : OffensiveBuffSkill
    {
        protected override UniTask PerformAction(CombatService combatService)
        {
            combatService.CombatBuffsService.AddOffensiveBuffTo(combatService.ActiveUnit, OffensiveBuffData);
            return UniTask.CompletedTask;
        }

        public override bool CanUse(CombatService combatService) =>
            !combatService.ActiveUnit.UnitActiveBuffsData.IsOffensiveBuffActive(OffensiveBuffData);
    }
}