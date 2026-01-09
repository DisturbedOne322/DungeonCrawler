using Gameplay.Facades;
using Gameplay.StatusEffects.Core;
using Gameplay.Units;
using Helpers;

namespace Gameplay.StatusEffects.Buffs.Services
{
    public class StatusEffectsProcessor
    {
        private readonly PlayerUnit _playerUnit;

        public StatusEffectsProcessor(PlayerUnit playerUnit)
        {
            _playerUnit = playerUnit;
        }
        
        public void EnableStatusEffectsOnTrigger(ICombatant activeUnit, ICombatant otherUnit,
            StatusEffectTriggerType triggerType)
        {
            var heldStatusEffects = activeUnit.UnitHeldStatusEffectsData.All;

            for (var i = heldStatusEffects.Count - 1; i >= 0; i--)
            {
                var statusEffect = heldStatusEffects[i];
                if (statusEffect.TriggerType != triggerType)
                    continue;

                AddStatusEffect(activeUnit, otherUnit, statusEffect);
            }
        }
        
        public void ProcessTurn(ICombatant activeUnit)
        {
            var activeStatusEffects =
                activeUnit.UnitActiveStatusEffectsData.AllActiveStatusEffects;

            for (var i = activeStatusEffects.Count - 1; i >= 0; i--)
            {
                var instance = activeStatusEffects[i];

                if (instance.EffectExpirationType != StatusEffectExpirationType.TurnCount)
                    continue;

                if (instance.DurationLeft.Value == 0)
                    instance.Revert();

                instance.DurationLeft.Value--;
            }
        }

        public void RemoveStatusEffectsOnAction(IGameUnit activeUnit, StatusEffectExpirationType effectExpirationType)
        {
            if (!StatusEffectsHelper.IsExpirationTypeActionBased(effectExpirationType))
                return;

            var activeStatusEffects = activeUnit.UnitActiveStatusEffectsData.AllActiveStatusEffects;

            for (var i = activeStatusEffects.Count - 1; i >= 0; i--)
            {
                var statusEffect = activeStatusEffects[i];
                if (statusEffect.EffectExpirationType != effectExpirationType)
                    continue;

                statusEffect.Revert();
            }
        }

        public void ClearAllStatusEffects(IGameUnit activeUnit)
        {
            var allEffects = activeUnit.UnitActiveStatusEffectsData.AllActiveStatusEffects;

            for (var index = allEffects.Count - 1; index >= 0; index--)
            {
                var effect = allEffects[index];
                if (effect.EffectExpirationType == StatusEffectExpirationType.Permanent)
                    continue;

                effect.Revert();
            }
        }

        private void AddStatusEffect(ICombatant activeUnit, ICombatant otherUnit,
            BaseStatusEffectData statusEffectData)
        {
            var isIndependent = statusEffectData.StatusEffectReapplyType.HasFlag(StatusEffectReapplyType.Independent);

            var activeStatusEffects =
                activeUnit.UnitActiveStatusEffectsData.AllActiveStatusEffects;

            if (!isIndependent)
                for (var i = activeStatusEffects.Count - 1; i >= 0; i--)
                {
                    var activeBuff = activeStatusEffects[i];
                    if (activeBuff.StatusEffectData != statusEffectData)
                        continue;

                    var expirationType = activeBuff.StatusEffectData.StatusEffectDurationData.EffectExpirationType;
                    if (expirationType == StatusEffectExpirationType.Reapply)
                    {
                        activeBuff.Revert();
                        return;
                    }
                    
                    StatusEffectsHelper.ReapplyStatusEffect(activeBuff);
                    return;
                }

            statusEffectData.CreateInstance().Apply(activeUnit, otherUnit);
        }
    }
}