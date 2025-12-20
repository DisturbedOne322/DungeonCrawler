using Gameplay.Facades;
using Gameplay.StatusEffects.Core;
using UniRx;

namespace Gameplay.StatusEffects.Buffs.HitBuffsCore
{
    public class HitBuffInstance : BaseStatusEffectInstance
    {
        public HitBuffData HitBuffData;
        public HitBuffPriorityType PriorityType;

        public static HitBuffInstance Create(HitBuffData buffData)
        {
            return new HitBuffInstance
            {
                DurationLeft = new IntReactiveProperty(buffData.StatusEffectDurationData.Duration),
                EffectExpirationType = buffData.StatusEffectDurationData.EffectExpirationType,
                PriorityType = buffData.Priority,
                StatusEffectData = buffData,
                HitBuffData = buffData
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