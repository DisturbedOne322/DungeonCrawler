using Cysharp.Threading.Tasks;
using Gameplay.Combat.Services;
using Gameplay.Skills.Core;
using UnityEngine;

namespace Gameplay.Skills.DefensiveBuffs
{
    [CreateAssetMenu(fileName = "DefensiveBuffSkill", menuName = "Gameplay/Skills/Buffs/DefensiveBuffSkill")]
    public class DefensiveBuffSkill : BaseDefensiveBuffSkill
    {
        protected override UniTask PerformAction(CombatService combatService)
        {
            combatService.CombatStatusEffectsService.AddDefensiveBuff(DefensiveBuffData);
            return UniTask.CompletedTask;
        }

        public override bool CanUse(CombatService combatService) => !combatService.ActiveUnit.UnitActiveStatusEffectsData.IsStatusEffectActive(DefensiveBuffData);
    }
}