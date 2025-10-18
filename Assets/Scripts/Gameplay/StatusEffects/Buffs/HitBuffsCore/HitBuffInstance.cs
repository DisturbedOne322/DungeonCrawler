using Gameplay.Facades;
using Gameplay.StatusEffects.Core;

namespace Gameplay.StatusEffects.Buffs.OffensiveCore
{
    public class HitBuffInstance : BaseStatusEffectInstance
    {
        public HitBuffData HitBuffData;
        public HitBuffPriorityType PriorityType;

        public static HitBuffInstance Create(HitBuffData buffData)
        {
            return new HitBuffInstance
            {
                TurnDurationLeft = buffData.StatusEffectDurationData.TurnDurations,
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

        public override void Revert() => AffectedUnit.UnitActiveStatusEffectsData.RemoveStatusEffect(this);
    }
}