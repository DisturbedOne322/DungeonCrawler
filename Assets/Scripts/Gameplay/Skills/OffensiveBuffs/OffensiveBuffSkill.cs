using Cysharp.Threading.Tasks;
using Data.Constants;
using Gameplay.Combat.Services;
using Gameplay.Skills.Core;
using UnityEngine;

namespace Gameplay.Skills.OffensiveBuffs
{
    [CreateAssetMenu(menuName = MenuPaths.GameplaySkillBuffs + "OffensiveBuffSkill")]
    public class OffensiveBuffSkill : BaseOffensiveBuffSkill
    {
        protected override UniTask PerformAction(CombatService combatService)
        {
            combatService.ApplyStatusEffectToActiveUnit(HitBuffData);
            return UniTask.CompletedTask;
        }

        public override bool CanUse(CombatService combatService)
        {
            return !combatService.ActiveUnit.UnitActiveStatusEffectsContainer.IsStatusEffectActive(HitBuffData);
        }
    }
}