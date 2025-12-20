using Gameplay.Facades;
using Gameplay.StatusEffects.Core;
using UniRx;

namespace Gameplay.StatusEffects.Buffs.HitStreamBuffsCore
{
    public class HitStreamBuffInstance : BaseStatusEffectInstance
    {
        public HitStreamBuffData HitStreamBuffData;
        public HitStreamBuffPriorityType PriorityType;

        public static HitStreamBuffInstance Create(HitStreamBuffData buffData)
        {
            return new HitStreamBuffInstance
            {
                DurationLeft = new IntReactiveProperty(buffData.StatusEffectDurationData.Duration),
                EffectExpirationType = buffData.StatusEffectDurationData.EffectExpirationType,
                StatusEffectData = buffData,
                PriorityType = buffData.Priority,
                HitStreamBuffData = buffData
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