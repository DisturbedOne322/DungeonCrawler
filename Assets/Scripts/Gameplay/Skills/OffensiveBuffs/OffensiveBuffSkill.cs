using Cysharp.Threading.Tasks;
using Gameplay.Combat.Services;
using Gameplay.Skills.Core;
using UnityEngine;

namespace Gameplay.Skills.OffensiveBuffs
{
    [CreateAssetMenu(fileName = "OffensiveBuffSkill", menuName = "Gameplay/Skills/Buffs/OffensiveBuffSkill")]
    public class OffensiveBuffSkill : BaseOffensiveBuffSkill
    {
        protected override UniTask PerformAction(CombatService combatService)
        {
            combatService.CombatStatusEffectsService.AddOffensiveBuff(HitBuffData);
            return UniTask.CompletedTask;
        }

        public override bool CanUse(CombatService combatService)
        {
            return !combatService.ActiveUnit.UnitActiveStatusEffectsData.IsStatusEffectActive(HitBuffData);
        }
    }
}