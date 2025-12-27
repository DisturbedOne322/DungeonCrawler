using System;
using Gameplay.Facades;
using Gameplay.StatusEffects.Buffs.StatBuffsCore;
using Gameplay.StatusEffects.Core;
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

        public override void Apply(IGameUnit activeUnit, IGameUnit otherUnit)
        {
            AffectedUnit = StatDebuffData.DebuffTarget == DebuffTarget.ThisUnit ? activeUnit : otherUnit;
            
            if (AffectedUnit == null)
                throw new Exception("TRIED TO APPLY STATUS EFFECT TO A NULL UNIT");

            ValueChange = UnitStatsModificationService.ModifyStat(AffectedUnit, StatDebuffData.DebuffedStatType, -ValueChange);
            AffectedUnit.UnitActiveStatusEffectsData.AddStatusEffect(this);
        }

        public override void Revert()
        {
            if (AffectedUnit == null)
                return;

            UnitStatsModificationService.ModifyStat(AffectedUnit, StatDebuffData.DebuffedStatType, -ValueChange);
            AffectedUnit.UnitActiveStatusEffectsData.RemoveStatusEffect(this);
        }
    }
}