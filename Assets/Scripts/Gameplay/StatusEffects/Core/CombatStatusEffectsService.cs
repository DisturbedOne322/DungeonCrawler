using Gameplay.Combat.Data;
using Gameplay.StatusEffects.Buffs.DefensiveCore;
using Gameplay.StatusEffects.Buffs.HitBuffsCore;

namespace Gameplay.StatusEffects.Core
{
    public class CombatStatusEffectsService
    {
        private readonly CombatData _combatData;

        public CombatStatusEffectsService(CombatData combatData)
        {
            _combatData = combatData;
        }
        
        public void AddOffensiveBuff(HitBuffData buffData)
        {
            buffData.CreateInstance().Apply(_combatData.ActiveUnit, _combatData.OtherUnit);
        }

        public void AddDefensiveBuff(DefensiveBuffData buffData)
        {
            buffData.CreateInstance().Apply(_combatData.ActiveUnit, _combatData.OtherUnit);
        }
    }
}