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
                TurnDurationLeft = new IntReactiveProperty(buffData.StatusEffectDurationData.TurnDurations),
                EffectExpirationType = buffData.StatusEffectDurationData.EffectExpirationType,
                PriorityType = buffData.Priority,
                StatusEffectData = buffData,
                DefensiveBuffData = buffData
            };
        }

        public override void Apply(IEntity activeUnit, IEntity otherUnit)
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