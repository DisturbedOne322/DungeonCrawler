using Cysharp.Threading.Tasks;
using Data;
using UnityEngine;

namespace Gameplay.Combat.Skills
{
    [CreateAssetMenu(fileName = "GuardSkill", menuName = "Gameplay/Skills/General/GuardSkill")]
    public class GuardSkill : DefensiveBuffSkill
    {
        protected override UniTask PerformAction(CombatService combatService)
        {
            combatService.CombatBuffsService.AddDefensiveBuffTo(combatService.ActiveUnit, 
                DefensiveBuffData);
            
            return UniTask.CompletedTask;
        }

        public override bool CanUse(CombatService combatService) => 
            !combatService.ActiveUnit.UnitActiveBuffsData.IsDefensiveBuffActive(DefensiveBuffData);
    }
}