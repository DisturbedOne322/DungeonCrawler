using Data;
using Gameplay.Facades;
using Gameplay.StatusEffects.Core;
using UniRx;
using UnityEngine;

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
            var affectedUnit = activeUnit;
            
            ValueChange = UnitStatsModificationService.ModifyStat(affectedUnit, StatType, ValueChange);
            affectedUnit.UnitActiveStatusEffectsContainer.AddStatusEffect(this);
            
            Context.SetAffectedUnit(affectedUnit);
        }

        protected override void ProcessRevert()
        {
            var affectedUnit = Context.AffectedUnit;
            
            UnitStatsModificationService.ModifyStat(affectedUnit, StatType, -ValueChange);
            affectedUnit.UnitActiveStatusEffectsContainer.RemoveStatusEffect(this);        
        }
    }
}