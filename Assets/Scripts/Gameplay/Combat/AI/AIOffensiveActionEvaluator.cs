using Gameplay.Combat.Data;
using Gameplay.Combat.Services;

namespace Gameplay.Combat.AI
{
    public class AIOffensiveActionEvaluator
    {
        private readonly DamageDealingService _damageDealingService;

        public AIOffensiveActionEvaluator(DamageDealingService damageDealingService)
        {
            _damageDealingService = damageDealingService;
        }

        public float GetOffensiveValue(AIContext context, HitDataStream hitDataStream)
        {
            var activeUnit = context.ActiveUnit;
            var otherUnit = context.OtherUnit;
            var config = context.Config;
            
            float damageSum = 0;

            foreach (HitData hitData in hitDataStream.Hits)
            {
                _damageDealingService.ProcessHit(activeUnit, otherUnit, hitData);

                if (!hitData.Missed)
                    damageSum += hitData.Damage;
            }

            int otherUnitMaxHp = otherUnit.UnitHealthData.MaxHealth.Value;
            
            float effectiveDamage = (damageSum * 1f / otherUnitMaxHp);
            float aggressionMultiplier = config.Aggression;
            
            float offensiveValue = effectiveDamage * aggressionMultiplier;
            return offensiveValue;
        }
    }
}