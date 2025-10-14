using Cysharp.Threading.Tasks;
using Gameplay.Combat;
using Gameplay.Combat.Services;
using Gameplay.Skills.Core;
using UnityEngine;

namespace Gameplay.Skills.DefensiveBuffs
{
    [CreateAssetMenu(fileName = "DefensiveBuffSkill", menuName = "Gameplay/Skills/Buffs/DefensiveBuffSkill")]
    public class DefensiveBuffSkill : Core.DefensiveBuffSkill
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