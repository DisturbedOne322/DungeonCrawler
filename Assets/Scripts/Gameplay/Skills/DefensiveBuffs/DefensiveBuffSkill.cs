using Cysharp.Threading.Tasks;
using Gameplay.Combat.Services;
using UnityEngine;

namespace Gameplay.Skills.DefensiveBuffs
{
    [CreateAssetMenu(fileName = "DefensiveBuffSkill", menuName = "Gameplay/Skills/Buffs/DefensiveBuffSkill")]
    public class DefensiveBuffSkill : Core.DefensiveBuffSkill
    {
        protected override UniTask PerformAction(CombatService combatService)
        {
            combatService.CombatBuffsService.AddDefensiveBuffTo(combatService.ActiveUnit,
                _defensiveBuffData);

            return UniTask.CompletedTask;
        }

        public override bool CanUse(CombatService combatService)
        {
            return !combatService.ActiveUnit.UnitActiveBuffsData.IsDefensiveBuffActive(_defensiveBuffData);
        }
    }
}