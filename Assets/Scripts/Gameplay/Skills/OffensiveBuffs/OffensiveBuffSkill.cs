using Cysharp.Threading.Tasks;
using Gameplay.Combat.Services;
using UnityEngine;

namespace Gameplay.Skills.OffensiveBuffs
{
    [CreateAssetMenu(fileName = "OffensiveBuffSkill", menuName = "Gameplay/Skills/Buffs/OffensiveBuffSkill")]
    public class OffensiveBuffSkill : Core.OffensiveBuffSkill
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