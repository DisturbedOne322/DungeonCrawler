using Data;
using Gameplay.Facades;
using Gameplay.StatusEffects.Buffs.StatBuffsCore;
using Gameplay.StatusEffects.Core;

namespace Gameplay.StatusEffects.Debuffs.Core
{
    public class StatDebuffInstance : BaseStatusEffectInstance
    {
        public StatDebuffData StatDebuffData;
        public StatType StatType;
        public float ValueChange;
        
        public static StatDebuffInstance Create(StatDebuffData buffDataData, float valueChange)
        {
            return new StatDebuffInstance
            {
                TurnDurationLeft = buffDataData.StatusEffectDurationData.TurnDurations,
                EffectExpirationType = buffDataData.StatusEffectDurationData.EffectExpirationType,
                StatType = buffDataData.DebuffedStatType,
                StatusEffectData = buffDataData,
                StatDebuffData = buffDataData,
                ValueChange = valueChange,
            };
        }

        public override void Apply(IEntity activeUnit, IEntity otherUnit)
        {
            AffectedUnit = otherUnit;
            
            ValueChange = UnitStatsModificationService.ModifyStat(AffectedUnit, StatType, -ValueChange);
            AffectedUnit.UnitActiveStatusEffectsData.AddStatusEffect(this);
        }

        public override void Revert()
        {
            UnitStatsModificationService.ModifyStat(AffectedUnit, StatType, -ValueChange);
            AffectedUnit.UnitActiveStatusEffectsData.RemoveStatusEffect(this);
        }
    }
}