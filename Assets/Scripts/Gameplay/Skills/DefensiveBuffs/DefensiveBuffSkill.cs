using Cysharp.Threading.Tasks;
using Data.Constants;
using Gameplay.Combat.Services;
using Gameplay.Skills.Core;
using UnityEngine;

namespace Gameplay.Skills.DefensiveBuffs
{
    [CreateAssetMenu(menuName = MenuPaths.GameplaySkillBuffs + "DefensiveBuffSkill")]
    public class DefensiveBuffSkill : BaseDefensiveBuffSkill
    {
        protected override UniTask PerformAction(CombatService combatService)
        {
            combatService.ApplyStatusEffectToActiveUnit(DefensiveBuffData);
            return UniTask.CompletedTask;
        }

        public override bool CanUse(CombatService combatService)
        {
            return !combatService.ActiveUnit.UnitActiveStatusEffectsContainer.IsStatusEffectActive(DefensiveBuffData);
        }
    }
}