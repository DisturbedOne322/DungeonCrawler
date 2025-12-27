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

        public override void Apply(ICombatant activeUnit, ICombatant otherUnit)
        {
            AffectedUnit = activeUnit;
            AffectedUnit.UnitActiveStatusEffectsData.AddStatusEffect(this);
        }

        public override void Revert()
        {
            AffectedUnit.UnitActiveStatusEffectsData.RemoveStatusEffect(this);
        }
    }
}