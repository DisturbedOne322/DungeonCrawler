using Data;
using Gameplay.Facades;
using Gameplay.StatusEffects.Core;
using UniRx;

namespace Gameplay.StatusEffects.Buffs.StatBuffsCore
{
    public class StatBuffInstance : BaseStatusEffectInstance
    {
        public StatType StatType;
        public float ValueChange;

        public static StatBuffInstance Create(StatBuffData buffDataData, float valueChange)
        {
            return new StatBuffInstance
            {
                TurnDurationLeft = new IntReactiveProperty(buffDataData.StatusEffectDurationData.TurnDurations),
                EffectExpirationType = buffDataData.StatusEffectDurationData.EffectExpirationType,
                StatType = buffDataData.BuffedStatType,
                StatusEffectData = buffDataData,
                ValueChange = valueChange
            };
        }

        public override void Apply(IEntity activeUnit, IEntity otherUnit)
        {
            AffectedUnit = activeUnit;

            ValueChange = UnitStatsModificationService.ModifyStat(AffectedUnit, StatType, ValueChange);
            AffectedUnit.UnitActiveStatusEffectsData.AddStatusEffect(this);
        }

        public override void Revert()
        {
            UnitStatsModificationService.ModifyStat(AffectedUnit, StatType, -ValueChange);
            AffectedUnit.UnitActiveStatusEffectsData.RemoveStatusEffect(this);
        }
    }
}