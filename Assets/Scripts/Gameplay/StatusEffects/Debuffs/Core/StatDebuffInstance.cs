using System;
using Gameplay.Facades;
using Gameplay.StatusEffects.Buffs.StatBuffsCore;
using Gameplay.StatusEffects.Core;
using Helpers;
using UniRx;

namespace Gameplay.StatusEffects.Debuffs.Core
{
    public class StatDebuffInstance : BaseStatusEffectInstance
    {
        public StatDebuffData StatDebuffData;
        public float ValueChange;

        public static StatDebuffInstance Create(StatDebuffData buffDataData, float valueChange)
        {
            return new StatDebuffInstance
            {
                DurationLeft = new IntReactiveProperty(buffDataData.StatusEffectDurationData.Duration),
                EffectExpirationType = buffDataData.StatusEffectDurationData.EffectExpirationType,
                StatusEffectData = buffDataData,
                StatDebuffData = buffDataData,
                ValueChange = valueChange
            };
        }

        protected override void ProcessApply(ICombatant activeUnit, ICombatant otherUnit)
        {
            var affectedUnit = StatDebuffData.DebuffTarget == DebuffTarget.ThisUnit ? activeUnit : otherUnit;

            if (affectedUnit == null)
            {
                DebugHelper.LogError("TRIED TO APPLY STATUS EFFECT TO A NULL UNIT");
                return;
            }

            
            ValueChange = UnitStatsModificationService.ModifyStat(affectedUnit, StatDebuffData.DebuffedStatType, -ValueChange);
            affectedUnit.UnitActiveStatusEffectsContainer.AddStatusEffect(this);    
            
            Context.SetAffectedUnit(affectedUnit);
        }

        protected override void ProcessRevert()
        {
            var affectedUnit = Context.AffectedUnit;
            if (affectedUnit == null)
                return;

            UnitStatsModificationService.ModifyStat(affectedUnit, StatDebuffData.DebuffedStatType, -ValueChange);
            affectedUnit.UnitActiveStatusEffectsContainer.RemoveStatusEffect(this);
        }
    }
}