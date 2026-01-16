using Gameplay.Combat.Data;
using Gameplay.Combat.Services;
using Gameplay.StatusEffects.Buffs.DefensiveCore;
using Gameplay.StatusEffects.Buffs.HitBuffsCore;
using UnityEngine;

namespace Gameplay.Combat.AI
{
    public class AIActionEvaluationService
    {
        private readonly AIHealingActionEvaluator _healingEvaluator;
        private readonly AIOffensiveActionEvaluator _offensiveActionEvaluator;
        private readonly AIBuffActionEvaluator _buffActionEvaluator;

        public AIActionEvaluationService(DamageDealingService damageDealingService, CombatService combatService)
        {
            _healingEvaluator = new ();
            _offensiveActionEvaluator = new(damageDealingService);
            _buffActionEvaluator = new(combatService);
        }
        
        public float GetHealingValue(AIContext context, int healAmount)
        {
            return Clamp(_healingEvaluator.GetHealingValue(context, healAmount));
        }

        public float GetHealingValue(AIContext context, float healPercent)
        {
            return Clamp(_healingEvaluator.GetHealingValue(context, healPercent));
        }

        public float GetOffensiveValue(AIContext context, HitDataStream hitDataStream)
        {
            return Clamp(_offensiveActionEvaluator.GetOffensiveValue(context, hitDataStream));
        }

        public float GetOffensiveBuffValue(AIContext context, HitBuffData buffData)
        {
            return Clamp(_buffActionEvaluator.GetOffensiveBuffValue(context, buffData));
        }

        public float GetDefensiveBuffValue(AIContext context, DefensiveBuffData buffData)
        {
            return Clamp(_buffActionEvaluator.GetDefensiveBuffValue(context, buffData));
        }
        
        private float Clamp(float value) => Mathf.Clamp01(value);
    }
}