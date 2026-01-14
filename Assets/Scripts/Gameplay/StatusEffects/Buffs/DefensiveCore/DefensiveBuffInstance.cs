using Gameplay.Facades;
using Gameplay.StatusEffects.Core;
using UniRx;

namespace Gameplay.StatusEffects.Buffs.DefensiveCore
{
    public class DefensiveBuffInstance : BaseStatusEffectInstance
    {
        public DefensiveBuffData DefensiveBuffData;
        public DefensiveBuffPriorityType PriorityType;

        public static DefensiveBuffInstance Create(DefensiveBuffData buffData)
        {
            return new DefensiveBuffInstance
            {
                DurationLeft = new IntReactiveProperty(buffData.StatusEffectDurationData.Duration),
                EffectExpirationType = buffData.StatusEffectDurationData.EffectExpirationType,
                PriorityType = buffData.Priority,
                StatusEffectData = buffData,
                DefensiveBuffData = buffData
            };
        }

        protected override void ProcessApply(ICombatant activeUnit, ICombatant otherUnit)
        {
            activeUnit.UnitActiveStatusEffectsContainer.AddStatusEffect(this);
            Context.SetAffectedUnit(activeUnit);
        }

        protected override void ProcessRevert()
        {
            Context.AffectedUnit.UnitActiveStatusEffectsContainer.RemoveStatusEffect(this);
        }
    }
}