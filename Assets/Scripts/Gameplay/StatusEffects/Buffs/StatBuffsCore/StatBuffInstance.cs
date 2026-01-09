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
                DurationLeft = new IntReactiveProperty(buffDataData.StatusEffectDurationData.Duration),
                EffectExpirationType = buffDataData.StatusEffectDurationData.EffectExpirationType,
                StatType = buffDataData.BuffedStatType,
                StatusEffectData = buffDataData,
                ValueChange = valueChange
            };
        }

        protected override void ProcessApply(ICombatant activeUnit, ICombatant otherUnit)
        {
            AffectedUnit = activeUnit;

            ValueChange = UnitStatsModificationService.ModifyStat(AffectedUnit, StatType, ValueChange);
            AffectedUnit.UnitActiveStatusEffectsData.AddStatusEffect(this);
        }

        protected override void ProcessRevert()
        {
            UnitStatsModificationService.ModifyStat(AffectedUnit, StatType, -ValueChange);
            AffectedUnit.UnitActiveStatusEffectsData.RemoveStatusEffect(this);        
        }
    }
}