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