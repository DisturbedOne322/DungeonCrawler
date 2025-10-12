using Cysharp.Threading.Tasks;
using Gameplay.Combat;
using Gameplay.Combat.Services;
using Gameplay.Skills.Core;
using UnityEngine;

namespace Gameplay.Skills.OffensiveBuffs
{
    [CreateAssetMenu(fileName = "ConcentrateSkill", menuName = "Gameplay/Skills/General/ConcentrateSkill")]
    public class ConcentrateSkill : OffensiveBuffSkill
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