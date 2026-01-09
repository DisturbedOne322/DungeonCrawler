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

        protected override void ProcessApply(ICombatant activeUnit, ICombatant otherUnit)
        {
            AffectedUnit = activeUnit;
            AffectedUnit.UnitActiveStatusEffectsData.AddStatusEffect(this);
        }

        protected override void ProcessRevert()
        {
            AffectedUnit.UnitActiveStatusEffectsData.RemoveStatusEffect(this);
        }
    }
}